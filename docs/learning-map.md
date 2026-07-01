# ReadFlow Learning Map

Mi objetivo en estos 19 días es practicar:

- C#
- API
- OOP
- SQL
- Angular
- Testing
- Debugging
- Git
- PRs
- Cloud básico
- Explicación técnica

## Proyecto

ReadFlow será una aplicación para gestionar libros y flujo de lectura.

La idea no es hacer un proyecto enorme, sino practicar skills reales de backend y full-stack:

- Crear una API con ASP.NET Core
- Separar responsabilidades por capas
- Usar entidades de dominio
- Crear DTOs
- Validar datos
- Persistir datos con EF Core
- Escribir queries SQL
- Crear tests
- Documentar decisiones técnicas
- Explicar el código como si estuviera en entrevista o en onboarding

## Capas del proyecto

### ReadFlow.Api

Expone endpoints HTTP.

Ejemplo:

- GET /books
- POST /books
- PUT /books/{id}/status

### ReadFlow.Domain

Contiene las reglas principales del negocio.

Ejemplo:

- Book
- ReadingStatus
- Validaciones de transición de estado

### ReadFlow.Application

Contiene casos de uso y servicios.

Ejemplo:

- CreateBook
- GetBooks
- UpdateBookStatus

### ReadFlow.Infrastructure

Contiene detalles externos.

Ejemplo:

- EF Core
- SQL Server
- Repositories

### ReadFlow.Tests

Contiene pruebas del proyecto.

Ejemplo:

- Tests de reglas de negocio
- Tests de servicios
- Tests de validaciones


## C# baseline notes

### Class

Una class es un molde para crear objetos.

```csharp
public class Book
{
    public string Title { get; set; }
}
```

### Object vs instance

Un object es algo creado desde una clase.  
Una instance es ese objeto específico en memoria.

```csharp
Book book = new Book();
book.Title = "Aura";
```

Aquí `book` es una instancia de `Book`.

### Constructor

Un constructor sirve para inicializar un objeto cuando se crea.

```csharp
public class Book
{
    public Book(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}
```

### Interface

Una interface define lo que una clase debe hacer, pero no cómo lo hace.

```csharp
public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
}
```

### Dependency Injection

Dependency Injection significa recibir dependencias desde afuera en lugar de crearlas dentro de la clase.

```csharp
public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }
}
```

Esto permite cambiar la implementación sin romper el servicio.

