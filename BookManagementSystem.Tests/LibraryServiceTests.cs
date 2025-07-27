using BookManagementSystem.Models;
using BookManagementSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookManagementSystem.Tests
{
    public class LibraryServiceTests
    {
        private readonly Mock<IDataRepository> _mockRepository;
        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _mockRepository = new Mock<IDataRepository>();
            _mockRepository.Setup(r => r.LoadBooks()).Returns(new List<Book>());
            _libraryService = new LibraryService(_mockRepository.Object);
        }

        [Fact]
        public void AddBook_ValidBook_AddsToCollection()
        {
            var book = new Book(1, "Test Book", "Author", 2023);

            _libraryService.AddBook(book);

            var retrievedBook = _libraryService.GetBook(1);
            Assert.NotNull(retrievedBook);
            Assert.Equal("Test Book", retrievedBook.Title);
            _mockRepository.Verify(r => r.SaveBooks(It.IsAny<List<Book>>()), Times.Once);
        }

        [Fact]
        public void AddBook_DuplicateId_ThrowsException()
        {
            var book1 = new Book(1, "Book 1", "Author", 2023);
            var book2 = new Book(1, "Book 2", "Author", 2023);
            _libraryService.AddBook(book1);

            Assert.Throws<ArgumentException>(() => _libraryService.AddBook(book2));
        }

        [Fact]
        public void RemoveBook_ExistingId_RemovesBook()
        {
            var book = new Book(1, "Test Book", "Author", 2023);
            _libraryService.AddBook(book);

            _libraryService.RemoveBook(1);

            Assert.Throws<KeyNotFoundException>(() => _libraryService.GetBook(1));
            _mockRepository.Verify(r => r.SaveBooks(It.IsAny<List<Book>>()), Times.Exactly(2));
        }

        [Fact]
        public void UpdateBook_ValidData_UpdatesBook()
        {
            var originalBook = new Book(1, "Original Title", "Original Author", 2020);
            _libraryService.AddBook(originalBook);

            var updatedBook = new Book(1, "Updated Title", "Updated Author", 2021, false);

            _libraryService.UpdateBook(1, updatedBook);

            var result = _libraryService.GetBook(1);
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated Author", result.Author);
            Assert.Equal(2021, result.PublicationYear);
            Assert.False(result.IsAvailable);
            _mockRepository.Verify(r => r.SaveBooks(It.IsAny<List<Book>>()), Times.Exactly(2));
        }

        [Fact]
        public void GetAvailableBooks_ReturnsOnlyAvailableBooks()
        {
            var books = new List<Book>
            {
                new Book(1, "Book 1", "Author 1", 2020, true),
                new Book(2, "Book 2", "Author 2", 2021, false),
                new Book(3, "Book 3", "Author 3", 2022, true)
            };
            _mockRepository.Setup(r => r.LoadBooks()).Returns(books);
            var service = new LibraryService(_mockRepository.Object);

            var availableBooks = service.GetAvailableBooks();

            Assert.Equal(2, availableBooks.Count);
            Assert.DoesNotContain(availableBooks, b => b.BookID == 2);
        }

        [Fact]
        public void SearchBooks_FindsMatchingBooks()
        {
            var books = new List<Book>
            {
                new Book(1, "C# Programming", "John Doe", 2020),
                new Book(2, "Advanced C#", "Jane Smith", 2021),
                new Book(3, "Python Basics", "John Doe", 2019)
            };
            _mockRepository.Setup(r => r.LoadBooks()).Returns(books);
            var service = new LibraryService(_mockRepository.Object);

            var csharpBooks = service.SearchBooks("C#");
            var johnBooks = service.SearchBooks("John");
            var noMatch = service.SearchBooks("Java");

            Assert.Equal(2, csharpBooks.Count);
            Assert.Equal(2, johnBooks.Count);
            Assert.Empty(noMatch);
        }
    }
}