using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using Site.App_Code;
using System.Configuration;

namespace Site
{
    public partial class Default : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }

        public void escolherConexao(object sender, EventArgs e)
        {            
            BLL ObjConexaoVegas = new BLL(conectVegas);
            BLL ObjConexao = new BLL(conectSite);

            string xRet = "";

            if (ObjConexao.MsgErro == "")
            {
                try
                {
                    if (ddlEscolha.SelectedValue == "0")
                    {
                        string campo = " * ";
                        string tabela = " st_usuario ";
                        string condicao = " LIMIT 20 ";

                        ObjConexao.Campo = campo;
                        ObjConexao.Tabela = tabela;
                        ObjConexao.Condicao = condicao;

                        DataTable dados = ObjConexao.RetCampos();
                        int contador = dados.Rows.Count;

                        for (int i = 0; i < contador; i++)
                        {
                            xRet += "<p style='padding: 0; margin:0; margin-left: 20px;'>" + "ASU: " + dados.Rows[i]["nome"] + " - " + i + "</p>";
                        }
                        lblResp.Text = xRet;


                        //lblResp.Text = "Conexão ASU";
                    }
                    else if (ddlEscolha.SelectedValue == "1")
                    {
                        string campo = " * ";
                        string tabela = " associa ";
                        string condicao = " LIMIT 20 ";

                        ObjConexaoVegas.Campo = campo;
                        ObjConexaoVegas.Tabela = tabela;
                        ObjConexaoVegas.Condicao = condicao;

                        DataTable dadosv = ObjConexaoVegas.RetCampos();
                        int contador = dadosv.Rows.Count;

                        for (int i = 0; i < contador; i++)
                        {
                            xRet += "<p style='padding: 0; margin:0; margin-left: 20px;'>" + "Vegas: " + dadosv.Rows[i]["titular"] + " - " + i + "</p>";
                        }
                        lblResp.Text = xRet;
                    }
                }
                catch (Exception ex)
                {
                    //lblResp.Text = ex.Message;
                    if (ddlEscolha.SelectedValue == "0")
                    {
                        lblResp.Text = "Erro de conexão ASU: " + ObjConexao.MsgErro;
                        //lblErro.Text = campo + tabela + condicao;
                    }
                    else
                    {
                        lblResp.Text = "Erro de Conexão Vegas: " + ObjConexao.MsgErro;
                        //lblErro.Text = campov + tabelav + condicaov;
                    }
                }
            }
            else
            {
                if (ddlEscolha.SelectedValue == "0")
                {
                    lblResp.Text = "Erro de conexão ASU: " + ObjConexao.MsgErro;
                }
                else
                {
                    lblResp.Text = "Erro de Conexão Vegas: " + ObjConexao.MsgErro;
                }

            }

        }

        public void apenasEscolher(object sender, EventArgs e)
        {

            if (ddlEscolha.SelectedValue == "0")
            {
                string conectstring = "Server: " + WebConfigurationManager.AppSettings["ServerASU"] + " - " + " DataBase: " + WebConfigurationManager.AppSettings["DBASU"] + " - " + " Usuário: " + WebConfigurationManager.AppSettings["userasu"] + " - Porta: " + WebConfigurationManager.AppSettings["portasu"];
                lblResp.Text = "Você escolheu ASU" + " ### " + conectstring;
            }
            else
            {
                string conectstring = "Server: " + WebConfigurationManager.AppSettings["ServerVegas"] + " - " + " DataBase: " + WebConfigurationManager.AppSettings["DBVegas"] + " - " + " Usuário: " + WebConfigurationManager.AppSettings["UserVegas"] + " - Porta: " + WebConfigurationManager.AppSettings["PortVegas"];
                lblResp.Text = "Você escolheu Vegas" + " ### " + conectstring;
            }
        }
    }
}