using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;
using System.Data;
using Site.App_Code;
#pragma warning disable CS0105 // A diretiva using para "System.Configuration" apareceu anteriormente neste namespace
using System.Configuration;
#pragma warning restore CS0105 // A diretiva using para "System.Configuration" apareceu anteriormente neste namespace
using System.Net;
using System.Net.Mail;

namespace Site.App_Code
{
    public class Apoio
    {
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        public string xPdf { get; set; }

        public string idMovExt { get; set; }
        public string nomeAssocExt { get; set; }
        public string momentoAssocExt { get; set; }
        public string convenioAssocExt { get; set; }
        public string cartaoAssocExt { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        //Período Extrato
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string Xret { get; set; }
        //Associado
        public string IdAssoc { get; set; }
        public string IdConv { get; set; }
        public string retorno { get; set; }//Utilizada para Gerar retorno de Msgs para explodir na tela pelo Java
        public String hora()
        {
            string hora = "";
            string min = "";
            string horaAtual = "";
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;

            if (h < 10)
            {
                hora = "0" + h;
            }
            else
            {
                hora = "" + h;
            }

            if (m < 10)
            {
                min = "0" + m;
            }
            else
            {
                min = "" + m;
            }

            horaAtual = hora + ":" + min;

            return horaAtual.ToString();
        }

        public string gerarPdf()
        {
            string PdfGerado = xPdf;

            Document pdf = new Document(PageSize.A4);
            pdf.SetMargins(40, 40, 40, 80);
            pdf.AddCreationDate();
            string caminhoPdf = ConfigurationManager.AppSettings["caminhoArquivoPdf"] + "Arquivo_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
            //string caminho = @"K:\Projetos\Web\Site\Site\Downloads\" + "Arquivo_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
            PdfWriter excreverPDF = PdfWriter.GetInstance(pdf, new FileStream(caminhoPdf, FileMode.Create));

            pdf.Open();

            string dados = "";

            Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 20));
            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Add(PdfGerado);

            pdf.Add(paragrafo);

            pdf.Close();

            return paragrafo.ToString();

        }

        public String GetIp()
        {

            string IPAddress = "";

            IPHostEntry Host = default(IPHostEntry);
            string ipHost = null;
            ipHost = System.Environment.MachineName;

            Host = Dns.GetHostEntry(ipHost);

            foreach (IPAddress iP in Host.AddressList)
            {
                if (iP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(iP);
                }
            }

            return IPAddress;
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

        public string bloqueioAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            string xRet = "";

            string query = " SELECT idassoc, titular, bloqueio, IF(bloqueio IS NULL, 'Liberado', 'Bloqueado') AS bloq_status, bloq_tip, bloq_mot FROM associa " +
                " WHERE cnscanmom IS NULL " +
                " AND idassoc in ('" + IdAssoc + "')";

            ObjDados.Query = query;

            //xRet = ObjDados.Query;

            DataTable dados = ObjDados.RetQuery();
            try
            {
                if (dados.Rows.Count > 0)
                {
                    xRet = dados.Rows[0]["bloq_status"].ToString();
                }
            }
            catch (Exception e) {
                xRet = e.Message;
            }            

            return xRet;
        }

        public string bloqueioAssociadoComParametro(string id)
        {
            BLL ObjDados = new BLL(conectVegas);

            string xRet = "";

            string query = " SELECT idassoc, titular, bloqueio, IF(bloqueio IS NULL, 'Liberado', 'Bloqueado') AS bloq_status, bloq_tip, bloq_mot FROM associa " +
                " WHERE cnscanmom IS NULL " +
                " AND idassoc in ('" + id + "')";

            ObjDados.Query = query;

            //xRet = ObjDados.Query;

            DataTable dados = ObjDados.RetQuery();
            try
            {
                if (dados.Rows.Count > 0)
                {
                    xRet = dados.Rows[0]["bloq_status"].ToString();
                }
            }
            catch (Exception e)
            {
                xRet = e.Message;
            }

            return xRet;
        }//bloqueioAssociadoComParametro

