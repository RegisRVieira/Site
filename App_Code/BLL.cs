using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

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




    }
}