<?php
require 'vendor/autoload.php';
use Dotenv\Dotenv;
use Src\System\Database;

$dotenv = new Dotenv(__DIR__);
$dotenv->load();

$dbConnection = (new Database())->getConnection();