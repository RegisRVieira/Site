using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace Site.App_Code
{
    public class BLL
    {
        public string Campo { get; set; }
        public string Tabela { get; set; }
        public string Condicao { get; set; }
        public string Valores { get; set; }
        public string Left { get; set; }
        public string Rcampos { get; set; }
        public string MsgErro { get; set; }
        public string TipoConexao { get; set; }
        public string Msg { get; set; } //Para exibir Query


        public BLL(string tipoConexao = "Site")
        {
            MsgErro = "";
            this.TipoConexao = tipoConexao;
        }

        public DataTable RetCampos()
        {
            DAL ObjCobexao = new DAL(TipoConexao);

            DataTable retorno;

            if (ObjCobexao.MsgError == "")
            {
                try
                {
                    Rcampos = (" SELECT " + Campo + " FROM " + Tabela + " " + Left + " " + Condicao);

                    retorno = ObjCobexao.RetDataTable(Rcampos);

                    if (ObjCobexao.MsgError != "")
                    {
                        MsgErro = ObjCobexao.MsgError;
                        return null;
                    }

                    return retorno;
                }
                finally
                {
                    ObjCobexao.DesconectDal();
                }
            }
            else
            {
                MsgErro = ObjCobexao.MsgError;
                return null;
            }
        }

        
        public void InsertEmLote(string tabela, string campos, string valores)
        {
            string conectSite = ConfigurationManager.AppSettings["ConectSite"];
            string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

            this.Tabela = tabela;
            this.Campo = campos;
            this.Valores = valores;

            DAL ObjConexao = new DAL(TipoConexao);

            string sql = "";



            if (TipoConexao == conectSite || TipoConexao == conectVegas)
            {
                if (ObjConexao.MsgError == "")
                {
                    try
                    {
                        sql = String.Format("INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "" + valores + "");
                        if (ObjConexao.MsgError != "")
                        {
                            MsgErro = ObjConexao.MsgError;
                        }
                        //Msg = "SQL:" + sql;
                        ObjConexao.ExecutarComandoSQL(sql); //05-08-2021: Desativar esse cara se precisar exibir a Query para análise.
                    }
                    finally
                    {
                        ObjConexao.DesconectDal();
                    }
                }
                else
                {
                    MsgErro = ObjConexao.MsgError;

                }
            }
            else
            {
                //sql = MsgErro;
            }

            Msg = "Dados Recebidos na BLL: " + sql;
        }

        public void InsertRegistro(string tabela, string campos, string valores)
        {
            string conectSite = ConfigurationManager.AppSettings["ConectSite"];
            string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

            this.Tabela = tabela;
            this.Campo = campos;
            this.Valores = valores;

            DAL ObjConexao = new DAL(TipoConexao);

            string sql = "";



            if (TipoConexao == conectSite || TipoConexao == conectVegas)
            {
                if (ObjConexao.MsgError == "")
                {
                    try
                    {
                        sql = String.Format("INSERT INTO" + tabela + "(" + campos + ")" + "VALUES " + "(" + valores + ")");
                        if (ObjConexao.MsgError != "")
                        {
                            MsgErro = ObjConexao.MsgError;
                        }
                        //Msg = "SQL:" + sql;
                        ObjConexao.ExecutarComandoSQL(sql); //05-08-2021: Desativar esse cara se precisar exibir a Query para análise.
                    }
                    finally
                    {
                        ObjConexao.DesconectDal();
                    }
                }
                else
                {
                    MsgErro = ObjConexao.MsgError;

                }
            }
            else
            {
                //sql = MsgErro;
            }

            Msg = "Dados Recebidos na BLL: " + sql;
        }

        public void EditRegistro(string tabela, string valores, string condicao)
        {
            string conectSite = ConfigurationManager.AppSettings["ConectSite"];
            string conectVegas = ConfigurationManager.AppSettings["ConectVegas"];

            this.Tabela = tabela;
            this.Valores = valores;
            this.Condicao = condicao;

            DAL ObjConexao = new DAL(TipoConexao);

            string sql = "";

            //sql = String.Format("UPDATE" + tabela + "SET" + valores + "WHERE" + condicao);
            if (TipoConexao == conectSite || TipoConexao == conectVegas)
            {
                if (ObjConexao.MsgError == "")
                {
                    if (ObjConexao.MsgError != "")
                    {
                        MsgErro = ObjConexao.MsgError;
                    }

                    sql = String.Format("UPDATE" + tabela + "SET" + valores + "WHERE" + condicao);

                    ObjConexao.ExecutarComandoSQL(sql);
                }
                else
                {
                    MsgErro = ObjConexao.MsgError;
                }
            }
            Msg = "SQL: " + sql;
        }

        //Fim Class BLL
    }
}