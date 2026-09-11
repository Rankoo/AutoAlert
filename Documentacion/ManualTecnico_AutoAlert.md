# MANUAL TÉCNICO
**Sistema de Información: AutoAlert**

---

## 1. Propósito
El propósito principal de este manual técnico es demostrar toda la estructuración física, técnica, lógica y funcional del sistema de información **AutoAlert**, identificando restricciones, riesgos, respaldos y usuarios que lo integran, proporcionando una guía para las actividades técnicas del sistema.

## 2. Introducción
**AutoAlert** (también conocido como AutoPago) es un aplicativo diseñado para la gestión y notificación de alertas de servicios públicos. Su arquitectura abarca un Backend desarrollado en ASP.NET Core (.NET 8), un Frontend moderno en React 19 con Vite, y una base de datos Microsoft SQL Server orquestada mediante Docker Compose. Su función principal es centralizar y automatizar el proceso de control de servicios asociados a diferentes empresas y tiendas, gestionando usuarios, roles y permisos de acceso para notificaciones oportunas de pagos.

### 2.1. Objetivo general del sistema
Desarrollar e implementar una plataforma tecnológica robusta, escalable y segura que centralice la información de servicios públicos por sucursales y empresas, emitiendo alertas automatizadas para prevenir vencimientos y cortes.

### 2.2. Objetivos específicos
- Proveer una arquitectura basada en contenedores (Docker) para un rápido despliegue y configuración.
- Implementar un sistema de autenticación seguro basado en JWT (JSON Web Tokens) y Google Auth.
- Diseñar un modelo de base de datos relacional para gestionar grupos empresariales, compañías, tiendas y alertas.
- Desarrollar una interfaz de usuario reactiva, amigable e intuitiva utilizando React, Zustand y TailwindCSS.

## 3. Documentos de referencia
- Script de creación de Base de Datos: `scriptAutoAlertDB.sql`
- Orquestación y arquitectura de contenedores: `docker-compose.yml`
- Modelo de arquitectura Frontend: package.json (Vite/React) y Backend: `.csproj` (.NET 8/Entity Framework).

## 4. Definiciones importantes

### 4.2 Procesos de entrada y salida
**Entradas:**
- Datos de registro de usuarios, roles, empresas y tiendas.
- Credenciales de inicio de sesión y tokens JWT.
- Configuración de alertas y facturas de servicios públicos.
- Archivos de configuración (`appsettings.json`, `.env`).

**Salidas:**
- Estados de servicios y alertas generadas.
- Mensajes de autenticación y validación.
- Listados e informes extraídos desde la Base de Datos.
- Interfaz gráfica en el cliente web.

## 5. Descripción de módulos

### 5.1 Módulo Gestión de Empresas y Tiendas
- **Funcionalidad/Propósito:** Permite la administración de grupos empresariales, las compañías que los integran y las diferentes tiendas/sucursales físicas. 
- **Dependencias funcionales:** Depende de la base de datos central y es prerrequisito para la asignación de servicios y alertas.

### 5.2 Módulo de Usuarios y Seguridad (Auth)
- **Funcionalidad/Propósito:** Administra el registro de usuarios, autenticación por JWT o Google, asignación de roles y control fino de permisos por submódulos (`RoleSubModules`, `UserSubmodules`).
- **Dependencias funcionales:** Base principal del sistema. Interactúa con casi todos los demás módulos para verificar permisos y autorizaciones de las peticiones HTTP (Axios/API REST).

### 5.3 Módulo de Servicios y Alertas
- **Funcionalidad/Propósito:** Controla el registro de servicios públicos, facturas, montos, fechas de vencimiento y programa alertas para envíos mediante canales (WhatsApp, SMS, Email).
- **Dependencias funcionales:** Depende estrechamente de la estructura de tiendas, ya que los servicios están asociados a una sucursal específica (`StoreId`).

## 6. Diccionario de datos

### 6.1 Modelo entidad-relación y 6.2 Diagrama UML
*El modelo está compuesto principalmente por las siguientes entidades interconectadas:*
- `Groups` (1:N) `Companies` (1:N) `Stores` (1:N) `Services` (1:N) `Alerts`.
- `Roles` y `Users`, con catálogos `DocumentTypes` y estructuras de acceso mediante `Modules` y `SubModules`.

