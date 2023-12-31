USE [master]
GO
/****** Object:  Database [db_sistema_venta]    Script Date: 12/8/2023 22:15:34 ******/
CREATE DATABASE [db_sistema_venta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_sistema_venta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\db_sistema_venta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_sistema_venta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\db_sistema_venta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db_sistema_venta] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_sistema_venta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_sistema_venta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_sistema_venta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_sistema_venta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_sistema_venta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_sistema_venta] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_sistema_venta] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_sistema_venta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_sistema_venta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_sistema_venta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_sistema_venta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_sistema_venta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_sistema_venta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_sistema_venta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_sistema_venta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_sistema_venta] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db_sistema_venta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_sistema_venta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_sistema_venta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_sistema_venta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_sistema_venta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_sistema_venta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_sistema_venta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_sistema_venta] SET RECOVERY FULL 
GO
ALTER DATABASE [db_sistema_venta] SET  MULTI_USER 
GO
ALTER DATABASE [db_sistema_venta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_sistema_venta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_sistema_venta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_sistema_venta] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_sistema_venta] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_sistema_venta] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_sistema_venta', N'ON'
GO
ALTER DATABASE [db_sistema_venta] SET QUERY_STORE = ON
GO
ALTER DATABASE [db_sistema_venta] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db_sistema_venta]
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[idcategoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[estado] [bit] NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[idcliente] [int] IDENTITY(1,1) NOT NULL,
	[documento] [varchar](50) NULL,
	[nombrecompleto] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[estado] [bit] NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idcliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[compra]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compra](
	[idcompra] [int] IDENTITY(1,1) NOT NULL,
	[idusuario] [int] NULL,
	[idproveedor] [int] NULL,
	[tipodocumento] [varchar](50) NULL,
	[numerodocumento] [varchar](50) NULL,
	[montototal] [decimal](10, 2) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idcompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_compra]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_compra](
	[iddetallecompra] [int] IDENTITY(1,1) NOT NULL,
	[idcompra] [int] NULL,
	[idproducto] [int] NULL,
	[preciocompra] [decimal](10, 2) NULL,
	[precioventa] [decimal](10, 2) NULL,
	[cantidad] [int] NULL,
	[montototal] [decimal](10, 2) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[iddetallecompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[iddetalleventa] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NULL,
	[idproducto] [int] NULL,
	[precioventa] [decimal](10, 2) NULL,
	[cantidad] [int] NULL,
	[subtotal] [decimal](10, 2) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[iddetalleventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permiso]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permiso](
	[idpermiso] [int] IDENTITY(1,1) NOT NULL,
	[idrol] [int] NULL,
	[nombremenu] [varchar](50) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idpermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[idproducto] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](50) NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
	[idcategoria] [int] NULL,
	[stock] [int] NOT NULL,
	[preciocompra] [decimal](10, 2) NULL,
	[precioventa] [decimal](10, 2) NULL,
	[estado] [bit] NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idproducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[idproveedor] [int] IDENTITY(1,1) NOT NULL,
	[documento] [varchar](50) NULL,
	[razonsocial] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[estado] [bit] NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idproveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol](
	[idrol] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idrol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[idusuario] [int] IDENTITY(1,1) NOT NULL,
	[documento] [varchar](50) NULL,
	[nombrecompleto] [varchar](50) NULL,
	[clave] [varchar](50) NULL,
	[idrol] [int] NULL,
	[estado] [bit] NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idusuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venta]    Script Date: 12/8/2023 22:15:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[idventa] [int] IDENTITY(1,1) NOT NULL,
	[idusuario] [int] NULL,
	[tipodocumento] [varchar](50) NULL,
	[numerodocumento] [varchar](50) NULL,
	[documentocliente] [varchar](50) NULL,
	[nombrecliente] [varchar](100) NULL,
	[montopago] [decimal](10, 2) NULL,
	[montocambio] [decimal](10, 2) NULL,
	[montototal] [decimal](10, 2) NULL,
	[fecharegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[cliente] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[compra] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[detalle_compra] ADD  DEFAULT ((0)) FOR [preciocompra]
GO
ALTER TABLE [dbo].[detalle_compra] ADD  DEFAULT ((0)) FOR [precioventa]
GO
ALTER TABLE [dbo].[detalle_compra] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[detalle_venta] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[permiso] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT ((0)) FOR [stock]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT ((0)) FOR [preciocompra]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT ((0)) FOR [precioventa]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[proveedor] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[rol] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[usuario] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[venta] ADD  DEFAULT (getdate()) FOR [fecharegistro]
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD FOREIGN KEY([idproveedor])
REFERENCES [dbo].[proveedor] ([idproveedor])
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuario] ([idusuario])
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD FOREIGN KEY([idcompra])
REFERENCES [dbo].[compra] ([idcompra])
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD FOREIGN KEY([idproducto])
REFERENCES [dbo].[producto] ([idproducto])
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD FOREIGN KEY([idproducto])
REFERENCES [dbo].[producto] ([idproducto])
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD FOREIGN KEY([idventa])
REFERENCES [dbo].[venta] ([idventa])
GO
ALTER TABLE [dbo].[permiso]  WITH CHECK ADD FOREIGN KEY([idrol])
REFERENCES [dbo].[rol] ([idrol])
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idcategoria])
REFERENCES [dbo].[categoria] ([idcategoria])
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD FOREIGN KEY([idrol])
REFERENCES [dbo].[rol] ([idrol])
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuario] ([idusuario])
GO
USE [master]
GO
ALTER DATABASE [db_sistema_venta] SET  READ_WRITE 
GO
