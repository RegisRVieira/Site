<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoceOnLine1.aspx.cs" Inherits="Site.VoceOnLine1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Você OnLine</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Form-Fluido.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <link rel="stylesheet" href="Css/StyleVoceOnLine.css" />
    <link rel="stylesheet" href="Css/Table-Extrato.css" />
    <script type="text/javascript" src="Js/Apoio.js"></script>
    <script type="text/javascript" src="Js/jQuery 3.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navHome-Internas">
            <div>
                <p>
                    <a href="Home.aspx">
                        <img  class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
                </p>
            </div>
            <div runat="server" class="BoxVOLogin">
                <div style=" color: white; float: right">
                    <img  src="Img/icon/usuLogin.svg"/>                    
                    <div class="BoxVOLoginMenu">
                        <ul>
                            <li><asp:Label ID="lblUsuLogado" runat="server" CssClass="lblUsuLogado"></asp:Label> 
                                <ul>                                    
                                    <li>Sobre</li>
                                    <li>Voltar à Home</li>
                                    <asp:LinkButton ID="lbtDeslogar" runat="server" Text="Sair" OnClick="fazerLogof"><li>Sair</li></asp:LinkButton>
                                </ul>
                            </li>
                            
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
                                    <li>
                                        <asp:LinkButton ID="lbtDadosAssociado" runat="server" Text="Dados Cadastrais" OnClick="ativarAssocDados"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtExtratoSocio" runat="server" Text="Extrato" OnClick="ativarAssocExtrato"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtCartoes" runat="server" Text="Cartões" OnClick="ativarAssocCartoes"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtAlteraSenha" runat="server" Text="Alterar Senha" OnClick="ativarAssocSenha"></asp:LinkButton></li>
                                    <li><asp:LinkButton ID="lbtFazerLogof" runat="server" Text-="Sair" OnClick="fazerLogof"></asp:LinkButton></li>
                                    <li id="treeline-icon" class="treeline-icon" onclick="openMenuFlutua()">&#9776;</li>
                                    <li id="treeline-closeicon" class="treeline-closeicon" onclick="closeMenuFlutua()">&cross;</li>
                                </ul>
                            </div>
                        </asp:View>
                        <asp:View ID="vwConvênio" runat="server">
                            <div>
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="lbtVoceOnLine" runat="server" Text="Venda OnLine" OnClick="ativarConvVenda"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtDadosConvenio" runat="server" Text="Dados Cadastrais" OnClick="ativarConvDados"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtRelEntrega" runat="server" Text="Relatório de Entrega" OnClick="ativarConvRelEntrega"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtFaturaMensal" runat="server" Text="Faturamento Mensal" OnClick="ativarConvFatura"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtExtratoConvenio" runat="server" Text="Extrato" OnClick="ativarConvExtrato"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtASenhaConvenio" runat="server" Text="Alterar Senha" OnClick="ativarConvSenha"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtDownloads" runat="server" Text="Downloads" OnClick="ativarConvDownloads"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lbtOfertas" runat="server" Text="Ofertas" OnClick="ativarConvOfertas"></asp:LinkButton></li>
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
                            <div style="width: 60%; max-height: 850px; margin: 0 auto; padding: 10px; overflow: scroll;">
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados Pessoais</p>
                                <input id="iNomeAssoc" runat="server" type="text" placeholder="Nome" style="font-size: .9em; padding: 12px 0 12px 15px; height: 12px;" />
                                <input id="iCpfAssoc" runat="server" type="text" placeholder="CPF" />
                                <input id="iRgAssoc" runat="server" type="text" placeholder="RG" />
                                <input id="iEmailAssoc" runat="server" type="text" placeholder="E-mail" />
                                <input id="iFoneAssoc" runat="server" type="tel" placeholder="Telefone" />
                                <input id="iCelAssoc" runat="server" type="tel" placeholder="Celular" />
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados de Endereço</p>
                                <input id="iCepAssoc" runat="server" type="text" maxlength="8" placeholder="CEP" style="font-size: .9em; padding: 12px 0 12px 15px; height: 12px;" />
                                <asp:Button ID="btnBuscarCep" runat="server" Text="Buscar Cep" OnClick="buscarCep" />
                                <asp:Label ID="lblErroAssoc" runat="server"></asp:Label>
                                <input id="iRuaAssoc" runat="server" type="text" placeholder="Rua" />
                                <input id="iNumCasaAssoc" runat="server" type="text" placeholder="Número" />
                                <input id="iBairroAssoc" runat="server" type="text" placeholder="Bairro" />
                                <input id="iComplemAssoc" runat="server" type="text" placeholder="Complemento" />
                                <input id="iCidadeAssoc" runat="server" type="text" placeholder="Cidade" />
                                <input id="iEstadoAssoc" runat="server" type="text" placeholder="Estado" />
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados do Trabalho</p>
                                <input id="iUnidadeAssoc" runat="server" type="text" placeholder="Unidade" />
                                <input id="iDepartAssoc" runat="server" type="text" placeholder="Departamento" />
                                <input id="iSetorAssoc" runat="server" type="text" placeholder="Setor" />
                                <input id="iFuncaoAssoc" runat="server" type="text" placeholder="Função" />
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados Bancários</p>
                                <input id="iBancoAssoc" runat="server" type="text" placeholder="Banco" />
                                <input id="iAgenciaAssoc" runat="server" type="text" placeholder="Agência" />
                                <input id="iContaAssoc" runat="server" type="text" placeholder="Conta Corrente" />
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar para Alteração" OnClick="enviarDadosCorrecao" />
                            </div>
                            <div>
                                <asp:Label ID="lblDados" runat="server"></asp:Label>
                                <asp:Label ID="lblResultado" runat="server"></asp:Label>
                            </div>
                        </asp:View>
                        <asp:View ID="vwAssocExtrato" runat="server">
                            <section style="min-height: 900px;">
                                <div style="width: 100%; font-size: 0.8em;">
                                    <table style="width: 80%;">
                                        <caption>Período</caption>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlMesExtratoAssoc" runat="server" CssClass="ddlVcOnLine">
                                                        <asp:ListItem Value="1" Text="Janeiro"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Fevereiro"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Março"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="abril"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Maio" ></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="Junho"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="Julho" ></asp:ListItem>
                                                        <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="Setembro"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Outubro"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Novembro"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dezembro"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAnoExtratoAssoc" runat="server" CssClass="ddlVcOnLine">
                                                        <asp:ListItem Value="0" Text="2019"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="2020"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2021" Selected=""></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="2022"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="2023"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:Button ID="btnGerarPeriodoExtAssoc" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="lblPeriodoAssoc" runat="server"></asp:Label>
                                </div>
                                <div style="width: 77.8%;">
                                    <%# extratoAssociado() %>
                                    <!-- <asp:Button ID="btnLogof" runat="server" Text="Sair" OnClick="fazerLogof" /> -->
                                    <section id="margemRodape"></section>
                                    <asp:Button ID="btnPdf" runat="server" Text="Gerar PDF" OnClick="gerarPdfExtratoAssoc" />
                                    <div style="margin-top: 20px; margin-bottom: 180px; background-color: #eae5e5">
                                        <asp:Label ID="lbArquivos" runat="server"></asp:Label>
                                    </div>                                    
                                </div>
                            </section>
                        </asp:View>
                        <asp:View ID="vwAssocCartoes" runat="server">                            
                            <div>
                                <%# metodoCartoesAssoc() %>
                            </div>                            
                        </asp:View>
                        <asp:View ID="vwAssocSenha" runat="server">
                            <section style="margin: 0 auto; margin-top: 30px; width: 60%; border: 2px solid #cfcfcf;">
                                <div style="width: 100%">
                                    <p style=" margin-top: 5px; color: #22396f; text-align: center; font-size: 2em;">Trocar Senha</p>
                                    <input id="iSenhaAtual" runat="server" type="password" placeholder="Senha Atual" />
                                    <input id="iSenhaNova" runat="server" type="password" placeholder="Nova Senha" />
                                    <input id="iSenhaConfirma" runat="server" type="password" placeholder="Confirma Nova Senha" />
                                    <asp:Button ID="btnTrocaSenha" runat="server" Text="Troca Senha" OnClick="trocarSenhaAssoc" />
                                    <asp:label ID="lblTrocaSenha" runat="server"></asp:label>
                                </div>
                            </section>
                        </asp:View>
                    </asp:MultiView>
                    <asp:MultiView ID="mwContConv" runat="server">
                        <asp:View ID="vwConvVenda" runat="server">
                            <div class="form-guia">
                                <h1>Venda OnLine</h1>
                                <input id="iNumCartao" runat="server" type="text" placeholder="Digite o Número do Cartão" maxlength="9" autocomplete="off" />
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
                                <input id="iSenha" runat="server" type="password" placeholder="Senha do cartão" />
                                <asp:Button ID="btnVender" runat="server" Text="Realizar Venda" OnClick="realizaVenda" />
                                <a href="VoceOnLineComprovante.aspx">Comprovante</a>
                            </div>
                            <div>
                                <asp:Label ID="lblRetorno" runat="server"></asp:Label>
                            </div>
                        </asp:View>
                        <asp:View ID="vwConvDados" runat="server">
                            <div style="width: 60%; max-height: 850px; min-height: 700px; margin: 0 auto; padding: 10px; overflow: scroll;">
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados Cadastrais</p>
                                <input id="iNomeConv" runat="server" type="text" placeholder="Convênio" />
                                <input id="iRazaoSocial" runat="server" type="tel" placeholder="Razão Social" />
                                <input id="iCnpj" runat="server" type="text" placeholder="CNPJ/CPF" />
                                <input id="iTipoConv" runat="server" type="text" placeholder="Tipo Convênio" />
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Telefones e Endereço</p>
                                <input id="iDddConv" runat="server" type="text" placeholder="DDD" />
                                <input id="iTelefoneConv" runat="server" type="tel" placeholder="Telefone" />
                                <input id="iCelularConv" runat="server" type="tel" placeholder="Celular" />
                                <input id="iCepConv" runat="server" type="text" placeholder="Cep" />
                                <asp:Button ID="btnBuscarCepConv" runat="server" Text="Buscar Cep" OnClick="buscarCep" />
                                <asp:Label ID="lblErroConv" runat="server"></asp:Label>
                                <input id="iLogradouroConv" runat="server" type="tel" placeholder="Logradouro" />
                                <input id="iRuaConv" runat="server" type="text" placeholder="Rua" />
                                <input id="iNumeroConv" runat="server" type="text" placeholder="Número" />
                                <input id="iBairroConv" runat="server" type="text" placeholder="Bairro" />
                                <input id="iComplementConv" runat="server" type="text" placeholder="Complemento" />
                                <input id="iCidadeConv" runat="server" type="text" placeholder="Cidade" />
                                <input id="iEstadoConv" runat="server" type="text" placeholder="Estado" />
                                <p style="font-size: 1.5em; font-family: 'Futura Md BT'; padding-top: 30px;">Dados Bancários</p>
                                <input id="iBancoConv" runat="server" type="text" placeholder="Banco" />
                                <input id="iAgenciaConv" runat="server" type="text" placeholder="Agência" />
                                <input id="iContaCorrenteConv" runat="server" type="text" placeholder="Conta Corrente" />
                                <input id="iDigCCConv" runat="server" type="text" placeholder="Dígito" />
                                <br />
                                <asp:Button ID="btnEnviarCorrecao" runat="server" Text="Enviar para Correção" OnClick="enviarDadosDoConvCorrecao" />
                            </div>
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
                            <div style="width: 100%; font-size: 0.8em;">
                                <div style="width: 200px; float: left">
                                    <asp:DropDownList ID="ddlMes" runat="server">
                                        <asp:ListItem Value="0" Text="Janeiro"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Fevereiro"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Março"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="abril"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Maio" Selected=""></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Junho"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Julho"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Agosto"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Setembro"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Outubro"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Novembro"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Dezembro"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 150px; float: left">
                                    <asp:DropDownList ID="ddlAno" runat="server">
                                        <asp:ListItem Value="0" Text="2019"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="2020"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2021" Selected=""></asp:ListItem>
                                        <asp:ListItem Value="3" Text="2022"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="2023"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 250px; font-size: 20px;">
                                    <asp:Button ID="btnGerarData" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
                                </div>
                            </div>
                            <style>
                                .lblperiodo {
                                    text-align: left;
                                }
                            </style>
                            <asp:Label ID="lblPeriodo" runat="server" CssClass="lblperiodo"></asp:Label>
                            <div style="width: 200px;">
                                <!--< %# criarDDL() %> -->
                            </div>

                            <%# montarRelEntrega() %>
                        </asp:View>
                        <asp:View ID="vwConvFatura" runat="server">
                            <div><a href="javascript:window.print()" id="A4"><b>Imprimir</b></a></div>
                            <div><%# montarRelMensal() %></div>
                        </asp:View>
                        <asp:View ID="vwConvExtrato" runat="server">
                            <div style="width: 100%; font-size: 0.8em;">
                                <div style="width: 200px; float: left">
                                    <asp:DropDownList ID="ddlMesExtrato" runat="server">
                                        <asp:ListItem Value="0" Text="Janeiro"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Fevereiro"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Março"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="abril"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Maio" Selected=""></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Junho"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Julho"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Agosto"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Setembro"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Outubro"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Novembro"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Dezembro"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 150px; float: left">
                                    <asp:DropDownList ID="ddlAnoExtrato" runat="server">
                                        <asp:ListItem Value="0" Text="2019"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="2020"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2021" Selected=""></asp:ListItem>
                                        <asp:ListItem Value="3" Text="2022"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="2023"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 250px; font-size: 20px;">
                                    <asp:Button ID="Button1" runat="server" Text="Visualizar Período" OnClick="atualizaPeriodo" />
                                </div>
                            </div>
                            <%# montarExtratoConv() %>
                        </asp:View>
                        <asp:View ID="vwConvSenha" runat="server">Altera Senha</asp:View>
                        <asp:View ID="vwConvDownloads" runat="server">Downloads</asp:View>
                        <asp:View ID="vwConvOfertas" runat="server">Ofertas</asp:View>
                    </asp:MultiView>
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
                <div class="footerHome-Img">
                    <img src="../Img/Icon/Logo2 Régis-ASU.png" />
                </div>
            </footer>
        </main>
    </form>
</body>
</html>
