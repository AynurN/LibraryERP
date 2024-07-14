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
    public class AuthorService : IAuthorService
    {
        IAuthorRepository authorRepository;
        public AuthorService()
        {
            authorRepository = new AuthorRepository();
        }
       
        public  async Task ChageDeleteStatus(int id)
        {
            var author = await authorRepository.Get(id);
            if (author == null)
                throw new NullReferenceException("Book not found!");
            author.isDeleted = !(author.isDeleted);
            await authorRepository.CommitAsync();
        }

        public async Task Create(Author author)
        {
            await authorRepository.Insert(author);
            await authorRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var book = await authorRepository.Get(id);
            if (book == null)
                throw new NullReferenceException("Book not found!");
            authorRepository.Delete(book);
            await authorRepository.CommitAsync();
        }

        public async Task<List<Author>> GetAll()
        {
            return await authorRepository.GetAll().Where(x=>x.isDeleted==false).Include(x=>x.BookAuthors).ThenInclude(x=>x.Book).AsNoTracking().ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            Author? b = await authorRepository.Get(id);
            if (b == null)
                throw new NullReferenceException("Author not found!");
            return b;
        }

        public async Task Update(int id, Author author)
        {
            var searched = await authorRepository.Get(id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            searched.FullName = author.FullName;
            await authorRepository.CommitAsync();
        }
    }
}
