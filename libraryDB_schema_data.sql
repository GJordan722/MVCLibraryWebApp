USE [master]
GO

/****** Object:  Database [LibraryDB]    Script Date: 4/26/2022 1:43:07 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [LibraryDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [LibraryDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [LibraryDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [LibraryDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [LibraryDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [LibraryDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [LibraryDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [LibraryDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [LibraryDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [LibraryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [LibraryDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [LibraryDB] SET  MULTI_USER 
GO

ALTER DATABASE [LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [LibraryDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [LibraryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [LibraryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [LibraryDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [LibraryDB] SET  READ_WRITE 
GO

USE [LibraryDB]
GO

/****** Object:  Table [dbo].[Media]    Script Date: 4/26/2022 1:43:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Media](
	[media_id] [int] NOT NULL,
	[media_name] [nvarchar](100) NOT NULL,
	[media_type] [nvarchar](40) NOT NULL,
	[account_id] [int] NULL,
	[author] [nvarchar](200) NULL,
	[Publisher] [nvarchar](200) NULL
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[media_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [LibraryDB]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 4/26/2022 1:43:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[role_id] [int] NOT NULL,
	[role_name] [nvarchar](10) NOT NULL
CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [LibraryDB]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 4/26/2022 1:43:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[account_id] [int] NOT NULL,
	[username] [nvarchar](30) NOT NULL,
	[password] [nvarchar](20) NOT NULL,
	[active] [int] NULL,
	[role_id] [int] NOT NULL
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [LibraryDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserDetails](
	[email] [nvarchar](100) NOT NULL,
	[first_name] [nvarchar](100) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[account_id] [int] NOT NULL
 CONSTRAINT [PK_UsersDetails] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [LibraryDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExceptionLogging](
	[ExceptionLoggingID] [int] IDENTITY(1,1) NOT NULL,
	[StackTrace] [nvarchar](1000) NULL,
	[Message] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Url] [nvarchar](100) NULL,
	[LogDate] [datetime] NOT NULL,
 CONSTRAINT [PK__ExeceptionLogging] PRIMARY KEY CLUSTERED
(
	[ExceptionLoggingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ExceptionLogging] ADD  CONSTRAINT [DF_ExceptionLogging_LogDate]  DEFAULT (getdate()) FOR [LogDate]
GO


INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (1,N'Admin')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (2,N'Librarian')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (3,N'Patron')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (4,N'Guest')



INSERT [dbo].[Users] ([account_id],[username],[password],[role_id]) VALUES (100,N'Admin123',N'password123',1)
INSERT [dbo].[Users] ([account_id],[username],[password],[role_id]) VALUES (101, N'LibKate',N'kateLiberarian',2)

INSERT [dbo].[UserDetails] ([email],[first_name],[last_name],[account_id]) VALUES (N'admin123@admin.com',N'Addy',N'Man',100)
INSERT [dbo].[UserDetails] ([email],[first_name],[last_name],[account_id]) VALUES (N'libkate@library.com',N'Kate',N'Liber',101)

INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (1,N'The Divine Comedy',N'Book',NULL,N'Dante Alighieri',N'Penguin Publishing Group')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (2,N'The Journey to the West, Revised Edition, Volume 1',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (3,N'The Journey to the West, Revised Edition, Volume 2',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (4,N'The Journey to the West, Revised Edition, Volume 3',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (5,N'The Journey to the West, Revised Edition, Volume 4',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[userCreate](
	@account_id int,
	@username nvarchar(30),
	@password nvarchar(20),
	@role_id int,
	@email nvarchar(100),
	@first_name nvarchar(100),
	@last_name nvarchar(100))
	as
	BEGIN
		BEGIN
		insert into Users(account_id,username,[password],role_id)
			values(@account_id,@username,@password,@role_id)
		END
		BEGIN
		Insert into UserDetails(email,first_name,last_name,account_id)
			Values(@email,@first_name,@last_name,@account_id)
		END
		SELECT 'NumberRecordsAffected'=@@ROWCOUNT
	END

SET ANSI_NULLS ON
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[deleteUser](
	@account_id int,
	@username nvarchar(30),
	@password nvarchar(20))
	as
	BEGIN
		DELETE FROM [dbo].[Users] WHERE [account_id]=@account_id AND [username] = @username AND [password] = @password
		DELETE FROM [dbo].UserDetails WHERE [account_id]=@account_id
		SELECT 'NumberRecordsAffected'=@@ROWCOUNT
	END




/****** Object:  StoredProcedure [dbo].[checkUserValid]    Script Date: 4/30/2022 12:02:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[checkUserValid](
	@username nvarchar(30),
	@password nvarchar(20) = null)
	as
	BEGIN
	DECLARE @isValid int;
	IF ((SELECT COUNT(*) FROM Users WHERE EXISTS (SELECT [username] = @username, [password] = @password) ) > 0)
		Begin
		SET @isValid = 1; 
		END

	ELSE
		BEGIN
		SET @isValid = 0;
		END
	IF(@isValid = 1)
	BEGIN
		select * FROM [dbo].[Users] U FULL JOIN UserDetails UD ON U.account_id = UD.account_id  WHERE [username] = @username AND [password] = @password 	END

	ELSE
	BEGIN
		SELECT 'No Account Found' = -1
	END
	
	END

/****** Object:  StoredProcedure [dbo].[updateMedia]    Script Date: 5/1/2022 6:29:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[checkIO](
	@media_id int,
	@media_type nvarchar(40),
	@account_id int)
	as
	BEGIN
	DECLARE @ACCOUNT int = 0
	SET @ACCOUNT = (SELECT Count(*) FROM Media M WHERE media_id = @media_id AND media_type = @media_type AND account_id=@account_id) 
		
	IF (@ACCOUNT = 0)
		BEGIN
		UPDATE Media 
		SET account_id = @account_id
		WHERE media_id=@media_id AND media_type=@media_type
		SELECT 'Number Records Affected' = 1
		END

		ELSE
		BEGIN
		UPDATE Media
		SET account_id = NULL
		WHERE media_id=@media_id AND media_type=@media_type AND account_id = @account_id
		SELECT 'Number Records Affected' = -1
		END
	END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[viewRoles]
as
BEGIN
	SELECT R.role_id, R.role_name, COUNT(U.role_id) AS 'Total'  FROM Roles R INNER JOIN Users U ON R.role_id = U.role_id GROUP BY R.role_id, R.role_name ;
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[viewUsers]
as
BEGIN
	SELECT U.username, R.role_name, U.account_id  FROM Users U INNER JOIN Roles R ON R.role_id = U.role_id ;
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
Alter PROCEDURE [dbo].[updateUser](
	@account_id int,
	@username nvarchar(30),
	@new_role_id int)
	as
	BEGIN
		UPDATE Users
		SET role_id = @new_role_id WHERE account_id = @account_id AND username = @username;
		SELECT 'New Role ID'= @new_role_id
	END

USE [LibraryDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[addExceptionLog](
	@ex_log_id int,
	@stack_trace nvarchar(100),
	@message nvarchar(100),
	@source nvarchar(100),
	@log_date datetime)
	as
	BEGIN
		insert into ExceptionLogging(ExceptionLoggingID,StackTrace,[Message],[Source],LogDate)
		values(@ex_log_id,@stack_trace,@message,@source,@log_date)
		SELECT 'LOG ADDED'=@@ROWCOUNT
	END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[setActive](@account_id int)
	as
	BEGIN
		Update Users SET 
			active = CASE
				WHEN active = 1 THEN 0
				WHEN active = 0 THEN 1
				ELSE 1
			END
		WHERE account_id = @account_id 
		SELECT active FROM Users;
	END