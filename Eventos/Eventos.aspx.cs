using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Site.App_Code;

namespace Site.Eventos
{
    public partial class Eventos : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        public string IdEvento { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            capturarIdEvento();

            if (!Page.IsPostBack)
            {
                mostrarLogado();
                ativarBoasVindas();
                popularCadAmbiente();
                //capturarIdEvento();
            }

            //MessageBox.Show("IdEvento: " + IdEvento);
        }

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

        protected void ativarBoasVindas()
        {
            mwConteudo.ActiveViewIndex = 0;
        }

        protected void ativarCadastro()
        {
            mwConteudo.ActiveViewIndex = 1;
            lblListaRelaorios.Text = String.Empty;
            //popularDdlVerMesa();
            popularCadEvento();
            popularCadMesa();
            //capturarIdEvento();
            //popularCadAmbiente();
        }

        protected void ativarCadastro(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 1;
            lblListaRelaorios.Text = String.Empty;
            //popularDdlVerMesa();
            popularCadEvento();
            popularCadMesa();
            //popularCadAmbiente();
        }
        protected void ativarRelatorios(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 2;
            lblListaRelaorios.Text = String.Empty;
            popularstRelatorios();
        }
        protected void ativarManutencao(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 3;
            popularddlEvento();

            lblListaRelaorios.Text = String.Empty;
        }

        protected void popularCadEvento()
        {
            //Popula Select se a data do evento ainda não for maior que a data do evento
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";            
            string tabela = " e_evento ";
            string condicao = "WHERE cod_status = 'ATI' AND DATE_FORMAT(dtevento, '%Y-%m-%d') >= CURDATE()";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            stEventoCadParticipante.DataSource = ObjDados.RetCampos();
            stEventoCadParticipante.DataValueField = "id";
            stEventoCadParticipante.DataTextField = "descricao";
            stEventoCadParticipante.DataBind();
        }
        protected void popularddlEvento()
        {
            //Popula DDL em manutenção, para selecionar evento a ser procurado
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " e_evento ";
            string condicao = "WHERE cod_status = 'ATI' AND DATE_FORMAT(dtevento, '%Y-%m-%d') >= CURDATE()";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            ddlEvento.DataSource = ObjDados.RetCampos();
            ddlEvento.DataValueField = "id";
            ddlEvento.DataTextField = "descricao";
            ddlEvento.DataBind();
        }

        protected void popularstRelatorios()
        {
            //Select para escolher o ento a ser exibido no Relatório
            BLL ObjDados = new BLL(conectSite);

            string query = "SELECT * FROM e_evento ORDER BY id DESC";
            ObjDados.Query = query; 

            ddlRelatorios.DataSource = ObjDados.RetQuery();
            ddlRelatorios.DataValueField = "id";
            ddlRelatorios.DataTextField = "descricao";
            ddlRelatorios.DataBind();


        }

        protected int nCadeiras(int nMesa)
        {//Verifica ocupação da Mesa
            BLL ObjValida = new BLL(conectSite);

            if (stBuscaMesa.SelectedValue != "id")
            {
                nMesa = Convert.ToInt32(stBuscaMesa.SelectedValue);
            }

            string query_bk = " SELECT n_cadeiras FROM e_localizacao" +
                           " WHERE n_mesa = '" + nMesa + "'";

            string query = " SELECT id_evento, id_ambiente, n_cadeiras FROM e_localizacao" +
                           " WHERE id_evento = " + stEventoCadParticipante.Value + "  AND id_ambiente = " + stEventoCadAmbiente.Value + " AND n_mesa = " + nMesa ;

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

            if (stBuscaMesa.SelectedValue != "id")
            {
                nMesa = Convert.ToInt32(stBuscaMesa.SelectedValue);
            }

            /*
            string query = " SELECT * FROM e_pessoas AS p " +
                                 " INNER JOIN e_evento AS e ON p.id_evento = e.id " +
                                 //" INNER JOIN _e_localizacao AS l ON p.id_localizacao = l.id" +
                                 " INNER JOIN e_localizacao AS l ON p.n_local = l.n_mesa " +
                                 " WHERE l.n_mesa = '" + nMesa + "' AND p.canmom IS NULL  ";
            */

            string query = " SELECT * FROM e_pessoas AS p " +
                           " WHERE p.id_evento = '" + stEventoCadParticipante.Value + "' " +
                           " AND p.id_ambiente = '" + stEventoCadAmbiente.Value + "' " +
                           " AND p.n_local = '" + stBuscaMesa.SelectedValue + "' AND canmom IS NULL";
                                    
            //" WHERE id_evento = " + stEventoCadParticipante.Value + "  AND id_ambiente = " + stEventoCadAmbiente.Value + " AND n_mesa = " + nMesa ;

            ObjValida.Query = query;

            DataTable dados = ObjValida.RetQuery();            

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
            int nMesa = 0;
            if (stBuscaMesa.SelectedValue == "id")
            {
                lblMsg.Text = "Selecione uma Mesa!";
            }
            else
            {
                nMesa = Convert.ToInt32(stBuscaMesa.SelectedValue);
            }

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

            //string Mesa = stBuscaMesa.SelectedValue;
            int nMesa = Convert.ToInt32(Mesa);

            if (checaMesa() == 0)
            {
                if (mOcupacao(nMesa) == 0)
                {
                    lblOcupacao.Text = "A Mesa está Vazia!" + mOcupacao(nMesa).ToString();
                }
                else
                {
                    lblOcupacao.Text = "A Mesa está Cheia!" + mOcupacao(nMesa).ToString();
                }
            }
            else
            {
                lblOcupacao.Text = "Há, " + checaMesa() + ",lugar(es) vago(s)!";
            }
        }

