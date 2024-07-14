using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface IAuthorBookService
    {
        Task AssignAuthorToTheBook(int authorId, int bookId);
    }
}
