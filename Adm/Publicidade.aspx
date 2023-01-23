<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Adm/Adm.Master" CodeBehind="Publicidade.aspx.cs" Inherits="Site.Adm.Publicidade" %>

<asp:Content ID="cMenu" ContentPlaceHolderID="cpContent" runat="server">
    <form id="frmConteudo" runat="server" class="form-p">
        <section class="secleft">
            <div class="dmenu">
                <div class="cad-esquerda">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lbtPublicidade" runat="server" Text="Publicidade" OnClick="ativaVwPublicidade"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lblGuiaDestaque" runat="server" Text="Destaque Guia" OnClick="ativaVwGuiaDestaque"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtGuiaLogo" runat="server" Text="Logo Guia" OnClick="ativaVwGuiaLogo"></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </section>
        <section class="secrigth">
            <div class="dcont-grid">
                <asp:MultiView ID="mwGridPublicidade" runat="server">
                    <asp:View ID="vwGridPublicidade" runat="server">
                        <h1 class="topform">Publicidade</h1>
                        <asp:Label ID="lblTeste" runat="server"></asp:Label>
                    </asp:View>
                    <asp:View ID="vwGridGuiaDestaque" runat="server">
                        <h1 class="topform">Destaque</h1>
                    </asp:View>
                    <asp:View ID="vwGridGuiaLogo" runat="server">
                        <h1 class="topform">Logo</h1>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="dcont">
                <div class="dcont-form">
                    <asp:MultiView ID="mwFormPublicidade" runat="server">
                        <asp:View ID="vwFormPublicidade" runat="server">
                            <h1>Conteúdo - Não sei...</h1>                            
                        </asp:View>
                        <asp:View ID="vwFormDestaque" runat="server">
                            <h1>Destaque</h1>
                            <input id="iIdConv" runat="server" type="text" placeholder="ID do Convênio" />
                            <asp:Button ID="btnCarregaDadosConv" runat="server" Text="Buscar Convênio" OnClick="carregaConvenio" />
                            <input id="iNomeConv" runat="server" type="text" disabled="disabled" />
                            <asp:FileUpload ID="fuDestaqueGuia" runat="server" />
                            <asp:Button ID="btnDestaqueGuia" runat="server" Text="Gravar" OnClick="cadastrarPublicidadeGuia"/>
                            <section>
                                <asp:MultiView ID="mwDadosDestaque" runat="server">
                                    <asp:View ID="vwDadosDestaque" runat="server">
                                        <asp:Label ID="lblDados" runat="server"></asp:Label>                                        
                                    </asp:View>
                                </asp:MultiView>
                            </section>
                        </asp:View>
                        <asp:View ID="vwFormLogo" runat="server">
                            <h1>Logo</h1>                            
                            <input id="iIdConvLogo" runat="server" type="text" placeholder="ID do Convênio" />                            
                            <asp:Button ID="btnDadosConvLogo" runat="server" Text="Buscar Convênio" OnClick="carregaConvenio" />
                            <input id="iNomeConvLogo" runat="server" type="text" disabled="disabled" />
                            <asp:FileUpload ID="fuLogoGuia" runat="server" />
                            <asp:Button ID="btnLogoGuia" runat="server" Text="Gravar" OnClick="cadastrarPublicidadeGuia"/>
                            <section>
                                <asp:MultiView ID="mwDadosLogo" runat="server">
                                    <asp:View ID="vwDadosLogo" runat="server">
                                        <asp:Label ID="lblDadosLogo" runat="server"></asp:Label>                                        
                                    </asp:View>
                                </asp:MultiView>
                            </section>
                        </asp:View>                        
                    </asp:MultiView>
                    <asp:Label id="lblMsg" runat="server"></asp:Label>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
