<?php
class leaderboard
{
    public function isAdmin(){
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if (isset($_SESSION['UserId'])) {
            return true;
        }
        else{
            header('location:' . URL . 'login');
            exit();
        }
    }

    public function index()
    {
        if($this->isAdmin()){
            require_once "application/models/LeaderboardMapper.php";
            $leaderboard_model = new \models\LeaderboardMapper();
            $leaderboard_data = $leaderboard_model->fetchAll();

            require_once 'application/views/leaderboard/index.php';
        }
    }

    public function showLeaderboardData(){
        require_once "application/models/LeaderboardMapper.php";
        $leaderboard_model = new \models\LeaderboardMapper();
        $leaderboard_data = $leaderboard_model->fetchAll();

        require_once 'application/views/leaderboard/index.php';
    }

    public function radioFilter(){
        if ($this->isAdmin()) {
            if (isset($_POST['friendGlobal'])) {
                require_once 'application/models/LeaderboardMapper.php';
                $leaderboardMapper = new \models\LeaderboardMapper();
                $leaderboard_data = $leaderboardMapper->fetchFriend($_SESSION["UserId"]);

                require_once 'application/views/leaderboard/index.php';
            }
        }
    }
}