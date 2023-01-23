/*!
 * Copyright Reginaldo Rodrigues Vieira
 *
 * Date: 2019-08-07
 */
//Define a Data no Rodapé da página
var agora = new Date();
var meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
var semana = ["Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado"];
var anoatual = agora.getFullYear();
var ano = anoatual - 1969;

//Operadores Matemáticos
var a = 5;
var b = 12;
function Somar() {
    var result = a + b;
    alert(result);
}
function Dividir() {
    var result = a / b;
    alert(result);
}
function Multiplicar() {
    var result = a * b;
    alert(result);
}

// Operadores de Comparação
function Atribuir() {
    var a = 5;
    var b = 5;

    if (a == b) {
        alert("São Iguais");
    } else {
        alert("São Diferentes");
    }
}

function Comparar() {
    var cpf = "333";
    var numeros = 333;

    if (cpf === numeros) {

        alert("São Iguais");
    } else {
        alert("Nada a ver");
    }
}

//Operadores de Incremento
function Incrementar() {
    a = 5;
    a += 10;

    alert(a);
}

//Estruturas de Repetição

//While

function While() {
    var i = 1;

    while (i <= 10) {

        console.log("Passo While: " + i);
        i++;
    }
}

//For

function For() {
    for (var i = 1; i <= 10; i++) {

        console.log("Passo For: " + i);
    }
}

//Do While

function doWhile() {
    var i = 0;
    do {
        console.log("Passo Do While:" + i);
        i++;
    } while (i <= 10)

}

//Funções

//Função de Execução

function escreverNome() {
    alert("Régis");
}

//Função de retorno

function temperatura() {
    //Inicio
    function converterTemperatura(celsius) {
        var fah = (celsius * 1.8) + 32;
        return fah;
    }

    var fahreinheint = converterTemperatura(30);
    alert(fahreinheint);
    //Fim
}

//Função Matemática

function fMatematica() {
    function calcularDiaria() {
        return (6000 / 39).toFixed(2);
    }

    var diaria = calcularDiaria();
    alert(diaria);
}

//Função com Parametro

function fParametro() {
    var bonus;

    function calcularDiaria(salario, dias) {
        bonus = 500;
        return ((salario / dias) + bonus).toFixed(2)
    }
    var diaria = calcularDiaria(8000, 22);
    console.log(bonus);
    alert(diaria);
}

/*Array*/

function array() {

    var salada = new Array();
    salada[0] = "alface";
    salada[1] = "tomate";
    salada[2] = "rucula";

    alert(salada);

    var frutas = ["Uva", "Pera", "Abacaxi", "Manga"];
    alert(frutas);
    console.log(frutas[1]);
}

//Inserção

function inserirArray() {

    var elemento = ["Elemento 1", "Pera", "Moto", "Casa", "Mulher"];

    elemento.push("Último");
    elemento.unshift("Primeiro");
    elemento.pop();
    elemento.shift();
    console.log(elemento);
    alert(elemento);
    alert("Número de Elementos:  " + elemento.length);
}

//Pesquisa

function metodosDePesquisa() {

    var elementos = new Array();
    elementos[1] = "Pera";
    elementos[2] = "Uva";
    elementos[3] = "Maçã";

    var pesquisa = elementos.indexOf("Uva");

    alert(pesquisa);
    console.log(pesquisa);
}

//Em Ordem de Elementos



function megaSena() {

    function ordenarElementos(a, b) {
        return a - b;
    }

    var megaSena = ["10", "09", "50", "25", "39", "19"];

    megaSena.sort(ordenarElementos);

    console.log(megaSena);
    alert(megaSena);
}

//Números

function trabalharNumeros() {
    //Numérico
    var x = 4;
    var y = 3;
    //String
    var x1 = "4";
    var y1 = "3";

    var a = 4;
    var b = 5;
    var c = 12;

    alert("Números: " + (x + y));
    alert("Número String: " + (x1 + y1));
    alert("Maior número: " + Math.max(a, b, c));
    alert("Menor Número: " + Math.min(a, b, c));

    //Converter string em numerico

    var num = "2244";
    var calculo;

    calculo = Number(num) * 2;

    alert("De string para Numérico: " + calculo);
}

//Sortear Núemros



