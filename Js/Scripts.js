
/* Ativar Div Princípios*/

function ativarDivMenu(ativa) {
    if (ativa == 1) {
        divJSprincipos.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSarray.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSstring.style.display = "none";
        divJSdate.style.display = "none";
        divJSobject.style.display = "none";

    }
    if (ativa == 2) {
        divJSfuncoes.style.display = "block";
        divJSprincipos.style.display = "none";
        divJSarray.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSstring.style.display = "none";
        divJSdate.style.display = "none";
        divJSobject.style.display = "none";

    }
    if (ativa == 3) {
        divJSarray.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSprincipos.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSstring.style.display = "none";
        divJSdate.style.display = "none";
        divJSobject.style.display = "none";

    }
    if (ativa == 4) {
        divJSnumeros.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSprincipos.style.display = "none";
        divJSarray.style.display = "none";
        divJSstring.style.display = "none";
        divJSdate.style.display = "none";
        divJSobject.style.display = "none";
    }
    if (ativa == 5) {
        divJSstring.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSprincipos.style.display = "none";
        divJSarray.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSdate.style.display = "none";
        divJSobject.style.display = "none";

    }
    if (ativa == 6) {
        divJSdate.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSprincipos.style.display = "none";
        divJSarray.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSstring.style.display = "none";
        divJSobject.style.display = "none";

    }
    if (ativa == 7) {
        divJSobject.style.display = "block";
        divJSfuncoes.style.display = "none";
        divJSprincipos.style.display = "none";
        divJSarray.style.display = "none";
        divJSnumeros.style.display = "none";
        divJSstring.style.display = "none";
        divJSdate.style.display = "none";
    }
    if (ativa == 8) {
        divCSSprincipio.style.display = "block";
        divCSSObject.style.display = "none";
    }
    if (ativa == 9) {
        divCSSObject.style.display = "block";
        divCSSprincipio.style.display = "none";
    }
}

function ativarMenuWebAvancado(ativar) {

    if (ativar == 1) {
        divJSAdom.style.display = "block";        
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";        
    }
    if (ativar == 2) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "block";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == 3) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "block";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSAcss) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "block";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSAediversos) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "block";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSAform) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "block";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSArepet) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "block";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSArelogio) {
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "block";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJSAajax) {        
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "block";
        divJQWidgets.style.display = "none";
    }
    if (ativar == divJQWidgets) {        
        divJSAdom.style.display = "none";
        divJSAedoc.style.display = "none";
        divJSAaddevent.style.display = "none";
        divJSAcss.style.display = "none";
        divJSAediversos.style.display = "none";
        divJSAform.style.display = "none";
        divJSArepet.style.display = "none";
        divJSArelogio.style.display = "none";
        divJSAajax.style.display = "none";
        divJQWidgets.style.display = "block";
    }
}

//Ativar UL JavaScript
btnJSjavascript.addEventListener("click", function (e) {
    e.preventDefault();
    btnULjs.style.display = "block";
    btnJScss.style.display = "none";

    btnJQul.style.display = "none";
    btnULjsavancado.style.display = "none";


    secCSS.style.display = "none";
    secJS.style.display = "block";

});
btnCSSjs.addEventListener("click", function (e) {
    e.preventDefault();

    btnJScss.style.display = "block";
    btnULjs.style.display = "none";

    btnJQul.style.display = "none";
    btnULjsavancado.style.display = "none";

    secJS.style.display = "none";
    secCSS.style.display = "block";

});

//JavaScript Avançado
btnJSavancado.addEventListener("click", function (e) {
    e.preventDefault();
    btnULjsavancado.style.display = "block";
    btnJQul.style.display = "none";

    btnJScss.style.display = "none";
    btnULjs.style.display = "none";

    secJQavancado.style.display = "none";
    secJSavancado.style.display = "block";
});

