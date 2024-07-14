﻿using LibraryERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryERP.Business.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAll();
        Task Create(Book book);
        Task Update (int id, Book book);
        Task Delete(int id);
        Task<List<Book>> FilterBooksByTitle(string title);
        Task<List<Book>> FilterBooksByAuthor(string authorName);
        Task BorrowBook(int bookId, int borrowerId);
        Task ReturnBook(int bookId);
    }
}