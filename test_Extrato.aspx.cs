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
    }//Fim
}