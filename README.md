# Desarrollo prueba tecnica

## Instrucciones de instalación y ejecución ##
### Clonar el repositorio ###

```bash
   git clone https://github.com/Leomonbu/pruebalink.git
 ```

### Ejecutar con Docker ###
```bash
   docker-compose up --build
```
- Esto ejecuta:
     - ProductoService en http://localhost:5120
     - InventarioService en http://localhost:5121
     - SQL Server interno accesible por ambas APIs

- Acceder a Swagger UI
     - Producto API: http://localhost:5120/swagger
     - Inventario API: http://localhost:5121/swagger

### Crear base de datos y tablas ###
- Ingresar por una terminal a SQLCMD
```bash
   sqlcmd -S localhost -U sa -P Tu_Pass123!
```   
- Crear la base de datos
```bash
   CREATE DATABASE [dbLynk]
   GO
```
- Direccionar a la base de datos
```bash
   USE dbLynk
   GO
```
- Crear la tabla para productos
```bash
   CREATE TABLE [dbo].[Productos](
  	[Id_producto] [bigint] IDENTITY(1,1) NOT NULL,
  	[nombre_producto] [varchar](80) NOT NULL,
  	[precio_producto] [int] NOT NULL,
   CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
   (
	  [Id_producto] ASC
   ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
  ) ON [PRIMARY] 
  GO
```
- Crear la tabla para inventario
```bash
   CREATE TABLE [dbo].[Inventario](
	[id_producto] [bigint] NOT NULL,
	[cantidad] [int] NOT NULL,
   CONSTRAINT [PK_Inventario] PRIMARY KEY CLUSTERED 
   (
	[id_producto] ASC
   )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
   ) ON [PRIMARY]
  GO

ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD CONSTRAINT [FK_Inventario_Producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Productos] ([id_producto])
GO

ALTER TABLE [dbo].[Inventario] CHECK CONSTRAINT [FK_Inventario_Producto]
GO
```
<br>

## Descripción de la arquitectura ##

### Estructura general ###
Solución con dos microservicios (dos APIs) desarrolladas en ASP.NET Core, cada una con responsabilidades distintas, contenedorizadas con Docker, y ambas conectadas a una base de datos SQL Server común.

### Componentes principales ###
| Componente                |    Descripcion                                          |
|---------------------------|---------------------------------------------------------|
| API Producto              |  Expone endpoints para la administracion de productos   |
| API Inventario            |  Administra el inventario de los productos. Se relaciona con los productos por id_producto   |
| Base de datos SQL Server  |  Contenedor compartido. Contiene las tablas Productos, Inventario, y sus relaciones          |
| Docker Compose            |  Orquesta los tres contenedores (Producto API, Inventario API, SQL Server), gestionando red y dependencia   |

### Comunicación ###
* Las APIs se comunican entre sí por HTTP interno usando el nombre del servicio en docker-compose como hostname.
* Se usa HttpClient en una de las APIs para consumir la otra (por ejemplo, Inventario puede consultar Producto).
* Se implementan API keys o autenticación para asegurar la comunicación.

### Dockerización ###
* Cada API tiene su propio Dockerfile.
* Un archivo docker-compose.yml define:
    - Red compartida
    - Contenedor de SQL Server
    - Las dos APIs
    - Variables de entorno y puertos expuestos

### Pruebas ###
* Se desarrollan pruebas unitarias para cada microservicio de forma separada.
<br>

## Decisiones técnicas y justificaciones ##
* ASP.NET Core como Framework
    * Decisión: Utilizar ASP.NET Core 8.0 para el desarrollo de ambas APIs.
    * Justificación: ASP.NET Core ofrece alto rendimiento, soporte para microservicios, facilidad de integración con Docker y un ecosistema moderno para desarrollo Web API.
 
* Base de datos compartida en contenedor
    * Decisión: Utilizar un solo contenedor de SQL Server accesible por ambas APIs.
    * Justificación: Simplifica el entorno de desarrollo y garantiza consistencia en los datos durante pruebas y desarrollo. Se aplicaron esquemas y relaciones (como claves foráneas) para mantener la integridad.
<br>

## Diagrama de interacción entre servicios ##
![image](https://github.com/user-attachments/assets/503b99ff-e8d2-431e-839e-97191539f26b)
