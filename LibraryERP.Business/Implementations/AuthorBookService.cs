using LibraryERP.Business.Interfaces;
using LibraryERP.Core.IRepositories;
using LibraryERP.Data.Repositories;
using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Implementations
{
    public class AuthorBookService : IAuthorBookService
    {
        IBookAuthorRepository bookAuthorRepository;
        public AuthorBookService()
        {
            bookAuthorRepository = new AuthorBookRepository();
        }
        public async Task AssignAuthorToTheBook(int authorId, int bookId)
        {
            IBookService bookService = new BookService();
            IAuthorService authorService = new AuthorService();
            Book book = await bookService.GetBookById(bookId);
            Author author = await authorService.GetAuthorById(authorId);
           await  bookAuthorRepository.Insert(new BookAuthor { AuthorId = authorId, BookId = bookId });
            await bookAuthorRepository.CommitAsync();
            await Console.Out.WriteLineAsync("Book assigned to the Author");
        }
    }
}
