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

namespace Site
{
    public partial class ContMaterias : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {

            this.DataBind();
        }

        public String montarListaMaterias()
        {
            BLL ObjDbASU = new BLL(conectSite);


            string idContMat = Request.QueryString["IDContMat"];

            string xRet = " ";

            if (ObjDbASU.MsgErro == "")
            {
                ObjDbASU.Campo = " " + " c.id, c.titulo, c.conteudo, c.introducao, c.fonte, c.autor, dt_publini, c.cadusu, c_cat.descricao AS Categoria, d.descricao AS Destaque, t.descricao AS Tipo, i.cod_destaque AS img_destaque, i.codtipo AS TipoImg, i.path_img AS PathImg ";
                ObjDbASU.Tabela = " " + " st_conteudo AS c ";
                ObjDbASU.Left = " " + " INNER JOIN st_categoria AS c_cat ON c.cod_categoria = c_cat.cod ";
                ObjDbASU.Left += " " + " INNER JOIN st_menu AS d ON c.cod_menu = d.cod ";
                ObjDbASU.Left += " " + " INNER JOIN st_tipo AS t ON c.cod_tipo = t.cod ";
                ObjDbASU.Left += " " + " LEFT JOIN st_imagens as i ON c.id = i.id_conteudo ";
                ObjDbASU.Condicao = " " + " WHERE c.cod_tipo = '" + "MAT" + "' AND i.cod_destaque = '" + "MAT" + "' AND i.codtipo = '" + "CHA" + "' ORDER BY c.id DESC LIMIT 2 ";

                DataTable dados = ObjDbASU.RetCampos();

                int contador = dados.Rows.Count;

                string IdMat = "";

                for (int i = 0; i < contador; i++)
                {
                    IdMat = dados.Rows[i]["id"].ToString();
                    xRet += "<section>";
                    xRet += "<a href='ContMaterias.aspx?IDContMat=" + IdMat + "' >";
                    xRet += "<img  src='../Img/Av Major Matheus 2.JPG' />"; // Capturar Foto do Banco de Dados
                    xRet += "<p class='pl-Titulo'>" + dados.Rows[i]["titulo"] + "</p>";
                    xRet += "<p class='pl-Data'>" + dados.Rows[i]["dt_PublIni"] + "</p>";
                    xRet += "</a>";
                    xRet += "</section>";
                }
            }
            else
            {
                string MsgErro = ObjDbASU.MsgErro;
                return null;
            }

            return xRet;
        }

        public String montarContMateria()
        {
            BLL ObjDbASU = new BLL(conectSite);
            BLL ObjDbImg = new BLL(conectSite);

            string idContMat = Request.QueryString["IDContMat"];
            string xRet = " ";

            if (ObjDbASU.MsgErro == "" || ObjDbImg.MsgErro == "")
            {

                ObjDbASU.Campo = " * ";
                ObjDbASU.Tabela = " st_conteudo ";
                ObjDbASU.Condicao = " WHERE ID = '" + idContMat + "' ";

                ObjDbImg.Campo = " *, ali.descricao ";
                ObjDbImg.Tabela = " st_imagens AS i ";
                ObjDbImg.Left = " INNER JOIN st_imgalinhamento AS ali ON i.cod_alinhamento = ali.cod ";
                ObjDbImg.Condicao = " WHERE id_conteudo = '" + idContMat + "' ";


                DataTable dados = ObjDbASU.RetCampos();
                DataTable imgs = ObjDbImg.RetCampos();

                int contador = dados.Rows.Count;
                int contaImg = imgs.Rows.Count;


                xRet += "<section class='publicidade'>";
                xRet += "<img src='../Img/Banner Publicidade Materia2.jpg' />";
                xRet += "</section>";
                xRet += "<section class='titulo'>";
                xRet += "<p>" + dados.Rows[0]["titulo"] + "</p>";
                for (int i = 0; i < contaImg; i++) //Varre o DB para encontrar imgs, se houver ele "mostra"
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICTIT")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }
                xRet += "</section>";
                xRet += "<section class='introducao texto'>";
                xRet += "<p>" + dados.Rows[0]["introducao"] + "</p>";
                for (int i = 0; i < contaImg; i++)
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICINT")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }
                xRet += "</section>";

                xRet += "<section class='contexto texto'>";
                for (int i = 0; i < contaImg; i++)
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICCON")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }
                xRet += "<p>" + dados.Rows[0]["conteudo"] + "</p>";
                xRet += "</section>";
                xRet += "<section class='complemento texto'>";
                for (int i = 0; i < contaImg; i++)
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICCOM")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }
                xRet += "<p>" + dados.Rows[0]["complemento"] + "</p>";
                xRet += "</section>";
                xRet += "<section class='conclusao texto'>";
                for (int i = 0; i < contaImg; i++)
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICCONC")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }
                xRet += "<p>" + dados.Rows[0]["conclusao"] + "</p>";
                xRet += "</section>";
            }
            else
            {
                xRet += "Erro: " + ObjDbASU.MsgErro;
            }
            return xRet;
        }

        /**/
    }
}