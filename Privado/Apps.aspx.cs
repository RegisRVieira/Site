using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
#pragma warning disable CS0105 // A diretiva using para "Site.App_Code" apareceu anteriormente neste namespace
using Site.App_Code;
#pragma warning restore CS0105 // A diretiva using para "Site.App_Code" apareceu anteriormente neste namespace

namespace Site.Privado
{
    public partial class Apps : System.Web.UI.Page
    {
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                popularSelectGuia();
            }
            DataBind();
        }

        public void popularSelectGuia()
        {
            BLL ObjDados = new BLL(conectVegas);
            //string campos = " c.idconven, t.descricao, COUNT(c.idconven) AS Qtde  ";
            string campos = " c.tipoconv, t.descricao  ";
            string tabela = " coconven AS c ";
            string left = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condicao = " WHERE c.cnscanmom IS NULL AND tipoconv NOT IN ('045', '046') GROUP BY c.tipoconv ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            stGuia.DataSource = ObjDados.RetCampos();
            stGuia.DataValueField = "tipoconv";
            stGuia.DataTextField = "descricao";
            stGuia.DataBind();
            
            ddlGuia.DataSource = ObjDados.RetCampos();
            ddlGuia.DataValueField = "tipoconv";
            ddlGuia.DataTextField = "descricao";
            ddlGuia.DataBind();

            
            
        }
        public void guiaPorSelect(object sender, EventArgs e) {
            string xRet = "";

            BLL ObjDados = new BLL(conectVegas);

            string tabela = " coconven ";
            string campos = " * ";
            string condicao = " WHERE tipoconv = '" + ddlGuia.SelectedValue + "' AND cnscanmom IS NULL";
            //string condicao = " ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            MessageBox.Show(ddlGuia.SelectedValue);

            DataTable dados = ObjDados.RetCampos();
            try
            {
                if (dados.Rows.Count > 0)
                {
                    xRet += "<div>";                    
                    for (int i = 0; i < dados.Rows.Count; i++)
                    {
                        xRet += "<p>";
                        xRet += dados.Rows[i]["nome"].ToString();
                        xRet += "</p>" + "\n";
                    }                    
                    xRet += "</div>";
                }
                else
                {
                    xRet += "Erro: " + ObjDados.MsgErro;
                }
            }
            catch (Exception x)
            {
                xRet += x.Message;
            }

            lblMsg.Text = xRet;
            
        }

        public string IdAssoc { get; set; }
        public void SaldoAssociado(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);
            BLL ObjValida = new BLL(conectVegas);
            

            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();
            string xRet = "";
            
            IdAssoc = iAssociado.Value;

            //#Valida Modelo de Crédito
            string valida = "";

            valida += " SELECT a.titular, a.credmodelo, a.credito FROM associa a ";            
            valida += " WHERE a.idassoc = '" + IdAssoc + "' AND cnscanmom IS NULL";

            ObjValida.Query = valida;
            DataTable validacao = ObjValida.RetQuery();                       

            //# # # Calcula Gastos # # #
            string query = "";
            query += " SELECT a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado ";

            if (String.IsNullOrEmpty(ObjValida.MsgErro))
            {
                if (validacao.Rows.Count > 0)
                {
                    if (validacao.Rows[0]["credmodelo"].ToString() != "VLPARCE")
                    {
                        if (Convert.ToInt32(diaAtual) < 20)
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                            query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";
                        }
                        else
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                            query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year).ToString() + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(diaAtual) < 20)
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                            query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year+ "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";
                        }
                        else
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                            query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                        }
                    }
                }
                else
                {
                    xRet += "Não há dados para Exibição!";
                }
            }
            else
            {
                xRet += "Erro: " + ObjValida.MsgErro.ToString();
            }

            ObjDados.Query = query;
            DataTable dados = ObjDados.RetQuery();

            //##Validação tido de cartão            
            if (String.IsNullOrEmpty(ObjValida.MsgErro))
            {
                if (validacao.Rows.Count > 0)
                {
                    if (validacao.Rows[0]["credmodelo"].ToString() != "VLPARCE")
                    {
                        if (Convert.ToInt32(diaAtual) < 20)
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                            xRet += "" + validacao.Rows[0]["credmodelo"].ToString() + " '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year +1).ToString() + "-" + DateTime.Now.Month + "-19'";
                        }
                        else
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                            xRet += "" + validacao.Rows[0]["credmodelo"].ToString() + " '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19'";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(diaAtual) < 20)
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                            xRet += "" + validacao.Rows[0]["credmodelo"].ToString() + " " + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + (DateTime.Now.Month + 9) + "-19'";
                        }
                        else
                        {
                            mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                            xRet += "" + validacao.Rows[0]["credmodelo"].ToString() + " '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + (DateTime.Now.Month + 9) + "-19'";
                        }
                    }
                }
                else
                {
                    xRet += "Não encontrou dados para retornar";
                }
            }
            else
            {
                xRet += "Erro: " + ObjValida.MsgErro.ToString();
            }
            //# # # Captura Crédito # # #

            //# Validar Período

