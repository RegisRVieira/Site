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
        public string limitBox100 = ConfigurationManager.AppSettings["limiteBox100"];

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

                int contador = 0;
                int contMat = 0;

                if (dados.Rows.Count > 0)
                {
                    contador = dados.Rows.Count;

                    for (int i = 0; i < contador; i++)
                    {
                        xImg += dados.Rows[i]["tipoImg"].ToString();
                        if (xImg == "CHA")
                        {
                            xImg += dados.Rows[i]["tipoImg"];
                            contMat++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Problema de conexão com o provedor do Site, verifique!");
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

                        string dtpublica = dados.Rows[i]["dt_publini"].ToString();

                        xRet += "<div class='HomeMateria'> ";
                        //xRet += "<img src='../Img/Foto - DAP.jpg' />";             //Capturar foto do Banco de Dados - 08-04-2021 23:20                                                                                                   
                        xRet += "<img src='" + dados.Rows[i]["PathImg"] + "' />";
                        xRet += "<h1 class='HomeMateriaTitulo'>" + dados.Rows[i]["titulo"] + "</h1>";
                        xRet += "<p class='HomeMateriaCategoria'>" + dados.Rows[i]["categoria"] + "</p>";
                        xRet += "<div class='HomeMateriaTexto'>" + dados.Rows[i]["introducao"] + "</div>";
                        //xRet += "<p class='HomeMateriaData'>" + "12 de Março de 2021" + "</p>";
                        xRet += "<p class='HomeMateriaData'>" + Convert.ToDateTime(dtpublica).ToString("dd-MM-yyyy") + "</p>";
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

        public String montarSlider()
        {
            BLL ObjDados = new BLL(conectSite);
            string xRet = "";

            string campos = " i.id, i.codtipo AS TipoImg, c.Cod_tipo AS TipoCont, i.cod_destaque, i.titulo, i.path_img, i.fonte, i.autor, i.hint, c.dt_publini, c.dt_publfim, c.id, c.cod_menu,  c.cod_categoria, c.titulo ";
            string tabela = " st_imagens AS i  ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo  ";
            string condicao = " WHERE i.codtipo IN ('SLI' ) AND c.Cod_tipo = 'PROP' ORDER BY c.id DESC LIMIT 3 ";

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            if (ObjDados.MsgErro == "")
            {


                xRet += "<div class='BoxSlider-Propaganda'>";
                xRet += "<section class='slider'>";
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    xRet += "<img class='STop' src='" + dados.Rows[i]["path_img"] + "' />";
                }
                xRet += "</section>";
                xRet += "</div>";
            }
            else
            {
                xRet += "<label>" + ObjDados.MsgErro + "</label>";
            }

            return xRet;
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
            BLL ObjDados = new BLL(conectSite);

            string campos = " i.id, i.codtipo AS TipoImg, c.Cod_tipo AS TipoCont, i.cod_destaque, i.titulo, i.path_img, i.fonte, i.autor, i.hint, c.dt_publini, c.dt_publfim, c.id, c.cod_menu,  c.cod_categoria, c.titulo ";
            string tabela = " st_imagens AS i ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo ";
            string condicao = " WHERE i.codtipo IN ('B33S1', 'B33S2', 'B33S3' ) AND c.Cod_tipo = 'BOX33' ORDER BY c.id DESC "; //É o seguinte, não tem as imagens na pasta. Então dá imagem "vazia"... Só carregar as imagens e Pronto! 14-09-2021

            ObjDados.Campo = campos;
            ObjDados.Tabela = tabela;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;
            DataTable dados = ObjDados.RetCampos();

            string xRet = "";

            //Sessão 1
            xRet += "<div class='tituloBox-33' >" + "O que temos para Você" + "</div>";
            xRet += "<section class='s1'>";
            xRet += "<section class='Box33'>";
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                //Criar método para checar se o arquivo existe             
                if (dados.Rows[i]["TipoImg"].ToString() == "B33S1")
                {
                    xRet += "<img class='BoxS1' src='" + dados.Rows[i]["path_img"] + "'>";
                }
            }
            xRet += "<div class='s-titulo'>";
            xRet += "<p>Promoção Vale Compras</p>";
            xRet += "</div>";
            xRet += "<div id = 'black-Box-Text' class='botaoSaibaMais'> ";
            xRet += "<a href = '#' > Saiba Mais</a>";
            xRet += "</div>";
            xRet += "</section>";
            xRet += "</section>";


            //Sessão 2
            xRet += "<section class='s2'>";
            xRet += "<section class='Box33'>";
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                if (dados.Rows[i]["TipoImg"].ToString() == "B33S2")
                {
                    xRet += "<img class='BoxS2' src='" + dados.Rows[i]["path_img"] + "'>";
                }
            }
            xRet += "<div class='s-titulo'>";
            xRet += "<p>Promoção Tanque Cheio</p>";
            xRet += "</div>";
            xRet += "<div id = '' class='botaoSaibaMais'>";
            xRet += "<a href='ContMaterias.aspx?IDContMat=70' > Saiba Mais</a>";
            xRet += "</div>";
            xRet += "</section>";
            xRet += "</section>";

            //Sessão 3
            xRet += "<section class='s3'>";
            xRet += "<section class='Box33'>";
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                if (dados.Rows[i]["TipoImg"].ToString() == "B33S3")
                {
                    xRet += "<img class='BoxS3' src='" + dados.Rows[i]["path_img"] + "'>";
                }
            }
            xRet += "<div class='s-titulo'>";
            xRet += "<p>Próximos Eventos</p>";
            xRet += "</div>";
            xRet += "<div id = '' class='botaoSaibaMais'>";
            xRet += "<a href = 'ContASU.aspx' > Saiba Mais</a>";
            xRet += "</div>";
            xRet += "</section>";
            xRet += "</section>";


            return xRet;
        }

        public String montarBox100()
        {
            BLL ObjDados = new BLL(conectSite);

            string campos = " i.id, i.codtipo AS TipoImg, c.Cod_tipo AS TipoCont, i.cod_destaque, i.titulo, i.path_img, i.fonte, i.autor, i.hint, c.dt_publini, c.dt_publfim, c.id, c.cod_menu,  c.cod_categoria, c.titulo ";
            string tabela = " st_imagens AS i ";
            string left = " INNER JOIN st_conteudo AS c ON c.id = i.id_conteudo ";
            string condicao = " WHERE i.codtipo IN ('B100' ) AND c.Cod_tipo = 'BOX100' ORDER BY c.id DESC LIMIT " + limitBox100 + " ";

            ObjDados.Tabela = tabela;
            ObjDados.Campo = campos;
            ObjDados.Left = left;
            ObjDados.Condicao = condicao;

            DataTable dados = ObjDados.RetCampos();

            //MessageBox.Show(campos +"\n" + tabela+"\n" + left + "\n" + condicao);

            string xRet = "";

            xRet += "<p class='tituloBox-1'>" + "Nossos Momentos" + "</p>";
            xRet += "<div class='BoxImg100'>";
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                xRet += "<img class='Box100' src='" + dados.Rows[i]["path_img"] + "'/>";
            }
            xRet += "</div>";
            xRet += "</div>";


            return xRet;
        }


    }//Fim
}