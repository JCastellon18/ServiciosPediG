
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/05/2022 02:06:08
-- Generated from EDMX file: C:\ProyectoFacultativa\ServiciosNuevaBD\ServiciosPediG\Models\PediGModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PediGBD];
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

-- Creating table 'Clientes'
CREATE TABLE [dbo].[Clientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(max)  NOT NULL,
    [Apellidos] nvarchar(max)  NOT NULL,
    [NumTelf] int  NULL,
    [Correo] nvarchar(max)  NULL,
    [User] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DetalleFacturas'
CREATE TABLE [dbo].[DetalleFacturas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrecionUnitario] float  NOT NULL,
    [Cantidad] int  NOT NULL,
    [Subtotal] float  NOT NULL,
    [FacturaId] int  NOT NULL,
    [ArticuloId] int  NOT NULL,
    [Factura_Id] int  NOT NULL
);
GO

-- Creating table 'TipoEntregas'
CREATE TABLE [dbo].[TipoEntregas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [NombreTipo] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Articulos'
CREATE TABLE [dbo].[Articulos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NombreArticulo] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Img] varbinary(max)  NULL,
    [Precio] float  NOT NULL,
    [Cantidad] int  NOT NULL,
    [CategoriaId] int  NOT NULL,
    [User] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categorias'
CREATE TABLE [dbo].[Categorias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [NombreCategoria] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [NombreUsuario] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Contrasenia] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Facturas'
CREATE TABLE [dbo].[Facturas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumeroFactura] int  NOT NULL,
    [FechaFactura] datetime  NOT NULL,
    [TotalNeto] float  NOT NULL,
    [TipoDePagoId] int  NOT NULL,
    [TipoEntregaId] int  NOT NULL,
    [ClienteId] int  NOT NULL,
    [DireccionPedido] nvarchar(max)  NOT NULL,
    [EstadoFacturaId] int  NOT NULL,
    [FechaEntregaPedido] datetime  NOT NULL,
    [User] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TipoDePagos'
CREATE TABLE [dbo].[TipoDePagos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [NombreTipo] nvarchar(max)  NOT NULL,
    [DescrpcionTipo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reservaciones'
CREATE TABLE [dbo].[Reservaciones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [NombreEstado] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [PK_Clientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetalleFacturas'
ALTER TABLE [dbo].[DetalleFacturas]
ADD CONSTRAINT [PK_DetalleFacturas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoEntregas'
ALTER TABLE [dbo].[TipoEntregas]
ADD CONSTRAINT [PK_TipoEntregas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Articulos'
ALTER TABLE [dbo].[Articulos]
ADD CONSTRAINT [PK_Articulos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categorias'
ALTER TABLE [dbo].[Categorias]
ADD CONSTRAINT [PK_Categorias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [PK_Facturas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoDePagos'
ALTER TABLE [dbo].[TipoDePagos]
ADD CONSTRAINT [PK_TipoDePagos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservaciones'
ALTER TABLE [dbo].[Reservaciones]
ADD CONSTRAINT [PK_Reservaciones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClienteId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_ClienteFactura]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteFactura'
CREATE INDEX [IX_FK_ClienteFactura]
ON [dbo].[Facturas]
    ([ClienteId]);
GO

-- Creating foreign key on [Factura_Id] in table 'DetalleFacturas'
ALTER TABLE [dbo].[DetalleFacturas]
ADD CONSTRAINT [FK_FacturaDetalleFactura]
    FOREIGN KEY ([Factura_Id])
    REFERENCES [dbo].[Facturas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacturaDetalleFactura'
CREATE INDEX [IX_FK_FacturaDetalleFactura]
ON [dbo].[DetalleFacturas]
    ([Factura_Id]);
GO

-- Creating foreign key on [ArticuloId] in table 'DetalleFacturas'
ALTER TABLE [dbo].[DetalleFacturas]
ADD CONSTRAINT [FK_ArticuloDetalleFactura]
    FOREIGN KEY ([ArticuloId])
    REFERENCES [dbo].[Articulos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticuloDetalleFactura'
CREATE INDEX [IX_FK_ArticuloDetalleFactura]
ON [dbo].[DetalleFacturas]
    ([ArticuloId]);
GO

-- Creating foreign key on [TipoEntregaId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_TipoEntregaFactura]
    FOREIGN KEY ([TipoEntregaId])
    REFERENCES [dbo].[TipoEntregas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoEntregaFactura'
CREATE INDEX [IX_FK_TipoEntregaFactura]
ON [dbo].[Facturas]
    ([TipoEntregaId]);
GO

-- Creating foreign key on [CategoriaId] in table 'Articulos'
ALTER TABLE [dbo].[Articulos]
ADD CONSTRAINT [FK_CategoriaArticulo]
    FOREIGN KEY ([CategoriaId])
    REFERENCES [dbo].[Categorias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoriaArticulo'
CREATE INDEX [IX_FK_CategoriaArticulo]
ON [dbo].[Articulos]
    ([CategoriaId]);
GO

-- Creating foreign key on [TipoDePagoId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_TipoDePagoFactura]
    FOREIGN KEY ([TipoDePagoId])
    REFERENCES [dbo].[TipoDePagos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoDePagoFactura'
CREATE INDEX [IX_FK_TipoDePagoFactura]
ON [dbo].[Facturas]
    ([TipoDePagoId]);
GO

-- Creating foreign key on [EstadoFacturaId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_EstadoFacturaFactura]
    FOREIGN KEY ([EstadoFacturaId])
    REFERENCES [dbo].[Reservaciones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EstadoFacturaFactura'
CREATE INDEX [IX_FK_EstadoFacturaFactura]
ON [dbo].[Facturas]
    ([EstadoFacturaId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------