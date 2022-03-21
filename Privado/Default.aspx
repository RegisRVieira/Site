<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Privado.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pessoal</title>
    <style>
        .secPrincipal {
            margin: 0 auto;
            margin-top: 20px;
            width: 600px;
            min-height: 600px;
            /*background-color: #ffd800;*/
        }

            .secPrincipal p {
                text-align: center;
                padding: 0;
                font-size: 30px;
                color: #574040;
            }

        .btnPrincipal {
            margin-left: 5px;
            margin-bottom: 5px;
            min-width: 192px;
            height: 60px;
            background-color: #ff0000;
            background-color: #65368a;
            border: 1px solid #f26907;
            border-radius: 6px;
            display: inline-block;
            float: left;
        }

            .btnPrincipal a {
                text-decoration: none;
            }

                .btnPrincipal a p {
                    margin: 0;
                    padding: 0;
                    padding-top: 20px;
                    padding-bottom: 20px;
                    text-align: center;
                    color: white;
                    font-size: 18px;
                    font-family: 'Verdana';
                    font-family: 'Courier New';
                    text-decoration: none;
                }

        .secLogo {
            width: 100%;
            min-height: 60px;
            border: 1px solid #65368a;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="secPrincipal">
            <section class="areaLogin">
                <asp:Label ID="lblLogado" runat="server"></asp:Label>
            </section>
            <section class="secLogo">
                <div style="width: 60%; height: 60px; margin: 0 auto;">
                    <div style="width: 40px; height: 60px; display: inline-block; float: left;">
                        <img style="width: 100%;" src="../Img/Logo Régis-2.png" /></div>
                    <div style="width: 200px; height: 50px; display: inline-block; float: left;">
                        <p style="margin: 0; padding-top: 10px; display: inline-block; color: #f26907;">Tecnologia</p>
                    </div>
                </div>
            </section>
            <p>Listas</p>
            <div class="btnPrincipal">
                <a id="lbtAdm" runat="server" href="Adm.aspx">
                    <p>Adm</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtCadastro" runat="server" href="Cadastro.aspx">
                    <p>Cadastro</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtListas" runat="server" href="Listas.aspx">
                    <p>Conteúdo</p>
                </a>
            </div>
        </section>
        <section style="width: 600px; min-height: 50px; margin: 0 auto;">
            <div class="btnPrincipal">
                <a id="lblEventos" runat="server" href="../Eventos/eLogin.aspx">
                    <p>Eventos ASU</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtProjetos" runat="server" href="tsHome.aspx">
                    <p>Projetos</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtEstudos" runat="server" href="Estudos.aspx">
                    <p>Estudos</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtTestes" runat="server" href="eTestes.aspx">
                    <p>Testes</p>
                </a>
            </div>
            <div class="btnPrincipal">
                <a id="lbtTesteGravarHTML" runat="server" href="testesGravarHtml.aspx">
                    <p>Recuperação de Tags HTML</p>
                </a>
            </div>

        </section>
    </form>
</body>
</html>
