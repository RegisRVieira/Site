<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Site.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Home ASU</title>
    <link  rel="stylesheet" href="Css/Global.css"/>
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <link rel="stylesheet" href="Css/SlideShow.css" />
    <link rel="stylesheet" href="Css/Botoes.css" />
    <script src="Js/jQuery 3.4.1.js"></script>
    <script type="text/javascript" src="Js/Apoio.js"></script>
</head>
<body>
    <main>
        <header>
        </header>
        <nav class="navHome">
            <section class="navHome-Container">
                <section class="navHome-Box" >
                    <p><a href="ContNossaEntidade.aspx?vNE=1">Junte-se a Nós</a></p>
                </section>
                <section class="navHome-Box2" >
                    <p><a href="https://www.facebook.com/ASUBotucatu"><img style="width: 25px;" src="../Img/Icon/Icon-Face-Site.png" /></a></p>
                </section>
                <section class="navHome-Box3" >
                    <p><a href="https://www.google.com.br/maps/place/ASU+Sub-sede+Aposentados+Unesp/@-22.8706326,-48.4498872,19z/data=!4m5!3m4!1s0x94c7213a0b41cb85:0xa5f0199ffa5db304!8m2!3d-22.8706746!4d-48.449568"><img src="../Img/Icon/map.svg" /></a></p>
                </section>
                <section class="navHome-Box4">                    
                        <div>
                            <img src="../Img/Icon/usuLogin.svg"/>
                            <a href="LoginVoceOnLine.aspx"><p><asp:Label ID="lblLogin" runat="server" >Você OnLine</asp:Label></p></a>
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
                        <li><a href="ContNossaEntidade.aspx?vNE=1">Nossa Entidade</a></li>
                        <li><a href="ContNossaEntidade.aspx?vNE=2">Estatuto Social</a></li>
                        <li><a href="FaleConosco.aspx">Fale conosco</a></li>
                        <li><a class="voceonlinejs" href="LoginVoceOnLine.aspx">Consultar Seu Saldo</a></li>
                        <li><a id="voceonlinejs" class="voceonlinejs" href="LoginVoceOnLine.aspx">Você On Line</a></li>
                        <li><a id="treeline-closeicon" class="treeline-closeicon" onclick="closeMenuHome()">&cross;</a></li>
                        <li id="treeline-icon" class="treeline-icon" onclick="openMenuHome()">&#9776;</li>                        
                    </ul>
                </div>
            </section>
        </nav>
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
                        </section>                        
                    </div>
                </div>
            </section>
            <section class="BoxMaterias">
                <div class="BoxMaterias-Interno">
                    <div class="tituloBox-2">Notícias</div>                    
                    <%# montarMateriasHome() %>
                    <div class="HomeMateria">
                        <div class="HomeMateria-Guia">
                            <div class="HomeMateria-Guia-Tit">
                                <p>Guia de Convênios</p>
                            </div>
                            <div class="HomeMateria-Guia-Img">
                                <a href="MontaGuia.aspx">
                                    <img style="height: 100%; border-top-left-radius: 16px; border-top-right-radius: 16px;" src="../../Img/Box Guia Amando 2.jpg" />
                                </a>
                            </div>                            
                        </div>
                    </div>
                </div>
            </section>
            <section class="BoxServicos">
                <div class="BoxS-Interno">                    
                    <%# montarBoxServicos() %>
                    <div class="BoxS-Conteudo" >
                        <div class="BoxS-Conteudo-Tit">
                            <p>Convênios</p>
                        </div>
                        <div class="BoxS-Img">
                            <img src="Img/Convenios.jpg" /></div>                        
                        <div class="botaoSaibaMais2">
                            <a href="ContConv.aspx">Saiba Mais</a>
                        </div>
                    </div>
                    <div class="BoxS-Conteudo">
                        <div class="BoxS-Conteudo-Tit">
                            <p>Acessoria Jurídica</p>
                        </div>
                        <div class="BoxS-Img">
                            <img src="Img/Juridico.jpg" /></div>
                        <div class="botaoSaibaMais2">
                            <a href="ContAssoc.aspx?pAssoc=68">Saiba Mais</a>
                        </div>
                    </div>
                </div>
            </section>
            <section class="BoxGaleria33" >
                
                <%# montarBox33() %>
                
                <section class="s2">
                    <section class="Box33">
                        <img class="BoxS2" src="Img/Vital Brasil.jpg" />
                        <img class="BoxS2" src="Img/Posto real.jpg" />
                        <div class="s-titulo">
                            <p>Promoção Tanque Cheio</p>
                        </div>
                        <div id="" class="botaoSaibaMais">
                            <a href="ContMaterias.aspx?IDContMat=70">Saiba Mais</a>
                        </div>
                    </section>
                </section>                
                <section class="s3">
                    <section class="Box33">
                        <img class="BoxS3" src="Img/Festa.jpg " />
                        <img class="BoxS3" src="Img/Crianças.jpg " />
                        <img class="BoxS3" src="Img/Pesca 2.jpg " />
                        <img class="BoxS3" src="Img/Truco.jpg " />
                        <img class="BoxS3" src="Img/Festa 2.jpg " />                        
                        <div class="s-titulo">
                            <p>Próximos Eventos</p>
                        </div>
                        <div id="" class="botaoSaibaMais">
                            <a href="ContASU.aspx">Saiba Mais</a>
                        </div>
                    </section>
                </section>
                
            </section>            
            <section class="Boxgaleria100">
                <p class="tituloBox-1">Nossos Momentos</p>
                <div class="BoxImg100">
                    <img class="Box100" src="../../Img/festa 2012 091.jpg" />
                    <img class="Box100" src="../../Img/Box100.jpg" />
                    <img class="Box100" src="../../Img/Box100-1.jpg" />
                    <img class="Box100" src="../../Img/Box100-2.jpg" />
                    <img class="Box100" src="../../Img/Box100-3.jpg" />
                </div>
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
            <div class="footerHome-Img"><img src="../Img/Icon/Logo2 Régis-ASU.png" /></div>            
        </footer>
    </main>
</body>
</html>