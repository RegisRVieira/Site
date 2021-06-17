<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Conteudo.Master" CodeBehind="ContNossasPessoas.aspx.cs" Inherits="Site.ContNossasPessoas" %>


<asp:Content ContentPlaceHolderID="CpLateral" runat="server">
    <div>.</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cpContent" runat="server">
    <form id="form1" runat="server" >  
        <div class="BoxPessoas">            
            <section class="BoxPessoas-Botoes" >
                <div class="btns" style="background-color: red; width: 100%"></div>
                <asp:LinkButton ID="lbDiretores" runat="server" Text="Diretoria" CssClass="efeito efeito-1" OnClick="ativarVwDiretores"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:LinkButton ID="lbConselho" runat="server" Text="Conselho Fiscal" CssClass="efeito efeito-1" OnClick="ativarVwConselho"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:LinkButton ID="lbFuncionarios" runat="server" Text="Funcionários" CssClass="efeito efeito-1" OnClick="ativarVwFuncionarios"></asp:LinkButton>
                <a id="voltarNEntidade" runat="server" class="efeito efeito-1" href="ContNossaEntidade.aspx?vNE=1">Voltar</a>
            </section>
            <!--
            <section class="BoxPessoas-Corpo">
                <section class="BoxPessoas-Lateral">                    
                </section>
                <section class="BoxPessoas-Dados">
                    <section class="BoxPessoas-Dados-Img">
                        Imagem
                    </section>
                    <section class="BoxPessoas-Dados-Text">
                        Texto
                    </section>
                </section>
            </section>
            -->            
        </div>        
        <div style="margin: 0; padding: 0; margin-top: 30px;" >
            <asp:Label ID="lblResult" runat="server" ></asp:Label>            
            <asp:MultiView ID="mwNossasPessoas" runat="server">
                <!--("DIRDEP" + "'" + " , " + "'" + "DIREXE")-->
                <asp:View ID="View1" runat="server">
                    <div><%# MontarNossasPessoas("DIREXE") %></div>
                    <div><%# MontarNossasPessoas("DIRDEP") %></div>                    
                </asp:View>
                <asp:View ID="vwConselho" runat="server">
                    <div>                        
                        <%# MontarNossasPessoas("DIRFISC") %>
                    </div>
                </asp:View>
                <asp:View ID="vwFuncionarios" runat="server">
                    <div>                        
                        <%# MontarNossasPessoas("FUNASU") %>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <asp:Label ID="lblMsgErro" runat="server"></asp:Label>
    </form>
</asp:Content>

