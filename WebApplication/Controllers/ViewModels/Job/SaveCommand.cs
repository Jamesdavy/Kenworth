using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Job
{
    public class SaveCommand
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ContactId { get; set; }
        public string Description { get; set; }
        public string CustomerRef { get; set; }
        public string Comments { get; set; }
    }

    public class SaveCommandValidator : AbstractValidator<SaveCommand>
    {
        public SaveCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.ClientId).NotNull().NotEmpty();
            RuleFor(x => x.ContactId).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }
}