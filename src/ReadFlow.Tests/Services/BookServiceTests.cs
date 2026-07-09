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
        var service = new BookService(repository, validator);

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
        var service = new BookService(repository, validator);

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
    public async Task UpdateStatus_WithValidTransition_UpdatesStatus()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();
        var service = new BookService(repository, validator);

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
        var service = new BookService(repository, validator);

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
    public async Task GetBookById_WhenMissing_ReturnsNotFound()
    {
        // Arrange
        var repository = new FakeBookRepository();
        var validator = new ReadingStatusTransitionValidator();
        var service = new BookService(repository, validator);

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }
}