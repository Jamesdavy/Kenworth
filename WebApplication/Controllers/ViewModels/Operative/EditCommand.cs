using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace WebApplication.Controllers.ViewModels.Operative
{
    public class EditCommand
    {
        public Guid UserID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }
        public bool StatusID { get; set; }
    }

    public class EditCommandValidator : AbstractValidator<EditCommand>
    {
        public EditCommandValidator()
        {
            RuleFor(x => x.Forename).NotNull().NotEmpty();
            RuleFor(x => x.Surname).NotNull().NotEmpty();
        }
    }
}