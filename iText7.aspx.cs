using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Site.App_Code;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace Site
{
    public partial class iText7 : System.Web.UI.Page
    {
        public static readonly string DEST = @"K:\Projetos\Web\Site\Site\Downloads\x.pdf";
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        protected void Page_Load(object sender, EventArgs e)
        {
            //FileInfo file = new FileInfo(DEST);
            //file.Directory.Create();            

        }

        protected void gerarPDF(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);

            ObjDados.Campo = " * ";
            ObjDados.Tabela = " associa ";
            ObjDados.Condicao = " WHERE idassoc IN (" + "'" + "2747" + "'" + ") ";

            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            xRet += "<div>";
            xRet += "<p>" + dados.Rows[0]["idassoc"].ToString() + "</p>";
            xRet += "<p>" + dados.Rows[0]["titular"].ToString() + "</p>";
            xRet += "</div>";

            lblMsg.Text = xRet;

            //FileInfo file = new FileInfo(DEST);
            //file.Directory.Create();
            string dest = @"K:\Projetos\Web\Site\Site\Downloads\x" + DateTime.Now.Day + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            float[] colunas = { 1, 5, 3, 3 }; //Esse cara 

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Table tableHeader = new Table(2);
            Table table = new Table(UnitValue.CreatePercentArray(colunas)).UseAllAvailableWidth(); //Com esse cara. Vão determinar a largura da coluna



            var cel1 = dados.Rows[0]["idassoc"].ToString();
            var cel2 = dados.Rows[0]["titular"].ToString();
            var cel3 = dados.Rows[0]["ie_rg"].ToString();
            var cel4 = dados.Rows[0]["cnpj_cpf"].ToString();



            table.AddCell(cel1).SetFontSize(10);
            table.AddCell(cel2);
            table.AddCell(cel3);
            table.AddCell(cel4);

            /*
            for (int i = 0; i < 16; i++)
            {
                table.AddCell("hi");
            }
            */

            doc.Add(table);

            doc.Close();

            //#######



            //#######

        }

        protected void gerarPdfExtrato(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);


            string xRet = "";
            string xPdf = "";



            ObjDados.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito, " +
                             " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '26870730830' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '26870730830'))) AND vencimento BETWEEN '2021-09-20' AND '2021-10-19' LIMIT 1) AS gastos  ";
            ObjDados.Tabela = " comovime AS c  ";
            ObjDados.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                            " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                            " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
            ObjDados.Condicao = " WHERE (a.cnpj_cpf ='26870730830' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '26870730830'))) AND c.vencimento BETWEEN '2021-09-20'  AND '2021-10-19' ";

            //## Extrato ##

            DataTable dados = ObjDados.RetCampos();

            double gastos = 0;
            double limite = 0;
            double saldo = 0;

            double totalExtrato = double.Parse(dados.Rows[0]["gastos"].ToString()) * (-1);

            xRet += "<p>" + dados.Rows[0]["titular"].ToString() + "";

            lblMsg.Text = xRet;

            string dest = @"K:\Projetos\Web\Site\Site\Downloads\x" + DateTime.Now.Day + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            float[] colunas = { 1, 4, 7, 2, 7, 1, 2 }; //Esse cara 
            float[] cHeader = { 2, 8, 1, 1, 1, 1, 1 };

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            
            Table table = new Table(UnitValue.CreatePercentArray(colunas)).UseAllAvailableWidth(); //Com esse cara. Vão determinar a largura da coluna

            string simg = @"K:\Projetos\Web\Site\Site\Img\Logo.png";

            //**

            //Cabeçalho
            Table tableHeader = new Table(2);
            tableHeader.AddCell("Logo");
            tableHeader.AddCell("Extrato Mensal");
            string textcabecaclho = "";

            textcabecaclho += "Olá, " + dados.Rows[0]["titular"].ToString() + "\n" + "Este é o seu Extrato para o período selecionado!";

            Paragraph cabecalho = new Paragraph(textcabecaclho);
                       

            // Rodapé
            Table tableFooter = new Table(2);
            tableFooter.AddCell("Total");
            tableFooter.AddCell("R$ " + totalExtrato);


            //# # Criar Células da Tabela # #
            table.AddCell("Autorização");
            table.AddCell("Momento");
            table.AddCell("Convênio");
            table.AddCell("Cartão");
            table.AddCell("Comprador");
            table.AddCell("Parc.");
            table.AddCell("Valor");

            if (dados.Rows.Count > 0)
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);

                    var cel1 = dados.Rows[i]["idmovime"].ToString();
                    var cel2 = dados.Rows[i]["cnscadmom"].ToString();
                    var cel3 = dados.Rows[i]["conveniado"].ToString();
                    var cel4 = dados.Rows[i]["cartao"].ToString() + "xx";
                    var cel5 = dados.Rows[i]["comprador"].ToString();
                    var cel6 = dados.Rows[i]["parcela"].ToString() + "/" + dados.Rows[i]["parctot"].ToString();
                    var cel7 = " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture).ToString();

                    
                    table.AddCell(cel1).SetFontSize(7);
                    table.AddCell(cel2);
                    table.AddCell(cel3);
                    table.AddCell(cel4);
                    table.AddCell(cel5);
                    table.AddCell(cel6);
                    table.AddCell(cel7);
                }
            }
            else
            {
                lblMsg.Text = ObjDados.MsgErro;
            }
            var cel8 = "";
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell("Total: ");
            table.AddCell("R$ " + totalExtrato);

            doc.Add(cabecalho);
            doc.Add(table);
            //doc.Add(tableFooter);

            doc.Close();



        }
    }
}