using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using News2.Interface;
using News2.Models;

namespace News2.Repositories
{
        public class BookRepository : IBookRepository1
        {
            NewsEntities BookDB = new NewsEntities();
            IEnumerable<Models.Book> IBookRepository1.GetAll()
            {
                //throw new NotImplementedException();
                return BookDB.Books;
            }
            Models.Book IBookRepository1.Get(int id)
            {
                //throw new NotImplementedException();
                return BookDB.Books.Find(id);
            }
            Models.Book IBookRepository1.Add(Models.Book item)
            {
                //throw new NotImplementedException();
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                // TO DO : Code to save record into database
                BookDB.Books.Add(item);
                BookDB.SaveChanges();
                return item;
            }
            bool IBookRepository1.Update(Models.Book item)
            {
                //throw new NotImplementedException();
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                // TO DO : Code to update record into database
                var books = BookDB.Books.Single(a => a.Id == item.Id);
                books.BookTitle = item.BookTitle;
                books.Author = item.Author;
                books.Price = item.Price;
                BookDB.SaveChanges();
                return true;
            }
            bool IBookRepository1.Delete(int id)
            {
                //throw new NotImplementedException();
                Book books = BookDB.Books.Find(id);
                BookDB.Books.Remove(books);
                BookDB.SaveChanges();
                return true;
            }
        }
    }