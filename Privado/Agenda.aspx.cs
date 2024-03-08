using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Configuration;
using System.Data;

namespace Site.Privado
{
    public partial class Agenda : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ativarViewPadrao();
                DataBind();
                inserirHoje();
            }
            
        }
        protected void ativarViewCadastrar(object sender, EventArgs e)
        {
            //Response.Redirect("https://localhost:44320/privado/agenda.aspx");
            mvAgenda.ActiveViewIndex = 1;
            iData.Value = DateTime.Now.ToString("dd-MM-yyyy");
        }
        protected void ativarViewAvaliar(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Redirect("https://localhost:44320/privado/agenda.aspx");
            }
            else
            {
                mvAgenda.ActiveViewIndex = 2;
                iDataAvalia.Value = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        protected void ativarViewPadrao()
        {
            mvAgenda.ActiveViewIndex =  0;
        }
        protected void ativarViewLista(object sender, EventArgs e)
        {
            mvAgenda.ActiveViewIndex = 0;
            checarData();
        }
        protected void checarData(object sender, EventArgs e)
        {
            string vazio = cAgenda.SelectedDate.ToShortDateString();

            DateTime agora = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            DateTime agenda = Convert.ToDateTime(cAgenda.SelectedDate.ToString("dd-MM-yyyy"));


            int r = DateTime.Compare(agora,agenda);
            //https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-8.0            
            //#### DateTime.Compare ####
            //Tipo de valor Condição
            //Menos do que zero   t1 é mais cedo do que t2( , . e.
            //Zero (s) t1 É o mesmo que t2 ( , . e. . (
            //Maior que zero  t1é mais tarde do que t2( , . e.
            //#### # # # # # # # #  ####
            
            if (r <= 0)
            {
                lblMsg.Text = String.Empty;
                // lblMsg.Text = "Pode: " + "Dia: " + DateTime.Now.ToString("dd") + ", Mês: " + DateTime.Now.ToString("MM") + ", Ano: " + DateTime.Now.ToString("yyyy") + ", Dia: " + cAgenda.SelectedDate.ToString("dd") + ", Mês: " + cAgenda.SelectedDate.ToString("MM") + ", Ano: " + cAgenda.SelectedDate.ToString("yyyy");
                if (cAgenda.SelectedDate.ToString("dd-MM-yyyy") == "01-01-0001")
                {
                    //lblMsg.Text = "Selecione uma Data para continuar";
                    iData.Value = DateTime.Now.ToString("dd-MM-yyyy");

                    if (checarData() != cAgenda.SelectedDate.ToString("dd-MM-yyyy"))
                    {
                        lblHoje.Text = "<div>" + cAgenda.SelectedDate.ToString("dd-MM-yyyy") + "</div>";
                    }
                    else
                    {
                        lblHoje.Text = checarData();
                    }
                }
                else
                {
                    //lblMsg.Text = cAgenda.SelectedDate.ToString("yyyy-MM-dd");
                    iData.Value = cAgenda.SelectedDate.ToString("dd-MM-yyyy");

                    if (checarData() != cAgenda.SelectedDate.ToString("dd-MM-yyyy")) 
                    {
                        lblHoje.Text = "<div>" + cAgenda.SelectedDate.ToString("dd-MM-yyyy") + "</div>";
                    }
                    else
                    {
                        lblHoje.Text = checarData();
                    }
                }
            }
            else
            {
                //lblMsg.Text = "Não pode" + "Dia: " + DateTime.Now.ToString("dd") + ", Mês: " + DateTime.Now.ToString("MM") + ", Ano: " + DateTime.Now.ToString("yyyy") + ", Dia: " + cAgenda.SelectedDate.ToString("dd") + ", Mês: " + cAgenda.SelectedDate.ToString("MM") + ", Ano: " + cAgenda.SelectedDate.ToString("yyyy");
                lblMsg.Text = cAgenda.SelectedDate.ToString("dd-MM-yyyy") + ", não é uma data válida para agendamento. Selecione a data presente, ou uma data futura para continuar!";
            }            
            
        }//checarData

        protected String checarData()
        {
            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            //lblHoje.Text = DateTime.Now.ToString("dd-MM-yyyy");

            //lblHoje.Text = hoje;

            return hoje;
        }//checarData
        protected String inserirHoje()
        {            
            lblHoje.Text = DateTime.Now.ToString("dd-MM-yyyy");
                       
            return lblHoje.Text;
        }//inserirHoje
        protected String listaPacientes() {

            string paciente = Request.QueryString["123"];

            string xRet = "";

            //xRet += "<div> "+ checarData() + " </div>";
            xRet += "<ul>";
            xRet += "<li class='agenda-lista-nome'><a href = 'Agenda_Ficha.aspx?paciente=123' > 08:00 - José da Silva</a> <span style = 'color: green; font-size: 12px;' > Consulta </ span ></ li >";
            xRet += "<li class='agenda-lista-nome'><a href = 'Agenda_Ficha.aspx?paciente=123' > 09:00 - Maria da Silva</a> <span style = 'color: green; font-size: 12px;' > Consulta </ span ></ li >";
            xRet += "<li class='agenda-lista-nome'><a href = 'Agenda_Ficha.aspx?paciente=123' > 10:00 - Joana da Silva</a> <span style = 'color: green; font-size: 12px;' > Consulta </ span ></ li >";                        
            xRet += "<li>11:00 - Solange da Silva<span style='color: orangered; font-size: 12px;'> Avaliação</span></li>";
            xRet += "<li>13:00 - Marta da Silva<span style='color: green; font-size: 12px;'> Consulta</span></li>";
            xRet += "<li>13:30 - Claudete da Silva<span style='color: #1552f8; font-size: 12px;'> Pilates</span></li>";
            xRet += "</ul>";

            return xRet;

        }
        public String dia()
        {            
            string dia = DateTime.Now.Day.ToString();            

            return dia;
        }

        protected void selecionarRegistroGvPacientes(object sender, EventArgs e)
        {
            
            string nome = "";

            try
            {                
                nome = gvPacientes.SelectedRow.Cells[2].Text;
                stTipo.Attributes["class"] = "displayOn";
                lblTipo.Attributes["class"] = "displayOn";
            }
            catch (Exception x)
            {
                lblMsg.Text = x.Message;
            }


            iNome.Value = nome;
            divGvPacientes.Attributes["class"] = "displayOff";
            btnBuscar.Visible = false;
            btnCadastrarAgenda.Visible = true;
        }
        protected void selecionarRegistroGvAvaliacao(object sender, EventArgs e)
        {
            
            string nome = "";

            try
            {
                nome = gvPacientesAvaliacao.SelectedRow.Cells[2].Text;
                stAvaliacao.Attributes["class"] = "displayOn";
                lblTipoAvalia.Attributes["class"] = "displayOn";

            }
            catch (Exception x)
            {
                lblMsg.Text = x.Message;
            }

            INomeAvalia.Value = nome;
            divGvAvaliacao.Attributes["class"] = "displayOff";
            btnBuscarAvaliacao.Visible = false;
            btnCadastrarAvaliacao.Visible = true;
        }

        protected void procurarParaCadastro(object sender, EventArgs e)
        {
            string nome = iNome.Value;
            string tipo = "agenda";
            
            procurarPaciente(nome, tipo);

        }

        protected void procurarParaAvaliacao(object sender, EventArgs e)
        {
            string nome = INomeAvalia.Value;
            string tipo = "avaliacao";
            procurarPaciente(nome, tipo);
        }

        protected void carregarGridView(string tipo)
        {
            BLL ObjCliente = new BLL(conectVegas);
            
            if (tipo == "agenda")
            {
                divGvPacientes.Attributes["class"] = "displayOn";

                //gvPacientes.DataSource = ObjCliente.RetQuery(); //Carregar todos os gridViewsa existentes - Por parametro!! Como????
                //gvPacientes.DataBind();
            }
            if (tipo == "avaliacao")
            {
                divGvAvaliacao.Attributes["class"] = "displayOn";

                gvPacientesAvaliacao.DataSource = ObjCliente.RetQuery(); //Carregar todos os gridViewsa existentes - Por parametro!! Como????
                gvPacientesAvaliacao.DataBind();
            }


        }
        protected void procurarPaciente(string paciente, string tipo)
        {                        
            BLL ObjCliente = new BLL(conectVegas);

            //string paciente = iNome.Value;

            string query = " SELECT idassoc, titular, cnpj_cpf AS cpf FROM associa WHERE titular LIKE '%" + paciente.Replace(" ", "%") + "%'";            

            ObjCliente.Query = query;

            DataTable dados = ObjCliente.RetQuery();

            string xRet = "";

            //lblMsg.Text = dados.Rows[0]["titular"].ToString() + " ** " + dados.Rows.Count.ToString();
            if (!String.IsNullOrEmpty(paciente))
            {
                try
                {

                    if (String.IsNullOrEmpty(ObjCliente.MsgErro))
                    {
                        if (dados.Rows.Count > 0)
                        {
                            if (tipo == "agenda")
                            {
                                divGvPacientes.Attributes["class"] = "displayOn";
                                gvPacientes.DataSource = ObjCliente.RetQuery(); //Carregar todos os gridViewsa existentes - Por parametro!! Como????
                                gvPacientes.DataBind();
                            }
                            if (tipo == "avaliacao")
                            {
                                divGvAvaliacao.Attributes["class"] = "displayOn";

                                gvPacientesAvaliacao.DataSource = ObjCliente.RetQuery(); //Carregar todos os gridViewsa existentes - Por parametro!! Como????
                                gvPacientesAvaliacao.DataBind();
                            }
                            //Criar uma View para exibir o GridView e utilizar a mesma GridView para todos os processos - 21-02-2024
                        }
                        else
                        {
                            lblMsg.Text = "Paciente não encontrado";
                            divDados.Attributes["class"] = "displayOn";
                            btnBuscar.Visible = false;
                            btnCadastrar.Visible = true;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Erro: " + ObjCliente.MsgErro;
                    }

                }
                catch (Exception x)
                {
                    lblMsg.Text = x.Message;
                }

            }
            else
            {
                lblMsg.Text = "É preciso preencher o campo nome para continuar...";
            }

            //lblMsg.Text = paciente + " - " + tipo;

        }//procurarPaciente

        protected void cadastrarPaciente(object sender, EventArgs e)
        {
            lblMsg.Text = "Paciente Cadastrado!!!";
            btnCadastrar.Visible = false;
            btnCadastrarAgenda.Visible = true;
            divDados.Attributes["class"] = "displayOff";
        }
        protected void cadastrarAgenda(object sender, EventArgs e)
        {
            lblMsg.Text = "Atendimento Cadastrado";
            btnCadastrarAgenda.Visible = false;
            btnBuscar.Visible = true;
        }

    }//Fim: public partial class 
}//nameSpace