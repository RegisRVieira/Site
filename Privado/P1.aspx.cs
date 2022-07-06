using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Privado
{
    public partial class P1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void executarQS(object sender, EventArgs e)
        {
            Response.Redirect("p2.aspx?id=" + iIdAssoc.Value);
        }
    }
}