<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContMaterias.aspx.cs" Inherits="Site.ContMaterias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Matérias</title>
     <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js"></script>
    <script src="Js/jQuery 3.4.1.js"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p><a href="Home.aspx">
            <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a></p>
    </nav>
    <main class="corpoContMateria">        
        <div class="montaMateria">            
            <div><%# montarContMateria() %></div>
        </div>        
        <div class="public-lateral">
            <%# montarListaMaterias() %>
            <!--
                <section>
                <a href="FaleConosco.aspx">
                <img  src="../../Img/Av Major Matheus 2.JPG" />
                <p class="pl-Titulo">Titulo da Matéria</p>
                <p class="pl-Data">08-04-2021 08:47</p>
                </a>
            </section>
                -->
            <section style="width: 100%; min-height: 140px; ">
                <a style="text-decoration: none;" href="FaleConosco.aspx">
                <img style="width: 100%" src="../../Img/Box33-2.jpg" />
                <p style="margin: 0; padding:0; padding-left: 5px;">Titulo da Matéria</p>
                <p style="margin: 0; padding:0; padding-left: 15px; padding-bottom: 10px; font-size: .7em;">08-04-2021 08:47</p>
                </a>
            </section>
            <section style="width: 100%; min-height: 140px; ">
                <a style="text-decoration: none;" href="FaleConosco.aspx">
                <img style="width: 100%" src="../../Img/Festa.jpg" />
                <p style="margin: 0; padding:0; padding-left: 5px;">Titulo da Matéria</p>
                <p style="margin: 0; padding:0; padding-left: 15px; padding-bottom: 10px; font-size: .7em;">08-04-2021 08:47</p>
                </a>
            </section>  
             <section style="width: 100%; min-height: 140px; ">
                <a style="text-decoration: none;" href="FaleConosco.aspx">
                <img style="width: 100%" src="../../Img/Pesca.jpg" />
                <p style="margin: 0; padding:0; padding-left: 5px;">Titulo da Matéria</p>
                <p style="margin: 0; padding:0; padding-left: 15px; padding-bottom: 10px; font-size: .7em;">08-04-2021 08:47</p>
                </a>
            </section> 
             <section style="width: 100%; min-height: 140px; ">
                <a style="text-decoration: none;" href="FaleConosco.aspx">
                <img style="width: 100%" src="../../Img/Crianças 2.jpg" />
                <p style="margin: 0; padding:0; padding-left: 5px;">Titulo da Matéria</p>
                <p style="margin: 0; padding:0; padding-left: 15px; padding-bottom: 10px; font-size: .7em;">08-04-2021 08:47</p>
                </a>
            </section> 
            <section id="margemRodape"></section>
        </div>
    </main>
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
