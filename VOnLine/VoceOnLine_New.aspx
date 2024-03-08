<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoceOnLine_New.aspx.cs" Inherits="Site.VOnLine.VoceOnLine_New" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Você OnLine</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/VoceOnLine.css" />
    <link rel="stylesheet" href="../Css/StyleVoceOnLine.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link href="https://fonts.googleapis.com/css2?family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Hind+Siliguri&family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Hind+Siliguri&family=Lobster&family=Noto+Serif&family=Yellowtail&display=swap" rel="stylesheet" />
    <link rel="stylesheet" media="print" href="../Css/Print.css" />
    <link rel="stylesheet" href="../Css/Table-Extrato.css" />
    <script src="../Js/Apoio.js"></script>
    <script src="../Js/jQuery 3.4.1.js"></script>
</head>
<body onload="detectarResolucao()">
    <form id="form1" runat="server">
        <style>
            .botao231 {
                margin: 0 auto;
                margin-top: 4px;
                margin-bottom: 4px;
                width: 98%;
                min-height: 20px;
                padding: 10px 0 10px 0;
                border-radius: 6px;
                text-align: center;
                background-color: #1c2435;
                color: #89c0e8;
            }

                .botao231:hover {
                    background-color: #0094ff;
                    color: #1c2435;
                }                
                .secPublicidade{                    
                    width: 100%;
                    min-height: 300px;    
                    margin-top: 200px;
                   
                }
                @media(max-width: 1000px){
                    
                    .secPublicidade{                                                       
                        margin-top: 300px;                        
                    }
                }
                        
        </style>
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
                        <div id="divSaldo" class="divSaldo" runat="server">
                            <div id="mostraSaldo" class="cxSaldo">
                                <p>Seu Saldo é:&nbsp<%# retornaSaldo() %></p>
                            </div>
                            <div id="escondeSaldo" style="display: none;" class="cxSaldo">
                                <p>Seu Saldo é:&nbsp <span>■ ■ ■ ■</span></p>
                            </div>
                            <div id="btnVerSaldo" class="cxSaldo margin">
                                <input type="image" src="../Img/icon/Icon-Mostrar.png" width="40" height="40" onclick="verSaldo()" />
                            </div>
                            <div id="btnOcultarSaldo" class="cxSaldo margin" style="display: none;">
                                <input type="image" src="../Img/icon/Icon-Esconder.png" width="40" height="40" onclick="ocultarSaldo()" />
                            </div>
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
                <asp:MultiView ID="mwBotoestopo" runat="server">                    
                    <asp:View ID="vwBotoesAssoc" runat="server">      
                        <style>
                            .tituloCartoes{                                
                                padding-top: 50px;
                            }
                            .tituloCartoes p{                                
                                margin: 0;
                                color: #808080;
                                font-size: 1.3em;
                                padding: 5px 0 5px 0;
                                font-weight: 700;
                            }
                        </style>
                        <div id="divOpcoes" runat="server" class="divOpcoes" >
                            <div class="tituloCartoes">
                                <p>Opções do Associado</p>
                            </div>
                            <asp:LinkButton ID="btnExtrato" runat="server" OnClick="ativarExtrato">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Extrato.png" /></div>
                                <div class="txtOpcoes"><p>Extrato</p></div>                        
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtCartoes" runat="server" OnClick="ativarCartao">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Cartao.png" /></div>
                                <div class="txtOpcoes"><p>Cartões</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtSenha" runat="server" OnClick="ativarSenhaAssoc">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Senha.png" /></div>
                                <div class="txtOpcoes"><p>Senha</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtDadosAssoc" runat="server" OnClick="ativarDadosAssoc">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Dados.png" /></div>
                                <div class="txtOpcoes"><p>Meus Dados</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtGuiaConvenios" runat="server" OnClick="irParaGuiaConvenios">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Guia.png" /></div>
                                <div class="txtOpcoes"><p>Guia de Convênios</p></div>
                            </div>
                            </asp:LinkButton>
                            <div id="divBtnPainelEventos" runat="server" class="opcoes">
                                <asp:LinkButton ID="btnPainelEventos" runat="server" OnClick="irParaPainelEventos">                            
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Eventos.png" /></div>
                            <div class="txtOpcoes"><p>Painel de Eventos</p></div>                            
                                </asp:LinkButton>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwBotoesConv" runat="server">
                        <div id="div1" runat="server" class="divOpcoesConv">
                            <asp:LinkButton ID="lbtVenda" runat="server" OnClick="ativarVenda">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Venda.png" /></div>
                                <div class="txtOpcoes"><p>Venda</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtRelConv" runat="server" OnClick="ativarRelConv">
                        <div class="opcoes">
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Relatorios.png" /></div>
                            <div class="txtOpcoes"><p>Relatórios</p></div>
                        </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtDadosConv" runat="server" OnClick="ativarDadosConv">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Dados.png" /></div>
                                <div class="txtOpcoes"><p>Meus Dados</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtSenhaConv" runat="server" OnClick="ativarSenhaConv">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Senha.png" /></div>
                                <div class="txtOpcoes"><p>Senha</p></div>
                            </div>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtDownloadsConv" runat="server" OnClick="ativarDownloadsConv">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Download.png" /></div>
                                <div class="txtOpcoes"><p>Downloads</p></div>
                            </div>
                            </asp:LinkButton>
                            <!--<div id="div2" runat="server" class="opcoes left">
                        <asp:LinkButton id="LinkButton6" runat="server" OnClick="irParaPainelEventos">                            
                            <div class="imgOpcoes"><img src="../Img/icon/Icon-Eventos.png" /></div>
                            <div class="txtOpcoes"><p>Ofertas</p></div>                            
                        </asp:LinkButton>
                        </div>-->
                            <asp:LinkButton ID="lbtGuiaConv" runat="server" OnClick="irParaGuiaConvenios">
                            <div class="opcoes">
                                <div class="imgOpcoes"><img src="../Img/icon/Icon-Guia.png" /></div>
                                <div class="txtOpcoes"><p>Guia de Convênios</p></div>
                            </div>
                            </asp:LinkButton>
                        </div>
                    </asp:View>
                </asp:MultiView>

            </section>
            <section class="secMeio" >
                <asp:MultiView ID="mwAssociado" runat="server">
                    <asp:View ID="vwPublicidade" runat="server">                        
                        <div class="msgAniversario" >
                            <div id="msgAniver">
                                <%# checarAniversario() %>
                            </div>
                        </div>

                        <section class="secPublicidade">
                            <div class="divPublicidadeTitulo">
                                <p>Para Você</p>
                            </div>
                            <div class="divPublicidade">
                                <%# publicidade() %>                            
                            </div>
                            <div class="conteudo" >
                <style>
                    .divPesquisa{
                        width: 80%;
                        min-height: 60px;
                        margin: 0 auto;
                        background-color: #f3f3f3;
                    }
                    .Desativa{
                        display:none;
                    }
                    .left{
                        float: left;
                    }
                    .divTextPesquisa{
                        width: 80%;
                        min-height: 60px;
                        
                    }
                    .divTextPesquisa p{
                        text-align: right;
                        padding: 16px 10px 10px 0;
                        font-size: 1.5em;
                        color: #f26907;
                    }
                    .divBtnPesquisa{
                        width: 19%;
                        min-height: 60px;
                        
                    }
                    .divBtnPesquisa a{
                        background-color: #f26907;
                        color: #fef3ec;
                        padding: 10px 20px;
                        border-radius: 7px;
                    }
                    .divBtnPesquisa a:hover{
                        background-color: #fef3ec;
                        color: #f26907;
                        border: 1px solid #f26907;
                    }
                    .btnPosicao{
                        margin-top: 20px;
                        width: 100%;
                        min-height: 20px;
                        
                    }
                </style>

                <div id="divPesquisa" runat="server" class="divPesquisa">
                    <div class="divTextPesquisa left" ><p> Olá, gostariamos da sua opnião. Responda nossa pesquisa!</p></div>
                    
                    <div class="divBtnPesquisa left">
                        <div class="btnPosicao">
                            <asp:LinkButton ID="btnPesquisa" runat="server" Text="Click para Participar" OnClick="irParaPesquisa"></asp:LinkButton>
                        </div>
                    </div>
                    

                </div>
                        </section>

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
                                                    <div class="botaoUm">
                                                        <asp:Button ID="btnGerarPeriodoExtAssoc" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" OnClientClick="detectarResolucao()" />
                                                    </div>
                                                    
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
                            </div>
                            <section id="margemRodape"></section>
                        </section>
                    </asp:View>
                    <asp:View ID="vwCartoes" runat="server">
                        <div style="float: left; width: 100%; min-height: 1950px;">
                            <div style="width: 925px; margin: 0 auto;">
                                <%# metodoCartoesAssoc() %>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwSenha" runat="server">
                        <section class="secSenhaAssoc">
                            <div class="divSenhaAssoc">
                                <p>Trocar Senha</p>
                                <div class="divInputSenha">
                                    <input id="iSenhaAtual" runat="server" type="password" placeholder="Sua Senha" />
                                    <input id="iSenhaNova" runat="server" type="password" placeholder="Nova Senha" />
                                    <input id="iSenhaConfirma" runat="server" type="password" placeholder="Confirma Nova Senha" />
                                </div>
                                <div class="btnSenhaAssoc">
                                    <asp:Button ID="btnTrocaSenha" runat="server" Text="Troca Senha" OnClick="trocarSenhaSite" />
                                </div>
                                <asp:Label ID="lblTrocaSenha" runat="server"></asp:Label>
                            </div>
                        </section>
                    </asp:View>
                    <asp:View ID="vwDados" runat="server">
                        <section id="secFormDados" runat="server" class="secFormDados">
                            <p class="titFormCad">Dados Pessoais</p>
                            <input id="iNomeAssoc" runat="server" type="text" placeholder="Nome" />
                            <input id="iCpfAssoc" runat="server" type="text" placeholder="CPF" />
                            <input id="iRgAssoc" runat="server" type="text" placeholder="RG" />
                            <input id="iEmailAssoc" runat="server" type="text" placeholder="E-mail" />
                            <input id="iFoneAssoc" runat="server" type="text" placeholder="Telefone" />
                            <input id="iCelAssoc" runat="server" type="text" placeholder="Celular" />
                            <p class="titFormCad">Dados de Endereço</p>
                            <input id="iCepAssoc" runat="server" type="text" maxlength="8" placeholder="CEP" />

                            <asp:LinkButton ID="lbtBuscaCepAssoc" runat="server" Text="Busca Cep" OnClick="buscarCep"><div class="botao231">Buscar Cep</div></asp:LinkButton>
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
                            <asp:LinkButton ID="lbtlEnviar" runat="server" OnClick="enviarDadosCorrecao"><div class="botao231">Enviar para Alteração</div></asp:LinkButton>
                        </section>
                    </asp:View>
                </asp:MultiView>
                <asp:MultiView ID="mwConveniado" runat="server">
                    <asp:View ID="vwPublicidadeConv" runat="server">                        
                        <section class="secPublicidade">
                            <div class="divPublicidadeTitulo">
                                <p>Para Você</p>
                            </div>
                            <div class="divPublicidade">
                                <%# publicidade() %>
                            </div>
                            <section>
                                <div id="punConv" runat="server" class="divPesquisa">
                                    <div class="divTextPesquisa left" ><p> Olá, gostariamos da sua opnião. Responda nossa pesquisa!</p></div>
                    
                                    <div class="divBtnPesquisa left">
                                        <div class="btnPosicao">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Click para Participar" OnClick="irParaPesquisa"></asp:LinkButton>
                                        </div>
                                    </div>
                    

                                </div>
                            </section>
                        </section>
                    </asp:View>
                    <asp:View ID="vwVenda" runat="server">
                        <div class="corpoFormVenda">
                            <div id="secCompVenda" runat="server" class="form-guia compVendaOn">
                                <h1>Venda OnLine</h1>
                                <input id="iNumCartao" runat="server" type="text" placeholder="Número do Cartão" maxlength="9" autocomplete="off" />
                                <!-- CSS pelo ID -->
                                <script>
                                    String.prototype.reverse = function () {
                                        return this.split('').reverse().join('');
                                    };

                                    function mascaraMoeda(campo, evento) {
                                        var tecla = (!evento) ? window.event.keyCode : evento.which;
                                        var valor = campo.value.replace(/[^\d]+/gi, '').reverse();
                                        var resultado = "";
                                        var mascara = "##.###.###,##".reverse();
                                        for (var x = 0, y = 0; x < mascara.length && y < valor.length;) {
                                            if (mascara.charAt(x) != '#') {
                                                resultado += mascara.charAt(x);
                                                x++;
                                            } else {
                                                resultado += valor.charAt(y);
                                                y++;
                                                x++;
                                            }
                                        }
                                        campo.value = resultado.reverse();
                                    }
                                </script>
                                <input id="iValorVenda" runat="server" type="text" placeholder="Valor da Venda" autocomplete="off" onkeyup="mascaraMoeda(this, event)" />
                                <select id="stParcelas" runat="server">
                                    <option value="01" selected="selected"></option>
                                    <option value="02"></option>
                                    <option value="03"></option>
                                    <option value="04"></option>
                                    <option value="05"></option>
                                    <option value="06"></option>
                                    <option value="07"></option>
                                    <option value="08"></option>
                                    <option value="09"></option>
                                    <option value="10"></option>
                                </select>
                                <input id="iSenhaVenda" runat="server" type="password" placeholder="Senha do Cartão" />
                                <div id="comunicando" runat="server" class="compVendaOff compVenda" style="width: 500px; margin: 0 auto;">
                                    <div style="width: 151px; min-height: 20px; margin: 0 auto;">
                                        <img style="width: 150px" src="../Img/comunicando.gif" />
                                    </div>
                                </div>
                                <script>
                                    function processando() {
                                        document.getElementById("comunicando").className = "compVendaOn";
                                    }
                                </script>
                                <asp:LinkButton ID="lbtVender" runat="server" Text="Realizar Venda" OnClick="realizaVenda" OnClientClick="processando()"><div class="lbtVender"><p>Realizar Venda</p></div></asp:LinkButton>
                                <asp:Label ID="lblMsgVenda" runat="server"></asp:Label>
                            </div>
                            <div id="compVenda" runat="server" class="compVendaOff">
                                <asp:LinkButton ID="lbtFinalizaVenda" runat="server" Text="Finalizar Venda" OnClick="finalizarVenda"><div class="lbtFinalizarVenda"><p>Finalizar Venda</p></div></asp:LinkButton>
                                <div style="width: 100%; min-height: 50px; background-color: #d8e1f3">
                                    <a href="#" onclick="PrintElem('#lblRetorno', 370, 'Comprovante de Venda <%# Titulo() %>')">
                                        <div class="left" style="margin-top: 7px; margin-left: 5px; width: 98%; min-height: 50px;">
                                            <img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></div>
                                    </a>&nbsp                                    
                                </div>
                                <!---->
                                <!--<p style="font-size: 20px; color: white; background-color: #7890c2;">Comprovante de Venda</p>-->
                                <div class="imprimirCompVenda">
                                    <div style="width: 90%; margin: 0 auto; margin-top: 2px;">
                                        <asp:Label ID="lblRetorno" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwRelatoriosConv" runat="server">
                        <style>
                            .corpoRelatoriosConv {
                                width: 670px;
                                min-height: 50px;
                                margin: 0 auto;         
                                
                            }

                            .divBotoesRelConv {
                                width: 98%;
                                min-height: 20px;
                                margin: 0 auto;         
                                
                            }
                            .botaoRelatorios {
                                margin: 2px;
                                width: 160px;
                                height: 50px;
                                border: 1px solid #808080;
                                background-color: #ddeefa;
                                border-radius: 6px;
                            }

                                .botaoRelatorios:hover {
                                    background-color: #d4eaca;
                                }
                                .imgBotaoRel {
                                    width: 35px;
                                    height: 35px;
                                    margin: 0 auto;
                                    float:left;                                    
                                }

                                    .imgBotaoRel img {
                                        width: 100%;
                                    }

                                .txtBotaoRel {
                                    width: 120px;
                                    margin-top: 7px;
                                    float: left;                                    
                                }

                                    .txtBotaoRel p {
                                        margin: 0;
                                        padding: 2px 0;
                                        text-align: center;
                                        color: #808080;
                                    }

                                @media(max-width: 1000px){                                    
                                    .corpoRelatoriosConv {
                                        width: 800px;
                                        min-height: 50px;                                
                                        margin: 0 auto;         
                                        

                                    }

                                    .divBotoesRelConv {
                                        width: 98%;
                                        min-height: 20px;
                                        margin: 0 auto;         
                                        
                                    }
                                    .botaoRelatorios {
                                        
                                        width: 24.3%;
                                        height: 60px;
                                        
                                    }

                                        .botaoRelatorios:hover {
                                            
                                        }
                                        .imgBotaoRel {
                                            width: 45px;
                                            height: 45px;
                                            margin: 0 auto;
                                            float:left;                                    
                                        }

                                            .imgBotaoRel img {
                                                width: 100%;
                                            }

                                        .txtBotaoRel {
                                            width: 120px;
                                            margin-top: 7px;
                                            float: left;                                    
                                        }

                                            .txtBotaoRel p {                                                                                                                                                
                                                font-size: 1.3em;
                                            }

                                }
                        </style>
                        <div class="corpoRelatoriosConv">
                            <div class="divBotoesRelConv">
                                <asp:LinkButton ID="lbtExtratoConv" runat="server" OnClick="ativarExtratoConv">
                            <div class="botaoRelatorios left">
                                <div class="imgBotaoRel"><img src="../Img/icon/Icon-Extrato.png" /></div>
                                <div class="txtBotaoRel"><p>Extrato</p></div>                        
                            </div>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lblRelEntregaConv" runat="server" OnClick="ativarRelEntregaConv">
                                    <div class="botaoRelatorios left">
                                        <div class="imgBotaoRel"><img src="../Img/icon/Icon-Relatorios2.png" /></div>
                                        <div class="txtBotaoRel"><p>Relatório de Entrega</p></div>
                                    </div>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtFaturaMensal" runat="server" OnClick="ativarFaturaConv">
                                    <div class="botaoRelatorios left">
                                        <div class="imgBotaoRel"><img src="../Img/icon/Icon-Relatorios2.png" /></div>
                                        <div class="txtBotaoRel"><p>Faturamento Mensal</p></div>
                                    </div>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lblReimprimeVenda" runat="server" OnClick="ativarConvReimpressao">
                                    <div class="botaoRelatorios left">
                                        <div class="imgBotaoRel"><img src="../Img/icon/Icon-Impressora.png" /></div>
                                        <div class="txtBotaoRel"><p>Reimpressão de Venda</p></div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <asp:MultiView ID="mwRelConv" runat="server">
                                <asp:View ID="vwExtratoConv" runat="server">
                                     <section class="SecPeriodoExtrato">
                                        <!--<div style="width: 100%; font-size: 0.8em;">-->
                                        <div class="BoxDdlPeriodoExtrato defaultTable">
                                            <table class="TabPeridoExtrato">
                                                <caption>Período</caption>
                                                <tbody>
                                                    <tr>
                                                        <td class="tdPeridoExtratoMes">
                                                            <asp:DropDownList ID="ddlMesExtratoConv" runat="server" CssClass="ddlVcOnLine">
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
                                                            <asp:DropDownList ID="ddlAnoExtratoConv" runat="server" CssClass="ddlVcOnLine">                                                                                                                
                                                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                                <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                                <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                                <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                                                <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <div class="botaoUm">
                                                                <asp:Button ID="btnGerarPeriodoExtConv" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="width: 100%; min-height: 50px; background-color: #d8e1f3">
                                            <div class="BtnGerarPDfAssoc" style="margin-top: 7px;  margin-left: 5px;">                                        
                                                <a href="#" onclick="PrintElem('#retExtratoConvenio', 1080, 'EXTRATO CONVÊNIO <%# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>
                                                <br />
                                            </div>                                                                       
                                            <div style="margin-left: 5px; width: 79%; min-height: 20px; float: left;">
                                                <p style="text-align: center; color: #22396f; font-size: 20px; padding-top: 14px; " >Extrato Pós Pago</p>
                                            </div>
                                        </div>                                                                
                                        <div id="retExtratoConvenio"  style="width: 100%; max-height: 300px; overflow: scroll; overflow-x: auto;" >                                   
                                            <asp:Label ID="lblExtratoConvenio" runat="server" CssClass="lblperiodo" ></asp:Label>
                                        </div>
                                
                                        <div style="margin-top: 20px; width: 100%; min-height: 50px;  background-color: #d8e1f3"">
                                            <div class="BtnGerarPDfAssoc" style="margin-top: 7px;  margin-left: 5px;">                                        
                                                <a href="#" onclick="PrintElem('#retExtratoConvenioPre', 1080, 'EXTRATO CONVÊNIO - PRÉ PAGO <%# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>
                                                <br />
                                            </div>
                                            <div style="margin-left: 5px; width: 79%; min-height: 20px; float: left;">
                                                <p style="text-align: center; color: #22396f; font-size: 20px; padding-top: 14px;" >Extrato Pré Pago</p>
                                            </div>
                                        </div>
                                
                                        <div id="retExtratoConvenioPre" style="width: 100%; max-height: 250px; overflow: scroll; overflow-x: auto;">                                    
                                            <asp:Label ID="lblExtratoConvenioPre" runat="server" CssClass="printable2"></asp:Label>
                                        </div>
                                
                                        <div>
                                            <section class="margemRodape"></section>
                                        </div>
                                    </section>
                                </asp:View>
                                <asp:View ID="vwRelEntregaConv" runat="server">
                                    <section class="SecPeriodoExtrato">
                                        <div class="BoxDdlPeriodoExtrato defaultTable">
                                            <table class="TabPeridoExtrato">
                                                <caption>Período</caption>
                                                <tbody>
                                                    <tr>
                                                        <td class="tdPeridoExtratoMes">
                                                            <style>
                                                                input[type="date"] {
                                                                    /*input[type="submit"]*/
                                                                    width: 130px;
                                                                    height: auto;
                                                                }

                                                                .cPeriodo {
                                                                    width: 130px;
                                                                    height: auto;
                                                                }
                                                            </style>                                                                                                                                                                                                                                                                   
                                                            <asp:TextBox ID="iDataIni" runat="server" type="date"></asp:TextBox>                                                    
                                                        </td>
                                                        <td class="tdPeridoExtratoAno">                                                    
                                                            <asp:TextBox ID="iDataFin" runat="server" type="date"></asp:TextBox>                                                    
                                                        </td>
                                                        <td>
                                                            <div class="botaoUm">
                                                                <asp:Button ID="btnGerarData" runat="server" Text="Visualizar Período" OnClick="montarRelEntrega" />                                                    
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>                                
                            
                                        <style>
                                            .lblperiodo {
                                                text-align: left;
                                            }
                                        </style>
                                        <script>

                                            function atualizaDataRelEntrega() {
                                                //preventDefault();

                                                var agora = new Date();

                                                var mes = "";
                                                var dia = agora.getDate();
                                                var dia_atual = agora.getDate();

                                                //Coloca 0 no mês menos que 10
                                                if (agora.getMonth() < 10) {
                                                    mes = "0" + (agora.getMonth() + 1);
                                                } else {
                                                    mes = (agora.getMonth() + 1);
                                                }

                                                // Verifica o dia do Mês para gerar o período a ser carregado no Calendário
                                                if (iDataIni.value == "") {//Executa apenas se o calendário não estiver preenchido

                                                    if (dia <= 10) {
                                                        dia_ini = "20";
                                                        dia_fin = "09";

                                                        if (agora.getMonth() < 10) {
                                                            var Calendar_ini = agora.getFullYear() + "-" + "0" + (agora.getMonth()) + "-" + dia_ini;
                                                        } else {
                                                            var Calendar_ini = agora.getFullYear() + "-" + (agora.getMonth()) + "-" + dia_ini;
                                                        }

                                                        var Calendar_fin = agora.getFullYear() + "-" + mes + "-" + dia_fin;

                                                        iDataIni.value = Calendar_ini;
                                                        iDataFin.value = Calendar_fin;

                                                        //alert("<=10");

                                                    }

                                                    if (dia > 10 && dia < 20) {
                                                        var dia_ini = "10";
                                                        var dia_fin = agora.getDate();

                                                        var Calendar_ini = agora.getFullYear() + "-" + mes + "-" + dia_ini;
                                                        var Calendar_fin = agora.getFullYear() + "-" + mes + "-" + dia_fin;

                                                        iDataIni.value = Calendar_ini;
                                                        iDataFin.value = Calendar_fin;

                                                        //alert(">10 e <20");
                                                    }

                                                    if (dia >= 20) {
                                                        dia_ini = "10";
                                                        dia_fin = "19";

                                                        var Calendar_ini = agora.getFullYear() + "-" + mes + "-" + dia_ini;
                                                        var Calendar_fin = agora.getFullYear() + "-" + mes + "-" + dia_fin;

                                                        iDataIni.value = Calendar_ini;
                                                        iDataFin.value = Calendar_fin;

                                                        //alert(">=20");                                     
                                                    }
                                                }

                                                //alert(wCalendar + " Mês: " + mes + " - Dia: " + dia);
                                            }

                                        </script>                                                     
                                        <div style="width: 100%; min-height: 50px; background-color: #d8e1f3" >
                                            <div class="left" style="margin-top: 7px;  margin-left: 5px;">
                                                <a href="#" onclick="PrintElem('#retRelEntrega', 1080, 'RELATÓRIO DE ENTREGA <%# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>
                                            </div>
                                            <div style="margin-left: 5px; width: 79%; min-height: 20px; float: left;">
                                                <p style="text-align: center; color: #22396f; font-size: 20px; padding-top: 14px; " >Relatório de Entrega</p>
                                            </div>    
                                        </div>                              
                                        <div id="retRelEntrega" style='height: 350px; overflow: scroll; font-family:Courier New; font-size:12px;'>
                                            <asp:Label ID="lblPeriodo" runat="server" CssClass="lblperiodo"></asp:Label>         <!-- Foi utilizado para testar se o intervalo, período, estava correto -->
                                            <asp:Label ID="lblRelEntrega" runat="server" CssClass="lblperiodo"></asp:Label>                                
                                        </div>
                            
                                        <div style="width: 100%; min-height: 50px; background-color: #d8e1f3" >
                                            <div id="divPrintRelEntregaPre" runat="server" class="left" style="margin-top: 7px;  margin-left: 5px;">
                                                <a href="#" onclick="PrintElem('#retRelEntregaPre', 1080, 'RELATÓRIO DE ENTREGA - PRÉ PAGO <%# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>
                                            </div>
                                            <div style="margin-left: 5px; width: 79%; min-height: 20px; float: left;">
                                                <p style="text-align: center; color: #22396f; font-size: 20px; padding-top: 14px; " >Relatório de Entrega - Pré Pago</p>
                                            </div>    
                                        </div>         
                                
                                        <div id="retRelEntregaPre" runat="server" style="height: 250px; overflow: scroll;" >
                                            <asp:Label ID="lblRelEntregaPre" runat="server" CssClass="lblperiodo"></asp:Label>
                                        </div>
                                    </section>

                                </asp:View>
                                <asp:View ID="vwFaturaConv" runat="server">
                                    <div>                                
                                        <table class="TabPeridoExtrato defaultTable">
                                                <caption>Período</caption>
                                                <tbody>
                                                    <tr>
                                                        <td class="tdPeridoExtratoMes">
                                                            <asp:DropDownList ID="ddlFatMensalMes" runat="server" CssClass="ddlVcOnLine">
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
                                                            <asp:DropDownList ID="ddlFatMensalAno" runat="server" CssClass="ddlVcOnLine">                                                                                                                
                                                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                                <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                                <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                                <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                                                <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <div class="botaoUm">
                                                                <asp:Button ID="Button2" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                    </div><br />

                                    <div style="width: 100%; min-height: 50px; background-color: #d8e1f3" >
                                            <div class="left" style="margin-top: 7px;  margin-left: 5px;">
                                                <a href="#" onclick="PrintElem('#retFatMensal', 1080, ' <%# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>
                                            </div>                                            
                                        </div>                              
                                        <div id="retFatMensal" style='height: 350px; overflow: scroll; font-family:Courier New; font-size:12px;'>                                            
                                            <asp:Label ID="lblFaturaMensal" runat="server" CssClass="lblperiodo"></asp:Label>                                
                                        </div>

                                    <div  style="width: 100%; max-height: 300px;" >                                   
                                        
                                    </div>   
                                </asp:View>
                                <asp:View ID="vwReimprimeVenda" runat="server">Reimpressão</asp:View>
                            </asp:MultiView>
                        </div>
                    </asp:View>
                    <asp:View ID="vwDadosConv" runat="server">
                        <section id="secFormDadosConv" runat="server" class="secFormDados">
                            <p class="titFormCad">Dados Cadastrais</p>
                            <input id="iNomeConv" runat="server" type="text" placeholder="Convênio" />
                            <input id="iRazaoSocial" runat="server" type="text" placeholder="Razão Social" />
                            <input id="iCnpj" runat="server" type="text" placeholder="CNPJ/CPF" />
                            <input id="iTipoConv" runat="server" type="text" placeholder="Tipo Convênio" />
                            <p class="titFormCad">Telefones e Endereço</p>
                            <input id="iDddConv" runat="server" type="text" placeholder="DDD" />
                            <input id="iTelefoneConv" runat="server" type="text" placeholder="Telefone" />
                            <input id="iCelularConv" runat="server" type="text" placeholder="Celular" />
                            <input id="iCepConv" runat="server" type="text" placeholder="Cep" />
                            <asp:LinkButton ID="lbtBuscaCepConv" runat="server" Text="Buscar Cep" OnClick="buscarCep"><div class="botao231">Buscar Cep</div></asp:LinkButton>
                            <asp:Label ID="lblErroConv" runat="server"></asp:Label>
                            <input id="iLogradouroConv" runat="server" type="text" placeholder="Logradouro" />
                            <input id="iRuaConv" runat="server" type="text" placeholder="Rua" />
                            <input id="iNumeroConv" runat="server" type="text" placeholder="Número" />
                            <input id="iBairroConv" runat="server" type="text" placeholder="Bairro" />
                            <input id="iComplementConv" runat="server" type="text" placeholder="Complemento" />
                            <input id="iCidadeConv" runat="server" type="text" placeholder="Cidade" />
                            <input id="iEstadoConv" runat="server" type="text" placeholder="Estado" />
                            <p class="titFormCad">Dados Bancários</p>
                            <input id="iBancoConv" runat="server" type="text" placeholder="Banco" />
                            <input id="iAgenciaConv" runat="server" type="text" placeholder="Agência" />
                            <input id="iContaCorrenteConv" runat="server" type="text" placeholder="Conta Corrente" />
                            <input id="iDigCCConv" runat="server" type="text" placeholder="Dígito" />
                            <br />
                            <asp:LinkButton ID="lbtEnviaCorrecaoConv" runat="server" Text="Enviar para Correção" OnClick="enviarDadosCorrecao"><div class="botao231">Enviar para Correção</div></asp:LinkButton>
                        </section>
                        <div style="min-height: 50px;">
                            <style>
                                .msgErroConv {
                                    padding: 20px 0;
                                    background-color: #cfcfcf;
                                }
                            </style>
                            <asp:Label ID="lblMsgConv" runat="server" CssClass="msgErroConv"></asp:Label>
                        </div>
                    </asp:View>
                    <asp:View ID="vwSenhaConv" runat="server">
                        <section class="secSenhaAssoc">
                            <div class="divSenhaAssoc">
                                <p>Trocar Senha</p>
                                <div class="divInputSenha">
                                    <input id="iSenhaAtualConv" runat="server" type="password" placeholder="Sua Senha" />
                                    <input id="iSenhaNovaConv" runat="server" type="password" placeholder="Nova Senha" />
                                    <input id="ISenhaConfirmaConv" runat="server" type="password" placeholder="Confirma Nova Senha" />
                                </div>
                                <div class="btnSenhaAssoc">
                                    <asp:Button ID="lbtTrocaSenhaConv" runat="server" Text="Troca Senha" OnClick="trocarSenhaSite" />
                                </div>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                        </section>
                    </asp:View>
                    <asp:View ID="vwDownloads" runat="server">
                        <style>
                            .corpoDownloadsConv {
                                width: 600px;
                                min-height: 50px;
                                margin: 0 auto;
                            }
                        </style>
                        <div class="corpoDownloadsConv">
                            <%# listarArquivosConvenios() %>
                            <asp:Label ID="lblArquivosConvenio" runat="server"></asp:Label>
                        </div>
                    </asp:View>
                    <asp:View ID="vwOfertas" runat="server">Ofertas</asp:View>
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
           
        </section>
    <asp:Label ID="lblScript" runat="server"></asp:Label>
    <footer class="footerVc">
         <section class="secRodape">
                <style>
                    .divOpcoesRodape {
                        margin: 0 auto;                        
                        width: 50%;
                        min-height: 53px;
                        
                    }
                    .opcoesRodape {
                        margin: 2px;
                        width: 150px;
                        height: 50px;
                        background-color: white;
                        border: 1px solid #eae5e5;
                        border-radius: 6px;
                    }
                    .opcoesRodape:hover{
                        background-color: #d4eaca;
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

                    @media(max-width: 1000px) {
                        .divOpcoesRodape {
                            width: 100%;
                            min-height: 74px;                            
                        }
                        .opcoesRodape {                            
                            width: 250px;    
                            height: 90px;
                            border: 1px solid #eae5e5;
                            border-radius: 6px;
                        }

                        .imgOpcoesRodape {                            
                            width: 40px;
                            height: 40px;
                        }
                        .imgOpcoesRodape {
                            
                        }
                        .imgOpcoesRodape img {
                            margin-top: 0px;
                        }
                        .txtOpcoesRodape {
                            
                        }
                            .txtOpcoesRodape p {
                                font-size: 1.4em;
                            }
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
                
            </section>
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
            <img src="../Img/Icon/Logo2 Régis-ASU.png" />
        </div>
    </footer>
        </form>
    <!--Google Analytics-->
    <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-83720917-1', 'auto');
        ga('send', 'pageview');
    </script>
</body>
</html>
