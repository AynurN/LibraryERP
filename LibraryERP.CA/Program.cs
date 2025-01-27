﻿using LibraryERP.Business.Implementations;
using LibraryERP.Business.Interfaces;
using LibraryERP.Core.Models;
using LibraryERP.Data.Repositories;

namespace LibraryERP.CA
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IAuthorService author = new AuthorService();
            IBookService book = new BookService();
            IBorrowerService borrower = new BorrowerService();
            ILoanService loan = new LoanService();
            IloanItemService loanItem = new LoanItemService();
            IAuthorBookService authorBook = new AuthorBookService();
        label1:
            Console.WriteLine("Choose action");
            Console.WriteLine("1.Author actions\n" +
                "2.Book actions\n" +
                "3.Borrower actions\n" +
                "4.Borrow book\n" +
               
                "5.Return book\n" +
                "6.Bring  most borrowed book\n" +
                "7.Bring borrowers who is late\n" +
                "8.Bring borrowers and books\n" +
                "9.Filter books by Titles\n" +
                "10.Filter books by Author Fullname\n" +
                "0. Exit");
            try
            {
                int c1 = Convert.ToInt32(Console.ReadLine());

                switch (c1)
                {
                    case 1:
                        {
                        label2:
                            Console.WriteLine("1.Author List\n" +
                                "2.Create author\n" +
                                "3.Edit author\n" +
                                "4.Delete author\n" +
                                "5.Assign book\n" +
                                "0.Exit");
                            int c2 = Convert.ToInt32(Console.ReadLine());
                            switch (c2)
                            {
                                case 2:
                                    {
                                        try
                                        {
                                            Console.WriteLine("Enter fullname:");
                                            string? name = Console.ReadLine();
                                            if (name != null)
                                            {
                                                await author.Create(new Author() { FullName = name });
                                                await Console.Out.WriteLineAsync("Author Created");
                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("Author could not be created!");

                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }


                                    }
                                    goto label2;
                                case 1:
                                    {
                                        try
                                        {
                                            List<Author> authors = await author.GetAll();
                                            foreach (var item in authors)
                                            {
                                                await Console.Out.WriteAsync(item.Id+" "+item.FullName+" Books: ");
                                               await  book.GetBooksByAuthor(item.Id);
                                                await Console.Out.WriteAsync("\n");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                    }
                                    goto label2;
                                case 3:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Author to edit:");
                                            List<Author> authors = await author.GetAll();
                                            foreach (var item in authors)
                                            {
                                                await Console.Out.WriteLineAsync(item.ToString());
                                            }
                                            int id = Convert.ToInt32(Console.ReadLine());
                                            await Console.Out.WriteLineAsync("Enter new  fullname:");
                                            string? name = Console.ReadLine();
                                            try
                                            {
                                                if (name != null)
                                                    await author.Update(id, new Author() { FullName = name });
                                                await Console.Out.WriteLineAsync("Author edited");
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }


                                    }
                                    goto label2;
                                case 4:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Author to delete:");
                                            List<Author> authors = await author.GetAll();
                                            foreach (var item in authors)
                                            {
                                                await Console.Out.WriteLineAsync(item.ToString());
                                            }
                                            int id = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                await author.ChageDeleteStatus(id);
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                    }
                                    goto label2;

                                case 5:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Author to assign:");
                                            List<Author> authors = await author.GetAll();
                                            foreach (var item in authors)
                                            {
                                                await Console.Out.WriteLineAsync(item.ToString());
                                            }
                                            int id = Convert.ToInt32(Console.ReadLine());
                                            await Console.Out.WriteLineAsync("Choose id of Book to assign:");
                                            List<Book> books = await book.GetAll();
                                            foreach (var item in books)
                                            {
                                                await Console.Out.WriteLineAsync(item.Id + " " + item.Title);
                                            }
                                            int bookId = Convert.ToInt32(Console.ReadLine());
                                            await authorBook.AssignAuthorToTheBook(id, bookId);

                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                    }
                                    goto label2;
                                case 0:
                                    goto label1;
                                default:
                                    await Console.Out.WriteLineAsync("Invalid operation!");
                                    goto label2;
                            }

                        }

                    case 2:
                        {
                        label3:
                            Console.WriteLine("1.Book List\n" +
                               "2.Create book\n" +
                               "3.Edit book\n" +
                               "4.Delete book\n" +
                               "0.Exit");
                            int c2 = Convert.ToInt32(Console.ReadLine());
                            switch (c2)
                            {
                                case 1:
                                    {
                                        try
                                        {
                                            List<Book> books = await book.GetAll();
                                            foreach (var item in books)
                                            {
                                                await Console.Out.WriteAsync(item.ToString()+" Authors:");
                                                await author.GetAuthorsByBook(item.Id);
                                                await Console.Out.WriteAsync("\n");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                    }
                                    goto label3;
                                case 2:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter title:");
                                            string? name = Console.ReadLine();
                                            await Console.Out.WriteLineAsync("Enter Publish year:");
                                            int? year = Convert.ToInt32(Console.ReadLine());
                                            await Console.Out.WriteLineAsync("Enter description");
                                            string? desc = Console.ReadLine();
                                            if (name != null && year != null)
                                            {
                                                await book.Create(new Book() { Title = name, Desc = desc, PublishYear = year, Avilability = true });

                                            }
                                            else
                                            {
                                                await Console.Out.WriteLineAsync("Book Could not be created!");
                                            }

                                        }

                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }



                                    }
                                    goto label3;
                                case 3:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Book to edit:");
                                            List<Book> books = await book.GetAll();
                                            foreach (var item in books)
                                            {
                                                await Console.Out.WriteLineAsync(item.Id + " " + item.Title);
                                            }
                                            int id = Convert.ToInt32(Console.ReadLine());
                                        l1:
                                            await Console.Out.WriteLineAsync("Choose what you want to edit:\n" +
                                                "1.Title\n" +
                                                "2.Publish year\n" +
                                                "3.Description\n");
                                            int? choice = Convert.ToInt32(Console.ReadLine());
                                            string? name = null; int? year = null; string? desc = null;
                                            switch (choice)
                                            {
                                                case 1:
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter new title:");
                                                        name = Console.ReadLine();
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter new Publish year:");
                                                        year = Convert.ToInt32(Console.ReadLine());
                                                    }
                                                    break;
                                                case 3:
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter new description");
                                                        desc = Console.ReadLine();

                                                    }
                                                    break;
                                                default:
                                                    {
                                                        await Console.Out.WriteLineAsync("Invalid choice");
                                                        goto l1;
                                                    }

                                            }
                                            try
                                            {
                                                await book.Update(id, new Book() { Title = name, Desc = desc, PublishYear = year });
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }


                                    }
                                    goto label3;
                                case 4:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Book to delete:");
                                            List<Book> books = await book.GetAll();
                                            foreach (var item in books)
                                            {
                                                await Console.Out.WriteLineAsync(item.Id + " " + item.Title);
                                            }
                                            int choice = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                await book.ChageDeleteStatus(choice);
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }


                                    }
                                    goto label3;
                                case 0:
                                    goto label1;
                                default:
                                    await Console.Out.WriteLineAsync("Invalid operation!");
                                    goto label3;

                            }
                        }
                    case 3:
                        {
                        label4:
                            Console.WriteLine("1.Borrower List\n" +
                               "2.Create borrower\n" +
                               "3.Edit borrower\n" +
                               "4.Delete borrower\n" +
                               "0.Exit");
                            int c2 = Convert.ToInt32(Console.ReadLine());
                            switch (c2)
                            {
                                case 1:
                                    {
                                        try
                                        {
                                            List<Borrower> borrowers = await borrower.GetAll();
                                            foreach (var item in borrowers)
                                            {
                                                await Console.Out.WriteLineAsync(item.ToString());
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }

                                    }
                                    goto label4;
                                case 2:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Enter borrower fullname:");
                                            string? name = Console.ReadLine();
                                            await Console.Out.WriteLineAsync("Enter email:");
                                            string? email = Console.ReadLine();
                                            if (name != null && email != null)
                                            {
                                                await borrower.Create(new Borrower() { FullName = name, Email = email });
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }
                                    }
                                    goto label4;
                                case 3:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Borrower to edit:");
                                            List<Borrower> borrowers = await borrower.GetAll();
                                            foreach (var item in borrowers)
                                            {
                                                await Console.Out.WriteLineAsync(item.Id + " " + item.FullName);
                                            }
                                            int id = Convert.ToInt32(Console.ReadLine());
                                        l2:
                                            await Console.Out.WriteLineAsync("Choose what you want to edit:\n" +
                                                "1.Fullname\n" +
                                                "2.Email\n");
                                            int? choice = Convert.ToInt32(Console.ReadLine());
                                            string? name = null; string? email = null;
                                            switch (choice)
                                            {
                                                case 1:
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter new Fullname");
                                                        name = Console.ReadLine();

                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        await Console.Out.WriteLineAsync("Enter new Email");
                                                        email = Console.ReadLine();

                                                    }
                                                    break;
                                                default:
                                                    {
                                                        await Console.Out.WriteLineAsync("Invalid choice");
                                                        goto l2;
                                                    }
                                            }
                                            try
                                            {
                                                await borrower.Update(id, new Borrower() { FullName = name, Email = email });
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }


                                    }
                                    goto label4;
                                case 4:
                                    {
                                        try
                                        {
                                            await Console.Out.WriteLineAsync("Choose id of Borrower to delete:");
                                            List<Borrower> borrowers = await borrower.GetAll();
                                            foreach (var item in borrowers)
                                            {
                                                await Console.Out.WriteLineAsync(item.Id + " " + item.FullName);
                                            }
                                            int choice = Convert.ToInt32(Console.ReadLine());
                                            try
                                            {
                                                await borrower.ChageDeleteStatus(choice);
                                            }
                                            catch (Exception ex)
                                            {
                                                await Console.Out.WriteLineAsync(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Console.Out.WriteLineAsync(ex.Message);
                                        }



                                    }
                                    goto label4;
                                case 0:
                                    goto label1;
                                default:
                                    await Console.Out.WriteLineAsync("Invalid operation!");
                                    goto label4;

                            }

                        }
                    case 4:
                        {
                            try
                            {
                                await loan.BorrowBooks();
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }

                        }


                        goto label1;

                    case 5:
                        {
                            try
                            {
                                await loan.ReturnBooks();

                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }


                        }
                        goto label1;
                    case 6:
                        {
                            try
                            {
                                Book b = await book.GetMostBorrowedBook();
                                await Console.Out.WriteLineAsync(b.Title);
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }


                        }
                        goto label1;
                    case 7:
                        {
                            try
                            {
                                List<Borrower> lateBorrowers = await borrower.GetLateBorrowers();
                                foreach (var item in lateBorrowers)
                                {
                                    await Console.Out.WriteLineAsync(borrower.ToString());

                                }

                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }
                        }
                        goto label1;
                    case 8:
                        {
                            await borrower.GetBorrowersAndBooks();
                        }
                        goto label1;
                    case 9:
                        {
                            try
                            {
                                await Console.Out.WriteLineAsync("Enter title:");
                                string? s = Console.ReadLine();
                                if (s != null)
                                {
                                    List<Book> books = await book.FilterBooksByTitle(s);
                                    foreach (var item in books)
                                    {
                                        await Console.Out.WriteLineAsync(item.Id + " " + item.Title + " " + item.Desc);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }


                        }
                        goto label1;
                    case 10:
                        {
                            try
                            {
                                await Console.Out.WriteLineAsync("Enter title:");
                                string? s = Console.ReadLine();
                                if (s != null)
                                {
                                    List<Book> books = await book.FilterBooksByAuthor(s);
                                    foreach (var item in books)
                                    {
                                        await Console.Out.WriteLineAsync(item.Title);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }

                        }
                        goto label1;
                    case 0:
                        {
                            await Console.Out.WriteLineAsync("Program exited!");
                        }
                        break;
                    default:
                        {
                            await Console.Out.WriteLineAsync("Invalid operation!");

                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                goto label1;
            }
            
        }
    }
}