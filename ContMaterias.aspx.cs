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
            BLL ObjValida = new BLL(conectSite);

            ObjValida.Campo = " * ";
            ObjValida.Tabela = " st_conteudo AS c";
            ObjValida.Left = " " + " INNER JOIN st_categoria AS c_cat ON c.cod_categoria = c_cat.cod ";
            ObjValida.Left += " " + " INNER JOIN st_menu AS d ON c.cod_menu = d.cod ";
            ObjValida.Left += " " + " INNER JOIN st_tipo AS t ON c.cod_tipo = t.cod ";
            ObjValida.Left += " " + " LEFT JOIN st_imagens as i ON c.id = i.id_conteudo ";
            ObjValida.Condicao = " " + " WHERE c.cod_tipo = '" + "MAT" + "' AND i.cod_destaque = '" + "MAT" + "' AND i.codtipo = '" + "CHA" + "' AND dt_PublFim IS NULL";

            DataTable valida = ObjValida.RetCampos();

            int qtde = valida.Rows.Count;

            string idContMat = Request.QueryString["IDContMat"];

            string xRet = " ";

            //MessageBox.Show(" Validação: SELECT " + ObjValida.Campo + " FROM " + ObjValida.Tabela + " " + ObjValida.Left + "" + ObjValida.Condicao);


            if (ObjDbASU.MsgErro == "")
            {
                ObjDbASU.Campo = " " + " c.id, c.titulo, c.conteudo, c.introducao, c.fonte, c.autor, dt_publini, c.cadusu, c_cat.descricao AS Categoria, d.descricao AS Destaque, t.descricao AS Tipo, i.cod_destaque AS img_destaque, i.codtipo AS TipoImg, i.path_img AS PathImg ";
                ObjDbASU.Tabela = " " + " st_conteudo AS c ";
                ObjDbASU.Left = " " + " INNER JOIN st_categoria AS c_cat ON c.cod_categoria = c_cat.cod ";
                ObjDbASU.Left += " " + " INNER JOIN st_menu AS d ON c.cod_menu = d.cod ";
                ObjDbASU.Left += " " + " INNER JOIN st_tipo AS t ON c.cod_tipo = t.cod ";
                ObjDbASU.Left += " " + " LEFT JOIN st_imagens as i ON c.id = i.id_conteudo ";
                if (qtde < 6)
                {
                    ObjDbASU.Condicao = " " + " WHERE c.cod_tipo = '" + "MAT" + "' AND i.cod_destaque = '" + "MAT" + "' AND i.codtipo = '" + "CHA" + "'" + " ORDER BY c.id DESC LIMIT " + qtde;
                }
                else
                {
                    ObjDbASU.Condicao = " " + " WHERE c.cod_tipo = '" + "MAT" + "' AND i.cod_destaque = '" + "MAT" + "' AND i.codtipo = '" + "CHA" + "' ORDER BY c.id DESC LIMIT 6 ";
                }

                DataTable dados = ObjDbASU.RetCampos();

                int contador = dados.Rows.Count;

                //MessageBox.Show(" Matéria:  SELECT " + ObjDbASU.Campo + " FROM " + ObjDbASU.Tabela + " " + ObjDbASU.Left + "" + ObjDbASU.Condicao);

                string IdMat = "";

                for (int i = 0; i < contador; i++)
                {
                    IdMat = dados.Rows[i]["id"].ToString();
                    xRet += "<section class='BoxListaMaterias'>";
                    xRet += "<a href='ContMaterias.aspx?IDContMat=" + IdMat + "' >";
                    //xRet += "<img src='../Img/Av Major Matheus 2.JPG' />"; // Capturar Foto do Banco de Dados
                    xRet += "<img src='" + dados.Rows[i]["Pathimg"] + "' />";
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
            BLL ObjDados = new BLL(conectSite);

            string idContMat = Request.QueryString["IDContMat"];
            string xRet = " ";

            if (String.IsNullOrEmpty(ObjDados.MsgErro))
            {
                string query = "";
              
                query = " SELECT c.*, i.*, ali.descricao AS alinha  FROM st_imagens AS i " +
                        " INNER JOIN st_conteudo AS c ON i.id_conteudo = c.id " +
                        " INNER JOIN st_imgalinhamento AS ali ON i.cod_alinhamento = ali.cod " +
                        " WHERE c.id = '" + idContMat + "' ";

                ObjDados.Query = query;

                //MessageBox.Show(idContMat);

                DataTable dadosQuery = ObjDados.RetQuery();

                //Testar integridade das Querys
                //MessageBox.Show("Conteudo: " + "SELECT " + ObjDbASU.Campo + "  FROM " + ObjDbASU.Tabela + " " + ObjDbASU.Condicao);
                //MessageBox.Show("Imagens: " + "SELECT " + ObjDbImg.Campo + " FROM " + ObjDbImg.Tabela + ObjDbImg.Left + " " + ObjDbImg.Condicao);

                //Publicidade da Matéria
                xRet += "<section class='publicidade' >";
                //xRet += "Query: " + ObjDados.Query.ToString();                
                int ValidaImagens = 0;  

                //## Checa se há Publicidade cadastrada
                for (int i = 0; i < dadosQuery.Rows.Count; i++) //Varre o DB para encontrar imgs, se houver ele "mostra"
                {                                        
                    if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICPROP")
                    {                        
                        ValidaImagens++;                        
                    }
                }

                //Se houver Publicidade, exibe-a. Do contrário, exibe uma imagem padrão.
                //if (ValidaImagens > 0)
                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++) //Varre o DB para encontrar imgs, se houver ele "mostra"
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICPROP")
                        {
                            xRet += "<img ' src='" + dadosQuery.Rows[i]["path_img"] + "' />";
                        }                        
                    }
                }
                else
                {
                    xRet += "<img src='../Img/Banner Publicidade Materia2.jpg' />";
                }
                xRet += "</section>";

                //Titulo da Matéria
                xRet += "<section class='titulo'>";
                xRet += "<p>" + dadosQuery.Rows[0]["titulo"] + "</p>";


                //if (ValidaImagens > 0)
                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++)
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICTIT")
                        {
                            if (!String.IsNullOrEmpty(dadosQuery.Rows[i]["alinha"].ToString()))
                            {
                                xRet += "<div class='matImg'>" + "<img style='float:" + dadosQuery.Rows[i]["alinha"].ToString() + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                            else
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + "center" + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                        }
                    }
                }
                xRet += "</section>";

                //Introdução
                xRet += "<section class='introducao texto'>";
                xRet += "<p>" + dadosQuery.Rows[0]["introducao"] + "</p>";
                //if (ValidaImagens > 0)
                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++)
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICINT")
                        {
                            if (!String.IsNullOrEmpty(dadosQuery.Rows[i]["alinha"].ToString()))
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + dadosQuery.Rows[i]["alinha"].ToString() + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                            else
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + "center" + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                        }
                    }
                }
                xRet += "</section>";

                //Contexto
                xRet += "<section class='contexto texto'>";
                xRet += "<p>" + dadosQuery.Rows[0]["conteudo"] + "</p>";

                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++)
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICCON")
                        {
                            if (!String.IsNullOrEmpty(dadosQuery.Rows[i]["alinha"].ToString()))
                            {
                                xRet += "<div class='matImg'>" + "<img style='float:" + dadosQuery.Rows[i]["alinha"].ToString() + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                            else
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + "center" + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                        }
                    }
                }
                
                xRet += "</section>";

                //Complemento
                xRet += "<section class='complemento texto'>";
                xRet += "<p>" + dadosQuery.Rows[0]["complemento"] + "</p>";

                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++)
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICCOM")
                        {
                            if (!String.IsNullOrEmpty(dadosQuery.Rows[i]["alinha"].ToString()))
                            {
                                xRet += "<div class='matImg'>" + "<img style='float:" + dadosQuery.Rows[i]["alinha"].ToString() + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                            else
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + "center" + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                        }
                    }
                }
                //xRet += "<section class='complemento texto'>";                
                
                xRet += "</section>";

                //Conclusão
                xRet += "<section class='conclusao texto'>";
                xRet += "<p>" + dadosQuery.Rows[0]["conclusao"] + "</p>";

                //if (ValidaImagens > 0)
                if (dadosQuery.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosQuery.Rows.Count; i++)
                    {
                        if (dadosQuery.Rows[i]["cod_campoconteudo"].ToString() == "ICCONC")
                        {
                            if (!String.IsNullOrEmpty(dadosQuery.Rows[i]["alinha"].ToString()))
                            {
                                xRet += "<div class='matImg'>" + "<img style='float:" + dadosQuery.Rows[i]["alinha"].ToString() + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                            else
                            {
                                xRet += "<div class='matImg'>" + "<img style='float: " + "center" + "' src='" + dadosQuery.Rows[i]["path_img"] + "' />" + "</div>";
                            }
                        }
                    }
                }               
                
                xRet += "</section>";
            }
            else
            {
                xRet += "Erro: " + ObjDados.MsgErro;
            }


            return xRet;
        }

        /**/
    }
}