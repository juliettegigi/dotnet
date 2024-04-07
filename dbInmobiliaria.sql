drop database InmobiliariaDotNet;


create database InmobiliariaDotNet;

use InmobiliariaDotNet;



CREATE TABLE inquilinos(
    id INT PRIMARY KEY AUTO_INCREMENT,
    dni VARCHAR(10) unique,
    nombre VARCHAR(255),
    apellido VARCHAR(55),
    telefono VARCHAR(12),  -- miinmo 10 chars 
    email VARCHAR(255) UNIQUE,    
    domicilio VARCHAR(255) 
);
CREATE TABLE propietarios(
    id INT PRIMARY KEY AUTO_INCREMENT,
    dni VARCHAR(10) unique,
    nombre VARCHAR(255),
    apellido VARCHAR(55),
    telefono VARCHAR(12),  -- miinmo 10 chars 
    email VARCHAR(255) UNIQUE,
	domicilio VARCHAR(255) 
);

CREATE TABLE inmuebleTipos(
id INT PRIMARY KEY AUTO_INCREMENT,
tipo VARCHAR(255) 
);
insert into inmuebleTipos
values (0,"Local"),(0,"Depósito"),(0,"Casa"),(0,"Departamento");

CREATE TABLE inmuebles(
    id INT PRIMARY KEY AUTO_INCREMENT,
    propietarioId int,
    inmuebleTipoId int,
    direccion VARCHAR(255) ,
    cantidadAmbientes int,
    uso Enum('Comercial','Residencial'),
    precioBase decimal,
    cLatitud decimal(18, 15),
    CLongitud decimal(18, 15),
	suspendido boolean default 0,
    disponible boolean default 1,
    
    FOREIGN KEY(propietarioId) REFERENCES propietarios(id),
    FOREIGN KEY(inmuebleTipoId) REFERENCES inmuebleTipos(id)
);




CREATE TABLE contratos(
    id INT PRIMARY KEY AUTO_INCREMENT,
    inquilinoId int,
    inmuebleId int,
    fechaInicio datetime,
    fechaFin datetime, 
    fechaFinAnticipada datetime,
    precioXmes decimal,
    estado boolean DEFAULT true,
    
    FOREIGN KEY(inquilinoId) REFERENCES inquilinos(id),
    FOREIGN KEY(inmuebleId) REFERENCES inmuebles(id)
);

CREATE TABLE pagos(
    numeroPago INT ,
    contratoId int,
    fecha datetime, 
    importe decimal,
    primary key(numeroPago,contratoId),
    FOREIGN KEY(contratoId) REFERENCES contratos(id)
);


-- ///////////////////////////////////////////////////////////////////////////////    Inserts
insert into inquilinos
values (0,"dni1","nombre1","apellido1","telefono1","email@gmail.com1","domicilio1"),
       (0,"dni2","nombre2","apellido2","telefono2","email@gmail.com2","domicilio2"),
       (0,"dni3","nombre3","apellido3","telefono3","email@gmail.com3","domicilio3"),
       (0,"dni4","nombre4","apellido4","telefono4","email@gmail.com4","domicilio4"),
       (0,"dni5","nombre5","apellido5","telefono5","email@gmail.com5","domicilio5"),
       (0,"dni6","nombre6","apellido6","telefono6","email@gmail.com6","domicilio6"),
       (0,"dni7","nombre7","apellido7","telefono7","email@gmail.com7","domicilio7"),
       (0,"dni8","nombre8","apellido8","telefono8","email@gmail.com8","domicilio8"),
       (0,"dni9","nombre9","apellido9","telefono9","email@gmail.com9","domicilio9"),
       (0,"dni10","nombre10","apellido10","telefono10","email@gmail.com10","domicilio10");

insert into propietarios
values (1,"dni1","nombre1","apellido1","telefono1","email@gmail.com1","domicilio1"),
       (2,"dni2","nombre2","apellido2","telefono2","email@gmail.com2","domicilio2"),
       (3,"dni3","nombre3","apellido3","telefono3","email@gmail.com3","domicilio3"),
       (4,"dni4","nombre4","apellido4","telefono4","email@gmail.com4","domicilio4"),
       (5,"dni5","nombre5","apellido5","telefono5","email@gmail.com5","domicilio5"),
       (6,"dni6","nombre6","apellido6","telefono6","email@gmail.com6","domicilio6"),
       (7,"dni7","nombre7","apellido7","telefono7","email@gmail.com7","domicilio7"),
       (8,"dni8","nombre8","apellido8","telefono8","email@gmail.com8","domicilio8"),
       (9,"dni9","nombre9","apellido9","telefono9","email@gmail.com9","domicilio9"),
       (10,"dni10","nombre10","apellido10","telefono10","email@gmail.com10","domicilio10");

insert into inmuebles
values (0,1,4,"direccion1",1,"Residencial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,2,3,"direccion2",2,"Residencial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,3,2,"direccion3",3,"Comercial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,4,1,"direccion4",1,"Comercial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,5,4,"direccion1",1,"Residencial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,6,3,"direccion2",2,"Residencial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,7,2,"direccion3",3,"Comercial",200500,-33.28447561138461, -66.31062020299996,0,1),
        (0,8,1,"direccion4",1,"Comercial",200500,-33.28447561138461, -66.31062020299996,0,1);


insert into contratos
values 
(0,1,1,"2024-3-29","2025-3-29","1-1-1",300000,1),
(0,2,2,"2024-3-29","2025-3-29","1-1-1",300000,1),
(0,3,3,"2024-3-29","2025-3-29","1-1-1",300000,1),
(0,4,4,"2024-3-29","2024-4-28","1-1-1",300000,1);

