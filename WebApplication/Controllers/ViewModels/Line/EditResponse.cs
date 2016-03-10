using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class EditResponse
    {
        public long LineId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string DeliveryComments { get; set; }
        public string DrawingNumber { get; set; }

        public double EstimatedHours { get; set; }
        public decimal EstimatedHourlyRate { get; set; }
        public decimal CalculatedUnitPrice { get; set; }

        public string CustomerRef { get; set; }

        public string FileName { get; set; }
        public string ContentType { get; set; }

        public string ExpectedDeliveryDateString
        {
            get { return ExpectedDeliveryDate.ToShortDateString(); }
        }

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