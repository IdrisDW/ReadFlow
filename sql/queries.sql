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
