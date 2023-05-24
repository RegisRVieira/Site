using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Site.App_Code;

namespace Site
{
    public partial class ContNossasPessoas : System.Web.UI.Page
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mwNossasPessoas.ActiveViewIndex = 0;
            }

            this.DataBind();
        }

        public void ativarVwDiretores(object sender, EventArgs e)
        {
            mwNossasPessoas.ActiveViewIndex = 0;
        }
        public void ativarVwConselho(object sender, EventArgs e)
        {
            mwNossasPessoas.ActiveViewIndex = 1;
        }
        public void ativarVwFuncionarios(object sender, EventArgs e)
        {
            mwNossasPessoas.ActiveViewIndex = 2;
        }

        public String MontarNossasPessoas(string tipoPessoa)
        {
            BLL ObjDbASU = new BLL(conectVegas);

            string xRet = "";

            ObjDbASU.Campo = "" + " i.tipo AS tp, i.associado, a.titular, u.descricao AS unidade, i.descricao AS cargo, i.valor, i.tipo, i.dt_inicio, i.dt_fim, (SELECT descricao FROM associnf AS inf WHERE inf.associado = a.idassoc AND tipo = 'FOTSITE' LIMIT 1) AS Path  ";
            ObjDbASU.Tabela = "" + " associnf AS i ";
            ObjDbASU.Left = "" + " INNER JOIN associa AS a ON a.idassoc = i.associado " +
                                 " INNER JOIN base_assocuni AS u ON a.trab_unida = u.unidade ";
            ObjDbASU.Condicao = "" + " WHERE i.tipo = '" + tipoPessoa + "'" +
                                     " AND((CURDATE() BETWEEN i.dt_inicio AND i.dt_fim) OR(i.dt_fim IS NULL)) " +
                                     " AND i.cnscanmom IS NULL " +
                                     " ORDER BY valor ASC ";

            DataTable dados = ObjDbASU.RetCampos();

            //MessageBox.Show("SELECT " + ObjDbASU.Campo + " FROM  " + ObjDbASU.Tabela + "  " + ObjDbASU.Left + "  " + ObjDbASU.Condicao);

            int contador = dados.Rows.Count;


            if (ObjDbASU.MsgErro == "")
            {
                if (tipoPessoa == "FUNASU" || tipoPessoa == "DIRFISC")
                {
                    xRet += "<section class='BoxPessoas-Corpo'>";
                    xRet += "<section class='BoxPessoas-Dados' style='border: 2px solid #22396f; background-color: #798ebe'>";
                    if (tipoPessoa == "DIRFISC")
                    {
                        xRet += "<section class='BoxPessoas-Topo'>" + "Conselho Fiscal" + "</section>";
                    }
                    else
                    {
                        xRet += "<section class='BoxPessoas-Topo'>" + "Funcionários" + "</section>";
                    }
                    for (int i = 0; i < contador; i++)
                    {
                        xRet += "<section class='BoxPessoas-Dados-Img'>";
                        xRet += "<img style='width: 75%;' src='" + dados.Rows[i]["path"] + "' />";
                        xRet += "</section>";
                        xRet += "<section class='BoxPessoas-Dados-Text' style='margin-bottom: 2px; color: #22396f;'>";
                        xRet += "<span>" + dados.Rows[i]["cargo"] + "</span>";
                        xRet += "<span>" + dados.Rows[i]["titular"] + "</span>";
                        xRet += "<span>" + "Unidade: " + dados.Rows[i]["unidade"] + "</span>";
                        xRet += "</section>";
                        xRet += "<br>";
                    }
                    xRet += "</section>";
                    xRet += "</section>";
                    xRet += "<section class='rodapePessoas'></section>";
                    xRet += "<section class='rodapePessoasFluido'></section>";
                    //xRet += "<section style='margin-bottom: 750px; width: 100%; height: 2px;'></section>";
                    xRet += "</section>";
                }
                else
                {
                    //xRet += "<section class='BoxDiretores'>";

                    if (tipoPessoa == "DIREXE")
                    {
                        xRet += "<section class='BoxPessoas-Corpo'>";
                        xRet += "<section class='BoxPessoas-Lateral'>" + "Diretoria Executiva" + "</section>";
                        xRet += "<section class='BoxPessoas-Dados'>";
                        for (int i = 0; i < contador; i++)
                        {
                            xRet += "<section class='BoxPessoas-Dados-Img'>";
                            xRet += "<img src='" + dados.Rows[i]["path"] + "' />";
                            xRet += "</section>";
                            xRet += "<section class='BoxPessoas-Dados-Text'>";
                            xRet += "<span>" + dados.Rows[i]["cargo"] + "</span>";
                            xRet += "<span>" + dados.Rows[i]["titular"] + "</span>";
                            xRet += "<span>" + "Unidade: " + dados.Rows[i]["unidade"] + "</span>";
                            xRet += "</section>";
                            xRet += "<br>";
                        }
                        xRet += "</section>";
                        xRet += "</section>";
                    }
                    else
                    {
                        xRet += "<section style='width: 130px; height: 50px; float: left;'>" + "</section>";
                        xRet += "<section class='BoxPessoas-Dados' style='border: 2px solid #22396f; float: left; background-color: #798ebe'>";
                        xRet += "<section class='BoxPessoas-Topo'>" + "Coordenadores" + "</section>";
                        for (int i = 0; i < contador; i++)
                        {
                            xRet += "<section class='BoxPessoas-Dados-Img'>";
                            xRet += "<img src='" + dados.Rows[i]["path"] + "' />";
                            xRet += "</section>";
                            xRet += "<section class='BoxPessoas-Dados-Text' style='margin-bottom: 2px; color: #22396f;'>";
                            xRet += "<span>" + dados.Rows[i]["cargo"] + "</span>";
                            xRet += "<span>" + dados.Rows[i]["titular"] + "</span>";
                            xRet += "<span>" +  "Unidade: " + dados.Rows[i]["unidade"] + "</span>";
                            xRet += "</section>";
                            xRet += "<br>";
                        }
                        xRet += "</section>";
                        xRet += "</section>";
                        xRet += "<section class='rodapePessoas'></section>";
                        xRet += "<section class='rodapePessoasFluido'></section>";
                        //xRet += "<section style='margin-bottom: 3610px; width: 100%; height: 2px;'></section>";
                        xRet += "</section>";
                    }

                    //xRet += "<section style='margin-bottom: 200px;'></section>";
                }
            }
            else
            {
                lblMsgErro.Text = ObjDbASU.MsgErro;
            }

            return xRet;
        }
    }
}