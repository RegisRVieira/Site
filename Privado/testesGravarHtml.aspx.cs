using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Site.App_Code;

namespace Site.Privado
{
    public partial class testesGravarHtml : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void recuperaTagsHTML(object sender, EventArgs e)
        {

            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " st_conteudo ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            int tagIni = 0;
            int tagFim = 0;
            

            //xRet += dados.Rows[0]["introducao"].ToString();
            xRet += dados.Rows[0]["titulo"].ToString();

            string x = dados.Rows[0]["titulo"].ToString();
            string y = dados.Rows[0]["titulo"].ToString();
                        

            if (x.Substring(0, 1) == "<")
            {
                while (x.Substring(tagIni, 1) != ">")
                {
                    tagIni++;
                }
                while (x.Substring(tagFim, 1) != "/")
                {
                    tagFim++;
                }
            }
            else
            {
                MessageBox.Show("Não deu 1...");
            }           

            //MessageBox.Show("TagIni: " + x.Substring(0, (tagIni + 1)) + ", " + (tagIni + 1));
            //MessageBox.Show("TagFim: " + x.Substring((0), (tagFim - 1)) + ", " + (tagFim - 1));
            //MessageBox.Show("Tamanho de X:" + x.Length + " Tamanho TagFim: " + tagFim + " x - TagFim: " + (x.Length - (tagFim - 1)));
            //MessageBox.Show("Tamanho Tag Ini: " + tagIni + " - Tamanho Tag Fim: " + (x.Length - (tagFim - 1)));

            MessageBox.Show("Como tem que ficar: " + (tagIni + 1) + ", " + ((tagFim - 1) - (tagIni + 1)));
            MessageBox.Show("Como tem que ficar: " + x.Substring((tagIni + 1), ((tagFim - 1) - (tagIni + 1))));

            lbResult.Text = xRet + "Tamanho Tag Ini: " + tagIni + " - Tamanho Tag Fim: " + (x.Length - (tagFim - 1)) + 
                                   "\n" + "Como tem que ficar: " + (tagIni + 1) + ", " + ((tagFim - 1) - (tagIni + 1)) +
                                   " --> " + x.Substring((tagIni + 1), ((tagFim - 1) - (tagIni + 1))) + "\n" + "Deu Ceeeerto!!!! 17-03-2022";


        }
    }
}