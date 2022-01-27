<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estudos.aspx.cs" Inherits="Site.Privado.Estudos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Página de Estudos</title>
    <script src="../Js/Scripts.js" type="text/javascript" defer="defer"></script>
    <link rel="stylesheet" href="../Css/eStyle.css" />
    <style>
        
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <section class="secLogo">
                <div class="secLogoBox">
                    <div class="secLogoImg">
                        <img  src="../Img/Logo Régis-3.png" /></div>
                    <div class="secLogoTexto">
                        <p>Tecnologia</p>
                    </div>
                    <div class="secLogoTitulo">
                        <p>Meus Cursos</p>
                    </div>
                </div>                
            </section>
        </nav>
        <main class="areaPagina">
            <section class="secMenuLateral">
                <div class="mljs">                    
                    <ul>
                        <li style=""> Desenvolvedor Web - Básico
                            <ul style="">
                                <li id="btnJSjavascript" > JavaScript
                                    <ul id="btnULjs" style="display: none">                                
                                        <li id="btnJSprincipos">Princípios Básicos</li>
                                        <li id="btnJSfuncoes">Funções</li>
                                        <li id="btnJSarray" >Array</li>
                                        <li id="btnJSnumeros" >Números</li>
                                        <li id="btnJSString" >String</li>
                                        <li id="btnJSDate" >Date</li>
                                        <li id="btnJSObject" >Object</li>
                                    </ul>
                                </li>
                                <li id="btnCSSjs" style="cursor: pointer;" >CSS
                                    <ul id="btnJScss" style="display: none;">
                                        <li id="btnCSSprincipios">Princípios Básicos</li>                                
                                        <li id="btnCSSObject">Object</li>
                                    </ul>
                                </li>
                            </ul>
                        </li><br />
                        <li style=""> Desenvolvedor Web - Avançado
                            <ul style="">
                                <li id="btnJSavancado" > JavaScript
                                    <ul id="btnULjsavancado" style="display: none">                                
                                        <li id="btnJSAdom">DOM</li>
                                        <li id="btnJSAeventos">Eventos de document</li>
                                        <li id="btnJSAevent" >Eventos com addEventListener</li>
                                        <li id="btnJSAjscss" >JavaScript com CSS</li>
                                        <li id="btnJSAdiversos" >Eventos Diversos</li>
                                        <li id="btnJSAform" >Formulários</li>
                                        <li id="btnJSArepeticao" >Ações de Repetições</li>
                                        <li id="btnJSArelogio">Relógio Digital</li>
                                        <li id="btnJSAajax">AJAX</li>                                        
                                    </ul>
                                </li>
                                <li id="btnjQueryAvancado" style="cursor: pointer;" >jQuery
                                    <ul id="btnJQul" style="display: none;">
                                        <li id="btnJQwid">WidGets</li>                                
                                        <li id="btnJQedocument">Eventos de Docuemntos</li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>

                </div>
            </section>            
            <section id="secJS" class="secJS">
                <div id="divJSprincipos" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Princípios</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input id="iJsNome" type="text" placeholder="Digite uma palavra"/>
                            <input id="btnExcutar" type="submit" value="Executar" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                Conteúdo<br />
                                <label id="lblResp"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSfuncoes" class="secJSBox" >
                    <main class="secJSmain">
                        <p>Funções</p> 
                        <section class="secJSmaiCad">
                            <div class="JScadDiv">
                                <h1>Função de Execução</h1>
                                <input id="iNomefExec" type="text" placeholder="Digite..."/>
                                <input id="btnExecutarfExec" type="submit" value="Exibe Texto Digitado" />
                            </div>
                            <div class="JScadDiv">
                                <h1>Função de Retorno</h1>                                
                                <input id="iTemperaturaRet" type="number" value="0"/>
                                <input id="btnExecutarfRet" type="submit" value="Converter Temperatra (C/F)" />
                            </div>
                            <div class="JScadDiv">
                                <h1>Função com Parâmetros</h1>
                                <input id="iDias" type="number" value="22"/>
                                <input id="iSalario" type="number" value="1100" />
                                <input id="btnExecutarfPar" type="submit" value="Calcular Salário" />
                            </div>
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblRespFuncoes"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSarray" class="secJSBox" >
                    <main class="secJSmain">
                        <p>Array</p> 
                        <section class="secJSmaiCad">
                            <div class="JScadDiv">
                                <h1>Mostrar uma Lista</h1>
                                <input id="btnExecutarArray" type="submit" value="Executar Array"/><br /><br />
                            </div>
                            <div class="JScadDiv">
                                <h1>Adicionar Item num Array</h1>
                                <input id="iTextArray" type="text" placeholder="Digite uma palavra..."/>
                                <input id="btnAdicionarArray" type="submit" value="Adicionar Item no Array" />
                            </div>
                            <div class="JScadDiv">
                                <h1>Remover Item de um Array</h1>                                
                                <input id="btnRemoveArray" type="submit" value="Remover Item no Array" />
                            </div>
                            <div class="JScadDiv">
                                <h1>Pesquisar</h1>
                                <input id="iPesquisaArray" type="text" placeholder="Digite uma palavra para Pesquisar" />
                                <input id="btnPesquisaArray" type="submit" value="Pesquisar"/>
                            </div>
                            <div class="JScadDiv">
                                <h1>Ordenar Elementos</h1>
                                <input id="btnOrdenarArray" type="submit" value="Ordenar Elementos" />
                            </div>
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblRespArray"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSnumeros" class="secJSBox" >
                    <main class="secJSmain">
                        <p>Números</p> 
                        <section class="secJSmaiCad">
                            <div class="JScadDiv">
                                <h1>Cálculos com Números</h1>
                                <input id="iNum1" type="number" />
                                <input id="iNum2" type="number" />
                                <input id="btnCalcularNum" type="submit" value="Calcular" />
                            </div>
                            <div class="JScadDiv">
                                <h1>Sorteio de Mega-Sena</h1>
                                <input id="btnSortearMega" type="submit" value="Sortear Mega-Sena"/>
                            </div>
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblResultNum"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSstring" class="secJSBox" >
                    <main class="secJSmain">
                        <p>String</p> 
                        <section class="secJSmaiCad">
                            <div class="JScadDiv">                                
                                <input id="iFrase" type="text" placeholder="Digite uma Frase"/>
                            </div>
                            <div class="JScadDiv">
                                <h1>Trabalhando com String</h1>                                
                                <input id="btncharAt" type="submit" value="charAt" />
                                <input id="btnsubStr" type="submit" value="subStr" />
                                <input id="btnsplit" type="submit" value="split" />
                                <input id="btnLowerCase" type="submit" value="toLoweCase" />
                                <input id="btnUpperCase" type="submit" value="toUpperCase" />
                            </div>
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblResultString"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSdate" class="secJSBox" >
                    <main class="secJSmain">
                        <p>Date</p> 
                        <section class="secJSmaiCad">
                            <input id="iCalendar" type="date" /><br /><br />
                            <input id="btnDia" type="submit" value="Dia" />
                            <input id="btnMes" type="submit" value="Mês" />
                            <input id="btnAno" type="submit" value="Ano" />
                            <input id="btnData" type="submit" value="Hoje" />
                            <input id="btnDiaNascimento" type="submit" value="Dia em que Você Nasceu" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblResultDate"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSobject" class="secJSBox" >                    
                     <main class="secJSmain">
                        <p>Object</p> 
                        <section class="secJSmaiCad">
                            <input id="btnObject" type="submit" value="Object" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">
                                <label id="lblResultObject"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                
            </section>
            <section id="secCSS" class="secCSS" style="display: none;">                
                <div id="divCSSprincipio" class="secJSBox" >
                    <main class="secJSmain">
                        <p>Princípio CSS</p> 
                        <section class="secJSmaiCad">
                            Cadastro
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">Conteúdo</div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divCSSObject" class="secJSBox" style="display: none" >                    
                    <main class="secJSmain">
                        <p>Object</p> 
                        <section class="secJSmaiCad">
                            Cadastro
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">Conteúdo</div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div> 
            </section>
            <section id="secJSavancado" class="secJS">
                <div id="divJSAdom" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>DOM</p>                         
                        <section class="secJSmaiCad" style="float: left;">                            
                            <input id="btnJSAtrabdom" type="submit" value="Trabalhando com DOM" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAdom"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAedoc" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Eventos de document</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAedoc"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAaddevent" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Eventos com  addEventListener</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAaddevent"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAcss" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>JavaScriprt e CSS</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAcss"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAediversos" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Eventos Diversos</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAediversos"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAform" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Formulários Inteligentes</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAform"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSArepet" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Ações de Repetição por Tempo</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSArepet"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSArelogio" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Relógio Digital</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSArelogio"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                <div id="divJSAajax" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Ajax</p>                         
                        <section class="secJSmaiCad" style="float: left;">
                            <input />
                            <input id="" type="submit" value="" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJSAajax"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>

            </section>
            <section id="secJQavancado" class="secCSS">
                <div id="divJQWidgets" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>Widgets</p>                         
                        <section class="secJSmaiCad" style="float: left;">                            
                            <input id="btnJQwidgets" type="submit" value="Widgets" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJQwidgets"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
                 <div id="divJQui" class="secJSBox">                    
                    <main class="secJSmain" >
                        <p>UI</p>                         
                        <section class="secJSmaiCad" style="float: left;">                            
                            <input id="btnJQedoc" type="submit" value="Widgets" />
                        </section>
                        <section class="secJSmainBox">
                            <div class="secJSmainDados">                                
                                <label id="lblRespJQedoc"></label>
                            </div>
                            <div class="secJSmainNotas">Anotações</div>
                            <div class="secJSmainMidia">Mídia</div>
                        </section>
                    </main>
                </div>
            </section>
        </main>
    </form>
</body>
</html>
