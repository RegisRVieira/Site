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
        <p><a href="http://www.asu.com.br/Home.aspx">
            <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a></p>
    </nav>
    <main class="corpoContMateria">        
        <div class="montaMateria">            
            <div><asp:Label ID="lblMateria" runat="server"> <%# montarContMateria() %></asp:Label></div>
            <div class="left" style="margin-top: 7px;  margin-left: 5px;">                                            
                <a href="#" onclick="PrintElem('#lblMateria', 1080, 'Extrato do Associado')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>&nbsp    <br />                                            
            </div>
        </div>        
        <div class="public-lateral">
            <%# montarListaMaterias() %>
            
            <a href="ContMaterias_Todas.aspx" style="">
            <div style="width: 100%; height: 50px; border: solid 1px #ffd800; margin-top: 20px;">
                <p style="text-align: center; width: 100%; margin:0; padding: 0; margin-top: 8px; padding-top: 9px;">
                Veja todas as Matérias...
                </p>
            </div></a>
            <!--
                <section>
                <a href="FaleConosco.aspx">
                <img  src="../../Img/Av Major Matheus 2.JPG" />
                <p class="pl-Titulo">Titulo da Matéria</p>
                <p class="pl-Data">08-04-2021 08:47</p>
                </a>
            </section>
                
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
                -->
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
    <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-83720917-1', 'auto');
        ga('send', 'pageview');
    </script>
</body>
</html>
