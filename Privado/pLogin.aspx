<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pLogin.aspx.cs" Inherits="Site.Privado.pLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Área Privada</title>
    <link rel="stylesheet" href="../Css/pStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="pLogin">
            <p>Login</p>
            <input id="iUsuario" runat="server" type="text" placeholder="Usuário" />
            <input id="iSenha" runat="server" type="password" placeholder="Senha" />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="logarPrivado" />
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>        
    </form>
</body>
</html>
