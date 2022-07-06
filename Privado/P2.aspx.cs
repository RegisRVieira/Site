using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Site.App_Code;

namespace Site.Privado
{
    public partial class P2 : System.Web.UI.Page
    {
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.DataBind();
        }

        public String metodoQueryString()
        {
            BLL ObjDados = new BLL(conectVegas);

            string idAssoc = Request.QueryString["id"];

            string xRet = "";



            string tabela = " associa ";
            string campos = " * ";
            string condicao = " WHERE idassoc = '"+ idAssoc + "' ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            if (String.IsNullOrEmpty(idAssoc))
            {
                xRet += "Cadê o ID????";
            }
            else
            {

                if (ObjDados.MsgErro == "")
                {
                    xRet += "<p>" + dados.Rows[0]["titular"].ToString() + "</p>";
                }
                else
                {
                    xRet += "Deu Pau!" + ObjDados.MsgErro.ToString();
                }
            }      

            return xRet;
        }
    }
}