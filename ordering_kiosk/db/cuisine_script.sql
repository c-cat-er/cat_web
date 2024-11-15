USE [master]
GO
/****** Object:  Database [playfood]    Script Date: 2024/7/17 下午 08:56:06 ******/
CREATE DATABASE [playfood]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'thirdMidterm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\thirdMidterm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'thirdMidterm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\thirdMidterm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [playfood] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [playfood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [playfood] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [playfood] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [playfood] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [playfood] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [playfood] SET ARITHABORT OFF 
GO
ALTER DATABASE [playfood] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [playfood] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [playfood] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [playfood] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [playfood] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [playfood] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [playfood] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [playfood] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [playfood] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [playfood] SET  DISABLE_BROKER 
GO
ALTER DATABASE [playfood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [playfood] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [playfood] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [playfood] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [playfood] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [playfood] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [playfood] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [playfood] SET RECOVERY FULL 
GO
ALTER DATABASE [playfood] SET  MULTI_USER 
GO
ALTER DATABASE [playfood] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [playfood] SET DB_CHAINING OFF 
GO
ALTER DATABASE [playfood] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [playfood] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [playfood] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [playfood] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'playfood', N'ON'
GO
ALTER DATABASE [playfood] SET QUERY_STORE = ON
GO
ALTER DATABASE [playfood] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [playfood]
GO
/****** Object:  Table [dbo].[ColdFood]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColdFood](
	[CID] [int] NOT NULL,
	[Cname] [nvarchar](50) NULL,
	[Cprice] [int] NULL,
	[Corigin] [nvarchar](50) NULL,
	[Cdesc] [nvarchar](100) NULL,
	[Cimg] [nvarchar](50) NULL,
 CONSTRAINT [PK_ColdFood] PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DessertFood]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DessertFood](
	[DID] [int] NOT NULL,
	[Dname] [nvarchar](50) NULL,
	[Dprice] [int] NULL,
	[Dorigin] [nvarchar](50) NULL,
	[Ddesc] [nvarchar](100) NULL,
	[Dimg] [nvarchar](50) NULL,
 CONSTRAINT [PK_DessertFood] PRIMARY KEY CLUSTERED 
(
	[DID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialTable]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialTable](
	[DaID] [int] IDENTITY(1,1) NOT NULL,
	[DaDate] [date] NULL,
	[DaRevenue] [int] NULL,
 CONSTRAINT [PK_FinancialTable] PRIMARY KEY CLUSTERED 
(
	[DaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HotFood]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotFood](
	[HID] [int] NOT NULL,
	[Hname] [nvarchar](50) NULL,
	[Hprice] [int] NULL,
	[Horigin] [nvarchar](50) NULL,
	[Hdesc] [nvarchar](100) NULL,
	[Himg] [nvarchar](50) NULL,
 CONSTRAINT [PK_HotFood] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MID] [int] IDENTITY(1,1) NOT NULL,
	[會員帳號姓名] [nvarchar](50) NULL,
	[會員姓名] [nvarchar](50) NULL,
	[會員身分證號碼] [char](10) NULL,
	[會員電話] [char](10) NULL,
	[會員地址] [nvarchar](50) NULL,
	[會員Email] [nvarchar](50) NULL,
	[會員生日] [date] NULL,
	[會員等級] [int] NULL,
	[會員點數] [int] NULL,
	[會員頭像] [nvarchar](50) NULL,
	[會員登入帳號] [int] NULL,
	[會員登入密碼] [int] NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OID] [int] IDENTITY(1,1) NOT NULL,
	[訂購人姓名] [nvarchar](50) NULL,
	[訂購人電話] [nchar](10) NULL,
	[訂購人地址] [nvarchar](50) NULL,
	[下訂日] [date] NULL,
	[訂單項目] [nvarchar](1000) NULL,
	[訂單金額] [int] NULL,
	[預計出貨日] [date] NULL,
	[預計到貨日] [date] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 2024/7/17 下午 08:56:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PID] [int] IDENTITY(1,1) NOT NULL,
	[管理者姓名] [nvarchar](50) NULL,
	[管理者電話] [char](10) NULL,
	[管理者地址] [nvarchar](50) NULL,
	[管理者Email] [nvarchar](50) NULL,
	[管理者生日] [date] NULL,
	[管理者婚姻狀態] [bit] NULL,
	[管理者權限] [int] NULL,
	[管理者登入帳號] [int] NULL,
	[管理者登入密碼] [int] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (1, N'杏仁豆腐', 60, NULL, NULL, N'img2-1.jpg')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (2, N'豆花', 30, NULL, NULL, N'img2-2.png')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (3, N'韓國冰酥', 70, NULL, NULL, N'img2-3.jpg')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (4, N'霜淇淋', 30, NULL, NULL, N'img2-4.jpg')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (5, N'包心粉圓芒果冰', 70, N'魏姊', NULL, N'img2-5.jpg')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (6, N'凍芒果西米露', 60, NULL, NULL, N'img2-6.jpg')
INSERT [dbo].[ColdFood] ([CID], [Cname], [Cprice], [Corigin], [Cdesc], [Cimg]) VALUES (7, N'珍珠奶茶', 40, NULL, NULL, N'img2-7.jpg')
GO
INSERT [dbo].[DessertFood] ([DID], [Dname], [Dprice], [Dorigin], [Ddesc], [Dimg]) VALUES (1, N'白糖粿', 30, NULL, NULL, N'img3-1.jpg')
INSERT [dbo].[DessertFood] ([DID], [Dname], [Dprice], [Dorigin], [Ddesc], [Dimg]) VALUES (2, N'鹹焦糖爆米花百香果雪貝', 80, NULL, NULL, N'img3-2.jpg')
INSERT [dbo].[DessertFood] ([DID], [Dname], [Dprice], [Dorigin], [Ddesc], [Dimg]) VALUES (3, N'獨角獸熊棉花糖', 80, NULL, NULL, N'img3-3.jpg')
INSERT [dbo].[DessertFood] ([DID], [Dname], [Dprice], [Dorigin], [Ddesc], [Dimg]) VALUES (4, N'豬血糕', 30, N'台北小林', NULL, N'img3-4.jpg')
GO
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (1, N'鴛鴦鍋', 140, NULL, NULL, N'img1-1.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (2, N'肉燥飯', 30, NULL, NULL, N'img1-2.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (3, N'米糕', 35, NULL, NULL, N'img1-3.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (4, N'阿婆魯麵', 50, NULL, NULL, N'img1-4.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (5, N'烤餅', 30, NULL, NULL, N'img1-5.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (6, N'碗粿', 45, NULL, NULL, N'img1-6.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (7, N'擔仔麵', 55, NULL, NULL, N'img1-7.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (8, N'鴨肉飯', 60, NULL, NULL, N'img1-8.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (9, N'蚵仔煎', 55, NULL, NULL, N'img1-9.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (10, N'蒸餃', 70, NULL, NULL, N'img1-10.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (11, N'台北金峰滷肉飯', 40, NULL, NULL, N'img1-11.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (12, N'台北欣葉三杯雞', 60, NULL, NULL, N'img1-12.png')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (13, N'鹹粥', 50, NULL, NULL, N'img1-13.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (14, N'牛肉麵', 80, NULL, NULL, N'img1-14.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (15, N'台北師大夜市蔥抓餅', 25, NULL, NULL, N'img1-15.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (16, N'台北JamesKitchen虱目魚', 80, NULL, NULL, N'img1-16.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (17, N'台北鴨肉扁鵝肉', 70, NULL, NULL, N'img1-17.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (18, N'赤肉羹', 50, NULL, NULL, N'img1-18.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (19, N'虱目魚湯', 60, NULL, NULL, N'img1-19.jpg')
INSERT [dbo].[HotFood] ([HID], [Hname], [Hprice], [Horigin], [Hdesc], [Himg]) VALUES (20, N'台北林東芳牛肉麵', 80, NULL, NULL, N'img1-20.png')
GO
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (1, N'1', N'1', N'1         ', N'1         ', N'1', N'1', CAST(N'1996-03-05' AS Date), 100, 1, N'會員頭像1.png', 1, 1)
INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (2, N'2', N'2', N'2         ', N'2         ', N'2', N'2', CAST(N'1989-07-23' AS Date), 200, 2, N'會員頭像2.png', 2, 2)
INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (3, N'3', N'3', N'3         ', N'3         ', N'3', N'3', CAST(N'1976-08-11' AS Date), 300, 3, N'會員頭像3.png', 3, 3)
INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (4, N'4', N'4', N'4         ', N'4         ', N'4', N'4', CAST(N'1995-06-04' AS Date), 300, 4, N'會員頭像4.png', 4, 4)
INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (5, N'5', N'5', N'5         ', N'5         ', N'5', N'5', CAST(N'2001-08-26' AS Date), 400, 5, N'會員頭像5.png', 5, 5)
INSERT [dbo].[Members] ([MID], [會員帳號姓名], [會員姓名], [會員身分證號碼], [會員電話], [會員地址], [會員Email], [會員生日], [會員等級], [會員點數], [會員頭像], [會員登入帳號], [會員登入密碼]) VALUES (6, N'6', N'6', N'6         ', N'6         ', N'6', N'6', CAST(N'2004-10-05' AS Date), 400, 6, N'會員頭像6.png', 6, 6)
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (2, N'2', N'2         ', N'2', CAST(N'2023-12-03' AS Date), N'2', 1, CAST(N'2023-12-03' AS Date), CAST(N'1992-05-09' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (3, N'3', N'3         ', N'3', CAST(N'2023-12-03' AS Date), N'3', 1, CAST(N'2023-12-03' AS Date), CAST(N'1979-07-22' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1007, N'2', N'2         ', N'2', CAST(N'2023-12-03' AS Date), N'獨角獸熊棉花糖 - 1 份 - 共 80 元, 豆花 - 1 份 - 共 30 元, 鴛鴦鍋 - 1 份 - 共 140 元, 肉燥飯 - 4 份 - 共 120 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1008, N'5', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'鴛鴦鍋 - 4 份 - 共 560 元, 肉燥飯 - 1 份 - 共 30 元, 豆花 - 1 份 - 共 30 元, 韓國冰酥 - 1 份 - 共 70 元, 獨角獸熊棉花糖 - 1 份 - 共 80 元, 鹹焦糖爆米花百香果雪貝 - 1 份 - 共 80 元, 杏仁豆腐 - 1 份 - 共 60 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1009, N'f', N'f         ', N'f', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 鴛鴦鍋 - 1 份 - 共 140 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1010, N'1', N'2         ', N'5', CAST(N'2023-12-03' AS Date), N'米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1011, N'4', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1012, N'5', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'鴛鴦鍋 - 1 份 - 共 140 元, 肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1013, N'5', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1014, N'5', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1015, N'f', N'f         ', N'f', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1016, N'f', N'f         ', N'f', CAST(N'2023-12-03' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1017, N'5', N'5         ', N'5', CAST(N'2023-12-03' AS Date), N'米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1018, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1019, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1020, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-03' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1022, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1023, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1024, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1025, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1026, N'JJ', N'J         ', N'J', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1027, N'JJ', N'J         ', N'J', CAST(N'2023-12-04' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1028, N'JJ', N'J         ', N'J', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1029, N'JJ', N'J         ', N'J', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1030, N'JJ', N'J         ', N'J', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1031, N'J', N'J         ', N'J', CAST(N'2023-12-05' AS Date), N'米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1032, N'j', N'jj        ', N'j', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1033, N'h', N'h         ', N'h', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1034, N'd', N'd         ', N'd', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1035, N'f', N'f         ', N'f', CAST(N'2023-12-05' AS Date), N'肉燥飯 - 1 份 - 共 30 元, 米糕 - 1 份 - 共 35 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1036, N'2', N'3         ', N'2', CAST(N'2023-12-05' AS Date), N'碗粿 - 1 份 - 共 45 元, 烤餅 - 1 份 - 共 30 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1037, N'2', N'2         ', N'2', CAST(N'2023-12-05' AS Date), N'碗粿 - 1 份 - 共 45 元, 烤餅 - 1 份 - 共 30 元, 蚵仔煎 - 1 份 - 共 55 元, 米糕 - 1 份 - 共 35 元, 肉燥飯 - 1 份 - 共 30 元, 鴨肉飯 - 1 份 - 共 60 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1038, N'2', N'2         ', N'2', CAST(N'2023-12-05' AS Date), N'碗粿 - 1 份 - 共 45 元, 烤餅 - 1 份 - 共 30 元, 蚵仔煎 - 1 份 - 共 55 元, 米糕 - 1 份 - 共 35 元, 肉燥飯 - 1 份 - 共 30 元, 鴨肉飯 - 1 份 - 共 60 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (1039, N'2', N'2         ', N'2', CAST(N'2023-12-05' AS Date), N'碗粿 - 1 份 - 共 45 元, 烤餅 - 1 份 - 共 30 元, 蚵仔煎 - 1 份 - 共 55 元, 米糕 - 1 份 - 共 35 元, 肉燥飯 - 1 份 - 共 30 元, 鴨肉飯 - 1 份 - 共 60 元', 1, CAST(N'2023-12-05' AS Date), CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Orders] ([OID], [訂購人姓名], [訂購人電話], [訂購人地址], [下訂日], [訂單項目], [訂單金額], [預計出貨日], [預計到貨日]) VALUES (2040, N'1', N'1         ', N'1', CAST(N'2023-12-04' AS Date), N'碗粿 - 1 份 - 共 45 元', 0, CAST(N'2023-12-04' AS Date), CAST(N'2023-12-04' AS Date))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (1, N'1', N'1         ', N'1', N'aa@gmail.com', CAST(N'1995-05-03' AS Date), 1, 10, 1, 1)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (2, N'2', N'2         ', N'2', N'bb@gmail.com', CAST(N'1997-12-23' AS Date), 0, 10, 2, 2)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (3, N'3', N'3         ', N'3', N'cc@gmail.com', CAST(N'2023-11-28' AS Date), 0, 20, 3, 3)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (5, N'4', N'4         ', N'4', N'dd@gmail.com', CAST(N'2021-10-23' AS Date), 1, 20, 4, 4)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (6, N'5', N'5         ', N'5', N'ee@gmail.com', CAST(N'1998-02-03' AS Date), 1, 30, 5, 5)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (7, N'6', N'6         ', N'6', N'a1@gmail.com', CAST(N'1897-02-22' AS Date), 1, 40, 6, 6)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (8, N'7', N'7         ', N'7', N'a2@', CAST(N'1987-12-09' AS Date), 0, 40, 7, 7)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (9, N'8', N'8         ', N'8', N'a3@', CAST(N'1988-08-11' AS Date), 1, 40, 8, 8)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (10, N'9', N'9         ', N'9', N'a4@', CAST(N'1999-07-23' AS Date), 0, 40, 9, 9)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (11, N'10', N'10        ', N'10', N'a5@', CAST(N'1898-06-24' AS Date), 0, 40, 10, 10)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (12, N'11', N'a111111111', N'11', N'1@', CAST(N'2023-06-27' AS Date), 1, 40, NULL, NULL)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (13, N'2', N'3         ', N'2', N'bb@gmail.com', CAST(N'1997-12-23' AS Date), 0, 10, NULL, NULL)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (14, N'3', N'3         ', N'3', N'cc@gmail.com', CAST(N'2023-11-28' AS Date), 0, 20, NULL, NULL)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (15, N'2', N'2         ', N'2', N'bb@gmail.com', CAST(N'1997-12-23' AS Date), 0, 10, NULL, NULL)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (16, N'3', N'3         ', N'3', N'cc@gmail.com', CAST(N'2023-11-28' AS Date), 0, 20, NULL, NULL)
INSERT [dbo].[Persons] ([PID], [管理者姓名], [管理者電話], [管理者地址], [管理者Email], [管理者生日], [管理者婚姻狀態], [管理者權限], [管理者登入帳號], [管理者登入密碼]) VALUES (17, N'2', N'2         ', N'2', N'bb@gmail.com', CAST(N'1997-12-23' AS Date), 0, 10, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
USE [master]
GO
ALTER DATABASE [playfood] SET  READ_WRITE 
GO
