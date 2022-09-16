<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdm.aspx.cs" Inherits="Site.Adm.LoginAdm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Adm</title>
    <link rel="stylesheet" href="../Css/Form-Clean.css" />
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <script src="../Js/Apoio.js"></script>
    <script>
        function acessarAdmin() {
            window.location.href = "SiteManutencao.aspx";
            return false;
        }
        function focarNome() {
            document.getElementById('iNome').focus()
        }
    </script>
</head>
<body>
    <nav class="naveg">
        <div>
            <h1>Sistema de Manutenção <span class="marca">&reg;</span></h1>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div class="BoxLogin">
            <h1>Administrador do Site</h1>
            <input id="iNome" type="text" runat="server"  placeholder="Nome" required="" autofocus="" />
            <input id="iSenha" type="password" runat="server" placeholder="Senha" required=""/>           
            <asp:Button ID="btnLogar" runat="server" Text="Login" OnClick="acessarAdmin"/>
            <asp:Label ID="lblUsuário" runat="server"></asp:Label>
        </div>
        <div style="margin: 0 auto; width: 500px; min-height: 30px; ">
            <p style="margin: 0; padding: 0; margin-top: -22px; text-align: right; width: 100%; min-height: 30px;">
                <a href="../Home.aspx" style="text-decoration: none; color: #22396f;" target="_blank">Ir para o Site</a>
            </p>
        </div>
    </form>
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
</body>
</html>
