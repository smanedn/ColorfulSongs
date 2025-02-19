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
            $checked = "friend";
            //$leaderboard_friend = $leaderboard_model->fetchFriend($_SESSION['UserId']);

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
            require_once 'application/models/LeaderboardMapper.php';
            $leaderboardMapper = new \models\LeaderboardMapper();

            if (isset($_POST['friend'])) {
                $leaderboard_data = $leaderboardMapper->fetchFriend($_SESSION["UserId"]);
                $checked = "friend";
                require_once 'application/views/leaderboard/index.php';
                //header('location:' . URL . 'leaderboard');
            }

            elseif(isset($_POST['global'])){
                $leaderboard_data = $leaderboardMapper->fetchAll();
                $checked = "global";
                require_once 'application/views/leaderboard/index.php';
                //header('location:' . URL . 'leaderboard');
            }
        }
    }
}