<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Adm/Adm.Master" CodeBehind="Admin.aspx.cs" Inherits="Site.Adm.Admin" %>

<asp:Content ID="cDBHeader" runat="server" ContentPlaceHolderID="cpHeader">
</asp:Content>

<asp:Content ID="cAdmin" runat="server" ContentPlaceHolderID="cpContent">
    <form id="frmTabelas" runat="server" class="form-p">
        <section class="secleft">
            <div class="dmenu">
                <div class="cad-esquerda">
                    <ul>
                        <li><asp:LinkButton ID="lbtEmpresa" runat="server" Text="Empresa" OnClick="ativarVwEmpresa"></asp:LinkButton></li>                        
                        <li><asp:LinkButton ID="lbtUsuarios" runat="server" Text="Usuários" OnClick="ativarUsuarios"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtConfigEmp" runat="server" Text="Emp. Config"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtTipo" runat="server" Text="Tipo" OnClick="ativarVwTipo"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtCategoria" runat="server" Text="Categoria" OnClick="ativarVwCategoria"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtMenu" runat="server" Text="Menu" OnClick="ativarVwMenu"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtTipoImg" runat="server" Text="Tipos das Imagens" OnClick="ativarVwTipoImg"></asp:LinkButton></li>
                        <li>Novos</li>
                        <li><asp:LinkButton ID="lbtCampoConteudo" runat="server" Text="Img Campo Conteúdo" OnClick="ativarVwImgCampoConteudo"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtmgPosicao" runat="server" Text="Img Posição" OnClick="ativarVwImgPosicao"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtImgAlinha" runat="server" Text="Img Alinhamento" OnClick="ativarVwImgAlinhamento"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtImagens" runat="server" Text="Imagens" OnClick="ativarVwImagens"></asp:LinkButton></li>
                        <li><asp:LinkButton ID="lbtSair" runat="server" Text="Sair" OnClick="fazerLogof"></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </section>
        <section class="secrigth">
            <div class="dcont-grid">
                <asp:MultiView ID="mwGrid" runat="server">
                    <asp:View ID="vwGvEmpresa" runat="server">
                        <h1 class="topform">Empresa</h1>
                        <asp:GridView ID="gvEmpresa" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGwEmpresa" OnSelectedIndexChanged="selecionarRegistroGvEmpresa">
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
                    <asp:View ID="vwGvCategoria" runat="server">
                        <h1 class="topform">Categoria</h1>
                        <asp:GridView ID="gvCategoria" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGwCategoria">
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
                    <asp:View ID="vwGvDestaque" runat="server">
                        <h1 class="topform">Menu</h1>
                        <asp:GridView ID="gvMenu" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" EditIndex="-1" SelectedIndex="-1" AllowPaging="True" AllowCustomPaging="False" OnPageIndexChanging="paginarGwMenu">
                            <AlternatingRowStyle BackColor="White" />
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
                    <asp:View ID="vwGvTiposImg" runat="server">
                        <h1 class="topform">Tipos de Imagens</h1>
                        <asp:GridView ID="gvImgTipo" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" EditIndex="-1" SelectedIndex="-1" AllowPaging="True" AllowCustomPaging="False" OnPageIndexChanging="paginarGwImgTipo">
                            <AlternatingRowStyle BackColor="White" />
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
                    <asp:View ID="vwGvUsuarios" runat="server">
                        <h1 class="topform">Usuários</h1>
                        <asp:GridView ID="gvUsuarios" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" EditIndex="-1" SelectedIndex="-1" AllowPaging="True" AllowCustomPaging="False" OnPageIndexChanging="paginarGwImgTipo">
                            <AlternatingRowStyle BackColor="White" />
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
                    <asp:View ID="vwGVTipo" runat="server">
                        <h1 class="topform">Tipo</h1>
                        <asp:GridView ID="gvTipo" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGwTipo">
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
                    <asp:View ID="vwGvImgCampoConteudo" runat="server">
                        <h1 class="topform">Imagem Campo Conteúdo</h1>
                        <asp:GridView ID="gvImgCampoConteudo" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGvImgCampoCont">
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
                    <asp:View ID="vwGvImgPosicao" runat="server"><h1 class="topform">Img Posição</h1>
                        <asp:GridView ID="gvImgPosicao" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGvImgPosicao">
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
                    <asp:View ID="vwGvImgAlinhamento" runat="server"><h1 class="topform">Img Alinhamento</h1>
                        <asp:GridView ID="gvImgAlinha" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" AllowPaging="True" OnPageIndexChanging="paginarGvImgAlinha">
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
                    <asp:View ID="vwGridImagens" runat="server">
                        <h1 class="topform">Imagens</h1>
                        <asp:GridView ID="gvImagens" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="4" EditIndex="-1" SelectedIndex="-1" AllowPaging="True" AllowCustomPaging="False" OnPageIndexChanging="paginarGwConteudo">
                            <AlternatingRowStyle BackColor="White" />
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
                    <asp:MultiView ID="mwForm" runat="server">
                        <asp:View ID="vwEmpresa" runat="server">
                            <h2>Empresa</h2>
                            <input id="iNome" name="Nome" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadEmpresa" runat="server" Text="Cadastrar" OnClick="cadastrarEmpresa" />
                            <asp:Button ID="btnEditEmpresa" runat="server" Text="Editar" OnClick="editarEmpresa" />
                            <asp:Button ID="btnExcEmpresa" runat="server" Text="Excluir" OnClick="excluirEmpresa" />
                        </asp:View>
                        <asp:View ID="vwCategoria" runat="server">
                            <h2>Categoria</h2>
                            <input id="iCodCategoria" runat="server" type="text" placeholder="Códigoo" />
                            <input id="iDescCategoria" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadCategoria" runat="server" Text="Cadastrar" OnClick="cadastrarCategoria" />
                            <asp:Button ID="btnEditCategoria" runat="server" Text="Editar" OnClick="editarCategoria" />
                            <asp:Button ID="btnExcCategoria" runat="server" Text="Excluir" OnClick="excluirCategoria" />
                        </asp:View>
                        <asp:View ID="vwMenu" runat="server">
                            <h2>Menu</h2>
                            <input id="iCodMenu" runat="server" type="text" placeholder="Código" />
                            <input id="iDescMenu" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadDestaque" runat="server" Text="Cadastrar" OnClick="cadastrarMenu" />
                            <asp:Button ID="btnEditDestaque" runat="server" Text="Editar" OnClick="editarMenu" />
                            <asp:Button ID="btnExcDestaque" runat="server" Text="Excluir" OnClick="excluirMenu" />
                        </asp:View>
                        <asp:View ID="vwTipoImagem" runat="server">
                            <h2>Tipo das Imagens</h2>
                            <input id="iCodTipoImg" runat="server" type="text" placeholder="Código" />
                            <input id="iTituloTipoImg" runat="server" type="text" placeholder="Título" />
                            <input id="iDescTipoImg" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadImg" runat="server" Text="Cadastrar" OnClick="cadastrarTipoImg" />
                            <asp:Button ID="btnEditImg" runat="server" Text="Editar" OnClick="editarTipoImg" />
                            <asp:Button ID="btnExcImg" runat="server" Text="Excluir" OnClick="excluirTipoImg" />
                        </asp:View>
                        <asp:View ID="vwUsuarios" runat="server">
                            <h2>Usuários</h2>
                            <input id="iNomeUsuario" runat="server" type="text" placeholder="Nome" />
                            <input id="iUsuarioUsu" name="Nome" runat="server" type="text" placeholder="Usuário" />
                            <input id="iSenhaUsuario" runat="server" type="password" placeholder="Senha" />
                            <input id="iEmailUsuario" runat="server" type="email" placeholder="E-mail" />
                            <input id="iCPFUsuario" runat="server" type="text" placeholder="C.P.F." />
                            <asp:Button ID="btnCadUsuario" runat="server" Text="Cadastrar" OnClick="cadastrarUsuario" />
                            <asp:Button ID="btnEditUsuario" runat="server" Text="Editar" OnClick="editarUsuario" />
                            <asp:Button ID="btnExcUsuario" runat="server" Text="Excluir" OnClick="excluirUsuario" />
                        </asp:View>
                        <asp:View ID="vwTipo" runat="server">
                            <h2>Tipo</h2>
                            <input id="iCodTipo" runat="server" type="text" placeholder="Códigoo" />
                            <input id="iDescTipo" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadTipo" runat="server" Text="Cadastrar" OnClick="cadastrarTipo" />
                            <asp:Button ID="btnEdiTipo" runat="server" Text="Editar" OnClick="editarTipo" />
                            <asp:Button ID="btnExTipo" runat="server" Text="Excluir" OnClick="excluirTipo" />
                        </asp:View>
                        <asp:View ID="vwCampoConteudo" runat="server">
                            <h2>Img Campo Conteúdo</h2>
                            <input id="iCodCampConteudo" runat="server" type="text" placeholder="Códigoo" />
                            <input id="iDescCampConteudo" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadCampConteudo" runat="server" Text="Cadastrar" OnClick="cadastrarImgCampoConteudo" />
                            <asp:Button ID="btnEdiCampConteudo" runat="server" Text="Editar" OnClick="editarImgCampoConteudo" />
                            <asp:Button ID="btnExCampConteudo" runat="server" Text="Excluir" OnClick="excluirImgCampoConteudo" />
                        </asp:View>
                        <asp:View ID="vwImgPosicao" runat="server">
                            <h2>Img Posição</h2>
                            <input id="iCodPosicao" runat="server" type="text" placeholder="Códigoo" />
                            <input id="iDescPosicao" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadPosicao" runat="server" Text="Cadastrar" OnClick="cadastrarImgPosicao" />
                            <asp:Button ID="btnEdiPosicao" runat="server" Text="Editar" OnClick="editarImgPosicao" />
                            <asp:Button ID="btnExcPosicao" runat="server" Text="Excluir" OnClick="excluirImgPosicao" />
                        </asp:View>
                        <asp:View ID="vwImgAlinhamento" runat="server">
                            <h2>Img Alinhamento</h2>
                            <input id="iCodImgAlinha" runat="server" type="text" placeholder="Códigoo" />
                            <input id="iDescImgAlinha" runat="server" type="text" placeholder="Descrição" />
                            <asp:Button ID="btnCadAlinha" runat="server" Text="Cadastrar" OnClick="cadastrarImgAlinhamento" />
                            <asp:Button ID="btnEdiAlinha" runat="server" Text="Editar" OnClick="editarImgAlinhamento" />
                            <asp:Button ID="btnExcAlinha" runat="server" Text="Excluir" OnClick="excluirImgAlinhamento" />
                        </asp:View>
                        <asp:View ID="vwFormImagens" runat="server">
                            <h1>Imagens</h1>
                            <input id="iImgTitulo" runat="server" type="text" placeholder="Título" />
                            <input id="iImgDescricao" runat="server" type="text" placeholder="Descrição" />
                            <label class="labelinpdate">Tipo:</label>
                            <select id="stImgTipo" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>
                            <label class="labelinpdate">Campo Conteúdo:</label>
                            <select id="stImgCampoConteudo" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>                            
                            <label class="labelinpdate">Posição:</label>
                            <select id="stImgPosicao" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>                            
                            <label class="labelinpdate">Alinhamento:</label>
                            <select id="stImgAlinha" runat="server" style="width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;"></select>                            
                            <input id="iImgFonte" runat="server" type="text" placeholder="Fonte da Imagem" />
                            <input id="iImgAutor" runat="server" type="text" placeholder="Autor da Imagem" />
                            <input id="iImgOrdem" runat="server" type="number" />
                            <input id="iImgHint" runat="server" type="text" placeholder="Hint" />                                
                            <asp:MultiView ID="mwImg" runat="server">
                                <asp:View ID="vwImg" runat="server">
                                    <asp:Label ID="lblDados" runat="server">1</asp:Label>                                    
                                </asp:View>
                            </asp:MultiView>
                            <asp:FileUpload ID="fuImgCont" runat="server" AllowMultiple="true" />
                            <label id="lblResp" runat="server"></label>
                            <asp:Button ib="btnTestes" runat="server" Text="Cadastrar" OnClick="cadastrarImagem" />                            
                        </asp:View>
                    </asp:MultiView>
                </div>
                <asp:Label ID="lblResult" runat="server"></asp:Label>
                <asp:Label ID="lblMsg" runat="server" ></asp:Label>
            </div>
        </section>
    </form>
</asp:Content>

