<?php
    include('config.php'); 

    $postjson         = json_decode(file_get_contents('php://input'), true);
    $menu             = $_REQUEST['menu'];
    
    $lengthCartao     = 9;
    $dados            = array();
    $urlTratadtm      = 'http://www.asu.com.br/API_ASU/ura_trata_dtm.php';

    if ($menu == 'ConsSaldo_SolicitaCartao')  { //Consulta Saldo Cartão Cartão
      $menu_SaldoCartao['acao'] = 'tts';
      $menu_SaldoCartao['acao_dados']['mensagem'] = 'Digite os últimos seis números do seu Cartão ASSU Online';
      $menu_SaldoCartao['coletar_dtmf'] = 6; 
      $menu_SaldoCartao['timeout'] = '20';

      $acao_SaldoCartao['acao'] = 'dinamico';
      $acao_SaldoCartao['acao_dados']['url'] =  $urlTratadtm.'?menu=ConsSaldo_dadosCartao';

      array_push($dados, $menu_SaldoCartao);
      array_push($dados, $acao_SaldoCartao);

    } else  if ($menu == 'ConsSaldo_SolicitaCPF')  { //Consulta Saldo Cartão CPF
      $cartao = $_REQUEST['cartao'];
       
      $menu_SaldoCartao['acao'] = 'tts';
      $menu_SaldoCartao['acao_dados']['mensagem'] = 'Digite os três últimos números do seu CPF';
      $menu_SaldoCartao['coletar_dtmf'] = 3; 
      $menu_SaldoCartao['timeout'] = '20';

      $acao_SaldoCartao['acao'] = 'dinamico';
      $acao_SaldoCartao['acao_dados']['url'] =  $urlTratadtm.'?menu=ConsSaldo_dadosCPF&cartao='.$cartao;

      array_push($dados, $menu_SaldoCartao);
      array_push($dados, $acao_SaldoCartao);
    } else if ($menu == 'ValeCompra_SolicitaCartao')  {  //Consulta Vale Compras Cartão
      $menu_SaldoCartao['acao'] = 'tts';
      $menu_SaldoCartao['acao_dados']['mensagem'] = 'Digite os últimos seis números do seu Cartão ASSU Online';
      $menu_SaldoCartao['coletar_dtmf'] = 6; 
      $menu_SaldoCartao['timeout'] = '20';

      $acao_SaldoCartao['acao'] = 'dinamico';
      $acao_SaldoCartao['acao_dados']['url'] =  $urlTratadtm.'?menu=ValeCompra_dadosCartao';

      array_push($dados, $menu_SaldoCartao);
      array_push($dados, $acao_SaldoCartao);

    } else  if ($menu == 'ValeCompra_SolicitaCPF')  {  //Consulta Vale Compras CPF
      $cartao = $_REQUEST['cartao'];
       
      $menu_SaldoCartao['acao'] = 'tts';
      $menu_SaldoCartao['acao_dados']['mensagem'] = 'Digite os três últimos números do seu CPF';
      $menu_SaldoCartao['coletar_dtmf'] = 3; 
      $menu_SaldoCartao['timeout'] = '20';

      $acao_SaldoCartao['acao'] = 'dinamico';
      $acao_SaldoCartao['acao_dados']['url'] =  $urlTratadtm.'?menu=ValeCompra_dadosCPF&cartao='.$cartao;

      array_push($dados, $menu_SaldoCartao);
      array_push($dados, $acao_SaldoCartao);
    }

    // Monto a array de saída
    $menu_resposta = array(
        'id'    => '42158',
        'nome'  => 'Menu Principal',
        'dados' => $dados
    );

    header('Content-Type: application/json'); 
    die(json_encode($menu_resposta)); 
?>
