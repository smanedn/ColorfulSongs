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

}