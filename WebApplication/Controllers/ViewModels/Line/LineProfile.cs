using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace WebApplication.Controllers.ViewModels.Line
{
    public class LineProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, EditViewModel>();
                //.ForMember(x => x.tblFileFileName, opt => opt.MapFrom(c => c.tblFiles.FirstOrDefault().FileName))
                //.ForMember(x => x.tblFileContentType, opt => opt.MapFrom(c => c.tblFiles.FirstOrDefault().ContentType));
            
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, IndexModel>()
                .ForMember(m => m.Value, opt => opt.MapFrom(
                    x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice)))
                .ForMember(m => m.tblJobtblClientClientCompanyName, opt => opt.MapFrom(
                    x => x.tblJob.tblClient.ClientCompanyName));
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, JobsModel>()
                .ForMember(m => m.Value, opt => opt.MapFrom(
                    x => ((double?) (x.Quantity ?? 0)*(double?) x.CalculatedUnitPrice)))
                .ForMember(m => m.tblJobtblClientClientCompanyName, opt => opt.MapFrom(
                    x => x.tblJob.tblClient.ClientCompanyName));
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, QuotesModel>()
                .ForMember(m => m.Value, opt => opt.MapFrom(
                    x => ((double?) (x.Quantity ?? 0)*(double?) x.CalculatedUnitPrice)))
                .ForMember(m => m.tblJobtblClientClientCompanyName, opt => opt.MapFrom(
                    x => x.tblJob.tblClient.ClientCompanyName));

            Mapper.CreateMap<Models.DatabaseFirst.tblPurchaseOrder, EditViewModel.BillOfMaterials>();
            Mapper.CreateMap<Models.DatabaseFirst.tblTimesheet, EditViewModel.TimeSheets>();
            Mapper.CreateMap<Models.DatabaseFirst.tblFile, EditViewModel.File>();
        }
    }
}