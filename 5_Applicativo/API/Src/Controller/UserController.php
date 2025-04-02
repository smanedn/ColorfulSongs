<?php
namespace Src\Controller;
use Src\UserGateways\UserGateway;
class UserController
{
    private $db;
    private $requestMethod;
    private $userId;
    private $userGateway;
    public function __construct($db, $requestMethod, $userId)
    {
        $this->db = $db;
        $this->requestMethod = $requestMethod;
        $this->userId = $userId;
        $this->userGateway = new UserGateway($db);
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
                $response = $this->createUserFromRequest();
                break;
            case 'PUT':
                $response = $this->updateUserFromRequest($this->userId);
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
        $response['body'] = json_encode($result);
        return $response;
    }

    private function getUser($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            return $this->notFoundResponse();
        }
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $response['body'] = json_encode($result);
        return $response;
    }

    private function createUserFromRequest()
    {

        $input = (array) json_decode(file_get_contents('php://input'), true);
        if (! $this->validateUser($input)){
            return $this->unprocessableEntityResponse();
        }
        $this->userGateway->insert($input);
        $response['status_code_header'] = 'HTTP/1.1 201 CREATED';
        $response['body'] = null;
        return $response;
    }

    private function updateUserFromRequest($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            return $this->notFoundResponse();
        }
        $input = (array) json_decode(file_get_contents('php://input'), true);
        if (! $this->validateUser($input)){
            return $this->unprocessableEntityResponse();
        }
        $this->userGateway->update($userId, $input);
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
        $response['body'] = null;
        return $response;
    }

    private function deleteUser($userId)
    {
        $result = $this->userGateway->find($userId);
        if (! $result){
            return $this->notFoundResponse();
        }
        $this->userGateway->delete($userId);
        $response['status_code_header'] = 'HTTP/1.1 200 OK';
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
