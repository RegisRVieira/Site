<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContMaterias_Todas.aspx.cs" Inherits="Site.ContMaterias_Todas1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Todas</title>
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js"></script>
    <script src="Js/jQuery 3.4.1.js"></script>
</head>
<body>
<nav class="navHome-Internas">
        <p><a href="http://www.asu.com.br/Home.aspx">
            <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a></p>
    </nav>    
    <div style="background-image: linear-gradient(to right, #83d12d, #bcae00, #e08300, #f2512f, #eb125e); width: 100%; height: 5px;"></div>
    <main style="width: 100%; min-height: 600px;">
        <div style="">
            <p style="text-align: center; font-size: 30px; padding: 0; padding-top: 20px; text-decoration: underline">Veja Todas as Notícias</p>
        </div>
        <%# montarListaGeral() %>           
    </main>   
    <section id="margemRodape"></section>
    <footer class="footerHome">
        <div class="footerHome-Dados">
            <small>&reg; 1969 -
                <script type="text/javascript">document.write(agora.getFullYear() + ". Todos os direitos reservados")</script>
            </small>
            <address>
                <script type="text/javascript">document.write(ano + " Anos")</script>
            </address>
        </div>
        <div class="footerHome-Img">
            <img src="../Img/Icon/Logo2 Régis-ASU.png" /></div>
    </footer>
</body>
</html>
