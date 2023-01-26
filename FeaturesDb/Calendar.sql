CREATE TABLE `Calendar` (
  `id` int NOT NULL AUTO_INCREMENT,
  `clientId` varchar(250) NOT NULL DEFAULT 's/n',
  `dateTime` datetime DEFAULT NULL,
  `status` tinyint(1) NOT NULL DEFAULT '0',
  `sent` tinyint(1) NOT NULL DEFAULT '0',
  `count` decimal(10,0) DEFAULT NULL,
  `template` varchar(4000) NOT NULL,
  `params` varchar(4000) NOT NULL,
  `filters` varchar(4000) DEFAULT NULL,
  `update` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
