using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.IO;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Site
{
    public partial class VoceOnLine1 : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

        public string VendaArq_envio { get; set; }
        public int nVezes { get; set; }
        public string cArquivo_mostra { get; set; }

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
            }
            this.DataBind();
        }

        /* - - -  Operações e Validações - - - */
        public void usuarioLogado()
        {
            string xRet = "";
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
                usuario = Session["LoginUsuario"].ToString();
            }
            string primeiroNome = usuario.Split(' ').FirstOrDefault();
            string primeiraLetra = usuario.Split(' ').FirstOrDefault();
            int tNome = primeiroNome.Length;

            //xRet += "" + primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome-1)).ToLower();
            lblUsuLogado.Text = primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome - 1)).ToLower();

            //return xRet;
        }

        public void checarSessao()
        {
            string identifica = "";

            if (Session["Identifica"] != null)
            {
                identifica = Session["Identifica"].ToString();
            }
            else
            {
                Response.Redirect("LoginVoceOnLine.aspx");

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
            else { MessageBox.Show("Alguma coisa deu errada..."); }
        }

        public void ativarExtratoOuVenda()
        {
            string identifica = "";

            if (Session["Identifica"] != null)
            {
                identifica = Session["Identifica"].ToString();
            }
            else
            {
                Response.Redirect("LoginVoceOnLine.aspx");
            }

            int checaQuemLogou = identifica.Length;

            if (checaQuemLogou == 11)
            {
                mwContAssoc.ActiveViewIndex = 1;
            }
            else if (checaQuemLogou == 14)
            {
                mwContConv.ActiveViewIndex = 0;
            }
        }

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
        }
        public void ativarConvFatura(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 3;
        }
        public void ativarConvExtrato(object sender, EventArgs e)
        {
            mwContConv.ActiveViewIndex = 4;
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


        public void buscarCep(object sender, EventArgs e)
        {
            using (var ws = new WSCorreios.AtendeClienteClient())
            {
                try
                {
                    var dCorreios = ws.consultaCEP(iCepAssoc.Value);
                    var dCorreiosC = ws.consultaCEP(iCepConv.Value);
                    if (string.IsNullOrEmpty(iCepAssoc.Value) || string.IsNullOrEmpty(iCepConv.Value))
                    {
                        //MessageBox.Show("Campo Cep Precisa ser Preenchido");
                        lblErroAssoc.Text = "Campo Cep Precisa ser Preenchido";
                        lblErroConv.Text = "Campo Cep Precisa ser Preenchido";
                    }
                    else
                    {
                        if (dCorreios is null || dCorreiosC is null)
                        {
                            MessageBox.Show("CEP Não encontrado, Preencha Manualmente");
                            lblErroAssoc.Text = "CEP Não encontrado, Preencha Manualmente";
                            lblErroConv.Text = "CEP Não encontrado, Preencha Manualmente";
                            return;
                        }
                        else
                        {
                            //Associado
                            iRuaAssoc.Value = dCorreios.end;
                            iComplemAssoc.Value = dCorreios.complemento2;
                            iBairroAssoc.Value = dCorreios.bairro;
                            iCidadeAssoc.Value = dCorreios.cidade;
                            iEstadoAssoc.Value = dCorreios.uf;
                            //Convênio
                            iRuaConv.Value = dCorreiosC.end;
                            iComplementConv.Value = dCorreiosC.complemento2;
                            iBairroConv.Value = dCorreiosC.bairro;
                            iCidadeConv.Value = dCorreiosC.cidade;
                            iEstadoConv.Value = dCorreiosC.uf;

                            iNumCasaAssoc.Value = String.Empty;
                            iNumeroConv.Value = String.Empty;
                            iNumCasaAssoc.Focus();
                            iNumeroConv.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Consultar o CEP!!! ", ex.ToString());
                    //lblErroAssoc.Text = "Erro ao Consultar o CEP!!! " + ex.ToString();
                    //lblErroConv.Text = "Erro ao Consultar o CEP!!! " + ex.ToString();
                }
            }

        }


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

                string xRet = "";

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

            if (Convert.ToInt32(diaAtual) < 20)
            {
                mesAtual = (Convert.ToInt32(mesAtual) - 1).ToString();
            }

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
        }

        public String extratoAssociado()
        {
            BLL ObjDbASU = new BLL(conectVegas);
            BLL ObjCredito = new BLL(conectVegas);
            BLL ObjGastos = new BLL(conectVegas);

            Apoio Apoio = new Apoio();

            string xRet = "";
            string xPdf = "";

            Apoio.Ano = ddlAnoExtratoAssoc.SelectedValue;
            Apoio.Mes = ddlMesExtratoAssoc.SelectedValue;

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
            if (ObjDbASU.MsgErro == "")
            {
                if (tCampo == 9 || tCampo == 11)
                {
                    ObjDbASU.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                     " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL LIMIT 1) AS gastos ";
                    ObjDbASU.Tabela = " comovime AS c ";
                    ObjDbASU.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                    " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                    " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
                    ObjDbASU.Condicao = " WHERE a.idassoc ='" + IdAssoc + "' AND c.vencimento BETWEEN " + Apoio.dtDataInicio() + " AND " + Apoio.dtDataFim() + " AND c.cnscanmom IS NULL ORDER BY dia ";


                    //MessageBox.Show("Do Extrato: " + iDAcesso.Substring(0,(tCampo- 2)));

                    DataTable dados = ObjDbASU.RetCampos();
                    int contador = dados.Rows.Count;

                    double gastosSaldo = 0;
                    double gastos = 0;
                    double limite = 0;
                    double saldo = 0;

                    Apoio.IdAssoc = IdAssoc; //Atribui Id do associado ao Método para calcular os gastos

                    string mesAtual = DateTime.Now.Month.ToString();
                    string diaAtual = DateTime.Now.Day.ToString();

                    //# # # Mostra Limite e Saldo # # #
                    xRet += "<div class='extAssocNormal'>";
                    xRet += "<table class='background-color: violet;'>";
                    xRet += "<caption style='font-size: 1em; text-align: left;'>" + "Seja bem Vindo: " + Session["LoginUsuario"].ToString() + "</caption>";
                    xRet += "<tbody>";
                    xRet += "<tr>";
                    xRet += "<td>" + "Seu Limite: " + "</td>";
                    xRet += "<td style='font-size: 30px;'>" + "R$ " + Apoio.limiteAssociado().ToString("F2", CultureInfo.InvariantCulture) + "</td>";
                    //xRet += "</tr>";
                    //xRet += "<tr>";
                    xRet += "<td>" + "Seu Saldo: " + "</td>";
                    xRet += "<td style='font-size: 40px;'>" + "R$ " + Apoio.SaldoAssociado().ToString("F2", CultureInfo.InvariantCulture) + "</td>";
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</div>";

                    //# # # # # Fim - saldo e limite # # # # #

                    if (contador > 0)
                    {
                        xRet += "<div class='extAssocNormal' style='width: 1000px; '>";
                        //Cria Extrato
                        xRet += "<table>";
                        xRet += "<caption>" + " Extrato " + "</caption>";
                        //xRet += "<caption>" + " Extrato " + " - Período: " + Apoio.Periodo() + "</caption>";
                        xRet += "<thead>";//Cabeçalho da Tabela
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
                        xRet += "<td colspan='5' class='right'>" + "Total: " + "</td>";
                        xRet += "<td colspan='2'>" + " R$: " + Apoio.GastosAssociado().ToString("F2", CultureInfo.InvariantCulture) + " " + "</td>";
                        xRet += "</tr>";
                        xRet += "</tfoot>";
                        xRet += "<tbody>";//Cabeçalho da Tabela
                        for (int i = 0; i < contador; i++)
                        {
                            double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                            xRet += "<tr>";
                            xRet += "<td>" + dados.Rows[i]["idmovime"] + "</td>";//Autorização
                            xRet += "<td>" + dados.Rows[i]["cnscadmom"] + "</td>";//Momento
                            xRet += "<td>" + dados.Rows[i]["conveniado"] + "</td>";//Convênio
                            xRet += "<td>" + dados.Rows[i]["cartao"] + "xx " + "</td>";//Cartão
                            xRet += "<td>" + dados.Rows[i]["comprador"] + "</td>";//Comprador
                            xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                            xRet += "<td>" + " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture) + "</td>";//Valor
                            xRet += "</tr>";
                        }
                        xRet += "</tbody>";
                        xRet += "</table>";

                        //xRet += "teste";
                        xRet += "</div>";
                    }
                    else
                    {
                        xRet += "<div>";
                        xRet += "<p>" + " Seu cartão não gerou débitos para este período " + "</p>";
                        xRet += "</div>";
                    }

                    //# # # # # Extrato Fluído # # # # #

                    xRet += "<div class='extAssocFluido'>";
                    xRet += "<table>";
                    xRet += "<caption style='font-size: 2em; text-align: left;'>" + "Seja bem Vindo: " + Session["LoginUsuario"].ToString() + "</caption>";
                    xRet += "<tbody>";
                    xRet += "<tr>";
                    xRet += "<td>" + "Seu Limite: " + "</td>";
                    xRet += "<td style='font-size: 30px;'>" + "R$ " + Apoio.limiteAssociado().ToString("F2", CultureInfo.InvariantCulture) + "</td>";
                    //xRet += "</tr>";
                    //xRet += "<tr>";
                    xRet += "<td>" + "Seu Saldo: " + "</td>";
                    xRet += "<td style='font-size: 40px;'>" + "R$ " + Apoio.SaldoAssociado().ToString("F2", CultureInfo.InvariantCulture) + "</td>";
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
                    //Cria Extrato
                    xRet += "<table>";
                    xRet += "<caption>" + " Extrato " + "</caption>";
                    xRet += "<thead>";//Cabeçalho da Tabela
                    xRet += "<tr>"; //Linha
                    xRet += "<td>" + "dia" + "</td>";
                    xRet += "<td colspan='4'>" + "Suas Compras" + "</td>";
                    xRet += "</tr>";
                    xRet += "</thead>";
                    xRet += "<tfoot>";//Rodape da Tabela
                    xRet += "<tr>"; //Linha
                    xRet += "<td colspan='3' class='right'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2'>" + " R$: " + Apoio.GastosAssociado().ToString("F2", CultureInfo.InvariantCulture) + " " + "</td>";
                    xRet += "</tr>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>";//Cabeçalho da Tabela
                    for (int i = 0; i < contador; i++)
                    {
                        double valorCompra = double.Parse(dados.Rows[i]["valor"].ToString()) * (-1);
                        xRet += "<tr>";
                        xRet += "<td>" + dados.Rows[i]["dia"] + "</td>";//Dia
                        xRet += "<td colspan='2' style='text-align: left;'>" + dados.Rows[i]["conveniado"] + "<br>" + "<span style='font-size: .7em; margin-left: 20px;'>" + dados.Rows[i]["comprador"] + "</span>" + "</td>";//Convênio                    
                        xRet += "<td>" + dados.Rows[i]["parcela"] + "/" + dados.Rows[i]["parctot"] + "</td>";//Parcela
                        xRet += "<td>" + " R$" + valorCompra.ToString("F2", CultureInfo.InvariantCulture) + "</td>";//Valor
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</div>";
                }
            }
            else
            {
                lblMsg.Text = "Erro Original: " + ObjDbASU.MsgErro;
            }
            extratoAssoc = xPdf;

            return xRet;
        }

        public String metodoCartoesAssoc()
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";

            string campos = " a.idassoc, d.iddepen, g.descricao AS grau, d.associado, car.idcartao, car.dependen, car.emissao, car.dt_inicio, car.dt_fim, car.validade, car.credito, a.titular, d.nome AS dependente ";
            string tabela = " asdepen AS d ";
            string left = " LEFT JOIN base_asdepgra AS g ON g.codgradp = d.grau " +
                                    " INNER JOIN associa AS a ON a.idassoc = d.associado " +
                                    " INNER JOIN asdepcar AS car ON car.dependen = iddepen ";
            string condicao = " WHERE a.cnscanmom IS NULL " +
                                        " AND d.cnscanmom IS NULL " +
                                        " AND car.cnscanmom IS NULL " +
                                        " AND a.idassoc = '2747' " +
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

                //xRet = dados.ToString();
                /*
                xRet += "<section style='width: 500px; min-height: 200px; margin-top: 30px; text-align: left;'>";
                for (int i = 0; i < nLinhas; i++)
                {
                    //xRet += "<p>" + dados.Rows[i]["idassoc"] + "</p>";
                    xRet += "<div style='padding-top: 0px; width: 100%; height: 40px'>";
                    xRet += "<div style='float: left; height: 40px'>";
                    xRet += "<img src='Img/icon/card.png'  style=''/>";
                    xRet += "</div>";
                    xRet += "<div style='float: left; height: 40px'>";
                    xRet += "<p style='margin: 0; margin-left: 5px; padding-top: 10px; '>" + dados.Rows[i]["idcartao"] + "xx - " + dados.Rows[i]["dependente"] + "</p>";
                    xRet += "</div>";
                    //xRet += "<img src='Img/icon/card.png'  style='background-color: red'/>";
                    xRet += "</div>"+"<br><br>";                    
                }
                xRet += "</section>";

                */

                for (int i = 0; i < nLinhas; i++)
                {
                    string validade = dados.Rows[0]["validade"].ToString();
                    DateTime.Parse(validade).ToString("dd-MM-yyyy");
                    xRet += "<div style='margin-top: 50px; width: 302px; height: 195px; float: left'>";

                    xRet += "<div style='background-color: yellow; width: 302px; height: 195px;'>";
                    xRet += "<img src='img/CartaoASUOnLine .jpg' style='width: 300px' />";
                    xRet += "</div>";
                    xRet += "<div style='margin-top: -195px; width: 302px; height: 195px; z-index: 1; position: absolute;'>";
                    xRet += "<p style='margin: 0; margin-left: 28px; text-align: left; padding-top: 100px; '>" + dados.Rows[i]["dependente"] + "</p>";
                    xRet += "<p style='margin: 0; margin-left: 28px; text-align: left; '>" + dados.Rows[i]["idcartao"] + "xx" + "</p>";
                    xRet += "<p style='margin: 0; margin-left: 98px; text-align: left; '>" + " Validade: " + DateTime.Parse(validade).ToString("dd-MM-yyyy") + "</p>";
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

        public String montarRelEntrega()
        {
            BLL ObjDbExtPos = new BLL(conectVegas);
            BLL ObjDBRelEnt = new BLL(conectVegas);
            BLL ObjDbExtPre = new BLL(conectVegas);

            //Variáveis
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string dtIni = ddlAno.SelectedItem.Value + "-" + DateTime.Now.Month + "-20";
            string dtFim = ddlAno.SelectedItem.Value + "-" + DateTime.Now.AddMonths(1) + "-19";
            string agora = "" + DateTime.Now.Month;
            agora = ddlMes.SelectedValue;
            string wMes = ddlMes.SelectedValue;
            string gastosPre = "";
            string xRet = "";

            //Define período
            switch (wMes)
            {
                case "0":
                    dtIni = (Convert.ToInt32(ddlAno.SelectedItem.Text) - 1) + "-12-20";
                    dtFim = ddlAno.SelectedItem.Text + "-01-19";
                    break;
                case "1":
                    dtIni = ddlAno.SelectedItem.Text + "-01-20";
                    dtFim = ddlAno.SelectedItem.Text + "-02-19";
                    break;
                case "2":
                    dtIni = ddlAno.SelectedItem.Text + "-02-20";
                    dtFim = ddlAno.SelectedItem.Text + "-03-19";
                    break;
                case "3":
                    dtIni = ddlAno.SelectedItem.Text + "-03-20";
                    dtFim = ddlAno.SelectedItem.Text + "-04-19";
                    break;
                case "4":
                    dtIni = ddlAno.SelectedItem.Text + "-04-20";
                    dtFim = ddlAno.SelectedItem.Text + "-05-19";
                    break;
                case "5":
                    dtIni = ddlAno.SelectedItem.Text + "-05-20";
                    dtFim = ddlAno.SelectedItem.Text + "-06-19";
                    break;
                case "6":
                    dtIni = ddlAno.SelectedItem.Text + "-06-20";
                    dtFim = ddlAno.SelectedItem.Text + "-07-19";
                    break;
                case "7":
                    dtIni = ddlAno.SelectedItem.Text + "-07-20";
                    dtFim = ddlAno.SelectedItem.Text + "-08-19";
                    break;
                case "8":
                    dtIni = ddlAno.SelectedItem.Text + "-08-20";
                    dtFim = ddlAno.SelectedItem.Text + "-09-19";
                    break;
                case "9":
                    dtIni = ddlAno.SelectedItem.Text + "-09-20";
                    dtFim = ddlAno.SelectedItem.Text + "-10-19";
                    break;
                case "10":
                    dtIni = ddlAno.SelectedItem.Text + "-10-20";
                    dtFim = ddlAno.SelectedItem.Text + "-11-19";
                    break;
                case "11":
                    dtIni = ddlAno.SelectedItem.Text + "-11-20";
                    dtFim = ddlAno.SelectedItem.Text + "-12-19";
                    break;
            }

            if (ObjDbExtPos.MsgErro == "" || ObjDbExtPre.MsgErro == "" || ObjDBRelEnt.MsgErro == "")
            {

                string camposPos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS conveniado, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + dtIni + "'  AND '" + dtFim + "' LIMIT 1) AS gastos ";
                string tabelaPos = " comovime AS m ";
                string leftPos = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                                 " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                                 " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
                string condicaoPos = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + dtIni + "' AND '" + dtFim + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";

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

                string camposRel = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + dtIni + "'  AND '" + dtFim + "' LIMIT 1) AS gastos ";
                string tabelaRel = " comovime AS m ";
                string leftRel = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                                 " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                                 " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
                string condicaoRel = " WHERE (m.convenio ='" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf ='" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + dtIni + "' AND '" + dtFim + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";

                ObjDBRelEnt.Campo = camposRel;
                ObjDBRelEnt.Tabela = tabelaRel;
                ObjDBRelEnt.Left = leftRel;
                ObjDBRelEnt.Condicao = condicaoRel;

                //MessageBox.Show(camposPre + tabelaPre + leftPre + condicaoPre);
                DataTable dadosRelEnt = ObjDBRelEnt.RetCampos();
                int nLinhasRelEnt = dadosRelEnt.Rows.Count;
                string gastosRelEnt = "";

                if (nLinhasRelEnt > 0)
                {
                    gastosRelEnt = dadosRelEnt.Rows[0]["gastos"].ToString();
                }

                //MessageBox.Show("Relatório de Entrega: " + iDAcesso.Substring(0,(tCampo-2)));

                string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + dtIni + "'  AND '" + dtFim + "' LIMIT 1 ) AS gastosPre ";
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
                string condicaoPre = " WHERE mov.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' AND mov.vencimento BETWEEN '" + dtIni + "'  AND '" + dtFim + "' ";

                ObjDbExtPre.Campo = camposPre;
                ObjDbExtPre.Tabela = tabelaPre;
                ObjDbExtPre.Left = leftPre;
                ObjDbExtPre.Condicao = condicaoPre;

                //MessageBox.Show(camposPre + tabelaPre + leftPre + condicaoPre);

                DataTable dadosPre = ObjDbExtPre.RetCampos();

                int nLinhasPre = dadosPre.Rows.Count;


                if (nLinhasPos > 0)
                {
                    /*Relatório de Entrega Pós Pago*/
                    xRet += "<div style='height: 500px; overflow: scroll;'>";
                    xRet += "<div style='height: 25px; width:100%; text-align:left;'>" + "Olá, " + dadosPos.Rows[0]["conveniado"] + ". Seja Bem Vindo!!!" + "</div>";
                    xRet += "<div>";
                    //xRet += "<asp:label ID='lblPeriodo' Visible='true' >" + lblPeriodo.Text + "</asp:label>";
                    xRet += "</div>";
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
                    xRet += "<td colspan='5' class='right'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2'>" + " R$: " + gastos + " " + "</td>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPos; i++)
                    {
                        double valor = double.Parse(dadosPos.Rows[i]["valor"].ToString());
                        int qtdeparc = int.Parse(dadosPos.Rows[i]["parctot"].ToString());
                        xRet += "<tr>";
                        xRet += "<td>" + (i + 1) + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["autorizacao"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["cartao"] + "XX" + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["titular"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["parctot"] + "</td>";
                        xRet += "<td>" + "R$ " + (valor * qtdeparc).ToString("F2", CultureInfo.InvariantCulture) + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";

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
                                totParc2 = totParc2 + valor;
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
                    xRet += "<table>";

                    xRet += "<thead style='font-size: 2em;'>";
                    xRet += "<td colspan='5' class='right' style='text-align: center;'>" + "Parcelas " + "</td>";
                    xRet += "</thead>";

                    xRet += "<tbody>";
                    xRet += "<tr>";
                    if (totParc1 > 0)
                    {
                        xRet += "<td>" + "Parcela 1: R$ " + totParc1 + "</td>";
                    }
                    if (totParc2 > 0)
                    {
                        xRet += "<td>" + "Parcela 2: R$ " + totParc2 + "</td>";
                    }
                    if (totParc3 > 0)
                    {
                        xRet += "<td>" + "Parcela 3: R$ " + totParc3 + "</td>";
                    }
                    if (totParc4 > 0)
                    {
                        xRet += "<td>" + "Parcela 4: R$ " + totParc4 + "</td>";
                    }
                    if (totParc5 > 0)
                    {
                        xRet += "<td>" + "Parcela 5: R$ " + totParc5 + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    if (totParc6 > 0)
                    {
                        xRet += "<td>" + "Parcela 6: R$ " + totParc6 + "</td>";
                    }
                    if (totParc7 > 0)
                    {
                        xRet += "<td>" + "Parcela 7: R$ " + totParc7 + "</td>";
                    }
                    if (totParc8 > 0)
                    {
                        xRet += "<td>" + "Parcela 8: R$ " + totParc8 + "</td>";
                    }
                    if (totParc9 > 0)
                    {
                        xRet += "<td>" + "Parcela 9: R$ " + totParc9 + "</td>";
                    }
                    if (totParc10 > 0)
                    {
                        xRet += "<td>" + "Parcela 10: R$ " + totParc10 + "</td>";
                    }
                    xRet += "</tr>";
                    xRet += "<tr>";
                    xRet += "</tr>";
                    xRet += "</tbody>";
                    xRet += "</table>";
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
                    gastosPre = dadosPre.Rows[0]["gastosPre"].ToString();

                    xRet += "<div style='height: 300px; overflow: scroll'>";
                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Relatório de Entrega - Pré Pago";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td>" + "Sequência" + "</td>";
                    xRet += "<td>" + "Autorização" + "</td>";
                    xRet += "<td>" + "Parcela" + "</td>";
                    //xRet += "<td>" + "Lote" + "</td>";
                    xRet += "<td>" + "Cartão" + "</td>";
                    xRet += "<td>" + "Comprador" + "</td>";
                    xRet += "<td>" + "Valor" + "</td>";
                    xRet += "</tr>";
                    xRet += "</thead>";
                    xRet += "<tfoot>"; //Footer
                    xRet += "<td colspan='5' class='right'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2'>" + " R$ " + gastosPre + " " + "</td>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPre; i++)
                    {
                        xRet += "<tr>";
                        xRet += "<td>" + (i + 1) + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["autorizacao"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["ParcelaDesc"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["numcartao"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["dependente"] + "</td>";
                        xRet += "<td>" + "R$ " + dadosPre.Rows[i]["valor"] + "</td>";
                        xRet += "</tr>";
                        xRet += "<tr>";
                        xRet += "</tr>";
                        xRet += "<tr>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";
                    xRet += "</div>";
                }
                else
                {
                    xRet += "<div>";
                    xRet += "<p>" + "Relatório de Entrega: Cartão Pré Pago" + "</p>";
                    xRet += "<p>" + "Não houve Vendas para o Período Selecionado!" + "</p>";
                    xRet += "</div>";
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
                    xRet += "Erro no ObjDbExtPre: " + ObjDbExtPre.MsgErro;
                }
                else if (ObjDBRelEnt.MsgErro != "")
                {
                    xRet += "Erro no ObjDbRelEnt: " + ObjDBRelEnt.MsgErro;
                }
            }

            return xRet;

        }

        public String montarRelMensal()
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;

            //int idConv = 224;
            string xRet = "";

            string dtIni = ddlAno.SelectedItem.Value + "-" + DateTime.Now.Month + "-20";
            string dtFim = ddlAno.SelectedItem.Value + "-" + DateTime.Now.AddMonths(1) + "-19";
            string agora = "" + DateTime.Now.Month;
            agora = ddlMes.SelectedValue;
            string wMes = ddlMes.SelectedValue;
            string gastosPre = "";

            //Define período
            switch (wMes)
            {
                case "0":
                    dtIni = (Convert.ToInt32(ddlAno.SelectedItem.Text) - 1) + "-12-20";
                    dtFim = ddlAno.SelectedItem.Text + "-01-19";
                    break;
                case "1":
                    dtIni = ddlAno.SelectedItem.Text + "-01-20";
                    dtFim = ddlAno.SelectedItem.Text + "-02-19";
                    break;
                case "2":
                    dtIni = ddlAno.SelectedItem.Text + "-02-20";
                    dtFim = ddlAno.SelectedItem.Text + "-03-19";
                    break;
                case "3":
                    dtIni = ddlAno.SelectedItem.Text + "-03-20";
                    dtFim = ddlAno.SelectedItem.Text + "-04-19";
                    break;
                case "4":
                    dtIni = ddlAno.SelectedItem.Text + "-04-20";
                    dtFim = ddlAno.SelectedItem.Text + "-05-19";
                    break;
                case "5":
                    dtIni = ddlAno.SelectedItem.Text + "-05-20";
                    dtFim = ddlAno.SelectedItem.Text + "-06-19";
                    break;
                case "6":
                    dtIni = ddlAno.SelectedItem.Text + "-06-20";
                    dtFim = ddlAno.SelectedItem.Text + "-07-19";
                    break;
                case "7":
                    dtIni = ddlAno.SelectedItem.Text + "-07-20";
                    dtFim = ddlAno.SelectedItem.Text + "-08-19";
                    break;
                case "8":
                    dtIni = ddlAno.SelectedItem.Text + "-08-20";
                    dtFim = ddlAno.SelectedItem.Text + "-09-19";
                    break;
                case "9":
                    dtIni = ddlAno.SelectedItem.Text + "-09-20";
                    dtFim = ddlAno.SelectedItem.Text + "-10-19";
                    break;
                case "10":
                    dtIni = ddlAno.SelectedItem.Text + "-10-20";
                    dtFim = ddlAno.SelectedItem.Text + "-11-19";
                    break;
                case "11":
                    dtIni = ddlAno.SelectedItem.Text + "-11-20";
                    dtFim = ddlAno.SelectedItem.Text + "-12-19";
                    break;
            }



            string campos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime AS mov WHERE (mov.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = mov.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND mov.cnscanmom IS NULL AND mov.data BETWEEN '" + dtIni + "'  AND '" + dtFim + "' LIMIT 1) AS gastos ";
            //" m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS convenio, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor * -1) AS valor , m.vencimento, m.data,  a.credito, (SELECT SUM(valor * -1) FROM comovime WHERE convenio = '" + iDAcesso + "' AND cnscanmom IS NULL AND vencimento BETWEEN '2021-03-20'  AND '2021-04-19'  LIMIT 1) AS gastos ";
            string tabela = " comovime AS m ";
            string left = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                          " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                          " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
            string condicao = " WHERE(m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.data BETWEEN '" + dtIni + "' AND '" + dtFim + "' GROUP BY m.link ORDER BY m.cnscadmom DESC ";
            //" WHERE m.convenio = '" + iDAcesso + "' OR co.cnpj_cpf = '" + "" + "' AND m.cnscanmom IS NULL AND m.vencimento BETWEEN '2021-03-20'  AND '2021-04-19' ORDER BY m.parcela DESC ";

            if (ObjDbASU.MsgErro == "")
            {

                ObjDbASU.Campo = campos;
                ObjDbASU.Tabela = tabela;
                ObjDbASU.Left = left;
                ObjDbASU.Condicao = condicao;

                DataTable dados = ObjDbASU.RetCampos();
                int nLinhas = dados.Rows.Count;


                double vparc1 = 0.00;
                double vparc2 = 0.00;
                double vparc3 = 0.00;
                double vparc4 = 0.00;
                double vparc5 = 0.00;
                double vparc6 = 0.00;
                double vparc7 = 0.00;
                double vparc8 = 0.00;
                double vparc9 = 0.00;
                double vparc10 = 0.00;


                for (int i = 0; i < nLinhas; i++)
                {
                    double parc1 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc2 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc3 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc4 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc5 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc6 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc7 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc8 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc9 = double.Parse(dados.Rows[i]["valor"].ToString());
                    double parc10 = double.Parse(dados.Rows[i]["valor"].ToString());

                    if (dados.Rows[i]["parcela"].ToString() == "1")
                    {
                        vparc1 = vparc1 + parc1;
                        //xRet += "<p style='text-align: left'>" + dados.Rows[i]["autorizacao"] + " - " + dados.Rows[i]["parcela"] + " - R$ " + dados.Rows[i]["valor"] + " - " + dados.Rows[i]["convenio"] + " - " + dados.Rows[i]["titular"] + " - " + dados.Rows[i]["cartao"] + "-XX" + " - " + dados.Rows[i]["Comprador"] + "</p>";                    
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "2")
                    {
                        vparc2 = vparc2 + parc2;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "3")
                    {
                        vparc3 = vparc3 + parc3;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "4")
                    {
                        vparc4 = vparc4 + parc4;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "5")
                    {
                        vparc5 = vparc5 + parc5;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "6")
                    {
                        vparc6 = vparc6 + parc6;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "7")
                    {
                        vparc7 = vparc7 + parc7;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "8")
                    {
                        vparc8 = vparc8 + parc8;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "9")
                    {
                        vparc9 = vparc9 + parc9;
                    }
                    if (dados.Rows[i]["parcela"].ToString() == "10")
                    {
                        vparc10 = vparc10 + parc10;
                    }
                    //Fazer if para as parcelas--- = Par1, = parc 2...
                }

                xRet += "<table>";
                xRet += "<caption>" + "Fatura Mensal" + "</caption>";
                xRet += "<tbody>";
                if (vparc1 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 1: " + vparc1.ToString("F2", CultureInfo.InvariantCulture) + " - Referente à movimentação de: " + DateTime.Now.ToString("MMMM") + "/" + DateTime.Now.Year + "</td>" + "</tr>";
                }
                if (vparc2 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 2: " + vparc2.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc3 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 3: " + vparc3.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc4 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 4: " + vparc4.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc5 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 5: " + vparc5.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc6 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 6: " + vparc6.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc7 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 7: " + vparc7.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc8 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 8: " + vparc8.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc9 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 9: " + vparc9.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                if (vparc10 > 0)
                {
                    xRet += "<tr>" + "<td>" + "Parcela 10: " + vparc10.ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                }
                xRet += "</tbody>";
                xRet += "<tfoot>";
                xRet += "<tr>" + "<td>" + "Total á Receber: R$ " + (vparc1 + vparc2 + vparc3 + vparc4 + vparc5 + vparc6 + vparc7 + vparc8 + vparc9 + vparc10).ToString("F2", CultureInfo.InvariantCulture) + "</td>" + "</tr>";
                xRet += "</tfoot>";
                xRet += "</table>";
            }
            else
            {
                xRet += "Erro: " + ObjDbASU.MsgErro;
            }

            return xRet;
        }

        public String montarExtratoConv()
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

            //Gera dados para Pós Pago
            string camposPos = " m.cnscadmom AS Momento, m.idmovime AS Autorizacao, m.parcela, m.parctot, m.convenio, m.lote, co.nome AS conveniado, a.titular, m.depcartao AS cartao, m.associado, m.dependen , d.nome AS Comprador, (m.valor *-1) AS valor, m.vencimento, m.data,  a.credito, (SELECT SUM(valor *-1) FROM comovime WHERE (convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE  c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND cnscanmom IS NULL AND vencimento BETWEEN " + Apoio.Periodo() + "  LIMIT 1) AS gastos ";
            string tabelaPos = " comovime AS m ";
            string leftPos = " INNER JOIN coconven AS co ON co.idconven = m.convenio " +
                             " INNER JOIN associa AS a ON m.associado = a.idassoc " +
                             " INNER JOIN asdepen AS d ON m.dependen = d.iddepen ";
            string condicaoPos = " WHERE (m.convenio = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS(SELECT NULL FROM coconven AS c WHERE c.idconven = m.convenio AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.cnscanmom IS NULL AND m.vencimento BETWEEN " + Apoio.Periodo() + " ORDER BY m.cnscadmom DESC ";


            //lblMsg.Text = Apoio.Periodo();

            if (ObjDbExtPos.MsgErro == "")
            {

                ObjDbExtPos.Campo = camposPos;
                ObjDbExtPos.Tabela = tabelaPos;
                ObjDbExtPos.Left = leftPos;
                ObjDbExtPos.Condicao = condicaoPos;

                DataTable dadosPos = ObjDbExtPos.RetCampos();
                //MessageBox.Show(camposPos + tabelaPos + leftPos + condicaoPos);

                //Apoio.IdConv = dadosPos.Rows[0]["convenio"].ToString(); - 07/12/2021, testar o impacto deste cara


                int nLinhasPos = dadosPos.Rows.Count;

                if (nLinhasPos > 0)
                {
                    gastos = dadosPos.Rows[0]["gastos"].ToString();

                }

                //lblPeriodo.Text = dtIni + " à " + dtFim;
                lblPeriodo.Text = Apoio.Periodo();

                if (nLinhasPos > 0)
                {
                    // xRet += "<div  id='print' class='conteudo' style='min-width: 990px; height: 600px; overflow: scroll; '>";
                    //xRet += "<div style='width: 200px;'>" + "<input type='button' onclick='cont();' value='Imprimir' >" + "</div>" + "<br>";
                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Extrato Mensal";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td colspan='4'>" + dadosPos.Rows[0]["conveniado"] + "</td>";
                    xRet += "<td colspan='3'>" + " Período: " + ddlMesExtratoConv.Text + "/" + ddlAnoExtratoConv.SelectedValue + "</td>";
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
                    xRet += "<td colspan='5' class='right'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2'>" + " R$: " + Apoio.vendasConvenio() + "</td>";
                    xRet += "</tfoot>";
                    xRet += "<tbody>"; //Corpo
                    for (int i = 0; i < nLinhasPos; i++)
                    {
                        xRet += "<tr>";
                        xRet += "<td>" + dadosPos.Rows[i]["momento"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["autorizacao"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["parcela"] + "/" + dadosPos.Rows[0]["parctot"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["lote"] + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["cartao"] + "XX" + "</td>";
                        xRet += "<td>" + dadosPos.Rows[i]["titular"] + "</td>";
                        xRet += "<td>" + "R$    " + dadosPos.Rows[i]["valor"] + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "</table>";
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
            return xRet;
        }

        public String montarExtratoPreConv()
        {
            BLL ObjDbExtPre = new BLL(conectVegas);
            Apoio Apoio = new Apoio();

            //Atribui Ano e Mês ao método Período
            Apoio.Ano = ddlAnoExtratoConv.SelectedValue;
            Apoio.Mes = ddlMesExtratoConv.SelectedValue;

            //Variáveis
            string iDAcesso = Session["codAcesso"].ToString();
            int tCampo = iDAcesso.Length;
            string gastos = "";
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

            string camposPre = " mov.IDSEQ AS autorizacao, mov.CNSCADMOM AS Momento, mov.vencimento, contr.UUIDCONTRATO, CONCAT_WS('', mov.PARCELA, '/', mov.PARCTOT) AS ParcelaDesc, mov.QTDE AS Valor, car.NUMCARTAO, assoc.TITULAR AS Titular, asdep.NOME AS Dependente, unid.DESCRICAO AS Unidade, dpto.DESCRICAO AS Departamento, mov.convenio, conv.NOME AS ConvenioNome, tp.DESCRICAO AS TipoMov, (SELECT  SUM(m.qtde) AS gastosPre FROM cgc_movime AS m  WHERE (m.conv_origid = '" + iDAcesso.Substring(0, (tCampo - 2)) + "' OR EXISTS (SELECT NULL FROM coconven AS c WHERE c.idconven = m.CONV_ORIGID AND c.cnpj_cpf = '" + iDAcesso + "')) AND m.vencimento BETWEEN '2021-4-20' AND '2021-5-19') AS gastosPre ";
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

            if (ObjDbExtPre.MsgErro == "")
            {
                ObjDbExtPre.Campo = camposPre;
                ObjDbExtPre.Tabela = tabelaPre;
                ObjDbExtPre.Left = leftPre;
                ObjDbExtPre.Condicao = condicaoPre;

                //MessageBox.Show(camposPre + tabelaPre + leftPre + condicaoPre);

                DataTable dadosPre = ObjDbExtPre.RetCampos();
                int nLinhasPre = dadosPre.Rows.Count;

                //Apoio.IdConv = dadosPre.Rows[0]["conv_origid"].ToString();

                if (nLinhasPre > 0)
                {
                    gastosPre = dadosPre.Rows[0]["gastosPre"].ToString();

                    //MessageBox.Show("SELECT " + camposPre + " FROM " + tabelaPre + leftPre + condicaoPre);
                }


                //lblPeriodo.Text = dtIni + " à " + dtFim;
                lblPeriodo.Text = Apoio.Periodo();

                // # # # # # Extrato Pré Pago # # # # # 

                if (nLinhasPre > 0)
                {
                    //xRet += "<div id='print' class='conteudo' style='min-height: 200px; min-width: 990px; overflow: scroll; background-color: yellow;'>";
                    //xRet += "<div style='width: 200px;'>" + "<input type='button' onclick='cont();' value='Imprimir' >" + "</div>" + "<br>";
                    //xRet += "<div style='width: 200px;'>" + "<asp:Button ID='btnPdfConvPre' runat='server' Text='Gerar PDF' />" + "Aqui" + "</div>" + "<br>";

                    xRet += "<table>";
                    xRet += "<caption>"; //Caption
                    xRet += "Extrato Mensal - Pré Pago";
                    xRet += "</caption>";
                    xRet += "<thead>"; //Head
                    xRet += "<tr>";
                    xRet += "<td colspan='3'>" + dadosPre.Rows[0]["ConvenioNome"] + "</td>";
                    xRet += "<td colspan='2'>" + " Período: " + ddlMesExtratoConv.Text + "/" + ddlAnoExtratoConv.SelectedValue + "</td>";
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
                        xRet += "<td>" + dadosPre.Rows[i]["dependente"] + "</td>";
                        xRet += "<td>" + dadosPre.Rows[i]["valor"] + "</td>";
                        xRet += "</tr>";
                    }
                    xRet += "</tbody>";
                    xRet += "<tfoot>"; //Footer
                    xRet += "<td colspan='4' class='right'>" + "Total: " + "</td>";
                    xRet += "<td colspan='2'>" + " R$: " + dadosPre.Rows[0]["gastosPre"].ToString() + " " + "</td>";
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
            return xRet;
        }

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
                MessageBox.Show("Sua mensagem foi enviada com sucesso!!! " + agora);

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
            string wMes = ddlMes.SelectedValue;
            string dtIni = "";
            string dtFim = "";
            string msgPeriodo = "";
            string xRet = "";

            //MessageBox.Show(wMes);

            switch (wMes)
            {
                case "0":
                    dtIni = "20-12-" + (Convert.ToInt32(ddlAno.SelectedItem.Text) - 1);
                    dtFim = "19-01-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "1":
                    dtIni = "20-01-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-02-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "2":
                    dtIni = "20-02-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-03-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "3":
                    dtIni = "20-03-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-04-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "4":
                    dtIni = "20-04-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-05-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "5":
                    dtIni = "20-05-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-06-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "6":
                    dtIni = "20-06-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-07-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "7":
                    dtIni = "20-07-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-08-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "8":
                    dtIni = "20-08-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-09-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "9":
                    dtIni = "20-09-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-10-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "10":
                    dtIni = "20-10-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-11-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
                case "11":
                    dtIni = "20-11-" + ddlAno.SelectedItem.Text;
                    dtFim = "19-12-" + ddlAno.SelectedItem.Text;
                    msgPeriodo = "Período: " + dtIni + " à " + dtFim;
                    break;
            }

            lblPeriodo.Text = msgPeriodo;

        }

        protected void fazerLogof(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }


        public void trocarSenhaAssoc(object sender, EventArgs e)
        {
            if (iSenhaNova.Value != iSenhaConfirma.Value)
            {
                //MessageBox.Show("As senhas não conferem, digite-as novamente!!!");
                lblTrocaSenha.Text = "As senhas não conferem, digite-as novamente!!!" + "\n" + "Estamos trabalhando nisso, aguarde. Por hora, entre em contato com o Atendimento da ASU.";

            }
            else
            {
                //MessageBox.Show("Senha: " + iSenhaAtual.Value + "\n" + "Nova Senha: " + iSenhaNova.Value);
                lblTrocaSenha.Text = "Senha: " + iSenhaAtual.Value + "\n" + "Nova Senha: " + iSenhaNova.Value + "\n" + "Estamos trabalhando nisso, aguarde. Por hora, entre em contato com o Atendimento da ASU.";
            }

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


        }

        public void troarSenhaCartao(object sender, EventArgs e)
        {
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

            string arquivosenha = "";

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
                string cArquivo_mostra = "";

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
            catch (Exception erro)
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
            //caminho físico
            string pathDocumento = "" + WebConfigurationManager.AppSettings["CaminhoVendaENV"];

            DirectoryInfo dir = new DirectoryInfo(pathDocumento);

            //Cria arquivo         
            string cVersao = "00001.03";
            //string cVersao = "00001.04";
            string cConvenio = "" + "63193";
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
            string cSenha = "" + iSenha.Value.Replace("'", "");


            cConvenio = Colocazero(cConvenio, 9);
            cValor = Colocazero(cValor, 12);

            string cArq_cont = cVersao + cConvenio + cCartao + cValor + cParcelas + cCod_retorno + cCod_autoriza + cChave_usuario + cNome_usuario + cObserva + cCpf + cSaldo + cSenha;

            //titulo do arquivo
            //string cArq_tit = "" + Session["conveniado"] + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
            //Cr=iando o Arquivo  
            string cArq_tit = "" + "63193" + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
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

            iNumCartao.Value = String.Empty;
            iValorVenda.Value = String.Empty;
            iNumCartao.Focus();

            Processar_retorno();

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
                        cRet_msg = "Cartão não registrado ou senha incorreta";
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

                cArquivo_mostra += "<div id='divretorno' style='font-family:Courier New;font-size:14px;width:360px;'>";// Lucida Console
                cArquivo_mostra += "----------------------------------------<br>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>" + "Conveniado" + "</div>";//xFuncoes.RetCampo("nome", "coconven", "idconven='" + Session["conveniado"] + "'", "ConexaoASU").ToUpper() + "</div>";
                cArquivo_mostra += "----------------------------------------";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Via do Convênio</div>";
                //cArquivo_mostra += "<br>AUTORIZACAO DE DEBITO<br>";
                cArquivo_mostra += "<table cellpadding='0' cellspacing='0' border='0'><tr>";
                cArquivo_mostra += "<td align='right' width='130px'>AUTORIZACAO: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cAutoriza) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Convênio: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cCodConv) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Cartão: </td>";
                cArquivo_mostra += "<td>" + cCodCarta + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Valor: </td>";
                cArquivo_mostra += "<td>" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Parcelas: </td>";
                cArquivo_mostra += "<td>" + cParcela + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Data: </td>";
                cArquivo_mostra += "<td>" + DateTime.Now + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>CPF: </td>";
                cArquivo_mostra += "<td>" + cCpfCnpj + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Retorno: </td>";
                cArquivo_mostra += "<td>" + cRet_msg + "</td>";
                cArquivo_mostra += "</tr></table><br>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>_____________________________________</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Nome do Comprador</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'> " + cNome + " </div><br>";

                cArquivo_mostra += "<div style='width:360px;text-align:center;'>_____________________________________</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Visto do Vendedor</div><br>";

                cArquivo_mostra += "<br><br>----------------------------------------<br>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>" + "Conveniado" + "</div>";//xFuncoes.RetCampo("nome", "coconven", "idconven='" + Session["conveniado"] + "'", "ConexaoASU").ToUpper() + "</div>";
                cArquivo_mostra += "----------------------------------------";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Via da ASU</div>";
                //cArquivo_mostra += "<br>AUTORIZACAO DE DEBITO<br><br>";
                cArquivo_mostra += "<table cellpadding='0' cellspacing='0' border='0'><tr>";
                cArquivo_mostra += "<td align='right' width='130px'>AUTORIZACAO: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cAutoriza) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Convênio: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cCodConv) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Cartão: </td>";
                cArquivo_mostra += "<td>" + cCodCarta + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Valor: </td>";
                cArquivo_mostra += "<td>" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Parcelas: </td>";
                cArquivo_mostra += "<td>" + cParcela + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Data: </td>";
                cArquivo_mostra += "<td>" + DateTime.Now + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>CPF: </td>";
                cArquivo_mostra += "<td>" + cCpfCnpj + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Retorno: </td>";
                cArquivo_mostra += "<td>" + cRet_msg + "</td>";
                cArquivo_mostra += "</tr></table><br>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>_____________________________________</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Nome do Comprador</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'> " + cNome + " </div><br>";

                cArquivo_mostra += "<div style='width:360px;text-align:center;'>_____________________________________</div>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Visto do Vendedor</div>";

                cArquivo_mostra += "<br><br><br>----------------------------------------<br>";
                cArquivo_mostra += "<div style='width:360px;text-align:center;'>Via do Associado</div><br>";
                //cArquivo_mostra += "<br>AUTORIZACAO DE DEBITO<br><br>";

                cArquivo_mostra += "<table cellpadding='0' cellspacing='0' border='0'><tr>";
                cArquivo_mostra += "<td align='right' width='130px'>AUTORIZACAO: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cAutoriza) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Convênio: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cCodConv) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Cod.Cartão: </td>";
                cArquivo_mostra += "<td>" + cCodCarta + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Valor: </td>";
                cArquivo_mostra += "<td>" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Parcelas: </td>";
                cArquivo_mostra += "<td>" + cParcela + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Data: </td>";
                cArquivo_mostra += "<td>" + DateTime.Now + "</td>";
                cArquivo_mostra += "</tr><tr>";
                cArquivo_mostra += "<td align='right'>Saldo: </td>";
                cArquivo_mostra += "<td>" + CortaZeros(cSaldo) + "</td>";
                cArquivo_mostra += "</tr></table><br>";
                cArquivo_mostra += "</div>";

                //verificando se a venda ocorreu
                if (cCodRet != "000")
                {
                    string cMsg_neg = "Venda NÃO realizada: " + cRet_msg;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Venda.aspx?op=convdados&menu=n';", true);
                }

                //EFETUAR RETORNO, relatórios, etc
                //lblRetorno.Text = "<span style='color:DarkGreen;font-size:18px;'>VENDA EFETUADA COM SUCESSO! Transa: " + cCod_transa + "</span>";
                lblRetorno.Text = cArquivo_mostra;

                //Response.Redirect("VoceOnLineComprovante.aspx"); Aprender a Abrir em uma nova Janela, e com botão para imprimir! 
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

        protected void gerarPdfExtrato(object sender, EventArgs e)
        {
            Apoio ObjPdf = new Apoio();

            //## Extrato ##

            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";
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


                double gastos = 0;
                double limite = 0;
                double saldo = 0;


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

        protected void gerarPdfExtratoAssoc(object sender, EventArgs e)
        {

            BLL ObjDados = new BLL(conectVegas);

            /*
             string xRet = "";
             string xPdf = "";



             ObjDados.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito, " +
                              " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '26870730830' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '26870730830'))) AND vencimento BETWEEN '2021-09-20' AND '2021-10-19' LIMIT 1) AS gastos  ";
             ObjDados.Tabela = " comovime AS c  ";
             ObjDados.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                             " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                             " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
             ObjDados.Condicao = " WHERE (a.cnpj_cpf ='26870730830' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '26870730830'))) AND c.vencimento BETWEEN '2021-09-20'  AND '2021-10-19' ";
            */

            string xRet = "";
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
                    ObjDados.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                     " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '" + janeiro + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                    //  " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND vencimento BETWEEN '" + janeiro + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";

                }
                else
                {
                    ObjDados.Campo = " c.idmovime, EXTRACT(DAY FROM c.data) AS dia, c.convenio, co.nome AS conveniado, c.associado, a.titular, c.depcartao AS cartao, c.dependen, d.nome AS comprador, c.valor, c.vencimento, c.data, c.parcela, c.parctot, c.cnscadmom, a.credito," +
                                     " (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE c.associado='" + IdAssoc + "' AND vencimento BETWEEN '" + dtIni + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                    //" (SELECT SUM(valor) FROM comovime AS c INNER JOIN associa AS a ON a.idassoc = c.associado INNER JOIN asdepen AS d ON c.dependen = d.iddepen WHERE (a.cnpj_cpf = '" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND vencimento BETWEEN '" + dtInicio + "' AND '" + dtFim + "' LIMIT 1) AS gastos ";
                }

                ObjDados.Tabela = " comovime AS c ";
                ObjDados.Left = " INNER JOIN coconven AS co ON co.idconven = c.convenio " +
                                " INNER JOIN associa AS a ON c.associado = a.idassoc " +
                                " INNER JOIN asdepen AS d ON c.dependen = d.iddepen ";
                if (mestFim.ToString("MM") == "01")
                {
                    ObjDados.Condicao = " WHERE a.idassoc ='" + IdAssoc + "' AND c.vencimento BETWEEN '" + janeiro + "'  AND '" + dtFim + "' ORDER BY dia ";
                    //" WHERE (a.cnpj_cpf ='" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND c.vencimento BETWEEN '" + janeiro + "'  AND '" + dtFim + "' ";
                }
                else
                {
                    ObjDados.Condicao = " WHERE a.idassoc='" + IdAssoc + "' AND c.vencimento BETWEEN '" + dtIni + "'  AND '" + dtFim + "' ORDER BY dia ";
                    //ObjDbASU.Condicao = " WHERE (a.cnpj_cpf ='" + iDAcesso + "' OR (EXISTS(SELECT NULL FROM asdepcar AS car WHERE d.iddepen = car.dependen AND car.idcartao = '" + iDAcesso.Substring(0, (tCampo - 2)) + "'))) AND c.vencimento BETWEEN '" + dtInicio + "'  AND '" + dtFim + "' ";
                }
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
            string dest = AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\" + IdAssoc + ".pdf";

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

            /*Exibir Arquivos no Diretório*/
            //DirectoryInfo diretorio = new DirectoryInfo(@"K:\Projetos\Web\Site\Site\Downloads");
            DirectoryInfo diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Downloads\");

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            //FileInfo[] Arquivos = diretorio.GetFiles("*.*");
            FileInfo[] Arquivos = diretorio.GetFiles(IdAssoc + ".pdf");

            string arquivos = "";
            string xArq = "";
            //Listar os arquivos



            foreach (FileInfo fileinfo in Arquivos)
            {
                arquivos = fileinfo.Name;

                xArq += "<div>";
                //xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>" + arquivos;
                xArq += "<a href='" + @"Downloads\" + arquivos + "' target=_blanck>";
                xArq += "<img style='width: 50%;' src ='" + "Img/icon/icon-pdf.png" + "' />";
                xArq += "<p> Seu Extrato </p>";
                xArq += "</a><br>";
                xArq += "</div>";
            }
            lblExtratoPdf.Text = xArq;


        }
        /**/
    }
}