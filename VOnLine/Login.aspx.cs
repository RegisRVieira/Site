using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using Site.App_Code;

namespace Site.VOnLine
{
    public partial class Login : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["VoceOnLine"] != null)
            {
                Session.Abandon();
            }
            if (!Page.IsPostBack)
            {
                //checarLogAcesso();
                checarTermoPrivacidade();
            }

        }
        protected void checarLogAcesso()
        {
            BLL ObjAcesso = new BLL(conectSite);

            string IP = "";
            IP = Request.UserHostAddress;

            string tabela = " st_log_acesso ";
            string campos = " id, ip ";
            //string condicao = " WHERE IP='" + IP + "' OR id IS NULL  ";
            string condicao = " WHERE IP='" + IP + "'";

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            ObjAcesso.Tabela = tabela;
            ObjAcesso.Campo = campos;
            ObjAcesso.Condicao = condicao;

            DataTable dados = ObjAcesso.RetCampos();

            //MessageBox.Show(" SELECT " + campos + " FROM " + tabela + condicao);

            if (String.IsNullOrEmpty(ObjAcesso.MsgErro))
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    if (dados.Rows[i]["ip"].ToString() == IP && String.IsNullOrEmpty(dados.Rows[i]["id"].ToString()))
                    {
                        //MessageBox.Show("Tem que Gravar!");
                        //MessageBox.Show(dados.Rows[0]["id"].ToString() + " - " + IP);
                        //termo.Attributes["class"] = "desativa";
                    }
                    else
                    {
                        termo.Attributes["class"] = "desativa";
                    }
                }

            }
            else
            {
                lblMsg.Text = "Há problemas com a Conexão: " + ObjAcesso.MsgErro;
            }
            //lblMsg.Text = xRet;
        }
        protected void checarTermoPrivacidade()
        {
            BLL ObjDados = new BLL(conectSite);

            string IP = "";
            IP = Request.UserHostAddress;

            string tabela = " st_termo_privacidade ";
            string campos = " id, ip, status ";
            //string condicao = " WHERE IP='" + IP + "' OR id IS NULL  ";
            string condicao = " WHERE IP='" + IP + "'";

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            //MessageBox.Show(" SELECT " + campos + " FROM " + tabela + condicao);

            if (String.IsNullOrEmpty(ObjDados.MsgErro))
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    if (dados.Rows[i]["ip"].ToString() == IP && String.IsNullOrEmpty(dados.Rows[i]["id"].ToString()))
                    {
                        //MessageBox.Show("Tem que Gravar!");
                        //MessageBox.Show(dados.Rows[0]["id"].ToString() + " - " + IP);
                        termo.Attributes["class"] = "desativa";
                    }
                    else
                    {
                        if (dados.Rows.Count > 0)
                        {
                            if (dados.Rows[0]["IP"].ToString() == IP.ToString())
                            {
                                if (dados.Rows[0]["status"].ToString() != "Recusado")
                                {
                                    termo.Attributes["class"] = "desativa";
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                lblMsg.Text = "Há problemas com a Conexão: " + ObjDados.MsgErro;
            }
            //lblMsg.Text = xRet;
        }
        protected void LiberarAcessoVO(object sender, EventArgs e)
        {
            termo.Attributes["class"] = "desativa";
        }
        public string GeraDigMod11(long intNumero)
        {
            string cDigito = "";

            cDigito = "" + intNumero + DigitoModulo11(intNumero);
            cDigito = "" + cDigito + DigitoModulo11(Convert.ToInt64(cDigito));

            return cDigito;
        }
        public int DigitoModulo11(long intNumero)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            string strText = intNumero.ToString();

            if (strText.Length > 16)

                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;

            int intIdx = 0;

            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];

                intIdx++;
            }

            int intResto = (intSoma * 10) % 11;

            int intDigito = intResto;

            if (intDigito >= 10)

                intDigito = 0;

            return intDigito;
        }
        protected void LogarVoceOnLine(object sender, EventArgs e)
        {
            BLL ObjDbVegas = new BLL(conectVegas);

            string codAcesso = iCpf.Value;
            string Senha = iSenha.Value;

            codAcesso.Replace(".", "").Replace("-", "").Replace(" ","").Replace("/","");

            string campo = "";
            string tabela = "";
            string left = "";
            string condicao = "";
            int tamanhocampo = codAcesso.Length;
            bool validacpf = true; //Variável para checar se o campo cpf tem 11 ou 14 caracteres, se não tiver emite mensagem de erro. 

#pragma warning disable CS0168 // A variável "rDigVerifica" está declarada, mas nunca é usada
            string rDigVerifica;
#pragma warning restore CS0168 // A variável "rDigVerifica" está declarada, mas nunca é usada
#pragma warning disable CS0219 // A variável "wdv" é atribuída, mas seu valor nunca é usado
            string wdv = "";
#pragma warning restore CS0219 // A variável "wdv" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "idCartao" é atribuída, mas seu valor nunca é usado
            string idCartao = "";
#pragma warning restore CS0219 // A variável "idCartao" é atribuída, mas seu valor nunca é usado

            //MessageBox.Show("Vamos Logar");

            if (String.IsNullOrEmpty(codAcesso) || String.IsNullOrEmpty(iSenha.Value))
            {
                lblResult.Text = "Preencha os campos para realizar o Login!!!";
            }
            else
            {
                if (tamanhocampo <= 1)
                {//Inserido essa checagem em 06/12/2023, após perceber erro no login quando o usuário digita apenas um caracter no campo usuário
                    lblResult.Text = "Dados para Login incorreto(s)!!!";
                }
                else
                {
                    if (ObjDbVegas.MsgErro == "")
                    {
                        if (tamanhocampo == 11 || tamanhocampo == 9)
                        {
                            //MessageBox.Show("Associado");
                            campo = " d.associado AS idassoc, d.iddepen, d.nome AS nomeAssoc, d.cnpj_cpf AS cpf, a.senha ";
                            tabela = " asdepen AS d ";
                            left = " INNER JOIN associa AS a ON d.associado = a.idassoc ";
                            //condicao = " WHERE cnpj_cpf ='" + codAcesso + "' AND senha = '" + Senha + "'";                
                            condicao = " WHERE(d.cnpj_cpf = '" + codAcesso + "' OR(EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + codAcesso.Substring(0, 7) + "'))) AND a.senha = '" + Senha + "' AND a.cnscanmom IS NULL ";

                        }
                        else if (tamanhocampo == 14 || tamanhocampo < 7)//else if (tamanhocampo == 14 || tamanhocampo == 5)
                        {
                            //MessageBox.Show("Convênio");
                            campo = " idconven AS id, nome AS nomeConv, cnpj_cpf AS cnpj, senha_adm AS senha  ";
                            tabela = " coconven ";
                            condicao = " WHERE cnpj_cpf ='" + codAcesso + "' OR idconven = '" + codAcesso.Substring(0, (tamanhocampo - 2)) + "' AND senha_adm = '" + Senha + "' AND cnscanmom IS NULL ";
                        }
                        else
                        {
                            validacpf = false;
                            lblResult.Text = "Dados para Login incorreto(s)!!!";
                        }


                        if (validacpf)
                        {
                            //MessageBox.Show(campo + " - " + tabela + " - " + left + " - " + condicao);
                            ObjDbVegas.Campo = campo;
                            ObjDbVegas.Tabela = tabela;
                            ObjDbVegas.Left = left;
                            ObjDbVegas.Condicao = condicao;

                            DataTable dados = ObjDbVegas.RetCampos();

                            if (Session["VoceOnline"] == null)
                            {
                                if (dados.Rows.Count > 0)
                                {
                                    if (tamanhocampo == 9 || tamanhocampo == 11)
                                    {
                                        Session.Add("IdAssoc", dados.Rows[0]["idassoc"]);
                                        Session.Add("LoginUsuario", dados.Rows[0]["nomeAssoc"].ToString());
                                        Session.Add("Identifica", dados.Rows[0]["cpf"].ToString());
                                    }
                                    else
                                    {
                                        Session.Add("LoginConvenio", dados.Rows[0]["nomeConv"].ToString());
                                        Session.Add("LoginUsuario", dados.Rows[0]["nomeConv"].ToString());
                                        Session.Add("LoginIdConvenio", dados.Rows[0]["id"].ToString());
                                        Session.Add("Identifica", dados.Rows[0]["cnpj"].ToString());
                                        Session.Add("IdAssoc", 0);//Atribui 0 para IdAssoc, caso contrário ocorre erro nos campos que requisitam a Id do associado.
                                    }

                                    Session.Add("VoceOnLine", "Sim");

                                    Session.Add("CodAcesso", codAcesso);

                                    //Response.Redirect("VoceOnLine.aspx");
                                    Response.Redirect("VoceOnLine_New.aspx");


                                }
                                else //É conveniado
                                {
                                    lblResult.Text = "Usuário ou Senha incorreto(s)";
                                }
                            }
                            else
                            {
                                lblResult.Text = "Você estava Logado, faça um novo Login?";
                                Session.Abandon();
                                //Response.Redirect("Login.aspx");
                            }
                        }
                        //lblResult.Text = "Parabéns: Você está logado como: " + Session["LoginUsuario"];
                    }
                    else
                    {
                        string msgErro = "Erro: " + ObjDbVegas.MsgErro;
                        lblMsg.Text = msgErro;
                    }
                }
            }
        }//LogarVoceOnLine
    }
}