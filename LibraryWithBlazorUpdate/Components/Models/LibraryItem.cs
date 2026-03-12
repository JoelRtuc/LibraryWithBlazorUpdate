using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWithBlazorUpdate.Components.Models
{
    public class LibraryItem : ISearchable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // kept original names to avoid changing call sites
        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int publishedYear { get; set; }
        public bool isAvailable { get; set; }

        // navigation: one item can have many loans
        public virtual ICollection<Loan> loans { get; set; }

        public LibraryItem() { }

        public string GetInfo()
        {
            string newInfo;
            newInfo = $"title: {title}, author: {author}, published in the year: {publishedYear}\n";
            if (isAvailable)
            {
                newInfo += "Title is available\n";
            }
            else
            {
                newInfo += "Title is not available\n";
            }
            newInfo += description;
            return newInfo;
        }

        public bool Matches(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem)) return false;
            if (!string.IsNullOrEmpty(title) && title.Contains(searchItem)) return true;
            if (!string.IsNullOrEmpty(author) && author.Contains(searchItem)) return true;
            return false;
        }
    }
}
