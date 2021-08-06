using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;

namespace Site.Adm
{
    public partial class LoginAdm : System.Web.UI.Page
    {
        static string UsuarioLogin { get; set; }

        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
                
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginAdm"] = null;

        }

        protected void acessarAdmin(object sender, EventArgs e)
        {
            BLL ObjAdm = new BLL(conectSite);
            
            string NLogin = iNome.Value.ToUpper();
            string SLogin = iSenha.Value.ToUpper();

            ObjAdm.Tabela = " st_usuario ";
            ObjAdm.Campo = " nome, usuario, senha ";
            ObjAdm.Condicao = " WHERE usuario = '" + NLogin + "' AND senha = '" + SLogin + "'";

            DataTable dados = ObjAdm.RetCampos();
            int contador = dados.Rows.Count;

            if (contador > 0)
            {
                Session.Add("LoginAdm", "Sim");
                Session.Add("LoginUsuario", dados.Rows[0]["usuario"].ToString());
                Response.Redirect("Admin.aspx");

                //Response.Redirect("InsertRegistro.aspx");

                //MessageBox.Show(dados.Rows[0]["usuario"].ToString());
            }
            else
            {
                MessageBox.Show("Usuário e/ou Senha, incorreto(s)!!!");
            }
        }
    }
}