<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eFormulario.aspx.cs" Inherits="Site.Privado.eFormulario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Formulário Inteligente</title>
    <link rel="stylesheet" href="../Css/eStyle.css" />
    <script src="../Js/Script2.js" type="text/javascript"></script>
    <style>
        h1#titulo {
            margin: 0;
            padding: 0;
            margin-top: 20px;
            margin-bottom: 20px;
            font-size: 1.5em;
            color: #968980;
            text-align: center;
        }

        input[type=text], input[type=tel], input[type=email] {
            margin: 0;
            width: 400px;
            height: 30px;
            display: block;
            border: 1px solid #F0F0F0;
            border-radius: 4px;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        input[type=submit] {
            width: 100px;
            height: 25px;
            background-color: #f26907;
            color: white;
            border: 1px solid #3b0974;
        }

        div#divInt {
            margin: 0 auto;
            width: 90%;
            min-height: 50px;
        }
    </style>
    <script>
       


    </script>
    
</head>
<body>
    <form id="frmFormInteligente" runat="server" name="formInteligente" method="post">
        <div id="divInt">
            <h1 id="titulo">Formulário Inteligente</h1>
            <h2>Dados</h2>
            <input id="iNome" type="text" placeholder="Nome" />
            <input id="iTelefone" type="tel" placeholder="Telefone" />
            <input id="iEmail" type="email" placeholder="usuario@dominio" />
            <input id="btnEnviarDados" type="submit" value="Enviar" /><br />
            <br />
        </div>
        <div>
            <span style="display: block; margin-bottom: 20px; margin-left: 20px;">
                <input id="iEscolherCidade" name="EscolherCidade" type="checkbox" />
                <label for="escolherCidade">Escolher Cidade</label>
            </span>
        </div>
        <div id="iCidade">
            <input id="Botucatu" name="Botucatu" type="checkbox" value="1" />
            <label for="Botucatu">Botucatu</label>
            <input id="SaoManuel" name="SM" type="checkbox" value="2" />
            <label for="SaoManuel">São Manuel</label>
            <input id="Lencois" name="Lençóis" type="checkbox" value="3" />
            <label for="Lencois">Lençóis</label>
            <input id="Bauru" name="Bauru" type="checkbox" value="4" />
            <label for="Bauru">Bauru</label>
        </div>
        <div id="mensagem"></div>
    </form>
</body>
</html>
