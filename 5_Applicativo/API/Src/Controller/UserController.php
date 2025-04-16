<?php
namespace Src\Controller;
use Src\UserGateways\UserGateway;
use Monolog\Logger;
use Monolog\Handler\StreamHandler;
class UserController
{
    private $db;
    private $requestMethod;
    private $userId;
    private $userGateway;
    private $logger;
    public function __construct($db, $requestMethod, $userId)
    {
        $this->db = $db;
        $this->requestMethod = $requestMethod;
        $this->userId = $userId;
        $this->userGateway = new UserGateway($db);
        $this->logger = new Logger("UserController");
        $this->logger->pushHandler(new StreamHandler('../log/errorLog.log'));
    }

    public function processRequest()
    {
        switch($this->requestMethod){
            case 'GET':
                if($this->userId){
                    $response = $this->getUser($this->userId);
                }else{
                    $response = $this->getAllUsers();
                }
                break;
            case 'POST':
                $response = $this->createFromRequest();
                break;
            case 'PUT':
                $response = $this->updateScoreFromRequest($this->userId);
                break;
            case 'DELETE':
                $response = $this->deleteUser($this->userId);
                break;
            default:
                $response = $this->notFoundResponse();
                break;
        }
        header($response['status_code_header']);
        if ($response['body']){
            echo $response['body'];
        }
    }

    private function getAllUsers()
    {
        $result = $this->userGateway->findAll();
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $this->logger->info("getAllUser: HTTP/1.1 200 OK");
        $response['body'] = json_encode($result);
        return $response;
    }

    private function getUser($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            $this->logger->error("getUser: HTTP/1.1 404 Not Found");
            return $this->notFoundResponse();
        }
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $this->logger->info("getUser: HTTP/1.1 200 OK");
        $response['body'] = json_encode($result);
        return $response;
    }

    private function createFromRequest()
    {
        $input = (array) json_decode(file_get_contents('php://input'), true);
//        if (! $this->validateUser($input) || ! $this->validateData($input)){
//            return $this->unprocessableEntityResponse();
//        }
        $this->userGateway->insert($input);
        $response['status_code_header'] = 'HTTP/1.1 201 CREATED';
        $this->logger->info("createFromRequest: HTTP/1.1 201 CREATED");
        $response['body'] = null;
        return $response;
    }

    private function updateScoreFromRequest($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            $this->logger->error($this->notFoundResponse());
            return $this->notFoundResponse();
        }
        $input = (array) json_decode(file_get_contents('php://input'), true);
        if (! $this->validateData($input)){
            return $this->unprocessableEntityResponse();
        }
        $this->userGateway->update($userId, $input);
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $this->logger->info("updateScoreFromRequest: HTTP/1.1 200 OK");
        $response['body'] = null;
        return $response;
    }

    private function deleteUser($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            $this->logger->error("deleteUser: HTTP/1.1 404 Not Found");
            return $this->notFoundResponse();
        }
        $this->userGateway->delete($userId);
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $this->logger->info("deleteUser: HTTP/1.1 200 OK");
        $response['body'] = null;
        return $response;
    }

    private function validateUser($input)
    {
        if (! isset($input['username'])){
            return false;
        }
        if (! isset($input['email'])){
            return false;
        }
        return true;
    }

    private function validateData($input)
    {
        if (! isset($input['score'])){
            return false;
        }
        if (! isset($input['user_id'])){
            return false;
        }
        if (! isset($input['dungeon_id'])){
            return false;
        }
        return true;
    }

    private function unprocessableEntityResponse()
    {
        $response['status_code_header'] = 'HTTP/1.1 422 Unprocessable Entity';
        $response['body'] = json_encode([
            'error' => 'Invalid input'
        ]);
        return $response;
    }

    private function notFoundResponse()
    {
        $response['status_code_header'] = 'HTTP/1.1 404 Not Found';
        $response['body'] = null;
        return $response;
    }

}
