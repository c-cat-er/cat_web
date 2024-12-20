USE [master]
GO
/****** Object:  Database [modpack]    Script Date: 2024/7/17 下午 09:09:48 ******/
CREATE DATABASE [modpack]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'modpack', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\modpack.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'modpack_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\modpack_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [modpack] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [modpack].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [modpack] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [modpack] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [modpack] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [modpack] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [modpack] SET ARITHABORT OFF 
GO
ALTER DATABASE [modpack] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [modpack] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [modpack] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [modpack] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [modpack] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [modpack] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [modpack] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [modpack] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [modpack] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [modpack] SET  DISABLE_BROKER 
GO
ALTER DATABASE [modpack] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [modpack] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [modpack] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [modpack] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [modpack] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [modpack] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [modpack] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [modpack] SET RECOVERY FULL 
GO
ALTER DATABASE [modpack] SET  MULTI_USER 
GO
ALTER DATABASE [modpack] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [modpack] SET DB_CHAINING OFF 
GO
ALTER DATABASE [modpack] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [modpack] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [modpack] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [modpack] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'modpack', N'ON'
GO
ALTER DATABASE [modpack] SET QUERY_STORE = ON
GO
ALTER DATABASE [modpack] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [modpack]
GO
/****** Object:  User [mir2]    Script Date: 2024/7/17 下午 09:09:52 ******/
CREATE USER [mir2] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [mir2]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[AdministratorID] [int] IDENTITY(1,1) NOT NULL,
	[TitleID] [int] NOT NULL,
	[AdminCode] [char](5) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[Image] [nvarchar](50) NULL,
	[Account] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
 CONSTRAINT [PK__Administ__ACDEFE33B7C742FC] PRIMARY KEY CLUSTERED 
