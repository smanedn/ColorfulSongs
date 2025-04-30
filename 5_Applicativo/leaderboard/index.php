<?php

require 'application/config/config.php';

require 'application/libs/application.php';

require_once "vendor/autoload.php";

require_once "application/libs/CsrfTokenManager.php";
new Database();
$csrf = new \libs\CsrfTokenManager();

$app = new \libs\Application();
