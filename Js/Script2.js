function controleFormulario(e) {
    e.preventDefault();

    var caixa_nome = iNome.value;

    if (caixa_nome == "") {
        mensagem.innerHTML = "Por Favor, Preencha o nome...";
    } else {
        mensagem.innerHTML = caixa_nome;
    }
}

function limparMensagem() {
    mensagem.innerHTML = "";
}

function mostrarCidade() {

    iEscolherCidade.onclick = function () {

        //alert("Clicou");

        if (iEscolherCidade.checked) {
            iCidade.style.display = "block";
            //alert("True");
        } else {
            iCidade.style.display = "none";
            //alert("False");
        }
    }
    iCidade.style.display = "none";
}


function iniciar() {
    frmFormInteligente.onsubmit = controleFormulario;
    iNome.onfocus = limparMensagem;
    mostrarCidade();    
}

window.onload = iniciar;

    