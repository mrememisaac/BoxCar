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

CREATE TABLE [BasketChangeEvents] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [EngineId] uniqueidentifier NOT NULL,
    [ChassisId] uniqueidentifier NOT NULL,
    [VehicleId] uniqueidentifier NOT NULL,
    [OptionPackId] uniqueidentifier NOT NULL,
    [InsertedAt] datetimeoffset NOT NULL,
    [BasketChangeType] int NOT NULL,
    CONSTRAINT [PK_BasketChangeEvents] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Baskets] (
    [BasketId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Baskets] PRIMARY KEY ([BasketId])
);
GO

CREATE TABLE [Chassis] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    CONSTRAINT [PK_Chassis] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Engines] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [FuelType] int NOT NULL,
    [IgnitionMethod] int NOT NULL,
    [Strokes] int NOT NULL,
    [Price] int NOT NULL,
    CONSTRAINT [PK_Engines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OptionPacks] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_OptionPacks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vehicles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ChassisId] uniqueidentifier NOT NULL,
    [EngineId] uniqueidentifier NOT NULL,
    [OptionPackId] uniqueidentifier NOT NULL,
    [Price] int NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehicles_Chassis_ChassisId] FOREIGN KEY ([ChassisId]) REFERENCES [Chassis] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vehicles_Engines_EngineId] FOREIGN KEY ([EngineId]) REFERENCES [Engines] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vehicles_OptionPacks_OptionPackId] FOREIGN KEY ([OptionPackId]) REFERENCES [OptionPacks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [BasketLines] (
    [BasketLineId] uniqueidentifier NOT NULL,
    [BasketId] uniqueidentifier NOT NULL,
    [VehicleId] uniqueidentifier NOT NULL,
    [EngineId] uniqueidentifier NOT NULL,
    [ChassisId] uniqueidentifier NOT NULL,
    [OptionPackId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] int NOT NULL,
    CONSTRAINT [PK_BasketLines] PRIMARY KEY ([BasketLineId]),
    CONSTRAINT [FK_BasketLines_Baskets_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [Baskets] ([BasketId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BasketLines_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BasketLines_BasketId] ON [BasketLines] ([BasketId]);
GO

CREATE INDEX [IX_BasketLines_VehicleId] ON [BasketLines] ([VehicleId]);
GO

CREATE INDEX [IX_Vehicles_ChassisId] ON [Vehicles] ([ChassisId]);
GO

CREATE INDEX [IX_Vehicles_EngineId] ON [Vehicles] ([EngineId]);
GO

CREATE INDEX [IX_Vehicles_OptionPackId] ON [Vehicles] ([OptionPackId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018150944_Initial', N'7.0.12');
GO

COMMIT;
GO

