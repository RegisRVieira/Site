<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LayOut-Home.aspx.cs" Inherits="Site.LayOuts.LayOut_Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Layout Home</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/SlideShow.css" />
    <script src="../Js/jQuery 3.4.1.js"></script>
    <script type="text/javascript" src="../Js/Apoio.js"></script>
</head>
<body>
    <header>
        <nav class="navHome">
            <section class="navHome-Container">
                <section class="navHome-Box">
                    <p><a href="#">Junte-se a Nós</a></p>
                </section>
                <section class="navHome-Box2">
                    <p><a href="#">
                        <img style="width: 25px;" src="../Img/Icon/Icon-Face-Site.png" /></a></p>
                </section>
                <section class="navHome-Box3">
                    <p><a href="#">
                        <img src="../Img/Icon/map.svg" /></a></p>
                </section>
                <section class="navHome-Box4">
                    <div>
                        <img src="../Img/Icon/usuLogin.svg" />
                        <a href="#">
                            <p>
                                <asp:Label ID="lblLogin" runat="server">Você OnLine</asp:Label></p>
                        </a>
                    </div>
                </section>
            </section>
            <section id="logohome" class="logohome">
                <a href="../Home.aspx">
                    <img src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </section>
            <section class="navmenu">
                <div class="navbarra">&vert;</div>
                <div id="navlista" class="navlista">
                    <ul>
                        <li><a href="NossaEntidade.aspx?vNE=1">Nossa Entidade</a></li>
                        <li><a href="NossaEntidade.aspx?vNE=2">Estatuto Social</a></li>
                        <li><a href="FaleConosco.aspx">Fale conosco</a></li>
                        <li><a class="voceonlinejs" href="#">Consultar Seu Saldo</a></li>
                        <li><a id="voceonlinejs" class="voceonlinejs" href="#">Você On Line</a></li>
                        <li><a id="treeline-closeicon" class="treeline-closeicon" onclick="closeMenuHome()">&cross;</a></li>
                        <li id="treeline-icon" class="treeline-icon" style="" onclick="openMenuHome()">&#9776;</li>
                    </ul>
                </div>
            </section>
        </nav>
    </header>
    <main class="corpoHome">
        <section class="BoxSlider">
            <div class="BoxSlider-Interno">
                <div class="BoxSlider-Propaganda">
                    <section class="slider">
                        <img class="STop" src="../Img/Painel-Sede.png" />
                        <img class="STop" src="../Img/Painel-Clube.jpg" />
                    </section>
                </div>
                <div class="BoxSlider-VoceOnLine">
                    <p>Você On Line</p>
                    <section class="BoxVOImg">
                        <a href="LoginVoceOnLine.aspx">
                            <img class="bannerSlider" src="../img/Seu Saldo.jpg" /></a>
                        <a href="LoginVoceOnLine.aspx">
                            <img class="bannerSlider" src="../img/Sua Venda.jpg" /></a>
                        <a href="LoginVoceOnLine.aspx">
                            <img class="bannerSlider" src="../img/Seu Extrato.jpg" /></a>
                        <div class="bullets" style="width: 21.5%; height: auto; text-align: center; position: relative; top: 90%;">
                        </div>
                        <!-- configurando os botões do slider -->
                    </section>
                    <!--Slider-->
                </div>
            </div>
        </section>
        <section class="BoxMaterias">
             <div class="BoxMaterias-Interno">
                    <div class="tituloBox-2">Notícias</div>
                    <!-- <h1>Noticias</h1> Colocar um título para cada seção -->                    
                    <div class="HomeMateria">
                        <img src="../Img/Foto - DAP.jpg" />
                        <h1 class="HomeMateriaTitulo">Fundação Abrinq </h1>
                        <p class="HomeMateriaCategoria">Categoria: Unesp</p>
                        <p class="HomeMateriaTexto">A Fundação Abrinq </p>
                        <p class="HomeMateriaData">12 de Março de 2021</p>
                        <div class="HomeMateriaMais"><a href="#">
                            <p>Leia Mais...</p>
                        </a></div>
                    </div>
                    <div class="HomeMateria">
                        <img src="../Img/Natal2.png" />
                        <h1 class="HomeMateriaTitulo">Fundação Abrinq direciona mais de 3,5 milhões a projetos de organizações da sociedade civil </h1>
                        <p class="HomeMateriaCategoria">Categoria: Unesp</p>
                        <p class="HomeMateriaTexto">A Fundação Abrinq acaba de abrir edital com informações completas para organizações sociais inscreverem…</p>
                        <p class="HomeMateriaData">12 de Março de 2021</p>
                        <div class="HomeMateriaMais"><a href="#">
                            <p>Leia Mais...</p>
                        </a></div>
                    </div>
                    
                    <!-- < %# montarMateriasHome() %>-->
                    <div class="HomeMateria">
                        <div class="HomeMateria-Guia">
                            <div class="HomeMateria-Guia-Tit">
                                <p>Guia de Convênios</p>
                            </div>
                            <div style="width: 100%; height: 85%;">
                                <a href="MontaGuia.aspx">
                                    <img style="height: 100%; border-top-left-radius: 16px; border-top-right-radius: 16px;" src="../../Img/Box Guia Amando 2.jpg" />
                                </a>
                            </div>
                            <!--
                            <div style="width: 100%; height: 10%;"><p style="margin: 0; padding: 5px 0 5px; font-size: 2em; text-align: center;">Guia de Convênios</p></div>
                            <div style="width: 100%; height: 60%; border-top: 1px solid #ddd"><a href="LoginVoceOnLine.aspx"><img style="height: 100%;" src="../../Img/Icon-Guia Lojista.jpg" /></a></div>
                            <div style="width: 100%; height: 25%; border-top: 1px solid #ddd; border-bottom: 1px solid #ddd;"><img style="height: 100%;" src="../../Img/Icon-Guia Saude.jpg" /></div>
                            -->
                        </div>
                    </div>
                </div>
        </section>
        <section class="BoxServicos">
        </section>
        <section class="BoxGaleria33" style="">
        </section>
        <section class="Boxgaleria100">
        </section>
        <section id="margemRodape"></section>
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
