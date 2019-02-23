USE [FinaroDB]
GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 02/23/2019 10:31:14 ******/
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
/****** Object:  Table [dbo].[MARKET_DATA]    Script Date: 02/23/2019 10:31:14 ******/
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
/****** Object:  Table [dbo].[ENTITIES]    Script Date: 02/23/2019 10:31:14 ******/
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
SET IDENTITY_INSERT [dbo].[ENTITIES] ON
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (1, 1, 4, NULL, N'Atlanta Hawks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (2, 1, 4, NULL, N'Boston Celtics', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (3, 1, 4, NULL, N'Charlotte Bobcats', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (4, 1, 4, NULL, N'Chicago Bulls', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (5, 1, 4, NULL, N'Cleveland Cavaliers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (6, 1, 4, NULL, N'Dallas Mavericks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (7, 1, 4, NULL, N'Denver Nuggets', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (8, 1, 4, NULL, N'Detroit Pistons', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (9, 1, 4, NULL, N'Golden State Warriors', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (10, 1, 4, NULL, N'Houston Rockets', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (11, 1, 4, NULL, N'Indiana Pacers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (12, 1, 4, NULL, N'LA Clippers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (13, 1, 4, NULL, N'LA Lakers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (14, 1, 4, NULL, N'Memphis Grizzlies', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (15, 1, 4, NULL, N'Miami Heat', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (16, 1, 4, NULL, N'Milwaukee Bucks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (17, 1, 4, NULL, N'Minnesota Timberwolves', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (18, 1, 4, NULL, N'New Orleans Hornets', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (19, 1, 4, NULL, N'New York Knicks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (20, 1, 4, NULL, N'Oklahoma City Thunder', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (21, 1, 4, NULL, N'Orlando Magic', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (22, 1, 4, NULL, N'Philadelphia Sixers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (23, 1, 4, NULL, N'Phoenix Suns', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (24, 1, 4, NULL, N'Portland Trail Blazers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (25, 1, 4, NULL, N'Sacramento Kings', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (26, 1, 4, NULL, N'San Antonio Spurs', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (27, 1, 4, NULL, N'Toronto Raptors', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (28, 1, 4, NULL, N'Utah Jazz', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (29, 1, 4, NULL, N'Washington Wizards', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (30, 1, 2, NULL, N'Arizona Cardinals', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (32, 1, 2, NULL, N'Atlanta Falcons', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (33, 1, 2, NULL, N'Baltimore Ravens', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (34, 1, 2, NULL, N'Buffalo Bills', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (35, 1, 2, NULL, N'Carolina Panthers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (36, 1, 2, NULL, N'Chicago Bears', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (37, 1, 2, NULL, N'Cincinnati Bengals', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (38, 1, 2, NULL, N'Cleveland Browns', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (39, 1, 2, NULL, N'Dallas Cowboys', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (40, 1, 2, NULL, N'Denver Broncos', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (41, 1, 2, NULL, N'Detroit Lions', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (42, 1, 2, NULL, N'Green Bay Packers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (43, 1, 2, NULL, N'Houston Texans', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (44, 1, 2, NULL, N'Indianapolis Colts', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (45, 1, 2, NULL, N'Jacksonville Jaguars', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (46, 1, 2, NULL, N'Kansas City Chiefs', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (47, 1, 2, NULL, N'Miami Dolphins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (48, 1, 2, NULL, N'Minnesota Vikings', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (49, 1, 2, NULL, N'New England Patriots', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (50, 1, 2, NULL, N'New Orleans Saints', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (51, 1, 2, NULL, N'New York Giants', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (52, 1, 2, NULL, N'New York Jets', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (53, 1, 2, NULL, N'Oakland Raiders', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (54, 1, 2, NULL, N'Philadelphia Eagles', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (55, 1, 2, NULL, N'Pittsburgh Steelers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (56, 1, 2, NULL, N'St. Louis Rams', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (57, 1, 2, NULL, N'San Diego Chargers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (58, 1, 2, NULL, N'San Francisco 49ers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (59, 1, 2, NULL, N'Seattle Seahawks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (60, 1, 2, NULL, N'Tampa Bay Buccaneers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (61, 1, 2, NULL, N'Tennessee Titans', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (62, 1, 2, NULL, N'Washington Redskins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (63, 1, 1, NULL, N'Arizona Diamondbacks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (64, 1, 1, NULL, N'Atlanta Braves', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (65, 1, 1, NULL, N'Baltimore Orioles', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (66, 1, 1, NULL, N'Boston Red Sox', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (67, 1, 1, NULL, N'Chicago Cubs', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (68, 1, 1, NULL, N'Chicago White Sox', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (69, 1, 1, NULL, N'Cincinnati Reds', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (70, 1, 1, NULL, N'Cleveland Indians', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (71, 1, 1, NULL, N'Colorado Rockies', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (72, 1, 1, NULL, N'Detroit Tigers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (73, 1, 1, NULL, N'Miami Marlins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (74, 1, 1, NULL, N'Houston Astros', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (75, 1, 1, NULL, N'Kansas City Royals', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (76, 1, 1, NULL, N'Los Angeles Angels of Anaheim', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (77, 1, 1, NULL, N'Los Angeles Dodgers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (78, 1, 1, NULL, N'Milwaukee Brewers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (79, 1, 1, NULL, N'Minnesota Twins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (80, 1, 1, NULL, N'New York Mets', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (81, 1, 1, NULL, N'New York Yankees', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (82, 1, 1, NULL, N'Oakland Athletics', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (83, 1, 1, NULL, N'Philadelphia Phillies', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (84, 1, 1, NULL, N'Pittsburgh Pirates', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (85, 1, 1, NULL, N'Saint Louis Cardinals', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (86, 1, 1, NULL, N'San Diego Padres', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (87, 1, 1, NULL, N'San Francisco Giants', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (88, 1, 1, NULL, N'Seattle Mariners', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (89, 1, 1, NULL, N'Tampa Bay Rays', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (90, 1, 1, NULL, N'Texas Rangers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (91, 1, 1, NULL, N'Toronto Blue Jays', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (92, 1, 1, NULL, N'Washington Nationals', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (93, 1, 3, NULL, N'Anaheim Ducks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (94, 1, 3, NULL, N'Arizona Coyotes', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (95, 1, 3, NULL, N'Boston Bruins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (96, 1, 3, NULL, N'Buffalo Sabres', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (97, 1, 3, NULL, N'Calgary Flames', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (98, 1, 3, NULL, N'Carolina Hurricanes', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (99, 1, 3, NULL, N'Chicago Blackhawks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (100, 1, 3, NULL, N'Colorado Avalanche', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (101, 1, 3, NULL, N'Columbus Blue Jackets', NULL, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (102, 1, 3, NULL, N'Dallas Stars', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (103, 1, 3, NULL, N'Detroit Red Wings', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (104, 1, 3, NULL, N'Edmonton Oilers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (105, 1, 3, NULL, N'Florida Panthers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (106, 1, 3, NULL, N'Los Angeles Kings', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (107, 1, 3, NULL, N'Minnesota Wild', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (108, 1, 3, NULL, N'Montreal Canadiens', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (109, 1, 3, NULL, N'Nashville Predators', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (110, 1, 3, NULL, N'New Jersey Devils', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (111, 1, 3, NULL, N'New York Islanders', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (112, 1, 3, NULL, N'New York Rangers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (113, 1, 3, NULL, N'Ottawa Senators', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (114, 1, 3, NULL, N'Philadelphia Flyers', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (115, 1, 3, NULL, N'Pittsburgh Penguins', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (116, 1, 3, NULL, N'San Jose Sharks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (117, 1, 3, NULL, N'St. Louis Blues', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (118, 1, 3, NULL, N'Tampa Bay Lightning', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (119, 1, 3, NULL, N'Toronto Maple Leafs', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (120, 1, 3, NULL, N'Vancouver Canucks', NULL, NULL)
INSERT [dbo].[ENTITIES] ([Id], [EntityTypeId], [EntityLeagueId], [EntityGroupRefId], [Name], [ShortDesc], [LongDesc]) VALUES (121, 2, 4, 13, N'LeBron James', NULL, NULL)
SET IDENTITY_INSERT [dbo].[ENTITIES] OFF
