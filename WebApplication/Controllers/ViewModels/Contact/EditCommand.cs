using System;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class EditCommand
    {
        public long ContactId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public class EditCommandValidator : AbstractValidator<EditCommand>
        {
            public EditCommandValidator()
            {
                RuleFor(x => x.ContactId).NotNull().NotEmpty();
                RuleFor(x => x.Forename).NotNull().NotEmpty();
                RuleFor(x => x.Surname).NotNull().NotEmpty();
            }
        }
    }
}