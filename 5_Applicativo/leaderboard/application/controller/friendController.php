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
        if (isset($_SESSION['userType'])) {
            $friends = Friend::friendRequestWithFriendId($_SESSION['UserId']);
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/notifications/index.php';
        }else{
            header("Location:" . URL);
        }
    }

    public function acceptRequest($userId)
    {
        if (isset($_SESSION['userType'])) {
            $friend = Friend::create(["userId1" => $_SESSION['UserId'], "userId2" => $userId, "pending" => 0]);
            $friendUpdate = Friend::where('userId1', $userId)->where('userId2', $_SESSION['UserId'])->update(['pending' => 0]);
            require_once 'application/views/_templates/header.php';
            require_once 'application/views/notifications/index.php';
        }else{
            header("Location:" . URL);
        }
    }

    public function friendRequest($friendId)
    {
        if (isset($_SESSION['userType'])) {
            $showFriends = Friend::showFriend($_SESSION['UserId']);
            $friendIds = [];
            foreach ($showFriends as $friend) {
                if ($friend->userId1 == $_SESSION['UserId']) {
                    $friendIds[] = $friend->userId2;
                } else {
                    $friendIds[] = $friend->userId1;
                }
            }
            try {
                $friend = Friend::create(["userId1" => $_SESSION['UserId'], "userId2" => $friendId, "pending" => 1]);

            } catch (\Exception $e) {
                $this->log->warning($e->getMessage());
            }


            require_once 'application/models/Leaderboard.php';
            if ($this->isAdmin()) {
                $leaderboard_data = Leaderboard::getData();
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/admin/index.php';
            } else {
                $leaderboard_data = Leaderboard::getData();
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/leaderboard/index.php';
            }
        }else{
            header("Location:" . URL);
        }

    }

    public function removeFriend($id)
    {
        if (isset($_SESSION['userType'])) {
            try {
                $friend = Friend::where('userId1', $id)
                    ->where('userId2', $_SESSION['UserId'])
                    ->delete();
                $friend = Friend::where('userId2', $id)
                    ->where('userId1', $_SESSION['UserId'])
                    ->delete();

            } catch (\Exception $e) {
                $this->log->warning($e->getMessage());
            }
            require_once 'application/models/Leaderboard.php';
            if ($this->isAdmin()) {
                $leaderboard_data = User::getDataByFriendId($_SESSION['UserId']);
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/admin/index.php';
            } else {
                $leaderboard_data = User::getDataByFriendId($_SESSION['UserId']);
                require_once 'application/views/_templates/header.php';
                require_once 'application/views/leaderboard/index.php';
            }
        }else{
            header("Location:" . URL);
        }
    }

}