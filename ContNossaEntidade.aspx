<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContNossaEntidade.aspx.cs" Inherits="Site.ContNossaEntidade" %>

<asp:Content ContentPlaceHolderID="CpLateral" runat="server">      
    <script type="text/javascript" src="Js/Apoio.js"></script>    
    <form id="frmNossaEntidade" runat="server">       
        <div id="menuLateralNE" class="menuflutuaNE">
            <ul>                
                <li><asp:LinkButton ID="lbAAsu" runat="server" Text="A ASU" OnClick="ativaASU"></asp:LinkButton></li>                          
                <li><a href="ContNossasPessoas.aspx">Nossas "Pessoas"</a></li>                
                <li><asp:LinkButton ID="lbSede" runat="server" Text="Sub Sede Lageado" OnClick="ativaSedeLageado"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbDap" runat="server" Text="Departamento de Aposentados" OnClick="ativaDap"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbClube" runat="server" Text="Clube de Campo" OnClick="ativaClube"></asp:LinkButton></li>                            
                <li><asp:LinkButton ID="lbEstatuto" runat="server" Text="Estatuto Social" OnClick="nossoEstatuto"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbAuxilioFuneral" runat="server" Text="Regimento Auxilio Funeral" OnClick="nossoRegimento"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbBalancete" runat="server" Text="Prestação de Contas" OnClick="nossoBalancete"></asp:LinkButton></li>
                <li><asp:LinkButton ID="lbJornal" runat="server" Text="Jornal" OnClick="nossoJornal"></asp:LinkButton></li>                            
                <li id="treeline-icon" class="treeline-icon" onclick="AcessarNossaEntidade()">&#9776;</li>
                <li id="treeline-NossaEntidade" class="treeline-closeicon" onclick="FecharNossaEntidade() ">&cross;</li>
            </ul>
        </div>
    </form>    
</asp:Content>
<asp:Content ContentPlaceHolderID="cpContent" runat="server">
    <asp:MultiView ID="mwConteudo" runat="server">        
        <asp:View ID="vwASU" runat="server">            
            <div style="margin: 0; padding: 0">
                 <%# montarConteudo(31) %>                
            </div>
        </asp:View>
        <asp:View ID="vwSedeLageado" runat="server">                    
            <div style="margin: 0; padding: 0">
                 <%# montarConteudo(63) %>                
            </div>
        </asp:View>
        <asp:View ID="vwDap" runat="server">            
            <div style="margin: 0; padding: 0">
                 <%# montarConteudo(62) %>                
            </div>
        </asp:View>
        <asp:View ID="vwClube" runat="server">  
            <main>
                 <%# montarConteudo(61) %>            
            </main>
        </asp:View>
        <asp:View ID="vwEstatuto" runat="server">
            <p>Estatuto</p>
            
        </asp:View>
        <asp:View ID="vwRegimento" runat="server">            
            <%# montarConteudo(64) %>
        </asp:View>
        <asp:View ID="vwBalancete" runat="server">
            <p>Balancete</p>
            
        </asp:View>
        <asp:View ID="vwJornal" runat="server">
            <p>Jornal</p>
            <div style="margin: 0; padding:0"><%# montarConteudo(27) %></div>
        </asp:View>     
        <asp:View ID="vwNPessoas" runat="server">
                <p>Nossas Pessoas</p>                            
        </asp:View>
    </asp:MultiView>  
    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>        
    <section id="margemRodape"></section>
</asp:Content>

