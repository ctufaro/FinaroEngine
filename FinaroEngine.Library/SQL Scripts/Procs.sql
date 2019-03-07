
GO

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueData]    Script Date: 3/7/2019 12:25:14 PM ******/
DROP PROCEDURE [dbo].[spSelectTeamLeagueData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueData]    Script Date: 3/7/2019 12:25:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTeamLeagueData] 
	-- Add the parameters for the stored procedure here
	@ENTITYTYPEID INT,
	@ENTITYLEAGUEID INT

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
	E.Id,
	E.Name,
	MD.CurrentBid,
	MD.CurrentAsk,
	MD.LastTradePrice [LastPrice]
	FROM ENTITIES E
	LEFT JOIN 
	(
		SELECT *,ROW_NUMBER() OVER(PARTITION BY EntityId ORDER BY MarketDate DESC) [RowNum] 
		FROM MARKET_DATA
	) AS MD	
	ON MD.EntityId = E.Id
	WHERE EntityLeagueId = @ENTITYLEAGUEID
	AND EntityTypeId = @ENTITYTYPEID	
	AND (MD.RowNum = 1 OR MD.RowNum IS NULL)
END






GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 3/7/2019 12:04:59 PM ******/
DROP PROCEDURE [dbo].[spSelectMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 3/7/2019 12:04:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectMarketData] 
	-- Add the parameters for the stored procedure here
	@ENTITYID INT
AS
BEGIN

	SET NOCOUNT ON;

	--EMPTY TABLE
	IF (NOT EXISTS(SELECT TOP 1 Id FROM MARKET_DATA WHERE EntityId = @ENTITYID)) BEGIN
		SELECT null [Volume], null [LastTradeTime], null [LastTradePrice], null [MarketPrice], null [ChangeInPrice], null [CurrentBid], null [CurrentAsk]
	END
	ELSE BEGIN
		DECLARE @RECENTDATE DATE
		SET @RECENTDATE = (SELECT MAX(MarketDate) FROM MARKET_DATA WHERE EntityId = @ENTITYID)

		--SAME DAY, RETURN ALL DATA
		IF @RECENTDATE = CAST(GETDATE() AS DATE) BEGIN
			SELECT * FROM MARKET_DATA
			WHERE MarketDate = CAST(GETDATE() AS DATE)
			AND EntityId = @ENTITYID
		END
		--DIFFERENT DAY, JUST RETURN 0 VOLUME
		ELSE BEGIN
			SELECT TOP 1 0 [Volume], LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice, CurrentBid, CurrentAsk 
			FROM MARKET_DATA
			WHERE EntityId = @ENTITYID
			ORDER BY MarketDate DESC			
		END
	END

END









GO

