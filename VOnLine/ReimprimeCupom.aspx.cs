using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace Site.VOnLine
{
    public partial class ReimprimeCupom : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ativarGridVenda();


                this.DataBind();
            }
        }

        protected void ativarGridVenda()
        {

            if (Session.Count > 0)
            {
                if (Session["LoginIdConvenio"].ToString() != "")
                {

                    mvReimprime.ActiveViewIndex = 0;
                    carregarGvTestes();
                }
                else
                {
                    lblMsg.Text = "Deu pau!";
                    //MessageBox.Show(Session["LoginConvenio"].ToString());
                }
            }

        }

        protected void ativarRempressao()
        {
            mvReimprime.ActiveViewIndex = 1;
        }

        protected void carregarGvTestes()
        {
            BLL ObjDados = new BLL(conectVegas);

            string idConvenio = Session["LoginIdConvenio"].ToString();            

            string query = " SELECT c.idmovime AS Autorizacao, d.nome AS Comprador, CONCAT( 'R$ ', FORMAT(SUM(c.valor) * -1, '2', 'pt-Br')) AS Valor , c.parctot AS Parcela,  c.cnscadmom AS Data" +
                           " FROM comovime AS c " +
                           " INNER JOIN associa AS a ON a.idassoc = c.associado " +
                           " INNER JOIN asdepen AS d ON c.dependen = iddepen " +
                           " WHERE c.convenio = '" + idConvenio + "' AND c.cnscanmom IS NULL" +
                           " GROUP BY c.link ORDER BY c.idmovime DESC LIMIT 10";


            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();
            try
            {
                if (dados.Rows.Count > 0)
                {
                    gvVendas.DataSource = ObjDados.RetQuery();
                    gvVendas.DataBind();
                }
                else
                {
                    lblMsg.Text = "Não há dados para exibição!";
                }
            }
            catch (Exception e)
            {
                lblMsg.Text = e.Message;
            }
        }

        //Gera Comprovante de Venda
        public String comprovanteDeVenda(string idAutorizacao)
        {
            BLL ObjDados = new BLL(conectVegas);
            BLL ObjVendas = new BLL(conectVegas);

            //string query = "SELECT * FRM comovime WHERE idmovime = '" + autoriza +"'";
            string query = "SELECT c.idmovime, c.associado, d.nome, d.cnpj_cpf, a.titular, c.depcartao, SUM(c.valor *-1) AS total , c.parcela, c.parctot, c.data, c.cnscadmom, c.convenio " +
                           " FROM comovime AS c " +
                           " INNER JOIN associa AS a ON a.idassoc = c.associado " +
                           " INNER JOIN asdepen AS d ON c.dependen = iddepen " +
                           //" WHERE c.convenio = '" + idConvenio + "' AND c.data BETWEEN '2023-08-01' AND '2023-08-22' GROUP BY c.link ORDER BY c.idmovime ASC ";
                           " WHERE c.idmovime = '" + idAutorizacao + "' GROUP BY c.link";

            ObjDados.Query = query;

            string qvendas = " SELECT c.idmovime, c.associado, c.depcartao, c.valor *-1 AS valor , c.parcela, c.parctot, c.data, c.cnscadmom, c.convenio " +
                           " FROM comovime AS c " +
                           " WHERE c.link = '" + idAutorizacao + "'";

            ObjVendas.Query = qvendas;

            DataTable dados = ObjDados.RetQuery();
            DataTable vendas = ObjVendas.RetQuery();
            string xRet = "";

            int Totalparc = Convert.ToInt32(dados.Rows[0]["parctot"]);
            double TotalVenda = 0.00;

            //Sumariza Vendas da Autorização Selecionada
            for (int i = 0; i < Totalparc; i++)
            {
                TotalVenda = TotalVenda + Convert.ToDouble(vendas.Rows[i]["valor"]);
            }

            xRet += "<section Class='CompVenda'>";
            //xRet += "<table style='font-size: 10px; font-weight: 700;'>"; Criar: Parametro pra atribuir tamanho da font para impressão
            xRet += "<table style='font-size: 10px; font-weight: 700;'>";
            xRet += "<thead style='vertical-align:bottom' >";
            xRet += "<tr style = 'height:auto;' >";
            xRet += "<td colspan='2' class='cvTopico' > 2º Via - Comprovante de Venda</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td colspan='2' class='cvTopico' >Via do Convênio</td>";
            xRet += "</tr>";
            xRet += "</thead>";
            xRet += "<tbody>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["idmovime"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["convenio"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["depcartao"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Valor</td>";
            xRet += "<td style='text-align: center;' >" + TotalVenda.ToString("C2") + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["parctot"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Data:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["cnscadmom"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>CPF</td>";
            xRet += "<td id='cpf2' style='text-align: center;' class='tFontExtrato' >" + dados.Rows[0]["cnpj_cpf"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr> ";
            xRet += "<tr>";
            xRet += "<td colspan = '2' id='nome2' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + dados.Rows[0]["nome"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
            xRet += "<tr style='height:auto;' >";
            xRet += "<td colspan='2' class='cvTopico' > 2º Via - Comprovante de Venda</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td colspan='2' class='cvTopico' >Via da ASU</td>";
            xRet += "</tr>";
            /**/
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["idmovime"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["convenio"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["depcartao"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Valor</td>";
            xRet += "<td style='text-align: center;' >" + TotalVenda.ToString("C2") + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["parctot"].ToString() + " </td>";
            xRet += "</tr >";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Data:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["cnscadmom"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>CPF</td>";
            xRet += "<td  id='cpf' style ='text-align: center;' class='tFontExtrato' >" + dados.Rows[0]["cnpj_cpf"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr>";
            xRet += "<td colspan='2' id='nome' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + dados.Rows[0]["nome"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
            xRet += "<tr style = 'height:auto;' >";
            /**/
            xRet += "<td colspan='2' class='cvTopico' >2º Via - Comprovante de Venda</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td colspan='2' class='cvTopico' >Via do Associado</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["idmovime"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["convenio"].ToString() + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["depcartao"].ToString() + " </td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Valor</td>";
            xRet += "<td style='text-align: center;' >" + TotalVenda.ToString("C2") + "</td>";
            xRet += "</tr>";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["parctot"].ToString() + " </td>";
            xRet += "</tr >";
            xRet += "<tr>";
            xRet += "<td style='width: 40%; text-align: right;'>Data:</td>";
            xRet += "<td style='text-align: center;' >" + dados.Rows[0]["cnscadmom"].ToString() + " </td>";
            xRet += "</tr>";
            //xRet += "<tr>";
            //xRet += "<td style='width: 40%; text-align: right;'>Saldo</td>";
            //xRet += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cSaldo) / 100).ToString("C2")) + "</td>";
            //xRet += "</tr>";
            xRet += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
            xRet += "</tbody>";
            xRet += "</table>";
            xRet += "</section>";



            return xRet;
        }

        //Processo para Reimprimir Venda
        protected void reimprimirVenda(object sender, EventArgs e)
        {
            ativarRempressao();

            string autorizacao = gvVendas.SelectedRow.Cells[1].Text;

            lblAutoriza.Text = comprovanteDeVenda(autorizacao);

        }//reimprimirVenda

        protected void vontarGridVendas(object sender, EventArgs e)
        {
            lblAutoriza.Text = String.Empty;

            mvReimprime.ActiveViewIndex = 0;
        }

        protected void voltarVOConvenio(object sender, EventArgs e)
        {
            if (Session["VoceOnLine"] != null)
            {
                Response.Redirect("VoceOnLine_New.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        public String Titulo()
        {
            string retorno = "";

            if (Session["VoceOnLine"].ToString() != null)
            {
                retorno += Session["LoginConvenio"].ToString();
                //retorno += Session["LoginUsuario"].ToString();
            }
            else
            {
                retorno += "Vazio";
            }

            return retorno;
        }

    }
}