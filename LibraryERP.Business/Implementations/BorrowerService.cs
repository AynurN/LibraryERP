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
    public class BorrowerService : IBorrowerService
    {
        private IBorrowerRepository borrowerRepository;
        public BorrowerService()
        {
            borrowerRepository = new BorrowerRepository();
        }
        public async Task Create(Borrower borrower)
        {
            await borrowerRepository.Insert(borrower);
            await borrowerRepository.CommitAsync();
            
        }
        public async Task<Borrower> GetBorrowerById(int id)
        {
            Borrower? b= await borrowerRepository.GetAll().Include(x=>x.Loans).FirstOrDefaultAsync(x=>x.Id==id);
            if (b == null)
                throw new NullReferenceException("Borrower not found!");
            return b;
        }

        public async Task Delete(int id)
        {
            var borrower = await borrowerRepository.Get(id);
            if (borrower == null)
                throw new NullReferenceException("Borrower not found!");
            borrowerRepository.Delete(borrower);
            await borrowerRepository.CommitAsync();

        }
        public async Task ChageDeleteStatus(int id)
        {
            var borrower = await borrowerRepository.Get(id);
            if (borrower == null)
                throw new NullReferenceException("Borrower not found!");
            borrower.isDeleted = !(borrower.isDeleted);
            await borrowerRepository.CommitAsync();

        }

        public async Task<List<Borrower>> GetAll()
        {
            return await borrowerRepository.GetAll().Where(x=>x.isDeleted==false).AsNoTracking().ToListAsync();
        }

        public async Task Update(int id, Borrower borrower)
        {
            var searched = await borrowerRepository.Get(id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            searched.FullName = borrower.FullName;
            searched.Email = borrower.Email;
            await borrowerRepository.CommitAsync();
        }

        public async Task<List<Borrower>> GetLateBorrowers()
        {
            return await  borrowerRepository.GetAll()
                                    .Include(b => b.Loans)
                                    .Where(b => b.Loans.Any(l => l.MustReturnDate < DateTime.Now))
                                    .ToListAsync();
        }
    }
}
