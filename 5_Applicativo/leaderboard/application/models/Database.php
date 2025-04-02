<?php
//namespace models;
use Illuminate\Database\Capsule\Manager as Capsule;
class Database
{
    private static $_connection;

    public function __construct()
    {
        $capsule = new Capsule;
        $capsule->addConnection([
            'driver' => 'mysql',
            'host' => 'localhost',
            'database' => 'colorfulsongs',
            'username' => 'colorfulsongs',
            'password' => 'Admin$00',
        ]);
        $capsule->setAsGlobal();
        $capsule->bootEloquent();
    }


//    public static function getConnection(){
//        if (is_null(self::$_connection)) {
//            try {
//                self::$_connection = new \PDO("mysql:host=localhost;dbname=colorfulsongs", USERNAME, PASSWORD);
//                self::$_connection->setAttribute(\PDO::ATTR_ERRMODE, \PDO::ERRMODE_EXCEPTION);
//            } catch (\PDOException $e) {
//                die("Connection failed: " . $e->getMessage());
//            }
//        }
//        return self::$_connection;
//    }

}