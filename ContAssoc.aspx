<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContAssoc.aspx.cs" Inherits="Site.ContAssoc" %>

<asp:Content ID="cAssociado" runat="server" ContentPlaceHolderID="CpLateral">
<form id="frmAssociados" runat="server">
    <script src="Js/Apoio.js"></script>
        <div id="menulateral" class="menuflutua">
            <ul>
                <li><asp:LinkButton ID="lbCartao" runat="server" Text="Cartão ASu On Line" OnClick="ativarCartao"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbMedico" runat="server" Text="Convênio Médico" OnClick="ativarMedico"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbOdonto" runat="server" Text="Convênio Odontológico" OnClick="ativarOdonto"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbJuridico" runat="server" Text="Assessoria Jurídica" OnClick="ativarJuridico"></asp:LinkButton></li>
                <li><a href="#">Promoções</a></li>
                <li><asp:LinkButton ID="lbValeCompras" runat="server" Text="Vale Compras" OnClick="ativarValeCompras"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbTanqueCheio" runat="server" Text="Tanque Cheio" OnClick="ativarTanqueCheio"></asp:LinkButton></li>
                <li id="treeline-icon" class="treeline-icon" onclick="openNav()">&#9776;</li>
                <li id="treeline-closeicon" class="treeline-closeicon" onclick="closeNav()">&cross;</li>
            </ul>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="cpContent" runat="server">
    <asp:MultiView ID="mwAssociados" runat="server">
        <asp:View ID="vwCartao" runat="server">
            <!-- Cartão ASU On Line -->
            <%# montarParaAssociados(65) %>
        </asp:View>
        <asp:View ID="vwMedico" runat="server">
            <!--Convênio Médico -->
            <%# montarParaAssociados(66) %>
        </asp:View>
        <asp:View ID="vwOdonto" runat="server">
            <!-- Convênio Odontológico -->
            <%# montarParaAssociados(67) %>
        </asp:View>
        <asp:View ID="vwJuridico" runat="server">
            <!-- Assessoria Jurídica -->
            <%# montarParaAssociados(68) %>
        </asp:View>
        <asp:View ID="vwValeCompras" runat="server">
            <!-- Vale Compras -->
            <%# montarParaAssociados(69) %>
        </asp:View>
        <asp:View ID="vwTanqueCheio" runat="server">
            <!-- Tanque Cheio -->
            <%# montarParaAssociados(70) %>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
</asp:Content>
    
