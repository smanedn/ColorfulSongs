CREATE DATABASE magicportal;
USE magicportal;
CREATE TABLE user(
	id int AUTO_INCREMENT PRIMARY KEY,
	username VARCHAR(64) UNIQUE NOT NULL,
	password VARCHAR(256) NOT NULL,
	email VARCHAR(256) NOT NULL,
    friend_code VARCHAR(10) unique
);

CREATE TABLE dungeon(
	id int PRIMARY KEY,
	ssid int
);

CREATE TABLE friend(
	user_id int,
    user_friend_code varchar(10),
    FOREIGN KEY (user_id) REFERENCES user(id),
    FOREIGN KEY (user_friend_code) REFERENCES user(friend_code),
    PRIMARY KEY(user_id, user_friend_code)
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
