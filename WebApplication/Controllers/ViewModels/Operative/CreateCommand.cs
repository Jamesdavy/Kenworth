using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Operative
{
    public class CreateCommand
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }
        public bool StatusID { get; set; }
    }

    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Forename).NotNull();
            RuleFor(x => x.Surname).NotNull();
        }
    }
}