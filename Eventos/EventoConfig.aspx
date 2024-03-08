<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventoConfig.aspx.cs" Inherits="Site.Eventos.EventoConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Configura Eventos</title>
    <link rel="stylesheet" href="Css/Global.css" />
</head>
<body>
    <form id="form1" runat="server">
        <style>
            .secBotoes {
                margin-left: 20px;
                margin-right: 5px;
                width: 300px;
                min-height: 300px;
                /*background-color: #0094ff;*/
                float: left
            }
            .secBotoes p{
                margin: 0;
                padding: 0;
                margin-top: 10px;
                margin-bottom: 20px;
                width: 100%;
                font-size: 24px;
                text-align: center;
                color: white;
                background-color: #f26907;
                background-color: #0094ff;
            }
            .secViews{
                width: 420px;
                min-height: 298px;
                border: 1px solid #0094ff;
                float: left;
            }
            .secViews p{
                margin: 0;
                padding: 0;
                margin-top: 10px;
                background-color: #0094ff;
                text-align: center;
                color: white;
                font-size: 24px;
                font-weight: 700;
                width: 405px;
                margin-left: 5px;
            }

            .botaoUm a{
                margin: 2px auto;                               
                width: 200px;
                min-height: 20px;                
                display: block;
                text-decoration: none;                
                background-color: #4800ff;
                background-color: #0094ff;                
                color: white;
                font-weight: 700;
                text-transform: uppercase;
                border: 1px solid #ffd800;
                border-radius: 6px;                
                text-align: center;
                line-height: 40px;                
            }
            
            .botaoUm a:hover{
                background-color: #ffd800;
                color: #0094ff;
                border: 1px solid #0094ff;
            }
            
            input{                
                width: 400px;
                min-height: 20px;
                line-height: 30px;
                border-radius: 6px;
                display: block;
                margin: 5px;
            }
            #ddlEventoTipo, #ddlListaEventos, #stAmbiente, #stEvento, #stFinanEvento{                
                width: 400px;
                min-height: 20px;
                line-height: 30px;
                border-radius: 6px;
                display: block;
                margin: 5px;
            }
            .gradialUm{
                background-image: linear-gradient(to right, #ffd800, #ffc481, #ffcdda, #ffe9ff, #ffffff);
                background-image: linear-gradient(to right, #ffd800, #ffd800, #ffd800, #ffd800, #ffd800, #ebe234, #d9ea53, #c9f06f, #b1faa8, #b2fdd7, #cefdf3, #f8f9f9);
                background-image: linear-gradient(to right, #ffd800, #fdd918, #fbd925, #fada2f, #f8da38, #f6db4a, #f5dd59, #f3de67, #f1e07f, #f0e296, #eee4ac, #ece6c2);
                 background-image: linear-gradient(to right, #ffd800, #fdd918, #fbd925, #fada2f, #f8da38, #f7dc4c, #f7df5e, #f6e16e, #f6e68b, #f7eaa7, #f6eec2, #f6f2dd);
                line-height: 50px;
                font-size: 30px;
                color: white;
                padding-left: 20px;
            }
            textarea{
                width: 400px;
                min-height: 50px;
                line-height: 30px;
                border-radius: 6px;
                display: block;
                margin: 5px;
            }
            
        </style>

        <section class="corpoDois">
            <p class="gradialUm">Configuração dos Eventos</p>
            <section>
                <p>GridView, exibir eventos cadastrados</p>
            </section>
            <section class="secBotoes">
                <div class="botaoUm">
                    <p>Cadastro</p>
                    <asp:LinkButton ID="lbtnEvento" runat="server" Text="Evento" OnClick="ativarEventos" ></asp:LinkButton>
                    <asp:LinkButton ID="lbtnAmbiente" runat="server" Text="Ambiente" OnClick="ativarAmbiente"></asp:LinkButton>                
                    <asp:LinkButton ID="lbtnMesa" runat="server" Text="Mesas" OnClick="ativarMesas"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnFinanceiro" runat="server" Text="Financeiro" OnClick="ativarFinanceiro"></asp:LinkButton>
                    <a href="Mesa.aspx">Mesa</a>
                    <a href="Pagamento.aspx">Pagamento</a>
                </div>
                <div>
                    <a href="eMenu.aspx" ><span>Voltar</span></a>
                </div>
            </section>
            <section class="secViews">
                <asp:MultiView ID="mwConfigEventos" runat="server">
                    <asp:View ID="vwEventos" runat="server">
                        <p>Eventos</p>
                        <div style="margin-bottom: 10px;">
                            <label style="font-size: 12px; color: #0094ff; margin-left: 10px;">Tipo</label>
                            <asp:DropDownList ID="ddlEventoTipo" runat="server"></asp:DropDownList>
                        </div>
                        <input id="iEventoTitulo" runat="server" type="text" placeholder="Título do Evento" />
                        <input id="iEventoLocal" runat="server" type="text" placeholder="Local" />
                        <textarea id="taEventoDetalhe" runat="server" placeholder="Detalhes sobre o evento"></textarea>
                        <asp:TextBox ID="tbEventoData" runat="server" type="Date"></asp:TextBox>
                        <input id="iEventoHoraIni" runat="server" type="text" placeholder="20:00" maxlength="5"/>
                        <input id="iEventoHoraFim" runat="server" type="text"  placeholder="04:00" maxlength="5"/>
                        <div class="botaoUm" >
                            <asp:LinkButton ID ="lbtnGravarEvento" runat="server" Text="Gravar" OnClick="gravarEvento" ></asp:LinkButton>
                        </div>
                    </asp:View>
                    <asp:View ID="vwAmbiente" runat="server">
                        <p>Ambiente</p>
                        <asp:DropDownList ID="ddlListaEventos" runat="server"></asp:DropDownList>
                        <input id="iAmbienteTitulo" runat="server" type="text" placeholder="Título do Ambiente" />
                        <input id="iAmbienteDescricao" runat="server" type="text" placeholder="Descrição" />                                     
                        <textarea ID="tbAmbienteDetalhes" runat="server" placeholder="Detalhes"></textarea>   
                        <div class="botaoUm">
                            <asp:LinkButton ID="lbtnGravarAmbiente" runat="server" Text="Gravar" OnClick="gravarAmbiente"></asp:LinkButton>
                        </div>
                    </asp:View>
                    <asp:View ID="vwMesas" runat="server">
                        <p>Mesas</p>                        
                        <asp:DropDownList ID="stEvento" runat="server" OnSelectedIndexChanged="identificarEvento" AutoPostBack="true"></asp:DropDownList>
                        <asp:DropDownList ID="stAmbiente" runat="server"></asp:DropDownList>                        
                        <input id="iMesasDescricao" runat="server" type="text" placeholder="Descrição" />                        
                        <textarea id="iMesasObserva" runat="server" placeholder="Observação"></textarea>
                        <input id="iMesaNumIni" runat="server" type="text" placeholder="001" required=""/>
                        <input id="iMesaNumFin" runat="server" type="text" placeholder="100" required=""/>
                        <input id="iMesaCadeiras" runat="server" type="text" placeholder="Número de Cadeiras" maxlength="2" required=""/>
                        <div class="botaoUm">
                            <asp:LinkButton ID="lbtnGravarMesas" runat="server" Text="Gravar" OnClick="gravarMesas"></asp:LinkButton>
                        </div>
                    </asp:View>
                    <asp:View ID="vwFinanceiro" runat="server">
                        <p>Financeiro</p>
                        <select id="stFinanEvento" runat="server"></select>
                        <input id="iCustoTitular" runat="server" placeholder="Custo do Assciado" />
                        <input id="iCustoDependente" runat="server" placeholder="Custo do Dependente" />
                        <input id="iCustoConvidado" runat="server" placeholder="Custo do Titular" />                        
                    </asp:View>
                </asp:MultiView>
            </section>            
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
         <section>
            <div style="width: 280px; float: right;">
                <p style="color: #fbb888;">
                    Você está logado como:
                    <asp:Label ID="lblLogado" runat="server" CssClass="corLogin"></asp:Label>&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lbtEncerrar" runat="server" Text="Sair" OnClick="encerrarLogin"></asp:LinkButton>
                </p>
            </div>
        </section>
        </section>
       
    </form>
</body>
</html>
