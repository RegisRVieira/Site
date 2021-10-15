using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Site.App_Code;
using System.Configuration;
using System.Data;
using System.Net;
using ceTe.DynamicPDF.HtmlConverter;

namespace Site
{
    public partial class testPdf : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void gerarPDF(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string xRet = "";

            string campos = " * ";
            string tabela = " st_conteudo";
            string condicao = "WHERE id = '74'";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable d_dados = ObjDados.RetCampos();

            xRet += "Título: " + d_dados.Rows[0]["titulo"] + "\n";
            xRet += "Introdução: " + d_dados.Rows[0]["introducao"] + "\n";
            xRet += "Conteúdo: " + d_dados.Rows[0]["conteudo"] + "\n";
            xRet += "Complemento: " + d_dados.Rows[0]["complemento"] + "\n";
            xRet += "Conlusão: " + d_dados.Rows[0]["conclusao"] + "\n";


            //Criar Arquivo PDF
            Document doc = new Document(); //Criando e estipulando o tipo de folha
            doc.SetMargins(40, 40, 40, 80); //Estipulando o espaçamento das margens
            doc.AddCreationDate();

            string caminho = @"K:\Projetos\Web\Site\Site\Downloads\" + "Arquivo_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";

            string caminho2 = ConfigurationManager.AppSettings["caminhoArquivoPdf"].ToString();

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            //Escrever no Arquivo PDF

            doc.Open();

            string dados = "";

            Paragraph paragrapfh = new Paragraph(dados, new Font(Font.NORMAL, 16));

            paragrapfh.Alignment = Element.ALIGN_CENTER;

            paragrapfh.Add("Este é meu primeiro Arquivo PDF. Foi criado com a sua ajuda, obrigado!!!" + "\n");

            paragrapfh.Font = new Font(Font.NORMAL, 16, (int)System.Drawing.FontStyle.Bold);
            paragrapfh.Add("Negrito ");

            paragrapfh.Font = new Font(Font.NORMAL, 18, (int)System.Drawing.FontStyle.Regular);
            paragrapfh.Add("\n" + "Sem Negrito" + "\n");

            paragrapfh.Add(xRet);

            doc.Add(paragrapfh);
            doc.Close();

            //System.Diagnostics.Process.Start(caminho); //Abre o arquivo

            /*Exibir Arquivos no Diretório*/
            DirectoryInfo diretorio = new DirectoryInfo(@"K:\Projetos\Web\Site\Site\Downloads");
            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            string arquivos = "";
            string xArq = "";
            //Listar os arquivos
            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;

                xArq += "<div>";
                xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>" + arquivos;
                xArq += "</a><br>";
                xArq += "</div>";

                lbArquivos.Text = xArq;
            }

            //MessageBox.Show("Veja lá, até aqui veio! " + DateTime.Now.ToString() + " *** " + arquivos);

        }

        public void gerarPDF2(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            ObjDados.Tabela = "st_usuario";
            ObjDados.Campo = " * ";

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                xRet += "<p>" + dados.Rows[i]["Nome"].ToString() + "</p>";
            }

            lbArquivos.Text = xRet;

            //Criar Documento PDF
            Document document = new Document();
            document.SetMargins(3, 2, 3, 2);

            //Indicar local onde o documento será armazenado
            string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + "CriandoPdf.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(caminho, FileMode.Create));

            //Abre o documento para edição
            document.Open();

            PdfPTable table = new PdfPTable(3);

            Font fonte = FontFactory.GetFont(BaseFont.HELVETICA, 10);

            Paragraph coluna1 = new Paragraph("Nome", fonte);
            Paragraph coluna2 = new Paragraph("Idade", fonte);
            Paragraph coluna3 = new Paragraph("Cidade", fonte);

            var cell1 = new PdfPCell();
            var cell2 = new PdfPCell();
            var cell3 = new PdfPCell();

            cell1.AddElement(coluna1);
            cell2.AddElement(coluna2);
            cell3.AddElement(coluna3);

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);


            List<Apoio> pessoas = new List<Apoio>();

            Apoio pessoa1 = new Apoio();
            for (int i = 0; i< dados.Rows.Count; i++) {
                pessoa1.Nome = dados.Rows[i]["Nome"].ToString();
                pessoa1.Idade = dados.Rows[i]["Usuario"].ToString();
                pessoa1.Cidade = dados.Rows[i]["Senha"].ToString(); 
            }
            

            pessoas.Add(pessoa1);

            foreach (var pessoa in pessoas)
            {
                Phrase nome = new Phrase(pessoa.Nome);
                var cell = new PdfPCell(nome);
                table.AddCell(cell);

                Phrase idade = new Phrase(pessoa.Idade);
                cell = new PdfPCell(idade);
                table.AddCell(cell);

                Phrase cidade = new Phrase(pessoa.Cidade);
                cell = new PdfPCell(cidade);
                table.AddCell(cell);
            }

            //Cria Tabela e Fecha Documento
            document.Add(table);
            document.Close();

        }
        public void converterHtmlPdf(object sender, EventArgs e)
        {
            string xRet = "";

            xRet += "<div style='width: 500px; height: 500px; background-color: #f26907;'>" + "Div1" + "</div>";
            xRet += "<div style='width: 500px; height: 500px; background-color: #239634;'>" + "Div2" + "</div>";
            xRet += "<p> Criando PDF a Partir de Um Html </p>";

            lbArquivos.Text = xRet;

            string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + "PdfEmUmaLinha.pdf";
            //Converter.Convert(new Uri("http://www.uol.com.br"), caminho);
            Converter.Convert(new Uri("https://localhost:44320/VoceOnLine1.aspx"), caminho);          
        }

        public void pdfClaudia(object sender, EventArgs e) {

            string xRet = "";

            xRet += "<div style='width: 500px; height: 500px; background-color: #f26907;'>" + "Div1" + "</div>";
            xRet += "<div style='width: 500px; height: 500px; background-color: #239634;'>" + "Div2" + "</div>";
            xRet += "<p> Criando PDF a Partir de Um Html </p>";

            lbArquivos.Text = xRet;



            Document pdfDoc = new Document();
            pdfDoc.SetMargins(2, 3, 2, 3);

            /*
             string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + "CriandoPdf.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(caminho, FileMode.Create));
             */


            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            HTMLWorker obj = new HTMLWorker(pdfDoc);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);

            PdfPTable table = new PdfPTable(4);

            pdfDoc.Open();

            
            pdfDoc.Add(table);

            table.AddCell("coluna 1");
            table.AddCell("coluna 2");
            table.AddCell("coluna 3");
            table.AddCell("coluna 4");

            pdfDoc.Close();

            Response.Clear();

            Response.OutputStream.Flush();
            Response.OutputStream.Close();

            Response.End();
        }
    }
}
