-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th10 01, 2019 lúc 04:14 AM
-- Phiên bản máy phục vụ: 10.1.37-MariaDB
-- Phiên bản PHP: 7.3.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `db_phi`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_3layer`
--

CREATE TABLE `tb_3layer` (
  `id` int(11) NOT NULL,
  `masv` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tensv` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `lop` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `diem` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_3layer`
--

INSERT INTO `tb_3layer` (`id`, `masv`, `tensv`, `lop`, `diem`) VALUES
(3, 'SV03', 'Minh', 'KTPM K15A', 7);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_checkid`
--

CREATE TABLE `tb_checkid` (
  `id` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_checkid`
--

INSERT INTO `tb_checkid` (`id`, `name`) VALUES
('1', 'Phi'),
('3', 'Tung'),
('4', 'Thuy');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_song`
--

CREATE TABLE `tb_song` (
  `song_id` int(10) NOT NULL,
  `song_name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `author` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `year` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_song`
--

INSERT INTO `tb_song` (`song_id`, `song_name`, `author`, `year`) VALUES
(8, 'mua', 'tung', '2000'),
(9, 'gio', 'thuy', '2017');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_sv`
--

CREATE TABLE `tb_sv` (
  `masv` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tensv` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `lop` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `diem` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_sv`
--

INSERT INTO `tb_sv` (`masv`, `tensv`, `lop`, `diem`) VALUES
('SV02', 'Tungtung', 'CNTT K16AAA', '8.5'),
('SV03', 'phi', 'CNTT', '4'),
('SV04', 'Thuy', 'HTTT QL K15A', '9'),
('SV05', 'Bac', 'QTVP K15A', '7'),
('SV07', 'Khanh', 'DTTT K15A CLC', '8');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tb_users`
--

CREATE TABLE `tb_users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `fullname` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `timecreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `tb_users`
--

INSERT INTO `tb_users` (`user_id`, `username`, `password`, `fullname`, `timecreated`) VALUES
(1, 'hoangphi2', 'hoangphi', 'Hoàng Phi', '2019-10-27 08:29:42');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `tb_3layer`
--
ALTER TABLE `tb_3layer`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `tb_checkid`
--
ALTER TABLE `tb_checkid`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `tb_song`
--
ALTER TABLE `tb_song`
  ADD PRIMARY KEY (`song_id`);

--
-- Chỉ mục cho bảng `tb_sv`
--
ALTER TABLE `tb_sv`
  ADD PRIMARY KEY (`masv`);

--
-- Chỉ mục cho bảng `tb_users`
--
ALTER TABLE `tb_users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `tb_3layer`
--
ALTER TABLE `tb_3layer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `tb_song`
--
ALTER TABLE `tb_song`
  MODIFY `song_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT cho bảng `tb_users`
--
ALTER TABLE `tb_users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
