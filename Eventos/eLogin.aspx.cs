using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.App_Code;
using System.Configuration;

namespace Site.Eventos
{
    public partial class eLogin : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Eventos"] != null)
            {
                Session.Abandon();
            }
        }

        protected void logarEventos(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " usuario, senha ";
            ObjDados.Tabela = " e_usuario ";
            ObjDados.Condicao = " WHERE usuario = '" + iUsuario.Value + "' AND senha ='" + iSenha.Value + "'";

            DataTable dados = ObjDados.RetCampos();

            string identifica = "";

            if (ObjDados.MsgErro == "")
            {
                if (Session["Eventos"] == null)
                {
                    if (dados.Rows.Count > 0)
                    {
                        Session.Add("LoginEventos", dados.Rows[0]["usuario"].ToString());                        
                        Response.Redirect("eMenu.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "Usuário ou Senha errado(s), tente novamente!";
                    }
                    Session.Add("Eventos", "Sim");
                }
            }
            else
            {
                lblMsg.Text = "Erro: " + ObjDados.MsgErro;
            }
        }
    }
}