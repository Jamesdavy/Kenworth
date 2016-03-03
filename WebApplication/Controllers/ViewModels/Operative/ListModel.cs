using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Operative
{
    public class ListModel
    {
        public Guid UserID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Enabled")]
        public bool StatusID { get; set; }
    }
}