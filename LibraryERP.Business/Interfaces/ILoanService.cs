using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface ILoanService
    {
        Task UpdateEntire(Loan loan);
        Task BorrowBooks();
        Task Create(Loan loan);
        Task<Loan> GetLoanById(int id);
        Task ReturnBooks();
    }
}
