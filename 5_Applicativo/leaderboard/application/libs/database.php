<?php
namespace libs;
class Database
{
    private static $_connection;

    private function __construct()
    {

    }

    public static function getConnection(){
        if (is_null(self::$_connection)) {
            try {
                self::$_connection = new \PDO("mysql:host=localhost;dbname=colorfulsongs", USERNAME, PASSWORD);
                self::$_connection->setAttribute(\PDO::ATTR_ERRMODE, \PDO::ERRMODE_EXCEPTION);
            } catch (\PDOException $e) {
                die("Connection failed: " . $e->getMessage());
            }
        }
        return self::$_connection;
    }


}