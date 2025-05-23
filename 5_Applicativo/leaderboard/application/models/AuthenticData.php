<?php
namespace models;
require_once "application/models/Database.php";
class AuthenticData
{
    private $conn;
    private $statement;

    public function __construct()
    {
        $this->conn = \models\Database::getConnection();
    }

    public function getData($username, $password)
    {
        echo "$username";
        $this->statement = $this->conn->prepare("select id,password from user where username=?");
        $this->statement->bindParam(1, $username);
        $this->statement->execute();
        var_dump($this->statement);
        $row = $this->statement->fetch(\PDO::FETCH_ASSOC);
        if($row){
            if (password_verify($password, $row["password"])) {
                return $row;
            }
        }
    }
}