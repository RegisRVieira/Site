using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class CompVenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string xCompVenda { get; set; }

        public String exibirCompVenda()
        {
            string venda = Request.QueryString["venda"];

            lblCompVenda.Text = venda;

            return lblCompVenda.Text;
        }

    }
}