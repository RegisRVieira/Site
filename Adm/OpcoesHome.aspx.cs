using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;
using System.Text.RegularExpressions;


namespace Site.Adm
{
    public partial class OpcoesHome : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //carregarGvBox100();
                //carregarGvBox33();
                //popularAlinhamento();
            }

            this.DataBind();
        }

        public void ativaViews(int ativa)
        {
            mwGridOpcoesHome.ActiveViewIndex = ativa;
            mwFormOpcoesHome.ActiveViewIndex = ativa;
        }

        public void ativarSlider(object sender, EventArgs e)
        {
            ativaViews(0);
        }
        public void ativarVwBox33(object sender, EventArgs e)
        {
            ativaViews(1);
            popularImgTipo();
            carregarGvBox33();
        }
        public void ativarVwBox100(object sender, EventArgs e)
        {
            ativaViews(2);
            carregarGvBox100();
        }
        public void ativarVwMateria(object sender, EventArgs e)
        {
            ativaViews(3);
            carregarGvMateria();
            popularAlinhamento();
            popularMatImgTipo();
            popularCampoConteudo();
        }
        public void ativarVwPublicidade(object sender, EventArgs e)
        {
            ativaViews(4);
        }
        public void ativarVwPublicidade2(object sender, EventArgs e)
        {
            ativaViews(5);
        }
        public void ativarVwGuiaDestaque(object sender, EventArgs e)
        {
            ativaViews(6);
        }
        public void ativarVwGuiaLogo(object sender, EventArgs e)
        {
            ativaViews(7);
        }

        public void popularImgTipo()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_img_tipo ";
            ObjDados.Condicao = " WHERE cod IN ('B33S1', 'B33S2', 'B33S3')";

            stImgTipo.DataSource = ObjDados.RetCampos();
            stImgTipo.DataValueField = "cod";
            stImgTipo.DataTextField = "descricao";
            stImgTipo.DataBind();
        }
        public void popularAlinhamento()
        {
            BLL ObjDados = new BLL(conectSite);

            string query = " SELECT cod, descricao, 1 AS ordem FROM st_imgalinhamento " +
                           " WHERE cod = 'NEN' " +
                           " UNION " +
                           " SELECT cod, descricao, 2 AS ordem FROM st_imgalinhamento  ";

            ObjDados.Query = query;            

            stMatImgAlinha.DataSource = ObjDados.RetQuery();
            stMatImgAlinha.DataValueField = "cod";
            stMatImgAlinha.DataTextField = "descricao";
            stMatImgAlinha.DataBind();
        }
        public void popularCampoConteudo()
        {
            BLL ObjDados = new BLL(conectSite);

            string query = " SELECT cod, descricao, 1 AS ordem FROM st_imgcampoconteudo " +
                           " WHERE cod = 'NEN' " +
                           " UNION " +
                           " SELECT cod, descricao, 2 AS ordem FROM st_imgcampoconteudo  ";
            ObjDados.Query = query;

            stMatImgCampoConteudo.DataSource = ObjDados.RetQuery();
            stMatImgCampoConteudo.DataValueField = "cod";
            stMatImgCampoConteudo.DataTextField = "descricao";
            stMatImgCampoConteudo.DataBind();
        }
        public void popularMatImgTipo()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_img_tipo ";
            ObjDados.Condicao = " WHERE cod IN ('cha', 'des', 'd2', 'gal', 'ptop')";

            stMatImgTipo.DataSource = ObjDados.RetCampos();
            stMatImgTipo.DataValueField = "cod";
            stMatImgTipo.DataTextField = "titulo";
            stMatImgTipo.DataBind();
        }
        public void paginarGvSlider(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string tabela = " st_conteudo ";
            string campos = " * ";
            string condicao = "";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;
            /*
            gvOpcoesHome.DataSource = ObjDados.RetCampos();
            gvOpcoesHome.PageIndex = e.NewPageIndex;
            gvOpcoesHome.DataBind();
        */
        }

        public void paginarBox33(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_conteudo ";

            gvBox33.DataSource = ObjDados.RetCampos();
            gvBox33.PageIndex = e.NewPageIndex;
            gvBox33.DataBind();

        }
        public void paginarBox100(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_conteudo ";

            gvBox100.DataSource = ObjDados.RetCampos();
            gvBox100.PageIndex = e.NewPageIndex;
            gvBox100.DataBind();
        }
        public void paginarMateria(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " st_conteudo ";

            gvMateria.DataSource = ObjDados.RetCampos();
            gvMateria.PageIndex = e.NewPageIndex;
            gvMateria.DataBind();
        }
        public void selecionarRegistroGvMateria(object sender, EventArgs e)
        {
            carregarGvMateria();

            //string x = gvMateria.SelectedRow.Cells[1].Text;
            //MessageBox.Show("Clicou!!!" + x);
            
            lblIdMateria.Text =  gvMateria.SelectedRow.Cells[1].Text;
            iMatTitulo.Value = gvMateria.SelectedRow.Cells[2].Text;
            taMatIntroducao.Value = gvMateria.SelectedRow.Cells[3].Text;
            taMatConteudo.Value =  gvMateria.SelectedRow.Cells[4].Text;
            taMatComplemento.Value = gvMateria.SelectedRow.Cells[5].Text;
            taMatConclusao.Value = gvMateria.SelectedRow.Cells[6].Text;

            string titulo = iMatTitulo.Value;

            bool existeCaracterEspecial = Regex.IsMatch(titulo, (@"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]"));

            if (existeCaracterEspecial)
            {
                //Verifica existência de caracteres especiais.
                int o = iMatTitulo.Value.Count(p => !char.IsLetterOrDigit(p));
                //Remove caracteres especiais. 
                iMatTitulo.Value = string.Join("", iMatTitulo.Value.ToCharArray().Where(char.IsLetterOrDigit));
            }

        }       

        public void capturarConteudoId(object sender, EventArgs e)
        {
            var id = gvSlider.SelectedRow.Cells[1].Text;
            string idCont = "";

            idCont = gvSlider.SelectedRow.Cells[1].Text;
            //stCategoria.Value = gvOpcoesHome.SelectedRow.Cells[3].Text;
            //stDestaque.Value = gvOpcoesHome.SelectedRow.Cells[4].Text;
            iTitulo.Value = gvSlider.SelectedRow.Cells[5].Text;
            //taConteudo.Value = gvOpcoesHome.SelectedRow.Cells[6].Text;
            //iFonteConteudo.Value = gvOpcoesHome.SelectedRow.Cells[9].Text;
            //iAutorConteudo.Value = gvOpcoesHome.SelectedRow.Cells[10].Text;

            //gvConteudo.DeleteRow(i);
        }

        protected void carregarData(object sender, EventArgs e)
        {
            int periodo = Convert.ToInt32(iDuracaoPub.Value);
            int periodoB33 = Convert.ToInt32(iB33DuracaoPub.Value);
            int periodoB100 = Convert.ToInt32(iB100DuracaoPub.Value);
            

            DateTime hoje = DateTime.Now;
            DateTime p30 = DateTime.Now.AddDays(periodo);
            DateTime p30B33 = DateTime.Now.AddDays(periodoB33);
            DateTime p30B100 = DateTime.Now.AddDays(periodoB100);
            


            iPublIniConteudo.Value = hoje.ToString("yyyy-MM-dd");
            iPublFimConteudo.Value = p30.ToString("yyyy-MM-dd");

            iB33PubIniConteudo.Value = hoje.ToString("yyyy-MM-dd");
            iB33PubFimConteudo.Value = DateTime.Now.AddDays(periodoB33).ToString("yyyyy-MM-dd");

            iB100PubIniConteudo.Value = hoje.ToString("yyyy-MM-dd");
            iB100PubFimConteudo.Value = DateTime.Now.AddDays(periodoB100).ToString("yyyyy-MM-dd");

            iMatPubIni.Value = hoje.ToString("yyyy-MM-dd");
            

        }

        public void carregarGvMateria()
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " id, titulo, introducao, complemento, conclusao, dt_PublIni, dt_PublFim  ";
            ObjDados.Tabela = " st_conteudo ";
            ObjDados.Condicao = " ORDER BY id DESC LIMIT 1";

            gvMateria.DataSource = ObjDados.RetCampos();
            gvMateria.DataBind();
        }

        public void carregarGvSlider()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            /* * * * # Captura ID do Conteúdo # * * * */
            string tConteudo = " st_conteudo ";
            string campoID = " * ";
            string condConteudo = " ORDER BY id DESC LIMIT 1 ";
            //Dados para Validação

            ObjValida.Tabela = tConteudo;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condConteudo;

            string IDConteudo = "";
            DataTable dadosID = ObjValida.RetCampos();
            int contador = dadosID.Rows.Count;
            IDConteudo = dadosID.Rows[0]["ID"].ToString();

            /* * * * # Fim # * * * */

            string tabela = " st_imagens AS i";
            string campos = " c.id, i.id_conteudo, c.cod_tipo, c.cod_categoria, c.cod_menu, c.titulo, c.introducao, c.dt_publIni, c.dt_PublFim, c.dt_cad, i.codtipo, i.titulo, i.descritivo, i.fonte, i.autor, i.hint ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo ";
            string condicao = " WHERE i.id_conteudo = '" + IDConteudo + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            gvSlider.DataSource = ObjDados.RetCampos();
            gvSlider.DataBind();

            //MessageBox.Show("carregarGvTipo");

            //MessageBox.Show("SELECT " +campos + " FROM "+ tabela + left +  condicao);
        }

        public void carregarGvBox33()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            /* * * * # Captura ID do Conteúdo # * * * */
            string tConteudo = " st_conteudo ";
            string campoID = " * ";
            string condConteudo = " ORDER BY id DESC LIMIT 1 ";
            //Dados para Validação

            ObjValida.Tabela = tConteudo;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condConteudo;

            string IDConteudo = "";
            DataTable dadosID = ObjValida.RetCampos();
            int contador = dadosID.Rows.Count;
            IDConteudo = dadosID.Rows[0]["ID"].ToString();

            /* * * * # Fim # * * * */

            string tabela = " st_imagens AS i";
            string campos = " c.id, i.id_conteudo, c.cod_tipo, c.cod_categoria, c.cod_menu, c.titulo, c.introducao, c.dt_publIni, c.dt_PublFim, c.dt_cad, i.codtipo, i.titulo, i.descritivo, i.fonte, i.autor, i.hint ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo ";
            string condicao = " WHERE i.id_conteudo = '" + IDConteudo + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            gvBox33.DataSource = ObjDados.RetCampos();
            gvBox33.DataBind();

        }
        public void carregarGvBox100()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            /* * * * # Captura ID do Conteúdo # * * * */
            string tConteudo = " st_conteudo ";
            string campoID = " * ";
            string condConteudo = " ORDER BY id DESC LIMIT 1 ";
            //Dados para Validação

            ObjValida.Tabela = tConteudo;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condConteudo;

            string IDConteudo = "";
            DataTable dadosID = ObjValida.RetCampos();
            int contador = dadosID.Rows.Count;
            IDConteudo = dadosID.Rows[0]["ID"].ToString();

            /* * * * # Fim # * * * */

            string tabela = " st_imagens AS i";
            string campos = " c.id, i.id_conteudo, c.cod_tipo, c.cod_categoria, c.cod_menu, c.titulo, c.introducao, c.dt_publIni, c.dt_PublFim, c.dt_cad, i.codtipo, i.titulo, i.descritivo, i.fonte, i.autor, i.hint ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo ";
            string condicao = " WHERE i.id_conteudo = '" + IDConteudo + "'";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            gvBox100.DataSource = ObjDados.RetCampos();
            gvBox100.DataBind();

        }
        protected void atualizaDataFim(object sender, EventArgs e)
        {
            int periodo = Convert.ToInt32(iDuracaoPub.Value);
            DateTime dias = DateTime.Now.AddDays(periodo);

            iPublFimConteudo.Value = periodo.ToString("yyyy-MM-dd");
        }

        public void cadastrarContSlider(object sender, EventArgs e)
        {
#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            DateTime dt_ini = DateTime.Now;
            DateTime dt_fin = DateTime.Now;

            iPublIniConteudo.Value = dt_ini.ToString("yyyy-MM-dd HH:mm:ss");
            iPublFimConteudo.Value = dt_fin.ToString("yyyy-MM-dd HH:mm:ss");

            string tabela = " st_conteudo ";
            string campos = " cod_tipo, titulo, introducao, dt_publIni, dt_PublFim, dt_cad ";
            string campoID = " * ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";
            string valores = String.Format("'" + "PROP" + "'," + 
                                           "'" + iTitulo.Value + "'," + 
                                           "'" + taIntroducao.Value.ToString() + "'," + 
                                           "'" + iPublIniConteudo.Value + "'," +
                                           "'" + iPublFimConteudo.Value + "'," + 
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
                btnCadContSlider.Enabled = false;
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
        }

        public void cadastrarContB33(object sender, EventArgs e)
        {

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            DateTime dt_ini = DateTime.Now;
            DateTime dt_fin = DateTime.Now;

            iB33PubIniConteudo.Value = dt_ini.ToString("yyyy-MM-dd HH:mm:ss");
            iB33PubFimConteudo.Value = dt_fin.ToString("yyyy-MM-dd HH:mm:ss");

            string tabela = " st_conteudo ";
            string campos = " cod_tipo, titulo, introducao, dt_publIni, dt_PublFim, dt_cad ";
            string campoID = " * ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";
            string valores = String.Format("'" + "BOX33" + "'," +
                                           "'" + iB33Titulo.Value + "'," +
                                           "'" + taB33Introducao.Value + "'," +
                                           "'" + iB33PubIniConteudo.Value + "'," +
                                           "'" + iB33PubFimConteudo.Value + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //Dados para Inserção
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (!String.IsNullOrEmpty(iB33Titulo.Value))
            {
                ObjDados.InsertRegistro(tabela, campos, valores);

                iB33ImgTitulo.Focus();
                lblMsg.Text = "Cadastro Realizado com Sucesso!";

                iB33Valida.Value = "Validado";

                btnCadBox33.Enabled = false;
            }
            else
            {
                iB33Titulo.Value = String.Empty;
                taB33Introducao.Value = String.Empty;

                lblMsg.Text = "É preciso preencher os Dados!!!";
                iB33Titulo.Focus();
            }

            //MessageBox.Show("INSERT INTO" + tabela + "(" + campos + ")" + " VALUES(" + valores + ")");

            //Dados para Validação
            ObjValida.Tabela = tabela;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condicao;
        }

        public void cadastrarContB100(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            DateTime dt_ini = DateTime.Now;
            DateTime dt_fin = DateTime.Now;
            
            string FinalizaPublicacao = "";
            
            iB100PubIniConteudo.Value = dt_ini.ToString("yyyy-MM-dd HH:mm:ss");
            
            if (!String.IsNullOrEmpty(iB100PubFimConteudo.Value))
            {
                iB100PubFimConteudo.Value = dt_fin.ToString("yyyy-MM-dd HH:mm:ss");

                FinalizaPublicacao = "'" + iB100PubFimConteudo.Value + "',";
            }
            else
            {
                FinalizaPublicacao = "" + "NULL" + ",";
            }

            string tabela = " st_conteudo ";
            string campos = " cod_tipo, titulo, introducao, dt_publIni, dt_PublFim, dt_cad ";
            string campoID = " * ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";
            string valores = String.Format("'" + "BOX100" + "'," +
                                           "'" + iB100Titulo.Value + "'," +
                                           "'" + taB100Introducao.Value + "'," +
                                           "'" + iB100PubIniConteudo.Value + "'," +
                                           "'" + FinalizaPublicacao + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //Dados para Inserção
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            if (!String.IsNullOrEmpty(iB100Titulo.Value))
            {
                ObjDados.InsertRegistro(tabela, campos, valores);

                iB100ImgTitulo.Focus();
                lblMsg.Text = "Cadastro Realizado com Sucesso!";

                iB100Valida.Value = "Validado";

                btnCadBox100.Enabled = false;
            }
            else
            {
                iB100Titulo.Value = String.Empty;
                taB100Introducao.Value = String.Empty;

                lblMsg.Text = "É preciso preencher os Dados!!!";
                iB100Titulo.Focus();
            }

            //MessageBox.Show("INSERT INTO" + tabela + "(" + campos + ")" + " VALUES(" + valores + ")");

        }

        public void proporcaoImgMateria(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " st_img_tipo ";
            string condicao = " WHERE cod IN ('cha', 'des', 'd2', 'gal') ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();
            int nLinhas = dados.Rows.Count;

            stMatImgTipo.DataSource = ObjDados.RetCampos();
            stMatImgTipo.DataValueField = "cod";
            stMatImgTipo.DataTextField = "titulo";
            stMatImgTipo.DataBind();

            popularMatImgTipo();
            //MessageBox.Show(stMatImgTipo.Items.Equals("CHA").ToString());

            string text = "Você selecionou a opção: ";

            //16-10-2022
            //Não tenho ideia do que pretendia com esse código 

            for (int i = 0; i <= stMatImgTipo.Items.Count - 1; i++)
            {
                if (stMatImgTipo.Items[i].Selected)
                {
                    text += stMatImgTipo.Items[i].Text;

                    MessageBox.Show(text);
                }

                MessageBox.Show("" + stMatImgTipo.Items[i].Selected);
            }


        }

        public void cadastrarMateria(object sender, EventArgs e)
        {            
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            DateTime dt_ini = DateTime.Now;
            DateTime dt_fin = DateTime.Now;


            iMatPubIni.Value = dt_ini.ToString("yyyy-MM-dd HH:mm:ss");

            string FinalizaPublicacao = "";
                                    
            if (!String.IsNullOrEmpty(iMatPubFim.Value))
            {
                iMatPubFim.Value = dt_fin.ToString("yyyy-MM-dd HH:mm:ss");

                FinalizaPublicacao = "'" + iMatPubFim.Value + "',";
            }
            else
            {
                FinalizaPublicacao = "" + "NULL" + ",";
            }

            //MessageBox.Show(iMatPubFim.Value);

            string tabela = " st_conteudo ";
            string campos = " cod_tipo, cod_categoria, cod_menu, titulo, introducao, conteudo, complemento, conclusao, fonte, autor, dt_publIni, dt_PublFim, id_empresa, dt_cad, cadMom, cadUsu  ";            
            string campoID = " * ";
            string condicao = " ORDER BY id DESC LIMIT 1 ";
            string valores = String.Format("'" + "MAT" + "'," +
                                           "'" + "GER" + "'," +
                                           "'" + "MAT" + "'," +
                                           "'" + iMatTitulo.Value + "'," +
                                           "'" + taMatIntroducao.Value.ToString() + "'," +
                                           "'" + taMatConteudo.Value + "'," +
                                           "'" + taMatComplemento.Value + "'," +
                                           "'" + taMatConclusao.Value + "'," +
                                           "'" + iMatFonte.Value + "'," +
                                           "'" + iMatAutor.Value + "'," +
                                           "'" + iMatPubIni.Value + "'," +
                                           //"'" + iMatPubFim.Value + "'," +
                                           FinalizaPublicacao +
                                           "'" + 1 + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + Session["LoginUsuario"].ToString() + "'");

            

            //Dados para Inserção
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;

            //MessageBox.Show("INSERT INTO" + tabela + "(" + campos + ")" + " VALUES(" + valores + ")");
                        

            if (!String.IsNullOrEmpty(iMatTitulo.Value))
            {
                ObjDados.InsertRegistro(tabela, campos, valores);

                iMatImgTitulo.Focus();  
                lblMsg.Text = "Cadastro Realizado com Sucesso!";                
                //lblMsg.Text = "Estamos testando! Gravação 'Desativada'! Descomente a linha 558 de Gravação após os testes...";

                iMatValida.Value = "Validado";

                btnCadMateria.Enabled = false;
            }
            else
            {
                iMatTitulo.Value = String.Empty;
                taMatIntroducao.Value = String.Empty;

                lblMsg.Text = "É preciso preencher os Dados!!!";
                iMatTitulo.Focus();
            }

            //MessageBox.Show(ObjDados.Msg);
            //MessageBox.Show("INSERT INTO" + tabela + "(" + campos + ")" + " VALUES(" + valores + ")");

            //Dados para Validação
            ObjValida.Tabela = tabela;
            ObjValida.Campo = campoID;
            ObjValida.Condicao = condicao;


            //MessageBox.Show("Título: " + iMatTitulo.Value + "\n" + "Introdução:" + taMatIntroducao.Value + "\n" +"Conteúdo: " + taMatConteudo.Value + 
            //  "\n" + "Complemento: " + taMatComplemento.Value + "\n" + "Conclusão: " + taMatConclusao.Value + "\n" + iMatFonte.Value + "\n" + iMatAutor.Value);

            carregarGvMateria();
        }

        public void checarTamanhoImg(object sender, EventArgs e) //Criado para testar a validação do tamanho do arquivo - 18-08-2021
        {
            AmbientProperties ObjAm = new AmbientProperties();

            if (fuImgCont.HasFile)
            {

                double tamahoImg = fuImgCont.PostedFile.ContentLength;

                ObjAm = (AmbientProperties)ConfigurationManager.GetSection("httpRuntime");

                if (tamahoImg < 6291456)
                {
                    lblMsg.Text = (tamahoImg * 1.00) + "Kb" + "\n" + (tamahoImg / 1024.00) + "MB" + "\n" + "Tudo normal, manda brasa!" + "\n" + "WebConfig: " + ConfigurationManager.GetSection("httpRuntime");


                    //Response.Write("MaxRequestLength: " +  configSection.MaxRequestLength + "<br>");
                }
                else if (tamahoImg > 6291456)
                {

                    lblMsg.Text = (tamahoImg * 1.00) + "Kb" + "\n" + (tamahoImg / 1024.00) + "MB" + "\n" + "Tamanho da Imagem Maior que o suportado!" + "\n" + "WebConfig: " + ConfigurationManager.GetSection("httpRuntime");
                }
            }
        }

        public string SqlImg { get; set; }
        public void inserirImagemSlider(object sender, EventArgs e)
        {
            string xRet = "";
#pragma warning disable CS0219 // A variável "xImg" é atribuída, mas seu valor nunca é usado
            string xImg = "";
#pragma warning restore CS0219 // A variável "xImg" é atribuída, mas seu valor nunca é usado

            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

            iValidador.Visible = false;


            if (!String.IsNullOrEmpty(iValidador.Value))
            {
                //MessageBox.Show("Apto");

                if (ObjDados.MsgErro == "")
                {
                    /* * * * # UpLoad da Imagem # * * * */

                    //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
                    //Encontrar uma forma para Não ocorrer este erro na página
                    double tamanho = fuImgCont.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;

                    string arquivo = "";
                    string caminho = "";
                    string caminho2 = "";
                    string CaminhoArquivo = "";


                    lblResp.InnerText = arquivo + " / " + (tamanho / 1024) / 1024 + "MB";

                    //MessageBox.Show("Tamanho do Arquivo: " + tamanho);                  

                    if (fuImgCont.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuImgCont.PostedFile.ContentType == "image/jpeg" ||
                            fuImgCont.PostedFile.ContentType == "image/png" ||
                            fuImgCont.PostedFile.ContentType == "image/gif")
                        {
                            if (fuImgCont.PostedFile.ContentLength < 4500000)
                            {
                                //MessageBox.Show("Blzura, no caminho certo");
                                try
                                {//Gravar Arquivo
                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Conteudo/").Replace(@"\", "/");
                                    caminho2 = ("/Img/Conteudo/").Replace(@"\", "/");
                                    arquivo = fuImgCont.FileName;
                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {

                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuImgCont.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "UpLoad de Arquivo realizado com Sucesso";

                                        /* * * * # FIM - UpLoad da Imagem # * * * */

                                        /* * * * * * * * * * Captura ID para Cadastrar Imagens * * * * * * * * * */
                                        string tConteudo = " st_conteudo ";
                                        string campoID = " * ";
                                        string condConteudo = " ORDER BY id DESC LIMIT 1 ";
                                        //Dados para Validação

                                        ObjValida.Tabela = tConteudo;
                                        ObjValida.Campo = campoID;
                                        ObjValida.Condicao = condConteudo;

                                        string IDConteudo = "";
                                        DataTable dadosID = ObjValida.RetCampos();
                                        int contador = dadosID.Rows.Count;
                                        IDConteudo = dadosID.Rows[0]["ID"].ToString();

                                        //MessageBox.Show("Quant.: " + contador + " ID: " + IDConteupo);

                                        /* * * * * * * Fim Captura ID * * * * * * */

                                        string tabela = " st_imagens ";
                                        string campos = " id_conteudo, codtipo, titulo, descritivo, fonte, autor, hint, cadmom, cadusu, path_img, id_empresa, id_noticia ";
                                        string condicao = " WHERE id = '" + IDConteudo + "'";
                                        string valores = String.Format("'" + IDConteudo + "'," +
                                                                       "'" + "SLI" + "'," +
                                                                       "'" + iImgTitulo.Value + "'," +
                                                                       "'" + iImgDescricao.Value + "'," +
                                                                       "'" + iImgFonte.Value + "'," +
                                                                       "'" + iImgAutor.Value + "'," +
                                                                       "'" + iImgHint.Value + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                       "'" + Session["LoginUsuario"].ToString() + "'," +
                                                                       "'" + caminho2 + arquivo + "'," +
                                                                       "'" + 1 + "'," +
                                                                       "'" + 2 + "'");

                                        //objImg.ImgCodTipo = stImgTipo.Value; //Fazer este SELECT                        

                                        ObjDados.Tabela = tabela;
                                        ObjDados.Campo = campos;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();
                                        int nLinhas = dados.Rows.Count;

                                        ObjDados.InsertRegistro(tabela, campos, valores);

                                        btnCadBox33.Enabled = true;
                                        iImgTitulo.Focus();


                                        //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);

                                        //xRet += "INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")";                                  
                                        //SqlImg = lblDados.Text;

                                        /* * * * # # * * * */

                                        if (contador > 0)
                                        {
                                            mwImg.ActiveViewIndex = 0;
                                            lbDados.Items.Add(xRet);

                                            ObjImg.Tabela = " st_imagens ";
                                            ObjImg.Campo = " * ";
                                            ObjImg.Condicao = " WHERE id_conteudo = '" + IDConteudo + "' ORDER BY id DESC";

                                            DataTable iDados = ObjImg.RetCampos();

                                            int iNlinhas = iDados.Rows.Count; //Validar. Checar se existe registro pra contar

                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < iNlinhas; i++)
                                            {
                                                path = iDados.Rows[i]["path_img"].ToString();
                                                xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["titulo"] + "</div>";
                                                xRet += "<div style='width: 300px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["descritivo"] + "</div>";
                                                xRet += "<div style='width: 100px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["path_img"] + "</div>";
                                                xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + iDados.Rows[i]["path_img"] + "'" + "/>" + "</div>";
                                                xRet += "</div>";
                                            }

                                        }
                                    }

                                    //Cadastrar dados no DB
                                    //MessageBox.Show("Caminho: " + CaminhoArquivo + "\n" + arquivo + "\n" + ConfigurationManager.AppSettings["caminhoArquivo"]);
                                    //MessageBox.Show("Caminho: " + caminho2 + arquivo);
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
                    lblMsg.Text = "Erro Original:" + ObjDados.MsgErro;
                }

            }
            else
            {
                lblMsg.Text = "É necessário que os dados do Conteúdo estejam preeenchidos! Preencha os Dados para prosseguir!!!";
                iTitulo.Focus();
            }

            carregarGvSlider();

        }//Fim
        // # # # # ## Inserir Imagens no BOX 33 ## # # # #
        public void inserirImagemB33(object sender, EventArgs e)
        {
            string xRet = "";
#pragma warning disable CS0219 // A variável "xImg" é atribuída, mas seu valor nunca é usado
            string xImg = "";
#pragma warning restore CS0219 // A variável "xImg" é atribuída, mas seu valor nunca é usado

            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            DAL ObjDal = new DAL(conectSite);

            iB33Valida.Visible = false;


            if (!String.IsNullOrEmpty(iB33Valida.Value))
            {
                if (ObjDados.MsgErro == "")
                {
                    // # UpLoad da Imagem # 

                    //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
                    //Encontrar uma forma para Não ocorrer este erro na página
                    double tamanho = fuB33ImgCont.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;

                    string arquivo = "";
                    string caminho = "";
                    string caminho2 = "";
                    string CaminhoArquivo = "";


                    lblMsg.Text = arquivo + " / " + (tamanho / 1024) / 1024 + "MB";

                    //MessageBox.Show("Tamanho do Arquivo: " + tamanho);        //Checar se está mostrando correto o tamanho do arquivo          

                    if (fuB33ImgCont.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuB33ImgCont.PostedFile.ContentType == "image/jpeg" ||
                            fuB33ImgCont.PostedFile.ContentType == "image/png" ||
                            fuB33ImgCont.PostedFile.ContentType == "image/gif")
                        {
                            if (fuB33ImgCont.PostedFile.ContentLength < 4500000)
                            {
                                try
                                {//Gravar Arquivo
                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Conteudo/").Replace(@"\", "/");
                                    caminho2 = ("/Img/Conteudo/").Replace(@"\", "/");
                                    arquivo = fuB33ImgCont.FileName;
                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {

                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuB33ImgCont.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "UpLoad de Arquivo realizado com Sucesso";

                                        // # Captura ID para Cadastrar Imagens # 
                                        string tConteudo = " st_conteudo ";
                                        string campoID = " * ";
                                        string condConteudo = " ORDER BY id DESC LIMIT 1 ";
                                        //Dados para Validação

                                        ObjValida.Tabela = tConteudo;
                                        ObjValida.Campo = campoID;
                                        ObjValida.Condicao = condConteudo;

                                        string IDConteudo = "";
                                        DataTable dadosID = ObjValida.RetCampos();
                                        int contador = dadosID.Rows.Count;
                                        IDConteudo = dadosID.Rows[0]["ID"].ToString();

                                        //MessageBox.Show("Quant.: " + contador + " ID: " + IDConteupo);

                                        // # Fim Captura ID #
                                        stImgTipo.DataValueField = "cod";

                                        string tabela = " st_imagens ";
                                        string campos = " id_conteudo, codtipo, titulo, descritivo, fonte, autor, hint, cadmom, cadusu, path_img, id_empresa, id_noticia ";
                                        string condicao = " WHERE id = '" + IDConteudo + "'";
                                        string valores = String.Format("'" + IDConteudo + "'," +
                                                                       "'" + stImgTipo.Value + "'," +
                                                                       "'" + iB33ImgTitulo.Value + "'," +
                                                                       "'" + iB33ImgDescricao.Value + "'," +
                                                                       "'" + iB33ImgFonte.Value + "'," +
                                                                       "'" + iB33ImgAutor.Value + "'," +
                                                                       "'" + iB33ImgHint.Value + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                       "'" + Session["LoginUsuario"].ToString() + "'," +
                                                                       "'" + caminho2 + arquivo + "'," +
                                                                       "'" + 1 + "'," +
                                                                       "'" + 2 + "'");
                                        ObjDados.Tabela = tabela;
                                        ObjDados.Campo = campos;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();
                                        int nLinhas = dados.Rows.Count;
                                        iB33ImgTitulo.Focus();

                                        ObjDados.InsertRegistro(tabela, campos, valores);

                                        if (contador > 0) // Executa se houver retorno de registro
                                        {
                                            mwImg33.ActiveViewIndex = 0;
                                            lbB33Dados.Items.Add(xRet);

                                            ObjImg.Tabela = " st_imagens ";
                                            ObjImg.Campo = " * ";
                                            ObjImg.Condicao = " WHERE id_conteudo = '" + IDConteudo + "' ORDER BY id DESC";

                                            DataTable iDados = ObjImg.RetCampos();

                                            int iNlinhas = iDados.Rows.Count; //Validar. Checar se existe registro pra contar


                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < iNlinhas; i++)
                                            {
                                                path = iDados.Rows[i]["path_img"].ToString();
                                                xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["titulo"] + "</div>";
                                                xRet += "<div style='width: 300px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["descritivo"] + "</div>";
                                                xRet += "<div style='width: 100px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["path_img"] + "</div>";
                                                xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + iDados.Rows[i]["path_img"] + "'" + "/>" + "</div>";

                                                //xRet += "<img style='width: 50px' src =" + path + "/>";
                                                xRet += "</div>";

                                                //MessageBox.Show(xRet += "<img style='width: 100px' src =" + "'../.." + dados.Rows[i]["path_img"] +"'" + "/>");
                                            }

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }

                                // * * * * # FIM - UpLoad da Imagem # * * * *



                                //#Testar Query#
                                //MessageBox.Show("INSERT INTO " + tabela + "(" + campos + ")" + "VALUES (" + valores + ")");
                                //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);
                                //MessageBox.Show(ObjDal.MsgError);



                                //xRet += "INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")";                                  
                                //SqlImg = lblDados.Text;

                                //MessageBox.Show(SqlImg + "\n" + " ID Conteúdo: " + IDConteudo);


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
                    lblB33Dados.Text = xRet;

                    /* FIM */

                    /*
                    iImgTitulo.Value = String.Empty;
                    iImgDescricao.Value = String.Empty;
                    iImgFonte.Value = String.Empty;
                    iImgAutor.Value = String.Empty;
                    iImgHint.Value = String.Empty;
                    */

                    //carregarGvImagens();

                    iTitulo.Focus();
                }
                else
                {
                    lblMsg.Text = "Erro Original:" + ObjDados.MsgErro;
                }

            }
            else
            {
                lblMsg.Text = "É necessário que os dados do Conteúdo estejam preeenchidos! Preencha os Dados para prosseguir!!!";
                iTitulo.Focus();
            }
            carregarGvBox33();
        }
        // # # # # ## Inserir Imagens no BOX 100 ## # # # #
        public void inserirImagemB100(object sender, EventArgs e)
        {

            string xRet = "";

            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            DAL ObjDal = new DAL(conectSite);

            iB100Valida.Visible = false;


            if (!String.IsNullOrEmpty(iB100Valida.Value))
            {
                if (ObjDados.MsgErro == "")
                {
                    // # UpLoad da Imagem # 

                    //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
                    //Encontrar uma forma para Não ocorrer este erro na página
                    double tamanho = fuB100ImgCont.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;

                    string arquivo = "";
                    string caminho = "";
                    string caminho2 = "";
                    string CaminhoArquivo = "";


                    lblMsg.Text = arquivo + " / " + (tamanho / 1024) / 1024 + "MB";

                    //MessageBox.Show("Tamanho do Arquivo: " + tamanho);        //Checar se está mostrando correto o tamanho do arquivo          

                    if (fuB100ImgCont.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuB100ImgCont.PostedFile.ContentType == "image/jpeg" ||
                            fuB100ImgCont.PostedFile.ContentType == "image/png" ||
                            fuB100ImgCont.PostedFile.ContentType == "image/gif")
                        {
                            if (fuB100ImgCont.PostedFile.ContentLength < 4500000)
                            {
                                try
                                {//Gravar Arquivo
                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Conteudo/").Replace(@"\", "/");
                                    caminho2 = ("/Img/Conteudo/").Replace(@"\", "/");
                                    arquivo = fuB100ImgCont.FileName;
                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {
                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuB100ImgCont.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "UpLoad de Arquivo realizado com Sucesso";

                                        // # Captura ID para Cadastrar Imagens # 
                                        string tConteudo = " st_conteudo ";
                                        string campoID = " * ";
                                        string condConteudo = " ORDER BY id DESC LIMIT 1 ";
                                        //Dados para Validação

                                        ObjValida.Tabela = tConteudo;
                                        ObjValida.Campo = campoID;
                                        ObjValida.Condicao = condConteudo;

                                        string IDConteudo = "";
                                        DataTable dadosID = ObjValida.RetCampos();
                                        int contador = dadosID.Rows.Count;
                                        IDConteudo = dadosID.Rows[0]["ID"].ToString();

                                        //MessageBox.Show("Quant.: " + contador + " ID: " + IDConteupo);

                                        // # Fim Captura ID #
                                        stImgTipo.DataValueField = "cod";

                                        string tabela = " st_imagens ";
                                        string campos = " id_conteudo, codtipo, titulo, descritivo, fonte, autor, hint, cadmom, cadusu, path_img, id_empresa, id_noticia ";
                                        string condicao = " WHERE id = '" + IDConteudo + "'";
                                        string valores = String.Format("'" + IDConteudo + "'," + "'" + "B100" + "'," + "'" + iB100ImgTitulo.Value + "'," + "'" + iB100ImgDescricao.Value + "'," +
                                                                       "'" + iB100ImgFonte.Value + "'," + "'" + iB100ImgAutor.Value + "'," + "'" + iB100ImgHint.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                       "'" + Session["LoginUsuario"].ToString() + "', " + "'" + caminho2 + arquivo + "', " + "'" + 1 + "', " + "'" + 2 + "'");

                                        ObjDados.Tabela = tabela;
                                        ObjDados.Campo = campos;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();
                                        int nLinhas = dados.Rows.Count;
                                        iB100ImgTitulo.Focus();

                                        ObjDados.InsertRegistro(tabela, campos, valores);


                                        //#Testar Query#
                                        //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);
                                        //MessageBox.Show(ObjDal.MsgError);
                                        //MessageBox.Show("INSERT INTO " + tabela + "(" + campos + ")" + "VALUES (" + valores + ")");                                        
                                        //xRet += "INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")"; 


                                        if (contador > 0) // Executa se houver retorno de registro
                                        {
                                            mwImg100.ActiveViewIndex = 0;
                                            lbB100Dados.Items.Add(xRet);

                                            ObjImg.Tabela = " st_imagens ";
                                            ObjImg.Campo = " * ";
                                            ObjImg.Condicao = " WHERE id_conteudo = '" + IDConteudo + "' ORDER BY id DESC";

                                            DataTable iDados = ObjImg.RetCampos();

                                            int iNlinhas = iDados.Rows.Count; //Validar. Checar se existe registro pra contar


                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < iNlinhas; i++)
                                            {
                                                path = iDados.Rows[i]["path_img"].ToString();
                                                xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["titulo"] + "</div>";
                                                xRet += "<div style='width: 300px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["descritivo"] + "</div>";
                                                xRet += "<div style='width: 100px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["path_img"] + "</div>";
                                                xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + iDados.Rows[i]["path_img"] + "'" + "/>" + "</div>";

                                                //xRet += "<img style='width: 50px' src =" + path + "/>";
                                                xRet += "</div>";

                                                //MessageBox.Show(xRet += "<img style='width: 100px' src =" + "'../.." + dados.Rows[i]["path_img"] +"'" + "/>");
                                            }

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }

                                // * * * * # FIM - UpLoad da Imagem # * * * *



                                //SqlImg = lblDados.Text;

                                //MessageBox.Show(SqlImg + "\n" + " ID Conteúdo: " + IDConteudo);


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

                    lblB100Dados.Text = xRet;

                    /* FIM */

                    /*
                    iImgTitulo.Value = String.Empty;
                    iImgDescricao.Value = String.Empty;
                    iImgFonte.Value = String.Empty;
                    iImgAutor.Value = String.Empty;
                    iImgHint.Value = String.Empty;
                    */

                    //carregarGvImagens();

                    iB100Titulo.Focus();
                }
                else
                {
                    lblMsg.Text = "Erro Original:" + ObjDados.MsgErro;
                }
            }
            else
            {
                lblMsg.Text = "É necessário que os dados do Conteúdo estejam preeenchidos! Preencha os Dados para prosseguir!!!";
                iB100ImgTitulo.Focus();
            }
            //carregarGvTipo();
        }
        protected void InserirImgMateria(object sender, EventArgs e)
        {
            string xRet = "";

            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            DAL ObjDal = new DAL(conectSite);

            iMatValida.Visible = false;


            if (!String.IsNullOrEmpty(iMatValida.Value))
            {
                if (ObjDados.MsgErro == "")
                {
                    // # UpLoad da Imagem # 

                    //Tamanho da Imagem - Funciona bem, porém: Quando a imagem possui tamanho maior do que o tamanho permitido no WebConfig, dá erro: Tamanho máximo de solicitação excedido.
                    //Encontrar uma forma para Não ocorrer este erro na página
                    double tamanho = fuMatImg.PostedFile.ContentLength;
                    tamanho = (tamanho / 1024) / 1024;

                    string arquivo = "";
                    string caminho = "";
                    string caminho2 = "";
                    string CaminhoArquivo = "";


                    lblMsg.Text = arquivo + " / " + (tamanho / 1024) / 1024 + "MB";

                    //MessageBox.Show("Tamanho do Arquivo: " + tamanho);        //Checar se está mostrando correto o tamanho do arquivo          

                    if (fuMatImg.HasFile)
                    {
                        //Validando tipo de arquivo
                        if (fuMatImg.PostedFile.ContentType == "image/jpeg" ||
                            fuMatImg.PostedFile.ContentType == "image/png" ||
                            fuMatImg.PostedFile.ContentType == "image/gif")
                        {
                            if (fuMatImg.PostedFile.ContentLength < 4500000)
                            {
                                try
                                {//Gravar Arquivo
                                    CaminhoArquivo = ConfigurationManager.AppSettings["caminhoArquivo"].Replace(@"\", "/"); //Esse cara "insere" um espaço na última barra - 13-08-2021: Checar!
                                    caminho = Server.MapPath(@"~/Img/Materia/").Replace(@"\", "/");
                                    caminho2 = ("/Img/Materia/").Replace(@"\", "/");
                                    arquivo = fuMatImg.FileName;
                                    string path = caminho.Replace(@"\", "/");

                                    if (System.IO.File.Exists(caminho + arquivo))
                                    {
                                        lblMsg.Text = "Arquivo já existe. Tente enviar com outro nome!";
                                    }
                                    else
                                    {
                                        fuMatImg.PostedFile.SaveAs(caminho + arquivo);
                                        lblMsg.Text = "Imagem cadastrada com Sucesso!!!";
                                        //lblMsg.Text = "UpLoad não realizado!!! Testes: descomentar linha 1282 e 1342 após os testes.";

                                        // # Captura ID para Cadastrar Imagens # 
                                        string tConteudo = " st_conteudo ";
                                        string campoID = " * ";
                                        string condConteudo = " ORDER BY id DESC LIMIT 1 ";
                                        //Dados para Validação

                                        ObjValida.Tabela = tConteudo;
                                        ObjValida.Campo = campoID;
                                        ObjValida.Condicao = condConteudo;

                                        string IDConteudo = "";
                                        DataTable dadosID = ObjValida.RetCampos();
                                        //int contador = 0;

                                        //MessageBox.Show(" SELECT " + ObjValida.Campo + " FROM " + ObjValida.Tabela + " " + ObjValida.Condicao);


                                        if (dadosID.Rows.Count > 0)
                                        {
                                            IDConteudo = dadosID.Rows[0]["ID"].ToString();
                                        }
                                        else
                                        {
                                            MessageBox.Show("* " + dadosID.Rows.Count);
                                        }

                                        //MessageBox.Show("Quant.: " + contador + " ID: " + IDConteudo);                                        
                                        

                                        // # Fim Captura ID #
                                        stMatImgTipo.DataValueField = "cod";

                                        string tabela = " st_imagens ";
                                        string campos = " id_conteudo, codtipo, cod_destaque, cod_campoconteudo, cod_alinhamento, titulo, descritivo, fonte, autor, hint, cadmom, cadusu, path_img, id_empresa";
                                        string condicao = " WHERE id = '" + IDConteudo + "'";
                                        string valores = String.Format("'" + IDConteudo + "'," +
                                                                       "'" + stMatImgTipo.Value + "'," +                                                                       
                                                                       "'" + "MAT" + "'," +
                                                                       "'" + stMatImgCampoConteudo.Value + "'," +
                                                                       "'" + stMatImgAlinha.Value + "'," +
                                                                       "'" + iMatImgTitulo.Value + "'," +
                                                                       "'" + iMatImgDesc.Value + "'," +
                                                                       "'" + iMatImgFonte.Value + "'," +
                                                                       "'" + iMatImgAutor.Value + "'," +
                                                                       "'" + iMatImgHint.Value + "'," +
                                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                                       "'" + Session["LoginUsuario"].ToString() + "', " +
                                                                       "'" + caminho2 + arquivo + "', " +
                                                                       "'" + 1 + "'");                                                                       

                                        ObjDados.Tabela = tabela;
                                        ObjDados.Campo = campos;
                                        ObjDados.Condicao = condicao;

                                        DataTable dados = ObjDados.RetCampos();
                                        int nLinhas = dados.Rows.Count;
                                        iMatImgTitulo.Focus();

                                        //#Testar Query#
                                        //MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);
                                        //MessageBox.Show(ObjDal.MsgError);
                                        //MessageBox.Show("INSERT INTO " + tabela + "(" + campos + ")" + "VALUES (" + valores + ")"); //Checar se variáveis estão carregando
                                        //xRet += "INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")"; 

                                        ObjDados.InsertRegistro(tabela, campos, valores);

                                        if (dadosID.Rows.Count > 0) // Executa se houver retorno de registro
                                        {
                                            mwImgMat.ActiveViewIndex = 0;
                                            lbMatDados.Items.Add(xRet);

                                            ObjImg.Tabela = " st_imagens ";
                                            ObjImg.Campo = " * ";
                                            ObjImg.Condicao = " WHERE id_conteudo = '" + IDConteudo + "' ORDER BY id DESC";

                                            DataTable iDados = ObjImg.RetCampos();

                                            int iNlinhas = iDados.Rows.Count; //Validar. Checar se existe registro pra contar


                                            //lblDados.Text = lblDados.Text + xRet + "\n"; //Esse cara foi pensado para criar várias linhas de INSERT

                                            xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 15px; display: inline-block; ; color: #22396f; '>";
                                            xRet += "<div style='width: 150px; display: inline-block;'>" + "Título" + "</div>" + "<div style='width: 150px; display: inline-block;'>" + "Descritivo" + "</div> " + "<div style='width: 150px; display: inline-block;'>" + "Path" + "</div>";
                                            xRet += "</div>";
                                            for (int i = 0; i < iNlinhas; i++)
                                            {
                                                path = iDados.Rows[i]["path_img"].ToString();
                                                xRet += "<div style='margin: 2px auto; margin-bottom: 5px; font.size: 10px, border: 1px solid #666;  width: 90%; height: 40px; display: inline-block; ; color: #7688b4; '>";
                                                xRet += "<div style='width: 150px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["titulo"] + "</div>";
                                                xRet += "<div style='width: 300px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["descritivo"] + "</div>";
                                                xRet += "<div style='width: 100px; font.size: 8px; display: inline-block;'>" + iDados.Rows[i]["path_img"] + "</div>";
                                                xRet += "<div style='width: 50px; font.size: 8px; display: inline-block;'>" + "<img style='width: 40px' src =" + "'.." + iDados.Rows[i]["path_img"] + "'" + "/>" + "</div>";

                                                //xRet += "<img style='width: 50px' src =" + path + "/>";
                                                xRet += "</div>";

                                                //MessageBox.Show(xRet += "<img style='width: 100px' src =" + "'../.." + dados.Rows[i]["path_img"] +"'" + "/>");
                                            }

                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }

                                // * * * * # FIM - UpLoad da Imagem # * * * *



                                //SqlImg = lblDados.Text;

                                //MessageBox.Show(SqlImg + "\n" + " ID Conteúdo: " + IDConteudo);


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

                    lblMatDados.Text = xRet;

                    /* FIM */

                    /*
                    iImgTitulo.Value = String.Empty;
                    iImgDescricao.Value = String.Empty;
                    iImgFonte.Value = String.Empty;
                    iImgAutor.Value = String.Empty;
                    iImgHint.Value = String.Empty;
                    */

                    //carregarGvImagens();

                    iMatImgTitulo.Focus();
                }
                else
                {
                    lblMsg.Text = "Erro Original:" + ObjDados.MsgErro;
                }

            }
            else
            {
                lblMsg.Text = "É necessário que os dados do Conteúdo estejam preeenchidos! Preencha os Dados para prosseguir!!!";
                iMatTitulo.Focus();
            }
            //carregarGvTipo();
        }

        /*FIM*/
    }



}