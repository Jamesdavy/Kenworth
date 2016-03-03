using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Controllers.ViewModels.Accounts
{
    public class AccountProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.AspNetUser, ListModel>()
                .ForMember(x=>x.LockedOut, opt => opt.Ignore());
            Mapper.CreateMap<Models.DatabaseFirst.AspNetUser, EditModel>();
        }
    }
}