### 6.3 Distribución física y lógica de base de datos
**Base de Datos:** AutoAlertDB
- **Ubicación:** Contenedor Docker (Volumen: `dbdata:/var/opt/mssql`)
- **Motor:** Microsoft SQL Server 2022
- **Codificación:** Por defecto SQL Server (Collation estándar)
- **Tablas Principales:** Groups, Companies, Stores, DocumentTypes, Roles, Users, Modules, SubModules, RoleSubModules, UserSubmodules, Services, Alerts.

### 6.4 Restricciones Especiales
- Contraseñas almacenadas de forma segura (Hash) usando `BCrypt`.
- Relaciones restrictivas a través de Llaves Foráneas (Ej: no se puede borrar una Tienda si tiene Servicios asociados).
- La validación de unicidad en los correos electrónicos de la tabla `Users` garantiza que no existan duplicados.

## 7. Políticas de respaldo
- **Archivos y Código:** Repositorio bajo control de versiones Git.
- **Base de Datos:** El volumen persistente de Docker `dbdata` garantiza que los datos perduren ante la destrucción del contenedor. Se recomienda agendar *dumps* semanales `.bak` usando herramientas de SQL Server Management.

## 8. Descripción de interfaces con otros sistemas
- **Frontend ↔ Backend:** Comunicación mediante peticiones HTTP/HTTPS con la librería **Axios** (Base URL provista en `VITE_API_URL`), transmitiendo `Bearer Tokens`.
- **Backend ↔ SQL Server:** Comunicación implementando el ORM **Entity Framework Core 9**.
- **Autenticación Externa:** Integración con Google Authentication.

## 9. Instalación y configuración

### 9.1 Requisitos generales pre-instalación
**Hardware:**
- Procesador: Core i5/Ryzen 5 en adelante.
- Memoria RAM: Mínimo 8GB (Recomendado 16GB por Docker).
- Almacenamiento: SSD 256GB mínimo.

**Software:**
- **Sistema Operativo:** Windows 10/11, Linux o macOS.
- **Docker y Docker Compose:** (Docker Desktop activado).
- **Entorno Local (opcional):** Node.js 20+, .NET 8 SDK, IDE (Visual Studio / VS Code).

### 9.2 Detalles del proceso de instalación
1. Clonar el repositorio `AutoAlert`.
2. Dirigirse al directorio raíz donde se encuentra `docker-compose.yml`.
3. Renombrar `.env.example` a `.env` y configurar las credenciales seguras (SA_PASSWORD, SA_USER, JWT_KEY, etc.).
4. Ejecutar el comando para levantar la infraestructura:
   ```bash
   docker compose up -d --build
   ```
5. El servidor SQL se inicializará y el script `entrypoint.sh` cargará la estructura desde `scriptAutoAlertDB.sql`.
6. Frontend estará disponible en: `http://localhost:3000`
7. Backend API (Swagger) estará disponible en: `http://localhost:5162/swagger`

### 9.3 Detalles de configuración de la aplicación
- **Variables de Entorno (.env):** Maneja credenciales de Base de datos (`SA_PASSWORD`, `DB_NAME`), y del entorno del JWT (`JWT_KEY`, `JWT_ISSUER`, `JWT_AUDIENCE`).
- **appsettings.json (Backend):** Configuración de Cadenas de conexión (ConnectionStrings).
- **vite.config.ts / package.json (Frontend):** Dependencias y configuraciones de compilación de React 19.

## 10. Procesos de continuidad y contingencia
- En caso de caída de contenedores, Docker Compose está orquestado para auto-recuperar los servicios o facilitar el reinicio ágil (`docker compose restart`).
- Los logs del backend y de SQL Server pueden ser trazados utilizando `docker logs <container_name>`.

## 11. Descripción de usuarios

**Nombre del usuario de BD:** SA (System Administrator) o usuario definido en el `.env`
- **Descripción:** Usuario maestro orquestador de SQL Server. Dispone de permisos completos de lectura/escritura/modificación de la estructura DDL y DML.

**Usuarios de la Aplicación:**
- **Administrador:** Tiene acceso a la gestión de Grupos, Empresas, Módulos y Usuarios.
- **Usuarios de sucursal / Estándar:** Limitados mediante la matriz de `RoleSubModules` y `UserSubmodules` a tareas específicas (ej. registro de facturas o revisión de alertas programadas).
