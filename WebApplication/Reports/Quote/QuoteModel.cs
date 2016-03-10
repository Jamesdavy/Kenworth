using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTitleIndex.Internal;

namespace WebApplication.Reports.Quote
{
    public class QuoteModel
    {
        public long JobID { get; set; }
        public string tblClientClientCompanyName { get; set; }
        public string tblContactForename { get; set; }
        public string tblContactSurname { get; set; }
        public DateTime? QuoteDate { get; set; }

        public string AttentionOf { get { return tblContactForename + " " + tblContactSurname; } }
        
        public string From { get; set; }
        public List<Quote> tblLines { get; set; }

        public class Quote{

            public string UniqueId
            {
                get { return JobID + "/" + JobLineID; }
            }

            public double LineTotal
            {
                get { return Quantity.GetValueOrDefault()*(double) CalculatedUnitPrice; }
            }

            public long? JobID { get; set; }
            public long JobLineID { get; set; }
            public string Description { get; set; }
            public string DrawingNumber { get; set; }
            public DateTime? ExpectedDeliveryDate { get; set; }
            public double? Quantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal CalculatedUnitPrice { get; set; }
        }

    }
}