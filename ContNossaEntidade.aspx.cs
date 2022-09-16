using System;
using System.IO;
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

        protected String montarBalancete()
        {

            /*Exibir Arquivos no Diretório*/
            DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\Balancetes\");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            string arquivos = "";
            string xArq = "";

            xArq += "<div style='margin-top: 30px; width: 600px; height: auto; '>";
            //xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(242,105,7,0), rgba(242,105,7,95));'>" + "Arquivos" + "</p>";
            xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(34,57,111,0), rgba(34,57,111,44));'>" + "Balancete Mensal" + "</p>";
            xArq += "<div style='padding: 10px'>";
            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;
                xArq += "<p style='text-align: left; margin: 5px 0 0 10px; padding: 0 0 0 0; '>";
                xArq += "<div style='width: 20px; height: 20px; float: left'>";
                xArq += "<img style='width: 22px;' src='Img/Icon/Balancete.png' />";
                xArq += "</div>";
                xArq += "<div style='width: 300px; height: 20px; float: left;'>";
                xArq += "<p style='text-align: left; margin: 0; margin-left: 10px; padding: 0;'> <a style='' href='" + @"Downloads\Balancetes\" + arquivos + "' target=_blanck>" + arquivos + "</a></p>";
                xArq += "</div>";
                xArq += "</p><br>";                
            }
            xArq += "</div>";
            xArq += "</div>";

            return xArq;
        }

        public String montarConteudo(int ID)
        {
            BLL ObjDbASU = new BLL(conectSite);
            BLL ObjDbImg = new BLL(conectSite);

            string xRet = " ";

            try
            {
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
                    if (String.IsNullOrEmpty(dados.Rows[0]["titulo"].ToString()))
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
                    if (String.IsNullOrEmpty(dados.Rows[0]["introducao"].ToString()))
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
                    if (String.IsNullOrEmpty(dados.Rows[0]["conteudo"].ToString()))
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
                    if (String.IsNullOrEmpty(dados.Rows[0]["complemento"].ToString()))
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
                    if (String.IsNullOrEmpty(dados.Rows[0]["conclusao"].ToString()))
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
                        xRet += ObjDbASU.MsgErro;
                    }
                    else
                    {
                        xRet += ObjDbImg.MsgErro;
                    }
                }
            }
            catch (Exception e) {
                xRet += "Erro: " + e.Message;
            }

            return xRet;
        }


    }//Fim
}