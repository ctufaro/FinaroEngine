USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyOrders]    Script Date: 02/24/2019 06:50:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectMyOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectMyOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTradeHistory]    Script Date: 02/24/2019 06:50:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectTradeHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectTradeHistory]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 02/24/2019 06:50:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectOrdersForMatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectOrdersForMatch]
GO

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueData]    Script Date: 02/24/2019 06:50:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectTeamLeagueData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectTeamLeagueData]
GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 02/24/2019 06:50:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectOrders]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 02/24/2019 06:50:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSelectMarketData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spSelectMarketData]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 02/24/2019 06:50:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spInsertMarketData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spInsertMarketData]
GO

USE [FinaroDB]
GO

/****** Object:  StoredProcedure [dbo].[spSelectMyOrders]    Script Date: 02/24/2019 06:50:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[spSelectMyOrders]
-- Add the parameters for the stored procedure here
@USERID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ORDERS.[Id]
		  ,[OrderId]
		  ,[UserId]
		  ,UPPER(E.Name)[Name]
		  ,[EntityId]
		  ,[TradeTypeId]
		  ,[Price]
		  ,[Date]
		  ,Quantity
		  ,UnsetQuantity
		  ,[Status]
	  FROM ORDERS
	  LEFT JOIN ENTITIES E ON E.ID = ORDERS.EntityId
	  WHERE [UserId] = @USERID

	END






GO

/****** Object:  StoredProcedure [dbo].[spSelectTradeHistory]    Script Date: 02/24/2019 06:50:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSelectTradeHistory] 
	-- Add the parameters for the stored procedure here
	@ENTITYID INT,
	@USERID INT
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT [Id]
      ,[OrderId]
      ,[UserId]
      ,[EntityId]
      ,[TradeTypeId]
      ,[Price]
      ,[Date]
      ,[Quantity]
      ,[UnsetQuantity]
      ,[Status] FROM ORDERS 
	WHERE EntityId = @ENTITYID 
	AND [Status] = 3
	ORDER BY [Date] DESC
END








GO

/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 02/24/2019 06:50:53 ******/
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

/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueData]    Script Date: 02/24/2019 06:50:53 ******/
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
	M.CurrentBid,
	M.CurrentAsk,
	M.LastTradePrice [LastPrice]
	FROM ENTITIES E
	LEFT JOIN MARKET_DATA M ON M.EntityId = E.Id
	WHERE EntityLeagueId = @ENTITYLEAGUEID
	AND EntityTypeId = @ENTITYTYPEID	

END



GO

/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 02/24/2019 06:50:53 ******/
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

/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 02/24/2019 06:50:53 ******/
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

/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 02/24/2019 06:50:53 ******/
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
	DECLARE @OPENPRICE DECIMAL(18,10)
	DECLARE @CMP DECIMAL(18,10)
	DECLARE @NMP DECIMAL(18,10) 
	DECLARE @CHANGE DECIMAL(18,10)
		
	--UPDATING CURRENTBID, CURRENTASK, OPENINGPRICE, AND CHANGE IN PRICE
	SET @BESTBID = (SELECT MIN(PRICE) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 1)
	SET @BESTASK = (SELECT MAX(PRICE) FROM ORDERS WHERE EntityId = @ENTITYID AND [Status] < 3 AND TradeTypeId = 2)
	SET @OPENPRICE = (SELECT TOP 1 [Price] FROM ORDERS WHERE CAST([DATE] AS DATE) = CAST(GETDATE() AS DATE) AND [STATUS] IN (2,3) AND EntityId = @ENTITYID ORDER BY [DATE])
	SET @CMP = ISNULL((SELECT TOP 1 MarketPrice FROM MARKET_DATA WHERE EntityId = @ENTITYID ORDER BY [MarketDate] DESC),@NMP)
	SET @NMP = ISNULL(@MARKETPRICE, 0)	

	IF ISNULL(@OPENPRICE,0) = 0 BEGIN
		SET @CHANGE = 1
	END
	ELSE
		SET @CHANGE = (@NMP - @OPENPRICE) / @OPENPRICE
	END
		
	--NEW DAY, INSERT
	IF NOT EXISTS (SELECT MarketDate FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE) AND EntityId = @ENTITYID) BEGIN
		INSERT INTO MARKET_DATA (MarketDate, EntityId, Volume, LastTradeTime, LastTradePrice, MarketPrice, ChangeInPrice, CurrentBid, CurrentAsk, OpenPrice) VALUES
		(CAST(GETDATE() AS DATE), @ENTITYID, @VOLUME, @LASTTRADETIME, @LASTTRADEPRICE, @MARKETPRICE, @CHANGE, @BESTBID, @BESTASK, @OPENPRICE)
	END

	--DAY EXISTS, UPDATE
	ELSE BEGIN
		DECLARE @NEWVOLUME INT
		SET @NEWVOLUME = (SELECT [Volume] FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE) AND EntityId = @ENTITYID) + @VOLUME
		UPDATE MARKET_DATA SET
		[Volume] = @NEWVOLUME, 
		LastTradeTime = @LASTTRADETIME, 
		LastTradePrice = @LASTTRADEPRICE, 
		MarketPrice = @NMP, 
		ChangeInPrice = @CHANGE,
		CurrentBid = @BESTBID, 
		CurrentAsk = @BESTASK,
		OpenPrice = @OPENPRICE
		WHERE MarketDate = CAST(GETDATE() AS DATE)
		AND EntityId = @ENTITYID
	END


	SELECT TOP 1 * FROM MARKET_DATA WHERE MarketDate = CAST(GETDATE() AS DATE) AND EntityId = @ENTITYID ORDER BY MarketDate DESC






GO

