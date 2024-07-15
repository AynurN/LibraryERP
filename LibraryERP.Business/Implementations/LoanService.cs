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
    public class LoanService : ILoanService
    {
        ILoanRepository loanRepository;
        public LoanService()
        {
            loanRepository = new LoanRepository();
        }
        IBorrowerService borrowerService = new BorrowerService();
        IBookService bookService = new BookService();
        IloanItemService loanItemService = new LoanItemService();
        public async Task Create(Loan loan)
        {
            loan.LoanDate = DateTime.Now;
            loan.MustReturnDate = DateTime.Now.AddDays(15);
            await loanRepository.Insert(loan);
            await loanRepository.CommitAsync();
        }
        public async Task BorrowBooks()
        {
            
            await Console.Out.WriteLineAsync("Choose id of borrower:");
            List<Borrower> borrowers = await borrowerService.GetAll();
            foreach (var item in borrowers)
            {
                await Console.Out.WriteLineAsync($"{item.Id} {item.FullName}");
            }
            int borrowerId = Convert.ToInt32(Console.ReadLine());
            Borrower bor = await borrowerService.GetBorrowerById(borrowerId);
            var newLoan = new Loan()
            {
                BorrowerId = bor.Id,
                LoanDate = DateTime.Now,
                MustReturnDate = DateTime.Now.AddDays(15)
            };
            await Create(newLoan);
            await loanRepository.CommitAsync();

            var loanItems = new List<LoanItem>();

            while (true)
            {
                await Console.Out.WriteLineAsync("Choose id of Book to borrow:");
                List<Book> books = await bookService.GetAll();
                foreach (var item in books)
                {
                    await Console.Out.WriteLineAsync($"{item.Id} {item.Title} Availability: {item.Avilability}");
                }
                int bookId = Convert.ToInt32(Console.ReadLine());
                Book book = await bookService.GetBookById(bookId);

                if (book.Avilability)
                {
                    var loanItem = new LoanItem() { BookId = book.Id, LoanId = newLoan.Id };
                    loanItems.Add(loanItem);
                    book.Avilability = false;
                    book.BorrowCount++;
                    
                   await  bookService.UpdateEntire(book);
                   await loanRepository.CommitAsync();
                }
                else
                {
                    await Console.Out.WriteLineAsync("Book is not available!");

                }
                await Console.Out.WriteLineAsync("1. Add another book\n"+"2. Confirm");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 2)
                {
                    break;
                }
            }
            if (loanItems.Count == 0)
            {
                loanRepository.Delete(newLoan);
            }
            foreach (var loanItem in loanItems)
            {
                await loanItemService.Create(loanItem);
            }
            await loanRepository.CommitAsync(); 
            await Console.Out.WriteLineAsync("Books Borrowed");
        }


        public async Task<Loan> GetLoanById(int id)
        {
            Loan? b = await loanRepository.Get(id);
            if (b == null)
                throw new NullReferenceException("Book not found!");
            return b;
        }


        public async Task ReturnBooks()
        {
            await Console.Out.WriteLineAsync("Choose id of borrower:");
            List<Borrower> borrowers = await borrowerService.GetAll();
            foreach (var item in borrowers)
            {
                await Console.Out.WriteLineAsync($"{item.Id} {item.FullName}");
            }

            int borrowerId = Convert.ToInt32(Console.ReadLine());
            Borrower borrower = await borrowerService.GetBorrowerById(borrowerId);

            if (borrower.Loans == null || borrower.Loans.Count == 0 || borrower.Loans.All(x=>x.ReturnDate!=null))
            {
                await Console.Out.WriteLineAsync("Borrower does not have any loans.");
                return;
            }
            await Console.Out.WriteLineAsync("Choose id of active loan:");
            List<Loan> loans = await GetByBorrowerId(borrowerId);
            foreach (var item in loans)
            {
                await Console.Out.WriteLineAsync($"{item.Id}, LoanDate: {item.LoanDate}, MustReturnDate:{item.MustReturnDate}");
            }
            int loanId = Convert.ToInt32(Console.ReadLine());
            Loan loan= await GetLoanById(loanId);
             List<LoanItem> loanItems = await loanItemService.GetLoanItemsByLoanId(loanId);
                foreach (var loanItem in loanItems)
                {
                    Book book = await bookService.GetBookById(loanItem.BookId);
                    book.Avilability = true;
                    await bookService.UpdateEntire(book); 
                    await loanItemService.ChangeDeleteStatus(loanItem.Id);
                }

                loan.ReturnDate = DateTime.Now;
                await UpdateEntire(loan); 
            

            await loanRepository.CommitAsync(); 

            await Console.Out.WriteLineAsync("Books Returned");
        }

        public async Task UpdateEntire(Loan loan)
        {
            var searched = await loanRepository.Get(loan.Id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            searched = loan;
            await loanRepository.CommitAsync();
        }
        public async Task ChangeDeleteStatus(int id)
        {
            var loan= await loanRepository.Get(id);
            if (loan == null)
                throw new NullReferenceException("Book not found!");
            loan.isDeleted = !(loan.isDeleted);
            await loanRepository.CommitAsync();
        }

        public async Task<List<Loan>> GetByBorrowerId(int id)
        {
            return await loanRepository.GetAll().Include(x => x.Borrower).Include(x => x.LoanItems).ThenInclude(x=>x.Book).Where(x => x.ReturnDate==null).Where(x=>x.BorrowerId==id).AsNoTracking().ToListAsync();
        }

    }
}
