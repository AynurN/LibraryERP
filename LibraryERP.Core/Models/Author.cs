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
            return $"ID:{Id}, FullName:{FullName}";
        }
    }
}
