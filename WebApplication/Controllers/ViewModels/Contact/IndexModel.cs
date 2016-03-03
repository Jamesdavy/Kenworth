using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class IndexModel
    {
        public long ContactID { get; set; }
        [Display(Name = "Client")]
        public string tblClientClientCompanyName { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Enabled")]
        public string Status { get; set; }
    }
}