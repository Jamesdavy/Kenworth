using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Job
{
    public class QuotesModel
    {
        public long JobID { get; set; }
        public string OurOrderReference { get; set; }
        public string Description { get; set; }
        [Display(Name = "Company Name")]
        public string tblClientClientCompanyName { get; set; }
        public double? Value { get; set; }
        public DateTime? QuoteDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string CustomerRef { get; set; }

        public string QuoteDateString
        {
            get { return QuoteDate.GetValueOrDefault().ToShortDateString(); }
        }

        public string ExpectedDeliveryDateString
        {
            get { return ExpectedDeliveryDate.GetValueOrDefault().ToShortDateString(); }
        }


    }
}