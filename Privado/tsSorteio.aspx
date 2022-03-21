<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tsSorteio.aspx.cs" Inherits="Site.Privado.tsSorteio" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="sortcut icon" type="image/png" href="..\img\privado\trevo.png" />
    <link rel="stylesheet" href="../Css/tsStyle.css" />
    <title>Sorteios</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="tsNav">
            <section class="tsNavCorpo">
                <section class="tsNavLogo">
                    <img src="../Img/Privado/Tua Sorte.png" />
                </section>
                <section class="tsNavCentro">
                    <ul>
                        <li>Sorteios</li>
                        <li>Ganhadores</li>
                    </ul>
                </section>
                <section class="tsNavLogin">
                    <div class="botaoLogin">
                        <asp:LinkButton ID="lbtLogin" runat="server" Text="Entrar"></asp:LinkButton>
                    </div>
                </section>
            </section>
        </nav>
        <main>
            <section class="secAprsenta">
                <section class="secImg">
                    <figure>
                        <img src='../img/privado/Img-01.jpg' title="Vamos ver essa bagaça!!!" />
                        <figcaption>Trevo de Quatro Folhas...</figcaption>
                    </figure>
                </section>
                <section class="secDesc">
                    <section class="secDescTit">
                        <p>Titulo da "Rifa"</p>
                    </section>
                    <section class="secDescText">
                        <p>
                            Mustang<br />
                            <br />

                            Upgrades

                            Aprox. R$510.000 mil investidos

                            Rodas aro 21 Forjadas com pneus michelin Pilot Sport 

                            - Kit Turbo Kings GT28
                            - Bicos ID1000
                            - Bombas AEM 340L
                            - Intake COBB 3"
                            - Blow-off
                            - EcuTek
                            - Donwpipe
                            - Mid Pipe
                            - Escapamento Completo Inox
                            - Aerofolio Carbono AP Racing
                            - Intercooler AMS
                            - Freio Dianteiro AP Racing
                            - SPL Titanium Camber
                            - Cooler HKS de Câmbio
                            - Farol 2017 
                            - Lanterna 2017 
                            - Envelopamento NTRCUSTOM
                            - Volante CB custom 
                            - Piscas branco 
                            - Revisado 
                            - JG Pastilhas novos 

                            Carro todo revisado e sem detalhes.

                            Veiculo para pessoas exigentes.

                            VALOR DO NÚMERO: R$60,00

                            Sorteio para todo Brasil!

                            -----------------------------------------------

                            O pagamento deve ser feito imediatamente após a reserva.

                            -(Os pagamentos realizados via Mercado Pago serão destinados para LUÍS HENRIQUE DE FARIA MARTINES)

                            Caso não consiga fazer o pagamento através do PIX AUTOMÁTICO na plataforma, envie o pagamento para o PIX abaixo e encaminhe o comprovante para o WhatsApp do número (19) 98458-7783

                            DADOS PARA PAGAMENTO:
                            Chave PIX CELULAR: (15) 98189-5366
                            LEONARDO PIRES FERREIRA
                            BANCO SAFRA S.A.


 

                            As reservas não pagas serão liberadas sem aviso prévio. 

                            NÃO ACEITAMOS DOC, agendamento de TED ou depósitos em ENVELOPE. SOMENTE PIX.

                            -----------------------------------------------

                            COMO PARTICIPAR DO SORTEIO:

                            1- Escolha 1 ou mais números no site

                            2- Clique no(s) número(s)

                            3- Clique em reservar número(s)

                            4- Preencher todos os dados solicitados CORRETAMENTE

                            5- Realize o pix automático no valor do(s) número(s) escolhido(s)

                            ------------------------------------------------

                            REGRAS DO SORTEIO:

                            1- Sorteio composto por 20.000 números (0000 ao 19999) no valor R$60,00 cada cota

                            2 - O número sorteado será extraído pelo site oficial da Loteria Federal.

                            O ganhador será o que acertar os 5 últimos números do primeiro prêmio da Loteria Federal do dia marcado para o sorteio.

                            Caso o resultado do primeiro prêmio seja SUPERIOR a milhar 19.999, utilizaremos o segundo prêmio, caso o segundo prêmio também seja superior a 19.999, utilizaremos o terceiro prêmio e assim sucessivamente. Esgotando todas as possibilidades do 1ª ao 5º prêmio o sorteio ocorrerá no concurso seguinte. 

                            3 - A data do sorteio está sujeita a mudança caso todos os números não tenham sido vendidos até a data PREVISTA. O sorteio só ocorre mediante a todas as cotas pagas. 

                            A data do sorteio e o ganhador serão divulgados em nossos grupos WhatsApp e no perfil do Instagram. 

                            4 - O veiculo será entregue no estado de conservação em que se encontra (vide fotos) com documentação em dia. 

                            5 - O veículo será entregue com DUT reconhecido ao ganhador por autenticidade. 

                            6 - O Veículo se encontra na cidade de Sorocaba/SP e qualquer custo de envio para outra cidade será de nossa responsabilidade.
                        </p>
                    </section>
                    <section class="secDescRedes">
                        <div>F</div>
                        <div>T</div>
                        <div>W</div>
                    </section>
                </section>
            </section>
            <section>
                <!-- < %# executarHint() %>-->                
                <ul>
                    <li class="tooltip">
                        <label id="iNum1">00010</label>
                    </li>
                    <span class="tooltip">
                        <label id="iNum2">0002</label>
                    </span>
                    <span id="iNum3" class="tooltip">
                        <label>0003</label>
                    </span>
                    <li id="iNum4" class="tooltip">
                        <label>0004</label>
                    </li>
                </ul>
            </section>
            <a href="eTestes.aspx">Testes</a>
            <section>
                <input id="btnSelecionar" type="submit" value="Selecionar" />
            </section>
            <section id="secNumEscolhidos" style="width: 500px; height: 500px; background-color: #F0F0F0; margin: 0 auto; margin-bottom: 20px;">
                <label id="recebeNumeros"></label>
            </section>
            <script>
                btnSelecionar.addEventListener("click", function (e) {
                    e.preventDefault();

                    //alert(" * ");

                    var sec = document.querySelector("#secNumEscolhidos");

                    sec.setAttribute("backgroundColor", "#22396f");
                    //sec.setAttribute("background-color", "#f26907");
                    //secNumEscolhidos.setAttribute("width", "800px");                                                           
                    secNumEscolhidos.style.width = "800px";
                    secNumEscolhidos.style.backgroundColor = "#f26907";

                    if (sec.getAttribute("backgroundColor") == "#f26907") {
                        alert("#22396f");
                    } else {
                        alert("Diferente");
                        secNumEscolhidos.style.backgroundColor = "#22693f";
                    }

                    console.log(sec.getAttribute("backgroundColor"))
                });
                            /*
    function changebackground() {
        var colors = ["#0099cc", "#c0c0c0", "#587b2e",
            "#990000", "#000000", "#1C8200", "#987baa", "#464646",
            "#AA8971", "#1987FC", "#99081E"];

        setInterval(function () {
            var bodybgarrayno = Math.floor(Math.random() * colors.length);
            var selectedcolor = colors[bodybgarrayno];
            secNumEscolhidos.style.background = selectedcolor;
        }, 3000);
        console.log(colors.length);
    }
    changebackground();*/
            </script>
            <script>

                var clickNumeros = document.querySelector('#iNum1');                

                clickNumeros.onclick = function () {

                    var xRet = "";

                    alert('Ai! Pare de me cutucar!');



                    function selecao() {

                        var nSelecionado;                        
                        var cont = iNum1.innerHTML;
                        
                        //var numDiv = cont.substr((cont.length - (cont.length - 1)), cont.length - (cont.length - 1));
                        //var numDiv = cont.substr((cont.length - (cont.length - 0)), cont.length - (cont.length - 1));

                        var y = parseInt(cont);
                        var id = "lNum";
                        var x = "";
                        var qs = String('"' + "#" + id + y + '"');
                        //var idrecuperada = document.querySelector("#" + id + y);
                        var idrecuperada = document.querySelector('li');

                        xRet += "<div class=" + "tooltip" + ">";
                        //xRet += "<label'" + "id='" + id + "'" + y + "'" + "' > " + cont + " - " + qs + "</label > ";
                        xRet += "<label'" + "id='" + id + "'" + y + "'" + "' > " + qs + "</label > ";
                        xRet += "</div>";


                        recebeNumeros.innerHTML = xRet;
                    }                   

                    selecao();
                    
                }

                iNum2.addEventListener("click", function (e) {
                    e.preventDefault();
                    //alert("*");

                    var xRet = "";

                    var pedeDados = prompt("Digite alguma coisa...");

                    /*
                    var nSelecionados = ["0001", "0002", "0003", "0004", "0005", "0006", "0007"];

                    function numSelecionado() {
                       
                        var contarNumeros = nSelecionados.length;

                        for (var i = 0; i < contarNumeros; i++) {
                            xRet += "<div class=" + "tooltip" + ">";
                            xRet += nSelecionados[i];
                            xRet += "</div>";          
                        }                                                

                        recebeNumeros.innerHTML = xRet;                                         
                    }*/


                    function selecao() {

                        var nSelecionado;
                        //xRet = iNum1.innerHTML;
                        //var cont = iNum1.innerHTML;
                        var cont = iNum2.innerHTML;

                        //var numDiv = cont.substr((cont.length - i), cont.length); fazer for

                        //var numDiv = cont.substr((cont.length - 1), cont.length);
                        //var numDiv = cont.substr(1, 1);
                        //var numDiv = cont.substr((cont.length - (cont.length - 1)), cont.length - (cont.length - 1));
                        var numDiv = cont.substr((cont.length - (cont.length - 0)), cont.length - (cont.length - 1));

                        var y = parseInt(cont);
                        var id = "lNum";
                        var x = "";
                        var qs = String('"' + "#" + id + y + '"');
                        //var idrecuperada = document.querySelector("#" + id + y);
                        var idrecuperada = document.querySelector("#" + id + y);

                        xRet += "<div class=" + "tooltip" + ">";
                        xRet += "<label'" + "id='" + id + "'" + y + "'" + "' > " + cont + " - " + qs+ ", Você Digitou: " + pedeDados +   "</label > ";
                        xRet += "</div>";                       


                        /*
                        for (var i = 0; i < cont.length; i++) {

                            var numDiv = cont.substr((cont.length - (cont.length - i)), cont.length - (cont.length - 1));

                            xRet += "<div class=" + "tooltip" + ">";
                            xRet += "<label'" + "id=" + "'lNum" + y + "'" + "'>" + cont + "</label>";
                            xRet += "</div>";
                            //xRet = x + " - " + y;

                            /*
                            if (numDiv == 0) {
                                x = x + numDiv;
                                alert("Deu " + numDiv + "/" + i);
                                xRet += "<div class=" + "tooltip" + ">";
                                xRet += "<label'" + "id=" + "'lNum" + y + "'" + "'>" + cont + "</label>";
                                xRet += "</div>";
                                xRet = x + " - " + y;
                            }*/
                        //}


                //var numDiv = cont.substr(0,1);


                //xRet = x + " - " + y;
                



                        recebeNumeros.innerHTML = xRet + "#" + numDiv;
                    }

                selecao();

                    //numSelecionado();


                });
            </script>

        </main>
        <footer>
        </footer>
    </form>
</body>
</html>
