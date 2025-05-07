<?php

use Monolog\Handler\StreamHandler;
use Monolog\Logger;

require_once 'vendor/autoload.php';

class leaderboardController
{
    private $validator;
    private $log;

    public function __construct()
    {
        require_once 'application/libs/validator.php';
        $this->validator = new \libs\Validator();
        $this->log = new Logger('leaderboardController');
        $this->log->pushHandler(new StreamHandler('application/logs/log.log'));
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
                if ($this->isAdmin()) {
                    $leaderboard_data = Leaderboard::getData();
                    $checked = "global";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/_templates/header.php';
                    require_once 'application/views/admin/index.php';
                }else{
                    $leaderboard_data = Leaderboard::getData();
                    $checked = "global";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/_templates/header.php';
                    require_once 'application/views/leaderboard/index.php';
                }
            } else if ($_POST['type'] == 'friend') {
                if ($this->isAdmin()) {

                    $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                    $checked = "friend";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/_templates/header.php';
                    require_once 'application/views/admin/index.php';
                }else{

                    $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                    $checked = "friend";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/_templates/header.php';
                    require_once 'application/views/leaderboard/index.php';
                }
            }
        }
    }

    public function searchFilter()
    {
        require_once "application/models/Leaderboard.php";
        if (isset($_POST['search'])) {
            $username = $this->validator->sanitizeInput($_POST['usernameSearch']);
            $username = $this->validator->checkTextArea($username);
            if (is_null($username)) {
                require_once 'application/views/_templates/header.php';
                if ($this->isAdmin()) {
                    require_once 'application/views/admin/index.php';
                }else {
                    require_once 'application/views/leaderboard/index.php';
                }
            }
            if (isset($_SESSION['type'])) {
                require_once 'application/views/_templates/header.php';
                if ($this->isAdmin()) {
                    $leaderboard_data = Leaderboard::getDataByUsername($username);
                    require_once 'application/views/admin/index.php';

                }else {
                    $leaderboard_data = Leaderboard::getDataByUsername($username);
                    require_once 'application/views/admin/index.php';
                }
            }
            require_once 'application/views/_templates/header.php';
            if ($this->isAdmin()) {
                require_once 'application/views/admin/index.php';
            }else {
                require_once 'application/views/leaderboard/index.php';
            }
        }
        if (isset($_POST['deleteFilter'])) {
//            setcookie('mapCode',$_COOKIE['usernameSearch'],time() - (3600), "/");
            require_once "application/models/Leaderboard.php";
            require_once 'application/views/_templates/header.php';
            if ($this->isAdmin()) {
                if ($_SESSION['type'] == 'friend') {
                    $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                }elseif ($_SESSION['type'] == 'global'){
                    $leaderboard_data = Leaderboard::getData();
                }
                require_once 'application/views/admin/index.php';
            }else {
                if ($_SESSION['type'] == 'friend') {
                    $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                }elseif ($_SESSION['type'] == 'global'){
                    $leaderboard_data = Leaderboard::getData();
                }
                require_once 'application/views/leaderboard/index.php';
            }
        }
    }

    public function friendRequest($friendId)
    {

        if ($this->isAdmin()) {
            try {
                $friend = Friend::create(["userId1" => $_SESSION['UserId'], "userId2" => $friendId, "pending" => 1]);

                $this->log->info('friend created with attribute userId1: ' . $friend->userId1 . ' userId2: ' . $friend->userId1 . ' pending: ' . $friend->pending );
            }catch (\Exception $e){
                $this->log->warning($e->getMessage());
            }


            require_once 'application/models/Leaderboard.php';
            $leaderboard_data = Leaderboard::getData();
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/admin/index.php';
        }
    }

}