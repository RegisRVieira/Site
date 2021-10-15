using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;

namespace Site.App_Code
{
    public class Apoio
    {
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
        public string gerarPdf()
        {
            string PdfGerado = xPdf;            

            Document pdf = new Document(PageSize.A4);
            pdf.SetMargins(40,40,40,80);
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
    }
}