        public double GastosAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            double gastos = 0;
            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();

            //# # # # # Período do Extrato # # # # #

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

            //# # # # # FIM Período # # # # #

            ObjDados.Campo = " SUM(valor) AS gastos ";
            ObjDados.Tabela = " comovime ";
            //ObjGastos.Condicao = " WHERE associado = '2747' AND vencimento BETWEEN '2021-10-20' AND '2021-11-19' "; //Exemplo                                        

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                //ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-19' ";
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN " + dtInicio + " AND " + dtFim + " AND cnscanmom IS NULL ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19' ";
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN " + dtInicio + " AND " + dtFim + " AND cnscanmom IS NULL ";
            }

            DataTable dados = ObjDados.RetCampos();
            //MessageBox.Show("select " + ObjGastos.Campo + " FROM " + ObjGastos.Tabela + " " + ObjGastos.Condicao);

            if (dados.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dados.Rows[0]["gastos"].ToString()))
                {
                    gastos = 0.00;
                }
                else
                {
                    gastos = double.Parse(dados.Rows[0]["gastos"].ToString()) * -1;
                }
            }
            else
            {
                gastos = 0.00;
            }


            return gastos;
        }

        public double DebitosAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            double gastos = 0;
            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();

            //# # # # # Período do Extrato # # # # #

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

            //# # # # # FIM Período # # # # #
            /* 
            ObjDados.Query = " SELECT SUM(valor) FROM assocdeb " +
                             " WHERE associado = '2747' AND dtvencim BETWEEN '2022-09-20' AND '2022-10-19' AND tipo NOT IN('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB', 'PAGEST' ) ";
            */
            ObjDados.Campo = " SUM(valor) AS gastos ";
            ObjDados.Tabela = " assocdeb ";
            //ObjGastos.Condicao = " WHERE associado = '2747' AND vencimento BETWEEN '2021-10-20' AND '2021-11-19' "; //Exemplo                                        

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                //ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN " + dtInicio + " AND " + dtFim + " AND cnscanmom IS NULL ";
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND dtvencim BETWEEN " + dtInicio + "AND " + dtFim + " AND tipo NOT IN('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB', 'PAGCON', 'PAGEST', 'PARFIN' ) AND CNSCANMOM IS NULL";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN " + dtInicio + " AND " + dtFim + " AND cnscanmom IS NULL ";
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND dtvencim BETWEEN " + dtInicio + "AND " + dtFim + " AND tipo NOT IN('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB', 'PAGCON', 'PAGEST', 'PARFIN' ) AND CNSCANMOM IS NULL";
            }

            DataTable dados = ObjDados.RetCampos();
            //MessageBox.Show("select " + ObjGastos.Campo + " FROM " + ObjGastos.Tabela + " " + ObjGastos.Condicao);

            if (dados.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dados.Rows[0]["gastos"].ToString()))
                {
                    gastos = 0.00;
                }
                else
                {
                    gastos = double.Parse(dados.Rows[0]["gastos"].ToString()) * -1;
                }
            }
            else
            {
                gastos = 0.00;
            }


            return gastos;
        }

        public double SaldoAssociado_ate_03_11_2022()
        {
            BLL ObjGastos = new BLL(conectVegas);
            BLL ObjCredito = new BLL(conectVegas);

            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();

            //# # # Calcula Gastos # # #

            ObjGastos.Campo = " SUM(valor) AS gastos ";
            ObjGastos.Tabela = " comovime ";

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                ObjGastos.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-19' AND cnscanmom IS NULL";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                ObjGastos.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19' AND cnscanmom IS NULL";
            }

            //# # # Captura Crédito # # #

            ObjCredito.Campo = " credito ";
            ObjCredito.Tabela = " associa ";
            ObjCredito.Condicao = " WHERE idassoc = '" + IdAssoc + "' AND cnscanmom IS NULL ";


            DataTable dGastos = ObjGastos.RetCampos();
            DataTable dCredito = ObjCredito.RetCampos();

            double saldo = 0.00;
            double gastos = 0.00;
            double credito = 0.00;

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            if (!String.IsNullOrEmpty(dGastos.Rows[0]["gastos"].ToString()))
            {
                gastos = double.Parse(dGastos.Rows[0]["gastos"].ToString());
                credito = double.Parse(dCredito.Rows[0]["credito"].ToString());
                saldo = credito - ((gastos) * -1);
            }
            else
            {
                credito = double.Parse(dCredito.Rows[0]["credito"].ToString());
                saldo = credito;
            }

            return saldo;
        }

        public double SaldoAssociadoBK()
        {
            BLL ObjDados = new BLL(conectVegas);

            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();
#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            //# # # Calcula Gastos # # #
            string query = "";
            /*
            query += " SELECT a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado "; */

            //query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito, (SUM(m.valor) + a.credito) AS Saldo FROM comovime AS m " +
            query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito,";
            query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
            query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";
            query += " FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado " ;
            query += " WHERE idassoc IN('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual +           "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";                        
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + (mesAtual) + "-19', m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year).ToString() + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + (mesAtual) + "-19', m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
            }


            ObjDados.Query = query;
            DataTable dados = ObjDados.RetQuery();

            double saldo = 0;
                        
            if (dados.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dados.Rows[0]["saldo"].ToString()))
                {
                    saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
                }
                else
                {
                    saldo = 0.00;
                }
            }
            else
            {
                saldo = 0.00;
            }

            return saldo;
        }

        public double SaldoAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            string query = " SELECT titular, credito, " +
                           " (SELECT IF(m.valor IS NOT NULL, (SUM(m.valor) + a.credito), SUM(m.valor))" +
                           " FROM comovime AS m" +
                           "  LEFT JOIN associa AS a ON a.idassoc = m.associado" +
                           " WHERE a.idassoc IN('" + IdAssoc + "')" +
                           "  AND a.cnscanmom IS NULL" +
                           "  AND m.cnscanmom IS NULL" +                           
                           "  AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_Saldo() + ", m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_SaldoNovoCartao() + ")) as Saldo " +
                           " FROM associa" +
                           " WHERE idassoc IN('" + IdAssoc + "')AND cnscanmom IS NULL";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();           

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

        }

        public String msgJava(string msgErro)
        {
            string msgJava = "";

            msgJava += "<script>" +
                        "function verMsgJava() {" +
                            "   var msg = '" + msgErro + "')" +
                            "Alert(msg);" +
                            //"event.preventDefault();" +
                            "}" +
                            "verMsgJava();" +
                            "</script>";
            return msgJava;
        }
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
                           " FROM associa" +
                           " WHERE idassoc IN('" + id + "')AND cnscanmom IS NULL";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

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

            string xRet = "";
            string msgErro = "";


            try
            {
                if (String.IsNullOrEmpty(dados.Rows[0]["saldo"].ToString()))
                {
                    saldo = Convert.ToDouble(dados.Rows[0]["credito"].ToString());
                    msgJava("Crédito");
                }
                else
                {
                    saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());                    
                    msgJava("Saldo");
                }

                
                return saldo;
            }
            catch (Exception e)
            {
                msgErro = "Erro Original:" + e.Message;

                msgJava(msgErro);

                /*xRet += "function verSaldo() {" +
                        "   var msg = " + msgErro + ")" +
                        "Alert(msg);" +
                        "event.preventDefault();}";
                */
                return 0.00;
            }

            return 0.00;

            //return query;


        }//SaldoAssociado
        public double SaldoAssociado_Original()
        {
            BLL ObjDados = new BLL(conectVegas);

            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();
#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            //# # # Calcula Gastos # # #
            string query = "";
            /*
            query += " SELECT a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado "; */

            //query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito, (SUM(m.valor) + a.credito) AS Saldo FROM comovime AS m " +
            query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito,";
            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                //query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
                //query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";
                query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + "2023" + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
                query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";

            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual)).ToString();
                query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
                query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";
            }
            query += " FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado ";
            query += " WHERE idassoc IN('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual)).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual +           "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";                        
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-19', m.vencimento " +
                                                                           "BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual)).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year).ToString() + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19', m.vencimento " +
                                                                           "BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + mesAtual + "-19') ";
            }


            ObjDados.Query = query;
            DataTable dados = ObjDados.RetQuery();

            double saldo = 0;

            if (dados.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dados.Rows[0]["saldo"].ToString()))
                {
                    saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
                }
                else
                {
                    saldo = 0.00;
                }
            }
            else
            {
                saldo = 0.00;
            }


            //return query;
            return saldo;
        }

        public String SaldoAssociadoT()
        {
            BLL ObjDados = new BLL(conectVegas);

            string mesAtual = DateTime.Now.Month.ToString();
            string diaAtual = DateTime.Now.Day.ToString();
#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            //# # # Calcula Gastos # # #
            string query = "";
            /*
            query += " SELECT a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado "; */

            /* query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito, (SUM(m.valor) + a.credito) AS Saldo FROM comovime AS m " +
                      " INNER JOIN associa AS a ON a.idassoc = m.associado " +
                      " WHERE idassoc IN('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";

             if (Convert.ToInt32(diaAtual) < 20)
             {
                 mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                 //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual +           "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";                        
                 query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + (mesAtual + 1) + "-19', m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
             }
             else
             {
                 mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                 //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year).ToString() + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                 query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + (mesAtual + 1) + "-19', m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
             }

             */

            query += " SELECT a.idassoc, a.titular,  SUM(m.valor) AS gastos, a.credmodelo, a.credito,";
            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
                query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                query += "(SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') AS compras, ";
                query += " IF((SELECT SUM(valor) FROM comovime WHERE associado = '" + IdAssoc + "' AND DATA BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') IS NULL, (a.credito), (SUM(m.valor) + a.credito)) AS Saldo ";
            }
            query += " FROM comovime AS m ";
            query += " INNER JOIN associa AS a ON a.idassoc = m.associado ";
            query += " WHERE idassoc IN('" + IdAssoc + "') AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual)).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual +           "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL ";                        
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-19', m.vencimento " +
                                                                           "BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + DateTime.Now.Month + "-19') ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //query += " WHERE m.associado = '" + IdAssoc + "' AND m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year).ToString() + "-" + mesAtual + "-19' AND a.cnscanmom IS NULL AND m.cnscanmom IS NULL";
                query += " AND IF(a.credmodelo IN ('vlparce'), m.vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19', m.vencimento " +
                                                                           "BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + Convert.ToInt32(DateTime.Now.Year + 1).ToString() + "-" + mesAtual + "-19') ";
            }

            ObjDados.Query = query;
            DataTable dados = ObjDados.RetQuery();

            string xQuery = query;

