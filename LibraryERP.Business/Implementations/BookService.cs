﻿using LibraryERP.Business.Interfaces;
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
        public Task BorrowBook(int bookId,int borrowerId)
        {
           
           
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
            return await _bookRepository.GetBooksByAuthorName(authorName).AsNoTracking().ToListAsync();
        }

        public async Task<List<Book>> FilterBooksByTitle(string title)
        {
            return await _bookRepository.GetBooksByTitle(title).AsNoTracking().ToListAsync();
        }

        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll().AsNoTracking().ToListAsync();
        }

        public async Task ReturnBook(int bookId)
        {
           
        }

        public async Task Update(int id, Book book)
        {
            var searched = await _bookRepository.Get(id);
            if (searched == null)
                throw new NullReferenceException("Book not found");
            searched.Title = book.Title;
            searched.Desc = book.Desc;

        }
    }
}