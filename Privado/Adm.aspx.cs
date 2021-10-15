using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Site.App_Code;
using System.Configuration;
using System.Data;

namespace Site.Privado
{
    public partial class Adm : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ativarBoasVindas();
                if (Session["LoginPrivado"] != null)
                {
                    lblLogado.Text = "Você está Logado como:" + Session["LoginPrivado"].ToString();
                }
                else
                {
                    Session.Abandon();
                    lblMsg.Text = "Você foi desconectado";
                    Response.Redirect("pLogin.aspx");
                }
            }

            this.DataBind();
        }
        protected void ativarViews(int ativa)
        {
            mwPessoal.ActiveViewIndex = ativa;
        }

        protected void ativarBoasVindas()
        {
            ativarViews(0);
        }
        protected void ativarUsuario(object sender, EventArgs e)
        {
            ativarViews(1);
            carregarUsuarios();
        }
        protected void ativarTipo(object sender, EventArgs e)
        {
            ativarViews(2);
        }
        protected void ativarConteudo(object sender, EventArgs e)
        {
            Response.Redirect("Cadastro.Aspx");
        }

        protected void carregarUsuarios()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " p_usuario ";
            ObjDados.Campo = " * ";

            gvUsuarios.DataSource = ObjDados.RetCampos();
            gvUsuarios.DataBind();
        }

        protected void cadastrarUsuario(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " p_usuario ";
            string campos = " Nome, Senha, Descricao, CadMom, CadUsu ";
            string valores = "'" + iNomeUsuario.Value + "'," +
                             "'" + iSenhaUsuario.Value + "'," +
                             "'" + iDescUsuario.Value + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                             "'" + Session["LoginPrivado"].ToString() + "'";
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (!String.IsNullOrEmpty(iNomeUsuario.Value))
            {
                ObjDados.InsertRegistro(tabela, campos, valores);
                lblMsg.Text = "Cadastro efetuado com sucesso";

                iNomeUsuario.Value = String.Empty;
                iDescUsuario.Value = String.Empty;
                iNomeUsuario.Focus();
            }
            else
            {
                lblMsg.Text = "Preencha os campos para continuar.";
            }
        }
        protected void cadastrarTipo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " p_tipo ";
            string campos = " cod, Nome, Descricao, CadMom, CadUsu ";
            string valores = "'" + iCodTipo.Value + "'," +
                             "'" + iNomeTipo.Value + "'," +
                             "'" + iDescTipo.Value + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                             "'" + Session["LoginPrivado"].ToString() + "'";
            string condicao = " WHERE COD = '" + iCodTipo.Value + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            if (ObjDados.MsgErro == "")
            {
                if (!String.IsNullOrEmpty(iCodTipo.Value))
                {
                    if (dados.Rows.Count > 0)
                    {
                        lblMsg.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                        iCodTipo.Focus();
                    }
                    else
                    {
                        ObjDados.InsertRegistro(tabela, campos, valores);
                        lblMsg.Text = "Cadastro efetuado com sucesso";

                        iCodTipo.Value = String.Empty;
                        iNomeTipo.Value = String.Empty;
                        iDescTipo.Value = String.Empty;
                        iCodTipo.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = "Preencha os campos para continuar.";
                }
            }
            else
            {
                lblMsg.Text = "Erro: " + ObjDados.MsgErro;
            }
        }

    }
}//Fim