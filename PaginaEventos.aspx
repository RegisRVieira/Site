<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaEventos.aspx.cs" Inherits="Site.PaginaEventos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Css/StylePagEventos.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Yellowtail&display=swap" rel="stylesheet">
    <title>Página de Eventos</title>
</head>
<body>
    <form id="form1" runat="server">
        <section class="corpoEventos">
            <section class="headerEvent">
                <style>
                    .centralizaTag{
                        width: 800px; min-height: 10px; margin: 0 auto;
                    }
                </style>
                <div class="centralizaTag">
                    <div class="headerEventImg">
                        <img src="Img/Logo ASU-White-Espaçado.png" />
                    </div>
                    <div class="headerEventText">                                                
                        <span style="font-family: 'Yellowtail', cursive; font-size: 3.5em;">Eventos</span>
                    </div>
                </div>
            </section>
            <section class="listEvents">
                <%# listaEventos() %>
            </section>
            <section class="">
                <%# mostrarPublicacao() %>                
            </section>
        </section>
    </form>
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
