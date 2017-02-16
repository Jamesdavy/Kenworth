using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.PivotGrid.ServerMode;
using Newtonsoft.Json;

namespace WebApplication.Controllers.ViewModels.Job
{
    public class ViewModel
    {
        public ViewModel()
        {
            tblLines = new List<Lines>();
        }

        public long JobID { get; set; }
        public string OurOrderReference { get; set; }
        public DateTime? QuoteDate { get; set; }
        public long? ClientID { get; set; }
        public long? ContactID { get; set; }
        public string tblClientClientCompanyName { get; set; }
        public string tblContactForename { get; set; }
        public string tblContactSurname { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string ExpectedDeliveryDateString
        {
            get { return ExpectedDeliveryDate.GetValueOrDefault().ToShortDateString(); }
        }

        public string ContactName
        {
            get { return tblContactForename + " " + tblContactSurname; }
        }

        public string Description { get; set; }
        public string CustomerRef { get; set; }
        public string Comments { get; set; }
        public List<Lines> tblLines { get; set; }
        public List<File> tblJobFiles { get; set; }
        public string tblStatusName { get; set; }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }

        public class File
        {
            public Guid FileID { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public string Name { get; set; }

            public string FilePath
            {
                get
                {
                    //return System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/") + tblFileFileName;
                    return "/Documents/" + FileName;
                }
            }
        }

        public class Lines
        {
            public Lines()
            {
                tblPurchaseOrders = new List<BillOfMaterials>();
                tblTimesheets = new List<TimeSheets>();
                tblFiles = new List<File>();
            }


            public long LineID { get; set; }
            public long JobLineID { get; set; }
            public string Description { get; set; }
            public string tblStatusName { get; set; }
            public double? Quantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal CalculatedUnitPrice { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }
            public bool LegacyQuote { get; set; }
            public string tblFileFileName { get; set; }
            public string tblFileContentType { get; set; }

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
                get { return ExpectedDeliveryDate.GetValueOrDefault().ToShortDateString(); }
            }

            public string DeliveryComments { get; set; }
            public string DrawingNumber { get; set; }

            public List<BillOfMaterials> tblPurchaseOrders { get; set; }
            public List<TimeSheets> tblTimesheets { get; set; }
            public List<File> tblFiles { get; set; }

            public class BillOfMaterials
            {
                public long? tblLineJobID { get; set; }
                public long? tblLineJobLineID { get; set; }

                public string UniqueId
                {
                    get { return tblLineJobID + "/" + tblLineJobLineID; }
                }

                public long PurchaseOrderID { get; set; }
                public string Description { get; set; }
                public decimal Cost { get; set; }
                public long Quantity { get; set; }
                public string Comments { get; set; }
                public DateTime? PurchaseOrderDate { get; set; }

                public string PurchaseOrderDateString
                {
                    get { return PurchaseOrderDate.GetValueOrDefault().ToShortDateString(); }
                }
            }

            public class TimeSheets
            {
                public long TimesheetID { get; set; }
                public long? tblLineJobID { get; set; }
                public long? tblLineJobLineID { get; set; }
                public string UniqueId
                {
                    get { return tblLineJobID + "/" + tblLineJobLineID; }
                }
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

            public class File
            {
                public Guid FileID { get; set; }
                public string FileName { get; set; }
                public string ContentType { get; set; }
                public string Name { get; set; }

                public string FilePath
                {
                    get
                    {
                        //return System.Web.Hosting.HostingEnvironment.MapPath("~/Documents/") + tblFileFileName;
                        return "/Documents/" + FileName;
                    }
                }
            }

        }

        
    }
}