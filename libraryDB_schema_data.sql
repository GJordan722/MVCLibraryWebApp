USE [master]
GO

/****** Object:  Database [LibraryDB]    Script Date: 4/26/2022 1:43:07 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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

/****** Object:  Table [dbo].[Media_Hold]    Script Date: 5/19/2022 2:36:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Media_Hold](
	[media_id] [int] PRIMARY KEY NOT NULL,
	[account_id] [int] NOT NULL,
) ON [PRIMARY]
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
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
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
	[salt] [nvarchar](max) NOT NULL,
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




INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (1,N'The Divine Comedy',N'Book',NULL,N'Dante Alighieri',N'Penguin Publishing Group')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (2,N'The Journey to the West, Revised Edition, Volume 1',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (3,N'The Journey to the West, Revised Edition, Volume 2',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (4,N'The Journey to the West, Revised Edition, Volume 3',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')
INSERT [dbo].[Media] ([media_id],[media_name],[media_type],[account_id],[author],[Publisher]) VALUES (5,N'The Journey to the West, Revised Edition, Volume 4',N'Book',NULL,N'Chengen Wu',N'University of Chicago Press')

/****** Object:  View [dbo].[AdminCheckOutView]    Script Date: 5/19/2022 2:03:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AdminCheckOutView]
AS
SELECT        dbo.Media.media_id, dbo.Media.media_name, dbo.Users.account_id, dbo.Users.username, dbo.UserDetails.first_name, dbo.UserDetails.last_name
FROM            dbo.Media INNER JOIN
                         dbo.UserDetails ON dbo.Media.account_id = dbo.UserDetails.account_id INNER JOIN
                         dbo.Users ON dbo.Media.account_id = dbo.Users.account_id
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "Media"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserDetails"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AdminCheckOutView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AdminCheckOutView'
GO

/****** Object:  View [dbo].[ViewHolds]    Script Date: 5/19/2022 3:00:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewHolds]
AS
SELECT        dbo.Media_Hold.media_id, dbo.Media.media_name, dbo.Users.account_id, dbo.Users.username, dbo.UserDetails.email
FROM            dbo.Media_Hold INNER JOIN
                         dbo.Media ON dbo.Media_Hold.media_id = dbo.Media.media_id INNER JOIN
                         dbo.UserDetails ON dbo.Media_Hold.account_id = dbo.UserDetails.account_id INNER JOIN
                         dbo.Users ON dbo.Media_Hold.account_id = dbo.Users.account_id
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "Media_Hold"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Media"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserDetails"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 136
               Right = 832
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewHolds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewHolds'
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[MediaHold](@mediaID int, @accountID int)
AS
BEGIN
	DECLARE @HOLD int = 0
	SET @HOLD = (SELECT Count(*) FROM Media_Hold MH WHERE MH.media_id = @mediaID AND MH.account_id= @accountID) 
	DECLARE @CheckedOut int
	SET @CheckedOut = (SELECT M.account_id FROM Media M WHERE M.media_id = @mediaID)
	IF(@CheckedOut IS NOT NULL)
	BEGIN
		IF (@HOLD = 0)
			BEGIN
				INSERT INTO Media_Hold(media_id,account_id)
				VALUES(@mediaID,@accountID)
				SELECT N'Hold Added' = @@ROWCOUNT
			END
		ELSE IF(@HOLD <> 0)
			BEGIN
				DELETE FROM Media_Hold WHERE account_id = @accountID AND media_id = @mediaID
				SELECT N'Hold Removed' = @@ROWCOUNT
			END
	END
	ELSE
	BEGIN
		SELECT N'NOHOLDCREATED' = 0
	END

END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[userCreate](
	@account_id int,
	@username nvarchar(50),
	@password nvarchar(max),
	@role_id int,
	@email nvarchar(100),
	@first_name nvarchar(100),
	@last_name nvarchar(100),
	@salt nvarchar(max))
	as
	BEGIN
		BEGIN
		insert into Users(account_id,username,[password],role_id)
			values(@account_id,@username,@password,@role_id)
		END
		BEGIN
		Insert into UserDetails(email,first_name,last_name,salt,account_id)
			Values(@email,@first_name,@last_name,@salt,@account_id)
		END
		SELECT 'NumberRecordsAffected'=@@ROWCOUNT
	END
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[deleteUser](
	@account_id int,
	@username nvarchar(30),
	@password nvarchar(max))
	as
	BEGIN
		DELETE FROM [dbo].[Users] WHERE [account_id]=@account_id AND [username] = @username AND [password] = @password
		DELETE FROM [dbo].UserDetails WHERE [account_id]=@account_id
		SELECT 'NumberRecordsAffected'=@@ROWCOUNT
	END




/****** Object:  StoredProcedure [dbo].[checkUserValid]    Script Date: 5/18/2022 2:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[checkUserValid](
	@username nvarchar(30),
	@password nvarchar(max),
	@email nvarchar(50))
	as
	BEGIN
	DECLARE @isValid int;
	DECLARE @EmailORUsername int;
	SET @EmailORUsername = CASE
		WHEN (@username IS NULL AND @email IS NOT Null) THEN 0
		WHEN (@email IS null AND @username IS NOT null) THEN 1
		ELSE 2
		END
	IF(@EmailORUsername = 1)
	BEGIN
		IF ((SELECT COUNT(*) FROM Users WHERE EXISTS (SELECT [username] = @username, [password] = @password) ) > 0)
			Begin
			SET @isValid = 1; 
			END

		ELSE
			BEGIN
			SET @isValid = 0;
			END
	END
	ELSE IF (@EmailORUsername = 0)
	BEGIN
		IF ((SELECT COUNT(*) FROM Users U FULL JOIN UserDetails UD ON U.account_id = UD.account_id WHERE EXISTS (SELECT [UD.email] = @email, [U.password] = @password)) > 0)
			Begin
			SET @isValid = 1; 
			END

		ELSE
			BEGIN
			SET @isValid = 0;
			END
	END
	IF(@isValid = 1 AND @EmailORUsername = 1)
	BEGIN
		select * FROM Users U FULL JOIN UserDetails UD ON U.account_id = UD.account_id  WHERE U.[username] = @username AND U.[password] = @password	
	END
	ELSE IF(@isValid = 1 AND @EmailORUsername = 0)
	BEGIN
		select * FROM Users U FULL JOIN UserDetails UD ON U.account_id = UD.account_id  WHERE UD.email = @email AND U.[password] = @password
	END
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
CREATE PROCEDURE [dbo].[updateUser](
	@account_id int,
	@username nvarchar(30),
	@new_role_id int)
	as
	BEGIN
		UPDATE Users
		SET role_id = @new_role_id WHERE account_id = @account_id AND username = @username;
		SELECT 'New Role ID'= @new_role_id
	END

/****** Object:  StoredProcedure [dbo].[addExceptionLog]    Script Date: 5/17/2022 6:05:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[addExceptionLog](
	@ex_log_id int,
	@stack_trace nvarchar(max),
	@message nvarchar(max),
	@source nvarchar(max),
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

