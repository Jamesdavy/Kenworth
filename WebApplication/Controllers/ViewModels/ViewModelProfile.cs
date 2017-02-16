using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication.Controllers.ViewModels.Accounts;
using WebApplication.Controllers.ViewModels.BillOfMaterials;
using WebApplication.Controllers.ViewModels.Client;
using WebApplication.Controllers.ViewModels.Contact;
using WebApplication.Controllers.ViewModels.DeliveryNote;
using WebApplication.Controllers.ViewModels.Enquiries;
using WebApplication.Controllers.ViewModels.Job;
using WebApplication.Controllers.ViewModels.Line;
using WebApplication.Controllers.ViewModels.Operative;
using WebApplication.Controllers.ViewModels.Timesheet;


namespace WebApplication.Controllers.ViewModels
{
    public class ViewModelProfile
    {
        public static void Configure()
        {
            Mapper.AddProfile(new ClientProfile());
            Mapper.AddProfile(new OperativeProfile());
            Mapper.AddProfile(new EnquiriesProfile());
            Mapper.AddProfile(new JobProfile());
            Mapper.AddProfile(new LineProfile());
            Mapper.AddProfile(new BillOfMaterialsProfile());
            Mapper.AddProfile(new TimeSheetProfile());
            Mapper.AddProfile(new AccountProfile());
            Mapper.AddProfile(new ContactProfile());
            Mapper.AddProfile(new DeliveryNoteProfile());
        }
    }
}