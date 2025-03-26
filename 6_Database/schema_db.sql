CREATE DATABASE colorfulsongs;
USE colorfulsongs;
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
    userId1 int,
    userId2 int,
    pending boolean,
    FOREIGN KEY (userId1) REFERENCES user(id) ON DELETE no action,
    FOREIGN KEY (userId2) REFERENCES user(id) ON DELETE no action,
    PRIMARY KEY(userId1, userId2)
);

CREATE TABLE leaderboard(
	score int PRIMARY KEY,
    user_id int,
    dungeon_id int,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE,
    FOREIGN KEY (dungeon_id) REFERENCES dungeon(id) ON DELETE CASCADE
);

CREATE USER 'colorfulsongs'@'%' IDENTIFIED BY 'Admin$00';
GRANT ALL PRIVILEGES ON colorfulsongs.* TO 'colorfulsongs'@'%';
FLUSH PRIVILEGES;

