using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using News2.Interface;
using News2.Models;
using News2.Repositories;

namespace News2.Controllers
{
    [RoutePrefix("api/Book")]
    public class BookController : ApiController
    {
        static readonly IBookRepository1 repository = new BookRepository();

        //[Route("")]
        public IEnumerable<Book> GetAllBooks()
        {
            return repository.GetAll();
        }
        public Book PostBook(Book item)
        {
            return repository.Add(item);
        }
        public IEnumerable<Book> PutBook(int id, Book book)
        {
            book.Id = id;
            if (repository.Update(book))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }
        public bool DeleteBook(int id)
        {
            if (repository.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

