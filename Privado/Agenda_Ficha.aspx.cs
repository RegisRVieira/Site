using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Privado
{
    public partial class Agenda_Ficha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBind();
            }

        }

        public String dia()
        {
            string dia = DateTime.Now.Day.ToString();

            return dia;
        }//dia
    }
}