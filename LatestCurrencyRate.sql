USE [bdoDB]
GO

/****** Object:  Table [dbo].[LatestCurrencyRate]    Script Date: 24-10-2022 07:04:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LatestCurrencyRate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApiTimestamp] [varchar](20) NULL,
	[BaseCur] [char](5) NULL,
	[ApiDate] [date] NOT NULL,
	[CurrencyCode] [char](5) NULL,
	[CurrencyRate] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ApiDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

