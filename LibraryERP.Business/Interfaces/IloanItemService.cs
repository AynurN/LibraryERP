using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface IloanItemService
    {
        Task<List<LoanItem>> GetAll();
        Task Create(LoanItem loanItem);
        Task BorrowBook(int bookId, int borrowerId);
       
    }
}
