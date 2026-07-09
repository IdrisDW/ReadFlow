SELECT Status, COUNT(*) AS Total
FROM Books
GROUP BY Status;

SELECT Genre, COUNT(*) AS Total
FROM Books
GROUP BY Genre;

SELECT b.Title, h.OldStatus, h.NewStatus, h.ChangedAt
FROM Books b
INNER JOIN ReadingStatusHistory h
    ON b.Id = h.BookId;

SELECT 
    b.Id,
    b.Title,
    b.Author,
    b.Genre,
    b.Status,
    b.Rating
FROM Books b
WHERE b.IsActive = 1
ORDER BY b.Title;

SELECT 
    b.Title,
    n.Content,
    n.CreatedAt
FROM Books b
INNER JOIN ReadingNotes n
    ON b.Id = n.BookId
ORDER BY n.CreatedAt DESC;

SELECT 
    b.Title,
    h.OldStatus,
    h.NewStatus,
    h.ChangedAt
FROM Books b
INNER JOIN ReadingStatusHistory h
    ON b.Id = h.BookId
WHERE b.Id = 1
ORDER BY h.ChangedAt DESC;


-- Books by author
SELECT 
    Author,
    COUNT(*) AS TotalBooks
FROM Books
WHERE IsActive = 1
GROUP BY Author
HAVING COUNT(*) > 2;


-- Reading summary
SELECT 
    COUNT(*) AS TotalBooks,
    SUM(CASE WHEN Status = 'Reading' THEN 1 ELSE 0 END) AS CurrentlyReading,
    SUM(CASE WHEN Status = 'Finished' THEN 1 ELSE 0 END) AS FinishedBooks,
    SUM(CASE WHEN Status = 'WantToRead' THEN 1 ELSE 0 END) AS WantToReadBooks,
    SUM(CASE WHEN Status = 'DNF' THEN 1 ELSE 0 END) AS DnfBooks
FROM Books
WHERE IsActive = 1;


-- Books by genre
SELECT 
    Genre,
    COUNT(*) AS Count
FROM Books
WHERE IsActive = 1
GROUP BY Genre
ORDER BY Genre;


-- Pagination example: page 1, pageSize 5
SELECT 
    Id,
    Title,
    Author,
    Genre,
    Status,
    Rating
FROM Books
WHERE IsActive = 1
ORDER BY Id
OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;


-- Pagination example: page 2, pageSize 5
SELECT 
    Id,
    Title,
    Author,
    Genre,
    Status,
    Rating
FROM Books
WHERE IsActive = 1
ORDER BY Id
OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY;