using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWithBlazorUpdate.Components.Models
{
    public class Book : LibraryItem
    {
        // ISBN as a mapped property (EF requires get/set)
        public string ISBN { get; set; }

        public Book() { }

        public Book(string ISBN, string title, string description, string author, int publishedYear, bool isAvailable)
        {
            this.ISBN = ISBN;
            base.title = title;
            base.description = description;
            base.publishedYear = publishedYear;
            base.isAvailable = isAvailable;
            base.author = author;
        }
    }
}
