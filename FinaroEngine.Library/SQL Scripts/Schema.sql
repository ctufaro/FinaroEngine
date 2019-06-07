USE [FinaroDB]
GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ORDERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[UserId] [int] NULL,
	[PublicKey] [varchar](500) NULL,
	[EntityId] [int] NULL,
	[TradeTypeId] [int] NULL,
	[Price] [decimal](18, 10) NULL,
	[Date] [datetime] NULL,
	[Quantity] [decimal](18, 0) NULL,
	[UnsetQuantity] [decimal](18, 0) NULL,
	[Status] [int] NULL,
	[TxHash] [varchar](500) NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MARKET_DATA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MarketDate] [date] NOT NULL,
	[EntityId] [int] NOT NULL,
	[Volume] [int] NULL,
	[OpenPrice] [decimal](18, 10) NULL,
	[LastTradeTime] [datetime] NULL,
	[LastTradePrice] [decimal](18, 10) NULL,
	[MarketPrice] [decimal](18, 10) NULL,
	[ChangeInPrice] [decimal](18, 10) NULL,
	[CurrentBid] [decimal](18, 10) NULL,
	[CurrentAsk] [decimal](18, 10) NULL,
 CONSTRAINT [PK_MARKET_DATA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MARGIN]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MARGIN](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[UserId] [int] NULL,
	[PublicKey] [varchar](500) NULL,
	[EntityId] [int] NULL,
	[TradeTypeId] [int] NULL,
	[Price] [decimal](18, 10) NULL,
	[Date] [datetime] NULL,
	[Quantity] [decimal](18, 0) NULL,
	[TxHash] [varchar](500) NULL,
 CONSTRAINT [PK_MARGIN] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ENTITIES]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ENTITIES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityTypeId] [int] NULL,
	[EntityLeagueId] [int] NULL,
	[EntityGroupRefId] [int] NULL,
	[Name] [varchar](1000) NULL,
	[ShortDesc] [varchar](100) NULL,
	[LongDesc] [varchar](max) NULL,
 CONSTRAINT [PK_ENTITIES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USERS](
	[Id] [int] NOT NULL,
	[Name] [varchar](1000) NULL,
	[Username] [varchar](500) NULL,
	[Password] [varchar](500) NULL,
	[PublicKey] [varchar](500) NULL,
	[PrivateKey] [varchar](500) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UNITS]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNITS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[EntityId] [int] NULL,
	[Units] [decimal](18, 0) NULL,
 CONSTRAINT [PK_UNITS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRENDS]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TRENDS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[URL] [varchar](max) NULL,
	[TweetVolume] [int] NULL,
	[LoadDate] [datetime] NULL,
	[UserEntry] [bit] NULL,
 CONSTRAINT [PK_TRENDS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TAGS]    Script Date: 06/07/2019 14:33:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TAGS](
	[TrendId] [int] NULL,
	[Title] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserUnits]    Script Date: 06/07/2019 14:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserUnits] 
	-- Add the parameters for the stored procedure here
	@USERID INT,
	@ENTITYID INT,
	@UNITS DECIMAL(18,0)
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
		
	--NEW ENTRY
	IF NOT EXISTS (SELECT TOP 1 Id FROM UNITS WHERE UserId = @USERID AND EntityId = @ENTITYID) BEGIN
		INSERT INTO UNITS (UserId, EntityId, Units) VALUES
		(@USERID, @ENTITYID, @UNITS)
	END

	--UPDATE
	ELSE BEGIN
		UPDATE UNITS SET
		Units = Units + @UNITS
		WHERE UserId = @USERID
		AND EntityId = @ENTITYID
	END

END
GO
/****** Object:  StoredProcedure [dbo].[spSelectTrends]    Script Date: 06/07/2019 14:33:58 ******/
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
/****** Object:  StoredProcedure [dbo].[spSelectTradeHistory]    Script Date: 06/07/2019 14:33:58 ******/
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
/****** Object:  StoredProcedure [dbo].[spSelectTeamLeagueData]    Script Date: 06/07/2019 14:33:58 ******/
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
/****** Object:  StoredProcedure [dbo].[spSelectOrdersForMatch]    Script Date: 06/07/2019 14:33:58 ******/
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
		IF(@TRADETYPEID = 1 OR @TRADETYPEID = 4) BEGIN
			--I'M BUYING OR COVER TO BUY SHOW ME THE LOWEST PRICE SALES
			SELECT *
			FROM ORDERS 
			WHERE EntityId = @ENTITYID 
			AND (TradeTypeId = 2 OR TradeTypeId = 3)
			AND [Status] < 3
			AND Price <= @PRICE
			ORDER BY PRICE ASC, [Date]
		END
		ELSE IF (@TRADETYPEID = 2 OR @TRADETYPEID = 3) BEGIN
			--I'M SELLING OR SHORT SELLING, SHOW ME THE HIGHEST PRICE BUYS
			SELECT *
			FROM ORDERS 
			WHERE EntityId = @ENTITYID 
			AND (TradeTypeId = 1 OR TradeTypeId = 4) 
			AND [Status] < 3
			AND Price >= @PRICE
			ORDER BY PRICE DESC, [Date]
		END
	END
