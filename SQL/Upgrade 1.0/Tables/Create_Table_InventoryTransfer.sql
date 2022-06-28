USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[InventoryTransfer]    Script Date: 25/03/2018 21:21:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InventoryTransfer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[FromDealer] [varchar](100) NULL,
	[FromSite] [varchar](100) NULL,
	[InventoryTransferNo] [varchar](50) NULL,
	[ItemTypeForTransfer] [smallint] NULL,
	[PersonInCharge] [varchar](100) NULL,
	[ReceiptDate] [datetime] NULL,
	[ReceiptNo] [varchar](100) NULL,
	[ReferenceNo] [varchar](100) NULL,
	[SearchVehicle] [varchar](50) NULL,
	[SourceData] [varchar](50) NULL,
	[State] [smallint] NULL,
	[ToDealer] [varchar](100) NULL,
	[ToSite] [varchar](100) NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionType] [smallint] NULL,
	[TransferStatus] [smallint] NULL,
	[TransferStep] [bit] NULL,
	[WONo] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_InventoryTransfer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


