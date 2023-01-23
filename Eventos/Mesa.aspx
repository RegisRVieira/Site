<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mesa.aspx.cs" Inherits="Site.Eventos.Mesa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mesa</title>
    <meta http-equiv="refresh" content="460" />
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="stylesheet" href="../Css/StyleEventos.css" />
    <link rel="sortcut icon" type="image/png" href="../Img/Logo-nav.png" />
    <script type="text/javascript" src="../Js/Apoio.js"></script>
    <script type="text/javascript" src="../Js/jQuery 3.4.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navHome-Internas">
            <p>
                <a href="Home.aspx">
                    <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </p>
        </nav>
        <section class="secLoginMesa">
            <div style="width: 280px; float: right;">
                <p style="color: #fbb888;">
                    Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                </p>
            </div>
        </section>
        <section>
            <section class="sec1-Mesa">
                <div class="sec1 secMesa">
                    <p>Mesa</p>
                    <div class="area">
                        <div style="width: 100%; height: 30px; color: #0094ff;">
                            <asp:Label ID="lblNmesa" runat="server"></asp:Label>
                        </div>
                        <div class="mesa">
                            <div class="MostraMesa">
                                <asp:Label ID="lblMostraMesa" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="cadeira cadeira1"></div>
                        <div class="cadeira cadeira2"></div>
                        <div class="cadeira cadeira3"></div>
                        <div class="cadeira cadeira4"></div>
                        <div class="cadeira cadeira5"></div>
                        <div class="cadeira cadeira6"></div>
                        <div class="cadeira cadeira7"></div>
                        <div class="cadeira cadeira8"></div>
                    </div>
                </div>
                <div class="sec1 secIntegrantes">
                    <p>Integrantes</p>
                    <div>
                        <asp:Label ID="lblIntegrantes" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="sec1 secBusca">
                    <p>Busca</p>

                    <p>Cadastrar Participantes</p>
                    <section id="secBusca" runat="server">
                        <div style="width: 90%; margin: 0 auto;">
                            <input id="iBusca" type="text" runat="server" placeholder="Busca" class="btnBusca1" />
                        </div>
                        <div style="width: 60%; height: 20px; margin: 0 auto;">
                            <asp:LinkButton ID="lbtnBusca" runat="server" Text="Procurar Associado/Dependente" CssClass="Botao1" OnClick="procurarAssoc"></asp:LinkButton>
                        </div>
                        <div style="width: 60%; height: 20px; margin: 0 auto;">
                            <asp:LinkButton ID="lbtnCadastraConvidado" runat="server" Text="Cadastrar Convidado" CssClass="Botao1" OnClick="preencherConvidado"></asp:LinkButton>
                        </div>
                    </section>

                </div>
            </section>
            <section class="sec2-Mesa">
                <div class="textMsg">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </section>
            <section id="secDadosFiltro" runat="server" class="sec3-Mesa escondeCadCampos" style="background-color: green;">
                <section>
                    <section id="cadBory" class="cadBory" runat="server">
                        <section id="secCadGrid" runat="server" class="cadGrid">
                            <asp:GridView ID="gvAssociados" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="100" EditIndex="-1" SelectedIndex="-1" AllowPaging="false" AllowCustomPaging="False" OnPageIndexChanging="paginarGwAssociados" OnSelectedIndexChanged="selecionarRegistroGvAssociados">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#F26907" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#f26907" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </section>
                        <section id="cadCampos" runat="server" class="cadCampos escondeCadCampos">
                            <p>Você selecionou</p>
                            <div id="divCampos">
                                <asp:Label ID="lblIdAssoc" runat="server" Visible="false"></asp:Label>
                                <input id="iNome" runat="server" type="text" style="width: 250px;" disabled="" />
                                <input id="iTitular" runat="server" type="text" style="width: 250px;" disabled="" />
                                <input id="iStatus" runat="server" type="text" style="width: 100px;" disabled="" />
                                <input id="iUnidade" runat="server" type="text" style="width: 100px;" disabled="disabled" />
                                <input id="iIdade" runat="server" type="text" style="width: 100px;" disabled="" visible="false" />
                                <input id="iObs" runat="server" type="text" style="width: 150px;" placeholder="Observação" />
                                <input id="iTipoPessoa" runat="server" type="text" style="width: 100px;" disabled="" visible="false" />
                            </div>
                            <div id="divBotao">
                                <asp:LinkButton ID="lbtnGravar" runat="server" Text="Gravar" OnClick="gravarParticipante" CssClass="Botao2"></asp:LinkButton>
                            </div>
                            <div style="width: 100%; min-height: 20px; background-color: #ebebeb; margin-top: 55px;">
                                <asp:Label ID="lblMostraDependentes" runat="server"></asp:Label>
                            </div>
                        </section>
                        <asp:Label ID="lblResult" CssClass="msgResult" runat="server"></asp:Label>
                    </section>

                    <section id="cadBoryConvidado" class="cadBory" runat="server">
                        <section id="secCadGridConvidado" runat="server" class="cadGrid">
                            <asp:GridView ID="gvParaConvidados" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="100" EditIndex="-1" SelectedIndex="-1" AllowPaging="false" AllowCustomPaging="False" OnPageIndexChanging="paginarGvConvidados" OnSelectedIndexChanged="selecionarRegistroGvConvidados">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#F26907" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#f26907" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                            </asp:GridView>
                        </section>
                        <section id="divCadConvidados" runat="server" class="cadCampos escondeCadCampos">
                            <p>Você selecionou</p>
                            <div id="divCamposConvidados">
                                <asp:Label ID="lblIdAssocConvidado" runat="server" Visible="false"></asp:Label>
                                <input id="iNomeConvidado"  class="inputConvidados maiuscula" runat="server" type="text" style="width: 250px;" placeholder="Digite o Nome do Convidado" required="required" />
                                <input id="iIdadeConvidado" class="inputConvidados" runat="server" type="text" style="width: 50px;" placeholder="Idade do Convidado" required="required" />
                                <input id="iNomeAssoc" class="inputConvidados" runat="server" type="text" style="width: 250px;" disabled="" />
                                <input id="iStatusAssoc" class="inputConvidados" runat="server" type="text" style="width: 100px;" disabled="" />
                                <input id="iUnidadeAssoc" class="inputConvidados" runat="server" type="text" style="width: 100px;" disabled="disabled" />
                                <input id="iObsConvidado" class="inputConvidados" runat="server" type="text" style="width: 100px;" placeholder="Observação" />
                            </div>
                            <div id="divBotao2">
                                <asp:LinkButton ID="lbtnGravarConvidado" runat="server" Text="Gravar" CssClass="Botao2" OnClick="gravarConvidado"></asp:LinkButton>
                            </div>
                            <div style="width: 100%; min-height: 20px; background-color: #ebebeb; margin-top: 55px;">
                                <asp:Label ID="lblMostraConvidados" runat="server"></asp:Label>
                            </div>
                        </section>
                        <asp:Label ID="Label3" CssClass="msgResult" runat="server"></asp:Label>
                    </section>

                </section>
            </section>
            <section class="sec5-Mesa">
                <div style="width: 400px; min-height: 50px; background-color: #f3f3f3; float: left; margin-right: 10px;">                    
                    <!--<asp:Label ID="lblLista" runat="server"></asp:Label> Comentarios no Código: pesquise pela ID para ver-->
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
                    <div class="dadosCartao">
                        <input id="iCartao" runat="server" class="inputCartao" maxlength="9" type="text" placeholder="Digite o Número do Cartão" />
                    </div>
                    <div class="dadosCartao">
                        <input id="iValorVenda" runat="server" type="text" class="inputCartao" placeholder="Valor da Venda" autocomplete="off" onkeyup="mascaraMoeda(this, event)" />
                    </div>
                    <div class="dadosCartao-ddl">
                        <select id="stParcelas" runat="server" style="margin: 0; width: 50px; min-height: 25px; font-size: 14px; color: #7890c2; border: 1px solid #d8e1f3; border-radius: 6px;">
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
                    </div>
                    <div class="dadosCartao">
                        <input id="iSenhaVenda" runat="server" type="password" class="inputCartao" placeholder="Senha do cartão" />
                    </div>
                    <div class="dadosCartao">
                        <asp:LinkButton ID="lbtnVender" CssClass="Botao1" runat="server" Text="Processar Cobraça" OnClick="realizaVenda"></asp:LinkButton>
                    </div>
                    <asp:Label ID="lblMsgCartao" runat="server"></asp:Label>
                </div>    
                <div style="width: 400px; min-height: 50px; float: left; margin-right: 10px; ">
                    <div class="left" style="margin-top: 7px; background-color: #7890c2; margin-left: 5px;">                    
                        <a href="#" onclick="PrintElem('#lblRetorno', 360, 'Comprovante de Venda')"><img style="width: 90px;" src="../Img/Layout/btImprimir.png" border="0" /></a>                    
                    </div>
                    <div id="retVenda" style='width: 400px; height: 183px; overflow: scroll; font-family: Courier New; font-size: 12px; background-color: #f3f3f3;     '>
                        <div>
                            <asp:Label ID="lblRetorno" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </section>
            <section class="sec2-Mesa">
                <div>
                    <asp:LinkButton ID="lbtnFinalizar" class="Botao1" runat="server" Text="Finalizar" OnClick="finalizarMesa"></asp:LinkButton>
                </div>
            </section>

        </section>
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
    </form>
</body>
</html>
