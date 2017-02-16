using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Controllers.ViewModels.Job
{
    public class ListModel
    {
        public long JobID { get; set; }
        public string OurOrderReference { get; set; }
        public string Description { get; set; }
        [Display(Name = "Company Name")]
        public string tblClientClientCompanyName { get; set; }
        [Display(Name = "Status")]
        public string tblStatusName { get; set; }
        public DateTime? QuoteDate { get; set; }
        public string CustomerRef { get; set; }

        public string QuoteDateString
        {
            get { return QuoteDate.GetValueOrDefault().ToShortDateString(); }
        }
    }
}