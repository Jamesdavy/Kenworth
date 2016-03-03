using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Reports.Jobs
{
    public class JobsProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblJob, JobsModel.Job>()
                .ForMember(m => m.QuotedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
                .ForMember(m => m.JobValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 4).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
                .ForMember(m => m.CompletedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))));
        }
    }
}