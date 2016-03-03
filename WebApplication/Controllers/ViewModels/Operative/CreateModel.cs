using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Operative
{
    public class CreateModel
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }
        [Display(Name = "Enabled")]
        public bool StatusID { get; set; }

    }

    public class CreateModelValidator : AbstractValidator<CreateModel>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.Forename).NotNull();
            RuleFor(x => x.Surname).NotNull();
        }
    }
}