<?php
namespace models;
require_once "application/libs/database.php";
class LeaderboardModel
{
    private $conn;
    private $statement;

    public function __construct()
    {
        $this->conn = \libs\Database::getConnection();
    }

    public function getAllData(){
        $selectAccesso = "select user.username, leaderboard.score from user JOIN leaderboard on leaderboard.user_id = user.id";

        $this->statement = $this->conn->query($selectAccesso);
        return $this->statement->fetch_all();
    }
}