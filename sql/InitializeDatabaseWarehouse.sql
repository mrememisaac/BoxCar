USE master
GO
DROP DATABASE IF EXISTS BoxCarWareHouseDb
GO

CREATE DATABASE BoxCarWareHouseDb
GO 
USE BoxCarWareHouseDb
GO 
USE master;
GO
CREATE LOGIN [cr_dbuser] WITH PASSWORD=N'Sql1nContainersR0cks!', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE BoxCarWareHouseDb;
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

CREATE TABLE [Items] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ItemType] int NOT NULL,
    [ItemTypeId] uniqueidentifier NOT NULL,
    [SpecificationKey] nvarchar(max) NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231018145624_Initial', N'7.0.12');
GO

COMMIT;
GO

