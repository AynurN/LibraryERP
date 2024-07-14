﻿using LibraryERP.Core.IRepositories;
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
       
    }
}
