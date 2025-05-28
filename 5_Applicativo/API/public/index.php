<?php
require_once '../bootstrap.php';
use Src\Controller\UserController;
use Monolog\Logger;
use Monolog\Handler\StreamHandler;
$logger = new Logger("index");
$logger->pushHandler(new StreamHandler('../log/log.log'));

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: OPTIONS,GET,POST,PUT,DELETE");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

$uri = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH);
$uri = explode('/', $uri);

// tutti gli endpoint iniziano per /user
// quindi tutto il resto che non inizia per /user risulterà in un 404 Not Found
if ($uri[3] !== 'user') {
    header('HTTP/1.1 404 Not Found');
    exit();
}

// questi sono i parametri opzionali dopo lo /user.
// Controlla che se ci sia un id o uno username
$userId = null;
$username = '';
if (isset($uri[4])) {
    try {
        $userId = (int) $uri[4];
    }catch (\Exception $e){
        $logger->info($e->getMessage());
    }
    $username = $uri[4];
}

$requestMethod = $_SERVER['REQUEST_METHOD'];

$controller = new UserController($dbConnection,$requestMethod,$userId,$username);
$controller->processRequest();

