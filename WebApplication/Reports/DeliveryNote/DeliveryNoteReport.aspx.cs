using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using Page = DevExpress.XtraPrinting.Page;

namespace WebApplication.Reports.DeliveryNote
{
    public partial class DeliveryNoteReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //long id = long.Parse(Request.QueryString["id"]);

            var reportViewer = Form.FindControl("documentViewer") as DevExpress.XtraReports.Web.ReportViewer;
            if (reportViewer != null)
            {
               
            }
        }
    }
}