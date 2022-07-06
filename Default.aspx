<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Teste de Conexão</title>
    <style>
        .form {
            margin: 0 auto;
            padding: 0;
            width: 70%;
            height: auto;
            background-color: bisque
        }

            .form h1 {
                text-align: center;
                background-color: burlywood;
            }

            .form p, select, input {
                padding-left: 20px;
            }

            .form select, input {
                margin-left: 20px;
            }

        .label {
            margin-left: 0px;
            color: #2e3a67;
            font-size: 1.5em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form">
            <h1>Bem Vindo</h1>
            <p>Vamos testar a conexão com o Banco</p>
            <asp:DropDownList ID="ddlEscolha" runat="server">
                <asp:ListItem Value="0" Text="ASU"></asp:ListItem>
                <asp:ListItem Value="1" Text="Vegas"></asp:ListItem>
            </asp:DropDownList><br />
            <br />
            <asp:Button ID="btnEscolher" runat="server" Text="Teste a Conexão" OnClick="escolherConexao" /><br />
            <br />
            <asp:Button ID="btnApenas" runat="server" Text="Apenas escolher uma opção" OnClick="apenasEscolher" /><br />
            <br />
            <asp:Label ID="lblResp" runat="server" CssClass="label"></asp:Label>
            <asp:Label ID="lblErro" runat="server"></asp:Label>
            <div>
                <style>
                    /*Menu Drop Down*/
                    .mdd ul {
                        margin: 0;
                        padding: 0;
                        list-style: none;
                    }

                        .mdd ul li {
                            float: left;
                            position: relative;
                        }
                            /*Menu Horizontal Principal*/
                            .mdd ul li a {
                                display: inline-block;
                                text-decoration: none;
                                color: darkgray;
                                background-color: #FFF;
                                padding: 5px 10px;
                                border: 1px solid darkgray;
                                margin-right: -5px;
                                width: auto;
                            }
                            /*SubMenu*/
                            .mdd ul li ul {
                                display: none;
                                position: absolute;
                                padding-top: 2px;
                            }
                                /*Itens do Sub Menu*/
                                .mdd ul li ul li a {
                                    width: 120px;
                                    margin-top: -1px;
                                }

                                    .mdd ul li ul li a:hover {
                                        background-color: #ddd;
                                    }
                    /*Pulo do Gato*/
                    .mdd li:hover ul {
                        display: block;
                    }
                    /*Altera a cor de BackGround dos Itens do elemento*/
                    .mdd ul li a:hover {
                        background-color: #ddd;
                    }
                </style>
                <div class="mdd">
                    <ul>
                        <li><a href="#">Home</a>
                            <ul>
                                <li><a href="Home.aspx">Home</a></li>
                            </ul>
                        </li>
                        <li><a href="#">LayOuts</a>
                            <ul>
                                <li><a href="LayOuts/LayOut-Home.aspx">LayOut Home</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div>
            <h1>Você OnLine</h1>
            <a href="LoginVoceOnLine.aspx">Você OnLine</a>
        </div>
        <div style="width: 600px; height: 300px; border: 1px solid #f26907; background-color: #ffd800">
            <asp:Button ID="btnTesBtn" runat="server" Text="Executar"  />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
