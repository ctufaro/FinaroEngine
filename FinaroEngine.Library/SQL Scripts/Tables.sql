
GO
/****** Object:  Table [dbo].[ENTITIES]    Script Date: 2/19/2019 4:58:35 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 2/19/2019 4:58:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MARKET_DATA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[Volume] [int] NULL,
	[LastTradeTime] [datetime] NULL,
	[LastTradePrice] [decimal](18, 10) NULL,
	[MarketPrice] [decimal](18, 10) NULL,
	[ChangeInPrice] [decimal](18, 10) NULL,
	[CurrentBid] [decimal](18, 10) NULL,
	[CurrentAsk] [decimal](18, 10) NULL,
 CONSTRAINT [PK_MARKET_DATA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 2/19/2019 4:58:35 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ENTITIES] ON 

GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (1, 1, 4, NULL, N'Atlanta Hawks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (2, 1, 4, NULL, N'Boston Celtics', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (3, 1, 4, NULL, N'Charlotte Bobcats', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (4, 1, 4, NULL, N'Chicago Bulls', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (5, 1, 4, NULL, N'Cleveland Cavaliers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (6, 1, 4, NULL, N'Dallas Mavericks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (7, 1, 4, NULL, N'Denver Nuggets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (8, 1, 4, NULL, N'Detroit Pistons', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (9, 1, 4, NULL, N'Golden State Warriors', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (10, 1, 4, NULL, N'Houston Rockets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (11, 1, 4, NULL, N'Indiana Pacers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (12, 1, 4, NULL, N'LA Clippers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (13, 1, 4, NULL, N'LA Lakers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (14, 1, 4, NULL, N'Memphis Grizzlies', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (15, 1, 4, NULL, N'Miami Heat', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (16, 1, 4, NULL, N'Milwaukee Bucks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (17, 1, 4, NULL, N'Minnesota Timberwolves', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (18, 1, 4, NULL, N'New Orleans Hornets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (19, 1, 4, NULL, N'New York Knicks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (20, 1, 4, NULL, N'Oklahoma City Thunder', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (21, 1, 4, NULL, N'Orlando Magic', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (22, 1, 4, NULL, N'Philadelphia Sixers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (23, 1, 4, NULL, N'Phoenix Suns', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (24, 1, 4, NULL, N'Portland Trail Blazers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (25, 1, 4, NULL, N'Sacramento Kings', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (26, 1, 4, NULL, N'San Antonio Spurs', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (27, 1, 4, NULL, N'Toronto Raptors', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (28, 1, 4, NULL, N'Utah Jazz', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (29, 1, 4, NULL, N'Washington Wizards', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (30, 1, 2, NULL, N'Arizona Cardinals', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (32, 1, 2, NULL, N'Atlanta Falcons', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (33, 1, 2, NULL, N'Baltimore Ravens', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (34, 1, 2, NULL, N'Buffalo Bills', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (35, 1, 2, NULL, N'Carolina Panthers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (36, 1, 2, NULL, N'Chicago Bears', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (37, 1, 2, NULL, N'Cincinnati Bengals', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (38, 1, 2, NULL, N'Cleveland Browns', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (39, 1, 2, NULL, N'Dallas Cowboys', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (40, 1, 2, NULL, N'Denver Broncos', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (41, 1, 2, NULL, N'Detroit Lions', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (42, 1, 2, NULL, N'Green Bay Packers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (43, 1, 2, NULL, N'Houston Texans', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (44, 1, 2, NULL, N'Indianapolis Colts', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (45, 1, 2, NULL, N'Jacksonville Jaguars', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (46, 1, 2, NULL, N'Kansas City Chiefs', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (47, 1, 2, NULL, N'Miami Dolphins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (48, 1, 2, NULL, N'Minnesota Vikings', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (49, 1, 2, NULL, N'New England Patriots', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (50, 1, 2, NULL, N'New Orleans Saints', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (51, 1, 2, NULL, N'New York Giants', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (52, 1, 2, NULL, N'New York Jets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (53, 1, 2, NULL, N'Oakland Raiders', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (54, 1, 2, NULL, N'Philadelphia Eagles', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (55, 1, 2, NULL, N'Pittsburgh Steelers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (56, 1, 2, NULL, N'St. Louis Rams', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (57, 1, 2, NULL, N'San Diego Chargers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (58, 1, 2, NULL, N'San Francisco 49ers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (59, 1, 2, NULL, N'Seattle Seahawks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (60, 1, 2, NULL, N'Tampa Bay Buccaneers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (61, 1, 2, NULL, N'Tennessee Titans', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (62, 1, 2, NULL, N'Washington Redskins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (63, 1, 1, NULL, N'Arizona Diamondbacks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (64, 1, 1, NULL, N'Atlanta Braves', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (65, 1, 1, NULL, N'Baltimore Orioles', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (66, 1, 1, NULL, N'Boston Red Sox', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (67, 1, 1, NULL, N'Chicago Cubs', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (68, 1, 1, NULL, N'Chicago White Sox', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (69, 1, 1, NULL, N'Cincinnati Reds', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (70, 1, 1, NULL, N'Cleveland Indians', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (71, 1, 1, NULL, N'Colorado Rockies', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (72, 1, 1, NULL, N'Detroit Tigers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (73, 1, 1, NULL, N'Miami Marlins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (74, 1, 1, NULL, N'Houston Astros', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (75, 1, 1, NULL, N'Kansas City Royals', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (76, 1, 1, NULL, N'Los Angeles Angels of Anaheim', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (77, 1, 1, NULL, N'Los Angeles Dodgers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (78, 1, 1, NULL, N'Milwaukee Brewers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (79, 1, 1, NULL, N'Minnesota Twins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (80, 1, 1, NULL, N'New York Mets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (81, 1, 1, NULL, N'New York Yankees', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (82, 1, 1, NULL, N'Oakland Athletics', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (83, 1, 1, NULL, N'Philadelphia Phillies', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (84, 1, 1, NULL, N'Pittsburgh Pirates', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (85, 1, 1, NULL, N'Saint Louis Cardinals', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (86, 1, 1, NULL, N'San Diego Padres', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (87, 1, 1, NULL, N'San Francisco Giants', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (88, 1, 1, NULL, N'Seattle Mariners', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (89, 1, 1, NULL, N'Tampa Bay Rays', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (90, 1, 1, NULL, N'Texas Rangers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (91, 1, 1, NULL, N'Toronto Blue Jays', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (92, 1, 1, NULL, N'Washington Nationals', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (93, 1, 3, NULL, N'Anaheim Ducks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (94, 1, 3, NULL, N'Arizona Coyotes', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (95, 1, 3, NULL, N'Boston Bruins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (96, 1, 3, NULL, N'Buffalo Sabres', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (97, 1, 3, NULL, N'Calgary Flames', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (98, 1, 3, NULL, N'Carolina Hurricanes', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (99, 1, 3, NULL, N'Chicago Blackhawks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (100, 1, 3, NULL, N'Colorado Avalanche', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (101, 1, 3, NULL, N'Columbus Blue Jackets', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (102, 1, 3, NULL, N'Dallas Stars', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (103, 1, 3, NULL, N'Detroit Red Wings', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (104, 1, 3, NULL, N'Edmonton Oilers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (105, 1, 3, NULL, N'Florida Panthers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (106, 1, 3, NULL, N'Los Angeles Kings', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (107, 1, 3, NULL, N'Minnesota Wild', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (108, 1, 3, NULL, N'Montreal Canadiens', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (109, 1, 3, NULL, N'Nashville Predators', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (110, 1, 3, NULL, N'New Jersey Devils', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (111, 1, 3, NULL, N'New York Islanders', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (112, 1, 3, NULL, N'New York Rangers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (113, 1, 3, NULL, N'Ottawa Senators', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (114, 1, 3, NULL, N'Philadelphia Flyers', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (115, 1, 3, NULL, N'Pittsburgh Penguins', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (116, 1, 3, NULL, N'San Jose Sharks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (117, 1, 3, NULL, N'St. Louis Blues', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (118, 1, 3, NULL, N'Tampa Bay Lightning', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (119, 1, 3, NULL, N'Toronto Maple Leafs', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (120, 1, 3, NULL, N'Vancouver Canucks', NULL, NULL)
GO
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (121, 2, 4, 13, N'LeBron James', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ENTITIES] OFF
GO
SET IDENTITY_INSERT [dbo].[MARKET_DATA] ON 

GO
INSERT [dbo].[MARKET_DATA] ([Id], [EntityId], [Volume], [LastTradeTime], [LastTradePrice], [MarketPrice], [ChangeInPrice], [CurrentBid], [CurrentAsk]) VALUES (4, 1, NULL, CAST(N'2019-02-19T16:48:14.147' AS DateTime), CAST(1.0000000000 AS Decimal(18, 10)), CAST(1.0000000000 AS Decimal(18, 10)), CAST(0.0000000000 AS Decimal(18, 10)), CAST(18.0000000000 AS Decimal(18, 10)), CAST(20.0000000000 AS Decimal(18, 10)))
GO
SET IDENTITY_INSERT [dbo].[MARKET_DATA] OFF
GO
SET IDENTITY_INSERT [dbo].[ORDERS] ON 

GO
INSERT [dbo].[ORDERS] ([Id], [OrderId], [UserId], [EntityId], [TradeTypeId], [Price], [Date], [Quantity], [Status]) VALUES (39, N'02ddbca7-0aff-4010-a36d-f45eefb1b797', 1, 1, 1, CAST(1.0000000000 AS Decimal(18, 10)), CAST(N'2019-02-19T16:48:00.957' AS DateTime), CAST(0 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[ORDERS] ([Id], [OrderId], [UserId], [EntityId], [TradeTypeId], [Price], [Date], [Quantity], [Status]) VALUES (40, N'b985a0d1-4ab7-468b-8c72-cfa604293145', 1, 1, 2, CAST(1.0000000000 AS Decimal(18, 10)), CAST(N'2019-02-19T16:48:14.147' AS DateTime), CAST(0 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[ORDERS] ([Id], [OrderId], [UserId], [EntityId], [TradeTypeId], [Price], [Date], [Quantity], [Status]) VALUES (41, N'e6fd768f-f53f-45da-8f16-dccaaabb762f', 1, 1, 1, CAST(18.0000000000 AS Decimal(18, 10)), CAST(N'2019-02-19T16:51:03.073' AS DateTime), CAST(1 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[ORDERS] ([Id], [OrderId], [UserId], [EntityId], [TradeTypeId], [Price], [Date], [Quantity], [Status]) VALUES (42, N'f274d0bd-0f60-4d8e-ad49-b651e4f04f68', 1, 1, 2, CAST(20.0000000000 AS Decimal(18, 10)), CAST(N'2019-02-19T16:51:09.840' AS DateTime), CAST(1 AS Decimal(18, 0)), 1)
GO
SET IDENTITY_INSERT [dbo].[ORDERS] OFF
GO
