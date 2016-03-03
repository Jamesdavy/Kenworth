using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Controllers.ViewModels.DeliveryNote
{
    public class DeliveryNoteProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, ListModel.DeliveryNote>();
            
        }
    }
}