using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class EditCommand
    {
        public long LineId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string DeliveryComments { get; set; }
        public string DrawingNumber { get; set; }

        public double EstimatedHours { get; set; }
        public string CustomerRef { get; set; }

        public FileCommand File { get; set; }
        public string FileObjectURL { get; set; }
        public byte[] FileBinary { get; set; }
        public string FileType { get; set; }

        
    }

    public class EditCommandValidator : AbstractValidator<EditCommand>
    {
        public EditCommandValidator()
        {
            RuleFor(x => x.LineId).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().NotEmpty();
            RuleFor(x => x.UnitPrice).NotNull().NotEmpty();
            RuleFor(x => x.ExpectedDeliveryDate).NotNull().NotEmpty();
        }
    }

}