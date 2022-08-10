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
                <input id="iCpf" runat="server" type="text" placeholder="CPF ou CNPJ" onkeypress="return event.charCode >= 48 && event.charCode <= 57"/>
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
                .dv_termo{
                    
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
            </style>            
            <div id="termo" class="dv_termo" runat="server">
                <div class="termo" >
                    <h1>Termo de Privacidade Você OnLine</h1>                    
                    
                    <p>Este regamento termo dita o uso do Portal bem como os serviços/produtos oferecidos e fornecidos pela Associação dos Servidores da Unesp, pessoa jurídica com sede no Campus da Unesp Botucatu, s/nº. – Estado de São Paulo, cep ******* inscrita no CNPJ sob o nº   *****************,  aos usuários de Internet.

                        Ao acessar pelo Portal,  você implica na aceitação integral e plena deste Termo e Política de Privacidade. Deste modo, é importante que você o leia com atenção.

                        Ao navegar através  do Portal também se submete aos demais, avisos, termos, regulamentos de uso e instruções disponibilizados ao usuário e as futuras atualizações.
                        1. Condições de acesso e utilização do portal
                        Você poderá utilizar o portal por qualquer veículo de comunicação, como celular, computador ou tablet, através de cadastro e login quando exigidos ou livremente quando assim se permitir.  Também, se for o caso,  possibilitará o acesso a informações e serviços de forma integrada e interativa por outros caminhos, como Google ou Facebook. 

                        Sempre é bom lembrar que, independentemente, do caminho ou modo escolhido, o login e senha de acesso são pessoais, intransferíveis  e de sua responsabilidade. Por isso, é importante que você os mantenha sob sigilo.


                        O mau uso do Portal, infringindo leis, disposições deste Termo de Uso ou da Política de Privacidade, a ASU poderá suspender ou mesmo interromper os serviços prestados

                        Fica vedado a reprodução, cópia, distribuição, permissão de  acesso público e, ainda,  transformar e/ou modificar o seu  conteúdo, a menos que possua a prévia autorização. 
                        O simples fato do uso do Portal ou dos serviços não transfere a  propriedade intelectual sob o conteúdo acessado.
                        Por ser um espaço amplo e plural, terceiros que dele se utilizavam para qualquer divulgação, são totalmente responsáveis pelo seu conteúdo e por eventuais danos causados, restringido a ASU o direito de suspender a sua veiculação se entender a prática de qualquer ilícito ou contrariedade ao estatuto da associação, independentemente de notificação ou aviso. 
                        O Portal da ASU  permite  interação com as diversas redes sociais, podendo compartilhar conteúdos e informações de seu interesse. Ao acessar seus links, é bom observar os  Termos de Uso e Política de Privacidade do site. 
                        Também é importante observar que criança ou adolescente  deve sempre  ter permissão e ser supervisionada pelos pais ou responsáveis


                        2. Das responsabilidades
                        A ASU prima em disponibilizar produtos de qualidade e fornecer serviços que satisfaça a necessidade de seus associados pelo PORTAL.  Quaisquer interrupções, em razão de ocorrências técnicas, operacionais ou imprevisíveis em razão da natureza do serviço oferecido, serão solucionadas de maneira célere e efetiva possível. Caso tenha qualquer problema na utilização de nossos serviços, não deixe de contatar nossa central de relacionamento.

                        O Portal possui ícones denominados “links de acesso”, tais como banners, botões, diretórios e ferramentas de busca que facilitam o acesso, direcionando ao destino desejado, cuja responsabilidade é de seu titular.
                        Da mesma forma, havendo espaço para comentários do usuário, a opinião será de responsabilidade de qual a publicou, não havendo qualquer relação, revisão, cerceamento  e fiscalização pela ASU. Caso você identifique algum conteúdo ofensivo disponibilizado no Portal ou nos Serviços, comunique a ASU, por meio do endereço eletrônico ***@***.

                        3. Segurança
                        A ASU preza pela segurança, confidencialidade e inviolabilidade de todos os dados cadastrais fornecidos por você. No entanto, você deve estar ciente que as medidas de proteção não são infalíveis. Deste modo,  a ASU não se responsabiliza por danos e/ou prejuízos decorrentes de caso fortuito ou força maior.
                    </p>
                </div>
                <div class="aprova" >                    
                    <asp:Button ID="btnDesativaTermo" runat="server" Text="Aceitar (C#)" OnClick="desativaTermo" />
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
