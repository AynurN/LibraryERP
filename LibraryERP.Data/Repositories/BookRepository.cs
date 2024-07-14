using LibraryERP.Core.IRepositories;
using LibraryERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public IQueryable<Book> GetBooksByAuthorName(string authorName)
        {
            return context.Books.Include(x => x.BookAuthors).ThenInclude(x => x.Author).Where(b => b.BookAuthors.Any(ab => ab.Author.FullName.Equals(authorName,StringComparison.OrdinalIgnoreCase))); 
            
        }
        public IQueryable<Book> GetBooksByTitle(string title)
        {
            return context.Books.Where(x=>x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        }

    }
}
