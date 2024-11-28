

------------------------------------------------------------------------------
--eliminar la BD ANTERIOR
USE master;
GO

-- Cerrar conexiones activas a la base de datos
ALTER DATABASE EcommerceDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- Eliminar la base de datos
DROP DATABASE EcommerceDB;
GO
----------------------------------------------------------------------------------

CREATE DATABASE EcommerceDB;
GO

USE EcommerceDB;
GO




CREATE TABLE Categorias (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Marcas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Articulos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    IDCategoria INT NOT NULL,
    IDMarca INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Articulos_Categorias FOREIGN KEY (IDCategoria) REFERENCES Categorias(ID),
    CONSTRAINT FK_Articulos_Marcas FOREIGN KEY (IDMarca) REFERENCES Marcas(ID)
);
GO

CREATE TABLE Imagenes (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDArticulo INT NOT NULL,
    ImagenURL NVARCHAR(1500) NOT NULL,
    CONSTRAINT FK_Imagenes_Articulos FOREIGN KEY (IDArticulo) REFERENCES Articulos(ID) ON DELETE CASCADE
);
GO


CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL
);
GO


CREATE TABLE DatosPersonales (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT NOT NULL,
    DNI NVARCHAR(20),
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Domicilio NVARCHAR(255),
    Pais NVARCHAR(100),
    Provincia NVARCHAR(100),
    Telefono NVARCHAR(20),
    CONSTRAINT FK_DatosPersonales_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO


CREATE TABLE Pedidos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT NOT NULL,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Estado NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO


CREATE TABLE DetallesPedidos (
    IDDetallePedido INT PRIMARY KEY IDENTITY(1,1),
    IDPedido INT NOT NULL,
    IDArticulo INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_DetallesPedidos_Pedidos FOREIGN KEY (IDPedido) REFERENCES Pedidos(ID),
    CONSTRAINT FK_DetallesPedidos_Articulos FOREIGN KEY (IDArticulo) REFERENCES Articulos(ID)
);
GO




-------------------------------------------------------------------------------------

INSERT INTO Categorias (Nombre) 
VALUES ('Celulares'), ('Televisores'), ('Consolas');

-- Marcas
INSERT INTO Marcas (Nombre) 
VALUES ('Apple'), ('Samsung'), ('Sony'), ('Microsoft'), ('LG');


INSERT INTO Articulos (Codigo, Descripcion, IDCategoria, IDMarca, Nombre, Precio)
VALUES 
 -- Celulares
('CEL002', 'Celular Samsung Galaxy S22 Ultra', 1, 2, 'Galaxy S22 Ultra', 389999.99),
('CEL003', 'Celular Sony Xperia 1 V', 1, 3, 'Sony Xperia 1 V', 319999.99),
('CEL004', 'Celular Apple iPhone 14 Pro Max', 1, 1, 'iPhone 14 Pro Max', 499999.99),
('CEL005', 'Celular Samsung Galaxy Z Fold 4', 1, 2, 'Galaxy Z Fold 4', 599999.99),
('CEL006', 'Celular Sony Xperia 10 V', 1, 3, 'Sony Xperia 10 V', 189999.99),

-- Televisores
('TV002', 'Televisor LG OLED CX 55 pulgadas', 2, 5, 'LG OLED CX 55"', 699999.99),
('TV003', 'Televisor Sony Bravia XR 65 pulgadas', 2, 3, 'Sony Bravia XR 65"', 799999.99),
('TV004', 'Televisor Samsung Neo QLED 8K 65 pulgadas', 2, 2, 'Samsung Neo QLED 8K', 1299999.99),
('TV005', 'Televisor LG NanoCell 50 pulgadas', 2, 5, 'LG NanoCell 50"', 399999.99),
('TV006', 'Televisor Sony X90J 55 pulgadas', 2, 3, 'Sony X90J 55"', 459999.99),

-- Consolas
('CONS002', 'Consola Microsoft Xbox Series S', 3, 4, 'Xbox Series S', 149999.99),
('CONS003', 'Consola Nintendo Switch Lite', 3, 3, 'Nintendo Switch Lite', 99999.99),
('CONS004', 'Consola Sony PlayStation 5 Digital Edition', 3, 3, 'PlayStation 5 Digital Edition', 219999.99),
('CONS005', 'Consola Microsoft Xbox Series X Halo Edition', 3, 4, 'Xbox Series X Halo Edition', 279999.99),
('CONS006', 'Consola Sony PlayStation 4 Slim 1TB', 3, 3, 'PlayStation 4 Slim 1TB', 139999.99);

INSERT INTO Usuarios (Nombre, Email, Contraseña)
VALUES 
('Juan Perez', 'admin@ecommerce.com', 'admin123'),
('Pedro Gomez', 'cliente@ecommerce.com', 'cliente123');


INSERT INTO DatosPersonales (IDUsuario, DNI, Nombre, Apellido, Domicilio, Pais, Provincia, Telefono)
VALUES 
(1, '12345678', 'Juan', 'Perez', 'Calle Falsa 123', 'Argentina', 'Buenos Aires', '01112345678'),
(2, '87654321', 'Pedro', 'Gomez', 'Avenida Siempre Viva 456', 'Argentina', 'Córdoba', '01187654321');


INSERT INTO Pedidos (IDUsuario, Estado)
VALUES (2, 'pendiente');

INSERT INTO DetallesPedidos (IDPedido, IDArticulo, Cantidad, PrecioUnitario)
VALUES 
(1, 2, 1, 189999.99),
(1, 3, 1, 249999.99);

------------------------------------------------------------------------------------------------------------------

-- Tabla de Imágenes
INSERT INTO Imagenes (IDArticulo, ImagenURL)
VALUES 
-- Celulares
(1, 'https://electrogv.com.ar/wp-content/uploads/2022/06/gakaxys22ultra.png'), -- Galaxy S22 Ultra (Imagen 1)
(1, 'https://i.blogs.es/f69f43/img_9583/450_1000.jpg'), -- Galaxy S22 Ultra (Imagen 2)
(1, 'https://cdn.andro4all.com/andro4all/2022/02/S-Pen-del-Samsung-Galaxy-S22-Ultra-1.jpg'), -- Galaxy S22 Ultra (Imagen 3)

(2, 'https://i.blogs.es/4a74a1/sony-xperia-1-v-10-v/1366_2000.jpeg'), -- Sony Xperia 1 V (Imagen 1)
(2, 'https://static1.pocketlintimages.com/wordpress/wp-content/uploads/wm/2023/08/sony-xperia-1-v-vs-apple-iphone-14-pro.jpg'), -- Sony Xperia 1 V (Imagen 2)
(2, 'https://www.trustedreviews.com/wp-content/uploads/sites/54/2023/06/Sony-Xperia-1-V-review-14-1300x731.jpg'), -- Sony Xperia 1 V (Imagen 3)

(3, 'https://gorilagames.com/img/Public/1019-producto-iphone-14-pro-max-1-8286.jpg'), -- iPhone 14 Pro Max (Imagen 1)
(3, 'https://i.blogs.es/3394d8/img_0797/1366_2000.jpg'), -- iPhone 14 Pro Max (Imagen 2)
(3, 'https://cdsassets.apple.com/live/SZLF0YNV/images/sp/111846_sp875-sp876-iphone14-pro-promax.png'), -- iPhone 14 Pro Max (Imagen 3)

(4, 'https://i.blogs.es/1609e4/2-023_product_galaxy_zfold4_beige_openback_hi-copia/650_1200.jpeg'), -- Galaxy Z Fold 4 (Imagen 1)
(4, 'https://i.pcmag.com/imagery/reviews/06zGAYOKvHFzoerdzdpTDQX-1.fit_scale.size_760x427.v1660661515.jpg'), -- Galaxy Z Fold 4 (Imagen 2)
(4, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSszVbrUAVed1NviTIDzPk59s1YoT0dBIgiSw&s'), -- Galaxy Z Fold 4 (Imagen 3)

(5, 'https://cdn.andro4all.com/andro4all/2023/03/trasera-poco-x5-pro-5g.jpg'), -- Sony Xperia 10 V (Imagen 1)
(5, 'https://files.gsmchoice.com/phones/sony-xperia-10-v/sony-xperia-10-v-02.jpg'), -- Sony Xperia 10 V (Imagen 2)


-- Televisores
(6, 'https://www.lg.com/content/dam/channel/wcms/co/gallery/galerias-tv/OLED65C4PSA-DZ-1.jpg/_jcr_content/renditions/thum-1600x1062.jpeg'), -- LG OLED CX 55 pulgadas (Imagen 1)
(6, 'https://s1.elespanol.com/2020/08/20/actualidad/actualidad_514459095_158052526_854x640.jpg'), -- LG OLED CX 55 pulgadas (Imagen 2)


(7, 'https://arsonyb2c.vtexassets.com/arquivos/ids/357813/Serie_X80J_1000x1000px_65inch_02_TV_Dimension_LATAM.jpg?v=637695493066200000'), -- Sony Bravia XR 65 pulgadas (Imagen 1)
(7, 'https://m.media-amazon.com/images/I/81aMS6p4xlL.jpg'), -- Sony Bravia XR 65 pulgadas (Imagen 2)

(8, 'https://images.samsung.com/is/image/samsung/p6pim/ar/qn85qn800bgczb/gallery/ar-qled-tv-qn85qn800bgczb-front-black-535134786?$650_519_PNG$'), -- Samsung Neo QLED 8K (Imagen 1)
(8, 'https://www.clarin.com/2023/03/23/gxlknnYGZ_2000x1500__1.jpg'), -- Samsung Neo QLED 8K (Imagen 2)


(9, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ2i1tsf83JJ7OJnArySAwh6HnQQBo02nYD2A&s'), -- LG NanoCell 50 pulgadas (Imagen 1)
(9, 'https://www.lg.com/ar/images/TV/features/NANO2022/tv-nanocell-03-1-a5-gen5-aI-processor-4k-desktop.jpg'), -- LG NanoCell 50 pulgadas (Imagen 2)


(10, 'https://i.blogs.es/178486/x90j-1-copia/450_1000.webp'), -- Sony X90J 55 pulgadas (Imagen 1)


-- Consolas
(11, 'https://nextgames.com.ar/img/Public/1040-producto-61hwabmvoxl-sl1500-7958.jpg'), -- Xbox Series S (Imagen 1)
(11, 'https://gamecenterok.com/img/Public/1155-producto-nueva-de-1-tb-1-2277.jpg'), -- Xbox Series S (Imagen 2)


(12, 'https://m.media-amazon.com/images/I/71sWmEcjPJL._AC_SL1500_.jpg'), -- Nintendo Switch Lite (Imagen 1)


(13, 'https://xtremegames.com.ar/wp-content/uploads/2024/01/61JbCra7GL._SL1500_-600x600.jpg'), -- PlayStation 5 Digital Edition (Imagen 1)


(14, 'https://m.media-amazon.com/images/I/81-93aoPibL._AC_SL1500_.jpg'), -- Xbox Series X Halo Edition (Imagen 1)


(15, 'https://http2.mlstatic.com/D_NQ_NP_798586-MLA40076060236_122019-O.webp'); -- PlayStation 4 Slim 1TB (Imagen 1)

--------------------------------------------------------------------------------------------------------------------------------------------------------
USE EcommerceDB;
GO

-- Agregar la columna 'tipoUsuario' a la tabla 'Usuarios'
ALTER TABLE Usuarios
ADD tipoUsuario INT NOT NULL DEFAULT 2;
GO

-- Actualizar la columna 'tipoUsuario' para que el usuario con 'IDUsuario' = 1 tenga el valor 1
UPDATE Usuarios
SET tipoUsuario = 1
WHERE IDUsuario = 1;
GO

-- Confirmar los cambios realizados
SELECT * FROM Usuarios;

ALTER TABLE [EcommerceDB].[dbo].[Usuarios]
DROP COLUMN [Nombre];

select * from DatosPersonales;


SELECT * FROM Pedidos;





--hacerlo null para que un usaurio que no este registrado pueda hacer la compra.
ALTER TABLE Pedidos ALTER COLUMN IDUsuario INT NULL;



------------------------------------------------------------------------------------------------------------->
-- 26 / 11 --> AGREGAMOS
ALTER TABLE Articulos ADD Stock INT NOT NULL DEFAULT 0;

	
--CREATE TRIGGER TR_ActualizarStock
--ON DetallesPedidos
--AFTER INSERT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    -- Actualizar el stock en la tabla Articulos
--    UPDATE A
--    SET A.Stock = A.Stock - DP.Cantidad
--    FROM Articulos A
--    INNER JOIN inserted DP
--        ON A.ID = DP.IDArticulo;

--    -- Verificar que ningún artículo quede con stock negativo
--    IF EXISTS (SELECT 1 FROM Articulos WHERE Stock < 0)
--    BEGIN
--        -- Revertir la transacción si el stock es insuficiente
--        ROLLBACK TRANSACTION;
--        THROW 50000, 'Error: Stock insuficiente para uno o más artículos.', 1;
--    END
--END;
--GO




USE EcommerceDB;
GO
-- Elimina y vuelve a agregar las claves foraneas en DatosPersonales y Pedidos.
-- Configura ON DELETE CASCADE para que la eliminación en Usuarios se propague a las tablas relacionadas 

ALTER TABLE DatosPersonales
DROP CONSTRAINT FK_DatosPersonales_Usuarios;

ALTER TABLE DatosPersonales
ADD CONSTRAINT FK_DatosPersonales_Usuarios
FOREIGN KEY (IDUsuario)
REFERENCES Usuarios(IDUsuario)
ON DELETE CASCADE;

ALTER TABLE Pedidos
DROP CONSTRAINT FK_Pedidos_Usuarios;

ALTER TABLE Pedidos
ADD CONSTRAINT FK_Pedidos_Usuarios
FOREIGN KEY (IDUsuario)
REFERENCES Usuarios(IDUsuario)
ON DELETE CASCADE;


