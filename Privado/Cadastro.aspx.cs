using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Site.App_Code;
using System.Data;
using System.Configuration;

namespace Site.Privado
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["LoginPrivado"] != null) {
                    lblLogado.Text = "Você está Logado como: " + Session["LoginPrivado"].ToString();
                }
                else
                {
                    Session.Abandon();
                }
                popularTipo();
                popularUsuario();
            }
        }

        protected void popularTipo()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " p_tipo ";
            ObjDados.Campo = " * ";

            sTipo.DataSource = ObjDados.RetCampos();
            sTipo.DataValueField = "cod";
            sTipo.DataTextField = "Nome";
            sTipo.DataBind();

        }
        protected void popularUsuario()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " p_usuario ";
            ObjDados.Campo = " * ";

            sUsuario.DataSource = ObjDados.RetCampos();
            sUsuario.DataValueField = "id";
            sUsuario.DataTextField = "Nome";
            sUsuario.DataBind();

        }

        protected void testar(object sender, EventArgs e)
        {
            lblMsg.Text = "Usuário:" + sUsuario.Value + " -  Tipo: " + sTipo.Value;
        }

        protected void gravarDados(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " p_link ";
            string campos = " Link, id_usuario, cod_tipo, Descricao, Indicacao, CadMom, CadUsu ";
            string valores = "'" + iLink.Value + "'," +
                             "'" + sUsuario.Value + "'," +
                             "'" + sTipo.Value + "'," +
                             "'" + iDescricao.Value + "'," +
                             "'" + iIndicacao.Value + "'," +                             
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                             "'" + Session["LoginPrivado"].ToString() + "'";
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (ObjDados.MsgErro == "")
            {
                if (!String.IsNullOrEmpty(iLink.Value))
                {
                    ObjDados.InsertRegistro(tabela, campos, valores);
                    lblMsg.Text = "Cadastro efetuado com sucesso";

                    iLink.Value = String.Empty;
                    iDescricao.Value = String.Empty;
                    iIndicacao.Value = String.Empty;
                    iLink.Focus();
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
}