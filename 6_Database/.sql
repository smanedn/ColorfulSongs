CREATE DATABASE magicportal;
USE magicportal;
CREATE TABLE user(
	id int AUTO_INCREMENT PRIMARY KEY,
	username VARCHAR(64) UNIQUE NOT NULL,
	password VARCHAR(256) NOT NULL,
	name VARCHAR(32) NOT NULL,
	surname VARCHAR(32) NOT NULL,
	email VARCHAR(256) NOT NULL
);
	
CREATE TABLE leaderboard(
	score int PRIMARY KEY
);

CREATE TABLE dungeon(
	id int PRIMARY KEY,
	ssid int
);