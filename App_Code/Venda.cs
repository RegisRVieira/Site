using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web.Configuration;
using System.IO;


namespace Site.App_Code
{
    public class Venda
    {
        string conectSite = ConfigurationManager.AppSettings["conectSite"];
        string conectVegas = ConfigurationManager.AppSettings["conectVegas"];

        public string VendaArq_envio { get; set; }
        public int nVezes { get; set; }
        public string cArquivo_mostra { get; set; }
        public double widthExtrato { get; set; }
        public string idAssoc { get; set; } //Armazenar ID do Associado no login
        public string idConv { get; set; } //Armazenar ID do Convênio no login

        public string Ncartao { get; set; }
        public string ValorVenda { get; set; }
        public string SenhaVenda { get; set; }
        public string ParcelasVenda { get; set; }

        public string GeraDigMod11(long intNumero)
        {
            string cDigito = "";

            cDigito = "" + intNumero + DigitoModulo11(intNumero);
            cDigito = "" + cDigito + DigitoModulo11(Convert.ToUInt32(cDigito));

            return cDigito;
        }

        public int DigitoModulo11(long intNumero)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            string strText = intNumero.ToString();

            if (strText.Length > 16)

                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;

            int intIdx = 0;

            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];

                intIdx++;
            }

            int intResto = (intSoma * 10) % 11;

            int intDigito = intResto;

            if (intDigito >= 10)

                intDigito = 0;

            return intDigito;
        }
        /* - - - Processo de Venda - - - */

        protected string Colocazero(string Campo, int Carac)
        {
            if (Campo.Length < Carac)
            {
                do
                {
                    Campo = "0" + Campo;

                } while (Campo.Length < Carac);
            }

            return Campo;
        }

        public string CortaZeros(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if ("" + cValor.Substring(i, 1) == "0")
                {
                    contador++;
                }
                else
                    break;
            }

            return cValor.Substring(contador, cValor.Length - contador);
        }

        public string ColocaVirgula(string cValor)
        {
            int contador = 0;

            for (int i = 0; i < cValor.Length; i++)
            {
                if (contador == (cValor.Length - 2))
                {
                    cValor = cValor.Substring(0, contador) + "," + cValor.Substring(contador, 2);
                }

                contador++;
            }

            return cValor;
        }

        public void realizaVenda(string idConv, string nCartao, string valor, string parcela, string senha)
        {

            //############################
            //string IdConv = Session["CodAcesso"].ToString();  Não é aceito sessão em Classe
            
            string IdConv =  this.idConv;

            if (String.IsNullOrEmpty(Ncartao) || String.IsNullOrEmpty(ValorVenda) || String.IsNullOrEmpty(SenhaVenda))
            {
                //lblMsgVenda.Text = "Preencha todos os campos";

                // ########################## Retornar dados
            }
            else
            {
                //caminho físico
                string pathDocumento = "" + WebConfigurationManager.AppSettings["CaminhoVendaENV"];

                DirectoryInfo dir = new DirectoryInfo(pathDocumento);

                //Cria arquivo         
                //string cVersao = "00001.03";
                string cVersao = "00001.04";
                //string cConvenio = "" + 63193;
                string cConvenio = "" + IdConv;
                //string cConvenio = "" + idCobv + "00"; //Não funciona com DV 00
                string cCartao = "" + Ncartao;
                string cValor = "" + ValorVenda.Replace(",", "").Replace(".", "");
                string cParcelas = "" + ParcelasVenda;
                string cCod_retorno = "000";
                string cCod_autoriza = "000000000";
                string cChave_usuario = "000000000";
                string cNome_usuario = "                                        ";
                string cObserva = "VENDA PELO SITE               ";
                string cCpf = "              ";
                string cSaldo = "00000000000000";
                string cSenha = "" + SenhaVenda.Replace("'", "");


                cConvenio = Colocazero(cConvenio, 9);
                cValor = Colocazero(cValor, 12);

                string cArq_cont = cVersao + cConvenio + cCartao + cValor + cParcelas + cCod_retorno + cCod_autoriza + cChave_usuario + cNome_usuario + cObserva + cCpf + cSaldo + cSenha;

                //titulo do arquivo
                //string cArq_tit = "" + Session["conveniado"] + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
                
                //Criando o Arquivo  
                string cArq_tit = "" + IdConv + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh.mm.ss") + ".ENV";// + DateTime.Now;
                                                                                                            //try
                                                                                                            //{
                GravaArquivo(System.IO.Path.Combine(pathDocumento, cArq_tit), cArq_cont);
                //}
                //catch
                //{
                //}

                //Session["vendaArq_envio"] = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                VendaArq_envio = "" + System.IO.Path.Combine(pathDocumento, cArq_tit);

                //MessageBox.Show("Venda enviada para Processamento, aguarde");                              

                //MessageBox.Show(cArq_cont);
                //MessageBox.Show(idCobv);

                Processar_retorno();
            }

        }

        public string GravaArquivo(string cPathArquivo, string texto)
        {

            string cAux = "";
            StreamWriter oFileStream = new System.IO.StreamWriter(cPathArquivo);
            try
            {
                oFileStream.Write(texto);
            }
            catch (Exception except)
            {
                return "Erro na gravação do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oFileStream.Close();
                //oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public string LeArquivo(string cPathArquivo)
        {
            string cAux = "";
            FileStream oFileStream = new FileStream(cPathArquivo, FileMode.Open);
            StreamReader oStreamReader = new StreamReader(oFileStream);
            try
            {
                cAux = oStreamReader.ReadToEnd();
            }
            catch (Exception except)
            {
                return "Erro na leitura do arquivo!<br><br>" + except.Message.ToString();
            }
            finally
            {
                oStreamReader.Dispose();
                oFileStream.Dispose();
            }
            return cAux;
        }

        public void Processar_retorno()
        {
            try
            {
                string cArquivo = LeArquivo("" + VendaArq_envio.ToString().Replace("ENV", "RET"));
                cArquivo_mostra = "";

                string cCod_transa = "" + cArquivo.Substring(1, 3);//cod. Transação
                string cVersao = "" + cArquivo.Substring(3, 5); //Versão do Protocolo de Venda
                string cCodConv = "" + cArquivo.Substring(8, 9); //cod. do Convênio
                string cCodCarta = "" + cArquivo.Substring(17, 9); // Cod. do Cartão
                string cValor = "" + cArquivo.Substring(26, 12); //Valor da Venda
                string cParcela = "" + cArquivo.Substring(38, 2); //Quantidade de Parcelas
                string cCodRet = "" + cArquivo.Substring(40, 3);//cod. Retorno
                string cAutoriza = "" + cArquivo.Substring(43, 9); // Autorização da Compra
                string cChaveUsuar = "" + cArquivo.Substring(52, 9); // Chave do Usuário
                string cNome = "" + cArquivo.Substring(61, 40); //Nome do Conveniado
                string cObserva = "" + cArquivo.Substring(101, 30); //Observação sobre a Vendfa
                string cCpfCnpj = "" + cArquivo.Substring(131, 14); //CPF ou CNPJ
                string cSaldo = "" + cArquivo.Substring(145, 14); //Saldo do Associado
                string cSenha = "" + cArquivo.Substring(160, 20); //Senha 
                string cRet_msg = "";

                switch (cCodRet)
                {
                    case "000":
                        cRet_msg = "Transação realizada com sucesso";
                        break;
                    case "110":
                        cRet_msg = "Tempo esgotado para retorno";
                        break;
                    case "120":
                        cRet_msg = "Código de convênio incompatível com a instalação";
                        break;
                    case "130":
                        cRet_msg = "Conexão com problemas";
                        break;
                    case "210":
                        cRet_msg = "Código do cartão inconsistente";
                        break;
                    case "220":
                        cRet_msg = "Código da transação não permitido";
                        break;
                    case "310":
                        cRet_msg = "Cartão ou senha incorreta";
                        break;
                    case "320":
                        cRet_msg = "Cartão vencido";
                        break;
                    case "330":
                        cRet_msg = "Cartão não liberado";
                        break;
                    case "340":
                        cRet_msg = "Cartão cancelado";
                        break;
                    case "350":
                        cRet_msg = "Cartão não autorizado para uso no convênio";
                        break;
                    case "360":
                        cRet_msg = "Tipo de cartão não aceita parcelamento";
                        break;
                    case "370":
                        cRet_msg = "Usuário do cartão inválido";
                        break;
                    case "410":
                        cRet_msg = "Convênio não confere com o registrado na configuração do sistema";
                        break;
                    case "420":
                        cRet_msg = "Convênio não registrado";
                        break;
                    case "430":
                        cRet_msg = "Convênio não liberado";
                        break;
                    case "440":
                        cRet_msg = "Convênio cancelado";
                        break;
                    case "510":
                        cRet_msg = "Crédito insuficiente";
                        break;
                    case "610":
                        cRet_msg = "Parcelamento superior ao permitido para o convênio";
                        break;
                    case "620":
                        cRet_msg = "Parcelamento superior ao permitido para o associado";
                        break;
                    case "630":
                        cRet_msg = "Valor da compra zerado";
                        break;
                    case "710":
                        cRet_msg = "Autorização não encontrada para cancelamento";
                        break;
                    case "720":
                        cRet_msg = "Autorização já cancelada";
                        break;
                    case "730":
                        cRet_msg = "Cancelamento não permitido";
                        break;
                    case "910":
                        cRet_msg = "Comunique-se com a central com urgência";
                        break;
                    case "920":
                        cRet_msg = "Transação não concluída";
                        break;
                    case "930":
                        cRet_msg = "Arquivo ENV vazio ou com problemas";
                        break;
                    case "940":
                        cRet_msg = "Transação já realizada, e não pode ser Duplicada. Verifique seu Extrato";
                        break;
                    case "950":
                        cRet_msg = "Protocolo ENV de comunicação inválido";
                        break;
                    default:
                        cRet_msg = "Erro desconhecido";
                        break;
                }
                //string cRetorno = "" + cArquivo.Substring(42, 3); //cod. retorno
                /*
                  cArquivo_mostra += "<td align='right'>Retorno: </td>";
                  cArquivo_mostra += "<td>" + cRet_msg + "</td>";
                 */



                //verificando se a venda ocorreu
                if (cCodRet != "000")
                {
                    //string cMsg_neg = "Venda NÃO realizada: " + cRet_msg;
                    cArquivo_mostra += "Venda NÃO realizada: " + cRet_msg;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "key1", "alert('" + cMsg_neg + "');location.href='Venda.aspx?op=convdados&menu=n';", true);
                    //comunicando.Attributes["class"] = "compVendaOff";
                }
                else
                {
                    cArquivo_mostra += "<section Class='CompVenda'>";
                    //cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>"; Criar: Parametro pra atribuir tamanho da font para impressão
                    cArquivo_mostra += "<table style='font-size: 10px; font-weight: 700;'>";
                    cArquivo_mostra += "<thead style='vertical-align:bottom' >";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via do Convênio</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "</thead>";
                    cArquivo_mostra += "<tbody>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>CPF</td>";
                    cArquivo_mostra += "<td id='cpf2' style='text-align: center;' class='tFontExtrato' >" + cCpfCnpj + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr> ";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan = '2' id='nome2' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + cNome + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
                    cArquivo_mostra += "<tr style='height:auto;' >";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via da ASU</td>";
                    cArquivo_mostra += "</tr>";
                    /**/
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>CPF</td>";
                    cArquivo_mostra += "<td  id='cpf' style ='text-align: center;' class='tFontExtrato' >" + cCpfCnpj + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' id='nome' class='cvTopico tFontExtrato' style='border-top: 2px solid black;' >" + cNome + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'> - - - - - - - - - - - </td></tr>";
                    cArquivo_mostra += "<tr style = 'height:auto;' >";
                    /**/
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Comprovante de Venda</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td colspan='2' class='cvTopico' >Via do Associado</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;' > AUTORIZACAO:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cAutoriza) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cod.Convênio</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros(cCodConv) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Cartão:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cCodCarta + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Valor</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cValor) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Parcelas:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + cParcela + " </td>";
                    cArquivo_mostra += "</tr >";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Data:</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + DateTime.Now + " </td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr>";
                    cArquivo_mostra += "<td style='width: 40%; text-align: right;'>Saldo</td>";
                    cArquivo_mostra += "<td style='text-align: center;' >" + CortaZeros((Convert.ToDecimal(cSaldo) / 100).ToString("C2")) + "</td>";
                    cArquivo_mostra += "</tr>";
                    cArquivo_mostra += "<tr><td colspan='2' class='cvTopico'>.</td></tr>";
                    cArquivo_mostra += "</tbody>";
                    cArquivo_mostra += "</table>";
                    cArquivo_mostra += "</section>";

                    //comunicando.Attributes["class"] = "compVendaOff";
                }

                //EFETUAR RETORNO, relatórios, etc
                //lblRetorno.Text = "<span style='color:DarkGreen;font-size:18px;'>VENDA EFETUADA COM SUCESSO! Transa: " + cCod_transa + "</span>";


                /* 
                compVenda.Attributes["class"] = "compVendaOn";
                secCompVenda.Attributes["class"] = "compVendaOff";

                lblRetorno.Text = cArquivo_mostra;
                */

                //###################################    Criar Variável para retornar dados

            }
            catch (Exception erro)
            {
                nVezes = 0;

                if (nVezes <= Convert.ToInt16("" + WebConfigurationManager.AppSettings["Timeout_venda"]))
                {
                    System.Threading.Thread.Sleep(Convert.ToInt16(1000));
                    nVezes++;
                    Processar_retorno();
                }
                else
                {
                    //lblRetorno.Text = "Tempo Esgotado, tente novamente!" + erro.Message;
                    //###################################    Criar Variável para retornar dados
                }
            }

        }

        protected void finalizarVenda(object sender, EventArgs e)
        {
            //MessageBox.Show("Finalizar Venda!!!");

            //lblRetorno.Text = String.Empty;

            //###################################    Criar Variável para retornar dados

            //compVenda.Attributes["class"] = "compVendaOff";
            //secCompVenda.Attributes["class"] = "form-guia compVendaOn";

        }


    }
}