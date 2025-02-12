<?php
namespace models;
require_once "Leaderboard.php";
require_once "application/libs/database.php";

class LeaderboardMapper
{
    private $validator;
    private $connection;

    public function __construct()
    {
        require_once "application/libs/validator.php";

        $this->validator = new \libs\Validator();
        $this->connection = \libs\Database::getConnection();
    }

    /**
     * @return array
     */
    public function fetchAll(): array
    {
        $selectUserData = "select user.username, leaderboard.score, leaderboard.dungeon_id, friend.user_friend_code from user JOIN leaderboard on leaderboard.user_id = user.id join friend on friend.user_id = user.id";
        $userData = $this->connection->query($selectUserData);
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['score'], $line['user_friend_code'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }
}