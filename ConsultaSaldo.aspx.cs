using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Windows.Forms;
using System.Configuration;
using System.Data;

namespace Site
{
    public partial class ConsultaSaldo : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        public string IdAssoc { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Processo para testar Consulta de Saldo do Novo Modelo de Crédito, com liberação do Saldo após pagamento
        protected void consultarSaldo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);



            string idAssoc = iIdAssoc.Value;

            string xRet = "";

            /*" SELECT a.idassoc, a.titular, " +
              " IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) AS Extrato, " +
              " a.credmodelo as ModCredito, a.credito as Credito, " +
              " (SELECT SUM(valor) FROM comovime WHERE associado = a.idassoc AND m.cnscanmom IS NULL AND DATA BETWEEN '2022-12-20' AND '2023-01-19') AS Compras, " +
              " IF((SELECT IF(SUM(m.valor) IS NULL, 0, a.credito) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE m.associado = '" + idAssoc + "' AND m.DATA BETWEEN '2022-12-20' AND '2023-01-19') = 0, a.credito, (SUM(m.valor) + a.credito)) AS Saldo, " +
              " IF ((SELECT valor FROM assocdeb WHERE tipo in ('RETBB', 'RETBAN') AND associado = a.idassoc and cnscanmom is null AND dtvencim = '2022-12-19') is null, 0, (SELECT valor FROM assocdeb WHERE tipo = 'RETBB' and cnscanmom is null AND associado = '" + idAssoc + "' AND dtvencim = '2022-12-19') ) as Pagamento, " +
              " (SELECT SUM(valor) AS Debito FROM assocdeb WHERE associado = '" + idAssoc + "' AND cnscanmom IS NULL) as Debito " +
              //" #(SELECT SUM(valor) AS Debito FROM assocdeb WHERE associado = '" + idAssoc + "' AND cnscanmom IS NULL AND dtvencim = '2022-12-19') AS Debito #Este cara para considerar um período " +
              " FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado " +
              " WHERE a.idassoc IN('" + idAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL " +
              " AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2022-12-20' AND '2023-01-19', m.vencimento BETWEEN '2022-12-20' AND '2023-12-19') GROUP BY a.idassoc "*/

            string query = " SELECT a.idassoc, a.titular,  " +
                           "(SELECT IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE associado IN('" + idAssoc + "') AND m.vencimento BETWEEN '2022-12-20' AND '2023-12-19' AND m.cnscanmom IS NULL) AS TotalGastos, " +
                           "(SELECT IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE associado IN('" + idAssoc + "') AND m.vencimento BETWEEN '2022-12-20' AND '2023-01-19' AND m.cnscanmom IS NULL) AS Extrato, " +
                           //" (SELECT IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) AS Extrato FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE associado IN('" + idAssoc + "') AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2022-12-20' AND '2023-01-19', m.vencimento BETWEEN '2022-12-20' AND '2023-12-19')) AS TotalGastos, " +
                           //" (SELECT IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) AS Extrato FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE associado IN('" + idAssoc + "') AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2022-12-20' AND '2023-01-19', m.vencimento BETWEEN '2022-12-20' AND '2023-01-19')) AS Extrato, " +
                           " a.credmodelo AS ModCredito, a.credito AS Credito, " +
                           " (SELECT SUM(valor) FROM comovime WHERE associado = a.idassoc AND m.cnscanmom IS NULL AND DATA BETWEEN '2022-12-20' AND '2023-01-19') AS Compras, " +
                           " IF((SELECT IF(SUM(m.valor) IS NULL, 0, a.credito) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE m.associado = a.idassoc AND m.DATA BETWEEN '2022-12-20' AND '2023-01-19') = 0, a.credito, (SUM(m.valor) + a.credito)) AS Saldo, " +

