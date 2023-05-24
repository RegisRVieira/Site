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

namespace Site.Eventos
{
    public partial class Brindes : System.Web.UI.Page
    {

        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ativarBoasVindas();
                mostrarLogado();
                checarSessao();
                listarEventos();
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

        protected void ativarBoasVindas()
        {
            mwConteudo.ActiveViewIndex = 0;
        }
        protected void ativarCadastro(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 1;
            lblListaRelaorios.Text = String.Empty;
        }
        protected void ativarRelatorios(object sender, EventArgs e)
        {
            mwConteudo.ActiveViewIndex = 2;
            lblListaRelaorios.Text = String.Empty;
        }
        public string Cod_Tipo { get; set; }
        public string Cod_Status { get; set; }
        protected void ativaOpcaoDaLista(object sender, EventArgs e)
        {
            mwLista.ActiveViewIndex = 0;

            BLL ObjDados = new BLL(conectSite);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " e_evento ";

            DataTable dados = ObjDados.RetCampos();

            Cod_Tipo = dados.Rows[0]["cod_tipo"].ToString();
            Cod_Status = dados.Rows[0]["cod_status"].ToString();

        }

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
        }

        protected void listarEventos()
        {
            BLL ObjDados = new BLL(conectSite);

            string xRet = "";

            //ObjDados.Campo = " e.id, t.cod AS Cod_Tipo, s.cod AS Cod_Status, e.descricao AS Descricao, e.detalhe AS Detalhe, e.dtInicio AS Inicio, e.dtfim AS Fim, t.descricao AS Tipo, s.descricao AS Status  ";
            ObjDados.Campo = " e.id, t.cod AS Cod_Tipo, s.cod AS Cod_Status, e.descricao AS Descricao, e.detalhe AS Detalhe, t.descricao AS Tipo, s.descricao AS Status  ";
            ObjDados.Tabela = " e_evento AS e ";
            ObjDados.Left = " INNER JOIN e_tipo AS t ON t.cod = e.cod_tipo" +
                            " INNER JOIN e_status AS s ON s.cod = e.cod_status";
            ObjDados.Condicao = " WHERE e.cod_tipo = 'BRIN' ";

            DataTable dados = ObjDados.RetCampos();

            Session["Cod_Tipo"] = dados.Rows[0]["cod_tipo"].ToString();
            Session["Cod_Status"] = dados.Rows[0]["cod_status"].ToString();
            Session["Id_Evento"] = dados.Rows[0]["id"].ToString();

            xRet += "<div>";
            xRet += "<label>" + "Lista:" + "</label>";
            xRet += "<p>" + dados.Rows[0]["Descricao"].ToString();
            xRet += "Cod_Tipo: " + dados.Rows[0]["cod_tipo"].ToString() + " - Cod_Status: " + dados.Rows[0]["cod_status"].ToString() + " - ID:" + Session["Id_Evento"] + "</p>";
            xRet += "</div>";



            lblListaEventos.Text = xRet;

        }

        protected void selecionarRegistroGvAssociados(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " e_pessoas ";
            string condicao = " WHERE nome = '" + gvAssociados.SelectedRow.Cells[2].Text + "' ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            btnGravar.Visible = dados.Rows.Count == 0;
            btnAlterar.Visible = dados.Rows.Count != 0;


            /*
             * Isso
             * 
            btnGravar.Visible = dados.Rows.Count == 0;
            btnAlterar.Visible = dados.Rows.Count != 0;
            
            É igual a Isso:

            if (dados.Rows.Count == 0)
            {
                btnGravar.Visible = true;
            }
            else
            {
                btnAlterar.Visible = true;
            }
            
            */
            secCadGrid.Attributes["class"] = "escondeCadCampos";

            lblIdAssoc.Text = gvAssociados.SelectedRow.Cells[1].Text;
            iNome.Value = gvAssociados.SelectedRow.Cells[2].Text;
            iStatus.Value = gvAssociados.SelectedRow.Cells[3].Text;
            iTempoASU.Value = gvAssociados.SelectedRow.Cells[4].Text;
            iUnidade.Value = gvAssociados.SelectedRow.Cells[5].Text;

            cadCampos.Attributes["class"] = "exibeCadCampos";


            lblResult.Text = String.Empty;
        }


