using System;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class CreateCommand
    {
        public long LineId { get; set; }
        public string Comments { get; set; }
        public Guid UserId { get; set; }
        public double Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime? TimesheetDate { get; set; }

        public class CreateCommandValidator : AbstractValidator<CreateCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(x => x.LineId).NotNull().NotEmpty();
                RuleFor(x => x.UserId).NotNull().NotEmpty();
                RuleFor(x => x.Hours).NotNull().NotEmpty();
                RuleFor(x => x.HourlyRate).NotNull().NotEmpty();
                RuleFor(x => x.TimesheetDate).NotNull().NotEmpty();
            }
        }
    }
}