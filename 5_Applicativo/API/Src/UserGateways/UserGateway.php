<?php
namespace Src\UserGateways;
class UserGateway{
    private $db = null;
    public function __construct($db){
        $this->db = $db;
    }

    public function findAll()
    {
        $statement = "select * from leaderboard";

        try{
            $statement = $this->db->prepare($statement);
            $statement->execute();
            $result = $statement->fetchAll(\PDO::FETCH_ASSOC);
            return $result;
        }catch(\PDOException $e){
            exit($e->getMessage());
        }
    }

    public function find($id)
    {
        $statement = "select * from user where id = ?";
        try{
            $statement = $this->db->prepare($statement);
            $statement->execute(array($id));
            $result = $statement->fetchAll(\PDO::FETCH_ASSOC);
            return $result;
        }catch(\PDOException $e){
            exit($e->getMessage());
        }
    }

    public function insert(Array $input)
    {
        $statement = "
                insert into user
                    (username, password, email)
                values
                    (:username, :password, :email)";

        try{
            var_dump($input);
            $statement = $this->db->prepare($statement);
            $statement->execute(array(
                'username' => $input['username'],
                'password' => $input['password'],
                'email' => $input['email']
            ));
            return $statement->rowCount();
        }catch(\PDOException $e){
            exit($e->getMessage());
        }
    }

    public function update($id, Array $input)
    {
        $statement = "
            update user 
            set
                username = :username, 
                password = :password,
                email = :email
            where id = :id";

        try {
            $statement = $this->db->prepare($statement);
            $statement->execute(array(
                'id' => (int) $id,
                'username' => $input['username'],
                'password' => $input['password'],
                'email' => $input['email'],
            ));
            return $statement->rowCount();
        }catch(\PDOException $e){
            exit($e->getMessage());
        }
    }

    public function delete($id)
    {
        $statement = "delete from user where id = :id";
        try{
            $statement = $this->db->prepare($statement);
            $statement->execute(array('id' => $id));
            return $statement->rowCount();
        }catch(\PDOException $e){
            exit($e->getMessage());
        }
    }
}