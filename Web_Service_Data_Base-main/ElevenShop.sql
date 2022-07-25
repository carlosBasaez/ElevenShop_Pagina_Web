/*drops*/
DROP TABLE Pago;
DROP TABLE Pedido;
DROP TABLE Producto_cantidad;
DROP TABLE Carrito_compra;
DROP TABLE Producto;
DROP TABLE Estado;
DROP TABLE Despachador;
DROP TABLE Bodeguero;
DROP TABLE Administrador;
DROP TABLE Cliente;
DROP TABLE Comuna;
DROP TABLE Ciudad;
DROP TABLE PassToken;
DROP TABLE Usuario;

/*creacion de tablas*/
CREATE TABLE Usuario (
NombreCompleto VARCHAR2 (100) NOT NULL,
CorreoElectronico VARCHAR2 (80) NOT NULL,
Rut VARCHAR2 (10) NOT NULL PRIMARY KEY,
Contrasena VARCHAR2 (30) NOT NULL,
Telefono NUMBER (10) null 
);

CREATE TABLE PassToken(
Rut VARCHAR2 (10) NOT NULL PRIMARY KEY,
Token VARCHAR (6) NOT NULL,
CONSTRAINT fk_pass_token_usuario FOREIGN KEY (Rut) REFERENCES Usuario(Rut)
);

CREATE TABLE Ciudad(
IdCiudad NUMBER (3) NOT NULL PRIMARY KEY,
NombreCiudad VARCHAR2 (30) NOT NULL
);

CREATE TABLE Comuna(
IdComuna NUMBER (3) NOT NULL PRIMARY KEY,
IdCiudad NUMBER (3) NOT NULL,
NombreComuna VARCHAR2 (30) NOT NULL,
CONSTRAINT fk_ciudad_comuna FOREIGN KEY (IdCiudad) REFERENCES Ciudad(IdCiudad)
);

CREATE TABLE Cliente(
RutCliente VARCHAR2 (10) NOT NULL PRIMARY KEY,
Direccion VARCHAR2 (50) NOT NULL,
Ciudad VARCHAR2 (40) NOT NULL,
Comuna NUMBER (3) NOT NULL,
CONSTRAINT fk_usuario_cliente FOREIGN KEY (RutCliente) REFERENCES Usuario(Rut),
CONSTRAINT fk_usuario_comuna FOREIGN KEY (Comuna) REFERENCES Comuna(IdComuna)
);

CREATE TABLE Administrador(
RutAdmin VARCHAR2 (10) NOT NULL PRIMARY KEY,
CONSTRAINT fk_admin_usuario FOREIGN KEY (RutAdmin) REFERENCES Usuario(Rut)
);

CREATE TABLE Bodeguero(
RutBodeguero VARCHAR2 (10) NOT NULL PRIMARY KEY,
CONSTRAINT fk_Bodeguero_usuario FOREIGN KEY (RutBodeguero) REFERENCES Usuario(Rut)
);

CREATE TABLE Despachador(
RutDespachador VARCHAR2 (10) NOT NULL PRIMARY KEY,
CONSTRAINT fk_despachador_usuario FOREIGN KEY (RutDespachador) REFERENCES Usuario(Rut)
);

CREATE TABLE Estado(
IdEstado NUMBER (3) NOT NULL PRIMARY KEY,
NombreEstado VARCHAR2 (20) NOT NULL
);

CREATE TABLE Producto(
IdProducto NUMBER (3) NOT NULL PRIMARY KEY,
Nombre VARCHAR2 (40) NOT NULL,
Reserva NUMBER (3) NOT NULL,
Stock NUMBER (4) NOT NULL,
Valor NUMBER (10) NOT NULL,
Caracteristicas VARCHAR2 (100) NOT NULL
);


CREATE TABLE Carrito_compra(
IdCarrito NUMBER (3) NOT NULL PRIMARY KEY,
ValorTotal NUMBER (8) NOT NULL
);

CREATE TABLE Producto_Cantidad(
IdCarrito NUMBER (3) NOT NULL,
IdProducto NUMBER (3) NOT NULL,
Cantidad NUMBER (5) NOT NULL,
CONSTRAINT fk_producto FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto),
CONSTRAINT fk_carrito FOREIGN KEY (IdCarrito) REFERENCES Carrito_Compra (IdCarrito),
CONSTRAINT producto_pk PRIMARY KEY (IdCarrito, IdProducto)
);


