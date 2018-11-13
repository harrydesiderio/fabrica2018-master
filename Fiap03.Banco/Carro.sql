CREATE TABLE [dbo].[Carro]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Marca] VARCHAR(50) NOT NULL, 
	[Placa] CHAR(8) NOT NULL,
    [Ano] INT NULL, 
    [Esportivo] BIT NULL, 
    [Combustivel] INT NULL, 
    [Descricao] VARCHAR(150) NULL
)
