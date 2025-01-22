CREATE DATABASE magicportal;
USE magicportal;
CREATE TABLE user(
	id int AUTO_INCREMENT PRIMARY KEY,
	username VARCHAR(64) UNIQUE NOT NULL,
	password VARCHAR(256) NOT NULL,
	email VARCHAR(256) NOT NULL
);

CREATE TABLE dungeon(
	id int PRIMARY KEY,
	ssid int
);

CREATE TABLE friend(
	player_user_id int,
    friend_user_id int,
    FOREIGN KEY (player_user_id) REFERENCES user(id),
    FOREIGN KEY (friend_user_id) REFERENCES user(id),
    PRIMARY KEY(player_user_id, friend_user_id)
);

CREATE TABLE leaderboard(
	score int PRIMARY KEY,
    user_id int,
    dungeon_id int,
    FOREIGN KEY (user_id) REFERENCES user(id),
    FOREIGN KEY (dungeon_id) REFERENCES dungeon(id)
);

CREATE USER 'magicportal'@'localhost' IDENTIFIED BY 'Admin$00';
GRANT ALL PRIVILEGES ON magicportal.* TO 'magicportal'@'localhost';
FLUSH PRIVILEGES;
EXIT;