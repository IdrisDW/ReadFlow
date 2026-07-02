# Debugging Notes

## Día 3 — POST /api/books validation

### Error intencional

Intenté crear un libro sin título.

Request:

```json
{
  "title": "",
  "authorName": "Ursula K. Le Guin",
  "genre": "Science Fiction",
  "isbn": "9780441478125",
  "year": 1969
}
```

Expected:

```txt
400 Bad Request
```

Actual:

```txt
400 Bad Request
```

Fix:

```txt
Agregué una validación en el endpoint POST /api/books usando string.IsNullOrWhiteSpace(request.Title). 
Si Title viene vacío, null o solo con espacios, el endpoint regresa BadRequest("Title is required.").
```

Código relevante:

```csharp
if (string.IsNullOrWhiteSpace(request.Title))
{
    return BadRequest("Title is required.");
}
```
