CREATE DATABASE if not exists colorfulsongs;
USE colorfulsongs;
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
	user_id1 int,
    user_id2 int,
    belongs boolean,
    FOREIGN KEY (user_id1) REFERENCES user(id) on delete cascade,
    FOREIGN KEY (user_id2) REFERENCES user(id) on delete cascade,
    PRIMARY KEY(user_id1, user_id2)
);

CREATE TABLE leaderboard(
	score int PRIMARY KEY,
    user_id int,
    dungeon_id int,
    FOREIGN KEY (user_id) REFERENCES user(id) on delete cascade,
    FOREIGN KEY (dungeon_id) REFERENCES dungeon(id) on delete cascade
);

CREATE USER 'colorfulsongs'@'%' IDENTIFIED BY 'Admin$00';
GRANT ALL PRIVILEGES ON colorfulsongs.* TO 'colorfulsongs'@'%';
FLUSH PRIVILEGES;

