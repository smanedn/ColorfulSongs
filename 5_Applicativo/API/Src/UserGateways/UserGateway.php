<?php
namespace Src\UserGateways;
use Monolog\Logger;
use Monolog\Handler\StreamHandler;

class UserGateway{
    private $db = null;
    private $logger = null;
    public function __construct($db){
        $this->db = $db;
        $this->logger = new Logger("UserGateway");
        $this->logger->pushHandler(new StreamHandler('../log/errorLog.log'));
    }

    public function findAll()
    {
        $statement = "select * from user";

        try{
            $statement = $this->db->prepare($statement);
            $statement->execute();
            $result = $statement->fetchAll(\PDO::FETCH_ASSOC);
            return $result;
        }catch(\PDOException $e){
            $this->logger->error("findAll: " . $e->getMessage());
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
            $this->logger->error("find: " . $e->getMessage());
        }
    }

    public function insert(Array $input)
    {
        if (isset($input['username']) && isset($input['password']) && isset($input['email']) && isset($input['type'])) {
            $statement = "
                insert into user
                    (username, password, email, type)
                values
                    (:username, :password, :email, :type)";

            try{
                var_dump($input);
                $statement = $this->db->prepare($statement);
                $statement->execute(array(
                    'username' => $input['username'],
                    'password' => password_hash($input['password'], PASSWORD_DEFAULT),
                    'email' => $input['email'],
                    'type' => $input['type']
                ));
                return $statement->rowCount();
            }catch(\PDOException $e){
                $this->logger->error("insert user: " . $e->getMessage());
            }
        }elseif(isset($input['score']) && isset($input['user_id'])){
            $statement = "
                insert into leaderboard
                    (score, user_id, dungeon_id)
                values
                    (:score, :user_id, :dungeon_id)";

            try{
                var_dump($input);
                $statement = $this->db->prepare($statement);
                $statement->execute(array(
                    'score' => $input['score'],
                    'user_id' => $input['user_id'],
                    'dungeon_id' => $input['dungeon_id']
                ));
                return $statement->rowCount();
            }catch(\PDOException $e){
                $this->logger->error("insert score: " . $e->getMessage());
            }
        }

    }

    public function update($id, Array $input)
    {
        $statement = "
            update leaderboard 
            set
                score = :score,
                user_id = :user_id,
                dungeon_id = :dungeon_id
            where user_id = :user_id
            and dungeon_id = :dungeon_id";

        try {
            $statement = $this->db->prepare($statement);
            $statement->execute(array(
                'id' => (int) $id,
                'score' => $input['score'],
                'user_id' => $input['user_id'],
                'dungeon_id' => $input['dungeon_id'],
            ));
            return $statement->rowCount();
        }catch(\PDOException $e){
            $this->logger->error("update: " . $e->getMessage());
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
            $this->logger->error("delete: " . $e->getMessage());
        }
    }
}