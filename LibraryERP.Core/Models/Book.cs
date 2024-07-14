using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class Book :BaseModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int PublishYear { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Loan> Loans { get; set; }
        public bool isDeleted { get; set; }
        public bool Avilability { get; set; }
    }
}
