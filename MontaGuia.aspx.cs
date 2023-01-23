using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;

namespace Site
{
    public partial class MontaGuia : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();

            if (!Page.IsPostBack)
            {
                popularRamo();
            }
        }


        protected void popularRamo()
        {
            BLL ObjQuery = new BLL(conectVegas);

#pragma warning disable CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado
            string xRet = "";
#pragma warning restore CS0219 // A variável "xRet" é atribuída, mas seu valor nunca é usado

            string query = " SELECT  'Escolha' AS tipoconv, 'Selecionar' AS  descricao, 1 AS Qtde, 1 AS ordem " +
                           " FROM base_cotipo " +
                           " UNION SELECT  c.tipoconv, t.descricao, COUNT(c.idconven) AS Qtde, 2 AS ordem " +
                           " FROM coconven AS c " +
                           " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo " +
                           " WHERE c.cnscanmom IS NULL AND tipoconv NOT IN('045', '046') GROUP BY c.tipoconv ORDER BY ordem, descricao ASC ";

            ObjQuery.Query = query;

            //string query = "SELECT " + campos + " FROM " + tabela + " " + left + " " + condicao;
            //MessageBox.Show(query);

            ddlBuscaPorRamo.DataSource = ObjQuery.RetQuery();
            ddlBuscaPorRamo.DataValueField = "tipoconv";
            ddlBuscaPorRamo.DataTextField = "descricao";
            ddlBuscaPorRamo.DataBind();

        }

        protected void executarConvenios(object sender, ImageClickEventArgs e)
        {
            iBuscar.Value = "%";
            lblRetDestaque.Text = String.Empty;
            buscarConveniosLogo("'%'", "'LOJA'");
            iBuscar.Value = String.Empty;
        }
        protected void executarSaude(object sender, ImageClickEventArgs e)
        {
            iBuscar.Value = "%";
            lblRetDestaque.Text = String.Empty;
            buscarConveniosLogo("'%'", "'MEDIC'");
            iBuscar.Value = String.Empty;
        }
        protected void BuscarConvenioDDL(string TextBusca, string Classe)
        {
            TextBusca = ddlBuscaPorRamo.SelectedItem.Text;
            Classe = "'Loja', 'medic', 'parcasu'";
            buscarConveniosDestaque(TextBusca);
            buscarConveniosLogo(TextBusca, Classe);

        }
        protected void MostrarConvenioDDL(object sender, EventArgs e)
        {
            iBuscar.Value = ddlBuscaPorRamo.SelectedItem.Text;
            string TextBusca = ddlBuscaPorRamo.SelectedItem.Text;
            string Classe = "'Loja', 'medic', 'parcasu'";

            BuscarConvenioDDL(TextBusca, Classe);

            iBuscar.Value = String.Empty;
        }
        public void buscarConveniosDestaque(string TextBusca)
        {
            BLL ObjDbDest = new BLL(conectVegas);

            TextBusca = iBuscar.Value.Replace(" ", "%").Replace("'", "%");

            string xRet = "";
            string msgErroCampoDeBusca = "";

            //Query para Retorno de dados do Destaque          

            string campoDest = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, i.descricao AS path, i.tipo AS tipoimg, i.dt_inicio, i.dt_fim AS Validade  ";
            string tabDest = " coinfo AS i  ";
            string leftDest = " INNER JOIN coconven AS c ON c.idconven = i.convenio " +
                              " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condDest = " WHERE i.tipo IN('SGUIADEST') AND i.dt_fim IS NULL AND i.cnscanmom IS NULL AND(c.nome LIKE '%" + TextBusca + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + TextBusca + "%')) ORDER BY c.nome ASC ";

            ObjDbDest.Campo = campoDest;
            ObjDbDest.Tabela = tabDest;
            ObjDbDest.Left = leftDest;
            ObjDbDest.Condicao = condDest;
            DataTable dadosDest = ObjDbDest.RetCampos();

            //MessageBox.Show("Destaque: " + "SELECT " + campoDest + " FROM " + tabDest + " " + leftDest + condDest);            

            try
            {
                xRet += "<section style='width: 100%; min-height: 20px;'>";
                if (String.IsNullOrEmpty(iBuscar.Value))
                {
                    msgErroCampoDeBusca = String.Empty;
                    msgErroCampoDeBusca = "É preciso preencher o campo de 'Busca' para procurarmos o que você deseja...";

                    xRet += "<div class='linha-convenio-p'>";
                    xRet += "<p>" + msgErroCampoDeBusca + "</p>";
                    xRet += "</div>";

                    lblRetDestaque.Text = xRet; //Exibe mensagem
                    iBuscar.Focus(); //Foca o campo Buscar para difitação do usuário
                }
                else
                {
                    if (String.IsNullOrEmpty(ObjDbDest.MsgErro))
                    {


                        if (dadosDest.Rows.Count > 0)
                        {
                            //MessageBox.Show("Teve dados...");
                            lblMsgErro.Text = String.Empty;
                        }
                        else
                        {
                            msgErroCampoDeBusca = String.Empty;
                            msgErroCampoDeBusca = " Não encontramos referência ao texto digitado. Por favor, faça uma nova Pesquisa ";
                            string xMsg = "";
                            xMsg += "<div class='linha-convenio-p'>";
                            xMsg += "<p>" + msgErroCampoDeBusca + "</p>";
                            xMsg += "</div>";

                            lblMsgErro.Text = xMsg;
                        }
                        //Destaque
                        if (dadosDest.Rows.Count > 0)
                        {
                            xRet += "<section class='SecGuiaDestaque'>";//Class em Form-Clean
                            for (int i = 0; i < dadosDest.Rows.Count; i++)
                            {
                                if (String.IsNullOrEmpty(dadosDest.Rows[i]["validade"].ToString()))
                                {
                                    xRet += "<section class='Slider'>";
                                    xRet += "<div class='guiaDestaque'>";
                                    xRet += "<img src='../" + dadosDest.Rows[i]["path"] + "'>";
                                    xRet += "</div>";
                                    xRet += "<div class='guiaTexto'>";
                                    xRet += "<p class='linha-convenio'>" + dadosDest.Rows[i]["nome"].ToString() + "</p>";
                                    if (!String.IsNullOrEmpty(dadosDest.Rows[i]["fax"].ToString()))
                                    {
                                        xRet += "<address style='margin: 0; padding: 0;'><a href='tel:" + dadosDest.Rows[i]["fone"] + "' > " + dadosDest.Rows[i]["fone"] + "</a>" + "<a href='https://wa.me/55" + dadosDest.Rows[i]["ddd"] + dadosDest.Rows[i]["fax"] + "?text=Olá, Sou Associado(a) da ASU e quero falar com vocês!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg' style='width: 25px;'/>" + /*"&nbsp" +*/ dadosDest.Rows[i]["fax"] + "</a></address>";
                                    }
                                    else
                                    {
                                        xRet += "<address style='margin: 0; padding: 0;'><a href='tel:" + dadosDest.Rows[i]["fone"] + "' > " + dadosDest.Rows[i]["fone"] + "</a></address>";
                                    }
                                    xRet += "<p class='linha-dadosconv'>" + dadosDest.Rows[i]["endereco"].ToString() + ", " + dadosDest.Rows[i]["numero"].ToString() + "</p>";
                                    xRet += "<p class='linha-dadosconv'>" + dadosDest.Rows[i]["bairro"].ToString() + " - " + dadosDest.Rows[i]["cidade"].ToString() + "</p>";
                                    xRet += "</div>";
                                    xRet += "</section>";
                                }
                            }
                            xRet += "</section>";
                        }
                        else
                        {
                            xRet += "<section class='SecGuiaDestaque'>";//Class em Form-Clean                        
                            xRet += "<div class='guiaImg'>";
                            xRet += "<img class='Slider' src='../img/guia/Banner Publicidade Guia.jpg '>";
                            xRet += "</div>";
                            xRet += "</section>";

                        }

                        lblRetDestaque.Text = xRet;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(ObjDbDest.MsgErro))
                        {
                            lblMsgErro.Text = "Destaque: " + ObjDbDest.MsgErro;
                        }

                    }

                }


                xRet += "</section>";
            }
            catch (Exception x)
            {
                xRet += "Erro:" + x.Message;
            }

            lblRetDestaque.Text = xRet;
        }
        public void buscarConveniosLogo(string TextBusca, string Classe)
        {
            BLL ObjDbLogo = new BLL(conectVegas);

            TextBusca = iBuscar.Value.Replace(" ", "%").Replace("'", "%");

            string xRet = "";
            string msgErroCampoDeBusca = "";

            //Query para retorno de dados do Logo            
            string campoLogo = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, " +
                               " (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIALOGO' LIMIT 1) AS path," +
                               " (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIALINK' AND((CURDATE() BETWEEN img.dt_inicio AND img.dt_fim) OR(img.dt_fim IS NULL)) ORDER BY img.cnscadmom DESC LIMIT 1 ) AS link,  " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, " +
                               " (SELECT dt_fim FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIALOGO') LIMIT 1) AS Validade, " +
                               " (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg ";
            string tabLogo = " coconven AS c ";
            string leftLogo = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condLogo = " WHERE c.cnscanmom IS NULL AND classe IN (" + Classe + ") AND (c.nome LIKE '%" + TextBusca + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + TextBusca + "%')) ORDER BY c.nome ASC ";


            //MessageBox.Show("Logo: " + "SELECT " + campoLogo + " FROM " + tabLogo + " " + leftLogo + condLogo);

            ObjDbLogo.Campo = campoLogo;
            ObjDbLogo.Tabela = tabLogo;
            ObjDbLogo.Left = leftLogo;
            ObjDbLogo.Condicao = condLogo;

            DataTable dadosLogo = ObjDbLogo.RetCampos();

            //int nLinhasLogo = dadosLogo.Rows.Count;

            try
            {
                if (Classe == "'MEDIC'")
                {
                    xRet += "<div style=''>";
                    xRet += "<h1 style='width: 100 %; text-align: center; background-color: #22396f; color: white; padding: 10px; font-size: 2em;'>" + "Saúde" + "</h1>";
                    xRet += "</div>";
                }
                if (Classe == "'LOJA'")
                {
                    xRet += "<div style=''>";
                    xRet += "<h1 style='width: 100 %; text-align: center; background-color: #22396f; color: white; padding: 10px; font-size: 2em;'>" + "Comércio" + "</h1>";
                    xRet += "</div>";
                }

                xRet += "<section style='width: 100%; min-height: 20px;'>";

                if (String.IsNullOrEmpty(iBuscar.Value))
                {
                    msgErroCampoDeBusca = String.Empty;
                    msgErroCampoDeBusca = "É preciso preencher o campo de 'Busca' para procurarmos o que você deseja...";

                    xRet += "<div class='linha-convenio-p'>";
                    xRet += "<p>" + msgErroCampoDeBusca + "</p>";
                    xRet += "</div>";

                    lblRetLogo.Text = xRet; //Exibe mensagem
                    iBuscar.Focus(); //Foca o campo Buscar para difitação do usuário
                }
                else
                {
                    if (ObjDbLogo.MsgErro == "")
                    {


                        if (dadosLogo.Rows.Count > 0)
                        {
                            //MessageBox.Show("Teve dados...");
                            lblMsgErro.Text = String.Empty;
                        }
                        else
                        {
                            msgErroCampoDeBusca = String.Empty;
                            msgErroCampoDeBusca = " Não encontramos referência ao texto digitado. Por favor, faça uma nova Pesquisa ";
                            string xMsg = "";
                            xMsg += "<div class='linha-convenio-p'>";
                            xMsg += "<p>" + msgErroCampoDeBusca + "</p>";
                            xMsg += "</div>";

                            lblMsgErro.Text = xMsg;
                        }
                        //Logo
                        for (int i = 0; i < dadosLogo.Rows.Count; i++)
                        {
                            xRet += "<section class='div-guia'>";
                            xRet += "<div class='publiLogo'>";
                            if (String.IsNullOrEmpty(dadosLogo.Rows[i]["tipoimg"].ToString()))
                            {
                                //xRet += "<img src='../Img/
                                ///Destaque Guia.jpg'>";
                                xRet += "<p>Seu Logo</p> ";
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(dadosLogo.Rows[i]["validade"].ToString()))//Checa Validade da Publicidade
                                {
                                    if (!String.IsNullOrEmpty(dadosLogo.Rows[i]["link"].ToString()))
                                    {
                                        xRet += "<a href='" + dadosLogo.Rows[i]["link"].ToString() + "' target='_blank'>";
                                        xRet += "<img src='../" + dadosLogo.Rows[i]["path"] + "'>";
                                        xRet += "</a>";
                                    }
                                    else
                                    {
                                        xRet += "<img src='../" + dadosLogo.Rows[i]["path"] + "'>";
                                    }
                                }
                                else
                                {
                                    //xRet += "<img src='../" + dadosLogo.Rows[i]["path"] + "'>";
                                    xRet += "<p>Seu Logo</p> ";
                                }

                                //xRet += "<p>Tem Logo</p> ";
                            }
                            xRet += "</div>";
                            xRet += "<div class='publiTexto'>";
                            xRet += "<span class='linha-convenio'>" + dadosLogo.Rows[i]["nome"] + "</span>" + "<br>";
                            xRet += "<span class='linha-dadosconvramo'>" + dadosLogo.Rows[i]["descricao"] + "</span>";
                            //xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dadosLogo.Rows[i]["fone"] + "'>" + dadosLogo.Rows[i]["fone"] + "</a></address>" + "</span>";

                            if (!String.IsNullOrEmpty(dadosLogo.Rows[i]["fax"].ToString()))
                            {
                                //xRet += "<address style='margin: 0; padding: 0;'><a href='tel:" + dadosLogo.Rows[i]["fone"] + "' > " + dadosLogo.Rows[i]["fone"] + "</a>" + "<a href='https://wa.me/55" + dadosLogo.Rows[i]["ddd"] + dadosLogo.Rows[i]["fax"] + "?text=Olá, preciso de ajuda!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg'/>" + "&nbsp" + dadosLogo.Rows[i]["fax"] + "</a></address>";
                                xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dadosLogo.Rows[i]["fone"] + "'>" + dadosLogo.Rows[i]["fone"] + "</a>" + "&nbsp" + "<a href='https://wa.me/55" + dadosLogo.Rows[i]["ddd"] + dadosLogo.Rows[i]["fax"] + "?text=Olá, Sou Associado(a) da ASU e quero falar com vocês!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg' class='imgLogo'/>" + /*"&nbsp" +*/ dadosLogo.Rows[i]["fax"] + "</a>" + "</address>" + "</span>";
                            }
                            else
                            {
                                xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dadosLogo.Rows[i]["fone"] + "'>" + dadosLogo.Rows[i]["fone"] + "</a></address>" + "</span>";
                            }

                            //xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + dadosLogo.Rows[i]["fone"] + "</span>" + "<br>"+ "<br>";
                            xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0; word-wrap: break-word'>" + dadosLogo.Rows[i]["logradouro"] + " " + dadosLogo.Rows[i]["endereco"] + ", " + dadosLogo.Rows[i]["numero"] + "</span>" + "<br>";
                            xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + dadosLogo.Rows[i]["bairro"] + " - " + dadosLogo.Rows[i]["cidade"] + "</span>" + "<br>";
                            xRet += "</div>";
                            xRet += "</section>";
                        }
                        lblRetLogo.Text = xRet;
                    }
                    else
                    {
                        if (ObjDbLogo.MsgErro == "")
                        {
                            lblMsgErro.Text = "Logo: " + ObjDbLogo.MsgErro;
                        }
                    }

                }


                xRet += "</section>";
            }
            catch (Exception x)
            {
                xRet += "Erro:" + x.Message;
            }

            lblRetLogo.Text = xRet;
        }
        public void MostrarConvenios(object sender, EventArgs e)
        {
            string TextBusca = iBuscar.Value;
            buscarConveniosDestaque(TextBusca);
            buscarConveniosLogo(TextBusca, "'Loja', 'medic', 'parcasu'");
        }
    }
}