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
                        <li><asp:LinkButton ID="lbtNossaEntidade" runat="server" Text="Nossa Entidade" OnClick="ativarVwNossaEntidade"></asp:LinkButton></li>
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
                    <asp:View ID="vwGridNossaEntidade" runat="server">
                        <h1>Nossa Entidade</h1>
                        <!--<ul>
                            <li>A ASU</li>
                            <li>Nossas Pessoas</li>
                            <li>Departamento de Aposentados</li>
                            <li>Clube de Campo</li>
                            <li>Estatuto Social</li>
                            <li>Regimeto Auxilio Funeral</li>
                            <li>Prestação de Contas</li>
                            <li>Jornal</li>
                        </ul>-->
                        <asp:GridView ID="GvNossaEntidade" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarGvNossaEntidade" OnSelectedIndexChanged="selecionarRegistroGvNossaEntidade">
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
                        <asp:View ID="vwFormNossaEntidade" runat="server">                                                            
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca; margin-bottom: 8px;">Nossa Entidade</h1>                            
                            <input id="iTituloNE" runat="server" type="text" placeholder="Exemplo: A Nossa entidade..." />
                            <textarea id="taIntroducaoNE" runat="server" placeholder="Exemplo: A Nossa entidade é uma entidade sem Fins Lucrativos..." class="alturainput"></textarea>                            
                            <input id="iValidador" type="text" runat="server" visible="false" />
                            <div class="intervaloDatas">
                                <div style="width: 100%;">
                                    <asp:Label ID="lblDuracao" runat="server" CssClass="labelinpdate">Duração da Propaganda (dias)</asp:Label>
                                    <input id="iNEDuracaoPub" runat="server" type="number" value="10" onchange="atualizaDataFim" />
                                </div>
                                <div>
                                    <asp:Label ID="lblNEDataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                                    <input id="iPubIniNE" runat="server" type="date" oninit="carregarData" />
                                </div>
                                <div>
                                    <asp:Label ID="lblNEDataFim" runat="server" CssClass="labelinpdate"> Data Final</asp:Label>
                                    <input id="iPublFimNE" runat="server" type="date" oninit="carregarData" />
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnCadNossaEntidade" runat="server" Text="Cadastrar" OnClick="cadastrarNossaEntidade"/>
                            </div>
                            <br />
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca;">Imagens</h1>
                            <section>
                                <label class="labelinpdate">Tipo: Nossa Entidade </label>                                
                                <input id="iImgTitulo" runat="server" type="text" placeholder="Título" />
                                <input id="iImgDescricao" runat="server" type="text" placeholder="Descrição" />
                                <input id="iImgFonte" runat="server" type="text" placeholder="Fonte da Imagem" />
                                <input id="iImgAutor" runat="server" type="text" placeholder="Autor da Imagem" />
                                <input id="iImgHint" runat="server" type="text" placeholder="Hint" />
                                <input id="iLink" runat="server" type="tel" placeholder="Link" />
                                <asp:MultiView ID="mwImg" runat="server">
                                    <asp:View ID="vwImg" runat="server">
                                        <asp:Label ID="lblDados" runat="server"></asp:Label>
                                        <asp:ListBox ID="lbDados" runat="server" CssClass="ListBoxDados"></asp:ListBox>
                                    </asp:View>
                                </asp:MultiView>
                                <asp:FileUpload ID="fuImgCont" runat="server" AllowMultiple="true" />
                                <label id="lblResp" runat="server"></label>
                                <br />
                                <asp:Button ID="btnCadImg" runat="server" Text="Inserir Imagem"  />
                                <asp:Button ID="btnTamImg" runat="server" Text="Checar Tamanho da Imagem"/>
                            </section>

                                           
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg"></asp:Label>
        </section>
    </form>
</asp:Content>
