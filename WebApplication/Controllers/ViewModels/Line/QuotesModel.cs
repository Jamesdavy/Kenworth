using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Line
{
    public class QuotesModel
    {
        [Display(Name = "Job Id")]
        public long tblJobJobID { get; set; }
        public string tblJobOurOrderReference{ get; set; }
        public long LineId { get; set; }

        public long JobLineId { get; set; }

        public string UniqueId
        {
            get { return tblJobOurOrderReference + "/" + JobLineId; }
        }

        [Display(Name = "Company Name")]
        public string tblJobtblClientClientCompanyName { get; set; }
        public string Description { get; set; }
        public double? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        [Display(Name = "Exp Delivery Date")]
        public DateTime? ExpectedDeliveryDate { get; set; }
        [Display(Name = "Drawing No.")]
        public string DrawingNumber { get; set; }
        [Display(Name = "Status")]
        public string tblStatusName { get; set; }
        public double? Value { get; set; }

        public string ExpectedDeliveryDateString
        {
            get { return ExpectedDeliveryDate.GetValueOrDefault().ToShortDateString(); }
        }
    }
}