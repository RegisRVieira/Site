using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data;
using Site.App_Code;
using System.Configuration;



namespace Site
{
    public partial class PaginaEventos : System.Web.UI.Page
    {
        public string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        public string conectSite = ConfigurationManager.AppSettings["conectSite"];
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }

        public void testar(object sender, EventArgs e)
        {
            MessageBox.Show("Blz");
        }

        public String listaEventos()
        {
            BLL ObjDados = new BLL(conectSite);
            string xRet = "";
            string query = "";

            query = " SELECT id, titulo, introducao, cod_tipo, cod_categoria, cod_menu, dt_publini, cadmom FROM st_conteudo  " +
                    " WHERE cod_categoria = 'eve' AND cod_tipo = 'even'" +
                    " ORDER BY dt_publini DESC " +
                    " LIMIT 6 ";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();
            
            xRet += "<section class='redesSociais'>";
            xRet += "<span></span>";
            
            xRet += "<a href='https://www.facebook.com/ASUBotucatu' target='_blank'> ";
            xRet += "<section class='navHome-Box2' >";
            xRet += "</section>";
            xRet += "</a> ";
            
            xRet += "<a href='https://wa.me/551431125125?text=Olá, preciso de ajuda!' target='_blank'> ";
            xRet += "<section class='navHome-Box5' >";
            xRet += "</section>";
            xRet += "</a> ";
            
            xRet += "<a href='https://www.instagram.com/asu_unesp_botucatu/' target='_blank'> ";
            xRet += "<section class='redes-Insta' >";
            xRet += "</section>";
            xRet += "</a> ";
            
            xRet += "</section>";

            xRet += "<style>"; 
            xRet += ".listEventsSec {" +
                "    overflow: auto;" +
                "    white-space: nowrap;" +                
                "        }" +
                "    .listEventsSec a{" +
                "            display: inline-block;" +
                "            margin: 2px;" +
                "            padding-right: 5%;" +
                "            white-space: nowrap;" +
                "            min-width: 60px;" +
                "            min-height: 20px;" +
                "            border-top: 1px solid #f26907;" +
                "            border-bottom: 2px solid #f26907;" +
                "            margin-right: 15px;" +
                "            background-color: #fc9e59;" +
                "            padding-left: 10px;" +
                "            text-decoration: none;" +
                "        }";
            xRet += "</style>";
            xRet += "<section class='listEventsSec'>";            
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                xRet += "<a href='#'>";
                xRet += "<p class='list-p1'>" + dados.Rows[i]["dt_publini"].ToString().Substring(0, 2) + "</p>";                
                xRet += "<p class='list-p2'>/ " + dados.Rows[i]["dt_publini"].ToString().Substring(3, 2) + "/ " + dados.Rows[i]["dt_publini"].ToString().Substring(6, 4) + "</p>";                
                //xRet += "<p class='list-p3'>" + dados.Rows[i]["titulo"].ToString().Substring(0,10) + "</p>";  
                xRet += "<p class='list-p3'>" + dados.Rows[i]["titulo"].ToString() + "</p>";                
                xRet += "</a>";                
            }
            xRet += "</section>";


            return xRet;
        }

        public System.Windows.Forms.Button Gostar(string text)
        {
            System.Windows.Forms.Button btnGostar = new System.Windows.Forms.Button();
            btnGostar.Text = text;
            btnGostar.Size = new System.Drawing.Size(100, 20);

            return btnGostar;
        }
 
        public String gerarListaCalendario()
        {
            BLL ObjDados = new BLL(conectSite);
            string xRet = "";
            string query = "";

            query = " SELECT id, titulo, introducao, cadmom FROM st_conteudo " +
                    " ORDER BY id DESC " +
                    " LIMIT 6 ";

            ObjDados.Query = query;
            
            DataTable dados = ObjDados.RetQuery();

            string evento = dados.Rows[0]["titulo"].ToString();
            string dia = dados.Rows[0]["cadmom"].ToString().Substring(0,2);
            string mes = dados.Rows[0]["cadmom"].ToString().Substring(3,2);
            string ano = dados.Rows[0]["cadmom"].ToString().Substring(6,4);
            string Local = dados.Rows[0]["introducao"].ToString();

            xRet += "<p>" + evento + "</p><br>";
            xRet += "<p>" + dia +  "</p><br>";
            xRet += "<p>" + mes +  "</p><br>";
            xRet += "<p>" + ano +  "</p><br>";
            xRet += "<p>" + Local +  "</p><br>";


            return xRet;
        }
        public String mostrarPublicacao()
        {
            BLL ObjDados = new BLL(conectSite);

            string xRet = "";

            string query = "";

            query = " SELECT c.id, c.titulo, c.introducao, c.conteudo AS LOCAL, c.conclusao AS horario, c.cod_tipo, c.cod_categoria, c.cod_menu, c.dt_publini AS DATA, c.dt_publfim, c.cadmom, i.path_img, c.complemento AS link FROM st_conteudo  AS c " +
                    " INNER JOIN st_imagens AS i ON i.id_conteudo = c.id " +
                    " WHERE c.cod_categoria = 'eve' AND c.cod_tipo = 'even' " +
                    " ORDER BY dt_publini DESC LIMIT 6 ";

            ObjDados.Query = query;

            DataTable dados = ObjDados.RetQuery();

            string alert = "";

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                alert = "Você Gostou disso! " + dados.Rows[i]["titulo"].ToString() + ";";
                //"alert('" + gerarListaCalendario() + "');" +
            }

            xRet += "<script>" +
                "function x()" +
                "{" +
                //"alert(' Você Gostou disso! " + dados.Rows[0]["titulo"].ToString() + "');" +
                "alert('" + alert +"');" +
                //"alert('" + gerarListaCalendario() + "');" +
                "}" +
                "</script> ";

            

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                xRet += "<section class='publicacao'>";
                xRet += "<section class='pubTop'>";
                xRet += "<p class='pub-p1'>" + dados.Rows[i]["titulo"].ToString() +  "</p>";
                xRet += "<p class='pub-p2'>" + Convert.ToDateTime(dados.Rows[i]["data"].ToString()).ToString("dd-MM-yyyy") + "</p>";                
                xRet += "<p class='pub-p2'>" + dados.Rows[i]["local"].ToString() + " &nbsp<a href='" + dados.Rows[i]["link"].ToString() + "' target='_blank'>";
                xRet += "<img class='localImg' src='Img/Icon/map.svg' /></a></p>";
                xRet += "<p class='pub-p2'>" + dados.Rows[i]["horario"].ToString() + "</p>";
                xRet += "</section>";
                xRet += "<section class='pubText'>";
                xRet += "<p>" + dados.Rows[i]["introducao"].ToString() + "</p>";
                xRet += "</section>";
                xRet += "<section class='pubImg'>";
                //xRet += "<img src = 'Img/eventos/Guaruja-2023.jpg' />";
                xRet += "<img src = '" + dados.Rows[i]["path_img"].ToString() + "' />";
                xRet += "</section><br>";
                xRet += "<section class='like'>";
                //xRet += "<input id='btn' type='submit' name='btn' value='Gostei' onclick='x()' />";
                //xRet += "<input id='btnimg' src='../Img/Icon/curtir.jpg' type='image' name='btn' onclick='x()'/>";
                xRet += "</section>";
                xRet += "</section>";
            }          

            return xRet;
        }


    }//Fim   

}