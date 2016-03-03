using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class ForgotPasswordInternalCommand
    {
        public string Id { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }

    public class ForgotPasswordInternalCommandValidator : AbstractValidator<ForgotPasswordInternalCommand>
    {
        public ForgotPasswordInternalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmNewPassword);
        }
    }
}