# API Walkthrough — Books Read Endpoints

## Goal

This API exposes basic read endpoints for books.

Current endpoints:

GET /api/books  
GET /api/books/{id}

The goal is to understand how an HTTP request moves through the application layers.

---

## Full Flow

Browser or Swagger  
→ BooksController  
→ IBookService  
→ BookService  
→ IBookRepository  
→ InMemoryBookRepository  
→ Book entity  
→ BookDto  
→ HTTP response

---

## 1. Browser / Swagger

The browser or Swagger sends an HTTP request to the API.

For example:

GET /api/books

This request asks the API to return all active books.

---

## 2. BooksController

BooksController receives the HTTP request.

It has endpoints like:

GET /api/books  
GET /api/books/{id}

The controller does not contain business logic. Its job is to receive the request, call the service, and return an HTTP response.

Example:

If books exist, it returns:

200 OK

If a book does not exist, it returns:

404 Not Found

---

## 3. IBookService

IBookService is an interface.

It defines what the book service can do:

GetAll()  
GetById(int id)

The controller depends on IBookService instead of depending directly on BookService.

This makes the code less coupled and easier to change later.

---

## 4. BookService

BookService contains the application logic.

It asks the repository for books, filters active books, and converts Book entities into BookDto objects.

It uses LINQ methods:

Where  
Select  
FirstOrDefault

Where filters books.

Select transforms Book into BookDto.

FirstOrDefault returns the first matching book or null if no book is found.

---

## 5. IBookRepository

IBookRepository is an interface for data access.

It defines that any book repository must have a method to get books.

The service depends on this interface instead of depending on a concrete repository.

This means later I can replace InMemoryBookRepository with SQL Server or EF Core without changing the controller.

---

## 6. InMemoryBookRepository

InMemoryBookRepository is the current implementation of IBookRepository.

It stores books in a List<Book>.

This is a temporary fake database used for practice.

Later, it can be replaced with a real database repository.

---

## 7. Book Entity

Book is the internal domain model.

It represents a book inside the system.

It has properties like:

Id  
Title  
Author  
Year  
IsActive

---

## 8. BookDto

BookDto is the object returned by the API.

The API does not expose the full Book entity directly.

For example, Book has IsActive, but BookDto does not expose IsActive.

This helps control what data the API returns.

---

## 9. HTTP Response

GET /api/books returns 200 OK with a list of active books.

GET /api/books/{id} returns 200 OK if the book exists.

GET /api/books/{id} returns 404 Not Found if the book does not exist.

---

## Self-check

I can explain why each layer exists:

The controller handles HTTP.

The service handles application logic.

The repository handles data access.

The DTO controls what the API exposes.

The interfaces reduce coupling between layers.