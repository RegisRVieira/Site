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

namespace Site.Adm
{
    public partial class Publicidade : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public void ativaViews(int ativa)
        {
            mwGridPublicidade.ActiveViewIndex = ativa;
            mwFormPublicidade.ActiveViewIndex = ativa;
        }

        protected void ativaVwPublicidade(object sender, EventArgs e)
        {
            ativaViews(0);
        }

        protected void ativaVwGuiaDestaque(object sender, EventArgs e)
        {
            ativaViews(1);
        }

        protected void ativaVwGuiaLogo(object sender, EventArgs e)
        {
            ativaViews(2);
        }
        /*
        public void carregaConvenio(object sender, KeyEventArgs e)
        {            
            BLL ObjDados = new BLL(conectVegas);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " coconven ";
            ObjDados.Condicao = " WHERE idconven = '"+ iIdConv.Value +"'";

            DataTable dados = ObjDados.RetCampos();

            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("PQP");
                iNomeConv.Value = dados.Rows[0]["nome"].ToString();
            }
        }
        */
        public void carregaConvenio(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " coconven ";

            DataTable dados = ObjDados.RetCampos();



            if (!String.IsNullOrEmpty(iIdConv.Value))
            {
                ObjDados.Condicao = " WHERE idconven = '" + iIdConv.Value + "'";
                dados = ObjDados.RetCampos();

                if (dados.Rows.Count > 0)
                {
                    iNomeConv.Value = dados.Rows[0]["nome"].ToString();
                }
                else
                {
                    lblMsg.Text = "Convênio não encontrado! Verifique o código e tente novamente!!!";
                }
            }
            else
            {
                ObjDados.Condicao = " WHERE idconven = '" + iIdConvLogo.Value + "'";
                dados = ObjDados.RetCampos();
                if (dados.Rows.Count > 0)
                {
                    iNomeConvLogo.Value = dados.Rows[0]["nome"].ToString();
                }
                else
                {
                    lblMsg.Text = "Convênio não encontrado! Verifique o código e tente novamente!!!";
                }
            }


        }

