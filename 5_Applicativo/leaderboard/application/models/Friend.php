<?php
use Illuminate\Database\Eloquent\Model;

class Friend extends Model
{
    protected $table = 'friend';

    public function user()
    {
        return $this->belongsTo(User::class);
    }
}