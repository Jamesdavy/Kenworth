using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebApplication.Reports.DeliveryNote
{
    public class DeliveryNoteProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblJob, DeliveryNoteModel>();
            Mapper.CreateMap<Models.DatabaseFirst.tblLine, DeliveryNoteModel.Line>();

            //Mapper.CreateMap<Models.DatabaseFirst.tblJob, ViewModel>();
            //Mapper.CreateMap<Models.DatabaseFirst.tblJob, QuotesModel>()
            //    .ForMember(m => m.Value, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))));

            //Mapper.CreateMap<Models.DatabaseFirst.tblJob, JobsModel>()
            //    .ForMember(m => m.QuotedValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
            //    .ForMember(m => m.JobValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 4).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))))
            //    .ForMember(m => m.CompletedValue, opt => opt.MapFrom(
            //        c => c.tblLines.Where(x => x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.UnitPrice))));

            //Mapper.CreateMap<Models.DatabaseFirst.tblLine, ViewModel.Lines>();
            //Mapper.CreateMap<Models.DatabaseFirst.tblPurchaseOrder, ViewModel.Lines.BillOfMaterials>();
            //Mapper.CreateMap<Models.DatabaseFirst.tblTimesheet, ViewModel.Lines.TimeSheets>();
        }
    }

}