        protected void mostrarIntegrantesMesa()
        {
            //string nMesa = Request.QueryString["mesa"];
            string nMesa = ddlVerMesa.SelectedValue;
            string nAmbiente = Request.QueryString["ambiente"];
            
            //MessageBox.Show("Número da Mesa: " + nMesa + ", no Ambiente: " + nAmbiente);

            BLL ObjMesa = new BLL(conectSite);
            BLL ObjDados = new BLL(conectSite);

            string xRet = "";

            string query = " SELECT * FROM e_localizacao AS l " +
                           " INNER JOIN e_evento AS e ON e.id = l.id_evento " +
                           " WHERE l.n_mesa = '" + nMesa + "'            " +
                           " AND l.id_evento = '" + stEventoCadParticipante.Value + "' " +
                           " AND l.id_ambiente = '" + stEventoCadAmbiente.Value + "' ";

            string qDados = " SELECT n_local, p.nome, e.descricao, a.titulo, p.unidade FROM e_pessoas AS p " +
                            " INNER JOIN  e_evento AS e ON p.id_evento = e.id " +
                            " INNER JOIN e_ambiente AS a ON a.id = p.id_ambiente " +
                            " WHERE n_local = '" + nMesa + "' AND p.canmom IS NULL  " +
                            " AND p.id_evento = '" + stEventoCadParticipante.Value + "' " +
                            " AND p.id_ambiente = '" + stEventoCadAmbiente.Value + "' ";

            ObjDados.Query = qDados;
            ObjMesa.Query = query;

            DataTable dados = ObjDados.RetQuery();
            DataTable mesa = ObjMesa.RetQuery();

            //MessageBox.Show(query);
            //MessageBox.Show("Dados: " + qDados);

            try
            {
                if (!String.IsNullOrEmpty(dados.Rows.Count.ToString()))
                {
                    xRet += "<style>.lista{font-size: 14px;" +
                           " margin: 0; padding: 0; text-align:left; display: inline-block;}</style>";
                    xRet += "<div>";
                    xRet += "<span class='lista' style='padding-top: 5px; padding-bottom: 10px'>" + "Você selecionou a Mesa: " + mesa.Rows[0]["n_mesa"].ToString() + "</span>";
                    xRet += "</div>";

                    //lblNmesa.Text = xMesa;

                    if (dados.Rows.Count > 0)
                    {
                        xRet += "<div>";
                        xRet += "<span class='lista'>" + "Integrantes da Mesa:" + "</span><br />";
                        for (int i = 0; i < dados.Rows.Count; i++)
                        {                            
                            xRet += "<span class='lista' style='font-size: 12px;'>" + (i + 1) + " - " + dados.Rows[i]["nome"].ToString() + ", " + dados.Rows[i]["unidade"].ToString() + "</span><br />";
                        }
                        xRet += "</div>";
                    }
                    else
                    {
                        xRet += "<span> <p>" + "Ainda não há cadastrados nesta Mesa" + "</p></span>";
                    }

                    lblConsultaMesa.Text = xRet;
                }
                else
                {
                    //lblConsultaMesa.Text = "Não há registro! Você precisa Selecionar uma Mesa!";
                    lblConsultaMesa.Text = "A mesa está vazia!";
                }
            }
            catch (Exception e)
            {
                lblConsultaMesa.Text = "Erro: " + e.Message;
            }

        }

