CREATE TABLE `direct_api`.`Chat` (
  `id` INT auto_increment,
  `ContactId` VARCHAR(250) NOT NULL DEFAULT 's/n',
  `Sender` VARCHAR(1) NULL, 
  `PhoneFrom` VARCHAR(45) NOT NULL,
  `PhoneTo` VARCHAR(45) NOT NULL,
  `DateTime` DATETIME DEFAULT CURRENT_TIMESTAMP,
  `Message` VARCHAR(4000) NOT NULL,
  `UrlPicture` VARCHAR(1000) NULL,
  `WasVisible` bool NOT NULL default false, 
  `Template` VARCHAR(250) NOT NULL,
  `bAnswerButton` bool NOT NULL default false, 
  PRIMARY KEY (`id`));

CREATE TABLE `direct_api`.`Order` (
`id` INT auto_increment,
`ContactId` VARCHAR(250) NOT NULL DEFAULT 's/n',
`OrderIdClient` VARCHAR(250) NOT NULL DEFAULT 's/n', 
`DateOrder` DATETIME NOT NULL ,
`DateOrderEnd` DATETIME NULL ,
`PriceItems` VARCHAR(250) NULL,
`PriceDelivery` VARCHAR(250) NULL,
`Discount` VARCHAR(250) NULL,
`Total` VARCHAR(250) NULL,
PRIMARY KEY (`id`));
