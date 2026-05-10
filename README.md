# personapi-dotnet

Laboratorio 1 - Implementación de Monolito con patrón MVC y DAO.

Este proyecto implementa una aplicación web ASP.NET Core MVC conectada a SQL Server, con vistas MVC y endpoints REST documentados con Swagger para realizar operaciones CRUD sobre el modelo de datos de personas, profesiones, teléfonos y estudios.

## Tecnologías utilizadas

- .NET 7
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server 2022 / SQL Server Express
- SQL Server Management Studio
- Swagger / Swashbuckle
- Docker
- Git / GitHub

## Arquitectura implementada

El proyecto sigue una arquitectura monolítica basada en el patrón MVC y DAO.

### Capas principales

- **Models/Entities:** contiene las entidades generadas a partir de la base de datos mediante Entity Framework Core.
- **DAO/Interfaces:** define los contratos de acceso a datos para cada entidad.
- **DAO/Repositories:** contiene la implementación de los repositorios.
- **Controllers:** contiene los controladores MVC y los controladores REST.
- **Views:** contiene las vistas Razor para las operaciones CRUD.
- **Program.cs:** configura servicios, conexión a base de datos, MVC, Swagger y dependencias.
- **database.sql:** contiene el script DDL y DML para crear y poblar la base de datos.

## Modelo de datos

El sistema gestiona las siguientes tablas:

- `persona`
- `profesion`
- `telefono`
- `estudios`

Relaciones principales:

- Una persona puede tener varios teléfonos.
- Una profesión puede estar asociada a varios estudios.
- Una persona puede tener varios estudios.
- La tabla `estudios` usa llave compuesta: `id_prof` y `cc_per`.

## Configuración de la base de datos

Crear una base de datos llamada:

```sql
persona_db