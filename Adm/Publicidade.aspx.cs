using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Site.App_Code;
using System.Windows.Forms;

namespace Site.Adm
{
    public partial class Publicidade : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public void ativaViews(int ativa)
        {
            mwGridPublicidade.ActiveViewIndex = ativa;
            mwFormPublicidade.ActiveViewIndex = ativa;
        }

        protected void ativaVwPublicidade(object sender, EventArgs e)
        {
            ativaViews(0);
        }

        protected void ativaVwGuiaDestaque(object sender, EventArgs e)
        {
            ativaViews(1);
        }

        protected void ativaVwGuiaLogo(object sender, EventArgs e)
        {
            ativaViews(2);
        }



        /*Fim*/
    }
}