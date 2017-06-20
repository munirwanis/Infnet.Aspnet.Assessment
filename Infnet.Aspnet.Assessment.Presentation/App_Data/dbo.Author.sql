CREATE TABLE [dbo].[Author]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(256) NOT NULL, 
    [LastName] VARCHAR(256) NOT NULL, 
    [Email] VARCHAR(256) NOT NULL, 
    [Birthdate] DATE NOT NULL
)
