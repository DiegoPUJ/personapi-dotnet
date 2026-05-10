-- ============================================================
-- Script DDL y DML - Laboratorio 1
-- Base de datos: persona_db
-- Proyecto: personapi-dotnet
-- Descripción: Creación de tablas e inserción de datos iniciales
-- ============================================================

-- Crear base de datos
IF DB_ID('persona_db') IS NULL
BEGIN
    CREATE DATABASE persona_db;
END
GO

USE persona_db;
GO

-- Eliminar tablas si existen, respetando dependencias
IF OBJECT_ID('dbo.estudios', 'U') IS NOT NULL
    DROP TABLE dbo.estudios;
GO

IF OBJECT_ID('dbo.telefono', 'U') IS NOT NULL
    DROP TABLE dbo.telefono;
GO

IF OBJECT_ID('dbo.profesion', 'U') IS NOT NULL
    DROP TABLE dbo.profesion;
GO

IF OBJECT_ID('dbo.persona', 'U') IS NOT NULL
    DROP TABLE dbo.persona;
GO

-- ============================================================
-- DDL: Creación de tablas
-- ============================================================

CREATE TABLE persona (
    cc INT NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    genero CHAR(1) NOT NULL,
    edad INT NOT NULL,
    CONSTRAINT PK_persona PRIMARY KEY (cc),
    CONSTRAINT CK_persona_genero CHECK (genero IN ('M', 'F')),
    CONSTRAINT CK_persona_edad CHECK (edad >= 0)
);
GO

CREATE TABLE profesion (
    id INT NOT NULL,
    nom VARCHAR(90) NOT NULL,
    des TEXT NULL,
    CONSTRAINT PK_profesion PRIMARY KEY (id)
);
GO

CREATE TABLE telefono (
    num VARCHAR(15) NOT NULL,
    oper VARCHAR(45) NOT NULL,
    duenio INT NOT NULL,
    CONSTRAINT PK_telefono PRIMARY KEY (num),
    CONSTRAINT FK_telefono_persona FOREIGN KEY (duenio)
        REFERENCES persona(cc)
);
GO

CREATE TABLE estudios (
    id_prof INT NOT NULL,
    cc_per INT NOT NULL,
    fecha DATE NOT NULL,
    univer VARCHAR(50) NOT NULL,
    CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
    CONSTRAINT FK_estudio_profesion FOREIGN KEY (id_prof)
        REFERENCES profesion(id),
    CONSTRAINT FK_estudio_persona FOREIGN KEY (cc_per)
        REFERENCES persona(cc)
);
GO

-- ============================================================
-- DML: Inserción de datos iniciales
-- ============================================================

INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
(1001, 'Carlos', 'Ramirez', 'M', 25),
(1002, 'Laura', 'Gomez', 'F', 30),
(1003, 'Andres', 'Martinez', 'M', 28),
(1004, 'Sofia', 'Lopez', 'F', 22);
GO

INSERT INTO profesion (id, nom, des) VALUES
(1, 'Ingeniero de Sistemas', 'Profesional encargado del diseño y desarrollo de soluciones tecnologicas.'),
(2, 'Administrador de Empresas', 'Profesional orientado a la gestion organizacional.'),
(3, 'Contador Publico', 'Profesional encargado de la gestion contable y financiera.'),
(4, 'Disenador Grafico', 'Profesional especializado en comunicacion visual.');
GO

INSERT INTO telefono (num, oper, duenio) VALUES
('3001234567', 'Claro', 1001),
('3109876543', 'Movistar', 1002),
('3205577788', 'Tigo', 1003),
('3154488999', 'WOM', 1004);
GO

INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES
(1, 1001, '2022-06-15', 'Pontificia Universidad Javeriana'),
(2, 1002, '2021-11-20', 'Universidad Nacional'),
(3, 1003, '2020-09-10', 'Universidad de los Andes'),
(4, 1004, '2023-03-25', 'Universidad Jorge Tadeo Lozano');
GO

-- ============================================================
-- Consultas de verificación
-- ============================================================

SELECT * FROM persona;
SELECT * FROM profesion;
SELECT * FROM telefono;
SELECT * FROM estudios;
GO