(
	[AdministratorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdministratorActivitylog]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdministratorActivitylog](
	[ActivitylogID] [int] IDENTITY(1,1) NOT NULL,
	[AdministratorID] [int] NOT NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ActivitylogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdministratorModification]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdministratorModification](
	[ModificationID] [int] IDENTITY(1,1) NOT NULL,
	[AdministratorID] [int] NOT NULL,
	[ModifierID] [int] NOT NULL,
	[ModifierDescribe] [nvarchar](50) NULL,
	[ModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ModificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdministratorTitle]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdministratorTitle](
	[TitleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Permissions] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[InspirationID] [int] NULL,
	[CustomizedID] [int] NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK__Cart__51BCD797FFCF03C9] PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ComponentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[ColorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RGB] [varchar](6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Component]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Component](
	[ComponentID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialID] [int] NOT NULL,
	[ColorID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](10) NOT NULL,
	[OriginalPrice] [decimal](10, 2) NOT NULL,
	[FBXFileName] [varchar](50) NOT NULL,
	[ImageFileName] [varchar](50) NOT NULL,
	[IsCustomized] [bit] NOT NULL,
 CONSTRAINT [PK__Componen__D79CF02E7BE6CD78] PRIMARY KEY CLUSTERED 
(
	[ComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credit]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credit](
	[CreditID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NULL,
	[HistoryName] [nvarchar](50) NOT NULL,
	[IncomingAmount] [int] NULL,
	[UsageAmount] [int] NULL,
	[ModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CreditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customized]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customized](
	[CustomizedID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[PromotionID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ImageFileName] [varchar](50) NOT NULL,
	[SalePrice] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__Customiz__603F25BF81F92FA7] PRIMARY KEY CLUSTERED 
(
	[CustomizedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomizedSpecification]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomizedSpecification](
	[CustomizedSpecificationID] [int] IDENTITY(1,1) NOT NULL,
	[CustomizedID] [int] NOT NULL,
	[ComponentID] [int] NOT NULL,
	[MaterialID] [int] NULL,
	[ColorID] [int] NULL,
	[Location] [int] NULL,
	[SizeX] [int] NULL,
	[SizeY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomizedSpecificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavoriteItems]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavoriteItems](
	[ItemsID] [int] IDENTITY(1,1) NOT NULL,
	[FavoritesID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[InspirationID] [int] NULL,
	[CustomizedID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[FavoritesID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FavoritesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageCarousel]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageCarousel](
	[ImageCarouselID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [varchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__ImageCar__23805719424F93BD] PRIMARY KEY CLUSTERED 
(
	[ImageCarouselID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inspiration]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inspiration](
	[InspirationID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ImageFileName] [varchar](50) NOT NULL,
	[SalePrice] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__Inspirat__9FEF7BB07521F338] PRIMARY KEY CLUSTERED 
(
	[InspirationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InspirationSpecification]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InspirationSpecification](
	[InspirationSpecificationID] [int] IDENTITY(1,1) NOT NULL,
	[InspirationID] [int] NOT NULL,
	[ComponentID] [int] NOT NULL,
	[MaterialID] [int] NULL,
	[ColorID] [int] NULL,
	[Location] [int] NULL,
	[SizeX] [int] NULL,
	[SizeY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[InspirationSpecificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[MaterialID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ImageFileName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[LevelID] [int] NULL,
	[Account] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](25) NULL,
	[Address] [nvarchar](100) NULL,
	[CreationTime] [datetime] NOT NULL,
	[ModificationTime] [datetime] NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
 CONSTRAINT [PK__Member__0CF04B382A47F456] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberActivitylog]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberActivitylog](
	[ActivitylogID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[LoginTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ActivitylogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberLevel]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberLevel](
	[LevelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Level] [int] NOT NULL,
 CONSTRAINT [PK__MemberLe__09F03C06A78E89D3] PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[PaymentStatusID] [int] NOT NULL,
	[PaymentID] [int] NOT NULL,
	[PromoCodeID] [int] NULL,
	[ShippingID] [int] NOT NULL,
	[RecipientName] [nvarchar](50) NULL,
	[RecipientAddress] [nvarchar](50) NULL,
	[BillRecipientName] [nvarchar](50) NULL,
	[BillRecipientAddress] [nvarchar](50) NULL,
	[ShippingStatusID] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[CompletionTime] [datetime] NULL,
	[OrderStatusID] [int] NOT NULL,
 CONSTRAINT [PK__Order__C3905BAFAB04E5A5] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[DetailsID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[InspirationID] [int] NULL,
	[CustomizedID] [int] NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK__OrderDet__BAC862ACC7D4E009] PRIMARY KEY CLUSTERED 
(
	[DetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[OrderStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordReset]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordReset](
	[TokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Token] [varchar](8) NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PasswordReset] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentStatus]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentStatus](
	[PaymentStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](10) NOT NULL,
	[ImageFileName] [varchar](50) NOT NULL,
	[OriginalPrice] [decimal](10, 2) NOT NULL,
	[SalePrice] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSpecification]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSpecification](
	[ProductSpecificationID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ComponentID] [int] NOT NULL,
	[MaterialID] [int] NULL,
	[ColorID] [int] NULL,
	[Location] [int] NULL,
	[SizeX] [int] NULL,
	[SizeY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductSpecificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PromoCode]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromoCode](
	[PromoCodeID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](8) NOT NULL,
	[Method] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Limitation] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PromoCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceRecord]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRecord](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[Question] [nvarchar](100) NOT NULL,
	[QuestionTime] [datetime] NULL,
	[AdministratorID] [int] NULL,
	[Answer] [nvarchar](200) NULL,
	[AnswerTime] [datetime] NULL,
	[IsResolved] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipping]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipping](
	[ShippingID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[DeliveryCost] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ShippingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShippingStatus]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingStatus](
	[ShippingStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShippingStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreLocation]    Script Date: 2024/7/17 下午 09:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreLocation](
	[StoreLocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OfficeTelephone] [varchar](10) NULL,
	[Address] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Administrator] ON 

INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (1, 4, N'A0001', N'超級管理員', N'sup.png', N'sup', N'0000')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (2, 6, N'B0001', N'葉威志', N'adm91fc0.png', N'gen', N'1111')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (3, 7, N'B0002', N'李承洧', N'adm06121.png', N'ken', N'2222')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (4, 7, N'B0003', N'劉亞讓', N'adm00862.png', N'zha', N'3333')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (5, 7, N'B0004', N'常茗豐', N'adm15dfb.png', N'mac', N'4444')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (8, 7, N'B0005', N'陳葉茙', N'admbc4b1.png', N'yeye2', N'5555')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (9, 9, N'C1001', N'員工一', N'em1.png', N'em1', N'6666')
INSERT [dbo].[Administrator] ([AdministratorID], [TitleID], [AdminCode], [Name], [Image], [Account], [Password]) VALUES (10, 9, N'C1002', N'員工二', N'em2.png', N'em2', N'7777')
SET IDENTITY_INSERT [dbo].[Administrator] OFF
GO
SET IDENTITY_INSERT [dbo].[AdministratorActivitylog] ON 

INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (1, 1, CAST(N'2024-01-31T08:50:50.173' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (2, 1, NULL, CAST(N'2024-01-31T08:50:50.173' AS DateTime))
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (3, 1, CAST(N'2024-01-31T08:50:50.173' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (4, 2, CAST(N'2024-01-31T08:50:50.177' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (5, 2, NULL, CAST(N'2024-01-31T08:50:50.177' AS DateTime))
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (6, 3, CAST(N'2024-01-31T08:50:50.177' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (7, 4, CAST(N'2024-01-31T08:50:50.177' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (8, 1, NULL, CAST(N'2024-01-31T08:50:50.180' AS DateTime))
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (9, 4, CAST(N'2024-02-20T09:54:52.697' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (10, 4, CAST(N'2024-02-20T15:10:50.780' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (11, 4, CAST(N'2024-02-20T15:29:22.830' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (12, 4, CAST(N'2024-02-20T15:33:10.773' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (13, 4, CAST(N'2024-02-20T15:34:09.063' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (14, 1, CAST(N'2024-02-21T09:12:35.510' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (15, 1, CAST(N'2024-02-21T09:44:01.983' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (16, 1, CAST(N'2024-02-21T10:31:28.300' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (17, 1, CAST(N'2024-02-21T12:27:04.217' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (18, 1, CAST(N'2024-02-21T12:28:24.523' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (19, 1, CAST(N'2024-02-21T12:33:42.010' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (20, 1, CAST(N'2024-02-21T12:33:42.010' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (21, 1, CAST(N'2024-02-21T12:34:16.080' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (22, 1, CAST(N'2024-02-21T14:27:32.787' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (23, 1, CAST(N'2024-02-21T15:47:54.390' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (24, 1, CAST(N'2024-02-21T16:17:53.510' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (25, 1, CAST(N'2024-02-22T10:00:57.327' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (26, 1, CAST(N'2024-02-22T10:02:26.660' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (27, 1, CAST(N'2024-02-22T14:29:30.860' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (28, 1, CAST(N'2024-02-22T14:30:39.410' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (29, 1, CAST(N'2024-02-22T16:27:44.540' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (30, 1, CAST(N'2024-02-23T09:19:26.777' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (31, 1, CAST(N'2024-02-23T09:26:48.517' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (32, 1, CAST(N'2024-02-23T09:27:36.737' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (33, 1, CAST(N'2024-02-23T09:30:09.207' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (34, 1, CAST(N'2024-02-23T09:42:47.057' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (35, 1, CAST(N'2024-02-23T09:59:27.090' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (36, 1, CAST(N'2024-02-23T10:05:41.807' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (37, 1, CAST(N'2024-02-23T10:06:57.130' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (38, 5, CAST(N'2024-02-23T12:11:14.363' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (39, 5, CAST(N'2024-02-23T12:14:08.587' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (40, 5, CAST(N'2024-02-23T13:42:45.033' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (41, 5, CAST(N'2024-02-23T13:46:38.270' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (42, 5, CAST(N'2024-02-23T14:44:34.717' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (43, 5, CAST(N'2024-02-23T14:57:18.267' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (44, 5, CAST(N'2024-02-23T15:19:20.293' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (45, 5, CAST(N'2024-02-23T15:29:01.037' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (46, 5, CAST(N'2024-02-23T15:31:06.260' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (47, 5, CAST(N'2024-02-23T15:51:37.643' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (48, 5, CAST(N'2024-02-23T15:52:56.833' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (49, 5, CAST(N'2024-02-23T15:57:21.347' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (50, 5, CAST(N'2024-02-24T19:59:26.507' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (51, 5, CAST(N'2024-02-24T20:01:54.357' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (52, 5, CAST(N'2024-02-24T20:10:09.603' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (53, 5, CAST(N'2024-02-24T20:10:09.600' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (54, 5, CAST(N'2024-02-24T20:26:52.683' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (55, 5, CAST(N'2024-02-25T00:48:48.173' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (56, 5, CAST(N'2024-02-25T00:58:16.830' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (57, 5, CAST(N'2024-02-25T01:02:47.023' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (58, 5, CAST(N'2024-02-25T01:04:15.617' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (59, 5, CAST(N'2024-02-25T01:06:44.333' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (60, 5, CAST(N'2024-02-25T01:08:04.567' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (61, 5, CAST(N'2024-02-25T01:11:38.127' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (62, 5, CAST(N'2024-02-25T01:20:43.813' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (63, 5, CAST(N'2024-02-25T01:30:18.460' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (64, 5, CAST(N'2024-02-25T01:31:26.677' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (65, 5, CAST(N'2024-02-25T01:38:10.823' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (66, 5, CAST(N'2024-02-25T01:42:42.490' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (67, 5, CAST(N'2024-02-25T01:45:45.003' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (68, 5, CAST(N'2024-02-25T01:52:30.297' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (69, 5, CAST(N'2024-02-25T02:21:07.330' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (70, 5, CAST(N'2024-02-25T02:23:45.453' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (71, 5, CAST(N'2024-02-25T02:26:08.553' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (72, 5, CAST(N'2024-02-25T02:26:08.550' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (73, 5, CAST(N'2024-02-25T02:28:35.367' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (74, 5, CAST(N'2024-02-25T11:21:19.927' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (75, 5, CAST(N'2024-02-25T11:24:34.803' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (76, 5, CAST(N'2024-02-25T11:34:40.097' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (77, 5, CAST(N'2024-02-25T11:39:23.293' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (78, 5, CAST(N'2024-02-25T14:06:01.553' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (79, 5, CAST(N'2024-02-25T14:19:38.103' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (80, 5, CAST(N'2024-02-25T14:32:25.363' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (81, 5, CAST(N'2024-02-25T14:34:18.020' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (82, 5, CAST(N'2024-02-25T15:03:18.853' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (83, 5, CAST(N'2024-02-25T15:07:15.677' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (84, 5, CAST(N'2024-02-25T15:07:16.000' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (85, 5, CAST(N'2024-02-25T15:11:31.543' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (86, 5, CAST(N'2024-02-25T15:16:14.997' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (87, 5, CAST(N'2024-02-25T21:14:26.140' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (88, 5, CAST(N'2024-02-25T21:18:11.707' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (89, 5, CAST(N'2024-02-25T21:19:20.397' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (90, 5, CAST(N'2024-02-25T21:22:47.037' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (91, 5, CAST(N'2024-02-25T21:24:05.040' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (92, 5, CAST(N'2024-02-25T21:25:03.870' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (93, 5, CAST(N'2024-02-25T21:55:05.287' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (94, 5, CAST(N'2024-02-25T23:01:18.200' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (95, 5, CAST(N'2024-02-25T23:01:18.200' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (96, 5, CAST(N'2024-02-25T23:03:46.500' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (97, 5, CAST(N'2024-02-25T23:05:10.257' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (98, 5, CAST(N'2024-02-25T23:06:33.713' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (99, 5, CAST(N'2024-02-25T23:41:06.977' AS DateTime), NULL)
GO
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (100, 5, CAST(N'2024-02-25T23:46:05.923' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (101, 5, CAST(N'2024-02-25T23:51:26.603' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (102, 5, CAST(N'2024-02-26T00:02:28.613' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (103, 5, CAST(N'2024-02-26T00:05:40.060' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (104, 5, CAST(N'2024-02-26T00:21:26.197' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (105, 5, CAST(N'2024-02-26T00:24:49.403' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (106, 5, CAST(N'2024-02-26T00:26:36.933' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (107, 5, CAST(N'2024-02-26T00:35:26.503' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (108, 5, CAST(N'2024-02-26T00:38:01.960' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (109, 5, CAST(N'2024-02-26T01:16:05.963' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (110, 5, CAST(N'2024-02-26T09:31:54.873' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (111, 5, CAST(N'2024-02-26T10:23:06.927' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (112, 5, CAST(N'2024-02-26T10:40:35.173' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (113, 5, CAST(N'2024-02-26T10:40:35.330' AS DateTime), NULL)
INSERT [dbo].[AdministratorActivitylog] ([ActivitylogID], [AdministratorID], [LoginTime], [LogoutTime]) VALUES (114, 5, CAST(N'2024-02-26T10:48:19.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[AdministratorActivitylog] OFF
GO
SET IDENTITY_INSERT [dbo].[AdministratorModification] ON 

INSERT [dbo].[AdministratorModification] ([ModificationID], [AdministratorID], [ModifierID], [ModifierDescribe], [ModificationTime]) VALUES (1, 1, 1, N'', CAST(N'2024-01-31T08:50:50.120' AS DateTime))
INSERT [dbo].[AdministratorModification] ([ModificationID], [AdministratorID], [ModifierID], [ModifierDescribe], [ModificationTime]) VALUES (2, 1, 2, N'', CAST(N'2024-01-31T08:50:50.120' AS DateTime))
INSERT [dbo].[AdministratorModification] ([ModificationID], [AdministratorID], [ModifierID], [ModifierDescribe], [ModificationTime]) VALUES (3, 1, 4, N'做太爛被降級', CAST(N'2024-01-31T08:50:50.120' AS DateTime))
INSERT [dbo].[AdministratorModification] ([ModificationID], [AdministratorID], [ModifierID], [ModifierDescribe], [ModificationTime]) VALUES (4, 2, 3, N'空降員工位階為主管', CAST(N'2024-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[AdministratorModification] ([ModificationID], [AdministratorID], [ModifierID], [ModifierDescribe], [ModificationTime]) VALUES (5, 4, 5, N'', CAST(N'2024-01-21T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[AdministratorModification] OFF
GO
SET IDENTITY_INSERT [dbo].[AdministratorTitle] ON 

INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (1, N'神', 0)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (2, N'CEO', 1)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (3, N'主管', 10)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (4, N'副主管', 11)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (5, N'經理', 100)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (6, N'副理', 101)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (7, N'店長', 1000)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (8, N'副店長', 1001)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (9, N'員工', 10000)
INSERT [dbo].[AdministratorTitle] ([TitleID], [Name], [Permissions]) VALUES (10, N'停權', 100000)
SET IDENTITY_INSERT [dbo].[AdministratorTitle] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (1, 6, 1, 3, 1, 10)
INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (3, 1, 1, 1, 2, 2)
INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (4, 2, 1, 1, 1, 1)
INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (5, 2, NULL, 1, NULL, 5)
INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (6, 3, 1, 3, 3, 19)
INSERT [dbo].[Cart] ([CartID], [MemberID], [ProductID], [InspirationID], [CustomizedID], [Quantity]) VALUES (27, 1, 1, 1, 1, 10)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (1, 1, N'學生用')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (2, 1, N'主包')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (3, 2, N'學生用')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (4, 2, N'配件')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (5, 3, N'通用')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (6, 3, N'配件')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (7, 4, N'學生用')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (8, 4, N'配件')
INSERT [dbo].[Category] ([CategoryID], [ComponentID], [Name]) VALUES (9, 5, N'測試用')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (1, N'紅色', N'FF0000')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (2, N'藍色', N'0000FF')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (3, N'黃色', N'FFFF00')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (4, N'橘色', N'FF8000')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (5, N'綠色', N'00FF00')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (6, N'紫色', N'8000FF')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (8, N'白色', N'FFFFFF')
INSERT [dbo].[Color] ([ColorID], [Name], [RGB]) VALUES (9, N'黑色', N'000000')
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[Component] ON 

INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (1, 1, 1, 3, N'學生主包', N'主包', CAST(110.00 AS Decimal(10, 2)), N'pack', N'1234.jpg', 1)
INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (2, 1, 2, 3, N'文具包', N'配件', CAST(120.00 AS Decimal(10, 2)), N'1', N'1235.jpg', 1)
INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (3, 1, 3, 3, N'通用包', N'配件', CAST(130.00 AS Decimal(10, 2)), N'2', N'1236.jpg', 1)
INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (4, 1, 4, 3, N'放書小包包', N'配件', CAST(150.00 AS Decimal(10, 2)), N'3', N'1237.jpg', 1)
INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (5, 1, 5, 1, N'測試用包', N'配件', CAST(200.00 AS Decimal(10, 2)), N'4', N'1238.jpg', 0)
INSERT [dbo].[Component] ([ComponentID], [MaterialID], [ColorID], [StatusID], [Name], [Category], [OriginalPrice], [FBXFileName], [ImageFileName], [IsCustomized]) VALUES (6, 1, 1, 3, N'測試用包2', N'配件', CAST(250.00 AS Decimal(10, 2)), N'5', N'1239.jpg', 1)
SET IDENTITY_INSERT [dbo].[Component] OFF
GO
SET IDENTITY_INSERT [dbo].[Credit] ON 

INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (1, 1, N'購買物品', 100, NULL, CAST(N'2024-01-31T08:50:52.697' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (2, 1, N'使用購物金', NULL, 100, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (3, 2, N'購買物品', 1000, NULL, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (4, 2, N'使用購物金', NULL, 1000, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (5, 3, N'購買物品', 500, NULL, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (6, 3, N'使用購物金', NULL, 500, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (7, 4, N'贈送', 200, NULL, CAST(N'2024-01-31T08:50:52.700' AS DateTime))
INSERT [dbo].[Credit] ([CreditID], [MemberID], [HistoryName], [IncomingAmount], [UsageAmount], [ModificationTime]) VALUES (8, 4, N'使用購物金', NULL, 200, CAST(N'2024-01-31T08:50:52.703' AS DateTime))
SET IDENTITY_INSERT [dbo].[Credit] OFF
GO
SET IDENTITY_INSERT [dbo].[Customized] ON 

INSERT [dbo].[Customized] ([CustomizedID], [MemberID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (1, 1, 1, N'Customizedtest1', N'151.png', CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[Customized] ([CustomizedID], [MemberID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (2, 2, 1, N'Customizedtest2', N'152.png', CAST(1100.00 AS Decimal(10, 2)))
INSERT [dbo].[Customized] ([CustomizedID], [MemberID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (3, 3, 1, N'Customizedtest3', N'153.png', CAST(1200.00 AS Decimal(10, 2)))
INSERT [dbo].[Customized] ([CustomizedID], [MemberID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (4, 4, 1, N'Customizedtest4', N'154.png', CAST(1300.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Customized] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomizedSpecification] ON 

INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (1, 1, 1, 1, 1, 0, 1, 1)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (2, 1, 1, 1, 2, 0, 1, 1)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (3, 1, 2, 1, 3, 1, 2, 3)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (4, 2, 1, 2, 4, 0, 1, 1)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (5, 2, 2, 2, 5, 1, 1, 2)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (6, 2, 2, 2, 6, 2, 1, 3)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (7, 3, 1, 3, 8, 0, 1, 1)
INSERT [dbo].[CustomizedSpecification] ([CustomizedSpecificationID], [CustomizedID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (8, 3, 3, 3, 9, 1, 2, 3)
SET IDENTITY_INSERT [dbo].[CustomizedSpecification] OFF
GO
SET IDENTITY_INSERT [dbo].[FavoriteItems] ON 

INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (1, 1, 1, NULL, NULL)
INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (2, 1, NULL, 1, NULL)
INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (3, 1, NULL, NULL, 1)
INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (4, 2, 1, NULL, NULL)
INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (5, 2, NULL, 1, NULL)
INSERT [dbo].[FavoriteItems] ([ItemsID], [FavoritesID], [ProductID], [InspirationID], [CustomizedID]) VALUES (6, 3, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[FavoriteItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorites] ON 

INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (1, 1)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (2, 1)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (3, 1)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (4, 2)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (5, 2)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (6, 3)
INSERT [dbo].[Favorites] ([FavoritesID], [MemberID]) VALUES (7, 4)
SET IDENTITY_INSERT [dbo].[Favorites] OFF
GO
SET IDENTITY_INSERT [dbo].[ImageCarousel] ON 

INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (1, N'cabcf8b.jpg', N'開學季')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (2, N'caeb392.jpg', N'越野攀登')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (3, N'ca98481.jpg', N'科技防水')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (4, N'cad3ff1.jpg', N'沒圖1')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (5, N'cabbcc0.jpg', N'沒圖2')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (6, N'ca4f39e.jpg', N'沒圖3')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (7, N'ca04a4d.jpg', N'沒圖4')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (8, N'cab4a7f.jpg', N'沒圖5')
INSERT [dbo].[ImageCarousel] ([ImageCarouselID], [Image], [Description]) VALUES (9, N'ca01c87.jpg', N'沒圖6')
SET IDENTITY_INSERT [dbo].[ImageCarousel] OFF
GO
SET IDENTITY_INSERT [dbo].[Inspiration] ON 

INSERT [dbo].[Inspiration] ([InspirationID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (1, 1, N'Inspirationtest1', N'141.png', CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[Inspiration] ([InspirationID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (2, 1, N'Inspirationtest2', N'142.png', CAST(1100.00 AS Decimal(10, 2)))
INSERT [dbo].[Inspiration] ([InspirationID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (3, 1, N'Inspirationtest3', N'143.png', CAST(1200.00 AS Decimal(10, 2)))
INSERT [dbo].[Inspiration] ([InspirationID], [PromotionID], [Name], [ImageFileName], [SalePrice]) VALUES (4, 1, N'Inspirationtest4', N'144.png', CAST(1300.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Inspiration] OFF
GO
SET IDENTITY_INSERT [dbo].[InspirationSpecification] ON 

INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (1, 1, 1, 1, 1, 0, 1, 1)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (2, 1, 1, 1, 2, 0, 1, 1)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (3, 1, 2, 1, 3, 1, 2, 3)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (4, 2, 1, 2, 4, 0, 1, 1)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (5, 2, 2, 2, 5, 1, 1, 2)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (6, 2, 2, 2, 6, 2, 1, 3)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (7, 3, 1, 3, 8, 0, 1, 1)
INSERT [dbo].[InspirationSpecification] ([InspirationSpecificationID], [InspirationID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (8, 3, 3, 3, 9, 1, 2, 3)
SET IDENTITY_INSERT [dbo].[InspirationSpecification] OFF
GO
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([MaterialID], [Name], [ImageFileName]) VALUES (1, N'皮革', N'131.png')
INSERT [dbo].[Material] ([MaterialID], [Name], [ImageFileName]) VALUES (2, N'蛇皮', N'132.png')
INSERT [dbo].[Material] ([MaterialID], [Name], [ImageFileName]) VALUES (3, N'虎皮', N'133.png')
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (1, 2, N'mgarcia', N'$46L5Kjsor', N'陳大文', N'arthur63@brown.com', N'0941681233', N'台中市西區美村路一段', CAST(N'2024-01-31T11:07:25.793' AS DateTime), CAST(N'2024-01-31T11:07:25.793' AS DateTime), 1)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (2, 2, N'fdiaz', N'^0m^q(Ae%Z', N'劉子瑜', N'reneesilva@james.com', N'0928586079', N'台北市大安區忠孝東路四段', CAST(N'2024-01-31T11:07:25.793' AS DateTime), CAST(N'2024-01-31T11:07:25.793' AS DateTime), 1)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (3, 1, N'jerry84', N'$XGu0pIhYj', N'王雅婷', N'tonyvasquez@smith.biz', N'0916948010', N'高雄市前鎮區成功二路', CAST(N'2024-01-31T11:07:25.793' AS DateTime), CAST(N'2024-01-31T11:07:25.793' AS DateTime), 1)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (4, 3, N'alexispark', N')0D!oLwlg#', N'林怡君', N'eric53@hotmail.com', N'0905846218', N'新北市板橋區中山路二段', CAST(N'2024-01-31T11:07:25.793' AS DateTime), CAST(N'2024-01-31T11:07:25.793' AS DateTime), 0)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (5, 1, N'test1', N'1234', N'待刪測試資料', N'xx111@gmail.com', N'0123456789', N'台北市', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-02-23T11:03:38.123' AS DateTime), 1)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (6, 2, N'test2', N'1234', N'待刪測試資料', N'xx123@gmail.com', N'0123456789', N'高雄市', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-02-23T11:03:47.070' AS DateTime), 0)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (7, 3, N'test3', N'1234', N'路人', NULL, NULL, NULL, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-01T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Member] ([MemberID], [LevelID], [Account], [Password], [Name], [Email], [Phone], [Address], [CreationTime], [ModificationTime], [IsConfirmed]) VALUES (8, 4, N'test4', N'1234', N'待刪測試資料', N'xx234@gmail.com', N'0123456789', N'台南市', CAST(N'2024-01-31T08:50:50.633' AS DateTime), CAST(N'2024-02-23T11:03:52.330' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberActivitylog] ON 

INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (1, 1, CAST(N'2024-01-31T08:50:50.727' AS DateTime))
INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (2, 1, CAST(N'2024-01-31T08:50:50.730' AS DateTime))
INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (3, 2, CAST(N'2024-01-31T08:50:50.730' AS DateTime))
INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (4, 3, CAST(N'2024-01-31T08:50:50.730' AS DateTime))
INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (5, 2, CAST(N'2024-01-31T08:50:50.730' AS DateTime))
INSERT [dbo].[MemberActivitylog] ([ActivitylogID], [MemberID], [LoginTime]) VALUES (6, 1, CAST(N'2024-01-31T08:50:50.730' AS DateTime))
SET IDENTITY_INSERT [dbo].[MemberActivitylog] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberLevel] ON 

INSERT [dbo].[MemberLevel] ([LevelID], [Name], [Level]) VALUES (1, N'新註冊', 1)
INSERT [dbo].[MemberLevel] ([LevelID], [Name], [Level]) VALUES (2, N'一般會員', 2)
INSERT [dbo].[MemberLevel] ([LevelID], [Name], [Level]) VALUES (3, N'VIP會員', 3)
INSERT [dbo].[MemberLevel] ([LevelID], [Name], [Level]) VALUES (4, N'黑名單', 4)
SET IDENTITY_INSERT [dbo].[MemberLevel] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (1, 1, 3, 1, 1, 1, N'陳先生', N'台中市', N'陳先生', N'台中市', 5, CAST(N'2023-12-08T12:26:33.000' AS DateTime), CAST(N'2023-12-08T19:20:07.170' AS DateTime), 2)
INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (2, 2, 3, 2, 2, 2, N'劉女士', N'台北市', N'陳女士', N'台東縣', 5, CAST(N'2024-01-22T20:58:02.120' AS DateTime), CAST(N'2024-01-23T12:55:45.000' AS DateTime), 2)
INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (3, 3, 3, 1, NULL, 3, N'王女士', N'高雄市', N'王女士', N'高雄市', 5, CAST(N'2024-01-24T17:50:53.390' AS DateTime), CAST(N'2024-01-26T20:57:18.500' AS DateTime), 2)
INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (8, 4, 4, 4, NULL, 4, N'林女士', N'新北市', N'葉女士', N'台中市', 4, CAST(N'2024-02-28T12:45:22.000' AS DateTime), CAST(N'2024-02-28T19:00:00.000' AS DateTime), 2)
INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (9, 5, 1, 2, NULL, 4, N'陳先生', N'台中市', N'鄒先生', N'新北市', 1, CAST(N'2024-03-07T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[Order] ([OrderID], [MemberID], [PaymentStatusID], [PaymentID], [PromoCodeID], [ShippingID], [RecipientName], [RecipientAddress], [BillRecipientName], [BillRecipientAddress], [ShippingStatusID], [CreationTime], [CompletionTime], [OrderStatusID]) VALUES (11, 6, 1, 1, 6, 3, N'王女士', N'高雄市', N'王女士', N'高雄市', 1, CAST(N'2024-03-16T00:00:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (1, 1, 1, NULL, NULL, CAST(300.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (2, 1, NULL, 2, NULL, CAST(100.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (5, 1, NULL, NULL, 1, CAST(300.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (6, 2, 2, NULL, NULL, CAST(100.00 AS Decimal(10, 2)), 10)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (7, 2, NULL, 1, NULL, CAST(300.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (8, 3, NULL, NULL, 3, CAST(90.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[OrderDetails] ([DetailsID], [OrderID], [ProductID], [InspirationID], [CustomizedID], [UnitPrice], [Quantity]) VALUES (11, 8, NULL, 2, NULL, CAST(90.00 AS Decimal(10, 2)), 7)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 

INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (1, N'新增中')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (2, N'已新增')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (3, N'退貨中')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (4, N'已退貨')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (5, N'未銷帳')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (6, N'已銷帳')
INSERT [dbo].[OrderStatus] ([OrderStatusID], [Name]) VALUES (7, N'已廢棄')
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[PasswordReset] ON 

INSERT [dbo].[PasswordReset] ([TokenId], [UserId], [Token], [ExpiryDate]) VALUES (1, 1, N'1234', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[PasswordReset] ([TokenId], [UserId], [Token], [ExpiryDate]) VALUES (2, 2, N'4132', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[PasswordReset] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([PaymentID], [Description]) VALUES (1, N'現金')
INSERT [dbo].[Payment] ([PaymentID], [Description]) VALUES (2, N'信用卡')
INSERT [dbo].[Payment] ([PaymentID], [Description]) VALUES (3, N'石油')
INSERT [dbo].[Payment] ([PaymentID], [Description]) VALUES (4, N'洗碗換錢')
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentStatus] ON 

INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (1, N'未付款')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (2, N'付款中')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (3, N'已付款')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (4, N'退款中')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (5, N'已退款')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (6, N'逃單中')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (7, N'賒帳中')
INSERT [dbo].[PaymentStatus] ([PaymentStatusID], [Name]) VALUES (8, N'已廢棄')
SET IDENTITY_INSERT [dbo].[PaymentStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [PromotionID], [StatusID], [Name], [Category], [ImageFileName], [OriginalPrice], [SalePrice]) VALUES (1, 1, 3, N'學生包包', N'主包配件', N'121.png', CAST(500.00 AS Decimal(10, 2)), CAST(300.00 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([ProductID], [PromotionID], [StatusID], [Name], [Category], [ImageFileName], [OriginalPrice], [SalePrice]) VALUES (2, 2, 3, N'學生主包', N'主包', N'122.png', CAST(110.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([ProductID], [PromotionID], [StatusID], [Name], [Category], [ImageFileName], [OriginalPrice], [SalePrice]) VALUES (3, 3, 6, N'文具包', N'配件', N'123.png', CAST(120.00 AS Decimal(10, 2)), CAST(90.00 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([ProductID], [PromotionID], [StatusID], [Name], [Category], [ImageFileName], [OriginalPrice], [SalePrice]) VALUES (4, 4, 3, N'通用包', N'配件', N'124.png', CAST(130.00 AS Decimal(10, 2)), CAST(70.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductSpecification] ON 

INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (1, 1, 1, 1, 1, 0, 1, 1)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (2, 1, 1, 1, 2, 0, 1, 1)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (3, 1, 2, 1, 3, 1, 2, 3)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (4, 2, 1, 2, 4, 0, 1, 1)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (5, 2, 2, 2, 5, 1, 1, 2)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (6, 2, 2, 2, 6, 2, 1, 3)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (7, 3, 1, 3, 8, 0, 1, 1)
INSERT [dbo].[ProductSpecification] ([ProductSpecificationID], [ProductID], [ComponentID], [MaterialID], [ColorID], [Location], [SizeX], [SizeY]) VALUES (8, 3, 3, 3, 9, 1, 2, 3)
SET IDENTITY_INSERT [dbo].[ProductSpecification] OFF
GO
SET IDENTITY_INSERT [dbo].[PromoCode] ON 

INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (1, N'STU1234', N'一折', N'特定品項一折', N'學生', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-05T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (2, N'STS1234', N'五折', N'特定品項五折', N'學生', CAST(N'2024-01-05T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (3, N'NON1234', N'每一送一', N'特定品項每一送一', NULL, CAST(N'2024-01-10T00:00:00.000' AS DateTime), CAST(N'2024-02-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (4, N'YU1234', N'免錢', N'年度優惠全館免錢', N'所有註冊用戶', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-01T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (5, N'NOU1234', N'免錢', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (6, N'YN1234', N'每一送一', NULL, N'所有註冊用戶', NULL, CAST(N'2024-12-31T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[PromoCode] ([PromoCodeID], [Code], [Method], [Description], [Limitation], [StartDate], [EndDate], [Status]) VALUES (7, N'YN1234', N'每一送一', N'年度優惠', N'所有註冊用戶', CAST(N'2024-01-01T00:00:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[PromoCode] OFF
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON 

INSERT [dbo].[Promotion] ([PromotionID], [Name]) VALUES (1, N'無優惠')
INSERT [dbo].[Promotion] ([PromotionID], [Name]) VALUES (2, N'買一送一')
INSERT [dbo].[Promotion] ([PromotionID], [Name]) VALUES (3, N'一折')
INSERT [dbo].[Promotion] ([PromotionID], [Name]) VALUES (4, N'五折')
INSERT [dbo].[Promotion] ([PromotionID], [Name]) VALUES (5, N'免錢')
SET IDENTITY_INSERT [dbo].[Promotion] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceRecord] ON 

INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (1, 1, N'如何使用', CAST(N'2024-01-21T00:00:00.000' AS DateTime), 1, N'Google', CAST(N'2024-01-31T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (2, 2, N'如何客製化', CAST(N'2024-01-21T00:00:00.000' AS DateTime), NULL, N'問客服', CAST(N'2024-01-31T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (3, 3, N'如何進網頁', CAST(N'2024-01-31T08:50:50.793' AS DateTime), NULL, NULL, CAST(N'2024-01-21T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (4, 1, N'東西太貴了', CAST(N'2024-01-31T08:50:50.793' AS DateTime), 3, NULL, CAST(N'2024-01-31T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (5, 1, N'可以應徵我嗎', CAST(N'2024-01-31T08:50:50.793' AS DateTime), 2, N'看你努力', NULL, 1)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (6, 1, N'東西太便宜了', CAST(N'2024-01-31T08:50:50.793' AS DateTime), 3, NULL, NULL, 0)
INSERT [dbo].[ServiceRecord] ([RecordID], [MemberID], [Question], [QuestionTime], [AdministratorID], [Answer], [AnswerTime], [IsResolved]) VALUES (7, 1, N'可以娶我嗎', CAST(N'2024-01-31T08:50:50.970' AS DateTime), NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ServiceRecord] OFF
GO
SET IDENTITY_INSERT [dbo].[Shipping] ON 

INSERT [dbo].[Shipping] ([ShippingID], [Description], [DeliveryCost]) VALUES (1, N'郵寄', CAST(60.00 AS Decimal(10, 2)))
INSERT [dbo].[Shipping] ([ShippingID], [Description], [DeliveryCost]) VALUES (2, N'黑貓', CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Shipping] ([ShippingID], [Description], [DeliveryCost]) VALUES (3, N'超商', CAST(60.00 AS Decimal(10, 2)))
INSERT [dbo].[Shipping] ([ShippingID], [Description], [DeliveryCost]) VALUES (4, N'我們直接送過去', CAST(999.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Shipping] OFF
GO
SET IDENTITY_INSERT [dbo].[ShippingStatus] ON 

INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (1, N'備貨中')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (2, N'出貨前')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (3, N'已出貨')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (4, N'已到達')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (5, N'已取貨')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (6, N'退貨中')
INSERT [dbo].[ShippingStatus] ([ShippingStatusID], [Name]) VALUES (7, N'已退貨')
SET IDENTITY_INSERT [dbo].[ShippingStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (1, N'研發中')
INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (2, N'未上架')
INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (3, N'上架中')
INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (4, N'補貨中')
INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (5, N'已下架')
INSERT [dbo].[Status] ([StatusID], [Name]) VALUES (6, N'已廢棄')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[StoreLocation] ON 

INSERT [dbo].[StoreLocation] ([StoreLocationID], [Name], [OfficeTelephone], [Address]) VALUES (1, N'陽明門市', N'0912345678', N'高雄市')
INSERT [dbo].[StoreLocation] ([StoreLocationID], [Name], [OfficeTelephone], [Address]) VALUES (2, N'新莊門市', N'0912345679', N'台北市')
INSERT [dbo].[StoreLocation] ([StoreLocationID], [Name], [OfficeTelephone], [Address]) VALUES (3, N'光明門市', N'0912345672', NULL)
INSERT [dbo].[StoreLocation] ([StoreLocationID], [Name], [OfficeTelephone], [Address]) VALUES (4, N'天空門市', NULL, N'高雄市')
INSERT [dbo].[StoreLocation] ([StoreLocationID], [Name], [OfficeTelephone], [Address]) VALUES (5, N'太平洋門市', NULL, NULL)
SET IDENTITY_INSERT [dbo].[StoreLocation] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Administ__B0C3AC460D29209B]    Script Date: 2024/7/17 下午 09:09:59 ******/
ALTER TABLE [dbo].[Administrator] ADD  CONSTRAINT [UQ__Administ__B0C3AC460D29209B] UNIQUE NONCLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Member__B0C3AC462CA1DC47]    Script Date: 2024/7/17 下午 09:09:59 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [UQ__Member__B0C3AC462CA1DC47] UNIQUE NONCLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdministratorModification] ADD  DEFAULT ('') FOR [ModifierDescribe]
GO
ALTER TABLE [dbo].[AdministratorModification] ADD  DEFAULT (getdate()) FOR [ModificationTime]
GO
ALTER TABLE [dbo].[Credit] ADD  DEFAULT (getdate()) FOR [ModificationTime]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF__Member__Creation__7D439ABD]  DEFAULT (getdate()) FOR [CreationTime]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF__Member__Modifica__7E37BEF6]  DEFAULT (getdate()) FOR [ModificationTime]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF__Order__CreationT__06B7F65E]  DEFAULT (getdate()) FOR [CreationTime]
GO
ALTER TABLE [dbo].[ServiceRecord] ADD  DEFAULT (getdate()) FOR [QuestionTime]
GO
ALTER TABLE [dbo].[Administrator]  WITH CHECK ADD  CONSTRAINT [FK__Administr__Title__37461F20] FOREIGN KEY([TitleID])
REFERENCES [dbo].[AdministratorTitle] ([TitleID])
GO
ALTER TABLE [dbo].[Administrator] CHECK CONSTRAINT [FK__Administr__Title__37461F20]
GO
ALTER TABLE [dbo].[AdministratorActivitylog]  WITH CHECK ADD  CONSTRAINT [FK__Administr__Admin__383A4359] FOREIGN KEY([AdministratorID])
REFERENCES [dbo].[Administrator] ([AdministratorID])
GO
ALTER TABLE [dbo].[AdministratorActivitylog] CHECK CONSTRAINT [FK__Administr__Admin__383A4359]
GO
ALTER TABLE [dbo].[AdministratorModification]  WITH CHECK ADD  CONSTRAINT [FK__Administr__Admin__392E6792] FOREIGN KEY([AdministratorID])
REFERENCES [dbo].[Administrator] ([AdministratorID])
GO
ALTER TABLE [dbo].[AdministratorModification] CHECK CONSTRAINT [FK__Administr__Admin__392E6792]
GO
ALTER TABLE [dbo].[AdministratorModification]  WITH CHECK ADD  CONSTRAINT [FK__Administr__Modif__3A228BCB] FOREIGN KEY([ModifierID])
REFERENCES [dbo].[Administrator] ([AdministratorID])
GO
ALTER TABLE [dbo].[AdministratorModification] CHECK CONSTRAINT [FK__Administr__Modif__3A228BCB]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK__Cart__Customized__04E4BC85] FOREIGN KEY([CustomizedID])
REFERENCES [dbo].[Customized] ([CustomizedID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK__Cart__Customized__04E4BC85]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK__Cart__Inspiratio__05D8E0BE] FOREIGN KEY([InspirationID])
REFERENCES [dbo].[Inspiration] ([InspirationID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK__Cart__Inspiratio__05D8E0BE]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK__Cart__MemberID__06CD04F7] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK__Cart__MemberID__06CD04F7]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK__Cart__ProductID__07C12930] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK__Cart__ProductID__07C12930]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK__Category__Compon__08B54D69] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[Component] ([ComponentID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK__Category__Compon__08B54D69]
GO
ALTER TABLE [dbo].[Component]  WITH CHECK ADD  CONSTRAINT [FK__Component__Color__09A971A2] FOREIGN KEY([ColorID])
REFERENCES [dbo].[Color] ([ColorID])
GO
ALTER TABLE [dbo].[Component] CHECK CONSTRAINT [FK__Component__Color__09A971A2]
GO
ALTER TABLE [dbo].[Component]  WITH CHECK ADD  CONSTRAINT [FK__Component__Mater__0A9D95DB] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([MaterialID])
GO
ALTER TABLE [dbo].[Component] CHECK CONSTRAINT [FK__Component__Mater__0A9D95DB]
GO
ALTER TABLE [dbo].[Component]  WITH CHECK ADD  CONSTRAINT [FK__Component__Statu__0B91BA14] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Component] CHECK CONSTRAINT [FK__Component__Statu__0B91BA14]
GO
ALTER TABLE [dbo].[Credit]  WITH CHECK ADD  CONSTRAINT [FK__Credit__MemberID__0C85DE4D] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[Credit] CHECK CONSTRAINT [FK__Credit__MemberID__0C85DE4D]
GO
ALTER TABLE [dbo].[Customized]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Membe__0E6E26BF] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[Customized] CHECK CONSTRAINT [FK__Customize__Membe__0E6E26BF]
GO
ALTER TABLE [dbo].[Customized]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Promo__0F624AF8] FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[Customized] CHECK CONSTRAINT [FK__Customize__Promo__0F624AF8]
GO
ALTER TABLE [dbo].[CustomizedSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Color] FOREIGN KEY([ColorID])
REFERENCES [dbo].[Color] ([ColorID])
GO
ALTER TABLE [dbo].[CustomizedSpecification] CHECK CONSTRAINT [FK__Customize__Color]
GO
ALTER TABLE [dbo].[CustomizedSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Compo__10566F31] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[Component] ([ComponentID])
GO
ALTER TABLE [dbo].[CustomizedSpecification] CHECK CONSTRAINT [FK__Customize__Compo__10566F31]
GO
ALTER TABLE [dbo].[CustomizedSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Custo__114A936A] FOREIGN KEY([CustomizedID])
REFERENCES [dbo].[Customized] ([CustomizedID])
GO
ALTER TABLE [dbo].[CustomizedSpecification] CHECK CONSTRAINT [FK__Customize__Custo__114A936A]
GO
ALTER TABLE [dbo].[CustomizedSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Customize__Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([MaterialID])
GO
ALTER TABLE [dbo].[CustomizedSpecification] CHECK CONSTRAINT [FK__Customize__Material]
GO
ALTER TABLE [dbo].[FavoriteItems]  WITH CHECK ADD  CONSTRAINT [FK__FavoriteI__Custo__123EB7A3] FOREIGN KEY([CustomizedID])
REFERENCES [dbo].[Customized] ([CustomizedID])
GO
ALTER TABLE [dbo].[FavoriteItems] CHECK CONSTRAINT [FK__FavoriteI__Custo__123EB7A3]
GO
ALTER TABLE [dbo].[FavoriteItems]  WITH CHECK ADD FOREIGN KEY([FavoritesID])
REFERENCES [dbo].[Favorites] ([FavoritesID])
GO
ALTER TABLE [dbo].[FavoriteItems]  WITH CHECK ADD  CONSTRAINT [FK__FavoriteI__Inspi__14270015] FOREIGN KEY([InspirationID])
REFERENCES [dbo].[Inspiration] ([InspirationID])
GO
ALTER TABLE [dbo].[FavoriteItems] CHECK CONSTRAINT [FK__FavoriteI__Inspi__14270015]
GO
ALTER TABLE [dbo].[FavoriteItems]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK__Favorites__Membe__160F4887] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK__Favorites__Membe__160F4887]
GO
ALTER TABLE [dbo].[Inspiration]  WITH CHECK ADD  CONSTRAINT [FK__Inspirati__Promo__17F790F9] FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[Inspiration] CHECK CONSTRAINT [FK__Inspirati__Promo__17F790F9]
GO
ALTER TABLE [dbo].[InspirationSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Inspirati__Color] FOREIGN KEY([ColorID])
REFERENCES [dbo].[Color] ([ColorID])
GO
ALTER TABLE [dbo].[InspirationSpecification] CHECK CONSTRAINT [FK__Inspirati__Color]
GO
ALTER TABLE [dbo].[InspirationSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Inspirati__Compo__18EBB532] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[Component] ([ComponentID])
GO
ALTER TABLE [dbo].[InspirationSpecification] CHECK CONSTRAINT [FK__Inspirati__Compo__18EBB532]
GO
ALTER TABLE [dbo].[InspirationSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Inspirati__Inspi__19DFD96B] FOREIGN KEY([InspirationID])
REFERENCES [dbo].[Inspiration] ([InspirationID])
GO
ALTER TABLE [dbo].[InspirationSpecification] CHECK CONSTRAINT [FK__Inspirati__Inspi__19DFD96B]
GO
ALTER TABLE [dbo].[InspirationSpecification]  WITH CHECK ADD  CONSTRAINT [FK__Inspirati__Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([MaterialID])
GO
ALTER TABLE [dbo].[InspirationSpecification] CHECK CONSTRAINT [FK__Inspirati__Material]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK__Member__LevelID__1AD3FDA4] FOREIGN KEY([LevelID])
REFERENCES [dbo].[MemberLevel] ([LevelID])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK__Member__LevelID__1AD3FDA4]
GO
ALTER TABLE [dbo].[MemberActivitylog]  WITH CHECK ADD  CONSTRAINT [FK__MemberAct__Membe__1BC821DD] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[MemberActivitylog] CHECK CONSTRAINT [FK__MemberAct__Membe__1BC821DD]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__MemberID__01F34141] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__MemberID__01F34141]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__OrderStat__02E7657A] FOREIGN KEY([OrderStatusID])
REFERENCES [dbo].[OrderStatus] ([OrderStatusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__OrderStat__02E7657A]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__PaymentSt__03DB89B3] FOREIGN KEY([PaymentStatusID])
REFERENCES [dbo].[PaymentStatus] ([PaymentStatusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__PaymentSt__03DB89B3]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__PromoCode__05C3D225] FOREIGN KEY([PromoCodeID])
REFERENCES [dbo].[PromoCode] ([PromoCodeID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__PromoCode__05C3D225]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__ShippingS__04CFADEC] FOREIGN KEY([ShippingStatusID])
REFERENCES [dbo].[ShippingStatus] ([ShippingStatusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__ShippingS__04CFADEC]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payment] ([PaymentID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Shipping] FOREIGN KEY([ShippingID])
REFERENCES [dbo].[Shipping] ([ShippingID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Shipping]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Custo__245D67DE] FOREIGN KEY([CustomizedID])
REFERENCES [dbo].[Customized] ([CustomizedID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Custo__245D67DE]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Inspi__25518C17] FOREIGN KEY([InspirationID])
REFERENCES [dbo].[Inspiration] ([InspirationID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Inspi__25518C17]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__09946309] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__09946309]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Produ__2739D489] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Produ__2739D489]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[ProductSpecification]  WITH CHECK ADD  CONSTRAINT [FK__ProductSp__Color] FOREIGN KEY([ColorID])
REFERENCES [dbo].[Color] ([ColorID])
GO
ALTER TABLE [dbo].[ProductSpecification] CHECK CONSTRAINT [FK__ProductSp__Color]
GO
ALTER TABLE [dbo].[ProductSpecification]  WITH CHECK ADD  CONSTRAINT [FK__ProductSp__Compo__2A164134] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[Component] ([ComponentID])
GO
ALTER TABLE [dbo].[ProductSpecification] CHECK CONSTRAINT [FK__ProductSp__Compo__2A164134]
GO
ALTER TABLE [dbo].[ProductSpecification]  WITH CHECK ADD  CONSTRAINT [FK__ProductSp__Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([MaterialID])
GO
ALTER TABLE [dbo].[ProductSpecification] CHECK CONSTRAINT [FK__ProductSp__Material]
GO
ALTER TABLE [dbo].[ProductSpecification]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[ServiceRecord]  WITH CHECK ADD  CONSTRAINT [FK__ServiceRe__Admin__650CE9D0] FOREIGN KEY([AdministratorID])
REFERENCES [dbo].[Administrator] ([AdministratorID])
GO
ALTER TABLE [dbo].[ServiceRecord] CHECK CONSTRAINT [FK__ServiceRe__Admin__650CE9D0]
GO
ALTER TABLE [dbo].[ServiceRecord]  WITH CHECK ADD  CONSTRAINT [FK__ServiceRe__Membe__2CF2ADDF] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
GO
ALTER TABLE [dbo].[ServiceRecord] CHECK CONSTRAINT [FK__ServiceRe__Membe__2CF2ADDF]
GO
USE [master]
GO
ALTER DATABASE [modpack] SET  READ_WRITE 
GO
