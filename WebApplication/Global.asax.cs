using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using NLog;
using NLog.Config;
using StructureMap;
using WebApplication.Controllers.ViewModels;
using WebApplication.Reports;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigurationItemFactory.Default.Targets.RegisterDefinition("SignalR", typeof(Infrastructure.Nlog.Targets.SignalRTarget));
            //Logger logger = LogManager.GetCurrentClassLogger();
            //logger.Info("Starting App");
            //logger.Error("Generating Error");
            ViewModelProfile.Configure();
            ReportProfile.Configure();

            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
