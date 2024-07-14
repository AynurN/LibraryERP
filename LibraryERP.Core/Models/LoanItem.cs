using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class LoanItem :BaseModel
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public  int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
