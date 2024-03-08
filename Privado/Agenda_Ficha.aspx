<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenda_Ficha.aspx.cs" Inherits="Site.Privado.Agenda_Ficha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ficha do Paciente</title>
</head>
<body>
    <style>
        html, body{
            margin: 0;
            padding: 0;
        }
        .ficha-main{
            width: 100%;
            min-height:900px;
            /*background-color: #23396f;*/
        }
        .ficha-header {
            width: 100%;
            min-height: 70px;
            border-top: 6px solid #edeaea;
            border-bottom: 2px solid #edeaea;
            margin-bottom: 10px;
        }

        .ficha-header-dia {
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

        .ficha-header-dia-dia {
            text-align: center;
            font-size: 22px;
            padding: 0;
            margin-top: 2px;
            color: #1552f8;
        }

        .ficha-header-titulo {
            width: 100%;
            height: auto;
            padding-top: 16px;
            padding-bottom: 16px;
            font-size: 2em;
            color: #1552f8;
        }

        .ficha-header-titulo-b {
            width: 100%;
            height: auto;
            padding-top: 25px;
            padding-bottom: 16px;
            font-size: 1.5em;
            color: #1552f8;
        }
        .ficha-body{
            width: 80%;
            min-height: 900px;
            /*background-color: #ff6a00;*/
            margin: 0 auto;
        }
        .ficha-body-dados{
            width: 90%;
            min-height: 125px;
            border: 1px solid #e0dfdf;
            margin: 0 auto;
            padding: 5px;
        }
        .ficha-body-agenda{
            width: 90%;
            min-height: 200px;
            border: 1px solid #e0dfdf;
            margin: 0 auto;
            padding: 5px;
        }
        .ficha-body-avalia{
            margin: 2px;
            width: 49.5%;
            min-height: 400px;
            border: 1px solid #e0dfdf;
            float: left;
        }
        .ficha-body-historico{
            margin: 2px;
            width: 49.5%;
            min-height: 400px;
            border: 1px solid #e0dfdf;
            float: left;
        }
        .ficha-body-grupo{
            width: 90%;
            min-height: 406px;
            border: 1px solid #e0dfdf;
            margin: 0 auto;
            padding: 5px;
            margin-top: 5px;
            margin-bottom: 5px;
            
        }
    </style>
    <form id="form1" runat="server">
        <section class="ficha-main">
            <section class="ficha-header">
                <div style="width: 10%; min-height: 70px; float: left">
                    <div class="ficha-header-dia">
                        <p class="ficha-header-dia-dia" id="dia"><%# dia() %></p>
                    </div>
                </div>
                <div style="width: 13%; min-height: 70px; float: left;">
                    <div class="ficha-header-titulo">
                        Ficha do paciente:
                    </div>
                </div>
                <div style="width: 75%; min-height: 70px; float: left">
                    <div class="ficha-header-titulo-b">
                        José da Silva
                    </div>
                </div>
            </section>
            <section class="ficha-body">
                <style>
                    .foto {
                        width: 80px;
                        min-height: 90px;
                        margin-top: 5px;
                        margin-left: 5px;
                        background-color: #edeaea;
                        border-radius: 180px;
                        float: left;
                    }
                    .foto img{
                        width: 80px;
                        min-height: 90px;
                        margin-top: 5px;
                        margin-left: 5px;
                        background-color: #edeaea;
                        border-radius: 180px;
                    }
                    .dados{
                        margin-left: 20px;
                        margin-top: 30px;
                        width: 400px;
                        min-height: 50px;
                        /*background-color: #1552f8;                        */
                        float: left;
                    }
                    .dados p{
                        padding: 0;
                        margin: 0;
                        padding-left: 20px;
                        
                    }
                    .dolado{
                        display: inline-block;
                    }
                </style>
                <div class="ficha-body-dados">
                    <div class="foto">
                        <img src="/Img/Jose da Silva.jpg"/>                        
                    </div>
                    <div class="dados">
                        <p>José da Silva</p>
                        <p>45 anos</p>
                        <p>CPF: 999.999.999-99</p>
                        <p>(14) 9-9999-9999</p>
                    </div>
                </div>
                <div class="ficha-body-agenda">
                    <h1>Agenda</h1>
                </div>
                <div class="ficha-body-grupo">
                    <style>
                        h1{
                            background-color: #e0dfdf;
                            margin: 0;
                            color: #808080;
                            font-size: 1.5em;
                        }
                    </style>
                    <div class="ficha-body-avalia">
                        <h1>Avaliação</h1>
                    </div>
                    <div class="ficha-body-historico">
                        <h1>Histórico</h1>
                    </div>
                </div>
                
            </section>
        </section>
    </form>
</body>
</html>
