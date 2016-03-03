<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteReport.aspx.cs" Inherits="WebApplication.Reports.Quote.QuoteReport" %>
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
                <asp:Button ID="CloseReportLink" CssClass="closeReport" OnClientClick="window.close(); return false;" ClientIDMode="Static" runat="server" Text="Close Report"></asp:Button>        
            </div>
        </div>
        <dx:ReportToolbar ID="ReportToolbar1" runat="server" ClientInstanceName="reportToolbar" ReportViewer="<%# documentViewer %>" />
        <dx:ReportViewer ID="documentViewer" ClientInstanceName="reportViewer" runat="server" ReportName="XtraReport1" />
    </form>
</body>
</html>
