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
        $selectUserData = "select user.username, leaderboard.score, leaderboard.dungeon_id
                            from user JOIN leaderboard 
                            on leaderboard.user_id = user.id
                            order by leaderboard.score desc";
        $userData = $this->connection->query($selectUserData);
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line[''],$line['username'], $line['score'], $line['dungeon_id'],$line['']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }

    public function fetchFriend($userId): array
    {
        $selectUserData = "SELECT user.username, user.id 
                            from user JOIN friend 
                            ON friend.idUtente1 = $userId AND friend.pending = 0 AND friend.idUtente2 = user.id";

        $userData = $this->connection->query($selectUserData);
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['id'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }
}