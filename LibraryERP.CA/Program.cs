using LibraryERP.Business.Implementations;
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
        label1:
            Console.WriteLine("Choose action");
            Console.WriteLine("1.Author actions" +
                "2.Book actions" +
                "3.Borrower actions" +
                "4.Borrow book" +
                "5.Return book" +
                "6.Bring book most borrowing" +
                "7.Bring borrowers who is late" +
                "8.Bring borrowers and books" +
                "9.Filter books by Titles" +
                "10.Filter books by Author Fullname" +
                "0. Exit");
            int c1 = Convert.ToInt32(Console.ReadLine());
            switch (c1)
            {
                case 1:
                    {
                        label2:
                        Console.WriteLine("1.Author List" +
                            "2.Create author" +
                            "3.Edit author" +
                            "4.Delete author" +
                            "0.Exit");
                        int c2 = Convert.ToInt32(Console.ReadLine());
                        switch (c2)
                        {
                            case 2:
                                { 
                                    Console.WriteLine("Enter fullname:");
                                    string? name=Console.ReadLine();
                                    if(name != null)
                                    {
                                       await author.Create(new Author() { FullName = name });
                                    }
                                    else
                                    {
                                        await Console.Out.WriteLineAsync("Author could not be created!");
                                        
                                    }


                                }
                                goto label2;
                                case 1:
                                {
                                    List<Author> authors = await author.GetAll();
                                    foreach (var item in authors)
                                    {
                                        await Console.Out.WriteLineAsync(item.ToString());
                                    }
                                }
                                goto label2;
                            case 3:
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
                                    { if(name !=null)
                                        await author.Update(id, new Author() { FullName = name });
                                    }
                                    catch(Exception ex)
                                    {
                                        await Console.Out.WriteLineAsync(ex.Message);
                                    }

                                }
                                goto label2;
                            case 4:
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
                                goto label2;
                            case 0:
                                goto label1;
                            default:
                                await Console.Out.WriteLineAsync("Invalid operation!");
                                goto label2;
                        }

                    }
                   
                case 2:
                    { label3:
                        Console.WriteLine("1.Book List" +
                           "2.Create book" +
                           "3.Edit book" +
                           "4.Delete book" +
                           "0.Exit");
                        int c2 = Convert.ToInt32(Console.ReadLine());
                        switch (c2) {
                            case 1: {

                                    List<Book> books = await book.GetAll();
                                    foreach (var item in books)
                                    {
                                        await Console.Out.WriteLineAsync(item.ToString());
                                    }
                                }
                                goto label3;
                            case 2: {
                                    await Console.Out.WriteLineAsync("Enter title:");
                                    string? name = Console.ReadLine();
                                    await Console.Out.WriteLineAsync("Enter Publish year:");
                                    int? year = Convert.ToInt32(Console.ReadLine());
                                    await Console.Out.WriteLineAsync("Enter description");
                                    string? desc=Console.ReadLine();
                                    if(name !=null && year != null)
                                    {
                                        await book.Create(new Book() { Title = name, Desc = desc, PublishYear = year, Avilability = true });

                                    }
                                    else
                                    {
                                        await Console.Out.WriteLineAsync("Book Could not be created!");
                                    }


                                }
                                goto label3;
                            case 3:
                                {
                                    await Console.Out.WriteLineAsync("Choose id of Book to edit:");
                                    List<Book> books = await book.GetAll();
                                    foreach (var item in books)
                                    {
                                        await Console.Out.WriteLineAsync(item.Id+" "+item.Title);
                                    }
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    l1:
                                    await Console.Out.WriteLineAsync("Choose what you want to edit:" +
                                        "1.Title" +
                                        "2.Publish year" +
                                        "3.Description");
                                    int? choice = Convert.ToInt32(Console.ReadLine());
                                    string? name=null; int? year=null;  string? desc = null;
                                    switch (choice) { 
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
                                goto label3;
                            case 4:
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
                                goto label3;
                            case 0:
                                goto label1;
                            default:
                                await Console.Out.WriteLineAsync("Invalid operation!");
                                goto label3;

                        }


                    }
                    goto label1;


            }
        }
    }
}
