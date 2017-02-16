using System.Linq;
using AutoMapper;
using WebApplication.Controllers.ViewModels.Enquiries;
using WebApplication.Controllers.ViewModels.Operative;

namespace WebApplication.Controllers.ViewModels.Job
{
    public class JobProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblJob, ListModel>();

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, EnquiriesModel>();

            Mapper.CreateMap<Models.DatabaseFirst.tblJobFile, ViewModel.File>();
            Mapper.CreateMap<Models.DatabaseFirst.tblFile, ViewModel.Lines.File>();

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, ViewModel>()
                .ForMember(x=>x.tblLines, opt => opt.MapFrom(c=>c.tblLines.OrderBy(o=>o.JobLineID)));

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, EventStoreModel>();
            Mapper.CreateMap<Models.DatabaseFirst.tblEventStore, EventStoreModel.EventStore>();

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, QuotesModel>()
                .ForMember(m => m.Value, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))));

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, JobsModel>()
                .ForMember(m => m.QuotedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))))
                .ForMember(m => m.JobValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 4).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))))
                .ForMember(m => m.CompletedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x=>x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))));

            Mapper.CreateMap<Models.DatabaseFirst.tblJob, ActiveJobsModel.Job>()
                .ForMember(m => m.QuotedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 2 || x.Status == 4 || x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))))
                .ForMember(m => m.JobValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 4).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))))
                .ForMember(m => m.CompletedValue, opt => opt.MapFrom(
                    c => c.tblLines.Where(x => x.Status == 8).Sum(x => ((double?)(x.Quantity ?? 0) * (double?)x.CalculatedUnitPrice))));


            Mapper.CreateMap<Models.DatabaseFirst.tblLine, ViewModel.Lines>()
                .ForMember(x=>x.tblFileFileName, opt => opt.MapFrom(c =>c.tblFiles.FirstOrDefault().FileName))
                .ForMember(x=>x.tblFileContentType, opt => opt.MapFrom(c =>c.tblFiles.FirstOrDefault().ContentType));
            Mapper.CreateMap<Models.DatabaseFirst.tblPurchaseOrder, ViewModel.Lines.BillOfMaterials>();
            Mapper.CreateMap<Models.DatabaseFirst.tblTimesheet, ViewModel.Lines.TimeSheets>();
        }
    }
}
