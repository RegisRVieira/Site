<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Adm/Adm.Master" CodeBehind="Conteudo.aspx.cs" Inherits="Site.Adm.Conteudo" %>

<asp:Content ID="cConteudo" ContentPlaceHolderID="cpContent" runat="server">        
    <script>
        function abrirImagens() {
            window.open("S-Imagens.aspx", "minhaJanela", "width= 950, height= 600 ");
        }
    </script>
    <form id="frmConteudo" runat="server" class="form-p">
        <section class="secleft">
            <div class="dmenu">
                <div class="cad-esquerda">                  
                    <ul>                        
                        <li><asp:LinkButton ID="lbtCadConteudo" runat="server" Text="Conteudo do Site" OnClick="ativarVwCadConteudo"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtNossaEntidade" runat="server" Text="Nossa Entidade"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtAssociados" runat="server" Text="Associados"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtConvenios" runat="server" Text="Convênios"></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </section>
        <section class="secrigth">
            <div class="dcont-grid" >
                <asp:MultiView ID="mwGridConteudo" runat="server">
                    <asp:View ID="vwGridConteudo" runat="server">
                        <h1 class="topform">Conteúdo</h1>                                   
                        <asp:GridView ID="gvConteudo" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarGwConteudo" OnSelectedIndexChanged="capturarConteudoId">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#22396f" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#22396f" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                        <asp:label id="lblTeste" runat="server"></asp:label>
                    </asp:View>
                </asp:MultiView>
            </div>            
            <div class="dcont">
                <div class="dcont-form">
                    <asp:MultiView ID="mwFormConteudo" runat="server">
                        <asp:View ID="vwFormConteudo" runat="server">
                            <h1>Conteúdo - Não sei...</h1>   
                            <asp:Label runat="server" CssClass="labelinpdate">Tipo do Conteúdo</asp:Label>
                            <!-- < %# gerarDDLMenu() %> Capturar cod  -->
                            <select id="stTipo" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>
                            <input id="iTitulo" runat="server" type="text" placeholder="Título"/>
                            <textarea id="taIntroducao" runat="server" placeholder="Introcução" class="alturainput"></textarea>
                            <textarea id="taConteudo" runat="server" placeholder="Contexto" class="alturainput"></textarea>
                            <textarea id="taComplemento" runat="server" placeholder="Complemento do Texto" class="alturainput"></textarea>
                            <textarea id="taConclusao" runat="server" placeholder="Conclusão do Texto" class="alturainput"></textarea>
                            <asp:Label runat="server" CssClass="labelinpdate">Categoria do Conteúdo:</asp:Label>
                            <select id="stCategoria" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select><br /><br />
                            <asp:Label runat="server" CssClass="labelinpdate">Destaque:</asp:Label>
                            <select id="stDestaque" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select><br /><br />
                            <asp:Label ID="lblDataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                            <input id="iPublIniConteudo" runat="server" type="date" />
                            <asp:Label ID="lblDataFim" runat="server" CssClass="labelinpdate"> Data Final:</asp:Label>
                            <input id="iPublFinConteudo" runat="server" type="date" />                            
                            <input id="abrirJanela" runat="server" type="submit" value="Cadastrar Imagens" onclick="abrirJanela()" />
                            <a id="aImagem" href="S-Imagens.aspx" runat="server" target="_blank" style="-moz-user-focus"> Imagens ... </a><br />                            
                            <!--<asp:Button ID="btnImagens" runat="server" Text="Cadastrar Imagens" OnClick="cadastrarImagens" CssClass="BotaoGrandao"/>-->
                            <input id="iFonteConteudo" runat="server" type="text" placeholder="Fonte" />
                            <input id="iAutorConteudo" runat="server" type="text" placeholder="Autor" />
                            <asp:Label ID="lblOrdem" runat="server" CssClass="labelinpdate">Ordem:</asp:Label>
                            <input id="iOrdemConteudo" runat="server" type="number" />                            
                            <asp:Button ID="btnCadConteudo" runat="server" Text="Inserir" OnClick="cadastrarConteudo" />
                            <asp:Button ID="btnEditarConteudo" runat="server" Text="Editar" OnClick="editarConteudo" />
                            <asp:Button ID="btnExcluirConteudo" runat="server" Text="Excluir" OnClick="excluirConteudo" />                            
                        </asp:View>
                        <asp:View ID="vwFormCadConteudo" runat="server">                            
                            <h1><asp:Label ID="lblTipo" runat="server"></asp:Label></h1>
                            <label class="labelinpdate">Destaque:</label>
                            <select id="stContMenu" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>
                            <textarea id="taContTitulo" runat="server" placeholder="Título" class="alturainput"></textarea>
                            <textarea id="taContIntroducao" runat="server" placeholder="Introdução" class="alturainput"></textarea>
                            <textarea id="taContConteudo" runat="server" placeholder="Contexto" class="alturainput"></textarea>
                            <textarea id="taContComplemento" runat="server" placeholder="Complemento" class="alturainput"></textarea>
                            <textarea id="taContConclusao" runat="server" placeholder="Conclusão" class="alturainput"></textarea>                             
                            <input id="iAbrirImagens" runat="server" type="submit" value="Cadastrar Imagens" onclick="abrirImagens()" />
                            <input id="iDataPubIni" runat="server" type="date" />
                            <input id="iDataPubFim" runat="server" type="date" />
                            <input id="iContFonte" runat="server" placeholder="Fonte" />
                            <input id="iContAutor" runat="server" placeholder="Autor" />
                            <asp:Button ID="btnContCadatro" runat="server" Text="Inserir" OnClick="cadastrarContConteudo" />
                            <asp:Button ID="btnContEditar" runat="server" Text="Editar" OnClick="editarContConteudo" />
                            <asp:Button ID="btnContExcluir" runat="server" Text="Excluir" OnClick="excluirContConteudo" />                            
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