btnjQueryAvancado.addEventListener("click", function (e) {
    e.preventDefault();

    btnJQul.style.display = "block";
    btnULjsavancado.style.display = "none";

    btnJScss.style.display = "none";
    btnULjs.style.display = "none";

    secJSavancado.style.display = "none";
    secJQavancado.style.display = "block";

});
// ######################################################

//const btnJSprincipos = document.querySelector("#btnJSprincipos");
//const divprincipos = document.querySelector("#divJSprincipos");


//Ativar Menus JS #################
btnJSprincipos.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(1);
});
btnJSfuncoes.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(2);
});
btnJSarray.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(3);
}
);
btnJSnumeros.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(4)
});
btnJSString.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(5);
});
btnJSDate.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(6);
});
btnJSObject.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(7);
});
//#### Ativar Divs Java Avançado ####
btnJSAdom.addEventListener("click", function (e) {
    e.preventDefault();    
    ativarMenuWebAvancado(1);    
});
btnJSAeventos.addEventListener("click", function (e) {
    e.preventDefault();    
    ativarMenuWebAvancado(2);
});
btnJSAevent.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(3);
});
btnJSAjscss.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSAcss);
});
btnJSAdiversos.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSAediversos);
});
btnJSAform.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSAform);
});
btnJSArepeticao.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSArepet);
});
btnJSArelogio.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSArelogio);
});
btnJSAajax.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJSAajax);
});
btnJQwid.addEventListener("click", function (e) {
    e.preventDefault();
    ativarMenuWebAvancado(divJQWidgets);
});
btnJQedocument.addEventListener("click", function (e) {
    e.preventDefault();
    alert("Aqui!!!");
});


/* Sessão CSS */

btnCSSprincipios.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(8);
});
btnCSSObject.addEventListener("click", function (e) {
    e.preventDefault();
    ativarDivMenu(9);
});

function iniciar() {
    btnTestes.addEventListener("click", function (e) {
        e.preventDefault();
        desativarDiv("none");
    });
}

window.onload = iniciar;


btnExcutar.addEventListener("click", function (e) {
    e.preventDefault();

    var txt = iJsNome.value;


    lblResp.innerHTML = iJsNome.value;

    iJsNome.value = "";
});

/* Funções */

/*Função de execução*/

btnExecutarfExec.addEventListener("click", function (e) {
    e.preventDefault();

    lblRespFuncoes.innerHTML = iNomefExec.value;
    iNomefExec.value = "";
});

/*Função de retorno*/
btnExecutarfRet.addEventListener("click", function (e) {
    e.preventDefault();

    var celsius = document.querySelector("#iTemperaturaRet").value;

    function converterTemperatura(celsius) {
        var fah = (1.8 * celsius) + 32;
        return fah
    }

    var ret = converterTemperatura(celsius);

    lblRespFuncoes.innerHTML = ret + "F";

});

/*Função com Parametro*/

btnExecutarfPar.addEventListener("click", function (e) {
    e.preventDefault();

    var dias = document.querySelector("#iDias").value;
    var salario = document.querySelector("#iSalario").value;


    function calcularSalario(salario, dias) {

        return (salario / dias).toFixed(2);
    }

    var diaria = calcularSalario(salario, dias);

    lblRespFuncoes.innerHTML = "Valor da Diária: R$" + diaria;

});

/* Array */

/*Mostrar uma Lista de Item: Array*/
btnExecutarArray.addEventListener("click", function (e) {
    e.preventDefault();

    var lista = new Array("Item 1", "Item 2", "Item 3");
    var lista2 = ["Um", "Dois", "Três"];
    var lista3 = new Array();
    lista3[0] = "Produto 1";
    lista3[1] = "Produto 2";
    lista3[2] = "Produto 3";

    lblRespArray.innerHTML = "Array 1: " + lista + "\n\n" + " - Array 2: " + lista2 + "\n\n" + " Lista 3: " + lista3;
});

/*Adicionar Item num Array*/

