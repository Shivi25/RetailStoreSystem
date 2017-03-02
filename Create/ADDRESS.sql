CREATE TABLE [dbo].[ADDRESS](
	[ADDRESSCODE] [int] NOT NULL,
	[STREET_NO] [varchar](5) NOT NULL,
	[STREET_ADDRESS] [varchar](50) NOT NULL,
	[CITY] [varchar](50) NOT NULL,
	[STATE] [nchar](2) NOT NULL,
	[ZIP] [varchar](10) NOT NULL,
	[X] [varchar](4) NOT NULL,
	[Y] [varchar](4) NOT NULL,
	[LOCATION] [geometry] NOT NULL
	CONSTRAINT pk_Address PRIMARY KEY (ADDRESSCODE)
) 