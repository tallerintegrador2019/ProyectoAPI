USE [master]
GO
/****** Object:  Database [TodaviaSirve]    Script Date: 05/06/2019 20:48:39 ******/
CREATE DATABASE [TodaviaSirve]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TodaviaSirve', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\TodaviaSirve.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TodaviaSirve_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\TodaviaSirve_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TodaviaSirve] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TodaviaSirve].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TodaviaSirve] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TodaviaSirve] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TodaviaSirve] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TodaviaSirve] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TodaviaSirve] SET ARITHABORT OFF 
GO
ALTER DATABASE [TodaviaSirve] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TodaviaSirve] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TodaviaSirve] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TodaviaSirve] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TodaviaSirve] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TodaviaSirve] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TodaviaSirve] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TodaviaSirve] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TodaviaSirve] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TodaviaSirve] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TodaviaSirve] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TodaviaSirve] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TodaviaSirve] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TodaviaSirve] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TodaviaSirve] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TodaviaSirve] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TodaviaSirve] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TodaviaSirve] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TodaviaSirve] SET  MULTI_USER 
GO
ALTER DATABASE [TodaviaSirve] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TodaviaSirve] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TodaviaSirve] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TodaviaSirve] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TodaviaSirve] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TodaviaSirve]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comentarios](
	[Id] [bigint] NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[FechaComentario] [datetime] NOT NULL,
	[Comentario] [varchar](5000) NOT NULL,
	[IdPuntaje] [bigint] NOT NULL,
	[IdPublicacion] [bigint] NOT NULL,
 CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Materiales]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Materiales](
	[Id] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdCategoria] [bigint] NOT NULL,
 CONSTRAINT [PK_Materiales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdCategoria] [bigint] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductosCategorias]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductosCategorias](
	[IdProducto] [bigint] NOT NULL,
	[IdCategoria] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Publicaciones]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publicaciones](
	[Id] [bigint] NOT NULL,
	[Titulo] [varchar](100) NOT NULL,
	[IdProducto] [bigint] NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Publicaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Puntajes]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puntajes](
	[Id] [bigint] NOT NULL,
	[IdPublicacion] [bigint] NOT NULL,
	[Puntaje] [float] NULL,
 CONSTRAINT [PK_Puntajes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rangos]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rangos](
	[Id] [bigint] NOT NULL,
	[Rango] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Rangos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [bigint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[TablaRelacionalTodaviaSirve]    Script Date: 05/06/2019 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TablaRelacionalTodaviaSirve]
AS
SELECT        dbo.Categorias.Id, dbo.Categorias.Nombre, dbo.Comentarios.Id AS Expr1, dbo.Comentarios.IdUsuario, dbo.Comentarios.FechaComentario, dbo.Comentarios.Comentario, dbo.Comentarios.IdPuntaje, 
                         dbo.Comentarios.IdPublicacion, dbo.Materiales.Id AS Expr2, dbo.Materiales.Nombre AS Expr3, dbo.Materiales.IdCategoria, dbo.ProductosCategorias.IdProducto, dbo.ProductosCategorias.IdCategoria AS Expr4, 
                         dbo.Productos.Id AS Expr5, dbo.Productos.Nombre AS Expr6, dbo.Productos.IdCategoria AS Expr7, dbo.Publicaciones.Id AS Expr8, dbo.Publicaciones.Titulo, dbo.Publicaciones.IdProducto AS Expr9, 
                         dbo.Publicaciones.Descripcion, dbo.Puntajes.Id AS Expr10, dbo.Puntajes.IdPublicacion AS Expr11, dbo.Puntajes.Puntaje, dbo.Rangos.Id AS Expr12, dbo.Rangos.Rango, dbo.Usuarios.Id AS Expr13, 
                         dbo.Usuarios.Nombre AS Expr14, dbo.Usuarios.Apellido, dbo.Usuarios.Usuario, dbo.Usuarios.Password, dbo.Usuarios.Email
FROM            dbo.Rangos CROSS JOIN
                         dbo.Categorias CROSS JOIN
                         dbo.Materiales CROSS JOIN
                         dbo.Productos CROSS JOIN
                         dbo.ProductosCategorias CROSS JOIN
                         dbo.Comentarios CROSS JOIN
                         dbo.Usuarios CROSS JOIN
                         dbo.Puntajes CROSS JOIN
                         dbo.Publicaciones

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[21] 2[39] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Rangos"
            Begin Extent = 
               Top = 155
               Left = 65
               Bottom = 251
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Puntajes"
            Begin Extent = 
               Top = 159
               Left = 1027
               Bottom = 272
               Right = 1236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Usuarios"
            Begin Extent = 
               Top = 37
               Left = 799
               Bottom = 204
               Right = 1008
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Publicaciones"
            Begin Extent = 
               Top = 236
               Left = 800
               Bottom = 368
               Right = 1009
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Comentarios"
            Begin Extent = 
               Top = 92
               Left = 560
               Bottom = 263
               Right = 769
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Materiales"
            Begin Extent = 
               Top = 7
               Left = 66
               Bottom = 120
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Productos"
            Begin Extent = 
               Top = 376
               Left = 783
               Bottom = 488
               Right = 992
            End
          ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TablaRelacionalTodaviaSirve'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Categorias"
            Begin Extent = 
               Top = 90
               Left = 305
               Bottom = 186
               Right = 514
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductosCategorias"
            Begin Extent = 
               Top = 268
               Left = 544
               Bottom = 364
               Right = 753
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TablaRelacionalTodaviaSirve'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TablaRelacionalTodaviaSirve'
GO
USE [master]
GO
ALTER DATABASE [TodaviaSirve] SET  READ_WRITE 
GO
