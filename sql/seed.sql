INSERT INTO Books (Title, Author, Genre, Status, Rating, IsActive)
VALUES
('The Metamorphosis', 'Franz Kafka', 'Fiction', 'Finished', 5, 1),
('Ficciones', 'Jorge Luis Borges', 'Short Stories', 'Reading', NULL, 1),
('Aura', 'Carlos Fuentes', 'Fiction', 'WantToRead', NULL, 1),
('Apology of Socrates', 'Plato', 'Philosophy', 'Finished', 4, 1),
('The Stranger', 'Albert Camus', 'Fiction', 'WantToRead', NULL, 1);

INSERT INTO ReadingNotes (BookId, Content, CreatedAt)
VALUES
(1, 'Kafka uses transformation to show alienation.', SYSUTCDATETIME()),
(2, 'Borges requires slow reading and rereading.', SYSUTCDATETIME()),
(4, 'Socrates defends the examined life.', SYSUTCDATETIME());

INSERT INTO ReadingStatusHistory (BookId, OldStatus, NewStatus, ChangedAt)
VALUES
(1, 'Reading', 'Finished', SYSUTCDATETIME()),
(2, 'WantToRead', 'Reading', SYSUTCDATETIME()),
(4, 'Reading', 'Finished', SYSUTCDATETIME());
