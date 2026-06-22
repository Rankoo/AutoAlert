# AutoAlertBackEnd

Proyecto backend para AutoAlert — API REST construida con .NET 8.

## Descripción

Este repositorio contiene la API del servicio AutoAlert que gestiona usuarios, roles, módulos, alertas y notificaciones.

## Requisitos

- .NET 8 SDK
- SQL Server (o la base de datos configurada en [appsettings.json](appsettings.json))

## Uso local

1. Restaurar dependencias:

```
dotnet restore
```

2. Ejecutar la aplicación:

```
dotnet run --project AutoAlertBackEnd.csproj
```

La API se expondrá en el puerto configurado (por defecto 5000/5001 para HTTP/HTTPS).

## Docker

Construir la imagen:

```
docker build -t autoalert-backend .
```

Ejecutar el contenedor (ejemplo):

```
docker run -d -p 5000:80 --name autoalert-backend autoalert-backend
```

Nota: Ajusta puertos y variables de entorno según tu configuración.

## Archivos importantes

- [Program.cs](Program.cs)
- [appsettings.json](appsettings.json)
- [Dockerfile](Dockerfile)

## Contribuciones

Pull requests y issues son bienvenidos. Para cambios mayores, abre un issue primero para discutir lo planificado.

## Licencia

Sin licencia especificada; contactar al autor del repositorio.

## Docker: Uso local vs Producción

### Uso en desarrollo (local)

- Crear un archivo de configuración para el entorno `Development` a partir del ejemplo incluido:

```
copy appsettings.Development.example.json appsettings.Development.json
```

- Ejecutar la aplicación localmente con la variable de entorno `ASPNETCORE_ENVIRONMENT=Development` (ejemplo PowerShell):

```
$Env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project AutoAlertBackEnd.csproj
```

- Si quieres ejecutar en Docker con la configuración local, monta el archivo `appsettings.Development.json` en el contenedor y pasa la variable de entorno:

Linux/macOS:

```
docker build -t autoalert-backend .
docker run -d -p 5000:80 \
	-e ASPNETCORE_ENVIRONMENT=Development \
	-v $(pwd)/appsettings.Development.json:/app/appsettings.Development.json \
	--name autoalert-backend autoalert-backend
```

Windows PowerShell:

```
docker build -t autoalert_backend .
docker run -d -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Development -v ${PWD}\appsettings.Development.json:/app/appsettings.development.json --name autoalert-backend autoalert_backend
```

> Nota: montar el archivo permite que la aplicación use la configuración local (connection strings, claves, etc.) sin bakearlas en la imagen.

### Uso en producción

- En producción, prefiera inyectar valores sensibles mediante variables de entorno o gestores de secretos en lugar de incluirlos en la imagen.

Ejemplo de ejecución con variables de entorno (override de cadenas de conexión):

```
docker build -t autoalert-backend:prod .
docker run -d -p 80:80 \
	-e ASPNETCORE_ENVIRONMENT=Production \
	-e ConnectionStrings__DefaultConnection="Server=DB_HOST;Database=DB;User Id=USER;Password=PASS;" \
	--name autoalert-backend-prod autoalert-backend:prod
```

- Otra opción es usar un volumen o un secreto montado por el orquestador (Docker Swarm, Kubernetes, etc.).

## Ejemplo de `appsettings.Development`

Incluyo un ejemplo mínimo para comenzar. Copia y adapta los valores según tu entorno.

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=localhost;Database=AutoAlertDb;User Id=sa;Password=Your_password123;"
	},
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft": "Warning",
			"Microsoft.Hosting.Lifetime": "Information"
		}
	},
	"AllowedHosts": "*"
}
```
