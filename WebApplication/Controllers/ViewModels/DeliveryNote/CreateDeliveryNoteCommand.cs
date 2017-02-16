using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication.Controllers.ViewModels.DeliveryNote
{
    public class CreateDeliveryNoteCommand
    {
        public long JobId { get; set; }
        public List<DeliveryNote> DeliveryNotes { get; set; }

        public class DeliveryNote
        {
            public long LineId { get; set; }
            public double Quantity { get; set; }
            public double QuantityAlreadyDispatched { get; set; }
            public double QuantityToDispatch { get; set; }
            //public bool SendDeliveryNote { get; set; }
        }
    }

    public class CreateDeliveryNoteCommandValidator : AbstractValidator<CreateDeliveryNoteCommand>
    {
        public CreateDeliveryNoteCommandValidator()
        {
            RuleFor(x => x.JobId).NotNull().NotEmpty();
            RuleFor(x => x.DeliveryNotes).SetCollectionValidator(new DeliveryNoteValidator());
            RuleFor(x => x.JobId).Must((o, qty) =>
                {
                    return o.DeliveryNotes.Any(x => x.QuantityToDispatch > 0);
                }).WithMessage("Must be at least one line with a Quantity to Dispatch");
        }

        public class DeliveryNoteValidator : AbstractValidator<CreateDeliveryNoteCommand.DeliveryNote>
        {
            public DeliveryNoteValidator()
            {
                RuleFor(x => x.LineId).NotNull().NotEmpty();
                RuleFor(x => x.QuantityToDispatch).Must((o, qty) =>
                {
                    return o.QuantityAlreadyDispatched + o.QuantityToDispatch <= o.Quantity;
                }).WithMessage("Remaining Quatity to Disaptch cannot be less than Zero");

                RuleFor(x => x.QuantityAlreadyDispatched).Must((o, qty) =>
                {
                    return o.QuantityAlreadyDispatched >= 0;
                }).WithMessage("quantity already Dispatched cannot be Negative");

                RuleFor(x => x.QuantityToDispatch).Must((o, qty) =>
                {
                    return o.QuantityToDispatch <= o.Quantity;
                }).WithMessage("Cannot dispatch more than the Quantity");

                RuleFor(x => x.QuantityToDispatch).Must((o, qty) =>
                    {
                        return o.Quantity - o.QuantityAlreadyDispatched - o.QuantityToDispatch >= 0;
                    }
                ).WithMessage("You have entere a Quantity to Dispatch which is greater than the Quantity left to Dispatch");
            }
        }
    }
}