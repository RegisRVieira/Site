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
                //popularddlListaEventos();                
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
            popularddlListaEventos();                
            
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
            string condicao = "WHERE cod_status = 'ATI'";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;

            stFinanEvento.DataSource = ObjDados.RetCampos();
            stFinanEvento.DataValueField = "id";
            stFinanEvento.DataTextField = "descricao";
            stFinanEvento.DataBind();
        }

        protected void identificarEvento(object sender, EventArgs e)
        {          
            string evento = stEvento.SelectedValue.ToString();

            //MessageBox.Show(evento);

            BLL ObjDados = new BLL(conectSite);


            string query = " SELECT  * FROM  e_ambiente " +
                           " WHERE id_evento = '" + evento + "' ";

            ObjDados.Query = query;

            stAmbiente.DataSource = ObjDados.RetQuery();
            stAmbiente.DataValueField = "id";
            stAmbiente.DataTextField = "Titulo";
            stAmbiente.DataBind();            


        }
        
        protected void popularddlListaEventos()
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjEvento = new BLL(conectSite);
                        
            string query = " SELECT * FROM e_evento " +
                           " WHERE CURDATE() <= dtevento ";
             
            ObjDados.Query = query;
            //# Carrega dados para o DDL do Ambiente
            ddlListaEventos.DataSource = ObjDados.RetQuery();
            ddlListaEventos.DataValueField = "id";
            ddlListaEventos.DataTextField = "descricao";
            ddlListaEventos.DataBind();
            
            //# Carrega dados para as Mesas
            stEvento.DataSource = ObjDados.RetQuery();
            stEvento.DataValueField = "id";
            stEvento.DataTextField = "descricao";
            stEvento.DataBind();

            //Popular DDL Ambientes do Evento
            string query2 = " SELECT  * FROM  e_ambiente " +
                           " WHERE id_evento = '" + stEvento.SelectedValue +  "'";

            ObjEvento.Query = query2;

            stAmbiente.DataSource = ObjEvento.RetQuery();
            stAmbiente.DataValueField = "id";
            stAmbiente.DataTextField = "Titulo";
            stAmbiente.DataBind();

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

            string msgRetorno = "";

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

            //lblMsg.Text = "Você selecionou o Evento: " + stEvento.Value + ", tem certeza?";
            //lblMsg.Text = "Ambiente: " + stAmbiente.Value + ", tenha certeza antes de continuar!";


            DataTable valida = ObjDados.RetCampos();            

            int checaMesas = 0;

            int mesaInicio = 0;
            int mesaFinal = 0;
            int nLocalizacao = (mesaInicio - 1);
            int qtdMesas = 0;

            //#Checa se Campos foram preenchido e estancia valor na variável correspondente.
            if (!String.IsNullOrEmpty(iMesaNumIni.Value))
            {
                mesaInicio = (Convert.ToInt32(iMesaNumIni.Value));
            }
            else
            {
                iMesaNumIni.Value = "Preencha este Campo!";                
            }
            if (!String.IsNullOrEmpty(iMesaNumFin.Value))
            {
                mesaFinal= (Convert.ToInt32(iMesaNumFin.Value));
            }
            else
            {            
                iMesaNumFin.Value = "Preencha este Campo!";
            }
            if (String.IsNullOrEmpty(iMesaCadeiras.Value))
            {                            
                iMesaCadeiras.Value = "Preencha este Campo!";
            }

            //MessageBox.Show("Início : " + mesaInicio);
            //MessageBox.Show("Fim : " + mesaFinal);

            //Calcula a quantidade de Mesas a ser cadastrada.
            qtdMesas = ((mesaFinal) - (mesaInicio - 1));

            //MessageBox.Show("Será cadastrado: " + qtdMesas + " Mesas");

            
            if (mesaFinal >= mesaInicio)
            {
                //Checar Validade do Intervalo de Mesa.Ou seja, mesa final não pode ser menor do que a mesa início
                //#Caso a mesa final seja menor que a inicial, não permite prosseguir

                if (ObjValida.MsgErro == "")
                {
                    if (valida.Rows.Count > 0)
                    {
                        for (int i = 0; i < valida.Rows.Count; i++)
                        {//Checar o Evento ####################################
                            if (valida.Rows[i]["id_evento"].ToString() == stEvento.SelectedValue)
                            {
                                if (valida.Rows[i]["n_mesa"].ToString() == iMesaNumIni.Value || valida.Rows[i]["n_mesa"].ToString() == iMesaNumFin.Value)
                                {
                                    checaMesas++;
                                }
                            }
                        }
                    }//Valida: Checa se a mesa a ser inserida já está cadastrada

                    int contador = (mesaInicio - 1);

                    if (checaMesas > 0)//Se a Mesa já estiver cadastrada, dá Msg dizendo que não pode. Caso contrário, cadastra
                    {
                        msgRetorno += "Não é possivel Cadastrar, pois já existe mesas com essa numeração.";
                    }
                    else
                    {

                        for (int i = 0; i < qtdMesas; i++)
                        {
                            contador++;
                            nLocalizacao++;

                            //MessageBox.Show(contador.ToString());                        

                            if (i < (qtdMesas - 1))
                            {
                                valores += String.Format("('" + stEvento.SelectedValue + "'," +
                                                        "'" + stAmbiente.SelectedValue + "'," +
                                                        "" + contador + "," +
                                                        "" + iMesaCadeiras.Value + "," +
                                                        "'" + iMesasDescricao.Value + "'," +
                                                        "'" + iMesasObserva.Value + "'," +
                                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                                        "'" + Session["LoginEventos"].ToString() + "')," + "\n");
                            }
                            else
                            {
                                valores += String.Format("('" + stEvento.SelectedValue + "'," +
                                                       "'" + stAmbiente.SelectedValue + "'," +
                                                       "" + contador + "," +
                                                        "" + iMesaCadeiras.Value + "," +
                                                       "'" + iMesasDescricao.Value + "'," +
                                                       "'" + iMesasObserva.Value + "'," +
                                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "'," +
                                                       "'" + Session["LoginEventos"].ToString() + "')" + "\n");
                            }
                            //lblNumSorteio.Text += i.ToString();
                        }

                        //# Inserir dados no DB                    
                        try
                        {
                            ObjDados.InsertEmLote(tabela, campos, valores);
                            msgRetorno += " Gravado com Sucesso!!!";
                        }
                        catch (Exception x)
                        {
                            msgRetorno = x.Message;
                        }
                        //MessageBox.Show(tabela + " * " + campos + " * " + valores);



                        //MessageBox.Show("INSERT INTO " + tabela + " (" + campos + ") " + " VALUES " + valores);
                    }

                }
                else
                {
                    msgRetorno = "Erro: " + ObjValida.MsgErro;
                }
            }
            else
            {
                msgRetorno = "Intervalo de mesa incorreto! É necessário que a mesa final seja maior que a mesa inicial.";

            }

            lblMsg.Text = msgRetorno;

        }//gravarMesas

        
    }//Fim class
}