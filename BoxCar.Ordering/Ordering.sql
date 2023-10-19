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

CREATE TABLE [Orders] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [OrderTotal] int NOT NULL,
    [OrderPlaced] datetime2 NOT NULL,
    [OrderPaid] bit NOT NULL,
    [FulfillmentStatus] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderLine] (
    [Id] uniqueidentifier NOT NULL,
    [VehicleId] uniqueidentifier NOT NULL,
    [EngineId] uniqueidentifier NOT NULL,
    [ChassisId] uniqueidentifier NOT NULL,
    [OptionPackId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] int NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_OrderLine] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderLine_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OrderLine_OrderId] ON [OrderLine] ([OrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018144123_Initial', N'7.0.12');
GO

COMMIT;
GO

