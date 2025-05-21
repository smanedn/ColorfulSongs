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


    public function isAdmin()
    {
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if ($_SESSION['userType'] == 'admin') {
            return true;
        }
    }

    public function index()
    {
        if (isset($_SESSION['userType'])) {
            require_once 'application/models/Leaderboard.php';
            $leaderboard_data = Leaderboard::getData();
            $friends = Friend::showFriend($_SESSION['UserId']);
            $friendIds = [];
            foreach ($friends as $friend) {
                if ($friend->userId1 == $_SESSION['UserId']) {
                    $friendIds[] = $friend->userId2;
                } else {
                    $friendIds[] = $friend->userId1;
                }
            }

            if (isset($_SESSION['type'])) {
                $_SESSION['type'] = $_POST['type'];
            } else {
                $_SESSION['type'] = "global";
            }
            require_once 'application/views/_templates/header.php';
            if ($this->isAdmin()) {
                require_once 'application/views/admin/index.php';
            } else {
                require_once 'application/views/leaderboard/index.php';
            }
        } else {
            header("Location:" . URL);
        }
    }

    public function radioFilter()
    {
        if (isset($_SESSION['userType'])) {
            require_once 'application/models/Leaderboard.php';
            $leaderboardMapper = Leaderboard::getData();
            if (isset($_POST['type'])) {
                $friends = Friend::showFriend($_SESSION['UserId']);
                $friendIds = [];
                foreach ($friends as $friend) {
                    if ($friend->userId1 == $_SESSION['UserId']) {
                        $friendIds[] = $friend->userId2;
                    } else {
                        $friendIds[] = $friend->userId1;
                    }
                }

                if ($_POST['type'] == 'global') {
                    $leaderboard_data = Leaderboard::getData();

                    $checked = "global";
                    $_SESSION['type'] = $checked;
                    if ($this->isAdmin()) {
                        require_once 'application/views/_templates/header.php';
                        require_once 'application/views/admin/index.php';
                    } else {
                        require_once 'application/views/_templates/header.php';
                        require_once 'application/views/leaderboard/index.php';
                    }
                } else if ($_POST['type'] == 'friend') {
                    $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);

                    $checked = "friend";
                    $_SESSION['type'] = $checked;
                    if ($this->isAdmin()) {
                        require_once 'application/views/_templates/header.php';
                        require_once 'application/views/admin/index.php';
                    } else {
                        require_once 'application/views/_templates/header.php';
                        require_once 'application/views/leaderboard/index.php';
                    }
                }
            }
        } else {
            header("Location:" . URL);
        }
    }

    public function searchFilter()
    {
        if (isset($_SESSION['userType'])) {
            require_once "application/models/Leaderboard.php";
            if (isset($_POST['search'])) {
                $username = $this->validator->sanitizeInput($_POST['usernameSearch']);
                $username = $this->validator->checkTextArea($username);
                if (is_null($username)) {
                    require_once 'application/views/_templates/header.php';
                    if ($this->isAdmin()) {
                        require_once 'application/views/admin/index.php';
                    } else {
                        require_once 'application/views/leaderboard/index.php';
                    }
                }
                $leaderboard_data = Leaderboard::getDataByUsername($username);
                $friends = Friend::showFriend($_SESSION['UserId']);

                $friendIds = [];
                foreach ($friends as $friend) {
                    if ($friend->userId1 == $_SESSION['UserId']) {
                        $friendIds[] = $friend->userId2;
                    } else {
                        $friendIds[] = $friend->userId1;
                    }
                }
                require_once 'application/views/_templates/header.php';
                if ($this->isAdmin()) {
                    require_once 'application/views/admin/index.php';
                } else {
                    require_once 'application/views/admin/index.php';
                }
            }
            if (isset($_POST['deleteFilter'])) {
                require_once "application/models/Leaderboard.php";
                require_once 'application/views/_templates/header.php';
                $friends = Friend::showFriend($_SESSION['UserId']);
                $friendIds = [];
                foreach ($friends as $friend) {
                    if ($friend->userId1 == $_SESSION['UserId']) {
                        $friendIds[] = $friend->userId2;
                    } else {
                        $friendIds[] = $friend->userId1;
                    }
                }
                if ($this->isAdmin()) {
                    if ($_SESSION['type'] == 'friend') {
                        $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                    } elseif ($_SESSION['type'] == 'global') {
                        $leaderboard_data = Leaderboard::getData();
                    }
                    require_once 'application/views/admin/index.php';
                } else {
                    if ($_SESSION['type'] == 'friend') {
                        $leaderboard_data = User::getDataByFriendId($_SESSION["UserId"]);
                    } elseif ($_SESSION['type'] == 'global') {
                        $leaderboard_data = Leaderboard::getData();
                    }
                    require_once 'application/views/leaderboard/index.php';
                }
            }
        } else {
            header("Location:" . URL);
        }
    }
}