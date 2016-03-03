using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Controllers.ViewModels.BillOfMaterials
{
    public class BillOfMaterialsProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblPurchaseOrder, EditModel>();
        }
    }
}