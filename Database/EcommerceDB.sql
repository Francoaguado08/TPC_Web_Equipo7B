--Crear la base de datos
CREATE DATABASE EcommerceDB;
GO

USE EcommerceDB;
GO

-- Tabla de Categorías
CREATE TABLE Categorias (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,   
);
GO

-- Tabla de Marcas
CREATE TABLE Marcas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
   
);
GO

-- Tabla de Artículos
CREATE TABLE Articulos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    IDCategoria INT FOREIGN KEY REFERENCES Categorias(ID),
    IDMarca INT FOREIGN KEY REFERENCES Marcas(ID),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
 
);
GO

-- Tabla de Imágenes
CREATE TABLE Imagenes (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDArticulo INT FOREIGN KEY REFERENCES Articulos(ID) ,
    ImagenURL NVARCHAR(255) NOT NULL
    
);
GO

-- Tabla de Tipos de Usuario
CREATE TABLE TiposUsuario (
    IDTipoUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL,
    IDTipoUsuario INT FOREIGN KEY REFERENCES TiposUsuario(IDTipoUsuario)
);
GO

-- Tabla de Datos Personales
CREATE TABLE DatosPersonales (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT FOREIGN KEY REFERENCES Usuarios(IDUsuario) ,
    DNI NVARCHAR(20),
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Domicilio NVARCHAR(255),
    Pais NVARCHAR(100),
    Provincia NVARCHAR(100),
    Telefono NVARCHAR(20)
);
GO

-- Tabla de Pedidos
CREATE TABLE Pedidos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT FOREIGN KEY REFERENCES Usuarios(IDUsuario),
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Estado NVARCHAR(50) NOT NULL -- (pendiente, enviado, cancelado)
);
GO

-- Tabla de Detalles del Pedido
CREATE TABLE DetallesPedidos (
    IDDetallePedido INT PRIMARY KEY IDENTITY(1,1),
    IDPedido INT FOREIGN KEY REFERENCES Pedidos(ID) ,
    IDArticulo INT FOREIGN KEY REFERENCES Articulos(ID),
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL
);


------------------------------------------------------------ hasta aca es la creacion!------------------------------------








--INSERTAR DATOS!

SELECT * FROM CATEGORIAS;


-- Insertar Categorías (Celulares, Televisores, Consolas)
INSERT INTO Categorias (Nombre) 
VALUES ('Celulares'), ('Televisores'), ('Consolas');

-- Insertar Marcas
INSERT INTO Marcas (Nombre) 
VALUES ('Apple'), ('Samsung'), ('Sony'), ('Microsoft'), ('LG');



-- Insertar datos en la tabla Artículos
INSERT INTO Articulos (Codigo, Descripcion, IDCategoria, IDMarca, Nombre, Precio)
VALUES 
-- Televisor Samsung Smart TV 55" (IDCategoria = 2 para Televisores, IDMarca = 2 para Samsung)
('TV001', 'Televisor Samsung 55 pulgadas', 2, 2, 'Samsung Smart TV 55''', 59999.99),

-- Celular Apple iPhone 13 (IDCategoria = 1 para Celulares, IDMarca = 1 para Apple)
('CEL001', 'Celular Apple iPhone 13', 1, 1, 'iPhone 13', 189999.99),

-- Consola Sony PlayStation 5 (IDCategoria = 3 para Consolas, IDMarca = 3 para Sony)
('CONS001', 'Consola Sony PlayStation 5', 3, 3, 'PlayStation 5', 249999.99);


SELECT * FROM Articulos;

-- Insertar imágenes para los artículos
INSERT INTO Imagenes (IDArticulo, ImagenURL)
VALUES 
(1, 'https://images.samsung.com/is/image/samsung/p6pim/ar/un55au7000gczb/gallery/ar-uhd-au7000-un55au7000gczb-489502379?$1300_1038_PNG$'),  -- Samsung Smart TV
(2, 'https://acdn.mitiendanube.com/stores/001/097/819/products/iphone-13-finish-select-202207-blue-e7a4d7a7689dadee5d16997239797318-1024-1024.jpeg'),      -- iPhone 13
(3, 'https://gmedia.playstation.com/is/image/SIEPDC/ps5-pro-dualsense-image-block-01-en-16aug24');            -- PlayStation 5






