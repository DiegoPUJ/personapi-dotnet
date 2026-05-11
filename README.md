# personapi-dotnet

Laboratorio 1 - Implementación de un monolito con patrón MVC y DAO.

Este proyecto corresponde a una aplicación web desarrollada en ASP.NET Core MVC, conectada a una base de datos SQL Server mediante Entity Framework Core. La aplicación permite gestionar operaciones CRUD sobre el modelo de datos de personas, profesiones, teléfonos y estudios.

Además de las vistas MVC, el proyecto incluye endpoints REST documentados con Swagger para consultar, crear, actualizar y eliminar registros desde una interfaz de prueba API.

---

## Tabla de contenido

- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Arquitectura implementada](#arquitectura-implementada)
- [Modelo de datos](#modelo-de-datos)
- [Requisitos previos](#requisitos-previos)
- [Configuración de la base de datos](#configuración-de-la-base-de-datos)
- [Configuración del proyecto](#configuración-del-proyecto)
- [Ejecución local](#ejecución-local)
- [Rutas MVC](#rutas-mvc)
- [Endpoints REST](#endpoints-rest)
- [Swagger](#swagger)
- [Docker](#docker)
- [Despliegue](#despliegue)
- [Control de versiones](#control-de-versiones)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Estado del laboratorio](#estado-del-laboratorio)
- [Autor](#autor)

---

## Tecnologías utilizadas

- .NET 7
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server 2022 / SQL Server Express
- SQL Server Management Studio
- Swagger / Swashbuckle
- Docker
- Git / GitHub

---

## Arquitectura implementada

El proyecto sigue una arquitectura monolítica basada en los patrones MVC y DAO.

El patrón MVC permite separar la aplicación en tres responsabilidades principales:

- **Modelo:** representa las entidades y la estructura de datos del sistema.
- **Vista:** muestra la información al usuario mediante páginas Razor.
- **Controlador:** recibe las solicitudes del usuario, coordina la lógica del sistema y devuelve una respuesta.

El patrón DAO se utiliza para separar la lógica de acceso a datos de los controladores. Esto permite que los controladores no interactúen directamente con el `DbContext`, sino a través de interfaces y repositorios.

### Capas principales

- **Models/Entities:** contiene las entidades generadas a partir de la base de datos mediante Entity Framework Core.
- **DAO/Interfaces:** contiene los contratos de acceso a datos.
- **DAO/Repositories:** contiene la implementación de los repositorios DAO.
- **Controllers:** contiene los controladores MVC y los controladores REST.
- **Views:** contiene las vistas Razor para las operaciones CRUD.
- **Program.cs:** configura los servicios del proyecto, la conexión a base de datos, MVC, Swagger y la inyección de dependencias.
- **database.sql:** contiene el script DDL y DML para crear y poblar la base de datos.

---

## Modelo de datos

El sistema gestiona las siguientes tablas:

- `persona`
- `profesion`
- `telefono`
- `estudios`

### Relaciones principales

- Una persona puede tener varios teléfonos.
- Una persona puede tener varios estudios.
- Una profesión puede estar asociada a varios estudios.
- La tabla `estudios` representa la relación entre `persona` y `profesion`.
- La tabla `estudios` utiliza llave compuesta formada por `id_prof` y `cc_per`.

---

## Requisitos previos

Para ejecutar el proyecto se requiere tener instalado:

- Visual Studio Community 2022
- SDK de .NET 7
- SQL Server Express o SQL Server 2022
- SQL Server Management Studio
- Git
- Docker Desktop, opcional para validar el Dockerfile
- Cloudflare Tunnel, ngrok o LocalTunnel, opcional para exponer temporalmente la aplicación local

---

## Configuración de la base de datos

El proyecto utiliza una base de datos llamada:

```sql
persona_db