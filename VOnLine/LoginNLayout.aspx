<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNLayout.aspx.cs" Inherits="Site.VOnLine.LoginNLayout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LoGin Novo Layout</title>
    <link rel="stylesheet" href="../Css/Form-Clean.css" />
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <script src="../Js/Apoio.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="BoxLogin">
            <h1>Você OnLine</h1>
            <input id="iCpf" runat="server" type="text" placeholder="Nº Cartão / ID. convênio" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
            <script>iSenha.focus();</script>
            <input id="iSenha" runat="server" type="password" placeholder="Senha" />
            <asp:Button ID="btnLogin" runat="server" Text="Acessar" OnClick="LogarVoceOnLine" />
        </div>
        <asp:Label ID="lblResult" runat="server"></asp:Label>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
