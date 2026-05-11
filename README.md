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
- [Autores](#autores)

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
- Cloudflare Tunnel

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
- **Dockerfile:** permite construir una imagen Docker del proyecto.

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
- Docker Desktop
- Cloudflare Tunnel, opcional para exponer temporalmente la aplicación local

---

## Configuración de la base de datos

El proyecto utiliza una base de datos llamada:

```sql
persona_db
```

Para crear la base de datos, las tablas y los datos iniciales, ejecutar el archivo:

```text
database.sql
```

Este archivo contiene:

- Creación de la base de datos `persona_db`.
- Creación de tablas.
- Definición de llaves primarias.
- Definición de llaves foráneas.
- Inserción de datos iniciales.

### Tablas creadas

```sql
persona
profesion
telefono
estudios
```

---

## Configuración del proyecto

La cadena de conexión se encuentra configurada en el archivo:

```text
appsettings.json
```

Para este laboratorio se usó SQL Server Express local con autenticación integrada de Windows:

```json
{
  "ConnectionStrings": {
    "PersonaDbConnection": "Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=True"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*"
}
```

Esta configuración permite que la aplicación se conecte a la base de datos `persona_db` ubicada en la instancia local `SQLEXPRESS`.

> Nota: si el proyecto se ejecuta desde Docker o desde otro equipo, esta cadena debe modificarse, ya que `localhost` dentro de un contenedor no apunta al equipo anfitrión sino al propio contenedor.

---

## Ejecución local

El proyecto puede ejecutarse desde Visual Studio 2022 o desde consola.

### Opción 1: Ejecutar desde Visual Studio

1. Abrir la solución del proyecto.
2. Verificar que la cadena de conexión apunte correctamente a SQL Server.
3. Compilar la solución.
4. Ejecutar el proyecto usando el perfil HTTP.

### Opción 2: Ejecutar desde consola

Desde la carpeta raíz del proyecto:

```bash
dotnet build
```

Luego ejecutar:

```bash
dotnet .\bin\Debug\net7.0\personapi-dotnet.dll --urls "http://localhost:5095"
```

La aplicación queda disponible en:

```text
http://localhost:5095
```

En caso de necesitar Swagger en ambiente de desarrollo, puede definirse el entorno así:

```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet .\bin\Debug\net7.0\personapi-dotnet.dll --urls "http://localhost:5095"
```

---

## Rutas MVC

La aplicación incluye vistas MVC para gestionar los módulos principales.

### Inicio

```text
http://localhost:5095
```

### Personas

```text
http://localhost:5095/Personas
```

### Profesiones

```text
http://localhost:5095/Profesiones
```

### Teléfonos

```text
http://localhost:5095/Telefonos
```

### Estudios

```text
http://localhost:5095/Estudios
```

Cada módulo cuenta con operaciones de:

- Listar registros.
- Crear registros.
- Ver detalles.
- Editar registros.
- Eliminar registros.

---

## Endpoints REST

El proyecto también incluye controladores REST para las entidades principales.

### Personas

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/personas` | Lista todas las personas |
| GET | `/api/personas/{id}` | Consulta una persona por cédula |
| POST | `/api/personas` | Crea una nueva persona |
| PUT | `/api/personas/{id}` | Actualiza una persona existente |
| DELETE | `/api/personas/{id}` | Elimina una persona |

### Profesiones

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/profesiones` | Lista todas las profesiones |
| GET | `/api/profesiones/{id}` | Consulta una profesión por ID |
| POST | `/api/profesiones` | Crea una nueva profesión |
| PUT | `/api/profesiones/{id}` | Actualiza una profesión existente |
| DELETE | `/api/profesiones/{id}` | Elimina una profesión |

### Teléfonos

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/telefonos` | Lista todos los teléfonos |
| GET | `/api/telefonos/{id}` | Consulta un teléfono por número |
| POST | `/api/telefonos` | Crea un nuevo teléfono |
| PUT | `/api/telefonos/{id}` | Actualiza un teléfono existente |
| DELETE | `/api/telefonos/{id}` | Elimina un teléfono |

### Estudios

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/estudios` | Lista todos los estudios |
| GET | `/api/estudios/{idProf}/{ccPer}` | Consulta un estudio por llave compuesta |
| POST | `/api/estudios` | Crea un nuevo estudio |
| PUT | `/api/estudios/{idProf}/{ccPer}` | Actualiza un estudio existente |
| DELETE | `/api/estudios/{idProf}/{ccPer}` | Elimina un estudio |

---

## Swagger

Swagger se encuentra configurado para documentar y probar los endpoints REST del proyecto.

Una vez ejecutada la aplicación, ingresar a:

```text
http://localhost:5095/swagger/index.html
```

Desde esta interfaz se pueden probar los endpoints de:

- Personas.
- Profesiones.
- Teléfonos.
- Estudios.

---

## Docker

El proyecto incluye un archivo `Dockerfile` para permitir la construcción de una imagen Docker de la aplicación.

### Construcción de la imagen

Desde la raíz del proyecto:

```bash
docker build -t personapi-dotnet .
```

La imagen fue construida correctamente y quedó registrada como:

```text
personapi-dotnet:latest
```

### Verificación de imagen creada

Para verificar que la imagen existe localmente:

```bash
docker images
```

En la lista de imágenes debe aparecer:

```text
personapi-dotnet    latest
```

### Ejecución del contenedor

La aplicación fue ejecutada en contenedor mediante el siguiente comando:

```bash
docker run -p 8080:80 personapi-dotnet
```

La aplicación quedó disponible desde Docker en:

```text
http://localhost:8080
```

Con esto se validó correctamente la construcción y ejecución inicial de la imagen Docker.

### Nota sobre conexión a base de datos en Docker

La cadena actual del proyecto está configurada para ejecución local:

```json
"Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=True"
```

Esta cadena funciona cuando la aplicación corre directamente en el equipo donde está instalado SQL Server Express.

Si la aplicación se ejecuta dentro de Docker, la cadena de conexión debe ajustarse para apuntar a un servidor SQL Server accesible desde el contenedor. Por ejemplo, podría usarse una instancia SQL Server en otro contenedor, una IP del equipo anfitrión o una base de datos remota.

Por esta razón, aunque la aplicación inicia correctamente desde Docker, la operación completa con base de datos requiere configurar la conectividad hacia SQL Server si se desea ejecutar todo el sistema desde contenedores.

---

## Despliegue

Para este laboratorio, la aplicación fue desarrollada y validada inicialmente en ambiente local.

El despliegue local se realiza mediante .NET, accediendo a la aplicación desde:

```text
http://localhost:5095
```

Adicionalmente, para validar el acceso externo a la aplicación, se expuso el servidor local mediante Cloudflare Tunnel.

Primero se ejecutó la aplicación localmente en:

```text
http://localhost:5095
```

Luego se creó un túnel público con el siguiente comando:

```bash
cloudflared tunnel --url http://localhost:5095
```

Cloudflare Tunnel generó la siguiente URL pública temporal:

```text
https://accomplished-wins-manga-excluding.trycloudflare.com
```

Desde esta URL se validó el acceso externo a las vistas MVC y a Swagger:

```text
https://accomplished-wins-manga-excluding.trycloudflare.com/Personas
```

```text
https://accomplished-wins-manga-excluding.trycloudflare.com/swagger/index.html
```

> Importante: este despliegue es temporal y depende de que la aplicación local y el túnel de Cloudflare estén activos. No corresponde a un despliegue productivo en nube, sino a una exposición pública temporal para validación externa del laboratorio.

---

## Control de versiones

El proyecto fue versionado usando Git y publicado en GitHub.

Repositorio:

```text
https://github.com/DiegoPUJ/personapi-dotnet
```

Durante el desarrollo se realizaron commits por cada avance principal:

- Creación inicial del proyecto.
- Implementación del CRUD de Persona.
- Implementación del CRUD de Profesión.
- Implementación del CRUD de Teléfono.
- Implementación del CRUD de Estudio.
- Ajuste de navegación principal.
- Configuración de Swagger y API REST.
- Creación del script `database.sql`.
- Actualización de documentación en README.
- Exposición temporal de la aplicación mediante Cloudflare Tunnel.
- Validación del Dockerfile mediante construcción y ejecución de imagen Docker.

---

## Estructura del proyecto

```text
personapi-dotnet/
│
├── Controllers/
│   ├── PersonasController.cs
│   ├── ProfesionesController.cs
│   ├── TelefonosController.cs
│   ├── EstudiosController.cs
│   ├── ApiPersonasController.cs
│   ├── ApiProfesionesController.cs
│   ├── ApiTelefonosController.cs
│   └── ApiEstudiosController.cs
│
├── DAO/
│   ├── Interfaces/
│   │   ├── IPersonaRepository.cs
│   │   ├── IProfesionRepository.cs
│   │   ├── ITelefonoRepository.cs
│   │   └── IEstudioRepository.cs
│   │
│   └── Repositories/
│       ├── PersonaRepository.cs
│       ├── ProfesionRepository.cs
│       ├── TelefonoRepository.cs
│       └── EstudioRepository.cs
│
├── Models/
│   └── Entities/
│       ├── Persona.cs
│       ├── Profesion.cs
│       ├── Telefono.cs
│       ├── Estudio.cs
│       └── PersonaDbContext.cs
│
├── Views/
│   ├── Personas/
│   ├── Profesiones/
│   ├── Telefonos/
│   ├── Estudios/
│   ├── Home/
│   └── Shared/
│
├── wwwroot/
│
├── appsettings.json
├── database.sql
├── Dockerfile
├── Program.cs
├── personapi-dotnet.csproj
└── README.md
```

---

## Estado del laboratorio

| Requisito | Estado |
|---|---|
| Repositorio público en GitHub | Completado |
| Proyecto ASP.NET Core MVC | Completado |
| Base de datos `persona_db` | Completado |
| Script DDL y DML | Completado |
| Entidades generadas con Entity Framework Core | Completado |
| Patrón DAO | Completado |
| Controladores MVC | Completado |
| Vistas CRUD | Completado |
| Endpoints REST | Completado |
| Swagger | Completado |
| Dockerfile | Validado correctamente mediante construcción y ejecución de imagen Docker |
| Documentación README | Completado |
| Despliegue local | Completado |
| Túnel público temporal | Completado |
| Código fuente en GitHub | Completado |
| TAG final | Pendiente |

---

## Autores

Desarrollado por:

**Diego Martinez, Juliana Bejarano y Sebastián Almanza**

Laboratorio 1 de Arquitectura de Software.

---