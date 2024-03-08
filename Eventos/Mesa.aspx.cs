using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.App_Code;
using System.Configuration;
using System.Windows.Forms;
using System.Net;
using System.Web.Configuration;
using System.IO;

namespace Site.Eventos
{
    public partial class Mesa : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];

        public string VendaArq_envio { get; set; }
        public int nVezes { get; set; }
        public string cArquivo_mostra { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            iBusca.Focus();

            if (!Page.IsPostBack)
            {
                verMesa();
                mostrarLogado();
                checarSessao();
            }
        }

        public void checarSessao()
        {
            string identifica = "";

            if (Session["LoginEventos"] != null)
            {
                identifica = Session["LoginEventos"].ToString();
            }
            else
            {
                Response.Redirect("eLogin.aspx");

            }

            int tamanhocampo = identifica.Length;


        }//checarSessao
        public void mostrarLogado()
        {
            string identifica = "";
            if (Session["LoginEventos"] != null)
            {

                identifica = Session["LoginEventos"].ToString();

                string primeiroNome = identifica.Split(' ').FirstOrDefault();
                string primeiraLetra = identifica.Split(' ').FirstOrDefault();
                int tNome = primeiroNome.Length;

                lblLogado.Text = primeiraLetra.Substring(0, 1).ToUpper() + primeiroNome.Substring(1, (tNome - 1)).ToLower();
            }
            else
            {
                Response.Redirect("eLogin.aspx");
            }
        }
        protected void encerrarLogin(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("eLogin.aspx");
        }
        protected void verMesa()
        {
            string nEvento = Request.QueryString["evento"];
            string nMesa = Request.QueryString["mesa"];
            string nAmbiente = Request.QueryString["ambiente"];

            lblMostraMesa.Text = nMesa;

            if (nMesa == "id")
            {
                secBusca.Attributes["class"] = "escondeCadCampos";
            }

            //MessageBox.Show("Número da Mesa: " + nMesa + ", no Ambiente: " + nAmbiente + " Evento: " + nEvento);

            BLL ObjMesa = new BLL(conectSite);
            BLL ObjDados = new BLL(conectSite);

            string xRet = "";

            string xMesa = "";


            /*string query = " SELECT * FROM _e_localizacao AS l " +
                           " INNER JOIN _e_evento AS e ON e.id = l.id_evento " +
                           " WHERE l.n_mesa = '" + nMesa + "'";*/

            string query = " SELECT * FROM e_localizacao AS l " +
                           " INNER JOIN e_evento AS e ON e.id = l.id_evento " +
                           " WHERE id_evento = '" + nEvento +  "' AND l.n_mesa = '" + nMesa + "'" +
                           " AND l.id_evento = '" + nEvento + "' " +
                           " AND l.id_ambiente = '" + nAmbiente + "' ";

            /*string qDados = " SELECT n_local, p.nome, e.descricao, a.titulo, p.unidade FROM e_pessoas AS p " +
                            " INNER JOIN  _e_evento AS e ON p.id_evento = e.id " +
                            " INNER JOIN _e_ambiente AS a ON a.id = p.id_ambiente " +
                            " WHERE n_local = '" + nMesa + "'";*/

            //MessageBox.Show(query);


            string qDados = " SELECT n_local, p.nome, e.descricao, a.titulo, p.unidade FROM e_pessoas AS p " +
                            " INNER JOIN  e_evento AS e ON p.id_evento = e.id " +
                            " INNER JOIN e_ambiente AS a ON a.id = p.id_ambiente " +
                            " WHERE n_local = '" + nMesa + "' AND p.canmom IS NULL"+
                            " AND p.id_evento = '" + nEvento + "' " +
                            " AND p.id_ambiente = '" + nAmbiente + "' ";


            ObjDados.Query = qDados;
            ObjMesa.Query = query;

            DataTable dados = ObjDados.RetQuery();
            DataTable mesa = ObjMesa.RetQuery();

            //MessageBox.Show(query);

            try
            {
                if (mesa.Rows.Count > 0)
                {
                    xRet += "<div>";
                    xRet += "<p style='padding-top: 5px; padding-bottom: 10px'>" + "Você selecionou a Mesa: " + mesa.Rows[0]["n_mesa"].ToString() + "</p>";
                    xRet += "</div>";

                    //lblNmesa.Text = xMesa;


                    if (dados.Rows.Count > 0)
                    {
                        xRet += "<div>";
                        xRet += "<span><p>" + "Integrantes da Mesa:" + "<p></span>";
                        for (int i = 0; i < dados.Rows.Count; i++)
                        {
                            //xRet += "<p style='font-size: 10px;'>" + dados.Rows[i]["nome"].ToString() + " - " + dados.Rows[i]["descricao"].ToString() + ", " + dados.Rows[i]["titulo"].ToString() + "</p>";
                            xRet += "<p style='font-size: 12px;'>" + (i + 1) + " - " + dados.Rows[i]["nome"].ToString() + ", " + dados.Rows[i]["unidade"].ToString() + "</p>";
                        }
                        xRet += "</div>";
                    }
                    else
                    {
                        xRet += "<span> <p>" + "Ainda não há cadastrados nesta Mesa" + "</p></span>";
                    }

                    lblIntegrantes.Text = xRet;
                }
                else
                {
                    lblMsg.Text = "Não há registro! Você precisa Selecionar uma Mesa!";
                }
            }
            catch (Exception e)
            {
                lblMsg.Text = "Erro: " + e.Message;
            }

        }
        public String procDependentes()
        {
            BLL ObjDados = new BLL(conectVegas);
            string xRet = "";

            string query = " SELECT d.nome, (YEAR(CURDATE()) - YEAR(d.DTNASCI)) AS Idade, d.grau, d.cnpj_cpf, d.ie_rg, g.descricao  FROM asdepen AS d " +
                           " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                           " WHERE d.associado = '" + lblIdAssoc.Text + "' AND d.grau IS NOT NULL AND d.cnscanmom IS NULL ";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            xRet += "<div>";
            xRet += "<h2>Este Associados possui os seguintes dependentes:</h2>";
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                xRet += "<span>";
                xRet += " - " + dados.Rows[i]["nome"].ToString() + ", " + dados.Rows[i]["descricao"].ToString() + " - " + dados.Rows[i]["idade"].ToString() + " anos";
                xRet += "</span>" + "<br />";
            }
            xRet += "</div>";

            return xRet;
        }//procDependentes
        protected void paginarGwAssociados(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            string tabela = " associa as a";
            string campos = " a.titular AS Associado, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS Status, (YEAR(CURDATE()) - YEAR(a.DATA)) AS Tempo, u.descricao AS Unidade ";
            string condicao = "";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            gvAssociados.DataSource = ObjDados.RetCampos();
            gvAssociados.PageIndex = e.NewPageIndex;
            gvAssociados.DataBind();
        }//paginarGwAssociados
        protected void paginarGvConvidados(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            ObjDados.Query = " SELECT a.idassoc AS ID, a.titular, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS STATUS, (YEAR(CURDATE()) - YEAR(a.DATA)) AS Tempo, u.descricao AS Unidade, a.cnpj_cpf, a.ie_rg " +
                             " FROM associa AS a " +
                             " INNER JOIN base_assocuni AS u ON a.trab_unida = u.unidade " +
                             " WHERE a.cnscanmom IS NULL ";

            gvParaConvidados.DataSource = ObjDados.RetQuery();
            gvParaConvidados.PageIndex = e.NewPageIndex;
            gvParaConvidados.DataBind();
        }//paginarGvConvidados

        protected String listaDependentes()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjListDeps = new BLL(conectSite);

            string IdAssociado = gvAssociados.SelectedRow.Cells[1].Text;

            string idEvento = Request.QueryString["evento"];

            //string query = " SELECT * FROM _e_pessoas " +
            string query = " SELECT * FROM e_pessoas " +
                             " WHERE idassoc = '" + IdAssociado + "' AND nome = '" + gvAssociados.SelectedRow.Cells[2].Text + "' AND id_evento = '" + idEvento + "' AND canmom IS NULL";
            ObjDados.Query = query;

            //Gera Lista dos Dependentes participantes
            //string listDeps = " SELECT * FROM _e_pessoas " +
            string listDeps = " SELECT * FROM e_pessoas " +
                             " WHERE idassoc = '" + IdAssociado + "' AND nome <> nometitular AND canmom IS NULL";

            ObjListDeps.Query = listDeps;

            DataTable dDependentes = ObjListDeps.RetQuery();
            //MessageBox.Show(query);

            //MessageBox.Show(dDependentes.Rows.Count.ToString());

            DataTable dados = ObjDados.RetQuery();

            string xRet = "";

            if (dados.Rows.Count > 0)
            {
                //MessageBox.Show("Já está Cadastrado");
                if (Page.IsPostBack)
                {
                    xRet += "<div style='background-color: ;'>";
                    xRet += "<p style='font-size: 16px; text-align: left;'>";
                    xRet += "Esse Associado:" + dados.Rows[0]["nome"] + ", já está cadastrado na Festa. Na mesa: " + dados.Rows[0]["n_local"].ToString();
                    xRet += "</p>";
                    secCadGrid.Attributes["class"] = "escondeCadCampos";
                    xRet += "<div style='background-color: ;'>";
                    xRet += "<p style='font-size: 16px; text-align: left;'>" + "Dependentes Cadastrados no Evento:" + "</p>";
                    if (dDependentes.Rows.Count > 0)
                    {
                        for (int i = 0; i < dDependentes.Rows.Count; i++)
                        {
                           // if (dDependentes.Rows[i]["nome"] != dDependentes.Rows[i]["nomeTitular"])
                            //{
                                xRet += "<p style='font-size: 14px; text-align: left;'>" + " - " + dDependentes.Rows[i]["nome"] + ". Na mesa: " + dDependentes.Rows[i]["n_local"].ToString() + "<p>";
                            //}
                        }
                    }
                    xRet += "</div>";
                    xRet += "<div>";
                }
                
            }
            return xRet;
        }
        protected void selecionarRegistroGvAssociados(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);            

            string idEvento = Request.QueryString["evento"];
            string IdAssociado = gvAssociados.SelectedRow.Cells[1].Text;
            string nome = gvAssociados.SelectedRow.Cells[2].Text;

            //string query = " SELECT * FROM _e_pessoas " +
            string query = " SELECT * FROM e_pessoas " +
                             " WHERE idassoc = '" + IdAssociado + "' AND nome = '"+ nome + "' AND id_evento = '" + idEvento + "' AND canmom IS NULL";
            ObjDados.Query = query;   

            DataTable dados = ObjDados.RetQuery();            

            if (dados.Rows.Count > 0)
            {
                lblMsg.Text = listaDependentes();
            }
            else
            {
                //MessageBox.Show("Soca a Bota!");

                secCadGrid.Attributes["class"] = "escondeCadCampos";

                lblIdAssoc.Text = gvAssociados.SelectedRow.Cells[1].Text;
                iNome.Value = gvAssociados.SelectedRow.Cells[2].Text;
                iIdade.Value = gvAssociados.SelectedRow.Cells[3].Text;
                iStatus.Value = gvAssociados.SelectedRow.Cells[4].Text;
                iUnidade.Value = gvAssociados.SelectedRow.Cells[5].Text;
                iTipoPessoa.Value = gvAssociados.SelectedRow.Cells[7].Text;
                iTitular.Value = gvAssociados.SelectedRow.Cells[8].Text;

                cadCampos.Attributes["class"] = "exibeCadCampos";
            }

            lblMostraDependentes.Text = procDependentes();

            lblResult.Text = String.Empty;
        }//selecionarRegistroGvAssociados
        protected void selecionarRegistroGvConvidados(object sender, EventArgs e)
        {
            secCadGridConvidado.Attributes["class"] = "escondeCadCampos";

            lblIdAssocConvidado.Text = gvParaConvidados.SelectedRow.Cells[1].Text;
            iNomeAssoc.Value = gvParaConvidados.SelectedRow.Cells[2].Text;
            iStatusAssoc.Value = gvParaConvidados.SelectedRow.Cells[3].Text;
            iUnidadeAssoc.Value = gvParaConvidados.SelectedRow.Cells[5].Text;

            divCadConvidados.Attributes["class"] = "exibeCadCampos";

            iNomeConvidado.Focus();

        }//selecionarRegistroGvConvidados        
        protected int nCadeiras(int nMesa)
        {//Verifica ocupação da Mesa
            BLL ObjValida = new BLL(conectSite);

            string Mesa = Request.QueryString["mesa"];
            string idEvento = Request.QueryString["evento"];

            if (Mesa != "id")
            {
                nMesa = Convert.ToInt32(Mesa);
            }

            //string query = " SELECT n_cadeiras FROM _e_localizacao" +
            string query = " SELECT n_cadeiras FROM e_localizacao" +
                           " WHERE n_mesa = '" + nMesa + "' AND id_evento ='" + idEvento + "'";

            ObjValida.Query = query;

            DataTable dados = ObjValida.RetQuery();

            int nCadeiras = 0;

            if (dados.Rows.Count > 0)
            {
                nCadeiras = Convert.ToInt32(dados.Rows[0]["n_cadeiras"].ToString());
            }
            else
            {
                nCadeiras = 0;
            }

            return nCadeiras;

            //MessageBox.Show("Número de Cadeiras: " + nCadeiras);

        }
        protected int mOcupacao(int nMesa)
        {//Verifica ocupação da Mesa
            BLL ObjValida = new BLL(conectSite);

            string Mesa = Request.QueryString["mesa"];
            string idEvento = Request.QueryString["evento"];

            if (Mesa != "id")
            {
                nMesa = Convert.ToInt32(Mesa);
            }

            string xRet = "";

            string query = " SELECT * FROM e_pessoas AS p " +                                 
                                 " WHERE p.n_local = '" + nMesa + "' AND p.canmom IS NULL AND id_evento = '" + idEvento  + "'";

            ObjValida.Query = query;

            DataTable dados = ObjValida.RetQuery();

            //MessageBox.Show(query);

            int ocupacao = 0;
            int contar = 0;

            try
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    contar++;
                }

                if (contar > 0)
                {
                    ocupacao = contar;
                }
                else
                {
                    ocupacao = 0;
                }
            }
            catch (Exception z)
            {
                string msg = z.Message;
            }

            //MessageBox.Show("Número de Cadeiras: " + nCadeiras);

            return ocupacao;
        }
        protected int checaMesa()
        {//Verifica ocupação da Mesa
            string Mesa = Request.QueryString["mesa"];

            int nMesa = Convert.ToInt32(Mesa);

            int xOcupacao = mOcupacao(nMesa);
            int xCadeiras = nCadeiras(nMesa);
            int retorno = 0;

            if (xOcupacao == 0)
            {
                //lblOcupacao.Text = "A Mesa está Vazia!";
                retorno = 0;
            }
            else
            {
                int cVagas = (xCadeiras - xOcupacao);
                //lblOcupacao.Text = "Há, " + xOcupacao.ToString() + ",Pessoas nesta Mesa " + xCadeiras;
                //lblOcupacao.Text = "Há, " + cVagas + ",lugar(es) vagos!";
                retorno = cVagas;
            }

            return retorno;
        }
        protected void checaOcupacaoMesa(object sender, EventArgs e)
        {
            string Mesa = Request.QueryString["mesa"];
            int nMesa = Convert.ToInt32(Mesa);

            string ocupacao = "";

            if (checaMesa() == 0)
            {
                if (mOcupacao(nMesa) == 0)
                {
                    ocupacao = "A Mesa está Vazia!" + mOcupacao(nMesa).ToString();
                }
                else
                {
                    ocupacao = "A Mesa está Cheia!" + mOcupacao(nMesa).ToString();
                }
            }
            else
            {
                ocupacao = "Há, " + checaMesa() + ",lugar(es) vagos!";
            }
        }
        protected void procurarAssoc(object sender, EventArgs e)
        {
            cadBoryConvidado.Attributes["class"] = "escondeCadCampos";
            cadBory.Attributes["class"] = "exibeCadCampos";

            BLL ObjVegas = new BLL(conectVegas);            

            lblMsg.Text = String.Empty;

            secDadosFiltro.Attributes["class"] = "exibeCadCampos";

            secCadGrid.Attributes["class"] = "exibeCadCampos";

            string textBusca = "";
            //   = iBusca.Value.Replace("", "%");
            textBusca = iBusca.Value.Replace(" ", "%");

            //ObjVegas.Query = " SELECT a.idassoc AS ID, d.nome AS Associado, (YEAR(CURDATE()) - YEAR(d.DTNASCI)) AS Idade, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS STATUS, (YEAR(CURDATE()) - YEAR(a.DATA)) AS Tempo, u.descricao AS Unidade, IF(d.grau IS NULL OR d.grau = '', 'Titular', g.descricao) AS Grau, d.cnpj_cpf, d.ie_rg " +
            ObjVegas.Query = " SELECT a.idassoc AS ID, d.nome AS Associado, IF((YEAR(CURDATE()) - YEAR(d.DTNASCI)) IS NULL, 0, (YEAR(CURDATE()) - YEAR(d.DTNASCI))) AS Idade, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS STATUS, u.descricao AS Unidade, IF(d.grau IS NULL OR d.grau = '', 'Titular', g.descricao) AS Grau, IF(d.grau IS NULL OR d.grau = '', 'TIT', 'DEP') AS tipopessoa, a.titular " +
                             " FROM associa AS a " +
                             " INNER JOIN base_assocuni AS u ON a.trab_unida = u.unidade " +
                             " INNER JOIN asdepen AS d ON d.associado = a.idassoc " +
                             " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                             " WHERE d.nome LIKE '%" + textBusca + "%' AND a.cnscanmom IS NULL AND d.cnscanmom IS NULL ";

            //DataTable dados = ObjVegas.RetCampos();
            DataTable dados = ObjVegas.RetQuery();
            string xRet = "";

            //# # # # # # Carregar Grid View # # # # # # 

            if (!String.IsNullOrEmpty(lblResult.Text))
            {
                lblResult.Text = String.Empty;
            }

            if (String.IsNullOrEmpty(iBusca.Value))
            {
                lblMsg.Text += "É preciso preencher o Campo Busca...";
            }
            else
            {
                gvAssociados.DataSource = dados;
                gvAssociados.DataBind();
            }

            string Msg = "";

            xRet += "<div>";
            if (String.IsNullOrEmpty(iBusca.Value))
            {
                Msg = "É preciso preencher o Campo Busca...";
            }
            else
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    xRet += "<span>" + dados.Rows[i]["Associado"].ToString() + " - " + dados.Rows[i]["Status"].ToString() + dados.Rows[i]["Unidade"].ToString() + "</span>";
                }
            }
            if (dados.Rows.Count < 1)
            {
                Msg = "Não encontrou dados para retorno, digite um nome válido!";
            }
            xRet += "</div>";

            lblMsg.Text = Msg;

        }//procurarAssoc
        protected void gravarParticipante(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjEdit = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            BLL ObjOcupacao = new BLL(conectSite);

            string nEvento = Request.QueryString["evento"];
            string nMesa = Request.QueryString["mesa"];
            string nAmbiente = Request.QueryString["ambiente"];


            //MessageBox.Show(iTipoPessoa.Value);

            //MessageBox.Show(stBuscaMesa.SelectedValue);

            if (nMesa == "id")
            {
                lblMsg.Text = "Selecione uma Mesa!";
            }
            else
            {
                //nMesa = Convert.ToInt32(stBuscaMesa.SelectedValue);
                lblMsg.Text = nMesa;
            }

            //string tabela = " _e_pessoas ";
            string tabela = " e_pessoas ";
            string campos = " id_evento,  n_local, id_ambiente, cod_tipo, cod_status, cod_tipopessoa, Nome, NomeTitular, idAssoc, STATUS, unidade, idade, observacao, CadMom, CadUsu";
            string valores = String.Format("'" + nEvento + "'," +
                                                 "'" + nMesa + "'," +
                                                 "'" + nAmbiente + "'," +
                                                 "'" + "EVEN" + "'," +
                                                 "'" + "ATI" + "'," +//Checar necessidade deste campo. 
                                                 "'" + iTipoPessoa.Value + "'," +
                                                 "'" + iNome.Value + "'," +
                                                 "'" + iTitular.Value + "'," +
                                                 "'" + lblIdAssoc.Text + "'," +
                                                 "'" + iStatus.Value + "'," +
                                                 "'" + iUnidade.Value + "'," +
                                                 "'" + iIdade.Value + "'," +
                                                 "'" + iObs.Value + "'," +
                                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                 "'" + Session["LoginEventos"].ToString() + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;
            ObjDados.Condicao = " WHERE idassoc = '" + lblIdAssoc.Text + "'"; ;

            DataTable pessoas = ObjDados.RetCampos();

            string _ObjDados = "INSERT INTO " + tabela + "(" + campos + ")" + " VALUES " + valores;

            //MessageBox.Show(_ObjDados);

            string lista = "";

            //#Editar Registro
            //string editTabela = " _e_localizacao ";
            string editTabela = " e_localizacao ";
            string set = " ocupacao = '" + "*" + "'" +
                             ", altusu = '" + Session["LoginEventos"].ToString() + "' " +
                             ", altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " n_mesa = '" + nMesa + "'";

            ObjEdit.Tabela = editTabela;
            ObjEdit.Valores = set;
            ObjEdit.Condicao = condicao;

            //#Checar se o associado já está cadastrado na Festa
            ObjValida.Tabela = " e_eventos ";
            ObjValida.Campo = " idassoc, nome, nometitular ";
            ObjValida.Condicao = " WHERE idassoc = '" + lblIdAssoc.Text + "'";

            //ObjValida.Query = " SELECT * FROM _e_pessoas " +
            string query = " SELECT * FROM e_pessoas " +                              
                              " WHERE idassoc = '" + lblIdAssoc.Text + "' AND nome = '" +iNome.Value + "' AND id_evento = '" + nEvento + "' AND canmom IS NULL";
            
            ObjValida.Query = query;
            
            DataTable valida = ObjValida.RetQuery();

            //MessageBox.Show(query);

            //ObjOcupacao.Query = " SELECT n_mesa, ocupacao FROM _e_localizacao WHERE n_mesa = '" + nMesa + "' ";
            ObjOcupacao.Query = " SELECT n_mesa, ocupacao FROM e_localizacao WHERE n_mesa = '" + nMesa + "' AND id_evento ='" + nEvento + "'";

            DataTable dOcupacao = ObjOcupacao.RetQuery();
            
            string xx = "" + dOcupacao.Rows[0]["n_mesa"] + " - "+ dOcupacao.Rows[0]["ocupacao"];

            //MessageBox.Show(xx + " # " + dOcupacao.Rows.Count);

            int xCadeiras = nCadeiras(Convert.ToInt32(nMesa));
            int xOcupacao = mOcupacao(Convert.ToInt32(nMesa));

            //MessageBox.Show(xCadeiras.ToString());
            //MessageBox.Show(xOcupacao.ToString());            

            if (Convert.ToInt32(iIdade.Value) <= 17 || String.IsNullOrEmpty(iIdade.Value))
            {
                lblMsg.Text = iIdade.Value + " anos. Menor de Idade, não pode participar do Evento.";
            }
            else
            {
                //MessageBox.Show(iIdade.Value + " anos, Boa Festa");

                if (iStatus.Value == "Liberado")
                {
                    if (valida.Rows.Count > 0)
                    {
                        lblMsg.Text = "Esse Associado:" + valida.Rows[0]["nome"] + ", já está cadastrado na Festa. Na mesa: " + valida.Rows[0]["n_local"].ToString();                        
                    }
                    else
                    {
                        try
                        {
                            
                            if (xOcupacao <= xCadeiras)
                            {
                                //###########################################AQUI########################################
                                if (dOcupacao.Rows.Count > 0)
                                {
                                    if (!String.IsNullOrEmpty(dOcupacao.Rows[0]["ocupacao"].ToString()))
                                    {
                                        lblMsg.Text = "Mesa está Cheia, não é possível incluir mais pessoas!";
                                        verMesa();
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Pode Cadastrar!");
                                        
                                        ObjDados.InsertRegistro(tabela, campos, valores);
                                        
                                        lblMsg.Text = "Gravado com Sucesso";

                                        //MessageBox.Show("Chegou aqui, para Gravar!");
                                        //MessageBox.Show("Ocupação: " + dOcupacao.Rows.Count);

                                        //MessageBox.Show(_ObjDados);
                                    }
                                }                                

                                for (int i = 0; i < xCadeiras; i++)
                                {
                                    lista += "" + iNome.Value + ", " + iTipoPessoa.Value + "Valor";
                                }

                                verMesa();
                            }
                            else
                            {
                                lblMsg.Text = "Mesa está Cheia, não é possível incluir mais pessoas!";
                                
                                secBusca.Attributes["class"] = "escondeCadCampos"; //TESTAR Esse Cara

                            }
                            //######### Verifica Ocupação da Mesa  ##########


                            //MessageBox.Show("Número de Cadeiras: " + nCadeiras(nMesa).ToString());
                            //MessageBox.Show("Ocupação: " + mOcupacao(nMesa).ToString());


                            if (mOcupacao(Convert.ToInt32(nMesa)) == nCadeiras(Convert.ToInt32(nMesa)))
                            {
                                if (String.IsNullOrEmpty(dOcupacao.Rows[0]["ocupacao"].ToString()))
                                {                                 
                                    lblMsg.Text = "Gravado com Sucesso! A Mesa atingiu a Capacidade Máxima! " + mOcupacao(Convert.ToInt32(nMesa)).ToString();
                                    //MessageBox.Show("UPDATE " + editTabela + " SET " + set + " WHERE " + condicao);
                                    ObjEdit.EditRegistro(editTabela, set, condicao);
                                }                                

                                cadCampos.Attributes["class"] = "escondeCadCampos";
                                secBusca.Attributes["class"] = "escondeCadCampos"; //Oculta as Opções de Busca do Associado para cadastro

                                //popularCadMesa();
                            }
                            else
                            {

                                //lblOcupacao.Text = "Há, " + xOcupacao.ToString() + ",Pessoas nesta Mesa " + xCadeiras;                

                                lblMsg.Text = "Gravado com Sucesso! Há, " + mOcupacao(Convert.ToInt32(nMesa)).ToString() + " pessoas nesta Mesa!";

                                cadCampos.Attributes["class"] = "escondeCadCampos"; //Oculta as Opções de Busca do Associado para cadastro

                                //lblMsg.Text =  "UPDATE " + editTabela + " SET " + set + " WHERE " + condicao;
                            }
                        }
                        catch (Exception x)
                        {
                            string msgErro = x.Message;

                            lblMsg.Text = "Erro:" + msgErro;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Esta pessoa: " + iNome.Value + ", não pôde ser cadastrada no Evento. Por vafor, encaminhar à Tesouraria!";
                }
            }
            //lblLista.Text = lista; //Pensar Melhor sobre esse cara :Metodo - ListarParticipantes()

            //MessageBox.Show(xRet + ", IDASSOC: " + lblIdAssoc.Text);            
            //MessageBox.Show("INSERT INTO " + tabela + " (" + campos + ") VALUES (" + valores + ")");




            //MessageBox.Show(xRet);

        }//gravarParticipante
        protected void preencherConvidado(object sender, EventArgs e)
        {
            secDadosFiltro.Attributes["class"] = "exibeCadCampos";
            cadBory.Attributes["class"] = "escondeCadCampos";
            cadBoryConvidado.Attributes["class"] = "exibeCadCampos";            

            BLL ObjDados = new BLL(conectVegas);

            secCadGridConvidado.Attributes["class"] = "exibeCadCampos";

            string textBusca = "";
            textBusca = iBusca.Value.Replace(" ", "%");

            ObjDados.Query = " SELECT a.idassoc AS ID, a.titular, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS STATUS, (YEAR(CURDATE()) - YEAR(a.DATA)) AS Tempo, u.descricao AS Unidade, a.cnpj_cpf, a.ie_rg " +
                             " FROM associa AS a " +
                             " INNER JOIN base_assocuni AS u ON a.trab_unida = u.unidade " +
                             " WHERE a.titular LIKE '%" + textBusca + "%' AND a.cnscanmom IS NULL ";

            DataTable dados = ObjDados.RetQuery();



            if (String.IsNullOrEmpty(textBusca))
            {
                lblMsg.Text = "É preciso Digitar o Nome do Associado no Campo de Busca";
            }
            else
            {
                //lblMsg.Text = "Está no caminho certo! Ja vamos continuar!!!";
                gvParaConvidados.DataSource = dados;
                gvParaConvidados.DataBind();
            }
            iNomeConvidado.Focus();
        }//preencherConvidado
        protected void gravarConvidado(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjEdit = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            BLL ObjOcupacao = new BLL(conectSite);


            string nEvento = Request.QueryString["evento"];
            string nMesa = Request.QueryString["mesa"];
            string nAmbiente = Request.QueryString["ambiente"];

            if (nMesa == "id")
            {
                lblMsg.Text = "Selecione uma Mesa!";
            }
            else
            {
                lblMsg.Text = nMesa;
            }

            //string tabela = " _e_pessoas ";
            string tabela = " e_pessoas ";
            string campos = " id_evento,  n_local, id_ambiente, cod_tipo, cod_status, cod_tipopessoa, Nome, NomeTitular, idAssoc, STATUS, unidade, idade, observacao, CadMom, CadUsu";
            string valores = String.Format("'" + nEvento + "'," +
                                                 "'" + nMesa + "'," +
                                                 "'" + nAmbiente + "'," +
                                                 "'" + "EVEN" + "'," +
                                                 "'" + "ATI" + "'," +//Checar necessidade deste campo. 
                                                 "'" + "CON" + "'," +
                                                 "'" + iNomeConvidado.Value + "'," +
                                                 "'" + iNomeAssoc.Value + "'," +
                                                 "'" + lblIdAssocConvidado.Text + "'," +
                                                 "'" + iStatusAssoc.Value + "'," +
                                                 "'" + iUnidadeAssoc.Value + "'," +
                                                 "'" + iIdadeConvidado.Value + "'," +
                                                 "'" + iObsConvidado.Value + "'," +
                                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                 "'" + Session["LoginEventos"].ToString() + "'");

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;
            ObjDados.Condicao = " WHERE idassoc = '" + lblIdAssocConvidado.Text + "'"; ;

            DataTable pessoas = ObjDados.RetCampos();

            //#Editar Registro
            //string editTabela = " _e_localizacao ";
            string editTabela = " e_localizacao ";
            string set = " ocupacao = '" + "*" + "'" +
                             ", canmot = '" + "Atingiu a Capacidade Máxima" + "' " +
                             ", canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicao = " n_mesa = '" + nMesa + "'";

            ObjEdit.Tabela = editTabela;
            ObjEdit.Valores = set;
            ObjEdit.Condicao = condicao;

            //#Checar se o associado já está cadastrado na Festa            

            //ObjValida.Query = " SELECT * FROM _e_pessoas " +
            string query = " SELECT * FROM e_pessoas " +
                              " WHERE idassoc = '" + lblIdAssocConvidado.Text + "' AND nome = '" + iNomeConvidado.Value + "' AND id_evento = '" + nEvento + "' AND canmom IS NULL";

            ObjValida.Query = query;            

            DataTable valida = ObjValida.RetQuery();

        //# Checa Ocupação da Mesa para Validar inserção do registro
            //ObjOcupacao.Query = " SELECT n_mesa, ocupacao FROM e_localizacao WHERE n_mesa = '" + nMesa + "' ";
            ObjOcupacao.Query = " SELECT n_mesa, ocupacao FROM e_localizacao WHERE n_mesa = '" + nMesa + "' AND id_evento ='" + nEvento + "'";
            DataTable dOcupacao = ObjOcupacao.RetQuery();


            int xCadeiras = nCadeiras(Convert.ToInt32(nMesa));
            int xOcupacao = mOcupacao(Convert.ToInt32(nMesa));

            if (String.IsNullOrEmpty(iIdadeConvidado.Value) || String.IsNullOrEmpty(iNomeConvidado.Value))
            {
                lblMsg.Text = "Nome e Idade do Convidado são Obrigatórios para Continuar";
                iIdadeConvidado.Focus();
            }
            else
            {
                if (Convert.ToInt32(iIdadeConvidado.Value) <= 17 || String.IsNullOrEmpty(iIdadeConvidado.Value))
                {
                    lblMsg.Text = iIdadeConvidado.Value + " anos. Menor de Idade, não pode participar do Evento.";
                }
                else
                {
                    //MessageBox.Show(iIdade.Value + " anos, Boa Festa");

                    if (iStatusAssoc.Value == "Liberado")
                    {
                        if (valida.Rows.Count > 0)
                        {
                            lblMsg.Text = "Esse Associado:" + valida.Rows[0]["nome"] + ", já está cadastrado na Festa. Na mesa: " + valida.Rows[0]["n_local"].ToString();
                        }
                        else
                        {
                            try
                            {
                                if (xOcupacao <= xCadeiras)
                                {
                                    //###########################################AQUI########################################
                                    if (dOcupacao.Rows.Count > 0)
                                    {
                                        if (!String.IsNullOrEmpty(dOcupacao.Rows[0]["ocupacao"].ToString()))
                                        {
                                            lblMsg.Text = "Mesa está Cheia, não é possível incluir mais pessoas!";
                                            verMesa();
                                        }
                                        else
                                        {
                                            //MessageBox.Show("Pode Cadastrar!");
                                            ObjDados.InsertRegistro(tabela, campos, valores);
                                            lblMsg.Text = "Gravado com Sucesso";
                                        }
                                    }

                                    verMesa();
                                }
                                else
                                {
                                    lblMsg.Text = "Mesa está Cheia, não é possível incluir mais pessoas!";

                                    secBusca.Attributes["class"] = "escondeCadCampos"; //TESTAR Esse Cara

                                }
                                //######### Verifica Ocupação da Mesa  ##########
                                
                                //MessageBox.Show("Número de Cadeiras: " + nCadeiras(Convert.ToInt32(nMesa)).ToString());
                                //MessageBox.Show("Ocupação: " + mOcupacao(Convert.ToInt32(nMesa)).ToString());


                                if (mOcupacao(Convert.ToInt32(nMesa)) == nCadeiras(Convert.ToInt32(nMesa)))
                                {
                                    if (String.IsNullOrEmpty(dOcupacao.Rows[0]["ocupacao"].ToString()))
                                    {
                                        lblMsg.Text = "Gravado com Sucesso! A Mesa atingiu a Capacidade Máxima! " + mOcupacao(Convert.ToInt32(nMesa)).ToString();
                                        //MessageBox.Show("UPDATE " + editTabela + " SET " + set + " WHERE " + condicao);
                                        ObjEdit.EditRegistro(editTabela, set, condicao);
                                    }

                                    divCadConvidados.Attributes["class"] = "escondeCadCampos"; //Oculta as Opções de Busca do Associado para cadastro                                    
                                    secBusca.Attributes["class"] = "escondeCadCampos"; //Oculta as Opções de Busca do Associado para cadastro

                                    //popularCadMesa();
                                }
                                else
                                {                                   
                                    //lblOcupacao.Text = "Há, " + xOcupacao.ToString() + ",Pessoas nesta Mesa " + xCadeiras;                

                                    lblMsg.Text = "Gravado com Sucesso! Há, " + mOcupacao(Convert.ToInt32(nMesa)).ToString() + " pessoas nesta Mesa!";

                                    divCadConvidados.Attributes["class"] = "escondeCadCampos"; //Oculta as Opções de Busca do Associado para cadastro

                                    //lblMsg.Text =  "UPDATE " + editTabela + " SET " + set + " WHERE " + condicao;
                                }
                            }
                            catch (Exception x)
                            {
                                string msgErro = x.Message;

                                lblMsg.Text = "Erro:" + msgErro;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Esta pessoa: " + iNomeAssoc.Value + ", não pôde ser cadastrada no Evento. Por vafor, encaminhar à Tesouraria!";
                    }
                }

            }
            //MessageBox.Show(xRet + ", IDASSOC: " + lblIdAssoc.Text);            
            //MessageBox.Show("INSERT INTO " + tabela + " (" + campos + ") VALUES (" + valores + ")");




            //MessageBox.Show(xRet);

        }//gravarConvidado
        protected void finalizarMesa(object sender, EventArgs e)
        {
            Response.Redirect("eventos.aspx");            
        }

        protected String ListarParticipantes(int idFesta) //Pensar Sobre esse cara - 03-12-2022
        {
            string lista = "";

            BLL ObjDados = new BLL(conectSite);

            //ObjDados.Query = " SELECT * FROM _e_pessoas WHERE ";
            ObjDados.Query = " SELECT * FROM e_pessoas WHERE ";

            return lista;
        }

        //#Processo de Venda

        public string GeraDigMod11(long intNumero)
        {
            string cDigito = "";

            cDigito = "" + intNumero + DigitoModulo11(intNumero);
            cDigito = "" + cDigito + DigitoModulo11(Convert.ToUInt32(cDigito));

            return cDigito;
        }

        public int DigitoModulo11(long intNumero)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            string strText = intNumero.ToString();

            if (strText.Length > 16)

                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;

            int intIdx = 0;

            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];

                intIdx++;
            }

            int intResto = (intSoma * 10) % 11;

            int intDigito = intResto;

            if (intDigito >= 10)

                intDigito = 0;

            return intDigito;
        }
        /* - - - Processo de Venda - - - */

        protected string Colocazero(string Campo, int Carac)
        {
            if (Campo.Length < Carac)
            {
                do
                {
                    Campo = "0" + Campo;

                } while (Campo.Length < Carac);
            }

            return Campo;
        }

        public string CortaZeros(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if ("" + cValor.Substring(i, 1) == "0")
                {
                    contador++;
                }
                else
                    break;
            }

            return cValor.Substring(contador, cValor.Length - contador);
        }

        public string ColocaVirgula(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if (contador == (cValor.Length - 2))
                {
                    cValor = cValor.Substring(0, contador) + "," + cValor.Substring(contador, 2);
                }

                contador++;
            }

            return cValor;
        }

        public void realizaVenda(object sender, EventArgs e)
        {

            //############################
            //string IdConv = Session["CodAcesso"].ToString();  Não é aceito sessão em Classe

            //MessageBox.Show(iCartao.Value + "-" + iValorVenda.Value + "-" + stParcelas.Value + "- " + iSenhaVenda.Value);

            string IdConv = "22081";

            if (String.IsNullOrEmpty(iCartao.Value) && String.IsNullOrEmpty(iValorVenda.Value) && String.IsNullOrEmpty(iSenhaVenda.Value))
            {
                lblMsgCartao.Text = "Você precisa preencher todos os Dados do cartão para Realizar a Cobrança!";
            }
            else
            {
                lblMsgCartao.Text = String.Empty;

                if (String.IsNullOrEmpty(iCartao.Value) || String.IsNullOrEmpty(iValorVenda.Value) || String.IsNullOrEmpty(iSenhaVenda.Value))
                {
                    //lblMsgVenda.Text = "Preencha todos os campos";

                    // ########################## Retornar dados
                }
                else
                {
                    //caminho físico
                    string pathDocumento = "" + WebConfigurationManager.AppSettings["CaminhoVendaENV"];

                    DirectoryInfo dir = new DirectoryInfo(pathDocumento);

                    //Cria arquivo         
                    //string cVersao = "00001.03";
                    string cVersao = "00001.04";
                    //string cConvenio = "" + 63193;
                    string cConvenio = "" + IdConv;
                    //string cConvenio = "" + idCobv + "00"; //Não funciona com DV 00
                    string cCartao = "" + iCartao.Value;
                    string cValor = "" + iValorVenda.Value.Replace(",", "").Replace(".", "");
                    string cParcelas = "" + stParcelas.Value;
                    string cCod_retorno = "000";
                    string cCod_autoriza = "000000000";
                    string cChave_usuario = "000000000";
                    string cNome_usuario = "                                        ";
                    //string cObserva = "VENDA PELO SITE               ";
                    string cObserva = "Festa de Fim de Ano DAP       ";
                    string cCpf = "              ";
                    string cSaldo = "00000000000000";
                    string cSenha = "" + iSenhaVenda.Value.Replace("'", "");


                    cConvenio = Colocazero(cConvenio, 9);
                    cValor = Colocazero(cValor, 12);

                    string cArq_cont = cVersao + cConvenio + cCartao + cValor + cParcelas + cCod_retorno + cCod_autoriza + cChave_usuario + cNome_usuario + cObserva + cCpf + cSaldo + cSenha;

                    //titulo do arquivo
                    //string cArq_tit = "" + Session["conveniado"] + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;

                    //Criando o Arquivo  
                    string cArq_tit = "" + IdConv + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
                                                                                                                //try
                                                                                                                //{
                    //MessageBox.Show(cArq_cont);

                    GravaArquivo(System.IO.Path.Combine(pathDocumento, cArq_tit), cArq_cont);
                    //}
                    //catch
                    //{
                    //}

                    //Session["vendaArq_envio"] = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                    VendaArq_envio = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                    //MessageBox.Show("Venda enviada para Processamento, aguarde");                              

                    //MessageBox.Show(cArq_cont);
                    //MessageBox.Show(idCobv);

                    Processar_retorno();
                }
            }
            iCartao.Value = String.Empty;
            iValorVenda.Value = String.Empty;
            iSenhaVenda.Value = String.Empty;
        }

        public string GravaArquivo(string cPathArquivo, string texto)
        {

            string cAux = "";
            StreamWriter oFileStream = new System.IO.StreamWriter(cPathArquivo);
            try
            {
                oFileStream.Write(texto);
            }
            catch (Exception except)
            {
                return "Erro na gravação do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oFileStream.Close();
                //oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public string LeArquivo(string cPathArquivo)
        {
            string cAux = "";
            FileStream oFileStream = new FileStream(cPathArquivo, FileMode.Open);
            StreamReader oStreamReader = new StreamReader(oFileStream);
            try
            {
                cAux = oStreamReader.ReadToEnd();
            }
            catch (Exception except)
            {
                return "Erro na leitura do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public void Processar_retorno()
        {
            try
            {
                string cArquivo = LeArquivo("" + VendaArq_envio.ToString().Replace("ENV", "RET"));
                cArquivo_mostra = "";

                string cCod_transa = "" + cArquivo.Substring(1, 3);//cod. Transação
                string cVersao = "" + cArquivo.Substring(3, 5); //Versão do Protocolo de Venda
                string cCodConv = "" + cArquivo.Substring(8, 9); //cod. do Convênio
                string cCodCarta = "" + cArquivo.Substring(17, 9); // Cod. do Cartão
                string cValor = "" + cArquivo.Substring(26, 12); //Valor da Venda
                string cParcela = "" + cArquivo.Substring(38, 2); //Quantidade de Parcelas
                string cCodRet = "" + cArquivo.Substring(40, 3);//cod. Retorno
                string cAutoriza = "" + cArquivo.Substring(43, 9); // Autorização da Compra
                string cChaveUsuar = "" + cArquivo.Substring(52, 9); // Chave do Usuário
                string cNome = "" + cArquivo.Substring(61, 40); //Nome do Conveniado
                string cObserva = "" + cArquivo.Substring(101, 30); //Observação sobre a Vendfa
                string cCpfCnpj = "" + cArquivo.Substring(131, 14); //CPF ou CNPJ
                string cSaldo = "" + cArquivo.Substring(145, 14); //Saldo do Associado
                string cSenha = "" + cArquivo.Substring(160, 20); //Senha 
                string cRet_msg = "";

                switch (cCodRet)
                {
                    case "000":
                        cRet_msg = "Transação realizada com sucesso";
                        break;
                    case "110":
                        cRet_msg = "Tempo esgotado para retorno";
                        break;
                    case "120":
                        cRet_msg = "Código de convênio incompatível com a instalação";
                        break;
                    case "130":
                        cRet_msg = "Conexão com problemas";
                        break;
                    case "210":
                        cRet_msg = "Código do cartão inconsistente";
                        break;
                    case "220":
                        cRet_msg = "Código da transação não permitido";
                        break;
                    case "310":
                        cRet_msg = "Cartão ou senha incorreta";
                        break;
                    case "320":
                        cRet_msg = "Cartão vencido";
                        break;
                    case "330":
                        cRet_msg = "Cartão não liberado";
                        break;
                    case "340":
                        cRet_msg = "Cartão cancelado";
                        break;
                    case "350":
                        cRet_msg = "Cartão não autorizado para uso no convênio";
                        break;
                    case "360":
                        cRet_msg = "Tipo de cartão não aceita parcelamento";
                        break;
                    case "370":
                        cRet_msg = "Usuário do cartão inválido";
                        break;
                    case "410":
                        cRet_msg = "Convênio não confere com o registrado na configuração do sistema";
                        break;
                    case "420":
                        cRet_msg = "Convênio não registrado";
                        break;
                    case "430":
                        cRet_msg = "Convênio não liberado";
                        break;
                    case "440":
                        cRet_msg = "Convênio cancelado";
                        break;
                    case "510":
                        cRet_msg = "Crédito insuficiente";
                        break;
                    case "610":
                        cRet_msg = "Parcelamento superior ao permitido para o convênio";
                        break;
                    case "620":
                        cRet_msg = "Parcelamento superior ao permitido para o associado";
                        break;
                    case "630":
                        cRet_msg = "Valor da compra zerado";
                        break;
                    case "710":
                        cRet_msg = "Autorização não encontrada para cancelamento";
                        break;
                    case "720":
                        cRet_msg = "Autorização já cancelada";
                        break;
                    case "730":
                        cRet_msg = "Cancelamento não permitido";
                        break;
                    case "910":
                        cRet_msg = "Comunique-se com a central com urgência";
                        break;
                    case "920":
                        cRet_msg = "Transação não concluída";
                        break;
                    case "930":
                        cRet_msg = "Arquivo ENV vazio ou com problemas";
                        break;
                    case "940":
                        cRet_msg = "Transação já realizada, e não pode ser Duplicada. Verifique seu Extrato";
                        break;
                    case "950":
                        cRet_msg = "Protocolo ENV de comunicação inválido";
                        break;
                    default:
                        cRet_msg = "Erro desconhecido";
                        break;
                }

                //verificando se a venda ocorreu
                if (cCodRet != "000")
                {
                    //string cMsg_neg = "Venda NÃO realizada: " + cRet_msg;
                    cArquivo_mostra += "Venda NÃO realizada: " + cRet_msg;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Venda.aspx?op=convdados&menu=n';", true);
                    //comunicando.Attributes["class"] = "compVendaOff";
                }
                else
                {
                    cArquivo_mostra += "<section Class='CompVenda'>";
                    //cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>"; Criar: Parametro pra atribuir tamanho da font para impressão
                    cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>";
                    cArquivo_mostra += "<thead style='vertical-align:bottom' >";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    //cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante Festa de Fim de Ano" + cObserva + "</td>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante Festa de Fim de Ano" + cObserva + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via da ASU</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "</thead>";
                    cArquivo_mostra += "<tbody>";                  
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>CPF</td>";
                    cArquivo_mostra += "<td  id='cpf' style ='text-align: center;' class='tFontExtrato' >" + cCpfCnpj + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' id='nome' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + cNome + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    /**/
                    //cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante Festa de Fim de Ano</td>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante Festa de Fim de Ano</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via do Associado</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Saldo</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cSaldo) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "</tbody>";
                    cArquivo_mostra += "</table>";
                    cArquivo_mostra += "</section>";

                    //comunicando.Attributes["class"] = "compVendaOff";
                }

                //EFETUAR RETORNO, relatórios, etc
                //lblRetorno.Text = "<span style='color:DarkGreen;font-size:18px;'>VENDA EFETUADA COM SUCESSO! Transa: " + cCod_transa + "</span>";


                /* 
                compVenda.Attributes["class"] = "compVendaOn";
                secCompVenda.Attributes["class"] = "compVendaOff";
                */
                lblRetorno.Text = cArquivo_mostra;
                

                //###################################    Criar Variável para retornar dados

            }
#pragma warning disable CS0168 // A variável "erro" está declarada, mas nunca é usada
            catch (Exception erro)
#pragma warning restore CS0168 // A variável "erro" está declarada, mas nunca é usada
            {
                nVezes = 0;

                if (nVezes <= Convert.ToInt16("" + WebConfigurationManager.AppSettings["Timeout_venda"]))
                {
                    System.Threading.Thread.Sleep(Convert.ToInt16(1000));
                    nVezes++;
                    Processar_retorno();
                }
                else
                {
                    //lblRetorno.Text = "Tempo Esgotado, tente novamente!" + erro.Message;
                    //###################################    Criar Variável para retornar dados
                }
            }

        }
     
      

    }//Class
}