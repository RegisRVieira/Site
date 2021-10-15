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
            min-width: 192px;
            height: 60px;
            background-color: #ff0000;
            background-color: #65368a;
            border: 1px solid #f26907;
            border-radius: 6px;
            display: inline-block;
            float: left;
        }
        .btnPrincipal a{
            text-decoration: none;
        }
                
        .btnPrincipal a p{
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
    </style>
</head>
<body>
    <form id="form1" runat="server">        
        <section class="secPrincipal">
            <section class="areaLogin">            
            <asp:Label ID="lblLogado" runat="server"></asp:Label>
        </section>
            <p>Minhas Coisas</p>
            <div class="btnPrincipal">
                <a ID="lbtAdm" runat="server" href="Adm.aspx"><p>Adm</p></a>
            </div>
            <div class="btnPrincipal">
                <a ID="lbtCadastro" runat="server" href="Cadastro.aspx"><p>Cadastro</p></a>
            </div>
            <div class="btnPrincipal">
                <a ID="lbtListas" runat="server" href="Listas.aspx"><p>Conteúdo</p></a>
            </div>
        </section>
    </form>
</body>
</html>
