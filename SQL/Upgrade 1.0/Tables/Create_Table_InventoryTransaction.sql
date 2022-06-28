USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[InventoryTransaction]    Script Date: 16/03/2018 8:58:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InventoryTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[DealerCode] [varchar](100) NULL,
	[InventoryTransactionNo] [varchar](100) NULL,
	[InventoryTransferNo] [varchar](100) NULL,
	[PersonInCharge] [varchar](100) NULL,
	[ProcessCode] [varchar](10) NULL,
	[SourceData] [varchar](50) NULL,
	[State] [smallint] NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionType] [smallint] NULL,
	[WONo] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_InventoryTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


