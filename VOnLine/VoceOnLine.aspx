<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoceOnLine.aspx.cs" Inherits="Site.VoceOnLine.VoceOnLine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Você OnLine</title>
    <link rel="stylesheet" media="print" href="../Css/Print.css" />
    <link rel="stylesheet" href="../Css/Form-Clean.css" />
    <link rel="stylesheet" href="../Css/Form-Fluido.css" />
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/StyleVoceOnLine.css" />
    <link rel="stylesheet" href="../Css/Table-Extrato.css" />    
    <script type="text/javascript" src="../Js/Apoio.js"></script>
    <script type="text/javascript" src="../Js/jQuery 3.4.1.js"></script>
</head>
<body onload="detectarResolucao()">
    <form id="form1" runat="server">
        <input id="hfTamanhoTela" type="hidden" runat="server" name="hfTamanhoTela" />
        <nav class="navHome-Internas">
            <div>
                <p>
                    <a href="http://www.asu.com.br/Home.aspx">
                        <img  class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
                </p>                
            </div>
            <div runat="server" class="BoxVOLogin">
                <div style=" color: white; float: right">
                    <img  src="../Img/icon/usuLogin.svg"/>                    
                    <div class="BoxVOLoginMenu">
                        <ul>
                            <li><asp:Label ID="lblUsuLogado" runat="server" CssClass=""></asp:Label></li>
                            <li><asp:Label ID="lblMsgIP" runat="server"></asp:Label></li>
                        </ul>
                    </div>    
                </div>
            </div>            
        </nav>
        <main>
            <div class="lateral">
                <div id="menuflutua" class="menuflutua">
                    <asp:MultiView ID="mwVoceOnLine" runat="server">
                        <asp:View ID="vwAssociado" runat="server">
                            <div>
                                <ul>
                                    <li><asp:LinkButton ID="lbtDadosAssociado" runat="server" Text="Dados Cadastrais" OnClick="ativarAssocDados"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtExtratoSocio" runat="server" Text="Extrato" OnClick="ativarAssocExtrato"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtCartoes" runat="server" Text="Cartões" OnClick="ativarAssocCartoes"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtAlteraSenha" runat="server" Text="Alterar Senha" OnClick="ativarAssocSenha"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtFazerLogof" runat="server" Text-="Sair" OnClick="fazerLogof"></asp:LinkButton></li>
                                    <li id="treeline-icon" class="treeline-icon" onclick="openMenuFlutua()">&#9776;</li>
                                    <li id="treeline-closeicon" class="treeline-closeicon" onclick="closeMenuFlutua()">&cross;</li>
                                </ul>
                            </div>
                        </asp:View>
                        <asp:View ID="vwConvênio" runat="server">
                            <div>
                                <ul>
                                    <li><asp:LinkButton ID="lbtVoceOnLine" runat="server" Text="Venda OnLine" OnClick="ativarConvVenda"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtDadosConvenio" runat="server" Text="Dados Cadastrais" OnClick="ativarConvDados"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtRelEntrega" runat="server" Text="Relatório de Entrega" OnClick="ativarConvRelEntrega"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtFaturaMensal" runat="server" Text="Faturamento Mensal" OnClick="ativarConvFatura"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtExtratoConvenio" runat="server" Text="Extrato" OnClick="ativarConvExtrato"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtASenhaConvenio" runat="server" Text="Alterar Senha" OnClick="ativarConvSenha"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtDownloads" runat="server" Text="Downloads" OnClick="ativarConvDownloads"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtOfertas" runat="server" Text="Ofertas" OnClick="ativarConvOfertas"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtSair" runat="server" Text="Sair" OnClick="fazerLogof"></asp:LinkButton></li>
                                    <li id="treeline-icon" class="treeline-icon" onclick="openMenuFlutua()">&#9776;</li>
                                    <li id="treeline-closeicon" class="treeline-closeicon" onclick="closeMenuFlutua()">&cross;</li>
                                </ul>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <div class="conteudo" >
                <div class="contassoc">
                    <asp:MultiView ID="mwContAssoc" runat="server">
                        <asp:View ID="vwAssocDados" runat="server">
                            <section class="secFormDados">
                                <p class="titFormCad">Dados Pessoais</p>
                                <input id="iNomeAssoc" runat="server" type="text" placeholder="Nome" style="font-size: .9em; padding: 12px 0 12px 15px; height: 12px;" />
                                <input id="iCpfAssoc" runat="server" type="text" placeholder="CPF" />
                                <input id="iRgAssoc" runat="server" type="text" placeholder="RG" />
                                <input id="iEmailAssoc" runat="server" type="text" placeholder="E-mail" />
                                <input id="iFoneAssoc" runat="server" type="text" placeholder="Telefone" />
                                <input id="iCelAssoc" runat="server" type="text" placeholder="Celular" />
                                <p class="titFormCad">Dados de Endereço</p>
                                <input id="iCepAssoc" runat="server" type="text" maxlength="8" placeholder="CEP" style="font-size: .9em; padding: 12px 0 12px 15px; height: 12px;" />
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
                            <div>
                                <asp:Label ID="lblDados" runat="server"></asp:Label>
                                <asp:Label ID="lblResultado" runat="server"></asp:Label>
                            </div>
                        </asp:View>
                        <asp:View ID="vwAssocExtrato" runat="server">
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
                                                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                        <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                        <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <div class="BtnPeriodoExtrato">                                                        
                                                        <asp:Button ID="btnGerarPeriodoExtAssoc" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" OnClientClick="detectarResolucao()"/>
                                                        
                                                        
                                                        <asp:Label ID="tTela" runat="server"></asp:Label>
                                                    </div>
                                                </td>                                               
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="lblPeriodoAssoc" runat="server"></asp:Label>
                                </div>                                
                                <div style="width: 77.8%;">
                                    <asp:Label ID="lblRetExtratoAssoc" runat="server" ><%# extratoAssociado() %></asp:Label>                                    
                                    <!-- <asp:Button ID="btnLogof" runat="server" Text="Sair" OnClick="fazerLogof" /> -->
                                    <div class="BtnGerarPDfAssoc">
                                    <!--<asp:Button ID="btnPdfAssoc" runat="server" Text="Extrato PDF" OnClick="gerarPdfExtratoAssoc" />-->
                                        <div class="left" style="margin-top: 7px;  margin-left: 5px;">                                            
                                            <a href="#" onclick="PrintElem('#lblRetExtratoAssoc', 1080, 'Extrato do Associado')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>&nbsp    <br />                                            
                                        </div>
                                    </div>
                                    <div style="width: 100px; height: 50px; float: left">
                                        <asp:Label ID="lblExtratoPdf" runat="server"></asp:Label>
                                        
                                    </div>
                                    <div style="margin-top: 20px; margin-bottom: 180px; background-color: #eae5e5">
                                        <asp:Label ID="lblArquivos" runat="server"></asp:Label>
                                    </div>
                                    <style>
                                        .xxx{
                                                width: 300px;
                                                height: 100px;
                                                background-color: #f26907;
                                            }
                                    </style>
                                    <asp:Label ID="lblTestaTermo" runat="server" CssClass="xxx"></asp:Label>
                                </div>
                                <style>
                                    .premiacao{
                                        position: absolute;
                                        top: 80px;
                                        right: 600px;
                                        width: 300px; 
                                        min-height: 250px; 
                                        background-color: #fef3ec;
                                        border-radius: 8px;
                                        border: 1px solid #f26907;
                                    }
                                    .msgParticipacao{
                                        position: absolute;
                                        top: 80px;
                                        right: 20px;
                                        width: 300px; 
                                        min-height: 50px; 
                                        background-color: #fef3ec;
                                        border-radius: 8px;
                                        border: 1px solid #f26907;
                                        
                                        
                                    }
                                    @media(max-width: 1000px) {
                                    .msgParticipacao{
                                        position: sticky;
                                        top: 184px;
                                        right: 0px;
                                        width: 90%;   
                                        min-height: 0px;
                                        margin-bottom: 120px;
                                    }
                                    }
                                    .msgParticipacao {
                                        padding: 9px;                                        
                                        font-size: 20px;
                                        color: #f26907;
                                    }
                                    .premioTexto{
                                        width: 100%;
                                        height: 200px;
                                        padding: 10px 5px 0 5px;                                        
                                        font-size: 1.5em;
                                    }
                                    .premioTexto img{
                                        margin-top: 5px;
                                        width: 140px;
                                    }
                                    .premioBotao{
                                        margin: 0 auto;
                                        margin-top: 5px;                                        
                                        width: 60%;
                                        min-height: 40px;                                        
                                    }
                                    .btnPremio{                                        
                                        border-radius: 12px;                                        
                                        font-weight: 900;
                                        font-size: 1.2em;
                                        font-family: 'Book Antiqua';
                                        padding: 6px;
                                        
                                    }
                                    .btnNegarBrinde{
                                        display: none;
                                    }
                                    @media(max-width: 1000px){
                                     .premiacao{
                                        top: 190px;
                                        right: 7%;
                                        width: 700px; 
                                        min-height: 360px; 
                                        background-color: #fef3ec;
                                        border-radius: 8px;
                                        border: 1px solid #f26907;
                                    }
                                    .premioTexto{
                                        width: 98%;
                                        font-size: 1.8em;                                        
                                    }
                                    .premioTexto img{
                                        margin-top: 5px;
                                        width: 140px;
                                    }
                                    .premioBotao{                                                                                                               
                                        width: 98%;
                                        margin-left: 50%;
                                        min-height: 40px;                                                                                
                                    }
                                    .btnPremio{                                        
                                        font-size: 1.2em;                                        
                                        padding: 6px;                                        
                                        
                                    }   
                                    .btnNegarBrinde{
                                        font-size: 2em;
                                        display: inline;                                        
                                    }
                                    }
                                </style>
                                <section  style="">
                                    <section id="secPremio" runat="server" class="premiacao">
                                        <div class="premioTexto">
                                            <p>Quero Ganhar um Prêmio da ASU!</p>
                                            <img src="../Img/Publicidade/Brinde - ASU.png" runat="server" />
                                        </div>
                                        <div class="premioBotao">
                                            <asp:Button ID="btnPremio" runat="server" Text="Participar" CssClass="btnPremio" OnClick="gravarBrinde"/>
                                            <asp:LinkButton ID="lbtnNegar" runat="server" Text="Não, Obrigado" CssClass="btnPremio btnNegarBrinde" OnClick="negarBrinde"></asp:LinkButton>
                                        </div>
                                    </section>
                                    <section id="secMsg" runat="server" class="desativa_termo">
                                        <asp:Label ID="lblMsgPremio" runat="server" class=""></asp:Label>
                                    </section>
                                </section>
                                <section id="margemRodape"></section>
                            </section>                            
                        
                        </asp:View>
                        <asp:View ID="vwAssocCartoes" runat="server">                            
                            <div style="float: left; width: 100%; min-height: 250px;">
                                <%# metodoCartoesAssoc() %>                                                              
                            </div>
                            <!-- Remover Display NONE quando for trabalha a troca da senha -->                            
                            <div style="width: 150px; min-height: 20px; display: none; float: left; margin: 10px 0 20px 0;" > 
                                <asp:Button ID="btnEscolherCartao"  runat="server" Text="Trocar Senha" OnClick="escolherCartao"/>
                            </div>                                                        
                            <div runat="server" id="divAlteraSenhaCartao" style="margin: 0 auto; float: left; margin-top: 20px; border: 1px solid #ffd800; border-radius: 6px; padding: 12px 6px;" class="esconderCampos">
                                <p style="width: 100%; ">Trocar Senha do Seu Cartao ASU OnLine</p>
                                <input id="iNovaSenhaCartao" runat="server" type="text" placeholder="Digite a Nova Senha" />
                                <input id="iConfirmaNovaSenhaCartao" runat="server" type="text" placeholder="Confirme a Nova Senha" />
                                <asp:Button ID="btntrocaSenhaCartao" runat="server" Text="Trocar Senha"  OnClick="trocarSenhaCartao"/>
                            </div>
                            <div>
                                <asp:Label ID="lblListaCartoes" runat="server"></asp:Label>                                
                            </div>
                        </asp:View>
                        <asp:View ID="vwAssocSenha" runat="server">
                            <section style="margin: 0 auto; margin-top: 30px; width: 60%; border: 2px solid #cfcfcf;">
                                <div style="width: 100%">
                                    <p style="margin-top: 5px; color: #22396f; text-align: center; font-size: 2em;">Trocar Senha</p>
                                    <input id="iSenhaAtual" runat="server" type="password" placeholder="Sua Senha" />
                                    <input id="iSenhaNova" runat="server" type="password" placeholder="Nova Senha" />
                                    <input id="iSenhaConfirma" runat="server" type="password" placeholder="Confirma Nova Senha" />
                                    <asp:Button ID="btnTrocaSenha" runat="server" Text="Troca Senha" OnClick="trocarSenhaAssoc" />
                                    <asp:Label ID="lblTrocaSenha" runat="server"></asp:Label>
                                </div>
                            </section>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mwContConv" runat="server">
                        <asp:View ID="vwConvVenda" runat="server">
                            <div id="secCompVenda" runat="server" class="form-guia compVendaOn">
                                <h1>Venda OnLine</h1>                                                                
                                <input id="iNumCartao" runat="server" type="text" placeholder="Digite o Número do Cartão" maxlength="9" autocomplete="off" /> <!-- CSS pelo ID -->
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
                                <select id="stParcelas" runat="server" style="width: 520px; min-height: 35px; font-size: 30px; color: #7890c2; border: 1px solid #d8e1f3; border-radius: 6px;">
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
                                <input id="iSenhaVenda" runat="server" type="password" placeholder="Senha do cartão" />
                                <div id="comunicando" runat="server" class="compVendaOff" style="width: 500px; height: 60px; margin-top:20px;margin-left:0px; ">
                                    <img style="width: 150px" src="../Img/comunicando.gif" />                                    
                                </div>
                                    <script>
                                        function processando() {
                                            document.getElementById("comunicando").className = "compVendaOn";
                                        }
                                    </script>
                                <div style="width: 540px;" >
                                    <asp:Button ID="btnVender"  runat="server" Text="Realizar Venda" OnClick="realizaVenda" OnClientClick="processando()" />
                                </div>
                                <asp:Label ID="lblMsgVenda" runat="server"></asp:Label>
                            </div>                            
                            <div id="compVenda" runat="server" class="compVendaOff" >
                                <div class="btnCompVenda">                                    
                                    <asp:Button  ID="btnFinalizaVenda" runat="server" Text="Finalizar Venda" OnClick="finalizarVenda" />
                                </div>
                                <!-- 24-10-2022: Novo modelo de impressão do Comprovante de Venda -->
                                <div style="width: 100%; min-height: 50px; background-color: #d8e1f3" >
                                    <div class="left" style="margin-top: 7px;  margin-left: 5px;">
                                        <!--<a href="#" onclick="PrintElem('#lblRetorno', 1080, 'Comprovante de Venda < %# Titulo() %>')"><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>-->
                                        <a href="#" onclick="PrintElem('#lblRetorno', 1080, 'Comprovante de Venda <%# Titulo() %>')"><span>Opção 1:</span><img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>&nbsp
                                        <a href="#" onclick="PrintComprovante('#lblRetorno', 360, 'Comprovante de Venda <%# Titulo() %>')"><span>Opção 2:</span><img style="width: 40px;" src="../Img/Layout/btImprimir-3.png" border="0" /></a>                                        
                                    </div>
                                    <div style="margin-left: 5px; width: 79%; min-height: 20px; float: left;">
                                        <!--<p style="text-align: center; color: #22396f; font-size: 20px; padding-top: 14px; " >Comprovante de Venda</p>-->
                                    </div>    
                                </div>
                                <!---->
                                <!--<p style="font-size: 20px; color: white; background-color: #7890c2;">Comprovante de Venda</p>-->
                                <div style=" margin-bottom: 50px; padding: 8px; border: 1px solid #7890c2; border-bottom-left-radius: 6px; border-bottom-right-radius: 6px;">
                                    <div>
                                        <asp:Label ID="lblRetorno" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="vwConvDados" runat="server">
                            <section class="secFormDados">
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
                                <div class="btnFormCad">
                                    <asp:Button ID="btnBuscarCepConv" runat="server" Text="Buscar Cep"  OnClick="buscarCepConv"/>
                                </div>
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
                                <div class="btnFormCad">
                                    <asp:Button ID="btnEnviarCorrecao" runat="server" Text="Enviar para Correção" OnClick="enviarDadosDoConvCorrecao" />
                                </div>
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
                        <asp:View ID="vwConvRelEntrega" runat="server">
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
                                                        .cPeriodo{
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
                                                    <asp:Button ID="btnGerarData" runat="server" Text="Visualizar Período" OnClick="montarRelEntrega" />                                                    
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
                        <asp:View ID="vwConvFatura" runat="server">
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
                                                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                        <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                        <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button2" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                            </div>
                            <div  style="width: 100%; max-height: 300px;" >                                   
                                <asp:Label ID="lblFaturaMensal" runat="server" CssClass="lblperiodo" ></asp:Label>
                            </div>    
                        </asp:View>
                        <asp:View ID="vwConvExtrato" runat="server">
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
                                                        <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                                        <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                                        <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                                        <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                                        <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                                        <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
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
                        <asp:View ID="vwConvSenha" runat="server">Em Breve, Aguarde!</asp:View>
                        <asp:View ID="vwConvDownloads" runat="server">                            
                            <%# listarArquivosConvenios() %>
                            <asp:Label ID="lblArquivosConvenio" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="vwConvOfertas" runat="server">Em Breve, aguarde!</asp:View>
                    </asp:MultiView>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Label ID="lblResult" runat="server"></asp:Label>
            <footer class="footerHome">
                <div class="footerHome-Dados">
                    <small>&reg; 1969 -
                <script type="text/javascript">document.write(agora.getFullYear() + ". Todos os direitos reservados")</script>
                    </small>
                    <address>
                        <script type="text/javascript">document.write(ano + " Anos")</script>
                    </address>
                </div>
                <div class="../footerHome-Img">
                    <img src="../Img/Icon/Logo2 Régis-ASU.png" />
                </div>
            </footer>
            </main>
    </form>
    <script>       
        atualizaDataRelEntrega();        
    </script>
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
