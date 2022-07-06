using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Site.App_Code;
using MySql.Data.MySqlClient;

namespace Site
{
    public partial class ContConv : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mwConvenios.ActiveViewIndex = 2;
            }

            this.DataBind();
        }

        //Ativar Views

        protected void ativarPublicidade(object sender, EventArgs e)
        {
            mwConvenios.ActiveViewIndex = 0;
        }
        protected void ativarAntecipacao(object sender, EventArgs e)
        {
            mwConvenios.ActiveViewIndex = 1;
        }
        protected void ativarComoFunciona(object sender, EventArgs e)
        {
            mwConvenios.ActiveViewIndex = 2;
        }

        public String montaParaConvenios(int ID) //30-03-2021
        {
            BLL ObjDbASU = new BLL(conectSite);
            BLL ObjDbImg = new BLL(conectSite);

            string xRet = " ";

            if (ObjDbASU.MsgErro == "" || ObjDbImg.MsgErro == "")
            {

                ObjDbASU.Campo = " * ";
                ObjDbASU.Tabela = " st_conteudo ";
                ObjDbASU.Condicao = " WHERE ID = '" + ID + "' ";

                ObjDbImg.Campo = " *, ali.descricao ";
                ObjDbImg.Tabela = " st_imagens AS i ";
                ObjDbImg.Left = " INNER JOIN st_imgalinhamento AS ali ON i.cod_alinhamento = ali.cod ";
                ObjDbImg.Condicao = " WHERE id_conteudo = '" + ID + "' ";


                DataTable dados = ObjDbASU.RetCampos();
                DataTable imgs = ObjDbImg.RetCampos();

                int contador = dados.Rows.Count;
                int contaImg = imgs.Rows.Count;

                xRet += "<section class='publicidade'>";
                //xRet += "<img src='../../Img/Banner Publicidade Materia2.jpg' />";

                for (int i = 0; i < contaImg; i++) //Varre o DB para encontrar imgs, se houver ele "mostra"
                {
                    if (imgs.Rows[i]["cod_campoconteudo"].ToString() == "ICEXT")
                    {
                        xRet += "<div class='matImg'>" + "<img style='float:" + imgs.Rows[i]["descricao"].ToString() + "' src='" + imgs.Rows[i]["path_img"] + "' />" + "</div>";
                    }
                }

                xRet += "</section>";

                //Título
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                if (dados.Rows[0]["titulo"] == "" || dados.Rows[0]["titulo"] is null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                {
                }
                else
                {
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
                }
                //Introdução
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                if (dados.Rows[0]["introducao"] == "" || dados.Rows[0]["introducao"] is null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                {

                }
                else
                {
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
                }
                //Conteúdo
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                if (dados.Rows[0]["conteudo"] == "" || dados.Rows[0]["conteudo"] is null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                {
                }
                else
                {
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
                }

                //Complemento
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                if (dados.Rows[0]["complemento"] == "" || dados.Rows[0]["complemento"] is null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                {

                }
                else
                {
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
                }
                //Conclusão
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                if (dados.Rows[0]["conclusao"] == "" || dados.Rows[0]["conclusao"] is null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                {
                }
                else
                {
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
                xRet += "<section style='margin-bottom: 80px;'>" + "</section>";
            }
            else
            {
                if (ObjDbASU.MsgErro != "")
                {
                    xRet = ObjDbASU.MsgErro;
                }
                else {
                    xRet = ObjDbImg.MsgErro;
                }
            }
            return xRet;
        }



    }/*Fim*/
}