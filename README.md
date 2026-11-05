# Library API

API hecha en .NET 10 que funciona como proxy entre el frontend y la [FakeRestAPI](https://fakerestapi.azurewebsites.net/index.html). No usa base de datos, los repositorios consultan directamente la API externa.

## Requisitos

- .NET 10 SDK
- Visual Studio 2026, VS Code o Rider (opcional)

## Cómo correrlo

```bash
git clone https://github.com/jeriaj9/book-library-backend.git
cd library-api
dotnet restore
dotnet run --project src/Library.Api
```

La API queda en http://localhost:5000 y el Swagger en http://localhost:5000/swagger

También se puede abrir `Library.sln` en Visual Studio y correr el proyecto `Library.Api`.

## Tests

```bash
dotnet test
```

## Estructura

Está organizado con Clean Architecture:

- **Library.Domain**: entidades `Book` y `Author`.
- **Library.Application**: interfaces de los repositorios y los servicios.
- **Library.Infrastructure**: repositorios que llaman a la FakeRestAPI usando `HttpClient`.
- **Library.Api**: controllers, CORS, Swagger y registro de dependencias.
- **Library.Tests**: pruebas unitarias con xUnit y Moq.

## Endpoints

Libros

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/books | Lista de libros |
| GET | /api/books/{id} | Un libro por id |
| POST | /api/books | Agrega un libro |
| PUT | /api/books/{id} | Actualiza un libro |
| DELETE | /api/books/{id} | Elimina un libro |

Autores

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/authors | Lista de autores |
| GET | /api/authors/{id} | Un autor por id |
| POST | /api/authors | Agrega un autor |
| PUT | /api/authors/{id} | Actualiza un autor |
| DELETE | /api/authors/{id} | Elimina un autor |

## Configuración

En `src/Library.Api/appsettings.json`:

- `FakeRestApi:BaseUrl`: url de la API externa.
- `FrontendUrl`: url del frontend que se permite en CORS (por defecto http://localhost:5173).

## Nota

La FakeRestAPI no guarda los cambios. Los POST, PUT y DELETE responden bien, pero si se vuelve a consultar los datos siguen iguales.
