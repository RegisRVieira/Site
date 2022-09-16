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
    public partial class Conteudo : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                carregarGvConteudo();
                popularStCategoria();                
                popularStTipo();                
            }
            this.DataBind();
        }

        protected void carregarData(object sender, EventArgs e)
        {            
            int periodoNE = Convert.ToInt32(iNEDuracaoPub.Value);
            //int periodoB100 = Convert.ToInt32(iB100DuracaoPub.Value);


            DateTime hoje = DateTime.Now;
            DateTime p30 = DateTime.Now.AddDays(periodoNE);
            //DateTime p30B33 = DateTime.Now.AddDays(periodoB33);
            //DateTime p30B100 = DateTime.Now.AddDays(periodoB100);



            iPubIniNE.Value = hoje.ToString("yyyy-MM-dd");
            iPublFimNE.Value = p30.ToString("yyyy-MM-dd");

            iPubIniNE.Value = hoje.ToString("yyyy-MM-dd");
            iPubIniNE.Value = DateTime.Now.AddDays(periodoNE).ToString("yyyyy-MM-dd");
                     

            iPubIniNE.Value = hoje.ToString("yyyy-MM-dd");

        }
        public void cadastrarNossaEntidade(object sender, EventArgs e)
        {
            string xRet = "";

            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            DateTime dt_ini = DateTime.Now;
            DateTime dt_fin = DateTime.Now;

            iPubIniNE.Value = dt_ini.ToString("yyyy-MM-dd HH:mm:ss");
            iPublFimNE.Value = dt_fin.ToString("yyyy-MM-dd HH:mm:ss");

            string tabela = " st_conteudo ";
            string campos = " cod_tipo, titulo, introducao, dt_publIni, dt_PublFim, dt_cad ";
            string campoID = " * ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";
            string valores = String.Format("'" + "NENT" + "'," +
                                           "'" + iTitulo.Value + "'," +
                                           "'" + taIntroducao.Value.ToString() + "'," +
                                           "'" + iPubIniNE.Value + "'," +
                                           "'" + iPublFimNE.Value + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //Dados para Inserção
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (!String.IsNullOrEmpty(iTitulo.Value))
            {
                ObjDados.InsertRegistro(tabela, campos, valores);

                iImgTitulo.Focus();
                lblMsg.Text = "Cadastro Realizado com Sucesso!";
                iValidador.Value = "Validado";
                btnCadNossaEntidade.Enabled = false;
            }
            else
            {
                iTitulo.Value = String.Empty;
                taIntroducao.Value = String.Empty;

                lblMsg.Text = "É preciso preencher os Dados!!!";
                iTitulo.Focus();
            }

            //MessageBox.Show("INSERT INTO" + tabela + "(" + campos + ")" + " VALUES(" + valores + ")");

            //Dados para Validação
            ObjValida.Tabela = tabela;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condicao;

        }//cadastrarContSlider
        protected void cadastrarImagens(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " " + " * ";
            ObjDados.Tabela = " " + " st_conteudo ";
            ObjDados.Left = "";
            ObjDados.Condicao = " " + " ORDER BY id DESC LIMIT 1 ";

            DataTable dados = ObjDados.RetCampos();

            int contador = dados.Rows.Count;
            int vRegistro = 0;

            if (ObjDados.MsgErro == "")
            {
                if (contador > 0)
                {
                    string ultimoRegistro = dados.Rows[0]["id"].ToString();

                    vRegistro = Convert.ToInt32(ultimoRegistro) + 1;

                    MessageBox.Show("Você tem: " + ultimoRegistro + " Registros na Tabela" + "\n" + "\n" +
                                    "Agora você terá: " + vRegistro);
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }

            MessageBox.Show(ObjDados.Msg);

            Response.Redirect("S-Imagens.aspx");
            //Response.Redirect("~/Default.aspx", "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
        }

        protected void ativarVwConteudo(object sender, EventArgs e)
        {
            mwGridConteudo.ActiveViewIndex = 0;
            mwFormConteudo.ActiveViewIndex = 0;
            carregarGvConteudo();
        }

        protected void carregarGvConteudo()
        {
            BLL ObjDados = new BLL();

            ObjDados.Campo = " " + " id, id_empresa, cod_menu, cod_categoria, titulo, conteudo, dt_publini, dt_publfim, fonte, autor, ordem, cadmom, cadusu ";
            ObjDados.Tabela = " " + " st_conteudo ";
            ObjDados.Left = "";
            ObjDados.Condicao = "WHERE cod_menu = 'NEN'";

            gvConteudo.DataSource = ObjDados.RetCampos();
            gvConteudo.DataBind();
        }

        protected void paginarGwConteudo(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_conteudo ";
            string campos = " * ";
            string condicao = "";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            gvConteudo.DataSource = ObjDados.RetCampos();
            gvConteudo.PageIndex = e.NewPageIndex;
            gvConteudo.DataBind();
        }

        protected void carregarGvNossaEntidade()
        {
            BLL ObjDados = new BLL();

            ObjDados.Campo = " " + " id, id_empresa, cod_menu, cod_categoria, titulo, conteudo, dt_publini, dt_publfim, fonte, autor, ordem, cadmom, cadusu ";
            ObjDados.Tabela = " " + " st_conteudo ";
            ObjDados.Left = "";
            //ObjDados.Condicao = " WHERE cod_categoria = 'NENT'";
            ObjDados.Condicao = " WHERE cod_categoria = 'NOV' "; //Teste

            GvNossaEntidade.DataSource = ObjDados.RetCampos();
            GvNossaEntidade.DataBind();
        }
        protected void paginarGvNossaEntidade(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_conteudo ";
            string campos = " * ";
            string condicao = "";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            GvNossaEntidade.DataSource = ObjDados.RetCampos();
            GvNossaEntidade.PageIndex = e.NewPageIndex;
            GvNossaEntidade.DataBind();
        }
        protected void ativarVwCadConteudo(object sender, EventArgs e)
        {
            mwGridConteudo.ActiveViewIndex = 0;
            mwFormConteudo.ActiveViewIndex = 0;
            carregarGvConteudo();
        }
        protected void ativarVwNossaEntidade(object sender, EventArgs e)
        {
            mwGridConteudo.ActiveViewIndex = 1;
            mwFormConteudo.ActiveViewIndex = 1;
            carregarGvNossaEntidade();
        }
     
        public void popularStTipo() //Popula "DropDownList" com dados da tabela st_tipo
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " st_tipo ";
            ObjDados.Campo = " * ";
            ObjDados.Condicao = " order by cadmom desc ";

            stTipo.DataSource = ObjDados.RetCampos();
            stTipo.DataValueField = "cod";
            stTipo.DataTextField = "descricao";
            stTipo.DataBind();
        }

        public void popularStCategoria() //Popula "DropDownList" com dados da tabela st_posicao
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " " + " cod, id_empresa, descricao, cadusu ";
            ObjDados.Tabela = " " + " st_categoria ";
            ObjDados.Condicao = " " + " ORDER BY cadmom ";

            stCategoria.DataSource = ObjDados.RetCampos();
            stCategoria.DataValueField = "cod";
            stCategoria.DataTextField = "descricao";
            stCategoria.DataBind();
        }

        protected void cadastrarConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_conteudo ";
            string campos = " id_empresa, cod_menu, cod_tipo, cod_categoria, titulo, introducao, conteudo, complemento, conclusao, dt_publini, dt_publfim, dt_cad, fonte, autor, ordem, cadusu, cadmom";
            string valores = String.Format("'" + 1 + "'," + "'" + stDestaque.Value.ToString() + "'," + "'" + stCategoria.Value + "'," + "'" + stTipo.Value + "'," + "'" + iTitulo.Value + "'," +
                             "'" + taIntroducao.Value + "'," + "'" + taConteudo.Value + "'," + "'" + taComplemento.Value + "'," + "'" + taConclusao.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + iFonteConteudo.Value + "'," + "'" + iAutorConteudo.Value + "'," + "'" + Session["LoginUsuario"] + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (ObjDados.MsgErro == "")
            {
                ObjDados.InsertRegistro(tabela, campos, valores);


                iTitulo.Value = String.Empty;
                taConteudo.Value = String.Empty;
                iFonteConteudo.Value = String.Empty;
                iAutorConteudo.Value = String.Empty;
                iOrdemConteudo.Value = String.Empty;
            }
            else
            {
                MessageBox.Show(ObjDados.Msg);
            }

            carregarGvConteudo();

            MessageBox.Show("Agora Você precisa Cadastrar as Imagens");

            //Response.Redirect("S-Imagens.aspx");

            //MessageBox.Show("Categoria: " + stCategoria.Value + "\n" + 
            //              "Destaque: " + stDestaque.Value.ToString());


        }

        protected void editarConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = " st_conteudo ";
            ObjDados.Campo = " * ";
            //ObjDados.Condicao = " WHERE id = " + ImgId;

            //MessageBox.Show("Tá no Caminho... Clicou para Editar");            
            string idCont = "";

            DataTable dados = ObjDados.RetCampos(); //objConteudo.RetConteudoId(int.Parse(idCont));

            iTitulo.Value = dados.Rows[0]["titulo"].ToString();
            taConteudo.Value = dados.Rows[0]["conteudo"].ToString();
            iFonteConteudo.Value = dados.Rows[0]["fonte"].ToString();
            iAutorConteudo.Value = dados.Rows[0]["autor"].ToString();

            MessageBox.Show("Id Digitada: " + idCont);
        }

        public void capturarConteudoId(object sender, EventArgs e)
        {
            //var id = gvConteudo.SelectedRow.Cells[0].Text;
#pragma warning disable CS0219 // A variável "idCont" é atribuída, mas seu valor nunca é usado
            string idCont = "";
#pragma warning restore CS0219 // A variável "idCont" é atribuída, mas seu valor nunca é usado
            MessageBox.Show(gvConteudo.SelectedRow.Cells[1].Text);
            
            /*
            idCont = gvConteudo.SelectedRow.Cells[1].Text;            
            iTitulo.Value = gvConteudo.SelectedRow.Cells[5].Text;
            taConteudo.Value = gvConteudo.SelectedRow.Cells[6].Text;
            iFonteConteudo.Value = gvConteudo.SelectedRow.Cells[9].Text;
            iAutorConteudo.Value = gvConteudo.SelectedRow.Cells[10].Text;
            */
            //gvConteudo.DeleteRow(i);
        }
        protected void selecionarRegistroGvNossaEntidade(object sender, EventArgs e)
        {
           /* iExcluEmpresaMot.Visible = false;
            iExcluEmpresaMot.Value = String.Empty;
            btnExcEmpresa.Visible = false;
            btnMotExcEmpresa.Visible = true;

            lblIdEmpresa.Text = gvEmpresa.SelectedRow.Cells[1].Text;
            iNome.Value = gvEmpresa.SelectedRow.Cells[2].Text;

            //MessageBox.Show(gvEmpresa.SelectedRow.Cells[1].Text);

            string nome = iNome.Value;*/


//            bool existeCaracterEspecial = Regex.IsMatch(nome, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));

  /*          if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iNome.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iNome.Value = string.Join("", iNome.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }*/


        }

        protected void excluirConteudo(object sender, EventArgs e)
        {
            MessageBox.Show("Clicou para Excluir. Cuidado Cabeçudo!!!");
        }
        /**/
        
       

        public void selecionarTipo(object sender, EventArgs e)
        {
            MessageBox.Show("Mudou a bagaça!!!");
            /*
            if (stTipoConteudo.Value == "Conteúdo")
            {
                MessageBox.Show("Conteúdo");
            }
            else if (stTipoConteudo.Value == "Ofertas")
            {
                MessageBox.Show("Ofertas");
            }*/
        }
        
        /*
        protected void cadastrarContConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = "" + " st_tipo ";
            string campos = "" + " * ";
            string condicao = "" + " WHERE cod = 'cont' ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos(); 
            string codtipo = dados.Rows[0]["cod"].ToString();
            string tipo = dados.Rows[0]["descricao"].ToString();

            string valores = String.Format("'" + 1 + "'," + "'" + stContMenu.Value + "'," + "'" + "CONT" + "'," + codtipo + "'" + taContTitulo.Value + "'," + "'" + taContIntroducao.Value + "'," +
                                         "'" + taContConteudo.Value + "'," + "'" + taContComplemento.Value + "'," + "'" + taContConclusao.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + iContFonte.Value + "'," + "'" + iContAutor.Value + "'," + "'" + Session["LoginUsuario"] + "',");
                                                           
            ObjDados.Valores = valores;
            //lblTipo.Text = tipo + " Aqui! " + codtipo;

            if (ObjDados.MsgErro == "")
            {
                MessageBox.Show(ObjDados.Msg);

                ObjDados.InsertRegistro(tabela, campos, valores);

                taContTitulo.Value = String.Empty;
                taContConteudo.Value = String.Empty;
                taContIntroducao.Value = String.Empty;
                taContComplemento.Value = String.Empty;
                taContConclusao.Value = String.Empty;
                iContFonte.Value = String.Empty;
                iContAutor.Value = String.Empty;
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }


            carregarGvConteudo();

            MessageBox.Show("Agora Você precisa Cadastrar as Imagens");

            //Response.Redirect("S-Imagens.aspx");

            MessageBox.Show("Categoria: " + stCategoria.Value + "\n" +
                          "Destaque: " + stDestaque.Value.ToString());

            MessageBox.Show(ObjDados.Msg);            
        }

        */
        protected void editarContConteudo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL();

