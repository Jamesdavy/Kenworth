using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.Reports.JobCard
{
    public partial class JobCardReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long id = long.Parse(Request.QueryString["id"]);

            var reportViewer = Form.FindControl("documentViewer") as DevExpress.XtraReports.Web.ReportViewer;
            if (reportViewer != null)
                reportViewer.Report = new JobCard(id);
        }
    }
}