        protected void procurarAssoc(object sender, EventArgs e)
        {
            BLL ObjVegas = new BLL(conectVegas);
            BLL ObjSite = new BLL(conectSite);

            secCadGrid.Attributes["class"] = "exibeCadCampos";

            string textBusca = "";
            //   = iBusca.Value.Replace("", "%");
            textBusca = iBusca.Value.Replace(" ", "%");

            ObjVegas.Campo = " a.idassoc as ID, a.titular AS Associado, IF(a.bloqueio IS NULL,'Liberado','Procurar Tesouraria') AS Status, (YEAR(CURDATE()) - YEAR(a.DATA)) AS Tempo, u.descricao AS Unidade ";
            ObjVegas.Tabela = " associa AS a ";
            ObjVegas.Left = " INNER JOIN base_assocuni AS u ON a.trab_unida =  u.unidade ";
            ObjVegas.Condicao = "WHERE a.titular LIKE '%" + textBusca + "%' AND a.cnscanmom IS NULL";

            DataTable dados = ObjVegas.RetCampos();
            string xRet = "";

            //# # # # # # Carregar Grid View # # # # # # 

            if (!String.IsNullOrEmpty(lblResult.Text))
            {
                lblResult.Text = String.Empty;
            }

            if (String.IsNullOrEmpty(iBusca.Value))
            {
                lblResult.Text += "É preciso preencher o Campo Busca...";
            }
            else
            {
                gvAssociados.DataSource = dados;
                gvAssociados.DataBind();
            }

            xRet += "<div>";
            if (String.IsNullOrEmpty(iBusca.Value))
            {
                xRet += "É preciso preencher o Campo Busca...";
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
                xRet += "Não encontrou dados para retorno, digite um nome válido!";
            }
            xRet += "</div>";

            //lblResult.Text = xRet;
        }

        protected void entregarBrinde(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);
            BLL ObjValida = new BLL(conectSite);


            //# # # Validação # # #


            string tabTipo = " e_pessoas ";
            string campTipo = " * ";
            string condTipo = "";

            ObjDados.Tabela = tabTipo;
            ObjDados.Campo = campTipo;
            ObjDados.Condicao = condTipo;

            ObjValida.Tabela = tabTipo;
            ObjValida.Campo = campTipo;

            DataTable dados = ObjValida.RetCampos();

            // - - - - - - - - - - - - - - - - - - - -
            string xRet = "";

            if (Session["LoginEventos"] != null)
            {

                string tabela = " e_pessoas ";
                string campos = " id_evento, idassoc, nome, cod_tipo, cod_status, STATUS, unidade, tempofiliacao, observacao, cadmom, cadusu ";
#pragma warning disable CS0219 // A variável "campoId" é atribuída, mas seu valor nunca é usado
                string campoId = " * ";
#pragma warning restore CS0219 // A variável "campoId" é atribuída, mas seu valor nunca é usado
                string valores = String.Format("'" + Session["Id_Evento"] + "'," +
                                                  "'" + lblIdAssoc.Text + "'," +
                                                  "'" + iNome.Value + "'," +
                                                  "'" + "TIT" + "'," +
                                                  "'" + "LIBE" + "'," +
                                                  "'" + iStatus.Value + "'," +
                                                  "'" + iUnidade.Value + "'," +
                                                  "'" + iTempoASU.Value + "'," +
                                                  "'" + iObs.Value + "'," +
                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                  "'" + Session["LoginEventos"].ToString() + "'");



                int cont = 0;
                string nomeCadastrado = "";



                for (int i = 0; dados.Rows.Count > i; i++)
                {
                    if (iNome.Value == dados.Rows[i]["nome"].ToString() && !String.IsNullOrEmpty(dados.Rows[i]["canmom"].ToString()))
                    {
                        xRet += "Está excluído!";
                    }
                    else
                    {
                        if (iNome.Value == dados.Rows[i]["nome"].ToString())
                        {
                            cont++;
                            nomeCadastrado = dados.Rows[i]["nome"].ToString();
                        }
                    }
                }

                if (cont < 1)
                {
                    if (iStatus.Value == "Liberado")
                    {
                        xRet += "<div>";
                        xRet += "<span>" + "Associado: " + iNome.Value + "</span>" + "<br>";
                        //xRet += "<span>" + "Status: " + iStatus.Value + "</span>" + "<br>";
                        //xRet += "<span>" + "Tempo de Filiação: " + iTempoASU.Value + " anos" + "</span>" + "<br>";
                        xRet += "<span>" + "Unidade: " + iUnidade.Value + "</span>" + "<br>";
                        xRet += "<span>" + "Obs.: " + iObs.Value + "</span>" + "<br>";
                        //xRet += "<span>" + "Usuário: " + Session["LoginEventos"] + "</span>" + "<br>";
                        xRet += "<span>" + "Cadastrado com Sucesso!!!" + "<span>" + "<br>";
                        xRet += "<div>";

                        ObjDados.InsertRegistro(tabela, campos, valores);
                    }
                    else
                    {
                        xRet += "" + "Não pode participar, procure a Tesouraria!!!" + "";
                    }
                }
                else
                {
                    xRet += "" + nomeCadastrado + ", Já está cadastrado neste Evento! " + "";
                }
            }
            else
            {
                Response.Redirect("eLogin.aspx");
            }
            //xRet += "Deu: " + cont;

