using AutoMapper;

namespace WebApplication.Controllers.ViewModels.Contact
{
    public class ContactProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Models.DatabaseFirst.tblContact, EditModel>();
            Mapper.CreateMap<Models.DatabaseFirst.tblContact, ListModel.Contact>();
            Mapper.CreateMap<Models.DatabaseFirst.tblContact, IndexModel>();
        }
    }
}