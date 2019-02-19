
GO

/****** Object:  StoredProcedure [dbo].[spUpdateBidsAsks]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spUpdateBidsAsks]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spSelectOrdersForMatch]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spSelectOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spSelectMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectEntities]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spSelectEntities]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 2/19/2019 4:57:42 PM ******/
DROP PROCEDURE [dbo].[spInsertMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 2/19/2019 4:57:42 PM ******/
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
	@LASTTRADETIME DATETIME,
	@LASTTRADEPRICE DECIMAL(18,10),
	@MARKETPRICE DECIMAL(18,10) = NULL
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	
	DECLARE @CMP DECIMAL(18,10) --Current Market Price
	DECLARE @NMP DECIMAL(18,10) --New Market Price
	DECLARE @RECENTID INT --Existing Market Data Row Id

	SET @CMP = (SELECT TOP 1 MarketPrice FROM MARKET_DATA WHERE EntityId = @ENTITYID ORDER BY LastTradeTime DESC)
	SET @CMP = ISNULL(@CMP,ISNULL(@MARKETPRICE,@LASTTRADEPRICE))
	SET @NMP = ISNULL(@MARKETPRICE, @CMP)	
	SET @RECENTID = (SELECT TOP 1 ID FROM MARKET_DATA WHERE CAST(LastTradeTime AS DATE) = CAST(GETDATE() AS DATE) AND EntityId = @ENTITYID)
	
	--New Day, INSERT
	IF @RECENTID IS NULL BEGIN
		INSERT INTO MARKET_DATA (EntityId, [Volume], LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice) VALUES
		(@ENTITYID, @VOLUME, @LASTTRADETIME, @LASTTRADEPRICE, @NMP, ((@NMP - @CMP)/@NMP))
	END
	--Day Exists, UPDATE
	ELSE BEGIN
		DECLARE @NEWVOLUME INT
		SET @NEWVOLUME = (SELECT [Volume] FROM MARKET_DATA WHERE Id = @RECENTID) + @VOLUME
		UPDATE MARKET_DATA SET
		[Volume] = @NEWVOLUME, LastTradeTime = @LASTTRADETIME, LastTradePrice = @LASTTRADEPRICE, MarketPrice = @NMP, ChangeInPrice = ((@NMP - @CMP)/@NMP)
		WHERE Id = @RECENTID
	END

END



GO

/****** Object:  StoredProcedure [dbo].[spSelectEntities]    Script Date: 2/19/2019 4:57:42 PM ******/
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

	SELECT [Id]
		  ,[EntityTypeId]
		  ,[EntityLeagueId]
		  ,[EntityGroupRefId]
		  ,[Name]
		  ,[ShortDesc]
		  ,[LongDesc]
	FROM [dbo].[ENTITIES]
	WHERE EntityLeagueId = @ENTITYLEAGUEID
	AND EntityTypeId = @ENTITYTYPEID
	

END
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 2/19/2019 4:57:42 PM ******/
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

	--empty table
	IF (NOT EXISTS(SELECT TOP 1 Id FROM MARKET_DATA)) BEGIN
		SELECT null [Volume], null [LastTradeTime], null [LastTradePrice], null [MarketPrice], null [ChangeInPrice]
	END
	ELSE BEGIN
		DECLARE @RECENTDATE DATE
		DECLARE @RECENTID INT		
		SET @RECENTID = (SELECT MAX(Id) FROM MARKET_DATA)
		SET @RECENTDATE = CAST((SELECT LastTradeTime FROM MARKET_DATA WHERE Id = @RECENTID) AS DATE)

		--same day, return all data
		IF @RECENTDATE = CAST(GETDATE() AS DATE) BEGIN
			SELECT [Volume], LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice FROM MARKET_DATA
			WHERE Id = @RECENTID
		END
		--different day, just return 0 volume
		ELSE BEGIN
			SELECT 0, LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice FROM MARKET_DATA
			WHERE Id = @RECENTID
		END
	END

END





GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 2/19/2019 4:57:42 PM ******/
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

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 2/19/2019 4:57:42 PM ******/
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

/****** Object:  StoredProcedure [dbo].[spUpdateBidsAsks]    Script Date: 2/19/2019 4:57:42 PM ******/
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

