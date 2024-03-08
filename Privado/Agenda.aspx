<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="Site.Privado.Agenda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agenda</title>
</head>
<body>
    <style>
        .html, body{
            margin: 0;
            padding: 0;
        }
        .agenda-main{
            width: 100%;
            min-height:900px;
            /*background-color: #23396f;*/
        }
        .agenda-menu{
            width: 400px;
            min-height: 500px;
            /*background-color: yellow;*/
            float: left;
        }
        .agenda-menu-calendar{
            margin: 0 auto;
            padding: 2px;
            width: 350px;
            background-color: #edeaea;
        }
        .agenda-dados{
            width: 600px;
            min-height: 500px;
            /*background-color: palevioletred;*/
            float: left;
            border: 1px solid #edeaea;
        }
        .agenda-Msg{
            width: 100%;
            min-height: 20px;
            /*background-color: cornflowerblue;*/
            float: left;
        }
        .agenda-dados-botoes{
            margin: 0 auto;
            margin-top: 5px;
            margin-bottom: 5px;
            width: 200px;
            min-height:20px;
            
            /*background-color: purple;*/

        }
        .btn1{
            margin-top: 2px;
            margin-bottom: 2px;
            width:99.5%;
            min-height: 25px;
            border-radius: 8px;
            padding: 8px 24px 8px 34px;
            color: #1552f8;
            text-align: center;
            background-color: #6f91ef;
            cursor: pointer;
            font-size: 20px;
        }
        
        .btn1:hover{
            color: #6f91ef;
            background-color: #1552f8;
        }
        .titulo1{
            background-color: #edeaea;
            padding-left: 20px;
            color: #454444;
        }
        .agenda-dados-input{
            width: 6    00px;
            min-height: 30px;
            float: left;
            margin: 0 auto;            
        }
        .agenda-dados-btn{
            width: 100%;
            min-height: 20px;
            
        }
            input[type=text], input[type=time], select{
                margin: 0 auto;
                margin-left: 5px;
                margin-right: 5px;
                width: 580px;
                height: 25px;
                margin-top: 5px;
                margin-bottom: 5px;                
                border: 1px solid #e0dfdf;
                padding: 5px 0 5px 10px;
                color: #454444;
            }

        .displayOn{
            display: block;
        }
        .displayOff{
            display: none;
        }
        .agenda-header{
            width: 100%;
            min-height: 70px;
            border-top: 6px solid #edeaea;
            border-bottom: 2px solid #edeaea;
            margin-bottom: 10px;
        }
        .agenda-header-dia{
            margin-top: 10px;
            margin-left: 100px;
            width: 35px;
            height: 30px;            
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
            border-top: 10px solid #ff6a00;
            border-bottom: 10px solid #6f91ef;
            border-left: 10px solid #1552f8;
            border-right: 10px solid #ff0000;

        }
        .agenda-header-dia-dia{            
            text-align: center;
            font-size: 22px;
            padding: 0;
            margin-top: 2px;
            color: #1552f8;
        }
        .agenda-header-titulo{
            width: 100%;
            height: auto;            
            padding-top: 16px;
            padding-bottom: 16px;
            font-size: 2em;
            color: #1552f8;
        }
        .left{
            float: left;
        }
        .agenda-lista-nome a{
            text-decoration: none;
            color: #454444;
        }
    </style>
    <form id="form1" runat="server">
        <main class="agenda-main">  
            <section class="agenda-header">
                <div style="width: 10%; min-height: 70px; float: left">                    
                    <div class="agenda-header-dia">
                    <p class="agenda-header-dia-dia" id="dia"><%# dia() %></p>
                </div>
                
                </div>
                <div style="width: 6%; min-height: 70px; float: left">
                    <div class="agenda-header-titulo">
                    Agenda:
                    </div>
                </div>
                <div style="width: 83%; min-height: 70px; float: left">
                    <div class="agenda-header-titulo">
                        Solange Pires
                    </div>
                </div>
            </section>
            <section class="agenda-menu">
                <div class="agenda-menu-calendar">
                    <asp:Calendar ID="cAgenda" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" OnSelectionChanged="checarData">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="#BFB4B4" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
                <div class="agenda-dados-botoes">                    
                    <asp:Button CssClass="btn1" ID="btnAgenda" runat="server" Text="Lista de Paciente" OnClick="ativarViewLista" OnClientClick="atualizar()"/>
                    <asp:Button CssClass="btn1" ID="btnData" runat="server" Text="Consulta" OnClick="ativarViewCadastrar" OnClientClick="atualizar()"/>
                    <asp:Button CssClass="btn1" ID="btnAvaliar" runat="server" Text="Avaliação" OnClick="ativarViewAvaliar" OnClientClick="atualizar()"/>
                </div>
            </section>
            <section class="agenda-dados">
                <asp:MultiView ID="mvAgenda" runat="server">                    
                    <asp:View ID="vLista" runat="server">
                        <section>
                            <asp:Label id="lblHoje" runat="server"></asp:Label>
                            <%# listaPacientes() %>
                        </section>
                    </asp:View>
                    <asp:View ID="vCadastro" runat="server">                        
                            <h1 class="titulo1">Consulta</h1>
                            <div class="agenda-dados-input">
                                <input id="iData" type="text" runat="server" placeholder="Data" disabled="disabled"/>
                                <input id="iHora" type="text" runat="server" placeholder="00:00" />
                                <select id="slHora" runat="server">
                                    <option value="00:00">00:00</option>
                                    <option value="01:00">01:00</option>
                                    <option value="02:00">02:00</option>
                                    <option value="03:00">03:00</option>
                                    <option value="04:00">04:00</option>
                                    <option value="05:00">05:00</option>
                                    <option value="06:00">06:00</option>
                                    <option value="07:00">07:00</option>
                                    <option value="08:00">08:00</option>
                                    <option value="09:00">09:00</option>
                                    <option value="10:00">10:00</option>
                                    <option value="11:00">11:00</option>
                                    <option value="12:00">12:00</option>
                                    <option value="13:00">13:00</option>
                                    <option value="14:00">14:00</option>
                                    <option value="15:00">15:00</option>
                                    <option value="16:00">16:00</option>
                                    <option value="17:00">17:00</option>
                                    <option value="18:00">18:00</option>
                                    <option value="19:00">19:00</option>
                                    <option value="20:00">20:00</option>
                                    <option value="21:00">21:00</option>
                                    <option value="22:00">22:00</option>
                                    <option value="23:00">23:00</option>
                                </select>
                                <input id="iNome" type="text" runat="server" placeholder="Nome" />
                                <label id="lblTipo" runat="server" class="displayOff">Tipo:</label>
                                <select id="stTipo" runat="server" name="TipoAtendimento" class="displayOff">
                                    <option value="Consulta">Consulta</option>
                                    <option value="Avaliação">Avaliação</option>
                                    <option value="Pilates">Pilates</option>
                                </select>
                                <div id="divDados" runat="server" class="displayOff">
                                    <input id="iCpf" type="text" runat="server" placeholder="CPF" />
                                    <input id="iRG" type="text" runat="server" placeholder="RG" />                                
                                    <input id="iConfirma" type="text" runat="server" placeholder="Confirmação" />                            
                                </div>
                                <div>
                                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn1" Text="Procurar" OnClick="procurarParaCadastro" />
                                    <asp:Button ID="btnCadastrar" runat="server" CssClass="btn1" Visible="false" Text="Cadastrar" OnClick="cadastrarPaciente" />
                                    <asp:Button ID="btnCadastrarAgenda" runat="server" CssClass="btn1" Visible="false" Text="Agendar" OnClick="cadastrarAgenda" />
                                </div>
                                <div id="divGvPacientes" runat="server">
                                    <asp:GridView ID="gvPacientes" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" OnSelectedIndexChanged="selecionarRegistroGvPacientes">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="agenda-dados-btn">
                                <asp:Button CssClass="btn1" ID="btnInserir" Visible="false" runat="server" Text="Inserir" OnClick="checarData" />                                        
                            </div>                        
                    </asp:View>                    
                    <asp:View ID="vAvaliacao" runat="server">
                        <h1 class="titulo1">Avaliação</h1>
                        <div class="agenda-dados-input">
                            <input id="iDataAvalia" type="text" runat="server" placeholder="Data" />
                            <input id="iHoraAvaliacao" type="text" runat="server" placeholder="00:00" />
                            <input id="INomeAvalia" type="text" runat="server" placeholder="Nome" />
                            <label id="lblTipoAvalia" runat="server" class="displayOff">Tipo:</label>
                            <select id="stAvaliacao" runat="server" name="TipoAtendimento" class="displayOff">
                                <option value="Consulta">Física</option>
                                <option value="Avaliação">RPG</option>
                                <option value="Pilates">Neuro</option>
                            </select>                            
                            <div>
                                <asp:Button ID="btnBuscarAvaliacao" runat="server" CssClass="btn1" Text="Procurar" OnClick="procurarParaAvaliacao" />
                                <asp:Button ID="btnCadastrarAvaliacao" runat="server" CssClass="btn1" Visible="false" Text="Cadastrar" />                                
                            </div>
                            <div id="divGvAvaliacao" runat="server">
                                <asp:GridView ID="gvPacientesAvaliacao" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" OnSelectedIndexChanged="selecionarRegistroGvAvaliacao">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>                
            </section>
            <section style="width: 200px; min-height: 500px; margin-left: 20px; border: 1px solid #edeaea; float: left;">
                <div style="width: 100%; background-color: #edeaea; color: #454444; text-align: center;">21-2-2024</div>
                <div style="width: 100%; background-color: #edeaea; color: #454444; text-align: center;">Horários Agendados</div>
                <ul>
                    <li>08:00 - 2 pacientes</li>
                    <li>09:00 - 1 pacientes</li>
                    <li>11:00 - 2 pacientes</li>
                    <li>13:00 - 3 pacientes</li>
                    <li>14:00 - 1 pacientes</li>
                    <li>15:00 - 3 pacientes</li>
                </ul>
            </section>
            <section style="width: 100%; height: 20px; float: left">
                <div style="color: #6f91ef; width: 600px; margin-left: 400px; text-align: right;">Usuário: Solange</div>
            </section>
            <section class="agenda-Msg">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </section>          
            <script>
                function atualizar(e) {

                    //alert("Atualizar");

                    //window.location.reload(true);
                    //location.href = "https://localhost:44320/privado/agenda.aspx";
                    //https://awari.com.br/como-atualizar-uma-pagina-com-javascript-guia-completo/?utm_source=blog&utm_campaign=projeto+blog&utm_medium=Como%20atualizar%20uma%20p%C3%A1gina%20com%20JavaScript:%20guia%20completo
                    //https://www.freecodecamp.org/portuguese/news/o-metodo-location-reload-como-recarregar-uma-pagina-em-javascript/   

                }
            </script>
            <!--<input id="iBtnReflesh" type="button" value="Atualizar" onclick="atualizar()" />-->
        </main>
    </form>
</body>
</html>
