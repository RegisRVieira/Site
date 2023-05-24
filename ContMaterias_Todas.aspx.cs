using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;

namespace Site
{
    public partial class ContMaterias_Todas1 : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }

        public String montarListaGeral()
        {
            BLL ObjDados = new BLL(conectSite); 

            string idContMat = Request.QueryString["IDContMat"];

            string xRet = " ";


            if (ObjDados.MsgErro == "")
            {
                ObjDados.Query = " SELECT c.id, c.titulo, c.conteudo, c.introducao, c.fonte, c.autor, dt_publini, c.cadusu, c_cat.descricao AS Categoria, d.descricao AS Destaque, t.descricao AS Tipo, i.cod_destaque AS img_destaque, i.codtipo AS TipoImg, i.path_img AS PathImg  FROM   st_conteudo AS c    INNER JOIN st_categoria AS c_cat ON c.cod_categoria = c_cat.cod   INNER JOIN st_menu AS d ON c.cod_menu = d.cod   INNER JOIN st_tipo AS t ON c.cod_tipo = t.cod   LEFT JOIN st_imagens AS i ON c.id = i.id_conteudo  " +
                                 " WHERE c.cod_tipo = 'MAT' AND c.id > '5' AND i.cod_destaque = 'MAT' AND i.codtipo = 'CHA' ORDER BY c.id DESC  ";


                DataTable dados = ObjDados.RetQuery();

                //MessageBox.Show(" Matéria:" + ObjDados.Query);

                //MessageBox.Show(dados.Rows.Count.ToString());

                string IdMat = "";

                for (int i = 0; i < dados.Rows.Count; i++)
                {                    
                    IdMat = dados.Rows[i]["id"].ToString();
                    xRet += "<section style='width: 358px; min-height: 340px; margin: 10px; float: left'>";
                    xRet += "<section class='BoxListaMaterias'>";
                    xRet += "<a href='ContMaterias.aspx?IDContMat=" + IdMat + "' >";
                    //xRet += "<img src='../Img/Av Major Matheus 2.JPG' />"; // Capturar Foto do Banco de Dados
                    xRet += "<img src='" + dados.Rows[i]["Pathimg"] + "' />";                    
                    xRet += "<p class='pl-Titulo'>" + dados.Rows[i]["titulo"] + "</p>";                    
                    xRet += "<p class='pl-Data'>" + dados.Rows[i]["dt_PublIni"] + "</p>";
                    xRet += "</a>";
                    xRet += "</section>";
                    xRet += "</section>";
                }
            }
            else
            {
                string MsgErro = ObjDados.MsgErro;
                return null;
            }

            return xRet;
        }
    }
}