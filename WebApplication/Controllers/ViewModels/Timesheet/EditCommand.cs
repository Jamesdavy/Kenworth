using System;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class EditCommand
    {
        public long TimesheetID { get; set; }
        public string Comments { get; set; }
        public double Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime? TimesheetDate { get; set; }


        public class EditCommandValidator : AbstractValidator<EditCommand>
        {
            public EditCommandValidator()
            {
                RuleFor(x => x.TimesheetID).NotNull().NotEmpty();
                RuleFor(x => x.Hours).NotNull().NotEmpty();
                RuleFor(x => x.HourlyRate).NotNull().NotEmpty();
                RuleFor(x => x.TimesheetDate).NotNull().NotEmpty();
            }
        }
    }
}