<?php
namespace libs;
class Log
{
    private $logDate = "";

    public function __construct()
    {

    }

    function errorLog($message){
        $this->logDate = new \DateTime();
        $this->logDate = $this->logDate->format('Y-m-d H:i:s');
        return file_put_contents('application/logs/errorLog.txt', "[ " . $this->logDate . " ] " . $message . PHP_EOL, FILE_APPEND);
    }

    function successLog(){
        $this->logDate = new \DateTime();
        $this->logDate = $this->logDate->format('Y-m-d H:i:s');
        file_put_contents('application/logs/successLog.txt', "[ " . $this->logDate . " ] " . PHP_EOL, FILE_APPEND);
    }
}