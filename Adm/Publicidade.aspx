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
                            <asp:Label runat="server" CssClass="labelinpdate">Tipo do Conteúdo</asp:Label>
                            <!-- < %# gerarDDLMenu() %> Capturar cod  -->
                            <select id="stTipo" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>
                            <input id="iTitulo" runat="server" type="text" placeholder="Título" />
                            <textarea id="taIntroducao" runat="server" placeholder="Introcução" class="alturainput"></textarea>
                            <textarea id="taConteudo" runat="server" placeholder="Contexto" class="alturainput"></textarea>
                            <textarea id="taComplemento" runat="server" placeholder="Complemento do Texto" class="alturainput"></textarea>
                            <textarea id="taConclusao" runat="server" placeholder="Conclusão do Texto" class="alturainput"></textarea>
                            <asp:Label runat="server" CssClass="labelinpdate">Categoria do Conteúdo:</asp:Label>
                            <select id="stCategoria" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select><br />
                            <br />
                            <asp:Label runat="server" CssClass="labelinpdate">Destaque:</asp:Label>
                            <select id="stDestaque" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select><br />
                            <br />
                            <asp:Label ID="lblDataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                            <input id="iPublIniConteudo" runat="server" type="date" />
                            <asp:Label ID="lblDataFim" runat="server" CssClass="labelinpdate"> Data Final:</asp:Label>
                            <input id="iPublFinConteudo" runat="server" type="date" />
                            <input id="abrirJanela" runat="server" type="submit" value="Cadastrar Imagens" onclick="abrirJanela()" />
                            <a id="aImagem" href="S-Imagens.aspx" runat="server" target="_blank" style="-moz-user-focus:normal">Imagens ... </a>
                            <br />
                            <input id="iFonteConteudo" runat="server" type="text" placeholder="Fonte" />
                            <input id="iAutorConteudo" runat="server" type="text" placeholder="Autor" />
                            <asp:Label ID="lblOrdem" runat="server" CssClass="labelinpdate">Ordem:</asp:Label>
                            <input id="iOrdemConteudo" runat="server" type="number" />
                            <asp:Button ID="btnCadConteudo" runat="server" Text="Inserir" />
                            <asp:Button ID="btnEditarConteudo" runat="server" Text="Editar" />
                            <asp:Button ID="btnExcluirConteudo" runat="server" Text="Excluir" />
                        </asp:View>
                        <asp:View ID="vwFormDestaque" runat="server">
                            <h1>Destaque</h1>
                        </asp:View>
                        <asp:View ID="vwFormLogo" runat="server">
                            <h1>Logo</h1>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
