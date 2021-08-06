using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.App_Code;
using System.Configuration;
using System.Windows.Forms;

namespace Site.Adm
{
    public partial class InsertRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cadastrarEmpresa(object sender, EventArgs e)
        {
            string conectSite = ConfigurationManager.AppSettings["ConectSite"];
            BLL ObjEmpresa = new BLL(conectSite);

            string tabela = " " + "st_empresa";
            string campos = "nome, cadusu, cadmom";                       
            string valores = String.Format("'" + iNome.Value + "'" + ", " + "'"+ iUsuario.Value + "'" + ", " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                        
            ObjEmpresa.Tabela = tabela;
            ObjEmpresa.Campo = campos;
            ObjEmpresa.Valores = valores;

            string sql = tabela + campos + valores;

            ObjEmpresa.InsertRegistro(tabela, campos, valores);

            MessageBox.Show(ObjEmpresa.Msg);

           // MessageBox.Show(ObjEmpresa.Msg2 + "\n" + ObjEmpresa.Msg); 

            
            MessageBox.Show("Cadastro realizado com Sucesso!!!");
            

            iNome.Value = String.Empty;
            iNome.Focus();
        }
    }
}