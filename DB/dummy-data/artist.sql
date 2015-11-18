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
-- Exportiere Daten aus Tabelle ufo.artist: ~60 rows (ungefähr)
DELETE FROM `artist`;
/*!40000 ALTER TABLE `artist` DISABLE KEYS */;
INSERT INTO `artist` (`id`, `name`, `image`, `video`, `category_id`, `country_id`, `email`) VALUES
	(1, 'circoPitanga', 'images/1.jpg', 'https://youtube.com/watch?v=bid2x95r4', 1, 42, 'circoPitanga@gmx.at'),
	(2, 'Compagnie Antipodes', 'images/2.jpg', 'https://youtube.com/watch?v=3sgnn0tr4', 1, 74, 'CompagnieAntipodes@gmx.at'),
	(3, 'Los Galindos', 'images/3.jpg', 'https://youtube.com/watch?v=2rb35bttg', 1, 67, 'LosGalindos@gmx.de'),
	(4, 'La Trocola Circo', 'images/4.jpg', NULL, 1, 11, 'LaTrocolaCirco@gmail.com'),
	(5, 'Cia. Jean Philippe Kikolas', 'images/5.jpg', 'https://youtube.com/watch?v=51entws0c', 1, 67, 'CiaJeanPhilippeKikolas@live.com'),
	(6, 'Hint', 'images/6.jpg', 'https://youtube.com/watch?v=i7alt1h3o', 1, 193, 'Hint@gmx.de'),
	(7, 'figurentheater (isipisi)', 'images/7.jpg', 'https://youtube.com/watch?v=uxxn3b0w7', 2, 13, 'figurentheater@gmx.at'),
	(8, 'Box Actor Yaya', 'images/8.jpg', 'https://youtube.com/watch?v=08vd1pcb7', 3, 113, 'BoxActorYaya@gmx.de'),
	(9, 'Compagnie MOBIL', 'images/9.jpg', 'https://youtube.com/watch?v=s4zbdntvn', 3, 163, 'CompagnieMOBIL@gmail.com'),
	(10, 'Derek Derek', 'images/10.jpg', 'https://youtube.com/watch?v=bh4wvd1us', 3, 227, 'DerekDerek@gmx.de'),
	(11, 'Die Buschs', 'images/11.jpg', 'https://youtube.com/watch?v=42nux0xid', 3, 56, 'DieBuschs@gmx.at'),
	(12, 'Dimitar and Tui (AiO Company)', 'images/12.jpg', 'https://youtube.com/watch?v=32reekctt', 3, 23, 'DimitarandTui@gmx.de'),
	(13, 'Dj Capuzzi & Senorita X', 'images/13.jpg', 'https://youtube.com/watch?v=buz5k6te6', 3, 11, 'DjCapuzzi&SenoritaX@gmx.de'),
	(14, 'Eddy Eighty', 'images/14.jpg', 'https://youtube.com/watch?v=ipx5ov6it', 3, 67, 'EddyEighty@gmx.de'),
	(15, 'El Diabolero', 'images/15.jpg', 'https://youtube.com/watch?v=9jvln5k6v', 3, 13, 'ElDiabolero@gmail.com'),
	(16, 'Herr Hundertpfund', 'images/16.jpg', 'https://youtube.com/watch?v=l4iz11rgp', 3, 56, 'HerrHundertpfund@live.com'),
	(17, 'Herr Konrad', 'images/17.jpg', 'https://youtube.com/watch?v=2vwn7ue7k', 3, 56, 'HerrKonrad@hotmail.com'),
	(18, 'HoopStep', 'images/18.jpg', 'https://youtube.com/watch?v=wl0xkm6r2', 3, 56, 'HoopStep@gmx.at'),
	(19, 'Ian Deadly', 'images/19.jpg', 'https://youtube.com/watch?v=rc5eegvrw', 3, 76, 'IanDeadly@gmx.de'),
	(20, 'Kammann', 'images/20.jpg', 'https://youtube.com/watch?v=00tr2vvf8', 3, 56, 'Kammann@gmail.com'),
	(21, 'Kana', 'images/21.jpg', 'https://youtube.com/watch?v=vkgc21o1v', 3, 113, 'Kana@gmx.de'),
	(22, 'Maurangas', 'images/22.jpg', 'https://youtube.com/watch?v=2dfsedemo', 3, 11, 'Maurangas@live.com'),
	(23, 'Mr. Mostacho', 'images/23.jpg', 'https://youtube.com/watch?v=7vv6jt8ik', 3, 45, 'MrMostacho@gmx.at'),
	(24, 'Anne & Mitja', 'images/24.jpg', 'https://youtube.com/watch?v=04arsbzvd', 4, 56, 'Anne&Mitja@gmx.at'),
	(25, 'Cia. ElOtro', 'images/25.jpg', 'https://youtube.com/watch?v=fv30lm26f', 4, 67, 'CiaElOtro@gmx.at'),
	(26, 'Cia. Passabarret', 'images/26.jpg', 'https://youtube.com/watch?v=c7rurxbcm', 4, 67, 'CiaPassabarret@gmx.de'),
	(27, 'Cia. ZagreB', 'images/27.jpg', 'https://youtube.com/watch?v=vhx5j2dh1', 4, 74, 'CiaZagreB@gmail.com'),
	(28, 'DANCEproject', 'images/28.jpg', 'https://youtube.com/watch?v=h1gx0zmva', 4, 13, 'DANCEproject@gmx.de'),
	(29, 'Duo Kate and Pasi', 'images/29.jpg', 'https://youtube.com/watch?v=lu3p2vvkx', 4, 69, 'DuoKateandPasi@gmail.com'),
	(30, 'Duo Looky', 'images/30.jpg', 'https://youtube.com/watch?v=oa6s4x7vg', 4, 102, 'DuoLooky@gmail.com'),
	(31, 'Duo Masawa', 'images/31.jpg', 'https://youtube.com/watch?v=edgvolm3c', 4, 109, 'DuoMasawa@gmail.com'),
	(32, 'Zirkus Gonzo', 'images/32.jpg', 'https://youtube.com/watch?v=4bzo9xm4f', 5, 58, 'ZirkusGonzo@gmx.de'),
	(33, 'Cia. Frutillas Con Crema', 'images/33.jpg', 'https://youtube.com/watch?v=j4pzkjvpa', 6, 67, 'CiaFrutillasConCrema@hotmail.com'),
	(34, 'Fausto Giori', 'images/34.jpg', 'https://youtube.com/watch?v=zph0a6s3v', 6, 109, 'FaustoGiori@gmail.com'),
	(35, 'Luca Bellezze', 'images/35.jpg', 'https://youtube.com/watch?v=r0h5za6t7', 6, 109, 'LucaBellezze@gmx.at'),
	(36, 'Mattress Circus', 'images/36.jpg', NULL, 6, 58, 'MattressCircus@gmx.de'),
	(37, 'Mute', 'images/37.jpg', 'https://youtube.com/watch?v=r3wzxihl8', 6, 212, 'Mute@gmail.com'),
	(38, 'Omnivolant', 'images/38.jpg', NULL, 6, 56, 'Omnivolant@gmx.de'),
	(39, 'Secret Circus', 'images/39.jpg', 'https://youtube.com/watch?v=2p5d21pax', 6, 30, 'SecretCircus@gmx.de'),
	(40, 'The Sideshow Charlatans', 'images/40.jpg', 'https://youtube.com/watch?v=lwe31mpkb', 6, 56, 'TheSideshowCharlatans@gmx.de'),
	(41, 'TON', 'images/41.jpg', 'https://youtube.com/watch?v=mwcuuggmj', 6, 163, 'TON@gmail.com'),
	(42, 'Trini-ty', 'images/42.jpg', 'https://youtube.com/watch?v=l17m82b6l', 6, 45, 'Trini-ty@gmx.at'),
	(43, 'Benni Green', 'images/43.jpg', 'https://youtube.com/watch?v=984nltz6r', 7, 109, 'BenniGreen@gmail.com'),
	(44, 'Les Contes dAsphalt', 'images/44.jpg', 'https://youtube.com/watch?v=x7xu2ljnd', 7, 21, 'LesContesdAsphalt@gmail.com'),
	(45, 'Lutrek Statues & Clowns', 'images/45.jpg', 'https://youtube.com/watch?v=modh2lihm', 7, 176, 'LutrekStatues&Clowns@gmail.com'),
	(46, 'OAKLEAF Stelzenkunst', 'images/46.jpg', NULL, 7, 56, 'OAKLEAFStelzenkunst@gmail.com'),
	(47, 'The LEDies', 'images/47.jpg', 'https://youtube.com/watch?v=6ud47fat0', 7, 13, 'TheLEDies@gmx.at'),
	(48, 'Urban Safari', 'images/48.jpg', 'https://youtube.com/watch?v=cdkf8nbbf', 7, 163, 'UrbanSafari@gmx.at'),
	(49, 'Barada Street', 'images/49.jpg', NULL, 8, 115, 'BaradaStreet@live.com'),
	(50, 'Blechsalat', 'images/50.jpg', 'https://youtube.com/watch?v=4vh0ev33', 8, 13, 'Blechsalat@gmail.com'),
	(51, 'Anta Agni', 'images/51.jpg', 'https://youtube.com/watch?v=wxobbfur0', 9, 198, 'AntaAgni@gmx.de'),
	(52, 'Lumen Invoco', 'images/52.jpg', 'https://youtube.com/watch?v=h3sda132p', 9, 109, 'LumenInvoco@gmx.de'),
	(53, 'Shining Shadows', 'images/53.jpg', NULL, 9, 13, 'ShiningShadows@gmail.com'),
	(54, 'Bigolis Teatre', 'images/54.jpg', NULL, 10, 67, 'BigolisTeatre@gmx.de'),
	(55, 'Ola Muchin', 'images/55.jpg', 'https://youtube.com/watch?v=exdv6gj2g', 10, 176, 'OlaMuchin@gmail.com'),
	(56, 'Bence Sarkadi Theater of Marionettes', 'images/56.jpg', 'https://youtube.com/watch?v=vmbfx4ixv', 10, 99, 'BenceSarkadiTheaterofMarionettes@gmail.com'),
	(57, 'Dr. Bubbles & die Seifenblansenbande', 'images/57.jpg', 'https://youtube.com/watch?v=bn38l29t0', 11, 56, 'DrBubbles&dieSeifenblansenbande@gmail.com'),
	(58, 'Puppenbühne Burattino-Koffertheater', 'images/58.jpg', 'https://youtube.com/watch?v=dkhglbh8i', 11, 56, 'PuppenbühneBurattino-Koffertheater@gmail.com'),
	(59, 'Kleines Grusel Gewusel', 'images/59.jpg', 'https://youtube.com/watch?v=jugfhx10p', 11, 13, 'KleinesGruselGewusel@gmail.com'),
	(60, 'Lucy Lou', 'images/60.jpg', NULL, 11, 56, 'LucyLou@gmail.com');
/*!40000 ALTER TABLE `artist` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
