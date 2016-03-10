using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblLine
    {
        public tblLine(string description, long jobLineId, int? status, double? quantity, decimal? unitPrice, DateTime? expectedDeliveryDate, string deliveryComments, string drawingNumber, string customerRef, double estimatedHours, decimal estimatedHourlyRate) : this()
        {
            Description = description;
            JobLineID = jobLineId;
            Status = status;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ExpectedDeliveryDate = expectedDeliveryDate.GetValueOrDefault().Date;
            DeliveryComments = deliveryComments;
            DrawingNumber = drawingNumber;
            CustomerRef = customerRef ?? "";
            EstimatedHours = estimatedHours;
            EstimatedHourlyRate = estimatedHourlyRate;
            CalculatedUnitPrice = CalculateUnitPrice();
        }

        public decimal CalculateUnitPrice()
        {
            if (tblJob.QuoteDate < new DateTime(2016, 01, 01))
            {
                return UnitPrice.GetValueOrDefault();
            }
            decimal labourTotal = (((decimal) EstimatedHours*EstimatedHourlyRate) + UnitPrice.GetValueOrDefault())*
                                  (decimal) Quantity.GetValueOrDefault();
            decimal bomTotal = 0;
            foreach (var tblPurchaseOrder in tblPurchaseOrders)
            {
                bomTotal = bomTotal + (tblPurchaseOrder.Cost*tblPurchaseOrder.Quantity);
            }
            return (bomTotal + labourTotal)/(decimal) Quantity.GetValueOrDefault();
        }

        public tblTimesheet GetTimesheet(long timesheetId)
        {
            var timesheet = tblTimesheets.Where(x => x.TimesheetID == timesheetId).SingleOrDefault();
            return timesheet;
        }

        public tblPurchaseOrder GetBillOfMaterials(long billOfMaterialsId)
        {
            var billOfMaterials = tblPurchaseOrders.Where(x => x.PurchaseOrderID == billOfMaterialsId).SingleOrDefault();
            return billOfMaterials;
        }
    }
}