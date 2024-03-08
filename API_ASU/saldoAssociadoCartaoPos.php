<?php
    include('conexaoDB.php');

    $postjson  = json_decode(file_get_contents('php://input'), true); 

    $cpf    = $postjson['cpf']; 
    $cartao = $postjson['cartao'];
    $msgRet = '';
    $msgRetSaldo = '';
      
    $dadosSaldo = SaldoCartao::saldoCartaoPos($cpf, $cartao, 'WHATS');
    
    $saldodependente = 0;
    $saldotitular    = 0;
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
      $dados = array(
        'titular' => '*' . $dadosSaldo['erro'] . '*',
        'saldo'   => ''
      );  
    } else if( $saldodependente <= $saldotitular){
      $dados = array(
        'titular' => 'Bem vindo, *' . $dadosSaldo['dependente'] . '*!',
        'saldo'   => 'Seu saldo é de *R$* *' . $dadosSaldo['saldodependente'] . '*' 
      );
    } else {
      $msgRet = ' Bem vindo, *' . $dadosSaldo['dependente'] . '*! Seu saldo é de: *R$* *' . $dadosSaldo['saldodependente'] . '*. Porém o saldo do titular, *' . $dadosSaldo['titular'] . '*, é de: *R$* *' . $dadosSaldo['saldotitular'] . '*.';
      $msgRetSaldo = 'Por isto, *R$* *'. $dadosSaldo['saldotitular'] . '* é o valor disponível para suas compras.' ;

      $dados = array(
        'titular' => $msgRet,
        'saldo'   => $msgRetSaldo
      );      
    }
              
    $result = json_encode(array('results' => $dados));
    echo $result;  
?>



