﻿using LibraryERP.Business.Interfaces;
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
            if (searched.isDeleted == false)
            {
                if (borrower.Email == null)
                {
                    searched.FullName = borrower.FullName;
                }
                else if (borrower.FullName == null)
                {
                    searched.Email = borrower.Email;

                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Borrower not found!");
            }
           
           
            await borrowerRepository.CommitAsync();
        }

        public async Task<List<Borrower>> GetLateBorrowers()
        {
            return await  borrowerRepository.GetAll()
                                    .Include(b => b.Loans)
                                    .Where(b => b.Loans.Any(l => l.MustReturnDate < DateTime.Now))
                                    .ToListAsync();
        }

     
        public async Task GetBorrowersAndBooks()
        {
            IloanItemService loanItemService = new LoanItemService();
            ILoanService loanService = new LoanService();
          List<Borrower> borrowers=await borrowerRepository.GetAll().Include(x=>x.Loans).ThenInclude(x=>x.LoanItems).ThenInclude(x=>x.Book).ToListAsync();
            foreach (var item in borrowers)
            {
                await Console.Out.WriteLineAsync(item.FullName+": ");
                List<Loan> loans = await loanService.GetByBorrowerId(item.Id);
                    foreach (Loan loan in loans) {
                    List<LoanItem> loanItems = await loanItemService.GetLoanItemsByLoanId(item.Id);
                    foreach (var item1 in loanItems)
                    {
                        await Console.Out.WriteAsync(item1.Book.Title+" ");
                    }
                }
                await Console.Out.WriteAsync("\n");
            }
        }

        }
    }

