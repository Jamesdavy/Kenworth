using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class ForgotPasswordResultModel
    {
        public ForgotPasswordResultModel(string id)
        {
            ID = id;
        }

        public string ID { get; set; }
    }
}