-- Insertar usuarios
INSERT INTO Usuarios (Nombre, Email, Contraseña, IDTipoUsuario)
VALUES 
('Juan Perez', 'admin@ecommerce.com', 'admin123', 1),  -- Admin
('Pedro Gomez', 'cliente@ecommerce.com', 'cliente123', 2); -- Cliente


-- Insertar datos personales para los usuarios
INSERT INTO DatosPersonales (IDUsuario, DNI, Nombre, Apellido, Domicilio, Pais, Provincia, Telefono)
VALUES 
(1, '12345678', 'Juan', 'Perez', 'Calle Falsa 123', 'Argentina', 'Buenos Aires', '01112345678'), -- Admin
(2, '87654321', 'Pedro', 'Gomez', 'Avenida Siempre Viva 456', 'Argentina', 'Córdoba', '01187654321'); -- Cliente


-- Insertar pedidos
INSERT INTO Pedidos (IDUsuario, Estado)
VALUES 
(2, 'pendiente');


-- Insertar detalles del pedido
INSERT INTO DetallesPedidos (IDPedido, IDArticulo, Cantidad, PrecioUnitario)
VALUES 
(1, 2, 1, 189999.99),  -- iPhone 13
(1, 3, 1, 249999.99);  -- PlayStation 5



-- Seleccionar todos los datos de la tabla Categorías
SELECT * FROM Categorias;

-- Seleccionar todos los datos de la tabla Marcas
SELECT * FROM Marcas;

-- Seleccionar todos los datos de la tabla Artículos
SELECT * FROM Articulos;

-- Seleccionar todos los datos de la tabla Imágenes
SELECT * FROM Imagenes;



-- Seleccionar todos los datos de la tabla Usuarios
SELECT * FROM Usuarios;

-- Seleccionar todos los datos de la tabla Datos Personales
SELECT * FROM DatosPersonales;

-- Seleccionar todos los datos de la tabla Pedidos
SELECT * FROM Pedidos;

-- Seleccionar todos los datos de la tabla Detalles del Pedido
SELECT * FROM DetallesPedidos;




--------------------------------------------------------------------------------------------------------

-- ELIMINAR TABLA DEL TIPOUSERS
	SELECT 
    name AS ForeignKeyName 
FROM 
    sys.foreign_keys 
WHERE 
    referenced_object_id = OBJECT_ID('TiposUsuario') 
    AND parent_object_id = OBJECT_ID('Usuarios');
-- PRIMERO ESTO




--2
ALTER TABLE Usuarios
DROP CONSTRAINT FK__Usuarios__IDTipo__44FF419A;  --(PRIMERO ESTAS DOS LINEAS)


DROP TABLE TiposUsuario; -- (LUEGO ESTA LINEA.)


-----------------------------------------------------------------------------------------------------
 

 --INSERTO MAS IMAGENES PARA PROBAR EL CARROUSEL...
 -- Insertar imágenes para los artículos
INSERT INTO Imagenes (IDArticulo, ImagenURL)
VALUES 
(1, 'https://tienda.personal.com.ar/images/Smart_TV_43_4_K_AU_7000_Frente_min_9ec8fb1c52.png'),  -- Samsung Smart TV
(2, 'https://icenter.ar/wp-content/uploads/2024/06/ip-13-usado.png'),      -- iPhone 13

(3, 'https://www.techopedia.com/wp-content/uploads/2024/09/PS5-Pro-vs-PS5-How-Does-the-Upgraded-PlayStation-5-Compare.jpg'); --PS5


-------------------------------------------------------------------------------------------------------------------------------------------