<?php

namespace models;

class Leaderboard
{
    private $id;
    private $username;
    private $score;
    private $mapCode;

    /**
     * @param $username
     * @param $score
     * @param $mapCode
     */
    public function __construct($username, $score, $mapCode)
    {
        $this->username = $username;
        $this->score = $score;
        $this->mapCode = $mapCode;
    }

    public function __construct($id, $friendCode)
    {
        $this->id = $id;
        $this->friendCode = $friendCode;
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

    public function getMapCode()
    {
        return $this->mapCode;
    }

    public function setMapCode($mapCode)
    {
        $this->mapCode = $mapCode;
    }


}