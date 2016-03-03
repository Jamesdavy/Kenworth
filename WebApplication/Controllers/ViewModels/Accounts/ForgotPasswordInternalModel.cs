using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class ForgotPasswordInternalModel
    {
        public ForgotPasswordInternalModel() { }

        public ForgotPasswordInternalModel(ApplicationUser user)
        {
            ID = user.Id;
            UserName = user.UserName;
        }

        public string ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NewPassword { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmNewPassword { get; set; }
    }
}