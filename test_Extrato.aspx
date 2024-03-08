<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test_Extrato.aspx.cs" Inherits="Site.test_Extrato" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Teste Extrato</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddlMes" runat="server">
                <asp:ListItem Text="Janeiro" Value="1"></asp:ListItem>
                <asp:ListItem Text="Fevereiro" Value="2"></asp:ListItem>
                <asp:ListItem Text="Março" Value="3"></asp:ListItem>
                <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                <asp:ListItem Text="Maio" Value="5"></asp:ListItem>
                <asp:ListItem Text="Junho" Value="6"></asp:ListItem>
                <asp:ListItem Text="Julho" Value="7"></asp:ListItem>
                <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                <asp:ListItem Text="Setembro" Value="9"></asp:ListItem>
                <asp:ListItem Text="Outubro" Value="10"></asp:ListItem>
                <asp:ListItem Text="Novembro" Value="11"></asp:ListItem>
                <asp:ListItem Text="Dezembro" Value="12"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlAno" runat="server">
                <asp:ListItem Value="2017" Text="2017" ></asp:ListItem>
                <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnPeriodo" runat="server" Text="Aplicar" OnClick="escolherPeriodo" />
            <asp:Button ID="btnMetodoPeriodo" runat="server" Text="Através de Método" OnClick="metodoPeriodo" />
            <div style="margin: 0 auto; width: 500px; height:300px; border: 2px solid #f26907">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div style="width: 100%; height: 300px; border: 1px solid #22396f">
            <asp:Button ID="btnEvoluirPeriodo" runat="server" Text="Evolução do período" OnClick="evoluirPerioro" />
            <asp:Label ID="lblMsgPeriodo" runat="server"></asp:Label>
        </div>

        <div>
            <p>Saldo</p>
            <input id="iIdAssoc" type="text" runat="server" placeholder="Id Associado" value="2747" />
            <asp:Button ID="btnSaldo" runat="server" Text="Saldo" OnClick="retornaSaldo"/>
            <asp:Label ID="lblSaldo" runat="server"></asp:Label>
        </div>
        <div>
            <p>Enviar E-mail</p>
            <input id="iPara" runat="server" type="text" value="reginaldo@asu.com.br" />
            <input id="iCopia" runat="server" type="text" value="regis@asu.com.br" />
            <input id="iAssunto" runat="server" type="text" value="Assunto" />
            <input id="iDados" runat="server" type="text" value="Dados" />
            <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar E-mail" OnClick="enviarEmail"/>            
        </div>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>
