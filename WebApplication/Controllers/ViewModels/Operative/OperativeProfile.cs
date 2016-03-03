using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication.Controllers.ViewModels.Client;

namespace WebApplication.Controllers.ViewModels.Operative
{
    public class OperativeProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblUser, ListModel>();
            Mapper.CreateMap<Models.DatabaseFirst.tblUser, EditModel>();
        }
    }
}