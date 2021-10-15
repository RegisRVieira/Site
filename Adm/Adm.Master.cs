using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Adm
{
    public partial class Adm : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                usuarioLogado();
            }

        }

        public void usuarioLogado()
        {
            string usuario = "";

            if (Session["LoginAdm"] != null)
            {
                usuario = Session["LoginUsuario"].ToString();

                string primeiroNome = usuario.Split(' ').FirstOrDefault();
                string primeiraLetra = usuario.Split(' ').FirstOrDefault();
                int tNome = primeiroNome.Length;

                //xRet += "" + primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome-1)).ToLower();
                lblUsuario.Text = primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome - 1)).ToLower();

            }
            else
            {
                lblUsuario.Text = "Entrar";
            }



            //return xRet;
        }
    }
}