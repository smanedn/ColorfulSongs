CREATE DATABASE if not exists colorfulsongs;
USE colorfulsongs;
CREATE TABLE user(
	id int AUTO_INCREMENT PRIMARY KEY,
	username VARCHAR(64) UNIQUE NOT NULL,
	password VARCHAR(256) NOT NULL,
	email VARCHAR(256) NOT NULL,
    type varchar(10) NOT NULL
);

CREATE TABLE friend(
	user_id1 int,
    user_id2 int,
    belongs boolean,
    FOREIGN KEY (user_id1) REFERENCES user(id) on delete cascade,
    FOREIGN KEY (user_id2) REFERENCES user(id) on delete cascade,
    PRIMARY KEY(user_id1, user_id2)
);

CREATE TABLE leaderboard(
	id int primary key auto_increment,
	score time,
    user_id int,
    FOREIGN KEY (user_id) REFERENCES user(id) on delete cascade
);

CREATE USER 'colorfulsongs'@'%' IDENTIFIED BY 'Admin$00';
GRANT ALL PRIVILEGES ON colorfulsongs.* TO 'colorfulsongs'@'%';
FLUSH PRIVILEGES;

delimiter // 
create trigger leaderboardScoreUpdate 
before update on leaderboard for each row begin 
	if new.score > old.score then 
		set new.score = new.score;
	elseif old.score = null then
		set new.score = new.score;
	else
		set new.score = old.score;
	end if; 
end;
// delimiter ;

-- drop trigger leaderboardScoreUpdate;