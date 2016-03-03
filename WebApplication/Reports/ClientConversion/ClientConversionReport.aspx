<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientConversionReport.aspx.cs" Inherits="WebApplication.Reports.ClientConversion.ClientConversionReport" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%:Styles.Render("~/Content/css") %>
    <%:Styles.Render("~/Content/themes/css") %>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-md-2">Date From</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="dateFromTextBox" CssClass="DatePicker" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2">Date To</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="dateToTextBox" CssClass="DatePicker" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2">Quote Threshold</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="quoteThreshold" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <asp:Button ID="CloseReportLink" CssClass="closeReport" OnClientClick="window.close(); return false;" ClientIDMode="Static" runat="server" Text="Close Report"></asp:Button>        
                <asp:Button ID="Run" runat="server" Text="Run Report" />
            </div>
        </div>
        <dx:ReportToolbar ID="ReportToolbar1" runat="server" ClientInstanceName="reportToolbar" ReportViewer="<%# documentViewer %>" />
        <dx:ReportViewer ID="documentViewer" ClientInstanceName="reportViewer" runat="server" ReportName="XtraReport1" />
    </form>
    <%:Scripts.Render("~/bundles/jquery")%>
    <script src="/Scripts/jquery-ui-1.11.4.min.js"></script>
    <script>
        $(document).ready(function () {
            $.datepicker.setDefaults($.extend({ showMonthAfterYear: false }));
            $(".DatePicker").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "dd/mm/yy"
                }
            );

            //var cookie = $.cookie("EVHCReportDateState");
            //if (typeof cookie != 'undefined') {
            //    var state = JSON.parse(cookie);
            //    if (state) {
            //        $("#dateFromTextBox").val(state.startDate);
            //        $("#dateToTextBox").val(state.endDate);
            //    }
            //}
        });
    </script>
</body>
</html>

