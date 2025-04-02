<?php
use Illuminate\Database\Eloquent\Model;

class User extends Model
{
    protected $table = 'user';

    public function friend()
    {
        return $this->hasMany(Friend::class);
    }

    public function leaderboard()
    {
        return $this->hasMany(leaderboardController::class);
    }

    public static function getDataByUserId($userId)
    {
        return self::select('user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->join('friend', 'friend.userId1', '=', $userId)
            ->where('friend.pending','=',0)
            ->where('friend.userId2', '=', 'user.id')
            ->join('leaderboard', 'friend.userId2', '=', 'leaderboard.user_id')
            ->orderBy('leaderboard.score', 'DESC')
            ->get();
    }

    //$selectUserData = "SELECT user.username, leaderboard.score ,leaderboard.dungeon_id
//                            from user JOIN friend
//                                ON friend.userId1 = '$userId'
//                                AND friend.pending = 0
//                                AND friend.userId2 = user.id
//                            JOIN leaderboard
//                                ON friend.userId2 = leaderboard.user_id
//                            order by leaderboard.score desc";

}