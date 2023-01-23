<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaSaldo.aspx.cs" Inherits="Site.ConsultaSaldo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Consulta Saldo</title>
    <link rel="stylesheet" href="Css/Botoes.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/StyleEventos.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;" >
            <input id="iIdAssoc" class="btnBusca1" type="text" runat="server" placeholder="Id do Associado" />
            <div style="text-align: center;">
            <asp:LinkButton ID="lbtnConsultar" CssClass="Botao1" runat="server" Text="Consultar Saldo - Novo Modelo de Crédito" OnClick="consultarSaldo"></asp:LinkButton>
            </div>
        </div>        
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;" >
            <p>Tratar Datas para Emissão do Saldo</p>
            <div style="width: 50%">
                <asp:LinkButton ID="lbtnDatas" runat="server" CssClass="Botao2" Text="Datas" OnClick="executarDatas"></asp:LinkButton>
            </div>
        </div>
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;">
            <p>Saldo com Data Início e Data Fim</p>
            <div  style="width: 50%">
                <asp:LinkButton ID="lbtnConsultaSaldo" CssClass="Botao1" runat="server" Text="Cosultar Saldo" OnClick="executarSaldo"></asp:LinkButton>
            </div>
        </div>
        <div style="width: 700px; margin: 0 auto;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
