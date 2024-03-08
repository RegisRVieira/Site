<?php
    include('config.php'); 

    $postjson         = json_decode(file_get_contents('php://input'), true);
    $dados            = Array(); 
    $urlMenuPrincipal = 'https://www.asu.com.br/API_ASU/uracentral.php';
    $urlMenuCartao    = 'https://www.asu.com.br/API_ASU/ura_menucartao.php';

    $dados_tts['timeout'] = 6;
    $dados_tts['opcao']   = '';
    $dados_tts['acao']    = 'tts';      
    $dados_tts['acao_dados']['mensagem'] = 'Olá, Você está na ASSU!!!! Digite um, Saldo Cartão ASSU Online. ou dois, Saldo Cartão Vale Compras.';

    // 1 - Saldo Cartão ASU 
    $menu_1['opcao'] = '1';
    $menu_1['acao']  = 'gotoMenu';
    $menu_1['acao_dados']['menu'] = 'MenuSaldoCartao';
    $menu_SaldoCartao['menu'] = 'MenuSaldoCartao'; 
    $menu_SaldoCartao['acao'] = 'dinamico';
    $menu_SaldoCartao['acao_dados']['url'] = $urlMenuCartao.'?menu=ConsSaldo_SolicitaCartao';

    // 2 - Vale Compras
    $menu_2['opcao'] = '2';
    $menu_2['acao']  = 'gotoMenu'; 
    $menu_2['acao_dados']['menu'] = 'MenuValeComras';
    $menu_ValeCompras['menu'] = 'MenuValeComras'; 
    $menu_ValeCompras['acao'] = 'dinamico';
    $menu_ValeCompras['acao_dados']['url'] = $urlMenuCartao.'?menu=ValeCompra_SolicitaCartao';

    // 3 - opção inválida
    $menu_3['opcao'] = '3';
    $menu_3['acao']  = 'gotoMenu';  
    $menu_3_acao['acao']    = 'dinamico';
    $menu_3_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 4 - opção inválida
    $menu_4['opcao'] = '4';
    $menu_4['acao']  = 'gotoMenu';  
    $menu_4_acao['acao']    = 'dinamico';
    $menu_4_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 5 - opção inválida
    $menu_5['opcao'] = '5';
    $menu_5['acao']  = 'gotoMenu';  
    $menu_5_acao['acao']    = 'dinamico';
    $menu_5_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 6 - opção inválida
    $menu_6['opcao'] = '6';
    $menu_6['acao']  = 'gotoMenu'; 
    $menu_6_acao['acao']    = 'dinamico';
    $menu_6_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 7 - opção inválida
    $menu_7['opcao'] = '7';
    $menu_7['acao']  = 'gotoMenu';  
    $menu_7_acao['acao']    = 'dinamico';
    $menu_7_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 4 - opção inválida
    $menu_8['opcao'] = '8';
    $menu_8['acao']  = 'gotoMenu';  
    $menu_8_acao['acao']    = 'dinamico';
    $menu_8_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 9 - opção inválida
    $menu_9['opcao'] = '9';
    $menu_9['acao']  = 'gotoMenu';   
    $menu_9_acao['acao']    = 'dinamico';
    $menu_9_acao['acao_dados']['url'] = $urlMenuPrincipal;

    // 0 - opção inválida
    $menu_0['opcao'] = '0';
    $menu_0['acao']  = 'gotoMenu';  
    $menu_0_acao['acao']    = 'dinamico';
    $menu_0_acao['acao_dados']['url'] = $urlMenuPrincipal;

    array_push($dados, $dados_tts);
    array_push($dados, $menu_1);
    array_push($dados, $menu_SaldoCartao);
    array_push($dados, $menu_2);
    array_push($dados, $menu_ValeCompras); 
    array_push($dados, $menu_3);
    array_push($dados, $menu_4);
    array_push($dados, $menu_5);
    array_push($dados, $menu_6);
    array_push($dados, $menu_7);
    array_push($dados, $menu_8);
    array_push($dados, $menu_9);
    array_push($dados, $menu_0);   

    // Monto a array de saída
    $menu_resposta = array(
        'id' => '42158',
        'nome' => 'Menu Principal',
        'dados' => $dados
    );

    header('Content-Type: application/json'); 
    die(json_encode($menu_resposta)); 
?>
