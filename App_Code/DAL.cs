using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace Site.App_Code
{
    public class DAL
    {
        /* - - - - ASU - - - -*/
        public string server = "";
        public string database = "";
        public string port = "";
        public string user = "";
        //public string password = "i)&@$)6";
        public string password = "";
        public string connectionString = "";
        public string MsgError = "";
        public MySqlConnection connection;        


        public DAL(string tipoConexao)
        {       
            if (tipoConexao == ConfigurationManager.AppSettings["ConectSite"])
            {
                server = ConfigurationManager.AppSettings["ServerASU"];
                database = ConfigurationManager.AppSettings["DBASU"];
                port = ConfigurationManager.AppSettings["PortASU"];
                user = ConfigurationManager.AppSettings["UserASU"];
                password = "i072406";
                //password = "i)&@$)6";
                connectionString = "Server={0}; Port={1};Database={2};Uid={3};Pwd={4};";
                MsgError = "";
            }
            else if(tipoConexao == ConfigurationManager.AppSettings["ConectVegas"])
            {
                server = ConfigurationManager.AppSettings["ServerVegas"];
                database = ConfigurationManager.AppSettings["DBVegas"];
                port = ConfigurationManager.AppSettings["PortVegas"];
                user = ConfigurationManager.AppSettings["UserVegas"];
                password = "Cns@Vegas#2020";
                connectionString = "Server={0}; Port={1};Database={2};Uid={3};Pwd={4};";
                MsgError = "";
            }

            //Estabelece a conexão com as variáveis criadas para conexão no ato da criação da estância DAL

            //Formatar a variável connectionString para receber os argumentos de conexão
            connectionString = String.Format(connectionString, server, port, database, user, password);
            //Criar Objeto para manter conexão ativa com o BD
            connection = new MySqlConnection(connectionString);
            try
            {
                //Abre a conexão
                connection.Open();
            }
            catch (Exception e)
            {
                MsgError = "Erro original: " + e.Message;
            }

        }
        public void DesconectDal()
        {
            connection.Close();
        }

        //Método para retornar dados, retorna uma tabela de dados.
        public DataTable RetDataTable(string sql)
        {
            //Criar objeto para receber os dados
            DataTable dataTable = new DataTable();
            try
            {
                //Criar Objeto que roda o comando no BD para estabelecer a conexão
                MySqlCommand command = new MySqlCommand(sql, connection);
                //Criar um Objeto para transformar o tipo de dados do MySql para que o Dot.Net compreenda
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                //Preenche o Objeto da, para retornar os dados
                da.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                MsgError = "Erro original: " + e.Message;
                return null;
            }
        }

        // Procedimento para capturar os dados do BD
        public void ExecutarComandoSQL(string sql)
        {
            /*
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            */

            
            // Este cara não funcionou, pois ele não grava e não exibe erro. Sem o Try funciona. Analisar este try. 02-09-2021
            try {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MsgError = "Erro Original: " + e.Message;
            }            
        }
        public void ExecutarComandoSQLASU(string Rcampos)
        {
            MySqlCommand command = new MySqlCommand(Rcampos, connection);
            command.ExecuteNonQuery();
        }
    }
}