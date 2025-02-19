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
            $checked = "global";

            require_once 'application/views/leaderboard/index.php';
        }
    }

    public function radioFilter(){
        if ($this->isAdmin()) {
            require_once 'application/models/LeaderboardMapper.php';
            $leaderboardMapper = new \models\LeaderboardMapper();

            if(isset($_POST['type'])) {
                if ($_POST['type'] == 'global') {
                    $leaderboard_data = $leaderboardMapper->fetchAll();
                    $checked = "global";
                    require_once 'application/views/leaderboard/index.php';
                } else if ($_POST['type'] == 'friend') {
                    $leaderboard_data = $leaderboardMapper->fetchFriend($_SESSION["UserId"]);
                    $checked = "friend";
                    require_once 'application/views/leaderboard/index.php';
                }
            }

        }
    }
}