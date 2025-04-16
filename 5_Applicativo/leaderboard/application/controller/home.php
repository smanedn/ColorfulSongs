<?php
class Home
{
    public function index()
    {

        if($this->isAdmin()){
            new Database();
            $leaderboard = Leaderboard::all();
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/admin/index.php';
        }else{
            new Database();
            $leaderboard = Leaderboard::all();
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/leaderboard/index.php';
        }
    }

    public function isAdmin(){
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if ($_SESSION['userType'] == 'admin') {
            return true;
        }
    }

    public function showLeaderboardData(){
        $leaderboard = Leaderboard::all();
        require_once 'application/views/_templates/header.php';
        require_once 'application/views/leaderboard/index.php';
    }
}
