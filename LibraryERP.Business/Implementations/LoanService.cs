using LibraryERP.Business.Interfaces;
using LibraryERP.Core.IRepositories;
using LibraryERP.Core.Models;
using LibraryERP.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Implementations
{
    public class LoanService : ILoanService
    {
        ILoanRepository loanRepository;
        public LoanService()
        {
            loanRepository = new LoanRepository();
        }
        public async Task Create(Loan loan)
        {
            loan.LoanDate= DateTime.Now;
            loan.MustReturnDate=DateTime.Now.AddDays(15);
            await loanRepository.Insert(loan);
            await loanRepository.CommitAsync();
        }

        public async Task<Loan> GetLoanById(int id)
        {
            Loan? b = await loanRepository.Get(id);
            if (b == null)
                throw new NullReferenceException("Book not found!");
            return b;
        }

        public async Task ReturnBooks(int LoanId)
        {
            Loan loan = await GetLoanById(LoanId);
            loan.ReturnDate = DateTime.Now;
            foreach (var item in loan.LoanItems)
            {
                item.Book.Avilability = true;
            }


        }
    }
}
