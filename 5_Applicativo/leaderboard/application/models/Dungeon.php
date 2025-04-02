<?php
use Illuminate\Database\Eloquent\Model;
class Dungeon extends Model
{
    protected $table = 'dungeon';

    public function leaderboard()
    {
        return $this->hasMany(leaderboardController::class);
    }

}