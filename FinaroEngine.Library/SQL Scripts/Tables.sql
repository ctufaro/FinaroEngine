USE [FinaroDB]
GO

ALTER TABLE [dbo].[TRENDS] DROP CONSTRAINT [DF_TRENDS_UserEntry]
GO

/****** Object:  Table [dbo].[TAGS]    Script Date: 6/12/2019 3:26:43 PM ******/
DROP TABLE [dbo].[TAGS]
GO

/****** Object:  Table [dbo].[TRENDS]    Script Date: 6/12/2019 3:26:43 PM ******/
DROP TABLE [dbo].[TRENDS]
GO

/****** Object:  Table [dbo].[TRENDS]    Script Date: 6/12/2019 3:26:43 PM ******/
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
	[AvgSentiment] [decimal](18, 2) NULL,
 CONSTRAINT [PK_TRENDS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[TAGS]    Script Date: 6/12/2019 3:26:43 PM ******/
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

ALTER TABLE [dbo].[TRENDS] ADD  CONSTRAINT [DF_TRENDS_UserEntry]  DEFAULT ((0)) FOR [UserEntry]
GO

