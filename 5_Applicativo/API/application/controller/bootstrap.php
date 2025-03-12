<?php
require 'vendor/autoload.php';
use Dotenv\Dotenv;
use src\libs\Database;

$dotenv = new Dotenv(__DIR__);
$dotenv->load();

$_connection = (new Database())->getConnection();