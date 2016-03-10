using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class EditViewModel
    {
        public long tblJobJobID { get; set; }
        public long LineId { get; set; }

        public long JobLineId { get; set; }

        public string UniqueId
        {
            get { return tblJobJobID + "/" + JobLineId; }
        }

        public DateTime? tblJobQuoteDate { get; set; }
        public string Description { get; set; }
        public double? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string DeliveryComments { get; set; }
        public string DrawingNumber { get; set; }
        public string tblStatusName { get; set; }

        public string tblFileFileName { get; set; }
        public string tblFileContentType { get; set; }

        public double EstimatedHours { get; set; }
        public decimal EstimatedHourlyRate { get; set; }
        public decimal CalculatedUnitPrice { get; set; }

        public string CustomerRef { get; set; }

        public bool LegacyQuote
        {
            get
            {
                if (tblJobQuoteDate < new DateTime(2016, 01, 01))
                {
                    return true;
                }
                return false;
            }
        }

        public string FilePath
        {
            get
            {
                //return System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/") + tblFileFileName;
                return "/Documents/" + tblFileFileName;
            }
        }

        public string ExpectedDeliveryDateString
        {
            get
            {
                return ExpectedDeliveryDate.GetValueOrDefault().ToShortDateString();
            } 
        }

        public List<BillOfMaterials> tblPurchaseOrders { get; set; }
        public List<TimeSheets> tblTimesheets { get; set; }

        public class BillOfMaterials
        {
            public long PurchaseOrderID { get; set; }
            public string Description { get; set; }
            public decimal Cost { get; set; }
            public long Quantity { get; set; }
            public string Comments { get; set; }
            public DateTime? PurchaseOrderDate { get; set; }
            public string SupplierRef { get; set; }

            public string PurchaseOrderDateString
            {
                get { return PurchaseOrderDate.GetValueOrDefault().ToShortDateString(); }
            }
        }

        public class TimeSheets
        {
            public long TimesheetID { get; set; }
            public string Comments { get; set; }
            public double Hours { get; set; }
            public decimal HourlyRate { get; set; }
            public DateTime? TimesheetDate { get; set; }

            public string tblUserForename { get; set; }
            public string tblUserSurname { get; set; }

            public string OperativeName
            {
                get { return tblUserForename + " " + tblUserSurname; }
            }

            public string TimesheetDateString
            {
                get { return TimesheetDate.GetValueOrDefault().ToShortDateString(); }
            }

        }


        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}