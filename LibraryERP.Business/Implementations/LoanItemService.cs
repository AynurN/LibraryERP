using LibraryERP.Business.Interfaces;
using LibraryERP.Core.IRepositories;
using LibraryERP.Core.Models;
using LibraryERP.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Implementations
{
    public class LoanItemService : IloanItemService
    {
        private ILoanItemRepository loanItemRepository;
        public LoanItemService()
        {
            loanItemRepository = new LoandItemRepository();
        }
     
        public async Task Create(LoanItem loanItem)
        {
            await loanItemRepository.Insert(loanItem);
            await loanItemRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            LoanItem? searched = await loanItemRepository.Get(id);
            if (searched == null)
                throw new NullReferenceException();
            loanItemRepository.Delete(searched);
           await loanItemRepository.CommitAsync();
        }

        public async Task<List<LoanItem>> GetAll()
        {
            return await loanItemRepository.GetAll().AsNoTracking().ToListAsync();

        }

        public async Task<LoanItem> GetLoanItemByBookId(int id)
        {

            LoanItem? b = await loanItemRepository.GetAllWhere(x => x.BookId == id).FirstOrDefaultAsync();
            if (b == null)
                throw new NullReferenceException("LoanItem not found!");
            return b;
        }

        public async Task<List<LoanItem>> GetLoanItemsByLoanId(int id)
        {
            List<LoanItem> b = await loanItemRepository.GetAllWhere(x => x.LoanId == id).ToListAsync();
            if (b == null)
                throw new NullReferenceException("LoanItem not found!");
            return b;
        }
    }
}
