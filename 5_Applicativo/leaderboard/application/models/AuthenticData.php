<?php
require_once "application/libs/database.php";
class AuthenticData
{
    private $conn;
    private $statement;

    public function __construct()
    {
        $this->conn = Database::getConnection();
    }

    public function getData($username, $password)
    {
        $selectAccesso = "select id,password from user where username='$username'";

        //var_dump($selectAccesso);
        $this->statement = $this->conn->query($selectAccesso);
        if(mysqli_num_rows($this->statement) === 1){ // controlla che ci siano risultati
            $row = mysqli_fetch_assoc($this->statement);// mette gli  elementi in un array associativo
            if (password_verify($password, $row["password"])) {
                //echo "<pre>";
                //var_dump($row);
                //echo "</pre>";
                return $row;
            }
        }


    }
}