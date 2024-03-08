<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pesquisa.aspx.cs" Inherits="Site.VOnLine.Pesquisa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <script src="../Js/Apoio.js"></script>
    <script src="../Js/jQuery 3.4.1.js"></script>
    <title>Pesquisa de Satisfação</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navHome-Internas">
            <p>
                <a href="http://www.asu.com.br/Home.aspx">
                    <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </p>
        </nav>
        <style>
            .corpoPesquisa {
                width: 800px;
                min-height: 20px;
                background-color: #f5f2f2;
                margin: 0 auto;
            }

            input[type=radio], input[type=checkbox] {
                margin-left: 30px;
            }

            .resposta {
                width: 500px;
                min-height: 60px;
                margin-left: 10px;
            }

            .pergunta {
                width: 500px;
                min-height: 60px;
                margin-left: 10px;
            }

            .BoxMsgConclusao {
                width: 500px;
                min-height: 100px;
                margin: 0 auto;
                margin-top: 200px;
            }

                .BoxMsgConclusao img {
                    margin: 0 auto;
                }

            .MsgConclusao {
                width: 200px;
                margin: 0 auto;
                font-size: 1.2em;
                color: #f26907;
                text-align: center;
            }

            .botao1 {
                padding: 10px 35px;
                background-color: #f26907;
                color: white;
                border-radius: 3px;
            }

                .botao1:hover {
                    background-color: #ffd800;
                    color: #22396f;
                }

            .BoxBtnConcluido {
                width: 125px;
                min-height: 500px;
                margin: 0 auto;
                margin-top: 50px;
            }
        </style>
        <asp:Label ID="lblMsgExterna" runat="server"></asp:Label>
        <asp:MultiView ID="mwPesquisa" runat="server">
            <asp:View ID="vwDados" runat="server">
                <div class="corpoPesquisa">
                    <asp:Label ID="lblMsgGeral" runat="server"></asp:Label>
                    <p>Pesquisa por método</p>
                    <br />

                    <style>
                        .label {
                            margin: 2px;
                            padding: 10px 20px;
                            font-size: 20px;
                            background-color: #ffd800;
                            cursor: pointer;
                        }

                            .label:hover {
                                background-color: #0094ff;
                            }

                        .nota { /*Tem que receber uma variação numérica*/
                            width: 585px;
                            padding: 10px 100px;
                            background-color: #ffd800;
                            text-align: center;
                            font-size: 30px;
                            display: none;
                        }

                        /*"Nova" Página */
                        .bodyPesquisa {
                            width: 800px;
                            min-height: 200px;
                            background-color: #ffd800;
                            margin-bottom: 30px;
                        }

                        .secNota {
                            width: 98%;
                            min-height: 20px;
                            background-color: #f26907;
                            margin: 0 auto;
                            margin-bottom: 10px;
                        }

                        .secPergunta {
                            width: 98%;
                            min-height: 20px;
                            background-color: #808080;
                            margin: 0 auto;
                        }

                        .secInput {
                            width: 98%;
                            min-height: 20px;
                            background-color: #ff0000;
                            margin: 0 auto;
                        }

                        .secLabel {
                            padding-top: 20px;
                            padding-bottom: 0px;
                            width: 98%;
                            min-height: 60px;
                            background-color: #0094ff;
                            margin: 0 auto;
                            border: 1px solid #f5f2f2;
                            display: inline-block;
                        }
                    </style>



                    <!--
                    <div><p>Qual nota você dá pora a itscocostar</p></div>
                    <div style="width: 800px; min-height: 200px; ">
                        <div style="width: 98%; margin: 0 auto; background-color: yellow;">
                            <label id="lblNota" class="nota" runat="server"></label>
                        </div>                        
                        <div>
                        <input class="nota" name="Teste" id="iTeste0" runat="server" type="radio" value="0" />                    
                        <input class="nota" name="Teste" id="iTeste1" runat="server" type="radio" value="1" />                    
                        <input class="nota" name="Teste" id="iTeste2" runat="server" type="radio" value="2" />                    
                        <input class="nota" name="Teste" id="iTeste3" runat="server" type="radio" value="3" />                    
                        <input class="nota" name="Teste" id="iTeste4" runat="server" type="radio" value="4" />
                        <input class="nota" name="Teste" id="iTeste5" runat="server" type="radio" value="5" />                    
                        <input class="nota" name="Teste" id="iTeste6" runat="server" type="radio" value="6" />                    
                        <input class="nota" name="Teste" id="iTeste7" runat="server" type="radio" value="7" />                    
                        <input class="nota" name="Teste" id="iTeste8" runat="server" type="radio" value="8" />                    
                        <input class="nota" name="Teste" id="iTeste9" runat="server" type="radio" value="9" />
                        <input class="nota" name="Teste" id="iTeste10" runat="server" type="radio" value="10" /><br />
                        </div>
                        <div id="fora" style="margin: 0 auto; width: 647px;">
                            <label class="label" id="Label1" runat="server" for="iTeste0" onclick="x()">0</label>
                            <label class="label" id="Label2" runat="server" for="iTeste1" onclick="x()">1</label>                    
                            <label class="label" id="Label3" runat="server" for="iTeste2" onclick="x()">2</label>                    
                            <label class="label" id="Label4" runat="server" for="iTeste3" onclick="x()">3</label>                    
                            <label class="label" id="Label5" runat="server" for="iTeste4" onclick="x()">4</label>
                            <label class="label" id="Label6" runat="server" for="iTeste0" onclick="x()">5</label>
                            <label class="label" id="Label7" runat="server" for="iTeste1" onclick="x()">6</label>                    
                            <label class="label" id="Label8" runat="server" for="iTeste2" onclick="x()">7</label>                    
                            <label class="label" id="Label9" runat="server" for="iTeste3" onclick="x()">8</label>                    
                            <label class="label" id="Label10" runat="server" for="iTeste4" onclick="x()">9</label>
                            <label class="label" id="Label11" runat="server" for="iTeste4" onclick="x()">10</label>
                        </div>               

                    </div>
                    <br /><br /><br />
                    -->
                    
                    <br />
                    <br />

                    <%# paginaPesquisa() %>

                    <br />
                    <br />


                    <script>
                        function gravarPesquisa(e) {

                            //PA = Pergunta Aberta. Determina a quantidade de perguntas a ser percorrida pelo FOR
                            var PA = document.querySelectorAll(".divPergunta");
                            var OE = document.querySelectorAll(".divOE");

                            //Pergunta Aberta
                            var TR = ""; //Tipo de Resposta
                            var R = ""; //Resposta

                            //Variáveis Fixas
                            var idpesquisa = "";
                            var idpergunta = "";
                            var idAssoc = "";
                            var Idconv = "";
                            var cnscadmom = "";
                            var cnscadusu = "";
                            var iQtdPerguntas = document.getElementById("iQtdPerguntas").innerText;

                            //alert("iQtdPerguntas: " + iQtdPerguntas);

                            //var Idconv = document.getElementById("idConv").innerText;

                            var cont = 0;
                            var contOE = 0;
                            var txt = "";
                            var n = 0;

                            for (i = 0; i < PA.length; i++) {
                                cont++;

                                TR = document.getElementById("SP" + cont).innerText;
                                R = document.getElementById("RSP" + cont).value;

                                //Variáveis Fixas
                                idpesquisa = document.getElementById("idpesquisa").innerText;
                                idppergunta = document.getElementById("idpergunta").innerText;
                                idAssoc = document.getElementById("idAssoc").innerText;
                                Idconv = document.getElementById("idConv").innerText;
                                cnscadmom = document.getElementById("icadmom").innerText;
                                cnscadusu = document.getElementById("icadusu").innerText;

                                txt += "('" + idpesquisa + "', ";
                                txt += "'" + idpergunta + "', ";
                                txt += "'" + idAssoc + "', ";
                                txt += "'" + "dependen" + "', "; //Ver com a Cláudia uma maneira de "pegar" a ID do Dependente
                                txt += "'" + Idconv + "', ";
                                txt += "'" + R + "', ";
                                txt += "'" + TR + "', ";
                                txt += "'" + cnscadusu + "', ";
                                txt += "'" + cnscadmom + "'),";
                                txt += "\n";
                            }

                            //alert(txt);

                            //Pergunas Alternativas

                            var radio = document.querySelectorAll('input[type=radio]');
                            var check = "";
                            var resposta = "";                            

                            for (i = 0; i < radio.length; i++) {
                                contOE++;                                

                                check = document.getElementById("iR" + contOE).checked;

                                //Variáveis Fixas

                                idpesquisa = document.getElementById("idpesquisa").innerText;
                                //alert('idpesquisa ' + idpesquisa);
                                idpergunta = document.getElementById("idpergunta").innerText;
                                //alert("idpergunta " + idpergunta);
                                idAssoc = document.getElementById("idAssoc").innerText;
                                //alert("idAssoc " + idAssoc);
                                Idconv = document.getElementById("idConv").innerText;
                                //alert("Idconv " + Idconv);
                                cnscadmom = document.getElementById("icadmom").innerText;
                                cnscadusu = document.getElementById("icadusu").innerText;                                
                                resposta = document.getElementById("iR" + contOE).value;
                                TR = document.getElementById("POE" + contOE).innerText;                                

                                var contCheck = 0;

                                if (check == true) {
                                    n++;
                                    //alert(i + "Pergunta " + TR + "\n" +"Resposta: " + R);

                                    txt += "('" + idpesquisa + "', ";
                                    txt += "'" + idpergunta + "', ";
                                    txt += "'" + idAssoc + "', ";
                                    txt += "'" + "dependen" + "', "; //Ver com a Cláudia uma maneira de "pegar" a ID do Dependente
                                    txt += "'" + Idconv + "', ";
                                    txt += "'" + resposta + "', ";
                                    txt += "'" + TR + "', ";
                                    txt += "'" + cnscadusu + "', ";
                                    txt += "'" + cnscadmom + "'),";
                                    txt += "\n";                                
                                }

                                //alert("No For: " + txt);
                            }

                            //alert("n: " + n);

                            if (n < iQtdPerguntas) {

                                alert("É preciso responder todas as perguntas para continuar!");

                                //alert("n: " + n + "\n" + "QtdePerguntas: " + iQtdPerguntas);

                            } else {
                                //alert("Feito!");

                                //alert("n: " + n + "\n" + "QtdePerguntas: " + iQtdPerguntas);

                                //alert("Quantidade de Perguntas de Escolha Respondidas: " + n);

                                document.getElementById("iDados").value = txt;

                                alert(txt);
                            }
                            e.preventDefault();
                        }

                        function check(e) {

                            //alert("Está no Check");
                            var contOE = 0;
                            var radio = document.querySelectorAll("input[type=radio]");
                            var check = "";
                            var resposta = "";
                            
                            
                            var txt = "";
                            var n = 0;

                            for (var i = 0; i < radio.length; i++) {
                                contOE++;

                                check = document.getElementById("iR" + contOE).checked;
                                resposta = document.getElementById("iR" + contOE).value;

                                var TR = document.getElementById("POE" + contOE).innerText;                            

                                if (check == true) {
                                    n++;

                                    txt += " Pergunta: " + TR + "\n" + "Resposta: " + resposta + "\n";

                                    //alert(n + " X " + "\n" + txt);

                                }                                

                                /*else {
                                    n++;
                                    alert(n + " X não selecionado");
                                }*/
                            }

                            alert(n + "\n" + txt);

                            e.preventDefault();

                            
                        }

                    </script>
                    <div class="BoxBtnConcluido">
                        <input style="display: none" type="text" id="iDados" runat="server" size="50" /><br />
                        <br />
                        <div style="display: none">
                            <input type="submit" value="Check" onclick="check()" /><br /><br /><br />
                        </div>
                        <asp:LinkButton ID="lbtconcluir" runat="server" Text="Enviar" CssClass="botao1" OnClick="finalizarPesquisa" OnClientClick="gravarPesquisa()"></asp:LinkButton>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vwMsg" runat="server">
                <div class="BoxMsgConclusao">
                    <div style="margin: 0 auto; width: 100%;">
                        <img style="width: 50px;" src="../Img/layout/img_enviado.png" />
                    </div>
                    <asp:Label ID="lblExibeDados" runat="server"></asp:Label>
                    <div style="margin-top: 10px; margin-bottom: 50px;">
                        <asp:Label ID="lblMsg" runat="server" CssClass="MsgConclusao"></asp:Label>
                    </div>
                </div>
                <div class="BoxBtnConcluido">
                    <asp:LinkButton ID="lbtVoltar" runat="server" Text="Finalizar" CssClass="botao1" OnClick="voltarVOConvenio"></asp:LinkButton>
                </div>
            </asp:View>
        </asp:MultiView>

    </form>
    <footer class="footerHome">
        <div class="footerHome-Dados">
            <small>Associação dos Servidores da Unesp 50.805.704/0001-00 
            </small>
            <address>
                1969 -
                <script type="text/javascript">document.write(agora.getFullYear())</script>
                <script type="text/javascript">document.write(" - " + ano + " Anos." + "&#169;" + " Todos os direitos reservados")</script>
            </address>
        </div>
        <div class="footerHome-Img">
            <img src="../Img/Icon/Logo2 Régis-ASU.png" /></div>
    </footer>
</body>
</html>
