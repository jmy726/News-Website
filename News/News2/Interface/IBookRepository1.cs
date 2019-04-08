using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News2.Models;

namespace News2.Interface
{
    interface IBookRepository1
    {
        IEnumerable<Book> GetAll();
        Book Get(int id);
        Book Add(Book item);
        bool Update(Book item);
        bool Delete(int id);
    }
}
