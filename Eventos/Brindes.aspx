<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Brindes.aspx.cs" Inherits="Site.Eventos.Brindes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Brindes ASU</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <link rel="sortcut icon" type="image/png" href="../Img/Logo-nav.png" />
    <script type="text/javascript" src="../Js/Apoio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navHome-Internas">
            <p>
                <a href="Home.aspx">
                    <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
            </p>
        </nav>
        <style>
            .bCorpo {
                width: 1200px;
                margin: 0 auto;
                margin-top: 20px;
                min-height: 600px;                
            }

            .bMenuLateral {
                width: 23%;
                margin-right: 2px;
                min-height: 600px;
                float: left;
                border: 2px solid #f26907;
            }

            .bConteudo {
                width: 75%;
                min-height: 600px;
                float: left;
                border: 2px solid #f26907;
                margin-bottom: 20px;
            }

            .BotoesMenuLateral {
                margin-top: 10px;
                margin-left: 5px;
                min-width: 96%;
                height: 20px;
                background-color: #ff0000;
                background-color: #65368a;
                border: 1px solid #f26907;
                border-radius: 6px;
                display: inline-block;
                float: left;
                font-size: 18px;
                font-family: 'Verdana';
                font-family: 'Courier New';
                padding-top: 20px;
                padding-bottom: 20px;
            }

                .BotoesMenuLateral:hover {
                    background-color: #462f58;
                }

            input[type=search], input[type=submit] {
                margin: 5px;
                width: 300px;
                height: 40px;
                border-radius: 6px;
                display: block;
            }

            .cadConteudo {
                width: 310px;
                min-height: 20px;
                margin: 0 auto;
                margin-top: 20px;
            }

            .cadBory p {
                width: 100%;
                padding-top: 10px;
                padding-bottom: 10px;
                background-color: #f26907;
                text-align: center;
                font-size: 1.5em;
                color: #feba89;
            }

            .bBoasVindas {
                margin: 0 auto;
                width: 100%;
                padding-top: 280px;
                padding-bottom: 280px;
                text-align: center;
                font-size: 2em;
                color: #23693f;
                color: #22396f;
            }

            .cadGrid {
                width: 98%;
                margin: 0 auto;
            }

            .cadCampos {
                background-color: red;
                width: 98%;
                margin-top: 10px;
                margin-left: 5px;
            }

            .escondeCadCampos {
                display: none
            }

            .exibeCadCampos {
                display: inline;
            }

            .corLogin {
                color: #f26907;
            }

            .btnAtivaEvento {
                margin: 0 auto;
                margin-top: 5px;
                padding-top: 5px;
                padding-bottom: 5px;
                width: 98%;
                min-height: 2px;
                text-align: center;
                font-size: 1.5em;
            }

                .btnAtivaEvento a {
                    color: #f26907;
                }

                    .btnAtivaEvento a:hover {
                        color: #feba89;
                    }

            #iNome, #iStatus, #iUnidade, #iTempoASU, #iObs {
                width: auto;
                padding-top: 5px;
                padding-bottom: 5px;
                padding-left: 3px;
                border-radius: 3px;
                margin-top: 10px;
                margin-left: 2px;
                border: 1px solid #ebebeb;
                display: inline;
            }

            #btnGravar, #btnAlterar {
                margin-top: 11px;
                width: 80px;
                height: 26px;
                float: left;
            }

            #divCampos {
                width: 765px;
                float: left;
            }

            #divBotao {
                width: 90px;
                float: left;
            }

            .msgResult {
                width: auto;
                margin-left: 20px;
                margin-top: 20px;
                color: #22396f;
            }

            .relCorpo {
                width: 100%;
                min-height: 20px;
            }

            .relTopo {
                width: 100%;
                min-height: 140px;                
            }
            .relTopo p {
                width: 100%;
                text-align: center;
                padding: 20px 0 10px;
                font-size: 1.8em;
                color: #22396f;
            }
            .relDados {
                width: 95%;
                min-height: 20px;                
                margin-left: 30px;
            }
            .BotaoRel{
                padding:    8px 0 4px 0;
                background-color: #22396f;
                width: 200px;
                border-radius: 6px;
                min-height: 25px;                                        
                margin-top: 10px;
                margin-bottom: 15px;
                float: left;
                margin-left: 20px;
                text-align: center;
            }
            .BotaoRel a{                                               
                padding-bottom: 8px;
            }
            .BotaoRel:hover{
                background-color:  #203053;
                
            }
        </style>
        <section class="bCorpo">
            <section class="bMenuLateral">
                <asp:Label ID="lblListaEventos" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblLista" runat="server"></asp:Label>
                <label style="margin-left: 20px; font-size: 2em; color: #65368a;">Lista:</label>
                <div class="btnAtivaEvento">
                    <asp:LinkButton ID="lbtAtivaEvento" runat="server" Text="Brinde 2021" OnClick="ativaOpcaoDaLista"></asp:LinkButton>
                </div>
                <asp:MultiView ID="mwLista" runat="server">
                    <asp:View ID="vwLista" runat="server">
                        <asp:LinkButton ID="lbtCadastro" runat="server" CssClass="BotoesMenuLateral" Text="Cadastro" OnClick="ativarCadastro"></asp:LinkButton>
                        <asp:LinkButton ID="lbtRelatorios" runat="server" CssClass="BotoesMenuLateral" Text="Relatórios" OnClick="ativarRelatorios"></asp:LinkButton>
                    </asp:View>
                </asp:MultiView>
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
                            <p>Cadastrar Participantes</p>
                            <section class="cadConteudo">
                                <input id="iBusca" type="search" runat="server" placeholder="Busca" />
                                <asp:Button ID="btnBusca" runat="server" CssClass="" Text="Localizar Associado" OnClick="procurarAssoc" />
                            </section>
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
                                <p>Você selecionou:</p>
                                <div id="divCampos">
                                    <asp:Label ID="lblIdAssoc" runat="server" Visible="false"></asp:Label>
                                    <input id="iNome" runat="server" type="text" style="width: 250px;" disabled="" />
                                    <input id="iStatus" runat="server" type="text" style="width: 100px;" disabled="" />
                                    <input id="iUnidade" runat="server" type="text" style="width: 100px;" disabled="disabled" />
                                    <input id="iTempoASU" runat="server" type="text" style="width: 100px;" disabled="" />
                                    <input id="iObs" runat="server" type="text" style="width: 150px;" placeholder="Observação" />
                                </div>
                                <div id="divBotao">
                                    <asp:Button ID="btnGravar" runat="server" Text="Gravar" OnClick="entregarBrinde" />
                                    <asp:Button ID="btnAlterar" runat="server" Text="Excluir" OnClick="excluirPessoa" />
                                </div>
                            </section>
                            <asp:Label ID="lblResult" CssClass="msgResult" runat="server"></asp:Label>
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
                                    <asp:LinkButton ID="lbtRelPartipa" runat="server" Text="Relatório de Entrega: A-Z" OnClick="relParticipantes"></asp:LinkButton>
                                </div>
                                <div class="BotaoRel">
                                    <asp:LinkButton ID="lbtRelDias" runat="server" Text="Entregas por dia" OnClick="relPorDia"></asp:LinkButton>
                                </div>
                            </section>
                            <section class="relDados">
                                <asp:Label ID="lblListaRelaorios" runat="server"></asp:Label>
                            </section>
                        </section>
                    </asp:View>
                </asp:MultiView>
            </section>
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
