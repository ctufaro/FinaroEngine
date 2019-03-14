USE [FinaroDB]
GO

/****** Object:  Table [dbo].[ORDERS]    Script Date: 03/13/2019 21:13:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORDERS]') AND type in (N'U'))
DROP TABLE [dbo].[ORDERS]
GO

/****** Object:  Table [dbo].[UNITS]    Script Date: 03/13/2019 21:13:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UNITS]') AND type in (N'U'))
DROP TABLE [dbo].[UNITS]
GO

USE [FinaroDB]
GO

/****** Object:  Table [dbo].[ORDERS]    Script Date: 03/13/2019 21:13:44 ******/
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
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [FinaroDB]
GO

/****** Object:  Table [dbo].[UNITS]    Script Date: 03/13/2019 21:13:44 ******/
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

