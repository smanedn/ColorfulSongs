<?php
require_once 'vendor/autoload.php';

class leaderboardController
{
    private $validator;

    public function __construct()
    {
        require_once 'application/libs/validator.php';
        $this->validator = new \libs\Validator();
    }


    public function isAdmin(){
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if ($_SESSION['userType'] == 'admin') {
            return true;
        }
    }

    public function index()
    {
        require_once 'application/models/Leaderboard.php';
        $leaderboard_data = Leaderboard::getData();
        if (isset($_SESSION['type'])){
            $_SESSION['type'] = $_POST['type'];
        }else{
            $_SESSION['type'] = "global";
        }
        require_once 'application/views/_templates/header.php';
        if($this->isAdmin()) {
            echo URL;
            require_once 'application/views/admin/index.php';
        }else{
            require_once 'application/views/leaderboard/index.php';
        }
    }

    public function radioFilter()
    {
        require_once 'application/models/Leaderboard.php';
        $leaderboardMapper = Leaderboard::getData();
        if(isset($_POST['type'])) {
            if ($_POST['type'] == 'global') {
                require_once 'application/views/_templates/header.php';
                if (isset($_COOKIE['mapCode'])) {
                    $leaderboard_data = Leaderboard::getDataByDungeonId($_COOKIE['mapCode']);
                }else{
                    $leaderboard_data = Leaderboard::getData();
                }
                $checked = "global";
                $_SESSION['type'] = $checked;
                if ($this->isAdmin()) {
                    require_once 'application/views/admin/index.php';
                }else{
                    require_once 'application/views/leaderboard/index.php';
                }
            } else if ($_POST['type'] == 'friend') {
                require_once 'application/views/_templates/header.php';
                if (isset($_COOKIE['mapCode'])){
                    $leaderboard_data = User::getDataByDungeonAndFriend($_COOKIE['mapCode'],$_SESSION['UserId']);
                }else{
                    $leaderboard_data = User::getDataByUserId($_SESSION["UserId"]);
                }
                $checked = "friend";
                $_SESSION['type'] = $checked;
                if ($this->isAdmin()) {
                    require_once 'application/views/admin/index.php';
                }else{
                    require_once 'application/views/leaderboard/index.php';
                }
            }
        }
    }

    public function searchFilter()
    {
        require_once "application/models/Leaderboard.php";
        if (isset($_POST['search'])) {

            $mapCode = $this->validator->sanitizeInput($_POST['mapCode']);
            $mapCode = $this->validator->checkNumber($mapCode);
            setcookie('mapCode',$mapCode,time() + (3600), "/");
            if (is_null($mapCode)) {
                $error = "Value must be numeric";
                require_once 'application/views/_templates/header.php';
                if ($this->isAdmin()) {
                    require_once 'application/views/admin/index.php';
                }else {
                    require_once 'application/views/leaderboard/index.php';
                }
            }
            if (isset($_SESSION['type'])) {
                require_once 'application/views/_templates/header.php';
                $path = 'application/views/leaderboard/index.php';
                if ($this->isAdmin()) {
                    $path = 'application/views/admin/index.php';
                }
                if ($_SESSION['type'] == 'friend') {
                    $leaderboard_data = User::getDataByDungeonAndFriend($mapCode,$_SESSION['UserId']);
                }elseif ($_SESSION['type'] == 'global'){
                    $leaderboard_data = Leaderboard::getDataByDungeonId($mapCode);
                }
                require_once $path;
            }
            require_once 'application/views/_templates/header.php';
            if ($this->isAdmin()) {
                require_once 'application/views/admin/index.php';
            }else {
                require_once 'application/views/leaderboard/index.php';
            }
        }

        if (isset($_POST['deleteFilter'])) {
            setcookie('mapCode',$_COOKIE['mapCode'],time() - (3600), "/");
            require_once "application/models/Leaderboard.php";
            require_once 'application/views/_templates/header.php';
            if ($_SESSION['type'] == 'friend') {
                $leaderboard_data = User::getDataByUserId($_SESSION["UserId"]);
            }elseif ($_SESSION['type'] == 'global'){
                $leaderboard_data = Leaderboard::getData();
            }
            if ($this->isAdmin()) {
                require_once 'application/views/admin/index.php';
            }else {
                require_once 'application/views/leaderboard/index.php';
            }
        }
    }
}