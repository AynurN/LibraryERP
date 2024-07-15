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
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        public BookService()
        {
            _bookRepository=new BookRepository();
        }
        public async Task<Book> GetBookById(int id)
        {
            Book? b = await _bookRepository.Get(id);
            if (b == null)
                throw new NullReferenceException("Book not found!");
            return b;
        }
       


        public async Task ChageDeleteStatus(int id)
        {
            var book = await _bookRepository.Get(id);
            if (book == null)
                throw new NullReferenceException("Book not found!");
            book.isDeleted = !(book.isDeleted);
            await _bookRepository.CommitAsync();
        }

        public async Task Create(Book book)
        {
            await _bookRepository.Insert(book);
            await _bookRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var book = await  _bookRepository.Get(id);
            if (book == null)
                throw new NullReferenceException("Book not found!");
            _bookRepository.Delete(book);
            await _bookRepository.CommitAsync();

        }

        public async Task<List<Book>> FilterBooksByAuthor(string authorName)
        {
            var books = await _bookRepository.GetAll().Include(x=>x.BookAuthors).ThenInclude(x=>x.Author).ToListAsync();
            return books.Where(x=>x.isDeleted==false).Where(b => b.BookAuthors.Any(ab => ab.Author.FullName.Equals(authorName, StringComparison.OrdinalIgnoreCase))).ToList();
        }
        public async Task GetBooksByAuthor(int authorId)
        {
            var books = await _bookRepository.GetAll().Include(x => x.BookAuthors).ThenInclude(x => x.Author).ToListAsync();
            List<Book> b= books.Where(b => b.BookAuthors.Any(ab => ab.Author.Id==authorId )).Where(x=>x.isDeleted==false).ToList();
            b.ForEach(x => Console.Write(x.Title+" "));
        }
        public async Task<List<Book>> FilterBooksByTitle(string title)
        {
            var books = await _bookRepository.GetAll().ToListAsync();
            return books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public async Task<Book> GetMostBorrowedBook()
        {
            List<Book> books = await GetAll();

            if (books == null || books.Count == 0)
            {
                throw new InvalidOperationException("No books found.");
            }

            Book mostBorrowedBook = books.OrderByDescending(b => b.BorrowCount).FirstOrDefault();

            return mostBorrowedBook ?? throw new InvalidOperationException("No borrowed books found.");
        }

        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll().Where(x=>x.isDeleted==false).Include(x=>x.BookAuthors).ThenInclude(x=>x.Author).AsNoTracking().ToListAsync();
        }

      
    
    public async Task Update(int id, Book book)
        {
            var searched = await _bookRepository.Get(id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            if (searched.isDeleted == false)
            {
                if (book.Title == null)
                {
                    searched.Desc = book.Desc;
                }
                if (book.Desc == null)
                {
                    searched.Title = book.Title;
                }

            }
            else
            {
                await Console.Out.WriteLineAsync("Book not found!");
            }
           
            
           
     await _bookRepository.CommitAsync();        }
        public async Task UpdateEntire( Book book)
        {
            var searched = await _bookRepository.Get(book.Id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            searched = book;
            await _bookRepository.CommitAsync();
        }
       

    }
    
}

