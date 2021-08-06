<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertRegistro.aspx.cs" Inherits="Site.Adm.InsertRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inserir Registro</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Inserir Registro</h1>
            <a href="../Home.aspx">Home</a>
            <input id="iNome" type="text" runat="server" placeholder="Nome" required="required"/>
            <input id="iUsuario" type="text" runat="server" placeholder="Usuário" required="required" />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="cadastrarEmpresa" />
        </div>
    </form>
</body>
</html>
