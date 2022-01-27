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
    public partial class tsSorteio : System.Web.UI.Page
    {        
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                executarHint();
            }
            this.DataBind();
        }

        protected String executarHint()
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " ts_teste ";
            //string campos = " numero, descricao ";
            string campos = " * ";
            //string condicao = " WHERE descricao = 'teste de gravacao' ";
            string condicao = " WHERE sorteio = '1' ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();
            
            string xRet = "";

            //xRet += "SELECT " + campos + " FROM " + tabela + condicao;
            
            //xRet += "Erro: " + ObjDados.MsgErro;


            //int num = dados.Rows.Count;
            /*
            if (dados.Rows.Count>0)
            {
                int num = dados.Rows.Count;
                xRet += "Tem: " + num;
            }
            else
            {
                xRet += "Erro: " + ObjDados.MsgErro;
            }

            */
            
            xRet += "<figure>";
            //xRet += "<img src = '../img/privado/trevo.png' title=" + "Vamos ver essa bagaça!!!" + "/>";
            //xRet += "<figcaption > Trevo de Quatro Folhas...</figcaption >";
            xRet += "</figure> ";
            xRet += "<br>";
            //xRet += "<span title=" + "Será..." + "Segunda linha..." + " class='tsCursor'>1500</span>";

            int nSorteio = 2000;
            //xRet += "<span class='tooltiptext'><span class='stbNumero'>" + "Número" + dados.Rows[0]["Numero"].ToString() + "</span><br /><span class='stbNome'>" + dados.Rows[0]["Descricao"].ToString() + "</span><br /><span class='stbStatus'>" + "Pago" + "</span></span>";

            xRet += "<section class='secTabNumeros'>";
            
            for (int i = 0; i < dados.Rows.Count; i++)
            {                
                xRet += "<div class='tooltip'>" + dados.Rows[i]["Numero"].ToString();              
                xRet += "<span class='tooltiptext'><span class='stbNumero'>" + "Número: " + dados.Rows[i]["Numero"].ToString() + "</span><br /><span class='stbNome'>" + dados.Rows[i]["descricao"].ToString() + "</span><br /><span class='stbStatus'>" + "Pago" + "</span></span>";
                xRet += "</div>";
            }
            
            xRet += "</section>";


            return xRet;
        }


    }
}