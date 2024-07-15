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
        Task<List<Loan>> GetByBorrowerId(int id);
        Task<List<Loan>> GetByBorrowerIdReturnable(int id);
        Task ChangeDeleteStatus(int id);
        Task UpdateEntire(Loan loan);
        Task BorrowBooks();
        Task Create(Loan loan);
        Task<Loan> GetLoanById(int id);
        Task ReturnBooks();
    }
}
