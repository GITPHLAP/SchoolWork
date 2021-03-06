/******
I had generated this script in SSMS generate script wizard, 
I had created the database in VS2019 so its propably a bit diffrent from what I had write
 ******/


USE [AirportDatabase]
GO
/****** Object:  Table [dbo].[Airlines]    Script Date: 27-01-2021 15:54:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airlines](
	[ICAO] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Airlines] PRIMARY KEY CLUSTERED 
(
	[ICAO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airplane]    Script Date: 27-01-2021 15:54:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airplane](
	[No] [varchar](15) NOT NULL,
	[Airline] [varchar](10) NULL,
	[Type] [varchar](15) NULL,
 CONSTRAINT [PK_Airplane] PRIMARY KEY CLUSTERED 
(
	[No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 27-01-2021 15:54:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[IATA] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[City] [varchar](25) NULL,
	[Country] [varchar](25) NULL,
 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
(
	[IATA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flights]    Script Date: 27-01-2021 15:54:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flights](
	[AirplaneNo] [varchar](15) NOT NULL,
	[Departure] [varchar](10) NOT NULL,
	[Arrival] [varchar](10) NOT NULL,
	[ICAO] [varchar](10) NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
	[DepartureTime] ASC,
	[ArrivalTime] ASC,
	[ICAO] ASC,
	[Arrival] ASC,
	[AirplaneNo] ASC,
	[Departure] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 27-01-2021 15:54:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[Departure] [varchar](10) NOT NULL,
	[Arrival] [varchar](10) NOT NULL,
	[ICAO] [varchar](10) NOT NULL,
 CONSTRAINT [PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[Departure] ASC,
	[Arrival] ASC,
	[ICAO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Airlines] ([ICAO], [Name]) VALUES (N'CCA', N'Air china')
INSERT [dbo].[Airlines] ([ICAO], [Name]) VALUES (N'SAS', N'Scandinavia Airlines')
INSERT [dbo].[Airlines] ([ICAO], [Name]) VALUES (N'UAL', N'United Airlines')
INSERT [dbo].[Airlines] ([ICAO], [Name]) VALUES (N'VIR', N'Virgin Atlantic')
GO
INSERT [dbo].[Airplane] ([No], [Airline], [Type]) VALUES (N'CA568', N'CCA', N'A359')
INSERT [dbo].[Airplane] ([No], [Airline], [Type]) VALUES (N'SK161', N'SAS', N'CRJ9')
INSERT [dbo].[Airplane] ([No], [Airline], [Type]) VALUES (N'UA2785', N'UAL', N'B772')
INSERT [dbo].[Airplane] ([No], [Airline], [Type]) VALUES (N'VS250', N'VIR', N'B789')
GO
INSERT [dbo].[Airport] ([IATA], [Name], [City], [Country]) VALUES (N'CPH', N'Copenhagen Airport', N'Copenhagen', N'Denmark')
INSERT [dbo].[Airport] ([IATA], [Name], [City], [Country]) VALUES (N'IAD', N'Wasinghton Airport', N'Wasington, D.C ', N'United States')
INSERT [dbo].[Airport] ([IATA], [Name], [City], [Country]) VALUES (N'MAD', N'Madrid Airport', N'Madrid', N'Espanio')
INSERT [dbo].[Airport] ([IATA], [Name], [City], [Country]) VALUES (N'MIA', N'Miami International Airport', N'Miami', N'United States')
INSERT [dbo].[Airport] ([IATA], [Name], [City], [Country]) VALUES (N'SKP', N'Skopje Airport', NULL, NULL)
GO
INSERT [dbo].[Flights] ([AirplaneNo], [Departure], [Arrival], [ICAO], [DepartureTime], [ArrivalTime]) VALUES (N'VS250', N'MIA', N'CPH', N'SAS', CAST(N'2021-01-27T15:48:08.717' AS DateTime), CAST(N'2021-01-27T19:48:08.717' AS DateTime))
INSERT [dbo].[Flights] ([AirplaneNo], [Departure], [Arrival], [ICAO], [DepartureTime], [ArrivalTime]) VALUES (N'SK161', N'CPH', N'MIA', N'SAS', CAST(N'2021-01-27T15:48:08.717' AS DateTime), CAST(N'2021-01-28T06:48:08.717' AS DateTime))
INSERT [dbo].[Flights] ([AirplaneNo], [Departure], [Arrival], [ICAO], [DepartureTime], [ArrivalTime]) VALUES (N'UA2785', N'IAD', N'MAD', N'CCA', CAST(N'2021-01-27T16:48:08.717' AS DateTime), CAST(N'2021-01-27T17:48:08.717' AS DateTime))
INSERT [dbo].[Flights] ([AirplaneNo], [Departure], [Arrival], [ICAO], [DepartureTime], [ArrivalTime]) VALUES (N'CA568', N'MAD', N'CPH', N'UAL', CAST(N'2021-01-27T18:48:08.717' AS DateTime), CAST(N'2021-01-28T09:48:08.717' AS DateTime))
INSERT [dbo].[Flights] ([AirplaneNo], [Departure], [Arrival], [ICAO], [DepartureTime], [ArrivalTime]) VALUES (N'VS250', N'CPH', N'SKP', N'VIR', CAST(N'2021-01-28T01:48:08.717' AS DateTime), CAST(N'2021-01-27T22:48:08.717' AS DateTime))
GO
INSERT [dbo].[Route] ([Departure], [Arrival], [ICAO]) VALUES (N'CPH', N'MIA', N'SAS')
INSERT [dbo].[Route] ([Departure], [Arrival], [ICAO]) VALUES (N'CPH', N'SKP', N'VIR')
INSERT [dbo].[Route] ([Departure], [Arrival], [ICAO]) VALUES (N'IAD', N'MAD', N'CCA')
INSERT [dbo].[Route] ([Departure], [Arrival], [ICAO]) VALUES (N'MAD', N'CPH', N'UAL')
INSERT [dbo].[Route] ([Departure], [Arrival], [ICAO]) VALUES (N'MIA', N'CPH', N'SAS')
GO
ALTER TABLE [dbo].[Airplane]  WITH CHECK ADD  CONSTRAINT [FK_Airline] FOREIGN KEY([Airline])
REFERENCES [dbo].[Airlines] ([ICAO])
GO
ALTER TABLE [dbo].[Airplane] CHECK CONSTRAINT [FK_Airline]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_AirplaneNoFlight] FOREIGN KEY([AirplaneNo])
REFERENCES [dbo].[Airplane] ([No])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_AirplaneNoFlight]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_ArrivalFlight] FOREIGN KEY([Arrival])
REFERENCES [dbo].[Airport] ([IATA])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_ArrivalFlight]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_DepartureFlight] FOREIGN KEY([Departure])
REFERENCES [dbo].[Airport] ([IATA])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_DepartureFlight]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_ICAOFlight] FOREIGN KEY([ICAO])
REFERENCES [dbo].[Airlines] ([ICAO])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_ICAOFlight]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_AirlineRoute] FOREIGN KEY([ICAO])
REFERENCES [dbo].[Airlines] ([ICAO])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_AirlineRoute]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_ArrivalRoute] FOREIGN KEY([Arrival])
REFERENCES [dbo].[Airport] ([IATA])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_ArrivalRoute]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_DepartureRoute] FOREIGN KEY([Departure])
REFERENCES [dbo].[Airport] ([IATA])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_DepartureRoute]
GO
