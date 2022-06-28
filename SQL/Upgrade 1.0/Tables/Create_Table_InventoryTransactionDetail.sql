USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[InventoryTransactionDetail]    Script Date: 26/03/2018 13:40:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InventoryTransactionDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[BaseQuantity] [float] NULL,
	[BatchNo] [varchar](100) NULL,
	[BU] [varchar](100) NULL,
	[Department] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[FromBU] [varchar](100) NULL,
	[InventoryTransactionID] [int] NULL,
	[InventoryTransactionNo] [varchar](100) NULL,
	[InventoryTransferDetail] [varchar](100) NULL,
	[InventoryUnit] [varchar](100) NULL,
	[Location] [varchar](100) NULL,
	[ProductCrossReference] [varchar](100) NULL,
	[ProductDescription] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[Quantity] [float] NULL,
	[ReasonCode] [varchar](100) NULL,
	[ReferenceNo] [varchar](100) NULL,
	[RegisterSerialNumber] [varchar](100) NULL,
	[RunningNumber] [int] NULL,
	[SerialNo] [varchar](100) NULL,
	[ServicePartsAndMaterial] [varchar](100) NULL,
	[Site] [varchar](100) NULL,
	[SourceData] [varchar](100) NULL,
	[StockNumber] [varchar](100) NULL,
	[StockNumberNV] [varchar](100) NULL,
	[TotalCost] [money] NULL,
	[TransactionType] [varchar](100) NULL,
	[TransactionUnit] [varchar](100) NULL,
	[UnitCost] [money] NULL,
	[VIN] [varchar](100) NULL,
	[Warehouse] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_InventoryTransactionDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[InventoryTransactionDetail]  WITH CHECK ADD  CONSTRAINT [FK_InventoryTransactionDetail[many]]_InventoryTransaction[one]]] FOREIGN KEY([InventoryTransactionID])
REFERENCES [dbo].[InventoryTransaction] ([ID])
GO

ALTER TABLE [dbo].[InventoryTransactionDetail] CHECK CONSTRAINT [FK_InventoryTransactionDetail[many]]_InventoryTransaction[one]]]
GO


