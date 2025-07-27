using BookManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementSystem.Services
{
    public class LibraryService
    {
        private readonly IDataRepository _dataRepository;
        private List<Book> _books;

        public LibraryService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _books = _dataRepository.LoadBooks();
        }

        public void AddBook(Book book)
        {
            if (_books.Any(b => b.BookID == book.BookID))
            {
                throw new ArgumentException("Book with this ID already exists!!!");
            }
            _books.Add(book);
            _dataRepository.SaveBooks(_books);
        }

        public void RemoveBook(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.BookID == bookId);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found!!!");
            }
            _books.Remove(book);
            _dataRepository.SaveBooks(_books);
        }

        public void UpdateBook(int bookId, Book updatedBook)
        {
            var existingBook = _books.FirstOrDefault(b => b.BookID == bookId);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found!!!");
            }

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.PublicationYear = updatedBook.PublicationYear;
            existingBook.IsAvailable = updatedBook.IsAvailable;

            _dataRepository.SaveBooks(_books);
        }

        public Book GetBook(int bookId)
        {
            return _books.FirstOrDefault(b => b.BookID == bookId)
                ?? throw new KeyNotFoundException("Book not found!!!");
        }

        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public List<Book> SearchBooks(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<Book>();
            }

            var lowerKeyword = keyword.ToLower();
            return _books.Where(b =>
                b.Title.ToLower().Contains(lowerKeyword) ||
                b.Author.ToLower().Contains(lowerKeyword))
                .ToList();
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }
    }
}