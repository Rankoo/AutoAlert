# MANUAL DE MANTENIMIENTO
**Sistema de Información: AutoAlert**

---

## 1. Introducción

### 1.1 Propósito del manual
El propósito de este Manual de Mantenimiento es establecer los lineamientos, procedimientos y rutinas técnicas necesarias para garantizar el correcto funcionamiento, la disponibilidad y el rendimiento óptimo del sistema de información **AutoAlert**. Este documento sirve como guía técnica para el equipo encargado de administrar la plataforma, detallando las tareas proactivas y reactivas esenciales para preservar la integridad de los datos, la estabilidad de la orquestación en contenedores y la fluidez de los procesos de gestión y alerta de pagos de servicios públicos.

### 1.2 Alcance
Este documento aborda los procedimientos técnicos aplicables a toda la infraestructura que soporta a AutoAlert, tanto en el cliente (Frontend), servidor (Backend) y motor de persistencia (Base de Datos). Su alcance específico abarca:
- **Mantenimiento Preventivo:** Acciones programadas como la realización de copias de seguridad (backups) de SQL Server, monitoreo de volúmenes, limpieza de recursos inactivos de Docker y actualización de dependencias de software (paquetes NuGet y NPM).
- **Mantenimiento Correctivo:** Protocolos para el diagnóstico de problemas (troubleshooting), lectura e interpretación de logs de los contenedores y los pasos para la recuperación de los servicios ante posibles caídas.
- **Mantenimiento Adaptativo/Evolutivo:** Directrices para el despliegue seguro de nuevas versiones de la aplicación y la ejecución de migraciones en los esquemas de la base de datos.

*Nota:* Se excluyen de este manual las instrucciones operativas dirigidas a los usuarios finales de la plataforma, las cuales se encuentran en el Manual de Usuario.

### 1.3 Perfiles de usuario
Las actividades descritas en este documento requieren de personal con conocimientos técnicos especializados. Los roles principales responsables de ejecutar estas rutinas son:
- **Administrador de Sistemas / DevOps:** Encargado de la gestión de contenedores Docker, la orquestación, revisión de logs del servidor y el despliegue del aplicativo en los distintos entornos.
- **Administrador de Base de Datos (DBA):** Responsable de garantizar la disponibilidad e integridad de *AutoAlertDB*, gestionar las políticas de respaldos y optimizar el rendimiento de Microsoft SQL Server 2022.
- **Desarrollador (Backend/Frontend):** Encargado de aplicar parches de seguridad, actualizar librerías de .NET 8 y React, y resolver las incidencias (bugs) que requieran modificaciones en el código fuente.

---

## 2. Descripción General del Entorno

El ecosistema de AutoAlert está diseñado bajo una arquitectura modular y en contenedores, lo que facilita su escalabilidad, despliegue y mantenimiento. Conocer la topología del sistema es fundamental antes de realizar cualquier intervención técnica.

### 2.1 Arquitectura del Sistema
El aplicativo está compuesto por tres capas principales:
- **Frontend (Cliente Web):** Desarrollado en **React 19** utilizando **Vite** como empaquetador. La interfaz gráfica se comunica directamente con el backend mediante peticiones HTTP (Axios) utilizando tokens de autorización web (JWT).
- **Backend (Servidor API):** Construido sobre **ASP.NET Core (.NET 8)**. Provee una API RESTful encargada de procesar las reglas de negocio, gestionar la autenticación y orquestar las notificaciones de alertas. Utiliza **Entity Framework Core** como su ORM (Object-Relational Mapper) principal.
- **Base de Datos:** Un motor relacional **Microsoft SQL Server 2022** que centraliza la información de usuarios, roles, empresas, tiendas, servicios públicos y estados de alertas.

### 2.2 Topología de Contenedores y Red
El sistema está orquestado mediante **Docker Compose**, asegurando que todos los servicios corran en entornos aislados y consistentes:
- **Red Interna Docker:** Los contenedores se comunican a través de una red virtual interna provista por Docker. Esto garantiza que el Backend pueda conectarse de forma segura a la Base de Datos utilizando su nombre de servicio (ej. `db`), mitigando la exposición de puertos sensibles.
- **Volúmenes Persistentes:** Para evitar la pérdida de la base de datos si el contenedor respectivo se detiene o elimina, el sistema implementa un volumen mapeado llamado `dbdata`. **Cualquier tarea de mantenimiento profundo y respaldos de SQL Server depende de la integridad de este volumen.**

