﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Windows;
using System.Windows.Forms;
using Site.App_Code;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Drawing;



namespace Site.VoceOnLine
{
    public partial class VoceOnLine : System.Web.UI.Page
    {

        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        public string VendaArq_envio { get; set; }
        public int nVezes { get; set; }
        public string cArquivo_mostra { get; set; }
        public double widthExtrato { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                checarSessao();
                ativarExtratoOuVenda();
                carregarDadosAssoc();
                carregarDadosConv();
                usuarioLogado();
                atualizaDdlExtrato();
                gravarTermoPrivacidade();
                desativaBrinde();
                carregarData();
                identificarTamanhoTela();
                participacaoPesquisa();
            }
            this.DataBind();
        }

        public void participacaoPesquisa()
        {
            BLL ObjDados = new BLL(conectVegas);
            BLL ObjPesquisa = new BLL(conectVegas);

            string codAcesso = Session["CodAcesso"].ToString();
            //int tCampo = codAcesso.Length; 
            string query = "";

            string queryPesquisa = " SELECT * FROM pesquisa " +
                                   " WHERE datainicial <= CURDATE() AND datafinal >= CURDATE()";

            ObjPesquisa.Query = queryPesquisa;
            DataTable dadosPesquisa = ObjPesquisa.RetQuery();

            if (dadosPesquisa.Rows.Count > 0)
            {
                //Checa se é Associadfo ou conveniado, e para quem a pesquisa está "aberta"
                if (tipoUsuarioLogado(codAcesso) == "Associado")
                {
                    //MessageBox.Show("Associado");

                    query = " SELECT * FROM PES_RESPOSTAS " +
                            " WHERE associado = '" + Session["IdAssoc"].ToString() + "'" +
                            //" AND idpesquisa = '" + "ID Pesquisa" + "'";
                            " AND idpesquisa = '" + "1" + "'";

                    //MessageBox.Show(Session["IdAssoc"].ToString());
                }
                else
                {
                    //MessageBox.Show("Conveniado");

                    query = " SELECT * FROM PES_RESPOSTAS " +
                            " WHERE convenio = '" + codAcesso.Substring(0, (codAcesso.Length - 2)) + "'" +
                            //" AND idpesquisa = '" + "ID Pesquisa" + "'";
                            " AND idpesquisa = '" + "1" + "'";

                    //MessageBox.Show(codAcesso.Substring(0, (codAcesso.Length - 2)));
                }

                ObjDados.Query = query;
                DataTable dados = ObjDados.RetQuery();

                //MessageBox.Show(dados.Rows.Count.ToString());

                if (ObjDados.MsgErro == "")
                {
                    if (dados.Rows.Count > 0)
                    {
                        divPesquisa.Attributes["class"] = "Desativa";
                    }
                }
                else
                {
                    lblMsg.Text = "Deu pau:" + ObjDados.MsgErro;
                }
            }
                   
        }
        public Double identificarTamanhoTela()
        {

            //# # # # # Fim - saldo e limite # # # # #
            double widht = Screen.PrimaryScreen.Bounds.Width; //Capturar por JavaSvript e armazenar em um label para capturar aqui
                                                              //double tam = System.sys
            widthExtrato = widht;

            return widthExtrato;
        }

        protected void negarBrinde(object sender, EventArgs e)
        {
            secPremio.Attributes["class"] = "desativa_termo";
        }
        protected void desativaBrinde()
        {
            BLL ObjBrinde = new BLL(conectSite);
            BLL ObjContar = new BLL(conectSite);

            string campos = " id_assoc, usuario ";
            string tabela = " st_premiacao ";
            string condicao = " WHERE id_assoc = '" + Session["IdAssoc"].ToString() + "'";

            string xRet = "";

            ObjBrinde.Tabela = tabela;
            ObjBrinde.Campo = campos;
            ObjBrinde.Condicao = condicao;

            ObjContar.Tabela = tabela;
            ObjContar.Campo = campos;

            DataTable dados = ObjBrinde.RetCampos();
            DataTable contar = ObjContar.RetCampos();

            //lblTestaTermo.Text = "Tem: " + contar.Rows.Count;

            //lblMsgPremio.Text = Session["IdAssoc"].ToString();
            //MessageBox.Show(Session["IdAssoc"].ToString());            

            try
            {
                if (String.IsNullOrEmpty(ObjBrinde.MsgErro))
                {
                    if (contar.Rows.Count >= 50)
                    {
                        //MessageBox.Show("Acabou");
                        secPremio.Attributes["class"] = "desativa_termo";
                    }

                    if (dados.Rows.Count > 0)
                    {
                        //MessageBox.Show("Você já está participando!!!");
                        //xRet += "Você está Participando" + "\n" + "Parabéns!";

                        xRet += "<section class='msgParticipacao'>";
                        //xRet += "<p>" + "Você está Participando da Premiação pelo lanaçamento do Novo Site, Parabéns!" + "</p>";
                        //xRet += "<p>" + "Retirada do Brinde: 07/11/2022" + "</p>";
                        xRet += "<p>" + "Venha receber seu Brinde: 07/11/2022, a partir das 13:00" + "</p>";
                        xRet += "</section>";

                        secPremio.Attributes["class"] = "desativa_termo";
                        //secMsg.Attributes["class"] = "ativa_termo";
                        secMsg.Attributes["class"] = "desativa_termo";
                    }
                }
                else
                {
                    xRet += "Erro: " + ObjBrinde.MsgErro;
                }
            }
            catch (Exception e)
            {
                xRet += "Erro: " + e.Message;

            }

            //# Comentei esta linha para não mostrar mais a Msg. Basta Editar a Msg e descomentar a linha para exibir. 02/11/2022.
            //lblMsgPremio.Text = xRet;



            //MessageBox.Show("Login Usuario: " + Session["LoginUsuario"].ToString() + " Identifica: " + Session["Identifica"].ToString() + " IDAssoc: " + Session["IdAssoc"].ToString());

        }//desativaBrinde
        protected void gravarBrinde(object sender, EventArgs e)
        {
            BLL ObjBrinde = new BLL(conectSite);

            string IP = "";
            IP = Request.UserHostAddress;

            string tabela = " st_premiacao "; //Tabela
            string campos = " login_acesso, ip, usuario, id_conf, id_conv, id_assoc, cadMom, cadUsu  "; //Campos 
            string valores = String.Format("'" + Session["CodAcesso"].ToString() + "'," + //Valores
                               "'" + IP + "'," +
                               "'" + Session["LoginUsuario"].ToString() + "'," +
                               "'" + "1" + "'," +
                               "'" + "224" + "'," +
                               "'" + Session["IdAssoc"].ToString() + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + "Site" + "'");

            //Dados para Inserção
            ObjBrinde.Tabela = tabela;
            ObjBrinde.Campo = campos;
            ObjBrinde.Valores = valores;

            DataTable dados = ObjBrinde.RetCampos();

            string xRet = "";

            //Testes
            //xRet += " INSERT INTO " + tabela + " (" + campos + ")" + " VALUES (" + valores +")";
            //xRet += " SELECT " + ObjLog.Campo + " FROM " + ObjLog.Tabela + " " + ObjLog.Condicao;
            //MessageBox.Show(xRet);
            //Fim Testes                       
            try
            {
                if (String.IsNullOrEmpty(ObjBrinde.MsgErro))
                {
                    ObjBrinde.InsertRegistro(tabela, campos, valores);

                    desativaBrinde();
                    //secMsg.Attributes["class"] = "ativa_termo";

                    if (xRet != "")
                    {
                        lblMsgPremio.Text = xRet;
                    }

                }
                else
                {
                    xRet += "Erro: " + ObjBrinde.MsgErro;
                }
            }
            catch (Exception x)
            {
                xRet += "Erro: " + x.Message;
            }

            lblMsgPremio.Text = xRet;

        }//gravarBrinde
        protected void gravarTermoPrivacidade()
        {
            BLL ObjLog = new BLL(conectSite);

            string IP = "";
            IP = Request.UserHostAddress;

            string codAcesso = Session["CodAcesso"].ToString();
            int tCampo = codAcesso.Length;

            string usuario = "";

            if (tCampo == 9 || tCampo == 11)
            {
                usuario = Session["LoginUsuario"].ToString();
            }
            else
            {
                usuario = Session["LoginIdConvenio"].ToString();
            }

            string tabela = " st_termo_privacidade "; //Tabela
            string campos = " Login_Acesso, ip, id_empresa, status, usuario, cadMom, cadUsu  "; //Campos             
            //string campos = " ip, id_empresa, status, cadMom, cadUsu  "; //Campos 
            string valores = String.Format("'" + Session["CodAcesso"].ToString() + "'," + //Valores
                               "'" + IP + "'," +
                               "'" + "1" + "'," +
                               "'" + "Aceito" + "'," +
                               "'" + usuario + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + "Site" + "'");
            string condicao = " WHERE Login_Acesso='" + Session["CodAcesso"].ToString() + "'"; //Condicao

            //Dados para Inserção
            ObjLog.Tabela = tabela;
            ObjLog.Campo = campos;
            ObjLog.Valores = valores;
            ObjLog.Condicao = condicao;

            DataTable dados = ObjLog.RetCampos();

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            //xRet += " SELECT " + ObjLog.Campo + " FROM " + ObjLog.Tabela + " " + ObjLog.Condicao;
            //MessageBox.Show(xRet + "Count:" + dados.Rows.Count);
            //MessageBox.Show(dados.Rows[0]["Login_Acesso"].ToString() + " * " + Session["CodAcesso"].ToString() );


            if (String.IsNullOrEmpty(ObjLog.MsgErro))
            {

                if (dados.Rows.Count == 0)
                {
                    //MessageBox.Show("Não há registro");
                    ObjLog.InsertRegistro(tabela, campos, valores);
                }
                else
                {
                    //MessageBox.Show("Já tem registro");
                    if (dados.Rows[0]["Login_Acesso"].ToString() == Session["CodAcesso"].ToString())
                    {

                    }
                    else
                    {
                        ObjLog.InsertRegistro(tabela, campos, valores);
                    }
                }
            }

            //MessageBox.Show(xRet);

        }//gravarTermoPrivacidade

        /* - - -  Operações e Validações - - - */
        public void usuarioLogado()
        {
            string codAcesso = Session["CodAcesso"].ToString();
            int tCampo = codAcesso.Length;
            string usuario = "";


            //MessageBox.Show(codAcesso + " T Campo: " + tCampo);

            if (tCampo == 9 || tCampo == 11)
            {
                usuario = Session["LoginUsuario"].ToString();
            }
            else
            {
                usuario = Session["LoginIdConvenio"].ToString();
            }
            string primeiroNome = usuario.Split(' ').FirstOrDefault();
            string primeiraLetra = usuario.Split(' ').FirstOrDefault();
            //string segundoNome = usuario.Split(' ')[1];
            //string primeiraLetraSegundoNome = usuario.Split(' ')[1];
            int tNome = primeiroNome.Length;
            //int tSnome = segundoNome.Length;

            //xRet += "" + primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome-1)).ToLower();
            //lblUsuLogado.Text = primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome - 1)).ToLower();
            //lblUsuLogado.Text = primeiraLetraSegundoNome.Substring(0, 1) + primeiraLetraSegundoNome.Substring(1, (tSnome - 1)).ToLower();
            lblUsuLogado.Text = usuario.ToUpper();

            //MessageBox.Show(primeiraLetraSegundoNome.Substring(0, 1) + primeiraLetraSegundoNome.Substring(1, (tSnome - 1)).ToLower());


            limpaExtratos();

            //return xRet;
        }//usuarioLogado

        public String tipoUsuarioLogado(string checa)
        {
            string codAcesso = Session["CodAcesso"].ToString();

            int tCampo = checa.Length;
            string usuario = "";

            if (tCampo == 9 || tCampo == 11)
            {
                usuario = "Associado";
            }
            else
            {
                usuario = "Conveniado";
            }

            return usuario;

        }//tipoUsuarioLogado