insert into pagos
values 
(1,1,"2024-3-29",300000),
(2,1,"2024-4-29",300000),
(3,1,"2024-5-29",300000);


/* Listar todos los contratos que terminen en 30, 60 o 90 días
   (permitir elegir o especificar plazo).
*/
select *       
from contratos 
inner join inquilinos on contratos.inquilinoId=inquilinos.id
inner join (select * 
            from inmuebles 
            inner join Inmuebletipos on inmuebles.inmuebletipoId=inmuebletipos.id
            inner join propietarios on inmuebles.propietarioId=propietarios.id) 
on contratos.inmuebleId=inmuebles.id
where datediff(fechaFin,curdate())=30;

SELECT contratos.id as idddd,contratos.*       
FROM contratos 
INNER JOIN inquilinos 
ON contratos.inquilinoId = inquilinos.id

INNER JOIN (  SELECT * 
              FROM inmuebles 
              INNER JOIN Inmuebletipos ON inmuebles.inmuebletipoId = Inmuebletipos.id
              INNER JOIN propietarios ON inmuebles.propietarioId = propietarios.id
           ) AS inmuebleCompleto 
ON contratos.inmuebleId = inmuebleCompleto.id
WHERE DATEDIFF(contratos.fechaFin, CURDATE()) = 30;




/* Listar todos los contratos, incluyendo su inquilino, de un inmueble
   en particular.
*/

select contratos.id,fechaInicio,fechaFin,fechaFinAnticipada,precioXmes,estado,
       inquilinos.id as idInquilino,nombre,apellido,dni,telefono,email,domicilio
from contratos
inner join inquilinos on contratos.inquilinoId=inquilinos.id
where inmuebleId=1;
       
       
/*
Listar los pagos realizados para un contrato en particular
*/

select numeroPago,contratoID,fecha,importe
from pagos
where contratoId=1;


select contratos.id as idContrato, contratos.inquilinoId,contratos.inmuebleId,contratos.fechaInicio,contratos.fechaFin,contratos.fechaFinAnticipada,contratos.precioXmes,contratos.estado,
       inquilinos.id as idInquilino,inquilinos.dni as dniInq,inquilinos.nombre as nombreInq, inquilinos.apellido as apellidoInq,inquilinos.telefono as telefonoInq,inquilinos.email as emailInq,inquilinos.domicilio as domicilioInq,
	   inmuebleCompleto.idInmueble,inmuebleCompleto.propietarioId,inmuebleCompleto.inmuebleTipoId,inmuebleCompleto.direccion,inmuebleCompleto.cantidadAmbientes,inmuebleCompleto.uso,inmuebleCompleto.precioBase,inmuebleCompleto.cLatitud,inmuebleCompleto.cLongitud,inmuebleCompleto.suspendido, inmuebleCompleto.disponible,
       inmuebleCompleto.idInmuebleTipo,inmuebleCompleto.tipo,
       inmuebleCompleto.idpropietario,inmuebleCompleto.dni,inmuebleCompleto.nombre,inmuebleCompleto.apellido,inmuebleCompleto.telefono,inmuebleCompleto.email,inmuebleCompleto.domicilio
       from contratos 
	   inner join inquilinos on contratos.inquilinoId=inquilinos.id
       inner join (select  inmuebles.id as idInmueble,
						   inmuebles.propietarioId,
                                            inmuebles.inmuebleTipoId,
                                            inmuebles.direccion,
                                            inmuebles.cantidadAmbientes,
                                            inmuebles.uso,
                                            inmuebles.precioBase,
                                            inmuebles.cLatitud,
                                            inmuebles.cLongitud,
                                            inmuebles.suspendido,
                                            inmuebles.disponible,
                                            inmuebletipos.id as idInmuebleTipo,
                                            Inmuebletipos.tipo,
                                            propietarios.id as idPropietario,
                                            propietarios.dni,
                                            propietarios.nombre,
                                            propietarios.apellido,
                                            propietarios.telefono,
                                            propietarios.email,
                                            propietarios.domicilio
                                            
                                    from inmuebles 
                                    inner join Inmuebletipos on inmuebles.inmuebletipoId=inmuebletipos.id
                                    inner join propietarios on inmuebles.propietarioId=propietarios.id) AS inmuebleCompleto 
                               on contratos.inmuebleId=inmuebleCompleto.idInmueble
                        where datediff(fechaFin,curdate())=30;


SELECT 
                     inmuebles.id as idInmueble,
	                 inmuebles.propietarioId,
	                 inmuebles.inmuebleTipoId,
	                 inmuebles.direccion,
	                 inmuebles.cantidadAmbientes,
	                 inmuebles.uso,
	                 inmuebles.precioBase,
	                 inmuebles.cLatitud,
	                 inmuebles.cLongitud,
					 inmuebles.suspendido, 
					 inmuebles.disponible,
                     propietarios.id as idPropietario,
                  propietarios.dni,
                  propietarios.nombre,
                  propietarios.apellido,
                  propietarios.telefono,
                  propietarios.email,
                  propietarios.domicilio,
                  inmuebletipos.id as idInmuebleTipo,
                  inmuebletipos.tipo
FROM inmuebles
            INNER JOIN propietarios ON inmuebles.propietarioId = propietarios.id
            INNER JOIN inmuebleTipos ON inmuebles.inmuebleTipoId = inmuebleTipos.id
            limit 10 offset 1;
