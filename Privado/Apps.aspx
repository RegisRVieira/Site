<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="Site.Privado.Apps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body onload="detectarResolucao()">
    <form id="form1" runat="server">
        <div>
            <p>Criar Listagem de convênios a partir de um SELECT HTML</p>            
            <select id="stGuia" runat="server" onchange="mudar()"></select>
            <asp:DropDownList ID="ddlGuia" runat="server" OnSelectedIndexChanged="guiaPorSelect" AutoPostBack="true"></asp:DropDownList>
            <asp:LinkButton ID="lbtnListaConvenios" runat="server" Text="Exibir" OnClick="guiaPorSelect"></asp:LinkButton>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div>
            <p>Consulta de Saldo</p>
            <input id="iAssociado" runat="server" type="text" placeholder="Digite a ID do Associado" value="2747" />
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar Saldo" OnClick="SaldoAssociado" /><br /><br />
            <asp:Label ID="lblSaldo" runat="server"></asp:Label>
        </div>
        <div>
            <p>Request.Form[]</p> 
            <input id="hdExtrato" runat="server" type="hidden" />
            <asp:Label ID="lblRetorno" runat="server"><%# MontarExtrato() %></asp:Label>
        </div>
    </form>
</body>
</html>
