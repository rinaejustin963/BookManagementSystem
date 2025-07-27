using BookManagementSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BookManagementSystem.Services
{
    public class JsonDataRepository : IDataRepository
    {
        private readonly string _filePath;

        public JsonDataRepository(string filePath)
        {
            _filePath = filePath;

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public List<Book> LoadBooks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Book>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void SaveBooks(List<Book> books)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(books, options);
            File.WriteAllText(_filePath, json);
        }
    }
}