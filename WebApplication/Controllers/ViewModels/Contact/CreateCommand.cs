using System;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class CreateCommand
    {

        public long ClientId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public class CreateCommandValidator : AbstractValidator<CreateCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(x => x.ClientId).NotNull().NotEmpty();
                RuleFor(x => x.Forename).NotNull().NotEmpty();
                RuleFor(x => x.Surname).NotNull().NotEmpty();
            }
        }
    }
}