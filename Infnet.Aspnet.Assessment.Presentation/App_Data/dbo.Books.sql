CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(MAX) NOT NULL, 
    [Isbn] VARCHAR(MAX) NOT NULL, 
    [LauchDate] DATE NOT NULL
)
