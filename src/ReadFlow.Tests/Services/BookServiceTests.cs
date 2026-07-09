using Microsoft.Extensions.Logging.Abstractions;
using ReadFlow.Application.Requests;
using ReadFlow.Application.Services;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;
using ReadFlow.Tests.Fakes;

namespace ReadFlow.Tests.Services;

public class BookServiceTests
{
    [Fact]
    public async Task CreateBook_WithValidData_ReturnsBook()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new CreateBookRequest
        {
            Title = "Ficciones",
            Author = "Jorge Luis Borges",
            Genre = "Fiction"
        };

        // Act
        var result = await service.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ficciones", result.Title);
        Assert.Equal("Jorge Luis Borges", result.Author);
        Assert.Equal("Fiction", result.Genre);
        Assert.Equal("WantToRead", result.Status);
    }

    [Fact]
    public async Task CreateBook_WithEmptyTitle_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new CreateBookRequest
        {
            Title = "",
            Author = "Jorge Luis Borges",
            Genre = "Fiction"
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync(request));
    }

    [Fact]
    public async Task CreateBook_WithEmptyAuthor_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new CreateBookRequest
        {
            Title = "Ficciones",
            Author = "",
            Genre = "Fiction"
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync(request));
    }

    [Fact]
    public async Task CreateBook_WithEmptyGenre_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new CreateBookRequest
        {
            Title = "Ficciones",
            Author = "Jorge Luis Borges",
            Genre = ""
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync(request));
    }

    [Fact]
    public async Task UpdateStatus_WithValidTransition_UpdatesStatus()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        repository.AddExisting(new Book
        {
            Id = 1,
            Title = "Aura",
            Author = "Carlos Fuentes",
            Genre = "Fiction",
            Status = ReadingStatus.WantToRead,
            IsActive = true
        });

        var request = new UpdateBookStatusRequest
        {
            Status = ReadingStatus.Reading
        };

        // Act
        var result = await service.UpdateStatusAsync(1, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Reading", result.Status);
    }

    [Fact]
    public async Task UpdateStatus_WithInvalidTransition_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        repository.AddExisting(new Book
        {
            Id = 1,
            Title = "La metamorfosis",
            Author = "Franz Kafka",
            Genre = "Fiction",
            Status = ReadingStatus.Finished,
            IsActive = true
        });

        var request = new UpdateBookStatusRequest
        {
            Status = ReadingStatus.Reading
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateStatusAsync(1, request));
    }

    [Fact]
    public async Task UpdateStatus_WithMissingBook_ReturnsNull()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new UpdateBookStatusRequest
        {
            Status = ReadingStatus.Reading
        };

        // Act
        var result = await service.UpdateStatusAsync(999, request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBookById_WhenMissing_ReturnsNull()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateRating_WithInvalidRating_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new UpdateBookRatingRequest
        {
            Rating = 6
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateRatingAsync(1, request));
    }

    [Fact]
    public async Task AddNote_WithEmptyContent_Fails()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();

        var service = new BookService(
            repository,
            validator,
            NullLogger<BookService>.Instance);

        var request = new CreateReadingNoteRequest
        {
            Content = ""
        };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddNoteAsync(1, request));
    }
}