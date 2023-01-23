using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
namespace Site.Privado
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*if (Session["LoginPrivado"] != null ) {
                    lblLogado.Text = "Você está Logado como: " + Session["LoginPrivado"].ToString();
                }*/
                mostrarLogado();
            }
        }

        public void mostrarLogado()
        {
            Apoio ObjApoio = new Apoio();
            string identifica = "";

            identifica = Session["LoginPrivado"].ToString();

            string primeiroNome = identifica.Split(' ').FirstOrDefault();
            string primeiraLetra = identifica.Split(' ').FirstOrDefault();
            int tNome = primeiroNome.Length;

            if (Session["LoginPrivado"] != null)
            {
                lblLogado.Text = primeiraLetra.Substring(0, 1).ToUpper() + primeiroNome.Substring(1, (tNome - 1)).ToLower();
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~pLogin.aspx");
            }

            string IP = "";
            IP = Request.UserHostAddress;

            //lblMsg.Text = IP;

        }
        protected void encerrarLogin(object sender, EventArgs e)
        {
            if (Session["Privado"] != null)
            {
                Session.Abandon();
            }

            Response.Redirect("pLogin.aspx");
        }
    }
}