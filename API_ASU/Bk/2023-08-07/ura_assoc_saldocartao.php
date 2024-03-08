<?php
    include('config.php'); 

    $postjson           = json_decode(file_get_contents('php://input'), true);
    $urlConsultaSaldo   = 'http://www.asu.com.br/API_ASU/URAconsultaSaldoAssociado.php';
    $urlMenuPrincipal   = 'http://www.asu.com.br/API_ASU/uracentral.php';
    $url_finalizar      = 'http://www.asu.com.br/API_ASU/url_finalizar.php';

    $dados        = array();
    $cartao       = $_REQUEST['cartao']; 
    $cpf          = $_REQUEST['cpf']; 
    $tipoConsulta = $_REQUEST['tipoConsulta']; 

    if($cartao == ''){      
      $dados_retMenu['timeout'] = '2';  
      $dados_retMenu['acao']    = 'tts';
      $dados_retMenu['acao_dados']['mensagem'] = 'Cartão não informado!'; 

      $dados_dinamico['acao'] = 'dinamico';
      $dados_dinamico['acao_dados']['url'] = $urlMenuPrincipal;
      array_push($dados, $dados_retMenu);
      array_push($dados, $dados_dinamico);
    } else if($cpf == ''){      
      $dados_retMenu['timeout'] = '2';  
      $dados_retMenu['acao']    = 'tts';
      $dados_retMenu['acao_dados']['mensagem'] = 'CPF não informado!'; 

      $dados_dinamico['acao'] = 'dinamico';
      $dados_dinamico['acao_dados']['url'] = $urlMenuPrincipal;
      array_push($dados, $dados_retMenu);
      array_push($dados, $dados_dinamico);
    }else if( ($cartao != '') & ($cpf != '') & ($tipoConsulta == 'SALDOCARTAO')) { 
      //$dadosSaldo = SaldoCartao::saldoCartaoPos($cpf, $cartao, 'URA');
      $dadosSaldo = SaldoCartao::saldoCartaoPos($cpf, $cartao, 'WHATS');

      $saldodependente = $dadosSaldo['saldodependente'];
      $saldotitular    = $dadosSaldo['saldotitular'];

      //tira os pontos. depois substitui a virgula pelo ponto
      $saldodependente = str_replace(".","",$saldodependente); 
      $saldodependente = str_replace(",",".",$saldodependente);
      $saldodependente = floatval($saldodependente); 

      //tira os pontos. depois substitui a virgula pelo ponto
      $saldotitular = str_replace(".","",$saldotitular);
      $saldotitular = str_replace(",",".",$saldotitular);
      $saldotitular = floatval($saldotitular);   

      if($dadosSaldo['erro'] != ''){
        $msgRet = $dadosSaldo['erro'];
      } else {
        if($saldodependente <= $saldotitular ){
          $msgRet = 'Bem vindo, ' . $dadosSaldo['dependente'] . '!. Seu saldo é de: R$ ' . $dadosSaldo['saldodependente'] ;
        } else {
          $msgRet = 'Bem vindo, ' . $dadosSaldo['dependente'] . '! Seu saldo é de: R$ ' . $dadosSaldo['saldodependente'] . '. Porém o saldo do titular,' . $dadosSaldo['titular'] . ', é de: R$ ' . $dadosSaldo['saldotitular'] . 'Por isto, R$ '. $dadosSaldo['saldotitular'] . ' é o valor disponível para suas compras.';
        }               
      }
      $dados_cartao['timeout'] = '2';
      $dados_cartao['acao']    = 'tts';
      $dados_cartao['opcao']   = '';
      $dados_cartao['acao_dados']['mensagem'] = $msgRet;

      $dados_retMenu['timeout'] = '10';  
      $dados_retMenu['acao']    = 'tts';
      $dados_retMenu['acao_dados']['mensagem'] = 'Digite um, para voltar ao menu principal. ou dois,  para encerrar a ligação.'; 

      $menu_1['opcao'] = '1';
      $menu_1['acao']  = 'gotoMenu';
      $menu_1['acao_dados']['menu'] = 'MenuPrincipal';
      $menu_1_acao['menu'] = 'MenuPrincipal'; 
      $menu_1_acao['acao'] = 'dinamico';
      $menu_1_acao['acao_dados']['url'] = $urlMenuPrincipal;

           
      $menu_2['opcao'] = '2';
      $menu_2['acao']  = 'gotoMenu';
      $menu_2['acao_dados']['menu'] = 'MenuEncerrar';
      $menu_2['menu']      = 'MenuPrincipal'; 
      $menu_2_acao['acao'] = 'dinamico';
      $menu_2_acao['acao_dados']['url'] = $url_finalizar;

      array_push($dados, $dados_cartao);
      array_push($dados, $dados_retMenu);
      array_push($dados, $menu_1);
      array_push($dados, $menu_1_acao);
      array_push($dados, $menu_2);
      array_push($dados, $menu_2);
    }else if( ($cartao != '') & ($cpf != '') & ($tipoConsulta == 'VALECOMPRA')) { 

      $dadosSaldo = SaldoCartao::saldoCartaoPre($cpf, $cartao, 'WHATS');

      if($dadosSaldo['erro'] != ''){
        $msgRet = $dadosSaldo['erro'];
      } else {
        $msgRet = 'Bem vindo' . $dadosSaldo['dependente'] . '!. Seu saldo é de: R$ ' . $dadosSaldo['saldo'] ;
      } 

      $dados_cartao['timeout'] = '2';
      $dados_cartao['acao']    = 'tts';
      $dados_cartao['opcao']   = '';
      $dados_cartao['acao_dados']['mensagem'] = $msgRet;

      $dados_retMenu['timeout'] = '10';  
      $dados_retMenu['acao']    = 'tts';
      $dados_retMenu['acao_dados']['mensagem'] = 'Digite um, para voltar ao menu principal. ou dois,  para encerrar a ligação.'; 

      $menu_1['opcao'] = '1';
      $menu_1['acao']  = 'gotoMenu';
      $menu_1['acao_dados']['menu'] = 'MenuPrincipal';
      $menu_1_acao['menu'] = 'MenuPrincipal'; 
      $menu_1_acao['acao'] = 'dinamico';
      $menu_1_acao['acao_dados']['url'] = $urlMenuPrincipal;

           
      $menu_2['opcao'] = '2';
      $menu_2['acao']  = 'gotoMenu';
      $menu_2['acao_dados']['menu'] = 'MenuEncerrar';
      $menu_2['menu']      = 'MenuPrincipal'; 
      $menu_2_acao['acao'] = 'dinamico';
      $menu_2_acao['acao_dados']['url'] = $url_finalizar;

      array_push($dados, $dados_cartao);
      array_push($dados, $dados_retMenu);
      array_push($dados, $menu_1);
      array_push($dados, $menu_1_acao);
      array_push($dados, $menu_2);
      array_push($dados, $menu_2);      
    }   
    
    $menu_resposta = array(
      'id'    => '42158',
      'nome'  => 'Volta menu Inicial',
      'dados' => $dados
    );

    header('Content-Type: application/json'); 
    die(json_encode($menu_resposta));   
?>
