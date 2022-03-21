<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tsHome.aspx.cs" Inherits="Site.Privado.tsHome" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="sortcut icon" type="image/png" href="..\img\privado\trevo.png" />
    <link rel="stylesheet" href="../Css/tsStyle.css" />
    <title>TuaSorte</title>
    <style>
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="tsNav">
            <section class="tsNavCorpo">
                <section class="tsNavLogo">
                    <img src="../Img/Privado/Tua Sorte.png" />
                </section>
                <section class="tsNavCentro">
                    <ul>
                        <li>Sorteios</li>
                        <li>Ganhadores</li>
                    </ul>
                </section>
                <section class="tsNavLogin">
                    <div class="botaoLogin">
                        <asp:LinkButton ID="lbtLogin" runat="server" Text="Entrar" OnClick="relizarLogin"></asp:LinkButton>
                    </div>
                </section>
            </section>
        </nav>
        <main class="tsMan">
            <section class="secHomeCorpo">
                <section class="secHomeDestaque">
                    <img src="../Img/privado/img-1000-01.jpg" />
                </section>
                <section class="secHomeSorteio">
                    <img src="../Img/privado/img-01.jpg"/>
                    <div>
                        <asp:LinkButton ID="lbtAbrirPagSorteio" runat="server" Text="Mustang" OnClick="acessarPagSorteio"></asp:LinkButton>
                    </div>
                </section>
                <section class="secHomeSorteio"></section>
                <p>Cadastrar</p>
                <!--
                <input id="iIdentificaSorteio" runat="server" type="text" placeholder="Número do Sorteio" disabled="disabled"/>
                <asp:Button ID="btnIdentificaSorteio" runat="server" Text="Valida Sorteio" OnClick="validarSorteio" />-->
                <input id="iNumInicio" runat="server" type="text" placeholder="Número Inicial" required=""/>
                <input id="iQtdeNumeros" runat="server" type="text" placeholder="Quantidade" required=""/>
                <input id="iDescricao" runat="server" type="text" placeholder="Descricao" required=""/>
                <asp:Button ID="btnCadNumeros" runat="server" Text="Cadastrar Números" OnClick="cadastrarNumeros" />
                <asp:Label ID="lblNumSorteio" runat="server"></asp:Label>
            </section>

        </main>
        <footer class="tsFooter">
            <p>Rodapé</p>
        </footer>
    </form>
</body>
</html>
