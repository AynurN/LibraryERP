using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface IBorrowerService
    {
        Task<List<Borrower>> GetLateBorrowers();
        Task<List<Borrower>> GetAll();
        Task Create(Borrower borrower);
        Task Update(int id, Borrower borrower);
        Task Delete(int id);
        Task<Borrower> GetBorrowerById(int id);
        Task ChageDeleteStatus(int id);

        Task GetBorrowersAndBooks();
    }
}
