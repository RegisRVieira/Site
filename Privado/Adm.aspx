<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adm.aspx.cs" Inherits="Site.Privado.Adm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Adm</title>
    <link rel="stylesheet" href="../Css/pStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="secMenu">            
            <asp:Label ID="lblLogado" runat="server"></asp:Label>
            <div style="margin: 0 auto; width: 70%; height: auto;">
                <ul>
                    <p>Cadastro</p>
                    <asp:LinkButton ID="lbtUsuarios" runat="server" OnClick="ativarUsuario"><li>Usuários</li></asp:LinkButton>
                    <asp:LinkButton ID="lbtTipo" runat="server" OnClick="ativarTipo"><li>Tipo</li></asp:LinkButton>
                    <asp:LinkButton ID="lbtConteudo" runat="server" OnClick="ativarConteudo"><li>Conteúdo</li></asp:LinkButton>
                </ul>
            </div>
            <section class="secForm">
                <asp:MultiView ID="mwPessoal" runat="server">
                    <asp:View ID="vwBoasVindas" runat="server">
                        <label>Bem Vindo!!!</label>
                        <label>Vamos Começar...</label>                                                
                    </asp:View>
                    <asp:View ID="vwUsuarios" runat="server">
                        <p>Usuários</p>
                        <input id="iNomeUsuario" runat="server" type="text" placeholder="Nome" />
                        <input id="iSenhaUsuario" runat="server" type="password" placeholder="Senha" />
                        <input id="iDescUsuario" runat="server" type="text" placeholder="Descrição" />
                        <div class="alinhaBotao">
                            <asp:Button ID="btnCadUsuario" runat="server" Text="Cadastrar" OnClick="cadastrarUsuario" />
                        </div>
                        <section >                            
                            <asp:GridView ID="gvUsuarios" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>                            
                        </section>
                    </asp:View>
                    <asp:View ID="vwTipo" runat="server">
                        <p>Tipo</p>                        
                        <input id="iCodTipo" runat="server" type="text" placeholder="Código" required="required" />
                        <input id="iNomeTipo" runat="server" type="text" placeholder="Nome" required="required" />
                        <input id="iDescTipo" runat="server" type="text" placeholder="Descrição" required="required" />
                        <div class="alinhaBotao">
                            <asp:Button ID="btnCadDescricao" runat="server" Text="Cadastrar" OnClick="cadastrarTipo" />
                        </div>
                    </asp:View>
                    <asp:View ID="vwConteudo" runat="server">
                        <p>Conteúdo</p>
                        <a href="Cadastro.aspx">Cadastrar</a>
                    </asp:View>
                </asp:MultiView>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </section>
        </section>
    </form>
</body>
</html>
