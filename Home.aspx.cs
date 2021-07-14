using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using Site.App_Code;
using System.Configuration;
using System.Windows.Forms;

namespace Site
{
    public partial class Home : System.Web.UI.Page
    {
        public string conectSite = ConfigurationManager.AppSettings["ConectSite"];
        public string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                usuarioLogado();
            }
            this.DataBind();
        }

        public void usuarioLogado()
        {
            string usuario = "";

            if (Session["VoceOnline"] != null)
            {
                usuario = Session["LoginUsuario"].ToString();

                string primeiroNome = usuario.Split(' ').FirstOrDefault();
                string primeiraLetra = usuario.Split(' ').FirstOrDefault();
                int tNome = primeiroNome.Length;

                //xRet += "" + primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome-1)).ToLower();
                lblUsuLogado.Text = primeiraLetra.Substring(0, 1) + primeiroNome.Substring(1, (tNome - 1)).ToLower();

            }
            else
            {
                lblUsuLogado.Text = "Você OnLine";
            }
                
            
            
            //return xRet;
        }

        public String montarMateriasHome()
        {
            BLL ObjConexao = new BLL(conectSite);
            BLL ObjConectVegas = new BLL(conectVegas);

            string xRet = "";
            string xImg = "";
            string IDMat = "";
            string campos = " " + " c.id, c.titulo, c.conteudo, c.introducao, c.fonte, c.autor, dt_publini, c.cadusu, c_cat.descricao AS Categoria, d.descricao AS Destaque, t.descricao AS Tipo, i.cod_destaque AS img_destaque, i.codtipo AS TipoImg, i.path_img AS PathImg ";
            string tabela = " " + " st_conteudo AS c ";
            string left = " " + " INNER JOIN st_categoria AS c_cat ON c.cod_categoria = c_cat.cod " +
                                " INNER JOIN st_menu AS d ON c.cod_menu = d.cod " +
                                " INNER JOIN st_tipo AS t ON c.cod_tipo = t.cod " +
                                " LEFT JOIN st_imagens as i ON c.id = i.id_conteudo ";
            string condicao = " " + " WHERE c.cod_tipo = '" + "MAT" + "' AND i.cod_destaque = '" + "MAT" + "' AND i.codtipo = '" + "CHA" + "' ORDER BY c.id DESC LIMIT 2 ";

            if (ObjConexao.MsgErro == "" || ObjConectVegas.MsgErro == "")
            {
                ObjConexao.Campo = campos;
                ObjConexao.Tabela = tabela;
                ObjConexao.Left = left;
                ObjConexao.Condicao = condicao;

                DataTable dados = ObjConexao.RetCampos();

                //MessageBox.Show(campos + tabela + left + condicao);                    


                int contador = dados.Rows.Count;

                int contMat = 0;

                for (int i = 0; i < contador; i++)
                {
                    xImg += dados.Rows[i]["tipoImg"].ToString();
                    if (xImg == "CHA")
                    {
                        xImg += dados.Rows[i]["tipoImg"];
                        contMat++;
                    }
                }

                for (int i = 0; i < 2; i++) //Para pegar apenas xImg = CHA
                {
                    if (xImg == "MAT")
                    {
                        //xRet += "<div style='display: inline-block; background-color: yellow;'>" + "<img style='width: 627px; height: 146px' src =" + "'../.." + Linha["PathImg"] + "'" + "/>" + "</div>";
                        xRet += "</a>";
                    }
                    else
                    {
                        IDMat = dados.Rows[i]["id"].ToString();

                        xRet += "<div class='HomeMateria'> ";                        
                        //xRet += "<img src='../Img/Foto - DAP.jpg' />";             //Capturar foto do Banco de Dados - 08-04-2021 23:20                                                                                                   
                        xRet += "<img src='"+ dados.Rows[i]["PathImg"] +"' />"; 
                        xRet += "<h1 class='HomeMateriaTitulo'>" + dados.Rows[i]["titulo"] + "</h1>";
                        xRet += "<p class='HomeMateriaCategoria'>" + dados.Rows[i]["categoria"] + "</p>";
                        xRet += "<div class='HomeMateriaTexto'>" + dados.Rows[i]["introducao"] + "</div>";
                        xRet += "<p class='HomeMateriaData'>" + "12 de Março de 2021" + "</p>";
                        xRet += "<div class='HomeMateriaMais'><a href='ContMaterias.aspx?IDContMat=" + IDMat + "'><p>" + /*dados.Rows[i]["PathImg"] +*/  "Leia Mais..." + "</p></a></div>";
                        //MessageBox.Show(dados.Rows[i]["id"].ToString());                    
                    }
                    xRet += "</div>";
                }
            }
            else
            {
                //string MsgErro = ObjConexao.MsgErro;
                xRet = "Erro: " + ObjConexao.MsgErro;
                return null;
            }

            return xRet;
        }


        public void teste()
        {
            BLL ObjConexao = new BLL(conectSite);

            string xRet = "";

            ObjConexao.Campo = " * ";
            ObjConexao.Tabela = " st_usuario ";

            DataTable dados = ObjConexao.RetCampos();

            if (ObjConexao.MsgErro != "")
            {
                MessageBox.Show("Deu erro");
            }
            else
            {
                xRet += "Metodo";
            }

        }

        public String montarBoxServicos()
        {

            string xRet = "";

            for (int i = 0; i < 1; i++)
            {
                xRet += "<div class='BoxS-Conteudo'>";
                xRet += "<div class='BoxS-Conteudo-Tit'>";
                xRet += "<p>" + "Associados" + "</p>";
                xRet += "</div>";
                xRet += "<div class='BoxS-Img'>";
                xRet += "<img src='Img/Associados2.jpg' />";
                xRet += "</div>";
                xRet += "<div class='botaoSaibaMais2'>";
                xRet += "<a href='ContAssoc.aspx?pAssoc=65' >" + "Saiba Mais" + "</a>";
                xRet += "</div>";
                xRet += "</div>";
            }

            return xRet;
        }

        public String montarBox33()
        {
            BLL ObjConexao = new BLL(conectVegas);

            //Query para Retorno de dados do Destaque
            string campos = " c.idconven, c.nome, c.tipoconv, c.ddd, c.fone, c.celular, c.fax, c.logradouro, c.endereco, c.numero, c.bairro, c.cidade, t.descricao, (SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIADEST' AND((CURDATE() BETWEEN img.dt_inicio AND img.dt_fim) OR(img.dt_fim IS NULL)) ORDER BY img.cnscadmom DESC LIMIT 1 ) AS path, " +
                               " (SELECT tipo FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS tipoimg, (SELECT valor FROM coinfo AS info WHERE c.idconven = info.convenio AND tipo IN('SGUIADEST', 'SGUIALOGO') LIMIT 1) AS ordemimg ";
            string tabela = " coconven AS c  ";
            string left = " INNER JOIN base_cotipo AS t ON c.tipoconv = t.codtipo ";
            string condicao = " WHERE c.cnscanmom IS NULL AND(c.nome LIKE '%" + "Manzi" + "%' OR EXISTS(SELECT tp.codtipo FROM base_cotipo AS tp WHERE tp.codtipo = c.tipoconv AND tp.descricao LIKE '%" + "Manzi" + "%')) AND EXISTS(SELECT img.descricao FROM coinfo AS img WHERE img.convenio = c.idconven AND img.tipo = 'SGUIADEST' AND((CURDATE() BETWEEN img.dt_inicio AND img.dt_fim) OR(img.dt_fim IS NULL)) ) ORDER BY c.nome ASC ";

            ObjConexao.Campo = campos;
            ObjConexao.Tabela = tabela;
            ObjConexao.Left = left;
            ObjConexao.Condicao = condicao;

            DataTable dados = ObjConexao.RetCampos();

            int nLinhasDest = dados.Rows.Count;

            string xRet = "";

            xRet += "<div class='tituloBox-33' >" + "O que temos para Você" + "</div>";
            xRet += "<section class='s1'>";
            xRet += "<section class='Box33'>";
            /*
            xRet += "<img class='BoxS1' src='../../Img/Manzini-Lj1.jpg' />";
            xRet += "<img class='BoxS1' src='../../Img/Jau-Lj11.jpg' />";
            xRet += "<img class='BoxS1' src='../../Img/Confiança.jpg' />";
            */
            for (int i = 0; i < nLinhasDest; i++)
            {
                xRet += "<img class='BoxS1' src='" + dados.Rows[i]["path"] + "'>";
            }
            xRet += "<div class='s-titulo'>";
            xRet += "<p>Promoção Vale Compras</p>";
            xRet += "</div>";
            xRet += "<div id = 'black-Box-Text' class='botaoSaibaMais'> ";
            xRet += "<a href = '#' > Saiba Mais</a>";
            xRet += "</div>";
            xRet += "</section>";
            xRet += "</section>";


            return xRet;
        }


    }
}