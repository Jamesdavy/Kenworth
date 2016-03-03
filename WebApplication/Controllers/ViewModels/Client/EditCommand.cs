using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class EditCommand
    {
        public long ClientID { get; set; }
        public string ClientCompanyName { get; set; }
        public string ClientAddress1 { get; set; }
        public string ClientAddress2 { get; set; }
        public string ClientTown { get; set; }
        public string ClientCounty { get; set; }
        public string ClientPostCode { get; set; }
        public string ClientTelephone { get; set; }
        public string ClientEmail { get; set; }
        public string ClientFax { get; set; }
        public string ClientWWW { get; set; }
        public string AccountsEmail { get; set; }
        public bool CopyToAccounts { get; set; }
        public bool Status { get; set; }

    }

    public class EditCommandValidator : AbstractValidator<EditCommand>
    {
        public EditCommandValidator()
        {
            RuleFor(x => x.ClientID).NotNull();
            RuleFor(x => x.ClientCompanyName).NotNull().NotEmpty();
            RuleFor(x => x.AccountsEmail).NotEmpty().When(x => x.CopyToAccounts);
            //RuleFor(x => x.ClientAddress1).NotNull().NotEmpty();
            //RuleFor(x => x.ClientEmail).NotNull().NotEmpty();
        }
    }
}