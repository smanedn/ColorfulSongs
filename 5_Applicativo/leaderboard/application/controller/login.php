<?php
require_once 'vendor/autoload.php';

class Login
{
    private $validator;

    public function __construct()
    {
        require_once "application/libs/validator.php";
        $this->validator = new \libs\Validator();
    }

    public function index()
    {
        require_once 'application/views/_templates/header.php';
        require_once 'application/views/login/index.php';
    }

    public function logIn()
    {
        session_start();
        if (isset($_POST['login'])) {
            new Database();
            require_once 'application/libs/validator.php';

            $username = $this->validator->sanitizeInput($_POST['username']);
            $password = $this->validator->sanitizeInput($_POST['password']);

            $result = User::where('username', $username)->first();


            if ($result && password_verify($password, $result->password)) {
                $_SESSION['userType'] = $result->type;
                $_SESSION['username'] = $username;
                $_SESSION["UserId"] = $result['id'];

                header("Location:" . URL . "leaderboardController");
                exit();
            } else {
                $error = "Username or Password incorrect";
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/login/index.php';
            }
        }
    }

    public function logout()
    {
        session_start();
        unset($_SESSION['UserId']);
        setcookie('mapCode',$_COOKIE['mapCode'],time() - (3600), "/");
        session_destroy();
        header("Location:" . URL);
        exit();
    }


}
