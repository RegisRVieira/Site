<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iText7.aspx.cs" Inherits="Site.iText7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPDF" runat="server" Text="Gerar PDF" OnClick="gerarPDF" /><br /><br />
            <asp:Button ID="btnPDFExtrato" runat="server" Text="Gerar Pdf do Extrato" OnClick="gerarPdfExtrato" />
        </div>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
