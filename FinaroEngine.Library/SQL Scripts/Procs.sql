USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spInsertTrend]    Script Date: 5/16/2019 3:22:06 PM ******/
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
	INSERT INTO [dbo].[TRENDS] ([Name],[URL],[TweetVolume], [LoadDate])
	VALUES (@NAME,@URL,@TWEETVOLUME,GETDATE())
END






GO

/****** Object:  StoredProcedure [dbo].[spSelectTrends]    Script Date: 5/16/2019 3:22:06 PM ******/
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
		SELECT [Id],[Name],[URL],[TweetVolume],Convert(date,[LoadDate])[LoadDate],ROW_NUMBER() OVER(PARTITION BY NAME ORDER BY TweetVolume DESC, Convert(date,[LoadDate]) DESC) [Row]
		FROM [FinaroDB].[dbo].[TRENDS]
	) AS TRENDDATA
	WHERE [Row] = 1
	ORDER BY Convert(date,[LoadDate]) DESC,TweetVolume DESC
END








GO

