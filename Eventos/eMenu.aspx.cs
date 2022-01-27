using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Site.App_Code;
using System.Net;

namespace Site.Eventos
{
    public partial class eMenu : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                checarSessao();
                mostrarLogado();
            }
        }

        protected void abrirBrindes(object sender, EventArgs e)
        {
            Response.Redirect("Brindes.aspx");
        }
        protected void abrirFestas(object sender, EventArgs e)
        {
            lblMsg.Text = "Em construção, aguarde!!!";
        }
        protected void abrirConfig(object sender, EventArgs e)
        {
            lblMsg.Text = "Em construção, aguarde!!!";
        }

        public void mostrarLogado()
        {
            Apoio ObjApoio = new Apoio();
            string identifica = "";

            identifica = Session["LoginEventos"].ToString();

            string primeiroNome = identifica.Split(' ').FirstOrDefault();
            string primeiraLetra = identifica.Split(' ').FirstOrDefault();
            int tNome = primeiroNome.Length;

            lblLogado.Text = primeiraLetra.Substring(0, 1).ToUpper() + primeiroNome.Substring(1, (tNome - 1)).ToLower();

            string IP = "";
            IP = Request.UserHostAddress;

            //lblMsg.Text = IP;

        }
        protected void encerrarLogin(object sender, EventArgs e)
        {
            if (Session["LoginEventos"] != null)
            {
                Session.Abandon();
            }

            Response.Redirect("eLogin.aspx");
        }
        protected void checarSessao()
        {
            string identifica = "";

            if (Session["LoginEventos"] != null)
            {
                identifica = Session["LoginEventos"].ToString();
            }
            else
            {
                Response.Redirect("eLogin.aspx");
            }
        }

        protected void realizarLogof()
        {
            Session.Abandon();
            Response.Redirect("eLogin.aspx");



            string usuario = "";

            if (Session["Eventos"] != null)
            {
                usuario = Session["LoginEventos"].ToString();

                string primeiroNome = usuario.Split(' ').FirstOrDefault();
                string primeiraLetra = usuario.Split(' ').FirstOrDefault();
                int tNome = primeiroNome.Length;

                //xRet += "" + primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome-1)).ToLower();
                //lblUsuario.Text = primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome - 1)).ToLower();

            }
            else
            {
                //lblUsuario.Text = "Entrar";
            }
        }
    }
}