btnAdicionarArray.addEventListener("click", function (e) {
    e.preventDefault();

    var lista = new Array();
    lista[0] = "Produto 1";
    lista[1] = "Produto 2";
    lista[2] = "Produto 3";

    lista.push(iTextArray.value);

    lblRespArray.innerHTML = "Lista de Produtos: " + lista;
    iTextArray.value = "";
});

/* Remover Item de Um Array */

btnRemoveArray.addEventListener("click", function (e) {
    e.preventDefault();

    var item = ["Item 1", "Item 2 ", "Item 3"];

    var lista = item;

    item.pop();

    lblRespArray.innerHTML = " Nova Lista de Itens: " + item;
});

/* Pesquisar Elemento */

btnPesquisaArray.addEventListener("click", function (e) {
    e.preventDefault();

    var item = ["Item 1", "Item 2", "Item 3"];

    var itemPesquisa = document.querySelector("#iPesquisaArray").value;

    var pesquisa = item.indexOf(itemPesquisa);

    if (pesquisa >= 0) {

        lblRespArray.innerHTML = "Está na Posição: " + item.indexOf(itemPesquisa);
    } else {
        lblRespArray.innerHTML = "Item não encontrado!!!";
    }

});

/* Ordenar Elementos */

function ordenarNumeros(a, b) {
    return a - b;
}

btnOrdenarArray.addEventListener("click", function (e) {
    e.preventDefault();

    var megasena = Array(1, 7, 3, 24, 40, 33);

    /*
     * O Short executa uma Ordenação String
     * Ordenação numérica: Para Ordenar numericamente é preciso criar uma função "de comparação", exemplo: a-b. Desta forma, a ordenação é feita do menor para o maior*/

    megasena.sort(ordenarNumeros);

    lblRespArray.innerHTML = megasena;
});

/*Números*/
btnCalcularNum.addEventListener("click", function (e) {
    e.preventDefault();

    var n1 = document.querySelector("#iNum1").value;
    var n2 = document.querySelector("#iNum2").value;

    lblResultNum.innerHTML = "Soma: " + (parseFloat(n1) + parseFloat(n2)) + ", Multiplicação: " + (n1 * n2) + ", Divisão: " + (n1 / n2) + " , Subtração: " + (n1 - n2);
});

/*Sortear Mega-Sena*/

btnSortearMega.addEventListener("click", function (e) {
    e.preventDefault();

    var sorteio;
    var mega = new Array();

    for (var i = 0; i < 6; i++) {

        sorteio = Math.ceil(Math.random() * 60);

        if (mega.indexOf(sorteio) < 0) {
            mega.push(sorteio);
        }

    }

    if (mega.length < 6) {
        lblResultNum.innerHTML = "Faça Outro Sorteio, este zicou!";
    } else {
        mega.sort(ordenarNumeros);
        lblResultNum.innerHTML = "Números Sorteados: " + mega.length + " , Sequência: " + mega;
    }
});

/*String*/

/*charAt*/
btncharAt.addEventListener("click", function (e) {
    e.preventDefault();

    var frase = document.querySelector("#iFrase").value;
    var letra = "";
    var primeiraPalavra = "";
    var i = 0;

    while (letra != " ") {
        letra = frase.charAt(i);
        primeiraPalavra += letra;
        i++;
    }

    lblResultString.innerHTML = primeiraPalavra;
    iFrase.value = "";

});
/*subStr*/

btnsubStr.addEventListener("click", function (e) {
    e.preventDefault();
    var frase = iFrase.value;
    var primeiraPalavra = "";
    var letra = ""
    var i = 0;

    while (letra != " ") {
        letra = frase.charAt(i);
        primeiraPalavra += letra;
        i++;
    }

    /*
    if (iFrase.value === "" || ifras.value === null) {
        lblResultString.innerHTML = "Digite uma Palavra ou Frase para continuar...";
    } else {
        while (letra != " ") {
            letra = frase.charAt(i);
            primeiraPalavra += letra;
            i++;
        }
    }
    /*
    if (iFrase.value === "" || ifrase.value === null) {
        lblResultString.innerHTML = "Está Vazio!!!";
    } else {
        lblResultString.innerHTML = "Está Preenchido!!!"
    }
    */
    var palavra = frase.substr(0, primeiraPalavra.length);

    lblResultString.innerHTML = palavra;
});

