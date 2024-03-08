using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Site.VOnLine
{
    public partial class NovoLayout : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        public double widthExtrato { get; set; }
        public string VendaArq_envio { get; set; }
        public int nVezes { get; set; }
        public string cArquivo_mostra { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                ativarPublicidade();
                checarAniversario();
                checarSessao();
                atualizaDdlExtrato();
                identificarTamanhoTela();
                exibirBotoesDivOpcoes();
            }
            this.DataBind();
        }
        //# # Checagens # #
        public String usuarioLogado()
        {
            string usuario = "";

            if (Session["VoceOnline"]!= null)
            {
                usuario = Session["LoginUsuario"].ToString();
            }
            else
            {
                Response.Redirect("NovoLayout.aspx");
            }            

            return usuario;
        }
        public String retornaSaldo()
        {
            Apoio Apoio = new Apoio();

            string idAssoc = Session["IdAssoc"].ToString();
            string saldo = "";

            //Exibe o Saldo apenas se o associado nao estiver Bloqueado
            if (Apoio.bloqueioAssociadoComParametro(idAssoc) == "Liberado")
            {
                if (Apoio.SaldoAssociadoComParametro(idAssoc) > 0)
                {
                    saldo += "<td style='font-size: 34px;'>" + Apoio.SaldoAssociadoComParametro(idAssoc).ToString("C2") + "</td>";
                }
                else
                {
                    saldo += "<td style='font-size: 34px;'>" + "R$ 0.00" + "</td>";
                }
            }
            else
            {
                saldo += "<td style='font-size: 34px;'>" + "R$ 0.00" + "</td>";
            }

            return saldo;
        }
        public void sairVoceOnline(object sender, EventArgs e)
        {
            if (Session["VoceOnline"].ToString() != null)
            {
                Session.Abandon();
                Response.Redirect("../Home.aspx");
            }
            else
            {
                Response.Redirect("LoginNLayout.aspx");
            }
        }
        public void checarSessao()
        {
            if (Session["VoceOnline"]==null)
            {
                Response.Redirect("LoginNLayout.aspx");
            }
        }
        public void atualizaPeriodo(object sender, EventArgs e)
        {            

            //montarExtratoConv();
            //montarExtratoPreConv();
            //Montar Relatório de Entrega
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            if (tCampo == 14 || tCampo < 7)
            {
                if ("" + Session["LoginConvenio"] != "")
                {
                    //montarRelMensal();
                }
            }

        }
        public String identificarFuncionarios(string idFunc)
        {
            BLL ObjDados = new BLL(conectVegas);

            string query = " SELECT idassoc, titular FROM associa" +
                           " WHERE categoria = '4' AND idassoc = '" + idFunc + "'";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            string retorno = "";

            if (dados.Rows.Count > 0)
            {
                retorno = "Funcionario";
            }
            else
                retorno = "Associado";

            return retorno;
        }

        public void exibirBotoesDivOpcoes()
        {
            string idAssoc = "";

            if (Session["Voceonline"] != null)
            {
                idAssoc = Session["IdAssoc"].ToString();
            }            

            if (identificarFuncionarios(idAssoc).ToString() == "Funcionario")
            {
                divOpcoes.Attributes["class"] = "divOpcoesFunc divOpcoes";
            }
            else
            {
                divOpcoes.Attributes["class"] = "divOpcoes";
                divBtnPainelEventos.Attributes["class"] = "oculto";
            }
        }
        
        public String checarAniversario()
        {
            Apoio Apoio = new Apoio();

            string xRet = "";


            string idAssoc = "";

            if (Session["VoceOnline"] != null)
            {
                idAssoc = Session["IdAssoc"].ToString();
            }
            else
            {
                Response.Redirect("LoginNLayout.aspx");
            }


            xRet += Apoio.checarAniversario(idAssoc);


            return xRet;
        }

        //# # FIM - Checagens # #

        //# # Operações MultiView # #
        protected void ativarPublicidade()
        {
            

            mwAssociado.ActiveViewIndex = 0;
        }
        protected void ativarExtrato(object sender, EventArgs e)
        {
            mwAssociado.ActiveViewIndex = 1;
            extratoAssociado();
        }
        public void ativarCartao(object sender, EventArgs e)
        {
            mwAssociado.ActiveViewIndex = 2;
        }
        public void ativarSenhaAssoc(object sender, EventArgs e)
        {
            mwAssociado.ActiveViewIndex = 3;
        }
        public void ativarDadosAssoc(object sender, EventArgs e )
        {
            mwAssociado.ActiveViewIndex = 4;
            carregarDadosAssoc();
            //secFormDados.Attributes["class"] = "mostrar";
            //secFormDados.Attributes["class"] = "secFormDados";    
        }
        //# # FIM - Operações MultiView # #

        //# # Operações com Botões do Associado # #
        public void irParaPainelEventos(object sender, EventArgs e)
        {
            //Redireciona para a Página de Eventos. Criado: 08/08/2023
            Response.Redirect("../Eventos/eLogin.aspx");
        }
        public void irParaGuiaConvenios(object sender, EventArgs e)
        {
            //Redireciona para a Página de Eventos. 
            Response.Redirect("../MontaGuia.aspx");
        }
        public void irParaEventos(object sender, EventArgs e)
        {
            //Redireciona para a Página de Eventos. 
            Response.Redirect("../PaginaEventos.aspx");
        }
        public void irParaNoticias(object sender, EventArgs e)
        {
            //Redireciona para a Página de Eventos. 
            //Response.Redirect("../ContMaterias_Todas.aspx", "_blank");
            Response.Redirect("../ContMaterias_Todas.aspx");
        }

        //# # FIM - Operações com Botões do Associado # #

        //# # Operações com Extrato do Associado # #
        public Double identificarTamanhoTela()
        {

            //# # # # # Fim - saldo e limite # # # # #
            double widht = Screen.PrimaryScreen.Bounds.Width; //Capturar por JavaSvript e armazenar em um label para capturar aqui
                                                              //double tam = System.sys

            //MessageBox.Show(widht.ToString()); 

            widthExtrato = widht;            

            return widthExtrato;
        }
        protected String tamanhoTela()
        {
            string widht = "1001";

            if (Request.Form["hfTamanhoTela"] != null)
            {

                //widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
                widht = Request.Form["hfTamanhoTela"].ToString();                
            }
            else
            {
                widht = "1001";                
            }            

            return widht;
        }
        protected void atualizaDdlExtrato()
        {
            //Checar data e atualizar ddl
            string diaAtual = DateTime.Now.Day.ToString();
            string mesAtual = DateTime.Now.Month.ToString();
            string anoAtual = DateTime.Now.Year.ToString();          

            if (Convert.ToInt32(diaAtual) >= 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //# # # # # Extrato Associado - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMes in ddlMesExtratoAssoc.Items)//Extrato Associado
                {
                    if (lMes.Value == mesAtual)
                    {
                        lMes.Selected = true;
                    }
                }
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #
                /*
                foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMesExtratoConv.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }
                */
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #


                /*foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMes.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }
                */

                //Fatura Mensal
                /*
                foreach (System.Web.UI.WebControls.ListItem lMesFatMensal in ddlFatMensalMes.Items)
                {
                    if (lMesFatMensal.Value == mesAtual)
                    {
                        lMesFatMensal.Selected = true;
                    }
                }
                */
            }
            else
            {
                //# # # # # Extrato Associado - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMes in ddlMesExtratoAssoc.Items)
                {
                    if (lMes.Value == mesAtual)
                    {
                        lMes.Selected = true;
                    }
                }
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #
                /*
                foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMesExtratoConv.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }
                */
                /*
                foreach (System.Web.UI.WebControls.ListItem lFatMensal in ddlFatMensalMes.Items)
                {
                    if (lFatMensal.Value == mesAtual)
                    {
                        lFatMensal.Selected = true;
                    }
                }
                */
            }
            //# # # # # Extrato Associado - Atualiza Ano # # # # #
            foreach (System.Web.UI.WebControls.ListItem lAno in ddlAnoExtratoAssoc.Items)
            {
                if (lAno.Value == anoAtual)
                {
                    lAno.Selected = true;
                }
            }
            //# # # # # Extrato Convênio - Atualiza Ano # # # # #
            /*
            foreach (System.Web.UI.WebControls.ListItem lAnoConv in ddlAnoExtratoConv.Items)
            {
                if (lAnoConv.Value == anoAtual)
                {
                    lAnoConv.Selected = true;
                }
            }
            */

            //# # # # # Relatório de Entrega Convênio - Atualiza Ano # # # # #
            /*
            foreach (System.Web.UI.WebControls.ListItem lAnoConv in ddlAno.Items)
            {
                if (lAnoConv.Value == anoAtual)
                {
                    lAnoConv.Selected = true;
                }
            }
            */
            
            // Fatura Mensal
            /*
            foreach (System.Web.UI.WebControls.ListItem lAnoFatMensal in ddlFatMensalAno.Items)
            {
                if (lAnoFatMensal.Value == anoAtual)
                {
                    lAnoFatMensal.Selected = true;
                }
            }
            */

        }
        public String extratoAssociado()
        {
            checarSessao();

            BLL ObjDbASU = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            //MessageBox.Show(SaldoAssociado());

            string xRet = "";
            string xPdf = "";

            //MessageBox.Show("Query: " + Apoio.SaldoAssociadoString());

            Apoio.Ano = ddlAnoExtratoAssoc.SelectedValue;
            Apoio.Mes = ddlMesExtratoAssoc.SelectedValue;

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string IdAssoc = "";

            string inicio = Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2));


            if (tCampo == 9 || tCampo == 11)
            {
                IdAssoc = Session["IdAssoc"].ToString();
            }          

            DateTime agora = DateTime.Now;
            DateTime mesInicio = DateTime.Now.AddMonths(-1);
            DateTime mestFim = DateTime.Now;
            if (ObjDbASU.MsgErro == "")
            {
                if (tCampo == 9 || tCampo == 11)
                {
                    string query = " SELECT a.credmodelo, deb.iddebito AS idmovime, EXTRACT(DAY FROM deb.dtvencim) AS dia, deb.convenio, deb.descricao AS conveniado, deb.associado, a.titular, 'ASU' AS cartao, deb.dependen, d.nome AS comprador, deb.valor, deb.dtvencim AS vencimento, deb.dtlimite AS DATA, deb.parcela, deb.parctot, deb.cnscadmom, a.credito, '0.00' AS gastos, " +
                                   " (SELECT SUM(valor) FROM assocdeb AS deb INNER JOIN associa AS a ON a.idassoc = deb.associado INNER JOIN asdepen AS d ON deb.dependen = d.iddepen WHERE deb.associado='" + IdAssoc + "' AND deb.tipo NOT IN ('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB' ) AND deb.dtvencim BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND deb.cnscanmom IS NULL LIMIT 1) AS debitos " +
                                   " FROM assocdeb AS deb " +
                                   " LEFT JOIN coconven AS co ON co.idconven = deb.convenio " +
                                   " LEFT JOIN associa AS a ON deb.associado = a.idassoc " +
                                   " LEFT JOIN asdepen AS d ON deb.dependen = d.iddepen " +
                                   " WHERE deb.associado = '" + IdAssoc + "' AND deb.tipo NOT IN('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB') " +
                                   " AND deb.dtvencim BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND deb.cnscanmom IS NULL " +
                                   " UNION " +
                                   " SELECT a.credmodelo, c.idmovime AS id, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito, " +
                                   " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '2022-10-20' AND '2022-11-19' AND c.cnscanmom IS NULL LIMIT 1) AS gastos, '0.00' AS debitos   " +
                                   " FROM comovime AS c" +
                                   " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                   " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                   " INNER JOIN asdepen AS d ON c.dependen = d.iddepen " +
                                   " WHERE a.idassoc = '" + IdAssoc + "' AND c.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL ORDER BY dia ";

                    ObjDbASU.Query = query;

                    DataTable dados = ObjDbASU.RetQuery();

                    Apoio.IdAssoc = IdAssoc; //Atribui Id do associado ao Método Apoio.IdAssoc, para calcular os gastos

                    string mesAtual = DateTime.Now.Month.ToString();
                    string diaAtual = DateTime.Now.Day.ToString();

                    string style = "<style> " +
                        ".defaultTable {" +
                        "                    }" +
                        " .defaultTable table {" +
                        "                    }    " +
                        ".defaultTable caption {" +
                        " background-color: #22396f;" +
                        " margin: 0;" +
                        " margin-top: 30px;" +
                        " padding: 10px 0 2px; " +
                        " font-size: 1.8em;" +
                        " color: white;" +
                        "}" +
                        " .defaultTable tr, td, th {" +
                        "/*border: thin solid #ccc;*/" +
                        " min-width: 15px;" +
                        " height: auto; " +
                        " border-bottom: none; " +
                        " border-right: none; " +
                        " padding: 5px 18px 2px 18px; " +
                        " font-size: .82em;" +
                        " /*font-size: 11px;*/" +
                        " font-weight: 300;" +
                        //" background-color: red; " +
                        " }" +
                        "   @media(max - width: 1000px) {" +
                        " table{ " +
                        " width: 120 %; " +
                        "} " +
                        " tr, td, th { " +
                        " font-size: 1.3em; " +
                        "} " +
                        "} " +
                        " .defaultTable td { " +
                        " text-align: center;" +
                        " } .defaultTable th " +
                        " { text-align: left; " +
                        "}" +
                        " .defaultTable thead th {" +
                        " text-align: center;" +
                        " background-color: #f26907;" +
                        "}" +
                        ".defaultTable thead td {" +
                        " background-color: #7693d5;" +
                        "} " +
                        " .defaultTable thead tr th:first-child {" +
                        " /*text-align: center;*/" +
                        " }" +
                        " .defaultTable tfoot td {" +
                        " font-size: 1.8em;" +
                        " color: white;" +
                        " background-color: #7693d5;" +
                        " padding: 5px 0;" +
                        " }" +
                        " .right {" +
                        " text-align: right;" +
                        " padding-right: 15px;" +
                        " }" +
                        " .left {" +
                        " text-align: left;" +
                        "}" +
                        " .defaultTable tbody tr: nth-child(odd) { /*Altera a cor da linha*/" +
                        " background-color: #ebf0f9;" +
                        " background-color: #d8e1f3;" +
                        "</style> ";

                    //# # # Mostra Limite e Saldo # # #
                    xRet += style;
                   
                    //xRet += Apoio.checarAniversario(IdAssoc);                    

                    //# # # # # Fim - saldo e limite # # # # #

                    if (Convert.ToInt32(tamanhoTela()) > 1000)
                    {
                        //MessageBox.Show("Extrato Fluído");

                        if (dados.Rows.Count > 0)
                        {                            
                            xRet += "<div class='extAssocNormal' style='width: 1000px; '>";//StyleVoceOnLine.css
                            //Cria Extrato
                            xRet += "<section Class='defaultTable'>";
                            xRet += "<table>";                            
                            xRet += "<caption>" + " Movimentação Financeira " + "</caption>";                            
                            xRet += "<thead>";//Cabeçalho da Tabela
                            xRet += "<tr>"; //Linha                                                
                            xRet += "<td colspan='7' class='right'  style='text-align: left'>" + Session["LoginUsuario"].ToString() + "</td>";
                            xRet += "</tr>"; //Linha                                                
                            xRet += "<tr>"; //Linha                                                
                            if (dados.Rows[0]["credmodelo"].ToString() == "VLPARCE")
                            {                                
                                xRet += "<td colspan='7' class='right' style='text-align: left'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                            }
                            else
                            {
                                xRet += "<td colspan='7' class='right' style='text-align: left'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                            }
                            xRet += "</tr>";
                            xRet += "<tr>"; //Linha
                            xRet += "<td>" + "Autorização" + "</td>";//Campo
                            xRet += "<td>" + "Momento" + "</td>";
                            xRet += "<td>" + "Convênio" + "</td>";
                            xRet += "<td>" + "Cartão" + "</td>";
                            xRet += "<td>" + "Comprador" + "</td>";
                            xRet += "<td>" + "Parcela(s)" + "</td>";
                            xRet += "<td>" + "Valor" + "</td>";
                            xRet += "</tr>";
                            xRet += "</thead>";
                            xRet += "<tfoot>";//Rodape da Tabela
                            xRet += "<tr>"; //Linha                            
                            xRet += "<td colspan='4' style='text-align: right; padding-right: 15px;'>" + "Total: " + "</td>";                            
                            xRet += "<td colspan='3'>" + (Apoio.GastosAssociado() + Apoio.DebitosAssociado()).ToString("C2") + " " + "</td>";
                            xRet += "</tr>";

                            xRet += "</tfoot>";
                            xRet += "<tbody>";//Cabeçalho da Tabela
                            for (int i = 0; i < dados.Rows.Count; i++)
                            {
                                double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                                xRet += "<tr>";
                                xRet += "<td>" + dados.Rows[i]["idmovime"] + "</td>";//Autorização
                                xRet += "<td>" + dados.Rows[i]["cnscadmom"] + "</td>";//Momento
                                xRet += "<td>" + dados.Rows[i]["conveniado"] + "</td>";//Convênio
                                if (dados.Rows[i]["cartao"].ToString() == "ASU")
                                {
                                    xRet += "<td>" + dados.Rows[i]["cartao"] + "</td>";//Cartão
                                }
                                else
                                {
                                    xRet += "<td>" + dados.Rows[i]["cartao"] + " XX " + "</td>";//Cartão
                                }
                                xRet += "<td>" + dados.Rows[i]["comprador"] + "</td>";//Comprador
                                xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                                xRet += "<td>" + valorCompra.ToString("C2") + "</td>";//Valor
                                xRet += "</tr>";
                            }
                            xRet += "</tbody>";
                            xRet += "</table>";
                            xRet += "</section>";

                            //xRet += "teste";
                            xRet += "</div>";
                        }
                        else
                        {
                            xRet += "<div>";
                            xRet += "<p>" + " Seu cartão não gerou débitos para este período " + "</p>";
                            xRet += "</div>";
                        }
                    }
                    else
                    {
                        //# # # # # Extrato Fluído # # # # #

                        xRet += "<div class='extAssocFluido'>";//StyleVoceOnLine.css
                        xRet += "<table>";                        
                        xRet += "<tbody>";
                        xRet += "<tr>";
                        xRet += "<td>" + "Limite do seu Cartão " + "</td>";
                        xRet += "<td>" + "Saldo disponível hoje " + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>";                        
                        xRet += "</tr>";
                        xRet += "<tr>";//Insere Uma linha para Espaçamento e utilizar cor
                        xRet += "<td colspan='2'></td>";
                        xRet += "</tr>";
                        xRet += "</tbody>";
                        xRet += "</table>";
                        //Cria Extrato
                        xRet += "<table>";                        
                        xRet += "<caption>" + " Movimentação Financeira " + "</caption>";
                        xRet += "<thead>";//Cabeçalho da Tabela
                        xRet += "<tr>";
                        xRet += "</tr>";
                        xRet += "<tr>"; //Linha
                        xRet += "<td colspan='4' class='right' style='text-align: left'>" + Session["LoginUsuario"].ToString() + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>"; //Linha
                        xRet += "<td colspan='4' class='right' style='text-align: left'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>"; //Linha
                        xRet += "<td>" + "dia" + "</td>";                        
                        xRet += "<td colspan='4' style'text-align:left;'>" + "" + "</td>";
                        xRet += "</tr>";
                        xRet += "</thead>";
                        xRet += "<tfoot>";//Rodape da Tabela
                        xRet += "<tr>"; //Linha
                        xRet += "<td colspan='3' class='right'>" + "Total: " + "</td>";                        
                        xRet += "<td colspan='2'>" + (Apoio.GastosAssociado() + Apoio.DebitosAssociado()).ToString("C2") + " " + "</td>";
                        xRet += "</tr>";
                        xRet += "</tfoot>";
                        xRet += "<tbody>";//Cabeçalho da Tabela
                        for (int i = 0; i < dados.Rows.Count; i++)
                        {
                            double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                            xRet += "<tr>";
                            xRet += "<td>" + dados.Rows[i]["dia"] + "</td>";//Dia
                            xRet += "<td colspan='2' style='text-align: left;'>" + dados.Rows[i]["conveniado"] + "<br>" + "<span style='font-size: .7em; margin-left: 20px;'>" + dados.Rows[i]["comprador"] + "</span>" + "</td>";//Convênio                    
                            xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                            xRet += "<td>" + valorCompra.ToString("C2") + "</td>";//Valor
                            xRet += "</tr>";
                        }
                        xRet += "</tbody>";
                        xRet += "</table>";
                        xRet += "</div>";
                    }
                    
                }
            }
            else
            {
                lblMsg.Text = "Erro Original: " + ObjDbASU.MsgErro;
            }

            return xRet;
        }
        //# # FIM - Operações com Extrato do Associado # #

        //# # Operações com Cartão do Associado # #
        public String metodoCartoesAssoc()
        {
            checarSessao();

            lblMsg.Text = String.Empty;

            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";

            string codAcesso = Session["CodAcesso"].ToString();

            int tCampo = codAcesso.Length;
            string idAssoc = "";

            if (tCampo == 9 || tCampo == 11)
            {
                idAssoc = Session["idAssoc"].ToString();
            }
            string campos = " a.idassoc, d.iddepen, g.descricao AS grau, d.associado, car.idcartao, car.dependen, car.emissao, car.dt_inicio, car.dt_fim, car.validade, car.credito, a.titular, d.nome AS dependente ";
            string tabela = " asdepen AS d ";
            string left = " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                                    " INNER JOIN associa AS a ON a.idassoc = d.associado " +
                                    " INNER JOIN asdepcar AS car ON car.dependen = iddepen ";
            string condicao = " WHERE a.cnscanmom IS NULL " +
                                        " AND d.cnscanmom IS NULL " +
                                        " AND car.cnscanmom IS NULL " +
                                        " AND a.idassoc = '" + idAssoc + "' " +
                                        " AND((CURDATE() BETWEEN car.dt_inicio AND car.validade) OR(car.dt_fim IS NULL)) ";

            if (ObjDbASU.MsgErro == "")
            {
                ObjDbASU.Campo = campos;
                ObjDbASU.Tabela = tabela;
                ObjDbASU.Left = left;
                ObjDbASU.Condicao = condicao;

                DataTable dados;
                dados = ObjDbASU.RetCampos();

                int nLinhas = dados.Rows.Count;

                for (int i = 0; i < nLinhas; i++)
                {
                    string validade = dados.Rows[i]["validade"].ToString();
                    string dv = dados.Rows[i]["idcartao"].ToString();


                    xRet += "<div class='divCartao'>";                    
                    //xRet += "<div style='background-color: yellow; width: 302px; height: 195px;'>";
                    xRet += "<script>" +
                        "function clicou() {" +
                        "alert('Clicou no Cartão');" +
                        "} </script>";

                    xRet += "<div class='divCartaoImg' onclick='clicou()'>";
                    xRet += "<img src='../img/CartaoASUOnLine.jpg' />";
                    //xRet += "<img src='img/CartaoASUOnLine.jpg' style='width: 300px' onclick='recuperaDadosAssocSenha' />";                    
                    xRet += "</div>";
                    xRet += "<div class='divDadosCartao'>";
                    xRet += "<p class='divCartaoNome'>" + dados.Rows[i]["dependente"] + "</p>";
                    xRet += "<p class='divCartaoNumero'>" + dados.Rows[i]["idcartao"] + "xx" + "</p>";
                    //xRet += "<p style='margin: 0; margin-left: 28px; text-align: left; '>" + GeraDigMod11(Convert.ToInt64(dv)) + "</p>";

                    xRet += "<p class='divCartaoValidade'>" + " Validade: " + DateTime.Parse(validade).ToString("dd-MM-yyyy") + "</p>";
                    xRet += "</div>" + "<br>";

                    xRet += "</div>";

                }

                xRet += "<br><br><br><br>";
            }
            else
            {
                xRet += "Erro: " + ObjDbASU.MsgErro;
            }

            return xRet;
        }
        //# # Operações com Senha do Associado # #
        public void trocarSenhaAssoc(object sender, EventArgs e)
        {

            BLL ObjDados = new BLL(conectVegas);
            BLL ObjUpDate = new BLL(conectVegas);
            BLL ObjLog = new BLL(conectVegas);

            string idAssoc = Session["idAssoc"].ToString();

            //Acessa dados Associado para comparar Senha
            //c = consulta
            string c_tabela = " associa ";
            string c_campos = " senha ";
            string c_condicao = " WHERE idassoc = '" + idAssoc + "' ";

            ObjDados.Campo = c_campos;
            ObjDados.Tabela = c_tabela;
            ObjDados.Condicao = c_condicao;

            DataTable dados = ObjDados.RetCampos();

            //l = Log
            string l_tabela = " ASSOCLOG ";
            string l_valores = " associado =  '" + idAssoc + "' , " + "dependen = '" + idAssoc + "', " + "descriacao = 'Alteração de Senha ADM', " + "detalhe = 'Senha Alterada através do Site: Antiga: " + dados.Rows[0]["senha"].ToString() + ", Nova: " + iSenhaConfirma.Value + "', " + "cnscadusu = 'SITE', " + "cnscadmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            //SET associado = "2747", dependen = "2747", descriacao = "Alteração de Senha ADM", detalhe = "Senha Alterada através do Site: Antiga: xxx, Nova: yyy", cnscadusu = "SITE", cnscadmom = "Agora"
            string l_condicao = " WHERE idassoc = '" + idAssoc + "' ";

            ObjLog.Tabela = l_tabela;
            ObjLog.Valores = l_valores;
            ObjLog.Condicao = l_condicao;

            string upDate = " UPDATE " + l_tabela + " SET " + l_valores + " " + l_condicao;

            //MessageBox.Show(upDate);

            //ObjUpDate.EditRegistro(l_tabela, l_valores, l_condicao);

            if (ObjDados.MsgErro == "")
            {
                if (iSenhaAtual.Value == dados.Rows[0]["senha"].ToString())
                {
                    if (iSenhaNova.Value != iSenhaConfirma.Value)
                    {
                        lblMsg.Text = "As senhas não conferem, digite-as novamente!!!";
                        iSenhaAtual.Focus();
                    }
                    else
                    {
                        //UpDate: Altera Campo Senha
                        string tabela = " associa ";
                        string valores = " senha = '" + iSenhaConfirma.Value + "' ";
                        string condicao = " idassoc = '" + idAssoc + "' ";

                        ObjUpDate.Valores = valores;
                        ObjUpDate.Tabela = tabela;
                        ObjUpDate.Condicao = condicao;

                        ObjDados.EditRegistro(tabela, valores, condicao);
                        //trocaSenha = codTransa + versao + codConv + codCart + valor + parcela + codRet + autoriza + chaveUsuar + nome + observa + cpfCnpj + saldo + senha;                        
                        //lblTrocaSenha.Text = trocaSenha;
                        lblMsg.Text = "Sua senha foi alterada com Sucesso!!!";
                    }
                }
                else
                {
                    lblMsg.Text = "Senha: " + "Não confere com a senha do sistema. Tente novamente!!!";
                    iSenhaAtual.Focus();
                }

            }
            else
            {
                lblMsg.Text = ObjDados.MsgErro.ToString();
            }

        }//trocarSenhaAssoc
        //# # Operações com Dados do Associado # #
        public void carregarDadosAssoc()
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;

            if (ObjDbASU.MsgErro == "")
            {

                if (tCampo == 9 || tCampo == 11)
                {
                    string campos = " d.iddepen, a.senha, a.idassoc, a.titular AS nome, u.descricao AS unidade, depto.descricao AS Departamento, s.descricao AS Setor, a.trab_funca, b.descricao AS banco, a.sexo, a.cnpj_cpf, a.ie_rg, a.email, a.fone, a.celular, a.cep, a.endereco, a.numero, a.complement, a.bairro, a.cidade, a.estado, a.credito, a.bco_banco, a.bco_agenci, a.bco_tipocc, a.bco_conta, a.bco_digcc ";
                    string tabela = " asdepen AS d ";
                    string left = " INNER JOIN associa AS a ON d.associado = a.idassoc " +
                                  " INNER JOIN base_assocuni AS u ON u.unidade = a.trab_unida " +
                                  " INNER JOIN base_assocdep AS depto ON depto.depto = a.trab_dpto " +
                                  " LEFT JOIN base_assocset AS s ON s.codsetor = a.trab_setor " +
                                  " INNER JOIN base_un_banco AS b ON b.codbanco = a.bco_banco ";
                    string condicao = " WHERE(d.cnpj_cpf = '" + iDAcesso + "' OR(EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, 7) + "'))) AND a.cnscanmom IS NULL ";

                    ObjDbASU.Campo = campos;
                    ObjDbASU.Tabela = tabela;
                    ObjDbASU.Left = left;
                    ObjDbASU.Condicao = condicao;

                    //MessageBox.Show(ObjDbASU.Campo + ObjDbASU.Tabela + ObjDbASU.Left + ObjDbASU.Condicao);

                    DataTable dados = ObjDbASU.RetCampos();

                    iNomeAssoc.Value = dados.Rows[0]["nome"].ToString();
                    iCpfAssoc.Value = dados.Rows[0]["cnpj_cpf"].ToString();
                    iRgAssoc.Value = dados.Rows[0]["ie_rg"].ToString();
                    iEmailAssoc.Value = dados.Rows[0]["email"].ToString();
                    iFoneAssoc.Value = dados.Rows[0]["fone"].ToString();
                    iCelAssoc.Value = dados.Rows[0]["celular"].ToString();
                    iCepAssoc.Value = dados.Rows[0]["cep"].ToString();
                    iRuaAssoc.Value = dados.Rows[0]["endereco"].ToString();
                    iNumCasaAssoc.Value = dados.Rows[0]["numero"].ToString();
                    iBairroAssoc.Value = dados.Rows[0]["complement"].ToString();
                    iComplemAssoc.Value = dados.Rows[0]["bairro"].ToString();
                    iCidadeAssoc.Value = dados.Rows[0]["cidade"].ToString();
                    iEstadoAssoc.Value = dados.Rows[0]["estado"].ToString();
                    iUnidadeAssoc.Value = dados.Rows[0]["unidade"].ToString();
                    iDepartAssoc.Value = dados.Rows[0]["departamento"].ToString();
                    iSetorAssoc.Value = dados.Rows[0]["setor"].ToString();
                    iFuncaoAssoc.Value = dados.Rows[0]["trab_funca"].ToString();
                    iBancoAssoc.Value = dados.Rows[0]["banco"].ToString();
                    iAgenciaAssoc.Value = dados.Rows[0]["bco_agenci"].ToString();
                    iContaAssoc.Value = dados.Rows[0]["bco_tipocc"].ToString() + dados.Rows[0]["bco_conta"].ToString() + "-" + dados.Rows[0]["bco_digcc"].ToString();
                }
            }
        }//carregarDadosAssoc
        public void buscarCepAssoc(object sender, EventArgs e)
        {
            Apoio ObjApoio = new Apoio(); 

            ObjApoio.buscarCep(iCepAssoc.Value);

            string logradouro = ObjApoio.Rua;
            string log = "";

            if (string.IsNullOrEmpty(iCepAssoc.Value))
            {
                iCepAssoc.Focus();
                iCepAssoc.Attributes.Add("placeholder", "Este campo, Precisa ser Preenchido");
            }
            else
            {
                log = logradouro.Split(' ')[0];
                iLogradouroAssoc.Value = log;
                iRuaAssoc.Value = logradouro.Substring((log.Length + 1), ((logradouro.Length - log.Length) - 1));
                iComplemAssoc.Value = ObjApoio.Complemento;
                iBairroAssoc.Value = ObjApoio.cBairro;
                iCidadeAssoc.Value = ObjApoio.cCidade;
                iEstadoAssoc.Value = ObjApoio.UF;
                iNumCasaAssoc.Value = String.Empty;
                iNumCasaAssoc.Focus();
                lblErroAssoc.Text = String.Empty;
            }

        }//buscarCepAssoc
        public void enviarDadosCorrecao(object sender, EventArgs e)
        {
            Apoio Apoio = new Apoio();
            string Dados = "";
            string xRet = "";

            string para = "alteracadastro@asu.com.br";
            string cc = "reginaldo@asu.com.br";
            string assunto = "Dados para Alteração, do(a) Associado(a): " + Session["LoginUsuario"].ToString();
            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            //Dados += "Assunto: " + assunto + "<br />";
            Dados += "Alteração Solicitada em: " + agora + "<br />";
            //Dados += "Enviado para: " + para + " - " + cc + "<br />";            
            Dados += "<section style='width: 70%; margin: 0 auto;'>";
            Dados += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            Dados += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Pessoais" + "</p>";
            Dados += "<p>" + iNomeAssoc.Value + "</p>";
            Dados += "<p>" + "Cpf: " + iCpfAssoc.Value + "</p>";
            Dados += "<p>" + " RG:" + iRgAssoc.Value + "</p>";
            Dados += "<p>" + "E-mail: " + iEmailAssoc.Value + "</p>";
            Dados += "<p>" + " Telefone: " + iFoneAssoc.Value + "</p>";
            Dados += "<p>" + " Celular: " + iCelAssoc.Value + "</p>";
            Dados += "</div>";
            Dados += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            Dados += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados de Endereço" + "</p>";
            Dados += "<p>" + "Cep: " + iCepAssoc.Value + "</p>";
            Dados += "<p>" + " Rua: " + iRuaAssoc.Value + ", " + iNumCasaAssoc.Value + "</p>";
            Dados += "<p>" + "Complemento: " + iComplemAssoc.Value + "</p>";
            Dados += "<p>" + "Bairro: " + iBairroAssoc.Value + " - " + iCidadeAssoc.Value + "/" + iEstadoAssoc.Value + "</p>";
            Dados += "</div>";
            Dados += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            Dados += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados de Trabalho" + "</p>";
            Dados += "<p>" + "Unidade: " + iUnidadeAssoc.Value + "</p>";
            Dados += "<p>" + "Departamento: " + iDepartAssoc.Value + "</p>";
            Dados += "<p>" + "Setor: " + iSetorAssoc.Value + "</p>";
            Dados += "<p>" + "Função: " + iFuncaoAssoc.Value + "</p>";
            Dados += "</div>";
            Dados += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            Dados += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Bancários" + "</p>";
            Dados += "<p>" + "Banco: " + iBancoAssoc.Value + "</p>";
            Dados += "<p>" + "Agência: " + iAgenciaAssoc.Value + "</p>";
            Dados += "<p>" + "Conta Corrente: " + iContaAssoc.Value + "</p>";
            Dados += "</div>";
            Dados += "</section>";

            //lblDados.Text = Dados;

            /*Enviar para E-mail*/

            

            if (!String.IsNullOrEmpty(Dados))
            {
                //lblMsg.Text = Dados;

                xRet += "<div class='BoxMsgConclusao'>";
                xRet += "<div style='margin: 0 auto; width: 100%;' >";
                xRet += "<img style='width: 50px;' src='../Img/layout/img_enviado.png' />";
                xRet += "</div>";
                xRet += "<div style='margin-top: 10px; margin-bottom: 50px;' >";
                xRet += "<p>" + "Seus dados foram enviados para Correção, obrigado!" + "</p>";
                xRet += "</div>";
                xRet += "</div>";                

                secFormDados.Attributes["class"] = "oculto";
                Apoio.EnviarEmail(para,cc,assunto,Dados);
                lblMsg.Text = xRet;

            }
            else
            {
                lblMsg.Text = "É preciso preencher os campos!";
            }





        }//enviarDadosCorrecao
        //# # FIM - Operações com Dados do Associado # #

    }//Fim
}