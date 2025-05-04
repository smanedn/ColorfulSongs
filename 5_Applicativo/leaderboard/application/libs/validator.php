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
        $pattern = '/^[\r\n0-9a-zA-Z!?()%*+^$£àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð\/ ,.:;\'-]+$/';
        if(preg_match($pattern, $val) && strlen($val) >= 2){
            return $val;
        }else{
            $this->log->warning("checkTextArea: Type is not a string");
        }

    }

    function sanitizeInput($data) {
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }
}