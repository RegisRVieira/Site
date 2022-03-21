<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testesGravarHtml.aspx.cs" Inherits="Site.Privado.testesGravarHtml" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="tbTexto" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnRecupera" runat="server" Text="Recuperar Dados do Banco" OnClick="recuperaTagsHTML" /><br />
            <asp:Label ID="lbResult" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
