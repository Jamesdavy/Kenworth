using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using WebApplication.Controllers.ViewModels.Job;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class EditCommand
    {
        public long PurchaseOrderID { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public long Quantity { get; set; }
        public string Comments { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string SupplierRef { get; set; }

        public class EditCommandValidator : AbstractValidator<EditCommand>
        {
            public EditCommandValidator()
            {
                RuleFor(x => x.PurchaseOrderID).NotNull().NotEmpty();
                RuleFor(x => x.Description).NotNull().NotEmpty();
                RuleFor(x => x.Cost).NotNull().NotEmpty();
                RuleFor(x => x.Quantity).NotNull().NotEmpty();
                RuleFor(x => x.PurchaseOrderDate).NotNull().NotEmpty();
            }
        }
    }
}