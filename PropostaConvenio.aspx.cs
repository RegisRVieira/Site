using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;

namespace Site
{
    public partial class PropostaConvenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ativarVwProposta();
            }
        }

        protected void ativarVwProposta()
        {
            mwProposta.ActiveViewIndex = 0;
        }

        protected void ativarVwMsg()
        {
            mwProposta.ActiveViewIndex = 1;
        }
        protected void voltarHome(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        public void enviarProposta(object sender, EventArgs e)
        {
            Apoio ObjApoio = new Apoio();

            string xRet = "";

            string assunto = "Proposta de Convênio";
            string para = "convenio@asu.com.br";            
            string cc = "informatica@asu.com.br";
            string msg = "";

            //Validação dos dados
            if (String.IsNullOrEmpty(iNome.Value) || String.IsNullOrEmpty(iRazao.Value) || 
                String.IsNullOrEmpty(iTel.Value) || String.IsNullOrEmpty(iCel.Value) || 
                String.IsNullOrEmpty(iEmail.Value) || String.IsNullOrEmpty(iContato.Value))
            {
                lblMsgErro.Text = "Todos os Campos Precisam ser preenchidos!!!";
            }
            else
            {
                try
                {
                    lblMsgErro.Text = String.Empty;
                    xRet += "<div style='width: 600px; min-height: 200px; margin: 0 auto; '>";                    
                    xRet += "<p>" +"Nome: " + iNome.Value + "</p>";
                    xRet += "<p>" + "Razão Social: " +  iRazao.Value + "</p>";
                    xRet += "<p>" + "Telefone: " + iTel.Value + "</p>";
                    xRet += "<p>" + "Celular: " + iCel.Value + " </p>";
                    xRet += "<p>" + "E-mail: " + iEmail.Value + "</p>";
                    xRet += "<p>" + "Ramo de Atividade: " + iRamo.Value + "</p>";
                    xRet += "<p>" + "Contato: " + iContato.Value + "</p>";
                    xRet += "</div>";
                    msg = xRet;

                    ObjApoio.EnviarEmail(para, cc, assunto, msg);

                    ativarVwMsg();

                    lblMsg.Text = "Sua Msg foi enviada com Sucesso. Aguarde nosso contato!!!";

                }
                catch (Exception x)
                {
                    lblMsgErro.Text = "Sua menssagem não pôde ser enviada: " + x.Message;
                }
            }
            
            

        }//enviarProposta

    }
}