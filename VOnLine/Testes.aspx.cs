using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Site.App_Code;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Site.VOnLine
{
    public partial class Testes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //teste();
                //msgJava();
            }

            this.DataBind();
            
        }

        public String msgJava(string msgErro)
        {
            string msgJava = "";

            msgJava += "<script>" +
                            "function verMsgJava(){" +
                            " var msg = '" + msgErro + "';" +
                            //" var msg = '" + "msgErro" + "';" +
                            "alert(msg);" +
                            "event.preventDefault();" +
                            "}" +
                            "verMsgJava();" + 
                            "</script>";

            lblScript.Text = msgJava;

            return lblScript.Text;
        }
        public String msgJava()
        {
            string msgJava = "";

            msgJava += "<script>" +
                            "function verMsgJava(){" +                            
                            " var msg = '" + "msgErro" + "';" +
                            "alert(msg);" +
                            "event.preventDefault();" +
                            "}" +
                            "verMsgJava();" +
                            "</script>";
            lblScript.Text = msgJava;
            return lblScript.Text;
        }

        public Double teste()
        {            
            double n = 0.00;
            try
            {
                if (n == 0.00)
                {
                    msgJava("Verdadeiro");
                    return 15.00;
                }
                else
                {
                    msgJava("Falso");
                    return 5.00;
                }
            }
            catch (Exception e)
            {
                msgJava("Erro Original: " + e.Message.ToString());
                return 5.00;
            }
            

            
        }

        public string retorno { get; set; }
        public String EnviarEmails(string para, string cc, string assunto, string msg)
        {//Método concluído e Testado em: 06-09-2023
            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;            

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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
                //cliente.Send(To);
                retorno = "<p>Sua mensagem foi enviada com sucesso!!! " + agora ;
                //retorno = "Enviado";                

                //msgJava(retorno);
            }
            catch (Exception ex)
            {
                retorno = "Erro: " + ex.Message;
            }            

            //lblResultado.Text = retorno;

            return null;

        }//EnviarEmail
        protected void enviarEmail(object sender, EventArgs e)
        {
            Apoio Apoio = new Apoio();
            DateTime agora = new DateTime();

            string para = iPara.Value;
            string cc = iCopia.Value;
            string assunto = iAssunto.Value;
            string msg = iDados.Value + " - " + DateTime.Now;
            
            EnviarEmails(para, cc, assunto, msg);

            msgJava("Retorno: " + retorno);
                       
            
            //lblMsg.Text = "Sua Mensagem foi enviada para (*): " + para + ", com cópia para: " + cc + " - Para tratar de: " + assunto + ", veja os detalhes: " + msg + " - " + Apoio.retornaMsg();
            lblMsg.Text = retorno;
        }
    }
}