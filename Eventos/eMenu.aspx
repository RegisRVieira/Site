<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eMenu.aspx.cs" Inherits="Site.Eventos.eMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Menu Eventos ASU</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/StyleEventos.css" />
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
        <style>
          
        </style>
        <section style="margin: 0 auto; width: 50%; min-height: 400px;">
            <div class="ePainelEventos">
                <p>Bem vindo ao Painel de Eventos da ASU </p>
            </div>

            <div class="BoxBotoes">
                <div style="margin-top: 20px; margin-bottom: 10px; background-color: #22396f; padding-top: 5px; padding-bottom: 5px; border-radius: 6px;">
                    <p style="margin-left: 5px; color: #6f87c2;">Menu de Opções</p>
                </div>               
                <div style="width: 100%; min-height: 150px;">
                    <asp:LinkButton ID="lbtAdmin" CssClass="eBotoes" runat="server" Text="Configurações" OnClick="abrirConfig"></asp:LinkButton>
                    <asp:LinkButton ID="lbtFestas" CssClass="eBotoes" runat="server" Text="Festas" OnClick="abrirFestas"></asp:LinkButton>
                    <asp:LinkButton ID="lbtBrindes" CssClass="eBotoes" runat="server" Text="Brindes" OnClick="abrirBrindes"></asp:LinkButton>
                </div>
                <div class="eDivLogin">
                    <p style="color: #fbb888;">
                        Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                    </p>
                </div>
            </div>
            <div style="margin: 0 auto; width: 50%; color: #22396f;" >
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
