<?php

    class DB_Conexao{

        private static $pdo;

        public static function conectar(){
            if(self::$pdo == null){
                try{
                    self::$pdo = new PDO('mysql:host='.HOST.';port='.PORTA.';dbname='.DATABASE,USER,PASSWORD,array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8"));
                    self::$pdo->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_EXCEPTION);
                }catch(Exception $e){
                    echo "<h2>Erro ao conectar no Banco de Dados. Erro original: $e</h2>";
                }
            }
            return self::$pdo;
        }
    }
?>