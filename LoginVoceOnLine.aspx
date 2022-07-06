<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginVoceOnLine.aspx.cs" Inherits="Site.LoginVoceOnLine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Você OnLine</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js" type="text/javascript"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p>
            <a href="Home.aspx">
                <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
        </p>
    </nav>
    <main>
        <form id="form1" runat="server">
            <div class="BoxLogin">
                <h1>Você OnLine</h1>
                <input id="iCpf" runat="server" type="text" placeholder="CPF ou CNPJ" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                <script>iSenha.focus();</script>
                <input id="iSenha" runat="server" type="password" placeholder="Senha" />
                <asp:Button ID="btnLogin" runat="server" Text="Acessar" OnClick="LogarVoceOnLine" />                
            </div>
            <div style="width: 600px; margin: 0 auto;">
                <asp:Label ID="lblResult" runat="server" CssClass="lblMsg"></asp:Label>
            </div>            
            
        </form>
    </main>
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
