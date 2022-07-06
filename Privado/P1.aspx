<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="P1.aspx.cs" Inherits="Site.Privado.P1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="iIdAssoc" runat="server" type="text" />
            <asp:Button ID="btnQS" runat="server" Text="Executar" OnClick="executarQS" />
        </div>
    </form>
</body>
</html>
