<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContConv.aspx.cs" Inherits="Site.ContConv" %>

<asp:Content ContentPlaceHolderID="CpLateral" runat="server">
    <form id="frmConveniados" runat="server">
        <script src="Js/Apoio.js"></script>
        <div id="menulateral" class="menuflutua">
            <ul>
            <li><asp:LinkButton ID="lbPublic" runat="server" Text="Publicidade" OnClick="ativarPublicidade"></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbAntecipa" runat="server" Text="Antecipação de Recebíveis" OnClick="ativarAntecipacao"></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbFunciona" runat="server" Text="Como Funciona" OnClick="ativarComoFunciona"></asp:LinkButton></li>                                                         
            <li id="treeline-icon" class="treeline-icon" onclick="openNav()" >&#9776;</li>
            <li id="treeline-closeicon" class="treeline-closeicon" onclick="closeNav()">&cross;</li>
        </ul>
        </div>
    </form>   
</asp:Content>
<asp:Content ContentPlaceHolderID="cpContent" runat="server">
    <asp:MultiView ID="mwConvenios" runat="server">
        <asp:View ID="vwPublic" runat="server">            
            <%# montaParaConvenios(40) %>            
        </asp:View>
        <asp:View ID="vwAntecipa" runat="server">            
            <%# montaParaConvenios(41) %>
        </asp:View>
        <asp:View ID="vwFunciona" runat="server">            
            <%# montaParaConvenios(42) %>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>    
</asp:Content>