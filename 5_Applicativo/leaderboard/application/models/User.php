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
        return $this->hasMany(Leaderboard::class);
    }

    public static function getDataByUserId($userId)
    {
        return self::select('u.username as username', 'l.score', 'l.dungeon_id')
            ->join('friend as f', 'f.userId1', '=', 'user.id')
            ->join('user as u', 'f.userId2', '=', 'u.id')
            ->join('leaderboard as l', 'f.userId2', '=', 'l.user_id')
            ->where('f.userId1', '=', $userId)
            ->where('f.pending', '=', 0)
            ->orderBy('l.score', 'DESC')
            ->get();
    }

    public static function getDataByDungeonAndFriend($dungeonId,$friendId)
    {
        return self::select('user.username as username', 'l.score', 'l.dungeon_id')
            ->distinct()
            ->join('leaderboard as l','l.user_id','=','user.id')
            ->join('dungeon as d','l.dungeon_id','=', 'd.id')//$dungeonId
            ->join('friend as f','l.user_id','=','f.userId1')//$friendId
            ->where('f.pending','=',0)
            ->where('f.userId2','=',$friendId)
            ->where('l.dungeon_id', '=', $dungeonId)
            ->orderBy('l.score','DESC')
            ->get();
    }
}