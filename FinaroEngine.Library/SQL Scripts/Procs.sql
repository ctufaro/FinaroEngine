USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTrends]    Script Date: 5/24/2019 4:34:10 PM ******/
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

/****** Object:  StoredProcedure [dbo].[spInsertTrend]    Script Date: 5/24/2019 4:34:10 PM ******/
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
	@TWEETVOLUME INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CURRENTMAXVOLUME INT
	DECLARE @DIFFVOLUME BIT
	SET @CURRENTMAXVOLUME = (SELECT TOP 1 TweetVolume FROM TRENDS WHERE [Name] = @NAME ORDER BY LoadDate DESC)

	IF NOT EXISTS (SELECT TOP 1 [Name] FROM TRENDS WHERE [Name] = @NAME) BEGIN
		INSERT INTO [dbo].[TRENDS] ([Name],[URL],[TweetVolume], [LoadDate]) VALUES (@NAME,@URL,@TWEETVOLUME,GETDATE())
	END
	ELSE BEGIN
		--IF IT DOES EXIST, HAS THE VOLUME CHANGED
		IF @CURRENTMAXVOLUME <> @TWEETVOLUME BEGIN
			INSERT INTO [dbo].[TRENDS] ([Name],[URL],[TweetVolume], [LoadDate]) VALUES (@NAME,@URL,@TWEETVOLUME,GETDATE())
		END
	END
END




GO

