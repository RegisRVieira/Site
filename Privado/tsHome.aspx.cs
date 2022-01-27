using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Site.App_Code;
using System.Windows.Forms;
using System.IO;

namespace Site.Privado
{
    public partial class tsHome : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void acessarPagSorteio(object sender, EventArgs e)
        {
            Response.Redirect("tsSorteio.aspx");
        }
        protected void relizarLogin(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }
        protected void validarSorteio(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " * ";
            string tabela = " ts_teste";
            string condicao = "";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            MessageBox.Show(dados.Rows.Count.ToString());

            if (ObjDados.MsgErro == "")
            {                
                if (dados.Rows.Count == 0)
                {
                    iIdentificaSorteio.Value = "1";
                }
                else
                {
                    iIdentificaSorteio.Value = (Convert.ToInt32(dados.Rows[0]["sorteio"].ToString()) + 1).ToString();
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }
        }
        protected void cadastrarNumeros(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectSite);

            int numInicio = Convert.ToInt32(iNumInicio.Value);
            int qtdNumeros = Convert.ToInt32(iQtdeNumeros.Value);
            string identifSorteio = "";

            int nSorteio = (numInicio - 1);

            string xRet = "";
            string xCont = "";

            string tabela = " ";
            string campos = " ";
            string valores = "";

            MessageBox.Show("Será cadastrado: " + qtdNumeros + " números! ");

            tabela = " ts_teste ";
            campos = " numero, descricao, sorteio, cadusu, cadmom ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;

            DataTable dados = ObjDados.RetCampos();

            if (ObjDados.MsgErro == "")
            {
                if (dados.Rows.Count == 0)
                {
                    identifSorteio = "1";
                }
                else
                {
                    identifSorteio = (Convert.ToInt32(dados.Rows[0]["sorteio"].ToString()) + 1).ToString();
                }
            }
            else
            {
                MessageBox.Show(ObjDados.MsgErro);
            }


            for (int i = 0; i < qtdNumeros; i++)
            {
                nSorteio++;

                if (i < (qtdNumeros - 1))
                {

                    valores += String.Format("('" + nSorteio + "'," +
                                            "'" + iDescricao.Value + "'," +
                                            "'" + identifSorteio + "'," +
                                            "'" + "Sistema" + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "')," + "\n");
                }
                else
                {
                    valores += String.Format("('" + nSorteio + "'," +
                                            "'" + iDescricao.Value + "'," +
                                            "'" + identifSorteio + "'," +
                                            "'" + "Sistema" + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "')" + "\n");
                }
                //lblNumSorteio.Text += i.ToString();
            }

            string sql = "";

            ///sql.cop

            ObjDados.Valores = valores;

            ObjDados.InsertEmLote(tabela, campos, valores);

            //MessageBox.Show("INSERT INTO " + tabela + "(" + campos + ")" + " VALUES " + valores + "");

            BLL ObjSorteio = new BLL(conectSite);

            string con_tabela = " ts_teste ";
            string con_campos = " * ";
            string con_condicao = " WHERE sorteio = '" + identifSorteio + "' ";

            ObjSorteio.Tabela = con_tabela;
            ObjSorteio.Campo = con_campos;
            ObjSorteio.Condicao = con_condicao;

            DataTable dSort = ObjSorteio.RetCampos();


            int contQtde = 0;

            if (ObjSorteio.MsgErro == "")
            {

                if (dSort.Rows[0]["sorteio"].ToString() == identifSorteio)
                {
                    for (int i = 0; i < qtdNumeros; i++)
                    {
                        contQtde++;
                    }
                    MessageBox.Show("Cadastro Finalizado com Sucesso!!! " + "Número do Sorteio: " + identifSorteio +", com: " + contQtde + " Registros");

                    iIdentificaSorteio.Value = String.Empty;
                    iNumInicio.Value = String.Empty;
                    iQtdeNumeros.Value = String.Empty;
                    iDescricao.Value = String.Empty;
                }
                else
                {
                    for (int i = 0; i < qtdNumeros; i++)
                    {
                        contQtde++;
                    }
                    MessageBox.Show("Ocorreu algum erro!" + "Número do Sorteio: " + identifSorteio + " = " + dados.Rows[0]["sorteio"].ToString() + " - " + contQtde + ", Registros");
                }

            }
            else
            {
                MessageBox.Show(ObjSorteio.MsgErro);
            }           

        }
    }
}