        public void checarSessao()
        {
            string identifica = "";

            if (Session["Identifica"] != null)
            {
                identifica = Session["Identifica"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");

            }

            int tamanhocampo = identifica.Length;

            if (tamanhocampo == 11)
            {
                ativarAssociado();
            }
            else if (tamanhocampo == 14)
            {
                ativarConveniado();
            }
            else
            {
                //MessageBox.Show("Alguma coisa deu errada..."); 
            }
        }//checarSessao

        public void ativarExtratoOuVenda()
        {
            string identifica = "";

            string codAcesso = Session["CodAcesso"].ToString();
            int tAcesso = codAcesso.Length;

            if (Session["Identifica"] != null)
            {
                identifica = Session["Identifica"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (tAcesso == 9 || tAcesso == 11)
            {
                mwContAssoc.ActiveViewIndex = 1;
            }
            else
            {
                mwContConv.ActiveViewIndex = 0;
            }
        }//ativarExtratoOuVenda

        /* - - - FIM Validações - - - */

        /* - - -  Ativa Views - - - */

        /*Associado*/
        public void ativarAssociado()
        {
            mwVoceOnLine.ActiveViewIndex = 0;
        }
        public void ativarAssocDados(object sender, EventArgs e)
        {
            mwContAssoc.ActiveViewIndex = 0;

        }
        public void ativarAssocExtrato(object sender, EventArgs e)
        {
            mwContAssoc.ActiveViewIndex = 1;
            extratoAssociado();
        }
        public void ativarAssocCartoes(object sender, EventArgs e)
        {
            mwContAssoc.ActiveViewIndex = 2;
        }
        public void ativarAssocSenha(object sender, EventArgs e)
        {
            mwContAssoc.ActiveViewIndex = 3;
        }
        /*Convênio*/
        public void ativarConveniado()
        {
            mwVoceOnLine.ActiveViewIndex = 1;
        }
        public void ativarConvVenda(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 0;
        }
        public void ativarConvDados(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 1;
        }
        public void ativarConvRelEntrega(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 2;
            carregarData();
        }
        public void ativarConvFatura(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 3;
            montarRelMensal();
        }
        public void ativarConvExtrato(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 4;
            montarExtratoConv();
            montarExtratoPreConv();
        }
        public void ativarConvSenha(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 5;
        }
        public void ativarConvDownloads(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 6;
        }
        public void ativarConvOfertas(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 7;
        }        
        public void ativarConvReimpressao(object sender, EventArgs e)
        {
            //mwContConv.ActiveViewIndex = 8;
            string Url = "<script type=”text/javascript”>" +
                         " function x(){" +
                         "    Window.open(“ReimprimeCupom.aspx, _Blanc”);}" +
                         "</ script > ";


            Response.Redirect("ReimprimeCupom.aspx");
            
        }

        public void buscarCepAssoc(object sender, EventArgs e)
        {
            Apoio ObjApoio = new Apoio();

            ObjApoio.buscarCep(iCepAssoc.Value);

            string logradouro = ObjApoio.Rua;
            string log = "";

            if (string.IsNullOrEmpty(iCepAssoc.Value))
            {
                iCepAssoc.Focus();
                iCepAssoc.Attributes.Add("placeholder", "Este campo, CEP, Precisa ser Preenchido");

            }
            else
            {
                log = logradouro.Split(' ')[0];
                iLogradouroAssoc.Value = log;
                iRuaAssoc.Value = logradouro.Substring((log.Length + 1), ((logradouro.Length - log.Length) - 1));
                iComplemAssoc.Value = ObjApoio.Complemento;
                iBairroAssoc.Value = ObjApoio.cBairro;
                iCidadeAssoc.Value = ObjApoio.cCidade;
                iEstadoAssoc.Value = ObjApoio.UF;

                iNumCasaAssoc.Value = String.Empty;
                iNumCasaAssoc.Focus();
                lblErroAssoc.Text = String.Empty;
            }

        }//buscarCepAssoc

        protected void buscarCepConv(object sender, EventArgs e)
        {
            Apoio ObjApoio = new Apoio();

            ObjApoio.buscarCep(iCepConv.Value);

            string logradouro = ObjApoio.Rua;

            string log = "";

            if (string.IsNullOrEmpty(iCepConv.Value))
            {
                iCepConv.Focus();

                //TXTPassR.Attributes.Add("placeholder", "Some Text");

                iCepConv.Attributes.Add("placeholder", "Este campo, CEP, Precisa ser Preenchido");
            }
            else
            {
                //Convênio
                log = logradouro.Split(' ')[0];
                iLogradouroConv.Value = log;
                iRuaConv.Value = logradouro.Substring((log.Length + 1), ((logradouro.Length - log.Length) - 1));
                iComplementConv.Value = ObjApoio.Complemento;
                iBairroConv.Value = ObjApoio.cBairro;
                iCidadeConv.Value = ObjApoio.cCidade;
                iEstadoConv.Value = ObjApoio.UF;

                iNumeroConv.Value = String.Empty;
                iNumeroConv.Focus();
                lblErroAssoc.Text = String.Empty;
            }

        }//buscarCepConv


        /* - - - FIM Views - - - */

        /* - - - Gerador Digito Verificador - - - */

        public string GeraDigMod11(long intNumero)
        {
            string cDigito = "";

            cDigito = "" + intNumero + DigitoModulo11(intNumero);
            cDigito = "" + cDigito + DigitoModulo11(Convert.ToUInt32(cDigito));

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

        /* - - - FIM Dig. Verificador - - - */

        /* - - - Processos - - - */

        public void carregarDadosAssoc()
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;

            if (ObjDbASU.MsgErro == "")
            {

                if (tCampo == 9 || tCampo == 11)
                {
                    string campos = " d.iddepen, a.senha, a.idassoc, a.titular AS nome, u.descricao AS unidade, depto.descricao AS Departamento, s.descricao AS Setor, a.trab_funca, b.descricao AS banco, a.sexo, a.cnpj_cpf, a.ie_rg, a.email, a.fone, a.celular, a.cep, a.endereco, a.numero, a.complement, a.bairro, a.cidade, a.estado, a.credito, a.bco_banco, a.bco_agenci, a.bco_tipocc, a.bco_conta, a.bco_digcc ";
                    string tabela = " asdepen AS d ";
                    string left = " INNER JOIN associa AS a ON d.associado = a.idassoc " +
                                  " INNER JOIN base_assocuni AS u ON u.unidade = a.trab_unida " +
                                  " INNER JOIN base_assocdep AS depto ON depto.depto = a.trab_dpto " +
                                  " LEFT JOIN base_assocset AS s ON s.codsetor = a.trab_setor " +
                                  " INNER JOIN base_un_banco AS b ON b.codbanco = a.bco_banco ";
                    string condicao = " WHERE(d.cnpj_cpf = '" + iDAcesso + "' OR(EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, 7) + "'))) AND a.cnscanmom IS NULL ";

                    ObjDbASU.Campo = campos;
                    ObjDbASU.Tabela = tabela;
                    ObjDbASU.Left = left;
                    ObjDbASU.Condicao = condicao;

                    //MessageBox.Show(ObjDbASU.Campo + ObjDbASU.Tabela + ObjDbASU.Left + ObjDbASU.Condicao);

                    DataTable dados = ObjDbASU.RetCampos();

                    iNomeAssoc.Value = dados.Rows[0]["nome"].ToString();
                    iCpfAssoc.Value = dados.Rows[0]["cnpj_cpf"].ToString();
                    iRgAssoc.Value = dados.Rows[0]["ie_rg"].ToString();
                    iEmailAssoc.Value = dados.Rows[0]["email"].ToString();
                    iFoneAssoc.Value = dados.Rows[0]["fone"].ToString();
                    iCelAssoc.Value = dados.Rows[0]["celular"].ToString();
                    iCepAssoc.Value = dados.Rows[0]["cep"].ToString();
                    iRuaAssoc.Value = dados.Rows[0]["endereco"].ToString();
                    iNumCasaAssoc.Value = dados.Rows[0]["numero"].ToString();
                    iBairroAssoc.Value = dados.Rows[0]["complement"].ToString();
                    iComplemAssoc.Value = dados.Rows[0]["bairro"].ToString();
                    iCidadeAssoc.Value = dados.Rows[0]["cidade"].ToString();
                    iEstadoAssoc.Value = dados.Rows[0]["estado"].ToString();
                    iUnidadeAssoc.Value = dados.Rows[0]["unidade"].ToString();
                    iDepartAssoc.Value = dados.Rows[0]["departamento"].ToString();
                    iSetorAssoc.Value = dados.Rows[0]["setor"].ToString();
                    iFuncaoAssoc.Value = dados.Rows[0]["trab_funca"].ToString();
                    iBancoAssoc.Value = dados.Rows[0]["banco"].ToString();
                    iAgenciaAssoc.Value = dados.Rows[0]["bco_agenci"].ToString();
                    iContaAssoc.Value = dados.Rows[0]["bco_tipocc"].ToString() + dados.Rows[0]["bco_conta"].ToString() + "-" + dados.Rows[0]["bco_digcc"].ToString();
                }
            }
        }

        public void carregarDadosConv()
        {
            BLL ObjDbExtPre = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            string campos = " * ";
            string tabela = " coconven ";
            string condicao = " where idconven = '" + iDAcesso + "' ";

            if (ObjDbExtPre.MsgErro == "")
            {
                ObjDbExtPre.Campo = campos;
                ObjDbExtPre.Tabela = tabela;
                ObjDbExtPre.Condicao = condicao;

                //MessageBox.Show(campos + tabela + condicao);

                DataTable dados = ObjDbExtPre.RetCampos();

                int nLinhas2 = dados.Rows.Count;

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
                string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

                if (nLinhas2 > 0)
                {
                    iNomeConv.Value = dados.Rows[0]["nome"].ToString();
                    iRazaoSocial.Value = dados.Rows[0]["raz_social"].ToString();
                    iCnpj.Value = dados.Rows[0]["cnpj_cpf"].ToString();
                    iDddConv.Value = dados.Rows[0]["ddd"].ToString();
                    iTelefoneConv.Value = dados.Rows[0]["fone"].ToString();
                    iCelularConv.Value = dados.Rows[0]["celular"].ToString();
                    iCepConv.Value = dados.Rows[0]["cep"].ToString();
                    iLogradouroConv.Value = dados.Rows[0]["logradouro"].ToString();
                    iRuaConv.Value = dados.Rows[0]["endereco"].ToString();
                    iNumeroConv.Value = dados.Rows[0]["numero"].ToString();
                    iBairroConv.Value = dados.Rows[0]["bairro"].ToString();
                    iComplementConv.Value = dados.Rows[0]["complement"].ToString();
                    iCidadeConv.Value = dados.Rows[0]["cidade"].ToString();
                    iBancoConv.Value = dados.Rows[0]["bco_banco"].ToString();
                    iAgenciaConv.Value = dados.Rows[0]["bco_agenci"].ToString();
                    iContaCorrenteConv.Value = dados.Rows[0]["bco_conta"].ToString();
                    iDigCCConv.Value = dados.Rows[0]["bco_digcc"].ToString();
                }
            }
        }

        public string extratoAssoc { get; set; }

        protected void atualizaDdlExtrato()
        {
            //Checar data e atualizar ddl
            string diaAtual = DateTime.Now.Day.ToString();
            string mesAtual = DateTime.Now.Month.ToString();
            string anoAtual = DateTime.Now.Year.ToString();

            /* 10-05-2022
            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
            }
            */

            if (Convert.ToInt32(diaAtual) >= 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) + 1).ToString();
                //# # # # # Extrato Associado - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMes in ddlMesExtratoAssoc.Items)//Extrato Associado
                {
                    if (lMes.Value == mesAtual)
                    {
                        lMes.Selected = true;
                    }
                }
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMesExtratoConv.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #


                /*foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMes.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }
                */

                //Fatura Mensal

                foreach (System.Web.UI.WebControls.ListItem lMesFatMensal in ddlFatMensalMes.Items)
                {
                    if (lMesFatMensal.Value == mesAtual)
                    {
                        lMesFatMensal.Selected = true;
                    }
                }
            }
            else
            {
                //# # # # # Extrato Associado - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMes in ddlMesExtratoAssoc.Items)
                {
                    if (lMes.Value == mesAtual)
                    {
                        lMes.Selected = true;
                    }
                }
                //# # # # # Extrato Convênio - Atualiza Mês # # # # #
                foreach (System.Web.UI.WebControls.ListItem lMesConv in ddlMesExtratoConv.Items)
                {
                    if (lMesConv.Value == mesAtual)
                    {
                        lMesConv.Selected = true;
                    }
                }

                foreach (System.Web.UI.WebControls.ListItem lFatMensal in ddlFatMensalMes.Items)
                {
                    if (lFatMensal.Value == mesAtual)
                    {
                        lFatMensal.Selected = true;
                    }
                }

            }
            //# # # # # Extrato Associado - Atualiza Ano # # # # #
            foreach (System.Web.UI.WebControls.ListItem lAno in ddlAnoExtratoAssoc.Items)
            {
                if (lAno.Value == anoAtual)
                {
                    lAno.Selected = true;
                }
            }
            //# # # # # Extrato Convênio - Atualiza Ano # # # # #
            foreach (System.Web.UI.WebControls.ListItem lAnoConv in ddlAnoExtratoConv.Items)
            {
                if (lAnoConv.Value == anoAtual)
                {
                    lAnoConv.Selected = true;
                }
            }
            //# # # # # Relatório de Entrega Convênio - Atualiza Ano # # # # #
            /*
            foreach (System.Web.UI.WebControls.ListItem lAnoConv in ddlAno.Items)
            {
                if (lAnoConv.Value == anoAtual)
                {
                    lAnoConv.Selected = true;
                }
            }
            */
            // Fatura Mensal
            foreach (System.Web.UI.WebControls.ListItem lAnoFatMensal in ddlFatMensalAno.Items)
            {
                if (lAnoFatMensal.Value == anoAtual)
                {
                    lAnoFatMensal.Selected = true;
                }
            }

        }//atualizaDdlExtrato

        /*
                 protected String tamanhoTela() {
            int widht = 1001;
            if (Request.Form["hfTamanhoTela"] != null)
            {
                //widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
                widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
            }

            return widht.ToString();
        }
         */
        protected String tamanhoTela()
        {
            string widht = "1001";
            if (Request.Form["hfTamanhoTela"] != null)
            {
                //widht = int.Parse(Request.Form["hfTamanhoTela"].ToString());
                widht = Request.Form["hfTamanhoTela"].ToString();
            }
            else
            {
                widht = "1001";
            }

            return widht;
        }

        /*#############Teste Saldo ############*/

        protected String dtIni_Saldo()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + mes + "-20'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) - 1) + "-" + "12" + "-20'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + mes + "-20'";
                }
                else
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) - 1).ToString().PadLeft(2, '0') + "-20'";
                }
            }

            return periodo;
        }//Início_Saldo

        protected String DtFim_Saldo()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + ano + "-" + mes + "-19'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + ano + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + ano + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//Fim_Saldo

        protected String DtFim_SaldoNovoCartao()
        {
            string periodo = "";

            string hoje = DateTime.Now.ToString("dd-MM-yyyy");
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.ToString("MM");
            string ano = DateTime.Now.Year.ToString();

            //Chaca Janeiro
            if (mes == "01")
            {
                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + mes + "-19'";
                }
            }
            else
            {

                if (Convert.ToInt32(dia) >= 20)
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + (Convert.ToInt32(mes) + 1).ToString().PadLeft(2, '0') + "-19'";
                }
                else
                {
                    periodo += "'" + (Convert.ToInt32(ano) + 1).ToString() + "-" + mes + "-19'";
                }
            }

            return periodo;
        }//FimSaldoNovoCartao
        public double SaldoAssociado()
        {
            BLL ObjDados = new BLL(conectVegas);
            
            string IdAssoc = Session["IdAssoc"].ToString();

            string query = " SELECT titular, credito, " +
                           " (SELECT IF(m.valor IS NOT NULL, (SUM(m.valor) + a.credito), SUM(m.valor))" +
                           " FROM comovime AS m" +
                           "  LEFT JOIN associa AS a ON a.idassoc = m.associado" +
                           " WHERE a.idassoc IN('" + IdAssoc + "')" +
                           "  AND a.cnscanmom IS NULL" +
                           "  AND m.cnscanmom IS NULL" +
                           "  AND IF(a.credmodelo IN('vlparce'), m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_Saldo() + ", m.vencimento BETWEEN " + dtIni_Saldo() + " AND " + DtFim_SaldoNovoCartao() + ")) as Saldo " +
                           " FROM associa" +
                           " WHERE idassoc IN('" + IdAssoc + "')AND cnscanmom IS NULL";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            double saldo = 0;


            if (String.IsNullOrEmpty(dados.Rows[0]["saldo"].ToString()))
            {
                saldo = Convert.ToDouble(dados.Rows[0]["credito"].ToString());
            }
            else
            {
                saldo = Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
            }


            //return query;
            return saldo;

        }

        /**/

        public String extratoAssociado()
        {
            checarSessao();                        

            BLL ObjDbASU = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            //MessageBox.Show(SaldoAssociado());

            string xRet = "";
            string xPdf = "";

            //MessageBox.Show("Query: " + Apoio.SaldoAssociadoString());

            Apoio.Ano = ddlAnoExtratoAssoc.SelectedValue;
            Apoio.Mes = ddlMesExtratoAssoc.SelectedValue;

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string IdAssoc = "";

            string inicio = Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2));
            

            if (tCampo == 9 || tCampo == 11)
            {
                IdAssoc = Session["IdAssoc"].ToString();
            }

            identificarFuncionarios(IdAssoc);//Parei aqui, colocar a ID do ASSOciado

            DateTime agora = DateTime.Now;
            DateTime mesInicio = DateTime.Now.AddMonths(-1);
            DateTime mestFim = DateTime.Now;
            if (ObjDbASU.MsgErro == "")
            {
                if (tCampo == 9 || tCampo == 11)
                {
                    string query = " SELECT a.credmodelo, deb.iddebito AS idmovime, EXTRACT(DAY FROM deb.dtvencim) AS dia, deb.convenio, deb.descricao AS conveniado, deb.associado, a.titular, 'ASU' AS cartao, deb.dependen, d.nome AS comprador, deb.valor, deb.dtvencim AS vencimento, deb.dtlimite AS DATA, deb.parcela, deb.parctot, deb.cnscadmom, a.credito, '0.00' AS gastos, " +
                                   " (SELECT SUM(valor) FROM assocdeb AS deb INNER JOIN associa AS a ON a.idassoc = deb.associado INNER JOIN asdepen AS d ON deb.dependen = d.iddepen WHERE deb.associado='" + IdAssoc + "' AND deb.tipo NOT IN ('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB' ) AND deb.dtvencim BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND deb.cnscanmom IS NULL LIMIT 1) AS debitos " +
                                   " FROM assocdeb AS deb " +
                                   " LEFT JOIN coconven AS co ON co.idconven = deb.convenio " +
                                   " LEFT JOIN associa AS a ON deb.associado = a.idassoc " +
                                   " LEFT JOIN asdepen AS d ON deb.dependen = d.iddepen " +
                                   " WHERE deb.associado = '" + IdAssoc + "' AND deb.tipo NOT IN('FECHAM', 'RETBAN', 'RETBA2', 'RETCEF', 'RETNCX', 'RETSIC', 'RETBB') " +
                                   " AND deb.dtvencim BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND deb.cnscanmom IS NULL " +
                                   " UNION " +
                                   " SELECT a.credmodelo, c.idmovime AS id, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito, " +
                                   " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '2022-10-20' AND '2022-11-19' AND c.cnscanmom IS NULL LIMIT 1) AS gastos, '0.00' AS debitos   " +
                                   " FROM comovime AS c" +
                                   " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                   " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                   " INNER JOIN asdepen AS d ON c.dependen = d.iddepen " +
                                   " WHERE a.idassoc = '" + IdAssoc + "' AND c.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL ORDER BY dia ";

                    ObjDbASU.Query = query;

                        
                    //MessageBox.Show("Do Extrato: " + iDAcesso.Substring(0,(tCampo- 2)));


                    //MessageBox.Show(ObjDbASU.Query);

                    //MessageBox.Show(Apoio.SaldoAssociadoT());

                    DataTable dados = ObjDbASU.RetQuery();

                    Apoio.IdAssoc = IdAssoc; //Atribui Id do associado ao Método Apoio.IdAssoc, para calcular os gastos

                    string mesAtual = DateTime.Now.Month.ToString();
                    string diaAtual = DateTime.Now.Day.ToString();

                    string style = "<style> " +
                        ".defaultTable {" +
                        "                    }" +
                        " .defaultTable table {" +
                        "                    }    " +
                        ".defaultTable caption {" +
                        " background-color: #22396f;" +
                        " margin: 0;" +
                        " margin-top: 30px;" +
                        " padding: 10px 0 2px; " +
                        " font-size: 1.8em;" +
                        " color: white;" +
                        "}" +
                        " .defaultTable tr, td, th {" +
                        "/*border: thin solid #ccc;*/" +
                        " min-width: 15px;" +
                        " height: auto; " +
                        " border-bottom: none; " +
                        " border-right: none; " +
                        " padding: 5px 18px 2px 18px; " +
                        " font-size: .82em;" +
                        " /*font-size: 11px;*/" +
                        " font-weight: 300;" +
                        //" background-color: red; " +
                        " }" +
                        "   @media(max - width: 1000px) {" +
                        " table{ " +
                        " width: 120 %; " +
                        "} " +
                        " tr, td, th { " +
                        " font-size: 1.3em; " +
                        "} " +
                        "} " +
                        " .defaultTable td { " +
                        " text-align: center;" +
                        " } .defaultTable th " +
                        " { text-align: left; " +
                        "}" +
                        " .defaultTable thead th {" +
                        " text-align: center;" +
                        " background-color: #f26907;" +
                        "}" +
                        ".defaultTable thead td {" +
                        " background-color: #7693d5;" +
                        "} " +
                        " .defaultTable thead tr th:first-child {" +
                        " /*text-align: center;*/" +
                        " }" +
                        " .defaultTable tfoot td {" +
                        " font-size: 1.8em;" +
                        " color: white;" +
                        " background-color: #7693d5;" +
                        " padding: 5px 0;" +
                        " }" +
                        " .right {" +
                        " text-align: right;" +
                        " padding-right: 15px;" +
                        " }" +
                        " .left {" +
                        " text-align: left;" +
                        "}" +
                        " .defaultTable tbody tr: nth-child(odd) { /*Altera a cor da linha*/" +
                        " background-color: #ebf0f9;" +
                        " background-color: #d8e1f3;" +
                        "</style> ";

                    //# # # Mostra Limite e Saldo # # #
                    xRet += style;
                    //xRet += "<div class='extAssocNormal'>"; //StyleVoceOnLine.css
                    xRet += "<div class='saldoAssocNormal'>"; //StyleVoceOnLine.css
                    xRet += "<section Class='defaultTable'>";//Table-Extrato
                    xRet += "<table style='min-width: 500px;'>";
                    xRet += "<caption style='font-size: 0.8em; text-align: left;'>" + "Seja bem Vindo: " + Session["LoginUsuario"].ToString() + "</caption>";
                    xRet += "<tbody>";
                    xRet += "<tr></tr>";
                    xRet += "<tr>";
                    xRet += "<td>" + "Limite do seu Cartão " + "</td>";
                    xRet += "<td>" + "Saldo disponível neste momento " + "</td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td style='font-size: 26px;'>" + Apoio.limiteAssociado().ToString("C2") + "</td>";
                    //Exibe o Saldo apenas se o associado nao estiver Bloqueado
                    if (Apoio.bloqueioAssociado() == "Liberado")
                    {
                        if (Apoio.SaldoAssociado() > 0)
                        {
                            xRet += "<td style='font-size: 34px;'>" + Apoio.SaldoAssociado().ToString("C2") + "</td>";                            
                        }
                        else
                        {
                            xRet += "<td style='font-size: 34px;'>" + "R$ 0.00" + "</td>";
                        }
                    }
                    else
                    {
                        xRet += "<td style='font-size: 34px;'>" + "R$ 0.00" + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</section>";
                    xRet += "</div>";
                    //xRet += "<div class='aniversario'>";
                    xRet += Apoio.checarAniversario(IdAssoc);
                    //xRet += "</div>";

                    //# # # # # Fim - saldo e limite # # # # #                    

                    //double x = identificarTamanhoTela();
                    //tTela.Text = x.ToString();

                    if (Convert.ToInt32(tamanhoTela()) > 1000)
                    {
                        if (dados.Rows.Count > 0)
                        {
                            //xRet += "<div class='extAssocNormal' style='width: 1000px; '>";//StyleVoceOnLine.css
                            xRet += "<div class='extAssocNormal' style='width: 1000px; '>";//StyleVoceOnLine.css
                            //Cria Extrato
                            xRet += "<section Class='defaultTable'>";
                            xRet += "<table>";
                            //xRet += "<caption>" + " Suas Compras " + "</caption>";
                            xRet += "<caption>" + " Movimentação Financeira " + "</caption>";
                            //xRet += "<caption>" + " Extrato " + " - Período: " + Apoio.Periodo() + "</caption>";
                            xRet += "<thead>";//Cabeçalho da Tabela
                            xRet += "<tr>"; //Linha                                                
                            if (dados.Rows[0]["credmodelo"].ToString() == "VLPARCE")
                            {
                                //xRet += "<td colspan='7' class='right'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + ", Seu Saldo para o período selecionado: " + (Apoio.limiteAssociado() - Apoio.GastosAssociado()).ToString("C2") + "</td>";
                                xRet += "<td colspan='7' class='right'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                            }
                            else
                            {
                                xRet += "<td colspan='7' class='right'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                            }
                            xRet += "</tr>";
                            xRet += "<tr>"; //Linha
                            xRet += "<td>" + "Autorização" + "</td>";//Campo
                            xRet += "<td>" + "Momento" + "</td>";
                            xRet += "<td>" + "Convênio" + "</td>";
                            xRet += "<td>" + "Cartão" + "</td>";
                            xRet += "<td>" + "Comprador" + "</td>";
                            xRet += "<td>" + "Parcela(s)" + "</td>";
                            xRet += "<td>" + "Valor" + "</td>";
                            xRet += "</tr>";
                            xRet += "</thead>";
                            xRet += "<tfoot>";//Rodape da Tabela
                            xRet += "<tr>"; //Linha                            
                            xRet += "<td colspan='4' style='text-align: right; padding-right: 15px;'>" + "Total: " + "</td>";
                            //xRet += "<td colspan='3'>" + Apoio.GastosAssociado().ToString("C2") + " " + "</td>";
                            xRet += "<td colspan='3'>" + (Apoio.GastosAssociado() + Apoio.DebitosAssociado()).ToString("C2") + " " + "</td>";
                            xRet += "</tr>";

                            xRet += "</tfoot>";
                            xRet += "<tbody>";//Cabeçalho da Tabela
                            for (int i = 0; i < dados.Rows.Count; i++)
                            {
                                double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                                xRet += "<tr>";
                                xRet += "<td>" + dados.Rows[i]["idmovime"] + "</td>";//Autorização
                                xRet += "<td>" + dados.Rows[i]["cnscadmom"] + "</td>";//Momento
                                xRet += "<td>" + dados.Rows[i]["conveniado"] + "</td>";//Convênio
                                if (dados.Rows[i]["cartao"].ToString() == "ASU")
                                {
                                    xRet += "<td>" + dados.Rows[i]["cartao"] + "</td>";//Cartão
                                }
                                else
                                {
                                    xRet += "<td>" + dados.Rows[i]["cartao"] + " XX " + "</td>";//Cartão
                                }
                                xRet += "<td>" + dados.Rows[i]["comprador"] + "</td>";//Comprador
                                xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                                xRet += "<td>" + valorCompra.ToString("C2") + "</td>";//Valor
                                xRet += "</tr>";
                            }
                            xRet += "</tbody>";
                            xRet += "</table>";
                            xRet += "</section>";

                            //xRet += "teste";
                            xRet += "</div>";
                        }
                        else
                        {
                            xRet += "<div>";
                            xRet += "<p>" + " Seu cartão não gerou débitos para este período " + "</p>";
                            xRet += "</div>";
                        }



                    }
                    else
                    {
                        //# # # # # Extrato Fluído # # # # #

                        xRet += "<div class='extAssocFluido'>";//StyleVoceOnLine.css
                        xRet += "<table>";
                        xRet += "<caption style='font-size: 2em; text-align: left;'>" + "Seja bem Vindo: " + Session["LoginUsuario"].ToString() + "</caption>";
                        xRet += "<tbody>";
                        xRet += "<tr>";
                        xRet += "<td>" + "Limite do seu Cartão " + "</td>";
                        xRet += "<td>" + "Saldo disponível hoje " + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>";
                        xRet += "<td style='font-size: 30px; '>" + Apoio.limiteAssociado().ToString("C2") + "</td>";                                                
                        xRet += "<td style='font-size: 40px;'>" + Apoio.SaldoAssociado().ToString("C2") + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>";//Insere Uma linha para Espaçamento e utilizar cor
                        xRet += "<td colspan='2'></td>";                                              
                        xRet += "</tr>";
                        xRet += "</tbody>";
                        xRet += "</table>";
                        //Cria Extrato
                        xRet += "<table>";
                        //xRet += "<caption>" + " Suas Compras " + "</caption>";
                        xRet += "<caption>" + " Movimentação Financeira " + "</caption>";
                        xRet += "<thead>";//Cabeçalho da Tabela
                        xRet += "<tr>";
                        xRet += "</tr>";
                        xRet += "<tr>"; //Linha
                        xRet += "<td colspan='4' class='right'>" + "Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>"; //Linha
                        xRet += "<td>" + "dia" + "</td>";
                        //xRet += "<td colspan='4' style'text-align:left;'>" + "Compras" + "</td>";
                        xRet += "<td colspan='4' style'text-align:left;'>" + "" + "</td>";
                        xRet += "</tr>";
                        xRet += "</thead>";
                        xRet += "<tfoot>";//Rodape da Tabela
                        xRet += "<tr>"; //Linha
                        xRet += "<td colspan='3' class='right'>" + "Total: " + "</td>";
                        //xRet += "<td colspan='2'>" + Apoio.GastosAssociado().ToString("C2") + " " + "</td>";                        
                        xRet += "<td colspan='2'>" + (Apoio.GastosAssociado() + Apoio.DebitosAssociado()).ToString("C2") + " " + "</td>";
                        xRet += "</tr>";
                        xRet += "</tfoot>";
                        xRet += "<tbody>";//Cabeçalho da Tabela
                        for (int i = 0; i < dados.Rows.Count; i++)
                        {
                            double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                            xRet += "<tr>";
                            xRet += "<td>" + dados.Rows[i]["dia"] + "</td>";//Dia
                            xRet += "<td colspan='2' style='text-align: left;'>" + dados.Rows[i]["conveniado"] + "<br>" + "<span style='font-size: .7em; margin-left: 20px;'>" + dados.Rows[i]["comprador"] + "</span>" + "</td>";//Convênio                    
                            xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                            xRet += "<td>" + valorCompra.ToString("C2") + "</td>";//Valor
                            xRet += "</tr>";
                        }
                        xRet += "</tbody>";
                        xRet += "</table>";
                        xRet += "</div>";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Erro Original: " + ObjDbASU.MsgErro;
            }

            extratoAssoc = xPdf;

            //lblRetExtratoAssoc.Text = xRet;

            //MessageBox.Show(xRet);

            return xRet;
        }

        public String metodoCartoesAssoc()
        {
            checarSessao();

            lblMsg.Text = String.Empty;

            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";

            string codAcesso = Session["CodAcesso"].ToString();
            int tCampo = codAcesso.Length;
            string idAssoc = "";

            if (tCampo == 9 || tCampo == 11)
            {
                idAssoc = Session["idAssoc"].ToString();
            }
            string campos = " a.idassoc, d.iddepen, g.descricao AS grau, d.associado, car.idcartao, car.dependen, car.emissao, car.dt_inicio, car.dt_fim, car.validade, car.credito, a.titular, d.nome AS dependente ";
            string tabela = " asdepen AS d ";
            string left = " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                                    " INNER JOIN associa AS a ON a.idassoc = d.associado " +
                                    " INNER JOIN asdepcar AS car ON car.dependen = iddepen ";
            string condicao = " WHERE a.cnscanmom IS NULL " +
                                        " AND d.cnscanmom IS NULL " +
                                        " AND car.cnscanmom IS NULL " +
                                        " AND a.idassoc = '" + idAssoc + "' " +
                                        " AND((CURDATE() BETWEEN car.dt_inicio AND car.validade) OR(car.dt_fim IS NULL)) ";

            if (ObjDbASU.MsgErro == "")
            {
                ObjDbASU.Campo = campos;
                ObjDbASU.Tabela = tabela;
                ObjDbASU.Left = left;
                ObjDbASU.Condicao = condicao;

                DataTable dados;
                dados = ObjDbASU.RetCampos();

                int nLinhas = dados.Rows.Count;

                for (int i = 0; i < nLinhas; i++)
                {
                    string validade = dados.Rows[i]["validade"].ToString();
                    xRet += "<div class='divCartao'>";

                    string dv = dados.Rows[i]["idcartao"].ToString();

                    //xRet += "<div style='background-color: yellow; width: 302px; height: 195px;'>";
                    xRet += "<script>" +
                        "function clicou() {" +
                        "alert('Clicou no Cartão');" +
                        "} </script>";
                    xRet += "<div class='divCartaoImg' onclick='clicou()'>";
                    xRet += "<img src='../img/CartaoASUOnLine.jpg' />";
                    //xRet += "<img src='img/CartaoASUOnLine.jpg' style='width: 300px' onclick='recuperaDadosAssocSenha' />";                    
                    xRet += "</div>";
                    xRet += "<div class='divDadosCartao'>";
                    xRet += "<p class='divCartaoNome'>" + dados.Rows[i]["dependente"] + "</p>";
                    xRet += "<p class='divCartaoNumero'>" + dados.Rows[i]["idcartao"] + "xx" + "</p>";
                    //xRet += "<p style='margin: 0; margin-left: 28px; text-align: left; '>" + GeraDigMod11(Convert.ToInt64(dv)) + "</p>";

                    xRet += "<p class='divCartaoValidade'>" + " Validade: " + DateTime.Parse(validade).ToString("dd-MM-yyyy") + "</p>";
                    xRet += "</div>" + "<br>";

                    xRet += "</div>";

                }

                xRet += "<br><br><br><br>";
            }
            else
            {
                xRet += "Erro: " + ObjDbASU.MsgErro;
            }

            return xRet;
        }

        public String Titulo()
        {
            BLL ObjDados = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string retorno = "";

            string campos = " conv.nome ";
            string tabela = " coconven AS CONV ";
            //string condicao = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + iDataIni.Value + "' AND '" + iDataFin.Value + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";
            string condicao = " WHERE(conv.idconven ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = conv.idconven AND c.cnpj_cpf = '" + iDAcesso + "')) AND conv.cnscanmom IS NULL ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            //MessageBox.Show(codAcesso + " T Campo: " + tCampo);

            if (tCampo == 14 || tCampo < 7)
            //tamanhocampo == 14 || tamanhocampo < 7
            {
                if ("" + Session["LoginConvenio"] != "")
                {
                    retorno += "<br><b>" + dados.Rows[0]["nome"].ToString() + "</b>";
                }
            }

            return retorno;
        }

        public String GerarIdConvenio()
        {
            BLL ObjDados = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string retorno = "";

            string campos = " conv.idconven ";
            string tabela = " coconven AS CONV ";
            //string condicao = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + iDataIni.Value + "' AND '" + iDataFin.Value + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";
            string condicao = " WHERE(conv.idconven ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = conv.idconven AND c.cnpj_cpf = '" + iDAcesso + "')) AND conv.cnscanmom IS NULL ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            //MessageBox.Show(codAcesso + " T Campo: " + tCampo);

            if (tCampo == 14 || tCampo < 7)
            //tamanhocampo == 14 || tamanhocampo < 7
            {
                if ("" + Session["LoginConvenio"] != "")
                {
                    retorno = dados.Rows[0]["idconven"].ToString();
                }
            }
            /*
            try
            {
                string dv = GeraDigMod11(Convert.ToInt64(retorno)).ToString();
                
                return dv;
            }
            catch (Exception e) {
                return e.Message;
            };
            */

            return retorno;
        }

        protected void carregarData()
        {
            int periodo = 10;

            DateTime hoje = DateTime.Now;
            DateTime p30 = DateTime.Now.AddDays(periodo);

            iDataIni.Text = hoje.ToString("yyyy-MM-dd");
            iDataFin.Text = p30.ToString("yyyy-MM-dd");
        }

        public void montarRelEntrega(object sender, EventArgs e)
        {
            BLL ObjDbExtPos = new BLL(conectVegas);
            BLL ObjDBRelEnt = new BLL(conectVegas);
            BLL ObjDbExtPre = new BLL(conectVegas);

            Apoio Apoio = new Apoio();

            //Variáveis
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;

            string gastosPre = "";

            string xRet = "";
            string xRet_Pre = "";

            //var Calendar_ini = cDataIni.SelectedDates;
            //var Calendar_fin = cDataFin.SelectedDates;
            var Calendar_ini = iDataIni.Text;
            var Calendar_fin = iDataFin.Text;

            /*
            if (Calendar_ini)
            {
                MessageBox.Show("Início não pode ser maior que fim");
            }*/


            //Apoio.Ano = ddlAno.SelectedValue;
            //Apoio.Mes = ddlMes.SelectedValue;

            //MessageBox.Show(iDataIni.Value + " AND " + iDataFin.Value);

            //MessageBox.Show(Apoio.Ano.ToString());
            //MessageBox.Show(Apoio.Mes.ToString());

            if (ObjDbExtPos.MsgErro == "" || ObjDbExtPre.MsgErro == "" || ObjDBRelEnt.MsgErro == "")
            {

                //string camposPos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS conveniado, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN " +Apoio.Periodo() + " LIMIT 1) AS gastos ";
                string camposPos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS conveniado, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, SUM(m.valor *-1) AS ValorCompra, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' LIMIT 1) AS gastos ";
                string tabelaPos = " comovime AS m ";
                string leftPos = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                                 " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                                 " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
                //string condicaoPos = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN " + Apoio.Periodo() + " GROUP BY m.link ORDER BY m.cnscadmom DESC ";
                string condicaoPos = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";

                ObjDbExtPos.Campo = camposPos;
                ObjDbExtPos.Tabela = tabelaPos;
                ObjDbExtPos.Left = leftPos;
                ObjDbExtPos.Condicao = condicaoPos;

                //MessageBox.Show("Select " + camposPos + " FROM " + tabelaPos + " " + leftPos + " " + condicaoPos);

                DataTable dadosPos = ObjDbExtPos.RetCampos();
                int nLinhasPos = dadosPos.Rows.Count;
                string gastos = "";
                if (nLinhasPos > 0)
                {
                    gastos = dadosPos.Rows[0]["gastos"].ToString();
                }

                //string camposRel = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS gastos ";
                string camposRel = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' LIMIT 1) AS gastos ";
                string tabelaRel = " comovime AS m ";
                string leftRel = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                                 " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                                 " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
                //string condicaoRel = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN " + Apoio.Periodo() + " GROUP BY m.link ORDER BY m.cnscadmom DESC ";
                string condicaoRel = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' ORDER BY m.cnscadmom DESC ";

                ObjDBRelEnt.Campo = camposRel;
                ObjDBRelEnt.Tabela = tabelaRel;
                ObjDBRelEnt.Left = leftRel;
                ObjDBRelEnt.Condicao = condicaoRel;

                //MessageBox.Show(camposRel+ tabelaRel + leftRel + condicaoRel);

                DataTable dadosRelEnt = ObjDBRelEnt.RetCampos();
                int nLinhasRelEnt = dadosRelEnt.Rows.Count;
                string gastosRelEnt = "";

                if (nLinhasRelEnt > 0)
                {
                    gastosRelEnt = dadosRelEnt.Rows[0]["gastos"].ToString();
                }

                //MessageBox.Show("Relatório de Entrega: " + iDAcesso.Substring(0,(tCampo-2)));

                //string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN " + Apoio.Periodo() + " LIMIT 1 ) AS gastosPre ";
                //string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + iDataIni.Value + "' AND '" + iDataFin.Value + "' LIMIT 1 ) AS gastosPre ";
                string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT SUM(m.qtde * -1) FROM cgc_movime AS m WHERE (m.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = m.conv_origid AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' LIMIT 1 ) AS gastosPre ";
                //(SELECT SUM(m.qtde * -1) FROM cgc_movime AS m WHERE (m.conv_origid = '14'                                          OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = m.conv_origid AND c.cnpj_cpf = '14'))               AND m.cnscanmom IS NULL AND m.vencimento BETWEEN '2022-02-20' AND '2022-06-19' LIMIT 1 ) AS gastosPre
                string tabelaPre = " CGC_MOVIME  AS mov ";
                string leftPre = " LEFT OUTER JOIN CGC_TPMOVIME tp ON tp.CODTIPO = mov.TPMOVIME " +
                                 " LEFT OUTER JOIN CGC_CARTAO car ON car.UUIDCARTAO = mov.UUIDCARTAO " +
                                 " LEFT OUTER JOIN CGC_CONTRATO  contr ON contr.UUIDCONTRATO = car.UUIDCONTRATO " +
                                 " LEFT OUTER JOIN CGC_CONTRXENTIDADE  contrxent ON contrxent.UUIDCONTRATO = contr.UUIDCONTRATO " +
                                 " LEFT OUTER JOIN ASDEPEN asdep ON asdep.IDDEPEN = contrxent.ORIGID AND contrxent.ORIGTAB = 'ASDEPEN' " +
                                 " LEFT OUTER JOIN ASSOCIA assoc ON assoc.IDASSOC = asdep.ASSOCIADO " +
                                 " LEFT OUTER JOIN BASE_ASSOCUNI unid ON unid.UNIDADE = assoc.TRAB_UNIDA " +
                                 " LEFT OUTER JOIN base_ASSOCDEP dpto ON dpto.DEPTO = assoc.TRAB_DPTO " +
                                 " LEFT OUTER JOIN COCONVEN CONV ON conv.IDCONVEN = mov.CONV_ORIGID AND mov.CONV_ORIGTAB = 'COCONVEN' ";
                //string condicaoPre = " WHERE mov.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' AND mov.vencimento BETWEEN " + Apoio.Periodo() + " ";
                string condicaoPre = " WHERE mov.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' AND mov.vencimento BETWEEN '" + Calendar_ini + "' AND '" + Calendar_fin + "' ";

                ObjDbExtPre.Campo = camposPre;
                ObjDbExtPre.Tabela = tabelaPre;
                ObjDbExtPre.Left = leftPre;
                ObjDbExtPre.Condicao = condicaoPre;

                //MessageBox.Show("SELECT" + camposPre + "FROM" + tabelaPre + leftPre + condicaoPre);

                DataTable dadosPre = ObjDbExtPre.RetCampos();

                int nLinhasPre = dadosPre.Rows.Count;

                string style = "<style> " +
                       ".relEntrega {" +
                       "                    }" +
                       " .relEntrega table {" +
                       "                    }    " +
                       ".relEntrega caption {" +
                       " background-color: #22396f;" +
                       " margin: 0;" +
                       " margin-top: 30px;" +
                       " padding: 10px 0 2px; " +
                       " font-size: 1.8em;" +
                       " color: white;" +
                       "}" +
                       " .relEntrega tr, td, th {" +
                       "/*border: thin solid #ccc;*/" +
                       " min-width: 25px;" +
                       " height: auto; " +
                       " border-bottom: none; " +
                       " border-right: none; " +
                       " padding: 5px 18px 2px 18px; " +
                       " font-size: .9em;" +
                       " /*font-size: 11px;*/" +    
                       " font-weight: 300;" +
                       //" background-color: red; " +
                       " }" +
                       "   @media(max - width: 1000px) {" +
                       " table{ " +
                       " width: 120 %; " +
                       "} " +
                       " tr, td, th { " +
                       " font-size: 1.3em; " +
                       "} " +
                       "} " +
                       " .relEntrega td { " +
                       " text-align: center;" +
                       " } .relEntrega th " +
                       " { text-align: left; " +
                       "}" +
                       " .relEntrega thead th {" +
                       " text-align: center;" +
                       " background-color: #f26907;" +
                       "}" +
                       ".relEntrega thead td {" +
                       " background-color: #7693d5;" +
                       "} " +
                       " .relEntrega thead tr th:first-child {" +
                       " /*text-align: center;*/" +
                       " }" +
                       " .relEntrega tfoot td {" +
                       " font-size: 1.8em;" +
                       " color: white;" +
                       " background-color: #7693d5;" +
                       " padding: 5px 0;" +
                       " }" +
                       " .right {" +
                       " text-align: right;" +
                       " padding-right: 15px;" +
                       " }" +
                       " .left {" +
                       " text-align: left;" +
                       "}" +
                       " .relEntrega tbody tr:nth-child(odd) { /*Altera a cor da linha*/" +
                       " background-color: #ebf0f9;" +
                       " background-color: #d8e1f3; }" +
                       " .relEntTitulo {height: 25px; width: 100 %; text-align:left; font-size: 16px;}" + 
                       "</style> ";


                if (nLinhasPos > 0)
                {
                    /*Relatório de Entrega Pós Pago*/
                    xRet += style;
                    xRet += "<div >";                    
                    xRet += "<div Class='relEntTitulo' >" + dadosPos.Rows[0]["conveniado"] + "</div>";
                    //xRet += "<div Class='relEntTitulo' >" + "Período Selecionado: " + Calendar_ini + " à " + Calendar_fin + "</div>";
                    xRet += "<div Class='relEntTitulo' >" + "Período Selecionado: " + Convert.ToDateTime(Calendar_ini).ToString("dd-MM-yyyy") + " à " + Convert.ToDateTime(Calendar_fin).ToString("dd-MM-yyyy") + "</div>";
                    xRet += "<div>";
                    //xRet += "<asp:label ID='lblPeriodo' Visible='true' >" + lblPeriodo.Text + "</asp:label>";
                    xRet += "</div>";

                    xRet += "<section Class='relEntrega' style='width:1000px;'>";
                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Relatório de Entrega";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td>" + "Sequência" + "</td>";
                    xRet += "<td>" + "Autorização" + "</td>";
                    xRet += "<td>" + "Cartão" + "</td>";
                    xRet += "<td>" + "Comprador" + "</td>";
                    xRet += "<td>" + "Qtde. Parcelas" + "</td>";
                    xRet += "<td>" + "Valor da Compra" + "</td>";
                    xRet += "</tr>";
                    xRet += "</thead>";
                    xRet += "<tfoot>"; //Footer
                    xRet += "<td colspan='5' style='font-size: 20px; text-align: right; margin-right: 10px;'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2' class='right' style='font-size: 22px;'>" + Convert.ToDecimal(gastos).ToString("C2") + " " + "</td>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPos; i++)
                    {
                        //double valor = double.Parse(dadosPos.Rows[i]["valor"].ToString());
                        double valor = double.Parse(dadosPos.Rows[i]["ValorCompra"].ToString());
                        int qtdeparc = int.Parse(dadosPos.Rows[i]["parctot"].ToString());
                        xRet += "<tr>";
                        xRet += "<td>" + (i + 1) + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["autorizacao"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["cartao"] + "XX" + "</td>";
                        xRet += "<td Class='left'>" + dadosPos.Rows[i]["titular"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["parctot"] + "</td>";
                        //xRet += "<td>" + Convert.ToDecimal(valor * qtdeparc).ToString("C2") + "</td>";
                        xRet += "<td>" + valor.ToString("C2") + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</section>";

                    double totParc1 = 0.00;
                    double totParc2 = 0.00;
                    double totParc3 = 0.00;
                    double totParc4 = 0.00;
                    double totParc5 = 0.00;
                    double totParc6 = 0.00;
                    double totParc7 = 0.00;
                    double totParc8 = 0.00;
                    double totParc9 = 0.00;
                    double totParc10 = 0.00;

                    if (nLinhasRelEnt > 0)
                    {
                        for (int i = 0; i < nLinhasRelEnt; i++)
                        {
                            double valor = double.Parse(dadosRelEnt.Rows[i]["valor"].ToString());
                            valor.ToString("F2", CultureInfo.InvariantCulture);

                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "1")
                            {
                                totParc1 = (totParc1 + valor);
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "2")
                            {
                                totParc2 = (totParc2 + valor);
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "3")
                            {
                                totParc3 = totParc3 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "4")
                            {
                                totParc4 = totParc4 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "5")
                            {
                                totParc5 = totParc5 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "6")
                            {
                                totParc6 = totParc6 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "7")
                            {
                                totParc7 = totParc7 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "8")
                            {
                                totParc8 = totParc8 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "9")
                            {
                                totParc9 = totParc9 + valor;
                            }
                            if (dadosRelEnt.Rows[i]["parcela"].ToString() == "10")
                            {
                                totParc10 = totParc10 + valor;
                            }
                        }
                    }
                    else
                    {
                        xRet += "<div><p>" + "Não há parcelamento!!!" + "</div></p>";
                    }
                    xRet += "<br>";
                    xRet += "<section Class='relEntrega'>";
                    xRet += "<table>";

                    xRet += "<thead style='font-size: 2em;'>";
                    xRet += "<td colspan='5' class='right' style='text-align: center;'>" + "Parcelas " + "</td>";
                    xRet += "</thead>";

                    xRet += "<tbody>";

                   
                    //Cria duas colunas com 5 linhas, para exibir as parcelas
                    xRet += "<tr>";
                    if (totParc1 > 0)
                    {
                        xRet += "<td>" + "Parcela 1: " + Convert.ToDecimal(totParc1).ToString("C2") + "</td>";
                    }
                    if (totParc6 > 0)
                    {
                        xRet += "<td>" + "" + "</td>";
                        xRet += "<td>" + "Parcela 6: " + Convert.ToDecimal(totParc6).ToString("C2") + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    if (totParc2 > 0)
                    {
                        xRet += "<td>" + "Parcela 2: " + Convert.ToDecimal(totParc2).ToString("C2") + "</td>";
                    }
                    if (totParc7 > 0)
                    {
                        xRet += "<td>" + "" + "</td>";
                        xRet += "<td>" + "Parcela 7: " + Convert.ToDecimal(totParc7).ToString("C2") + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    if (totParc3 > 0)
                    {
                        xRet += "<td>" + "Parcela 3: " + Convert.ToDecimal(totParc3).ToString("C2") + "</td>";
                    }
                    if (totParc8 > 0)
                    {
                        xRet += "<td>" + "" + "</td>";
                        xRet += "<td>" + "Parcela 8: " + Convert.ToDecimal(totParc8).ToString("C2") + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    if (totParc4 > 0)
                    {
                        xRet += "<td>" + "Parcela 4: " + Convert.ToDecimal(totParc4).ToString("C2") + "</td>";
                    }
                    if (totParc9 > 0)
                    {
                        xRet += "<td>" + "" + "</td>";
                        xRet += "<td>" + "Parcela 9: " + Convert.ToDecimal(totParc9).ToString("C2") + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    if (totParc5 > 0)
                    {
                        xRet += "<td>" + "Parcela 5: " + Convert.ToDecimal(totParc5).ToString("C2") + "</td>";
                    }
                    if (totParc10 > 0)
                    {
                        xRet += "<td>" + "" + "</td>";
                        xRet += "<td>" + "Parcela 10: " + Convert.ToDecimal(totParc10).ToString("C2") + "</td>";
                    }
                    xRet += "</tr>";

                    xRet += "<tr>";
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</section>";
                    xRet += "<br>";
                    xRet += "</div>";
                }
                else
                {
                    xRet += "<div>";
                    xRet += "<p>" + "Relatório de Entrega" + "</p>";
                    xRet += "<p>" + "Não houve Vendas para o Período Selecionado!" + "</p>";
                    xRet += "</div>";
                }

                /*Relatório de Entrega Pré Pago*/

                if (nLinhasPre > 0)
                {
                    if (!String.IsNullOrEmpty(dadosPre.Rows[0]["gastosPre"].ToString()))
                    {
                        gastosPre = dadosPre.Rows[0]["gastosPre"].ToString();
                    }
                    else
                    {
                        gastosPre = "0,00";
                    }

                    xRet_Pre += "<div >";
                    if (!String.IsNullOrEmpty(dadosPre.Rows[0]["convenionome"].ToString()))
                    {
                        xRet_Pre += "<div Class='relEntTitulo'>" + dadosPre.Rows[0]["convenionome"] + "</div>";
                    }
                    xRet_Pre += "<div Class='relEntTitulo'>" + "Período Selecionado: " + Convert.ToDateTime(Calendar_ini).ToString("dd-MM-yyyy") + " à " + Convert.ToDateTime(Calendar_fin).ToString("dd-MM-yyyy") + "</div>";
                    //xRet_Pre += "<div style='height: 25px; width:100%; text-align:left;'>" + "Período Selecionado: " + Calendar_ini + " à " + Calendar_fin + "</div>";
                    xRet_Pre += "<section Class='relEntrega'>";
                    xRet_Pre += "<table>";
                    xRet_Pre += "<caption>"; //Caption
                    xRet_Pre += "Relatório de Entrega - Pré Pago";
                    xRet_Pre += "</caption>";
                    xRet_Pre += "<thead>"; //Head
                    xRet_Pre += "<tr>";
                    xRet_Pre += "<td>" + "Sequência" + "</td>";
                    xRet_Pre += "<td>" + "Autorização" + "</td>";
                    xRet_Pre += "<td>" + "Parcela" + "</td>";
                    //xRet_Pre += "<td>" + "Lote" + "</td>";
                    xRet_Pre += "<td>" + "Cartão" + "</td>";
                    xRet_Pre += "<td>" + "Comprador" + "</td>";
                    xRet_Pre += "<td>" + "Valor" + "</td>";
                    xRet_Pre += "</tr>";
                    xRet_Pre += "</thead>";
                    xRet_Pre += "<tfoot>"; //Footer
                    xRet_Pre += "<td colspan='5' style='font-size: 20px; text-align: right; margin-right: 10px;'>" + "Total: " + "</td>";
                    xRet_Pre += "<td colspan='2' style='font-size: 22px; text-align: right;'>" + (Convert.ToDecimal(gastosPre) * -1).ToString("C2") + " " + "</td>";
                    xRet_Pre += "</tfoot>";
                    xRet_Pre += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPre; i++)
                    {
                        xRet_Pre += "<tr>";
                        xRet_Pre += "<td>" + (i + 1) + "</td>";
                        xRet_Pre += "<td>" + dadosPre.Rows[i]["autorizacao"] + "</td>";
                        xRet_Pre += "<td>" + dadosPre.Rows[i]["ParcelaDesc"] + "</td>";
                        xRet_Pre += "<td>" + dadosPre.Rows[i]["numcartao"] + "</td>";
                        xRet_Pre += "<td>" + dadosPre.Rows[i]["dependente"] + "</td>";
                        xRet_Pre += "<td>" + Convert.ToDecimal(dadosPre.Rows[i]["valor"]).ToString("C2") + "</td>";
                        xRet_Pre += "</tr>";
                        xRet_Pre += "<tr>";
                        xRet_Pre += "</tr>";
                        xRet_Pre += "<tr>";
                        xRet_Pre += "</tr>";
                    }
                    xRet_Pre += "</tbody>";
                    xRet_Pre += "</table>";
                    xRet_Pre += "</section>";
                    xRet_Pre += "</div>";
                }
                else
                {
                    xRet_Pre += "<div>";
                    xRet_Pre += "<p>" + "Relatório de Entrega: Cartão Pré Pago" + "</p>";
                    xRet_Pre += "<p>" + "Não houve Vendas para o Período Selecionado!" + "</p>";
                    xRet_Pre += "</div>";
                    //MessageBox.Show("Não houve Vendas No Cartão Pós Pago!");
                }
            }
            else
            {
                if (ObjDbExtPos.MsgErro != "")
                {
                    xRet += "Erro no ObjDbExtPos: " + ObjDbExtPos.MsgErro;
                }
                else if (ObjDbExtPre.MsgErro != "")
                {
                    xRet_Pre += "Erro no ObjDbExtPre: " + ObjDbExtPre.MsgErro;
                }
                else if (ObjDBRelEnt.MsgErro != "")
                {
                    xRet += "Erro no ObjDbRelEnt: " + ObjDBRelEnt.MsgErro;
                }
            }

            lblRelEntrega.Text = xRet;
            lblRelEntregaPre.Text = xRet_Pre;

            //divPrintRelEntregaPre.Attributes["class"] = "mostrarCampos";
            //divPrintRelEntrega.Attributes["class"] = "mostrarCampos";
            //retRelEntregaPre.Attributes["class"] = "mostrarCampos";

            //MessageBox.Show(xRet);
        }//montarRelEntrega

        public void montarRelMensal()
        {
            BLL ObjDadosLinha = new BLL(conectVegas);
            BLL ObjDadosColuna = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            Apoio.Ano = ddlFatMensalAno.SelectedValue;
            Apoio.Mes = ddlFatMensalMes.SelectedValue;

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;

            //int idConv = 224;
            string xRet = "";
            string xTab = "";

            //string campos_mes = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS gastos ";
            string campos_linha = " parcela, m.parctot, m.data, vencimento,lote, SUM(valor) AS valor, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS gastos, (SELECT idconven FROM coconven AS c WHERE m.convenio = c.idconven) AS idConv ";
            //string campos_parc = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.vencimento BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS gastos ";
            string campos_coluna = " parcela, m.parctot, m.data, vencimento,lote, SUM(valor) AS valor, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.vencimento BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS tot_vendas, (SELECT idconven FROM coconven AS c WHERE m.convenio = c.idconven) AS idConv ";
            string tabela = " comovime AS m ";
            string left = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                          " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                          " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
            string condicao_linha = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN " + Apoio.Periodo() + " GROUP BY m.parcela ORDER BY m.cnscadmom, parcela ASC ";
            //string condicao_parc = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.Periodo() + " GROUP BY m.parcela ORDER BY m.cnscadmom DESC ";
            string condicao_coluna = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " GROUP BY m.parcela ORDER BY m.cnscadmom DESC ";
            //string parc2 = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.Periodo() + " GROUP BY m.parcela ORDER BY m.cnscadmom DESC ";
            //string parc3 = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.Periodo() + " GROUP BY m.parcela ORDER BY m.cnscadmom DESC ";
            string parc4 = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " GROUP BY m.parcela ORDER BY m.cnscadmom DESC ";

            lblFaturaMensal.Text = "Em Brave!";


            if (ObjDadosLinha.MsgErro == "" || ObjDadosColuna.MsgErro == "")
            {
                ObjDadosLinha.Campo = campos_linha;
                ObjDadosLinha.Tabela = tabela;
                ObjDadosLinha.Left = left;
                ObjDadosLinha.Condicao = condicao_linha;

                ObjDadosColuna.Campo = campos_coluna;
                ObjDadosColuna.Tabela = tabela;
                ObjDadosColuna.Left = left;
                ObjDadosColuna.Condicao = condicao_coluna;

                DataTable dados_linha = ObjDadosLinha.RetCampos();
                DataTable dados_coluna = ObjDadosColuna.RetCampos();

                // lblFaturaMensal.Text +=  "SELECT " + campos_linha + " FROM " + tabela + "" + left + "" + condicao_linha;
                // lblFaturaMensal.Text +=  "SELECT " + campos_coluna + " FROM " + tabela + "" + left + "" + condicao_linha;

                if (dados_linha.Rows.Count > 0 || dados_coluna.Rows.Count > 0)
                {
                    //MessageBox.Show("Coluna: SELECT " + campos_coluna + " FROM " + tabela + "" + left + "" + condicao_coluna);
                    //MessageBox.Show("Linha: SELECT " + campos_linha + " FROM " + tabela + "" + left + "" + condicao_linha);

                    //LayOut Fatura Mensal

                    xTab += "<section Class='defaultTable'>";
                    xTab += "<table class='fatMensal'>";
                    xTab += "<caption>Fatura Mensal</caption>";
                    xTab += "<thead class='tabHead'>";
                    xTab += "<tr>";
                    xTab += "<td rowspan = '2' > Venda </ td >";
                    xTab += "<td colspan='11'>Recebimento</td>";
                    xTab += "</tr>";
                    xTab += "<tr>";
                    xTab += "<td>Recebimento</td>";
                    xTab += "<td>Parcela 2</td>";
                    xTab += "<td>Parcela 3</td>";
                    xTab += "<td>Parcela 4</td>";
                    xTab += "<td>Parcela 5</td>";
                    xTab += "<td>Parcela 6</td>";
                    xTab += "<td>Parcela 7</td>";
                    xTab += "<td>Parcela 8</td>";
                    xTab += "<td>Parcela 9</td>";
                    xTab += "<td>Parcela 10</td>";
                    xTab += "<td>Total</td>";
                    xTab += "</tr>";
                    xTab += "</thead>";
                    xTab += "<tbody class='tabBody'>";
                    xTab += "<tr>";
                    xTab += "<td>Mês Corrente</td>";
                    //Obter Parcelas do Mês
                    for (int i = 0; i < dados_linha.Rows.Count; i++)
                    {
                        double valor = Convert.ToDouble(dados_linha.Rows[i]["valor"]);
                        xTab += "<td>" + (valor * (-1)).ToString("C2") + "</td>";
                    }
                    if (dados_linha.Rows.Count > 0)
                    {
                        xTab += "<td>" + (Convert.ToDouble(dados_linha.Rows[0]["gastos"]) * (1)).ToString("C2") + "</td>";
                    }
                    else
                    {
                        xTab += "<td>" + 0.00 + "</td>";
                    }
                    xTab += "</tr>";
                    //Obter Parcelas Futuras
                    for (int i = 1; i < dados_coluna.Rows.Count; i++)
                    {
                        xTab += "<tr>";
                        xTab += "<td> Ref. Parcela " + (i + 1) + "</td>";
                        xTab += "<td>" + (Convert.ToDecimal(dados_coluna.Rows[i]["valor"]) * (-1)).ToString("C2") + "</td>";
                        // for (int j = 0; j < 2; i++)
                        //{
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        xTab += "<td>" + "</td>";
                        //}
                        xTab += "</tr>";
                    }
                    xTab += "<td>Total</td>";
                    if (!String.IsNullOrEmpty(dados_coluna.Rows[0]["tot_vendas"].ToString()))
                    {
                        xTab += "<td>" + Convert.ToDecimal(dados_coluna.Rows[0]["tot_vendas"]).ToString("C2") + "</td>";
                    }
                    else
                    {
                        xTab += "<td>" + "0,00" + "</td>";
                    }
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td>" + "" + "</td>";
                    xTab += "<td></td>";
                    xTab += "<td></td>";
                    xTab += "</tr>";
                    xTab += "<tr>";
                    xTab += "</tbody>";
                    xTab += "<tfoot></tfoot>";
                    xTab += "</table>";
                    xRet += "</section>";
                }
                else
                {
                    xRet += "Não Gerou dados para Retorno";
                }
            }
            else
            {
                xRet += "Dados Mês: " + ObjDadosLinha.MsgErro + ", Dados Parcelas Futuras: " + ObjDadosColuna.MsgErro;
            }

            lblFaturaMensal.Text = xRet + xTab;

        }//montarRelMensal

        public void montarExtratoConv()
        {
            BLL ObjDbExtPos = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            //Atribui Ano e Mês ao método Período
            Apoio.Ano = ddlAnoExtratoConv.SelectedValue;
            Apoio.Mes = ddlMesExtratoConv.SelectedValue;

            //Variáveis
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string gastos = "";
#pragma warning disable CS0219 // A variável "gastosPre" é atribuída, mas seu valor nunca é usado
            string gastosPre = "";
#pragma warning restore CS0219 // A variável "gastosPre" é atribuída, mas seu valor nunca é usado
            string xRet = "";

            //Retorna o Digito Verificador 
            string rDigVerifica;

            if (tCampo == 9)
            {
                rDigVerifica = GeraDigMod11(Convert.ToInt32(iDAcesso));
                lblResultado.Text = rDigVerifica.Substring(7, 2);
                //MessageBox.Show(lblResultado.Text);
            }

            //Gera dados para Pós Pago          
            string camposPos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS conveniado, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.vencimento BETWEEN " + Apoio.Periodo() + " LIMIT 1) AS gastos  ";
            string tabelaPos = " comovime AS m ";
            string leftPos = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                             " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                             " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
            string condicaoPos = " WHERE (m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.Periodo() + " ORDER BY m.cnscadmom DESC ";
            //AND m.cnscanmom IS NULL AND m.data BETWEEN '2022-05-20' AND '2022-06-19'

            //lblMsg.Text = Apoio.Periodo();

            if (ObjDbExtPos.MsgErro == "")
            {

                ObjDbExtPos.Campo = camposPos;
                ObjDbExtPos.Tabela = tabelaPos;
                ObjDbExtPos.Left = leftPos;
                ObjDbExtPos.Condicao = condicaoPos;

                DataTable dadosPos = ObjDbExtPos.RetCampos();

                //MessageBox.Show(camposPos + tabelaPos + leftPos + condicaoPos);
                //MessageBox.Show(Apoio.Periodo());
                //MessageBox.Show(Apoio.vendasConvenio().ToString());
                //MessageBox.Show(iDAcesso.Substring(0, (tCampo - 2)));


                //Apoio.IdConv = dadosPos.Rows[0]["convenio"].ToString(); - 07/12/2021, testar o impacto deste cara


                int nLinhasPos = dadosPos.Rows.Count;

                if (nLinhasPos > 0)
                {
                    gastos = dadosPos.Rows[0]["gastos"].ToString();

                }

                //lblPeriodo.Text = dtIni + " à " + dtFim;
                //lblPeriodo.Text = Apoio.Periodo();

                string style = "<style> " +
                       ".extConv {" +
                       "                    }" +
                       " .extConv table {" +
                       "                    }    " +
                       ".extConv caption {" +
                       " background-color: #22396f;" +
                       " margin: 0;" +
                       " margin-top: 30px;" +
                       " padding: 10px 0 2px; " +
                       " font-size: 1.8em;" +
                       " color: white;" +
                       "}" +
                       " .extConv tr, td, th {" +
                       "/*border: thin solid #ccc;*/" +
                       " min-width: 25px;" +
                       " height: auto; " +
                       " border-bottom: none; " +
                       " border-right: none; " +
                       " padding: 5px 18px 2px 18px; " +
                       " font-size: 10px;" +
                       " /*font-size: 11px;*/" +
                       " font-weight: 300;" +
                       //" background-color: red; " +
                       " }" +
                       "   @media(max-width: 1000px) {" +
                       " table{ " +
                       " width: 120 %; " +
                       "} " +
                       " tr, td, th { " +
                       " font-size: 1.3em; " +
                       "} " +
                       "} " +
                       " .extConv td { " +
                       " text-align: center;" +
                       " } " +
                       " .extConv th " +
                       " { text-align: left; " +
                       "}" +
                       " .extConv thead th {" +
                       " text-align: center;" +
                       " background-color: #f26907;" +
                       "}" +
                       ".extConv thead td {" +
                       " background-color: #7693d5;" +
                       "} " +
                       " .extConv thead tr th:first-child {" +
                       " /*text-align: center;*/" +
                       " }" +
                       " .extConv tfoot td {" +
                       " font-size: 1.8em;" +
                       " color: white;" +
                       " background-color: #7693d5;" +
                       " padding: 5px 0;" +
                       " }" +
                       " .right {" +
                       " text-align: right;" +
                       " padding-right: 15px;" +
                       " }" +
                       " .left {" +
                       " text-align: left;" +
                       "}" +
                       " .extConv tbody tr:nth-child(odd) { /*Altera a cor da linha*/" +
                       " background-color: #ebf0f9;" +
                       " background-color: #d8e1f3; }" +
                       " .relEntTitulo {height: 25px; width: 100 %; text - align:left; font-size: 16px;}" +
                       "</style> ";

                if (nLinhasPos > 0)
                {
                    // xRet += "<div  id='print' class='conteudo' style='min-width: 990px; height: 600px; overflow: scroll; '>";
                    //xRet += "<div style='width: 200px;'>" + "<input type='button' onclick='cont();' value='Imprimir' >" + "</div>" + "<br>";
                    xRet += style;
                    xRet += "<section Class='extConv' style='width: 1000px;'>";
                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Extrato Mensal";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td colspan='4'>" + dadosPos.Rows[0]["conveniado"] + "</td>";
                    //xRet += "<td colspan='3'>" + " Período: " + ddlMesExtratoConv.Text + "/" + ddlAnoExtratoConv.SelectedValue + "</td>";
                    xRet += "<td colspan='3'>" + " Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td>" + "Momento" + "</td>";
                    xRet += "<td>" + "Autorização" + "</td>";
                    xRet += "<td>" + "Parcela" + "</td>";
                    xRet += "<td>" + "Lote" + "</td>";
                    xRet += "<td>" + "Cartão" + "</td>";
                    xRet += "<td>" + "Comprador" + "</td>";
                    xRet += "<td>" + "Valor" + "</td>";
                    xRet += "</tr>";
                    xRet += "</thead>";
                    xRet += "<tfoot>"; //Footer
                    xRet += "<td colspan='5' style='font-size: 20px; text-align: right; margin-right: 10px;'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2' style='font-size: 22px; text-align: right;'>" + Apoio.vendasConvenio(iDAcesso.Substring(0, (tCampo - 2)), iDAcesso).ToString("C2") + "</td>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPos; i++)
                    {
                        xRet += "<tr>";
                        xRet += "<td style='font-size: 8px;'>" + dadosPos.Rows[i]["momento"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["autorizacao"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["parcela"] + "/" + dadosPos.Rows[i]["parctot"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["lote"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["cartao"] + "XX" + "</td>";
                        xRet += "<td Class='left'>" + dadosPos.Rows[i]["comprador"] + "</td>";
                        xRet += "<td>" + "R$    " + dadosPos.Rows[i]["valor"] + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</section>";
                    xRet += "<br>";
                    // xRet += "</div>";
                }
                else
                {
                    //xRet += "<div>";
                    xRet += "<p>" + "Extrato: Cartão Pré Pago" + "</p>";
                    xRet += "<p>" + "Não houve Vendas para o Período Selecionado!" + "</p>";
                    //xRet += "</div>";
                }
            }
            else
            {

                xRet += "Erro Pós Pago: " + ObjDbExtPos.MsgErro;

            }

            lblExtratoConvenio.Text = xRet;

            //return xRet;
        }//montarExtratoConv

        public void montarExtratoPreConv()
        {
            BLL ObjDbExtPre = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            //Atribui Ano e Mês ao método Período
            Apoio.Ano = ddlAnoExtratoConv.SelectedValue;
            Apoio.Mes = ddlMesExtratoConv.SelectedValue;

            //Variáveis
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
#pragma warning disable CS0219 // A variável "gastos" é atribuída, mas seu valor nunca é usado
            string gastos = "";
#pragma warning restore CS0219 // A variável "gastos" é atribuída, mas seu valor nunca é usado
            string gastosPre = "";
            string xRet = "";

            //Retorna o Digito Verificador 
            string rDigVerifica;

            if (tCampo == 9)
            {
                rDigVerifica = GeraDigMod11(Convert.ToInt32(iDAcesso));
                lblResultado.Text = rDigVerifica.Substring(7, 2);

                //MessageBox.Show(lblResultado.Text);
            }

            //Gera Dados para Pré Pago

            string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT  SUM(m.qtde) AS gastosPre FROM cgc_movime AS m  WHERE (m.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS (SELECT NULL FROM coconven AS c WHERE c.idconven = m.CONV_ORIGID AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.vencimento BETWEEN " + Apoio.Periodo() + ") AS gastosPre ";
            //string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT  SUM(m.qtde) AS gastosPre FROM cgc_movime AS m  WHERE (m.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS (SELECT NULL FROM coconven AS c WHERE c.idconven = m.CONV_ORIGID AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.vencimento BETWEEN '"+ Apoio.Periodo() + "') AS gastosPre ";
            string tabelaPre = " CGC_MOVIME  AS mov ";
            string leftPre = " LEFT OUTER JOIN CGC_TPMOVIME tp ON tp.CODTIPO = mov.TPMOVIME " +
                             " LEFT OUTER JOIN CGC_CARTAO car ON car.UUIDCARTAO = mov.UUIDCARTAO " +
                             " LEFT OUTER JOIN CGC_CONTRATO  contr ON contr.UUIDCONTRATO = car.UUIDCONTRATO " +
                             " LEFT OUTER JOIN CGC_CONTRXENTIDADE  contrxent ON contrxent.UUIDCONTRATO = contr.UUIDCONTRATO " +
                             " LEFT OUTER JOIN ASDEPEN asdep ON asdep.IDDEPEN = contrxent.ORIGID AND contrxent.ORIGTAB = 'ASDEPEN' " +
                             " LEFT OUTER JOIN ASSOCIA assoc ON assoc.IDASSOC = asdep.ASSOCIADO " +
                             " LEFT OUTER JOIN BASE_ASSOCUNI unid ON unid.UNIDADE = assoc.TRAB_UNIDA " +
                             " LEFT OUTER JOIN base_ASSOCDEP dpto ON dpto.DEPTO = assoc.TRAB_DPTO " +
                             " LEFT OUTER JOIN COCONVEN CONV ON conv.IDCONVEN = mov.CONV_ORIGID AND mov.CONV_ORIGTAB = 'COCONVEN' ";
            string condicaoPre = " WHERE (mov.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = mov.CONV_ORIGID AND conv.cnpj_cpf = '" + iDAcesso + "')) AND mov.vencimento BETWEEN " + Apoio.Periodo() + " ";

            //lblMsg.Text = Apoio.Periodo();

            ObjDbExtPre.Campo = camposPre;
            ObjDbExtPre.Tabela = tabelaPre;
            ObjDbExtPre.Left = leftPre;
            ObjDbExtPre.Condicao = condicaoPre;

            if (ObjDbExtPre.MsgErro == "")
            {
                //MessageBox.Show("SELECT" + camposPre  + " FROM " + tabelaPre + leftPre + condicaoPre);

                DataTable dadosPre = ObjDbExtPre.RetCampos();
                int nLinhasPre = dadosPre.Rows.Count;

                //Apoio.IdConv = dadosPre.Rows[0]["conv_origid"].ToString();

                if (dadosPre.Rows.Count > 0)
                {
                    gastosPre = dadosPre.Rows[0]["gastosPre"].ToString();

                    //MessageBox.Show("SELECT " + camposPre + " FROM " + tabelaPre + leftPre + condicaoPre);
                }


                //lblPeriodo.Text = dtIni + " à " + dtFim;
                //lblPeriodo.Text = Apoio.Periodo();

                // # # # # # Extrato Pré Pago # # # # # 

                if (nLinhasPre > 0)
                {
                    //xRet += "<div id='print' class='conteudo' style='min-height: 200px; min-width: 990px; overflow: scroll; background-color: yellow;'>";
                    //xRet += "<div style='width: 200px;'>" + "<input type='button' onclick='cont();' value='Imprimir' >" + "</div>" + "<br>";
                    //xRet += "<div style='width: 200px;'>" + "<asp:Button ID='btnPdfConvPre' runat='server' Text='Gerar PDF' />" + "Aqui" + "</div>" + "<br>";
                    xRet += "<section Class='defaultTable'>";
                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Extrato Mensal - Pré Pago";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td colspan='3'>" + dadosPre.Rows[0]["ConvenioNome"] + "</td>";
                    //xRet += "<td colspan='2'>" + " Período: " + ddlMesExtratoConv.Text + "/" + ddlAnoExtratoConv.SelectedValue + "</td>";
                    xRet += "<td colspan='2'>" + " Período: " + Convert.ToDateTime(Apoio.dtDataInicio().Substring(1, (Apoio.dtDataInicio().Length - 2))).ToString("dd/MM/yyyy") + " á " + Convert.ToDateTime(Apoio.dtDataFim().Substring(1, (Apoio.dtDataFim().Length - 2))).ToString("dd/MM/yyyy") + "</td>";
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "<td>" + "Momento" + "</td>";
                    xRet += "<td>" + "Autorização" + "</td>";
                    //xRet += "<td>" + "Parcela" + "</td>";
                    //xRet += "<td>" + "Lote" + "</td>";
                    xRet += "<td>" + "Cartão" + "</td>";
                    xRet += "<td>" + "Comprador" + "</td>";
                    xRet += "<td>" + "Valor" + "</td>";
                    xRet += "</tr>";
                    xRet += "</thead>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPre; i++)
                    {
                        xRet += "<tr>";
                        xRet += "<td>";
                        xRet += dadosPre.Rows[i]["momento"];
                        xRet += "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["autorizacao"] + "</td>";
                        //xRet += "<td>" + dadosPre.Rows[i]["ParcelaDesc"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["numcartao"] + "</td>";
                        xRet += "<td Class='left'>" + dadosPre.Rows[i]["dependente"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["valor"] + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "<tfoot>"; //Footer
                    xRet += "<td colspan='4' style='font-size: 20px; text-align: right; margin-right: 10px;'>" + "Total: " + "</td>";                    
                    xRet += "<td colspan='2' class='right' style='font-size: 22px;'>" + Convert.ToDecimal(gastosPre).ToString("C2") + " " + "</td>";
                    xRet += "</tfoot>";
                    xRet += "</table>";
                    //  xRet += "</div>";
                    //xRet += "<div style=' width: 100%; height: 9px; margin-bottom: 140px; background-color: yellow;'>" + "</div>";
                }
                else
                {
                    //       xRet += "<div id='print' class='conteudo' style='height: 300px; overflow: scroll'>";
                    xRet += "<p>" + "Extrato Mensal: Cartão Pré Pago" + "</p>";
                    xRet += "<p>" + "Não houve Vendas para o Período Selecionado!" + "</p>";
                    //     xRet += "</div>";
                    //MessageBox.Show("Não houve Vendas No Cartão Pós Pago!");
                }
            }
            else
            {
                xRet += "Erro Pré Pago:" + ObjDbExtPre.MsgErro;
            }
            lblExtratoConvenioPre.Text = xRet;
        }//montarExtratoPreConv

        /* - - - Botões - - - */

        public void enviarDadosCorrecao(object sender, EventArgs e)
        {
            string xRet = "";

            xRet += "<section style='width: 70%; margin: 0 auto;'>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Pessoais" + "</p>";
            xRet += "<p>" + iNomeAssoc.Value + "</p>";
            xRet += "<p>" + "Cpf: " + iCpfAssoc.Value + "</p>";
            xRet += "<p>" + " RG:" + iRgAssoc.Value + "</p>";
            xRet += "<p>" + "E-mail: " + iEmailAssoc.Value + "</p>";
            xRet += "<p>" + " Telefone: " + iFoneAssoc.Value + "</p>";
            xRet += "<p>" + " Celular: " + iCelAssoc.Value + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados de Endereço" + "</p>";
            xRet += "<p>" + "Cep: " + iCepAssoc.Value + "</p>";
            xRet += "<p>" + " Rua: " + iRuaAssoc.Value + ", " + iNumCasaAssoc.Value + "</p>";
            xRet += "<p>" + "Complemento: " + iComplemAssoc.Value + "</p>";
            xRet += "<p>" + "Bairro: " + iBairroAssoc.Value + " - " + iCidadeAssoc.Value + "/" + iEstadoAssoc.Value + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados de Trabalho" + "</p>";
            xRet += "<p>" + "Unidade: " + iUnidadeAssoc.Value + "</p>";
            xRet += "<p>" + "Departamento: " + iDepartAssoc.Value + "</p>";
            xRet += "<p>" + "Setor: " + iSetorAssoc.Value + "</p>";
            xRet += "<p>" + "Função: " + iFuncaoAssoc.Value + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Bancários" + "</p>";
            xRet += "<p>" + "Banco: " + iBancoAssoc.Value + "</p>";
            xRet += "<p>" + "Agência: " + iAgenciaAssoc.Value + "</p>";
            xRet += "<p>" + "Conta Corrente: " + iContaAssoc.Value + "</p>";
            xRet += "</div>";
            xRet += "</section>";

            //lblDados.Text = xRet;

            /*Enviar para E-mail*/

            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            SmtpClient cliente = new SmtpClient();
            NetworkCredential credenciais = new NetworkCredential();

            //Configurar Cliente
            // cliente.Host = "smtp.gmail.com";
            cliente.Host = "smtp.asu.com.br";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            //Libera envio de e-mail validando certificados
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Credenciais de acesso
            //credenciais.UserName = "regisrvieira@gmail.com";
            credenciais.UserName = "reginaldo@asu.com.br";
            credenciais.Password = "c142795";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            string para = " ";
            MailMessage Msg = new MailMessage();

            para = "reginaldo@asu.com.br";
            //para = "regisrvieira@gmail.com";
            //para = "gsxtecnologia@gmail.com";

            Msg.IsBodyHtml = true;
            Msg.From = new MailAddress(para);
            Msg.Subject = "Alterações Cadastrais: " + iNomeAssoc.Value;
            Msg.Body = xRet + " <h1 style='color: red' >" + "Você Acaba de Receber o Primeiro e-mail enviado para alterações cadastrais do Associado, pelo novo Site da ASU" + "</h1> " + agora;
            Msg.To.Add(para);

            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                lblResultado.Text = "Sua mensagem foi enviada com sucesso!!! " + agora;
                //MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

            }
            catch (Exception ex)
            {
                lblResultado.Text = "Ocorreu problemas no envio da sua mensagem" + ex;
            }
            finally { cliente.Dispose(); }

            /* - - - - */

        }

        public void enviarDadosDoConvCorrecao(object sender, EventArgs e)
        {
            string xRet = "";

            xRet += "<section style='width: 70%; margin: 0 auto;'>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Cadastrais" + "</p>";
            xRet += "<p>" + iNomeConv.Value + "</p>";
            xRet += "<p>" + "Cpf: " + iCnpj.Value + "</p>";
            xRet += "<p>" + "E-mail: " + iEmailAssoc.Value + "</p>";
            xRet += "<p>" + " Telefone: " + iTelefoneConv.Value + "</p>";
            xRet += "<p>" + " Celular: " + iCelularConv.Value + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados de Endereço" + "</p>";
            xRet += "<p>" + "Cep: " + iCepConv.Value + "</p>";
            xRet += "<p>" + " Rua: " + iRuaConv.Value + ", " + iNumeroConv.Value + "</p>";
            xRet += "<p>" + "Complemento: " + iComplementConv.Value + "</p>";
            xRet += "<p>" + "Bairro: " + iBairroConv.Value + " - " + iCidadeConv.Value + "/" + iEstadoConv.Value + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados para Contato" + "</p>";
            xRet += "</div>";
            xRet += "<div style='width: 100%; text-align: left; background-color: #cfcaca; margin: 0;'>";
            xRet += "<p style='text-align: center; background-color: #808080; font-size: 2em; margin: 0; padding: 15px 0 5px 0; color: #fff; '>" + "Dados Bancários" + "</p>";
            xRet += "<p>" + "Banco: " + iBancoConv.Value + "</p>";
            xRet += "<p>" + "Agência: " + iAgenciaConv.Value + "</p>";
            xRet += "<p>" + "Conta Corrente: " + iContaCorrenteConv.Value + " - " + iDigCCConv.Value + "</p>";
            xRet += "</div>";
            xRet += "</section>";

            //lblDados.Text = xRet;

            /*Enviar para E-mail*/

            string agora = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " às " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

            SmtpClient cliente = new SmtpClient();
            NetworkCredential credenciais = new NetworkCredential();

            //Configurar Cliente
            // cliente.Host = "smtp.gmail.com";
            cliente.Host = "smtp.asu.com.br";
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.UseDefaultCredentials = false;

            //Libera envio de e-mail validando certificados
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Credenciais de acesso
            //credenciais.UserName = "regisrvieira@gmail.com";
            credenciais.UserName = "reginaldo@asu.com.br";
            credenciais.Password = "c142795";
            cliente.Credentials = credenciais;

            //Dados para Envio do e-mail
            string para = " ";
            MailMessage Msg = new MailMessage();

            para = "reginaldo@asu.com.br";
            //para = "regisrvieira@gmail.com";
            //para = "gsxtecnologia@gmail.com";

            Msg.IsBodyHtml = true;
            Msg.From = new MailAddress(para);
            Msg.Subject = "Alterações Cadastrais: " + iNomeConv.Value;
            Msg.Body = xRet + " <h1 style='color: red' >" + "Você Acaba de Receber o Primeiro e-mail enviado para alterações cadastrais do Associado, pelo novo Site da ASU" + "</h1> " + agora;
            Msg.To.Add(para);

            //Enviar Mensagem
            try
            {
                cliente.Send(Msg);
                lblMsgConv.Text = "Sua mensagem foi enviada com sucesso!!! " + agora;
                //MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

            }
            catch (Exception ex)
            {
                lblMsgConv.Text = "Ocorreu problemas no envio da sua mensagem" + ex;
            }
            finally { cliente.Dispose(); }

            /* - - - - */

        }

        public void atualizaPeriodo(object sender, EventArgs e)
        {
            //string wMes = ddlMes.SelectedValue;
#pragma warning disable CS0219 // A variável "dtIni" é atribuída, mas seu valor nunca é usado
            string dtIni = "";
#pragma warning restore CS0219 // A variável "dtIni" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "dtFim" é atribuída, mas seu valor nunca é usado
            string dtFim = "";
#pragma warning restore CS0219 // A variável "dtFim" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "msgPeriodo" é atribuída, mas seu valor nunca é usado
            string msgPeriodo = "";
#pragma warning restore CS0219 // A variável "msgPeriodo" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            //double widht = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //MessageBox.Show("Tamanho: " + widht);

            //lblPeriodo.Text = msgPeriodo;

            montarExtratoConv();
            montarExtratoPreConv();
            //Montar Relatório de Entrega
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            if (tCampo == 14 || tCampo < 7)
            {
                if ("" + Session["LoginConvenio"] != "")
                {
                    montarRelMensal();
                }
            }

        }

        //Método para Limpar extratos dos associados em Downloads
        public void limpaExtratos()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\");

            string arquivos = "";

            string IdAssoc = "";
            string codAcesso = Session["CodAcesso"].ToString();
            int tCampo = codAcesso.Length;

            if (tCampo == 9 || tCampo == 11)
            {
                IdAssoc = Session["IdAssoc"].ToString();

                foreach (FileInfo file in di.GetFiles())
                {
                    arquivos = file.Name;

                    if (arquivos == IdAssoc + ".pdf")
                    {
                        file.Delete();
                    }
                    else
                    {
                        //MessageBox.Show(arquivos);
                    }
                }
            }
        }

        protected void fazerLogof(object sender, EventArgs e)
        {
            DAL ObjConexao = new DAL(conectVegas);
            //Exclui PDFs Criados na sessão do Usuário

            ObjConexao.DesconectDal();

            limpaExtratos();

            Session.Abandon();
            Response.Redirect("http://www.asu.com.br");

        }


        public void escolherCartao(object sender, EventArgs e)
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";

            string codAcesso = Session["CodAcesso"].ToString();
            int tCampo = codAcesso.Length;
            string idAssoc = "";

            if (tCampo == 9 || tCampo == 11)
            {
                idAssoc = Session["idAssoc"].ToString();
            }
            string campos = " a.idassoc, d.iddepen, g.descricao AS grau, d.associado, car.idcartao, car.dependen, car.emissao, car.dt_inicio, car.dt_fim, car.validade, car.credito, a.titular, d.nome AS dependente ";
            string tabela = " asdepen AS d ";
            string left = " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                                    " INNER JOIN associa AS a ON a.idassoc = d.associado " +
                                    " INNER JOIN asdepcar AS car ON car.dependen = iddepen ";
            string condicao = " WHERE a.cnscanmom IS NULL " +
                                        " AND d.cnscanmom IS NULL " +
                                        " AND car.cnscanmom IS NULL " +
                                        " AND a.idassoc = '" + idAssoc + "' " +
                                        " AND((CURDATE() BETWEEN car.dt_inicio AND car.validade) OR(car.dt_fim IS NULL)) ";


            if (ObjDbASU.MsgErro == "")
            {
                ObjDbASU.Campo = campos;
                ObjDbASU.Tabela = tabela;
                ObjDbASU.Left = left;
                ObjDbASU.Condicao = condicao;

                DataTable dados;
                dados = ObjDbASU.RetCampos();

                int nLinhas = dados.Rows.Count;

                for (int i = 0; i < nLinhas; i++)
                {
                    string validade = dados.Rows[i]["validade"].ToString();
                    string dv = dados.Rows[i]["idcartao"].ToString();

                    xRet += "<div style='margin-top: 10px; width: 502px; min-height: 20px;'>";
                    xRet += "<p>" + dados.Rows[i]["dependente"] + " - " + GeraDigMod11(Convert.ToInt64(dv)) + "</p>";

                    xRet += "</div>";
                }
            }
            else
            {
                xRet += "Erro: " + ObjDbASU.MsgErro;
            }

            //lblListaCartoes.Text = xRet;

            habilitaTrocaSenhaCartaoAssoc();

            btnEscolherCartao.Visible = false;

        }//escolherCartao

        public void trocarSenhaCartao(object sender, EventArgs e)
        {

            //LayOut de Dados
            string codTransa = "910";//1-3
            string versao = "01.04";//4-5
            string codConv = "000000000";//9-9
            string codCart = "000000000"; //18-9 Criar método para capturar o número do catão
            string valor = "000000000000"; //27-12
            string parcela = "00"; //39-2
            string codRet = "000"; //41-3
            string autoriza = "000000000"; //44-9
            string chaveUsuar = "000000000"; //53-9
            string nome = "0000000000000000000000000000000000000000"; //62-40
            //string observa = "000000000000000000000000000000";//102-30
            string observa = "Senha alterado através do Site";//102-30
            string cpfCnpj = "00000000000000"; //132-14
            string saldo = "00000000000000"; //146-14
            string senha = iConfirmaNovaSenhaCartao.Value; //160-20

            string trocaSenha = "";


            if (!String.IsNullOrEmpty(iConfirmaNovaSenhaCartao.Value))
            {
                if (iNovaSenhaCartao.Value != iConfirmaNovaSenhaCartao.Value)
                {
                    lblMsg.Text = "As senhas não conferem, digite-as novamente!!!";
                    iNovaSenhaCartao.Focus();
                }
                else
                {
                    trocaSenha = codTransa + versao + codConv + codCart + valor + parcela + codRet + autoriza + chaveUsuar + nome + observa + cpfCnpj + saldo + senha;
                }
            }
            else
            {
                lblMsg.Text = "Senha: " + "Não confere com a senha do sistema. Tente novamente!!!";
                iSenhaAtual.Focus();
            }

            //Modelo do Arquivo a ser criado
            //string cArq_cont = "910" + "01.04" + "000000000" + Session["cartaosenha"] + "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" + txtSenha_nova_confirma.Text;
            //91001.0400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004321

            iNovaSenhaCartao.Value = String.Empty;
            iConfirmaNovaSenhaCartao.Value = String.Empty;

            divAlteraSenhaCartao.Attributes["class"] = "esconderCampos";
            //lblListaCartoes.Text = "Sua Senha foi alterada com Suceso! Ou, não!";
            lblListaCartoes.Text = trocaSenha;

            btnEscolherCartao.Visible = true;

        }//trocarSenhaCartao
        public void habilitaTrocaSenhaCartaoAssoc()
        {
            divAlteraSenhaCartao.Attributes["class"] = "mostrarCampos";
            lblListaCartoes.Text = String.Empty;
        }//habilitaTrocaSenhaCartaoAssoc


        public void trocarSenhaAssoc(object sender, EventArgs e)
        {

            BLL ObjDados = new BLL(conectVegas);
            BLL ObjUpDate = new BLL(conectVegas);
            BLL ObjLog = new BLL(conectVegas);

            string idAssoc = Session["idAssoc"].ToString();

            //Acessa dados Associado para comparar Senha
            //c = consulta
            string c_tabela = " associa ";
            string c_campos = " senha ";
            string c_condicao = " WHERE idassoc = '" + idAssoc + "' ";

            ObjDados.Campo = c_campos;
            ObjDados.Tabela = c_tabela;
            ObjDados.Condicao = c_condicao;

            DataTable dados = ObjDados.RetCampos();

            //l = Log
            string l_tabela = " ASSOCLOG ";
            string l_valores = " associado =  '" + idAssoc + "' , " + "dependen = '" + idAssoc + "', " + "descriacao = 'Alteração de Senha ADM', " + "detalhe = 'Senha Alterada através do Site: Antiga: " + dados.Rows[0]["senha"].ToString() + ", Nova: " + iSenhaConfirma.Value + "', " + "cnscadusu = 'SITE', " + "cnscadmom = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            //SET associado = "2747", dependen = "2747", descriacao = "Alteração de Senha ADM", detalhe = "Senha Alterada através do Site: Antiga: xxx, Nova: yyy", cnscadusu = "SITE", cnscadmom = "Agora"
            string l_condicao = " WHERE idassoc = '" + idAssoc + "' ";

            ObjLog.Tabela = l_tabela;
            ObjLog.Valores = l_valores;
            ObjLog.Condicao = l_condicao;

            string upDate = " UPDATE " + l_tabela + " SET " + l_valores + " " + l_condicao;

            //MessageBox.Show(upDate);

            //ObjUpDate.EditRegistro(l_tabela, l_valores, l_condicao);

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado



            if (ObjDados.MsgErro == "")
            {
                if (iSenhaAtual.Value == dados.Rows[0]["senha"].ToString())
                {
                    if (iSenhaNova.Value != iSenhaConfirma.Value)
                    {
                        lblMsg.Text = "As senhas não conferem, digite-as novamente!!!";
                        iSenhaAtual.Focus();
                    }
                    else
                    {
                        //UpDate: Altera Campo Senha
                        string tabela = " associa ";
                        string valores = " senha = '" + iSenhaConfirma.Value + "' ";
                        string condicao = " idassoc = '" + idAssoc + "' ";

                        ObjUpDate.Valores = valores;
                        ObjUpDate.Tabela = tabela;
                        ObjUpDate.Condicao = condicao;

                        ObjDados.EditRegistro(tabela, valores, condicao);
                        //trocaSenha = codTransa + versao + codConv + codCart + valor + parcela + codRet + autoriza + chaveUsuar + nome + observa + cpfCnpj + saldo + senha;                        
                        //lblTrocaSenha.Text = trocaSenha;
                        lblMsg.Text = "Sua senha foi alterada com Sucesso!!!";
                    }
                }
                else
                {
                    lblMsg.Text = "Senha: " + "Não confere com a senha do sistema. Tente novamente!!!";
                    iSenhaAtual.Focus();
                }

            }
            else
            {
                lblMsg.Text = ObjDados.MsgErro.ToString();
            }

        }//trocarSenhaAssoc

        public void troarSenhaCartao(object sender, EventArgs e)
        {
            //LayOut de Dados
#pragma warning disable CS0219 // A variável "codTransa" é atribuída, mas seu valor nunca é usado
            string codTransa = "910";//1-3
#pragma warning restore CS0219 // A variável "codTransa" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "versao" é atribuída, mas seu valor nunca é usado
            string versao = "01.04";//4-5
#pragma warning restore CS0219 // A variável "versao" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "codConv" é atribuída, mas seu valor nunca é usado
            string codConv = "000000000";//9-9
#pragma warning restore CS0219 // A variável "codConv" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "codCart" é atribuída, mas seu valor nunca é usado
            string codCart = "000000000"; //18-9
#pragma warning restore CS0219 // A variável "codCart" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "valor" é atribuída, mas seu valor nunca é usado
            string valor = "000000000000"; //27-12
#pragma warning restore CS0219 // A variável "valor" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "parcela" é atribuída, mas seu valor nunca é usado
            string parcela = "00"; //39-2
#pragma warning restore CS0219 // A variável "parcela" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "codRet" é atribuída, mas seu valor nunca é usado
            string codRet = "000"; //41-3
#pragma warning restore CS0219 // A variável "codRet" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "autoriza" é atribuída, mas seu valor nunca é usado
            string autoriza = "000000000"; //44-9
#pragma warning restore CS0219 // A variável "autoriza" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "chaveUsuar" é atribuída, mas seu valor nunca é usado
            string chaveUsuar = "000000000"; //53-9
#pragma warning restore CS0219 // A variável "chaveUsuar" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "nome" é atribuída, mas seu valor nunca é usado
            string nome = "0000000000000000000000000000000000000000"; //62-40
#pragma warning restore CS0219 // A variável "nome" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "observa" é atribuída, mas seu valor nunca é usado
            string observa = "000000000000000000000000000000";//102-30
#pragma warning restore CS0219 // A variável "observa" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "cpfCnpj" é atribuída, mas seu valor nunca é usado
            string cpfCnpj = "00000000000000"; //132-14
#pragma warning restore CS0219 // A variável "cpfCnpj" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado
            string saldo = "00000000000000"; //146-14
#pragma warning restore CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "senha" é atribuída, mas seu valor nunca é usado
            string senha = "novaSenha"; //160-20
#pragma warning restore CS0219 // A variável "senha" é atribuída, mas seu valor nunca é usado

#pragma warning disable CS0219 // A variável "trocaSenha" é atribuída, mas seu valor nunca é usado
            string trocaSenha = "";
#pragma warning restore CS0219 // A variável "trocaSenha" é atribuída, mas seu valor nunca é usado

            //Modelo do Arquivo a ser criado
            //string cArq_cont = "910" + "01.04" + "000000000" + Session["cartaosenha"] + "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" + txtSenha_nova_confirma.Text;
            //91001.0400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004321






            /*
             //Proceso do Paulo para Trocar senha do Associado
            string cErro = "";
            string cSenha_atual = "" + xFuncoes.RetCampo("senha", "associa", "idassoc = '" + Session["associado"] + "'", "ConexaoASU");

            if (cSenha_atual == "")
                cErro = "<span style='color:Red;'>Senha não cadastrada!</span>";
            else
            {
                if (cSenha_atual != txtSenha_atual.Text)
                {
                    cErro = "<span style='color:Red;'>A senha atual digitada está incorreta!</span>";
                }
                else
                {
                    //Gravando a nova senha
                    xFuncoes.UpdateReg("associa", "senha = '" + txtSenha.Text.Replace("'", "") + "'", "idassoc = '" + Session["associado"] + "'", "ConexaoASU");
                    cErro = "<span style='color:DarkGreen;'>A senha foi alterado com SUCESSO!</span>";

                    txtSenha.Text = "";
                    txtSenha_atual.Text = "";
                    txtSenha_conf.Text = "";
                }
            }

            //Trocando a senha do associado
            if (cErro != "")
                lblErro_assoc.Text = "<table><tr><td><img src='Img/Layout/alert.gif' /></td><td><span style='font-weight:bold;font-size:15px;'>" + cErro + "</span></td></tr></table><br><br>";

            */









            //caminho físico
            string pathDocumento = "" + WebConfigurationManager.AppSettings["CaminhoVendaENV"];
            //string pathDocumento = "d:\teste";
            DirectoryInfo dir = new DirectoryInfo(pathDocumento);

            string cArq_cont = "910" + "01.04" + "000000000" + Session["cartaosenha"] + "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" + iSenhaConfirma.Value;

            //titulo do arquivo
            string cArq_tit = "" + Session["cartaosenha"] + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
            //string cArq_tit = "" + "xxx" + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;

            try
            {
                GravaArquivo(System.IO.Path.Combine(pathDocumento, cArq_tit), cArq_cont);
            }
            catch
            {
            }

#pragma warning disable CS0219 // A variável "arquivosenha" é atribuída, mas seu valor nunca é usado
            string arquivosenha = "";
#pragma warning restore CS0219 // A variável "arquivosenha" é atribuída, mas seu valor nunca é usado

            Session["arquivosenha"] = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);
            //arquivosenha = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

            Retorna_senha();
        }

        protected void Retorna_senha() //Para trocar a Senha do Cartão
        {
            //Session["arquivosenha"]

            try
            {
                string cArquivo = LeArquivo("" + Session["arquivosenha"].ToString().Replace("ENV", "RET"));
#pragma warning disable CS0219 // A variável "cArquivo_mostra" é atribuída, mas seu valor nunca é usado
                string cArquivo_mostra = "";
#pragma warning restore CS0219 // A variável "cArquivo_mostra" é atribuída, mas seu valor nunca é usado

                string cCod_transa = "" + cArquivo.Substring(1, 3);//cod. Transação
                string cVersao = "" + cArquivo.Substring(3, 5); //Versão do Protocolo de Venda
                string cCodConv = "" + cArquivo.Substring(8, 9); //cod. do Convênio
                string cCodCarta = "" + cArquivo.Substring(17, 9); // Cod. do Cartão
                string cValor = "" + cArquivo.Substring(26, 12); //Valor da Venda
                string cParcela = "" + cArquivo.Substring(38, 2); //Quantidade de Parcelas
                string cCodRet = "" + cArquivo.Substring(40, 3);//cod. Retorno
                string cAutoriza = "" + cArquivo.Substring(43, 9); // Autorização da Compra
                string cChaveUsuar = "" + cArquivo.Substring(52, 9); // Chave do Usuário
                string cNome = "" + cArquivo.Substring(61, 40); //Nome do Conveniado
                string cObserva = "" + cArquivo.Substring(101, 30); //Observação sobre a Vendfa
                string cCpfCnpj = "" + cArquivo.Substring(131, 14); //CPF ou CNPJ
                string cSaldo = "" + cArquivo.Substring(145, 14); //Saldo do Associado
                string cSenha = "" + cArquivo.Substring(160, 20); //Senha 
                string cRet_msg = "";

                switch (cCodRet)
                {
                    case "000":
                        cRet_msg = "Transação realizada com sucesso";
                        break;
                    case "110":
                        cRet_msg = "Tempo esgotado para retorno";
                        break;
                    case "130":
                        cRet_msg = "Conexão com problemas";
                        break;
                    case "210":
                        cRet_msg = "Código do cartão inconsistente";
                        break;
                    case "320":
                        cRet_msg = "Cartão vencido";
                        break;
                    case "330":
                        cRet_msg = "Cartão não liberado";
                        break;
                    case "340":
                        cRet_msg = "Cartão cancelado";
                        break;
                    default:
                        cRet_msg = "Erro desconhecido";
                        break;
                }

                //verificando se a venda ocorreu
                if (cCodRet != "000")
                {
                    string cMsg_neg = "Ocorreram problemas nesse processo, tente novamente mais tarde: " + cRet_msg;
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Restrita.aspx?op=assocextra&menu=sim';", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cRet_msg + "');location.href='Restrita.aspx?op=assocextra&menu=sim';", true);
                }
            }
#pragma warning disable CS0168 // A variável "erro" está declarada, mas nunca é usada
            catch (Exception erro)
#pragma warning restore CS0168 // A variável "erro" está declarada, mas nunca é usada
            {
                if (nVezes <= Convert.ToInt16("" + WebConfigurationManager.AppSettings["Timeout_venda"]))
                {
                    System.Threading.Thread.Sleep(Convert.ToInt16(1000));
                    nVezes++;
                    Retorna_senha();
                }
                else
                {
                    string cMsg_neg = "Ocorreram problemas nesse processo, tente novamente mais tarde: " + "timeout";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Restrita.aspx?op=assocextra&menu=sim';", true);
                }
            }

            //mvRestrita.ActiveViewIndex = 14;
        }

        /* - - - Processo de Venda - - - */

        protected string Colocazero(string Campo, int Carac)
        {
            if (Campo.Length < Carac)
            {
                do
                {
                    Campo = "0" + Campo;

                } while (Campo.Length < Carac);
            }

            return Campo;
        }

        public string CortaZeros(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if ("" + cValor.Substring(i, 1) == "0")
                {
                    contador++;
                }
                else
                    break;
            }

            return cValor.Substring(contador, cValor.Length - contador);
        }

        public string ColocaVirgula(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if (contador == (cValor.Length - 2))
                {
                    cValor = cValor.Substring(0, contador) + "," + cValor.Substring(contador, 2);
                }

                contador++;
            }

            return cValor;
        }

        public void realizaVenda(object sender, EventArgs e)
        {

            string IdConv = Session["CodAcesso"].ToString();
            if (String.IsNullOrEmpty(iNumCartao.Value) || String.IsNullOrEmpty(iValorVenda.Value) || String.IsNullOrEmpty(iSenhaVenda.Value))
            {
                lblMsgVenda.Text = "Preencha todos os campos";
            }
            else
            {
                //caminho físico
                string pathDocumento = "" + WebConfigurationManager.AppSettings["CaminhoVendaENV"];

                DirectoryInfo dir = new DirectoryInfo(pathDocumento);

                //Cria arquivo         
                //string cVersao = "00001.03";
                string cVersao = "00001.04";
                //string cConvenio = "" + 63193;
                string cConvenio = "" + IdConv;
                //string cConvenio = "" + idCobv + "00"; //Não funciona com DV 00
                string cCartao = "" + iNumCartao.Value;
                string cValor = "" + iValorVenda.Value.Replace(",", "").Replace(".", "");
                string cParcelas = "" + stParcelas.Value;
                string cCod_retorno = "000";
                string cCod_autoriza = "000000000";
                string cChave_usuario = "000000000";
                string cNome_usuario = "                                        ";
                string cObserva = "VENDA PELO SITE               ";
                string cCpf = "              ";
                string cSaldo = "00000000000000";
                string cSenha = "" + iSenhaVenda.Value.Replace("'", "");


                cConvenio = Colocazero(cConvenio, 9);
                cValor = Colocazero(cValor, 12);

                string cArq_cont = cVersao + cConvenio + cCartao + cValor + cParcelas + cCod_retorno + cCod_autoriza + cChave_usuario + cNome_usuario + cObserva + cCpf + cSaldo + cSenha;

                //titulo do arquivo
                //string cArq_tit = "" + Session["conveniado"] + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
                //Cr=iando o Arquivo  
                string cArq_tit = "" + IdConv + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
                                                                                                            //try
                                                                                                            //{
                GravaArquivo(System.IO.Path.Combine(pathDocumento, cArq_tit), cArq_cont);
                //}
                //catch
                //{
                //}

                //Session["vendaArq_envio"] = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                VendaArq_envio = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                //MessageBox.Show("Venda enviada para Processamento, aguarde");
                //MessageBox.Show(VendaArq_envio);

                iNumCartao.Value = String.Empty;
                iValorVenda.Value = String.Empty;
                iNumCartao.Focus();

                //MessageBox.Show(cArq_cont);
                //MessageBox.Show(idCobv);

                Processar_retorno();
            }

        }

        public string GravaArquivo(string cPathArquivo, string texto)
        {

            string cAux = "";
            StreamWriter oFileStream = new System.IO.StreamWriter(cPathArquivo);
            try
            {
                oFileStream.Write(texto);
            }
            catch (Exception except)
            {
                return "Erro na gravação do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oFileStream.Close();
                //oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public string LeArquivo(string cPathArquivo)
        {
            string cAux = "";
            FileStream oFileStream = new FileStream(cPathArquivo, FileMode.Open);
            StreamReader oStreamReader = new StreamReader(oFileStream);
            try
            {
                cAux = oStreamReader.ReadToEnd();
            }
            catch (Exception except)
            {
                return "Erro na leitura do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public void Processar_retorno()
        {
            try
            {
                string cArquivo = LeArquivo("" + VendaArq_envio.ToString().Replace("ENV", "RET"));
                cArquivo_mostra = "";

                string cCod_transa = "" + cArquivo.Substring(1, 3);//cod. Transação
                string cVersao = "" + cArquivo.Substring(3, 5); //Versão do Protocolo de Venda
                string cCodConv = "" + cArquivo.Substring(8, 9); //cod. do Convênio
                string cCodCarta = "" + cArquivo.Substring(17, 9); // Cod. do Cartão
                string cValor = "" + cArquivo.Substring(26, 12); //Valor da Venda
                string cParcela = "" + cArquivo.Substring(38, 2); //Quantidade de Parcelas
                string cCodRet = "" + cArquivo.Substring(40, 3);//cod. Retorno
                string cAutoriza = "" + cArquivo.Substring(43, 9); // Autorização da Compra
                string cChaveUsuar = "" + cArquivo.Substring(52, 9); // Chave do Usuário
                string cNome = "" + cArquivo.Substring(61, 40); //Nome do Conveniado
                string cObserva = "" + cArquivo.Substring(101, 30); //Observação sobre a Vendfa
                string cCpfCnpj = "" + cArquivo.Substring(131, 14); //CPF ou CNPJ
                string cSaldo = "" + cArquivo.Substring(145, 14); //Saldo do Associado
                string cSenha = "" + cArquivo.Substring(160, 20); //Senha 
                string cRet_msg = "";

                switch (cCodRet)
                {
                    case "000":
                        cRet_msg = "Transação realizada com sucesso";
                        break;
                    case "110":
                        cRet_msg = "Tempo esgotado para retorno";
                        break;
                    case "120":
                        cRet_msg = "Código de convênio incompatível com a instalação";
                        break;
                    case "130":
                        cRet_msg = "Conexão com problemas";
                        break;
                    case "210":
                        cRet_msg = "Código do cartão inconsistente";
                        break;
                    case "220":
                        cRet_msg = "Código da transação não permitido";
                        break;
                    case "310":
                        cRet_msg = "Cartão ou senha incorreta";
                        break;
                    case "320":
                        cRet_msg = "Cartão vencido";
                        break;
                    case "330":
                        cRet_msg = "Cartão não liberado";
                        break;
                    case "340":
                        cRet_msg = "Cartão cancelado";
                        break;
                    case "350":
                        cRet_msg = "Cartão não autorizado para uso no convênio";
                        break;
                    case "360":
                        cRet_msg = "Tipo de cartão não aceita parcelamento";
                        break;
                    case "370":
                        cRet_msg = "Usuário do cartão inválido";
                        break;
                    case "410":
                        cRet_msg = "Convênio não confere com o registrado na configuração do sistema";
                        break;
                    case "420":
                        cRet_msg = "Convênio não registrado";
                        break;
                    case "430":
                        cRet_msg = "Convênio não liberado";
                        break;
                    case "440":
                        cRet_msg = "Convênio cancelado";
                        break;
                    case "510":
                        cRet_msg = "Crédito insuficiente";
                        break;
                    case "610":
                        cRet_msg = "Parcelamento superior ao permitido para o convênio";
                        break;
                    case "620":
                        cRet_msg = "Parcelamento superior ao permitido para o associado";
                        break;
                    case "630":
                        cRet_msg = "Valor da compra zerado";
                        break;
                    case "710":
                        cRet_msg = "Autorização não encontrada para cancelamento";
                        break;
                    case "720":
                        cRet_msg = "Autorização já cancelada";
                        break;
                    case "730":
                        cRet_msg = "Cancelamento não permitido";
                        break;
                    case "910":
                        cRet_msg = "Comunique-se com a central com urgência";
                        break;
                    case "920":
                        cRet_msg = "Transação não concluída";
                        break;
                    case "930":
                        cRet_msg = "Arquivo ENV vazio ou com problemas";
                        break;
                    case "940":
                        cRet_msg = "Transação já realizada, e não pode ser Duplicada. Verifique seu Extrato";
                        break;
                    case "950":
                        cRet_msg = "Protocolo ENV de comunicação inválido";
                        break;
                    default:
                        cRet_msg = "Erro desconhecido";
                        break;
                }
                //string cRetorno = "" + cArquivo.Substring(42, 3); //cod. retorno
                /*
                  cArquivo_mostra += "<td align='right'>Retorno: </td>";
                  cArquivo_mostra += "<td>" + cRet_msg + "</td>";
                 */



                //verificando se a venda ocorreu
                if (cCodRet != "000")
                {
                    //string cMsg_neg = "Venda NÃO realizada: " + cRet_msg;
                    cArquivo_mostra += "Venda NÃO realizada: " + cRet_msg;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Venda.aspx?op=convdados&menu=n';", true);
                    //comunicando.Attributes["class"] = "compVendaOff";
                }
                else
                {
                    cArquivo_mostra += "<section Class='CompVenda'>";
                    //cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>"; Criar: Parametro pra atribuir tamanho da font para impressão
                    cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>";
                    cArquivo_mostra += "<thead style='vertical-align:bottom' >";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via do Convênio</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "</thead>";
                    cArquivo_mostra += "<tbody>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>CPF</td>";
                    cArquivo_mostra += "<td id='cpf2' style='text-align: center;' class='tFontExtrato' >" + cCpfCnpj + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr> ";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan = '2' id='nome2' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + cNome + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
                    cArquivo_mostra += "<tr style='height:auto;' >";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via da ASU</td>";
                    cArquivo_mostra += "</tr>";
                    /**/
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>CPF</td>";
                    cArquivo_mostra += "<td  id='cpf' style ='text-align: center;' class='tFontExtrato' >" + cCpfCnpj + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' id='nome' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + cNome + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    /**/
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via do Associado</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Saldo</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cSaldo) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "</tbody>";
                    cArquivo_mostra += "</table>";
                    cArquivo_mostra += "</section>";

                    //comunicando.Attributes["class"] = "compVendaOff";
                }

                //EFETUAR RETORNO, relatórios, etc
                //lblRetorno.Text = "<span style='color:DarkGreen;font-size:18px;'>VENDA EFETUADA COM SUCESSO! Transa: " + cCod_transa + "</span>";

                compVenda.Attributes["class"] = "compVendaOn";
                secCompVenda.Attributes["class"] = "compVendaOff";

                lblRetorno.Text = cArquivo_mostra;

            }
            catch (Exception erro)
            {
                nVezes = 0;

                if (nVezes <= Convert.ToInt16("" + WebConfigurationManager.AppSettings["Timeout_venda"]))
                {
                    System.Threading.Thread.Sleep(Convert.ToInt16(1000));
                    nVezes++;
                    Processar_retorno();
                }
                else
                {
                    lblRetorno.Text = "Tempo Esgotado, tente novamente!" + erro.Message;
                }
            }

        }

        protected void finalizarVenda(object sender, EventArgs e)
        {
            //MessageBox.Show("Finalizar Venda!!!");

            lblRetorno.Text = String.Empty;

            compVenda.Attributes["class"] = "compVendaOff";
            secCompVenda.Attributes["class"] = "form-guia compVendaOn";

        }

        protected void gerarPdfExtrato(object sender, EventArgs e)
        {
            Apoio ObjPdf = new Apoio();

            //## Extrato ##

            BLL ObjDbASU = new BLL(conectVegas);

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xPdf = "";


            //Criar Variáveis para: Associado, Data início e Fim - Criado 02-04-2021
            //int idAssoc = 2747;
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string IdAssoc = "";

            if (tCampo == 9 || tCampo == 11)
            {
                IdAssoc = Session["IdAssoc"].ToString();
            }
            DateTime agora = DateTime.Now;
            DateTime mesInicio = DateTime.Now.AddMonths(-1);
            DateTime mestFim = DateTime.Now;

            string wMes = ddlMesExtratoAssoc.SelectedValue;
            string dtIni = ddlAnoExtratoAssoc.SelectedItem.Value + "-" + DateTime.Now.Month + "-20";
            string dtFim = ddlAnoExtratoAssoc.SelectedItem.Value + "-" + DateTime.Now.AddMonths(1) + "-19";

            string janeiro = agora.AddYears(-1).ToString("yyyy") + "-" + mesInicio.ToString("MM") + "-20";
            /*
            string dtInicio = agora.Year + "-" + mesInicio.ToString("MM") + "-20";
            string dtFim = agora.Year + "-" + mestFim.ToString("MM") + "-19";
            */
            switch (wMes)
            {
                case "0":
                    dtIni = (Convert.ToInt32(ddlAnoExtratoAssoc.SelectedItem.Text) - 1) + "-12-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-01-19";
                    break;
                case "1":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-01-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-02-19";
                    break;
                case "2":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-02-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-03-19";
                    break;
                case "3":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-03-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-04-19";
                    break;
                case "4":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-04-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-05-19";
                    break;
                case "5":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-05-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-06-19";
                    break;
                case "6":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-06-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-07-19";
                    break;
                case "7":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-07-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-08-19";
                    break;
                case "8":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-08-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-09-19";
                    break;
                case "9":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-09-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-10-19";
                    break;
                case "10":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-10-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-11-19";
                    break;
                case "11":
                    dtIni = ddlAnoExtratoAssoc.SelectedItem.Text + "-11-20";
                    dtFim = ddlAnoExtratoAssoc.SelectedItem.Text + "-12-19";
                    break;
            }

            //lblPeriodoAssoc.Text = dtIni + " à " + dtFim;



            if (tCampo == 9 || tCampo == 11)
            {

                if (mestFim.ToString("MM") == "01")
                {
                    ObjDbASU.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                     " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '" + janeiro + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                    //  " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND vencimento BETWEEN '" + janeiro + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";

                }
                else
                {
                    ObjDbASU.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                     " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '" + dtIni + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                    //" (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND vencimento BETWEEN '" + dtInicio + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                }

                ObjDbASU.Tabela = " comovime AS c ";
                ObjDbASU.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
                if (mestFim.ToString("MM") == "01")
                {
                    ObjDbASU.Condicao = " WHERE a.idassoc ='" + IdAssoc + "' AND c.vencimento BETWEEN '" + janeiro + "'  AND '" + dtFim + "' ORDER BY dia ";
                    //" WHERE (a.cnpj_cpf ='" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND c.vencimento BETWEEN '" + janeiro + "'  AND '" + dtFim + "' ";
                }
                else
                {
                    ObjDbASU.Condicao = " WHERE a.idassoc='" + IdAssoc + "' AND c.vencimento BETWEEN '" + dtIni + "'  AND '" + dtFim + "' ORDER BY dia ";
                    //ObjDbASU.Condicao = " WHERE (a.cnpj_cpf ='" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND c.vencimento BETWEEN '" + dtInicio + "'  AND '" + dtFim + "' ";
                }

                //MessageBox.Show("Do Extrato: " + iDAcesso.Substring(0,(tCampo- 2)));

                DataTable dados = ObjDbASU.RetCampos();
                int contador = dados.Rows.Count;


#pragma warning disable CS0219 // A variável "gastos" é atribuída, mas seu valor nunca é usado
                double gastos = 0;
#pragma warning restore CS0219 // A variável "gastos" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "limite" é atribuída, mas seu valor nunca é usado
                double limite = 0;
#pragma warning restore CS0219 // A variável "limite" é atribuída, mas seu valor nunca é usado
#pragma warning disable CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado
                double saldo = 0;
#pragma warning restore CS0219 // A variável "saldo" é atribuída, mas seu valor nunca é usado


                //# # # # # # # # # # # Extrato para PDF # # # # # # # # # # # # #
                xPdf += "Autorização";//Campo
                xPdf += "Momento";
                xPdf += "Convênio";
                xPdf += "Cartão";
                xPdf += "Comprador";
                xPdf += "Parcela(s)";
                xPdf += "Valor" + "\n";
                for (int i = 0; i < contador; i++)
                {
                    double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                    xPdf += dados.Rows[i]["idmovime"];//Autorização
                    xPdf += dados.Rows[i]["cnscadmom"];//Momento
                    xPdf += dados.Rows[i]["conveniado"];//Convênio
                    xPdf += dados.Rows[i]["cartao"] + "xx ";//Cartão
                    xPdf += dados.Rows[i]["comprador"];//Comprador
                    xPdf += dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"];//Parcela
                    xPdf += " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture) + "\n";//Valor

                }

                double totalExtrato = double.Parse(dados.Rows[0]["gastos"].ToString()) * (-1);
                /*
                //# # Gera PDF # #
                string PdfGerado = extratoAssoc;

                Document pdf = new Document(PageSize.A4);
                pdf.SetMargins(40, 40, 40, 80);
                pdf.AddCreationDate();
                //string caminhoPdf = ConfigurationManager.AppSettings["caminhoArquivoPdf"] + "Arquivo_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
                //string caminho = @"K:\Projetos\Web\Site\Site\Downloads\" + "Extrato_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + "Extrato_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
                PdfWriter excreverPDF = PdfWriter.GetInstance(pdf, new FileStream(caminho, FileMode.Create));

                pdf.Open();

                string simg = @"K:\Projetos\Web\Site\Site\Img\Logo.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(simg);
                img.ScaleAbsolute(50, 20);

                //img.SetAbsolutePosition(100, 20);

                //Cabeçalho
                PdfPTable tableHeader = new PdfPTable(2);
                tableHeader.AddCell(img);
                tableHeader.AddCell("Extrato Mensal");

                //Rodapé
                PdfPTable tableFooter = new PdfPTable(2);
                tableFooter.AddCell("Total");
                tableFooter.AddCell("R$ " + totalExtrato);
                float[] ctable = {1,2,3,2,3,1,1 };
                
                //Corpo
                PdfPTable table = new PdfPTable(ctable);
                Font fonte = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7);

                //# # Formatação das Células # #
                PdfPCell cell = new PdfPCell();                

                //# # Criar Células da Tabela # # 
                
                table.AddCell("Autorização");
                table.AddCell("Momento");
                table.AddCell("Convênio");
                table.AddCell("Cartão");
                table.AddCell("Comprador");
                table.AddCell("Parcelas");
                table.AddCell("Valor");
                
                for (int i = 0; i < contador; i++)
                {
                    double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);

                    var cel1 = dados.Rows[i]["idmovime"].ToString();
                    var cel2 = dados.Rows[i]["cnscadmom"].ToString();
                    var cel3 = dados.Rows[i]["conveniado"].ToString();
                    var cel4 = dados.Rows[i]["cartao"].ToString() + "xx";
                    var cel5 = dados.Rows[i]["comprador"].ToString();
                    var cel6 = dados.Rows[i]["parcela"].ToString() + "/" + dados.Rows[i]["parctot"].ToString();
                    var cel7 = " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture).ToString();

                    table.AddCell(cel1);
                    table.AddCell(cel2);
                    table.AddCell(cel3);
                    table.AddCell(cel4);
                    table.AddCell(cel5);
                    table.AddCell(cel6);
                    table.AddCell(cel7);
                }

                string dadosPdf = "";

                Paragraph paragrafo = new Paragraph(xPdf, new Font(Font.NORMAL, 6));
                paragrafo.Alignment = Element.ALIGN_CENTER;
                paragrafo.Add(xPdf);

                pdf.Add(img);
                pdf.Add(tableHeader);
                pdf.Add(table);
                pdf.Add(tableFooter);


                

                pdf.Close();
                */

                //string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + "Extrato_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";

                /*Exibir Arquivos no Diretório*/
                //DirectoryInfo diretorio = new DirectoryInfo(@"K:\Projetos\Web\Site\Site\Downloads");
                DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\");
                //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
                FileInfo[] Arquivos = diretorio.GetFiles("*.*");

                string arquivos = "";
                string xArq = "";
                //Listar os arquivos
                foreach (FileInfo fileinfo in Arquivos)
                {
                    arquivos = fileinfo.Name;

                    xArq += "<div>";
                    xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>" + arquivos;
                    xArq += "</a><br>";
                    xArq += "</div>";

                    lblArquivos.Text = xArq;
                }




            }

            extratoAssoc = xPdf;

            //## Fim Extrato ##




            //MessageBox.Show(ObjPdf.gerarPdf());

        }

        /* - - - Fim Processo de Venda - - - */

        /*
        protected void gerarPdfExtratoAssoc(object sender, EventArgs e)
        {
            BLL ObjDados = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            string xRet = "";
#pragma warning disable CS0219 // A variável "xPdf" é atribuída, mas seu valor nunca é usado
            string xPdf = "";
#pragma warning restore CS0219 // A variável "xPdf" é atribuída, mas seu valor nunca é usado

            //Criar Variáveis para: Associado, Data início e Fim - Criado 02-04-2021
            //int idAssoc = 2747;
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string IdAssoc = "";

            if (tCampo == 9 || tCampo == 11)
            {
                IdAssoc = Session["IdAssoc"].ToString();
            }

            //Mê e Ano                       
            Apoio.Ano = ddlAnoExtratoAssoc.SelectedValue;
            Apoio.Mes = ddlMesExtratoAssoc.SelectedValue;


            if (tCampo == 9 || tCampo == 11)
            {

                ObjDados.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                      " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL LIMIT 1) AS gastos ";
                ObjDados.Tabela = " comovime AS c ";
                ObjDados.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
                ObjDados.Condicao = " WHERE a.idassoc ='" + IdAssoc + "' AND c.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL ORDER BY dia ";

                //## Extrato ##
            }
            DataTable dados = ObjDados.RetCampos();

            double gastos = 0;
            double limite = 0;
            double saldo = 0;

            gastos = Convert.ToDouble(dados.Rows[0]["gastos"].ToString());
            limite = Convert.ToDouble(dados.Rows[0]["credito"].ToString());
            saldo = (gastos + limite); //Criar Query para "trazer" os registros do mês corrente * * * * * * * * *  AQUI * * * * * *  * 21-10-2021 15:37

            double totalExtrato = double.Parse(dados.Rows[0]["gastos"].ToString()) * (-1);

            xRet += "<p>" + dados.Rows[0]["titular"].ToString() + "";

            lblMsg.Text = xRet;

            //string dest = @"K:\Projetos\Web\Site\Site\Downloads\" + IdAssoc + "-"+ DateTime.Now.Day + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
            //string dest = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + IdAssoc + ".pdf";
            //string extratoPdf = IdAssoc +"-"+ DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
            string extratoPdf = IdAssoc + ".pdf";
            string dest = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + extratoPdf;

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            float[] colunas = { 1, 4, 7, 2, 7, 1, 2 }; //Esse cara 
            float[] cHeader = { 2, 8, 1, 1, 1, 1, 1 };

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Table table = new Table(UnitValue.CreatePercentArray(colunas)).UseAllAvailableWidth(); //Com esse cara. Vão determinar a largura da coluna

            string simg = AppDomain.CurrentDomain.BaseDirectory + @"\Img\Logo.png";

            //**

            //Cabeçalho
            Table tableHeader = new Table(2);
            tableHeader.AddCell("Logo");
            tableHeader.AddCell("Extrato Mensal");
            string textcabecaclho = "";

            textcabecaclho += "Olá, " + dados.Rows[0]["titular"].ToString() + "\n";
            textcabecaclho += "Seu limite mensal é: R$ " + limite.ToString("F2", CultureInfo.InvariantCulture) + "\n";
            textcabecaclho += "Seu saldo é: R$ " + saldo.ToString("F2", CultureInfo.InvariantCulture) + "\n";
            textcabecaclho += "Seu extrato é referente à: " + ddlMesExtratoAssoc.SelectedItem + "/" + ddlAnoExtratoAssoc.SelectedItem;

            Paragraph cabecalho = new Paragraph(textcabecaclho);


            // Rodapé
            Table tableFooter = new Table(2);
            tableFooter.AddCell("Total");
            tableFooter.AddCell("R$ " + totalExtrato);


            //# # Criar Células da Tabela # #
            table.AddCell("Autorização");
            table.AddCell("Momento");
            table.AddCell("Convênio");
            table.AddCell("Cartão");
            table.AddCell("Comprador");
            table.AddCell("Parc.");
            table.AddCell("Valor");

            if (dados.Rows.Count > 0)
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);

                    var cel1 = dados.Rows[i]["idmovime"].ToString();
                    var cel2 = dados.Rows[i]["cnscadmom"].ToString();
                    var cel3 = dados.Rows[i]["conveniado"].ToString();
                    var cel4 = dados.Rows[i]["cartao"].ToString() + "xx";
                    var cel5 = dados.Rows[i]["comprador"].ToString();
                    var cel6 = dados.Rows[i]["parcela"].ToString() + "/" + dados.Rows[i]["parctot"].ToString();
                    var cel7 = " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture).ToString();


                    table.AddCell(cel1).SetFontSize(7);
                    table.AddCell(cel2);
                    table.AddCell(cel3);
                    table.AddCell(cel4);
                    table.AddCell(cel5);
                    table.AddCell(cel6);
                    table.AddCell(cel7);
                }
            }
            else
            {
                lblMsg.Text = ObjDados.MsgErro;
            }
            var cel8 = "";
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell(cel8);
            table.AddCell("Total: ");
            table.AddCell("R$ " + totalExtrato.ToString("F2", CultureInfo.InvariantCulture));

            doc.Add(cabecalho);
            doc.Add(table);
            //doc.Add(tableFooter);

            doc.Close();

            //Exibir Arquivos no Diretório
            //DirectoryInfo diretorio = new DirectoryInfo(@"K:\Projetos\Web\Site\Site\Downloads");
            DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            //FileInfo[] Arquivos = diretorio.GetFiles("*.*");            
            //FileInfo[] Arquivos = diretorio.GetFiles(extratoPdf);
            FileInfo[] Arquivos = diretorio.GetFiles(IdAssoc + ".pdf");

            string arquivos = "";
            string xArq = "";

            //Listar os arquivos
            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;

                xArq += "<div style='margin-bottom: 50px; '>";
                //xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>" + arquivos;
                xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>";
                xArq += "<img style='width: 50%;' src ='" + "Img/icon/icon-pdf.png" + "' />";
                xArq += "<p> Seu Extrato </p>";
                xArq += "</a><br>";
                xArq += "</div>";
            }
            lblExtratoPdf.Text = xArq;


        }//gerarPdfExtratoAssoc
        */

        public void executarTest(object sender, EventArgs e)
        {
            //MessageBox.Show("Você Clicou Pelo C#");
        }
        /**/

        public String listarArquivosConvenios()
        {
            /*Exibir Arquivos no Diretório*/
            DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\Convenios\");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            FileInfo[] Arquivos = diretorio.GetFiles("*.*");

            string arquivos = "";
            string xArq = "";

            xArq += "<div style='margin-top: 30px; width: 600px; height: auto; '>";
            //xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(242,105,7,0), rgba(242,105,7,95));'>" + "Arquivos" + "</p>";
            xArq += "<p style='height: 30px; color: white; text-align: left; background-image: linear-gradient(to left, rgba(34,57,111,0), rgba(34,57,111,44));'>" + "Arquivos" + "</p>";
            xArq += "<div style='padding: 10px'>";
            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;
                xArq += "<p style='text-align: left; margin: 5px 0 0 10px; padding: 0 0 0 0; '>";
                xArq += "<div style='width: 20px; height: 20px; float: left'>";
                xArq += "<img style='width: 18px;' src='../Img/Icon/ArquivosDownloadsCD.png' />";
                xArq += "</div>";
                xArq += "<div style='width: 300px; height: 20px; float: left;'>";
                xArq += "<p style='text-align: left; margin: 0; margin-left: 10px; padding: 0;'> <a style='' href='" + @"Downloads\Convenios\" + arquivos + "' target=_blanck>" + arquivos + "</a></p>";
                xArq += "</div>";
                xArq += "</p><br>";


                //lblArquivos.Text = xArq;
            }
            xArq += "</div>";
            xArq += "</div>";

            return xArq;
        }
        
        /* - - - Fim Processo de Venda - - - */

        //Metodo para exibir botão de acesso à Página Eventos. Criado: 08/08/2023
        public String identificarFuncionarios(string idFunc)
        {
            BLL ObjDados = new BLL(conectVegas);

            string query = " SELECT idassoc, titular FROM associa" +
                           " WHERE categoria = '4' AND idassoc = '" + idFunc + "'";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            string xRet = "";

            if (dados.Rows.Count > 0)
            {
                return btnPainelEventos.Attributes["class"] = "ativa_termo";
            }

            return null;
        }

        public void irParaEventos(object sender, EventArgs e)
        {
            //Redireciona para a Página de Eventos. Criado: 08/08/2023
            Response.Redirect("../Eventos/eLogin.aspx");
        }         
        
        public void irParaPesquisa(object sender, EventArgs e)
        {
            Response.Redirect("Pesquisa.aspx");
        }



    }//Fim
}