CREATE TABLE Pedido(
IdPedido NUMBER (3) NOT NULL PRIMARY KEY,
IdCarrito NUMBER (3) NOT NULL,
Fecha TIMESTAMP NOT NULL,
RutCliente VARCHAR2 (10) NOT NULL,
RutBodeguero VARCHAR2 (10) NULL,
RutDespachador VARCHAR2 (10) NULL,
IdEstado NUMBER (3) NOT NULL,
CONSTRAINT fk_pedido_carrito FOREIGN KEY (IdCarrito) REFERENCES Carrito_compra (IdCarrito),
CONSTRAINT fk_cliente_pedido FOREIGN KEY (RutCliente) REFERENCES Cliente (RutCliente),
CONSTRAINT fk_bodeguero_pedido FOREIGN KEY (RutBodeguero) REFERENCES Bodeguero (RutBodeguero),
CONSTRAINT fk_despachador_pedido FOREIGN KEY (RutDespachador) REFERENCES Despachador (RutDespachador),
CONSTRAINT fk_estado_pedido FOREIGN KEY (IdEstado) REFERENCES Estado (IdEstado)
);

CREATE TABLE Pago(
IdPedido NUMBER (3) NOT NULL PRIMARY KEY,
token VARCHAR2 (100) NOT NULL,
CONSTRAINT fk_idPedido_Pago FOREIGN KEY (IdPedido) REFERENCES Pedido (IdPedido)
);

/*poblamiento basico*/
INSERT INTO Ciudad VALUES (1, 'Santiago');
INSERT INTO Ciudad VALUES (2, 'Concepcion');
INSERT INTO Ciudad VALUES (3, 'Valparaiso');

INSERT INTO Comuna VALUES (10, 1, 'La Florida');
INSERT INTO Comuna VALUES (20, 2, 'San Pedro');
INSERT INTO Comuna VALUES (30, 3, 'Viña del Mar');


--INSERT INTO Usuario VALUES ('Carlos Basaez', 'zcar@gmail.com', '20698546-4', 'c?>I]SW?z%???7?8GWzD????j', 978541235);
INSERT INTO Usuario VALUES ('Sebastian Groselj', 'se.groselj@duocuc.cl', '19475329-2', '???#?4:?@?,??)m?C%?i?4', 987453214);

--INSERT INTO Administrador VALUES ('20698546-4'); --clave 1234
INSERT INTO Administrador VALUES ('19475329-2'); --clave 1234


INSERT INTO Estado VALUES(0, 'Reservado');
INSERT INTO Estado VALUES(1, 'Pagado');
INSERT INTO Estado VALUES(2, 'En Bodega');
INSERT INTO Estado VALUES(3, 'En Despacho');
INSERT INTO Estado VALUES(4, 'Entregado');
INSERT INTO Estado VALUES(5, 'Cancelado');

INSERT INTO Producto VALUES (1, 'Ryzen 5 5600X', 0, 20, 180000, 'Procesador Ryzen de quinta generacion');
INSERT INTO Producto VALUES (2, 'Intel Core i7 11th', 0, 20, 200000, 'Procesador de 11th generacion intel');
INSERT INTO Producto VALUES (3, 'Intel i9 9990K', 0, 20, 450000, 'Procesador de 9th generacion intel');
INSERT INTO Producto VALUES (4, 'Nvidia Titan X', 0, 20, 800000, 'Tarjeta gama ultra para mejor rendimiento');
INSERT INTO Producto VALUES (5, 'AMD Radeon RX580', 0, 20, 300000, 'Tarjeta de video Radeon de 8GB para videojuegos');
INSERT INTO Producto VALUES (6, 'Nvidia 1650Super', 0, 20, 320000, 'Tarjeta Nvidia de gama alta para videojuegos');
INSERT INTO Producto VALUES (7, 'WD Blue 1TB', 0, 20, 80000, 'Disco de estado solido de 1TB de almacenamiento');
INSERT INTO Producto VALUES (8, 'WD Purple 8TB', 0, 20, 150000, 'Disco duro de 8TB de almacenamiento');
INSERT INTO Producto VALUES (9, 'WD Disco externo 1TB', 0, 20, 80000, 'Disco duro externo de 1TB con USB 3.0');
INSERT INTO Producto VALUES (10, 'Servidor Cisco UCS-SPR-C240M4-P1', 0, 20, 500000, 'Servidor Cisco con Intel Xeon dual');
INSERT INTO Producto VALUES (11, 'Server Dell PowerEdge T40', 0, 20, 600000, 'Servidor con Intel Xeon y 8GB de ram');
INSERT INTO Producto VALUES (12, 'Servidor HP ProLiant ML30', 0, 20, 450000, 'Servidor con Intel de 9na generacion');


/*test*/
select * from Producto;
commit;