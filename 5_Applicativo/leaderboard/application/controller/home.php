<?php
class Home
{
    public function index()
    {

        if($this->isAdmin()){
            require_once "application/models/LeaderboardMapper.php";
            $leaderboard_model = new \models\LeaderboardMapper();
            $leaderboard_data = $leaderboard_model->fetchAll();

            require_once 'application/views/leaderboard/index.php';
        }
    }

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

    public function showLeaderboardData(){
        require_once "application/models/LeaderboardMapper.php";
        $leaderboard_model = new \models\LeaderboardMapper();
        $leaderboard_data = $leaderboard_model->fetchAll();

        require_once 'application/views/leaderboard/index.php';
    }
}
