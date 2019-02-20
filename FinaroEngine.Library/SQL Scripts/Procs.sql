
GO

/****** Object:  StoredProcedure [dbo].[spUpdateBidsAsks]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spUpdateBidsAsks]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueBidsAsks]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spSelectTeamLeagueBidsAsks]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spSelectOrdersForMatch]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spSelectOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spSelectMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectEntities]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spSelectEntities]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 2/20/2019 5:01:13 PM ******/
DROP PROCEDURE [dbo].[spInsertMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertMarketData] 
	-- Add the parameters for the stored procedure here
	@ENTITYID INT,
	@VOLUME INT,
	@LASTTRADETIME DATETIME = NULL,
	@LASTTRADEPRICE DECIMAL(18,10) = NULL,
	@MARKETPRICE DECIMAL(18,10) = NULL
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	DECLARE @BESTBID DECIMAL(18,10)
	DECLARE @BESTASK DECIMAL(18,10)
	DECLARE @CMP DECIMAL(18,10)
	DECLARE @NMP DECIMAL(18,10) 
	DECLARE @CHANGE DECIMAL(18,10)

	SET @BESTBID = (SELECT MIN(PRICE) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 1)
	SET @BESTASK = (SELECT MAX(PRICE) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 2)
	SET @CMP = ISNULL((SELECT TOP 1 MarketPrice FROM MARKET_DATA ORDER BY [MarketDate] DESC),@NMP)
	SET @NMP = ISNULL(@MARKETPRICE, @CMP)
	
	--CALCULATE CHANGE IN PRICE
	IF @CMP = 0 BEGIN
		SET @CHANGE = 1
	END
	ELSE
		SET @CHANGE = (@NMP - @CMP) / @CMP
	END
		
	--NEW DAY, INSERT
	IF NOT EXISTS (SELECT MarketDate FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE) AND EntityId = @ENTITYID) BEGIN
		INSERT INTO MARKET_DATA (MarketDate, EntityId, Volume, LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice, CurrentBid, CurrentAsk) VALUES
		(CAST(GETDATE() AS DATE), @ENTITYID, @VOLUME, @LASTTRADETIME, @LASTTRADEPRICE, @MARKETPRICE, @CHANGE, @BESTBID, @BESTASK)
	END

	--DAY EXISTS, UPDATE
	ELSE BEGIN
		DECLARE @NEWVOLUME INT
		SET @NEWVOLUME = (SELECT [Volume] FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE)) + @VOLUME
		UPDATE MARKET_DATA SET
		[Volume] = @NEWVOLUME, LastTradeTime = @LASTTRADETIME, LastTradePrice = @LASTTRADEPRICE, MarketPrice = @NMP, ChangeInPrice = @CHANGE,
		CurrentBid = @BESTBID, CurrentAsk = @BESTASK
		WHERE MarketDate = CAST(GETDATE() AS DATE)
	END


	SELECT TOP 1 * FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE) ORDER BY MarketDate DESC


GO

/****** Object:  StoredProcedure [dbo].[spSelectEntities]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectEntities] 
	-- Add the parameters for the stored procedure here
	@ENTITYTYPEID INT,
	@ENTITYLEAGUEID INT

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
	E.Name,
	M.CurrentBid,
	M.CurrentAsk
	FROM ENTITIES E
	LEFT JOIN MARKET_DATA M ON M.EntityId = E.Id
	WHERE EntityLeagueId = @ENTITYLEAGUEID
	AND EntityTypeId = @ENTITYTYPEID	

END
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 2/20/2019 5:01:13 PM ******/
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
		SET @RECENTDATE = (SELECT MarketDate FROM MARKET_DATA WHERE EntityId = @ENTITYID)

		--SAME DAY, RETURN ALL DATA
		IF @RECENTDATE = CAST(GETDATE() AS DATE) BEGIN
			SELECT * FROM MARKET_DATA
			WHERE MarketDate = CAST(GETDATE() AS DATE)
		END
		--DIFFERENT DAY, JUST RETURN 0 VOLUME
		ELSE BEGIN
			SELECT TOP 1 0, LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice, CurrentBid, CurrentAsk FROM MARKET_DATA
			ORDER BY MarketDate DESC			
		END
	END

END





GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spSelectOrders]
-- Add the parameters for the stored procedure here
@ENTITYID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;

	SELECT *
	FROM ORDERS 
	WHERE EntityId = @ENTITYID 
	AND [Status] < 3
	ORDER BY TradeTypeId DESC, Price DESC, [DATE] 

	END



GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:	<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectOrdersForMatch]
-- Add the parameters for the stored procedure here
	@ENTITYID INT,
	@TRADETYPEID INT,
	@PRICE DECIMAL(18,10)
	AS
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ARITHABORT ON;
		IF(@TRADETYPEID = 1) BEGIN
			--I'M BUYING, SHOW ME THE LOWEST PRICE SALES
			SELECT *
			FROM ORDERS 
			WHERE EntityId = @ENTITYID 
			AND TradeTypeId = 2 
			AND [Status] < 3
			AND Price <= @PRICE
			ORDER BY PRICE ASC, [Date]
		END
		ELSE IF (@TRADETYPEID = 2) BEGIN
			--I'M SELLING, SHOW ME THE HIGHEST PRICE BUYS
			SELECT *
			FROM ORDERS 
			WHERE EntityId = @ENTITYID 
			AND TradeTypeId = 1 
			AND [Status] < 3
			AND Price >= @PRICE
			ORDER BY PRICE DESC, [Date]
		END
	END



GO

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueBidsAsks]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTeamLeagueBidsAsks] 
	-- Add the parameters for the stored procedure here
	@ENTITYTYPEID INT,
	@ENTITYLEAGUEID INT

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
	E.Id,
	E.Name,
	M.CurrentBid,
	M.CurrentAsk,
	0 [LastPrice]
	FROM ENTITIES E
	LEFT JOIN MARKET_DATA M ON M.EntityId = E.Id
	WHERE EntityLeagueId = @ENTITYLEAGUEID
	AND EntityTypeId = @ENTITYTYPEID	

END
GO

/****** Object:  StoredProcedure [dbo].[spUpdateBidsAsks]    Script Date: 2/20/2019 5:01:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateBidsAsks] 
	-- Add the parameters for the stored procedure here
	@ENTITYID INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @BID DECIMAL(18,10)
	DECLARE @ASK DECIMAL(18,10)
	SET @BID = (SELECT MAX(Price) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 1)
	SET @ASK = (SELECT MIN(Price) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 2)

	IF (SELECT MAX(ID) FROM MARKET_DATA WHERE EntityId = @ENTITYID) IS NULL BEGIN
		INSERT INTO MARKET_DATA (EntityId,LastTradeTime,CurrentBid,CurrentAsk) VALUES (@ENTITYID,GETDATE(), @BID, @ASK)
	END
	ELSE BEGIN
		UPDATE MARKET_DATA SET
		CurrentBid = @BID,
		CurrentAsk = @ASK
		WHERE EntityId = @ENTITYID
		AND ID = (SELECT MAX(Id) FROM MARKET_DATA WHERE EntityId = @ENTITYID)
	END

END



GO


