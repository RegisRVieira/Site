<?php
	class SaldoCartao{		
        //retorna saldo do cartão pós pago
        public static function saldoCartaoPos($cpf, $cartao, $tipochamada){

          $diamesini    = DIAMESINI;
          $diamesfin    = DIAMESFIN;   
          $nSaldo       = 0;
          $nSaldoAssoc  = 0;
          $dataAtual    = date('Y-m-d');
          $dia          = date('d');
          $cartaoSemDig = 0;
          $nTamCart     = 0;
          $dados; 

          if( $dia <= $diamesfin){        
            $dDataIni = date("Y-m-$diamesini", strtotime(' - 1 month'));
          }else{
            $dDataIni = date("Y-m-$diamesini");
          }

          if( $dia <= $diamesfin){
            $dDataFin = date("Y-m-$diamesfin");
          } else {
            $dDataFin = date("Y-m-$diamesfin", strtotime(' + 1 month'));
          }      
          
          $sSql = " SELECT assoc.idassoc, assoc.titular, assoc.credito as credassoc, assoc.credmodelo, car.credito as credcartao, dep.nome as dependente
                    FROM asdepen dep  
                      INNER JOIN asdepcar car ON car.dependen = dep.iddepen
                      INNER JOIN associa assoc ON assoc.idassoc = dep.associado
                    WHERE dep.cnscanmom is null 
                      AND car.cnscanmom is null                  
                      AND car.dt_fim is null 
                      AND assoc.cnscanmom is null  ";
          if($cpf != ''){
            $sSql = $sSql . ' AND SUBSTRING(dep.cnpj_cpf, 9, LENGTH(dep.cnpj_cpf) ) = :cnpj_cpf ';
          }
          if( ($cartao != '') && ($tipochamada == 'WHATS') ){
            $sSql = $sSql . ' AND SUBSTRING(car.idcartao, 4, LENGTH(car.idcartao) )  = :cartao ';
          } else { //ura
            $sSql = $sSql . ' AND car.idcartao = :cartao ';
          } 

          $query = DB_Conexao::conectar()->prepare($sSql);        
          if($cpf != ''){
            $query->bindValue('cnpj_cpf', $cpf);   
          }     
          if($cartao != ''){   
            $nTamCart      = (strlen($cartao) - 2);
            $cartaoSemDig  = substr($cartao, 0, $nTamCart);                     
            $query->bindValue('cartao', $cartaoSemDig); 
          }    

          try
          {
            $query->execute();
            $viewAssoc = $query->fetch();
            if(!empty($viewAssoc)){
              $titular   = $viewAssoc['titular'];

              // SQL referente a tabela de movimentação dos convênios para associado ( GLOBAL)
              $sSql = " SELECT SUM(valor) AS valor 
                        FROM comovime
                        WHERE associado = :associado
                          AND cnscanmom is null  ";
              if($viewAssoc["credmodelo"] == 'VLTOTAL'){
                $sSql = $sSql . " AND vencimento >= :dDataIni ";
              }else{
                $sSql = $sSql . " AND vencimento BETWEEN :dDataIni and :dDataFin " ;
              }
              $query = DB_Conexao::conectar()->prepare($sSql);
              $query->bindValue('associado', $viewAssoc["idassoc"]);
              if($viewAssoc["credmodelo"] == 'VLTOTAL'){
                $query->bindValue('dDataIni', $dDataIni); 
              }else{
                $query->bindValue('dDataIni', $dDataIni);
                $query->bindValue('dDataFin', $dDataFin); 
              }        
                
              $query->execute();
              $viewSaldo = $query->fetch();
              $nSaldo = ($viewAssoc["credassoc"] + $viewSaldo["valor"]);

              if($nSaldo < 0){
                $nSaldo = 0;
              }else{
                if ($viewAssoc["credcartao"] > 0){
                  if( ($cartao != '') && ($tipochamada == 'WHATS') ){
                    $sSql = $sSql . ' AND SUBSTRING(depcartao, 4, LENGTH(depcartao) )  = :depcartao ';
                  } else { //ura                                  
                    $sSql = $sSql . ' AND depcartao = :depcartao ';
                  }  

                  $query = DB_Conexao::conectar()->prepare($sSql);
                  $query->bindValue('associado', $viewAssoc["idassoc"]);
                  $query->bindValue('depcartao', $cartaoSemDig);
                  if($viewAssoc["credmodelo"] == 'VLTOTAL'){
                    $query->bindValue('dDataIni', $dDataIni); 
                  }else{
                    $query->bindValue('dDataIni', $dDataIni);
                    $query->bindValue('dDataFin', $dDataFin); 
                  }
                  
                  $query->execute();
                  $viewSaldoCartao = $query->fetch();

                  $nSaldoAssoc = $viewAssoc["credcartao"] + $viewSaldoCartao["valor"];

                  if($nSaldoAssoc < 0){
                    $nSaldoAssoc = 0;
                  } 
                } else {
                  $nSaldoAssoc = $nSaldo;
                }
              }     
              $nSaldo      = number_format($nSaldo, 2, ',', '.');  
              $nSaldoAssoc = number_format($nSaldoAssoc, 2, ',', '.'); 

              $dadosRet = array(
                'titular' => $viewAssoc['titular'],
                'dependente' => $viewAssoc['dependente'],
                'saldotitular' => $nSaldo,
                'saldodependente' => $nSaldoAssoc,
                'erro' => ''
              );             
            } else {
              $dadosRet = array(
                'titular' => '',
                'dependente' => '',
                'saldotitular' => 0,
                'saldodependente' => 0,
                'erro' => 'Cartão não localizado!'
              );   
            }
          }catch(Exception $e){
            $dadosRet = array(
                'titular' => '',
                'dependente' => '',
                'saldotitular' => 0,
                'saldodependente' => 0,
                'erro' => 'Erro ao executar o processo. Por favor, entrar em contato com suporte técnico da ASU. Erro original: ' . $e
            );         
          }          

          return $dadosRet;
        }


        //retorna saldo do cartão pré pago (Vale Compras)
        public static function saldoCartaoPre($cpf, $cartao, $tipochamada){          

          $sSql = " SELECT assoc.idassoc, assoc.titular, dep.nome as dependente, 
                      ( SELECT saldo.qtde   
                        FROM cgc_saldo saldo
                        WHERE saldo.cnscanmom is null
                          and saldo.uuidcontrato = contr.uuidcontrato
                        LIMIT 1) as saldocartao
                    FROM cgc_contrato contr
                      INNER JOIN cgc_cartao car on car.uuidcontrato = contr.uuidcontrato 
                      INNER JOIN cgc_contrxentidade contrxent  on contrxent.uuidcontrato = contr.uuidcontrato
                      INNER JOIN asdepen dep ON dep.iddepen = contrxent.origid AND contrxent.origtab = 'ASDEPEN'
                      INNER JOIN associa assoc ON assoc.idassoc = dep.associado                  
                    WHERE assoc.cnscanmom is null 
                      AND dep.cnscanmom is null 
                      AND contr.cnscanmom is null
                      AND car.cnscanmom is null 
                      AND car.dt_fim is null ";
          if($cpf != ''){
            $sSql = $sSql . ' AND SUBSTRING(dep.cnpj_cpf, 9, LENGTH(dep.cnpj_cpf) ) = :cnpj_cpf ';
          }
          if( ($cartao != '') && ($tipochamada == 'WHATS') ){
            $sSql = $sSql . ' AND SUBSTRING(car.numcartao, 4, LENGTH(car.numcartao) )  = :cartao ';
          } else { //ura
            $sSql = $sSql . ' AND car.numcartao = :cartao ';
          } 

          $query = DB_Conexao::conectar()->prepare($sSql);        
          if($cpf != ''){
            $query->bindValue('cnpj_cpf', $cpf);   
          }     
          if($cartao != ''){   
            $nTamCart      = (strlen($cartao) - 2);
            $cartaoSemDig  = substr($cartao, 0, $nTamCart);                     
            $query->bindValue('cartao', $cartaoSemDig); 
          }    

          try
          {
            $query->execute();
            $viewAssoc = $query->fetch();
            if(!empty($viewAssoc)){
              $dadosRet = array(
                'titular' => $viewAssoc['titular'],
                'dependente' => $viewAssoc['dependente'],
                'saldo' => number_format($viewAssoc['saldocartao'], 2, ',', '.'),
                'erro' => ''
              );   
            }else{
              $dadosRet = array(
                'titular' => '',
                'dependente' => '',
                'saldo' => 0,
                'erro' => 'Cartão não localizado'
              );            
            }
          }catch(Exception $e){
            $dadosRet = array(
                'titular' => '',
                'dependente' => '',
                'saldo' => 0,
                'erro' => 'Erro ao executar o processo. Por favor, entrar em contato com suporte técnico da ASU. Erro original: ' . $e
              );      
          } 
          echo $dadosRet; 
        } 
  }
?>