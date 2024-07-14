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
        public async  Task BorrowBook(int bookId, int borrowerId)
        {
            IBookService bookService = new BookService();
            IBorrowerService borrowerService = new BorrowerService();
            Book book = await bookService.GetBookById(bookId);
            Borrower borrower = await borrowerService.GetBorrowerById(borrowerId);
            if(book.isDeleted==false && book.Avilability == true)
            {

                await Create(new LoanItem() { BookId = book.Id, LoanId = borrower.Loan.Id });
                book.Avilability = false;
               
                await Console.Out.WriteLineAsync("Book borrowed");
            }
            else if(book.Avilability == false)
            {
                await Console.Out.WriteLineAsync("Book is not available!");
            }


        }

        public async Task Create(LoanItem loanItem)
        {
            await loanItemRepository.Insert(loanItem);
            await loanItemRepository.CommitAsync();
        }

        public async Task<List<LoanItem>> GetAll()
        {
            return await loanItemRepository.GetAll().AsNoTracking().ToListAsync();

        }

       
    }
}
