<?php

namespace models;

class Leaderboard
{
    private $username;
    private $score;
    private $friendCode;
    private $mapCode;

    /**
     * @param $username
     * @param $score
     * @param $friendCode
     * @param $mapCode
     */
    public function __construct($username, $score, $friendCode, $mapCode)
    {
        $this->username = $username;
        $this->score = $score;
        $this->friendCode = $friendCode;
        $this->mapCode = $mapCode;
    }

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

    public function getFriendCode()
    {
        return $this->friendCode;
    }

    public function setFriendCode($friendCode)
    {
        $this->friendCode = $friendCode;
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