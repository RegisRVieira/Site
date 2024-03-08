<?php
    include('conexaoDB.php');

    $postjson  = json_decode(file_get_contents('php://input'), true);
            
    $cpf       = $postjson['cpf']; 
    $cartao    = $postjson['cartao'];      
    $dados;  

    $dadosSaldo = SaldoCartao::saldoCartaoPre($cpf, $cartao, 'WHATS');
    if($dadosSaldo['erro'] != ''){
      $dados = array(
        'titular' => '*' . $dadosSaldo['erro'] . '*',
        'saldo'   => ''
      ); 
    } else {
      $dados = array(
        'titular' => 'Olá *' . $dadosSaldo['dependente'] . '*!',
        'saldo'   => 'Seu saldo é de *R$* *' . $dadosSaldo['saldo'] . '*'
      );      
    }

    $result = json_encode(array('results' => $dados));
    echo $result;  
?>



