<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropostaConvenio.aspx.cs" Inherits="Site.PropostaConvenio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Proposta de Convênio</title>
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js"></script>
    <script src="Js/jQuery 3.4.1.js"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p>
            <a href="http://www.asu.com.br/Home.aspx">
                <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
        </p>
    </nav>
    <form id="form1" runat="server">
        <style>
            .corpoProposta{
                width: 700px;
                min-height: 30px;
                padding-bottom: 10px;
                margin: 0 auto;
                border: 1px solid #808080;
                border-radius: 6px;
            }
            input{
                width: 670px;
                min-height: 30px;
                display: inline-block;     
                margin-left: 5px;
                margin-top: 10px;
                margin-left: 10px;
                padding-left: 5px;
            }
            .btnBotao1{
                
                
                
                
                text-align: center;
                margin: 0 auto;
                margin-top: 20px;
                border-radius: 12px;                
            }
            .btnBotao1 a{
                background-color: #22396f;
                padding-left: 15px;
                padding-right: 15px;
                padding-top: 5px;
                padding-bottom: 5px;
                font-size: 30px;
                text-decoration: none;
                color: white;
                border-radius: 12px; 
            }
            .btnBotao1 a:hover{
                background-color: #f26907;
            }
            .cabecalho{
                width:800px;
                min-height: 20px;
                background-color: #f6f3f3;
                margin: 0 auto;
                margin-top: 10px;
                margin-bottom: 10px;
            }
            .cabecalho p{
                text-align: center;
                font-size: 40px;
                font-weight: 700;
                font-family: 'Lucida Sans Typewriter';
                color: #22396f;
            }
/*Viwer*/
            .BoxMsgConclusao{
                width: 500px; 
                min-height: 100px; 
                margin: 0 auto;                        
                margin-top: 200px;
            }
            .MsgConclusao{
                width:200px; 
                margin: 0 auto;
            }
            .msgConclusao{
                font-size: 1.5em;
                color: #f26907;
                text-align: center;
            }            
            .msgErro{
                font-size: 1.5em;
                color: #ff0000;
                text-align: center;
            }
            .BoxMsgConclusao img{
                margin: 0 auto;
            }
            .BoxBtnConcluido{
                width: 125px; 
                min-height: 500px; 
                margin: 0 auto;                        
            }
            .botao1{
                padding: 10px 35px;
                background-color: #f26907;
                color: white;
                border-radius: 3px;
            }
            .botao1:hover {
                background-color: #ffd800;
                color: #22396f;
            }

        </style>
        <asp:MultiView ID="mwProposta" runat="server">
            <asp:View ID="vwProposta" runat="server">
                <div class="cabecalho">
            <p>Proposta para Convênio</p>
        </div>
        <div class="corpoProposta">

            <input id="iNome" runat="server" type="text" name="Nome" placeholder="Nome Fantasia" />
            <input id="iRazao" runat="server" type="text" name="Razao" placeholder="Razão Social" />
            <input id="iTel" runat="server" type="tel" name="Telefone" placeholder="Telefone"  />
            <input id="iCel" runat="server" type="tel" name="Celular" placeholder="Celular" />
            <input id="iEmail" runat="server" type="email" name="Email" placeholder="e-mail" />            
            <input id="iRamo" runat="server" type="text" name="Ramo" placeholder="Ramo de Atividade" />
            <input id="iContato" runat="server" type="text" name="contato" placeholder="Contato" />
            <div class="btnBotao1">
                <asp:LinkButton ID="lbtEnviar" runat="server" Text="Enviar Proposta" OnClick="enviarProposta"></asp:LinkButton>
            </div>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.js"></script>
            <script>
                $('#iTel').mask('(00) 0000-0000');
                $('#iCel').mask('(00) 0 0000-0000');
            </script>
        </div>
            </asp:View>
            <asp:View ID="vwMsg" runat="server">
                <div class="BoxMsgConclusao">
                    <div style="margin: 0 auto; width: 100%;">
                        <img style="width: 50px;" src="../Img/layout/img_enviado.png" />
                    </div>
                    <div style="margin-top: 10px; margin-bottom: 50px;">
                        <asp:Label ID="lblMsg" runat="server"  CssClass="msgConclusao"></asp:Label>                    
                    </div>
                </div>
                <div class="BoxBtnConcluido">
                    <asp:LinkButton ID="lbtconcluir" runat="server" Text="Concluir" CssClass="botao1" OnClick="voltarHome"></asp:LinkButton>
                </div>
            </asp:View>
        </asp:MultiView>
    </form>
    <div style="margin: 0 auto;  margin-top: 10px; margin-bottom: 50px; width: 700px; min-height: 20px;">
        <asp:Label ID="lblMsgErro" runat="server"  CssClass="msgErro"></asp:Label>
    </div>
</body>
</html>
