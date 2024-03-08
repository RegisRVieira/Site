<?php
    include('conexaoDB.php');

    $postjson  = json_decode(file_get_contents('php://input'), true);
    if(isset($postjson['idconven'])){
      $idconven = $postjson['idconven']; 
      $cnpj_cpf  = $postjson['cnpj_cpf'];

     // $idconven = '19615'; 
     // $cnpj_cpf = '607';*/

      $diamesini = DIAMESINI;
      $diamesfin = DIAMESFIN;   
      $nTotal    = 0;
      $totalpos  = 0;
      $totalpre  = 0;
      $nome      = "";
      $dataAtual = date('Y-m-d');
      $dia       = date('d');      
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

      //pegando o id do convênio desprezando o dígito verificado que são os dois ultimos caractres
      $nTamID   = (strlen($idconven) - 2);
      $idconven = substr($idconven, 0, $nTamID);
      
      $sSql = " SELECT co.idconven, co.nome as x
                FROM coconven co 
                WHERE co.cnscanmom is null 
                  AND co.idconven = :idconven  
                  AND SUBSTRING(co.cnpj_cpf, (LENGTH(co.cnpj_cpf) - 2), LENGTH(co.cnpj_cpf) ) = :cnpj_cpf ";

      $query = $pdo->prepare($sSql);   
      $query->bindValue('idconven', $idconven);                                
      $query->bindValue('cnpj_cpf', $cnpj_cpf); 
      try
      {
        $query->execute();
        $viewConvenio = $query->fetch();       

        if(!empty($viewConvenio)){
          $nome   = $viewConvenio['x'];

          // SQL referente a tabela de movimentação dos convênios para associado ( GLOBAL)
          $sSql = " SELECT SUM(valor) AS valor 
                    FROM comovime
                    WHERE convenio = :convenio
                      AND cnscanmom is null  
                      AND vencimento BETWEEN :dDataIni and :dDataFin ";
                    
          $query = $pdo->prepare($sSql);
          $query->bindValue('convenio', $viewConvenio["idconven"]);
          $query->bindValue('dDataIni', $dDataIni);
          $query->bindValue('dDataFin', $dDataFin);            
          $query->execute();
          $viewTotal  = $query->fetch();
          $nTotalCPos = ($viewTotal["valor"] * -1);


          $sSql = " SELECT SUM(qtde) AS valor 
                    FROM cgc_movime
                    WHERE convenio = :convenio
                      AND cnscanmom is null  
                      AND vencimento BETWEEN :dDataIni and :dDataFin ";
          $query = $pdo->prepare($sSql);
          $query->bindValue('convenio', $viewConvenio["idconven"]);
          $query->bindValue('dDataIni', $dDataIni);
          $query->bindValue('dDataFin', $dDataFin);            
          $query->execute();
          $viewTotal  = $query->fetch();
          $nTotalCPre = $viewTotal["valor"];

          $nTotal = $nTotalCPos + $nTotalCPre;          
          $nTotal = number_format($nTotal, 2, ',', '.');
          $nTotalCPos = number_format($nTotalCPos, 2, ',', '.');
          $nTotalCPre = number_format($nTotalCPre, 2, ',', '.');

          $dados  = array(
            'convenio' => $nome,
            'total'    => "R$ $nTotal",            
            'totalpos' => "R$ $nTotalCPos",
            'totalpre' => "R$ $nTotalCPre"
          );  
        }else{
          $dados = array(
            'convenio' => "não foi localizado nenhum convênio com os dados informados",
            'total'    => "R$ 0,00",
            'totalpos' => "R$ 0,00",
            'totalpre' => "R$ 0,00"
          );  
        }
      }catch(Exception $e){
        $dados = array(
            'convenio' => "problema ao consultar o seu total de vendas! Erro original: $e",
            'total'    => "R$ 0,00",
            'totalpos' => "R$ 0,00",
            'totalpre' => "R$ 0,00"
          );         
      }  
    }else{
      $dados = array(
            'convenio' => "não foi informado nenhum convênio",
            'total'    => "R$ 0,00",
            'totalpos' => "R$ 0,00",
            'totalpre' => "R$ 0,00"
          );        
    }
    $result = json_encode(array('results' => $dados));
    echo $result;  
?>