function sortearNumeros() {

    var sorteio;
    var mega = new Array();

    var i = 0;

    while (i < 6) {
        sorteio = Math.ceil(Math.random() * 60);
        if (mega.indexOf(sorteio) < 0) {
            mega.push(sorteio);
            i++;
        }
    }

    function ordenar(a, b) {
        return a - b;
    }
    mega.sort(ordenar);

    alert(mega);
    console.log(mega);
}

/*
 * * * String * * * 
 */

function charAt() {

    var frase = "Puta que o Pariu meu gato botou um ovo";
    var primeira = frase.charAt(0);

    var letra = "";
    var primeira_palavra = "";
    var i = 0;

    while (letra != " ") {
        letra = frase.charAt(i);
        primeira_palavra += letra;
        i++
    }

    alert(frase)
    alert(primeira_palavra)
    console.log(primeira_palavra)
}

// SubStr
var frase = "Puta que o Pariu meu gato botou um ovo";
var primeira = "";
var letra = "";
var i = 0;

function subStr() {

    while (letra != " ") {
        letra = frase.charAt(i);
        primeira = frase.substr(0, i);
        i++
    }

    alert(primeira)
}

//Split

function sPlit() {
    primeira = frase.split(" ")[0];
    alert(primeira);
    console.log(primeira);
}

function toLowerCase() {
    palavra = frase.toLowerCase();
    alert(palavra);
}

function toUpperCase() {

    palavra = frase.toUpperCase();
    alert(palavra);
}

//Formulário inteligente
function controleFormulario(e) {
    e.preventDefault();

    var caixa_nome = nome.value;

    if (caixa_nome == "") {
        mensagem.innerHTML = "Preencha este Campo";
    }
}

function controleFormularios(e) {
    e.preventDefault();

    var caixa_nome = nome.value;

    if (caixa_nome = "") {
        mensagens.innerHTML = "PQP";
    }
}

function comecar(e) {
    formViagens.onsubmit = controleFormularios;
}

//Mostrar ou ocultar objetos
function iniciar(e) {

    escolhercidade.onclick = function () {
        if (escolhercidade.checked) {
            cidade.style.display = "block";
        } else {
            cidade.style.display = "none";
        }
    }
    /*document.querySelector("#cidade").style.display = "none";*/
    cidade.style.display = "none";

    //onSubmit
    formViagem.onsubmit = controleFormulario;
}

window.onload = iniciar;
//window.onload = comecar;

//Java Script Avançado

function executarElementos() {
    var elemento = document.getElementById("opcoesPizza");

    //console.log(elemento.children);
    //console.log(elemento.children[5].innerHTML);
    //console.log(elemento.childElementCount);
    //console.log(elemento.firstElementChild);
    console.log(elemento.firstChild);
}

function executarTagName() {
    var elemento = document.getElementsByTagName("li");

    console.log(elemento);
}

function executarByIdFor() {

    var elemento = document.getElementById("opcoesPizza");
    var tamanho = elemento.childElementCount;

    for (var i = 0; i < tamanho; i++) {

        console.log(elemento.children[i].innerHTML);

    }

    console.log(tamanho);


}

function executarByTagNameFor() {

    var elemento = document.getElementsByTagName("li");

    for (var i = 0; i < elemento.length; i++) {

        console.log(elemento[i].innerText);
    }

}

function executarQuerySelector() {

    var elemento = document.querySelectorAll("li");
    //var elemento = document.querySelector("#Id");
    //var elemento = document.querySelector(".nome_da_classe");
    //var elemento = document.querySelector("TagHtml, exemplo: 'LI'");        

    for (var i = 0; i < elemento.length; i++) {

        console.log(elemento[i].innerText);
    }
}

//Inserir Elementos
function incluirElementos() {

    var meuElemento = document.querySelector("#opcoesPizza");
    //Criar novo Elemento
    var novoElemento = document.createElement("li");
    meuElemento.appendChild(novoElemento);

    //Atribuir Valor ao Item
    novoElemento.innerHTML = "Quatro Queijos";

    //Criar e Atribuir
    meuElemento.appendChild(document.createElement("li")).innerHTML = "Margeritta";
}


function openNav() {
    var menu = document.getElementById("menulateral");
    var icon = document.getElementById("treeline-icon");

    //alert("Aqui!!!");

    if (menu.className === "menuflutua") {
        menu.className += " menujs";
        //icon.innerHTML = "&cross;";
    } else {
        manu.className = "menuflutua";
        icon.innerHTML = "&#9776;";
    }
}
function closeNav() {
    var menu = document.getElementById("menulateral");
    var icon = document.getElementById("treeline-closeicon");

    //alert("Sair?!?");

    if (menu.className === "menuflutua menujs") {
        menu.className = "menuflutua";
        //icon.innerHTML = "&#9776;";
    }
}


