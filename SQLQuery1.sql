--CREACION Y USO DE LA BD--
create database LibreriasReto;
use LibreriasReto;

--FORMATO DE FECHA--
set dateformat dmy

--CREACION DE TABLAS
create table area
(
	idArea int primary key not null identity(1,1),
	cargo varchar(45) not null,
	sueldo decimal(10,2) not null
);
go

create table genero
(
	idGenero int primary key not null identity (1,1),
	nombre varchar(30)
);
go

create table metodoPago
(
	idMetodoPago int primary key not null identity(1,1),
	nombre varchar(25)
);
go

create table cliente
(
	idCliente int primary key not null identity(1000,1),
	dni varchar(9) not null,
	nombre varchar(30) not null,
	apellido varchar(30) not null,
	-- Campos nulos --
	esActivo bit default 1
);
go



create table empleado
(
	idEmpleado int primary key not null identity(2000,1),
	idArea int references area(idArea),
	dni varchar(9) not null,
	nombre varchar(30) not null,
	apellido varchar(30) not null,
	telefono varchar(9) not null,
	fechaIngreso date not null,
	-- Campos nulos --
	email varchar(45) null,
	direccion varchar(40) null,
	esActivo bit default 1
);
go

create table acceso
(
	idAcceso int primary key not null identity (1,1),
	idEmpleado int not null references empleado, --DNI
	--SE AÑADIO LA COLUMNA DNI A LA TABLA ACCESO--
	dniEmpleado varchar(9) not null,
	clave varchar(150) not null --PASSWORD
);
go

create table libro
(
	idLibro int primary key not null identity(3000,1),
	idGenero int references genero,
	nombre varchar(50) not null,
	autor varchar(25) not null,
	editorial varchar(20) not null,
	precio int not null,
	anioPublicacion int not null,
	stock int default 0,
	--SE AGREGO LA COLUMNA URLIMAGEN--
	urlImagen varchar(550),
	esActivo bit default 1
);
go

create table recepcion
(
	idRecepcion int primary key not null identity(5000,1),
	idLibro int references libro not null,
	cantidad int not null,
	fechaIngreso date
);
go

create table comprobante
(
	idComprobante int primary key not null identity(7000,1),
	idCliente int references cliente not null,
	idEmpleado int references empleado not null,
	idMetodoPago int references metodoPago not null,
	total decimal(10,2),
	fechaVenta date default(getdate())
);
go

create table venta
(
	idVenta int primary key not null identity(6000,1),
	idlibro int references libro not null,
	cantidad int not null,
	precio decimal(10,2) not null,
	total decimal(10,2)
);
go

--INGRESO DE REGISTROS POR DEFAULT--
insert into area values
	('Limpieza', 1200),
	('Vendedor', 2200),
	('Almacenero', 1500),
	('Seguridad', 1700)
go

insert into genero values
	('Drama'),
	('Romance'),
	('Comedia'),
	('Terror'),
	('Suspenso'),
	('Aventura'),
	('Ficcion'),
	('Tecnología'),
	('Gastronomía')
go

insert into metodoPago values
	('Efectivo'),
	('Deposito'),
	('Transferencia'),
	('Banca Movil')
go

insert into cliente values
	('72557870', 'Diego', 'Doria Crisostomo', 1),
	('72555875', 'Luis', 'Zambrano Garcia', 1),
	('16930231', 'Junior', 'Torres Hinostroza', 1),
	('84309481', 'Nicole', 'Roman Vazques', 1),
	('28593001', 'Alfonso', 'Quiroes', 1),
	('32545346', 'Erica ', 'Doria Crisostomo', 1),
	('65871325', 'Jesus', 'Sideral Carrion', 1),
	('89005671', 'Andrez', 'Roman Vazques', 0),
	('28673451', 'Carlos', 'Rodriguez', 0),
	('56239814', 'Jesus', 'Fernandez', 0),
	('64009253', 'Carlos', 'Doria Crisostomo', 0),
	('13264578', 'Jesus', 'Fernandez', 0),
	('22356431', 'Enoc', 'Rodriguez', 0)
go

