using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class ListModel
    {
        public long ClientID { get; set; }
        [Display(Name = "Company Name")]
        public string ClientCompanyName { get; set; }
        [Display(Name = "Enabled")]
        public string Status { get; set; }
    }
}