/*
function openNav() {
    var menu = document.getElementById("menulateral");
    var icon = document.getElementById("treeline-icon");    

    if (menu.className === "menulateral") {
        menu.className += " menujs";
        //icon.innerHTML = "&cross;";
    } else {
        manu.className = "menulateral";
        icon.innerHTML = "&#9776;";
    }
}
function closeNav() {
    var menu = document.getElementById("menulateral");
    var icon = document.getElementById("treeline-closeicon");

    if (menu.className === "menulateral menujs") {
        menu.className = "menulateral";
        //icon.innerHTML = "&#9776;";
    }
}
*/
function AcessarNossaEntidade() {
    var menu = document.getElementById("menuLateralNE");
    var icoFechar = document.getElementById("treeline-NossaEntidade");
    //alert("Tá na Função do Apoio");

    if (menu.className === "menuflutuaNE") {
        menu.className += " menujs";
        //icoFechar.innerHTML = "FECHAR a Bagaça";
    }
}
function FecharNossaEntidade() {
    var menu = document.getElementById("menuLateralNE");
    var icoFechar = document.getElementById("treeline-NossaEntidade");

    if (menu.className === "menuflutuaNE menujs") {
        menu.className = "menuflutuaNE";
        //icoFechar.innerHTML = "FECHAR a Bagaça";        
    }
}



function AcessarVoceOnLine() {
    window.location.href = "LoginVoceOnLine.aspx";
}

jQuery(document).ready(function (e) {

});
//17-02-2021, depois de muito custo.
function abrirImagens() {
    window.open("S-Imagens.aspx", "minhaJanela", "width= 950, height= 600 ");
}

/* - - - - Abilita Menu Responsi do Você OnLine - - - - */
function openMenuFlutua() {
    var menu = document.getElementById("menuflutua");
    var icon = document.getElementById("treeline-icon");

    if (menu.className === "menuflutua") {
        menu.className += " menujs";
        //icon.innerHTML = "&cross;";
    } else {
        manu.className = "menuflutua";
        icon.innerHTML = "&#9776;";
    }
}
function closeMenuFlutua() {
    var menu = document.getElementById("menuflutua");
    var icon = document.getElementById("treeline-closeicon");

    if (menu.className === "menuflutua menujs") {
        menu.className = "menuflutua";
        //icon.innerHTML = "&#9776;";
    }
}
/* - - - - FIM - - - - */
/*
 - - - - - Abilita Menu Responsivo da Home - - - - -
 */
function openMenuHome() {
    //alert("Tá clicando para abrir");

    var menu = document.getElementById("navlista");
    var icon = document.getElementById("treeline-icon");

    var ativa = document.getElementById("ativa-CloseIcon");
    var desativa = document.getElementById("desativa-TreeLineIcon");

    if (menu.className === "navlista") {
        menu.className += " menujs";
        //icon.innerHTML = "&cross;";
    }
    if ((menu.className === "treeline-closeicon")) {
        manu.className += " desativa-TreeLineIcon";
        //icon.innerHTML = "&cross";
    }
}
function closeMenuHome() {

    //alert("Tá clicando para fechar");

    var menu = document.getElementById("navlista");
    var icon = document.getElementById("treeline-closeicon");

    if (menu.className === "navlista menujs") {
        menu.className = "navlista";
        //icon.innerHTML = "&#9776;";
    }
}

/* - - - - FIM - - - - */

/* - - - Slider - STop - - - */
$(function () {
    var curSlider = 0;
    var maxSlider7 = $('.STop').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.STop').hide(); //apaga todos os sliders
        $('.STop').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0
    }
    function changeSlide() {
        setInterval(function () {
            $('.STop').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider7)
                curSlider = 0;
            $('.STop').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 11000);
    }
})

