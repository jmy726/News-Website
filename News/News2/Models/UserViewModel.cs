using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News2.Models
{
    public class UserViewModel
    {
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<Journalist> Journalists { get; set; }
    }
}