-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 05, 2020 at 06:17 PM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.3.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `management_auto_attendance_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `auto_id` int(11) NOT NULL,
  `employee_id` varchar(200) NOT NULL,
  `in_time` varchar(100) NOT NULL,
  `out_time` varchar(100) NOT NULL,
  `_date` varchar(100) NOT NULL,
  `face_recognition_entering` varchar(300) NOT NULL,
  `face_recognition_exiting` varchar(300) NOT NULL,
  `face_recognition_entering_img_path` varchar(300) NOT NULL,
  `face_recognition_exiting_img_path` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`auto_id`, `employee_id`, `in_time`, `out_time`, `_date`, `face_recognition_entering`, `face_recognition_exiting`, `face_recognition_entering_img_path`, `face_recognition_exiting_img_path`) VALUES
(20, '123', '01:35:52 AM', '12:22:03 PM', '18-08-2020', 'False', 'True', '18-Aug-2020_(01-35-52-290080)_Entering.jpg', ''),
(21, '1234', '08:08:07 PM', '10:43:14 AM', '24-08-2020', 'True', 'True', '', ''),
(22, '4567', '07:11:39 PM', '07:12:07 PM', '22-09-2020', 'True', 'True', '', ''),
(23, 'th2345', '09:04:35 PM', '', '29-09-2020', 'True', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `datasets`
--

CREATE TABLE `datasets` (
  `auto_id` int(11) NOT NULL,
  `employee_id` varchar(200) NOT NULL,
  `full_name` varchar(200) NOT NULL,
  `number_of_images` varchar(100) NOT NULL,
  `created_date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `datasets`
--

INSERT INTO `datasets` (`auto_id`, `employee_id`, `full_name`, `number_of_images`, `created_date`) VALUES
(1, '123', 'kuna rajkj', '15', '09-08-2020'),
(3, 'new', 'dksjadk jklj', '20', '10-08-2020'),
(4, 'kj', 'kljkljlk jklj', '15', '13-08-2020'),
(5, '1234', 'Thusy illanko', '15', '24-08-2020'),
(6, '4567', 'Sathu Sathu', '15', '22-09-2020'),
(7, 'th2345', 'Thulkifly Zuahir', '15', '29-09-2020');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `auto_id` int(11) NOT NULL,
  `employee_id` varchar(200) NOT NULL,
  `first_name` varchar(200) NOT NULL,
  `last_name` varchar(200) NOT NULL,
  `dob` varchar(100) NOT NULL,
  `gender` varchar(10) NOT NULL,
  `job_title` varchar(100) NOT NULL,
  `nic` varchar(100) NOT NULL,
  `phone_no` varchar(15) NOT NULL,
  `address` varchar(300) NOT NULL,
  `email_address` varchar(200) NOT NULL,
  `marital_status` varchar(20) NOT NULL,
  `photo_path` varchar(1000) NOT NULL,
  `is_dataset_available` varchar(15) NOT NULL,
  `is_model_available` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`auto_id`, `employee_id`, `first_name`, `last_name`, `dob`, `gender`, `job_title`, `nic`, `phone_no`, `address`, `email_address`, `marital_status`, `photo_path`, `is_dataset_available`, `is_model_available`) VALUES
(29, '123', 'kuna', 'rajkj', 'kljkljklj', 'Male', 'jkjlkjlk', 'jkljlkjl', 'jkljlkjlk', 'jkljkljkl', 'jkjl', 'Single', 'No_Image', 'True', 'True'),
(30, 'new', 'dksjadk', 'jklj', 'kljlkj', 'Male', 'jkjkl', ' jkjlkjkl', 'kljkljkljkljklj', 'jkkljkljkljkl', 'jkljkljlk', 'Single', 'No_Image', 'True', 'False'),
(31, 'jhjk', 'hjkhjk', 'hjkh', 'jkhjk', 'Male', 'hjkhjk', 'hjkhjk', 'hjkhjk', 'hjkh', 'jkhjkhjk', 'Single', 'No_Image', 'False', 'True'),
(32, 'kj', 'kljkljlk', 'jklj', 'kljlkj', 'Male', 'jjlkjkl', ' hjhjkhkj', 'jlkjlkjhjkhkjh', 'jkhjkhkjh', 'jkhjkhjk', 'Single', 'No_Image', 'True', 'False'),
(33, '1234', 'Thusy', 'illanko', '10/10/1099', 'Male', 'jdkasjl', 'jlkj', 'kljklj', 'lkjkl', 'jkljlk', 'Single', 'No_Image', 'True', 'True'),
(34, '4567', 'Sathu', 'Sathu', 'jsdfjkl', 'Male', 'jdksjkldjl', 'jkljkl', 'jklj', 'kljkl', 'jklj', 'Single', 'No_Image', 'True', 'True'),
(35, 'th2345', 'Thulkifly', 'Zuahir', 'dlsak', 'Male', ' klkl;k;l', ' klkl;k', 'k;l', 'kl;k;l', 'klklkl', 'Single', 'No_Image', 'True', 'True');

-- --------------------------------------------------------

--
-- Table structure for table `training`
--

CREATE TABLE `training` (
  `auto_id` int(11) NOT NULL,
  `employee_id` varchar(200) NOT NULL,
  `full_name` varchar(200) NOT NULL,
  `number_of_trained_images` varchar(200) NOT NULL,
  `trained_date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `training`
--

INSERT INTO `training` (`auto_id`, `employee_id`, `full_name`, `number_of_trained_images`, `trained_date`) VALUES
(3, 'jhjk', 'hjkhjk hjkh', '15', '10-08-2020'),
(4, '123', 'kuna rajkj', '15', '10-08-2020'),
(5, '1234', 'Thusy illanko', '15', '24-08-2020'),
(6, '4567', 'Sathu Sathu', '15', '22-09-2020'),
(7, 'th2345', 'Thulkifly Zuahir', '15', '29-09-2020');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `auto_id` int(11) NOT NULL,
  `username` varchar(200) NOT NULL,
  `password` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`auto_id`, `username`, `password`) VALUES
(1, 'admin', 'kuna123');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`auto_id`);

--
-- Indexes for table `datasets`
--
ALTER TABLE `datasets`
  ADD PRIMARY KEY (`auto_id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`auto_id`);

--
-- Indexes for table `training`
--
ALTER TABLE `training`
  ADD PRIMARY KEY (`auto_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`auto_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `datasets`
--
ALTER TABLE `datasets`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `training`
--
ALTER TABLE `training`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