GO
/****** Object:  StoredProcedure [dbo].[spSelectOrders]    Script Date: 06/07/2019 14:33:58 ******/
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
/****** Object:  StoredProcedure [dbo].[spSelectMyUnits]    Script Date: 06/07/2019 14:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelectMyUnits]
-- Add the parameters for the stored procedure here
@USERID INT,
@ENTITYID INT
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @RETUNITS DECIMAL(18,0)
	SET @RETUNITS = (SELECT TOP 1 UNITS FROM UNITS WHERE UserId = @USERID AND EntityId = @ENTITYID)
	SELECT ISNULL(@RETUNITS,0) [Units]

	END
GO
/****** Object:  StoredProcedure [dbo].[spSelectMyOrders]    Script Date: 06/07/2019 14:33:58 ******/
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
		  ,[TxHash]
	  FROM ORDERS
	  LEFT JOIN ENTITIES E ON E.ID = ORDERS.EntityId
	  WHERE [UserId] = @USERID

	END
GO
/****** Object:  StoredProcedure [dbo].[spSelectMyBalance]    Script Date: 06/07/2019 14:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelectMyBalance]
-- Add the parameters for the stored procedure here
@USERID INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ISNULL(SUM([UNITS] * [MARKETPRICE]),0) AS UserBalance FROM UNITS U
	LEFT JOIN MARKET_DATA M ON U.EntityId = M.EntityId
	WHERE U.UserId = @USERID

	END
GO
/****** Object:  StoredProcedure [dbo].[spSelectMarketData]    Script Date: 06/07/2019 14:33:58 ******/
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
/****** Object:  StoredProcedure [dbo].[spInsertTrend]    Script Date: 06/07/2019 14:33:57 ******/
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
/****** Object:  StoredProcedure [dbo].[spInsertTestUnits]    Script Date: 06/07/2019 14:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertTestUnits] 
	-- Add the parameters for the stored procedure here
	@USERID INT

AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
		
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,1,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,2,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,3,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,4,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,5,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,6,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,7,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,8,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,9,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,10,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,11,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,12,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,13,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,14,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,15,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,16,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,17,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,18,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,19,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,20,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,21,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,22,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,23,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,24,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,25,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,26,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,27,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,28,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,29,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,30,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,32,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,33,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,34,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,35,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,36,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,37,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,38,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,39,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,40,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,41,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,42,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,43,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,44,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,45,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,46,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,47,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,48,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,49,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,50,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,51,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,52,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,53,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,54,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,55,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,56,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,57,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,58,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,59,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,60,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,61,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,62,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,63,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,64,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,65,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,66,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,67,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,68,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,69,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,70,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,71,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,72,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,73,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,74,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,75,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,76,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,77,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,78,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,79,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,80,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,81,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,82,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,83,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,84,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,85,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,86,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,87,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,88,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,89,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,90,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,91,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,92,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,93,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,94,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,95,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,96,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,97,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,98,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,99,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,100,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,101,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,102,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,103,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,104,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,105,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,106,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,107,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,108,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,109,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,110,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,111,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,112,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,113,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,114,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,115,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,116,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,117,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,118,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,119,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,120,1000)
	INSERT INTO UNITS (UserId,EntityId,Units) VALUES (@USERID,121,1000)

END
GO
/****** Object:  StoredProcedure [dbo].[spInsertMarketData]    Script Date: 06/07/2019 14:33:57 ******/
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
/****** Object:  StoredProcedure [dbo].[spInsertMargin]    Script Date: 06/07/2019 14:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertMargin] 
	-- Add the parameters for the stored procedure here
	@ORDERID UNIQUEIDENTIFIER,
	@USERID INT,
	@PUBLICKEY VARCHAR(500),
	@ENTITYID INT,
	@TRADETYPEID INT,
	@PRICE DECIMAL(18,10),
	@QUANTITY DECIMAL(18,0),
	@TXHASH VARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	INSERT INTO [MARGIN]
           ([OrderId]
           ,[UserId]
           ,[PublicKey]
           ,[EntityId]
           ,[TradeTypeId]
           ,[Price]
           ,[Date]
           ,[Quantity]
           ,[TxHash])
     VALUES
           (@ORDERID,
            @USERID,
            @PUBLICKEY,
            @ENTITYID,
            @TRADETYPEID,
            @PRICE,
            GETDATE(),
            @QUANTITY,
            @TXHASH)
END
GO
/****** Object:  StoredProcedure [dbo].[spClearData]    Script Date: 06/07/2019 14:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spClearData]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	TRUNCATE TABLE ORDERS
	TRUNCATE TABLE MARGIN
	TRUNCATE TABLE MARKET_DATA
END
GO
/****** Object:  Default [DF_TRENDS_UserEntry]    Script Date: 06/07/2019 14:33:56 ******/
ALTER TABLE [dbo].[TRENDS] ADD  CONSTRAINT [DF_TRENDS_UserEntry]  DEFAULT ((0)) FOR [UserEntry]
GO
