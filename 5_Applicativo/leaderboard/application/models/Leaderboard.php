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
        return self::select('user.id','user.username as username', 'leaderboard.score', 'leaderboard.dungeon_id')
            ->distinct()
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
}