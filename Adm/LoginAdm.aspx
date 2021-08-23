<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAdm.aspx.cs" Inherits="Site.Adm.LoginAdm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Adm</title>
    <link rel="stylesheet" href="../Css/Form-Clean.css" />
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
        <div class="formGuia">
            <h1>Administrador do Site</h1>
            <input id="iNome" type="text" runat="server"  placeholder="Nome" required="" autofocus="" />
            <input id="iSenha" type="password" runat="server" placeholder="Senha" required=""/>           
            <asp:Button ID="btnLogar" runat="server" Text="Login" OnClick="acessarAdmin"/>
            <asp:Label ID="lblUsuário" runat="server"></asp:Label>
        </div>
    </form>
    <footer class="rodape">
            <small>&reg; 1969 -
                <script type="text/javascript">document.write(agora.getFullYear() + ".  Todos os direitos reservados")</script>
            </small>
            <address>
                <script type="text/javascript">document.write(ano + " Anos")</script>
            </address>
        </footer>
</body>
</html>
