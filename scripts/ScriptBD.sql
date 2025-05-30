USE [master]
GO
/****** Object:  Database [DepositoDental]    Script Date: 30/4/2025 18:48:29 ******/
CREATE DATABASE [DepositoDental]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DepositoDental', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DepositoDental.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DepositoDental_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DepositoDental_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DepositoDental] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DepositoDental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DepositoDental] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DepositoDental] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DepositoDental] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DepositoDental] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DepositoDental] SET ARITHABORT OFF 
GO
ALTER DATABASE [DepositoDental] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DepositoDental] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DepositoDental] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DepositoDental] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DepositoDental] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DepositoDental] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DepositoDental] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DepositoDental] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DepositoDental] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DepositoDental] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DepositoDental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DepositoDental] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DepositoDental] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DepositoDental] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DepositoDental] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DepositoDental] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DepositoDental] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DepositoDental] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DepositoDental] SET  MULTI_USER 
GO
ALTER DATABASE [DepositoDental] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DepositoDental] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DepositoDental] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DepositoDental] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DepositoDental] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DepositoDental] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DepositoDental] SET QUERY_STORE = ON
GO
ALTER DATABASE [DepositoDental] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DepositoDental]
GO
/****** Object:  UserDefinedTableType [dbo].[InsumosTableType]    Script Date: 30/4/2025 18:48:29 ******/
CREATE TYPE [dbo].[InsumosTableType] AS TABLE(
	[InsumoID] [int] NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [decimal](10, 2) NULL
)
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[CategoriaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCategoria] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoriaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ClienteID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [varchar](100) NOT NULL,
	[Telefono] [varchar](20) NULL,
	[CorreoElectronico] [varchar](100) NULL,
	[Direccion] [varchar](255) NULL,
	[LinkDireccion] [varchar](255) NULL,
	[FechaRegistro] [datetime] NULL,
	[EstadoID] [int] NOT NULL,
	[NIT] [varchar](20) NULL,
	[NRC] [varchar](20) NULL,
	[TipoContribuyente] [varchar](50) NULL,
	[Giro] [varchar](100) NULL,
	[TiposDocumentoID] [int] NULL,
	[NumeroDocumento] [varchar](20) NULL,
	[TipoDocumentoID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[CompraID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NOT NULL,
	[FechaCompra] [datetime] NOT NULL,
	[MontoTotal] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompraID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControlVentasMensuales]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlVentasMensuales](
	[ControlID] [int] IDENTITY(1,1) NOT NULL,
	[Mes] [int] NOT NULL,
	[Anio] [int] NOT NULL,
	[TotalVentas] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ControlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[DetalleFacturaID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NOT NULL,
	[InsumoID] [int] NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[Subtotal] [decimal](10, 2) NOT NULL,
	[ImpuestoID] [int] NULL,
	[MontoImpuesto] [decimal](10, 2) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DetalleFacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePedidoProveedor]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedidoProveedor](
	[DetallePedidoID] [int] IDENTITY(1,1) NOT NULL,
	[PedidoID] [int] NULL,
	[InsumoID] [int] NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DetallePedidoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[DetalleVentaID] [int] IDENTITY(1,1) NOT NULL,
	[VentaID] [int] NULL,
	[InsumoID] [int] NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
	[ImpuestoID] [int] NULL,
	[Subtotal] [decimal](10, 2) NULL,
	[MontoImpuesto] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[DetalleVentaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[EstadoID] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[FacturaID] [int] IDENTITY(1,1) NOT NULL,
	[VentaID] [int] NULL,
	[FechaFactura] [datetime] NULL,
	[TotalFactura] [decimal](10, 2) NOT NULL,
	[NumeroFactura] [varchar](50) NOT NULL,
	[TipoPagoID] [int] NULL,
	[EstadoID] [int] NULL,
	[CodigoGeneracion] [uniqueidentifier] NULL,
	[TipoDte] [varchar](2) NULL,
	[SelloRecibido] [varchar](500) NULL,
	[NumeroControl] [varchar](50) NULL,
	[EstadoDte] [varchar](20) NULL,
	[JsonDte] [nvarchar](max) NULL,
	[PdfBase64] [varchar](max) NULL,
	[ClienteId] [int] NULL,
	[CodigoControl] [varchar](100) NULL,
	[FechaHoraCertificacion] [datetime] NULL,
	[CAE] [varchar](100) NULL,
	[FechaVencimientoCAE] [date] NULL,
	[Observaciones] [varchar](500) NULL,
	[TipoDocumentoCliente] [int] NULL,
	[NumeroDocumentoCliente] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[FacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaSincronizacion]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaSincronizacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FacturaId] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[UltimoIntento] [datetime] NULL,
	[IntentosRealizados] [int] NOT NULL,
	[EstadoSincronizacion] [varchar](20) NOT NULL,
	[MensajeError] [varchar](500) NULL,
 CONSTRAINT [PK_FacturaSincronizacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Impuestos]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impuestos](
	[ImpuestoID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](10) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Porcentaje] [decimal](5, 2) NOT NULL,
	[Descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ImpuestoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsumosDentales]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsumosDentales](
	[InsumoID] [int] IDENTITY(1,1) NOT NULL,
	[NombreInsumo] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[CantidadEnStock] [int] NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[FechaVencimiento] [date] NULL,
	[ProveedorID] [int] NULL,
	[CategoriaID] [int] NULL,
	[CodigoBarras] [varchar](50) NULL,
	[TieneImpuesto] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InsumoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InsumosDesactivados]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InsumosDesactivados](
	[InsumoID] [int] NOT NULL,
	[MotivoDesactivacion] [varchar](255) NULL,
	[FechaDesactivacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InsumoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosProveedores]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosProveedores](
	[PedidoID] [int] IDENTITY(1,1) NOT NULL,
	[ProveedorID] [int] NULL,
	[FechaPedido] [datetime] NULL,
	[FechaEntrega] [datetime] NULL,
	[TotalPedido] [decimal](10, 2) NOT NULL,
	[UsuarioID] [int] NULL,
	[EstadoID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PedidoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[ProveedorID] [int] IDENTITY(1,1) NOT NULL,
	[NombreProveedor] [varchar](100) NOT NULL,
	[Contacto] [varchar](100) NULL,
	[Telefono] [varchar](20) NULL,
	[CorreoElectronico] [varchar](100) NULL,
	[Direccion] [varchar](255) NULL,
	[FechaRegistro] [datetime] NULL,
	[EstadoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProveedorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeriesFacturacion]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeriesFacturacion](
	[SerieId] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](2) NOT NULL,
	[Serie] [varchar](10) NOT NULL,
	[NumeroActual] [int] NOT NULL,
	[Activa] [bit] NOT NULL,
 CONSTRAINT [PK_SeriesFacturacion] PRIMARY KEY CLUSTERED 
(
	[SerieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposDocumento]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposDocumento](
	[TipoDocumentoID] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](5) NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoDocumentoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposPago]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposPago](
	[TipoPagoID] [int] IDENTITY(1,1) NOT NULL,
	[NombreTipoPago] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoPagoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[CorreoElectronico] [varchar](100) NOT NULL,
	[Contrasena] [varchar](255) NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[RolID] [int] NULL,
	[EstadoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[VentaID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NULL,
	[FechaVenta] [datetime] NULL,
	[TotalVenta] [decimal](10, 2) NOT NULL,
	[UsuarioID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VentaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Clientes_CorreoElectronico]    Script Date: 30/4/2025 18:48:29 ******/
CREATE NONCLUSTERED INDEX [IX_Clientes_CorreoElectronico] ON [dbo].[Clientes]
(
	[CorreoElectronico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_InsumosDentales_CodigoBarras]    Script Date: 30/4/2025 18:48:29 ******/
CREATE NONCLUSTERED INDEX [IX_InsumosDentales_CodigoBarras] ON [dbo].[InsumosDentales]
(
	[CodigoBarras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_InsumosDentales_NombreInsumo]    Script Date: 30/4/2025 18:48:29 ******/
CREATE NONCLUSTERED INDEX [IX_InsumosDentales_NombreInsumo] ON [dbo].[InsumosDentales]
(
	[NombreInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Ventas_FechaVenta]    Script Date: 30/4/2025 18:48:29 ******/
CREATE NONCLUSTERED INDEX [IX_Ventas_FechaVenta] ON [dbo].[Ventas]
(
	[FechaVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ((1)) FOR [EstadoID]
GO
ALTER TABLE [dbo].[Compras] ADD  DEFAULT (getdate()) FOR [FechaCompra]
GO
ALTER TABLE [dbo].[Facturas] ADD  DEFAULT (getdate()) FOR [FechaFactura]
GO
ALTER TABLE [dbo].[FacturaSincronizacion] ADD  DEFAULT ((0)) FOR [IntentosRealizados]
GO
ALTER TABLE [dbo].[InsumosDentales] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[InsumosDentales] ADD  DEFAULT ((0)) FOR [TieneImpuesto]
GO
ALTER TABLE [dbo].[InsumosDentales] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[InsumosDesactivados] ADD  DEFAULT (getdate()) FOR [FechaDesactivacion]
GO
ALTER TABLE [dbo].[PedidosProveedores] ADD  DEFAULT (getdate()) FOR [FechaPedido]
GO
ALTER TABLE [dbo].[Proveedores] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Proveedores] ADD  CONSTRAINT [DF_Proveedores_EstadoID]  DEFAULT ((1)) FOR [EstadoID]
GO
ALTER TABLE [dbo].[SeriesFacturacion] ADD  DEFAULT ((1)) FOR [Activa]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_EstadoID]  DEFAULT ((1)) FOR [EstadoID]
GO
ALTER TABLE [dbo].[Ventas] ADD  DEFAULT (getdate()) FOR [FechaVenta]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Estados] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Estados]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_TiposDocumento] FOREIGN KEY([TiposDocumentoID])
REFERENCES [dbo].[TiposDocumento] ([TipoDocumentoID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_TiposDocumento]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Facturas] FOREIGN KEY([FacturaID])
REFERENCES [dbo].[Facturas] ([FacturaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_Facturas]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Impuestos] FOREIGN KEY([ImpuestoID])
REFERENCES [dbo].[Impuestos] ([ImpuestoID])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_Impuestos]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_InsumosDentales] FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_InsumosDentales]
GO
ALTER TABLE [dbo].[DetallePedidoProveedor]  WITH CHECK ADD FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[DetallePedidoProveedor]  WITH CHECK ADD FOREIGN KEY([PedidoID])
REFERENCES [dbo].[PedidosProveedores] ([PedidoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetallePedidoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidoProveedor_InsumosDentales] FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[DetallePedidoProveedor] CHECK CONSTRAINT [FK_DetallePedidoProveedor_InsumosDentales]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([VentaID])
REFERENCES [dbo].[Ventas] ([VentaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_Impuestos] FOREIGN KEY([ImpuestoID])
REFERENCES [dbo].[Impuestos] ([ImpuestoID])
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_Impuestos]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_InsumosDentales] FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_InsumosDentales]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD FOREIGN KEY([VentaID])
REFERENCES [dbo].[Ventas] ([VentaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Estados] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Estados]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_TiposDocumento] FOREIGN KEY([TipoDocumentoCliente])
REFERENCES [dbo].[TiposDocumento] ([TipoDocumentoID])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_TiposDocumento]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_TiposPago] FOREIGN KEY([TipoPagoID])
REFERENCES [dbo].[TiposPago] ([TipoPagoID])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_TiposPago]
GO
ALTER TABLE [dbo].[FacturaSincronizacion]  WITH CHECK ADD  CONSTRAINT [FK_FacturaSincronizacion_Factura] FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Facturas] ([FacturaID])
GO
ALTER TABLE [dbo].[FacturaSincronizacion] CHECK CONSTRAINT [FK_FacturaSincronizacion_Factura]
GO
ALTER TABLE [dbo].[InsumosDentales]  WITH CHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[InsumosDentales]  WITH CHECK ADD  CONSTRAINT [FK_InsumosDentales_Categorias] FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categorias] ([CategoriaID])
GO
ALTER TABLE [dbo].[InsumosDentales] CHECK CONSTRAINT [FK_InsumosDentales_Categorias]
GO
ALTER TABLE [dbo].[InsumosDesactivados]  WITH CHECK ADD FOREIGN KEY([InsumoID])
REFERENCES [dbo].[InsumosDentales] ([InsumoID])
GO
ALTER TABLE [dbo].[PedidosProveedores]  WITH CHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([ProveedorID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PedidosProveedores]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PedidosProveedores]  WITH CHECK ADD  CONSTRAINT [FK_PedidosProveedores_Estados] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[PedidosProveedores] CHECK CONSTRAINT [FK_PedidosProveedores_Estados]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Estados] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Estados]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Estados] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Estados]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE SET NULL
GO
/****** Object:  StoredProcedure [dbo].[AgregarProducto]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarProducto]
    @NombreInsumo VARCHAR(100),
    @Descripcion VARCHAR(500) = NULL,
    @PrecioUnitario DECIMAL(10, 2),
    @CantidadEnStock INT,
    @FechaVencimiento DATE = NULL,
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CodigoBarras VARCHAR(50) = NULL,
    @TieneImpuesto BIT = 0
AS
BEGIN
    INSERT INTO [dbo].[InsumosDentales] (
        [NombreInsumo],
        [Descripcion],
        [PrecioUnitario],
        [CantidadEnStock],
        [FechaRegistro],
        [FechaVencimiento],
        [ProveedorID],
        [CategoriaID],
        [CodigoBarras],
        [TieneImpuesto]
    )
    VALUES (
        @NombreInsumo,
        @Descripcion,
        @PrecioUnitario,
        @CantidadEnStock,
        GETDATE(),
        @FechaVencimiento,
        @ProveedorID,
        @CategoriaID,
        @CodigoBarras,
        @TieneImpuesto
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[CancelarVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CancelarVenta]
    @VentaID INT
AS
BEGIN
    -- Revertir la cantidad de insumos vendidos
    DECLARE @InsumoID INT, @Cantidad INT;

    DECLARE cur CURSOR FOR
    SELECT InsumoID, Cantidad
    FROM DetalleVenta
    WHERE VentaID = @VentaID;

    OPEN cur;
    FETCH NEXT FROM cur INTO @InsumoID, @Cantidad;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE InsumosDentales
        SET CantidadEnStock = CantidadEnStock + @Cantidad
        WHERE InsumoID = @InsumoID;

        FETCH NEXT FROM cur INTO @InsumoID, @Cantidad;
    END;

    CLOSE cur;
    DEALLOCATE cur;

    -- Eliminar los detalles de la venta
    DELETE FROM DetalleVenta
    WHERE VentaID = @VentaID;

    -- Eliminar la venta
    DELETE FROM Ventas
    WHERE VentaID = @VentaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[DesactivarCliente]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DesactivarCliente]
    @ClienteID INT,
    @MotivoInactivacion VARCHAR(255)
AS
BEGIN
    -- Verificar si el cliente existe y está activo
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Clientes] WHERE [ClienteID] = @ClienteID AND [EstadoID] = 1)
    BEGIN
        RAISERROR ('El cliente no existe o ya está inactivo.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    
    -- Actualizar estado a inactivo (EstadoID = 2)
    UPDATE [dbo].[Clientes]
    SET [EstadoID] = 2 -- Estado "Inactivo"
    WHERE [ClienteID] = @ClienteID;

    COMMIT TRANSACTION;
END;
GO
/****** Object:  StoredProcedure [dbo].[EditarCliente]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditarCliente]
    @ClienteID INT,
    @NombreCompleto VARCHAR(100),
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @Direccion VARCHAR(255) = NULL,
    @LinkDireccion VARCHAR(255) = NULL,
    @EstadoID INT = NULL
AS
BEGIN
    UPDATE [dbo].[Clientes]
    SET
        [NombreCompleto] = @NombreCompleto,
        [Telefono] = @Telefono,
        [CorreoElectronico] = @CorreoElectronico,
        [Direccion] = @Direccion,
        [LinkDireccion] = @LinkDireccion,
        [EstadoID] = ISNULL(@EstadoID, [EstadoID])
    WHERE
        [ClienteID] = @ClienteID;

    -- Verificar si se actualizó algún registro
    IF @@ROWCOUNT = 0
        RAISERROR ('No se encontró el cliente con el ID especificado.', 16, 1);
END;
GO
/****** Object:  StoredProcedure [dbo].[EditarProducto]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Modificar SP EditarProducto para evitar editar productos inactivos
CREATE PROCEDURE [dbo].[EditarProducto]
    @InsumoID INT,
    @NombreInsumo VARCHAR(100),
    @Descripcion TEXT = NULL,
    @PrecioUnitario DECIMAL(10, 2),
    @CantidadEnStock INT,
    @FechaVencimiento DATE = NULL,
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL
AS
BEGIN
    -- Verificar si el producto está activo
    IF NOT EXISTS (SELECT 1 FROM [dbo].[InsumosDentales] WHERE [InsumoID] = @InsumoID AND [Estado] = 1)
    BEGIN
        RAISERROR ('El producto no existe o está inactivo.', 16, 1);
        RETURN;
    END

    UPDATE [dbo].[InsumosDentales]
    SET 
        [NombreInsumo] = @NombreInsumo,
        [Descripcion] = @Descripcion,
        [PrecioUnitario] = @PrecioUnitario,
        [CantidadEnStock] = @CantidadEnStock,
        [FechaVencimiento] = @FechaVencimiento,
        [ProveedorID] = @ProveedorID,
        [CategoriaID] = @CategoriaID
    WHERE [InsumoID] = @InsumoID;

    -- Verificar si la actualización fue exitosa
    IF @@ROWCOUNT = 0
        THROW 50001, 'No se encontró el producto con el ID especificado.', 1;
END;

GO
/****** Object:  StoredProcedure [dbo].[EditarVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditarVenta]
    @VentaID INT,
    @ClienteID INT,
    @FechaVenta DATETIME,
    @TotalVenta DECIMAL(10, 2),
    @UsuarioID INT
AS
BEGIN
    UPDATE Ventas
    SET ClienteID = @ClienteID,
        FechaVenta = @FechaVenta,
        TotalVenta = @TotalVenta,
        UsuarioID = @UsuarioID
    WHERE VentaID = @VentaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarCliente]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarCliente]
    @ClienteID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el cliente tiene ventas asociadas
    IF EXISTS (SELECT 1 FROM Ventas WHERE ClienteID = @ClienteID)
    BEGIN
        RAISERROR('No se puede eliminar el cliente porque tiene ventas asociadas.', 16, 1);
        RETURN;
    END

    -- Eliminar el cliente
    DELETE FROM [dbo].[Clientes]
    WHERE ClienteID = @ClienteID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarClienteLogico]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarClienteLogico]
    @ClienteID INT
AS
BEGIN
    UPDATE [dbo].[Clientes]
    SET [EstadoID] = 3 -- Estado "Eliminado"
    WHERE [ClienteID] = @ClienteID;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Modificar SP EliminarProducto para desactivar en lugar de eliminar
CREATE PROCEDURE [dbo].[EliminarProducto]
    @InsumoID INT,
    @MotivoDesactivacion VARCHAR(255) = NULL
AS
BEGIN
    -- Verificar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM [dbo].[InsumosDentales] WHERE [InsumoID] = @InsumoID)
    BEGIN
        RAISERROR ('No se encontró el producto con el ID especificado.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

    -- Desactivar el producto
    UPDATE [dbo].[InsumosDentales]
    SET [Estado] = 0
    WHERE [InsumoID] = @InsumoID;

    -- Registrar en la tabla de insumos desactivados
    INSERT INTO [dbo].[InsumosDesactivados] ([InsumoID], [MotivoDesactivacion])
    VALUES (@InsumoID, @MotivoDesactivacion);

    COMMIT TRANSACTION;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarVenta]
    @VentaID INT
AS
BEGIN
    DELETE FROM Ventas
    WHERE VentaID = @VentaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarCliente]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarCliente]
    @NombreCompleto VARCHAR(100),
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(100) = NULL,
    @Direccion VARCHAR(255) = NULL,
    @LinkDireccion VARCHAR(255) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO [dbo].[Clientes] (
            [NombreCompleto],
            [Telefono],
            [CorreoElectronico],
            [Direccion],
            [LinkDireccion],
            [EstadoID]
        )
        VALUES (
            @NombreCompleto,
            @Telefono,
            @CorreoElectronico,
            @Direccion,
            @LinkDireccion,
            1 -- Estado "Activo"
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarUsuario]
    @NombreUsuario VARCHAR(50),
    @CorreoElectronico VARCHAR(100),
    @Contrasena VARCHAR(255),
    @RolID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el correo ya existe
        IF EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [CorreoElectronico] = @CorreoElectronico)
        BEGIN
            THROW 50001, 'El correo electrónico ya está en uso.', 1;
        END

        -- Insertar el nuevo usuario con la contraseña en texto plano
        -- (La aplicación deberá hashearla con BCrypt antes de enviarla)
        INSERT INTO [dbo].[Usuarios] (
            [NombreUsuario],
            [CorreoElectronico],
            [Contrasena],
            [FechaRegistro],
            [EstadoID],
            [RolID]
        )
        VALUES (
            @NombreUsuario,
            @CorreoElectronico,
            @Contrasena,  -- Ahora recibe el hash BCrypt directamente
            GETDATE(),
            1,
            @RolID
        );
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[MostrarClientes]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarClientes]
AS
BEGIN
    SELECT 
        [ClienteID],
        [NombreCompleto],
        [Telefono],
        [CorreoElectronico],
        [Direccion],
        [LinkDireccion],
        [FechaRegistro],
        e.[NombreEstado] AS EstadoCliente
    FROM [dbo].[Clientes] c
    JOIN [dbo].[Estados] e ON c.EstadoID = e.EstadoID
    ORDER BY [NombreCompleto];
END;
GO
/****** Object:  StoredProcedure [dbo].[MostrarClientesActivos]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarClientesActivos]
AS
BEGIN
    SELECT 
        [ClienteID],
        [NombreCompleto],
        [Telefono],
        [CorreoElectronico],
        [Direccion],
        [LinkDireccion],
        [FechaRegistro]
    FROM [dbo].[Clientes]
    WHERE [EstadoID] = 1 -- Estado "Activo"
    ORDER BY [NombreCompleto];
END;
GO
/****** Object:  StoredProcedure [dbo].[MostrarClientesInactivos]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarClientesInactivos]
AS
BEGIN
    SELECT 
        [ClienteID],
        [NombreCompleto],
        [Telefono],
        [CorreoElectronico],
        [Direccion],
        [LinkDireccion],
        [FechaRegistro]
    FROM [dbo].[Clientes]
    WHERE [EstadoID] = 2 -- Estado "Inactivo"
    ORDER BY [FechaRegistro] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[MostrarDetalleVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Modificar SP MostrarDetalleVenta para evitar mostrar productos inactivos
CREATE PROCEDURE [dbo].[MostrarDetalleVenta]
    @VentaID INT
AS
BEGIN
    SELECT dv.InsumoID, i.NombreInsumo, dv.Cantidad, dv.PrecioUnitario, dv.Total
    FROM DetalleVenta dv
    JOIN InsumosDentales i ON dv.InsumoID = i.InsumoID
    WHERE dv.VentaID = @VentaID AND i.Estado = 1; -- Solo productos activos
END;
GO
/****** Object:  StoredProcedure [dbo].[MostrarProductos]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MostrarProductos]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        i.InsumoID,
        i.NombreInsumo,
        i.Descripcion,
        i.PrecioUnitario,
        i.CantidadEnStock,
        i.FechaRegistro,
        i.FechaVencimiento,
        p.NombreProveedor AS Proveedor,
        c.NombreCategoria AS Categoria,
        i.CodigoBarras,
        i.TieneImpuesto,
        (i.PrecioUnitario * i.CantidadEnStock) AS ValorTotal
    FROM 
        dbo.InsumosDentales i
    LEFT JOIN 
        dbo.Proveedores p ON i.ProveedorID = p.ProveedorID
    LEFT JOIN 
        dbo.Categorias c ON i.CategoriaID = c.CategoriaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para actualizar una factura
CREATE PROCEDURE [dbo].[sp_ActualizarFactura]
    @FacturaID INT,
    @TipoPagoID INT = NULL,
    @EstadoID INT = NULL,
    @EstadoDte VARCHAR(20) = NULL,
    @SelloRecibido VARCHAR(500) = NULL,
    @NumeroControl VARCHAR(50) = NULL,
    @PdfBase64 VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        UPDATE [dbo].[Facturas]
        SET 
            [TipoPagoID] = ISNULL(@TipoPagoID, [TipoPagoID]),
            [EstadoID] = ISNULL(@EstadoID, [EstadoID]),
            [EstadoDte] = ISNULL(@EstadoDte, [EstadoDte]),
            [SelloRecibido] = ISNULL(@SelloRecibido, [SelloRecibido]),
            [NumeroControl] = ISNULL(@NumeroControl, [NumeroControl]),
            [PdfBase64] = ISNULL(@PdfBase64, [PdfBase64])
        WHERE [FacturaID] = @FacturaID;
        
        IF @@ROWCOUNT = 0
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('La factura con ID %d no existe.', 16, 1, @FacturaID);
            RETURN;
        END
        
        COMMIT TRANSACTION;
        
        -- Devolver los datos actualizados de la factura
        SELECT * FROM [dbo].[Facturas] WHERE [FacturaID] = @FacturaID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AnularFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para anular una factura
CREATE PROCEDURE [dbo].[sp_AnularFactura]
    @FacturaID INT,
    @MotivoAnulacion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar si la factura existe y no está anulada
        DECLARE @EstadoActual INT;
        SELECT @EstadoActual = [EstadoID] FROM [dbo].[Facturas] WHERE [FacturaID] = @FacturaID;
        
        IF @EstadoActual IS NULL
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('La factura con ID %d no existe.', 16, 1, @FacturaID);
            RETURN;
        END
        
        IF @EstadoActual = 3 -- Suponiendo que 3 es el ID para estado "Anulado"
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('La factura con ID %d ya está anulada.', 16, 1, @FacturaID);
            RETURN;
        END
        
        -- Actualizar el estado de la factura a anulado
        UPDATE [dbo].[Facturas]
        SET 
            [EstadoID] = 3, -- ID para estado "Anulado"
            [EstadoDte] = 'ANULADO'
            -- Aquí podrías agregar un campo para guardar el motivo de anulación si lo agregas a la tabla
        WHERE [FacturaID] = @FacturaID;
        
        -- Si tienes una tabla de log de anulaciones, podrías insertarla aquí
        -- INSERT INTO [dbo].[LogAnulacionesFactura] ([FacturaID], [MotivoAnulacion], [FechaAnulacion], [UsuarioID])
        -- VALUES (@FacturaID, @MotivoAnulacion, GETDATE(), @UsuarioID);
        
        COMMIT TRANSACTION;
        
        -- Devolver el resultado de la operación
        SELECT 'Factura anulada correctamente.' AS Resultado;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EstadisticasFacturas]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para obtener conteo y totales de facturas por estado
CREATE PROCEDURE [dbo].[sp_EstadisticasFacturas]
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Conteo de facturas por estado
    SELECT 
        E.[NombreEstado] AS Estado,
        COUNT(F.[FacturaID]) AS CantidadFacturas,
        SUM(F.[TotalFactura]) AS MontoTotal
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[Estados] E ON F.[EstadoID] = E.[EstadoID]
    WHERE 
        CAST(F.[FechaFactura] AS DATE) BETWEEN @FechaInicio AND @FechaFin
    GROUP BY 
        E.[NombreEstado]
    ORDER BY 
        COUNT(F.[FacturaID]) DESC;
        
    -- Conteo de facturas por tipo de documento
    SELECT 
        F.[TipoDte],
        COUNT(F.[FacturaID]) AS CantidadFacturas,
        SUM(F.[TotalFactura]) AS MontoTotal
    FROM 
        [dbo].[Facturas] F
    WHERE 
        CAST(F.[FechaFactura] AS DATE) BETWEEN @FechaInicio AND @FechaFin
    GROUP BY 
        F.[TipoDte]
    ORDER BY 
        COUNT(F.[FacturaID]) DESC;
        
    -- Conteo de facturas por forma de pago
    SELECT 
        TP.[NombreTipoPago] AS FormaPago,
        COUNT(F.[FacturaID]) AS CantidadFacturas,
        SUM(F.[TotalFactura]) AS MontoTotal
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[TiposPago] TP ON F.[TipoPagoID] = TP.[TipoPagoID]
    WHERE 
        CAST(F.[FechaFactura] AS DATE) BETWEEN @FechaInicio AND @FechaFin
    GROUP BY 
        TP.[NombreTipoPago]
    ORDER BY 
        COUNT(F.[FacturaID]) DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarFactura]
    @VentaID INT = NULL,
    @ClienteID INT,
    @TotalFactura DECIMAL(10, 2),
    @TipoPagoID INT,
    @TipoDte VARCHAR(2),
    @JsonDte NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        -- Obtener información del cliente
        DECLARE @TipoDocumentoCliente VARCHAR(2);
        DECLARE @NumeroDocumentoCliente VARCHAR(20);
        DECLARE @NITCliente VARCHAR(20);
        DECLARE @NRCCliente VARCHAR(20);
        
        SELECT 
            @TipoDocumentoCliente = TiposDocumentoID,
            @NumeroDocumentoCliente = NumeroDocumento,
            @NITCliente = NIT,
            @NRCCliente = NRC
        FROM dbo.Clientes 
        WHERE ClienteID = @ClienteID;
        
        IF @TipoDocumentoCliente IS NULL
            SET @TipoDocumentoCliente = 'CF'; -- Consumidor Final por defecto

        BEGIN TRANSACTION;

        -- Declarar variables
        DECLARE @NumeroFactura VARCHAR(50);
        DECLARE @EstadoID INT = 1; -- Estado Activo por defecto
        DECLARE @CodigoGeneracion UNIQUEIDENTIFIER = NEWID();
        DECLARE @FacturaID INT;
        DECLARE @TotalImpuestos DECIMAL(10, 2) = 0;
        DECLARE @Subtotal DECIMAL(10, 2) = 0;

        -- Calcular subtotal e impuestos si hay venta asociada
        IF @VentaID IS NOT NULL
        BEGIN
            SELECT 
                @Subtotal = SUM(Subtotal),
                @TotalImpuestos = SUM(MontoImpuesto)
            FROM dbo.DetalleVenta
            WHERE VentaID = @VentaID;
        END
        ELSE
        BEGIN
            SET @Subtotal = @TotalFactura / 1.13; -- Asumir que incluye IVA si no hay venta asociada
            SET @TotalImpuestos = @TotalFactura - @Subtotal;
        END

        -- Obtener número de factura de la serie activa
        EXEC dbo.sp_ObtenerNumeroFactura @TipoDte, @NumeroFactura OUTPUT;

        -- Insertar la factura
        INSERT INTO dbo.Facturas (
            VentaID,
            ClienteID,
            FechaFactura,
            TotalFactura,
            NumeroFactura,
            TipoPagoID,
            EstadoID,
            CodigoGeneracion,
            TipoDte,
            EstadoDte,
            JsonDte,
            TipoDocumentoCliente,
            NumeroDocumentoCliente
            
        )
        VALUES (
            @VentaID,
            @ClienteID,
            GETDATE(),
            @TotalFactura,
            @NumeroFactura,
            @TipoPagoID,
            @EstadoID,
            @CodigoGeneracion,
            @TipoDte,
            'EMITIDO',
            @JsonDte,
            @TipoDocumentoCliente,
            @NumeroDocumentoCliente
            
        );

        -- Obtener el ID de la factura insertada
        SET @FacturaID = SCOPE_IDENTITY();

        -- Si se proporcionó un ID de venta, insertar detalles en DetalleFactura
        IF @VentaID IS NOT NULL
        BEGIN
            INSERT INTO dbo.DetalleFactura (
                FacturaID,
                InsumoID,
                Cantidad,
                PrecioUnitario,
                Subtotal,
                ImpuestoID,
                MontoImpuesto,
                Total
            )
            SELECT
                @FacturaID,
                InsumoID,
                Cantidad,
                PrecioUnitario,
                Subtotal,
                ImpuestoID,
                MontoImpuesto,
                Total
            FROM dbo.DetalleVenta
            WHERE VentaID = @VentaID;
        END

        COMMIT TRANSACTION;

        -- Devolver el ID de la factura creada con detalles
        SELECT 
            @FacturaID AS FacturaID, 
            @NumeroFactura AS NumeroFactura, 
            @CodigoGeneracion AS CodigoGeneracion,
            @Subtotal AS Subtotal,
            @TotalImpuestos AS TotalImpuestos,
            @TotalFactura AS Total;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarVenta]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarVenta]
    @ClienteID int,
    @FechaVenta datetime,
    @Insumos InsumosTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validación de datos
    IF @ClienteID IS NULL OR @ClienteID = 0
        RAISERROR('El ID del cliente no puede ser nulo o cero.', 16, 1);

    BEGIN TRY
        -- Iniciar transacción
        BEGIN TRANSACTION;

        -- Validar stock disponible
        IF EXISTS (
            SELECT 1
            FROM @Insumos i
            INNER JOIN InsumosDentales id ON id.InsumoID = i.InsumoID
            WHERE id.CantidadEnStock < i.Cantidad OR id.CantidadEnStock = 0
        )
        BEGIN
            -- Crear tabla temporal para almacenar los productos sin stock suficiente
            DECLARE @ProductosSinStock TABLE (
                NombreInsumo varchar(100),
                StockDisponible int,
                CantidadSolicitada int
            );

            -- Insertar en la tabla temporal los productos sin stock suficiente
            INSERT INTO @ProductosSinStock
            SELECT 
                id.NombreInsumo,
                id.CantidadEnStock,
                i.Cantidad
            FROM @Insumos i
            INNER JOIN InsumosDentales id ON id.InsumoID = i.InsumoID
            WHERE id.CantidadEnStock < i.Cantidad OR id.CantidadEnStock = 0;

            -- Crear mensaje de error detallado
            DECLARE @ErrorMessage nvarchar(4000) = 'Stock insuficiente para los siguientes productos:' + CHAR(13);
            
            SELECT @ErrorMessage = @ErrorMessage + 
                'Producto: ' + NombreInsumo + 
                ', Stock disponible: ' + CAST(StockDisponible AS varchar) + 
                ', Cantidad solicitada: ' + CAST(CantidadSolicitada AS varchar) + CHAR(13)
            FROM @ProductosSinStock;

            ROLLBACK TRANSACTION;
            THROW 51000, @ErrorMessage, 1;
        END

        DECLARE @VentaID int;
        DECLARE @TotalVenta decimal(10, 2);
        DECLARE @TotalImpuestos decimal(10, 2) = 0;

        -- Insertar registro en la tabla Ventas
        INSERT INTO Ventas (ClienteID, FechaVenta, TotalVenta)
        VALUES (@ClienteID, @FechaVenta, 0);

        -- Obtener el ID de la venta recién insertada
        SET @VentaID = SCOPE_IDENTITY();

        -- Insertar registros en la tabla DetalleVenta con cálculo de impuestos
        INSERT INTO DetalleVenta (VentaID, InsumoID, Cantidad, PrecioUnitario, Subtotal, ImpuestoID, MontoImpuesto, Total)
        SELECT 
            @VentaID, 
            i.InsumoID, 
            i.Cantidad, 
            i.PrecioUnitario,
            i.Cantidad * i.PrecioUnitario AS Subtotal,
            CASE WHEN id.TieneImpuesto = 1 THEN 1 ELSE 2 END AS ImpuestoID, -- 1=IVA, 2=Exento
            CASE WHEN id.TieneImpuesto = 1 THEN (i.Cantidad * i.PrecioUnitario * 0.13) ELSE 0 END AS MontoImpuesto,
            CASE WHEN id.TieneImpuesto = 1 THEN (i.Cantidad * i.PrecioUnitario * 1.13) ELSE (i.Cantidad * i.PrecioUnitario) END AS Total
        FROM @Insumos i
        INNER JOIN InsumosDentales id ON id.InsumoID = i.InsumoID;

        -- Calcular total de impuestos
        SELECT @TotalImpuestos = SUM(MontoImpuesto) FROM DetalleVenta WHERE VentaID = @VentaID;

        -- Actualizar el total de la venta en la tabla Ventas
        UPDATE v
        SET v.TotalVenta = (SELECT SUM(dv.Total) FROM DetalleVenta dv WHERE dv.VentaID = v.VentaID)
        FROM Ventas v
        WHERE v.VentaID = @VentaID;

        -- Actualizar el stock de los insumos
        UPDATE id
        SET id.CantidadEnStock = id.CantidadEnStock - i.Cantidad
        FROM InsumosDentales id
        INNER JOIN @Insumos i ON id.InsumoID = i.InsumoID;

        -- Verificar productos con stock bajo después de la actualización
        DECLARE @ProductosBajoStock TABLE (
            NombreInsumo varchar(100),
            StockActual int
        );

        INSERT INTO @ProductosBajoStock
        SELECT 
            NombreInsumo,
            CantidadEnStock
        FROM InsumosDentales
        WHERE CantidadEnStock <= 5;

        -- Si hay productos con stock bajo, devolver la información
        IF EXISTS (SELECT 1 FROM @ProductosBajoStock)
        BEGIN
            SELECT 'StockBajo' as Tipo, NombreInsumo, StockActual
            FROM @ProductosBajoStock;
        END

        -- Devolver información de la venta creada
        SELECT 
            @VentaID AS VentaID,
            v.TotalVenta,
            @TotalImpuestos AS TotalImpuestos,
            v.TotalVenta - @TotalImpuestos AS Subtotal
        FROM Ventas v
        WHERE v.VentaID = @VentaID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMsg nvarchar(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int = ERROR_SEVERITY();
        DECLARE @ErrorState int = ERROR_STATE();

        RAISERROR(@ErrorMsg, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_MarcarFacturaPagada]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para marcar factura como pagada
CREATE PROCEDURE [dbo].[sp_MarcarFacturaPagada]
    @FacturaID INT,
    @TipoPagoID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar si la factura existe y no está anulada
        DECLARE @EstadoActual INT;
        SELECT @EstadoActual = [EstadoID] FROM [dbo].[Facturas] WHERE [FacturaID] = @FacturaID;
        
        IF @EstadoActual IS NULL
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('La factura con ID %d no existe.', 16, 1, @FacturaID);
            RETURN;
        END
        
        IF @EstadoActual = 3 -- Suponiendo que 3 es el ID para estado "Anulado"
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('No se puede marcar como pagada una factura anulada.', 16, 1);
            RETURN;
        END
        
        -- Actualizar el estado de la factura a pagado
        UPDATE [dbo].[Facturas]
        SET 
            [EstadoID] = 2, -- Suponiendo que 2 es el ID para estado "Pagado"
            [TipoPagoID] = @TipoPagoID
        WHERE [FacturaID] = @FacturaID;
        
        COMMIT TRANSACTION;
        
        -- Devolver el resultado de la operación
        SELECT 'Factura marcada como pagada correctamente.' AS Resultado;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_MostrarVentas]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MostrarVentas]
AS
BEGIN
    SELECT 
        V.VentaID,
        V.ClienteID,
        C.NombreCompleto AS ClienteNombre,
        V.FechaVenta,
        V.TotalVenta,
        V.UsuarioID
    FROM 
        Ventas V
    INNER JOIN 
        Clientes C ON V.ClienteID = C.ClienteID;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerDetalleFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para obtener detalle de una factura
CREATE PROCEDURE [dbo].[sp_ObtenerDetalleFactura]
    @FacturaID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Información de la factura
    SELECT 
        F.[FacturaID],
        F.[VentaID],
        F.[FechaFactura],
        F.[TotalFactura],
        F.[NumeroFactura],
        TP.[NombreTipoPago],
        E.[NombreEstado],
        F.[CodigoGeneracion],
        F.[TipoDte],
        F.[EstadoDte],
        C.[NombreCompleto] AS Cliente,
        C.[CorreoElectronico],
        C.[Direccion],
        C.[Telefono]
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[Clientes] C ON F.[ClienteID] = C.[ClienteID]
    INNER JOIN 
        [dbo].[TiposPago] TP ON F.[TipoPagoID] = TP.[TipoPagoID]
    INNER JOIN 
        [dbo].[Estados] E ON F.[EstadoID] = E.[EstadoID]
    WHERE 
        F.[FacturaID] = @FacturaID;
    
    -- Detalle de productos vendidos (si la factura está asociada a una venta)
    IF EXISTS (SELECT 1 FROM [dbo].[Facturas] WHERE [FacturaID] = @FacturaID AND [VentaID] IS NOT NULL)
    BEGIN
        SELECT 
            DV.[InsumoID],
            I.[NombreInsumo],
            DV.[Cantidad],
            DV.[PrecioUnitario],
            DV.[Total],
            I.[TieneImpuesto]
        FROM 
            [dbo].[Facturas] F
        INNER JOIN 
            [dbo].[DetalleVenta] DV ON F.[VentaID] = DV.[VentaID]
        INNER JOIN 
            [dbo].[InsumosDentales] I ON DV.[InsumoID] = I.[InsumoID]
        WHERE 
            F.[FacturaID] = @FacturaID
        ORDER BY 
            I.[NombreInsumo];
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerFacturasPorCliente]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para obtener facturas por cliente
CREATE PROCEDURE [dbo].[sp_ObtenerFacturasPorCliente]
    @ClienteID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        F.[FacturaID],
        F.[VentaID],
        F.[FechaFactura],
        F.[TotalFactura],
        F.[NumeroFactura],
        TP.[NombreTipoPago],
        E.[NombreEstado],
        F.[CodigoGeneracion],
        F.[TipoDte],
        F.[EstadoDte],
        C.[NombreCompleto] AS Cliente
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[Clientes] C ON F.[ClienteID] = C.[ClienteID]
    INNER JOIN 
        [dbo].[TiposPago] TP ON F.[TipoPagoID] = TP.[TipoPagoID]
    INNER JOIN 
        [dbo].[Estados] E ON F.[EstadoID] = E.[EstadoID]
    WHERE 
        F.[ClienteID] = @ClienteID
    ORDER BY 
        F.[FechaFactura] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerFacturasPorPeriodo]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para obtener facturas por período
CREATE PROCEDURE [dbo].[sp_ObtenerFacturasPorPeriodo]
    @FechaInicio DATE,
    @FechaFin DATE,
    @EstadoID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        F.[FacturaID],
        F.[VentaID],
        F.[ClienteID],
        C.[NombreCompleto] AS Cliente,
        F.[FechaFactura],
        F.[TotalFactura],
        F.[NumeroFactura],
        TP.[NombreTipoPago],
        E.[NombreEstado],
        F.[TipoDte],
        F.[EstadoDte]
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[Clientes] C ON F.[ClienteID] = C.[ClienteID]
    INNER JOIN 
        [dbo].[TiposPago] TP ON F.[TipoPagoID] = TP.[TipoPagoID]
    INNER JOIN 
        [dbo].[Estados] E ON F.[EstadoID] = E.[EstadoID]
    WHERE 
        CAST(F.[FechaFactura] AS DATE) BETWEEN @FechaInicio AND @FechaFin
        AND (@EstadoID IS NULL OR F.[EstadoID] = @EstadoID)
    ORDER BY 
        F.[FechaFactura] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerNumeroFactura]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para generar número de factura
CREATE PROCEDURE [dbo].[sp_ObtenerNumeroFactura]
    @TipoDocumento VARCHAR(2)
AS
BEGIN
    DECLARE @NumeroFactura VARCHAR(20)
    DECLARE @Serie VARCHAR(10)
    DECLARE @Numero INT
    
    BEGIN TRANSACTION
        SELECT TOP 1 @Serie = Serie, @Numero = NumeroActual 
        FROM SeriesFacturacion 
        WHERE TipoDocumento = @TipoDocumento AND Activa = 1
        
        UPDATE SeriesFacturacion 
        SET NumeroActual = NumeroActual + 1 
        WHERE Serie = @Serie
        
        SET @NumeroFactura = @Serie + '-' + RIGHT('00000000' + CAST(@Numero AS VARCHAR(8)), 8)
    COMMIT
    
    SELECT @NumeroFactura AS NumeroFactura
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteFacturas]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para generar reporte de facturas
CREATE PROCEDURE [dbo].[sp_ReporteFacturas]
    @FechaInicio DATE,
    @FechaFin DATE,
    @TipoDte VARCHAR(2) = NULL,
    @EstadoID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        F.[FacturaID],
        F.[NumeroFactura],
        F.[FechaFactura],
        C.[NombreCompleto] AS Cliente,
        F.[TotalFactura],
        TP.[NombreTipoPago] AS FormaPago,
        E.[NombreEstado] AS Estado,
        F.[TipoDte],
        F.[EstadoDte],
        F.[CodigoGeneracion]
    FROM 
        [dbo].[Facturas] F
    INNER JOIN 
        [dbo].[Clientes] C ON F.[ClienteID] = C.[ClienteID]
    INNER JOIN 
        [dbo].[TiposPago] TP ON F.[TipoPagoID] = TP.[TipoPagoID]
    INNER JOIN 
        [dbo].[Estados] E ON F.[EstadoID] = E.[EstadoID]
    WHERE 
        CAST(F.[FechaFactura] AS DATE) BETWEEN @FechaInicio AND @FechaFin
        AND (@TipoDte IS NULL OR F.[TipoDte] = @TipoDte)
        AND (@EstadoID IS NULL OR F.[EstadoID] = @EstadoID)
    ORDER BY 
        F.[FechaFactura] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[ValidarUsuario]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidarUsuario]
    @NombreUsuario VARCHAR(100),
    @Contrasena VARCHAR(100)
AS
BEGIN
    SELECT 
        u.[UsuarioID],
        u.[NombreUsuario],
        u.[CorreoElectronico],
        u.[RolID],
        r.[NombreRol] AS Rol,
        e.[NombreEstado] AS Estado
    FROM [dbo].[Usuarios] u
    JOIN [dbo].[Roles] r ON u.RolID = r.RolID
    JOIN [dbo].[Estados] e ON u.EstadoID = e.EstadoID
    WHERE u.[NombreUsuario] = @NombreUsuario
    AND u.[Contrasena] = @Contrasena
    AND u.EstadoID = 1; -- Solo usuarios activos
END;
GO
/****** Object:  StoredProcedure [dbo].[VentasFrecuentes]    Script Date: 30/4/2025 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VentasFrecuentes]
    @ClienteID INT
AS
BEGIN
    SELECT COUNT(*) AS NumeroDeVentas
    FROM Ventas
    WHERE ClienteID = @ClienteID;
END;
GO
USE [master]
GO
ALTER DATABASE [DepositoDental] SET  READ_WRITE 
GO
