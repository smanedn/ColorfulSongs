<?php
use Illuminate\Database\Eloquent\Model;
class Leaderboard extends Model
{
    protected $table = 'leaderboard';

    public function user()
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function dungeon()
    {
        return $this->belongsTo(Dungeon::class,'dungeon_id');
    }

    public static function getData()
    {
        return self::select('user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->join('user', 'leaderboard.user_id', '=', 'user.id')
            ->orderBy('leaderboard.score', 'DESC')
            ->get();
    }

    public static function getDataByDungeonId($dungeon_id)
    {
        return self::select('user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->join('user', 'leaderboard.user_id', '=', 'user.id')
            ->join('dungeon', 'leaderboard.dungeon_id', '=', 'dungeon.id')
            ->where('dungeon_id', $dungeon_id)
            ->orderBy('leaderboard.score', 'DESC')
            ->get();
    }

    public static function getDataByUserId($userId)
    {
        return self::select('user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->join('friend', 'leaderboard.user_id', '=', 'friend.userId2')
            ->join('user', 'friend.userId1', '=', $userId)
            ->where('friend.pending','=',0)
            ->where('friend.userId1', '=', 'user.id')
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

    public static function getDataByDungeonAndFriend($dungeonId,$friendId)
    {
        return self::select('user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->distinct()
            ->join('user','leaderboard.user_id','=','user.id')
            ->join('dungeon','leaderboard.dungeon_id','=', 'dungeon.id')//$dungeonId
            ->join('friend','leaderboard.user_id','=','friend.userId1')//$friendId
            ->where('friend.pending','=',0)
            ->where('friend.userId1','=',$friendId)
            ->where('leaderboard.dungeon_id', '=', $dungeonId)
            ->orderBy('leaderboard.score','DESC')
            ->get();
    }
//$selectUserData = "SELECT DISTINCT user.username, leaderboard.score, leaderboard.dungeon_id
//                            from user JOIN leaderboard
//                            ON user.id = leaderboard.user_id
//                            JOIN dungeon
//                            ON leaderboard.dungeon_id = '$mapId'
//                            JOIN friend
//                            ON friend.idUtente1 = '$userId'
//                            AND friend.pending = 0
//                            AND friend.idUtente2 = user.id
//                            AND friend.idUtente2 = leaderboard.user_id
//                            order by leaderboard.score desc";
}