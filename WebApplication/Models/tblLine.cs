using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DatabaseFirst
{
    public partial class tblLine
    {
        public tblLine(tblJob job, string description, long jobLineId, int? status, bool legacyQuote, double? quantity, decimal? unitPrice, DateTime? expectedDeliveryDate, string deliveryComments, string drawingNumber, double estimatedHours, decimal estimatedHourlyRate) : this()
        {
            tblJob = job;
            Description = description;
            JobLineID = jobLineId;
            Status = status;
            LegacyQuote = legacyQuote;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ExpectedDeliveryDate = expectedDeliveryDate.GetValueOrDefault().Date;
            DeliveryComments = deliveryComments;
            DrawingNumber = drawingNumber;
            CustomerRef = "Not Used";
            EstimatedHours = estimatedHours;
            EstimatedHourlyRate = estimatedHourlyRate;
            CalculatedUnitPrice = CalculateUnitPrice();
        }

        public decimal CalculateUnitPrice()
        {
            //if (tblJob.QuoteDate < new DateTime(2016, 01, 01))
            //{
            //    return UnitPrice.GetValueOrDefault();
            //}
            if (LegacyQuote)
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
            var timesheet = tblTimesheets.SingleOrDefault(x => x.TimesheetID == timesheetId);
            return timesheet;
        }

        public void AddFile(tblFile file)
        {
            //if (tblFiles.Count == 0)
            //{
                tblFiles.Add(file);
            //}
            //else
            //{
            //    var currentfile = tblFiles.FirstOrDefault();
            //    currentfile.FileID = file.FileID;
            //    currentfile.FileName = file.FileName;
            //    currentfile.ContentType = file.ContentType;
            //}
        }

        //public void AddFile(Guid fileId, string name, string fileName, string contentType)
        //{
        //    //if (tblFiles.Count == 0)
        //    //{
        //        tblFiles.Add(new tblFile(fileId, name, fileName, contentType));
        //    //}
        //    //else
        //    //{
        //    //    var currentfile = tblFiles.FirstOrDefault();
        //    //    currentfile.FileID = fileId;
        //    //    currentfile.FileName = fileName;
        //    //    currentfile.ContentType = contentType;
        //    //}
        //}

        //public void DeleteFile(Guid fileId)
        //{
        //    //if (tblFiles.Count() != 0)
        //    //{
        //    var file = tblFiles.SingleOrDefault(x => x.FileID == fileId);
        //    tblFiles.Remove(file);
        //    //file.FileName = "";
        //    //file.ContentType = "";
        //    //file.FileID = Guid.Empty;
        //    //}
        //}

        public tblPurchaseOrder GetBillOfMaterials(long billOfMaterialsId)
        {
            var billOfMaterials = tblPurchaseOrders.SingleOrDefault(x => x.PurchaseOrderID == billOfMaterialsId);
            return billOfMaterials;
        }
    }
}