USE [master]
GO
/****** Object:  Database [ModelDB]    Script Date: 5/18/2025 1:21:12 AM ******/
CREATE DATABASE [ModelDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudentDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\StudentDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\StudentDB_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ModelDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ModelDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ModelDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ModelDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ModelDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ModelDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ModelDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ModelDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ModelDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ModelDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ModelDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ModelDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ModelDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ModelDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ModelDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ModelDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ModelDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ModelDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ModelDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ModelDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ModelDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ModelDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ModelDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ModelDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ModelDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ModelDB] SET  MULTI_USER 
GO
ALTER DATABASE [ModelDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ModelDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ModelDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ModelDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ModelDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ModelDB', N'ON'
GO
USE [ModelDB]
GO
/****** Object:  User [test]    Script Date: 5/18/2025 1:21:12 AM ******/
CREATE USER [test] FOR LOGIN [test] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[AuditLog]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[AuditLogId] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[PrimaryKeyValue] [nvarchar](max) NULL,
	[Action] [nvarchar](50) NULL,
	[ControllerName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[AuditLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[ItemInUse] [bit] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterData]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterData](
	[MasterDataId] [int] IDENTITY(1,1) NOT NULL,
	[MasterDataName] [nvarchar](50) NOT NULL,
	[ColumnsNumberCount] [int] NOT NULL,
	[InUse] [bit] NOT NULL,
 CONSTRAINT [PK_TbData] PRIMARY KEY CLUSTERED 
(
	[MasterDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleISActive] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectedItems]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectedItems](
	[SelectedItemsId] [int] IDENTITY(1,1) NOT NULL,
	[MasterDataId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
 CONSTRAINT [PK_TbDataItem] PRIMARY KEY CLUSTERED 
(
	[SelectedItemsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectedNumber]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectedNumber](
	[SelectedNumberId] [int] IDENTITY(1,1) NOT NULL,
	[MasterDataId] [int] NOT NULL,
	[ColumnsNumber] [int] NOT NULL,
 CONSTRAINT [PK_TbDataNumber] PRIMARY KEY CLUSTERED 
(
	[SelectedNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserFullName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[ISActive] [bit] NULL,
	[CrDate] [datetime] NULL,
	[CrUserId] [int] NULL,
	[Up_Date] [datetime] NULL,
	[UpUserID] [int] NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CrDate] [datetime] NULL,
	[CrUser] [int] NULL,
	[Up_Date] [datetime] NULL,
	[UPUserId] [int] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/18/2025 1:21:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (1, N'دفتر', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (2, N'قلم', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (3, N'رصاص', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (4, N'ممحاة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (5, N'أقلام تظليل', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (6, N'دباسة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (7, N'مشابك ورق', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (8, N'مجلد', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (9, N'ظرف', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (10, N'ورق طباعة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (11, N'مقص', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (12, N'لاصق', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (13, N'مسطرة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (14, N'آلة حاسبة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (15, N'مصباح مكتبي', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (16, N'ذاكرة USB', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (17, N'فأرة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (18, N'لوحة مفاتيح', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (19, N'حليب', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (20, N'خبز', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (21, N'بيض', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (22, N'زبدة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (23, N'جبن', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (24, N'أرز', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (25, N'سكر', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (26, N'ملح', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (27, N'قهوة', 1)
INSERT [dbo].[Items] ([ItemId], [ItemName], [ItemInUse]) VALUES (28, N'شاي', 1)
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName], [RoleISActive]) VALUES (1, N'admin', 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserID], [UserName], [UserFullName], [PassWord], [ISActive], [CrDate], [CrUserId], [Up_Date], [UpUserID]) VALUES (1, N'admin', N'admin', N'admin', 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserID], [RoleID], [CrDate], [CrUser], [Up_Date], [UPUserId]) VALUES (1, 1, 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250515182500_InitialDbMigration', N'6.0.0')
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_ItemInUse]  DEFAULT ((1)) FOR [ItemInUse]
GO
ALTER TABLE [dbo].[MasterData] ADD  CONSTRAINT [DF_TbData_InUse]  DEFAULT ((1)) FOR [InUse]
GO
ALTER TABLE [dbo].[SelectedItems]  WITH CHECK ADD  CONSTRAINT [FK_TbDataItem_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ItemId])
GO
ALTER TABLE [dbo].[SelectedItems] CHECK CONSTRAINT [FK_TbDataItem_Items]
GO
ALTER TABLE [dbo].[SelectedItems]  WITH CHECK ADD  CONSTRAINT [FK_TbDataItem_TbMasterData] FOREIGN KEY([MasterDataId])
REFERENCES [dbo].[MasterData] ([MasterDataId])
GO
ALTER TABLE [dbo].[SelectedItems] CHECK CONSTRAINT [FK_TbDataItem_TbMasterData]
GO
ALTER TABLE [dbo].[SelectedNumber]  WITH CHECK ADD  CONSTRAINT [FK_SelectedNumber_MasterData] FOREIGN KEY([MasterDataId])
REFERENCES [dbo].[MasterData] ([MasterDataId])
GO
ALTER TABLE [dbo].[SelectedNumber] CHECK CONSTRAINT [FK_SelectedNumber_MasterData]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Roles]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([UserID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_UserInfo]
GO
USE [master]
GO
ALTER DATABASE [ModelDB] SET  READ_WRITE 
GO
