<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaSaldo.aspx.cs" Inherits="Site.ConsultaSaldo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Consulta Saldo</title>
    <link rel="stylesheet" href="Css/Botoes.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/StyleEventos.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;" >
            <script>
                function datas() {

                    var agora = new Date();

                    var mes = "";                                        

                    //Coloca 0 no mês menos que 10
                    if (agora.getMonth() < 10) {
                        mes = "0" + (agora.getMonth() + 1);
                    } else {
                        mes = (agora.getMonth() + 1);
                    }                    

                    alert(agora.getFullYear());

                    tbDtIni.value = agora.getFullYear() + "-" + mes + "-20";
                    tbDtFim.value = agora.getFullYear() + "-" + mes + "-19";
                }

                function atualizaData() {
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

                    //tbDtIni.value = "2023-02-19";
                    //tbDtFim.value = "2023-03-20";

                    // Verifica o dia do Mês para gerar o período a ser carregado no Calendário
                    if (tbDtIni.value == "") {//Executa apenas se o calendário não estiver preenchido

                        if (dia <= 31) {
                            dia_ini = "20";
                            dia_fin = "19";

                            if (agora.getMonth() < 10) {
                                var Calendar_ini = agora.getFullYear() + "-" + "0" + (agora.getMonth()) + "-" + dia_ini;
                            } else {
                                var Calendar_ini = agora.getFullYear() + "-" + (agora.getMonth()) + "-" + dia_ini;
                            }

                            var Calendar_fin = agora.getFullYear() + "-" + mes + "-" + dia_fin;

                            tbDtIni.value = Calendar_ini;
                            tbDtFim.value = Calendar_fin;

                            //alert("<=10");

                        }


                    }

                    //alert(wCalendar + " Mês: " + mes + " - Dia: " + dia);
                }
            </script>
            <div style="width: 150px; float: left">
                <asp:Label>Início:</asp:Label>
                <asp:TextBox ID="tbDtIni" runat="server" type="date"></asp:TextBox>
            </div>
            <div style="width: 150px; float: left">
                <asp:Label>Fim:</asp:Label>
                <asp:TextBox ID="tbDtFim" runat="server" type="date"></asp:TextBox>
            </div>
        </div>
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;" >
            <input id="iIdAssoc" class="btnBusca1" type="text" runat="server" placeholder="Id do Associado" />
            <div style="text-align: center;">
            <asp:LinkButton ID="lbtnConsultar" CssClass="Botao1" runat="server" Text="Consultar Saldo" OnClick="consultarSaldoNovo"></asp:LinkButton>
            </div>
        </div>        
        <!--
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;" >
            <p>Tratar Datas para Emissão do Saldo</p>
            <div style="width: 50%">
                <asp:LinkButton ID="lbtnDatas" runat="server" CssClass="Botao2" Text="Datas" OnClick="executarDatas"></asp:LinkButton>
            </div>
        </div>
        <div style="width: 600px; min-height: 100px; margin: 0 auto; margin-top: 20px;">
            <p>Saldo com Data Início e Data Fim</p>
            <div  style="width: 50%">
                <asp:LinkButton ID="lbtnConsultaSaldo" CssClass="Botao1" runat="server" Text="Cosultar Saldo" OnClick="executarSaldo"></asp:LinkButton>
            </div>
        </div>
        -->
        <div style="width: 700px; margin: 0 auto;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <script>
            atualizaData();
        </script>
    </form>
</body>
</html>
