using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Site.App_Code;
using System.Configuration;
using System.Data;

namespace Site.Privado
{
    public partial class pLogin : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Privado"] != null)
            {
                Session.Abandon();
            }

        }

        protected void logarPrivado(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " p_usuario ";
            string campos = " Nome, Senha ";
            string condicao = " WHERE nome = '" + iUsuario.Value + "'" + " AND senha = '" + iSenha.Value + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            ObjDados.RetCampos();

            DataTable dados = ObjDados.RetCampos();

            if (Session["Privado"] == null)
            {
                if (dados.Rows.Count > 0)
                {
                    Session.Add("LoginPrivado", dados.Rows[0]["Nome"].ToString());
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMsg.Text = "Usuário ou Senha errado(s), tente novamente!";
                }

                Session.Add("Privado","Sim");

                
            }


        }
    }
}