﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Privado
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                if (Session["LoginPrivado"] != null ) {
                    lblLogado.Text = "Você está Logado como: " + Session["LoginPrivado"].ToString();
                }
            }
        }
    }
}