#pragma warning disable CS0219 // A variável "tabela" é atribuída, mas seu valor nunca é usado
            string tabela = " st_conteudo ";
#pragma warning restore CS0219 // A variável "tabela" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "campos" é atribuída, mas seu valor nunca é usado
            string campos = " * ";
#pragma warning restore CS0219 // A variável "campos" é atribuída, mas seu valor nunca é usado
            //string condicao = " WHERE id =" + ImgId;

            //MessageBox.Show("Tá no Caminho... Clicou para Editar");            
            string idCont = "";

            DataTable dados = ObjDados.RetCampos();  //RetConteudoId(int.Parse(idCont));

            iTitulo.Value = dados.Rows[0]["titulo"].ToString();
            taConteudo.Value = dados.Rows[0]["conteudo"].ToString();
            iFonteConteudo.Value = dados.Rows[0]["fonte"].ToString();
            iAutorConteudo.Value = dados.Rows[0]["autor"].ToString();

            MessageBox.Show("Id Digitada: " + idCont);
        }

        protected void excluirContConteudo(object sender, EventArgs e)
        {
            MessageBox.Show("Clicou para Excluir. Cuidado Cabeçudo!!!");
        }


        /**/
        public String gerarDDLMenu() //
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " " + " cod, id_empresa, descricao, cadusu ";
            ObjDados.Tabela = " " + " st_tipo ";
            ObjDados.Left = "";
            ObjDados.Condicao = " " + " ORDER BY cadmom ";

            string xRet = " ";

            DataTable dados = ObjDados.RetCampos();

            int contador = dados.Rows.Count;

            xRet += "<select name='Menu' id='MenuCat' style='width: 578px; display: block; margin-bottom: 10px; margin-left: 2px; border: 1px solid #999; padding: 8px; border-radius: 3px;'>";
            for (int i = 0; i < contador; i++)
            {
                xRet += "<option " + " value='item'" + i + ">";
                //xRet += dados.Rows[i]["cod"];
                xRet += dados.Rows[i]["descricao"];
                xRet += "</option>";
            }
            xRet += "</select>";

            return xRet;
        }

    }
}