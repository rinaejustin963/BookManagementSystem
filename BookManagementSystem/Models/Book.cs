using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementSystem.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }

        public Book(int bookId, string title, string author, int publicationYear, bool isAvailable = true)
        {
            BookID = bookId;
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            IsAvailable = isAvailable;
        }

        public Book() { }
    }
}
