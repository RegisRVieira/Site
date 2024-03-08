using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Configuration;
using System.Windows.Forms;
using System.Data;


namespace Site.VOnLine
{
    public partial class LoginNLayout : System.Web.UI.Page
    {

        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["VoceOnLine"] != null)
            {
                Session.Abandon();
            }
        }

        protected void LogarVoceOnLine(object sender, EventArgs e)
        {
            BLL ObjDbVegas = new BLL(conectVegas);

            string codAcesso = iCpf.Value;
            string Senha = iSenha.Value;

            codAcesso.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");

            string campo = "";
            string tabela = "";
            string left = "";
            string condicao = "";
            int tamanhocampo = codAcesso.Length;
            bool validacpf = true; //Variável para checar se o campo cpf tem 11 ou 14 caracteres, se não tiver emite mensagem de erro.             

            //MessageBox.Show("Vamos Logar");

            if (String.IsNullOrEmpty(codAcesso) || String.IsNullOrEmpty(iSenha.Value))
            {
                lblResult.Text = "Preencha os campos para realizar o Login!!!";
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
        }//LogarVoceOnLine
    }
}