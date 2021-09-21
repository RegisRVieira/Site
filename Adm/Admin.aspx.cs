﻿using System;
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
    public partial class Admin : System.Web.UI.Page
    {

        /*
        AdmBLL objDB = new AdmBLL();
        Login objLogin = new Login();
        BLL objDBASU = new BLL();
        */
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
            string IdEmpSel;
            //IdEmpSel = gvEmpresa.SelectedValue.ToString();

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
            ultimoRegistro();

            BLL ObjDados = new BLL(conectVegas);

            //MessageBox.Show("Último Registro Cadastrado: " + UltimoRegistro);

            ObjDados.Campo = "" + " c.titulo, c.fonte, c.autor, i.path_img, i.titulo, i.descritivo, i.cadusu, t.descricao ";
            ObjDados.Tabela = "" + " st_imagens AS i ";
            ObjDados.Left = "" + " INNER JOIN st_conteudo AS c ON i.id_conteudo = c.id " +
                                 " INNER JOIN st_tipo AS t ON t.cod = c.cod_tipo ";
            //ObjDbASU.Condicao = " WHERE i.id_conteudo = '55' ";
            ObjDados.Condicao = " WHERE i.id_conteudo = '" + UltimoRegistro + "' ";

            gvImagens.DataSource = ObjDados.RetCampos();
            gvImagens.DataBind();
        }

        protected void cadastrarEmpresa(object sender, EventArgs e)
        {
            BLL ObjEmpresa = new BLL(conectSite);

            string tabela = " " + " st_empresa ";
            string campos = " nome, cadusu, cadmom ";
            string valores = String.Format("'" + iNome.Value + "'" + ", " + "'" + Session["LoginUsuario"].ToString() + "'" + ", " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

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
            MessageBox.Show("Edição de Empresa");
        }

        protected void excluirEmpresa(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Empresa");
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

                    MessageBox.Show(ObjCategoria.Msg);
                }
            }
            else
            {
                MessageBox.Show(ObjCategoria.MsgErro);
            }
        }
        protected void editarCategoria(object sender, EventArgs e)
        {
            MessageBox.Show("Edição de Categoria");
        }
        protected void excluirCategoria(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Categoria");
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
            MessageBox.Show("Edição de Menu");
        }

        protected void excluirMenu(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Menu");
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
            MessageBox.Show("Edição de Tipo Imagem");
        }

        protected void excluirTipoImg(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Tipo Imagem");
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
            MessageBox.Show("Edição de Tipo");
        }

        protected void excluirTipo(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Tipo");
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

            int contador = 0;

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
            MessageBox.Show("Edição de Usuários");
        }

        protected void excluirUsuario(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão de Usuários");
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
            MessageBox.Show("Edição em Construção");
        }

        protected void excluirImgCampoConteudo(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão em Construção");
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
            MessageBox.Show("Edição em Construção");
        }

        protected void excluirImgPosicao(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão em Construção");
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

                    MessageBox.Show(ObjDados.Msg);

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
            MessageBox.Show("Edição em Construção");
        }

        protected void excluirImgAlinhamento(object sender, EventArgs e)
        {
            MessageBox.Show("Exclusão em Construção");
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

        protected void cadastrarImagem(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjDbASU = new BLL(conectSite);
            //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
            //Encontrar uma forma para Não ocorrer este erro na página
            double tamanho = fuImgCont.PostedFile.ContentLength;
            tamanho = tamanho / 1024;

            string arquivo = "";

            lblResp.InnerText = arquivo + " / " + tamanho + "Kb";

            string icampos = " id, id_conteudo, codtipo AS tipo, cod_menu AS destaque, titulo, descritivo, path_img, fonte, autor, ordem, hint, cadusu, cadmom ";
            string itabela = " st_imagens ";
            string icondicao = " order by cadmom desc ";

            ObjImg.Tabela = itabela;
            ObjImg.Campo = icampos;
            ObjImg.Condicao = icondicao;

            if (fuImgCont.PostedFile.ContentLength < 4500000)
            {
                //MessageBox.Show("Deu certo...");

                //Validando tipo de arquivo
                if (fuImgCont.PostedFile.ContentType == "image/jpeg" ||
                fuImgCont.PostedFile.ContentType == "image/png" ||
                fuImgCont.PostedFile.ContentType == "image/gif")
                {
                    //MessageBox.Show("Blzura, no caminho certo");
                    try
                    {
                        //Gravar Arquivo

                        string caminho = Server.MapPath(@"~/Img/Conteudo/").Replace(@"\", "/");
                        string caminho2 = ("/Img/Conteudo/").Replace(@"\", "/");
                        arquivo = fuImgCont.FileName;

                        DataTable dados = ObjImg.RetCampos();

                        string xRet = " ";
                        //int nLinhas = dados.Rows.Count;

                        string path = caminho.Replace(@"\", "/");

                        fuImgCont.PostedFile.SaveAs(caminho + arquivo);

                        MessageBox.Show("UpLoad de Arquivo realizado com Sucesso");

                        //Cadastrar dados no DB

                        ObjDados.Campo = " " + " * ";
                        ObjDados.Tabela = " " + " st_conteudo ";
                        ObjDados.Left = "";
                        ObjDados.Condicao = " " + " ORDER BY id DESC LIMIT 1  ";

                        DataTable dados2 = ObjDados.RetCampos();

                        int contador = dados2.Rows.Count;
                        int vRegistro = 0;
                        UltimoRegistro = dados2.Rows[0]["id"].ToString();

                        if (contador > 0)
                        {
                            vRegistro = Convert.ToInt32(UltimoRegistro);
                        }
                        /*
                        MessageBox.Show("Você tem: " + UltimoRegistro + " Registros na Tabela" + "\n" + "\n" +
                                            "Agora você terá: " + vRegistro + "\n" + usu.usuarioLogado);
                        */

                        string campos = " id_empresa, id_conteudo, id_noticia, codtipo, cod_destaque, cod_CampoConteudo, cod_posicao, cod_alinhamento, titulo, descritivo, path_img,   fonte, autor, ordem,   hint, cadusu, cadmom ";
                        string tabela = " st_imagens ";
                        string valores = String.Format("'" + 1 + "'," + "'" + vRegistro + "'," + "'" + 2 + "'," + "'" + stImgTipo.Value + "'," + "'" + stImgCampoConteudo.Value + "'," + "'" + "'" +
                                                        stImgPosicao.Value + "'," + "'" + stImgAlinha.Value + "'," + iImgTitulo.Value + "'," + "'" + iImgDescricao.Value + "'," + "'" + caminho2 + arquivo + "'," +
                                                        "'" + iImgFonte.Value + "'," + "'" + iImgAutor.Value + "'," + "'" + iImgHint.Value + "'," + "'" + Session["LoginUsuario"] + "'");

                        ObjImg.Campo = campos;
                        ObjImg.Tabela = tabela;
                        ObjImg.Valores = valores;

                        ObjImg.InsertRegistro(tabela, campos, valores);


                        MessageBox.Show(ObjImg.Msg);

                        //MessageBox.Show("Destaque: " + stImgDestaque.Value + "\n" +
                        //              "Tipo: " + stImgTipo.Value);

                        /*
                        MessageBox.Show("Caminho: " + caminho + "\n" +
                                       "Caminho 2: " + caminho2 + "\n" +
                                       " - - - - - - - - - - " + "\n" +
                                       "Path: " + path);
                        */

                        ObjDbASU.Campo = " " + " * ";
                        ObjDbASU.Tabela = " " + " st_imagens ";
                        ObjDbASU.Left = "";
                        ObjDbASU.Condicao = " " + " WHERE id_conteudo = '" + vRegistro + "' ";

                        DataTable dadosImg = ObjDbASU.RetCampos();
                        int contadorImg = 0;
                        contadorImg = dadosImg.Rows.Count;

                        xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                        xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                        xRet += "</div>";
                        for (int i = 0; i < contadorImg; i++)
                        {
                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                            xRet += "<div style='width: 150px; display: inline-block;'>" + dadosImg.Rows[i]["titulo"] + "</div>";
                            xRet += "<div style='width: 300px; display: inline-block;'>" + dadosImg.Rows[i]["descritivo"] + "</div>";
                            //xRet += "<div style='width: 100px; display: inline-block;'>"  + dados.Rows[i]["path_img"] + "</div>";                
                            xRet += "<div style='width: 50px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'../.." + dadosImg.Rows[i]["path_img"] + "'" + "/>" + "</div>";
                            //xRet += "<img style='width: 50px' src =" + path + "/>";
                            xRet += "</div>";

                            //MessageBox.Show(xRet += "<img style='width: 100px' src =" + "'../.." + dados.Rows[i]["path_img"] +"'" + "/>");
                        }

                        lblDados.Text = xRet;



                        //Limpar Campos
                        iImgTitulo.Value = String.Empty;
                        iImgDescricao.Value = String.Empty;
                        //iArquivoUpLoad.Value = String.Empty;
                        //iTamanhoUpLoad.Value = String.Empty;
                        //iPathImg.Value = String.Empty;
                        iImgFonte.Value = String.Empty;
                        iImgAutor.Value = String.Empty;
                        iImgOrdem.Value = String.Empty;
                        iImgHint.Value = String.Empty;

                        mwImg.ActiveViewIndex = 0;
                        carregarGvImagens();

                        iImgTitulo.Focus();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Tipo de arquivo não suportado");
                }
            }
            else
            {
                MessageBox.Show("Imagem maior que o permitido: " + tamanho / 1024 + "Mb, máximo de 4.5Mb");
            }
        }

        /*Fim*/
    }
}