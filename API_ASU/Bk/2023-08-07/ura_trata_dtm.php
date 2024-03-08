<?php
    include('config.php'); 

    $postjson                = json_decode(file_get_contents('php://input'), true);
    $menu                    = $_REQUEST['menu'];
    $urlConsultaSaldoCartao  = 'http://www.asu.com.br/API_ASU/ura_assoc_saldocartao.php';
    $urlConsultaValeAlimento = 'http://www.asu.com.br/API_ASU/ura_assoc_valealimento.php';
    $urlMenuPrincipal        = 'http://www.asu.com.br/API_ASU/uracentral.php';
    $urlMenuCartao           = 'http://www.asu.com.br/API_ASU/ura_menucartao.php';

    $dados      = array();
    $cartao     = '';
    $informacao = $postjson['ultimo_dtmf']; 

    if ($menu == 'ConsSaldo_dadosCartao')  { //Consulta Saldo Cartão 
    	$dados_dinamico['acao'] = 'dinamico';
		$dados_dinamico['acao_dados']['url'] = $urlMenuCartao.'?menu=ConsSaldo_SolicitaCPF&cartao='.$informacao;
	} else if ($menu == 'ConsSaldo_dadosCPF')  { // Consulta Saldo CPF 
		$cartao = $_REQUEST['cartao'];
		$dados_dinamico['acao'] = 'dinamico';
		$dados_dinamico['acao_dados']['url'] = $urlConsultaSaldoCartao.'?tipoConsulta=SALDOCARTAO&cartao='.$cartao.'&cpf='.$informacao;

	} else if ($menu == 'ValeCompra_dadosCartao')  { //Vale Compras Cartão
    	$dados_dinamico['acao'] = 'dinamico';
		$dados_dinamico['acao_dados']['url'] = $urlMenuCartao.'?menu=ValeCompra_SolicitaCPF&cartao='.$informacao;
	} else if ($menu == 'ValeCompra_dadosCPF')  { //Vale Compras CPF
		$cartao = $_REQUEST['cartao'];
		$dados_dinamico['acao'] = 'dinamico';
		$dados_dinamico['acao_dados']['url'] = $urlConsultaSaldoCartao.'?tipoConsulta=VALECOMPRA&cartao='.$cartao.'&cpf='.$informacao;
	} 
    array_push($dados, $dados_dinamico);

	$menu_resposta = array(
		'id'    => '42158',
	    'nome'  => 'Menu Principal',
	    'dados' => $dados
	);

	sleep(2); 
	header('Content-Type: application/json');
	die(json_encode($menu_resposta));
?>