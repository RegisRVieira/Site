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
using Site.App_Code;
using System.Configuration;
using System.Data;
using System.Net;

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
            Document doc = new Document(PageSize.A4); //Criando e estipulando o tipo de folha
            doc.SetMargins(40, 40, 40, 80); //Estipulando o espaçamento das margens
            doc.AddCreationDate();

            string caminho = @"K:\Projetos\Web\Site\Site\Downloads\" + "Arquivo_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";

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
    }
}