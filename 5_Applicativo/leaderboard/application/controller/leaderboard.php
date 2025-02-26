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

    public function searchFilter(){
        if ($this->isAdmin()) {
            if (isset($_POST['search'])) {
                require_once 'application/libs/validator.php';
                $this->validator = new \libs\Validator();
                $mapCode = $this->validator->sanitizeInput($_POST['mapCode']);
                $mapCode = $this->validator->checkNumber($mapCode);
                if ($mapCode == "") {
                    $error = "Value must be numeric";
                    require_once 'application/views/leaderboard/index.php';
                }
                require_once "application/models/LeaderboardMapper.php";
                $leaderboardMapper = new \models\LeaderboardMapper();

                $leaderboard_data = $leaderboardMapper->fetchMaps($mapCode);
                require_once 'application/views/leaderboard/index.php';
            }
        }
    }
}