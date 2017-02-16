using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Infrastructure.Logging;

namespace WebApplication.Models.DatabaseFirst
{
    


    public partial class ApplicationEntities : DbContext 
    {

        public class DBConfiguration : DbConfiguration
        {
            public DBConfiguration()
            {
                AddInterceptor(new QueryInterceptorLogging());
            }
        }

        public override int SaveChanges()
        {
             
            foreach (var orphan in tblLines.Local.Where(a => a.tblJob == null).ToList())
            {
                tblLines.Remove(orphan);
            }

            foreach (var orphan in tblPurchaseOrders.Local.Where(a => a.tblLine == null).ToList())
            {
                tblPurchaseOrders.Remove(orphan);
            }

            foreach (var orphan in tblTimesheets.Local.Where(a => a.tblLine == null).ToList())
            {
                tblTimesheets.Remove(orphan);
            }

            foreach (var orphan in tblFiles.Local.ToList())
            {
                if (orphan.FileID == Guid.Empty)
                    tblFiles.Remove(orphan);
            }

            foreach (var orphan in tblJobFiles.Local.ToList())
            {
                if (orphan.tblJob == null)
                    tblJobFiles.Remove(orphan);
            }

            return base.SaveChanges();
        }
    }
}