#pragma warning disable CS0219 // A variável "diferencaMeses" é atribuída, mas seu valor nunca é usado
            int diferencaMeses = 0;
#pragma warning restore CS0219 // A variável "diferencaMeses" é atribuída, mas seu valor nunca é usado
            int qtdeParcelas = 2;
            int qtdeMesesQueFalta = ((Convert.ToInt32(DateTime.Now.Month.ToString()) - 13 /*Tira de 13 por incluir o mês corrente. Exemplo: janeiro = 1 - 13 = 12 meses*/) * -1);

            //diferencaMeses = ((Convert.ToInt32(DateTime.Now.Month.ToString()) - 12) * -1) - 10;

            xRet += "<div style='background-color: #f26907; width: 500px; height: 400px; '>";
            xRet += "<p>" + "Valida Período" + "</p>";
            xRet += "<span>";
            xRet += "Data Inicio: " + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20" + "\n";
            xRet += "<p>";            
            xRet += "Mês Atual: " + DateTime.Now.Month.ToString() + ", Quanto falta até Dezembro..." + ((Convert.ToInt32(DateTime.Now.Month.ToString()) - 13 /*Tira de 13 por incluir o mês corrente. Exemplo: janeiro = 1 - 13 = 12 meses*/) * -1 ) + " # Meses até o fim do ano: " + qtdeMesesQueFalta;
            xRet += "<p>" + "Meses que Faltam para quando Vira o Ano: " +  (qtdeParcelas - qtdeMesesQueFalta) ;
            xRet += "</p>";
            xRet += "</p>";
            xRet += "<p>";
            if (DateTime.Now.Month > 2)
            {
                xRet += "Data Fim 2: " + (DateTime.Now.Year + 1) + "-" + DateTime.Now.Month.ToString() + "-19";
                
                /*
                if (qtdeParcelas <= qtdeMesesQueFalta)
                {
                    xRet += "Data Fim 1: " + (DateTime.Now.Year) + "-" + (Convert.ToInt32(DateTime.Now.Month) +1) + "-19";                    
                }
                else
                {                    
                    xRet += "Data Fim 2: " + (DateTime.Now.Year + 1) + "-" + (qtdeParcelas - qtdeMesesQueFalta) + "-19";
                }*/
            }
            else
            {
                xRet += "Data Fim 3: " + (DateTime.Now.Year + 1) + "-" + (qtdeParcelas - qtdeMesesQueFalta)+ "-19";
            }
            xRet += "</p>";
            xRet += "</span>";            
            xRet += "</div>";
            

            double saldo = 0.00;
            double gastos = 0.00;
            double credito = 0.00;

            if (String.IsNullOrEmpty(ObjValida.MsgErro))
            {
                if (validacao.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(dados.Rows[0]["gastos"].ToString()))
                    {
                        gastos = double.Parse(dados.Rows[0]["gastos"].ToString());
                        credito = double.Parse(dados.Rows[0]["credito"].ToString());
                        saldo = credito - ((gastos) * -1);
                    }
                    else
                    {
                        credito = double.Parse(dados.Rows[0]["credito"].ToString());
                        saldo = credito;
                    }
                    xRet += "<div>";
                    xRet += "<p>";
                    xRet += "Associado: " + dados.Rows[0]["titular"].ToString() + ", Modelo de Credito: " + validacao.Rows[0]["credmodelo"].ToString();
                    xRet += "</p>";
                    xRet += "<p>";
                    xRet += "Credito: " + dados.Rows[0]["credito"].ToString() + " - Seu Saldo é: " + saldo.ToString("C2");
                    xRet += "</p>";
                    xRet += "</div>";
                }
                else
                {
                    xRet += "Não encontrou dados para retornar";
                }
            }
            else
            {
                xRet += "Erro: " + ObjValida.MsgErro.ToString();
            }
            lblSaldo.Text = xRet;

            //return saldo;
        }

        public String requestForm() {
            int xRet = 1001;

            if (Request.Form["hdExtrato"] != null)
            {
                xRet = int.Parse(Request.Form["hdExtrato"].ToString());
            }

            return xRet.ToString();
        }
        /*
        protected String tamanhoTela()
        {
            int widht = 1001;
            if (Request.Form["hfTamanhoTela"] != null)
            {
                //widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
                widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
            }

            return widht.ToString();
        }
        */
        public String MontarExtrato()
        {
            string xRet = "Request Form";

            xRet += requestForm();


            return xRet;
        }
    }
}