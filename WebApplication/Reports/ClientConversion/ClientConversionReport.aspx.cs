using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.Reports.ClientConversion
{
    public partial class ClientConversionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateFromTextBox.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateToTextBox.Text = DateTime.Now.AddDays(1).ToShortDateString();
                quoteThreshold.Text = "0";
            }

            var reportViewer = Form.FindControl("documentViewer") as DevExpress.XtraReports.Web.ReportViewer;
            if (reportViewer != null)
                reportViewer.Report = new ClientConversion(double.Parse(quoteThreshold.Text), DateTime.Parse(dateFromTextBox.Text), DateTime.Parse(dateToTextBox.Text));
        }
    }
}