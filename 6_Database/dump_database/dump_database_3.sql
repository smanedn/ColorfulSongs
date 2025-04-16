CREATE DATABASE  IF NOT EXISTS `colorfulsongs` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `colorfulsongs`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: colorfulsongs
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `dungeon`
--

DROP TABLE IF EXISTS `dungeon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dungeon` (
  `id` int(11) NOT NULL,
  `ssid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dungeon`
--

LOCK TABLES `dungeon` WRITE;
/*!40000 ALTER TABLE `dungeon` DISABLE KEYS */;
INSERT INTO `dungeon` VALUES (1,NULL),(2,NULL),(3,NULL),(4,NULL),(5,NULL),(6,NULL),(7,NULL),(8,NULL),(9,NULL),(10,NULL),(11,NULL),(12,NULL),(13,NULL),(14,NULL),(15,NULL),(16,NULL),(17,NULL),(18,NULL),(19,NULL),(20,NULL);
/*!40000 ALTER TABLE `dungeon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `friend`
--

DROP TABLE IF EXISTS `friend`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `friend` (
  `userId1` int(11) NOT NULL,
  `userId2` int(11) NOT NULL,
  `pending` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`userId1`,`userId2`),
  KEY `userId2` (`userId2`),
  CONSTRAINT `friend_ibfk_1` FOREIGN KEY (`userId1`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  CONSTRAINT `friend_ibfk_2` FOREIGN KEY (`userId2`) REFERENCES `user` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `friend`
--

LOCK TABLES `friend` WRITE;
/*!40000 ALTER TABLE `friend` DISABLE KEYS */;
INSERT INTO `friend` VALUES (1,3,0),(1,5,0),(3,1,0),(3,5,1),(5,1,0),(5,3,1);
/*!40000 ALTER TABLE `friend` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `leaderboard`
--

DROP TABLE IF EXISTS `leaderboard`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `leaderboard` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `score` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `dungeon_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `dungeon_id` (`dungeon_id`),
  CONSTRAINT `leaderboard_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
  CONSTRAINT `leaderboard_ibfk_2` FOREIGN KEY (`dungeon_id`) REFERENCES `dungeon` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `leaderboard`
--

LOCK TABLES `leaderboard` WRITE;
/*!40000 ALTER TABLE `leaderboard` DISABLE KEYS */;
INSERT INTO `leaderboard` VALUES (1,1001,1,1),(2,2301,1,2),(3,5001,2,3),(4,11100,4,4),(5,490641,4,4),(6,62650,5,9),(7,110652,2,9),(8,56414,3,9),(9,6540,9,5),(10,6945,7,6),(11,1469,6,8),(12,14015,5,1),(13,6540,9,5),(14,6432,7,19),(15,1554,12,18),(16,1137,12,9),(17,1812,10,10),(18,5961,8,20),(19,1139,16,10),(20,1653,13,13),(21,3210,19,11),(22,15022,8,14),(23,5611,20,15),(24,1227,22,13),(25,15630,23,16);
/*!40000 ALTER TABLE `leaderboard` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(64) NOT NULL,
  `password` varchar(256) NOT NULL,
  `email` varchar(256) NOT NULL,
  `type` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','$2y$10$bgq0b4qZz6RvOW1pXCQC4ObVFtMGugzG70i8A6OiZdp6nyMzhaIHS','admin@colorfulsongs.ti.ch','admin'),(2,'msans1','$2a$04$JCmjW/tfXDd68b2etgVZfOzskcIs0ZtdzVNTNDIfMw4FmXqgfSLKG','hfortun1@adobe.com','user'),(3,'adelleschi2','$2a$04$xJB1xrjOu6U3ZoSs0vDm1.p520A6c.az9/O7dnP7M0ltU4SDOV.pq','tchesman2@bandcamp.com','user'),(4,'gcoucher3','$2a$04$g2/XfDWmY9aTmPqZaMgt5.ViVpey/fb59tGzZmC4HXSQ96fkYUqAC','ebottrill3@artisteer.com','user'),(5,'pmayman4','$2a$04$ghv8rpdFgGJdhGLyrwDwDOoDCUetFP8sgvNOTDU.ouY6NS/x.bb/q','rakenhead4@123-reg.co.uk','user'),(6,'pchadbourne5','$2a$04$WQ/np1ZnxkNuDyTCy7hweeU3rFYS.icgoO2dit.VqfTZ3x4A1TAAS','jponceford5@theglobeandmail.com','user'),(7,'hpengilley6','$2a$04$MwhAGGIIXISMqqlcg8lIr.nseiKnI0jzWTFnfl6IZngbEJdoaoID2','lmcbain6@craigslist.org','user'),(8,'tfibbens7','$2a$04$.0aU8eubWa1svKAp9qlO1.NdPe5SUGRJ4qqKiKmjM0Pk.3SuLYczq','adukelow7@amazon.de','user'),(9,'sbrentnall8','$2a$04$DMLsCV.VkFSbSQowbg4.Lu1YurlcHGoPJZ9/5SOHJx6dvC121J8h.','rstorer8@ocn.ne.jp','user'),(10,'nstretton9','$2a$04$UMeCdoFZ0ltgN5H9H92Xruuv5C3K0kZWgFeiaRe8JBRhqDyZqeoyK','mphizacklea9@sbwire.com','user'),(11,'jfobidge0','$2a$04$Lke/iWUCNrkb1POaGjecB.nSLCxiaVwDnlpP1/Q4YCeBJ.9jEGG1.','mdahlen0@wordpress.com','user'),(12,'tfeilden1','$2a$04$PsuJWmZuBICIY2yd/GZbdOI5JiJliYv/AWK5oOz3ir6PC8Bxnv27e','mgonnard1@barnesandnoble.com','user'),(13,'bsharratt2','$2a$04$Y1uQ7aAu1mbnmkKppHqKVOIg/F1HrMynivRtrIUetAVUfoKfCda3.','jdacth2@dropbox.com','user'),(14,'rclimar3','$2a$04$dB702OsdU8xSqji4s891FuqP/qNF2eW1CNjLZCbkIe8yI8fdGQS4e','mwarnes3@japanpost.jp','user'),(15,'egilvear4','$2a$04$gADBDmfUX1Sa0/xne.7sCOPNRk2IAKHxBfAVh3qcDrXNnS144Cm5.','cslowey4@xinhuanet.com','user'),(16,'edonnel5','$2a$04$iD68II.BlujRussB3mjCReClJUIWFUYBv0UbiV4.4iJzg3N8Qvqum','glumsdaine5@ask.com','user'),(17,'bbaggally6','$2a$04$Zpn.JZs7NH2LWLCZUl4YAeQcDbJ0wF1eVhlhlrSarz7AG5eSUhLg.','jsamber6@google.ca','user'),(18,'eternault7','$2a$04$b3Z7wK6JMr0LbO18UIT8l.lAMCiwfYGne1D0RJatSlc4t7jdPiMge','estilgo7@msn.com','user'),(19,'astockney8','$2a$04$wUIcX.CLaw4m3szXvNAMcemSrH3kFLoCjKsQnw4T4TcufrUtQqxnq','fquin8@booking.com','user'),(20,'rblazy9','$2a$04$ilci55Km2F8LyQlcXFOcMui0Qcoc5P9iy3nPmBEhWNMlhVkZQAYJy','lloads9@aol.com','user'),(21,'user','$2y$10$pAPwK6dhR1fCmUndZ6ETCuTSV5uhZGYRen/CPVf7VZrMVcjdbDcD6','user@colorfulsongs.ti.ch','user'),(22,'ajdnn','$2y$10$mZrUO9A0D1zvid76J7eoTOS2ntHZNHWPpynCF.T1v1uQxO.wHqfEe','ajdnn@colorfulsongs.ti.ch','user'),(23,'dainc','$2y$10$7Bx9DglZ6OMuW6pSJT8BiOg30cHFBhwzaAZe1XbqraYUcQzgUTm7a','dainc@colorfulsongs.ti.ch','user');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-16 15:26:31
