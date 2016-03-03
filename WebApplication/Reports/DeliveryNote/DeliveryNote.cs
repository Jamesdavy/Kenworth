using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraReports.UI;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.DeliveryNote
{
    public partial class DeliveryNote : DevExpress.XtraReports.UI.XtraReport
    {
        public DeliveryNote()
        {
            InitializeComponent();
        }

        private string _watermark;

        public DeliveryNote(long clientId, long deliveryNoteId, long[] ids, string watermark = "")
            : this()
        {
            _watermark = watermark;

            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var client = db.tblClients.Where(x => x.ClientID == clientId).Select(x=>x.ClientCompanyName).SingleOrDefault();
            var lines = db.tblLines.Where(x => ids.Contains(x.LineID)).ProjectTo<DeliveryNoteModel.Line>().ToList();

            var model = new DeliveryNoteModel()
            {
                DeliveryNoteID = deliveryNoteId,
                DeliveryNoteDate = DateTime.Now,
                tblClientsClientCompanyName = client,
                tblLines = lines
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox2.ImageUrl = "/Content/images/kenworthlogo.png";
        }

        private void DeliveryNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Watermark.Text = _watermark;
        }

    }
}
