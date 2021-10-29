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
using System.Configuration;

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


        public String Periodo()
        {
            string mesAtual = DateTime.Now.Month.ToString();
            string anoAtual = DateTime.Now.Year.ToString();
            Xret = "";

            if (Mes == mesAtual)
            {

                if (Mes == "1") //Janeiro
                {
                    Xret += "'"+(Convert.ToInt32(Ano) - 1) + "-" + "12" + "-20' AND ";
                    Xret += "'"+Ano + "-" + (Convert.ToInt32(Mes)).ToString() + "-19'";
                }
                else
                {
                    Xret += "'"+ Ano + "-" + (Convert.ToInt32(mesAtual) - 1).ToString() + "-20' AND ";
                    Xret += "'" +Ano + "-" + (Convert.ToInt32(mesAtual)).ToString() + "-19'";
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
                    if (Convert.ToInt32(mesAtual) < Convert.ToInt32(mesAtual))
                    {
                        Xret += "'" + Ano + "-" + (Convert.ToInt32(Ano) - 1).ToString() + "-20' AND ";
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

            ObjDados.Campo = " SUM(valor) AS gastos ";
            ObjDados.Tabela = " comovime ";
            //ObjGastos.Condicao = " WHERE associado = '2747' AND vencimento BETWEEN '2021-10-20' AND '2021-11-19' "; //Exemplo                                        

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + mesAtual + "-20' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-19' ";
            }
            else
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                ObjDados.Condicao = " WHERE associado = '" + IdAssoc + "' AND vencimento BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-20' AND '" + DateTime.Now.Year + "-" + mesAtual + "-19' ";
            }

            DataTable dados = ObjDados.RetCampos();
            //MessageBox.Show("select " + ObjGastos.Campo + " FROM " + ObjGastos.Tabela + " " + ObjGastos.Condicao);
            if (dados.Rows.Count > 0)
            {

                gastos = double.Parse(dados.Rows[0]["gastos"].ToString()) * -1;
            }
            else
            {
                gastos = 0.00;
            }

            return gastos;
        }
    }
}