/*Split*/

btnsplit.addEventListener("click", function (e) {
    e.preventDefault();

    var frase = iFrase.value;

    var primeiraPalavra = frase.split(" ")[0];

    lblResultString.innerHTML = primeiraPalavra;
});

btnLowerCase.addEventListener("click", function (e) {
    e.preventDefault();
    var frase = iFrase.value;
    //var frase = "MINUSCULA";
    var minus = frase.toLowerCase();

    lblResultString.innerHTML = minus;

});

btnUpperCase.addEventListener("click", function (e) {
    e.preventDefault();

    var frase = iFrase.value;
    //var frase = "maiuscula";
    var maius = frase.toUpperCase();

    lblResultString.innerHTML = maius;
});

/*Date*/

// Descobrir Como carregar data atual no calendário
iCalendar.addEventListener("onload", function (e) {

    var hoje = new Date();

    iCalendar.innerHTML = hoje.getUTCDate();

});
/*Dia*/
btnDia.addEventListener("click", function (e) {
    e.preventDefault();

    var dia = ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"];
    var agora = new Date();

    lblResultDate.innerHTML = dia[agora.getDay()];
});
/*Mês*/
btnMes.addEventListener("click", function (e) {
    e.preventDefault();
    var mes = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
    var agora = new Date();

    lblResultDate.innerHTML = mes[agora.getMonth()];
});
/*Ano*/
btnAno.addEventListener("click", function (e) {
    e.preventDefault();

    var agora = new Date();
    lblResultDate.innerHTML = agora.getFullYear();
});
/*Data Selecionada no Calendário*/
btnData.addEventListener("click", function (e) {
    e.preventDefault();

    var agora = new Date();

    lblResultDate.innerHTML = iCalendar.value;
});

/*Dia de nascimento*/
btnDiaNascimento.addEventListener("click", function (e) {
    e.preventDefault();

    var dia = ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"];

    var agora = new Date();
    var diaNascimento = new Date(iCalendar.value);

    var diaCalendar = dia[agora.getDay(iCalendar.value)];
    var nascimento = dia[diaNascimento.getDay(iCalendar.value)];
    
    /*

    if (parseInt(diaNascimento.getDay(iCalendar.value)) === 0 || parseInt((diaNascimento.getDay(iCalendar.value)) + 1) === 6) {
    //if (1 === 1) {
        lblResultDate.innerHTML = iCalendar.value + " Você nasceu num: " + nascimento;
        alert("Sábado ou Domingo" + " - " + diaNascimento.getDay(iCalendar.value));

    } else {
        lblResultDate.innerHTML = iCalendar.value + " Você nasceu numa: " + nascimento;
        alert("Dia da Semana" + " - " + diaNascimento.getDay(iCalendar.value));
    }
    
    //lblResultDate.innerHTML = iCalendar.value + " Você nasceu: " + nascimento;
    */
    lblResultDate.innerHTML = iCalendar.value + " getDay: " + diaNascimento.getDay(iCalendar.value) + " getDate: " + (diaNascimento.getDate(iCalendar.value) + 1) + "-" + diaNascimento.getMonth(iCalendar.value) +" - "+ diaNascimento.getFullYear(iCalendar.value);

// É o seguinte, o dia da semana é pego com um a menos, ou seja, dia 26 ele pega 25.

});

/* # # # Object # # # */

btnObject.addEventListener("click", function (e) {
    e.preventDefault();

    //alert(" + ");
    var Produto = new Object();

    Produto.nome = "Caneta";
    Produto.preco = "3.50";
    Produto.cor = "Azul";

    function retornarInfo() {
        return "Produto: " + this.nome + ", custa: " + this.preco + " na cor, " + this.cor;
    }

    Produto.info = retornarInfo;

    alert(Produto.info());


    lblResultObject.innerHTML = Produto.info();
});

