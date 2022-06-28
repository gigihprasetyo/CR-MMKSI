/****** Object:  Table [dbo].[AssistPartStock]    Script Date: 13/03/2018 16:18:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AssistPartStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssistUploadLogID] [int] NULL,
	[Month] [nchar](10) NULL,
	[Year] [nchar](10) NULL,
	[DealerID] [int] NULL,
	[DealerCode] [varchar](30) NULL,
	[DealerBranchID] [int] NULL,
	[DealerBranchCode] [varchar](30) NULL,
	[SparepartMasterID] [int] NULL,
	[NoParts] [varchar](50) NULL,
	[JumlahStokAwal] [float] NULL,
	[JumlahDatang] [float] NULL,
	[HargaBeli] [money] NULL,
	[RemarksSystem] [varchar](max) NULL,
	[StatusAktif] [smallint] NOT NULL,
	[ValidateSystemStatus] [smallint] NOT NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AssistPartStock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO