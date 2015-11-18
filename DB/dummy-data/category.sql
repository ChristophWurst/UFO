-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.1.8-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             9.1.0.4867
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
-- Exportiere Daten aus Tabelle ufo.category: ~11 rows (ungefähr)
DELETE FROM `category`;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`id`, `description`) VALUES
	(1, 'Straßentheater'),
	(2, 'Local Art'),
	(3, 'Comedy & Jonglage'),
	(4, 'Akrobatik & Tanz'),
	(5, 'Luftakrobatik'),
	(6, 'Clownerie & Pantomime'),
	(7, 'Walkact, Stelzen, Stehstill'),
	(8, 'Musik, Samba, Precussion'),
	(9, 'Feuerperformances'),
	(10, 'Figuren- und Objekttheater'),
	(11, 'Kinderprogramm');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
