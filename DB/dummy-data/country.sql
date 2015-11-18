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
-- Exportiere Daten aus Tabelle ufo.country: ~243 rows (ungefähr)
DELETE FROM `country`;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`id`, `name`) VALUES
	(1, 'Andorra'),
	(2, 'Vereinigte Arabische Emirate'),
	(3, 'Afghanistan'),
	(4, 'Antigua und Barbuda'),
	(5, 'Anguilla'),
	(6, 'Albanien'),
	(7, 'Armenien'),
	(8, 'Niederländische Antillen'),
	(9, 'Angola'),
	(10, 'Antarktis'),
	(11, 'Argentinien'),
	(12, 'Amerikanisch-Samoa'),
	(13, 'Österreich'),
	(14, 'Australien'),
	(15, 'Aruba'),
	(16, 'Åland'),
	(17, 'Aserbaidschan'),
	(18, 'Bosnien und Herzegowina'),
	(19, 'Barbados'),
	(20, 'Bangladesch'),
	(21, 'Belgien'),
	(22, 'Burkina Faso'),
	(23, 'Bulgarien'),
	(24, 'Bahrain'),
	(25, 'Burundi'),
	(26, 'Benin'),
	(27, 'Bermuda'),
	(28, 'Brunei Darussalam'),
	(29, 'Bolivien'),
	(30, 'Brasilien'),
	(31, 'Bahamas'),
	(32, 'Bhutan'),
	(33, 'Bouvetinsel'),
	(34, 'Botswana'),
	(35, 'Belarus (Weißrussland)'),
	(36, 'Belize'),
	(37, 'Kanada'),
	(38, 'Kokosinseln (Keelinginseln)'),
	(39, 'Kongo'),
	(40, 'Zentralafrikanische Republik'),
	(41, 'Republik Kongo'),
	(42, 'Schweiz'),
	(43, 'Elfenbeinküste'),
	(44, 'Cookinseln'),
	(45, 'Chile'),
	(46, 'Kamerun'),
	(47, 'China, Volksrepublik'),
	(48, 'Kolumbien'),
	(49, 'Costa Rica'),
	(50, 'Serbien und Montenegro'),
	(51, 'Kuba'),
	(52, 'Kap Verde'),
	(53, 'Weihnachtsinsel'),
	(54, 'Zypern'),
	(55, 'Tschechische Republik'),
	(56, 'Deutschland'),
	(57, 'Dschibuti'),
	(58, 'Dänemark'),
	(59, 'Dominica'),
	(60, 'Dominikanische Republik'),
	(61, 'Algerien'),
	(62, 'Ecuador'),
	(63, 'Estland (Reval)'),
	(64, 'Ägypten'),
	(65, 'Westsahara'),
	(66, 'Eritrea'),
	(67, 'Spanien'),
	(68, 'Äthiopien'),
	(69, 'Finnland'),
	(70, 'Fidschi'),
	(71, 'Falklandinseln (Malwinen)'),
	(72, 'Mikronesien'),
	(73, 'Färöer'),
	(74, 'Frankreich'),
	(75, 'Gabun'),
	(76, 'Großbritannien und Nordirland'),
	(77, 'Grenada'),
	(78, 'Georgien'),
	(79, 'Französisch-Guayana'),
	(80, 'Guernsey (Kanalinsel)'),
	(81, 'Ghana'),
	(82, 'Gibraltar'),
	(83, 'Grönland'),
	(84, 'Gambia'),
	(85, 'Guinea'),
	(86, 'Guadeloupe'),
	(87, 'Äquatorialguinea'),
	(88, 'Griechenland'),
	(89, 'Südgeorgien und die Südl. Sandwichinseln'),
	(90, 'Guatemala'),
	(91, 'Guam'),
	(92, 'Guinea-Bissau'),
	(93, 'Guyana'),
	(94, 'Hongkong'),
	(95, 'Heard- und McDonald-Inseln'),
	(96, 'Honduras'),
	(97, 'Kroatien'),
	(98, 'Haiti'),
	(99, 'Ungarn'),
	(100, 'Indonesien'),
	(101, 'Irland'),
	(102, 'Israel'),
	(103, 'Insel Man'),
	(104, 'Indien'),
	(105, 'Britisches Territorium im Indischen Ozean'),
	(106, 'Irak'),
	(107, 'Iran'),
	(108, 'Island'),
	(109, 'Italien'),
	(110, 'Jersey (Kanalinsel)'),
	(111, 'Jamaika'),
	(112, 'Jordanien'),
	(113, 'Japan'),
	(114, 'Kenia'),
	(115, 'Kirgisistan'),
	(116, 'Kambodscha'),
	(117, 'Kiribati'),
	(118, 'Komoren'),
	(119, 'St. Kitts und Nevis'),
	(120, 'Nordkorea'),
	(121, 'Südkorea'),
	(122, 'Kuwait'),
	(123, 'Kaimaninseln'),
	(124, 'Kasachstan'),
	(125, 'Laos'),
	(126, 'Libanon'),
	(127, 'St. Lucia'),
	(128, 'Liechtenstein'),
	(129, 'Sri Lanka'),
	(130, 'Liberia'),
	(131, 'Lesotho'),
	(132, 'Litauen'),
	(133, 'Luxemburg'),
	(134, 'Lettland'),
	(135, 'Libyen'),
	(136, 'Marokko'),
	(137, 'Monaco'),
	(138, 'Moldawien'),
	(139, 'Madagaskar'),
	(140, 'Marshallinseln'),
	(141, 'Mazedonien'),
	(142, 'Mali'),
	(143, 'Myanmar (Burma)'),
	(144, 'Mongolei'),
	(145, 'Macao'),
	(146, 'Nördliche Marianen'),
	(147, 'Martinique'),
	(148, 'Mauretanien'),
	(149, 'Montserrat'),
	(150, 'Malta'),
	(151, 'Mauritius'),
	(152, 'Malediven'),
	(153, 'Malawi'),
	(154, 'Mexiko'),
	(155, 'Malaysia'),
	(156, 'Mosambik'),
	(157, 'Namibia'),
	(158, 'Neukaledonien'),
	(159, 'Niger'),
	(160, 'Norfolkinsel'),
	(161, 'Nigeria'),
	(162, 'Nicaragua'),
	(163, 'Niederlande'),
	(164, 'Norwegen'),
	(165, 'Nepal'),
	(166, 'Nauru'),
	(167, 'Niue'),
	(168, 'Neuseeland'),
	(169, 'Oman'),
	(170, 'Panama'),
	(171, 'Peru'),
	(172, 'Französisch-Polynesien'),
	(173, 'Papua-Neuguinea'),
	(174, 'Philippinen'),
	(175, 'Pakistan'),
	(176, 'Polen'),
	(177, 'St. Pierre und Miquelon'),
	(178, 'Pitcairninseln'),
	(179, 'Puerto Rico'),
	(180, 'Palästinensische Autonomiegebiete'),
	(181, 'Portugal'),
	(182, 'Palau'),
	(183, 'Paraguay'),
	(184, 'Katar'),
	(185, 'Réunion'),
	(186, 'Rumänien'),
	(187, 'Russische Föderation'),
	(188, 'Ruanda'),
	(189, 'Saudi-Arabien'),
	(190, 'Salomonen'),
	(191, 'Seychellen'),
	(192, 'Sudan'),
	(193, 'Schweden'),
	(194, 'Singapur'),
	(195, 'St. Helena'),
	(196, 'Slowenien'),
	(197, 'Svalbard und Jan Mayen'),
	(198, 'Slowakei'),
	(199, 'Sierra Leone'),
	(200, 'San Marino'),
	(201, 'Senegal'),
	(202, 'Somalia'),
	(203, 'Suriname'),
	(204, 'São Tomé und Príncipe'),
	(205, 'El Salvador'),
	(206, 'Syrien'),
	(207, 'Swasiland'),
	(208, 'Turks- und Caicosinseln'),
	(209, 'Tschad'),
	(210, 'Französische Süd- und Antarktisgebiete'),
	(211, 'Togo'),
	(212, 'Thailand'),
	(213, 'Tadschikistan'),
	(214, 'Tokelau'),
	(215, 'Timor-Leste'),
	(216, 'Turkmenistan'),
	(217, 'Tunesien'),
	(218, 'Tonga'),
	(219, 'Türkei'),
	(220, 'Trinidad und Tobago'),
	(221, 'Tuvalu'),
	(222, 'Taiwan'),
	(223, 'Tansania'),
	(224, 'Ukraine'),
	(225, 'Uganda'),
	(226, 'Amerikanisch-Ozeanien'),
	(227, 'Vereinigte Staaten von Amerika'),
	(228, 'Uruguay'),
	(229, 'Usbekistan'),
	(230, 'Vatikanstadt'),
	(231, 'St. Vincent und die Grenadinen'),
	(232, 'Venezuela'),
	(233, 'Britische Jungferninseln'),
	(234, 'Amerikanische Jungferninseln'),
	(235, 'Vietnam'),
	(236, 'Vanuatu'),
	(237, 'Wallis und Futuna'),
	(238, 'Samoa'),
	(239, 'Jemen'),
	(240, 'Mayotte'),
	(241, 'Südafrika'),
	(242, 'Sambia'),
	(243, 'Simbabwe');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
