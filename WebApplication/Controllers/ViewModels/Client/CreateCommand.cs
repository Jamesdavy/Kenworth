using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class CreateCommand
    {
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

    }

    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.ClientCompanyName).NotNull().NotEmpty();
            //RuleFor(x => x.ClientAddress1).NotNull().NotEmpty();
            //RuleFor(x => x.ClientEmail).NotNull().NotEmpty();
        }
    }
}