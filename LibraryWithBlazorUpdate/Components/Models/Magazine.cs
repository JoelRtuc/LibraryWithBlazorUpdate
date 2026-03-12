using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWithBlazorUpdate.Components.Models
{
    public class Magazine : LibraryItem
    {
        public int issue;
        public string company;

        public Magazine() { }

        public Magazine(int ISBN, string title, string description, string author, int publishedYear, bool isAvailable, int issue, string company)
        {
            base.title = title;
            base.description = description;
            base.publishedYear = publishedYear;
            base.isAvailable = isAvailable;
            base.author = author;
            this.issue = issue;
            this.company = company;
        }
    }
}
