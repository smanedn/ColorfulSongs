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

    public static function getDataByFriendId($userId)
    {
        return self::select('u.username as username', 'l.score', 'u.id as id')
            ->distinct()
            ->join('friend as f', 'f.userId1', '=', 'user.id')
            ->join('user as u', 'f.userId2', '=', 'u.id')
            ->join('leaderboard as l', 'f.userId2', '=', 'l.user_id')
            ->where('f.userId1', '=', $userId)
            ->where('f.pending', '=', 0)
            ->orderBy('l.score', 'ASC')
            ->get();
    }
}