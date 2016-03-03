using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Reports.Jobs
{
    public class JobsModel
    {
        public JobsModel(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Jobs = new List<Job>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Job> Jobs { get; set; } 

        public class Job
        {
            public long JobID { get; set; }
            public string Description { get; set; }
            public DateTime? QuoteDate { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }
            public double? QuotedValue { get; set; }
            public double? JobValue { get; set; }
            public double? CompletedValue { get; set; }

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
}