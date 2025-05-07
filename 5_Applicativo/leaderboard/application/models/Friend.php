<?php
use Illuminate\Database\Eloquent\Model;

class Friend extends Model
{
    protected $table = 'friend';
    protected $fillable = ['userId1', 'userId2', 'pending'];
    public $timestamps = false;

    public function user()
    {
        return $this->belongsTo(User::class,'userId1');
    }

    public static function friendRequestWithFriendName($userId)
    {
        return self::select('user.username as username', 'friend.userId2', 'friend.userId1')
            ->distinct()
            ->join('user', 'friend.userId1', '=', 'user.id')
            ->where('friend.userId2', '=', $userId)
            ->where('friend.pending', '=', 1)
            ->get();
    }
}