//Você OnLine
$(function () {
    var curSlider = 0;
    var maxSlider = $('.bannerSlider').length - 1;
    var delay = 1;


    initSlider();
    changeSlide();

    function initSlider() {
        $('.bannerSlider').hide(); //apaga todos os sliders
        $('.bannerSlider').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        for (var i = 0; i <= maxSlider; i++) {
            var content = $('.bullets').html(); //pega o texto html e atribui para a variável
            if (i == 0) {
                content += '<span class="activeSlider"> </span>'; //adiciona as tags html desejadas
            } else {
                content += '<span> </span>'; //adiciona as tags html desejadas
            }
            $('.bullets').html(content); //atualiza o texto html com as novas tags
        }
    }

    function changeSlide() {
        setInterval(function () {
            $('.bannerSlider').eq(curSlider).stop().fadeOut(2000); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider)
                curSlider = 0;
            $('.bannerSlider').eq(curSlider).stop().fadeIn(2000); //fadeIn inicia a apresentação do slider
            //Trocar bullets da navegação do slider
            $('.bullets span').removeClass('activeSlider');
            $('.bullets span').eq(curSlider).addClass('activeSlider');
        }, delay * 5000);
    }

    //evento do click para mudar o slider de acordo com o bullet clicado
    //no body pega o evento click da classe definida
    $('body').on('click', '.bullets span', function () {
        var currentBullets = $(this); //pega õ conteúdo da classe
        $('.bannerSlider').eq(curSlider).stop().fadeOut(0); //fadeOut finaliza a apresentação do slider 
        curSlider = currentBullets.index(); //pega o index atual
        $('.bannerSlider').eq(curSlider).stop().fadeIn(0); //fadeIn inicia a apresentação do slider
        $('.bullets span').removeClass('activeSlider'); //remove a classe activeSlider de todos os span
        currentBullets.addClass('activeSlider'); //adiciona a classe activeSlider no span atual
    });
})


/* - - - Você OnLine - - - */

/* A ideia é ativar a View com JavaScript direto pelo LI. */
function ativarView() {
    alert("Quase lá");
}
function Mudarestado(el) {

    alert("Entrou");
    var display = document.getElementById(el).style.display;
    if (display == "none")
        document.getElementById(el).style.display = 'block';
    else
        document.getElementById(el).style.display = 'none';
}

function ActiveIndex(index) {
    $('view').hide(); // change this selector to be more specific
    $('#view' + index).show();
}

//Imprimir Páginas
/*
function PrintElem(elem, largura, titulo) {
    Popup($(elem).html(), largura, titulo);
}
function Popup(data, largura, titulo) {
    var mywindow = window.open('Imprimir', 'imprimir', 'width=' + 300 + ',height=800,scrollbars=yes');
    //mywindow.document.write('<div>Imprimir</div>');
    //*optional stylesheet
    //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body style="font-family:Courier New;font-size:12px;"><table align="center" style="font-size:18px;"><tr><td><img src="../../Img/Logo.png" /></td><td style="padding-left:20px;">' + "Título" + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" src="Img/Layout/btImprimir.jpg" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
    //*mywindow.print();
    mywindow.close();
    //*mywindow.close();

    //*return true;
}
*/
/* # # # # # B O X 33 # # # # # */

/* - - - Box33 - S6 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider6 = $('.BoxS6').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.BoxS6').hide(); //apaga todos os sliders
        $('.BoxS6').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0              
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS6').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider6)
                curSlider = 0;
            $('.BoxS6').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 11000);
    }
})

/* - - - Box33 - S5 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider5 = $('.BoxS5').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.BoxS5').hide(); //apaga todos os sliders
        $('.BoxS5').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0              
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS5').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider5)
                curSlider = 0;
            $('.BoxS5').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 11000);
    }
})

/* - - - Box33 - S4 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider4 = $('.BoxS4').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.BoxS4').hide(); //apaga todos os sliders
        $('.BoxS4').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0              
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS4').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider4)
                curSlider = 0;
            $('.BoxS4').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 11000);
    }
})

/* - - - Box33 - S3 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider3 = $('.BoxS3').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.BoxS3').hide(); //apaga todos os sliders
        $('.BoxS3').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0              
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS3').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider3)
                curSlider = 0;
            $('.BoxS3').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 11000);
    }
})

/* - - - Box33 - S2 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider2 = $('.BoxS2').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    /*S2*/
    function initSlider() {
        //percorre a qtde de slider para adicionar o botão de navegação do slider
        $('.BoxS2').hide(); //apaga todos os sliders
        $('.BoxS2').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0              
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS2').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider2)
                curSlider = 0;
            $('.BoxS2').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 9000);
    }
    //alert("Entrou!!!");
})
/* - - - Box33 - S1 - - - */
$(function () {
    var curSlider = 0;
    var maxSlider = $('.BoxS1').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    function initSlider() {
        $('.BoxS1').hide(); //apaga todos os sliders
        $('.BoxS1').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0
        //percorre a qtde de slider para adicionar o botão de navegação do slider
    }
    function changeSlide() {
        setInterval(function () {
            $('.BoxS1').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider)
                curSlider = 0;
            $('.BoxS1').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider

        }, delay * 10000);
    }
})
/* - - - - */

