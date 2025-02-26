<?php
namespace libs;
require_once "application/libs/log.php";
class Validator
{
    private $logs;
    function checkNumber($val)
    {
        $this->logs = new \libs\Log();
        if(is_numeric($val) && $val > 0 && $val <= 9999){
            return $val;
        }
        else{
            $this->logs->errorLog("Type is not a number");
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