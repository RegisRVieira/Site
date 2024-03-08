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
            if (!Page.IsPostBack)
            {
                ativarFormFaleConosco();
            }

            this.DataBind();
        }

        protected void fazerLogof(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }

        protected void ativarFormFaleConosco()
        {
            mwFalecosnosco.ActiveViewIndex = 0;
        }
        protected void ativarConcluido()
        {
            mwFalecosnosco.ActiveViewIndex = 1;
        }

        protected void voltarHome(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
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
            credenciais.UserName = "postmaster@asu.com.br";
            credenciais.Password = "Asu#1969";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            string para = " ";
            string cc = " ";

            MailMessage Msg = new MailMessage();
            MailMessage To = new MailMessage();


            string xMsg = "";
            string destino = "";

            if (destino_atendimento.Checked)
            {
                destino = destino_atendimento.Value;
                //para = "reginaldo@asu.com.br, regis@asu.com.br";
                //para = "reginaldo@asu.com.br";
                para = "atendimento@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_convenio.Checked)
            {
                destino = destino_convenio.Value;
                para = "convenios@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_financeiro.Checked)
            {
                destino = destino_financeiro.Value;
                para = "tesouraria@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_jornal.Checked)
            {
                destino = destino_jornal.Value;
                para = "jornal@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_juridico.Checked)
            {
                destino = destino_juridico.Value;
                para = "juridico@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_secretaria.Checked)
            {
                destino = destino_secretaria.Value;
                para = "secretaria@asu.com.br";
                cc = "informatica@asu.com.br";
            }

            xMsg += "<fieldset style='width: 600px;'>";
            xMsg += "<div style='margin: 0; padding: 0; width: 600px; min-height: 100px; background-color: #F0F0F0;'>";
            xMsg += "<p style='font-size: 1.4em; color: #22396f;'>" + "Fale Conosco: " + destino + "</p>";

            /*Testes
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Enviado por: " + "Nome" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Telefone: " + "(14) 9-9696 8565" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "E-mail: " + "fulano@domino.com.br" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Empresa: " + "Empresa" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Momento: "+ agora + "</p> ";
            xMsg += "</div>";
            xMsg += "<div style='margin-top: 20px; width: 500px; min-height: 200px;'>";
            xMsg += "<p style = 'margin: 0; padding: 0; margin-top: 20px;' > " + "Assunto: " + "Assunto" + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Corpo da Mensagem" + "</p>";
            */

            xMsg += "<p style='margin: 0; padding: 0;'>" + "Enviado por: " + enviar_nome.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Telefone: " + enviar_telefone.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "E-mail: " + enviar_email.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Empresa: " + enviar_empresa.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Momento: " + agora + "</p> ";
            xMsg += "</div>";
            xMsg += "<div style='margin-top: 20px; width: 500px; min-height: 200px;'>";
            xMsg += "<p style = 'margin: 0; padding: 0; margin-top: 20px;' > " + "Assunto: " + enviar_assunto.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + corpo_mensagem.Value.ToString() + "</p>";                        
            xMsg += "</div>";
            xMsg += "</fieldset>";
          

            Msg.IsBodyHtml = true;
            Msg.From = new MailAddress(para);                        
            Msg.Subject = "Assunto: " + enviar_assunto.Value.ToString();
            Msg.Body = xMsg;
            Msg.To.Add(para);

            
            To.IsBodyHtml = true;
            To.From = new MailAddress(cc);
            To.Subject = "Assunto: " + enviar_assunto.Value.ToString();
            To.Body = xMsg;            
            To.To.Add(cc);



            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                cliente.Send(To);
                lblResultado.Text = "<p>Sua mensagem foi enviada com sucesso!!! </p><br>" +"<p>" +  agora + "</p>";
                //MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

                ativarConcluido();

            }
            catch (Exception ex)
            {
                lblResultado.Text = "Erro no envio da sua mensagem: " + ex;
                ativarConcluido();
            }
            finally 
            { 
                cliente.Dispose(); 
            }
            
            //lblMsg.Text = "Enviado para: " + para;
            /*
            lblFalta.Text = "1 - Formatar o Corpo da Mensagem (Formatação HTML)" +
                            "2 - Ajustar Método de captura do e-mail. Buscar e-mail na base de dados";*/
        }

        protected void enviarEmailFaleConosco(object sender, EventArgs e)
        {//Processo concluído e Testado em: 06-09-2023                        
            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            
            //Dados para Envio do e-mail
            string para = " ";
            string cc = " ";                                    
            
            string xMsg = "";
            string destino = "";

            string assunto = enviar_assunto.Value.ToString();

            if (destino_atendimento.Checked)
            {
                destino = destino_atendimento.Value;
                //para = "reginaldo@asu.com.br, regis@asu.com.br";
                //para = "reginaldo@asu.com.br";
                para = "atendimento@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_convenio.Checked)
            {
                destino = destino_convenio.Value;
                //para = "reginaldo@asu.com.br";
                para = "convenios@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_financeiro.Checked)
            {
                destino = destino_financeiro.Value;
                para = "tesouraria@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_jornal.Checked)
            {
                destino = destino_jornal.Value;
                para = "jornal@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_juridico.Checked)
            {
                destino = destino_juridico.Value;
                para = "juridico@asu.com.br";
                cc = "informatica@asu.com.br";
            }
            if (destino_secretaria.Checked)
            {
                destino = destino_secretaria.Value;
                para = "secretaria@asu.com.br";
                cc = "informatica@asu.com.br";
            }

            xMsg += "<fieldset style='width: 600px;'>";
            xMsg += "<div style='margin: 0; padding: 0; width: 600px; min-height: 100px; background-color: #F0F0F0;'>";
            xMsg += "<p style='font-size: 1.4em; color: #22396f;'>" + "Fale Conosco: " + destino + "</p>";            
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Enviado por: " + enviar_nome.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Telefone: " + enviar_telefone.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "E-mail: " + enviar_email.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Empresa: " + enviar_empresa.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + "Momento: " + agora + "</p> ";
            xMsg += "</div>";
            xMsg += "<div style='margin-top: 20px; width: 500px; min-height: 200px;'>";
            xMsg += "<p style = 'margin: 0; padding: 0; margin-top: 20px;' > " + "Assunto: " + enviar_assunto.Value + "</p>";
            xMsg += "<p style='margin: 0; padding: 0;'>" + corpo_mensagem.Value.ToString() + "</p>";
            xMsg += "</div>";
            xMsg += "</fieldset>";

            EnviarEmail(para, cc, assunto, xMsg);
            
        }
        protected String EnviarEmail(string para, string cc, string assunto, string msg)
        {//Método concluído e Testado em: 06-09-2023
            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            string retorno = "";

            SmtpClient cliente = new SmtpClient();
            NetworkCredential credenciais = new NetworkCredential();

            //Configurar Cliente            
            cliente.Host = "smtp.asu.com.br";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            //Libera envio de e-mail validando certificados
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Credenciais de acesso            
            credenciais.UserName = "postmaster@asu.com.br";
            credenciais.Password = "Asu#1969";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            MailMessage Msg = new MailMessage();
            MailMessage To = new MailMessage();
       
            Msg.IsBodyHtml = true;
            Msg.From = new MailAddress(para);
            Msg.Subject = "Assunto: " + assunto;
            Msg.Body = msg;
            Msg.To.Add(para);


            To.IsBodyHtml = true;
            To.From = new MailAddress(cc);
            To.Subject = "Assunto: " + assunto;
            To.Body = msg;
            To.To.Add(cc);



            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                cliente.Send(To);
                retorno = "<p>Sua mensagem foi enviada com sucesso!!! </p><br>" + "<p>" + agora + "</p>";
                //MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

                ativarConcluido();

            }
            catch (Exception ex)
            {
                retorno = "Erro no envio da sua mensagem: " + ex;
                ativarConcluido();
            }
            finally
            {
                cliente.Dispose();
            }
            
            lblResultado.Text = retorno;

            return lblResultado.Text;

        }//EnviarEmail


    }//FIM
}