#pragma warning disable CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado
            double saldo = 0;
#pragma warning restore CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado

            if (dados.Rows.Count > 0)
            {
                //saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
                saldo = 10.00;
            }
            else
            {
                saldo = 0.00;
            }

            //return saldo;
            return xQuery;
        }

        public double limiteAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            double limite = 0.00;

            ObjDados.Campo = " credito ";
            ObjDados.Tabela = " associa ";
            ObjDados.Condicao = "WHERE idassoc ='" + IdAssoc + "' AND cnscanmom IS NULL ";

            DataTable dcredito = ObjDados.RetCampos();

            if (!String.IsNullOrEmpty(dcredito.Rows[0]["credito"].ToString()))
            {

                limite = double.Parse(dcredito.Rows[0]["credito"].ToString());
            }
            else
            {
                limite = 0.00;
            }
            return limite;
        }

        public double vendasConvenio(string idConv, string idConv2)
        {
            this.IdConv = idConv;
            string idAcesso = idConv2;
            BLL ObjDados = new BLL(conectVegas);

            double vendas = 0.00;
            string Ms = "";


            ObjDados.Campo = " SUM(valor*-1) AS vendas ";
            ObjDados.Tabela = " comovime AS m ";
            //ObjDados.Condicao = " where (m.convenio = '" + IdConv + "' or exists(SELECT null FROM coconven as c WHERE c.idconven = m.convenio and c.cnpj_cpf = '" + IdConv + "')) AND m.cnscanmom IS NULL and vencimento between " + dtDataInicio() + " and " + dtDataFim();
            ObjDados.Condicao = " where (m.convenio = '" + IdConv + "' or exists(SELECT null FROM coconven as c WHERE c.idconven = m.convenio and c.cnpj_cpf = '" + idConv2 + "')) AND m.cnscanmom IS NULL and vencimento between " + Periodo();

            DataTable dados = ObjDados.RetCampos();

            string query = ObjDados.Campo + " " + ObjDados.Tabela + "" + ObjDados.Condicao;

            if (ObjDados.MsgErro == "")
            {

                if (String.IsNullOrEmpty(dados.Rows[0]["vendas"].ToString()))
                {
                    vendas = 0.00;
                }
                else
                {
                    vendas = double.Parse(dados.Rows[0]["vendas"].ToString());
                }
            }
            else
            {
                Ms = ObjDados.MsgErro;

                //Ver como resolver esse cara. Retornar uma string (a msg de erro) num double (29-11-2021)

            }

            return vendas;
            //return query;
        }

        public String checarAniversario(string Id)
        {
            BLL ObjDados = new BLL(conectVegas);

            //string campos = " associado, iddepen, nome, dtnasci,CONCAT((YEAR(CURDATE()) - (YEAR(dtnasci) + 1)), ' ano(s)') AS Idade,  IF(EXTRACT(MONTH FROM CURDATE()) = EXTRACT(MONTH FROM dtnasci), IF((EXTRACT(DAY FROM CURDATE()) = EXTRACT(DAY FROM dtnasci)), CONCAT(EXTRACT(YEAR FROM CURDATE()) - (EXTRACT(YEAR FROM dtnasci)), ', " + "Hoje é o seu aniversário, Parabéns!!!" + "'), ''), '') AS aniversario, grau";
            string campos = " associado, iddepen, nome,  dtnasci,  CONCAT((YEAR(CURDATE()) - (YEAR(dtnasci)+1)), ' ano(s)') AS Idade,  IF(EXTRACT(MONTH FROM CURDATE()) = EXTRACT(MONTH FROM dtnasci),IF((EXTRACT(DAY FROM CURDATE()) = EXTRACT(DAY FROM dtnasci)),'Aniversariante' ,'Ainda não é o dia do seu aniversário'),'Ainda não chegou seu mês / dia') AS aniversario, IF(grau IN ('01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13'), 'Dependente', 'Titular') AS grau  ";
            string tabela = " asdepen ";
            string condicao = " WHERE associado = '" + Id + "' AND cnscanmom IS NULL ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();            

            string xRet = "";
            try
            {
                if (ObjDados.MsgErro == "")
                {
                    if (dados.Rows.Count > 0)
                    {
                        //xRet += "<p style='width: 350px; min-height: 10px; border: 1px solid #f26907; text-align: center; color: #f26907 '>" + " Titular: " + dados.Rows[0]["aniversario"].ToString() + "</p>";

                        for (int i = 0; i < dados.Rows.Count; i++)
                        {
                            string nome = "";  
                            int indexOf = 0;
                            if (dados.Rows[i]["aniversario"].ToString() == "Aniversariante")
                            {
                                if (dados.Rows[i]["grau"].ToString() == "Dependente")
                                {
                                    nome = dados.Rows[i]["nome"].ToString(); 
                                    indexOf = dados.Rows[i]["nome"].ToString().IndexOf(" ");
                                    xRet += "<div class='divAniver'>";
                                    xRet += "<div class='aniversario'>";                     
                                    xRet += "<p style='width: 100%; margin-left: 20px; min-height: 10px; text-align: left; color: #595959'>" + "Hoje é o dia do seu dependente: " + nome.Substring(0, indexOf) + ". " + "\n" + "</p>";
                                    xRet += "</div>";
                                    xRet += "<ul>";
                                    xRet += "<p>É com muita Satisfação e Alegria que estamos aqui para lhe Desejar:</p>";
                                    xRet += "<br />";
                                    xRet += "<li>Feliz aniversário.</li>";
                                    xRet += "<li>Muitas felicidades.</li>";
                                    xRet += "<li>Toda sorte do mundo.</li>";
                                    xRet += "<li>Que o amor nunca esteja longe.</li>";
                                    xRet += "<li>Que as provações sejam fáceis.</li>";
                                    xRet += "<li>E que os prazeres sejam longos.</li>";
                                    xRet += "<li>Que a vida passe,<br />";
                                    xRet += " mas fique sempre algo<br />";
                                    xRet += " que não tem preço.</li>";
                                    xRet += "<li>E assim nada tenha<br />";
                                    xRet += " nem fim, nem começo.</li>";
                                    xRet += "</ul>";
                                    xRet += "<div id = 'btnVerMsgAniversario' style= 'width: 30px; min-height: 30px; float: right; margin-right: 30px; margin-top: -30px;' >";
                                    xRet += "<input type= 'image' src='../Img/icon/Icon-Mostrar2.png' width='25' height='25' onclick= 'ocultar()' /></div >";
                                    xRet += "</div >";                                    
                                }
                                else
                                {
                                    nome = dados.Rows[i]["nome"].ToString();
                                    indexOf = dados.Rows[i]["nome"].ToString().IndexOf(" ");

                                    xRet += "<div class='divAniver'>";
                                    xRet += "<div class='aniversario'>";                                    
                                    xRet += "<p style='width: 100%; margin-left: 20px; min-height: 10px; text-align: left; color: #595959'>" + nome.Substring(0, indexOf) + ", hoje é o seu dia!" + "</p>";
                                    xRet += "</div>";
                                    xRet += "<ul>";
                                    xRet += "<p>É com muita Satisfação e Alegria que estamos aqui para lhe Desejar:</p>";
                                    xRet += "<br />";
                                    xRet += "<li>Feliz aniversário.</li>";
                                    xRet += "<li>Muitas felicidades.</li>";
                                    xRet += "<li>Toda sorte do mundo.</li>";
                                    xRet += "<li>Que o amor nunca esteja longe.</li>";
                                    xRet += "<li>Que as provações sejam fáceis.</li>";
                                    xRet += "<li>E que os prazeres sejam longos.</li>";
                                    xRet += "<li>Que a vida passe,<br />";
                                    xRet += " mas fique sempre algo<br />";
                                    xRet += " que não tem preço.</li>";
                                    xRet += "<li>E assim nada tenha<br />";
                                    xRet += " nem fim, nem começo.</li>";
                                    xRet += "</ul>";
                                    xRet += "<div id = 'btnVerMsgAniversario' style= 'width: 30px; min-height: 30px; float: right; margin-right: 30px; margin-top: -30px;' >";
                                    xRet += "<input type= 'image' src='../Img/icon/Icon-Mostrar2.png' width='25' height='25' onclick= 'ocultar()' /></div >";
                                    xRet += "</div >";
                                }
                            }
                            else
                            {
                                // xRet += "<div class='aniversario'>";
                                // xRet += "<p style='width: 350px; min-height: 10px; border: 1px solid #f26907; text-align: center; color: #f26907 '>" + " Dependente: " + dados.Rows[i]["aniversario"].ToString() + "</p>";
                                // xRet += "</div>";
                            }

                        }
                    }
                    //xRet += "<p>" + dados.Rows[0]["aniversario"].ToString() + "</p>";
                    //xRet += "<p>" + dados.Rows[0]["nome"].ToString() + "</p>";                
                }
                else
                {
                    xRet += "Deu Erro:" + ObjDados.MsgErro;
                }
            }
            catch (Exception e)
            {
                xRet += "Erro: " + e.Message;
            }

            return xRet;
        }

        public string idCep { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string cBairro { get; set; }
        public string cCidade { get; set; }
        public string UF { get; set; }

        public void buscarCep(string id)
        {
            this.idCep = id;
            string Msg = "";

            using (var ws = new SRCorreios.AtendeClienteClient())
            {
                if (string.IsNullOrEmpty(id))
                {
                    Msg = "Campo Cep Precisa ser Preenchido";
                }
                else
                {
                    try
                    {
                        var Correios = ws.consultaCEP(idCep);

                        //Associado
                        Rua = Correios.end;
                        Complemento = Correios.complemento2;
                        cBairro = Correios.bairro;
                        cCidade = Correios.cidade;
                        UF = Correios.uf;

                    }
                    catch (Exception ex)
                    {
                        Msg = "Erro ao Consultar o CEP!!! " + ex.Message;
                    }
                }
            }
        }//buscarCep

        public void fazerDownload()
        {

            /*Exibir Arquivos no Diretório*/
            DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\Convenios\");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            string arquivos = "";
            string xArq = "";

            xArq += "<div style='margin-top: 30px; width: 600px; height: auto; '>";
            //xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(242,105,7,0), rgba(242,105,7,95));'>" + "Arquivos" + "</p>";
            xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(34,57,111,0), rgba(34,57,111,44));'>" + "Arquivos" + "</p>";
            xArq += "<div style='padding: 10px'>";
            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;
                xArq += "<p style='text-align: left; margin: 5px 0 0 10px; padding: 0 0 0 0; '>";
                xArq += "<div style='width: 20px; height: 20px; float: left'>";
                xArq += "<img style='width: 18px;' src='Img/Icon/ArquivosDownloadsCD.png' />";
                xArq += "</div>";
                xArq += "<div style='width: 300px; height: 20px; float: left;'>";
                xArq += "<p style='text-align: left; margin: 0; margin-left: 10px; padding: 0;'> <a style='' href='" + @"Downloads\Convenios\" + arquivos + "' target=_blanck>" + arquivos + "</a></p>";
                xArq += "</div>";
                xArq += "</p><br>";


                //lblArquivos.Text = xArq;
            }
            xArq += "</div>";
            xArq += "</div>";

            //return xArq;
        }
                
        public String EnviarEmail(string para, string cc, string assunto, string msg)
        {//Método concluído e Testado em: 06-09-2023
            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        
            SmtpClient cliente = new SmtpClient();
            NetworkCredential credenciais = new NetworkCredential();

            //Configurar Cliente            
            cliente.Host = "smtp.asu.com.br";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            //Libera envio de e-mail validando certificados
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //Credenciais de acesso            
            credenciais.UserName = "postmaster@asu.com.br";
            credenciais.Password = "Asu#1969";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            MailMessage Msg = new MailMessage();
            MailMessage To = new MailMessage();

            Msg.IsBodyHtml = true;
            Msg.From = new MailAddress(para);
            Msg.Subject = "Assunto: " + assunto;
            Msg.Body = msg;
            Msg.To.Add(para);

            To.IsBodyHtml = true;
            To.From = new MailAddress(cc);
            To.Subject = "Assunto: " + assunto;
            To.Body = msg;
            To.To.Add(cc);



            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                cliente.Send(To);

                retorno = "Sua mensagem foi enviada com sucesso!!! " + agora;

            }
            catch (Exception ex)
            {
                retorno = "Erro: " + ex.Message;
            }

            //lblResultado.Text = retorno;

            return null;

        }//EnviarEmail
        public String retornaMsg()
        {
            string retorno = "Foi";
            
            return retorno;
        }

    }//class Apoio
}