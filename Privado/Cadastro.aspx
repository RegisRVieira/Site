<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Site.Privado.Cadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cadastro</title>
    <link rel="stylesheet" href="../Css/pStyle.css" />
    <style>
        .secForm33 {
            margin: 0 auto;
            margin-top: 30px;
            border: 1px solid #808080;
            width: 50%;
            min-height: 500px;
            background-color: #ff006e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="secMenu">
            <div class="pDivLogin">
                <p style="color: #fbb888;">                    
                    Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                </p>
            </div>
            <section class="secForm">
                <p>Conteúdo</p>
                <select id="sTipo" runat="server" class="sSelect"></select>
                <select id="sUsuario" runat="server" class="sSelect"></select>
                <input id="iLink" runat="server" type="text" placeholder="Link" />
                <input id="iIndicacao" runat="server" type="text" placeholder="Indicação" />
                <input id="iDescricao" runat="server" type="text" placeholder="Descrição" />
                <div class="alinhaBotao">
                    <asp:Button ID="btnGravar" runat="server" Text="Gravar" OnClick="gravarDados" />
                </div>
            </section>
            <asp:Label ID="lblRet" runat="server"></asp:Label>
            <div style="margin-left: 10px;">
                <a href="Default.aspx">Voltar</a>
            </div>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </form>
</body>
</html>
