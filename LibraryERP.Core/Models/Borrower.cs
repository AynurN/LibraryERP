﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Models
{
    public class Borrower: BaseModel
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public List<Loan> Loans { get; set; }
        public override string ToString()
        {
            return $"FullName:{FullName}, Email:{Email}";
        }
    }
}