### 2.3 Archivos de Configuración Críticos
Durante las rutinas de mantenimiento técnico, es posible que se requiera auditar, ajustar o respaldar los siguientes archivos clave que dictan el comportamiento del sistema:
- `docker-compose.yml`: Archivo principal de orquestación que define las imágenes base, puertos expuestos, variables de entorno y mapeo de volúmenes.
- `.env`: Archivo de variables de entorno globales. Almacena de manera segura credenciales confidenciales como la contraseña de base de datos (`SA_PASSWORD`) y las llaves de encriptación (`JWT_KEY`).
- `appsettings.json` (Backend): Archivo propio de configuración de .NET que contiene, entre otros elementos, las cadenas de conexión a la base de datos (Connection Strings) y configuraciones detalladas del comportamiento del servidor.

---

## 3. Mantenimiento Preventivo

Las tareas de mantenimiento preventivo tienen como objetivo anticiparse a posibles fallas del sistema, evitar cuellos de botella en el rendimiento y mantener actualizados los componentes de seguridad.

### 3.1 Mantenimiento de Base de Datos (SQL Server)
Para garantizar la integridad y disponibilidad de la información en *AutoAlertDB*, se deben ejecutar las siguientes acciones de forma periódica:

- **Respaldos de Información (Backups):**
  - **Frecuencia recomendada:** Ejecución diaria para copias incrementales y semanal para copias completas (`.bak`).
  - **Procedimiento manual mediante Docker:** Es posible generar un archivo de backup interactuando directamente con el contenedor de la base de datos ejecutando:
    ```bash
    docker exec -it <nombre_contenedor_sql> /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<tu_contraseña>" -Q "BACKUP DATABASE AutoAlertDB TO DISK = '/var/opt/mssql/backup/autoalertdb_backup.bak'"
    ```
  - **Resguardo Externo:** Los archivos `.bak` generados dentro del volumen deben ser extraídos del servidor principal (ej. copiándolos con `docker cp`) y almacenados en un repositorio externo o en la nube para asegurar la recuperación ante desastres físicos.

- **Monitoreo del almacenamiento (`dbdata`):**
  - Supervisar periódicamente el espacio en disco del servidor anfitrión. Si el volumen en el que reside `dbdata` se llena al 100%, SQL Server detendrá las transacciones, provocando la caída del sistema.

### 3.2 Mantenimiento de Contenedores y Recursos Docker
Dado que el ecosistema se basa en contenedores, el motor de Docker requiere rutinas de higiene para liberar recursos inactivos:

- **Auditoría de Recursos:**
  - Ejecutar periódicamente el comando `docker stats` para verificar el consumo de Memoria RAM y CPU de los contenedores (Frontend, Backend, DB). Esto ayuda a identificar posibles "fugas de memoria" (memory leaks).
- **Limpieza de Entorno Docker:**
  - Durante las actualizaciones y redespliegues, Docker tiende a almacenar imágenes antiguas y construir caché innecesario.
  - Para liberar espacio de forma segura, ejecutar:
    ```bash
    docker system prune -f
    ```
    *(Nota: Este comando limpia contenedores detenidos, redes sin uso e imágenes colgantes. No afecta a los volúmenes de datos activos persistentes).*

### 3.3 Mantenimiento de Aplicación y Seguridad
Para mitigar vulnerabilidades y asegurar la compatibilidad continua del sistema:

- **Actualización de Dependencias (.NET y React):**
  - **Backend (.NET):** Revisar periódicamente la existencia de parches de seguridad críticos en los paquetes NuGet.
  - **Frontend (React):** Auditar los paquetes de Node mediante `npm audit` y aplicar correcciones a dependencias fundamentales (ej. Axios, Zustand, Tailwind).
