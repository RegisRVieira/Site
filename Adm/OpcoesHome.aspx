<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Adm/Adm.Master" CodeBehind="OpcoesHome.aspx.cs" Inherits="Site.Adm.OpcoesHome" ValidateRequest="false" %>

<asp:Content ID="cMenu" ContentPlaceHolderID="cpContent" runat="server">
    <form id="frmOpcoes" runat="server" class="form-p">
        <style>
            input[type=number] {
                width: 110px;
                height: auto;
                color: #f26907;
            }

            .iDuracao {
                width: 20px;
                height: auto;
                color: #f26907; 
            }

            .intervaloDatas {
                margin: 0 auto;
                border: 1px solid #cccaca;
                width: 74%;
                height: 120px;
            }

                .intervaloDatas div {
                    float: left;
                }

            .ListBoxDados {
                width: 400px;
                min-height: 50px;
            }
            .lblMsg{
                font-family: 'Book Antiqua';
                color: #3958a0;
                font-size: 1em;
            }
            .slTipo{
                width: 98%; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;
            }
        </style>
        <section class="secleft">
            <div class="dmenu">
                <div class="cad-esquerda">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lbtSlider" runat="server" Text="Slider" OnClick="ativarSlider"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtBox33" runat="server" Text="Box33" OnClick="ativarVwBox33"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtBox100" runat="server" Text="Box100" OnClick="ativarVwBox100"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtMateria" runat="server" Text="Matéria" OnClick="ativarVwMateria"></asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="lbtConteudo" runat="server" Text="Publicidade" OnClick="ativarVwPublicidade"></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </section>
        <section class="secrigth">
            <div class="dcont-grid">
                <asp:MultiView ID="mwGridOpcoesHome" runat="server">
                    <asp:View ID="vwGridSlider" runat="server">
                        <h1 class="topform">Slider</h1>
                        <asp:GridView ID="gvSlider" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarGvSlider" OnSelectedIndexChanged="capturarConteudoId">
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
                        <asp:Label ID="lblTeste" runat="server"></asp:Label>
                    </asp:View>
                    <asp:View ID="vwGridBox33" runat="server">
                        <h1 class="topform">Box33</h1>
                        <asp:GridView ID="gvBox33" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarBox33" OnSelectedIndexChanged="capturarConteudoId">
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
                    <asp:View ID="vwGridBox100" runat="server">
                        <h1 class="topform">Box100</h1>
                        <asp:GridView ID="gvBox100" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarBox100" OnSelectedIndexChanged="capturarConteudoId">
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
                    <asp:View ID="vwGridMateria" runat="server">
                        <h1 class="topform">Materia</h1>
                        <asp:GridView ID="gvMateria" runat="server" CellPadding="2" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="2" AllowPaging="True" OnPageIndexChanging="paginarMateria" OnSelectedIndexChanged="selecionarRegistroGvMateria">
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
                    <asp:View ID="vwGridConteudo" runat="server">
                        <h1 class="topform">Publicidade</h1>
                    </asp:View>
                    <asp:View ID="vwGridPublicidade" runat="server">
                        <h1 class="topform">Publicidade2</h1>
                    </asp:View>
                    <asp:View ID="vwGridGuiaDestaque" runat="server">
                        <h1 class="topform">Guia Destaque</h1>
                    </asp:View>
                    <asp:View ID="vwGridGuiaLogo" runat="server">
                        <h1 class="topform">Guia Logo</h1>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="dcont">
                <div class="dcont-form">
                    <asp:MultiView ID="mwFormOpcoesHome" runat="server">
                        <asp:View ID="vwFormSlider" runat="server">
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca; margin-bottom: 8px;">Dados Conteúdo</h1>
                            <!--<asp:Label runat="server" CssClass="labelinpdate">Tipo do Conteúdo</asp:Label>-->
                            <!-- < %# gerarDDLMenu() %> Capturar cod  -->
                            <!--<select id="stTipo" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>-->
                            <input id="iTitulo" runat="server" type="text" placeholder="Exemplo: Festa Fim de Ano" />
                            <textarea id="taIntroducao" runat="server" placeholder="Exemplo: Slider Promocional para divulgação da Festa de Fim de ano..." class="alturainput"></textarea>                            
                            <input id="iValidador" type="text" runat="server" visible="false" />
                            <div class="intervaloDatas">
                                <div style="width: 100%;">
                                    <asp:Label ID="lblDuracao" runat="server" CssClass="labelinpdate">Duração da Propaganda (dias)</asp:Label>
                                    <input id="iDuracaoPub" runat="server" type="number" value="10" onchange="atualizaDataFim" />
                                </div>
                                <div>
                                    <asp:Label ID="lblDataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                                    <input id="iPublIniConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                                <div>
                                    <asp:Label ID="lblDataFim" runat="server" CssClass="labelinpdate"> Data Final</asp:Label>
                                    <input id="iPublFimConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnCadContSlider" runat="server" Text="Cadastrar" OnClick="cadastrarContSlider" />
                            </div>
                            <br />
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca;">Imagens</h1>
                            <section>
                                <label class="labelinpdate">Tipo: SLIDE </label>                                
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
                                <asp:Button ID="btnCadImg" runat="server" Text="Inserir Imagem" OnClick="inserirImagemSlider" />
                                <asp:Button ID="btnTamImg" runat="server" Text="Checar Tamanho da Imagem" OnClick="checarTamanhoImg" />
                            </section>
                        </asp:View>
                        <asp:View ID="vwFormBox33" runat="server">                           
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca; margin-bottom: 8px;">Box33</h1>                                    
                            <input id="iB33Titulo" runat="server" type="text" placeholder="Exemplo: Promoção Vale..." />
                            <textarea id="taB33Introducao" runat="server" placeholder="Exemplo: Promoção Vale compras Mensal" class="alturainput"></textarea>                            
                            <input id="iB33Valida" type="text" runat="server" visible="false" />
                            <div class="intervaloDatas">
                                <div style="width: 100%;">
                                    <asp:Label ID="lblB33Duracao" runat="server" CssClass="labelinpdate">Duração da Propaganda (dias)</asp:Label>
                                    <input id="iB33DuracaoPub" runat="server" type="number" value="10" onchange="atualizaDataFim" />
                                </div>
                                <div>
                                    <asp:Label ID="lblB33DataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                                    <input id="iB33PubIniConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                                <div>
                                    <asp:Label ID="lblB33DataFim" runat="server" CssClass="labelinpdate"> Data Final</asp:Label>
                                    <input id="iB33PubFimConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnCadBox33" runat="server" Text="Cadastrar" OnClick="cadastrarContB33" />
                            </div>
                            <br />
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca;">Imagens</h1>
                            <section>
                                <label class="labelinpdate">Tipo: Box33 </label><br />
                                <label class="labelinpdate">Escolha o <span style="font-style: oblique">Box</span> onde deverá ser exibida a Imagem</label>
                                <select id="stImgTipo" runat="server" class="slTipo"></select>
                                <input id="iB33ImgTitulo" runat="server" type="text" placeholder="Título" />
                                <input id="iB33ImgDescricao" runat="server" type="text" placeholder="Descrição" />
                                <input id="iB33ImgFonte" runat="server" type="text" placeholder="Fonte da Imagem" />
                                <input id="iB33ImgAutor" runat="server" type="text" placeholder="Autor da Imagem" />
                                <input id="iB33ImgHint" runat="server" type="text" placeholder="Hint" />
                                <input id="iB33ImgLink" runat="server" type="text" placeholder="Link" />
                                <asp:MultiView ID="mwImg33" runat="server">
                                    <asp:View ID="vwImg33" runat="server">
                                        <asp:Label ID="lblB33Dados" runat="server"></asp:Label>
                                        <asp:ListBox ID="lbB33Dados" runat="server" CssClass="ListBoxDados"></asp:ListBox>
                                    </asp:View>
                                </asp:MultiView>
                                <asp:FileUpload ID="fuB33ImgCont" runat="server" AllowMultiple="true" />                                
                                <br />
                                <asp:Button ID="btnCadImgB33" runat="server" Text="Inserir Imagem" OnClick="inserirImagemB33" />                                
                            </section>
                        </asp:View>
                        <asp:View ID="vwFormBox100" runat="server">                            
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca; margin-bottom: 8px;">Box100</h1>

                            <input id="iB100Titulo" runat="server" type="text" placeholder="Exemplo: Festa fim de Ano Dap" />
                            <textarea id="taB100Introducao" runat="server" placeholder="Exemplo: Foto da Festa de Final de Ano dos Aposentados..." class="alturainput"></textarea>                            
                            <input id="iB100Valida" type="text" runat="server" visible="false" />
                            <div class="intervaloDatas">
                                <div style="width: 100%;">
                                    <asp:Label ID="lblB100Duracao" runat="server" CssClass="labelinpdate">Duração da Propaganda (dias)</asp:Label>
                                    <input id="iB100DuracaoPub" runat="server" type="number" value="10" onchange="atualizaDataFim" />
                                </div>
                                <div>
                                    <asp:Label ID="lblB100DataIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                                    <input id="iB100PubIniConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                                <div>
                                    <asp:Label ID="lblB100DataFim" runat="server" CssClass="labelinpdate"> Data Final</asp:Label>
                                    <input id="iB100PubFimConteudo" runat="server" type="date" oninit="carregarData" />
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnCadBox100" runat="server" Text="Cadastrar" OnClick="cadastrarContB100" />
                            </div>
                            <br />
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca;">Imagens</h1>
                            <section>
                                <label class="labelinpdate">Tipo: Box100 </label><br />
                                <input id="iB100ImgTitulo" runat="server" type="text" placeholder="Título" />
                                <input id="iB100ImgDescricao" runat="server" type="text" placeholder="Descrição" />
                                <input id="iB100ImgFonte" runat="server" type="text" placeholder="Fonte da Imagem" />
                                <input id="iB100ImgAutor" runat="server" type="text" placeholder="Autor da Imagem" />
                                <input id="iB100ImgHint" runat="server" type="text" placeholder="Hint" />
                                <input id="iB100ImgLink" runat="server" type="text" placeholder="Link" />
                                <asp:MultiView ID="mwImg100" runat="server">
                                    <asp:View ID="vwImg100" runat="server">
                                        <asp:Label ID="lblB100Dados" runat="server"></asp:Label>
                                        <asp:ListBox ID="lbB100Dados" runat="server" CssClass="ListBoxDados"></asp:ListBox>
                                    </asp:View>
                                </asp:MultiView>
                                <asp:FileUpload ID="fuB100ImgCont" runat="server" AllowMultiple="true" />                                
                                <br />
                                <asp:Button ID="btnCadImgB100" runat="server" Text="Inserir Imagem" OnClick="inserirImagemB100" />
                            </section>
                        </asp:View>
                        <asp:View ID="vwFormMateria" runat="server">
                            <h1>Materia</h1>
                            <asp:Label ID="lblIdMateria" runat="server" Visible="false"></asp:Label>
                            <input id="iMatTitulo" runat="server" type="text" placeholder="Título" />
                            <textarea id="taMatIntroducao" runat="server" placeholder="Introcução" class="alturainput"></textarea>                            
                            <textarea id="taMatConteudo" runat="server" placeholder="Conteúdo" class="alturainput"></textarea>
                            <textarea id="taMatComplemento" runat="server" placeholder="Complemento" class="alturainput"></textarea>
                            <textarea id="taMatConclusao" runat="server" placeholder="Conclusão" class="alturainput"></textarea>
                            <input id="iMatValida" type="text" runat="server" visible="false" />
                            <div class="intervaloDatas">
                                <div>
                                    <asp:Label ID="lblMatPubIni" runat="server" CssClass="labelinpdate">Data Inicial</asp:Label>
                                    <input id="iMatPubIni" runat="server" type="date" oninit="carregarData" />
                                </div>
                                <div>
                                    <asp:Label ID="lblMatPubFim" runat="server" CssClass="labelinpdate"> Data Final</asp:Label>
                                    <input id="iMatPubFim" runat="server" type="date" oninit="carregarData" />
                                </div>
                            </div>
                            <input id="iMatFonte" runat="server" placeholder="Fonte" />
                            <input id="iMatAutor" runat="server" placeholder="Autor" />
                            <div>
                                <asp:Button ID="btnCadMateria" runat="server" Text="Cadastrar" OnClick="cadastrarMateria" />
                            </div>
                            <br />
                            <h1 style="width: 100%; background: gray; font-size: 28px; color: #cccaca;">Imagens</h1>
                            <section>                                
                                <label class="labelinpdate">Tipo de Imagem</label>
                                <select id="stMatImgTipo" runat="server" class="slTipo"></select>
                                <label class="labelinpdate">Alinhamento da Imagem</label>
                                <select id="stMatImgAlinha" runat="server" class="slTipo"></select>
                                <label class="labelinpdate">Local onde a Imagem deve ser Exibida</label>
                                <select id="stMatImgCampoConteudo" runat="server" class="slTipo"></select>
                                <!--<asp:Button ID="btntestarSelect" runat="server" Text="Testar Select" OnClick="proporcaoImgMateria" />-->
                                <input id="iMatImgTitulo" runat="server" type="text" placeholder="Título" />
                                <input id="iMatImgDesc" runat="server" type="text" placeholder="Descrição" />
                                <input id="iMatImgFonte" runat="server" type="text" placeholder="Fonte da Imagem" />
                                <input id="iMatImgAutor" runat="server" type="text" placeholder="Autor da Imagem" />
                                <input id="iMatImgHint" runat="server" type="text" placeholder="Hint" />
                                <input id="iMatImgLink" runat="server" type="text" placeholder="Link" />
                                <asp:MultiView ID="mwImgMat" runat="server">
                                    <asp:View ID="vwImgMat" runat="server">
                                        <asp:Label ID="lblMatDados" runat="server"></asp:Label>
                                        <asp:ListBox ID="lbMatDados" runat="server" CssClass="ListBoxDados"></asp:ListBox>
                                    </asp:View>
                                </asp:MultiView>
                                <asp:FileUpload ID="fuMatImg" runat="server" AllowMultiple="true" />                                
                                <br />
                                <asp:Button ID="btnIsenrirImgMateria" runat="server" Text="Inserir Imagem" OnClick="InserirImgMateria" />
                            </section>

                        </asp:View>
                        <asp:View ID="vwFormConteudo" runat="server">
                            <h1>Conteudo</h1>
                        </asp:View>
                        <asp:View ID="vwFormPublicidade" runat="server">
                            <h1>Publicidade</h1>
                        </asp:View>
                        <asp:View ID="vwFormGuiaDestaque" runat="server">
                            <h1>Guia Destaque</h1>
                        </asp:View>
                        <asp:View ID="vwFormGuiaLogo" runat="server">
                            <h1>Guia Logo</h1>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg"></asp:Label>
        </section>
    </form>
</asp:Content>
