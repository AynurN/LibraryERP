using LibraryERP.Core.Models;
using LibraryERP.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Data.Repositories
{
    public  class GenericRepository<T> : IGenericRepository<T>
        where T : BaseModel, new()
    {
        public  AppDbContext context;

        public GenericRepository()
        {
            context = new AppDbContext();
        }

    

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<T?> Get(int id)
        {
           return await context.Set<T>().Where(x=>x.isDeleted==false).Where(x=> x.Id==id).FirstOrDefaultAsync();
        }


        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsQueryable();
        }

        public IQueryable<T>? GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = context.Set<T>().AsQueryable();
            if(includes !=null && includes.Length > 0)
            {
                foreach(var include in includes)
                {
                    query.Include(include);
                }
            }
            return predicate is not null ? query.Where(predicate) : null;
        }

        public async Task<T?> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }


        public async Task Insert(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

      
    }
}