- **Rotación de Credenciales:**
  - Se aconseja rotar credenciales críticas como la contraseña de `SA` (SQL Server) y el token maestro `JWT_KEY` ubicado en el archivo `.env` cada 6 meses. Tras modificar este archivo, los servicios afectados deben reiniciarse ejecutando `docker-compose up -d`.

---

## 4. Mantenimiento Correctivo

El mantenimiento correctivo comprende todas las acciones reactivas que se llevan a cabo en respuesta a un fallo, error o comportamiento inesperado del sistema. Su objetivo es restablecer la operación normal del aplicativo en el menor tiempo posible.

### 4.1 Diagnóstico de Problemas (Troubleshooting)
Antes de intentar cualquier corrección, es fundamental identificar el origen del problema mediante la revisión de los registros (logs) del sistema.

- **Revisión de logs de contenedores:**
  - Para ver los registros en tiempo real de todos los servicios orquestados, ejecutar desde el directorio raíz del proyecto:
    ```bash
    docker-compose logs -f
    ```
  - Para filtrar los registros por un servicio específico (ej. solo el backend):
    ```bash
    docker-compose logs -f backend
    ```
  - Los mensajes de error más relevantes suelen indicar el tipo de fallo: errores de conexión a la base de datos, excepciones no controladas en el servidor API, o problemas de autenticación.

- **Identificación de errores HTTP:**
  - Desde el navegador o herramienta de pruebas de API (ej. Postman), los códigos de respuesta HTTP indican el tipo de fallo:

  | Código | Significado | Posible Causa |
  |--------|-------------|---------------|
  | `401 Unauthorized` | Sin autenticación válida | JWT vencido o `JWT_KEY` modificada sin reiniciar los servicios. |
  | `403 Forbidden` | Sin permisos suficientes | El rol del usuario no tiene acceso al submódulo solicitado. |
  | `500 Internal Server Error` | Error no controlado en el servidor | Excepción en el Backend. Revisar logs del contenedor `backend`. |
  | `503 Service Unavailable` | Servicio no disponible | El contenedor Backend o DB está caído. Verificar con `docker ps`. |

### 4.2 Resolución de Fallas Comunes

- **Contenedor caído o detenido:**
  - Verificar el estado de todos los contenedores con:
    ```bash
    docker ps -a
    ```
  - Si algún servicio aparece en estado `Exited`, reiniciarlo individualmente con:
    ```bash
    docker-compose restart <nombre_servicio>
    ```
  - Si el reinicio falla, verificar los logs del servicio para identificar la causa raíz antes de intentar recrear el contenedor.

- **Fallo de conexión a la base de datos:**
  - Este error es frecuente si el contenedor de SQL Server (`db`) no ha terminado de inicializarse antes de que el Backend intente conectarse.
  - **Solución:** Verificar que el contenedor `db` esté en estado `healthy` y luego reiniciar únicamente el contenedor del backend:
    ```bash
    docker-compose restart backend
    ```
  - Si el error persiste, verificar que las credenciales en el archivo `.env` (`SA_PASSWORD`, `SA_USER`) coincidan con las configuradas en el `appsettings.json` del Backend.

- **Problemas de CORS (Frontend no puede comunicarse con el Backend):**
  - Este error se manifiesta en la consola del navegador como: `Access to XMLHttpRequest at '...' has been blocked by CORS policy`.
  - **Solución:** Verificar que la variable `VITE_API_URL` en el archivo `.env` del Frontend apunte correctamente a la URL del Backend y que la política de CORS en `Program.cs` (Backend) incluya el origen del Frontend.

### 4.3 Plan de Recuperación ante Desastres

En caso de pérdida total de datos o corrupción del entorno, seguir el siguiente procedimiento para restablecer el sistema desde cero:

**Paso 1: Recrear el entorno de contenedores**
```bash
# Detener y eliminar todos los contenedores y redes
docker-compose down

# Reconstruir e iniciar todos los servicios
docker-compose up -d --build
```

**Paso 2: Restaurar la base de datos desde un backup**
- Copiar el archivo `.bak` al contenedor de la base de datos:
  ```bash
  docker cp ./autoalertdb_backup.bak <nombre_contenedor_sql>:/var/opt/mssql/backup/
  ```
