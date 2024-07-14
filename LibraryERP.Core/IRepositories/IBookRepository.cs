using LibraryERP.Core.Models;
using LibraryERP.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.IRepositories
{
    public interface IBookRepository :IGenericRepository<Book>
    {
        public IQueryable<Book> GetBooksByAuthorName(string authorName);
        public IQueryable<Book> GetBooksByTitle(string title);
    }
}
