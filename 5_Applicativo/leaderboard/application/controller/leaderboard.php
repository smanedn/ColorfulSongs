<?php
class leaderboard
{
    private $validator;
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
            if (!isset($_SESSION['type'])){
                $_SESSION['type'] = "global";
                require_once 'application/views/leaderboard/index.php';
            }else{
                $_SESSION['type'] = $_POST['type'];
                require_once 'application/views/leaderboard/index.php';
            }
            require_once 'application/views/leaderboard/index.php';
        }
    }

    public function radioFilter(){
        if ($this->isAdmin()) {
            require_once 'application/models/LeaderboardMapper.php';
            $leaderboardMapper = new \models\LeaderboardMapper();
            if(isset($_POST['type'])) {
                if ($_POST['type'] == 'global') {
                    if (isset($_COOKIE['mapCode'])) {
                        $leaderboard_data = $leaderboardMapper->fetchMaps($_COOKIE['mapCode']);
                    }else{
                        $leaderboard_data = $leaderboardMapper->fetchAll();
                    }
                    $checked = "global";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/leaderboard/index.php';
                } else if ($_POST['type'] == 'friend') {
                    if (isset($_COOKIE['mapCode'])){
                        $leaderboard_data = $leaderboardMapper->fetchMapsFriends($_COOKIE['mapCode'],$_SESSION['UserId']);
                    }else{
                        $leaderboard_data = $leaderboardMapper->fetchFriend($_SESSION["UserId"]);
                    }
                    $checked = "friend";
                    $_SESSION['type'] = $checked;
                    require_once 'application/views/leaderboard/index.php';
                }
            }
        }
    }

    public function searchFilter(){
        if ($this->isAdmin()) {
            if (isset($_POST['search'])) {
                require_once 'application/libs/validator.php';
                require_once "application/models/LeaderboardMapper.php";
                $leaderboardMapper = new \models\LeaderboardMapper();
                $this->validator = new \libs\Validator();
                $mapCode = $this->validator->sanitizeInput($_POST['mapCode']);
                setcookie('mapCode',$mapCode,time() + (3600), "/");
                $mapCode = $this->validator->checkNumber($mapCode);
                if (is_null($mapCode)) {
                    $error = "Value must be numeric";
                    require_once 'application/views/leaderboard/index.php';
                }
                if (isset($_SESSION['type'])) {
                    if ($_SESSION['type'] == 'friend') {
                        $leaderboard_data = $leaderboardMapper->fetchMapsFriends($mapCode,$_SESSION['UserId']);
                    }elseif ($_SESSION['type'] == 'global'){
                        $leaderboard_data = $leaderboardMapper->fetchMaps($mapCode);
                    }
                    require_once 'application/views/leaderboard/index.php';
                }
                require_once 'application/views/leaderboard/index.php';
            }
            if (isset($_POST['deleteFilter'])) {
                setcookie('mapCode',$_COOKIE['mapCode'],time() - (3600), "/");
                require_once "application/models/LeaderboardMapper.php";
                $leaderboard_model = new \models\LeaderboardMapper();
                $leaderboard_data = $leaderboard_model->fetchAll();
                require_once 'application/views/leaderboard/index.php';
            }

        }
    }
}