- Ejecutar el comando de restauración dentro del contenedor:
  ```bash
  docker exec -it <nombre_contenedor_sql> /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<tu_contraseña>" -Q "RESTORE DATABASE AutoAlertDB FROM DISK = '/var/opt/mssql/backup/autoalertdb_backup.bak' WITH REPLACE"
  ```

**Paso 3: Verificar la restauración**
- Ingresar a la aplicación y comprobar que los datos de grupos, empresas, tiendas y alertas sean correctos.
- Revisar que los módulos de autenticación, gestión de servicios y envío de alertas funcionen con normalidad.

---

## 5. Mantenimiento Adaptativo y Evolutivo

El mantenimiento adaptativo y evolutivo contempla todas las modificaciones planificadas que se realizan al sistema con el fin de incorporar nuevas funcionalidades, mejorar el rendimiento o adaptar la plataforma a cambios en el entorno tecnológico. A diferencia del mantenimiento correctivo, estas intervenciones son anticipadas y programadas.

### 5.1 Despliegue de Nuevas Versiones
Cuando se ha desarrollado y validado una nueva versión del sistema en el entorno de desarrollo, se debe seguir el siguiente proceso para actualizar el entorno de producción de forma segura.

**Procedimiento general de actualización:**

**Paso 1: Obtener los últimos cambios del repositorio**
```bash
git pull origin main
```

**Paso 2: Detener los servicios activos**
```bash
docker-compose down
```
> ⚠️ Este comando detiene los contenedores pero **no elimina** los volúmenes, por lo que los datos de la base de datos (`dbdata`) se conservan.

**Paso 3: Reconstruir las imágenes con los cambios actualizados**
```bash
docker-compose up -d --build
```
- La bandera `--build` fuerza a Docker a recompilar las imágenes del Backend y Frontend desde el código fuente más reciente, descartando la caché de construcciones anteriores.

**Paso 4: Verificar que todos los servicios estén corriendo correctamente**
```bash
docker ps
docker-compose logs -f
```
- Confirmar que los tres servicios (Frontend, Backend, DB) tienen el estado `Up` y que no hay errores en los logs.

---

### 5.2 Actualización del Esquema de Base de Datos (Migraciones)
Cuando se realizan cambios en el modelo de datos del Backend (ej. agregar una nueva columna a la tabla `Services` o crear una nueva entidad), es necesario aplicar una migración de Entity Framework Core para reflejar esos cambios en la base de datos sin perder la información existente.

**Proceso para generar y aplicar una migración:**

**Paso 1: (Solo Desarrolladores) Crear la migración en el proyecto Backend**

Desde el directorio del proyecto Backend (`.csproj`), ejecutar:
```bash
dotnet ef migrations add <NombreDescriptivoDeLaMigracion>
```
- Esto generará automáticamente un archivo de migración dentro del directorio `Migrations/` del proyecto con los cambios detectados en el modelo.

**Paso 2: Aplicar la migración a la base de datos**

Existen dos formas de aplicar la migración:
- **Opción A — Mediante CLI (entorno local con .NET SDK):**
  ```bash
  dotnet ef database update
  ```
- **Opción B — Mediante Docker (sin instalar .NET SDK en el servidor):**
  Al reconstruir la imagen del Backend con `docker-compose up -d --build`, la aplicación puede estar configurada para aplicar las migraciones pendientes automáticamente al inicio, si así fue implementado en `Program.cs`.

**Paso 3: Verificar los cambios aplicados**
- Conectarse a la base de datos mediante SQL Server Management Studio (SSMS) o Azure Data Studio y confirmar que la nueva estructura (tabla, columna o relación) se creó correctamente en *AutoAlertDB*.
- Revisar la tabla del historial de migraciones `__EFMigrationsHistory` para confirmar que el registro de la nueva migración fue insertado.

---

### 5.3 Gestión de Ramas y Control de Versiones (Git)
Para garantizar la trazabilidad de todos los cambios realizados al sistema, se deben seguir las siguientes convenciones de trabajo con el repositorio Git:

