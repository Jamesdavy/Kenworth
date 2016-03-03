using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Controllers.ViewModels.Client
{
    public class ClientProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblClient, ListModel>();
            Mapper.CreateMap<Models.DatabaseFirst.tblClient, EditModel>();
        }
    }
}