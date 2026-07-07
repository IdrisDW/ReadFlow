IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Books_Status'
      AND object_id = OBJECT_ID('dbo.Books')
)
BEGIN
    CREATE INDEX IX_Books_Status
    ON Books(Status);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Books_Genre'
      AND object_id = OBJECT_ID('dbo.Books')
)
BEGIN
    CREATE INDEX IX_Books_Genre
    ON Books(Genre);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Books_IsActive'
      AND object_id = OBJECT_ID('dbo.Books')
)
BEGIN
    CREATE INDEX IX_Books_IsActive
    ON Books(IsActive);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ReadingNotes_BookId'
      AND object_id = OBJECT_ID('dbo.ReadingNotes')
)
BEGIN
    CREATE INDEX IX_ReadingNotes_BookId
    ON ReadingNotes(BookId);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ReadingStatusHistory_BookId'
      AND object_id = OBJECT_ID('dbo.ReadingStatusHistory')
)
BEGIN
    CREATE INDEX IX_ReadingStatusHistory_BookId
    ON ReadingStatusHistory(BookId);
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ReadingStatusHistory_BookId_ChangedAt'
      AND object_id = OBJECT_ID('dbo.ReadingStatusHistory')
)
BEGIN
    CREATE INDEX IX_ReadingStatusHistory_BookId_ChangedAt
    ON ReadingStatusHistory(BookId, ChangedAt DESC);
END;
