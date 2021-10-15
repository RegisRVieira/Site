using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Pessoal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ativarViews(int Ativa)
        {
            mwUtilidades.ActiveViewIndex = Ativa;   
        }
        public void ativarListaMusica(object sender, EventArgs e)
        {
            ativarViews(0);
        }
        public void ativarMusicaRecebida(object sender, EventArgs e)
        {
            ativarViews(1);
        }

        public void ativarListaInteresses(object sender, EventArgs e)
        {
            ativarViews(2);
        }
        public void ativarMma(object sender, EventArgs e)
        {
            ativarViews(3);
        }
        public void ativarDicas(object sender, EventArgs e)
        {
            ativarViews(4);
        }
        public void ativarMarcenaria(object sender, EventArgs e)
        {
            ativarViews(5);
        }
        public void ativarGsx(object sender, EventArgs e)
        {
            ativarViews(6);
        }
        public void ativarEstudar(object sender, EventArgs e)
        {
            ativarViews(7);
        }
        public void ativarZenvia(object sender, EventArgs e)
        {
            ativarViews(8);
        }
        public void ativarASU(object sender, EventArgs e)
        {
            ativarViews(9);
        }
        public void ativarFulCycle(object sender, EventArgs e)
        {
            ativarViews(10);
        }

        //FIM
    }
}