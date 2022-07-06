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

        public double SaldoAssociado()
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


            gastos = double.Parse(dGastos.Rows[0]["gastos"].ToString());
            credito = double.Parse(dCredito.Rows[0]["credito"].ToString());

            saldo = credito - ((gastos) * -1);

            return saldo;
        }

        public double limiteAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);

            double limite = 0.00;

            ObjDados.Campo = " credito ";
            ObjDados.Tabela = " associa ";
            ObjDados.Condicao = "WHERE idassoc ='" + IdAssoc + "' AND cnscanmom IS NULL ";

            DataTable dcredito = ObjDados.RetCampos();

            limite = double.Parse(dcredito.Rows[0]["credito"].ToString());

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

#pragma warning disable CS0219 // A variável "msgNiver" é atribuída, mas seu valor nunca é usado
            string msgNiver = "";
#pragma warning restore CS0219 // A variável "msgNiver" é atribuída, mas seu valor nunca é usado


            //string campos = " associado, iddepen, nome, dtnasci,CONCAT((YEAR(CURDATE()) - (YEAR(dtnasci) + 1)), ' ano(s)') AS Idade,  IF(EXTRACT(MONTH FROM CURDATE()) = EXTRACT(MONTH FROM dtnasci), IF((EXTRACT(DAY FROM CURDATE()) = EXTRACT(DAY FROM dtnasci)), CONCAT(EXTRACT(YEAR FROM CURDATE()) - (EXTRACT(YEAR FROM dtnasci)), ', " + "Hoje é o seu aniversário, Parabéns!!!" + "'), ''), '') AS aniversario, grau";
            string campos = " associado, iddepen, nome,  dtnasci,  CONCAT((YEAR(CURDATE()) - (YEAR(dtnasci)+1)), ' ano(s)') AS Idade,  IF(EXTRACT(MONTH FROM CURDATE()) = EXTRACT(MONTH FROM dtnasci),IF((EXTRACT(DAY FROM CURDATE()) = EXTRACT(DAY FROM dtnasci)),'Aniversariante' ,'Ainda não é o dia do seu aniversário'),'Ainda não chegou seu mês / dia') AS aniversario, IF(grau IN ('01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13'), 'Dependente', 'Titular') AS grau  ";
            string tabela = " asdepen ";
            string condicao = " WHERE associado = '" + Id + "' AND cnscanmom IS NULL ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";
            if (ObjDados.MsgErro == "")
            {
                if (dados.Rows.Count > 0)
                {
                    //xRet += "<p style='width: 350px; min-height: 10px; border: 1px solid #f26907; text-align: center; color: #f26907 '>" + " Titular: " + dados.Rows[0]["aniversario"].ToString() + "</p>";

                    for (int i = 0; i < dados.Rows.Count; i++)
                    {
                        //if (dados.Rows[i]["aniversario"].ToString() != "")


                        //xRet += "<p style='width: 350px; min-height: 10px; border: 1px solid #f26907; text-align: center; color: #f26907 '>" + " Todos: " + dados.Rows[i]["nome"].ToString() + ", " + dados.Rows[i]["aniversario"].ToString() + "</p>";

                        if (dados.Rows[i]["aniversario"].ToString() == "Aniversariante")
                        {
                            if (dados.Rows[i]["grau"].ToString() == "Dependente")
                            {
                                xRet += "<div class='aniversario'>";
                                xRet += "<p style='width: 100%; min-height: 10px; text-align: left; color: #f26907 '>" + "Hoje é o aniversário do seu dependente: " + dados.Rows[i]["nome"].ToString() + ". Desejamos Muitas felicidades e realizações. " + "</p>";
                                xRet += "</div>";
                            }
                            else
                            {
                                xRet += "<div class='aniversario'>";
                                xRet += "<p style='width: 100%; min-height: 10px; text-align: left; color: #f26907 '>" + "Parabéns " + dados.Rows[i]["nome"].ToString() + ", hoje é o seu dia! Muitas Felicidades e Realizações. " + "</p>";
                                xRet += "</div>";
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


    }//class Apoio
}