        public void cadastrarDestaqueGuia(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            string xRet = "";
            string arquivo = "";
            string caminho = "";
            string caminho2 = "";
            string CaminhoArquivo = "";

            if (ObjDados.MsgErro == "")
            {
                /* * * * # UpLoad da Imagem # * * * */

                //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
                //Encontrar uma forma para Não ocorrer este erro na página
                double tamanho = 0;

                if (!String.IsNullOrEmpty(iNomeConv.Value))
                {
                    tamanho = fuDestaqueGuia.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;
                }
                else
                {
                    tamanho = fuLogoGuia.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;
                }

                


                //lblResp.InnerText = arquivo + " / " + (tamanho / 1024) / 1024 + "MB";

                //MessageBox.Show("Tamanho do Arquivo: " + tamanho);                  
                if (!String.IsNullOrEmpty(iNomeConv.Value))
                {
                    if (fuDestaqueGuia.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuDestaqueGuia.PostedFile.ContentType == "image/jpeg" ||
                            fuDestaqueGuia.PostedFile.ContentType == "image/png" ||
                            fuDestaqueGuia.PostedFile.ContentType == "image/gif")
                        {
                            if (fuDestaqueGuia.PostedFile.ContentLength < 4500000)
                            {
                                //MessageBox.Show("Blzura, no caminho certo");
                                try
                                {//Gravar Arquivo

                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Guia/").Replace(@"\", "/"); //Grava a raiz, tipo: K:/Projetos/Web/Site/Site/Img/Guia/Manzini-Lj1.jpg

                                    caminho2 = ("/Img/Guia/").Replace(@"\", "/");
                                    arquivo = fuDestaqueGuia.FileName;

                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {

                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuDestaqueGuia.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "UpLoad de Arquivo realizado com Sucesso";

                                        /* * * * # FIM - UpLoad da Imagem # * * * */

                                        // # # # # # # # # # Insere Dados na Tabela # # # # # # # # #
                                        string campos = " convenio, tipo, descricao, cnscadusu, cnscadmom, dt_inicio ";
                                        string tabela = " coinfo ";
                                        string condicao = " WHERE convenio = '" + iIdConv.Value + "'";
                                        string valores = String.Format("'" + iIdConv.Value + "'," +
                                                                       "'" + "SGUIADEST" + "'," +
                                                                       "'" + caminho2 + arquivo + "'," +
                                                                       "'" + "Site" + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'");

                                        ObjDados.Campo = campos;
                                        ObjDados.Tabela = tabela;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();

                                        ObjDados.InsertRegistro(tabela, campos, valores);

                                        // # # # # # # # # # FIM - Insere Dados na Tabela # # # # # # # # #

                                        //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);


                                        /* * * * # # * * * */

                                        if (dados.Rows.Count > 0)
                                        {
                                            mwDadosDestaque.ActiveViewIndex = 0;

                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < dados.Rows.Count; i++)
                                            {
                                                if (dados.Rows[i]["tipo"].ToString() == "SGUIADEST")
                                                {
                                                    xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["convenio"] + "</div>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["tipo"] + "</div>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["descricao"] + "</div>";
                                                    xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + dados.Rows[i]["descricao"] + "'" + "/>" + "</div>";
                                                    xRet += "</div>";
                                                }
                                            }
                                            iIdConv.Value = String.Empty;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Imagem maior que o permitido: " + (tamanho / 1024) / 1024 + "MB, máximo de 4.5MB";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Tipo de arquivo não suportado. Apenas Arquivos: JPG, PNG e GIF, são aceitos! ";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Selecione um arquivo para poder continuar";
                    }

                    lblDados.Text = xRet;
                }
                else
                {

                    if (fuLogoGuia.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuLogoGuia.PostedFile.ContentType == "image/jpeg" ||
                            fuLogoGuia.PostedFile.ContentType == "image/png" ||
                            fuLogoGuia.PostedFile.ContentType == "image/gif")
                        {
                            if (fuLogoGuia.PostedFile.ContentLength < 4500000)
                            {
                                //MessageBox.Show("Blzura, no caminho certo");
                                try
                                {//Gravar Arquivo
                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Guia/").Replace(@"\", "/"); //Grava a raiz, tipo: K:/Projetos/Web/Site/Site/Img/Guia/Manzini-Lj1.jpg
                                    caminho2 = ("/Img/Guia/").Replace(@"\", "/");
                                    arquivo = fuLogoGuia.FileName;

                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {

                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuLogoGuia.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "UpLoad de Arquivo realizado com Sucesso";

                                        /* * * * # FIM - UpLoad da Imagem # * * * */

                                        // # # # # # # # # # Insere Dados na Tabela # # # # # # # # #
                                        string campos = " convenio, tipo, descricao, cnscadusu, cnscadmom, dt_inicio ";
                                        string tabela = " coinfo ";
                                        string condicao = " WHERE convenio = '" + iIdConv.Value + "'";
                                        string valores = String.Format("'" + iIdConv.Value + "'," +
                                                                       "'" + "SGUIALOGO" + "'," +
                                                                       "'" + caminho2 + arquivo + "'," +
                                                                       "'" + "Site" + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'");

                                        ObjDados.Campo = campos;
                                        ObjDados.Tabela = tabela;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();

                                        ObjDados.InsertRegistro(tabela, campos, valores);

                                        //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);

                                        // # # # # # # # # # FIM - Insere Dados na Tabela # # # # # # # # #

                                        if (dados.Rows.Count > 0)
                                        {
                                            mwDadosLogo.ActiveViewIndex = 0;

                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < dados.Rows.Count; i++)
                                            {
                                                if (dados.Rows[i]["tipo"].ToString() == "SGUIALOGO")
                                                {
                                                    xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["convenio"] + "</div>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["tipo"] + "</div>";
                                                    xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + dados.Rows[i]["descricao"] + "</div>";
                                                    xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + dados.Rows[i]["descricao"] + "'" + "/>" + "</div>";
                                                    xRet += "</div>";
                                                }
                                            }

                                        }
                                        iIdConvLogo.Value = String.Empty;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString() + " # " + caminho + arquivo;
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Imagem maior que o permitido: " + (tamanho / 1024) / 1024 + "MB, máximo de 4.5MB";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Tipo de arquivo não suportado. Apenas Arquivos: JPG, PNG e GIF, são aceitos! ";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Selecione um arquivo para poder continuar";
                    }

                    lblDados.Text = xRet;
                }
            }
            else
            {
                lblMsg.Text = "Erro Original:" + ObjDados.MsgErro;
            }

            /* # # # # # # # # # # # # # # # # # # # # */






            //Fazer UpLoad da Imagem
            //Gravar Path do caminho da imagem

        }


        /*Fim*/
    }
}