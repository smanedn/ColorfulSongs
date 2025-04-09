<?php
require_once 'vendor/autoload.php';

class Admin
{
    public function isAdmin(){
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if ($_SESSION['userType'] == 'admin') {
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
            new Database();
            require_once 'application/models/Leaderboard.php';
            $leaderboard = Leaderboard::getData();
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/admin/userManagement.php';
        }
    }

    public function delete($id)
    {
        new Database();
        if($this->isAdmin()){
//            echo $id;
            User::find($id)->delete();
        }
        $this->index();
    }

}