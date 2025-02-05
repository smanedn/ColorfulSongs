<?php
class Database
{
    private static $username = USERNAME;
    private static $password = PASSWORD;
    private static $hostname = HOST;
    private static $database = DATABASE;
    private static $_connection;

    private function __construct()
    {

    }

    public static function getConnection(){
        if(is_null(self::$_connection)){
            try{
                self::$_connection = new mysqli(self::$hostname, self::$username, self::$password, self::$database);
            }catch (Exception $e){
                echo "Error: " . $e->getMessage();
                die;
                return false;

            }
        }
        return self::$_connection;
    }


}