                           " (IFNULL((SELECT IF(SUM(m.valor) IS NULL, 0, (SUM(m.valor)*-1)) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE associado IN('" + idAssoc + "') AND m.vencimento BETWEEN '2022-12-20' AND '2023-01-19' AND m.cnscanmom IS NULL), 0) + " +
                           " (IF((SELECT IF(SUM(m.valor) IS NULL, 0, a.credito) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE m.associado = '" + idAssoc + "' AND m.DATA BETWEEN '2022-12-20' AND '2023-01-19') = 0, a.credito, (SUM(m.valor) + a.credito))  ) ) AS SaldoNovoModelo,  " +
                           " IF((SELECT valor FROM assocdeb WHERE tipo  IN('RETBB', 'RETBAN', 'PAGEST') AND associado = '" + idAssoc + "' AND cnscanmom IS NULL AND dtvencim = '2022-12-19') IS NULL, 0, (SELECT valor FROM assocdeb WHERE tipo IN('RETBB', 'RETBAN', 'PAGEST') AND cnscanmom IS NULL AND associado = a.idassoc AND dtvencim = '2022-12-19') ) AS Pagamento, " +                           
                           " (SELECT SUM(valor) FROM assocdeb WHERE tipo  IN ('FECHAM', 'DEBANT') AND associado = '" + idAssoc + "' AND cnscanmom IS NULL AND dtvencim = '2022-12-19') AS SumarizaMovimentacao," +
                           " (SELECT SUM(valor) AS Debito FROM assocdeb WHERE associado = '" + idAssoc + "' AND cnscanmom IS NULL) AS Debito,  " +
                           " (SELECT SUM(valor) AS Debito FROM assocdeb WHERE associado = a.idassoc AND cnscanmom IS NULL AND dtvencim = '2022-12-19') AS DebitoComPeriodo " +
                           " FROM comovime AS m " +
                           " INNER JOIN associa AS a ON a.idassoc = m.associado " +
                           " WHERE a.idassoc IN('" + idAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL " +
                           " AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2022-12-20' AND '2023-01-19', m.vencimento BETWEEN '2022-12-20' AND '2023-12-19') GROUP BY a.idassoc ";
            // 2747, 735, 5289
            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            double pagamento;
            double gastos;
            double pagamentoxgastos;

            //MessageBox.Show(ObjDados.Query);

            //MessageBox.Show("Tem: " + dados.Rows[0]["SumarizaMovimentacao"].ToString());


 

