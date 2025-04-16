<?php
use Illuminate\Database\Capsule\Manager as Capsule;
class Database
{
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
}