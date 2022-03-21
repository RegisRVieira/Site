using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.App_Code;
using System.Configuration;

namespace Site.Privado
{
    public partial class Listas : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mwConteudo.ActiveViewIndex = 0;
                //gerarLista();
                //gerarLink();
                popularTipo();

                if (Session["LoginPrivado"] != null)
                {
                    lblLogado.Text = "Você está Logado como: " + Session["LoginPrivado"].ToString();
                }
                else
                {
                    Session.Abandon();
                }
            }
        }
        protected void ativarViews(int ativa)
        {
            mwConteudo.ActiveViewIndex = ativa;
        }
        protected void ativarLista(object sender, EventArgs e)
        {
            ativarViews(1);
        }
        protected void ativarProcura(object sender, EventArgs e)
        {
            ativarViews(2);
        }

        protected void popularTipo()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " p_tipo ";
            ObjDados.Campo = " * ";
            ObjDados.Condicao = "ORDER BY nome";

            sTipo.DataSource = ObjDados.RetCampos();
            sTipo.DataValueField = "cod";
            sTipo.DataTextField = "Nome";
            sTipo.DataBind();

        }

        protected void gerarLista(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjLink = new BLL(conectSite);

            string tabela = " p_tipo";
            string campos = " * ";
            string condicao = "WHERE cod = '" + sTipo.Value + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            if (sTipo.Value == dados.Rows[0]["COD"].ToString())
            {

                string tLink = " p_link";
                string cLink = " * ";
                string condLink = "WHERE cod_tipo ='" + sTipo.Value + "' ORDER BY descricao ";

                ObjLink.Tabela = tLink;
                ObjLink.Campo = cLink;
                ObjLink.Condicao = condLink;

                DataTable dLink = ObjLink.RetCampos();

                xRet += "<section>";
                if (dLink.Rows.Count > 0)
                {
                    xRet += "<ol>";
                    for (int i = 0; i < dLink.Rows.Count; i++)
                    {

                        xRet += "<li>";
                        xRet += "<a href='" + dLink.Rows[i]["Link"].ToString() + "'" + "target" + "='_blank'";
                        //xRet += "<div class='pLista'>";
                        if (String.IsNullOrEmpty(dLink.Rows[i]["Indicacao"].ToString())) {
                            xRet += "<p>" + dLink.Rows[i]["Descricao"].ToString() + "</p>";
                        }
                        else
                        {
                            xRet += "<p>" + dLink.Rows[i]["Descricao"].ToString() + " : " + dLink.Rows[i]["indicacao"].ToString() + "</p>";
                        }
                        //xRet += "</div>";
                        xRet += "</a >";
                        xRet += "</li>";
                    }
                    xRet += "</ol>";
                }
                else
                {
                    xRet += "Não há dados para exibir!";
                }
                xRet += "</section>";

            }
            lblResp.Text = xRet;
        }
        protected void gerarLink()
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " p_link";
            string campos = " * ";
            string condicao = "WHERE cod_tipo ='" + sTipo.Value + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";
            string voltarHome = "";

            xRet += "<section>";
            if (dados.Rows.Count > 0)
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    xRet += "<a href='" + dados.Rows[i]["Link"].ToString() + "'";
                    xRet += "<div class='pLista'>";
                    xRet += "<p>" + dados.Rows[i]["Descricao"].ToString() + "</p>";
                    xRet += "</div>";
                    xRet += "</a >";
                }
            }
            else
            {
                xRet += "Não há dados para exibir!!!";
                voltarHome += "<a href='Default.aspx'>" + "Voltar..." + "</a>";
            }
            xRet += "</section>";

            lblListas.Text = xRet;

        }
        protected void procurarItens(object sender, EventArgs e)
        {
            BLL ObjItens = new BLL(conectSite);

            string tabela = " p_link AS l";
            string campos = " * ";
            string condicao = " WHERE l.link LIKE " + "'%" + iProcura.Value + "%'" + " OR l.descricao LIKE " + "'%" + iProcura.Value + "%'" +
                              " OR EXISTS(SELECT * FROM p_tipo AS t WHERE l.cod_tipo = t.cod AND  cod LIKE " + "'%" + iProcura.Value + "%'" + ")";
                              //" OR EXISTS(SELECT * FROM p_tipo WHERE cod LIKE " + "'%" + iProcura.Value + "%'" + ")" +
                              //"AND cod_tipo ='" + iProcura.Value + "'";

            ObjItens.Tabela = tabela;
            ObjItens.Campo = campos;
            ObjItens.Condicao = condicao;

            DataTable dados = ObjItens.RetCampos();

            string xRet = "";
            string voltarHome = "";

            //xRet += "SELECT " + campos + " FROM " + tabela + " WHERE " + condicao;            
            //xRet += "SELECT " + ObjDados.Campo + " FROM " + ObjDados.Tabela+ " WHERE " + ObjDados.Condicao;

            if (ObjItens.MsgErro == "")
            {
                xRet += "<section class='links'>";
                if (dados.Rows.Count > 0)
                {
                    xRet += "<ol>";
                    for (int i = 0; i < dados.Rows.Count; i++)
                    {

                        xRet += "<li>";
                        xRet += "<a href='" + dados.Rows[i]["Link"].ToString() + "'" + "target" + "='_blank'";
                        //xRet += "<div class='pLista'>";
                        if (String.IsNullOrEmpty(dados.Rows[i]["indicacao"].ToString()))
                        {
                            xRet += "<p>" + dados.Rows[i]["Descricao"].ToString() + "</p>";                            
                        }
                        else
                        {
                            xRet += "<p>" + dados.Rows[i]["Descricao"].ToString() + " : " + dados.Rows[i]["Indicacao"].ToString() + "</p>";
                        }
                        //xRet += "</div>";
                        xRet += "</a >";
                        xRet += "</li>";
                    }
                    xRet += "</ol>";
                }
                else
                {
                    xRet += "Não há dados para exibir!!!";
                    xRet += "<div style='margin-top: 50px; margin-left: 10px; width:200px; heigth: 20px; '>";
                    xRet += "<a href='Default.aspx'>" + "Voltar..." + "</a>";
                    xRet += "</div";
                }
                xRet += "</section>";
            }
            else
            {
                xRet += ObjItens.MsgErro;
            }

            lblMsg.Text = xRet;
        }
    }
}