- **Rama principal (`main`):** Contiene únicamente código estable, probado y listo para producción. **No se realizan commits directamente sobre esta rama.**
- **Ramas de desarrollo (`feature/`, `fix/`, `hotfix/`):** Cada nueva funcionalidad o corrección debe desarrollarse en una rama independiente con un nombre descriptivo (ej. `feature/nueva-alerta-email`, `fix/error-login-google`).
- **Merge a producción:** Los cambios son incorporados a `main` únicamente a través de un *Pull Request* (PR) revisado y aprobado por al menos un miembro del equipo técnico.

---

## 6. Gestión de Incidencias y Soporte

Esta sección define el proceso formal para reportar, clasificar y dar seguimiento a los problemas técnicos que se presenten en el sistema AutoAlert, garantizando tiempos de atención adecuados según la criticidad del fallo.

### 6.1 Canales de Atención y Reporte
Cuando se detecta un problema en el sistema, el usuario técnico o administrador debe reportarlo a través de los canales habilitados, proporcionando la mayor cantidad de información posible para agilizar el diagnóstico:

- **Reporte interno (equipo de desarrollo):** A través del sistema de seguimiento de incidencias del repositorio Git (Issues de GitHub/GitLab), creando un nuevo ticket con la siguiente información:
  - **Título:** Descripción corta del problema (ej. `Error 500 al crear un nuevo servicio`).
  - **Descripción:** Pasos exactos para reproducir el error.
  - **Evidencia:** Capturas de pantalla, mensajes de error de la consola del navegador o del log de Docker.
  - **Entorno:** Sistema operativo, navegador utilizado y versión del sistema.

- **Reporte de emergencia (caída total del sistema):** Contactar directamente al **Administrador de Sistemas** o al responsable técnico designado del proyecto a través del canal de comunicación interno del equipo (ej. correo electrónico, WhatsApp de soporte o canal de Slack/Teams).

---

### 6.2 Niveles de Criticidad y Tiempos de Respuesta
Las incidencias se clasifican en tres niveles de prioridad, cada uno con un tiempo de respuesta y atención establecido:

| Nivel | Criticidad | Descripción | Tiempo de respuesta | Tiempo de resolución |
|-------|-----------|-------------|--------------------|--------------------|
| 🔴 **P1** | **Crítica** | El sistema completo está caído o inaccesible. Pérdida o corrupción de datos. | Inmediato (< 1 hora) | < 4 horas |
| 🟡 **P2** | **Alta** | Un módulo principal falla (ej. autenticación, gestión de alertas). Impacto en la mayoría de usuarios. | < 4 horas | < 24 horas |
| 🟢 **P3** | **Baja** | Fallo menor o visual que no impide el uso del sistema. Mejoras menores o ajustes de configuración. | < 24 horas | < 72 horas |

---

### 6.3 Ciclo de Vida de una Incidencia
Toda incidencia reportada sigue el siguiente flujo de atención hasta su cierre:

```
Reporte → Clasificación (P1/P2/P3) → Asignación → Diagnóstico → Solución → Verificación → Cierre
```

1. **Reporte:** El usuario o técnico registra el problema con evidencia en el canal correspondiente.
2. **Clasificación:** El Administrador de Sistemas asigna el nivel de prioridad (P1, P2 o P3).
3. **Asignación:** Se designa al responsable técnico (DBA, Desarrollador o DevOps) según la naturaleza del fallo.
4. **Diagnóstico:** El técnico asignado analiza logs, reproduce el error y determina la causa raíz.
5. **Solución:** Se aplica la corrección en el entorno afectado siguiendo los procedimientos del **Mantenimiento Correctivo (Sección 4)**.
6. **Verificación:** El usuario o supervisor confirma que el sistema funciona correctamente tras la corrección.
7. **Cierre:** Se documenta la solución aplicada en el ticket de incidencia y se cierra el caso.

---

### 6.4 Registro y Documentación de Incidencias
Es **obligatorio** mantener un registro histórico de todas las incidencias atendidas. Este registro permite identificar patrones de fallo recurrentes y tomar decisiones de mejora proactiva. Cada incidencia cerrada debe documentar como mínimo:

- Fecha y hora de detección.
- Descripción del problema y nivel de criticidad asignado.
- Causa raíz identificada.
- Solución aplicada y responsable técnico.
- Tiempo total de resolución.
