CREATE DATABASE PROYECT_PRACTICA1;
GO

USE PROYECT_PRACTICA1;
GO

-- Tabla de formas de pago
CREATE TABLE FORMASPAGO (
    id_formaPago INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(30) NOT NULL,
);

-- Tabla de artículos
CREATE TABLE ARTICULOS (
    id_articulo INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(40) NOT NULL,
    pre_unitario decimal(18,2) NOT NULL,
    activo BIT NOT NULL DEFAULT 1
);


-- Tabla de facturas
CREATE TABLE FACTURAS (
    nroFactura INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    cliente VARCHAR(100) NOT NULL,
    id_formaPago INT NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_FORMAPAGO FOREIGN KEY (id_formaPago)
        REFERENCES FORMASPAGO(id_formaPago)
);

-- Tabla de detalles de factura (opcional: puedes agregarlo si quieres, pero no es común)
CREATE TABLE DETALLESFACTURA (
    id_detalleFactura INT IDENTITY(1,1) PRIMARY KEY,
    cantidad INT NOT NULL,
    nroFactura INT NOT NULL,
    id_articulo INT NOT NULL,
    CONSTRAINT FK_FACTURA FOREIGN KEY (nroFactura) 
        REFERENCES FACTURAS(nroFactura),
    CONSTRAINT FK_ARTICULO FOREIGN KEY (id_articulo) 
        REFERENCES ARTICULOS(id_articulo)
);


INSERT INTO FORMASPAGO (nombre) VALUES ('Efectivo');
INSERT INTO FORMASPAGO (nombre) VALUES ('Tarjeta de Crédito');
INSERT INTO FORMASPAGO (nombre) VALUES ('Transferencia');
INSERT INTO FORMASPAGO (nombre) VALUES ('Cheque');

INSERT INTO ARTICULOS (nombre, pre_unitario) VALUES ('Lapicera', 20.50);
INSERT INTO ARTICULOS (nombre, pre_unitario) VALUES ('Cuaderno', 50.00);
INSERT INTO ARTICULOS (nombre, pre_unitario) VALUES ('Regla', 15.75);
INSERT INTO ARTICULOS (nombre, pre_unitario) VALUES ('Goma', 8.20);

INSERT INTO FACTURAS (fecha, cliente, id_formaPago) VALUES ('2025-08-01', 'Juan Perez', 1);
INSERT INTO FACTURAS (fecha, cliente, id_formaPago) VALUES ('2025-08-02', 'Ana Lopez', 2);
INSERT INTO FACTURAS (fecha, cliente, id_formaPago) VALUES ('2025-08-03', 'Carlos Ruiz', 3);
INSERT INTO FACTURAS (fecha, cliente, id_formaPago) VALUES ('2025-08-04', 'Maria Gomez', 4);

INSERT INTO DETALLESFACTURA (cantidad, nroFactura, id_articulo) VALUES (2, 1, 1);
INSERT INTO DETALLESFACTURA (cantidad, nroFactura, id_articulo) VALUES (1, 1, 3);
INSERT INTO DETALLESFACTURA (cantidad, nroFactura, id_articulo) VALUES (5, 2, 2);
INSERT INTO DETALLESFACTURA (cantidad, nroFactura, id_articulo) VALUES (3, 3, 4); 

