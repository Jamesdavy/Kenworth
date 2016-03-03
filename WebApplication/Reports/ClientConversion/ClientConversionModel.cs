using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Reports.ClientConversion
{
    public class ClientConversionModel
    {
        public ClientConversionModel(double? threshhold, DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Threshhold = threshhold;
            Clients = new List<Client>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double? Threshhold { get; set; }
        public List<Client> Clients { get; set; } 

        public class Client
        {
            public string ClientCompanyName { get; set; }
            public double? QuotedValue { get; set; }
            public double? QuoteValue { get; set; }
            public double? WOPValue { get; set; }
            public double? CompletedValue { get; set; }
            public double? DeadValue { get; set; }
            public double? QuotePercentage { get; set; }
            public double? CompletedPercentage { get; set; }
            public double? WOPPercentage { get; set; }
            public double? DeadPercentage { get; set; }
        }
    }
}