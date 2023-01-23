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

namespace Site.Eventos
{
    public partial class EventoConfig : System.Web.UI.Page
    {
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                popularddlEventoTipo();
                popularddlListaEventos();
                popularddlEventoAmbiente();
                mostrarLogado();
            }
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
        protected void ativarViwes(int ID)
        {
            mwConfigEventos.ActiveViewIndex = ID;
        }

        protected void ativarEventos(object sender, EventArgs e)
        {
            ativarViwes(0);

        }
        protected void ativarAmbiente(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                popularddlListaEventos();
            }
            ativarViwes(1);
        }
        protected void ativarMesas(object sender, EventArgs e)
        {
            ativarViwes(2);
        }
        protected void ativarFinanceiro(object sender, EventArgs e)
        {
            ativarViwes(3);
            PopularddlFinanceiro();
        }

        protected void popularddlEventoTipo()
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " e_tipo ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;

            ddlEventoTipo.DataSource = ObjDados.RetCampos();
            ddlEventoTipo.DataValueField = "cod";
            ddlEventoTipo.DataTextField = "descricao";
            ddlEventoTipo.DataBind();
        }

        protected void PopularddlFinanceiro()
        {

            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            //string tabela = " _e_evento ";
            string tabela = " e_evento ";
#pragma warning disable CS0219 // A variável "condicao" é atribuída, mas seu valor nunca é usado
            string condicao = "WHERE cod_status = 'ATI'";
#pragma warning restore CS0219 // A variável "condicao" é atribuída, mas seu valor nunca é usado

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;

            stFinanEvento.DataSource = ObjDados.RetCampos();
            stFinanEvento.DataValueField = "id";
            stFinanEvento.DataTextField = "descricao";
            stFinanEvento.DataBind();
        }
        protected void popularddlEventoAmbiente()
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            //string tabela = " _e_ambiente ";
            string tabela = " e_ambiente ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;

            stAmbiente.DataSource = ObjDados.RetCampos();
            stAmbiente.DataValueField = "id";
            stAmbiente.DataTextField = "Titulo";
            stAmbiente.DataBind();
        }

        protected void popularddlListaEventos()
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            //string tabela = " _e_evento ";
            string tabela = " e_evento ";
            string condicao = "WHERE cod_status = 'ATI'";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            ddlListaEventos.DataSource = ObjDados.RetCampos();
            ddlListaEventos.DataValueField = "id";
            ddlListaEventos.DataTextField = "descricao";
            ddlListaEventos.DataBind();
            //# Mesas
            stEvento.DataSource = ObjDados.RetCampos();
            stEvento.DataValueField = "id";
            stEvento.DataTextField = "descricao";
            stEvento.DataBind();

        }
        protected void gravarEvento(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            //string tabela = " _e_evento ";
            string tabela = " e_evento ";
            string campos = " cod_tipo, cod_status, Descricao, Local, Detalhe, dtEvento, horaIni, horaFin, CadMom, CadUsu ";
            string valores = String.Format("('" + ddlEventoTipo.SelectedValue + "'," +
                                       "'" + "ATI" + "'," +
                                       "'" + iEventoTitulo.Value + "'," +
                                       "'" + iEventoLocal.Value + "'," +
                                       "'" + taEventoDetalhe.Value + "'," +
                                       "'" + tbEventoData.Text + "'," +
                                       "'" + iEventoHoraIni.Value + "'," +
                                       "'" + iEventoHoraFim.Value + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                       "'" + Session["LoginEventos"].ToString() + "')" + "\n");

            //MessageBox.Show(campos + "#" + tabela + "#" + valores);

            ObjDados.InsertEmLote(tabela, campos, valores);

            //#Validação
            BLL ObjValida = new BLL(conectSite);

#pragma warning disable CS0219 // A variável "vCampos" é atribuída, mas seu valor nunca é usado
            string vCampos = " * ";
#pragma warning restore CS0219 // A variável "vCampos" é atribuída, mas seu valor nunca é usado
            //string vTabela = " _e_evento ";
#pragma warning disable CS0219 // A variável "vTabela" é atribuída, mas seu valor nunca é usado
            string vTabela = " e_evento ";
#pragma warning restore CS0219 // A variável "vTabela" é atribuída, mas seu valor nunca é usado
            string vCondicao = "WHERE Descricao = '" + iEventoTitulo.Value + "' ORDER BY id DESC ";

            DataTable dados = ObjDados.RetCampos();

            if (dados.Rows.Count > 0)
            {
                lblMsg.Text = "Cadstrado Realizado com Sucesso";
            }
            else
            {
                lblMsg.Text = "Deu alguma coisa Errada!";
            }

        }//gravarEvento

        protected void gravarAmbiente(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " id_evento, Titulo, Descricao, Detalhe, CadMom, CadUsu ";
            //string tabela = " _e_ambiente ";
            string tabela = " e_ambiente ";
            string valores = String.Format("('" + ddlListaEventos.SelectedValue + "'," +
                                       "'" + iAmbienteTitulo.Value + "'," +
                                       "'" + iAmbienteDescricao.Value + "'," +
                                       "'" + tbAmbienteDetalhes.Value + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                       "'" + Session["LoginEventos"].ToString() + "')" + "\n");

            //MessageBox.Show("INSERT INTO " + tabela + " (" + campos + ") " + " SET " + valores);

            ObjDados.InsertEmLote(tabela, campos, valores);

            MessageBox.Show(ObjDados.MsgErro);

            //# Validação
            BLL ObjValida = new BLL(conectSite);

            string vCampos = " * ";
            //string vTabela = " _e_ambiente ";
            string vTabela = " e_ambiente ";
            string vCondicao = " WHERE titulo = '" + iAmbienteTitulo.Value + "' ORDER BY id DESC ";

            ObjValida.Campo = vCampos;
            ObjValida.Tabela = vTabela;
            ObjValida.Condicao = vCondicao;

            DataTable dados = ObjValida.RetCampos();



            if (dados.Rows.Count > 0)
            {
                lblMsg.Text = "Ambiente gravado com Sucesso!!!";
            }
            else
            {
                lblMsg.Text = "Aconteceu algum problema, não foi gravado! " + ObjValida.MsgErro;
            }

        }//gravarAmbiente

        protected void gravarMesas(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);
            //string tabela = " _e_localizacao ";
            string tabela = " e_localizacao ";
            string campos = " id_evento, id_ambiente, n_mesa, n_cadeiras, Descricao, Detalhe, CadMom, CadUsu ";
            string valores = "";

            string condicao = "";
            //#Dados
            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Valores = valores;
            //#Validação
            ObjValida.Tabela = tabela;
            ObjValida.Campo = campos;
            ObjValida.Condicao = condicao;

            DataTable valida = ObjDados.RetCampos();
            DataTable dados = ObjDados.RetQuery();

            int checaMesas = 0;
            int numInicio = (Convert.ToInt32(iMesaNumIni.Value));
            int qtdNumeros = Convert.ToInt32(iMesaNumFin.Value);
            int nLocalizacao = (numInicio - 1);


            if (ObjValida.MsgErro == "")
            {
                MessageBox.Show("Valida: " + valida.Rows.Count.ToString());
            }
            else
            {
                MessageBox.Show(ObjValida.MsgErro);
            }


            if (ObjValida.MsgErro == "")
            {
                if (valida.Rows.Count > 0)
                {
                    for (int i = 0; i < valida.Rows.Count; i++)
                    {//Checar o Evento ####################################
                        if (valida.Rows[i]["id_evento"].ToString() == stEvento.Value)
                        {
                            if (valida.Rows[i]["n_mesa"].ToString() == iMesaNumIni.Value || valida.Rows[i]["n_mesa"].ToString() == iMesaNumFin.Value)
                            {
                                checaMesas++;
                            }
                        }
                    }
                }//Valida: Checa se a mesa a ser inserida já está cadastrada

                if (checaMesas > 0)//Se a Mesa já estiver cadastrada, dá Msg dizendo que não pode. Caso contrário, cadastra
                {
                    lblMsg.Text = "Não é possivel Cadastrar, pois já existe mesas com essa numeração.";
                }
                else
                {
                    for (int i = 0; i < qtdNumeros; i++)
                    {
                        nLocalizacao++;
                        numInicio++;

                        if (i < (qtdNumeros - 1))
                        {                            
                            valores += String.Format("('" + stEvento.Value + "'," +
                                                    "'" + stAmbiente.Value + "'," +
                                                    "" + (numInicio + 1) + "," +
                                                    "" + iMesaCadeiras.Value + "," +
                                                    "'" + iMesasDescricao.Value + "'," +
                                                    "'" + iMesasObserva.Value + "'," +
                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                                    "'" + Session["LoginEventos"].ToString() + "')," + "\n");
                        }
                        else
                        {
                            valores += String.Format("('" + stEvento.Value + "'," +
                                                   "'" + stAmbiente.Value + "'," +
                                                   "" + (numInicio + 1) + "," +
                                                    "" + iMesaCadeiras.Value + "," +
                                                   "'" + iMesasDescricao.Value + "'," +
                                                   "'" + iMesasObserva.Value + "'," +
                                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                                   "'" + Session["LoginEventos"].ToString() + "')" + "\n");
                        }
                        //lblNumSorteio.Text += i.ToString();
                    }

                    //# Inserir dados no DB

                    //ObjDados.InsertEmLote(tabela, campos, valores);
                    MessageBox.Show(tabela + " * " + campos + " * " + valores);

                    MessageBox.Show("Gravou!!!");

                    //MessageBox.Show("INSERT INTO " + tabela + " (" + campos + ") " + " VALUES " + valores);
                }

            }
            else
            {
                lblMsg.Text = "Erro: " + ObjValida.MsgErro;
            }


        }//gravarMesas

    }//Fim class
}