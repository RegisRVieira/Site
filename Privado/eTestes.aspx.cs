using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Site.App_Code;
using System.Data;
using System.Configuration;

namespace Site.Privado
{
    public partial class eTestes : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        
        protected void Page_Load(object sender, EventArgs e)
        {

            this.DataBind();

            if (Page.IsPostBack)
            {
                //                selecionarNumeros(idNum);
            }
        }

        public string xRet { get; set; }
        public string nSelecionados { get; set; }
        public string idNum { get; set; }
        public string idQs { get; set; }
        public string click { get; set; }


        public String gerarNumeros()
        {
            xRet = "";
            nSelecionados = "";

            idNum = "";
            idQs = "";

            for (int i = 1; i < 16; i++)
            {
                idNum = "click" + i;
                idQs = "#click" + i;
                xRet += "<div id='" + idNum + "' style='background-color: #22396f;' class='dvNumero'>" + i + "</div>";
            }
            xRet += "<script>";
            xRet += idNum + "= document.querySelector('" + idQs + "');";
            xRet += "var nSel = new Array();";
            xRet += idNum + ".onclick = function() {";//Início Função
            /*
            xRet += "alert('";
            xRet += "ID: " + idNum;
            xRet += "');";

            xRet += "alert('";
            xRet += "querySelector: " + idQs;                
            xRet += "');";               
            */

            //xRet += "dvSelecao.innerHTML = ' " + idNum + " ' ";

            xRet += "var x= prompt('Digite!');";

            //xRet += "nSel.push('" + idNum + "') ; ";
            xRet += "nSel.push(" + "x" + ") ; ";

            xRet += "dvSelecao.innerHTML = nSel;";

            xRet += "};";//Fim Função
            xRet += "</script>";

            return xRet;

        }

        public void selecionarNumeros(string idNumero)
        {
            this.idNum = idNumero;

            xRet = "";
            nSelecionados = "";



            if (!String.IsNullOrEmpty(idNumero))
            {
                xRet += "<script>";
                xRet += idNum + "= document.querySelector('" + idQs + "');";
                xRet += idNum + ".onclick = function() {";//Início Função             
                xRet += "alert('";
                xRet += "ID: " + idNum;
                xRet += "');";
                //xRet += "alert('";
                //xRet += "querySelector: " + idQs;
                //xRet += "');";                
                xRet += "};";//Fim Função
                xRet += "</script>";
            }


            //criar um Array e armazenar cada click

            //dvSelecao.InnerHtml = xRet;

        }

        public void GravarHTML(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " sorteio, numero, descricao, cadusu, cadmom ";
            string tabela = " ts_teste ";
                        string valores = String.Format("'" + "50" + "'," +
                                                       "'" + "100" + "'," +
                                                       "'" + tbTexto.Text.ToString() + "'," +
                                                       "'" + Session["LoginPrivado"].ToString() + "'," +                                                             
                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            string condicao = " ORDER BY id DESC LIMIT 5";

            //MessageBox.Show(" INSERT INTO " + tabela + "(" + campos + ")" + " VALUES (" + valores + ")");

            
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;
            
            ObjDados.InsertRegistro(tabela,campos, valores);


            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            if (ObjDados.MsgErro == "")
            {

                xRet += "<div>" + "Vamos ver o conteudo com Tags";                
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    xRet += "<div style='width: 500px; min-height: 500px; margin: 0 auto; border: 1px solid #f26907'>";
                    xRet += "" + dados.Rows[i]["descricao"].ToString() + "";
                    xRet += "</div>";
                }
                xRet += "</div>";
            }

            //xRet += "<div>" + "Vamos ver o conteudo com Tags" + "</div>";
            lblResult.Text = xRet;
        }

    }
}