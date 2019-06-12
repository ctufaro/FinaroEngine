USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTrends]    Script Date: 6/12/2019 3:54:27 PM ******/
DROP PROCEDURE [dbo].[spSelectTrends]
GO

/****** Object:  StoredProcedure [dbo].[spInsertTrend]    Script Date: 6/12/2019 3:54:27 PM ******/
DROP PROCEDURE [dbo].[spInsertTrend]
GO

/****** Object:  StoredProcedure [dbo].[spClearTrends]    Script Date: 6/12/2019 3:54:27 PM ******/
DROP PROCEDURE [dbo].[spClearTrends]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTrendTwitterData]    Script Date: 6/12/2019 3:54:27 PM ******/
DROP PROCEDURE [dbo].[spSelectTrendTwitterData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTrendTwitterData]    Script Date: 6/12/2019 3:54:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTrendTwitterData] 
	-- Add the parameters for the stored procedure here
	@NAME VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @SUM INT
	SET @SUM = (SELECT SUM(TweetVolume) FROM TRENDS WHERE NAME = @NAME)

	IF(@SUM = 0) BEGIN
		SET @SUM = 1
	END
		
	SELECT *, 
	CAST(CAST(TweetVolume as FLOAT)/CAST(@SUM as FLOAT)*100 AS DECIMAL(18,1)) [Radius]
	FROM TRENDS WHERE NAME = @NAME
	ORDER BY LoadDate
END

GO

/****** Object:  StoredProcedure [dbo].[spClearTrends]    Script Date: 6/12/2019 3:54:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spClearTrends] 
-- Add the parameters for the stored procedure here
@DUMMY INT
AS
BEGIN
SET NOCOUNT ON;
TRUNCATE TABLE TRENDS
END
GO

/****** Object:  StoredProcedure [dbo].[spInsertTrend]    Script Date: 6/12/2019 3:54:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertTrend] 
	-- Add the parameters for the stored procedure here
	@NAME VARCHAR(MAX),
	@URL VARCHAR(MAX),
	@TWEETVOLUME INT,
	@AVGSENTIMENT DECIMAL(18,2)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CURRENTMAXVOLUME INT
	DECLARE @DIFFVOLUME BIT
	SET @CURRENTMAXVOLUME = (SELECT TOP 1 TweetVolume FROM TRENDS WHERE [Name] = @NAME ORDER BY LoadDate DESC)

	IF NOT EXISTS (SELECT TOP 1 [Name] FROM TRENDS WHERE [Name] = @NAME) BEGIN
		INSERT INTO [dbo].[TRENDS] ([Name],[URL],[TweetVolume], [LoadDate], [AvgSentiment]) VALUES (@NAME,@URL,@TWEETVOLUME,GETDATE(), @AVGSENTIMENT)
	END
	ELSE BEGIN
		--IF IT DOES EXIST, HAS THE VOLUME CHANGED
		IF @CURRENTMAXVOLUME <> @TWEETVOLUME BEGIN
			INSERT INTO [dbo].[TRENDS] ([Name],[URL],[TweetVolume], [LoadDate], [AvgSentiment]) VALUES (@NAME,@URL,@TWEETVOLUME,GETDATE(), @AVGSENTIMENT)
		END
	END
END

GO

/****** Object:  StoredProcedure [dbo].[spSelectTrends]    Script Date: 6/12/2019 3:54:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTrends] 
	-- Add the parameters for the stored procedure here

AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT * FROM 
	(
		SELECT [Id],[Name],[URL],[TweetVolume],[LoadDate],
		ROW_NUMBER() OVER(PARTITION BY NAME,CAST([LoadDate] AS DATE) ORDER BY [LoadDate] DESC) [Row]
		FROM [FinaroDB].[dbo].[TRENDS]
	) AS TRENDDATA
	WHERE [Row] = 1
	AND CAST([LoadDate] AS DATE) = CAST(GETDATE() AS DATE)
	ORDER BY Convert(date,[LoadDate]) DESC,TweetVolume DESC
END

GO

