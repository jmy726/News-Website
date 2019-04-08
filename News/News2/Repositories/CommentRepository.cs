using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using News2.Interface;
using News2.Models;

namespace News2.Repositories
{
    public class CommentRepository : ICommentRepository1
    {
        NewsEntities db = new NewsEntities();
        IEnumerable<Models.Comment> ICommentRepository1.GetAll()
        {
            return db.Comments;
        }
        Models.Comment ICommentRepository1.Get(int Id)
        {
           return db.Comments.Find(Id);
        }

         Models.Comment ICommentRepository1.Add(Models.Comment item)
        {
            if (item == null){ throw new ArgumentNullException("item");
        }
         //Code to save record into database
         db.Comments.Add(item);
         db.SaveChanges();
         return item;
        }
        bool ICommentRepository1.Update(Models.Comment item)
        {
           if (item == null) { throw new ArgumentNullException("item"); }
           //Code to update record into database
           var comments = db.Comments.Single(a => a.Id == item.Id);
           comments.comment1 = item.comment1;
           comments.rating = item.rating;
           comments.articleId = item.articleId;
           comments.name = item.name;
           db.SaveChanges();
           return true;
        }
       bool ICommentRepository1.Delete(int Id)
       {
         //throw new NotImplementedException();
         Comment comments = db.Comments.Find(Id);
         db.Comments.Remove(comments);
         db.SaveChanges();
        return true;
       }
    }
}