<?php
namespace models;
require_once "Leaderboard.php";
require_once "application/libs/database.php";

class LeaderboardMapper
{
    private $validator;
    private $connection;
    private $logs;
    public function __construct()
    {
        require_once "application/libs/validator.php";
        require_once "application/libs/log.php";
        $this->validator = new \libs\Validator();
        $this->connection = \libs\Database::getConnection();
        $this->logs = new \libs\Log();
    }

    /**
     * @return array
     */
    public function fetchAll(): array
    {
        try {
            $selectUserData = "select user.username, leaderboard.score, leaderboard.dungeon_id
                            from user JOIN leaderboard 
                            on leaderboard.user_id = user.id
                            order by leaderboard.score desc";
            $userData = $this->connection->query($selectUserData);
        }catch (\Exception $e){
            $this->logs->errorLog($e);
        }
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['score'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }


    public function fetchFriend($userId): array
    {
        try {
            $selectUserData = "SELECT user.username, leaderboard.score ,leaderboard.dungeon_id  
                            from user JOIN friend 
                                ON friend.userId1 = '$userId' 
                                AND friend.pending = 0 
                                AND friend.userId2 = user.id
                            JOIN leaderboard 
                                ON friend.userId2 = leaderboard.user_id
                            order by leaderboard.score desc";

            $userData = $this->connection->query($selectUserData);
        }catch (\Exception $e){
            $this->logs->errorLog($e);
        }
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['score'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }

    public function fetchMaps($mapId): array
    {
        try{
            $selectUserData = "SELECT DISTINCT user.username, leaderboard.score, leaderboard.dungeon_id
                            from user JOIN leaderboard
                            ON user.id = leaderboard.user_id
                            JOIN dungeon
                            ON leaderboard.dungeon_id = '$mapId'
                            order by leaderboard.score desc";

            $userData = $this->connection->query($selectUserData);
        }catch (\Exception $e){
            $this->logs->errorLog($e);
        }
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['score'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;
    }

    public function fetchMapsFriends($mapId,$userId){
        try{
            $selectUserData = "SELECT DISTINCT user.username, leaderboard.score, leaderboard.dungeon_id
                            from user JOIN leaderboard
                            ON user.id = leaderboard.user_id
                            JOIN dungeon
                            ON leaderboard.dungeon_id = '$mapId'
                            JOIN friend
                            ON friend.userId1 = '$userId'
                            AND friend.pending = 0
                            AND friend.userId2 = user.id
                            AND friend.userId2 = leaderboard.user_id
                            order by leaderboard.score desc";

            $userData = $this->connection->query($selectUserData);
        }catch (\Error $e){
            $this->logs->errorLog($e);
        }
        $allUserData = array();
        foreach ($userData as $line) {
            $userData = new Leaderboard($line['username'], $line['score'], $line['dungeon_id']);
            $allUserData[] = $userData;
            unset($userData);
        }
        return $allUserData;

    }

}