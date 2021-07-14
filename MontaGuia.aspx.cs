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

        public void buscarConvenios(object sender, EventArgs e)
        {
            BLL ObjDbDest = new BLL(conectVegas);
            BLL ObjDbLogo = new BLL(conectVegas);

            string TextBusca = "";
            TextBusca = iBuscar.Value.Replace(" ", "%");

            string xErro = "";
            string xRet = "";

            /* Variáveis para Geração dos Dados da pesquisa de Convênios */
            /*
            string campos = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT descricao FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS img, (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg ";
            string tabela = " coconven AS c  ";
            string left = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condicao = " WHERE c.cnscanmom IS NULL AND (c.nome LIKE '%" + iBuscar.Value + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + iBuscar.Value + "%')) ";
            */
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

            int nLinhasDest = dadosDest.Rows.Count;

            //Query para retorno de dados do Logo
            string campoLogo = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIALOGO' LIMIT 1) AS path, " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg "; ;
            string tabLogo = " coconven AS c ";
            string leftLogo = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condLogo = " WHERE c.cnscanmom IS NULL AND (c.nome LIKE '%" + TextBusca + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + TextBusca + "%')) ORDER BY c.nome ASC ";

            ObjDbLogo.Campo = campoLogo;
            ObjDbLogo.Tabela = tabLogo;
            ObjDbLogo.Left = leftLogo;
            ObjDbLogo.Condicao = condLogo;
            DataTable dadosLogo = ObjDbLogo.RetCampos();

            int nLinhasLogo = dadosLogo.Rows.Count;
                                    

            if (iBuscar.Value == "")
            {
                string msgErroCampoVazio = "É preciso preencher o campo 'Buscar' para procurarmos o que você deseja";
                

                xErro += "<div class='linha-convenio-p'>";  
                xErro += "<p>" + msgErroCampoVazio + "</p>";
                xErro += "</div>";

                lblResult.Text = xErro; //Exibe mensagem
                iBuscar.Focus(); //Foca o campo Buscar para difitação do usuário
            }
            else
            {
                if (ObjDbDest.MsgErro == "" || ObjDbLogo.MsgErro == "")
                {
                    

                    if (nLinhasDest > 0 || nLinhasLogo > 0)
                    {
                        //MessageBox.Show("Teve dados...");
                        lblMsgErro.Text = String.Empty;
                    }
                    else
                    {
                        string xMsg = "";
                        xMsg += "<div class='linha-convenio-p'>";
                        xMsg += "<p>" + " Não há dados para Retorno, faça uma nova Pesquisa " + "</p>";
                        xMsg += "</div>";

                        lblMsgErro.Text = xMsg;
                    }

                    if (nLinhasDest > 0)
                    {
                        xRet += "<section class='publiPrincipal' style=''>";
                        xRet += "<section class='margem-guia-principal'>";
                        xRet += "</section>";
                        xRet += "<section class='div-guia-principal'>";
                        xRet += "<div class='guiaImg'>";
                        for (int i = 0; i < nLinhasDest; i++)
                        {
                            xRet += "<img class='Slider' src='../" + dadosDest.Rows[i]["path"] + "'>";
                        }
                        xRet += "</div>";
                        xRet += "</section>";
                        xRet += "</section>";
                    }
                    else
                    {
                        xRet += "<section class='publiPrincipal' style=''>";
                        xRet += "<section class='margem-guia-principal'>";
                        xRet += "</section>";
                        xRet += "<section class='div-guia-principal'>";
                        xRet += "<div class='guiaImg'>";
                        xRet += "<img class='Slider' src='../img/guia/Banner Publicidade Guia.jpg '>";                        
                        xRet += "</div>";
                        xRet += "</section>";
                        xRet += "</section>";
                    }

                    for (int i = 0; i < nLinhasLogo; i++)
                    {
                        xRet += "<div class='div-guia'>";
                        xRet += "<div class='publiLogo'>";
                        if (String.IsNullOrEmpty(dadosLogo.Rows[i]["tipoimg"].ToString()))
                        {
                            //xRet += "<img src='../Img/Guia/Destaque Guia.jpg'>";
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
                        xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + "<address style='margin: 0; padding: 0;'><a style='margin: 0; padding: 0;' href='tel:" + dadosLogo.Rows[i]["fone"] + "'>" + dadosLogo.Rows[i]["fone"] + "</a></address>" + "</span>";
                        //xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + dadosLogo.Rows[i]["fone"] + "</span>" + "<br>"+ "<br>";
                        xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0; word-wrap: break-word'>" + dadosLogo.Rows[i]["logradouro"] + " " + dadosLogo.Rows[i]["endereco"] + ", " + dadosLogo.Rows[i]["numero"] + "</span>" + "<br>";
                        xRet += "<span class='linha-dadosconv' style='margin: 8px 0 8px 0;'>" + dadosLogo.Rows[i]["bairro"] + " - " + dadosLogo.Rows[i]["cidade"] + "</span>" + "<br>";
                        xRet += "</div>";
                        xRet += "</div>";
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

        }
    }
}