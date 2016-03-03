using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Reports.Quotes
{
    public class QuotesProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblJob, QuotesModel.Quote>()
                .ForMember(m => m.Value, opt => opt.MapFrom(
                    c => c.tblLines.Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))));

        }

    }
}