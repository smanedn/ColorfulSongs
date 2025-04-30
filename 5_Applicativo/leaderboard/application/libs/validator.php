<?php
namespace libs;
use Monolog\Logger;
use Monolog\Handler\StreamHandler;
class Validator
{
    private $log;

    public function __construct()
    {
        $this->log = new Logger('validator');
        $this->log->pushHandler(new StreamHandler('application/logs/log.log'));
    }


    function checkNumber($val)
    {
        if(is_numeric($val) && $val > 0 && $val <= 9999){
            return $val;
        }
        else{
            $this->log->warning("checkNumber: Type is not a number");
        }
    }

    function checkTextArea($val)
    {
        return (preg_match('/^[\r\n0-9a-zA-Z!?()%*+^$£àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð\/ ,.:;\'-]+$/', $val)) && strlen($val) >= 2;
    }

    function sanitizeInput($data) {
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }
}