
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/18/2018 22:12:34
-- Generated from EDMX file: D:\Project5\EntityModel\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SalesInfo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_CustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_ManagerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemOrder_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_ItemSet] DROP CONSTRAINT [FK_ItemOrder_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrder_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_ItemSet] DROP CONSTRAINT [FK_OrderOrder_Item];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CustomerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[ItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemSet];
GO
IF OBJECT_ID(N'[dbo].[ManagerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerSet];
GO
IF OBJECT_ID(N'[dbo].[Order_ItemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order_ItemSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Customer_Id] int IDENTITY(1,1) NOT NULL,
    [Customer_First_name] nvarchar(50)  NOT NULL,
    [Customer_Last_Name] nvarchar(50)  NOT NULL,
    [Phone_number] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Order_Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Customer_Id] int  NOT NULL,
    [Manager_Id] int  NOT NULL,
    [Customer_Customer_Id] int  NOT NULL,
    [Manager_Manager_Id] int  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [Item_Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Cost] float  NOT NULL,
    [Amount] int  NOT NULL
);
GO

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [Manager_Id] int IDENTITY(1,1) NOT NULL,
    [Manager_First_name] nvarchar(50)  NOT NULL,
    [Manager_Last_name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Order_ItemSet'
CREATE TABLE [dbo].[Order_ItemSet] (
    [Order_Id] int IDENTITY(1,1) NOT NULL,
    [Item_Id] int  NOT NULL,
    [Item_amount] int  NOT NULL,
    [Total_cost] float  NOT NULL,
    [Item_Item_Id] int  NOT NULL,
    [Order_Order_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Customer_Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Customer_Id] ASC);
GO

-- Creating primary key on [Order_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Order_Id] ASC);
GO

-- Creating primary key on [Item_Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Item_Id] ASC);
GO

-- Creating primary key on [Manager_Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([Manager_Id] ASC);
GO

-- Creating primary key on [Order_Id], [Item_Id] in table 'Order_ItemSet'
ALTER TABLE [dbo].[Order_ItemSet]
ADD CONSTRAINT [PK_Order_ItemSet]
    PRIMARY KEY CLUSTERED ([Order_Id], [Item_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_Customer_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_CustomerOrder]
    FOREIGN KEY ([Customer_Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Customer_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrder'
CREATE INDEX [IX_FK_CustomerOrder]
ON [dbo].[OrderSet]
    ([Customer_Customer_Id]);
GO

-- Creating foreign key on [Manager_Manager_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ManagerOrder]
    FOREIGN KEY ([Manager_Manager_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Manager_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerOrder'
CREATE INDEX [IX_FK_ManagerOrder]
ON [dbo].[OrderSet]
    ([Manager_Manager_Id]);
GO

-- Creating foreign key on [Item_Item_Id] in table 'Order_ItemSet'
ALTER TABLE [dbo].[Order_ItemSet]
ADD CONSTRAINT [FK_ItemOrder_Item]
    FOREIGN KEY ([Item_Item_Id])
    REFERENCES [dbo].[ItemSet]
        ([Item_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemOrder_Item'
CREATE INDEX [IX_FK_ItemOrder_Item]
ON [dbo].[Order_ItemSet]
    ([Item_Item_Id]);
GO

-- Creating foreign key on [Order_Order_Id] in table 'Order_ItemSet'
ALTER TABLE [dbo].[Order_ItemSet]
ADD CONSTRAINT [FK_OrderOrder_Item]
    FOREIGN KEY ([Order_Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Order_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrder_Item'
CREATE INDEX [IX_FK_OrderOrder_Item]
ON [dbo].[Order_ItemSet]
    ([Order_Order_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------