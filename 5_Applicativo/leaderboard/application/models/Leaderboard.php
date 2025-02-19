<?php

namespace models;

class Leaderboard
{
    private $id;
    private $username;
    private $friendId;
    private $score;
    private $mapCode;

    /**
     * @param $username
     * @param $score
     * @param $mapCode
     */
    public function __construct($id,$username, $score, $mapCode,$friendId)
    {
        $this->id = $id;
        $this->username = $username;
        $this->score = $score;
        $this->mapCode = $mapCode;
        $this->friendId = $friendId;
    }

    //public function constructorUsernameScoreMapCode($username, $score, $mapCode) {
    //    $this->username = $username;
    //    $this->score = $score;
    //    $this->mapCode = $mapCode;
    //}
//
    //public function constructorIdFriendCode($id, $friendId) {
    //    $this->id = $id;
    //    $this->friendId = $friendId;
    //}
//
    //public function __construct() {
    //    $arguments = func_get_args();
    //    $numberOfArguments = func_num_args();
//
    //    if (method_exists($this, $function =
    //        'ConstructorWithArgument'.$numberOfArguments)) {
    //        call_user_func_array(
    //            array($this, $function), $arguments);
    //    }
    //}


    //public function __construct($id, $friendCode)
    //{
    //    $this->id = $id;
    //    $this->friendCode = $friendCode;
    //}

    public function getUsername()
    {
        return $this->username;
    }

    public function setUsername($username)
    {
        $this->username = $username;
    }

    public function getScore()
    {
        return $this->score;
    }

    public function setScore($score)
    {
        $this->score = $score;
    }

    public function getMapCode()
    {
        return $this->mapCode;
    }

    public function setMapCode($mapCode)
    {
        $this->mapCode = $mapCode;
    }


}