/* # # # # # # BOX 100 # # # # # # */

$(function () {
    var curSlider = 0;
    var maxSlider = $('.Box100').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();
    function initSlider() {
        $('.Box100').hide(); //apaga todos os sliders
        $('.Box100').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0                       
        for (var i = 0; i <= maxSlider; i++) {
        }
    }
    function changeSlide() {
        setInterval(function () {
            $('.Box100').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider)
                curSlider = 0;
            $('.Box100').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider                            
        }, delay * 10000);
    }
})
/* - - - FIM BOX100 - - - */

/* # # # Slider Guia de Convênios # # # */

/* - - - Slider - - - */
$(function () {
    var curSlider = 0;
    var maxSlider = $('.Slider').length - 1;
    var delay = 1;
    initSlider();
    changeSlide();

    function initSlider() {
        $('.Slider').hide(); //apaga todos os sliders
        $('.Slider').eq(0).show(); //eq é a posição e show é para apresentar. Apresentar o slider na posição 0
        //percorre a qtde de slider para adicionar o botão de navegação do slider
    }
    function changeSlide() {
        setInterval(function () {
            $('.Slider').eq(curSlider).stop().fadeOut(200); //fadeOut finaliza a apresentação do slider 
            curSlider++;
            if (curSlider > maxSlider)
                curSlider = 0;
            $('.Slider').eq(curSlider).stop().fadeIn(200); //fadeIn inicia a apresentação do slider
        }, delay * 10000);
    }
})

/* - - - FIM GUIA - - - */

/* - - - - FIM - - - - */

/* - - - -  Impressões - - - - */

function printBy(selector) {
    //alert("Você clicou para imprimir, se vai ou não são outros 500");

    var $print = $(selector)
        .clone()
        .addClass('print')
        .prependTo('div');


    // Stop JS execution
    window.print();
    //printable.print();
    // Remove div once printed
    $print.remove();
}

/* - - -  Impressão: Fim - - - */

/*<script type="text/javascript">*/
/*
function PrintElem(elem) {
    Popup(document.getElementById(elem).innerHTML);
}
*/
/* Exemplo
var win = window.open('','printwindow');
win.document.write('<html><head><title>Print it!</title><link rel="stylesheet" type="text/css" href="styles.css"></head><body>');
win.document.write($("#content").html());
win.document.write('</body></html>');
win.print();
win.close();
 */

/*
function Popup(data) {
    var mywindow = window.open('Comprovante', 'comprovante', 'width=900, height=600');
    mywindow.document.write('<html><head><title>Comprovante</title>');
    //optional stylesheet //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    //mywindow.document.write('<link rel="stylesheet" href="Css/Print.css" />')
    //mywindow.document.write('</head><body style="font-family:Courier New;font-size:12px;"><table style="font-size:8px;" > ');
    mywindow.document.write('</head><body>');
    
    mywindow.document.write(data);
    
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
    mywindow.print();
    mywindow.close();

    //mywindow.close();

    // Teste para alterar tamanho da Fonte, porém, é para alterar a fonte apenas na impressão
    //var printRelEntPos = document.querySelector("#lblPeriodo");
    //var printRelEntPre = document.querySelector("#lblRelEntregaPre");

    //printRelEntPos.style.fontSize = "20px";
    //printRelEntPre.style.fontSize = "16px";
    

    return true;
}
*/

function PrintElem(elem, largura, titulo) {
    Popup($(elem).html(), largura, titulo);
}