            //Aplica Classe CSS para esconder section com Dados de preenchimento

            cadCampos.Attributes["class"] = "escondeCadCampos";

            lblResult.Text = xRet;
            iBusca.Value = String.Empty;
            iObs.Value = String.Empty;
            iBusca.Focus();
        }
        protected void excluirPessoa(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

#pragma warning disable CS0219 // A variável "valores" é atribuída, mas seu valor nunca é usado
            string valores = " cnmom, canusu";
#pragma warning restore CS0219 // A variável "valores" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "tabela" é atribuída, mas seu valor nunca é usado
            string tabela = " e_pessoa ";
#pragma warning restore CS0219 // A variável "tabela" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "condicao" é atribuída, mas seu valor nunca é usado
            string condicao = "";
#pragma warning restore CS0219 // A variável "condicao" é atribuída, mas seu valor nunca é usado

            //ObjDados.EditRegistro(tabela, valores, condicao);

            lblResult.Text = "Já está Cadastrado! Cadastre alguém que esteja liberado!";

            iBusca.Focus();
        }

        protected void encerrarLogin(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("eLogin.aspx");
        }

        protected void checarSessao()
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


        }


        protected void relParticipantes(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " e_pessoas";
            string condicao = " ORDER BY nome ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            string xRet = " ";

            xRet += "<div>";
            xRet += "<p>" + "Relatório de Entrega: A-Z" + "</p> <br>";
            for (int i = 0; dados.Rows.Count > i; i++)
            {
                xRet += "<p>" + (i + 1) + " - " + dados.Rows[i]["nome"].ToString() + ", " + dados.Rows[i]["unidade"].ToString() + " - " + dados.Rows[i]["cadmom"].ToString() + " - " + dados.Rows[i]["cadusu"].ToString() + "</p>";
            }
            xRet += "</div>";

            lblListaRelaorios.Text = xRet;

        }

        protected void relPorDia(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            //ObjDados.Campo = " COUNT(id) Qtde, DAY(cadmom) Dia,MONTH(cadmom) Mes,YEAR(cadmom) Ano ";
            ObjDados.Campo = " COUNT(id) Qtde, DAY(cadmom) Dia,MONTH(cadmom) Mes,YEAR(cadmom) Ano, (SELECT COUNT(id)  Dap FROM e_pessoas WHERE cadusu IN ('Mirela', 'Edneia')) AS DAP, (SELECT COUNT(id)  Dap FROM e_pessoas WHERE cadusu NOT IN ('Mirela', 'Edneia')) AS Sede ";
            ObjDados.Tabela = " e_pessoas ";
            ObjDados.Condicao = " GROUP BY dia ORDER BY dia ASC ";

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            xRet += "<div>";
            xRet += "<p>" + "Entregas por dia" + "</p>";
            if (ObjDados.MsgErro == "")
            {
                if (dados.Rows.Count > 0)
                {
                    for (int i = 0; dados.Rows.Count > i; i++)
                    {
                        xRet += "<p>" + dados.Rows[i]["Dia"].ToString() + "/" + dados.Rows[i]["Mes"].ToString() + " : " + dados.Rows[i]["Qtde"].ToString() + "</p>";
                    }
                }
                else
                {
                    xRet += "Não há dados para gerar o Relatório";
                }
                if (dados.Rows.Count > 0)
                {
                    xRet += "<br><br><p>" + "Total de Entregas" + "</p>";
                    xRet += "<p>" + "Dap: " + dados.Rows[0]["Dap"].ToString() + "</p>";
                    xRet += "<p>" + "Sede: " + dados.Rows[0]["Sede"].ToString() + "</p>";
                }
                else
                {
                    xRet += "Não há dados para gerar o Relatório";
                }
            }
            else
            {
                xRet += ObjDados.MsgErro;
            }
            xRet += "</div>";

            lblListaRelaorios.Text = xRet;

        }

    }
}