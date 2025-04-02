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
            echo $password;
            var_dump($result);
            if ($result && password_verify($password, $result->password)) {
                var_dump($result);
                $_SESSION['username'] = $username;
                $_SESSION["UserId"] = $result['id'];

                header("Location:" . URL . "leaderboard");
                exit();
            } else {
                $error = "Username or Password incorrect";
                require_once 'application/views/login/index.php';
            }
        }
    }

    public function logout()
    {
        session_start();
        unset($_SESSION['UserId']);

        session_destroy();
        header("Location:" . URL);
        exit();
    }


}
