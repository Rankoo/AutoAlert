# Manual de Instalación y Configuración - AutoPago

## 1. Propósito y Alcance del Documento

### Propósito
El propósito de este documento es proporcionar una guía técnica detallada, paso a paso, para la correcta instalación, configuración y puesta en marcha del **Sistema de Información para la Automatización de Pagos (AutoPago / AutoAlert)**. Este manual está diseñado para asegurar que el despliegue del aplicativo se realice de manera estandarizada, minimizando errores y garantizando que todos los componentes (Frontend, Backend y Base de Datos) se comuniquen de forma correcta y segura.

### Alcance
Este manual cubre exclusivamente el proceso de instalación y configuración de la plataforma utilizando la arquitectura de contenedores provista (Docker y Docker Compose). Abarca desde la preparación del entorno y la configuración de las variables de seguridad, hasta el levantamiento de los servicios y la verificación del correcto funcionamiento del sistema. 

El documento está dirigido a:
- **Desarrolladores e Ingenieros de Software:** Que requieran levantar el entorno en sus máquinas locales para desarrollo o pruebas.
- **Administradores de Sistemas / DevOps:** Encargados de preparar los entornos de pruebas (Staging) o de producción.
- **Soporte Técnico:** Como material de referencia para solucionar problemas iniciales de despliegue y conectividad.

Este manual **no** cubre instrucciones sobre el uso funcional de la plataforma (para ello, refiérase al *Manual de Usuario*) ni explicaciones profundas a nivel de código fuente (refiérase a la *Documentación de Arquitectura*).

## 2. Requisitos Previos (Pre-requisitos)

Para asegurar el correcto despliegue y funcionamiento de AutoPago, el servidor o equipo local donde se instalará debe cumplir con los siguientes requisitos mínimos:

### 2.1 Requisitos de Hardware
Debido a que el sistema utiliza múltiples contenedores (SQL Server suele demandar recursos), se recomienda:
- **Procesador:** Intel Core i5 / AMD Ryzen 5 o superior (mínimo 4 núcleos).
- **Memoria RAM:** Mínimo 8 GB (Recomendado 16 GB para un entorno fluido y sin cuellos de botella).
- **Almacenamiento:** Mínimo 20 GB de espacio libre (preferiblemente en un disco SSD para acelerar la creación de imágenes y base de datos).

### 2.2 Requisitos de Software
El sistema está orquestado completamente, por lo que la instalación de dependencias se reduce al motor de contenedores:
- **Sistema Operativo:** Windows 10/11 (con WSL2 habilitado), macOS o Linux (Ubuntu/Debian recomendado para producción).
- **Docker:** Docker Engine instalado y ejecutándose (Docker Desktop es la opción más sencilla para Windows y Mac).
- **Docker Compose:** Debe estar instalado (generalmente incluido en Docker Desktop).
- **Git (Opcional pero recomendado):** Para clonar el repositorio y gestionar las actualizaciones del código fuente.

### 2.3 Puertos de Red
El entorno requiere que los siguientes puertos estén libres y no sean bloqueados por firewalls para la comunicación entre los servicios y el usuario:
- **Puerto 3000:** Para el acceso a la aplicación web (Frontend en React).
- **Puerto 5162 (o el definido en Docker):** Para el acceso a la API (Backend en .NET).
- **Puerto 1433:** Para la conexión a la base de datos (SQL Server). *Nota: En entornos de producción, este puerto no debe estar expuesto a Internet, solo a la red interna.*

## 3. Obtención del Código Fuente

El primer paso es obtener los archivos del proyecto que contienen la configuración de contenedores, la base de datos, el backend y el frontend.

1. **Abrir la terminal o consola de comandos.**
2. **Navegar al directorio** donde se desea alojar el proyecto:
   ```bash
   cd /ruta/de/tu/preferencia
   ```
3. **Clonar el repositorio** utilizando Git:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd AutoAlert
   ```
   *(Nota: Si no utilizas Git, puedes descargar el código fuente en formato .zip desde el repositorio y extraerlo en una carpeta de tu equipo).*

Al ingresar al directorio raíz del proyecto (`AutoAlert`), deberías visualizar carpetas como `Codigo`, `Base de Datos`, `Documentacion` y el archivo principal `docker-compose.yml`.

## 4. Configuración del Entorno (Variables y Credenciales)

Antes de levantar el sistema, es fundamental configurar las variables de entorno que controlan la seguridad (contraseñas, cifrado JWT) y las conexiones a la base de datos.

### 4.1 Configuración del archivo `.env`
En la raíz del proyecto existe un archivo de plantilla llamado `.env.example`. Este archivo sirve de guía para las variables requeridas.
1. Haz una copia del archivo `.env.example` y nómbralo **`.env`** (asegúrate de que tenga el punto inicial).
2. Abre el archivo `.env` con un editor de texto (como VS Code o Notepad) y configura los siguientes parámetros de forma segura:

```env
# Configuración Base de Datos
SA_USER=sa
SA_PASSWORD=Tu_Password_Seguro_123!
DB_NAME=AutoAlertDB
MSSQL_PID=Developer

# Configuración JWT (Autenticación)
JWT_KEY=Clave_Secreta_Muy_Larga_Y_Segura_Para_JWT
JWT_ISSUER=AutoAlertIssuer
JWT_AUDIENCE=AutoAlertAudience
```
*Advertencia: En entornos de producción, asegúrate de utilizar contraseñas fuertes y complejas. SQL Server requiere que la contraseña (`SA_PASSWORD`) cumpla con políticas estrictas de complejidad (mayúsculas, minúsculas, números y símbolos).*

### 4.2 Verificación del Backend y Frontend
El archivo `docker-compose.yml` ya está preconfigurado para inyectar estas variables hacia el Backend y el Frontend. 
- **Backend:** Enlaza automáticamente la cadena de conexión a SQL Server utilizando el usuario y contraseña del archivo `.env`.
- **Frontend:** Utilizará el puerto `3000` y el entorno predeterminado definido en su `Dockerfile`. No requiere variables adicionales a menos que se desee apuntar a una API externa.
