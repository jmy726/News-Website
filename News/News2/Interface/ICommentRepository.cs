using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News2.Models;

namespace News2.Interface
{
    interface ICommentRepository1
    {
        IEnumerable<Comment> GetAll();
        Comment Get(int commentId);
        Comment Add(Comment item);
        bool Update(Comment item);
        bool Delete(int commentId);
    }
}
