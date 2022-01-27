<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eMenu.aspx.cs" Inherits="Site.Eventos.eMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Menu Eventos ASU</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
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
            .eBotoes {
                margin-left: 5px;
                min-width: 48%;
                height: 20px;
                background-color: #ff0000;
                background-color: #65368a;
                border: 1px solid #f26907;
                border-radius: 6px;
                display: inline-block;
                float: left;
                font-size: 18px;
                font-family: 'Verdana';
                font-family: 'Courier New';
                padding-top: 20px;
                padding-bottom: 20px;
                margin-top: 2px;
                margin-bottom: 8px;
            }

                .eBotoes:hover {
                    background-color: #6f87c2;
                    color: #65368a;
                }

                .eBotoes p {
                    margin: 0;
                    padding: 0;
                    padding-top: 20px;
                    padding-bottom: 20px;
                    text-align: center;
                    color: white;
                    font-size: 18px;
                    font-family: 'Verdana';
                    font-family: 'Courier New';
                    text-decoration: none;
                }

            .ePainelEventos {
                margin: 0 auto;
                margin-top: 50px;
                width: 98%;
                height: 50px;
            }

                .ePainelEventos p {
                    color: #f26907;
                    text-align: center;
                    font-size: 2.5em;
                }

            .BoxBotoes {
                width: 50%;
                min-height: 10px;
                margin: 0 auto;
                margin-top: 5px;
            }

            .eDivLogin {
                width: 100%;
                height: 30px;
                text-align: right;
                color: #f26907;
                padding-top: 7px;
            }

            .corLogin {
                color: #f26907;
            }
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
