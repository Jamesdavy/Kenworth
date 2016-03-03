using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Controllers.ViewModels.Timesheet
{
    public class TimeSheetProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblTimesheet, EditModel>();
        }
    }
}