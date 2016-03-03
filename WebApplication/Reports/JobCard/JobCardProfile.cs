using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Reports.JobCard
{
    public class JobCardProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, JobCardModel.JobCard>()
                .ForMember(x => x.ClientName, opt => opt.MapFrom(m => m.tblJob.tblClient.ClientCompanyName));
        }
    }
}