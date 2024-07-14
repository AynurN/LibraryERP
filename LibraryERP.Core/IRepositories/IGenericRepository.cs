using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Core.Repositories
{
    public interface IGenericRepository<T> where T: BaseModel, new()
    {
        IQueryable<T>? GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<T?> GetWhere(Expression<Func<T, bool>> predicate); 
        Task<int> CommitAsync();
        void Delete(T entity);
        IQueryable<T> GetAll();
        Task<T?> Get(int id);
        Task Insert(T entity);
    }
}
