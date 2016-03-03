using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace WebApplication.Reports.ClientConversion
{
    public class ClientConversionProfile : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<Models.DatabaseFirst.tblJob, ClientConversionModel.Client>()
            //    .ForMember(m => m.QuotedValue, opt => opt.MapFrom(
            //        c => c.tblLines.Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
            //    .ForMember(m => m.JobValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 4).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
            //    .ForMember(m => m.CompletedValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
            //    .ForMember(m => m.DeadValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 1).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))));
        }
    }
}