        protected void consultarMesa(object sender, EventArgs e) {

            //lblConsultaMesa.Text = consultarMesa();

            mostrarIntegrantesMesa();
        }
        protected void popularCadAmbiente()
        {
            BLL ObjDados = new BLL(conectSite);

            string evento = stEventoCadParticipante.Value;

            string campos = " * ";            
            string tabela = " e_ambiente WHERE id_evento = '" + evento + "'";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;

            stEventoCadAmbiente.DataSource = ObjDados.RetCampos();
            stEventoCadAmbiente.DataValueField = "id";
            stEventoCadAmbiente.DataTextField = "Titulo";
            stEventoCadAmbiente.DataBind();

            //MessageBox.Show("Você selecionou: " + stEventoCadAmbiente.Value);
        }

        public String capturarIdEvento()
        {
            popularCadAmbiente();
            //popularDdlVerMesa();

            IdEvento = stEventoCadParticipante.Value;

            return IdEvento;
        }
        protected void popularCadMesa()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjLista = new BLL(conectSite);

            capturarIdEvento();

            //MessageBox.Show("#popularCadMesa()    " + capturarIdEvento());

            string query = " SELECT 'id' AS id, 'id_evento' AS id_evento, 'id_ambi' AS id_ambiente, 'Selecionar' AS n_mesa, 'n_cadeiras' AS n_cadeiras, 'descricao' AS descricao, 1 AS ordem " +
                           " FROM e_status LIMIT 1" +
                           " UNION" +
                           " SELECT id, id_evento, id_ambiente, n_mesa, n_cadeiras, descricao, 2 AS ordem" +
                           " FROM e_localizacao " +
                           " WHERE id_evento = '" + IdEvento + "' AND ocupacao IS NULL ORDER BY n_mesa *1, n_mesa ASC";

            string query2 = " SELECT 'id' AS id, 'id_evento' AS id_evento, 'id_ambi' AS id_ambiente, 'Selecionar' AS n_mesa, 'n_cadeiras' AS n_cadeiras, 'descricao' AS descricao, 1 AS ordem " +
                           " FROM e_status LIMIT 1" +
                           " UNION" +
                           " SELECT id, id_evento, id_ambiente, n_mesa, n_cadeiras, descricao, 2 AS ordem" +
                           " FROM e_localizacao " +
                           " WHERE id_evento = '" + IdEvento + "' ORDER BY n_mesa *1, n_mesa ASC";

            //MessageBox.Show("Ambiente: " + stEventoCadAmbiente.Value + " # popularCadMesa()");

            //MessageBox.Show(query + " # popularCadMesa()");

            ObjDados.Query = query;
            ObjLista.Query = query2;

            DataTable dados = ObjDados.RetQuery();            

            if (dados.Rows.Count > 0)
            {                
                stBuscaMesa.DataSource = ObjDados.RetQuery();
                stBuscaMesa.DataValueField = "n_mesa";
                stBuscaMesa.DataTextField = "n_mesa";
                stBuscaMesa.DataBind();                
            }
            else
            {
                lblMsg.Text = "Não há lugares Cadastrado neste Ambiente";
            }

