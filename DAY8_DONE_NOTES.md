# Day 8 fix notes

This ZIP keeps the ReadFlow solution structure and adds the Day 8 work:

- `ReadingStatusHistory` domain entity
- `Book.Genre`
- `Book.StatusHistory` navigation collection
- `DbSet<ReadingStatusHistory>` in `AppDbContext`
- EF Core relationship and indexes
- repository methods for notes and status history
- service methods restored for notes/rating plus status history logging
- `GET /api/books/{id}/status-history`
- `sql/schema.sql`
- `sql/seed.sql`
- `sql/queries.sql`
- `sql/indexes.sql`
- migration `AddReadingStatusHistory`

Important commands:

```bash
dotnet restore
dotnet build
dotnet ef database update --project src/ReadFlow.Infrastructure --startup-project src/ReadFlow.Api
```

Test in Swagger:

1. `POST /api/books`
2. `PATCH /api/books/{id}/status`
3. `GET /api/books/{id}/status-history`
