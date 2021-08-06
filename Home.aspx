﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Site.Home" %>

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
    <script src="https://static.zenvia.com/embed/js/zenvia-chat.min.js"></script><script>  var chat = new ZenviaChat('b0ec78cf08497aecd5aeffd29b7bf5f9').embedded('button').build();</script>
    <main>
        <header>
        </header>
        <nav class="navHome">
            <section class="navHome-Container">
                <section class="navHome-Box" >
                    <p><a href="ContNossaEntidade.aspx?vNE=1">Junte-se a Nós</a></p>
                </section>
                <a href="https://www.facebook.com/ASUBotucatu" target="_blank">
                    <section class="navHome-Box2" >                     
                    </section>
                </a>
                <a href="https://www.google.com.br/maps/place/ASU+Sub-sede+Aposentados+Unesp/@-22.8706326,-48.4498872,19z/data=!4m5!3m4!1s0x94c7213a0b41cb85:0xa5f0199ffa5db304!8m2!3d-22.8706746!4d-48.449568" target="_blank">
                    <section class="navHome-Box3" >                    
                    </section>
                </a>
                <a href="https://wa.me/551431125125?text=Olá, preciso de ajuda!" target="_blank">
                    <section class="navHome-Box5" >
                    </section>
                </a>
                
                <section class="navHome-Box4">                    
                        <div>
                            <img src="../Img/Icon/usuLogin.svg"/>
                            
                            <div class="BoxVOLoginMenu">
                                <ul>
                                    <li><a href="VOnLine/Login.aspx"><p><asp:Label ID="lblUsuLogado" runat="server" CssClass="lblUsuLogado"></asp:Label></p></a> 
                                        <ul>                                                                                                                         
                                            <a href="VOnLine/Login.aspx" target="_blank"><li>Você OnLine</li></a>
                                            <a href="testPdf.aspx" target="_blank"><li>Teste PDF</li></a>
                                            <a href="LoginVoceOnLine.aspx">Você On Line - Para teste Local</a>
                                            <a href="Adm/LoginAdm.aspx"><li>Adm</li></a>
                                            <a href="Adm/InsertRegistro.aspx"> <li>Teste - Insert</li></a>
                                        </ul>
                                    </li>                            
                                </ul>
                            </div>
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
                            <img class="STop" src="../img/Festa Julina.jpg" />
                            <img class="STop" src="../Img/Painel-Sede.png" />
                            <img class="STop" src="../img/Seja Socio.jpg" />
                            <img class="STop" src="../Img/Painel-Clube.jpg" />
                        </section>
                    </div>
                    <div class="BoxSlider-VoceOnLine">
                        <p>Você On Line</p>
                        <section class="BoxVOImg">
                            <a href="VOnLine/Login.aspx">
                                <img class="bannerSlider" src="../img/Seu Saldo.jpg" /></a>
                            <a href="VOnLine/Login.aspx">
                                <img class="bannerSlider" src="../img/Sua Venda.jpg" /></a>
                            <a href="VOnLine/Login.aspx">
                                <img class="bannerSlider" src="../img/Seu Extrato.jpg" /></a>
                            <div class="bullets" style="width: 21.5%; height: auto; text-align: center; position: relative; top: 90%;">
                            </div>                            
                        </section>                        
                    </div>
                </div>
            </section>
            <style> /*Stilo Sessão Parceiros*/
                .BoxParceiros{
                    margin: 0 auto;
                    width: 1200px;
                    height: 250px;     
                    display: none;
                }
                .BoxParceiros p{
                    text-align: center;
                    color: #808080;
                    font-size: 1.5em;
                }
                .pubParceiro{
                    margin-top: 10px;
                    margin-left: 20px;
                    width: 31%;
                    height: 75%;
                    float: left;                 
                }
                .pubParceiro img{
                    width: 100%;
                    height: 100%;
                }
                .pubParceiro img:hover{
                    opacity: .8;
                }
            </style>
            <section class="BoxParceiros">
                <p>Parceiros</p>
                <div class="pubParceiro">
                    <img src="Img/Publicidade/Box Parceiros.jpg" />
                </div>
                <div class="pubParceiro">
                    <img src="Img/Publicidade/Box Parceiros.jpg" />
                </div>
                <div class="pubParceiro">
                    <img src="Img/Publicidade/Box Parceiros.jpg" />
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
                            <p>Assessoria Jurídica</p>
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
            
             <style> /*Stilo Sessão Depoimentos*/
                .BoxDepoimentos{
                    margin-top: 20px;
                    margin-bottom: 20px;
                    margin: 0 auto;
                    width: 1200px;
                    height: 400px;                    
                    color: #808080;                    
                    font-family: 'Open Sans', sans-serif;           
                    display: none;
                }
                .BoxDepoimentos p{                    
                    text-align: center;                                        
                }
                .BoxDepoimentos h3{
                    margin: 0;
                    font-size: 1.6em;
                    text-align: center;                    
                    margin-bottom: 25px;
                    color: #22396f;
                }
                .DepoimentoPrincipal{
                    float: left;                    
                    width: 35%;
                    height: 80%;
                    margin-left: 80px;
                    margin-right: 50px;                         
                }
                .DepoimentoPrincipal img{
                    margin: 0 auto;
                    border-radius: 180px;
                    width: 150px;
                    height: 150px;
                }
                .DepoimentoSecundario{
                    float: left;
                    /*background-color: gold;*/                     
                    width: 25.9%;
                    height: 80%;           
                    opacity: .5;
                }
                .DepoimentoSecundario img{
                    border-radius: 180px;
                    opacity: .5;
                    width: 120px;
                    height: 120px;
                }
                .divImg{
                    max-width: 150px;
                    max-height: 150px;
                    margin: 0 auto;                    
                }
                .divText{
                    margin: 0;
                    padding: 0;
                }
                .divText p{
                    
                }
                .divImg-s{
                    max-width: 120px;
                    max-height: 120px;
                    margin: 0 auto;                    
                }
                .aspas{
                    margin: 0;
                    padding: 0;
                    font-size: 1em;
                    color: #f26907;
                }
                .textoDepoimento{
                    margin: 0;
                    padding: 0;
                    color: #808080;
                    font-size: 1.2em;                    
                }
                .nomeDepoimento {
                    margin: 0;
                    padding: 0px;
                    margin-top: 10px;                    
                    color: #22396f;
                    font-size: 1.1em;                    
                    font-weight: 600;
                 }
                .unidadeDepoimento{
                    margin: 0;
                    padding: 0;
                    font-size: 1em;                                        
                    color: #808080;
                }

                .textoDepoimento-s{
                    margin: 0;
                    padding: 0;
                    color: #808080;
                    font-size: .9em;                    
                }
                .nomeDepoimento-s {
                    margin: 0;
                    padding: 0px;
                    margin-top: 10px;                    
                    color: #22396f;
                    font-size: .8em;                    
                    font-weight: 600;
                 }
                .unidadeDepoimento-s{
                    margin: 0;
                    padding: 0;
                    font-size: .7em;                                        
                    color: #808080;
                }

            </style>
            <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@300&display=swap" rel="stylesheet"> 
            <section class="BoxDepoimentos">
                <p>DEPOIMENTOS</p>
                <h3>O que Dizem</h3>
                <div class="DepoimentoSecundario">
                    <div class="divImg-s">
                        <img src="Img/Publicidade/Box Parceiros.jpg" />
                    </div>
                    <div class="divText">
                        <div style="margin: 0 auto; width: 100px; height: 50px; "><img style="width: 100px; height: 50px; margin: 0; padding:0;" src="img\icon\aspas.svg" /></div>                        
                        <p class="textoDepoimento-s">Sempre fui Associado da ASu, pois encontro tudo que preciso aqui.</p>
                        <p class="nomeDepoimento-s">Aparecida Conceição</p>
                        <p class="unidadeDepoimento-s">Famesp</p>
                    </div>
                </div>
                <div class="DepoimentoPrincipal">
                    <div class="divImg">
                        <img src="Img/Publicidade/Box Parceiros.jpg" />                        
                    </div>
                    <div class="divText">
                        <div style="margin: 0 auto; width: 100px; height: 50px; "><img style="width: 100px; height: 50px; margin: 0; padding:0;" src="img\icon\aspas.svg" /></div>                        
                        <p class="textoDepoimento">Sempre fui Associado da ASu, pois encontro tudo que preciso aqui.</p>
                        <p class="nomeDepoimento">Agrepino Simplório</p>
                        <p class="unidadeDepoimento">F.M.V.Z.</p>
                    </div>
                </div>
                <div class="DepoimentoSecundario">
                    <div class="divImg-s">
                        <img src="Img/Publicidade/Box Parceiros.jpg" />
                    </div>
                    <div class="divText">
                        <div style="margin: 0 auto; width: 100px; height: 50px; "><img style="width: 100px; height: 50px; margin: 0; padding:0;" src="img\icon\aspas.svg" /></div>                        
                        <p class="textoDepoimento-s">Sempre fui Associado da ASu, pois encontro tudo que preciso aqui.</p>
                        <p class="nomeDepoimento-s">João da Silva</p>
                        <p class="unidadeDepoimento-s">Medicina</p>
                    </div>
                </div>
            </section>

            <section class="Boxgaleria100">
                <p class="tituloBox-1">Nossos Momentos</p>
                <div class="BoxImg100">                    
                    <img class="Box100" src="../../Img/Box100.jpg" />
                    <img class="Box100" src="../../Img/Box100-Claudia.jpg" />
                    <img class="Box100" src="../../Img/festa 2012 091.jpg" />
                    <img class="Box100" src="../../Img/Box100-1.jpg" />
                    <img class="Box100" src="../../Img/Box100-2.jpg" />
                    <img class="Box100" src="../../Img/Box100-3.jpg" />
                </div>
            </section>   
           
           
            
            <section class="redeSocialHome">
                <a href="https://www.facebook.com/ASUBotucatu" target="_blank">
                    <section class="redeSocialHome-Box1" >                     
                    </section>
                </a>
                <a href="https://www.google.com.br/maps/place/ASU+Sub-sede+Aposentados+Unesp/@-22.8706326,-48.4498872,19z/data=!4m5!3m4!1s0x94c7213a0b41cb85:0xa5f0199ffa5db304!8m2!3d-22.8706746!4d-48.449568" target="_blank">
                    <section class="redeSocialHome-Box2" >                    
                    </section>
                </a>
                <a href="https://wa.me/551431125125?text=Olá, preciso de ajuda!" target="_blank">
                    <section class="redeSocialHome-Box3" >
                    </section>
                </a>
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