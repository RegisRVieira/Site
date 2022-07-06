<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listas.aspx.cs" Inherits="Site.Privado.Listas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Listas</title>
    <link rel="stylesheet" href="../Css/pStyle.css" />
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="secMenu">
            <div class="pDivLogin">
                <p style="color: #fbb888;">
                    <asp:Label ID="lblLogadox" runat="server"></asp:Label>
                    Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                </p>
            </div>
            <asp:MultiView ID="mwConteudo" runat="server">
                <asp:View ID="vwBotoes" runat="server">
                    <section class="lbtForm">
                        <div style="width: 310px; margin: 0 auto;">
                            <p>Como deseja Procurar?</p>
                            <asp:LinkButton ID="btnAtivaLista" runat="server" Text="Listas" OnClick="ativarLista"></asp:LinkButton>
                            <asp:LinkButton ID="btnAtivarProcura" runat="server" Text="Campo de Busca" OnClick="ativarProcura"></asp:LinkButton>
                        </div>
                    </section>
                </asp:View>
                <asp:View ID="vwLista" runat="server">
                    <asp:Label ID="lblListas" runat="server"></asp:Label>
                    <select id="sTipo" runat="server" class="sSelect"></select>
                    <asp:Button ID="btnGeraLista" runat="server" Text="Gerar lista" OnClick="gerarLista" />
                    <section>
                        <asp:Label ID="lblResp" runat="server"></asp:Label>
                    </section>
                </asp:View>
                <asp:View ID="vwProcurar" runat="server">
                    <input id="iProcura" runat="server" type="search" placeholder="Procurar" />
                    <asp:Button ID="btnProcurar" runat="server" Text="Busca" OnClick="procurarItens" />
                </asp:View>
            </asp:MultiView>
            <br /><br /><br /><br /><br />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div style="margin-left: 10px;">
                <a href="Default.aspx">Voltar</a>
            </div>

        </section>
    </form>
</body>
</html>
