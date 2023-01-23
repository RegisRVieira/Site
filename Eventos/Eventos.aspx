<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="Site.Eventos.Eventos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Eventos</title>
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
        <section class="bCorpo">
            <section class="bMenuLateral">
                <p>Eventos</p>
                <asp:Label ID="lblListaEventos" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblLista" runat="server"></asp:Label>
                <asp:LinkButton ID="lbtCadastro" runat="server" CssClass="BotoesMenuLateral" Text="Cadastro" OnClick="ativarCadastro"></asp:LinkButton>
                <asp:LinkButton ID="lbtRelatorios" runat="server" CssClass="BotoesMenuLateral" Text="Relatórios" OnClick="ativarRelatorios"></asp:LinkButton>
                <asp:LinkButton ID="lbtManutencao" runat="server" CssClass="BotoesMenuLateral" Text="Manutenção" OnClick="ativarManutencao"></asp:LinkButton>
                <a href="eMenu.aspx"><span>Voltar</span></a>
            </section>
            <section class="bConteudo">
                <asp:MultiView ID="mwConteudo" runat="server">
                    <asp:View ID="vwBoasVindas" runat="server">
                        <div class="bBoasVindas">
                            <p>Bem Vindo à ASU</p>
                        </div>
                    </asp:View>
                    <asp:View ID="vwCadastro" runat="server">
                        <section class="cadBory">
                            <section style="margin-bottom: 40px;">
                                <p>Cadastrar Participantes</p>
                                <div style="width: 50%; margin: 0 auto; min-height: 170px;">
                                    <div style="width: 100%; text-align: center; padding: 20px 0 10px 0; color: #f26907; font-size: 18px; font-weight: 700;">
                                        <label>Selecione as opções para cadastrar o Participante no Evento</label>
                                    </div>
                                    <select id="stEventoCadParticipante" runat="server"></select>
                                    <select id="stEventoCadAmbiente" runat="server"></select>
                                    <div>
                                        <div style="width: 50%; min-height: 57px; float: left">
                                            <asp:DropDownList ID="stBuscaMesa" Width="90%" runat="server" OnSelectedIndexChanged="checaOcupacaoMesa" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="lblOcupacao" runat="server" CssClass="verMesaText"></asp:Label>
                                        </div>
                                        <div style="width: 40%; float: left;">
                                            <div>
                                                <asp:LinkButton CssClass="Botao1" ID="lbtnMostrarMesa" runat="server" Text="Ver esta Mesa" OnClick="verMesa"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>                                    
                                </div>
                                <br />
                            </section>
                            <section style=" width: 50%; min-height: 350px; border: 2px solid #f26907; border-bottom-left-radius: 12px; border-bottom-right-radius: 12px; margin: 0 auto;">
                                <p style="width: 100%; text-align: center; font-size: 24px; font-weight: 700; clear: both;">Consultar Mesa</p>
                                <asp:DropDownList ID="ddlVerMesa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="consultarMesa"></asp:DropDownList>
                                <asp:Label ID="lblConsultaMesa" runat="server"></asp:Label>
                            </section>                          
                    </asp:View>
                    <asp:View ID="vwRelatorios" runat="server">
                        <section class="relCorpo">
                            <section class="relTopo">
                                <style>
                                    /*#lbtRelPartipa, #lbtRelDias {*/
                                </style>
                                <p>Relatórios</p>
                                <asp:Label ID="lblTituloEvento" runat="server"></asp:Label>

                                <div class="BotaoRel">
                                    <asp:LinkButton ID="lbtRelPartipa" runat="server" Text="Relatório de Entrega: A-Z" OnClick="relatorioAZ"></asp:LinkButton>
                                </div>
                                <div class="BotaoRel">
                                    <asp:LinkButton ID="lbtRelDias" runat="server" Text="Entregas por dia" OnClick="relatorioPorDia"></asp:LinkButton>
                                </div>
                                <div class="BotaoRel">
                                    <asp:LinkButton ID="lbtnRelLocal" runat="server" Text="Por Local" OnClick="relatorioPorLocal"></asp:LinkButton>
                                </div>
                            </section>
                            <section id="secRelatorios" runat="server" class="relDados ocultar">
                                <div class="left" style="margin-top: 7px; background-color: #7890c2; margin-left: 5px;">                    
                                    <a href="#" onclick="PrintElem('#lblListaRelaorios', 700, '')"><img style="width: 90px;" src="../Img/Layout/btImprimir.png" border="0" /></a>                    
                                </div>
                                <div style='width: 95%; height: 450px; overflow: scroll; '>
                                    <asp:Label ID="lblListaRelaorios" runat="server"></asp:Label>    
                                </div>                            
                            </section>                              
                        </section>
                    </asp:View>
                    <asp:View ID="vwManutencao" runat="server">
                        <p style="font-size: 20px; color: #fbb888; padding: 5px 0 15px 15px;">Manutenção</p>
                        <p style="width: 100%; text-align: center; background-color: #f26907; font-size: 24px; color: antiquewhite;">Excluir Participante</p>
                        <div style="width: 50%; margin: 0 auto; margin-top: 20px; height: 50px; ">
                            <style>
                                .ddlOpcao1{
                                    width: 100%;
                                    min-height: 35px;
                                    color: #f26907;
                                    border: 1px solid #fbb888;
                                    font-size: 30px;
                                    text-align: center;
                                }
                            </style>
                            <asp:DropDownList ID="ddlEvento" runat="server" CssClass="ddlOpcao1"></asp:DropDownList>
                        </div>
                    <section id="secBusca" runat="server" style=" width: 100%; min-height: 120px;">
                        <div style="width: 70%; margin: 0 auto;">
                            <input id="iBusca" type="text" runat="server" placeholder="Busca" class="btnBusca1" />
                        </div>
                        <div style="width: 60%; height: 20px; margin: 0 auto;">
                            <asp:LinkButton ID="lbtnBusca" runat="server" Text="Procurar Participante" CssClass="Botao1" OnClick="procurarParticipante"></asp:LinkButton>
                        </div>
                    </section>
                    <section id="secExcluiParticipante" class="secExcluiParticipante" runat="server" >                        
                        <asp:GridView ID="gvParticipantes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="99.5%" PageSize="100" EditIndex="-1" SelectedIndex="-1" AllowPaging="false" AllowCustomPaging="False" OnPageIndexChanging="paginarGwParticipante" OnSelectedIndexChanged="selecionarRegistroGvParticipantes">
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
                    <section>
                        <div style="width: 100%; color: #0094ff; padding: 10px 0 10px 0; text-align:center;">
                        <asp:Label ID="lblMsgExcluir" runat="server"></asp:Label>
                        </div>
                        <asp:LinkButton ID="lbtnExcluir" runat="server" CssClass="Botao1" Text="Excluir Participante do Evento" OnClick="excluirParticipante" Visible="false"></asp:LinkButton>
                    </section>
                        <section>
                            <asp:LinkButton ID="lbtnFinalizar" Visible="false" CssClass="Botao1" runat="server" Text="Finalizar" OnClick="finalizarExclusao"></asp:LinkButton>
                        </section>
                    </asp:View>
                </asp:MultiView>
            </section>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </section>
        <section style="width: 1200px; min-height: 20px; margin: 0 auto; margin-top: 6px;">
            <div style="width: 280px; float: right;">
                <p style="color: #fbb888;">
                    Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                </p>
            </div>
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
