using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorById(int id);
        Task<List<Author>> GetAll();
        Task Create(Author author);
        Task Update(int id,Author author);
        Task Delete(int id);
        Task ChageDeleteStatus(int id);
    }
}
