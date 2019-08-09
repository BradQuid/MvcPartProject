IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723190822_Initial')
BEGIN
    CREATE TABLE [Part] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(25) NOT NULL,
        [Now] datetime2 NOT NULL,
        [UsedFor] nvarchar(max) NULL,
        [Flagged] nvarchar(max) NULL,
        [Color] nvarchar(max) NULL,
        [TextColor] nvarchar(max) NULL,
        CONSTRAINT [PK_Part] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723190822_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190723190822_Initial', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723193808_Project')
BEGIN
    CREATE TABLE [Project] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(25) NOT NULL,
        CONSTRAINT [PK_Project] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723193808_Project')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190723193808_Project', N'2.2.6-servicing-10079');
END;

GO

