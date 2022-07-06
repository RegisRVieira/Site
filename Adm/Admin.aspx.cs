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
using System.Text.RegularExpressions;

namespace Site.Adm
{
    public partial class Admin : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];

        public string UltimoRegistro { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                carregarGvInicioPagina();
            }

            //MessageBox.Show(Session["LoginUsuario"].ToString());

        }

        protected void fazerLogof(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginAdm.aspx");
        }
        public void carregarGvInicioPagina()
        {
            carregarGvEmpresa();
            carregarGvMenu();
            carregarGvCategoria();
            carregarGvTipoImg();
            carregarGvUsuarios();
            carregarGvTipo();
            carregarGvImgCampoCont();
            carregarGvImgPosicao();
            carregarGvImgAlinha();
            carregarGvImagens();            
        }

        public void ativarViews(int ativa)
        {
            mwForm.ActiveViewIndex = ativa;
            mwGrid.ActiveViewIndex = ativa;

        }
        public void ativarVwEmpresa(object sender, EventArgs e)
        {
            ativarViews(0);
        }

        public void ativarVwCategoria(object sender, EventArgs e)
        {
            ativarViews(1);
        }
        public void ativarVwMenu(object sender, EventArgs e)
        {
            ativarViews(2);
        }
        public void ativarVwTipoImg(object sender, EventArgs e)
        {
            ativarViews(3);
        }
        protected void ativarUsuarios(object sender, EventArgs e)
        {
            ativarViews(4);
        }
        public void ativarVwTipo(object sender, EventArgs e)
        {
            ativarViews(5);
        }

        public void ativarVwImgCampoConteudo(object sender, EventArgs e)
        {
            ativarViews(6);
        }
        public void ativarVwImgPosicao(object sender, EventArgs e)
        {
            ativarViews(7);
        }

        public void ativarVwImgAlinhamento(object sender, EventArgs e)
        {
            ativarViews(8);
        }
        public void ativarVwImagens(object sender, EventArgs e)
        {
            ativarViews(9);
        }
        protected void ativarConfig(object sender, EventArgs e)
        {
            ativarViews(10);
        }
        public void ultimoRegistro()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_conteudo ";
            ObjDados.Condicao = " ORDER BY id DESC ";

            DataTable dados = ObjDados.RetCampos();
            if (ObjDados.MsgErro == "")
            {
                UltimoRegistro = dados.Rows[0]["id"].ToString();
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }
            //UltimoRegistro =  dados.Rows.Count.ToString();
            //MessageBox.Show("Método Último Registro: " + UltimoRegistro);
        }

        protected void carregarGvImagens()
        {
            //ultimoRegistro();

            BLL ObjDados = new BLL(conectSite);

            //MessageBox.Show("Último Registro Cadastrado: " + UltimoRegistro);

            //ObjDados.Campo = "" + " c.titulo, c.fonte, c.autor, i.path_img, i.titulo, i.descritivo, i.cadusu, t.descricao ";
            ObjDados.Campo = "" + " i.titulo, i.fonte, i.autor, i.path_img, i.descritivo,  i.cadusu, i.hint, id_conteudo ";
            ObjDados.Tabela = "" + " st_imagens AS i ";
            
            //ObjDados.Left = "" + " INNER JOIN st_conteudo AS c ON i.id_conteudo = c.id " +
            //                     " INNER JOIN st_tipo AS t ON t.cod = c.cod_tipo ";
            //ObjDbASU.Condicao = " WHERE i.id_conteudo = '55' ";
            //ObjDados.Condicao = " WHERE i.id_conteudo = '" + UltimoRegistro + "' ";

            gvImagens.DataSource = ObjDados.RetCampos();
            gvImagens.DataBind();
        }
        
        public void carregarGvEmpresa()
        {
            BLL ObjAdm = new BLL(conectSite);

            string campos = " * ";
            string tabela = " st_empresa ";

            ObjAdm.Campo = campos;
            ObjAdm.Tabela = tabela;

            DataTable dados = ObjAdm.RetCampos();

            gvEmpresa.DataSource = dados;
            gvEmpresa.DataBind();
        }

        public void paginarGwEmpresa(object sender, GridViewPageEventArgs e)
        {
            BLL ObjAdm = new BLL(conectSite);

            string campos = " id,nome, cadusu, cadmom ";
            string tabela = " st_empresa ";
            string condicao = " order by id desc ";

            ObjAdm.Campo = campos;
            ObjAdm.Tabela = tabela;
            ObjAdm.Condicao = condicao;

            DataTable dados = ObjAdm.RetCampos();

            gvEmpresa.DataSource = dados;
            gvEmpresa.PageIndex = e.NewPageIndex;
            gvEmpresa.DataBind();
        }
        protected void selecionarRegistroGvEmpresa(object sender, EventArgs e)
        {
            iExcluEmpresaMot.Visible = false;
            iExcluEmpresaMot.Value = String.Empty;
            btnExcEmpresa.Visible = false;
            btnMotExcEmpresa.Visible = true;

            lblIdEmpresa.Text = gvEmpresa.SelectedRow.Cells[1].Text;
            iNome.Value = gvEmpresa.SelectedRow.Cells[2].Text;

            //MessageBox.Show(gvEmpresa.SelectedRow.Cells[1].Text);

            string nome = iNome.Value;

            //bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[^a-zA-Z0-9]"));
            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));

            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iNome.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iNome.Value = string.Join("", iNome.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarRegistrosGvCategoria(object sender, EventArgs e)
        {
            lblCodCategoria.Text = gvCategoria.SelectedRow.Cells[1].Text;
            iCodCategoria.Value = gvCategoria.SelectedRow.Cells[1].Text;
            iDescCategoria.Value = gvCategoria.SelectedRow.Cells[4].Text;
            
            string nome = iDescCategoria.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));            

            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescCategoria.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescCategoria.Value = string.Join("", iDescCategoria.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarRegistrosGvMenu(object sender, EventArgs e)
        {
            lblCodMenu.Text = gvMenu.SelectedRow.Cells[1].Text;
            iCodMenu.Value = gvMenu.SelectedRow.Cells[1].Text;
            iDescMenu.Value = gvMenu.SelectedRow.Cells[3].Text;

            string nome = iDescMenu.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));

            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescMenu.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescMenu.Value = string.Join("", iDescMenu.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarRegistrosGvImgTipo(object sender, EventArgs e)
        {
            lblCodImgTipo.Text = gvImgTipo.SelectedRow.Cells[1].Text;
            iCodTipoImg.Value = gvImgTipo.SelectedRow.Cells[1].Text;
            iTituloTipoImg.Value = gvImgTipo.SelectedRow.Cells[3].Text;
            iDescTipoImg.Value = gvImgTipo.SelectedRow.Cells[4].Text;

            string nome = iDescTipoImg.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));

            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescTipoImg.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescTipoImg.Value = string.Join("", iDescTipoImg.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarRegistroGvUsuario(object sender, EventArgs e)
        {
            lblIdUsuario.Text = gvUsuarios.SelectedRow.Cells[1].Text;
            iNomeUsuario.Value = gvUsuarios.SelectedRow.Cells[4].Text;
            iUsuarioUsu.Value = gvUsuarios.SelectedRow.Cells[5].Text;
            iSenhaUsuario.Value = gvUsuarios.SelectedRow.Cells[6].Text;
            iEmailUsuario.Value = gvUsuarios.SelectedRow.Cells[7].Text;
            iCPFUsuario.Value = gvUsuarios.SelectedRow.Cells[8].Text;
            

            string nome = iUsuarioUsu.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));
            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iUsuarioUsu.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iUsuarioUsu.Value = string.Join("", iUsuarioUsu.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarRegistroGvTipo(object sender, EventArgs e)
        {
            lblCodTipo.Text = gvTipo.SelectedRow.Cells[1].Text;
            iCodTipo.Value = gvTipo.SelectedRow.Cells[1].Text;
            iDescTipo.Value = gvTipo.SelectedRow.Cells[3].Text;            

            string nome = iDescTipo.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));
            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescTipo.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescTipo.Value = string.Join("", iDescTipo.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarImgCampoConteudo(object sender, EventArgs e)
        {
            lblImgCampoConteudo.Text = gvImgCampoConteudo.SelectedRow.Cells[1].Text;
            iCodCampConteudo.Value = gvImgCampoConteudo.SelectedRow.Cells[1].Text;
            iDescCampConteudo.Value = gvImgCampoConteudo.SelectedRow.Cells[3].Text;

            string nome = iDescCampConteudo.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));
            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescCampConteudo.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescCampConteudo.Value = string.Join("", iDescCampConteudo.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarImgPosicao(object sender, EventArgs e)
        {
            lblCodImgPosicao.Text = gvImgPosicao.SelectedRow.Cells[1].Text;
            iCodPosicao.Value = gvImgPosicao.SelectedRow.Cells[1].Text;
            iDescPosicao.Value = gvImgPosicao.SelectedRow.Cells[3].Text;

            string nome = iDescPosicao.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));
            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescPosicao.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescPosicao.Value = string.Join("", iDescPosicao.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarImgAlinha(object sender, EventArgs e)
        {
            lblCodImgAlinha.Text = gvImgAlinha.SelectedRow.Cells[1].Text;
            iCodImgAlinha.Value = gvImgAlinha.SelectedRow.Cells[1].Text;
            iDescImgAlinha.Value = gvImgAlinha.SelectedRow.Cells[3].Text;

            string nome = iDescImgAlinha.Value;

            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));
            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iDescImgAlinha.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iDescImgAlinha.Value = string.Join("", iDescImgAlinha.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }
        }
        protected void selecionarImagens(object sender, EventArgs e)
        {
            string idImagem = gvImagens.SelectedRow.Cells[1].Text;

            iImgTitulo.Value = gvImagens.SelectedRow.Cells[1].Text;            
            iImgFonte.Value = gvImagens.SelectedRow.Cells[2].Text;
            iImgAutor.Value = gvImagens.SelectedRow.Cells[3].Text;
            iImgPath.Value = gvImagens.SelectedRow.Cells[4].Text;
            iImgDescricao.Value = gvImagens.SelectedRow.Cells[5].Text;
            iImgHint.Value = gvImagens.SelectedRow.Cells[7].Text;
            
            //" i.titulo, i.fonte, i.autor, i.path_img, i.descritivo,  i.cadusu, i.hint, id_conteudo "
        }
        public void carregarGvCategoria()
        {
            BLL ObjAdm = new BLL(conectSite);

            string campos = " * ";
            string tabela = " st_categoria ";
            string condicao = " order by cadmom desc ";

            ObjAdm.Campo = campos;
            ObjAdm.Tabela = tabela;
            ObjAdm.Condicao = condicao;

            DataTable dados = ObjAdm.RetCampos();

            gvCategoria.DataSource = dados;
            gvCategoria.DataBind();
        }
        public void paginarGwCategoria(object sender, GridViewPageEventArgs e)
        {
            BLL ObjAdm = new BLL(conectSite);

            string campos = " * ";
            string tabela = " st_categoria ";
            string condicao = " order by cadmom desc ";

            ObjAdm.Campo = campos;
            ObjAdm.Tabela = tabela;
            ObjAdm.Condicao = condicao;

            DataTable dados = ObjAdm.RetCampos();

            gvCategoria.DataSource = dados;
            gvCategoria.PageIndex = e.NewPageIndex;
            gvCategoria.DataBind();
        }

        public void carregarGvMenu()
        {
            BLL ObjMenu = new BLL(conectSite);
            ObjMenu.Campo = " * ";
            ObjMenu.Tabela = " st_menu ";
            ObjMenu.Condicao = " order by cadmom desc ";

            DataTable dados = ObjMenu.RetCampos();

            gvMenu.DataSource = dados;
            gvMenu.DataBind();
        }
        public void paginarGwMenu(object sender, GridViewPageEventArgs e)
        {
            BLL ObjMenu = new BLL(conectSite);

            ObjMenu.Campo = " * ";
            ObjMenu.Tabela = " st_menu ";
            ObjMenu.Condicao = " ORDER BY cadmom DESC";

            DataTable dados = ObjMenu.RetCampos();

            gvMenu.DataSource = dados;
            gvMenu.PageIndex = e.NewPageIndex;
            gvMenu.DataBind();
        }

        public void carregarGvTipoImg()
        {
            BLL ObjTipo = new BLL(conectSite);

            ObjTipo.Campo = " * ";
            ObjTipo.Tabela = " st_img_tipo ";
            ObjTipo.Condicao = " ORDER BY cadmom DESC ";

            DataTable dados = ObjTipo.RetCampos();

            gvImgTipo.DataSource = dados;
            gvImgTipo.DataBind();
        }

        public void paginarGwImgTipo(object sender, GridViewPageEventArgs e)
        {
            BLL ObjTipo = new BLL(conectSite);

            ObjTipo.Campo = " * ";
            ObjTipo.Tabela = " st_img_tipo ";
            ObjTipo.Condicao = " ORDER BY cadmom ";

            DataTable dados = ObjTipo.RetCampos();

            gvImgTipo.DataSource = dados;
            gvImgTipo.PageIndex = e.NewPageIndex;
            gvImgTipo.DataBind();
        }

        public void carregarGvUsuarios()
        {
            BLL ObjUsuarios = new BLL(conectSite);

            ObjUsuarios.Campo = " * ";
            ObjUsuarios.Tabela = " st_usuario ";
            ObjUsuarios.Condicao = "";

            DataTable dados = ObjUsuarios.RetCampos();

            gvUsuarios.DataSource = dados;
            gvUsuarios.DataBind();
        }

        public void paginarGwUsuarios(object sender, GridViewPageEventArgs e)
        {
            BLL ObjUsuarios = new BLL(conectSite);

            ObjUsuarios.Campo = " * ";
            ObjUsuarios.Tabela = " st_usuario ";
            ObjUsuarios.Condicao = "";

            DataTable dados = ObjUsuarios.RetCampos();

            gvUsuarios.DataSource = dados;
            gvUsuarios.PageIndex = e.NewPageIndex;
            gvUsuarios.DataBind();
        }

        public void carregarGvTipo()
        {
            BLL ObjTipo = new BLL(conectSite);

            ObjTipo.Campo = " * ";
            ObjTipo.Tabela = " st_tipo ";
            ObjTipo.Condicao = " ORDER BY cadmom DESC ";

            gvTipo.DataSource = ObjTipo.RetCampos();
            gvTipo.DataBind();
        }

        public void paginarGwTipo(object sender, GridViewPageEventArgs e)
        {
            BLL ObjTipo = new BLL(conectSite);

            ObjTipo.Campo = " * ";
            ObjTipo.Tabela = " st_tipo ";
            ObjTipo.Condicao = " ORDER BY cadmom DESC ";

            gvTipo.DataSource = ObjTipo.RetCampos();
            gvTipo.PageIndex = e.NewPageIndex;
            gvTipo.DataBind();
        }

        public void carregarGvImgCampoCont()
        {
            BLL ObjImgCampo = new BLL(conectSite);

            ObjImgCampo.Campo = " * ";
            ObjImgCampo.Tabela = " st_imgcampoconteudo ";
            ObjImgCampo.Condicao = " ORDER BY cadmom ";

            gvImgCampoConteudo.DataSource = ObjImgCampo.RetCampos();
            gvImgCampoConteudo.DataBind();
        }

        public void paginarGvImgCampoCont(object sender, GridViewPageEventArgs e)
        {
            BLL ObjImgCampo = new BLL(conectSite);

            ObjImgCampo.Campo = " * ";
            ObjImgCampo.Tabela = " st_imgcampoconteudo ";
            ObjImgCampo.Condicao = " ORDER BY cadmom ";

            gvImgCampoConteudo.DataSource = ObjImgCampo.RetCampos();
            gvImgCampoConteudo.PageIndex = e.NewPageIndex;
            gvImgCampoConteudo.DataBind();
        }
        public void carregarGvImgPosicao()
        {
            BLL ObjPosicao = new BLL(conectSite);

            ObjPosicao.Campo = " * ";
            ObjPosicao.Tabela = " st_imgposicao ";
            ObjPosicao.Condicao = " ORDER BY cadmom ";

            gvImgPosicao.DataSource = ObjPosicao.RetCampos();
            gvImgPosicao.DataBind();
        }

        public void paginarGvImgPosicao(object sender, GridViewPageEventArgs e)
        {
            BLL ObjPosicao = new BLL(conectSite);

            ObjPosicao.Campo = " * ";
            ObjPosicao.Tabela = " st_imgposicao ";
            ObjPosicao.Condicao = " ORDER BY cadmom ";

            gvImgPosicao.DataSource = ObjPosicao.RetCampos();
            gvImgPosicao.PageIndex = e.NewPageIndex;
            gvImgPosicao.DataBind();
        }
        public void carregarGvImgAlinha()
        {
            BLL ObjAlinha = new BLL(conectSite);

            ObjAlinha.Campo = " * ";
            ObjAlinha.Tabela = " st_imgalinhamento ";
            ObjAlinha.Condicao = " ORDER BY cadmom ";

            gvImgAlinha.DataSource = ObjAlinha.RetCampos();
            gvImgAlinha.DataBind();
        }

        public void paginarGvImgAlinha(object sender, GridViewPageEventArgs e)
        {
            BLL ObjAlinha = new BLL(conectSite);

            ObjAlinha.Campo = " * ";
            ObjAlinha.Tabela = " st_imgalinhamento ";
            ObjAlinha.Condicao = " ORDER BY cadmom ";

            gvImgAlinha.DataSource = ObjAlinha.RetCampos();
            gvImgAlinha.PageIndex = e.NewPageIndex;
            gvImgAlinha.DataBind();
        }        
        protected void cadastrarEmpresa(object sender, EventArgs e)
        {
            BLL ObjEmpresa = new BLL(conectSite);

            string tabela = " " + " st_empresa ";
            string campos = " nome, cadusu, cadmom ";
            string valores = String.Format("'" + iNome.Value + "'" + ", " +
                                           "'" + Session["LoginUsuario"].ToString() + "'" + ", " +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            ObjEmpresa.Tabela = tabela;
            ObjEmpresa.Campo = campos;
            ObjEmpresa.Valores = valores;

            string sql = tabela + campos + valores;
            if (ObjEmpresa.MsgErro == "")
            {
                ObjEmpresa.InsertRegistro(tabela, campos, valores);
                lblResult.Text = "Cadastro realizado com Sucesso!!!";

                iNome.Value = String.Empty;
                iNome.Focus();

                //MessageBox.Show(ObjEmpresa.Msg);
            }
            else
            {
                MessageBox.Show(ObjEmpresa.MsgErro);
            }
        }
        protected void editarEmpresa(object sender, EventArgs e)
        {
            string idEmpresa = lblIdEmpresa.Text;

            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_empresa ";
            string valores = " nome = " + "'" + iNome.Value + "'" +
                             ", altusu = " + "'" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " id = " + "'" + idEmpresa + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!!!";
            }

            //MessageBox.Show(ObjDados.Msg);
            //MessageBox.Show("UPDATE" + tabela + " SET " + valores + condicao);
            carregarGvEmpresa();

        }                
        protected void ativarMotivo(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            //System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;

            if (btn.Name == "btnMotExcEmpresa")
            {
                iExcluEmpresaMot.Visible = true;
                btnExcEmpresa.Visible = true;
                btnMotExcEmpresa.Visible = false;
            }
            else
            {
                lblMsg.Text = "Algo Errado...";
            }
            
            /*iExcluEmpresaMot.Visible = true;
            btnExcEmpresa.Visible = true;
            btnMotExcEmpresa.Visible = false;*/
        }
        protected void excluirEmpresa(object sender, EventArgs e)
        {
            string idEmpresa = lblIdEmpresa.Text;

            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_empresa ";
            string valores = ", canusu = " + "'" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmot = " + "'" + iExcluEmpresaMot.Value + "'" +
                             ", canmom = " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " id = " + "'" + idEmpresa + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {                
                if (!String.IsNullOrEmpty(iExcluEmpresaMot.Value))
                {
                    ObjDados.EditRegistro(tabela, valores, condicao);

                    lblMsg.Text = "Exclusão realizada com sucesso!!!";
                    //MessageBox.Show(ObjDados.Msg);
                    btnExcEmpresa.Visible = false;
                    btnMotExcEmpresa.Visible = true;
                    iExcluEmpresaMot.Visible = false;
                    iExcluEmpresaMot.Value = String.Empty;
                }
                else
                {
                    lblMsg.Text = "Obrigatório preeenchimento do Motivo";
                }
                
            }

           
            
            carregarGvEmpresa();
        }
        protected void cadastrarCategoria(object sender, EventArgs e)
        {

            BLL ObjCategoria = new BLL(conectSite);

            string tabela = " st_categoria ";
            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string condicao = " WHERE cod = '" + iCodCategoria.Value + "' ";
            string valores = String.Format("'" + iCodCategoria.Value + "'," + "'" + iDescCategoria.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjCategoria.Tabela = tabela;
            ObjCategoria.Campo = campos;
            ObjCategoria.Condicao = condicao;
            ObjCategoria.Valores = valores;

            DataTable dados = ObjCategoria.RetCampos();

            if (ObjCategoria.MsgErro == "")
            {
                //MessageBox.Show(ObjCategoria.Msg);

                int contador = dados.Rows.Count;

                //Checa se há registros, para não Duplicar
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                    iCodCategoria.Focus();
                }
                else
                {
                    carregarGvCategoria();

                    //Inserir dados
                    ObjCategoria.InsertRegistro(tabela, campos, valores);
                    lblResult.Text = String.Empty;
                    iCodCategoria.Value = String.Empty;
                    iDescCategoria.Value = String.Empty;
                    iCodCategoria.Focus();

                    //MessageBox.Show(ObjCategoria.Msg);
                }
            }
            else
            {
                MessageBox.Show(ObjCategoria.MsgErro);
            }
        }
        protected void editarCategoria(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_categoria ";
            string valores = " cod = '" + iCodCategoria.Value + "'" +
                             ", descricao = '" + iDescCategoria.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";                                              
            string condicao = " cod = '" + lblCodCategoria.Text + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescCategoria.Value = String.Empty;
            iCodCategoria.Value = String.Empty;
            carregarGvCategoria();
        }
        protected void excluirCategoria(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_categoria ";
            string valores = ", canmot = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + lblCodCategoria.Text + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Excluisão realizada com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescCategoria.Value = String.Empty;
            iCodCategoria.Value = String.Empty;
            carregarGvCategoria();
        }
        protected void cadastrarMenu(object sender, EventArgs e)
        {
            BLL ObjMenu = new BLL(conectSite);

            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_menu ";
            string condicao = " WHERE cod = '" + iCodMenu.Value + "'";
            string valores = String.Format("'" + iCodMenu.Value + "'," + "'" + iDescMenu.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjMenu.Tabela = tabela;
            ObjMenu.Campo = campos;
            ObjMenu.Condicao = condicao;
            ObjMenu.Valores = valores;

            DataTable dados = ObjMenu.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjMenu.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                    iCodMenu.Focus();
                }
                else
                {
                    ObjMenu.InsertRegistro(tabela, campos, valores);

                    //MessageBox.Show(ObjMenu.Msg);
                    carregarGvMenu();

                    lblResult.Text = String.Empty;
                    iCodMenu.Value = String.Empty;
                    iDescMenu.Value = String.Empty;
                    lblResult.Text = String.Empty;
                    iCodMenu.Focus();
                }
            }
            else
            {
                MessageBox.Show(ObjMenu.MsgErro);
            }
        }
        protected void editarMenu(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codMenu = lblCodMenu.Text;

            string tabela = " st_menu ";
            string valores = " cod = '" + iCodMenu.Value + "'" +                             
                             ", descricao = '" + iDescMenu.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + lblCodMenu.Text + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescMenu.Value = String.Empty;
            iCodMenu.Value = String.Empty;
            carregarGvMenu();
        }
        protected void excluirMenu(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codMenu = lblCodMenu.Text;

            string tabela = " st_menu ";
            string valores = ", canusu = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + lblCodMenu.Text + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescMenu.Value = String.Empty;
            iCodMenu.Value = String.Empty;
            carregarGvMenu();
        }


        //Tipo Imagens

        protected void cadastrarTipoImg(object sender, EventArgs e)
        {
            BLL ObjTipoImg = new BLL(conectSite);

            string campos = " cod, titulo, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_img_tipo ";
            string condicao = "  WHERE cod = '" + iCodTipoImg.Value + "' ";
            string valores = String.Format("'" + iCodTipoImg.Value + "'" + ", " + "'" + iTituloTipoImg.Value + "'" + ", " + "'" + iDescTipoImg.Value + "'" + ", " + "'" + Session["LoginUsuario"].ToString() + "'" + ", " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + "'" + 1 + "'");

            ObjTipoImg.Tabela = tabela;
            ObjTipoImg.Campo = campos;
            ObjTipoImg.Condicao = condicao;
            ObjTipoImg.Valores = valores;

            DataTable dados = ObjTipoImg.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjTipoImg.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblMsg.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                    iCodTipo.Focus();
                }
                else
                {
                    carregarGvTipoImg();

                    ObjTipoImg.InsertRegistro(tabela, campos, valores);

                    //MessageBox.Show("INSERT INTO " + tabela +  " (" + campos + ") " + "values (" + valores +")");

                    //MessageBox.Show(ObjTipoImg.Msg);

                    lblMsg.Text = "Cadastro realizado com Sucesso!";

                    iCodTipoImg.Value = String.Empty;
                    iTituloTipoImg.Value = String.Empty;
                    iDescTipoImg.Value = String.Empty;

                    iCodTipoImg.Focus();
                }
            }
            else
            {
                MessageBox.Show(ObjTipoImg.MsgErro);
            }
        }
        protected void editarTipoImg(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codTipoImg = lblCodImgTipo.Text;

            string tabela = " st_img_tipo ";
            string valores = " cod = '" + codTipoImg + "'" +
                             ", titulo = '" + iTituloTipoImg.Value + "'" +
                             ", descricao = '" + iDescTipoImg.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codTipoImg + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescTipoImg.Value = String.Empty;
            iCodTipoImg.Value = String.Empty;
            carregarGvTipoImg();
        }
        protected void excluirTipoImg(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codTipoImg = lblCodImgTipo.Text;

            string tabela = " st_img_tipo ";
            string valores = ", canmot = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codTipoImg + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescTipoImg.Value = String.Empty;
            iCodTipoImg.Value = String.Empty;
            carregarGvTipoImg();
        }

        //*Fim  Tipo Imagens
        protected void cadastrarTipo(object sender, EventArgs e)
        {
            BLL ObjTipo = new BLL(conectSite);

            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_tipo ";
            string condicao = " WHERE cod = '" + iCodTipo.Value + "' ";
            string valores = String.Format("'" + iCodTipo.Value + "'," + "'" + iDescTipo.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjTipo.Tabela = tabela;
            ObjTipo.Campo = campos;
            ObjTipo.Condicao = condicao;
            ObjTipo.Valores = valores;

            DataTable dados = ObjTipo.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjTipo.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                    iCodTipo.Focus();
                }
                else
                {
                    string Msg = "";
                    Msg = "Erro: " + Convert.ToString(ObjTipo.Msg);
                    lblResult.Text = Msg;

                    ObjTipo.InsertRegistro(tabela, campos, valores);

                    carregarGvTipo();

                    lblResult.Text = String.Empty;
                    iCodTipo.Value = String.Empty;
                    iDescTipo.Value = String.Empty;
                    iCodTipo.Focus();

                    //MessageBox.Show(ObjTipo.Msg);
                }
            }
            else
            {
                MessageBox.Show(ObjTipo.MsgErro);
            }
        }
        protected void editarTipo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codTipo = lblCodTipo.Text;

            string tabela = " st_tipo ";
            string valores = " cod = '" + codTipo + "'" +                             
                             ", descricao = '" + iDescTipo.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codTipo + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescTipo.Value = String.Empty;
            iCodTipo.Value = String.Empty;
            carregarGvTipo();
        }
        protected void excluirTipo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codTipo = lblCodTipo.Text;

            string tabela = " st_tipo ";
            string valores = ", canusu = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codTipo + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iDescTipo.Value = String.Empty;
            iCodTipo.Value = String.Empty;
            carregarGvTipo();
        }
        protected void cadastrarUsuario(object sender, EventArgs e)
        {
            BLL ObjUsuario = new BLL(conectSite);

            string nome = iNomeUsuario.Value;
            string usuario = iUsuarioUsu.Value;
            string senha = iSenhaUsuario.Value;
            string email = iEmailUsuario.Value;
            string cpf = iCPFUsuario.Value;


            string tabela = " st_usuario ";
            string campos = " nome, usuario, senha, email, cpf, cadusu, cadmom, idempresa, idpessoa ";
            string valores = String.Format("'" + nome + "'" + ", " + "'" + usuario + "'" + ", " + "'" + senha + "'" + ", " + "'" + email + "'" + ", " + "'" + cpf + "'" + ", " + "'" + Session["LoginUsuario"].ToString() + "'" + ", " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + "'" + 1 + "'" + ", " + "'" + 1 + "'");
            string sql = "";

#pragma warning disable CS0219 // A variável "contador" é atribuída, mas seu valor nunca é usado
            int contador = 0;
#pragma warning restore CS0219 // A variável "contador" é atribuída, mas seu valor nunca é usado

            ObjUsuario.Tabela = tabela;
            ObjUsuario.Campo = campos;
            ObjUsuario.Valores = valores;

            sql = tabela + campos + valores;

            if (ObjUsuario.MsgErro == "")
            {
                ObjUsuario.InsertRegistro(tabela, campos, valores);

                iNomeUsuario.Focus();

                iNomeUsuario.Value = String.Empty;
                iUsuarioUsu.Value = String.Empty;
                iSenhaUsuario.Value = String.Empty;
                iEmailUsuario.Value = String.Empty;
                iCPFUsuario.Value = String.Empty;

                lblResult.Text = "Cadastro realizado com Sucesso!!!";

                //MessageBox.Show(ObjUsuario.Msg);
            }
            else
            {
                MessageBox.Show("Erro: " + ObjUsuario.MsgErro);
            }
        }
        protected void editarUsuario(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string idUsuario = lblIdUsuario.Text;

            string tabela = " st_usuario ";
            string valores = " id = '" + idUsuario + "'" +
                             ", nome = '" + iNomeUsuario.Value + "'" +
                             ", usuario = '" + iUsuarioUsu.Value + "'" +
                             ", senha = '" + iSenhaUsuario.Value + "'" +
                             ", email = '" + iEmailUsuario.Value + "'" +
                             ", cpf = '" + iCPFUsuario.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " id = '" + idUsuario + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iNomeUsuario.Value = String.Empty;
            iUsuarioUsu.Value = String.Empty;
            iSenhaUsuario.Value = String.Empty;
            iEmailUsuario.Value = String.Empty;
            iCPFUsuario.Value = String.Empty;
                        
            carregarGvUsuarios();
        }
        protected void excluirUsuario(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string idUsuario = lblIdUsuario.Text;

            string tabela = " st_usuario ";
            string valores = ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canusu = '" + "Testes" + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " id = '" + idUsuario + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iNomeUsuario.Value = String.Empty;
            iUsuarioUsu.Value = String.Empty;
            iSenhaUsuario.Value = String.Empty;
            iEmailUsuario.Value = String.Empty;
            iCPFUsuario.Value = String.Empty;

            carregarGvUsuarios();
        }
        protected void cadastrarImgCampoConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_imgcampoconteudo ";
            string condicao = " WHERE cod = '" + iCodCampConteudo.Value + "' ";
            string valores = String.Format("'" + iCodCampConteudo.Value + "'," + "'" + iDescCampConteudo.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;
            ObjDados.Valores = valores;

            DataTable dados = ObjDados.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjDados.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                    iCodCampConteudo.Focus();
                }
                else
                {
                    ObjDados.InsertRegistro(tabela, campos, valores);

                    //MessageBox.Show(ObjDados.Msg);

                    carregarGvImgCampoCont();

                    iCodCampConteudo.Value = String.Empty;
                    iDescCampConteudo.Value = String.Empty;
                    lblResult.Text = String.Empty;
                    iCodCampConteudo.Focus();
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }
        }
        protected void editarImgCampoConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codCampoConteudo = lblImgCampoConteudo.Text;

            string tabela = " st_imgcampoconteudo ";
            string valores = " cod = '" + codCampoConteudo + "'" +
                             ", descricao = '" + iDescCampConteudo.Value + "'" +                             
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codCampoConteudo + "'";
                        

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodCampConteudo.Value = String.Empty;
            iDescCampConteudo.Value = String.Empty;            

            carregarGvImgCampoCont();
        }
        protected void excluirImgCampoConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codCampoConteudo = lblImgCampoConteudo.Text;

            string tabela = " st_imgcampoconteudo ";
            string valores = ", canmot = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codCampoConteudo + "'";


            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodCampConteudo.Value = String.Empty;
            iDescCampConteudo.Value = String.Empty;

            carregarGvImgCampoCont();
        }
        protected void cadastrarImgPosicao(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_imgposicao ";
            string condicao = " WHERE cod = '" + iCodPosicao.Value + "' ";
            string valores = String.Format("'" + iCodPosicao.Value + "'," + "'" + iDescPosicao.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;
            ObjDados.Valores = valores;

            DataTable dados = ObjDados.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjDados.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                }
                else
                {
                    ObjDados.InsertRegistro(tabela, campos, valores);

                    //MessageBox.Show(ObjDados.Msg);

                    carregarGvImgPosicao();

                    iCodPosicao.Value = String.Empty;
                    iDescPosicao.Value = String.Empty;
                    lblResult.Text = String.Empty;
                    iCodPosicao.Focus();
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }

        }
        protected void editarImgPosicao(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codImgPosicao = lblCodImgPosicao.Text;

            string tabela = " st_imgposicao ";
            string valores = " cod = '" + codImgPosicao + "'" +
                             ", descricao = '" + iDescPosicao.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codImgPosicao + "'";


            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodPosicao.Value = String.Empty;
            iDescPosicao.Value = String.Empty;

            carregarGvImgPosicao();
        }
        protected void excluirImgPosicao(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codImgPosicao = lblCodImgPosicao.Text;

            string tabela = " st_imgposicao ";
            string valores = ", canmot = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codImgPosicao + "'";


            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodPosicao.Value = String.Empty;
            iDescPosicao.Value = String.Empty;

            carregarGvImgPosicao();
        }
        protected void cadastrarImgAlinhamento(object sender, EventArgs e)
        {

            BLL ObjDados = new BLL(conectSite);

            string campos = " cod, descricao, cadusu, cadmom, id_empresa ";
            string tabela = " st_imgalinhamento ";
            string condicao = " WHERE cod = '" + iCodImgAlinha.Value + "' ";
            string valores = String.Format("'" + iCodImgAlinha.Value + "'," + "'" + iDescImgAlinha.Value + "'," + "'" + Session["LoginUsuario"].ToString() + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + 1 + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;
            ObjDados.Valores = valores;

            DataTable dados = ObjDados.RetCampos();
            int contador = dados.Rows.Count;

            if (ObjDados.MsgErro == "")
            {
                if (contador > 0)
                {
                    lblResult.Text = "Este Cógido: " + dados.Rows[0]["cod"].ToString() + ", já existe. Crie um novo COD e tente novamente!!!";
                }
                else
                {
                    ObjDados.InsertRegistro(tabela, campos, valores);

                    //MessageBox.Show(ObjDados.Msg);

                    carregarGvImgAlinha();

                    iCodImgAlinha.Value = String.Empty;
                    iDescImgAlinha.Value = String.Empty;
                    lblResult.Text = String.Empty;
                    iCodImgAlinha.Focus();
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }

        }
        protected void editarImgAlinhamento(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codImgAlinha = lblCodImgAlinha.Text;

            string tabela = " st_imgalinhamento ";
            string valores = " cod = '" + codImgAlinha + "'" +
                             ", descricao = '" + iDescImgAlinha.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codImgAlinha + "'";


            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodImgAlinha.Value = String.Empty;
            iDescImgAlinha.Value = String.Empty;

            carregarGvImgAlinha();
        }
        protected void excluirImgAlinhamento(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string codImgAlinha = lblCodImgAlinha.Text;

            string tabela = " st_imgalinhamento ";
            string valores = ", canmot = '" + "Testes" + "'" +
                             ", canusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " cod = '" + codImgAlinha + "'";


            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iCodImgAlinha.Value = String.Empty;
            iDescImgAlinha.Value = String.Empty;

            carregarGvImgAlinha();
        }

        protected void paginarGwConteudo(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " id, id_conteudo, codtipo AS tipo, cod_menu AS destaque, titulo, descritivo, path_img, fonte, autor, ordem, hint, cadusu, cadmom ";
            string campos = " st_imagens ";
            string condicao = " order by cadmom desc ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            gvImagens.DataSource = ObjDados.RetCampos();
            gvImagens.PageIndex = e.NewPageIndex;
            gvImagens.DataBind();
        }

        protected void editarImagem(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string idImagem = lbliIdImagem.Text;

            string tabela = " st_imagens ";
            string valores = " id = '" + idImagem + "'" +
                             ", titulo = '" + iImgTitulo.Value + "'" +
                             ", descritivo = '" + iImgDescricao.Value + "'" +
                             ", fonte = '" + iImgFonte.Value + "'" +
                             ", autor = '" + iImgAutor.Value + "'" +
                             ", path_img = '" + iImgPath.Value + "'" +
                             ", altusu = '" + Session["LoginUsuario"].ToString() + "'" +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";            
            string condicao = " cod = '" + idImagem + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Valores = valores;
            ObjDados.Condicao = condicao;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.EditRegistro(tabela, valores, condicao);
                lblMsg.Text = "Dados alterados com sucesso!";
            }
            //MessageBox.Show(ObjDados.Msg);

            iImgTitulo.Value = String.Empty;
            iImgDescricao.Value = String.Empty;
            iImgFonte.Value = String.Empty;
            iImgAutor.Value = String.Empty;
            iImgPath.Value = String.Empty;

            carregarGvImagens();            

        }

        /*Fim*/
    }
}