<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaleConosco.aspx.cs" Inherits="Site.FaleConosco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Fale Conosco</title>
    <link rel="stylesheet" href="Css/Form-Clean.css" />
    <link rel="stylesheet" href="Css/Form-Fluido.css" />
    <link rel="stylesheet" href="Css/Global.css" />
    <link rel="stylesheet" href="Css/Global-Fluido.css" />
    <script src="Js/Apoio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <section style="margin-bottom: 30px;">
            <nav class="navHome-Internas">
                <div>
                    <p>
                        <a href="Home.aspx">
                            <img class="navHome-Internas-Img" src="Img/Logo ASU-White-Espaçado.png" /></a>
                    </p>
                </div>
                <div runat="server" class="BoxVOLogin">
                    <div style="color: white; float: right">
                        <img src="Img/icon/usuLogin.svg" />
                        <div class="BoxVOLoginMenu">
                            <ul>
                                <li>
                                    <asp:Label ID="lblUsuLogado" runat="server" CssClass="lblUsuLogado"></asp:Label>
                                    <ul>
                                        <li>Sobre</li>
                                        <li>Voltar à Home</li>
                                        <asp:LinkButton ID="lbtDeslogar" runat="server" Text="Sair" OnClick="fazerLogof"><li>Sair</li></asp:LinkButton>
                                    </ul>
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
        </section>
        <section class="corpoFaleConsoco">
            <h2>Envie Mensagem para ASU</h2>
            <input type="text" runat="server" id="enviar_nome" name="nome" maxlength="60" required="" placeholder="Nome" autofocus="">
            <input type="email" runat="server" id="enviar_email" name="email" placeholder="E-mail" required="">
            <input type="tel" runat="server" id="enviar_telefone" name="telefone" placeholder="Telefone">
            <input type="text" runat="server" id="enviar_empresa" name="empresa" placeholder="Empresa">
            <label class="lblTitRadio">Com quem você quer conversar:</label><br>
            <input runat="server" type="radio" style="font-size: 3em; color: red;" id="destino_atendimento" name="destino" value="Atendimento" checked="">
            <label for="Atendimento">Atendimento</label><br>
            <input runat="server" type="radio" id="destino_convenio" name="destino" value="Convênios">
            <label for="Convênios" >Convênios</label><br>
            <input runat="server" type="radio" id="destino_financeiro" name="destino" value="Financeiro">
            <label for="Financeiro">Financeiro</label><br>
            <input runat="server" type="radio" id="destino_juridico" name="destino" value="Jurídico">
            <label for="Jurídico">Jurídico</label><br>
            <input runat="server" type="radio" id="destino_secretaria" name="destino" value="Secretaria">
            <label for="Secretaria">Secretaria</label><br>
            <input runat="server" type="radio" id="destino_jornal" name="destino" value="Jornal">
            <label for="Jornal">Jornal</label><br>
            <br>
            <label for="Assunto" id="lblAssundo" class="lblTitRadio" runat="server">Assunto</label>
            <!-- Lista -->
            <select name="assunto" id="enviar_assunto" runat="server" class="stAssunto">
                <option value="Dúvida" runat="server">Dúvida</option>
                <option value="Sugestão" runat="server" selected="">Sugestão</option>
                <option value="Reclamação" runat="server">Reclação</option>
                <option value="Outros" runat="server">Outros</option>
            </select>
            <label for="mensagem" class="lblTitRadio">Mensagem</label>
            <textarea id="corpo_mensagem" runat="server" maxlength="600" placeholder="Mensagem" class="taMensagem"></textarea>
            <asp:Button ID="btnFaleConosco" runat="server" Text="Fale Conosco" OnClick="EMailFaleConosco" />
            <asp:Label ID="lblResultado" runat="server"></asp:Label>
            <asp:Label ID="lblFalta" runat="server"></asp:Label>
        </section>
        <section id="margemRodape"></section>
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
