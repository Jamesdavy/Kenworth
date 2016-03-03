using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class EditModel
    {
        public long ClientID { get; set; }
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
        [Display(Name = "Enabled")]
        public bool Status { get; set; }
    }

    public class EditModelValidator : AbstractValidator<EditModel>
    {
        public EditModelValidator()
        {
            RuleFor(x => x.ClientID).NotEmpty();
            RuleFor(x => x.ClientCompanyName).NotEmpty();
            RuleFor(x => x.AccountsEmail).NotEmpty().When(x => x.CopyToAccounts);
            //RuleFor(x => x.ClientAddress1).NotEmpty();
            //RuleFor(x => x.ClientEmail).NotEmpty();
        }
    }
}