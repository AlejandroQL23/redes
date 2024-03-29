USE [master]
GO
/****** Object:  Database [contagiaDOSredes]    Script Date: 19/7/2021 09:42:28 ******/
CREATE DATABASE [contagiaDOSredes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'contagiaDOSredes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\contagiaDOSredes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'contagiaDOSredes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\contagiaDOSredes_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [contagiaDOSredes] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [contagiaDOSredes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [contagiaDOSredes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET ARITHABORT OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [contagiaDOSredes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [contagiaDOSredes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [contagiaDOSredes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [contagiaDOSredes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [contagiaDOSredes] SET  MULTI_USER 
GO
ALTER DATABASE [contagiaDOSredes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [contagiaDOSredes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [contagiaDOSredes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [contagiaDOSredes] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [contagiaDOSredes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [contagiaDOSredes] SET QUERY_STORE = OFF
GO
USE [contagiaDOSredes]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [contagiaDOSredes]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 19/7/2021 09:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[gameId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[owner] [nvarchar](20) NULL,
	[password] [nvarchar](20) NULL,
	[status] [nvarchar](20) NULL,
	[players] [nvarchar](max) NULL,
	[temporalp] [nvarchar](50) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[gameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 19/7/2021 09:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[roundId] [int] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 19/7/2021 09:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[gameId] [int] NULL,
	[psycho] [bit] NULL,
 CONSTRAINT [PK_PLAYER] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Round]    Script Date: 19/7/2021 09:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Round](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[leader] [nvarchar](20) NULL,
	[psychowin] [bit] NULL,
	[gameId] [int] NULL,
	[roundNumber] [int] NULL,
 CONSTRAINT [PK_ROUND] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Game] ON 

INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (1, N'Buscaminas', N'heidi', N'145', N'Leader', N'heidi , Olarte , Matusalen  , Lolito , Fargan , Rabis , Luzu , Pinocho', N'Pinocho ')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (13, N'Ecomoda', N'Betty', N'456', N'Lobby', N'Betty', NULL)
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (14, N'AlejandroServer', N'Alejandro', N'123', NULL, N' , Sara', NULL)
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (15, N'CServer', N'Cbastian', N'123', NULL, N' ', N'')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (16, N'Ventilador', N'Goku', N'1234', N'Leader', N'Goku , heidi , Rabis , Olarte , Luzu', N'Luzu')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (17, N'Olimpo', N'Yo', N'1234', N'Ended', N'Yo, Zeus, Hera, Poseidon, Ares', NULL)
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (18, N'Telettubielandia', N'Sol', N'666', N'Ended', N'Sol , Po , Lala , TinkieWinkie , Dipsie', N'Dipsie')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (19, N'Bosque', N'HelenaNito', N'456', N'Lobby', N'HelenaNito', NULL)
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (20, N'Pepegrillo', N'Pinocho', N'789', NULL, N'Pinocho , Goku , heidi , Rabis , Olarte , Pinocho', N'Pinocho ')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (21, N'ArnoldoServer', N'Arnoldo', N'123', N'Ended', N'Arnoldo , a , b , c , d', N'd')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (22, N'MarvinServer', N'Marvin', N'123', N'Leader', N'Marvin , 1 , 2 , 3 , 4', N'4')
INSERT [dbo].[Game] ([gameId], [name], [owner], [password], [status], [players], [temporalp]) VALUES (23, N'PedroServer', N'Pedro', N'123', N'Leader', N'Pedro , z , x , c , v', N'v')
SET IDENTITY_INSERT [dbo].[Game] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (19, N'Rabis', 29)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (20, N'Olarte', 29)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (21, N'Olarte', 39)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (22, N'Rabis', 39)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (23, N'Olarte', 40)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (24, N'Luzu', 40)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (25, N'Goku', 40)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (26, N'Goku', 41)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (27, N'Olarte', 41)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (28, N'Olarte', 42)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (29, N'Rabis', 42)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (30, N'Goku', 42)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (34, N'Zeus', 45)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (35, N'Hera', 45)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (47, N'Zeus', 46)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (48, N'Yo', 46)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (49, N'Hera', 46)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (54, N'Ares', 49)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (55, N'Hera', 49)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (58, N'Sol', 53)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (59, N'Dipsie', 53)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (60, N'Lala', 53)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (61, N'Lala', 54)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (62, N'Dipsie', 54)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (63, N'Olarte', 55)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (64, N'Rabis', 55)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (65, N'a', 58)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (66, N'Arnoldo', 58)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (67, N'a', 59)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (68, N'b', 59)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (69, N'c', 59)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (70, N'd', 60)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (71, N'b', 60)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (72, N'c', 61)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (73, N'd', 61)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (74, N'b', 61)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (75, N'Pedro', 63)
INSERT [dbo].[Group] ([id], [name], [roundId]) VALUES (76, N'z', 63)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Player] ON 

INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (74, N'green goo', 13, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (75, N'Payaso frufru', 13, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (81, N'Alejandro', 14, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (82, N'FOFO', 14, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (84, N'Sara', 14, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (85, N'heidi', 1, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (87, N'Seven7UP', 14, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (91, N'GRanada', 14, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (93, N'JorgeVega', 15, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (94, N'KtorC', 15, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (96, N'Olarte', 1, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (97, N'Matusalen', 1, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (99, N'Lolito', 1, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (100, N'Fargan', 1, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (106, N'Rabis', 1, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (109, N'Luzu', 1, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (110, N'Goku', 16, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (111, N'heidi', 16, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (112, N'Rabis', 16, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (113, N'Olarte', 16, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (114, N'Luzu', 16, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (115, N'Yo', 17, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (116, N'Zeus', 17, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (117, N'Poseidon', 17, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (118, N'Hera', 17, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (119, N'Ares', 17, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (120, N'Sol', 18, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (121, N'Po', 18, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (122, N'Lala', 18, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (123, N'TinkieWinkie', 18, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (124, N'Dipsie', 18, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (125, N'HelenaNito', 19, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (126, N'Pinocho', 20, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (127, N'Goku', 20, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (128, N'heidi', 20, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (129, N'Rabis', 20, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (130, N'Olarte', 20, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (131, N'Pinocho', 20, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (133, N'Arnoldo', 21, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (134, N'a', 21, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (135, N'b', 21, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (136, N'c', 21, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (137, N'd', 21, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (138, N'Marvin', 22, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (139, N'1', 22, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (140, N'2', 22, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (141, N'3', 22, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (142, N'4', 22, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (143, N'Pedro', 23, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (144, N'z', 23, 1)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (145, N'x', 23, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (146, N'c', 23, 0)
INSERT [dbo].[Player] ([id], [name], [gameId], [psycho]) VALUES (147, N'v', 23, 0)
SET IDENTITY_INSERT [dbo].[Player] OFF
GO
SET IDENTITY_INSERT [dbo].[Round] ON 

INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (23, N'Payaso frufru', 0, 13, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (24, N'green goo', 0, 13, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (29, N'Luzu', 0, 1, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (38, N'Rabis', 0, 1, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (39, N'heidi', 1, 16, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (40, N'Luzu', 0, 16, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (41, N'Olarte', 0, 16, 2)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (42, N'heidi', 1, 16, 3)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (45, N'Poseidon', 1, 17, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (46, N'Yo', 1, 17, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (49, N'Ares', 1, 17, 2)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (50, N'Goku', 0, 16, 4)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (51, N'Olarte', 0, 16, 5)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (52, N'TinkieWinkie', 0, 18, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (53, N'Sol', 0, 18, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (54, N'Lala', 0, 18, 2)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (55, N'Pinocho', 0, 20, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (56, N'Rabis', 0, 20, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (57, N'Luzu', 0, 1, 2)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (58, N'a', 1, 21, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (59, N'b', 0, 21, 1)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (60, N'Arnoldo', 0, 21, 2)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (61, N'c', 0, 21, 3)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (62, N'2', 0, 22, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (63, N'z', 1, 23, 0)
INSERT [dbo].[Round] ([id], [leader], [psychowin], [gameId], [roundNumber]) VALUES (64, N'v', 0, 23, 1)
SET IDENTITY_INSERT [dbo].[Round] OFF
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Round] FOREIGN KEY([roundId])
REFERENCES [dbo].[Round] ([id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Round]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Game] FOREIGN KEY([gameId])
REFERENCES [dbo].[Game] ([gameId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Game]
GO
ALTER TABLE [dbo].[Round]  WITH CHECK ADD  CONSTRAINT [FK_Round_Game] FOREIGN KEY([gameId])
REFERENCES [dbo].[Game] ([gameId])
GO
ALTER TABLE [dbo].[Round] CHECK CONSTRAINT [FK_Round_Game]
GO
USE [master]
GO
ALTER DATABASE [contagiaDOSredes] SET  READ_WRITE 
GO
