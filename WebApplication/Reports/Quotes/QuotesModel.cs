using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApplication.Reports.Quotes
{
    public class QuotesModel
    {
        public QuotesModel(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Quotes = new List<Quote>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Quote> Quotes { get; set; } 

        public class Quote
        {
            public long JobID { get; set; }
            public string Description { get; set; }
            public double? Value { get; set; }
            public DateTime? QuoteDate { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }

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