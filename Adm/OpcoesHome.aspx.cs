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
                carregarGvTipo();
                carregaGvBox33();
                popularImgTipo();
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
        }
        public void ativarVwBox100(object sender, EventArgs e)
        {
            ativaViews(2);
        }
        public void ativarVwMateria(object sender, EventArgs e)
        {
            ativaViews(3);
        }
        public void ativarVwConteudo(object sender, EventArgs e)
        {
            ativaViews(4);
        }
        public void ativarVwPublicidade(object sender, EventArgs e)
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

            DateTime hoje = DateTime.Now;
            DateTime p30 = DateTime.Now.AddDays(periodo);
            DateTime p30B33 = DateTime.Now.AddDays(periodoB33);


            iPublIniConteudo.Value = hoje.ToString("yyyy-MM-dd");
            iPublFimConteudo.Value = p30.ToString("yyyy-MM-dd");

            iB33PubIniConteudo.Value = hoje.ToString("yyyy-MM-dd");
            iB33PubFimConteudo.Value = DateTime.Now.AddDays(periodoB33).ToString("yyyyy-MM-dd");

        }

        public void carregarGvTipo()
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

            //MessageBox.Show("SELECT " +campos + " FROM "+ tabela + left +  condicao);
        }

        public void carregaGvBox33()
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
        protected void atualizaDataFim(object sender, EventArgs e)
        {
            int periodo = Convert.ToInt32(iDuracaoPub.Value);
            DateTime dias = DateTime.Now.AddDays(periodo);

            iPublFimConteudo.Value = periodo.ToString("yyyy-MM-dd");
        }

        public void cadastrarContSlider(object sender, EventArgs e)
        {
            string xRet = "";

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
            string valores = String.Format("'" + "PROP" + "'," + "'" + iTitulo.Value + "'," + "'" + taIntroducao.Value + "'," + "'" + iPublIniConteudo.Value + "'," +
                                           "'" + iPublFimConteudo.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
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

            string xRet = "";

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
            string valores = String.Format("'" + "BOX33" + "'," + "'" + iB33Titulo.Value + "'," + "'" + taB33Introducao.Value + "'," + "'" + iB33PubIniConteudo.Value + "'," +
                                           "'" + iB33PubFimConteudo.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
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
        public void inserirImagem(object sender, EventArgs e)
        {
            string xRet = "";
            string xImg = "";

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
                                    }

                                    //Cadastrar dados no DB
                                    //MessageBox.Show("Caminho: " + CaminhoArquivo + "\n" + arquivo + "\n" + ConfigurationManager.AppSettings["caminhoArquivo"]);
                                    //MessageBox.Show("Caminho: " + caminho2 + arquivo);
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }

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
                                string valores = String.Format("'" + IDConteudo + "'," + "'" + "SLI" + "'," + "'" + iImgTitulo.Value + "'," + "'" + iImgDescricao.Value + "'," + "'" + iImgFonte.Value + "'," +
                                                               "'" + iImgAutor.Value + "'," + "'" + iImgHint.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + Session["LoginUsuario"].ToString() + "'," +
                                                               "'" + caminho2 + arquivo + "'," + "'" + 1 + "'," + "'" + 2 + "'");

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

                                //MessageBox.Show(SqlImg + "\n" + " ID Conteúdo: " + IDConteudo);

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

                                    string path = "";
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

                    /* FIM */

                    /*
                    iImgTitulo.Value = String.Empty;
                    iImgDescricao.Value = String.Empty;
                    iImgFonte.Value = String.Empty;
                    iImgAutor.Value = String.Empty;
                    iImgHint.Value = String.Empty;
                    */

                    //carregarGvImagens();

                    
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

            carregarGvTipo();

        }//Fim

        public void inserirImagemB33(object sender, EventArgs e)
        {

            string xRet = "";
            string xImg = "";

            BLL ObjDados = new BLL(conectSite);
            BLL ObjImg = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);

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
                                    }                                   
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.ToString();
                                }

                                // * * * * # FIM - UpLoad da Imagem # * * * *

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
                                string valores = String.Format("'" + IDConteudo + "'," + "'" + stImgTipo.Value + "'," + "'" + iImgTitulo.Value + "'," + "'" + iImgDescricao.Value + "'," + "'" + iImgFonte.Value + "'," +
                                                               "'" + iImgAutor.Value + "'," + "'" + iImgHint.Value + "'," + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "'" + Session["LoginUsuario"].ToString() + "'," +
                                                               "'" + caminho2 + arquivo + "'," + "'" + 1 + "'," + "'" + 2 + "'" );

                                //objImg.ImgCodTipo = stImgTipo.Value; //Fazer este SELECT                        

                                ObjDados.Tabela = tabela;
                                ObjDados.Campo = campos;
                                ObjDados.Condicao = condicao;

                                DataTable dados = ObjDados.RetCampos();
                                int nLinhas = dados.Rows.Count;
                                iB33ImgTitulo.Focus();

                                ObjDados.InsertRegistro(tabela, campos, valores);


                                MessageBox.Show("Dados: " + ObjDados.Msg + "Validação: " + ObjValida.Msg);

                                //xRet += "INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")";                                  
                                //SqlImg = lblDados.Text;

                                //MessageBox.Show(SqlImg + "\n" + " ID Conteúdo: " + IDConteudo);
                                                                
                                if (contador > 0) // Executa se houver retorno de registro
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

                                    string path = "";
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

            carregarGvTipo();
        }


        /*FIM*/
    }
}