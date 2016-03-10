using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DevExpress.Xpo.DB;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.Ajax.Utilities;
using WebApplication.Models.DatabaseFirst;

namespace WebApplication.Reports.JobCard
{
    public partial class JobCard : DevExpress.XtraReports.UI.XtraReport
    {

        public JobCard()
        {
            InitializeComponent();
        }

        public JobCard(long id)
            : this()
        {
            var db = StructureMap.ObjectFactory.GetInstance<ApplicationEntities>();
            var jobCards = db.tblLines.Where(x => x.JobID == id).ProjectTo<JobCardModel.JobCard>().ToList();
            var model = new JobCardModel()
            {
                JobCards = jobCards
            };

            bindingSource1.DataSource = model;
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int cellsInRow = 3;
            int rowsCount = 20;
            float rowHeight = 35f;

            XRTable table = new XRTable();
            xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.All;
            xrTable1.BeginInit();

            for (int i = 0; i < rowsCount; i++)
            {
                XRTableRow row = new XRTableRow();
                row.HeightF = rowHeight;
                for (int j = 0; j < cellsInRow; j++)
                {
                    XRTableCell cell = new XRTableCell();
                    //cell.Text = "text";
                    switch (j)
                    {
                        case 0:
                            {
                                cell.WidthF =  (float)151.04;
                                break;
                            }
                        case 1:
                            {
                                cell.WidthF = (float)401.04;
                                break;
                            }
                        case 2:
                            {
                                cell.WidthF = (float)184.92;
                                break;
                            }
                    };

                    row.Cells.Add(cell);
                }
                xrTable1.Rows.Add(row);
            }

            //table.BeforePrint += new PrintEventHandler(table_BeforePrint);
            xrTable1.AdjustSize();
            xrTable1.EndInit();
            //return table;
            
            
            //for (var r = 1; r <= 20; r++)
            //{
            //    // Create new row..
            //    var row = new XRTableRow();
            //    row.Height = 100;
            //    for (var c = 1; c <= 3; c++)
            //    {
            //        // Create a new cell and add it to the row.
            //        var cell = new XRTableCell {Borders = BorderSide.All, Text = "text"};
            //        cell.CanGrow = false;
            //        cell.Height = 100;
            //        switch (c)
            //        {
            //            case 1:
            //            {
            //                cell.Width = 141;
            //                break;
            //            }
            //            case 2:
            //            {
            //                cell.Width = 401;
            //                break;
            //            }
            //            case 3:
            //            {
            //                cell.Width = 185;
            //                break;
            //            }
            //        };

            //        row.Cells.Add(cell);
            //    }

            //    // ..and add it to the table.
            //    xrTable1.Rows.Add(row);
            //}
        }

        //private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    xrPictureBox1.ImageUrl = "/Content/images/kenworthlogo.png";
        //}
    }
}
