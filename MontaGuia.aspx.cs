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
        }

        protected void executarSaude(object sender, ImageClickEventArgs e)
        {
            listarConvenios("MEDIC");
        }

        protected void executarConvenios(object sender, ImageClickEventArgs e)
        {
            listarConvenios("LOJA");
        }

        protected void listarConvenios(string classe)
        {
            BLL ObjGuia = new BLL(conectVegas);

            string xRet = "";

            //Query para retorno de dados do Logo
            string campos = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIALOGO' LIMIT 1) AS path, " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg "; ;
            string tabela = " coconven AS c ";
            string left = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condicao = " WHERE c.cnscanmom IS NULL AND c.classe = '" + classe + "' ORDER BY c.tipoconv, c.nome ASC ";

            ObjGuia.Campo = campos;
            ObjGuia.Tabela = tabela;
            ObjGuia.Left = left;
            ObjGuia.Condicao = condicao;

            DataTable dados = ObjGuia.RetCampos();

            //MessageBox.Show(classe);

            //MessageBox.Show("SELECT" + campos + "FROM" + tabela + "" + left + "" + condicao);
            try
            {
                if (String.IsNullOrEmpty(ObjGuia.MsgErro))
                {
                    if (classe == "MEDIC")
                    {
                        xRet += "<div style=''>";
                        xRet += "<h1 style='width: 100 %; text-align: center; background-color: #22396f; color: white; padding: 10px; font-size: 2em;'>" + "Saúde" + "</h1>";
                        xRet += "</div>";
                    }
                    else
                    {
                        xRet += "<div style=''>";
                        xRet += "<h1 style='width: 100 %; text-align: center; background-color: #22396f; color: white; padding: 10px; font-size: 2em;'>" + "Comércio" + "</h1>";
                        xRet += "</div>";
                    }
                    for (int i = 0; i < dados.Rows.Count; i++)
                    {
                        xRet += "<div class='div-guia'>";
                        xRet += "<div class='publiLogo'>";
                        if (String.IsNullOrEmpty(dados.Rows[i]["tipoimg"].ToString()))
                        {
                            //xRet += "<img src='../Img/Guia/Destaque Guia.jpg'>";
                            xRet += "<p>Oferecer Logo</p> ";
                        }
                        else
                        {
                            xRet += "<img src='../" + dados.Rows[i]["path"] + "'>";
                        }
                        xRet += "</div>";
                        xRet += "<div class='publiTexto'>";
                        xRet += "<span class='linha-convenio'>" + dados.Rows[i]["nome"] + "</span>" + "<br>";
                        xRet += "<span class='linha-dadosconvramo'>" + dados.Rows[i]["descricao"] + "</span>";

                        //xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dados.Rows[i]["fone"] + "'>" + dados.Rows[i]["fone"] + "</a></address>" + "</span>";

                        if (!String.IsNullOrEmpty(dados.Rows[i]["fax"].ToString()))
                        {
                            //xRet += "<address style='margin: 0; padding: 0;'><a href='tel:" + dadosLogo.Rows[i]["fone"] + "' > " + dadosLogo.Rows[i]["fone"] + "</a>" + "<a href='https://wa.me/55" + dadosLogo.Rows[i]["ddd"] + dadosLogo.Rows[i]["fax"] + "?text=Olá, preciso de ajuda!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg'/>" + "&nbsp" + dadosLogo.Rows[i]["fax"] + "</a></address>";
                            xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dados.Rows[i]["fone"] + "'>" + dados.Rows[i]["fone"] + "</a>" + "&nbsp" + "<a href='https://wa.me/55" + dados.Rows[i]["ddd"] + dados.Rows[i]["fax"] + "?text=Olá, Sou Associado(a) da ASU e quero falar com vocês!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg' style='width: 30px;'/>" + /*"&nbsp" +*/ dados.Rows[i]["fax"] + "</a>" + "</address>" + "</span>";
                        }
                        else
                        {
                            xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dados.Rows[i]["fone"] + "'>" + dados.Rows[i]["fone"] + "</a></address>" + "</span>";
                        }

                        xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0; word-wrap: break-word'>" + dados.Rows[i]["logradouro"] + " " + dados.Rows[i]["endereco"] + ", " + dados.Rows[i]["numero"] + "</span>" + "<br>";
                        xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + dados.Rows[i]["bairro"] + " - " + dados.Rows[i]["cidade"] + "</span>" + "<br>";
                        xRet += "</div>";
                        xRet += "</div>";
                    }
                }
                else
                {
                    xRet += "Erro: " + ObjGuia.MsgErro.ToString();
                }
            }
            catch(Exception e)
            {
                xRet += "Erro:" + e.Message;
            }

            lblResult.Text = xRet;
        }//listarConvenios

        public void buscarConvenios(object sender, EventArgs e)
        {
            BLL ObjDbDest = new BLL(conectVegas);
            BLL ObjDbLogo = new BLL(conectVegas);

            string TextBusca = "";

            TextBusca = iBuscar.Value.Replace(" ", "%").Replace("'", "%");            
                        
            string xRet = "";
            string msgErroCampoDeBusca = "";

            //Query para Retorno de dados do Destaque
            string campoDest = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIADEST' AND((CURDATE() BETWEEN img.dt_inicio AND img.dt_fim) OR(img.dt_fim IS NULL)) ORDER BY img.cnscadmom DESC LIMIT 1 ) AS path, " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg ";
            string tabDest = " coconven AS c  ";
            string leftDest = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condDest = " WHERE c.cnscanmom IS NULL AND(c.nome LIKE '%" + TextBusca + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + TextBusca + "%')) AND EXISTS(SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIADEST' AND((CURDATE() BETWEEN img.dt_inicio AND img.dt_fim) OR(img.dt_fim IS NULL)) ) ORDER BY c.nome ASC ";

            ObjDbDest.Campo = campoDest;
            ObjDbDest.Tabela = tabDest;
            ObjDbDest.Left = leftDest;
            ObjDbDest.Condicao = condDest;
            DataTable dadosDest = ObjDbDest.RetCampos();

            //Query para retorno de dados do Logo
            string campoLogo = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIALOGO' LIMIT 1) AS path, " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg "; ;
            string tabLogo = " coconven AS c ";
            string leftLogo = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condLogo = " WHERE c.cnscanmom IS NULL AND (c.nome LIKE '%" + TextBusca + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + TextBusca + "%')) ORDER BY c.nome ASC ";

            //MessageBox.Show("Destaque: " + "SELECT " + campoDest + " FROM " + tabDest + " " + leftDest + condDest);
                        
            ObjDbLogo.Campo = campoLogo;
            ObjDbLogo.Tabela = tabLogo;
            ObjDbLogo.Left = leftLogo;
            ObjDbLogo.Condicao = condLogo;
            DataTable dadosLogo = ObjDbLogo.RetCampos();

            //int nLinhasLogo = dadosLogo.Rows.Count;

            try
            {
                xRet += "<section style='width: 100%; min-height: 20px;'>";
                if (iBuscar.Value == "")
                {
                    msgErroCampoDeBusca = String.Empty;
                    msgErroCampoDeBusca = "É preciso preencher o campo de 'Busca' para procurarmos o que você deseja...";

                    xRet += "<div class='linha-convenio-p'>";
                    xRet += "<p>" + msgErroCampoDeBusca + "</p>";
                    xRet += "</div>";

                    lblResult.Text = xRet; //Exibe mensagem
                    iBuscar.Focus(); //Foca o campo Buscar para difitação do usuário
                }
                else
                {
                    if (ObjDbDest.MsgErro == "" || ObjDbLogo.MsgErro == "")
                    {


                        if (dadosDest.Rows.Count > 0 || dadosLogo.Rows.Count > 0)
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

                        if (dadosDest.Rows.Count > 0)
                        {
                            xRet += "<section class='SecGuiaDestaque'>";//Class em Form-Clean
                            for (int i = 0; i < dadosDest.Rows.Count; i++)
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

                        for (int i = 0; i < dadosLogo.Rows.Count; i++)
                        {
                            xRet += "<section class='div-guia'>";
                            xRet += "<div class='publiLogo'>";
                            if (String.IsNullOrEmpty(dadosLogo.Rows[i]["tipoimg"].ToString()))
                            {
                                //xRet += "<img src='../Img/
                                ///Destaque Guia.jpg'>";
                                xRet += "<p>Oferecer Logo</p> ";
                            }
                            else
                            {
                                xRet += "<img src='../" + dadosLogo.Rows[i]["path"] + "'>";
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
                                xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dadosLogo.Rows[i]["fone"] + "'>" + dadosLogo.Rows[i]["fone"] + "</a>" + "&nbsp" + "<a href='https://wa.me/55" + dadosLogo.Rows[i]["ddd"] + dadosLogo.Rows[i]["fax"] + "?text=Olá, Sou Associado(a) da ASU e quero falar com vocês!' target='_blank' > " + "<img src='../Img/Icon/whatsapp2.svg' style='width: 30px;'/>" + /*"&nbsp" +*/ dadosLogo.Rows[i]["fax"] + "</a>" + "</address>" + "</span>";
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
                        lblResult.Text = xRet;
                    }
                    else
                    {
                        if (ObjDbDest.MsgErro == "")
                        {
                            lblMsgErro.Text = "Destaque: " + ObjDbDest.MsgErro;
                        }
                        else
                        {
                            lblMsgErro.Text = "Logo: " + ObjDbLogo.MsgErro;
                        }
                    }

                }


                xRet += "</section>";
            }
            catch (Exception x) {
                xRet += "Erro:" + x.Message;
            }

            lblResult.Text = xRet;
        }


    }
}