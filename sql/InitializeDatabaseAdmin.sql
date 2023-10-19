USE master
GO
DROP DATABASE IF EXISTS BoxCarAdminDb
GO

CREATE DATABASE BoxCarAdminDb
GO 
USE BoxCarAdminDb
GO 

USE master;
GO
CREATE LOGIN [cr_dbuser] WITH PASSWORD=N'Sql1nContainersR0cks!', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE BoxCarAdminDb;
GO
CREATE USER [cr_dbuser] FOR LOGIN [cr_dbuser];
GO
EXEC sp_addrolemember N'db_owner', [cr_dbuser];
GO

----------------------------------------------------------------------------
--- TABLE CREATION
----------------------------------------------------------------------------
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Chassis] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Price] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Chassis] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Engines] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [FuelType] nvarchar(max) NOT NULL,
    [IgnitionMethod] nvarchar(max) NOT NULL,
    [Strokes] int NOT NULL,
    [Price] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Engines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Factories] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Factories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Option] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Option] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OptionPacks] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_OptionPacks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [WareHouses] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(50) NOT NULL,
    [UpdatedBy] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_WareHouses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OptionOptionPack] (
    [OptionPacksId] uniqueidentifier NOT NULL,
    [OptionsId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_OptionOptionPack] PRIMARY KEY ([OptionPacksId], [OptionsId]),
    CONSTRAINT [FK_OptionOptionPack_OptionPacks_OptionPacksId] FOREIGN KEY ([OptionPacksId]) REFERENCES [OptionPacks] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OptionOptionPack_Option_OptionsId] FOREIGN KEY ([OptionsId]) REFERENCES [Option] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Vehicles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [EngineId] uniqueidentifier NOT NULL,
    [ChassisId] uniqueidentifier NOT NULL,
    [OptionPackId] uniqueidentifier NOT NULL,
    [Price] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehicles_Chassis_ChassisId] FOREIGN KEY ([ChassisId]) REFERENCES [Chassis] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vehicles_Engines_EngineId] FOREIGN KEY ([EngineId]) REFERENCES [Engines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vehicles_OptionPacks_OptionPackId] FOREIGN KEY ([OptionPackId]) REFERENCES [OptionPacks] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OptionOptionPack_OptionsId] ON [OptionOptionPack] ([OptionsId]);
GO

CREATE INDEX [IX_Vehicles_ChassisId] ON [Vehicles] ([ChassisId]);
GO

CREATE INDEX [IX_Vehicles_EngineId] ON [Vehicles] ([EngineId]);
GO

CREATE INDEX [IX_Vehicles_OptionPackId] ON [Vehicles] ([OptionPackId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018092641_Initial', N'7.0.12');
GO

COMMIT;
GO