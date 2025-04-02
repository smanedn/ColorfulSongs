<?php
class Home
{
    public function index()
    {

        if($this->isAdmin()){
            new Database();
            $leaderboard = Leaderboard::all();
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
        $leaderboard = Leaderboard::all();
        require_once 'application/views/leaderboard/index.php';
    }
}
