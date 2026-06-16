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
