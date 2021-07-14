<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContASU.aspx.cs" Inherits="Site.ContASU" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Eventos ASU</title>
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css"/>
    <link rel="stylesheet" href="Css/SlideShow.css" />
    <script src="Js/Apoio.js" type="text/javascript"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p>
            <a href="Home.aspx">
                <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
        </p>
    </nav>
    <main class="corpoContASU">
        <section class="secContCalendar">
            <h1>Calendário de Eventos</h1>
            <div class="calendar efeito-1">
                <p class="diaEvento">30-04-2021</p>
                <p class="txtEvento">Torneio de Truco</p>
            </div>
            <div class="calendar efeito-1">
                <p class="diaEvento">22-05-2021</p>
                <p class="txtEvento">Noite do Boteco</p>
            </div>
            <div class="calendar efeito-1">
                <p class="diaEvento">28-05-2021</p>
                <p class="txtEvento">Fique em Casa</p>
            </div>
            <div class="calendar efeito-1">
                <p class="diaEvento">22-10-2021</p>
                <p class="txtEvento">Dia das Crianças</p>
            </div>
            <div class="calendar efeito-1">
                <p class="diaEvento">17-12-2021</p>
                <p class="txtEvento">Festa de Final de Ano</p>
            </div>
            <div class="calendarHistorico">
                <a href="#">Histórico de Eventos</a>
            </div>
        </section>
        <section class="BoxContEvento">
            <div class="BoxContEvento-Info">
                <p>22-05-2021</p>
                <p>Espaço Daruma</p>
                <p>20:00</p>
            </div>
            <div class="BoxContEvento-Text" style="margin-bottom: 20px;">
                <p>Convidamos todos nossos associados, seus dependentes e convidados a participarem da nossa Noite do Boteco!</p>
                <p>Bebidas Open Bar</p>
                <p>Cardápio: Tradicional de Boteco</p>
            </div>            
            <section class="BoxContLocal">
                <div class="BoxContLocal-Img">
                    <img src="../../Img/Daruma.jpg" />
                </div>
                <div class="BoxContLocal-Img">
                    <!--<img src="../../Img/Mapa-Clube.jpg" />-->
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3674.2547596022328!2d-48.44683698444747!3d-22.9408429448559!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x94c6de8c104164bb%3A0x32ccd8cd68ddd330!2sSabor%20%26%20Mordomia%20Buffet%2FEspa%C3%A7o%20Daruma%20Eventos!5e0!3m2!1spt-BR!2sbr!4v1624576770337!5m2!1spt-BR!2sbr" allowfullscreen="" loading="lazy"></iframe>
                </div>
            </section>
        </section>
        <section class="BoxContGaleria">
            <div style="margin: 0 auto; width: 100%; height: 100%; ">
                <section class="slider-mini">
                    <section class="s-box-apresenta-mini">
                        <img src="../../Img/BoxApresentacao1.jpg" />
                    </section>
                    <section class="s-mini">
                        <div class="box-mov">&lsaquo;</div>
                        <div class="box1-mini box-mini">
                            <img src="../../Img/BoxMini1.jpg" />
                        </div>
                        <div class="box2-mini box-mini">
                            <img src="../../Img/BoxMini2.jpg" />
                        </div>
                        <div class="box3-mini box-mini">
                            <img src="../../Img/BoxMini3.jpg" />
                        </div>
                        <div class="box1-mini box-mini">
                            <img src="../../Img/BoxMini4.jpg" />
                        </div>
                        <div class="box2-mini box-mini">
                            <img src="../../Img/BoxMini5.jpg" />
                        </div>
                        <div class="box3-mini box-mini">
                            <img src="../../Img/BoxMini6.jpg" />
                        </div>
                        <div class="box4-mini box-mini">
                            <img src="../../Img/BoxMini7.jpg" />
                        </div>
                        <div class="box-mov2">&rsaquo;</div>
                    </section>
                </section>
            </div>
        </section>
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
                    <img src="../Img/Icon/Logo2 Régis-ASU.png" />
                </div>
        </footer>
</body>
</html>
