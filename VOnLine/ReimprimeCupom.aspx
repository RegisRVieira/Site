<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReimprimeCupom.aspx.cs" Inherits="Site.VOnLine.ReimprimeCupom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reimpressão de Comprovante de Venda</title>
    <link rel="stylesheet" href="../Css/Global.css" />
    <link rel="stylesheet" href="../Css/Global-Fluido.css" />
    <script type="text/javascript" src="../Js/Apoio.js"></script>
    <script type="text/javascript" src="../Js/jQuery 3.4.1.js"></script>
</head>
<body>
    <nav class="navHome-Internas">
        <p>
            <a href="http://www.asu.com.br/Home.aspx">
                <img class="navHome-Internas-Img" src="../Img/Logo ASU-White-Espaçado.png" /></a>
        </p>
    </nav>
    <form id="form1" runat="server">
        <div style="width:50%; min-height: 20px; margin: 0 auto; margin-top: 30px;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:MultiView ID="mvReimprime" runat="server">
            <asp:View ID="vwGridVendas" runat="server">
                <div style="margin: 0 auto; margin-top: 20px; width: 999px; min-height: 20px;">
                    <div style="margin: 10px">                        
                        <%# Titulo() %>
                    </div>
                    <asp:GridView ID="gvVendas" runat="server" AllowPaging="true" OnSelectedIndexChanged="reimprimirVenda">
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" SelectText="Reimprimir" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" BorderColor="White" />
                            <FooterStyle BackColor="#22396f" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#22396f" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" ForeColor="White"/>
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" ForeColor="White"/>
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    <div style="margin-top: 40px;">
                    <asp:LinkButton CssClass="botaoGlobal" ID="encerrarReimpressao" runat="server" Text="Voltar" OnClick="voltarVOConvenio"></asp:LinkButton>
                </div>
                </div>                
            </asp:View>
            <asp:View ID="vwComprovante" runat="server">
                <div style="margin: 0 auto; width: 300px; min-height: 20px;">
                    <div class="left" style="margin-top: 7px; margin-left: 5px;">
                        <a href="#" onclick="PrintElem('#lblAutoriza', 370, 'Comprovante de Venda')">
                            <img style="width: 80px;" src="../Img/Layout/btImprimir.png" border="0" /></a>&nbsp   
                        <br />
                    </div>
                    <asp:Label ID="lblAutoriza" runat="server"></asp:Label>
                    <div>
                        <asp:LinkButton CssClass="botaoGlobal" ID="btnFinaliza" runat="server" Text="Finalizar" OnClick="vontarGridVendas"></asp:LinkButton>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        
    </form>
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
</body>
</html>
