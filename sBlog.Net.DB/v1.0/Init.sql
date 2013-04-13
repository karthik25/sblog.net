-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Apr 13, 2013 at 06:48 PM
-- Server version: 5.5.27
-- PHP Version: 5.4.7

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `sblognet2`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE IF NOT EXISTS `categories` (
  `CategoryID` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `CategorySlug` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`CategoryID`, `CategoryName`, `CategorySlug`) VALUES
(1, 'General', 'general');

-- --------------------------------------------------------

--
-- Table structure for table `categorymapping`
--

CREATE TABLE IF NOT EXISTS `categorymapping` (
  `PostCategoryMappingID` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryID` int(11) NOT NULL,
  `PostID` int(11) NOT NULL,
  PRIMARY KEY (`PostCategoryMappingID`),
  KEY `CategoryID` (`CategoryID`),
  KEY `PostID` (`PostID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

--
-- Dumping data for table `categorymapping`
--

INSERT INTO `categorymapping` (`PostCategoryMappingID`, `CategoryID`, `PostID`) VALUES
(1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE IF NOT EXISTS `comments` (
  `CommentID` int(11) NOT NULL AUTO_INCREMENT,
  `CommentUserFullName` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `CommenterEmail` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `CommenterSite` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `CommentContent` varchar(512) COLLATE latin1_general_ci NOT NULL,
  `CommentPostedDate` datetime NOT NULL,
  `CommentStatus` int(11) NOT NULL,
  `PostID` int(11) NOT NULL,
  `UserID` int(11) DEFAULT NULL,
  PRIMARY KEY (`CommentID`),
  KEY `PostID` (`PostID`),
  KEY `UserID` (`UserID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

--
-- Dumping data for table `comments`
--

INSERT INTO `comments` (`CommentUserFullName`, `CommenterEmail`, `CommenterSite`, `CommentContent`, `CommentPostedDate`, `CommentStatus`, `PostID`, `UserID`) VALUES
('admin', NULL, NULL, 'Welcome to the blogosphere!', CURDATE(), 0, 1, NULL),
('admin', NULL, NULL, 'About Me!', CURDATE(), 0, 2, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `errors`
--

CREATE TABLE IF NOT EXISTS `errors` (
  `ErrorID` int(11) NOT NULL AUTO_INCREMENT,
  `ErrorDateTime` datetime NOT NULL,
  `ErrorMessage` varchar(500) COLLATE latin1_general_ci NOT NULL,
  `ErrorDescription` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`ErrorID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

-- --------------------------------------------------------

--
-- Table structure for table `posts`
--

CREATE TABLE IF NOT EXISTS `posts` (
  `PostID` int(11) NOT NULL AUTO_INCREMENT,
  `PostTitle` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `PostContent` text COLLATE latin1_general_ci NOT NULL,
  `PostUrl` text COLLATE latin1_general_ci NOT NULL,
  `PostAddedDate` datetime NOT NULL,
  `PostEditedDate` datetime DEFAULT NULL,
  `OwnerUserID` int(11) NOT NULL,
  `UserCanAddComments` bit(1) NOT NULL,
  `CanBeShared` bit(1) NOT NULL,
  `IsPrivate` bit(1) NOT NULL,
  `EntryType` tinyint(4) NOT NULL,
  `Order` int(11) DEFAULT NULL,
  PRIMARY KEY (`PostID`),
  KEY `OwnerUserID` (`OwnerUserID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

--
-- Dumping data for table `posts`
--

INSERT INTO `posts` (`PostID`, `PostTitle`, `PostContent`, `PostUrl`, `PostAddedDate`, `PostEditedDate`, `OwnerUserID`, `UserCanAddComments`, `CanBeShared`, `IsPrivate`, `EntryType`, `Order`) VALUES
(1, 'Hello World!', 'Hello World!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />', 'hello-world', '2013-04-11 00:00:00', NULL, 1, 1, 1, 0, 1, NULL),
(2, 'About', 'This is just a basic &quot;About&quot; page!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />', 'about', '2013-04-11 00:00:00', NULL, 1, 1, 1, 0, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `sblog_settings`
--

CREATE TABLE IF NOT EXISTS `sblog_settings` (
  `KeyName` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `KeyValue` text COLLATE latin1_general_ci,
  PRIMARY KEY (`KeyName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

--
-- Dumping data for table `sblog_settings`
--

INSERT INTO `sblog_settings` (`KeyName`, `KeyValue`) VALUES
('BlogAdminEmailAddress', NULL),
('BlogAkismetDeleteSpam', 'false'),
('BlogAkismetEnabled', 'false'),
('BlogAkismetKey', NULL),
('BlogAkismetUrl', NULL),
('BlogCaption', 'Just another sBlog.net blog!'),
('BlogDbVersion', '02_01'),
('BlogDisqusShortName', NULL),
('BlogName', 'sBlog.Net'),
('BlogPostsPerPage', '5'),
('BlogSiteErrorEmailAction', 'false'),
('BlogSmtpAddress', NULL),
('BlogSmtpPassword', NULL),
('BlogSocialSharing', 'false'),
('BlogSocialSharingChoice', NULL),
('BlogSyntaxHighlighting', 'false'),
('BlogSyntaxScripts', NULL),
('BlogSyntaxTheme', NULL),
('BlogTheme', NULL),
('DisqusEnabled', NULL),
('InstallationComplete', 'false'),
('ManageItemsPerPage', '5');

-- --------------------------------------------------------

--
-- Table structure for table `tagmapping`
--

CREATE TABLE IF NOT EXISTS `tagmapping` (
  `PostTagMappingID` int(11) NOT NULL AUTO_INCREMENT,
  `TagID` int(11) NOT NULL,
  `PostID` int(11) NOT NULL,
  PRIMARY KEY (`PostTagMappingID`),
  KEY `TagID` (`TagID`),
  KEY `PostID` (`PostID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

--
-- Dumping data for table `tagmapping`
--

INSERT INTO `tagmapping` (`PostTagMappingID`, `TagID`, `PostID`) VALUES
(1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tags`
--

CREATE TABLE IF NOT EXISTS `tags` (
  `TagID` int(11) NOT NULL AUTO_INCREMENT,
  `TagName` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `TagSlug` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`TagID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

--
-- Dumping data for table `tags`
--

INSERT INTO `tags` (`TagID`, `TagName`, `TagSlug`) VALUES
(1, 'General', 'general');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `Password` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `UserEmailAddress` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `UserDisplayName` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `UserActiveStatus` int(11) DEFAULT NULL,
  `ActivationKey` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `OneTimeToken` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `UserCode` varchar(128) COLLATE latin1_general_ci DEFAULT NULL,
  `UserSite` varchar(128) COLLATE latin1_general_ci DEFAULT NULL,
  `LastLoginDate` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `UserName`, `Password`, `UserEmailAddress`, `UserDisplayName`, `UserActiveStatus`, `ActivationKey`, `OneTimeToken`, `UserCode`, `UserSite`, `LastLoginDate`) VALUES
(1, 'admin', '', '', 'admin', 1, NULL, NULL, NULL, NULL, NULL);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `categorymapping`
--
ALTER TABLE `categorymapping`
  ADD CONSTRAINT `categorymapping_ibfk_1` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`),
  ADD CONSTRAINT `categorymapping_ibfk_2` FOREIGN KEY (`PostID`) REFERENCES `posts` (`PostID`);

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`PostID`) REFERENCES `posts` (`PostID`),
  ADD CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `posts`
--
ALTER TABLE `posts`
  ADD CONSTRAINT `posts_ibfk_1` FOREIGN KEY (`OwnerUserID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `tagmapping`
--
ALTER TABLE `tagmapping`
  ADD CONSTRAINT `tagmapping_ibfk_2` FOREIGN KEY (`PostID`) REFERENCES `posts` (`PostID`),
  ADD CONSTRAINT `tagmapping_ibfk_1` FOREIGN KEY (`TagID`) REFERENCES `tags` (`TagID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
