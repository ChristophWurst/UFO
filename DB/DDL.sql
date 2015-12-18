-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema ufo
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `ufo` ;

-- -----------------------------------------------------
-- Schema ufo
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ufo` DEFAULT CHARACTER SET utf8 ;
USE `ufo` ;

-- -----------------------------------------------------
-- Table `ufo`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`user` ;

CREATE TABLE IF NOT EXISTS `ufo`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `email` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`category` ;

CREATE TABLE IF NOT EXISTS `ufo`.`category` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `description` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`country`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`country` ;

CREATE TABLE IF NOT EXISTS `ufo`.`country` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`artist`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`artist` ;

CREATE TABLE IF NOT EXISTS `ufo`.`artist` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `image` VARCHAR(45) NULL,
  `video` VARCHAR(45) NULL,
  `category_id` INT NOT NULL,
  `country_id` INT NOT NULL,
  `email` VARCHAR(45) NULL,
  `deleted` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  INDEX `fk_artist_category1_idx` (`category_id` ASC),
  INDEX `fk_artist_country1_idx` (`country_id` ASC),
  CONSTRAINT `fk_artist_category1`
    FOREIGN KEY (`category_id`)
    REFERENCES `ufo`.`category` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_artist_country1`
    FOREIGN KEY (`country_id`)
    REFERENCES `ufo`.`country` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`area`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`area` ;

CREATE TABLE IF NOT EXISTS `ufo`.`area` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`venue`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`venue` ;

CREATE TABLE IF NOT EXISTS `ufo`.`venue` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `area_id` INT NOT NULL,
  `short_desc` VARCHAR(45) NULL,
  `desc` VARCHAR(45) NULL,
  `longitude` DOUBLE NULL,
  `latitude` DOUBLE NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_location_area_idx` (`area_id` ASC),
  CONSTRAINT `fk_location_area`
    FOREIGN KEY (`area_id`)
    REFERENCES `ufo`.`area` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`timeslot`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`timeslot` ;

CREATE TABLE IF NOT EXISTS `ufo`.`timeslot` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `start` TIME NULL,
  `end` TIME NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`spectacleday`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`spectacleday` ;

CREATE TABLE IF NOT EXISTS `ufo`.`spectacleday` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `day` DATE NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `day_UNIQUE` (`day` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`spectacleday_timeslot`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`spectacleday_timeslot` ;

CREATE TABLE IF NOT EXISTS `ufo`.`spectacleday_timeslot` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `timeslot_id` INT NOT NULL,
  `spectacleday_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_spectacle_has_timeslot_timeslot1_idx` (`timeslot_id` ASC),
  INDEX `fk_spectacleday_spectacleday1_idx` (`spectacleday_id` ASC),
  CONSTRAINT `fk_spectacle_has_timeslot_timeslot1`
    FOREIGN KEY (`timeslot_id`)
    REFERENCES `ufo`.`timeslot` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_spectacleday_spectacleday1`
    FOREIGN KEY (`spectacleday_id`)
    REFERENCES `ufo`.`spectacleday` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`performance`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`performance` ;

CREATE TABLE IF NOT EXISTS `ufo`.`performance` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `artist_id` INT NOT NULL,
  `venue_id` INT NOT NULL,
  `spectacleday_timeslot_id` INT NOT NULL,
  INDEX `fk_performance_artist1_idx` (`artist_id` ASC),
  INDEX `fk_performance_venue1_idx` (`venue_id` ASC),
  INDEX `fk_performance_spectacleday_timeslot1_idx` (`spectacleday_timeslot_id` ASC),
  PRIMARY KEY (`id`),
  UNIQUE INDEX `artist_timeslot` (`artist_id` ASC, `spectacleday_timeslot_id` ASC),
  UNIQUE INDEX `venue_timeslot` (`venue_id` ASC, `spectacleday_timeslot_id` ASC),
  CONSTRAINT `fk_performance_artist1`
    FOREIGN KEY (`artist_id`)
    REFERENCES `ufo`.`artist` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_performance_venue1`
    FOREIGN KEY (`venue_id`)
    REFERENCES `ufo`.`venue` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_performance_spectacleday_timeslot1`
    FOREIGN KEY (`spectacleday_timeslot_id`)
    REFERENCES `ufo`.`spectacleday_timeslot` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
