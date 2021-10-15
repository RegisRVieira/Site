<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pessoal.aspx.cs" Inherits="Site.Pessoal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Utilidades</title>
    <style type="text/css">
        .corpo {
            margin: 20px auto;
			border: 2px solid #2b2b2b;
            width: 40%;
            height: auto;
            display: inline-block;            
        }

        p {
            margin-top: 10px;
            font-size: 28px;
            text-align: center;
            text-shadow: 0 0 3px #FF0000; /*0 0 3px #FF0000, 0 0 5px #0000FF*/			
        }

        .menuBotoes a {
			margin-top: 20px;
            width: 205px;
            height: 60px;
            border-radius: 10px;
            text-align: center;
            display: inline-block;
            margin-right: 2px;
            background-color: #f26907;
            color: white;
            padding-top: 25px;
        }

        a {
            text-decoration: none;
            color: #f26907;
            line-height: 20px;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
		<section id="scMenu" runat="server" class="menuBotoes">
			<asp:LinkButton ID="lbtListaMusica" runat="server" Text="Lista de Músicas" OnClick="ativarListaMusica"></asp:LinkButton>
			<asp:LinkButton ID="lbtMusicasRecebidas" runat="server" Text=" Músicas Recebidas" OnClick="ativarMusicaRecebida"></asp:LinkButton>
			<asp:LinkButton ID="lbtListaInteresses" runat="server" Text="Lista de Interesses" OnClick="ativarListaInteresses"></asp:LinkButton>
			<asp:LinkButton ID="lbtMma" runat="server" Text="MMA" OnClick="ativarMma"></asp:LinkButton>
			<asp:LinkButton ID="lbtDicas" runat="server" Text="Dicas" OnClick="ativarDicas"></asp:LinkButton>
			<asp:LinkButton ID="lbtMarcenaria" runat="server" Text="Marcenaria" OnClick="ativarMarcenaria"></asp:LinkButton>
			<asp:LinkButton ID="lbtGSX" runat="server" Text="GSX" OnClick="ativarGsx"></asp:LinkButton>
			<asp:LinkButton ID="lbtEstudar" runat="server" Text="Estudar" OnClick="ativarEstudar"></asp:LinkButton>
			<asp:LinkButton ID="lbtZenvia" runat="server" Text="Zenvia" OnClick="ativarZenvia"></asp:LinkButton>
			<asp:LinkButton ID="lbtASU" runat="server" Text="Equipamentos ASU" OnClick="ativarASU"></asp:LinkButton>			
			<asp:LinkButton ID="lbtFullCycle" runat="server" Text="Ful Cycle" OnClick="ativarFulCycle"></asp:LinkButton>
		</section>
        <asp:MultiView ID="mwUtilidades" runat="server">
			<asp:View ID="vwListaMusicas" runat="server">
				<section class="corpo">
					<p>Lista de Músicas</p>	
				<ol>
					<li><a href="https://www.youtube.com/watch?time_continue=237&v=JPJjwHAIny4">Lady Gaga, Bradley Cooper - Shallow</a></li>
					<li><a href="https://www.youtube.com/watch?v=kxZyuAt9bHo">Always Remember Us This Way - Lady Gaga</a></li>
					<li><a href="https://www.vagalume.com.br/avril-lavigne/my-happy-ending-traducao.html#play:all">My Happy Ending</a></li>
					<li><a href="https://www.vagalume.com.br/marshmello/here-with-me-feat-chvrches-traducao.html#play:all">Here With Me</a></li>
					<li><a href="https://www.vagalume.com.br/bruce-springsteen/secret-garden-traducao.html#play:all">Secret Garden</a></li>
					<li><a href="https://www.vagalume.com.br/ed-sheeran/thinking-out-loud-traducao.html#play:all">Thinking Out Loud</a></li>
					<li><a href="https://www.vagalume.com.br/shania-twain/any-man-of-mine-algum-homem-meu.html">Any Man Of Mine</a></li>
					<li><a href="https://www.vagalume.com.br/u2/i-still-havent-found-what-im-looking-for-traducao.html">I Still Haven't Found What I'm Looking For</a></li>
					<li><a href="https://www.youtube.com/watch?v=6jZVsr7q-tE&list=RDBL8Wd25oEk8&index=24">Never Enough</a></li>
					<li><a href="https://www.youtube.com/watch?v=I-QfPUz1es8&list=RDBL8Wd25oEk8&index=25">Bad Liar</a></li>
					<li><a href="https://www.vagalume.com.br/chuck-berry/you-never-can-tell-traducao.html">You Never Can Tell </a></li>
					<li><a href="https://www.youtube.com/watch?v=7blckSDcLs0">My Hometown Live 2013</a></li>
					<li><a href="https://www.vagalume.com.br/sam-smith/dancing-with-a-stranger-with-normani-traducao.html">Dancing With A Stranger</a></li>
					<li><a href="https://www.vagalume.com.br/daniel/eu-nao-te-amo.html">Eu não te amo</a></li>
					<li><a href="https://www.youtube.com/watch?v=nr1AI0CB8TA">Far Away</a></li>
					<li><a href="https://www.youtube.com/watch?v=sjaMIzSJayc">I Look To You</a></li>
				</ol>
				</section>
			</asp:View>
			<asp:View ID="vwMusicasRecebidas" runat="server">
                <section class="corpo">
					<p>Lista de Músicas Recebidas</p>
				<ul>					
					<li><a href="https://www.youtube.com/watch?v=JiuYnJYKLus&feature=youtu.be">24-08-2020 - MB</a></li>
					<li><a href="https://www.youtube.com/watch?v=ZYy0Z2_w728&feature=youtu.be">27-09-2020 - CC</a></li>
					<li><a href=https://www.youtube.com/watch?v=RsKqMNDoR4o&list=RDdMbngWI2NRA&index=2>CC - 16-10-2020</a></li>
					<li><a href="https://www.youtube.com/watch?v=aoh4J8brBuQ&feature=youtu.be">CC - 04-12-2020 - Zero a Dez</a></li>
					<li><a href="https://www.youtube.com/watch?v=3WQ0xZLOmes&feature=youtu.be">CC - 04-12-2020 - All Of Me</a></li>
					<li><a href="https://www.youtube.com/watch?v=OeZVE5LuMIs">O Tempo não Espera Ninguém</a></li>
					<li><a href="https://www.youtube.com/watch?v=sGzosBwvURg&feature=youtu.be">CC - 14-01-2021 - Fruto Especal</a></li>
					<li><a href="https://www.youtube.com/watch?v=y5gtPs2dm0Y&feature=youtu.be">MB - 14-01-2021 - Só Você e EU</a></li>
					<li><a href="https://youtu.be/EVANElNspTY">FC - 14-01-2021 - Boa Companhia</a></li>
					<li><a href="https://www.youtube.com/watch?v=y0wzDTutlmE">FC - Dia Especial</a></li>
					<li><a href="https://www.youtube.com/watch?v=At7oJMe-Kng">CC - 20-04-2021 - Quando Você Vem Me Abraçar</a></li>
					<li><a href="https://youtu.be/W5LIUUJAQkw">P - 30-07-2021 - Canteiros</a></li>
					<li><a href="https://youtu.be/jvgkiurPmZk">P - 30-07-2021 - Deslizes</a></li>
					<li><a href="https://youtu.be/Qfd0XCexKFI">P - 30-07-2021 - Revelação</a></li>
					<li><a href="https://www.youtube.com/watch?v=pHhe-etuurE">P - 05-08-2021 - Marca Evidente</a></li>
					<li><a href="https://youtu.be/C-XXpByKvhE">CC - 19-08-2021 - Frisson</a></li>
					<li><a href="https://youtu.be/YuV7vUtV820">CC - 19-08-2021 - Te Amar Sem Medo</a></li>
					<li><a href="https://www.youtube.com/watch?v=aR8FCm-4QX4">P - 31/08/2021 - Jogado na Rua</a></li>
					<li><a href="https://www.youtube.com/watch?v=l-FxY25lYzY">P - 01/09/2021 - La Belle de Jour / Girassol</a></li>
					<li><a href="https://www.youtube.com/watch?v=7bUUHI8tQdQ">Na Luz do Som</a></li>
					<li><a href="https://youtu.be/2_xBCeBmaS8">P - 09-09-2021 - Arranhão</a></li>
					<li><a href="https://youtu.be/k4MSte-XQ1o">P - 09-09-2021 - Não posso ter medo de amar</a></li>
					<li><a href="https://www.youtube.com/watch?v=wfJBjgxEfK8">P - 10-09-2021 - Juras de Amor</a></li>
					<li><a href="https://youtu.be/NFX9AHBCfmE"> P - 05-10-2021 - You'll Never Find Michael Buble & Laura Pausini</a></li>
				</ul>
                </section>
			</asp:View>
			<asp:View ID="vwListaInteresses" runat="server">
				<section class="corpo">
				<p>Lista de Interesses</p>
				<ol>
					<li><a href="https://www.consiste.com.br/portal.nsf/aconsiste.xsp">Desenvolvedor Web</a></li>
					<li><a href="https://www.devmedia.com.br/orientacoes-basicas-na-elaboracao-de-um-diagrama-de-classes/37224">Diagrama de Classe</a></li>
					<li><a href="https://docs.microsoft.com/pt-br/aspnet/overview">Visão geral do ASP.NET</a></li>
					<li><a href="https://www.urionlinejudge.com.br/judge/pt/login">URI - Problemas e Contextos</a></li>
					<li><a href="https://github.com/">GitHub</a></li>
					<li><a href="https://www.webcis.com.br/centralizar-divs-na-horizontal-e-vertical-com-css.html">DIV com CSS</a></li>
					<li><a href="https://dev.w3.org/html5/html-author/charref">Caracteres Especiais para Web</a></li>
					<li><a href="https://fonts.google.com/">Fonts - Google</a></li>
					<li><a href="https://html.spec.whatwg.org/multipage/input.html">Tags html</a></li>
					<li><a href="https://ibooked.com.br/widgets/weather?cityid=40520">Exibir Clima no Site</a></li>
					<li><a href="https://www.youtube.com/watch?v=_oB1zfraGCo&feature=youtu.be">Pagamento Estantânio</a></li>
					<li><a href="http://eoh.com.br/nao-se-engane-apos-dizer-eu-te-amo-voce-precisa-ter-muito-mais-o-que-dizer/">Tem que Dizer muito Mais</a></li>
					<li><a href="https://www.uol.com.br/universa/noticias/redacao/2019/07/31/bullet-jato-ponto-u-we-vibe-entenda-melhor-orgasmo-da-mulher-de-a-a-z.htm">Orgasmo Feminino</a></li>
					<li><a href="https://www.devmedia.com.br/dimensionando-elementos-com-jquery/28784">Redimensinar Elementos</a></li>
					<li><a href="https://www.youtube.com/watch?v=3AzAi7VTmbw">Enviar dados de um Formulário para um e-mail</a></li>
					<li><a href="https://www.youtube.com/watch?v=a-IJdB6QFq0">Enviar dados de um Formulário para um e-mail - Outro Autor</a></li>
					<li><a href="https://www.youtube.com/watch?v=wYLvLWctowE">Enviar Arquivo pelo Site</a></li>
					<li><a href="https://tableless.github.io/iniciantes/manual/js/inserindo-js.html">Adicinar Java Script</a></li>
					<li><a href="https://menudodia.blogosfera.uol.com.br/2019/10/03/abacaxi-e-versatil-e-bom-para-a-digestao-aprenda-a-usar-em-pratos-salgados/">Abacaxi</a></li>
					<li><a href="https://www.youtube.com/watch?v=2aDySEqiy0o">Warren Buffett</a></li>
					<li><a href="https://www.uol.com.br/universa//videos/?id=nao-guarda-dinheiro-porque-nunca-sobra-livrese-dessa-desculpa-furada-04024D1A356CC0B96326">Economizar</a></li>
					<li><a href="https://www.softdownload.com.br/10-comandos-dos-prompt-comando-windows.html">Comandos DOS</a></li>
					<li><a href="http://www.linhadecodigo.com.br/artigo/3261/strings-em-csharp-coisas-que-nem-todos-sabem.aspx">Trabalhando com Strings</a></li>
					<li><a href="https://www.uol.com.br/universa/noticias/redacao/2019/11/19/como-a-gente-conquista-um-sagitariano-marcia-fernandes-explica.htm">Signo de Sagitário</a></li>
					<li><a href="https://reginanavarro.blogosfera.uol.com.br/2020/01/16/por-que-casais-que-vivem-ressentidos-nao-se-separam/">Separação</a></li>
					<li><a href="https://www.uol.com.br/tilt/noticias/redacao/2020/01/19/como-a-china-esta-ditando-as-tendencias-para-2020.htm">Tendências da China</a></li>
					<li><a href="https://ivanmartins.blogosfera.uol.com.br/2019/11/26/amor-incondicional-isso-nao-existe-nem-em-filme/">Amor incondicional</a></li>
					<li><a href="https://mauriciodesouzalima.blogosfera.uol.com.br/2020/01/22/quantos-banhos-sera-que-eu-posso-tomar-por-dia-nao-sao-tantos-assim/">Banho</a></li>
					<li><a href="https://liabock.blogosfera.uol.com.br/2020/01/23/a-vida-e-mais-facil-pra-casais-que-transam-todo-dia">Todo dia</a></li>
					<li><a href="https://www.tecmundo.com.br/mercado/150513-tudo-pix-sistema-acabar-ted-doc-bandeira-cartao.htm">Pix, Banco Central do Brasil</a></li>
					<li><a href="https://www.youtube.com/watch?v=_oB1zfraGCo">Pagamento Instantâneo</a></li>
					<li><a href="https://www.loja.canon.com.br/pt/canonbr">Canon</a></li>
					<li><a href="https://www.ferramentaskennedy.com.br/106279/kit-ferramentas-bits-brocas-chave-trena-74pcs-p90336-makita?utm_source=google-shop&utm_medium=shop&utm_campaign=google_shop&gclid=Cj0KCQiAhojzBRC3ARIsAGtNtHWIUQP3r5FmAJlj3yhAN0rYfVgzpDtchj9Xpb93pnURmfZB6kTkwscaAs7oEALw_wcB">Kit</a></li>
					<li><a href="https://www.americanas.com.br/produto/38262959/bateria-recarregavel-ion-de-litio-12v-max-1-5ah-makita?WT.srch=1&acc=e789ea56094489dffd798f86ff51c7a9&epar=bp_pl_00_go_pla_casaeconst_geral_gmv&gclid=Cj0KCQiAhojzBRC3ARIsAGtNtHVgf4Sob5FIVM9AA1LXCeNGGNoN-SlR6Yy1QNiYx1XuryqMPpWai9saAsqUEALw_wcB&i=577ee3deeec3dfb1f84b1508&o=5b2d433aebb19ac62c750494&opn=YSMESP&sellerid=314550000347&wt.srch=1">Bateria</a></li>
					<li><a href="https://pt.aliexpress.com/item/32647519732.html">Mochila</a></li>
					<li><a href="https://www.oobj.com.br/bc/article/licen%C3%A7a-terminal-server-conex%C3%A3o-de-%C3%81rea-de-trabalho-remota-62.html">Licença Terminal Server</a></li>
					<li><a href="https://www.google.com/search?q=sleep+pillow&sxsrf=ALeKk029uhhgQjBRG-wOIrfEpYJoh8h20g:1605008415769&source=lnms&tbm=shop&sa=X&ved=2ahUKEwj1zO6j8vfsAhWZD7kGHbV6AUsQ_AUoAXoECA0QAw&biw=1920&bih=937">Travesseiro</a></li>
					<li><a href="https://maujor.com/blog/pg_apoio/height100/exemplo2.html">height: 100%</a></li>
					<li><a href="https://maujor.com/blog/pg_apoio/height100/exemplo4.html">height: 100%</a>
					<li><a href="https://www.w3.org/Style/Examples/007/scrollbars.pt_BR.html">Barra de Rolagem</a></li>
					<li><a href="https://www.youtube.com/watch?v=l5Y_IXmmLDk">Master Page</a></li>
					<li><a href="https://www.youtube.com/watch?v=_uOqhLW5nkA">UpLoad de Arquivos</a></li>
					<li><a href="http://www.macoratti.net/10/11/asp_vlsc.htm">Obter dados selecionando um registro do Grid</a></li>
					<li><a href="https://support.microsoft.com/pt-br/help/323246/how-to-upload-a-file-to-a-web-server-in-asp-net-by-using-visual-c-net">Tamanho padrão de arquivo para UpLoad</a></li>		
					<li><a href="https://www.youtube.com/watch?v=Fbf-u2ZXCd0">Botão CSS</a></li>
					<li><a href="https://youtu.be/k-bCjNX5yWM">Embrulha pra viagem</a></li>
					<li><a href="https://www.youtube.com/watch?v=giZyXUzrywE">Tô ficando velho, to ficando fraco</a></li>
					<li><a href="https://exceleasy.com.br/inserir-imagem-em-celula-no-excel/">Excel</a></li>
					<li><a href="https://www.devmedia.com.br/css-z-index-entendendo-sobre-o-eixo-z-na-web/28698">z-index</a></li>
					<li><a href="https://www.maujor.com/tutorial/propriedade-css-box-sizing.php">A propriedade CSS box-sizing</a></li>
					<li><a href="https://www.youtube.com/watch?v=1Es0kCfzZZs">Request.QueryString - Vídeo</a></li>
					<li><a href="https://www.devmedia.com.br/conceitos-e-exemplo-pratico-usando-querystring/18094">Request.QueryString - Texto</a></li>
					<li><a href="https://www.w3.org/Style/Examples/007/center.pt_BR.html">Centralizar Coisas</a></li>
					<li><a href="https://docs.microsoft.com/pt-br/dotnet/api/system.string.isnullorempty?view=net-5.0">IsNullOrEmpety</a></li>
					<li><a href="https://www.youtube.com/watch?v=_w57bWIPZKE">Efeitos em Botões</a></li>
					<li><a href="https://www.youtube.com/watch?v=wvF9Mrzm4bw">Photoshop - Como Criar Shapes</a></li>
					<li><a href="https://saraiva.digipix.com.br/index.php">Revelação Digital</a></li>
					<li><a href="https://pt.stackoverflow.com/questions/166405/mascara-em-campo-input">Mácara para Campo Valor</a></li>
					<li><a href="http://blog.conradosaud.com.br/artigo/26">Mácara para Campo Input, Texto, Números e Dinheiro</a></li>
					<li><a href="https://www.todoespacoonline.com/w/2014/04/imagens-para-layouts-responsivos-com-css/">Imagens para Layout Responsivo</a></li>
					<li><a href="https://developer.mozilla.org/pt-BR/docs/Web/CSS/background-size">background-size, Imagem Responsiva</a></li>
					<li><a href="https://pt.stackoverflow.com/questions/782/como-imprimir-o-conte%C3%BAdo-dentro-de-uma-div-html">Enviar página para a Impressora</a></li>
					<li><a href="https://www.devmedia.com.br/csharp-switch-case/38214">Switch Case</a></li>
					<li><a href="https://docs.microsoft.com/pt-br/dotnet/api/system.web.ui.htmlcontrols.htmlselect?view=netframework-4.8">HtmlSelect Classe - Inserir Código no HTML</a></li>
					<li><a href="https://stackoverflow.com/questions/2378338/how-to-get-selected-value-of-a-html-select-with-asp-net">Script C# no HTML</a></li>
					<li><a href="https://www.dotnetperls.com/">C#</a></li>
					<li><a href="https://www.codigofonte.net/">C# - AspNet</a></li>
					<li><a href="https://qastack.com.br/programming/18981118/http-error-403-14-forbidden-the-web-server-is-configured-to-not-list-the-con">Configurar Página Inicial no WebConfig</a></li>
					<li><a href="http://jquerydicas.blogspot.com/2013/11/mysql-extrair-data-com-extract.html">Extrair data do MySql</a></li>
					<li><a href="https://www.devmedia.com.br/funcoes-de-manipulacao-de-data-do-mysql-5-5/25539">Extrair data do MySql - 2</a></li>
					<li><a href="https://sl.empiricus.com.br/p/pe145v-oportunidades-a/?xpromo=XE-ME-YT-PE145V-IMKT-X-TVI-X-X&gclid=Cj0KCQjw7pKFBhDUARIsAFUoMDbQM8qXbGY8o5EDu39JgekuTdvjb-DfK11z_1WJo3a-V8qfTQAIdWAaAuTFEALw_wcB"></a>Investimento</li>
					<li><a href="https://www.youtube.com/watch?v=glpiHx0NNcE">Mudar Cor de Objeto Photoshop</a></li>
					<li><a href="https://pt.stackoverflow.com/questions/118183/limitar-exibi%C3%A7%C3%A3o-de-texto-por-quantidade-de-caracteres-com-css">Formatação de Texto CSS - text-overflow</a></li>		
					<li><a href="https://www.dynamicpdf.com/examples/html-to-pdf-.net-core?gclid=CjwKCAjwoZWHBhBgEiwAiMN66V1nI84aan7M-NyeB5d2Zg6L2L0B3pSm00KSaAbKVSFN3wd-WGDwERoCt9UQAvD_BwE">Criar PDF</a></li>
					<li><a href="https://www.devmedia.com.br/criando-e-manipulando-arquivos-pdf-com-a-biblioteca-itextsharp-em-csharp/33392">Criar e Manipular Arquivos PDF</a></li>		
					<li><a href="https://www.lojadomecanico.com.br/produto/119773/21/367/Kit-Multi-Funcao-Maksipower-8--a-Bateria-18V-Bivolt-com-FuradeiraParafusadeira-Serra-Circular-Serra-Tico-Tico-Lanterna-e-Bancada/153/?utm_source=googleshopping&utm_campaign=xmlshopping&utm_medium=cpc&utm_content=119773&gclid=CjwKCAjw55-HBhAHEiwARMCszpY78tk2ZY3XLYaQL3uynKI4n2zPbonLS3RF5Xh3pQ6EiHlR21iqYhoCFjwQAvD_BwE">Serralheria</a></li>
					<li><a href="https://www.ferramentaskennedy.com.br/118145/kit-ferramentas-multifuncao-8-em-1-maksipower8-azul-maksiwa?utm_source=google-shop&utm_medium=shop&utm_campaign=google_shop&gclid=CjwKCAjw55-HBhAHEiwARMCszp1KySEI6g6fuURahEQoedaLrFHeTqkzemCmZxWq65V3gRSuRBIJARoCV9IQAvD_BwE">Serralheria 2</a></li>
					<li><a href="https://www.youtube.com/watch?v=vLyy9z5lIeo">Suporte Esmerilhadeira</a></li>
					<li><a href="https://www.youtube.com/watch?v=ZQHLKmd080Y">Http Error 500.19. Internal Server Error. The Request Page ...</a></li>
					<li><a href="https://www.youtube.com/watch?v=MbIRpU7AHL8">Configurar NTP - Sincronismo de horário do Servidor com os terminais</a></li>		
					<li><a href="https://www.youtube.com/watch?v=xrMwr7v4MCY">Premiere</a></li>		
					<li><a href="https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.exists?view=net-5.0">Checar se Arquivo Existe</a></li>
					<li><a href="https://www.devmedia.com.br/asp-net-upload-file-upload-e-redimensionamento-imagens-em-csharp/30928">File UpLoad</a></li>
					<li><a href="https://www.devmedia.com.br/determinando-limitacao-fileupload/7810">File UpLoad</a></li>
					<li><a href="https://www.devmedia.com.br/web-config-armazenando-e-recuperando-dados-parte-1/17620">WebConfig - Recuperar Dados 1</a></li>
					<li><a href="https://www.devmedia.com.br/web-config-armazenando-e-recuperando-dados-parte-2/17621">WebConfig - Recuperar Dados 2</a></li>
					<li><a href="https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/try-catch">try-catch (Referência de C#)</a></li>
					<li><a href="https://pt.stackoverflow.com/questions/384645/executar-fun%C3%A7%C3%A3o-ao-perder-foco-de-um-input">Executar função ao perder foco de um input</a></li>
					<li><a href="https://www.lojagtsm1.com.br/bicicletas/aro-29/bicicleta-gts-aro-29-freio-a-disco-cambio-traseiro-shimano-24-marchas-e-amortecedor-gts-m1-advanced-2021">Bike</a></li>
				</ol>
				</section>
			</asp:View>
			<asp:View ID="vwMMA" runat="server">
				<section class="corpo">
					<p>MMA</p>
				<ol>				
					<li><a href="https://www.youtube.com/watch?v=Q10uiGqydfQ">Defesa Mata Leão em Pé</a></li>
					<li><a href="https://www.youtube.com/watch?v=D3sseg9oO_c">Defesa Mata Leão</a></li>
				</ol>
				</section>
			</asp:View>
			<asp:View ID="vwDicas" runat="server">
				<section class="corpo">
					<p>Dicas</p>
				<ul>
					<li><a href="https://www.youtube.com/watch?v=0j5P5TqapJU">Degradê com Photoshop</a></li>
					<li><a href="https://mybest-brazil.com.br/18731">Fogão</a></li>
					<li><a href="https://social.msdn.microsoft.com/Forums/pt-BR/92a5bc7f-53f6-4469-9749-c412882a69b0/verificar-se-existe-algum-caracter-especial-em-uma-string?forum=aspnetpt">Caracteres Especiais</a></li>
				</ul>
				</section>
			</asp:View>
			<asp:View ID="vwMarcenaria" runat="server">
				<section class="corpo">
					<p>Marcenaria</p>
				<ul>
					<li><a href="https://goinshep.com/products/serra-dobravel-push-pull?fbclid=IwAR2TiFKL9TzWFq-jJOQE9OEuAB_spAzH3rpl4gSvVhf9eGO8WiJKGe8N1L4">Marcenaria</a></li>
					<li><a href="https://lojamegazu.com/products/bancada-para-perfuracao?fbclid=IwAR3-dWrgEWOGjjD0nLvOZ3gThUSBedISX93wQ_7anWDw3jx7Nw6fKDNu6Y0">Bancada para Perfuração</a></li>
					<li><a href="https://inspireshopbr.com/products/90-degree-right-angle-clamp-fixing-clips-picture-frame-corner-clamp-woodworking-hand-tool-furniture-repaire-photo-reinforcement?fbclid=IwAR1Y22ifm90Yy028MrgEdx69EvrMKQgPa4y2CEL5kWDjRulnmMpkgAkFbXA">Braçadeiras</a></li>
					<li><a href="https://www.facebook.com/EscolaProfissaoMarceneiro/">Curso</a></li>
				</ul>
				</section>
			</asp:View>
			<asp:View ID="vwGSX" runat="server">
				<section class="corpo">
					<p>GSX</p>
				<ul>				
					<li><a href="https://www.screencast.com/t/yHHoKOBdFc">Pedido de Venda</a></li>
					<li><a href="https://www.screencast.com/t/27FJDfC3N4">Pedido de Venda 2</a></li>
					<li><a href="https://www.screencast.com/t/hqIIfzIQEPOo">Pedido de Venda 3</a></li>
					<li><a href="https://www.screencast.com/t/CQ2mHelqjUme">Orçamento</a></li>
					<li><a href="https://www.screencast.com/t/qri6t1RC2">Nota Fiscal</a></li>
					<li><a href="https://www.screencast.com/t/H2YVkZtFmPx">Impressão</a></li>
				</ul>
				</section>
			</asp:View>
			<asp:View ID="vwEstudar" runat="server">
				<section class="corpo">
					<p>Estudar</p>
				<ul>
					<li><a href="https://www.youtube.com/watch?v=ETm9S_psDmI&app=desktop">Sessions - 1</a></li>
					<li><a href="https://www.youtube.com/watch?v=bwUVC0qohlA">Sessions - 2</a></li>
					<li><a href="https://www.youtube.com/watch?v=wYLvLWctowE">UpLoad de Arquivos</a></li>
					<li><a href="https://www.youtube.com/user/Didox59">Danilo Aparecido - Torne-se um programador</a></li>
					<li><a href="https://www.domestika.org/pt/courses/557-marcenaria-profissional-para-principiantes?gclid=CjwKCAjw55-HBhAHEiwARMCszuG5S0AxETXgP7puHRyA6OJLMQ58J78KVeXeQB76Y-GX_V0xXCrdOBoCQSUQAvD_BwE">Marcenaria</a></li>
					<li><a href="https://www.youtube.com/watch?v=vLyy9z5lIeo">Sistema deslizante</a></li>
					<li><a href="http://bertuc.ci/listando-arquivos-de-um-diretorio-com-visual-c/">Listando Arquivos de um Diretório</a></li>
				</ul>
				</section>
			</asp:View>
			<asp:View ID="vwZenvia" runat="server">
				<section class="corpo">
					<p>Zenvia</p>
				<ul>
					<li><a href="https://www.loom.com/share/cfd48497c2074f3e849795e15381c0f8">Administrador</a></li>
					<li><a href="https://www.loom.com/share/423e9d0375e148a4a9d06139c4300922">Agentes</a></li>
					<li><a href="https://www.loom.com/share/6e05702cc98f4bc1a969efd018ca2b99">Voltar ao iníco ou Encerrar</a></li>
					<li><a href="https://support.zenvia.com/pt/article/alterando-o-canal-do-fluxo">Alterando o Canal do alterando-o-canal-do-fluxo</a></li>
					<li><a href="https://support.zenvia.com/pt/article/relat%C3%B3rios-uso">Relatórios</a></li>
					<li><a href="https://www.loom.com/share/450906f510f24cb4a5d49371220f8a21">Relatórios - Vídeo</a></li>
				</ul>
				</section>
			</asp:View>
			<asp:View ID="vwASU" runat="server">
				<section class="corpo">
					<p>Equipamentos ASU</p>
				<ol>
					<li><a href="http://192.168.0.253/web/device/login?lang=0">Switch</a></li>
					<li><a href="http://192.168.0.230/leituras.htm">NoBreak</a></li>
					<li><a href="http://192.168.0.55/cgi-bin/firmware.cgi">Roteador AP300</a></li>
					<li><a href="#192.1680.251">Card Printer</a></li>
					<li><a href="https://192.168.0.221/restgui/start.html?login">Remote Access - Servidor de Sistema (User: regis, senha padrão)</a></li>				
				</ol>
				</section>
			</asp:View>
			<asp:View ID="vwFulCycle" runat="server">
				<section class="corpo">
					<ol>
					<p>Links</p>
					<li><a href="https://youtu.be/5RvmeQRqfSM">Kubernetes</a></li>
					<li><a href="https://youtu.be/StqjbrHWLRk">Microsserviços</a></li>
					<li><a href="https://youtu.be/59wfoeCoNVk">Pipelines de CI / CD (Integração Contínua e Entrega Contínua) </a></li>
					<li><a href="https://youtu.be/KZpSghOWOnE">Microfrontends </a></li>
					<li><a href="https://youtu.be/aBZvS-1N_ys">Domain Driven Design (DDD) e Design Patterns.</a></li>
				</ol>
				<ol>
					<p>Curso:  Imersão Full Stack && Full Cycle</p>
					<li><a href="https://imersao.fullcycle.com.br/aula/aula-1/"> Aula 1</a></li>
					<li><a href="https://imersao.fullcycle.com.br/aula/frontend-moderno-com-nextjs/">Aula 2: Next.js e frontend nas grandes empresas</a></li>
					<li><a href="https://imersao.fullcycle.com.br/aula/kafka-e-keycloak-comunicacao-e-autenticacao-entre/">Aula 3: Kafka e Keycloak: Comunicação e autenticação entre sistemas</a></li>
					<li><a href="https://imersao.fullcycle.com.br/aula/mundo-assincrono-com-kafka-connect-e-elastic-stack/">Aula 4: Kafka Connect + Kibana Dashboards</a></li>
					<li><a href="https://imersao.fullcycle.com.br/aula/microsservico-de-relatorios-com-golang-e-elasticse/">Aula 5: Microsserviço com Golang, Elasticsearch e AW</a></li>
					<li><a href="https://imersao.fullcycle.com.br/aula/orquestracao-de-containers-com-docker-e-kubernetes/">Aula 6: Kubernetes</a></li>
				</ol>
				</section>
			</asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