function Popup(data, largura, titulo) {
    var mywindow = window.open('Impressão', 'imprimir', 'width=' + largura + ',height=800,scrollbars=yes');    
    //mywindow.document.write('<link rel="stylesheet" href="Table-Extrato.css" type="text/css" />');/*optional stylesheet*/
    //mywindow.document.write('<link rel="\"stylesheet"" href="\Table-Extrato.css\"" type="\text/css\" />');/*optional stylesheet*/
    //mywindow.document.write('<link rel="\"stylesheet\"" href="\StyleVoceOnLine.css\"" type="\"text/css\"/" >');/*optional stylesheet*/
    //mywindow.document.write('<link rel="\"stylesheet\"" href = "\"PrintCss.css\"" type = "\"text/css\"/" > ' ); Exemplo

    mywindow.document.write('<html><head><title>Imprimir</title>');
    mywindow.document.write('</head><body style="font-family:Courier New;font-size:12px;"><table align="center" style="font-size:14px;"><tr><td><img src="../Img/Layout/asulogo.png" /></td><td style="padding-left:20px;">' + titulo + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" src="../Img/Layout/btImprimir.jpg" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
    /*mywindow.print();
    mywindow.close();*/
    /*mywindow.close();*/

    /*return true;*/
}

//Imprimir Extrato Associado
function PrintExtAssoc(elem, largura, titulo) {
    PopupExtAssoc($(elem).html(), largura, titulo);
}

function PopupExtAssoc(data, largura, titulo) {
    var mywindow = window.open('Imprimir', 'imprimir', 'width=' + largura + ',height=800,scrollbars=yes');
    mywindow.document.write('<html><head><title>Extrato Associado</title>');
    mywindow.document.write('</head><body style="font-family:Courier New;font-size:8px;"><table align="center" style="font-size:10px;"><tr style="font-size:10px;"><td style="font-size:10px;"><img src="../Img/Layout/asulogo.png" /></td><td style="padding-left:20px;">' + titulo + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" src="../Img/Layout/btImprimir.jpg" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();    
}

//Imprimir Relatório de entrega
function PrintRelEntrega(elem, largura, titulo) {
    PopupRelEntrega($(elem).html(), largura, titulo);
}

function PopupRelEntrega(data, largura, titulo) {
    var mywindow = window.open('Imprimir', 'imprimir', 'width=' + largura + ',height=800,scrollbars=yes');
    mywindow.document.write('<html><head><title>Extrato Associado</title>');
    mywindow.document.write('</head><body style="font-family:Courier New;font-size:8px;"><table align="center" style="font-size:10px;"><tr style="font-size:10px;"><td style="font-size:10px;"><img src="../Img/Layout/asulogo.png" /></td><td style="padding-left:20px;">' + titulo + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" src="../Img/Layout/btImprimir.jpg" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
}

//Imprimir Comprovante de Venda

function PrintComprovante(elem, largura, titulo) {
    PopupCV($(elem).html(), largura, titulo);
}

function PopupCV(data, largura, titulo) {
    var mywindow = window.open('Imprimir', 'imprimir', 'width=' + largura + ',height=800,scrollbars=yes');
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="Table-Extrato.css" type="text/css" />');
    mywindow.document.write('<html><head><title>Comprovante de Venda</title>');
    mywindow.document.write('</head><body style="width: 220px; font-family:Courier New; font-size: 8px "><table align="center" style="font-size:14px; font-weight: 700;"><tr><td></td><td style="padding-left:20px;">' + titulo + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" style="width: 40px;" src="../Img/Layout/btImprimir-3.png" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
    /*mywindow.print();
    mywindow.close();*/
    /*mywindow.close();*/

    /*return true;*/
}

// Fim - Impressão Comprovante


//Capturar o Tamanho do Monitor para executar no PC ou no celular
function resolucaoTela() {

    var largura = document.documentElement.clientWidth;    

    if (document.querySelector("#hfTamanhoTela").value != null) {
        document.querySelector("#hfTamanhoTela").value = largura;
    }

    //alert(largura);
}

function detectarResolucao() {
    
    resolucaoTela();
}

aceitarTermo.addEventListener("mousemove", function () {
    alert("Passou o Mouse!!!");
});

//#### Esse cara Não funcionou
function aceitarTermo() {    
    
    var desativa = document.querySelector("#termo");
    /*
    window.onsubmit = function () {
        desativa.style.display = 'none';
    }*/

    desativa.classList.remove('ativa');
    desativa.classList.add('desativa');


    preventDefault();
    alert("No caminho");
}
//##### Fim aceitaTermo

function test() {
    alert("Foi!");
}