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