<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MontaGuia.aspx.cs" Inherits="Site.MontaGuia" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Guia de Convênios</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Form-Fluido.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />    
    <script src="Js/Apoio.js"></script>
    <script src="Js/jQuery 3.4.1.js"></script>     
    <style>
        html{
            background-color: #f2f6ff;            
        }
    </style>
</head>
<body>
   <nav class="navHome-Internas">
        <p><a href="Home.aspx">
            <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a></p>
    </nav>
    <div style="background-image: linear-gradient(to right, #83d12d, #bcae00, #e08300, #f2512f, #eb125e); width: 100%; height: 5px;">

    </div>
    <form class="formGuia" id="form1" runat="server">
        <section>        
            <div class="titGuia">
                <p>Este é o seu Guia de Convênios, digite o que você procura e vamos te ajudar a encontrar.</p>
            </div>            
                <input id="iBuscar" runat="server"  type="text" placeholder="Digite o que você procura..." />                                             
            <div class="botaoBuscar">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="buscarConvenios" />       
            </div>                        
            <asp:Label ID="lblTeste" runat="server"></asp:Label>               
        </section>    
        <section style="width: 100%; min-height: 210px;">
            <div style="margin:0 auto; width: 500px; margin-top: 100px; height: 40px; text-align: center;">
                <h1>Ou, veja a listagem Completa:</h1>                
            </div>
            <div style="margin:0 auto; width: 400px; height: 20px; text-align: center;">                 
                <style>
                    .btnImage{
                        width: 175px;
                        border: solid 1px #FFF;
                    }
                </style>
                <div style="width: 49%; float: left;">
                    <asp:ImageButton ID="ibtnComercio" runat="server" ImageUrl="~/Img/Icon/Botões - Comércio.png" CssClass="btnImage" OnClick="executarConvenios"/>
                </div>
                <div style="width: 49%; float: left;">
                    <asp:ImageButton ID="ibtnSaude" runat="server" ImageUrl="~/Img/Icon/Botões - Saúde.png" CssClass="btnImage" OnClick="executarSaude"/>
                </div>
            </div>
        </section>
    </form>
    <section>        
        <script>
            $(function () {
                var curSlider = 0;
                var maxSlider = $('.Slider').length - 1;
                var delay = 1;
                initSlider();
                changeSlide();

                function initSlider() {
                    $('.Slider').hide(); //apaga todos os sliders
                    $('.Slider').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0
                    //percorre a qtde de slider para adicionar o botão de navegação do slider
                }
                function changeSlide() {
                    setInterval(function () {
                        $('.Slider').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
                        curSlider++;
                        if (curSlider > maxSlider)
                            curSlider = 0;
                        $('.Slider').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
                    }, delay * 10000);
                }
            })

        </script>
        <asp:Label ID="lblResult" runat="server" ></asp:Label>
        <asp:Label ID="lblMsgErro" runat="server"></asp:Label>        
    </section>
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
