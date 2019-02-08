USE [Sandbox]
GO

/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 2/8/2019 4:38:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MARKET_DATA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[24HrVolume] [int] NOT NULL,
	[LastTradeTime] [datetime] NOT NULL,
	[LastTradePrice] [decimal](18, 10) NOT NULL,
	[MarketPrice] [decimal](18, 10) NOT NULL,
	[ChangeInPrice] [decimal](18, 10) NOT NULL,
 CONSTRAINT [PK_MARKET_DATA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


