<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContConv.aspx.cs" Inherits="Site.ContConv" %>


<asp:Content ContentPlaceHolderID="CpLateral" runat="server">
    <form id="frmConveniados" runat="server">
        <script src="Js/Apoio.js"></script>
        <div id="menulateral" class="menuflutua">
            <ul>
            <li><asp:LinkButton ID="lbPublic" runat="server" Text="Publicidade" OnClick="ativarPublicidade"></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbAntecipa" runat="server" Text="Antecipação de Recebíveis" OnClick="ativarAntecipacao"></asp:LinkButton></li>
            <li><asp:LinkButton ID="lbFunciona" runat="server" Text="Como Funciona" OnClick="ativarComoFunciona"></asp:LinkButton></li>                                                         
            <li><a href="PropostaConvenio.aspx" target="_blank">Enviar Proposta para se Conveniar</a></li>
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
        <asp:View ID="vwProposta" runat="server">
            <input id="iNome" runat="server" type="text" name="Nome" placeholder="Nome Fantasia">
            <input id="iRazao" runat="server" type="text" name="Razao" placeholder="Razão Social">
            <input id="iTel" runat="server" type="tel" name="Telefone" placeholder="Telefone">
            <input id="iCel" runat="server" type="tel" name="Celular" placeholder="Celular">
            <input id="iEmail" runat="server" type="email" name="email" placeholder="e-mail"> 
            <input id="iContato" runat="server" type="text" name="contato" placeholder="Contato">
            
                <div class="botaoGlobal">                    
                    <script>
                        function enviarProposta() {

                            var nome = document.getElementById('iNome');
                            var razao = document.getElementById('iRazao');

                            
                            // alerta o valor do campo
                            alert(nome.value + " - " + razao.value);

                                // impede o envio do form
                            e.preventDefault();
                            

                            var xRet = "";

                            xRet += "<p>" + iNome.Value + "</p>";
                            xRet += "<p>" + iRazao.Value + "</p>";
                            xRet += "<p>" + iTel.Value + "</p>";
                            xRet += "<p>" + iCel.Value + "</p>";
                            xRet += "<p>" + iEmail.Value + "</p>";
                            xRet += "<p>" + iContato.Value + "</p>";

                            //lblMsg.Text = "Sua Msg foi enviada com Sucesso!!!" + "\n" + xRet;

                            alert(xRet);
                            
                        }
                    </script>
                    <input id="btnProposta" type="submit" name="Enviar" onclick="enviarProposta()"/>                    
                </div>
            
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblMsgErro" runat="server"></asp:Label>    
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>