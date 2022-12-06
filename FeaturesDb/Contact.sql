CREATE TABLE `direct_api`.`Contact` (
  `id` INT auto_increment,
  `Name` VARCHAR(250) NOT NULL DEFAULT 's/n',
  `Email` VARCHAR(250) NULL,
  `Phone` VARCHAR(45) NOT NULL,
  `IdClient` VARCHAR(45) NOT NULL,
  `DateInclude` DATETIME DEFAULT CURRENT_TIMESTAMP,
  `DateBirth` DATETIME NULL,
  `Unity` VARCHAR(250) NULL,
  `Classification` VARCHAR(250) NULL,
  `Status` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`));
