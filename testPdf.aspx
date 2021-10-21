<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testPdf.aspx.cs" Inherits="Site.testPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPdf" runat="server" Text=" Gerar PDF" OnClick="converterHtmlPdf" /><br />
            <asp:Button ID="btnCepPdf" runat="server" Text="Gerar PDF ceTe" OnClick="criarPdfCete" />
            <a href="Downloads\Arquivo.pdf" target="_blank">Arquivo PDF</a>               
            <asp:Label ID="lbArquivos" runat="server"></asp:Label>            
        </div>
        <div>
            <a href="iText7.aspx">PDF - iText7</a>
        </div>
    </form>
</body>
</html>
