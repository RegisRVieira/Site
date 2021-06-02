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

function AcessarVoceOnLine() {
    window.location.href = "LoginVoceOnLine.aspx";
}

jQuery(document).ready(function (e) {

});
//17-02-2021, depois de muito custo.
function abrirImagens() {
    window.open("S-Imagens.aspx", "minhaJanela", "width= 950, height= 600 ");
}


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

function PrintElem(elem, largura, titulo) {
    Popup($(elem).html(), largura, titulo);
}
function Popup(data, largura, titulo) {
    var mywindow = window.open('Imprimir', 'imprimir', 'width=' + 300 + ',height=800,scrollbars=yes');
    //mywindow.document.write('<div>Imprimir</div>');
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body style="font-family:Courier New;font-size:12px;"><table align="center" style="font-size:18px;"><tr><td><img src="../../Img/Logo.png" /></td><td style="padding-left:20px;">' + "Título" + '</td><td><a href="#" onclick=document.getElementById("btImpressao").style.display="none";window.print();window.close();><img id="btImpressao" src="Img/Layout/btImprimir.jpg" border="0" style="padding-left:40px;cursor:pointer;" /></a></td></tr></table><br><br><br>');

    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();
    /*mywindow.print();
    mywindow.close();*/
    /*mywindow.close();*/

    /*return true;*/
}
