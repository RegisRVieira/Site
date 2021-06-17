﻿using System;
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

namespace Site
{
    public partial class LoginVoceOnLine : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];

        protected void Page_Load(object sender, EventArgs e)
        {

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

            string campo = "";
            string tabela = "";
            string left = "";
            string condicao = "";
            int tamanhocampo = codAcesso.Length;
            bool validacpf = true; //Variável para checar se o campo cpf tem 11 ou 14 caracteres, se não tiver emite mensagem de erro. 

            string rDigVerifica;
            string wdv = "";
            string idCartao = "";

            if (String.IsNullOrEmpty(iCpf.Value) || String.IsNullOrEmpty(iSenha.Value))
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
                        condicao = " WHERE(d.cnpj_cpf = '" + codAcesso + "' OR(EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + codAcesso.Substring(0, 7) + "'))) AND a.senha = '" + Senha + "' ";

                    }
                    else if (tamanhocampo == 14 || tamanhocampo < 7)//else if (tamanhocampo == 14 || tamanhocampo == 5)
                    {
                        //MessageBox.Show("Convênio");
                        campo = " idconven AS id, nome AS nomeConv, cnpj_cpf AS cnpj, senha_adm AS senha  ";
                        tabela = " coconven ";
                        condicao = " WHERE cnpj_cpf ='" + codAcesso + "' OR idconven = '" + codAcesso.Substring(0, (tamanhocampo - 2)) + "' AND senha_adm = '" + Senha + "'";
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

                        int contador = dados.Rows.Count;

                        if (Session["VoceOnline"] == null)
                        {
                            if (contador > 0)
                            {
                                if (tamanhocampo == 9 || tamanhocampo == 11)
                                {
                                    Session.Add("IdAssoc", dados.Rows[0]["idassoc"]);
                                    Session.Add("LoginUsuario", dados.Rows[0]["nomeAssoc"].ToString());
                                    Session.Add("Identifica", dados.Rows[0]["cpf"].ToString());
                                }
                                else
                                {
                                    Session.Add("LoginUsuario", dados.Rows[0]["nomeConv"].ToString());
                                    Session.Add("Identifica", dados.Rows[0]["cnpj"].ToString());
                                }
                                //Session.Abandon();
                                //Session.Remove("VoceOnLine");
                                Session.Add("VoceOnLine", "Sim");

                                Session.Add("CodAcesso", codAcesso);
                                //Session.Add("iDAssoc", dados.Rows[0]["associado"].ToString());
                                Response.Redirect("VoceOnLine.aspx");
                                /*
                                if (tamanhocampo == 9 || tamanhocampo == 11)
                                {
                                    MessageBox.Show(dados.Rows[0]["nomeAssoc"].ToString());
                                }
                                else
                                {
                                    MessageBox.Show(dados.Rows[0]["nomeConv"].ToString());
                                }
                                */
                                //MessageBox.Show(dados.Rows[0]["nome"].ToString());
                            }
                            else //É conveniado
                            {
                                lblResult.Text = "Usuário ou Senha incorreto(s)";
                                Response.Redirect("VoceOnLine.aspx");
                                //MessageBox.Show(dados.Rows[0]["nomeAssoc"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Você já está Logado, encerre sua sessão para poder iniciar uma nova!");
                        }
                    }
                    //lblResult.Text = "Parabéns: Você está logado como: " + Session["LoginUsuario"];
                }
                else
                {
                    string msgErro = "Erro: " + ObjDbVegas.MsgErro;
                    MessageBox.Show(msgErro);
                }
            }
        }
    }
}