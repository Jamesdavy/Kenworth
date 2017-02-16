using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication.Reports.ClientConversion;
using WebApplication.Reports.DeliveryNote;
using WebApplication.Reports.JobCard;
using WebApplication.Reports.Jobs;
using WebApplication.Reports.Quote;
using WebApplication.Reports.Quotes;
using WebApplication.Reports.SageConsolidation;
using WebApplication.Reports.WorkInProgress;

namespace WebApplication.Reports
{
    public class ReportProfile
    {
        public static void Configure()
        {
            Mapper.AddProfile(new QuoteProfile());
            Mapper.AddProfile(new QuotesProfile());
            Mapper.AddProfile(new JobsProfile());
            Mapper.AddProfile(new DeliveryNoteProfile());
            Mapper.AddProfile(new ClientConversionProfile());
            Mapper.AddProfile(new JobCardProfile());
            Mapper.AddProfile(new SageConsolidationProfile());
            Mapper.AddProfile(new WorkInProgressProfile());

        }
    }
}