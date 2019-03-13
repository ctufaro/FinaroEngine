USE [FinaroDB]

GO

/****** Object:  Table [dbo].[UNITS]    Script Date: 3/13/2019 3:53:13 PM ******/
DROP TABLE [dbo].[UNITS]
GO

/****** Object:  Table [dbo].[UNITS]    Script Date: 3/13/2019 3:53:13 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

