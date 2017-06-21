CREATE TABLE [dbo].[AuthorBook] (
    [Id]       INT NOT NULL IDENTITY,
    [AuthorId] INT NOT NULL,
    [BookId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorBook_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id]),
    CONSTRAINT [FK_AuthorBook_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
);