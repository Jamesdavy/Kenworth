using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class EditCommand
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class EditCommandValidator : AbstractValidator<EditCommand>
    {
        public EditCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
        }
    }
}