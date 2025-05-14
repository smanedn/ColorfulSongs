<?php
use Monolog\Handler\StreamHandler;
use Monolog\Logger;

require_once 'vendor/autoload.php';

class FriendController
{
    private $log;

    public function __construct()
    {
        $this->log = new Logger('notify');
        $this->log->pushHandler(new StreamHandler('application/logs/log.log'));
    }

    public function isAdmin(){
        if (session_status() === PHP_SESSION_NONE) {
            session_start();
        }
        if ($_SESSION['userType'] == 'admin') {
            return true;
        }
    }

    public function newFriend()
    {
        $friends = Friend::friendRequestWithFriendName($_SESSION['UserId']);

        require_once 'application/views/_templates/header.php';
        require_once 'application/views/notifications/index.php';

    }

    public function acceptRequest($userId)
    {
        $friend = Friend::create(["userId1" => $_SESSION['UserId'], "userId2" => $userId, "pending" => 0]);
        $friendUpdate = Friend::where('userId1', $userId)->where('userId2', $_SESSION['UserId'])->update(['pending'=> 0]);
        require_once 'application/views/_templates/header.php';
        require_once 'application/views/notifications/index.php';
    }

    public function friendRequest($friendId)
    {


            try {
                $friend = Friend::create(["userId1" => $_SESSION['UserId'], "userId2" => $friendId, "pending" => 1]);

            }catch (\Exception $e){
                $this->log->warning($e->getMessage());
            }


            require_once 'application/models/Leaderboard.php';
            if ($this->isAdmin()){
                $leaderboard_data = Leaderboard::getData();
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/admin/index.php';
            }else{
                $leaderboard_data = Leaderboard::getData();
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/leaderboard/index.php';
            }

    }

    public function removeFriend($id)
    {
        try {
            $friend = Friend::where('userId1', $id)
                    ->where('userId2', $_SESSION['UserId'])
                    ->delete();
            $friend = Friend::where('userId2', $id)
                ->where('userId1', $_SESSION['UserId'])
                ->delete();

        }catch (\Exception $e){
            $this->log->warning($e->getMessage());
        }
        require_once 'application/models/Leaderboard.php';
        if ($this->isAdmin()){
            $leaderboard_data = User::getDataByFriendId($_SESSION['UserId']);
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/admin/index.php';
        }else{
            $leaderboard_data = User::getDataByFriendId($_SESSION['UserId']);
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/leaderboard/index.php';
        }
    }

}