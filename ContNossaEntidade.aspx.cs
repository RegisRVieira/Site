using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using Site.App_Code;

namespace Site
{
    public partial class ContNossaEntidade : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            string vNE = Request.QueryString["vNE"];

            if (!Page.IsPostBack)
            {
                if (vNE == "1")
                {
                    mwConteudo.ActiveViewIndex = 0;
                }
                else if (vNE == "2")
                {
                    mwConteudo.ActiveViewIndex = 4;
                }
            }

            this.DataBind();
        }

        //Ativar View's
        public void ativaASU(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 0;
        }
        public void ativaSedeLageado(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 1;
        }
        public void ativaDap(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 2;
        }
        public void ativaClube(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 3;
        }
        public void nossoEstatuto(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 4;
        }
        public void nossoRegimento(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 5;
        }
        public void nossoBalancete(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 6;
        }
        public void nossoJornal(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 7;
        }
        public void ativaNossasPessoas(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 8;
        }


        public String montarConteudo(int ID)
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
                if (ObjDbASU.MsgErro!="")
                {
                    xRet += ObjDbASU.MsgErro;
                }
                else
                {
                    xRet += ObjDbImg.MsgErro;
                }
            }

            return xRet;
        }


    }//Fim
}