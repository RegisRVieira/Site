<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTestes.aspx.cs" Inherits="Site.Privado.eTestes" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
        ul li{
            width: 60px;
            height: 20px;
            padding: 10px 0 10px;
            margin: 5px;
            list-style: none;
            display: inline-block;
            border: 1px solid #f26907;
            text-align: center;
            border-radius: 6px;
            cursor: pointer;
        }
        .dvNumero{
            width: 60px;
            height: 20px;
            padding: 10px 0 10px;
            margin: 5px;
            list-style: none;
            display: inline-block;
            border: 1px solid #f26907;
            text-align: center;
            border-radius: 6px;
            cursor: pointer;     
            color: white;
        }
        .tbTexto{
            width: 500px;
            min-height: 50px;
        }
    </style>
    <title>Testes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <h1>Gerando por C#</h1>
                <%# gerarNumeros() %>
            </div><br /><br />
            <ul id="numeros">
                <li>001</li>
                <li>002</li>
                <li>003</li>
                <li>004</li>
                <li>005</li>
            </ul>
            <section>
                <h1>Números Selecionados</h1>
                <div id="dvSelecao" runat="server">
                    <label id="lblSelecao" runat="server" ></label>
                </div>
            </section>
        </div>
        <script>
            //var click = document.querySelectorAll("li");

            //click.onclick = function () {
            var click = numeros;
            var id;
            var nSel = new Array();
            click.onclick = function () {                
                //alert("Você clicou - HTML");

                var x = prompt("Digite!");                
                nSel.push(x);

                dvSelecao.innerHTML = nSel;
            }
        </script>
        <div>
            <h1>ValidateRequest</h1>
            <asp:TextBox ID="tbTexto" runat="server" TextMode="MultiLine" CssClass="tbTexto"></asp:TextBox><br />            
            <asp:Button ID="btnGravar" runat="server" Text="Gravar" OnClick="GravarHTML" />
        </div>
        <asp:Label id="lblResult" runat="server">aQUI</asp:Label>
    </form>
</body>
</html>
