<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginVoceOnLine.aspx.cs" Inherits="Site.LoginVoceOnLine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Você OnLine</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js" type="text/javascript"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p>
            <a href="Home.aspx">
                <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
        </p>
    </nav>
    <main>
        <form id="form1" runat="server">
            <div class="BoxLogin">
                <h1>Você OnLine</h1>
                <input id="iCpf" runat="server" type="text" placeholder="Nº Cartão / ID. convênio" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
                <script>iSenha.focus();</script>
                <input id="iSenha" runat="server" type="password" placeholder="Senha" />
                <asp:Button ID="btnLogin" runat="server" Text="Acessar" OnClick="LogarVoceOnLine" />                
            </div>
            <div style="width: 600px; margin: 0 auto;">
                <asp:Label ID="lblResult" runat="server" CssClass="lblMsg"></asp:Label>
            </div>            
            <style>
                .ativa{
                    display: block;
                }
                .desativa{
                    display: none;                    
                }
                .dv_termo_login{
                    
                    position: fixed;                    
                    top: 60px;
                    left: 50%;
                    width:50%;
                    height: 300px;
                    margin-left: -25%;
                    border-radius: 8px;
                    
                    /*background-color: rgba(247, 67, 5, 0.5);*/
                    background-color: white;
                }
                .termo{
                    width: 100%;
                    height: 88%;
                    overflow-y: scroll;
                    opacity: 0.5;
                    cursor: pointer;
                }
                .aprova{
                    margin: 0 auto;
                    width: 50%;
                    height: 10%;                    
                }
                @media(max-width: 1000px){
                    .dv_termo_login{                                            
                        top: 120px;
                        left: 3%;
                        width: 96%;
                        height: 450px;
                        margin-left: 0;                              
                    }
                    .termo{
                        height: 70%;                        
                    }
                    .aprova{                        
                        width: 50%;                                                
                    }
                    .aprova input[type=submit]{                        
                        font-size: 2.5em;
                    }
                    
                }
            </style>            
            <div id="termo" class="dv_termo_login" runat="server">
                <div class="termo" >
                    <h1>Termo de Privacidade Você OnLine</h1>                    
                    
                    <p> A ASU atende à Lei nº 13.709/18 – Lei Geral de Proteção de Dados Pessoais (LGPD), 
                        que dispõe sobre o tratamento de dados pessoais com o objetivo de proteger os direitos fundamentais de liberdade e 
                        de privacidade e o livre desenvolvimento da personalidade da pessoa natural. Na página www.asu.com.br está disponível 
                        a descrição do tratamento que a ASU dará a seus dados pessoais, bem como o meio disponível para que os titulares exerçam seus direitos elencados no artigo 18 da LGPD.
                    </p>
                </div>
                <div class="aprova" >                    
                    <asp:Button ID="btnLiberaAcessoVO" runat="server" Text="Aceitar" OnClick="LiberarAcessoVO" />
                </div>
            </div>    
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </form>
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
