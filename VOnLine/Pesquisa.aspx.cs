using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Site.App_Code;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Site.VOnLine
{
    public partial class Pesquisa : System.Web.UI.Page
    {    
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {

           this.DataBind();

            if (!Page.IsPostBack) {
                ativarVwDados();                
            }
        }

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
            
        }//usuarioLogado
        protected void ativarVwDados()
        {
            mwPesquisa.ActiveViewIndex = 0;
        }
        protected void ativarVwMsg()
        {
            mwPesquisa.ActiveViewIndex = 1;
            
        }

        public String checarSessao(string tamanho)
        {
            string identifica = "";

            if (Session["VoceOnLine"] != null)
            {
                identifica = Session["Identifica"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");

            }

            int tamanhocampo = identifica.Length;

            string logado = "";

            if (tamanhocampo == 11)
            {
                logado = Session["IdAssoc"].ToString();
            }
            else if (tamanhocampo == 14)
            {
                logado = Session["LoginIdConvenio"].ToString();
            }
            return logado;
        }//checarSessao

        protected void voltarVOConvenio(object sender, EventArgs e)
        {
            if (Session["VoceOnLine"] != null)
            {
                Response.Redirect("VoceOnLine_New.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            
        }
        protected void finalizarPesquisa(object sender, EventArgs e)
        {
            Apoio ObjApoio = new Apoio();
            BLL ObjValida = new BLL(conectVegas);

            string codAcesso = Session["CodAcesso"].ToString();

            //MessageBox.Show(tipoUsuarioLogado(codAcesso));

            string query = "";            

            string xRet = "";
            //string para = "gsxtecnologia@gmail.com";
            string para = "informatica@asu.com.br";
            string cc = "reginaldo@asu.com.br";
            string assunto = "Pesquisa de Opinião";
            string msg = "";
            string insert = "";

            //Criar dados para Insert

            string tabela = " PES_RESPOSTAS ";
            string campos = " IDPESQUISA, IDPERGUNTA, ASSOCIADO, DEPENDEN, CONVENIO, RESPVALOR, TIPORESPOSTADADO, CNSCADUSU, CNSCADMOM ";
            string valores = "";

            if (!String.IsNullOrEmpty(iDados.Value))
            {
                valores = iDados.Value.Substring(0, (iDados.Value.Length - 1));
            }
            else
            {
                valores = "";

            }

            //# Query para Checar se Associado já participou da Pesquisa            
            string validar = "";            

            //# Fim Query
            try
            {
                if (Session["VoceOnLine"] != null)
                {
                    //Checa se é Associado ou Conveniado
                    if (tipoUsuarioLogado(codAcesso) == "Associado")
                    {
                        validar = " SELECT * FROM PES_RESPOSTAS " +
                                  " WHERE associado = '" + Session["IdAssoc"].ToString() + "'" +
                                  " AND idpesquisa = '1'";
                    }
                    else
                    {
                        validar = " SELECT * FROM PES_RESPOSTAS " +
                                  " WHERE convenio = '" + codAcesso.Substring(0, (codAcesso.Length - 2)) + "'" +
                                  " AND idpesquisa = '1'";
                    }

                    ObjValida.Query = validar;
                    DataTable dadosvalidacao = ObjValida.RetQuery();


                    //MessageBox.Show(validar);

                    if (ObjValida.MsgErro == "")
                    {

                        if (!String.IsNullOrEmpty(dadosvalidacao.Rows.Count.ToString()))
                        {
                            //MessageBox.Show("Entrou para cadastrar " + iDados.Value);                            

                            //MessageBox.Show("Ainda não participou");
                            if (!String.IsNullOrEmpty(iDados.Value))
                            {
                                insert = "INSERT INTO " + tabela + " (" + campos + ") " + " VALUES" + valores + "";                                
                                
                                //MessageBox.Show(insert);

                                iDados.Value = String.Empty;
                                iDados.Value = insert;

                                //# # # Realiza a gravação dos dados # # # 
                                //ObjValida.InsertEmLote(tabela, campos, valores.Substring(0, valores.Length - 1));

                                ativarVwMsg();

                                //# # # Enviar E-mail # # #

                                msg = insert;

                                ObjApoio.EnviarEmail(para,cc,assunto,msg);

                                lblMsg.Text = "Sua Pesquisa foi processada com Sucesso. Obrigado por participar!!!";

                                //lblExibeDados.Text = iDados.Value.Substring(0, valores.Length - 1); //Para Exibir Query
                                //MessageBox.Show(iDados.Value); //Para Exibir Query


                            }
                            else
                            {
                                ativarVwMsg();
                                lblMsg.Text = "Sem dados para processamento!";
                                //MessageBox.Show("Sem dados para processamento!");
                            }

                        }
                        else
                        {
                            ativarVwMsg();
                            lblMsg.Text = "Você já está participando, obrigado!";
                            //msg = "<label id='lblMsgParticipando' runat='server'>" + "Você já está participando, obrigado!" + "</label>";
                            /*<script>
                               function participa() {
                                     alert("Você já está participando, obrigado!");
                              }
                            </script>*/

                            //MessageBox.Show("Você já está participando, obrigado!");
                        }
                    }
                    else
                    {
                        ativarVwMsg();
                        lblMsg.Text = "Erro: " + ObjValida.MsgErro.ToString();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

            }
            catch (Exception x)
            {
                lblMsg.Text = "Erro: " + x.Message;
                //MessageBox.Show("Erro: " + x.Message);


            }

            //MessageBox.Show(ObjValida.RetQuery().ToString());

        }

        public String paginaPesquisa()
        {
            //Objetos
            BLL ObjPerguntas = new BLL(conectVegas);
            BLL ObjRespostas = new BLL(conectVegas);
            BLL ObjQtdPerguntas = new BLL(conectVegas);
            

            //Variáceis
            string xRet = "";
            string Q_perguntas = "SELECT " +
                             " CONCAT(per.tiporesposta, per.id) AS 'ById'," +
                             " pes.descricao, " +
                             " pes.datainicial, " +
                             " pes.datafinal, " +
                             " per.idpesquisa AS idpesquisa, " +
                             " per.id AS idpergunta,  " +
                             " per.pergunta, " +
                             " per.tiporesposta" +
                             " FROM PESQUISA AS PES " +
                             " INNER JOIN PES_PERGUNTA AS PER ON PES.ID = PER.IDPESQUISA " +
                             " WHERE pes.datainicial <= CURDATE() AND pes.datafinal >= CURDATE()" +
                             //" AND tiporesposta IN('sp')" +
                             " ORDER BY idpergunta, tiporesposta";
            
            string Q_Respostas = " SELECT " +
                             " CONCAT(p.tiporesposta, p.id) AS ById," +
                             " r.respdescricao, r.respcodigo, r.id AS idResposta,p.id AS idPergunta, p.pergunta, p.tiporesposta" +
                             " FROM pes_opcaoresp AS r" +
                             " LEFT JOIN PES_PERGUNTA AS P ON r.idpergunta = p.id " +
                             " WHERE r.cnscanmom IS NULL AND p.cnscanmom IS NULL ";

            string Q_QtdPerguntas = " SELECT COUNT(id) AS qtd FROM pes_pergunta " +
                                    " WHERE tiporesposta IN('OE', 'EN') ";


            //#Checar Usuário logado
            string codAcesso = "";
            int tCampo = 0;
            string idUsuario = "";
            string idConv = "";

            //Acesso aos dados
            //Q_ = Query
            //D_ = Dados

            ObjPerguntas.Query = Q_perguntas;
            DataTable D_Perguntas = ObjPerguntas.RetQuery();            

            ObjRespostas.Query = Q_Respostas;
            DataTable D_Respostas = ObjRespostas.RetQuery();

            ObjQtdPerguntas.Query = Q_QtdPerguntas;
            DataTable D_QtdPerguntas = ObjQtdPerguntas.RetQuery();

            //MessageBox.Show("Quantidade de Perguntas: " + D_QtdPerguntas.Rows[0]["qtd"].ToString());
                
            //MessageBox.Show("Perguntas: " + D_Perguntas.Rows.Count.ToString());
                        
            //MessageBox.Show("Respostas: " + D_Respostas.Rows.Count.ToString());

            if (Session["VoceOnLine"] != null)
            {
                codAcesso = Session["CodAcesso"].ToString();

                if (tipoUsuarioLogado(codAcesso).ToString() == "Associado")
                {
                    idUsuario = Session["IdAssoc"].ToString();
                    xRet += "<style>" +
                    ".corpoPesquisaC{" +
                    "    margin-top: 10px;" +
                    "    padding-bottom: 20px;" +
                    "    width: 600px;" +
                    "    min-height: 50px;" +
                    //"    background-color: #3873b7;" +
                    "}" +
                    ".divPergunta{" +
                    "    width: 550px;" +
                    "    min-height: 20px;" +
                    "    margin: 0 auto;" +
                    //"    background-color: #89c0e8;" +
                    "}" +
                    ".divOE{" +
                    "    width: 550px;" +
                    "    min-height: 20px;" +
                    "    margin: 0 auto;" +
                    //"    background-color: #A83B5A;" +
                    "}" +
                    ".divResposta{" +
                    "    width: 550px;" +
                    "    min-height: 20px;" +
                    "    margin: 0 auto;" +
                    //"    background-color: #d4eaca;" +
                    "}" +
                    "</style>";
                    int contSP = 0;
                    int contPOE = 0;
                    int contROE = 0;

                    //Pergunta Aberta
                    xRet += "<section id='secPA' class='corpoPesquisaC'>";
                    for (int i = 0; i < D_Perguntas.Rows.Count; i++)
                    {
                        if (D_Perguntas.Rows[i]["tiporesposta"].ToString() == "SP")
                        {
                            contSP++;

                            //MessageBox.Show("ContSP: " + contSP);

                            //xRet += "<p>" + "Pergunta Aberta" + "</p>";
                            xRet += "<div class='divPergunta'>";
                            xRet += "<p id='" + D_Perguntas.Rows[i]["tiporesposta"].ToString() + Convert.ToString(contSP) + "'>" + D_Perguntas.Rows[i]["pergunta"].ToString() + "</p>";
                            xRet += "</div>";
                            xRet += "<div class='divResposta'>";
                            xRet += " <textarea id='" + "R" + D_Perguntas.Rows[i]["tiporesposta"].ToString() + Convert.ToString(contSP) + "' runat='server' class='resposta'></textarea>";
                            //xRet += "<p>" + "Resposta" + "</p>";
                            xRet += "</div>";

                            xRet += "<div style='display: none'>";
                            xRet += "<label id='idpesquisa' visible='false' value='" + D_Perguntas.Rows[i]["idpesquisa"].ToString() + "' >" + D_Perguntas.Rows[i]["idpesquisa"].ToString() + "</label>";
                            xRet += "<label id='idpergunta" + "' visible='false' value='" + D_Perguntas.Rows[i]["idpergunta"].ToString() + "' >" + D_Perguntas.Rows[i]["idpergunta"].ToString() + "</label>";
                            xRet += "<label id='idAssoc' visible='false' value='" + idUsuario + "' >" + idUsuario + "</label>";
                            xRet += "<label id='idDepen' visible='false' value='" + "IDDepen" + "' >" + "IDDepen" + "</label>";
                            xRet += "<label id='idConv' visible='false' value='" + "Null" + "' >" + "Null" + "</label>";
                            xRet += "<label id='icadmom' visible='false' value='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "' >" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "</label>";
                            xRet += "<label id='icadusu' visible='false' value='" + "Site" + "' >" + "Site" + "</label>";
                            xRet += "</div>";
                        }
                    }
                    xRet += "</section>";
                    
                    //Opções de Escolha
                    xRet += "<section id='secOE' class='corpoPesquisaC'>";
                    for (int i = 0; i < D_Perguntas.Rows.Count; i++)
                    {
                        if (D_Perguntas.Rows[i]["tiporesposta"].ToString() == "OE" || D_Perguntas.Rows[i]["tiporesposta"].ToString() == "EN")
                        {
                            contPOE++;                             

                            //xRet += "<p>" + "Opções de Escolha" + "</p>";
                            xRet += "<div class='divOE'>";
                            xRet += "<p id='" + "OE" + Convert.ToString(contPOE) + "'>" + D_Perguntas.Rows[i]["pergunta"].ToString() + "</p>";
                            xRet += "</div>";
                            xRet += "<div class='divResposta'>";

                            //MessageBox.Show("No If: " + "OE" + Convert.ToString(contPOE) + " - " + dadosSP.Rows[i]["pergunta"].ToString());


                            for (int j = 0; j< D_Respostas.Rows.Count; j++)
                            {
                                if (D_Perguntas.Rows[i]["idpergunta"].ToString() == D_Respostas.Rows[j]["idpergunta"].ToString())                                {
                                    contROE++;
                                    xRet += "<div style='display: none'>";
                                    xRet += "<label id='POE" + contROE + "' runat='server'  for='iR" + contROE + "'>" + D_Perguntas.Rows[i]["pergunta"].ToString() + "</label><br />";
                                    xRet += "</div>";
                                    xRet += "<input id='iR" + contROE + "' name='radio" + D_Respostas.Rows[j]["idpergunta"].ToString() + "' runat='server' type='radio' value='" + D_Respostas.Rows[j]["respdescricao"].ToString() + "' />";                                    
                                    xRet += "<label id='ROE" + contROE + "' runat='server'  for='iR" + contROE + "'" + "value='" + D_Perguntas.Rows[i]["pergunta"].ToString()  + "' >" + D_Respostas.Rows[j]["respdescricao"].ToString() + "</label><br />";                                                                        
                                }
                            }
                            
                            xRet += "</div>";
                            xRet += "<div style='display: none'>";
                            xRet += "<label id='idpesquisa' visible='false' value='" + D_Perguntas.Rows[i]["idpesquisa"].ToString() + "' >" + D_Perguntas.Rows[i]["idpesquisa"].ToString() + "</label>";
                            xRet += "<label id='idpergunta" + "' visible='false' value='" + D_Perguntas.Rows[i]["idpergunta"].ToString() + "' >" + D_Perguntas.Rows[i]["idpergunta"].ToString() + "</label>";
                            
                            xRet += "<label id='idAssoc' visible='false' value='" + idUsuario + "' >" + idUsuario + "</label>";
                            xRet += "<label id='idDepen' visible='false' value='" + "IDDepen" + "' >" + "IDDepen" + "</label>";
                            xRet += "<label id='idConv' visible='false' value='" + idConv + "' >" + idConv + "</label>";
                            xRet += "<label id='icadmom' visible='false' value='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "' >" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "</label>";
                            xRet += "<label id='icadusu' visible='false' value='" + "Site" + "' >" + "Site" + "</label>";
                            xRet += "<label id='iQtdPerguntas' visible='false' value='" + D_QtdPerguntas.Rows[0]["qtd"].ToString() + "' >" + D_QtdPerguntas.Rows[0]["qtd"].ToString() + "</label>";
                            xRet += "</div>";
                        }                        
                    }
                    xRet += "</section>";


                    //MessageBox.Show( " Fora do For -> id=iR + contROE + " + contROE);
                }
                else
                {
                    //Aqui quando for conveniado

                    idUsuario = Session["LoginIdConvenio"].ToString();
                }

                //MessageBox.Show("IdUsu: " + idUsuario + " - IdConv: " + idConv);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


            return xRet;
        }//paginaPesquisa


    }//class
}