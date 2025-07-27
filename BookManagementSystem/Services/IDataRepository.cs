using BookManagementSystem.Models;
using System.Collections.Generic;

namespace BookManagementSystem.Services
{
    public interface IDataRepository
    {
        List<Book> LoadBooks();
        void SaveBooks(List<Book> books);
    }
}