CREATE TABLE [dbo].[AuthorBook]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AuthorId] INT NOT NULL, 
    [BookId] INT NOT NULL, 
    CONSTRAINT [FK_AuthorBook_Author] FOREIGN KEY ([AuthorId]) REFERENCES [Author]([Id]), 
    CONSTRAINT [FK_AuthorBook_Books] FOREIGN KEY ([BookId]) REFERENCES [Books]([Id])
)
