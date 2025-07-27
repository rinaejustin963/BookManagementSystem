using BookManagementSystem.Models;
using BookManagementSystem.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BookManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "C:\\Users\\justi\\source\\repos\\BookManagementSystem\\appsettings.json"))
            .Build();

            var dataFilePath = config["DataFilePath"] ?? "Data/books.json";
            var dataRepository = new JsonDataRepository(dataFilePath);
            var libraryService = new LibraryService(dataRepository);

            Console.WriteLine("Welcome to the Book Management System!!!!");

            while (true)
            {
                DisplayMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddBook(libraryService);
                            break;
                        case "2":
                            RemoveBook(libraryService);
                            break;
                        case "3":
                            UpdateBook(libraryService);
                            break;
                        case "4":
                            SearchBooks(libraryService);
                            break;
                        case "5":
                            ListAvailableBooks(libraryService);
                            break;
                        case "6":
                            ListAllBooks(libraryService);
                            break;
                        case "7":
                            Console.WriteLine("Exiting the application...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to Book Management System");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Update book information");
            Console.WriteLine("4. Search for a book");
            Console.WriteLine("5. List all available books");
            Console.WriteLine("6. List all books");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        static void AddBook(LibraryService libraryService)
        {
            Console.WriteLine("\nAdd a New Book");

            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                throw new ArgumentException("Invalid Book ID");
            }

            Console.Write("Enter Title: ");
            var title = Console.ReadLine();

            Console.Write("Enter Author: ");
            var author = Console.ReadLine();

            Console.Write("Enter Publication Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                throw new ArgumentException("Invalid Publication Year");
            }

            var book = new Book(bookId, title, author, year);
            libraryService.AddBook(book);
            Console.WriteLine("Book added successfully!!!");
        }

        static void RemoveBook(LibraryService libraryService)
        {
            Console.WriteLine("\nRemove a Book");
            Console.Write("Enter Book ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                throw new ArgumentException("Invalid Book ID");
            }

            libraryService.RemoveBook(bookId);
            Console.WriteLine("Book removed successfully!!!");
        }

        static void UpdateBook(LibraryService libraryService)
        {
            Console.WriteLine("\nUpdate Book Information");
            Console.Write("Enter Book ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                throw new ArgumentException("Invalid Book ID");
            }

            var existingBook = libraryService.GetBook(bookId);

            Console.Write($"Enter new Title (current: {existingBook.Title}): ");
            var title = Console.ReadLine();

            Console.Write($"Enter new Author (current: {existingBook.Author}): ");
            var author = Console.ReadLine();

            Console.Write($"Enter new Publication Year (current: {existingBook.PublicationYear}): ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                throw new ArgumentException("Invalid Publication Year");
            }

            Console.Write($"Is available? (true/false) (current: {existingBook.IsAvailable}): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isAvailable))
            {
                throw new ArgumentException("Invalid availability value");
            }

            var updatedBook = new Book(bookId, title, author, year, isAvailable);
            libraryService.UpdateBook(bookId, updatedBook);
            Console.WriteLine("Book updated successfully!!!");
        }

        static void SearchBooks(LibraryService libraryService)
        {
            Console.WriteLine("\nSearch Books");
            Console.Write("Enter search keyword: ");
            var keyword = Console.ReadLine();

            var books = libraryService.SearchBooks(keyword);
            DisplayBooks(books, $"Search results for '{keyword}'");
        }

        static void ListAvailableBooks(LibraryService libraryService)
        {
            var books = libraryService.GetAvailableBooks();
            DisplayBooks(books, "Available Books");
        }

        static void ListAllBooks(LibraryService libraryService)
        {
            var books = libraryService.GetAllBooks();
            DisplayBooks(books, "All Books");
        }

        static void DisplayBooks(List<Book> books, string title)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine("--------------------------------------------------");
            if (books.Count == 0)
            {
                Console.WriteLine("No books found!!!");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BookID}");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Year: {book.PublicationYear}");
                Console.WriteLine($"Available: {(book.IsAvailable ? "Yes" : "No")}");
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}