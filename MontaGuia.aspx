<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MontaGuia.aspx.cs" Inherits="Site.MontaGuia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Buscar Convênios</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Form-Fluido.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />    
    <script src="Js/Apoio.js"></script>
    <script src="Js/jQuery 3.4.1.js"></script>
</head>
<body>
   <nav class="navHome-Internas">
        <p><a href="Home.aspx">
            <img src="Img/Logo ASU-White-Espaçado.png" /></a></p>
    </nav>
    <section class="buscaConv" >
        <form class="formGuia" id="form1" runat="server">
            <input id="iBuscar" runat="server" type="text" placeholder="Digite o que você procura..." />                     
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="buscarConvenios" />            
        </form>        
    </section>
    <div style="">        
        <asp:Label ID="lblResult" runat="server" ></asp:Label>
        <asp:Label ID="lblMsgErro" runat="server"></asp:Label>        
    </div>
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
