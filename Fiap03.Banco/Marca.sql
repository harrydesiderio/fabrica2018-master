﻿CREATE TABLE [dbo].[Marca]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] VARCHAR(50) NOT NULL, 
    [DataCriacao] DATETIME NULL, 
    [Cnpj] VARCHAR(50) NULL
)
