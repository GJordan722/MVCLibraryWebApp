USE [LibraryDB]
GO
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

