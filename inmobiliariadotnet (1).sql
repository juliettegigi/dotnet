-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-04-2024 a las 23:54:05
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliariadotnet`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoriacontrato`
--

CREATE TABLE `auditoriacontrato` (
  `id` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `fechaInicio` datetime DEFAULT NULL,
  `fechacancelacion` datetime DEFAULT NULL,
  `id_contrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auditoriacontrato`
--

INSERT INTO `auditoriacontrato` (`id`, `id_usuario`, `fechaInicio`, `fechacancelacion`, `id_contrato`) VALUES
(2, 5, '2024-04-24 17:04:32', NULL, 21),
(3, 5, NULL, '2024-04-24 17:07:33', 21);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoriapagos`
--

CREATE TABLE `auditoriapagos` (
  `id` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `fechaPago` datetime DEFAULT NULL,
  `fechaCancelacion` datetime DEFAULT NULL,
  `numero_pago` int(11) NOT NULL,
  `id_contrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auditoriapagos`
--

INSERT INTO `auditoriapagos` (`id`, `id_usuario`, `fechaPago`, `fechaCancelacion`, `numero_pago`, `id_contrato`) VALUES
(4, 5, '2024-04-24 18:44:52', NULL, 1, 21),
(5, 5, '2024-04-24 18:45:22', NULL, 2, 21),
(6, 5, NULL, '2024-04-24 18:52:44', 2, 21);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `id` int(11) NOT NULL,
  `inquilinoId` int(11) DEFAULT NULL,
  `inmuebleId` int(11) DEFAULT NULL,
  `fechaInicio` datetime DEFAULT NULL,
  `fechaFin` datetime DEFAULT NULL,
  `fechaFinAnticipada` datetime DEFAULT NULL,
  `precioXmes` decimal(10,0) DEFAULT NULL,
  `estado` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`id`, `inquilinoId`, `inmuebleId`, `fechaInicio`, `fechaFin`, `fechaFinAnticipada`, `precioXmes`, `estado`) VALUES
(21, 11, 1, '2024-04-24 00:00:00', '2025-03-24 00:00:00', '2024-04-24 17:07:33', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `id` int(11) NOT NULL,
  `propietarioId` int(11) DEFAULT NULL,
  `inmuebleTipoId` int(11) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `cantidadAmbientes` int(11) DEFAULT NULL,
  `uso` enum('Comercial','Residencial') DEFAULT NULL,
  `precioBase` decimal(10,0) DEFAULT NULL,
  `cLatitud` decimal(18,15) DEFAULT NULL,
  `CLongitud` decimal(18,15) DEFAULT NULL,
  `suspendido` tinyint(1) DEFAULT 0,
  `disponible` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`id`, `propietarioId`, `inmuebleTipoId`, `direccion`, `cantidadAmbientes`, `uso`, `precioBase`, `cLatitud`, `CLongitud`, `suspendido`, `disponible`) VALUES
(1, 1, 4, 'direccion1', 1, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(2, 2, 2, 'direccionzzz2', 2, 'Residencial', 222222, -58.443194900000000, -34.595614500000000, 0, 1),
(3, 3, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(4, 4, 1, 'direccion4', 1, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(5, 5, 4, 'direccion1', 1, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(6, 6, 3, 'direccion2', 2, 'Residencial', 1, -58.443194900000000, -34.595614500000000, 0, 1),
(7, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(8, 8, 1, 'direccion4', 1, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(9, 1, 2, 'juana', 1, 'Residencial', 10, -33.299601368773700, -66.319150507812510, 0, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebletipos`
--

CREATE TABLE `inmuebletipos` (
  `id` int(11) NOT NULL,
  `tipo` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `inmuebletipos`
--

INSERT INTO `inmuebletipos` (`id`, `tipo`) VALUES
(1, 'Local'),
(2, 'Depósito'),
(3, 'Casa'),
(4, 'Departamento');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `id` int(11) NOT NULL,
  `dni` varchar(10) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `telefono` varchar(12) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`id`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `domicilio`) VALUES
(1, 'dni1', 'nombre1', 'apellido1', 'telefono1', 'email@gmail.com1', 'domicilio1'),
(2, 'dni2', 'nombre2', 'apellido2', 'telefono2', 'email@gmail.com2', 'domicilio2'),
(3, 'dni3', 'nombre3', 'apellido3', 'telefono3', 'email@gmail.com3', 'domicilio3'),
(4, 'dni4', 'nombre4', 'apellido4', 'telefono4', 'email@gmail.com4', 'domicilio4'),
(5, 'dni5', 'nombre5', 'apellido5', 'telefono5', 'email@gmail.com5', 'domicilio5'),
(6, 'dni6', 'nombre6', 'apellido6', 'telefono6', 'email@gmail.com6', 'domicilio6'),
(7, 'dni7', 'nombre7', 'apellido7', 'telefono7', 'email@gmail.com7', 'domicilio7'),
(8, 'dni8', 'nombre8', 'apellido8', 'telefono8', 'email@gmail.com8', 'domicilio8'),
(9, 'dni9', 'nombre9', 'apellido9', 'telefono9', 'email@gmail.com9', 'domicilio9'),
(10, 'dni10', 'nombre10', 'apellido10', 'telefono10', 'email@gmail.com10', 'domicilio10'),
(11, '21100', 'Ru', 'Villalobos', '02664', 'crinoo@hotmail.com', 'domicilio23@hotmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `numeroPago` int(11) NOT NULL,
  `contratoId` int(11) NOT NULL,
  `fecha` datetime DEFAULT NULL,
  `fechaPago` datetime DEFAULT NULL,
  `importe` decimal(10,0) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`numeroPago`, `contratoId`, `fecha`, `fechaPago`, `importe`) VALUES
(1, 21, '2024-04-24 00:00:00', '2024-04-24 18:44:52', 1),
(2, 21, '2024-05-24 00:00:00', NULL, 1),
(3, 21, '2024-06-24 00:00:00', NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `id` int(11) NOT NULL,
  `dni` varchar(10) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `telefono` varchar(12) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`id`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `domicilio`) VALUES
(1, 'dni1', 'nombre1', 'apellido1', 'telefono1', 'email@gmail.com1', 'domicilio1'),
(2, 'dni2', 'nombre2', 'apellido2', 'telefono2', 'email@gmail.com2', 'domicilio2'),
(3, 'dni3', 'nombre3', 'apellido3', 'telefono3', 'email@gmail.com3', 'domicilio3'),
(4, 'dni4', 'nombre4', 'apellido4', 'telefono4', 'email@gmail.com4', 'domicilio4'),
(5, 'dni5', 'nombre5', 'apellido5', 'telefono5', 'email@gmail.com5', 'domicilio5'),
(6, 'dni6', 'nombre6', 'apellido6', 'telefono6', 'email@gmail.com6', 'domicilio6'),
(7, 'dni7', 'nombre7', 'apellido7', 'telefono7', 'email@gmail.com7', 'domicilio7'),
(8, 'dni8', 'nombre8', 'apellido8', 'telefono8', 'email@gmail.com8', 'domicilio8'),
(9, 'dni9', 'nombre9', 'apellido9', 'telefono9', 'email@gmail.com9', 'domicilio9'),
(10, 'dni10', 'nombre10', 'apellido10', 'telefono10', 'email@gmail.com10', 'domicilio10');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `pass` varchar(255) DEFAULT NULL,
  `avatar` varchar(255) DEFAULT NULL,
  `avatarFile` varchar(255) DEFAULT NULL,
  `rol` int(11) DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `nombre`, `apellido`, `email`, `pass`, `avatar`, `avatarFile`, `rol`, `domicilio`) VALUES
(1, 'julia', 'gutierrez', 'mar9ina@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_1.jpg', NULL, 2, NULL),
(2, 'kk', 'kk', 'kk@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_2.jpg', NULL, 1, NULL),
(3, 'lautaro', 'ferr', 'lauti@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_3.jpg', NULL, 1, NULL),
(4, 'toreto', 'juju', 'tori@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_4.jpg', NULL, 2, NULL),
(5, 'Ruben', 'Villalobos', 'cristian_villalobos2004@hotmail.com', 'kD4n+zg/2Ih2Ky5BfHihWzqVclHZH59m1o0lY+W5izg=', NULL, NULL, 1, NULL),
(6, 'facu', 'vidia', 'facu@hot.com', 'ixT8q7B3t1NbFvxeYIsgaKq/D4CTL7+g79pI0bzaVgM=', NULL, NULL, 1, NULL),
(7, 'patito', 'patito', 'patito@hotmail.com', '3N9vNDz1CcL5AJEsxirL0nCEjFdRqvDr3rPocoxdTGE=', NULL, NULL, 1, NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoriacontrato`
--
ALTER TABLE `auditoriacontrato`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_usuario_id` (`id_usuario`),
  ADD KEY `fk_contrato_id` (`id_contrato`);

--
-- Indices de la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kf_usuario_Id` (`id_usuario`),
  ADD KEY `kf_numero_Pago` (`numero_pago`),
  ADD KEY `kf_contrato_id` (`id_contrato`);

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `inquilinoId` (`inquilinoId`),
  ADD KEY `inmuebleId` (`inmuebleId`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`id`),
  ADD KEY `propietarioId` (`propietarioId`),
  ADD KEY `inmuebleTipoId` (`inmuebleTipoId`);

--
-- Indices de la tabla `inmuebletipos`
--
ALTER TABLE `inmuebletipos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `dni` (`dni`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`numeroPago`,`contratoId`),
  ADD KEY `contratoId` (`contratoId`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `dni` (`dni`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `auditoriacontrato`
--
ALTER TABLE `auditoriacontrato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inmuebletipos`
--
ALTER TABLE `inmuebletipos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `auditoriacontrato`
--
ALTER TABLE `auditoriacontrato`
  ADD CONSTRAINT `fk_contrato_id` FOREIGN KEY (`id_contrato`) REFERENCES `contratos` (`id`),
  ADD CONSTRAINT `fk_usuario_id` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`);

--
-- Filtros para la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  ADD CONSTRAINT `kf_contrato_id` FOREIGN KEY (`id_contrato`) REFERENCES `contratos` (`id`),
  ADD CONSTRAINT `kf_numero_Pago` FOREIGN KEY (`numero_pago`) REFERENCES `pagos` (`numeroPago`),
  ADD CONSTRAINT `kf_usuario_Id` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`);

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`inquilinoId`) REFERENCES `inquilinos` (`id`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`inmuebleId`) REFERENCES `inmuebles` (`id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`propietarioId`) REFERENCES `propietarios` (`id`),
  ADD CONSTRAINT `inmuebles_ibfk_2` FOREIGN KEY (`inmuebleTipoId`) REFERENCES `inmuebletipos` (`id`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`contratoId`) REFERENCES `contratos` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;