<?php 
	header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Credentials: true');
	header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
	header('Access-Control-Allow-Headers: Content-Type, Authorization, X-Requested-With'); 
	header('Content-Type: application/json; charset=utf-8');  

	date_default_timezone_set('America/Sao_Paulo');
	
	//Conectar com banco de dados!
	define('HOST','https://vendas.asu.com.br');
	define('PORTA','3308');
	define('USER','site');
	define('PASSWORD','sccp@2303');
	define('DATABASE','vegas');

	/*define('HOST','localhost');
	define('PORTA','3308');
	define('USER','york');
	define('PASSWORD','york');
	define('DATABASE','vegasasu');*/

	define('DIAMESINI','20');
	define('DIAMESFIN','19');

	//essa variável irá adicionar na aplicação a classe passada como parâmetro 
    $autoload = function($class){
        include('class/'.$class.'.php') ;
    };

    //essa função vai registrar as classes
    spl_autoload_register($autoload);
   
?>