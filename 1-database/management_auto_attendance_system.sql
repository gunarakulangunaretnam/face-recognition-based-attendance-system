-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 04, 2022 at 09:35 AM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
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
(33, '0001', '02:44:42 PM', '', '03-03-2022', 'True', '', '', '');

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
(8, '0001', 'Gunarakulan Gunaretnam', '15', '03-03-2022');

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
(36, '0001', 'Gunarakulan', 'Gunaretnam', '1997-01-11', 'Male', 'Software Developer', '970110720V', '12435789', '345768', 'e43257689', 'Single', 'No_Image', 'True', 'True'),
(37, '0002', 'David', 'Mike', '123456', 'Female', 'dsadsad', '3245476453', '456543542132', '56543542132', '35241435', 'Single', 'No_Image', 'False', 'False');

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
(8, '0001', 'Gunarakulan Gunaretnam', '15', '03-03-2022');

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
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT for table `datasets`
--
ALTER TABLE `datasets`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `training`
--
ALTER TABLE `training`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `auto_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
