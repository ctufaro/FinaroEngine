USE [FinaroDB]
GO

/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 02/09/2019 00:00:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MARKET_DATA]') AND type in (N'U'))
DROP TABLE [dbo].[MARKET_DATA]
GO

/****** Object:  Table [dbo].[ORDERS]    Script Date: 02/09/2019 00:00:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORDERS]') AND type in (N'U'))
DROP TABLE [dbo].[ORDERS]
GO

USE [FinaroDB]
GO

/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 02/09/2019 00:00:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MARKET_DATA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[Volume] [int] NOT NULL,
	[LastTradeTime] [datetime] NOT NULL,
	[LastTradePrice] [decimal](18, 10) NOT NULL,
	[MarketPrice] [decimal](18, 10) NOT NULL,
	[ChangeInPrice] [decimal](18, 10) NOT NULL,
 CONSTRAINT [PK_MARKET_DATA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [FinaroDB]
GO

/****** Object:  Table [dbo].[ORDERS]    Script Date: 02/09/2019 00:00:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ORDERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [uniqueidentifier] NULL,
	[UserId] [int] NULL,
	[EntityId] [int] NULL,
	[TradeTypeId] [int] NULL,
	[Price] [decimal](18, 10) NULL,
	[Date] [datetime] NULL,
	[Quantity] [decimal](18, 0) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

