using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class Author  :BaseModel
    {
        public string FullName { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public override string ToString()
        {
            string books = string.Empty;
            foreach (var item in BookAuthors)
            {
                books += item.Book.Title+" ";
                
            }
            return $"ID:{Id}, FullName:{FullName}, Books:{books}";
        }
    }
}
