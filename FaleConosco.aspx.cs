using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Site.App_Code;
using System.Net.Mail;
using System.Net;
namespace Site
{
    public partial class FaleConosco : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];

        protected void Page_Load(object sender, EventArgs e)
        {

            this.DataBind();
        }

        protected void fazerLogof(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }

        protected void EMailFaleConosco(object sender, EventArgs e)
        {
            BLL ObjEmail = new BLL(conectSite);

            ObjEmail.Campo = " * ";
            ObjEmail.Tabela = " st_fc_email ";            

            string xRet = " ";
            int linha = 0;
            
            DataTable dados = ObjEmail.RetCampos();



            if (ObjEmail.MsgErro == "")
            {
                linha = dados.Rows.Count;
            }
            else
            {
                lblResultado.Text =  ObjEmail.MsgErro.ToString();
            }
            

            //xRet += "< input runat = 'server' type = 'radio' id = " + dados.Rows[i]["ID"] +
            //        " name = 'destino' value = " + dados.Rows[i]["ID"] + "checked= '' > " + dados.Rows[i]["descricao"] + "< br >";


            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            SmtpClient cliente = new SmtpClient();
            NetworkCredential credenciais = new NetworkCredential();

            //Configurar Cliente
            //cliente.Host = "smtp.gmail.com";
            cliente.Host = "smtp.asu.com.br";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            //Libera envio de e-mail validando certificados
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Credenciais de acesso
            //credenciais.UserName = "dinhorod@gmail.com";
            credenciais.UserName = "reginaldo@asu.com.br";
            credenciais.Password = "c142795";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            string para = " ";
            MailMessage Msg = new MailMessage();


            string xMsg = "";
            /*
            xMsg += "<table>";
            xMsg += "<caption style='font-size: 1.4em; color: #22396f;'>" + "Fale Conosco" + "</caption>";
            xMsg += "<thead style='margin: 0; padding: 0; width: 300px; height: 100px; background-color: #bbb;'>";
            xMsg += "<tr style='color: #f26907;'>" + "Olá, " + enviar_nome.Value + "</tr>";
            xMsg += "<tr style:'margin: 0; padding: 0;'>" + "Telefone: " + enviar_telefone.Value + "</tr>";
            xMsg += "<tr style:'margin: 0; padding: 0;'>" + "E-mail: " + enviar_email.Value + "</tr>";
            xMsg += "<thead>";
            xMsg += "<tbody  style='margin: 0; padding: 0; margin-top: 20px; width: 500px; height: 600px;'>";
            xMsg += "<tr>";
            xMsg += "<td style:'margin: 0; padding: 0;>" + corpo_mensagem.Value + "</td>";
            xMsg += "</tr>";
            xMsg += "</tbody>";
            xMsg += "</table>";
            */

            xMsg += "<fieldset style='width: 600px; background-color: #f26907; '>";
            xMsg += "<div style='margin: 0; padding: 0; width: 300px; height: 100px; background-color: #bbb;'>";
            xMsg += "<p style='font-size: 1.4em; color: #22396f;'>" + "Fale Conosco" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Olá, " + enviar_nome.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + enviar_telefone.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + enviar_email.Value + "</p>";
            xMsg += "</div>";
            xMsg += "<div style='margin-top: 20px; width: 500px; height: 600px;'>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + corpo_mensagem.Value.ToString() + "</p>";
            xMsg += "<h1 style='color: red' >" + "Você Acaba de Receber o Primeiro e-mail enviado pelo Fale Conosco do novo Site da ASU" + "</h1> " + agora;
            xMsg += "</div>";
            xMsg += "</fieldset>";

            //Pegar valor do Radio posicionado
            if (destino_atendimento.Checked || destino_convenio.Checked || destino_financeiro.Checked || destino_juridico.Checked || destino_secretaria.Checked || destino_jornal.Checked)
            {
                if (destino_atendimento.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "1")
                        {
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];
                            //para = "atendimento@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            para = "reginaldo@asu.com.br";

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Atendimento" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);
                        }
                    }
                }
                if (destino_convenio.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "2")
                        {
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];
                            //para = "convenios@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            para = "reginaldo@asu.com.br";

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Convênio Selecionado, Parabéns!!!" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);
                        }
                    }
                }
                if (destino_financeiro.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "3")
                        {
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];
                            //para = "tesouraria@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            para = "reginaldo@asu.com.br";

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Financeiro Selecionado, Envie sua mensagem!!!" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);

                        }
                    }
                }
                if (destino_juridico.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "4")
                        {
                            //para = "juridico@asu.com.br";
                            para = "reginaldo@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Juridico Selecionado, Pergunte ao advogado!!!" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);
                        }
                    }
                }
                if (destino_secretaria.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "5")
                        {
                            //para = "secretaria@asu.com.br";
                            para = "reginaldo@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Secretaria Selecionado, O que você precisa???" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);

                        }
                    }
                }
                if (destino_jornal.Checked)
                {
                    for (int i = 0; i < linha; i++)
                    {
                        if (dados.Rows[i]["id"].ToString() == "6")
                        {
                            //para = "jornal@asu.com.br";
                            para = "reginaldo@asu.com.br";
                            //para = dados.Rows[i]["email"].ToString();
                            xRet += " " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"];

                            Msg.IsBodyHtml = true;
                            Msg.From = new MailAddress(para);
                            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
                            Msg.Body = xMsg;

                            MessageBox.Show("Jornal Selecionado, Pode ler!!!" + " ---- " + dados.Rows[i]["id"] + " - " + dados.Rows[i]["email"] + " - " + dados.Rows[i]["descricao"]);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("É necessário selecionar um Destino!");
            }

            Msg.To.Add(para);

            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                lblResultado.Text = "Sua mensagem foi enviada com sucesso!!! " + agora;
                MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

            }
            catch (Exception ex)
            {
                lblResultado.Text = "Ocorreu problemas no envio da sua mensagem" + ex;
            }
            finally { cliente.Dispose(); }
            lblResultado.Text = "Enviado para: " + para;
            lblFalta.Text = "1 - Formatar o Corpo da Mensagem (Formatação HTML)" +
                            "2 - Ajustar Método de captura do e-mail. Buscar e-mail na base de dados";
        }


    }//FIM
}