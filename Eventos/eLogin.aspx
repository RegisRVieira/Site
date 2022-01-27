<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eLogin.aspx.cs" Inherits="Site.Eventos.eLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login Eventos ASU</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/pStyle.css" />
    <link rel="sortcut icon" type="image/png" href="../Img/Logo-nav.png" />
    <script type="text/javascript" src="../Js/Apoio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navHome-Internas">
            <p>
                <a href="Home.aspx">
                    <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </p>
        </nav>
        <section class="pLogin" style="border: 1px solid #f26907; margin-top: 20px; border-radius: 6px;">
            <p>Login</p>
            <input id="iUsuario" type="text" runat="server" placeholder="Usuário" />
            <input id="iSenha" type="password" runat="server" placeholder="Senha" required="required" />
            <asp:Button ID="btnLogin" runat="server" Text="Acessar" OnClick="logarEventos" />
            <div style="margin-top: 30px; color: #f26907; margin-left: 10px;">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </section>
        <footer class="footerHome">
            <div class="footerHome-Dados">
                <small>&reg; 1969 -
                <script type="text/javascript">document.write(agora.getFullYear() + ". Todos os direitos reservados")</script>
                </small>
                <address>
                    <script type="text/javascript">document.write(ano + " Anos")</script>
                </address>
            </div>
            <div class="footerHome-Img">
                <img src="../Img/Icon/Logo2 Régis-ASU.png" />
            </div>
        </footer>
    </form>
</body>
</html>
