SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[immobilien](
	[link] [nvarchar](100) NOT NULL,
	[bezugsfrei] [nvarchar](80) NULL,
	[bonitaetsauskunft] [nvarchar](80) NULL,
	[etageNummer] [int] NULL,
	[etageVon] [int] NULL,
	[gesamtmiete] [numeric](18, 2) NULL,
	[html] [text] NULL,
	[kaltmiete] [numeric](18, 2) NULL,
	[nebenkosten] [numeric](18, 2) NULL,
	[title] [nvarchar](250) NULL,
	[typ] [nvarchar](80) NULL,
	[wohnflaeche] [numeric](18, 2) NULL,
	[zimmer] [numeric](18, 2) NULL,
	[inserted] [datetime] NULL,
	[updated] [datetime] NULL,
	[address] [nvarchar](250) NULL,
 CONSTRAINT [PK_immobilien] PRIMARY KEY CLUSTERED 
(
	[link] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO