-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 07-04-2024 a las 15:09:52
-- Versión del servidor: 8.0.30
-- Versión de PHP: 8.2.12

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
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `id` int NOT NULL,
  `inquilinoId` int DEFAULT NULL,
  `inmuebleId` int DEFAULT NULL,
  `fechaInicio` datetime DEFAULT NULL,
  `fechaFin` datetime DEFAULT NULL,
  `fechaFinAnticipada` datetime DEFAULT NULL,
  `precioXmes` decimal(10,0) DEFAULT NULL,
  `estado` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`id`, `inquilinoId`, `inmuebleId`, `fechaInicio`, `fechaFin`, `fechaFinAnticipada`, `precioXmes`, `estado`) VALUES
(1, 1, 1, '2024-03-29 00:00:00', '2025-03-29 00:00:00', '0001-01-01 00:00:00', 300000, 1),
(2, 2, 2, '2024-03-29 00:00:00', '2025-03-29 00:00:00', '0001-01-01 00:00:00', 300000, 1),
(3, 3, 3, '2024-03-29 00:00:00', '2025-03-29 00:00:00', '0001-01-01 00:00:00', 300000, 1),
(7, 3, 2, '2024-04-18 00:00:00', '2024-04-09 00:00:00', '2001-01-01 00:00:00', 656, 1),
(8, 6, 4, '2024-04-24 00:00:00', '2024-04-20 00:00:00', '2001-01-01 00:00:00', 656, 0),
(9, 3, 6, '2024-05-02 00:00:00', '2024-04-25 00:00:00', '0001-01-01 00:00:00', 5000000, 1),
(12, 4, 9, '2024-04-16 00:00:00', '2024-04-10 00:00:00', '0001-01-01 00:00:00', 656, 1),
(13, 4, 9, '2024-11-01 00:00:00', '2024-01-01 00:00:00', '0001-01-01 00:00:00', 656, 1),
(14, 4, 6, '2024-04-08 00:00:00', '2024-04-23 00:00:00', '0001-01-01 00:00:00', 5000000, 1),
(15, 3, 6, '2024-04-25 00:00:00', '2024-04-25 00:00:00', '0001-01-01 00:00:00', 88, 1),
(16, 5, 1, '2024-03-29 00:00:00', '2025-03-29 00:00:00', '0001-01-01 00:00:00', 5000, 1),
(17, 9, 1, '2024-03-29 00:00:00', '2025-03-29 00:00:00', '0001-01-01 00:00:00', 300000, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `id` int NOT NULL,
  `propietarioId` int DEFAULT NULL,
  `inmuebleTipoId` int DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `cantidadAmbientes` int DEFAULT NULL,
  `uso` enum('Comercial','Residencial') DEFAULT NULL,
  `precioBase` decimal(10,0) DEFAULT NULL,
  `cLatitud` decimal(18,15) DEFAULT NULL,
  `CLongitud` decimal(18,15) DEFAULT NULL,
  `suspendido` tinyint(1) DEFAULT '0',
  `disponible` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`id`, `propietarioId`, `inmuebleTipoId`, `direccion`, `cantidadAmbientes`, `uso`, `precioBase`, `cLatitud`, `CLongitud`, `suspendido`, `disponible`) VALUES
(1, 1, 4, 'direccion1', 1, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(2, 2, 3, 'direccion2', 2, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(3, 3, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(4, 4, 1, 'direccion4', 1, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(5, 5, 4, 'direccion1', 1, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(6, 6, 3, 'direccion2', 2, 'Residencial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(7, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(8, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(9, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(10, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(11, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(12, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(13, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(14, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(15, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(16, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(17, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(18, 7, 2, 'direccion3', 3, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(19, 8, 1, 'direccion4', 1, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebletipos`
--

CREATE TABLE `inmuebletipos` (
  `id` int NOT NULL,
  `tipo` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
  `id` int NOT NULL,
  `dni` varchar(10) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `telefono` varchar(12) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
(10, 'dni10', 'nombre10', 'apellido10', 'telefono10', 'email@gmail.com10', 'domicilio10');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `numeroPago` int NOT NULL,
  `contratoId` int NOT NULL,
  `fecha` datetime DEFAULT NULL,
  `importe` decimal(10,0) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`numeroPago`, `contratoId`, `fecha`, `importe`) VALUES
(1, 1, '2024-03-29 00:00:00', 300000),
(2, 1, '2024-04-29 00:00:00', 300000),
(3, 1, '2024-05-29 00:00:00', 300000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `id` int NOT NULL,
  `dni` varchar(10) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `telefono` varchar(12) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
(10, 'dni10', 'nombre10', 'apellido10', 'telefono10', 'email@gmail.com10', 'domicilio10'),
(11, 'pop', 'porr', 'peep', '45356', 'sss@gmail.com', NULL);

--
-- Índices para tablas volcadas
--

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
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `inmuebletipos`
--
ALTER TABLE `inmuebletipos`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Restricciones para tablas volcadas
--

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
