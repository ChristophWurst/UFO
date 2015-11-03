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
  `id` INT NOT NULL,
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
  `id` INT NOT NULL,
  `description` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`country`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`country` ;

CREATE TABLE IF NOT EXISTS `ufo`.`country` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`artist`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`artist` ;

CREATE TABLE IF NOT EXISTS `ufo`.`artist` (
  `id` INT NOT NULL,
  `firstname` VARCHAR(45) NOT NULL,
  `lastname` VARCHAR(45) NOT NULL,
  `image` VARCHAR(45) NULL,
  `video` VARCHAR(45) NULL,
  `category_id` INT NOT NULL,
  `country_id` INT NOT NULL,
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
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`location` ;

CREATE TABLE IF NOT EXISTS `ufo`.`location` (
  `id` INT NOT NULL,
  `area_id` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `longitude` FLOAT NULL,
  `latitude` FLOAT NULL,
  PRIMARY KEY (`id`, `area_id`),
  INDEX `fk_location_area_idx` (`area_id` ASC),
  CONSTRAINT `fk_location_area`
    FOREIGN KEY (`area_id`)
    REFERENCES `ufo`.`area` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ufo`.`performance`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ufo`.`performance` ;

CREATE TABLE IF NOT EXISTS `ufo`.`performance` (
  `id` INT NOT NULL,
  `start` DATETIME NULL,
  `end` DATETIME NULL,
  `artist_id` INT NOT NULL,
  `location_id` INT NOT NULL,
  `location_area_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_performance_artist1_idx` (`artist_id` ASC),
  INDEX `fk_performance_location1_idx` (`location_id` ASC, `location_area_id` ASC),
  CONSTRAINT `fk_performance_artist1`
    FOREIGN KEY (`artist_id`)
    REFERENCES `ufo`.`artist` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_performance_location1`
    FOREIGN KEY (`location_id` , `location_area_id`)
    REFERENCES `ufo`.`location` (`id` , `area_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
