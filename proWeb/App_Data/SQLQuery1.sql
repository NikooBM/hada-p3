-- Creando Tabla categorías
CREATE TABLE [dbo].[Categories]
(
[id] INT IDENTITY (1, 1) NOT NULL,
[name] NVARCHAR (32) NOT NULL,
PRIMARY KEY CLUSTERED ([id] ASC)
)
-- Creando Tabla productos
CREATE TABLE [dbo].[Products]
(
 [id] INT IDENTITY (1, 1) NOT NULL,
 [name] NVARCHAR (32) NOT NULL,
 [code] NVARCHAR (16) NOT NULL,
 [amount] INT NOT NULL,
 [price] FLOAT NOT NULL,
 [category] INT NOT NULL,
 [creationDate] DATETIME NOT NULL,
 PRIMARY KEY CLUSTERED ([id] ASC),
 UNIQUE NONCLUSTERED ([code] ASC),
 constraint FK_Products_category foreign key (category)
references Categories(id)
)
-- Insertando categorías
INSERT INTO [dbo].[Categories] VALUES
('Computing'),('Telephony'),('Gaming'),('Home appliances');