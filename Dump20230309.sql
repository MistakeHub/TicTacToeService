CREATE DATABASE  IF NOT EXISTS `tttservice` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `tttservice`;
-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: tttservice
-- ------------------------------------------------------
-- Server version	8.0.31

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
-- Table structure for table `games`
--

DROP TABLE IF EXISTS `games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `games` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Iduser1` int DEFAULT NULL,
  `Iduser2` int DEFAULT NULL,
  `Idwinner` int DEFAULT NULL,
  `Dategamestart` datetime DEFAULT NULL,
  `Idcurrentturn` int DEFAULT NULL,
  `Isfinished` tinyint DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_FIRST_USER_ID_idx` (`Iduser1`),
  KEY `FK_SECOND_USER_ID_idx` (`Iduser2`),
  CONSTRAINT `FK_FIRST_USER_ID` FOREIGN KEY (`Iduser1`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_SECOND_USER_ID` FOREIGN KEY (`Iduser2`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `games`
--

LOCK TABLES `games` WRITE;
/*!40000 ALTER TABLE `games` DISABLE KEYS */;
INSERT INTO `games` VALUES (2,1,2,1,'2023-03-08 18:41:13',2,1),(3,1,2,2,'2023-03-08 20:42:24',1,1),(4,1,2,-1,'2023-03-08 20:50:19',1,0),(5,1,2,2,'2023-03-08 20:54:08',1,1);
/*!40000 ALTER TABLE `games` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `moves`
--

DROP TABLE IF EXISTS `moves`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `moves` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Iduser` int DEFAULT NULL,
  `Row` int NOT NULL,
  `Column` int NOT NULL,
  `Idgame` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_GAME_MOVE_idx` (`Idgame`),
  KEY `FK_USER_MOVE_idx` (`Iduser`),
  CONSTRAINT `FK_GAME_MOVE` FOREIGN KEY (`Idgame`) REFERENCES `games` (`Id`),
  CONSTRAINT `FK_USER_MOVE` FOREIGN KEY (`Iduser`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `moves`
--

LOCK TABLES `moves` WRITE;
/*!40000 ALTER TABLE `moves` DISABLE KEYS */;
INSERT INTO `moves` VALUES (1,1,1,1,2),(2,2,1,2,2),(3,1,2,1,2),(4,2,1,3,2),(5,1,2,3,2),(6,2,2,2,2),(7,1,3,1,2),(8,1,1,1,3),(9,2,1,2,3),(10,1,1,1,4),(11,2,1,2,4),(12,1,2,1,4),(13,2,1,3,4),(14,1,3,1,4),(15,2,1,1,5),(16,1,1,2,5),(17,2,2,1,5),(18,1,1,3,5),(19,2,3,1,5);
/*!40000 ALTER TABLE `moves` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(10) DEFAULT NULL,
  `Password` varchar(70) DEFAULT NULL,
  `Totalgames` int DEFAULT NULL,
  `Totalwins` int DEFAULT NULL,
  `Totallosses` int DEFAULT NULL,
  `Registerdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'d','d',1,0,1,'2008-03-20 23:00:00'),(2,'v','v',1,1,0,'2012-05-20 00:00:00'),(5,'Heyo123','328be0b33c2eedf653970509126f7d71',0,0,0,'2023-03-08 23:56:46');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-09  0:36:11
