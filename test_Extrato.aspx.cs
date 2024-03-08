using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Site.App_Code;
using System.Configuration;

namespace Site
{
    public partial class test_Extrato : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        public string Mes { get; set; }
        public string Ano { get; set; }
        public string Xret { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                atualizaPeriodoDdls();
            }
        }

        protected void evoluirPerioro(object sender, EventArgs e)
        {
            Apoio Apoio = new Apoio();

            string xRet = "";

            Apoio.Mes = ddlMes.SelectedValue;
            Apoio.Ano = ddlAno.SelectedValue;

            string xMes = ddlMes.SelectedValue;
            string xAno = ddlAno.SelectedValue;
            int x;
            x = (Convert.ToInt32(xMes));

            int y = 12 - x;
            string xMudaAno = "";

            for (int i = 0; i < (y - 1); i++)
            {
                //xMudaAno += "" + (12 - i);
                if ((12 - i) == 12)
                {
                    xRet += "<p>" + " dtvencime = " + "2021-" + (12 - i) + "-20" + " AND " + "2022-" + "01" + "-19" + "</p>" + "\n";
                }
                else
                {

                    xRet += "<p>" + " dtvencime = " + "2021-" + (12 - i) + "-20" + " AND " + "2021-" + ((12 - i) + 1) + "-19" + "</p>" + "\n";
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                if (x != 0)
                {

                    while (x > 1)
                    {
                        xRet +="<p>" + " dtvencime = " + "2022-" + (x - i) + "-20" + " AND " + "2022-" + ((x - 1) + 1) + "-19" + "</p>" + "\n";
                        //xRet += x - i;
                        x--;
                    }
                }
            }




            lblMsgPeriodo.Text = xRet + " - " + xMudaAno;
        }
        protected void atualizaPeriodoDdls()
        {
            string anoAtual = DateTime.Now.Year.ToString();
            string mesAtual = DateTime.Now.Month.ToString();

            foreach (System.Web.UI.WebControls.ListItem lAno in ddlAno.Items)
            {
                if (lAno.Value == anoAtual)
                {
                    lAno.Selected = true;
                }
            }
            foreach (ListItem lMes in ddlMes.Items)
            {
                if (lMes.Value == mesAtual)
                {
                    lMes.Selected = true;
                }
            }

        }

        protected void escolherPeriodo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            string mesAtual = DateTime.Now.Month.ToString();
            string anoAtual = DateTime.Now.Year.ToString();


            if (ddlMes.SelectedValue == mesAtual)
            {
                string xRet = "";
                if (ddlMes.SelectedValue == "1") //Janeiro
                {
                    xRet += "Mês selecionado: " + ddlMes.Text + ", Ano Selecionado: " + ddlAno.Text + "\n" + "<br>";
                    xRet += "Início: " + (Convert.ToInt32(ddlAno.SelectedValue) - 1) + "-" + "12" + "-20";
                    xRet += "\n" + "<br>";
                    xRet += "Fim: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(ddlMes.SelectedValue)).ToString() + "-19";
                }
                else
                {
                    xRet += "Mês selecionado: " + ddlMes.Text + ", Ano Selecionado: " + ddlAno.Text + "\n" + "<br>";
                    xRet += "Início: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(mesAtual) - 1).ToString() + "-20";
                    xRet += "\n" + "<br>";
                    xRet += "Fim: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(mesAtual)).ToString() + "-19";
                }

                lblMsg.Text = xRet;
            }
            else
            {
                //MessageBox.Show(ddlMes.SelectedItem.ToString());
                string xRet = "";
                if (ddlMes.SelectedValue == "1")
                {
                    xRet += "Mês selecionado: " + ddlMes.Text + ", Ano Selecionado: " + ddlAno.Text + "\n" + "<br>";
                    xRet += "Início: " + (Convert.ToInt32(ddlAno.SelectedValue) - 1) + "-" + "12" + "-20";
                    xRet += "\n" + "<br>";
                    xRet += "Fim: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(ddlMes.SelectedValue)).ToString() + "-19";
                }
                else
                {
                    if (Convert.ToInt32(mesAtual) < Convert.ToInt32(mesAtual))
                    {
                        //MessageBox.Show("Mês Atual: " + mesAtual + ", Mês Selecionado: " + ddlMes.SelectedItem + " - Menos 1: " + (Convert.ToInt32(ddlMes.SelectedValue) - 1).ToString());
                        xRet += "Mês selecionado: " + ddlMes.Text + ", Ano Selecionado: " + ddlAno.Text + "\n" + "<br>";
                        xRet += "Início: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(ddlMes.SelectedValue) - 1).ToString() + "-20";
                        xRet += "\n" + "<br>";
                        xRet += "Fim: " + ddlAno.SelectedValue + "-" + ddlMes.SelectedValue + "-19";
                        //MessageBox.Show(xRet);

                    }
                    else
                    {
                        //MessageBox.Show("Mês atual: " + mesAtual + ", Mês Selecionado: " + ddlMes.SelectedItem + " - Mais 1: " + (Convert.ToInt32(ddlMes.SelectedValue) + 1).ToString());
                        xRet += "Mês selecionado: " + ddlMes.Text + ", Ano Selecionado: " + ddlAno.Text + "\n" + "<br>";
                        xRet += "Início: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(ddlMes.SelectedValue) - 1) + "-20";
                        xRet += "\n" + "<br>";
                        xRet += "Fim: " + ddlAno.SelectedValue + "-" + (Convert.ToInt32(ddlMes.SelectedValue)).ToString() + "-19";
                    }

                }
                lblMsg.Text = xRet;
            }
        }

        protected void metodoPeriodo(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            Apoio.Ano = ddlAno.SelectedValue;
            Apoio.Mes = ddlMes.SelectedValue;


            lblMsg.Text = Apoio.Periodo();

        }

        //# # # # # # # # # # # # Operações com Data # # # # # # # # # # # #
        public String dtDataInicio()
        {
            string mesAtual = DateTime.Now.Month.ToString();
            string dtInicio = "";
            string dtFim = "";

            if (Mes == mesAtual)
            {
                if (Mes == "1") //Janeiro
                {
                    dtInicio += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    dtInicio += "'" + Ano + "-" + (Convert.ToInt32(mesAtual) - 1).ToString() + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(mesAtual)).ToString() + "-19'";
                }
            }
            else
            {
                if (Mes == "1")
                {
                    dtInicio += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    if (Convert.ToInt32(mesAtual) < Convert.ToInt32(mesAtual))
                    {
                        dtInicio += "'" + Ano + "-" + (Convert.ToInt32(Ano) - 1).ToString() + "-20'";
                        dtFim += "'" + Ano + "-" + Mes + "-19'";
                    }
                    else
                    {
                        dtInicio += "'" + Ano + "-" + (Convert.ToInt32(Mes) - 1) + "-20'";
                        dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                    }

                }
            }
            return dtInicio;
        }

        public String dtDataFim()
        {

            string mesAtual = DateTime.Now.Month.ToString();
            string dtInicio = "";
            string dtFim = "";

            if (Mes == mesAtual)
            {
                if (Mes == "1") //Janeiro
                {
                    dtInicio += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    dtInicio += "'" + Ano + "-" + (Convert.ToInt32(mesAtual) - 1).ToString() + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(mesAtual)).ToString() + "-19'";
                }
            }
            else
            {
                if (Mes == "1")
                {
                    dtInicio += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20'";
                    dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    if (Convert.ToInt32(mesAtual) < Convert.ToInt32(mesAtual))
                    {
                        dtInicio += "'" + Ano + "-" + (Convert.ToInt32(Ano) - 1).ToString() + "-20'";
                        dtFim += "'" + Ano + "-" + Mes + "-19'";
                    }
                    else
                    {
                        dtInicio += "'" + Ano + "-" + (Convert.ToInt32(Mes) - 1) + "-20'";
                        dtFim += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                    }

                }
            }
            return dtFim;
        }

        public String Periodo()
        {
            string mesAtual = DateTime.Now.Month.ToString();
            string anoAtual = DateTime.Now.Year.ToString();
            Xret = "";

            if (Mes == mesAtual)
            {
                if (Mes == "1") //Janeiro
                {
                    Xret += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20' AND ";
                    Xret += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    Xret += "'" + Ano + "-" + (Convert.ToInt32(mesAtual) - 1).ToString() + "-20' AND ";
                    Xret += "'" + Ano + "-" + (Convert.ToInt32(mesAtual)).ToString() + "-19'";
                }
            }
            else
            {
                if (Mes == "1")
                {
                    Xret += "'" + (Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20' AND ";
                    Xret += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    if (Convert.ToInt32(mesAtual) < Convert.ToInt32(Mes))
                    {
                        Xret += "'" + Ano + "-" + (Convert.ToInt32(Mes) - 1).ToString() + "-20' AND ";
                        Xret += "'" + Ano + "-" + Mes + "-19'";
                    }
                    else
                    {
                        Xret += "'" + Ano + "-" + (Convert.ToInt32(Mes) - 1) + "-20' AND ";
                        Xret += "'" + Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                    }
                }
            }
            return Xret;
        }

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

            if (Convert.ToInt32(mes) < 12)
            {
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
            }
            else
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes)).ToString().PadLeft(2, '0') + "-19'";
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

            if (Convert.ToInt32(mes) < 12)
            {
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
            }
            else
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + (Convert.ToInt32(mes)).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//FimSaldoNovoCartao

        //# # # # # # # # # # # # Fim Operações com Data # # # # # # # # # # # #

        public double SaldoAssociadoComParametro(string id)
        {
            BLL ObjDados = new BLL(conectVegas);

            string query = " SELECT titular, credito, " +
                           " (SELECT IF(m.valor IS NOT NULL, (SUM(m.valor) + a.credito), SUM(m.valor))" +
                           " FROM comovime AS m" +
                           "  LEFT JOIN associa AS a ON a.idassoc = m.associado" +
                           " WHERE a.idassoc IN('" + id + "')" +
                           "  AND a.cnscanmom IS NULL" +
                           "  AND m.cnscanmom IS NULL" +
                           "  AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_Saldo() + ", m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_SaldoNovoCartao() + ")) as Saldo " +
                           //" AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN '2024-01-20' AND '2024-02-19', m.vencimento BETWEEN '2024-01-20' AND '2024-12-19')) AS Saldo " +
                           " FROM associa" +
                           " WHERE idassoc IN('" + id + "')AND cnscanmom IS NULL";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            //MessageBox.Show(query);

            double saldo = 0;

            /*
            string valida = dados.Rows[0]["saldo"].ToString();
            
            if (String.IsNullOrEmpty(valida))
            {
                saldo = Convert.ToDouble(dados.Rows[0]["credito"].ToString());
            }
            else
            {
                saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
            }
            */

            if (String.IsNullOrEmpty(dados.Rows[0]["saldo"].ToString()))
            {
                saldo = Convert.ToDouble(dados.Rows[0]["credito"].ToString());
            }
            else
            {
                saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
            }


            //return query;
            return saldo;

        }//SaldoAssociado
        public void retornaSaldo(object sender, EventArgs e)
        {
            Apoio Apoio = new Apoio();

            string idAssoc = iIdAssoc.Value;
            string saldo = "";

            //MessageBox.Show("Clicou");

            //Exibe o Saldo apenas se o associado nao estiver Bloqueado
            if (Apoio.bloqueioAssociadoComParametro(idAssoc) == "Liberado")
            {
                if (SaldoAssociadoComParametro(idAssoc) > 0)
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

            lblSaldo.Text =  saldo;
        }


        protected void enviarEmail(object sender, EventArgs e)
        {
            Apoio Apoio = new Apoio();

            string para = iPara.Value;
            string cc = iCopia.Value;
            string assunto = iAssunto.Value;
            string msg = iDados.Value;

            lblMsg.Text = "Sua Mensagem foi enviada para: " + para + ", com cópia para: " + cc + " - Para tratar de: " + assunto + ", veja os detalhes: " + msg;

            Apoio.EnviarEmail(para, cc, assunto, msg);
        }


    }//Fim



}