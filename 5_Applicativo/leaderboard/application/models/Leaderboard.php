<?php
use Illuminate\Database\Eloquent\Model;
use Monolog\Logger;
use Monolog\Handler\StreamHandler;
class Leaderboard extends Model
{
    protected $table = 'leaderboard';
    private $log;

    public function __construct()
    {
        $this->log = new Logger('leaderboard');
        $this->log->pushHandler(new StreamHandler('application/logs/log.log'));
    }

    public function user()
    {
        return $this->belongsTo(User::class, 'user_id');
    }


    public static function getData()
    {
        return self::select('user.id','user.username as username', 'leaderboard.score')
            ->distinct()
            ->join('user', 'leaderboard.user_id', '=', 'user.id')
            ->orderBy('leaderboard.score', 'ASC')
            ->get();
    }

    public static function getDataByUsername($username)
    {
        return self::select('user.username as username', 'leaderboard.score')
            ->distinct()
            ->join('user', 'leaderboard.user_id', '=', 'user.id')
            ->where('user.username', 'like' , "%$username%")
            ->orderBy('leaderboard.score', 'ASC')
            ->get();
    }
}