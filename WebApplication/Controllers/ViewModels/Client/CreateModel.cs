using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class CreateModel
    {
        [Display(Name = "Client Name")]
        public string ClientCompanyName { get; set; }
        [Display(Name = "Address 1")]
        public string ClientAddress1 { get; set; }
        [Display(Name = "Address 2")]
        public string ClientAddress2 { get; set; }
        [Display(Name = "Town")]
        public string ClientTown { get; set; }
        [Display(Name = "County")]
        public string ClientCounty { get; set; }
        [Display(Name = "Post Code")]
        public string ClientPostCode { get; set; }
        [Display(Name = "Telephone")]
        public string ClientTelephone { get; set; }
        [Display(Name = "Email")]
        public string ClientEmail { get; set; }
        [Display(Name = "Fax")]
        public string ClientFax { get; set; }
        [Display(Name = "WWW")]
        public string ClientWWW { get; set; }
        [Display(Name = "Accounts Email")]
        public string AccountsEmail { get; set; }
        [Display(Name = "Copy to Accounts")]
        public bool CopyToAccounts { get; set; }
       
    }

    public class CreateModelValidator : AbstractValidator<CreateModel>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.ClientCompanyName).NotEmpty();
            //RuleFor(x => x.ClientAddress1).NotEmpty();
            //RuleFor(x => x.ClientEmail).NotEmpty();
        }
    }
}