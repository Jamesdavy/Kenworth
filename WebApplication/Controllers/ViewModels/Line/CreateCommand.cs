using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class CreateCommand
    {
        public long JobId { get; set; }
        public string Description { get; set; }
        public bool LegacyQuote { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string DeliveryComments { get; set; }
        public string DrawingNumber { get; set; }

        public double EstimatedHours { get; set; }
        public decimal EstimatedHourlyRate { get; set; }

        //public string CustomerRef { get; set; }

        //public FileCommand File { get; set; }
        //public string FileObjectURL { get; set; }
        //public byte[] FileBinary { get; set; }
        //public string FileType { get; set; }

        
    }

    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().NotEmpty();
            RuleFor(x => x.UnitPrice).NotNull();
            RuleFor(x => x.ExpectedDeliveryDate).NotNull().NotEmpty();
            RuleFor(x => x.EstimatedHours).NotNull();
            RuleFor(x => x.EstimatedHourlyRate).NotNull();
            //RuleFor(x => x.DrawingNumber).NotEmpty().When(x => x.File != null);
        }
    }

}