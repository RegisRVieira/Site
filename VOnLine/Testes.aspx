<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testes.aspx.cs" Inherits="Site.VOnLine.Testes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Enviar E-mail</p>
            <input id="iPara" runat="server" type="text" value="reginaldo@asu.com.br" />
            <input id="iCopia" runat="server" type="text" value="regis@asu.com.br" />
            <input id="iAssunto" runat="server" type="text" value="Assunto" />
            <input id="iDados" runat="server" type="text" value="Alterado" />
            <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar E-mail" OnClick="enviarEmail"/>            
        </div>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
             <script>
                 function verMsgJava() {
                     
                     var msg = 'msgErro';
                     alert(msg);
                     event.preventDefault();
                 }
                  //verMsgJava();
            </script>        
        <asp:Label ID="lblScript" runat="server"></asp:Label>
        <!--Janela Modal-->
        <style>
            .modal{
                width: 500px;
                min-height: 500px;
                background-color: #ff6a00;
            }
            .modal-dialog{
                width: 98%;
                min-height: 494px;
                margin: 0 auto;
                opacity: .2;
                background-color: yellow;
            }
            .btn{
                width: 150px;
                min-height: 50px;
            }
            .btn-info{
                background-color: palevioletred;
                border-radius: 8px;                
            }
        </style>
        <div class="modal" id="conteudo-modal">
            <div class="modal-dialog">
                <div class="modal-header">
                    Janela Modal

                </div>
                <div class="modal-body">
                    Aqui vai todo texto

                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal">
                        Fechar
                    </button>
                </div>
                
            </div>
            <button class="btn btn-info" data-toggle="modal" data-target="#conteudo-modal">
                Abrir Janela Modal
            </button>
        </div>

    </form>
</body>
</html>