            ddlVerMesa.DataSource = ObjLista.RetQuery();
            ddlVerMesa.DataValueField = "n_mesa";
            ddlVerMesa.DataTextField = "n_mesa";
            ddlVerMesa.DataBind();

        }
        /*
        protected void popularDdlVerMesa()
        {
            BLL ObjDados = new BLL(conectSite);

            string idEventoMesa = capturarIdEvento();

            //MessageBox.Show(idEventoMesa);

            //MessageBox.Show("popularDdlVerMesa " + capturarIdEvento());

            string query = " SELECT 'id' AS id, 'id_evento' AS id_evento, 'id_ambi' AS id_ambiente, 'Selecionar' AS n_mesa, 'n_cadeiras' AS n_cadeiras, 'descricao' AS descricao, 1 AS ordem " +
                           " FROM e_status LIMIT 1" +
                           " UNION" +
                           " SELECT id, id_evento, id_ambiente, n_mesa, n_cadeiras, descricao, 2 AS ordem" +
                           " FROM e_localizacao " +
                           " WHERE id_evento = '" + "4" + "' ORDER BY n_mesa * 1, n_mesa ASC";
                           //" WHERE id_evento = '" + IdEvento + "'";
            
            //" WHERE id_evento = '" + stEventoCadParticipante.Value + "'";
            
            //MessageBox.Show("Capturado: " + capturarIdEvento().ToString());
            //MessageBox.Show("DDL: " + stEventoCadParticipante.Value);


            //" WHERE id_ambiente = '" + IdEvento + "' AND ocupacao IS NULL ";

            //MessageBox.Show("Ambiente: " + stEventoCadAmbiente.Value);

            //MessageBox.Show(query);

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            ddlVerMesa.DataSource = ObjDados.RetQuery();
            ddlVerMesa.DataValueField = "n_mesa";
            ddlVerMesa.DataTextField = "n_mesa";
            ddlVerMesa.DataBind();

        }       
        */

        protected void irParaMesa(object sender, EventArgs e)
        {
            string nMesa = stBuscaMesa.SelectedValue;
            string nAmbiente = stEventoCadAmbiente.Value;
            string nEvento = stEventoCadParticipante.Value;

            //MessageBox.Show("Evento:" + nEvento + " Mesa: " + nMesa + " Ambiente: " + nAmbiente  + " #verMesa(object sender, EventArgs e)");

            //xRet += "<a href='ContMaterias.aspx?IDContMat=" + IdMat + "' >";
            //Response.Redirect("Mesa.aspx?mesa=" + nMesa + ", _blank");

            if (stBuscaMesa.SelectedValue == "Selecionar")
            {
                lblMsg.Text = "Você Precisa Selecionar Uma Mesa!";
            }
            else
            {
                Response.Redirect("Mesa.aspx?evento=" + nEvento + "&mesa=" + nMesa + "&ambiente=" + nAmbiente);
            }
        }

        protected void paginarGwParticipante(object sender, GridViewPageEventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string IdEvent = ddlEvento.SelectedValue;

            string query = " SELECT * FROM e_pessoas WHERE id_evento = '"+ IdEvent + "' AND canmom IS NULL";

            ObjDados.Query = query;

            gvParticipantes.DataSource = ObjDados.RetCampos();
            gvParticipantes.PageIndex = e.NewPageIndex;
            gvParticipantes.DataBind();
        }//paginarGwAssociados

        protected void selecionarRegistroGvParticipantes(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string IdEvent = ddlEvento.SelectedValue;
            string IdPart = gvParticipantes.SelectedRow.Cells[1].Text;

            //MessageBox.Show(IdPart);

            //string query = " SELECT * FROM _e_pessoas " +
            string query = " SELECT * FROM e_pessoas WHERE id = '" + IdPart + "'";
            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            //Para Ocultar GridViw
            secExcluiParticipante.Attributes["class"] = "ocultar";
            

            lblMsgExcluir.Text = "Você Selecionou: " + gvParticipantes.SelectedRow.Cells[2].Text + ". Se você tem certeza, clique no Botão Excluir para concluir a operação.";
            lbtnExcluir.Visible = true;
            
        }//selecionarRegistroGvParticipantes

        protected void excluirParticipante(object sender, EventArgs e)
        {
            string IdPart = gvParticipantes.SelectedRow.Cells[1].Text;
            string nMesa = gvParticipantes.SelectedRow.Cells[3].Text;

            //MessageBox.Show("Id: " + IdPart + " Mesa: " + nMesa + " - Usuário: " + Session["LoginEventos"].ToString() );

            BLL ObjParticipante = new BLL(conectSite);
            BLL ObjOcupacao = new BLL(conectSite);            
            BLL ObjUpDateParticipante = new BLL(conectSite);
            BLL ObjUpDateOcupacao = new BLL(conectSite);

            //#Checa Tabelas
            string checaParticipante = " SELECT * FROM e_pessoas WHERE id = '" + IdPart + "'"; //Ver Participante
            string checaOcupacao = " SELECT * FROM e_localizacao WHERE n_mesa = '" + nMesa + "' AND ocupacao = '*'";//Checar se o campo Ocupação está "preenchido" (*) 

            //#Efetua UpDate nas Tabelas
            //string UpDateParticipante = " update e_pessoas set n_local = '" + nMesa + "' , canusu = '" + Session["LoginEventos"].ToString() + "', canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id in('" + IdPart + "')";            
            string tabelaParticipa =   " e_pessoas ";
            string valoresParticipa =  " canusu = '" + Session["LoginEventos"].ToString() + "' "+
                                       " , canmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string condicaoParticipa = " id in('" + IdPart + "')";

            //MessageBox.Show(valoresParticipa);

            //string UpDateOcupacao = " UPDATE e_localizacao SET ocupacao IS NULL , altusu = '"+ Session["LoginEventos"].ToString() + "', altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE n_mesa = '" + nMesa + "' ";

            string tabelaLocalizacao =   " e_localizacao ";
            string valoresLocalizacao =  " ocupacao = NULL "+
                                         " , altusu = '"+ Session["LoginEventos"].ToString() + "' "+
                                         " , altmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' ";
            string condicaoLocalizacao = " n_mesa = '" + nMesa + "' ";

            //#Chaca se Particpante e Ocupação 
            ObjParticipante.Query = checaParticipante;
            ObjOcupacao.Query = checaOcupacao;
            
            //#Edita Participante e Ocupação
            ObjUpDateParticipante.Tabela = tabelaParticipa;
            ObjUpDateParticipante.Valores = valoresParticipa;
            ObjUpDateParticipante.Condicao = condicaoParticipa;



            ObjUpDateOcupacao.Tabela = tabelaLocalizacao;
            ObjUpDateOcupacao.Valores = valoresLocalizacao;
            ObjUpDateOcupacao.Condicao = condicaoLocalizacao;

            DataTable dadosParticipante = ObjParticipante.RetQuery();
            DataTable dadosOcupacao = ObjOcupacao.RetQuery();

            //MessageBox.Show("Particpante: " + dadosParticipante.Rows.Count);
            //MessageBox.Show("Ocupacao: " + dadosOcupacao.Rows.Count);

            string msg = "";

            try
           {
                if (dadosParticipante.Rows.Count > 0)
                {                     
                    //MessageBox.Show("UPDATE" + tabelaParticipa + "SET" + valoresParticipa + "WHERE" +  condicaoParticipa);
                    
                    ObjUpDateParticipante.EditRegistro(tabelaParticipa, valoresParticipa, condicaoParticipa);

                    msg += "Cancelado com Sucesso! " + ObjUpDateParticipante.MsgErro;
                }

                if (dadosOcupacao.Rows.Count > 0)
                {
                    //MessageBox.Show(dadosOcupacao.Rows[0]["n_mesa"].ToString() + ", Vou Excluir!");
                    //MessageBox.Show("Ocupação: " + ObjUpDateOcupacao.Query.ToString());
                    
                    //MessageBox.Show("UPDATE" + tabelaLocalizacao + "SET" + valoresLocalizacao + "WHERE" + condicaoLocalizacao);
                    
                    ObjUpDateOcupacao.EditRegistro(tabelaLocalizacao, valoresLocalizacao, condicaoLocalizacao);

                    msg += "Esta Mesa foi liberada para inserção de Participantes! " + ObjUpDateOcupacao.MsgErro;
                }
                else
                {                    
                    msg +=  "Mesa não está completa. Pode continuar cadastrando Participantes!";
                }

                lblMsgExcluir.Text = msg;
            }
            catch (Exception n)
            {
                lblMsg.Text = "Erro: " + n.Message;
            }

            lbtnExcluir.Visible = false;
            lbtnFinalizar.Visible = true;



        }//excluirParticipante
        protected void finalizarExclusao(object sender, EventArgs e)
        {
            Response.Redirect("Eventos.aspx");
        }//finalizarExclusao

        protected void procurarParticipante(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string IdEvent = ddlEvento.SelectedValue;

            string nomeParticipante = iBusca.Value.Replace(" ", "%");

            string query = " SELECT id, nome AS Nome, n_local AS Mesa FROM e_pessoas WHERE id_evento = '" + IdEvent + "'  AND nome LIKE '%" + nomeParticipante + "%' AND canmom IS NULL ";

            ObjDados.Query = query;

            //MessageBox.Show(ObjDados.Query);

            DataTable dados = ObjDados.RetQuery();

            if (String.IsNullOrEmpty(iBusca.Value))
            {
                lblMsg.Text = "É preciso Digitar um Nome para Pesquisar";
                iBusca.Focus();
            }
            else
            {
                if (dados.Rows.Count > 0)
                {
                    gvParticipantes.DataSource = dados;
                    gvParticipantes.DataBind();
                    secExcluiParticipante.Attributes["class"] = "exibir";
                }
                else
                {
                    lblMsg.Text = "Nome não encontrado";
                }
            }

        }



        protected void relatorioAZ(object sender, EventArgs e)
        {
            
            BLL ObjCadeiras = new BLL(conectSite);

            string IdEvent = ddlRelatorios.SelectedValue;

            //MessageBox.Show(IdEvent.ToString());
            //string query = "";
            /* query = " SELECT * " +
                           " FROM e_localizacao AS l" +
                           " LEFT JOIN e_pessoas AS p ON l.n_mesa = p.n_local " +
                           " LEFT JOIN e_evento AS e ON p.id_evento = e.id " +
                           " LEFT JOIN e_ambiente AS a ON p.id_ambiente = a.id " +
                           " WHERE l.id_evento = '" + IdEvent + "' AND l.id IS NOT NULL ORDER BY l.n_mesa";
            */            
            
            string queryParticipantes = " SELECT * FROM e_pessoas WHERE id_evento = '" + IdEvent + "' AND canmom IS NULL ORDER BY nome";
            
            ObjCadeiras.Query = queryParticipantes;
            
            DataTable cadeiras = ObjCadeiras.RetQuery();

            string xRet = "";                        

            int titular = 0;
            int dependente = 0;
            int convidado = 0;

            try
            {
                if (cadeiras.Rows.Count > 0)
                {
                    xRet += "<section>";
                    xRet += "<div>";
                    xRet += "<div>";
                    xRet += "<style>" +
                            "caption{ font-size: 18px}" +
                            //"table, td{ width: 850px; border: 1px solid #f26907; font-size: 12px};" +
                            "table { border-collapse: collapse; font-size: 12px }" +
                            "th, td { border-bottom: solid 1px #9d9b9b; padding: 0; padding-top: 10px; }" + 
                            "td:first-child { width: 300px;  }" + 
                            "td:first-child + td { width: 300px }"+
                            "td:first-child + td + td { width: 20px; text-align: center; }" +
                            "td:first-child + td + td  + td{ width: 30px }" +
                            "tfoot tr, td{ width:auto; }" +
                    "</style> ";
                    xRet += "</div>";
                    xRet += "<table>";
                    xRet += "<caption> Relatório A - Z </caption>";
                    xRet += "<thead>";
                    xRet += "<tr>";
                    xRet += "<td> Nome </td>";
                    xRet += "<td> Responsável </td>";
                    xRet += "<td> Mesa </td>";
                    xRet += "<td> Tipo </td>";
                    xRet += "</tr>";
                    xRet += "</thead >";
                    xRet += "<tbody>";
                    for (int i = 0; i < cadeiras.Rows.Count; i++)
                    {
                        if (cadeiras.Rows[i]["cod_tipopessoa"].ToString() == "TIT")
                        {
                            titular++;
                            xRet += "<tr>";
                            xRet += "<td> " + cadeiras.Rows[i]["nome"].ToString().ToUpper() + " </td>";
                            xRet += "<td> " + "" + " </td>";
                            xRet += "<td style='width: 50px;'> " + cadeiras.Rows[i]["n_local"].ToString()  + " </td>";
                            xRet += "<td style='width: 50px;'> Titular </td>";
                            xRet += "</tr>";
                        }
                        if (cadeiras.Rows[i]["cod_tipopessoa"].ToString() == "DEP")
                        {
                            dependente++;
                            xRet += "<tr>";
                            xRet += "<td> " + cadeiras.Rows[i]["nome"].ToString().ToUpper() + " </td>";
                            xRet += "<td > " + cadeiras.Rows[i]["nometitular"].ToString().ToUpper() + " </td>";
                            xRet += "<td style='width: 50px;'> " + cadeiras.Rows[i]["n_local"].ToString() + " </td>";
                            xRet += "<td style='width: 50px;'> Dependente </td>";
                            xRet += "</tr>";
                        }
                        if (cadeiras.Rows[i]["cod_tipopessoa"].ToString() == "CON")
                        {
                            convidado++;
                            xRet += "<tr>";
                            xRet += "<td> " + cadeiras.Rows[i]["nome"].ToString().ToUpper() + " </td>";
                            xRet += "<td style='width: 50px;'> " + cadeiras.Rows[i]["nometitular"].ToString().ToUpper() + " </td>";
                            xRet += "<td style='width: 50px;'> " + cadeiras.Rows[i]["n_local"].ToString() + " </td>";
                            xRet += "<td> Convidado </td>";
                            xRet += "</tr>";
                        }
                    }
                    xRet += "</tbody>";                   
                    xRet += "</table>";                    
                    xRet += "</div>";
                    xRet += "<p>  </p>";
                    xRet += "<section>";
                    xRet += "<table>";
                    xRet += "<caption>" + "Número de Participantes" + "<caption>";
                    xRet += "<tbody>";
                    xRet += "<tr>";
                    xRet += "<td> Titular: </td>";
                    xRet += "<td> " + titular + " </td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td> Dependente </td>";
                    xRet += "<td> " + dependente + " </td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td> Convidado </td>";
                    xRet += "<td> " + convidado + " </td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td> Total </td>";
                    xRet += "<td> " + (titular + dependente + convidado) + " </td>";
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</section>";
                    xRet += "</section>";
                    
                }
                else
                {
                    xRet += "Não há dados!";
                }
            }
            catch (Exception n)
            {
                xRet += "Erro: " + n.Message;
            }

            secRelatorios.Attributes["class"] = "exibir";

            lblListaRelaorios.Text = xRet;

        }

        protected void relatorioPorDia(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string IdEvent = ddlRelatorios.SelectedValue;

            //MessageBox.Show(IdEvent.ToString());

            string xRet = "";

            //string query = " SELECT EXTRACT(DAY FROM (cadmom)) AS dia, EXTRACT(MONTH FROM (cadmom)) AS Mes, EXTRACT(YEAR FROM (cadmom)) AS ano, COUNT(cod_tipopessoa) AS entregues FROM e_pessoas " +
            string query = " SELECT CONCAT(EXTRACT(DAY FROM (cadmom)),'/' , EXTRACT(MONTH FROM (cadmom)), '/', EXTRACT(YEAR FROM (cadmom))) AS Dia, COUNT(cod_tipopessoa) AS entregues, " +
                           " (SELECT COUNT(nome) FROM e_pessoas WHERE id_evento ='" + IdEvent + "' AND canmom IS NULL) AS Qtde " +
                           " FROM e_pessoas " +
                           " WHERE id_evento = '" + IdEvent + "' AND canmom IS NULL " +
                           " GROUP BY EXTRACT(DAY FROM(cadmom))  ";
            
            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            //MessageBox.Show(dados.Rows.Count.ToString());    

            if (dados.Rows.Count>0)
            {
                xRet += "<section>";
                xRet += "<table>";
                xRet += "<caption> Entrega de Convites </caption>";
                xRet += "<thead>";
                xRet += "<tr>";
                xRet += "<td>";
                xRet += "Data";
                xRet += "</td>";
                xRet += "<td>";
                xRet += "Quantidade";
                xRet += "</td>";
                xRet += "</thead>";
                xRet += "<tbody>";

                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    xRet += "<tr>";
                    xRet += "<td>";
                    xRet += dados.Rows[i]["dia"].ToString();
                    xRet += "</td>";
                    xRet += "<td>";
                    xRet += dados.Rows[i]["entregues"].ToString();
                    xRet += "</td>";
                    xRet += "</tr>";
                }
                xRet += "<tr>";
                xRet += "<td>" + "Total" + "</td>";
                if (dados.Rows.Count > 0)
                {
                    xRet += "<td>" + dados.Rows[0]["Qtde"].ToString() + "</td>";
                }
                xRet += "</tr>";
                xRet += "</tbody>";
                xRet += "</table>";
                xRet += "</section>";
            }
            else
            {
                xRet+="Não há dados para ser exibido";
            }

            

            secRelatorios.Attributes["class"] = "exibir";

            lblListaRelaorios.Text = xRet;

        }

        protected void relatorioPorLocal(object sender, EventArgs e)
        {
            BLL ObjMesas = new BLL(conectSite);
            BLL ObjCadeiras = new BLL(conectSite);

            string IdEvent = ddlRelatorios.SelectedValue;

            //MessageBox.Show(IdEvent.ToString());

            string mesas = " SELECT * FROM e_localizacao ";
            //string cadeiras = " SELECT * FROM e_pessoas WHERE id_evento = '2'";
            
            string cadeiras = " SELECT id_evento, id_ambiente, n_mesa AS n_local, '' AS  cod_tipopessoa,  n_cadeiras, '' AS nome, '' AS nometitular FROM e_localizacao WHERE id_evento = '" + IdEvent + "'" +
                " UNION " +
                " SELECT id_evento, id_ambiente, n_local, cod_tipopessoa, '' AS n_cadeiras, nome, nometitular FROM e_pessoas " +
                " WHERE id_evento = '" + IdEvent + "' AND canmom IS NULL ORDER BY n_local, nome, n_cadeiras DESC";
            
            string xRet = "";
            
            ObjMesas.Query = mesas;
            ObjCadeiras.Query = cadeiras;


            DataTable dMesas = ObjMesas.RetQuery();
            DataTable dCadeiras = ObjCadeiras.RetQuery();

            int qtdemesa = 0;
            xRet += "<style>" +
                ".CabecalhoMesa{ margin: 20px 0 5px 0;}" +
                ".cadeiras{margin: 0px 0 0px 0 }" +
                "</style>";
            xRet += "<div>";
            xRet += "<p> Relatório: Mesas...</p>";            
            for (int i = 0; i< dCadeiras.Rows.Count; i++)
            {
                //xRet += "<p>" + dMesas.Rows[i]["n_mesa"].ToString() + "</p>";
                /*for (int j = 0; j<dCadeiras.Rows.Count; j++)
                {
                    xRet += "<p>" + dCadeiras.Rows[j]["n_local"].ToString() + ", " + dCadeiras.Rows[j]["nome"].ToString() + " - " + dCadeiras.Rows[j]["cod_tipopessoa"].ToString() + "</p>";
                }
                */

                xRet += "<div>";
                if (!String.IsNullOrEmpty(dCadeiras.Rows[i]["n_cadeiras"].ToString()))
                {
                    if (Convert.ToInt32(dCadeiras.Rows[i]["n_local"].ToString()) == Convert.ToInt32(dCadeiras.Rows[i]["n_local"].ToString()))
                    {
                        //qtdemesa++;
                        qtdemesa++;
                    }
                    //MessageBox.Show(qtdemesa.ToString());

                    xRet += "<div class='CabecalhoMesa'>";
                    xRet += "<p>" + "Mesa: " + dCadeiras.Rows[i]["n_local"].ToString();
                    xRet += "</div>";
                    
                    
                    if (dCadeiras.Rows[i]["n_cadeiras"].ToString() != "" ) {
                        //qtdemesa++;
                    }
                }
                else
                {
                    xRet += "<div class='cadeiras'>";                    
                    if (dCadeiras.Rows[i]["cod_tipopessoa"].ToString() == "TIT")
                    {
                        xRet += "<p>" + dCadeiras.Rows[i]["nome"].ToString().ToUpper() + " - " + "Titular" + "</p>";
                    }
                    if (dCadeiras.Rows[i]["cod_tipopessoa"].ToString() == "DEP")
                    {
                        xRet += "<p>" + dCadeiras.Rows[i]["nome"].ToString().ToUpper() + " - " + "Dependente" + "</p>";
                    }
                    if (dCadeiras.Rows[i]["cod_tipopessoa"].ToString() == "CON")
                    {
                        xRet += "<p>" + dCadeiras.Rows[i]["nome"].ToString().ToUpper() + " - " + "Convidado" + "</p>";
                    }
                    xRet += "</div>";
                }
                xRet += "</div>";
                //xRet += "Essa mesa tem: " + qtdemesa + ", pessoas.";
            }
            xRet += "</div>";

            secRelatorios.Attributes["class"] = "exibir";
            lblListaRelaorios.Text = xRet;

        }

    }//class
}