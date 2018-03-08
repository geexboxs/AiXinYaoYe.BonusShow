
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/08/2018 14:36:55
-- Generated from EDMX file: C:\Users\lulus\OneDrive\私活\爱心药业积分中心\AiXinYaoYe\Database\MyDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aixinyaoye.bonusshow];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BonusProducts'
CREATE TABLE [dbo].[BonusProducts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Desc] nvarchar(max)  NOT NULL,
    [Bonus] decimal(18,0)  NOT NULL,
    [DetailPics] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RecommandProducts'
CREATE TABLE [dbo].[RecommandProducts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Desc] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [DetailPics] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BonusProducts'
ALTER TABLE [dbo].[BonusProducts]
ADD CONSTRAINT [PK_BonusProducts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecommandProducts'
ALTER TABLE [dbo].[RecommandProducts]
ADD CONSTRAINT [PK_RecommandProducts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------