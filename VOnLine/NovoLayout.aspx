<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoLayout.aspx.cs" Inherits="Site.VOnLine.NovoLayout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Você OnLine</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/VoceOnLine.css" />
    <link rel="stylesheet" href="../Css/StyleVoceOnLine.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link href="https://fonts.googleapis.com/css2?family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Hind+Siliguri&family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Hind+Siliguri&family=Lobster&family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet">



    <link rel="stylesheet" media="print" href="../Css/Print.css" />
    <!--
    <link rel="stylesheet" href="../Css/Form-Clean.css" />
    <link rel="stylesheet" href="../Css/Form-Fluido.css" />    
    -->
    <link rel="stylesheet" href="../Css/Table-Extrato.css" />

    <script src="../Js/Apoio.js"></script>
    <script src="../Js/jQuery 3.4.1.js"></script>
</head>
<body onload="detectarResolucao()">
    <form id="form1" runat="server">
        <!--Armazena a Resolução da Tela para o Método Extrato identificar qual extrato deve exibir. Normal ou fluído.-->
        <input id="hfTamanhoTela" type="hidden" runat="server" name="hfTamanhoTela" />
        <nav class="navHome-Internas">
            <p>
                <a href="http://www.asu.com.br/Home.aspx">
                    <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </p>
        </nav>
        <section class="secCorpo">
            <section class="secTopo">
                <section class="secTopoDados">
                    <div class="divTopoDados">
                        <div class="divLogado">
                            <script>
                                function ocultar() {
                                    var ocultar = document.getElementById("msgAniver");

                                    ocultar.style.display = "none";
                                    event.preventDefault();
                                }
                                function verSaldo() {

                                    var mostrarSaldo = document.getElementById("mostraSaldo");
                                    var btnVerSaldo = document.getElementById("btnVerSaldo");
                                    var btnOcultarSaldo = document.getElementById("btnOcultarSaldo");
                                    var ocultarSaldo = document.getElementById("escondeSaldo");

                                    btnVerSaldo.style.display = "none";
                                    btnOcultarSaldo.style.display = "Block";
                                    mostrarSaldo.style.display = "none";
                                    ocultarSaldo.style.display = "block";

                                    event.preventDefault();
                                }

                                function ocultarSaldo() {

                                    var mostrarSaldo = document.getElementById("mostraSaldo");
                                    var btnVerSaldo = document.getElementById("btnVerSaldo");
                                    var btnOcultarSaldo = document.getElementById("btnOcultarSaldo");
                                    var ocultarSaldo = document.getElementById("escondeSaldo");

                                    btnVerSaldo.style.display = "block";
                                    btnOcultarSaldo.style.display = "none";
                                    mostrarSaldo.style.display = "block";
                                    ocultarSaldo.style.display = "none";

                                    event.preventDefault();
                                }
                            </script>
                            <p>Olá, <%# usuarioLogado() %></p>
                        </div>
                        <div class="divSaldo">
                            <div id="mostraSaldo" class="cxSaldo">
                                <p>Seu Saldo é:&nbsp<%# retornaSaldo() %></p>
                            </div>
                            <div id="escondeSaldo" style="display: none;" class="cxSaldo">
                                <p>Seu Saldo é:&nbsp <span>■ ■ ■ ■</span></p>
                            </div>
                            <div id="btnVerSaldo" class="cxSaldo margin">
                                <input type="image" src="../Img/icon/Icon-Mostrar.png" width="25" height="25" onclick="verSaldo()" /></div>
                            <div id="btnOcultarSaldo" class="cxSaldo margin" style="display: none;">
                                <input type="image" src="../Img/icon/Icon-Esconder.png" width="25" height="25" onclick="ocultarSaldo()" /></div>
                        </div>
                    </div>
                    <div class="divSair">
                        <asp:LinkButton ID="btnSair" runat="server" OnClick="sairVoceOnline">
                            <div class="divSairImg">
                                <img src="../Img/Icon/Icon-Desligar.png" />
                            </div>
                            <div class="divSairTxt">
                                <p>Sair</p>
                            </div>
                        </asp:LinkButton>
                    </div>
                </section>
                <div id="divOpcoes" runat="server" class="divOpcoes">
                    <asp:LinkButton ID="btnExtrato" runat="server" OnClick="ativarExtrato">
                        <div class="opcoes left">
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Extrato.png" /></div>
                            <div class="txtOpcoes"><p>Extrato</p></div>                        
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtCartoes" runat="server" OnClick="ativarCartao">
                        <div class="opcoes left">
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Cartao.png" /></div>
                            <div class="txtOpcoes"><p>Cartões</p></div>
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtSenha" runat="server" OnClick="ativarSenhaAssoc">
                    <div class="opcoes left">
                        <div class="imgOpcoes"><img src="../Img/icon/Icon-Senha.png" /></div>
                        <div class="txtOpcoes"><p>Senha</p></div>
                    </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtDadosAssoc" runat="server" OnClick="ativarDadosAssoc">
                        <div class="opcoes left">
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Dados.png" /></div>
                            <div class="txtOpcoes"><p>Meus Dados</p></div>
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtGuiaConvenios" runat="server" OnClick="irParaGuiaConvenios">
                        <div class="opcoes left">
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Guia.png" /></div>
                            <div class="txtOpcoes"><p>Guia de Convênios</p></div>
                        </div>
                    </asp:LinkButton>
                    <div id="divBtnPainelEventos" runat="server" class="opcoes left">
                        <asp:LinkButton id="btnPainelEventos" runat="server" OnClick="irParaPainelEventos">                            
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Eventos.png" /></div>
                            <div class="txtOpcoes"><p>Painel de Eventos</p></div>                            
                        </asp:LinkButton>
                    </div>
                </div>
                <style>
                    
                </style>
            </section>
            <section class="secMeio">
                <asp:MultiView ID="mwAssociado" runat="server">
                    <asp:View ID="vwPublicidade" runat="server">
                        <div class="divPublicidadeTitulo">
                            <p>Para Você</p>
                        </div>                        
                        <div class="msgAniversario">
                            <div id="msgAniver">
                                <%# checarAniversario() %>
                            </div>
                        </div>

                        <div class="divPublicidade">
                            <img src="../Img/Painel-Clube.jpg" />
                        </div>

                    </asp:View>
                    <asp:View ID="vwExtrato" runat="server">
                        <section class="SecPeriodoExtrato">
                            <div class="BoxDdlPeriodoExtrato defaultTable">
                                <table class="TabPeridoExtrato">
                                    <caption>Período</caption>
                                    <tbody>
                                        <tr>
                                            <td class="tdPeridoExtratoMes">
                                                <asp:DropDownList ID="ddlMesExtratoAssoc" runat="server" CssClass="ddlVcOnLine">
                                                    <asp:ListItem Value="1" Text="Janeiro"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Fevereiro"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Março"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="abril"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Maio"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Junho"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="Julho"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="Setembro"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="Outubro"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="Novembro"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="Dezembro"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdPeridoExtratoAno">
                                                <asp:DropDownList ID="ddlAnoExtratoAssoc" runat="server" CssClass="ddlVcOnLine">
                                                    <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                    <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                    <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                    <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                    <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                                    <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                <div class="BtnPeriodoExtrato">
                                                    <asp:Button ID="btnGerarPeriodoExtAssoc" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" OnClientClick="detectarResolucao()" />
                                                    <asp:Label ID="tTela" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Label ID="lblPeriodoAssoc" runat="server"></asp:Label>
                            </div>
                            <div style="width: 77.8%;">
                                <asp:Label ID="lblRetExtratoAssoc" runat="server"><%# extratoAssociado() %></asp:Label>

                                <div class="BtnGerarPDfAssoc">
                                    <div class="left" style="margin-top: 7px; margin-left: 5px;">
                                        <a href="#" onclick="PrintElem('#lblRetExtratoAssoc', 1080, 'Extrato do Associado')">
                                            <img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>&nbsp   
                                        <br />
                                    </div>
                                </div>
                                <div style="width: 100px; height: 50px; float: left">
                                    <asp:Label ID="lblExtratoPdf" runat="server"></asp:Label>

                                </div>
                                <div style="margin-top: 20px; margin-bottom: 180px; background-color: #eae5e5">
                                    <asp:Label ID="lblArquivos" runat="server"></asp:Label>
                                </div>
                                <style>
                                    .xxx {
                                        width: 300px;
                                        height: 100px;
                                        background-color: #f26907;
                                    }
                                </style>
                                <asp:Label ID="lblTestaTermo" runat="server" CssClass="xxx"></asp:Label>
                            </div>

                            <section id="margemRodape"></section>
                        </section>
                    </asp:View>
                    <asp:View ID="vwCartoes" runat="server">
                        <div style="float: left; width: 100%; min-height: 250px;">
                            <div style="width: 925px; margin: 0 auto;">
                                <%# metodoCartoesAssoc() %>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwSenha" runat="server">
                        <style>
                            .secSenhaAssoc {
                                margin: 0 auto;
                                margin-top: 30px;
                                width: 100%;
                                border: 2px solid #cfcfcf;
                            }

                                .secSenhaAssoc p {
                                    margin-top: 5px;
                                    color: #22396f;
                                    text-align: center;
                                    font-size: 2em;
                                }

                                .secSenhaAssoc input {
                                    margin: 2px;
                                    display: inline-block;
                                    width: 99%;
                                    min-height: 45px;
                                    padding-left: 10px;
                                }

                                    .secSenhaAssoc input[type=submit] {
                                        width: 200px;
                                        min-height: 50px;
                                        text-align: center;
                                        background-color: #0094ff;
                                        cursor: pointer;
                                        border-radius: 6px;
                                        color: white;
                                        font-weight: 700;
                                    }

                                        .secSenhaAssoc input[type=submit]:hover {
                                            background-color: #89c0e8;
                                            color: #0094ff;
                                        }

                            .btnSenhaAssoc {
                                width: 202px;
                                margin: 0 auto;
                                margin-top: 10px;
                                margin-bottom: 10px;
                            }

                            .divSenhaAssoc {
                                margin: 0 auto;
                                width: 850px;
                                min-height: 20px;
                            }

                            .divInputSenha {
                                width: 500px;
                                min-height: 20px;
                                margin: 0 auto;
                            }
                        </style>
                        <section class="secSenhaAssoc">
                            <div class="divSenhaAssoc">
                                <p>Trocar Senha</p>
                                <div class="divInputSenha">
                                    <input id="iSenhaAtual" runat="server" type="password" placeholder="Sua Senha" />
                                    <input id="iSenhaNova" runat="server" type="password" placeholder="Nova Senha" />
                                    <input id="iSenhaConfirma" runat="server" type="password" placeholder="Confirma Nova Senha" />
                                </div>
                                <div class="btnSenhaAssoc">
                                    <asp:Button ID="btnTrocaSenha" runat="server" Text="Troca Senha" OnClick="trocarSenhaAssoc" />
                                </div>
                                <asp:Label ID="lblTrocaSenha" runat="server"></asp:Label>
                            </div>
                        </section>
                    </asp:View>
                    <asp:View ID="vwDados" runat="server">
                        <section id="secFormDados" runat="server" class="secFormDados">
                            <style>
                                .secFormDados {
                                    background-color: #c0e0f7;
                                    width: 600px;
                                    min-height: 925px;
                                    margin: 0 auto;
                                    margin-bottom: 10px;
                                }

                                    .secFormDados input {
                                        margin: 2px;
                                        display: inline-block;
                                        width: 90%;
                                        min-height: 25px;
                                    }
                            </style>
                            <p class="titFormCad">Dados Pessoais</p>
                            <input id="iNomeAssoc" runat="server" type="text" placeholder="Nome" />
                            <input id="iCpfAssoc" runat="server" type="text" placeholder="CPF" />
                            <input id="iRgAssoc" runat="server" type="text" placeholder="RG" />
                            <input id="iEmailAssoc" runat="server" type="text" placeholder="E-mail" />
                            <input id="iFoneAssoc" runat="server" type="text" placeholder="Telefone" />
                            <input id="iCelAssoc" runat="server" type="text" placeholder="Celular" />
                            <p class="titFormCad">Dados de Endereço</p>
                            <input id="iCepAssoc" runat="server" type="text" maxlength="8" placeholder="CEP" />
                            <div class="btnFormCad">
                                <asp:Button ID="btnBuscarCep" runat="server" Text="Buscar Cep" OnClick="buscarCepAssoc" />
                            </div>
                            <asp:Label ID="lblErroAssoc" runat="server"></asp:Label>
                            <input id="iLogradouroAssoc" runat="server" type="text" placeholder="Logradouro" />
                            <input id="iRuaAssoc" runat="server" type="text" placeholder="Rua" />
                            <input id="iNumCasaAssoc" runat="server" type="text" placeholder="Número" />
                            <input id="iBairroAssoc" runat="server" type="text" placeholder="Bairro" />
                            <input id="iComplemAssoc" runat="server" type="text" placeholder="Complemento" />
                            <input id="iCidadeAssoc" runat="server" type="text" placeholder="Cidade" />
                            <input id="iEstadoAssoc" runat="server" type="text" placeholder="Estado" />
                            <p class="titFormCad">Dados do Trabalho</p>
                            <input id="iUnidadeAssoc" runat="server" type="text" placeholder="Unidade" />
                            <input id="iDepartAssoc" runat="server" type="text" placeholder="Departamento" />
                            <input id="iSetorAssoc" runat="server" type="text" placeholder="Setor" />
                            <input id="iFuncaoAssoc" runat="server" type="text" placeholder="Função" />
                            <p class="titFormCad">Dados Bancários</p>
                            <input id="iBancoAssoc" runat="server" type="text" placeholder="Banco" />
                            <input id="iAgenciaAssoc" runat="server" type="text" placeholder="Agência" />
                            <input id="iContaAssoc" runat="server" type="text" placeholder="Conta Corrente" />
                            <div class="btnFormCad">
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar para Alteração" OnClick="enviarDadosCorrecao" />
                            </div>
                        </section>
                    </asp:View>
                </asp:MultiView>
                <style>
                    .divMsg {
                        width: 850px;
                        min-height: 20px;
                        margin: 0 auto;
                        padding: 5px;
                        color: #f26907;
                        font-size: 16px;
                        text-align: center;
                    }
                </style>
                <div class="divMsg">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </section>
        <section class="secRodape">
            <style>
                .divOpcoesRodape {
                    margin: 0 auto;
                    background-color: white;
                    width: 50%;
                    min-height: 57px;
                }

                .opcoesRodape {
                    margin: 2px;
                    width: 150px;
                    height: 50px;
                    background-color: white;
                }

                .imgOpcoesRodape {
                    width: 25px;
                    height: 25px;
                    margin: 0 auto;
                }

                    .imgOpcoesRodape img {
                        width: 100%;
                    }

                .txtOpcoesRodape {
                    width: 100%;
                    margin-top: 1px;
                }

                    .txtOpcoesRodape p {
                        margin: 0;
                        padding: 2px 0;
                        text-align: center;
                        color: #808080;
                    }
            </style>
            <div class="divOpcoesRodape">
                <asp:LinkButton ID="lbtEventos" runat="server" OnClick="irParaEventos">
                        <div class="opcoesRodape left">
                            <div class="imgOpcoesRodape"><img src="../Img/icon/Icon-Eventos.png" /></div>
                            <div class="txtOpcoesRodape"><p>Eventos</p></div>
                        </div>
                </asp:LinkButton>
                <asp:LinkButton ID="lbtNoticias" runat="server" OnClick="irParaNoticias">
                        <div class="opcoesRodape left">
                            <div class="imgOpcoesRodape"><img src="../Img/icon/Icon-Noticias.png" /></div>
                            <div class="txtOpcoesRodape"><p>Notícias</p></div>
                        </div>
                </asp:LinkButton>
            </div>
            <div></div>
        </section>
        </section>        
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