            try
            {
                if (String.IsNullOrEmpty(iIdAssoc.Value))
                {                    
                    lblMsg.Text = "É preciso preencher o campo!";
                    //MessageBox.Show("É preciso preencher o campo!");
                }
                else
                {
                    lblMsg.Text = String.Empty;
                    if (!String.IsNullOrEmpty(dados.Rows[0]["SumarizaMovimentacao"].ToString()))
                    {
                        gastos = Convert.ToDouble(dados.Rows[0]["SumarizaMovimentacao"].ToString());
                        pagamento = Convert.ToDouble(dados.Rows[0]["Pagamento"].ToString());

                    }
                    else
                    {
                        gastos = 0.00;
                        pagamento = 0.00;

                    }
                    //MessageBox.Show("Gastos: " + (gastos * -1).ToString());
                    //MessageBox.Show("Pagamento: " + pagamento.ToString());

                    if (pagamento == 0.00)
                    {
                        pagamentoxgastos = ((pagamento - gastos) * -1);
                    }
                    else
                    {
                        pagamentoxgastos = (pagamento - gastos);
                    }

                    if (dados.Rows.Count > 0)
                    {
                        xRet += "<div>";
                        xRet += "<p>" + "Associado: " + dados.Rows[0]["titular"].ToString() + "</p>";

                        xRet += "<p>" + "Crédito: " + dados.Rows[0]["Credito"].ToString() + "</p>";
                        xRet += "<p>" + "Total de Gastos: " + dados.Rows[0]["TotalGastos"].ToString() + " (Representa a sumarização de todos os valores á vencer.)" + "</p>";
                        xRet += "<p>" + "Extrato: " + dados.Rows[0]["Extrato"].ToString() + " (Representa os Valores do mês corrente)" + "</p>";
                        xRet += "<p>" + "Gastos Futuro: " + dados.Rows[0]["Compras"].ToString() + " ( Referente às compras realizadas no período.) " + "</p>";
                        xRet += "<p>" + "Pagamento: " + dados.Rows[0]["Pagamento"].ToString() + "</p>";
                        xRet += "<p>" + "Sumariza Movimentação: " + dados.Rows[0]["SumarizaMovimentacao"].ToString() + "(Refrente a sumarização das compras registradas na Movimentação)" + "</p>"; //Esse cara deve ser utilizado para determinar se o pagamento corresponde com o total da d´vida e determinar o valor a ser devolvido ao saldo, se parcial ou total                        
                        xRet += "<p>" + "Débito: " + dados.Rows[0]["Debito"].ToString() + "</p>";
                        xRet += "<p>" + "Débito com Período: " + dados.Rows[0]["DebitoComPeriodo"].ToString() + "</p>";
                        if (dados.Rows[0]["ModCredito"].ToString() == "VLPARCE")
                        {
                            xRet += "<p>" + "Modelo de Crédito: " + dados.Rows[0]["ModCredito"].ToString() + " - Atual " + "</p>";
                            //xRet += "<p>" + "Modelo de Crédito: Atual" + "</p>";
                        }
                        else
                        {
                            //xRet += "<p>" + "Modelo de Crédito: Novo" + "</p>";
                            xRet += "<p>" + "Modelo de Crédito: " + dados.Rows[0]["ModCredito"].ToString() + " - Novo " + "</p>";
                            if (pagamento > (gastos * -1))
                            {
                                xRet += "<p>" + "Modelo de Saldo Proposto: " + ((Convert.ToDouble(dados.Rows[0]["SumarizaMovimentacao"].ToString())*-1) + Convert.ToDouble(dados.Rows[0]["Saldo"].ToString())) + " Houve Pagamento Total!" + "</p>";
                            }
                            else
                            {
                                xRet += "<p>" + "Modelo de Saldo Proposto: " + "Gastos: " + gastos.ToString("C2") + ", Pagamento: " + pagamento.ToString("C2") + " = " + (Convert.ToDouble(dados.Rows[0]["Saldo"].ToString()) + pagamento).ToString("C2") + " Não Houve Pagamento, ou ele foi Parcial!" + "</p>";
                            }
                        }
                        
                        xRet += "<p>" + "Saldo: " + dados.Rows[0]["Saldo"].ToString() + "</p>";

                        xRet += "<div>";
                    }
                    else
                    {
                        xRet += "Não encontrou dados para retornar";
                    }
                    
                    lblMsg.Text = xRet;
                }                
            }
            catch (Exception erro)
            {
                lblMsg.Text = erro.Message;
            }



        }//consultarSaldo

        protected void executarDatas(object sender, EventArgs e)
        {
            string xRet = "";
            string xMes = "";
            string xDia = "";


            string hoje = DateTime.Now.ToString("dd-MM-yyyy");            
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                //xMes += "Janeiro";

                if (Convert.ToInt32(dia) >= 20)
                {
                    //xDia += "Maior ou = a 20, Hoje é: " + DateTime.Now.ToString("dd-MM") + "<br />";                    
                    xDia += "'" + ano + "-" + mes + "-20'" + " AND " + "'"+ ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    //xDia += "Menor que 20, Hoje é: " + DateTime.Now.ToString("dd-MM") + "<br />";                    
                    xDia += "'" + (Convert.ToInt32(ano) - 1) + "-" + "12" + "-20'" + " AND " + "'" + ano + "-" + mes + "-19'";
                }
            }
            else
            {
                //xMes += "Não é Janeiro, é: " + DateTime.Now.ToString("Y") ;

                if (Convert.ToInt32(dia) >= 20)
                {
                    //xDia += "Maior ou = a  20, Hoje é: " + DateTime.Now.ToString("dd-MM") + "<br />";                                        
                    xDia += "'" + ano + "-" + mes + "-20'" + " AND " + "'"+ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    //xDia += "Menor que 20, Hoje é: " + DateTime.Now.ToString("dd-MM") + "<br />";
                    
                    xDia += "'" + ano  + "-" + (Convert.ToInt32(mes) - 1).ToString().PadLeft(2, '0') + "-20'" + " AND " +"'" +ano + "-" + mes + "-19'";
                }
            }
            //Checa dia 20

            xRet += "Itens:" + "<br />";
            xRet += "1 - Dia Inicial: 20" + "<br />";
            xRet += "2 - Dia Final: 19" + "<br />";
            xRet += "3 - Janeiro" + "<br /><br />";
            xRet += "Item 1" + "<br />";
            xRet += "Hoje: " + hoje + "<br />";            
            xRet += "Dia: " + dia + "<br />";
            xRet += "Mês: " + mes + "<br />";
            xRet += "Ano: " + ano + "<br /><br />";
            xRet += "Item 2" + "<br />";
            //xRet += "Checa Janeiro: " + xMes + "<br />";
            xRet += "Periodo:  <br /> " + xDia + " <br />";
            xRet +="# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #" + "<br />";
            xRet += "Datas: Inicio e Fim" + "<br />";
            xRet += "Data Inicial: " + dtIni_Saldo() + "<br />";
            xRet += "Data Final: " + DtFim_Saldo() + "<br />";
            xRet += "Data Fibal (Novo):" + DtFim_SaldoNovoCartao();            

            lblMsg.Text = xRet;
        }//executarDatas

        protected String Periodo()
        {            
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {               
                if (Convert.ToInt32(dia) >= 20)
                {                    
                    periodo += "'" + ano + "-" + mes + "-20'" + " AND " + "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {                    
                    periodo += "'" + (Convert.ToInt32(ano) - 1) + "-" + "12" + "-20'" + " AND " + "'" + ano + "-" + mes + "-19'";
                }
            }
            else
            {
                
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + mes + "-20'" + " AND " + "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {                    
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) - 1).ToString().PadLeft(2, '0') + "-20'" + " AND " + "'" + ano + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//Periodo

        protected String dtIni_Saldo()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + mes + "-20'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) - 1) + "-" + "12" + "-20'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + mes + "-20'";
                }
                else
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) - 1).ToString().PadLeft(2, '0') + "-20'";
                }
            }

            return periodo;
        }//Início_Saldo

        protected String DtFim_Saldo()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + ano + "-" + mes + "-19'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + ano + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//Fim_Saldo

        protected String DtFim_SaldoNovoCartao()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + mes + "-19'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//FimSaldoNovoCartao

        protected void executarSaldo(object sender, EventArgs e) {            

            BLL ObjDados = new BLL(conectVegas);
            Apoio ObjApoio = new Apoio();

            IdAssoc = iIdAssoc.Value;
            string xRet = "";            

            string queryXXX = "SELECT a.idassoc, a.titular,  IF(SUM(m.valor) IS NULL, 0, SUM(m.valor)) AS gastos, a.credmodelo, a.credito, " +
                "(SELECT SUM(valor) FROM comovime WHERE associado = a.idassoc AND DATA BETWEEN '2022-12-20' AND '2023-01-19') AS compras, " +
                "IF((SELECT IF(SUM(m.valor) IS NULL, 0, a.credito) FROM comovime AS m INNER JOIN associa AS a ON a.idassoc = m.associado WHERE m.associado = '" + IdAssoc + "' AND m.DATA BETWEEN '"+ObjApoio.dtDataInicio()+"' AND '"+ ObjApoio.dtDataFim() +"') = 0, a.credito, (SUM(m.valor) + a.credito)) AS Saldo,  " +
                "(SELECT SUM(valor) AS Debito FROM assocdeb WHERE associado = a.idassoc AND cnscanmom IS NULL) as Debito " +
                "FROM comovime AS m " +
                "INNER JOIN associa AS a ON a.idassoc = m.associado " +
                "WHERE a.idassoc IN('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL " +
                "AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '" + ObjApoio.dtDataInicio() + "' AND '" + ObjApoio.dtDataFim() + "', m.vencimento BETWEEN '" + ObjApoio.dtDataInicio() + "' AND '2023-12-19')";

            //Query Fixa
            string queryYYY = " SELECT titular, credito, " +
                " (SELECT IF(m.valor IS NOT NULL, (SUM(m.valor) + a.credito), SUM(m.valor)) " +
                " FROM comovime AS m " +
                " LEFT JOIN associa AS a ON a.idassoc = m.associado " +
                " WHERE a.idassoc in ('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL " +
                " AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2023-01-20' AND '2023-02-19', m.vencimento BETWEEN '2023-01-20' AND '2023-12-19') ) as Saldo " +
                " FROM associa " +
                " WHERE idassoc in ('" + IdAssoc + "') and cnscanmom is null ";
            //Query Dinâmica


            string query = " SELECT titular, credito, " +
                " (SELECT IF(m.valor IS NOT NULL, (SUM(m.valor) + a.credito), SUM(m.valor)) " +
                " FROM comovime AS m " +
                " LEFT JOIN associa AS a ON a.idassoc = m.associado " +
                " WHERE a.idassoc in ('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL " +
                " AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_Saldo() + ", m.vencimento BETWEEN "+ dtIni_Saldo() + " AND " + DtFim_SaldoNovoCartao() + ") ) as Saldo " +
                " FROM associa " +
                " WHERE idassoc in ('" + IdAssoc + "') and cnscanmom is null ";

            ObjDados.Query = query;
            DataTable dados = ObjDados.RetQuery();
            string valida = "";
            
            if (dados.Rows.Count > 0)
            {
                valida = dados.Rows[0]["saldo"].ToString();
            }

            if (String.IsNullOrEmpty(valida))
            {
                xRet += dados.Rows[0]["credito"].ToString();
            }
            else
            {
                xRet += dados.Rows[0]["saldo"].ToString();
            }
            

            if (String.IsNullOrEmpty(iIdAssoc.Value))
            {
                xRet += "Precisa Preencher o campo!!!";
            }
            else
            {

                lblMsg.Text = "Seu Saldo é: " + xRet;
            }

            //lblMsg.Text = xRet;
        }//executarSaldo

    }
}