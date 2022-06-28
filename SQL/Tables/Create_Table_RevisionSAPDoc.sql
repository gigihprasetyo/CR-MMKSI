/****** Object:  Table [dbo].[RevisionSAPDoc]    Script Date: 01/10/2018 14:15:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionSAPDoc](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RevisionFakturID] [int] NULL,
	[DebitChargeNo] [varchar](10) NULL,
	[DCAmount] [money] NULL,
	[DebitMemoNo] [varchar](15) NULL,
	[DMAmount] [money] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionSAPDoc] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


