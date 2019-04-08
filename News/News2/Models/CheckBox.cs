using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace News2.Models
{
    public class CheckBox
    {
        public bool IsSelected { get; set; }
        public string Email { get; set; }
        public string JournalistId { get; set; }
        public string JournalistFirstName { get; set; }
        public string JournalistLastName { get; set; }
    }
    public class CheckBoxList
    {
        public List<CheckBox> checklist { get; set; }
        public string message { get; set; }

        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Recipients' Email")]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        //[RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))+$", ErrorMessage = "Please enter correct email")]
        public string ToEmail { get; set; }

        public string FromEmail { get; set; }

        public HttpPostedFileBase Upload { get; set; }
        public IEnumerable<string> GetSelectedEmail()
        {
            return (from email in checklist
                    where email.IsSelected
                    select email.Email).ToList();
        }
    }
 }