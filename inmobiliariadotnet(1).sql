-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 25-04-2024 a las 17:08:15
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
-- Estructura de tabla para la tabla `auditoriacontrato`
--

CREATE TABLE `auditoriacontrato` (
  `id` int NOT NULL,
  `id_usuario` int NOT NULL,
  `fechaInicio` datetime DEFAULT NULL,
  `fechacancelacion` datetime DEFAULT NULL,
  `id_contrato` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `auditoriacontrato`
--

INSERT INTO `auditoriacontrato` (`id`, `id_usuario`, `fechaInicio`, `fechacancelacion`, `id_contrato`) VALUES
(1, 6, '2024-04-25 10:31:06', NULL, 1),
(2, 6, '2024-04-25 11:23:02', NULL, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoriapagos`
--

CREATE TABLE `auditoriapagos` (
  `id` int NOT NULL,
  `id_usuario` int NOT NULL,
  `id_contrato` int NOT NULL,
  `fechaPago` datetime DEFAULT NULL,
  `fechaCancelacion` datetime DEFAULT NULL,
  `numero_pago` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `auditoriapagos`
--

INSERT INTO `auditoriapagos` (`id`, `id_usuario`, `id_contrato`, `fechaPago`, `fechaCancelacion`, `numero_pago`) VALUES
(1, 6, 1, '2024-04-25 10:31:50', NULL, 1);

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
(1, 3, 3, '2024-05-11 00:00:00', '2024-05-25 00:00:00', '0001-01-01 00:00:00', 8, 1),
(2, 8, 5, '2024-04-27 00:00:00', '2024-05-03 00:00:00', '0001-01-01 00:00:00', 500000, 1);

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
(8, 8, 1, 'direccion4', 1, 'Comercial', 200500, -33.284475611384610, -66.310620202999960, 0, 1),
(9, 11, 2, 'direccion11', 0, 'Comercial', 20000, -33.291386952509846, -66.318935931091320, 0, 1);

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
  `fechaPago` datetime DEFAULT NULL,
  `importe` decimal(10,0) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`numeroPago`, `contratoId`, `fecha`, `fechaPago`, `importe`) VALUES
(1, 1, '2024-05-03 00:00:00', '2024-04-25 10:31:50', 500000),
(1, 2, '2024-04-27 00:00:00', NULL, 500000);

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
(11, 'dni11', 'nombre11', 'apellido11', 'telefono11', 'email@gmail.com11', 'domicilio11'),
(17, 'dni20', 'NOMBRE20', 'APELLIDO20', '02664877098', 'email@gmail.com20', 'gt');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(55) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `pass` varchar(255) DEFAULT NULL,
  `avatar` varchar(255) DEFAULT NULL,
  `avatarFile` varchar(255) DEFAULT NULL,
  `rol` int DEFAULT NULL,
  `domicilio` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `nombre`, `apellido`, `email`, `pass`, `avatar`, `avatarFile`, `rol`, `domicilio`) VALUES
(1, 'julia', 'gutierrez', 'mar9ina@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_1.jpg', NULL, 2, NULL),
(2, 'kk', 'kk', 'kk@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_2.jpg', NULL, 1, NULL),
(3, 'lautaro', 'ferr', 'lauti@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_3.jpg', NULL, 1, NULL),
(4, 'toreto', 'juju', 'tori@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_4.jpg', NULL, 2, NULL),
(5, 'carla', 'tat', 'carla@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_5.png', NULL, 1, NULL),
(6, 'karina', 'milei', 'kari@gmail.com', 'KDa9xkd7uC5ppAF9LVVP4wOEmmDb96jKjB8L2mDPlgA=', '/Uploads\\avatar_6.jpg', NULL, 2, NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoriacontrato`
--
ALTER TABLE `auditoriacontrato`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_usuario` (`id_usuario`),
  ADD KEY `id_contrato` (`id_contrato`);

--
-- Indices de la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_contrato` (`id_contrato`),
  ADD KEY `id_usuario` (`id_usuario`);

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
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

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
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `auditoriacontrato`
--
ALTER TABLE `auditoriacontrato`
  ADD CONSTRAINT `auditoriacontrato_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`),
  ADD CONSTRAINT `auditoriacontrato_ibfk_2` FOREIGN KEY (`id_contrato`) REFERENCES `contratos` (`id`);

--
-- Filtros para la tabla `auditoriapagos`
--
ALTER TABLE `auditoriapagos`
  ADD CONSTRAINT `auditoriapagos_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contratos` (`id`),
  ADD CONSTRAINT `auditoriapagos_ibfk_2` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`);

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
