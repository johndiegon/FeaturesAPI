CREATE TABLE `Calendar` (
  `id` int NOT NULL AUTO_INCREMENT,
  `clientId` varchar(250) NOT NULL,
  `dateTime` datetime,
  `status` tinyint(1) NOT NULL DEFAULT '0',
  `sent` tinyint(1) NOT NULL DEFAULT '0',
  `count` decimal,
  `template` varchar(4000) NOT NULL,
  `params` varchar(4000) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2821 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
