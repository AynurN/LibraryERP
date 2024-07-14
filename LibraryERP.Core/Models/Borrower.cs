using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class Borrower: BaseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool isDeleted { get; set; }
        public Loan Loan { get; set; }
       
    }
}
