using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class Book :BaseModel
    {
        public static int BorrowCount { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public int? PublishYear { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Loan>? Loans { get; set; }
        public bool Avilability { get; set; }
        public override string ToString()
        {
            string authors = string.Empty;
            foreach (var item in BookAuthors)
            {
                authors += item.Author.FullName + " ";

            }
            return $"ID:{Id}, Book:{Title}, Publish year:{PublishYear}, Authors:{authors}, Availability:{Avilability}";
        }
    }
}
