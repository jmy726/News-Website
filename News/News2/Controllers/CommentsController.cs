using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using News2.Interface;
using News2.Models;
using News2.Repositories;

namespace News2.Controllers
{
    public class CommentsController : ApiController
    {
        static readonly ICommentRepository1 repository = new CommentRepository();
        public IEnumerable<Comment> GetAllComments()
        {
            return repository.GetAll();
        }
        public Comment PostComment(Comment item)
        {
            return repository.Add(item);
        }
        public IEnumerable<Comment> PutComment(int id, Comment comment)
        {
            comment.Id = id;
            if (repository.Update(comment))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }
        public bool DeleteComment(int commentid)
        {
            if (repository.Delete(commentid))
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