insert into empleado values
	(1, '16130376', 'Diego', 'nahui Rodriguez', '924221252', '13-02-2024', 'Diegon@gamil.com', 'Barrios Altos', 1),
	(2, '16130376', 'Edwin', 'Castillo Reto', '974201252', '13-02-2024', 'EdwinC@gamil.com', 'AA.HH Vecino cuida mi calamina', 1)
go

insert into libro values
	( 5, 'El dia que se perdio la cordura', 'Javier Castillo', 'Anagrama', 78, 2014, 0, 'https://images.cdn3.buscalibre.com/fit-in/360x360/0b/68/0b68a08e4322fb80fe61ba03c0bca729.jpg', 1),
	( 1, 'La pareja de al lado', 'Shari Lapena', 'Debolsillo', 59, 2021, 0, 'https://www.crisol.com.pe/media/catalog/product/cache/cf84e6047db2ba7f2d5c381080c69ffe/9/7/9788466342803_ulcekzvwusxazoqq.jpg', 1),
	( 7, 'Futuro azul', 'Eoin Colfer', 'Penguin Random', 19, 2009, 0, 'https://www.crisol.com.pe/media/catalog/product/cache/f6d2c62455a42b0d712f6c919e880845/f/u/futuro-azul_bxbb4ivqejrh1iin.jpg', 1),
	( 4, 'El silencio de la ciudad blanca', 'Eva Garcia Saenz', 'Editorial Planeta', 89, 2016, 0, 'https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/99665d90-f8e2-4345-b96c-10c318baf1b0/d_360_620/portada_el-silencio-de-la-ciudad-blanca_eva-garcia-saenz-de-urturi_201704051340.webp', 1),
	( 8, 'Domain Driver Desing', 'Eric Evans', 'Addison-Wesley', 223, 2003, 0, 'https://images.cdn3.buscalibre.com/fit-in/360x360/a5/f9/a5f924a164612f22d63ead8876217c7f.jpg', 1),
	( 7, 'Maldita Roma', 'Santiago Posteguillo', 'Ediciones B', 99, 2023, 0, 'https://images.cdn3.buscalibre.com/fit-in/360x360/a5/f9/a5f924a164612f22d63ead8876217c7f.jpg', 1),
	( 7, 'El Hobbit', 'Tolkien', 'Booket', 59, 2019, 0, 'https://www.crisol.com.pe/media/catalog/product/cache/cf84e6047db2ba7f2d5c381080c69ffe/9/7/9786124181450_zbcqesjs6lmmoapu.jpg', 1),
	( 9, 'Eleva tu juego culinario', 'Giacomo Bocchio', 'Planeta', 69, 2023, 0, 'https://www.crisol.com.pe/media/catalog/product/cache/cf84e6047db2ba7f2d5c381080c69ffe/9/7/9786123198695.jpg', 1),
	( 7, 'La sangre del padre', 'Alfonso Goizueta', 'Planeta', 88, 2023, 0, 'https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/43cf622c-4467-49ae-afcd-920a53ed2fed/d_360_620/portada_la-sangre-del-padre_alfonso-goizueta_202310200850.webp', 1),
	( 7, 'La sangre del padre', 'Alfonso Goizueta', 'Planeta', 88, 2023, 0, 'https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/43cf622c-4467-49ae-afcd-920a53ed2fed/d_360_620/portada_la-sangre-del-padre_alfonso-goizueta_202310200850.webp', 0),
	( 2, 'Las hijas de la criada', 'Sonsoles Ónega', 'Planeta', 98, 2023, 0, 'https://proassetspdlcom.cdnstatics2.com/usuaris/libros/thumbs/d6bee1af-fed3-44f7-87a3-1c0cff274749/d_360_620/portada_las-hijas-de-la-criada_sonsoles-onega_202310200852.webp', 1)
go

insert into acceso values
	(2001, '12345678', '32207a220eed3e0e041190bb17e623c6c38f8ab4d65986084d5bd473a7a8b0d2')
go


--SELECT A LAS TABLAS
select * from area;
select * from genero;
select * from metodoPago;
select * from cliente;
select * from empleado;
select * from libro;
select * from acceso;
select * from recepcion;

update libro set stock = 0, esActivo = 1
update cliente set esActivo = 1