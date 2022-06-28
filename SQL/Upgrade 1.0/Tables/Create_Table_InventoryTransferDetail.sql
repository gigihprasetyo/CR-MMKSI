USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[InventoryTransferDetail]    Script Date: 25/03/2018 21:25:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InventoryTransferDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryTransferID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[BaseQuantity] [float] NULL,
	[ConsumptionTaxIn] [varchar](100) NULL,
	[ConsumptionTaxOut] [varchar](100) NULL,
	[FromBatchNo] [varchar](100) NULL,
	[FromDealer] [varchar](100) NULL,
	[FromConfiguration] [varchar](100) NULL,
	[FromExteriorColor] [varchar](100) NULL,
	[FromInteriorColor] [varchar](100) NULL,
	[FromLocation] [varchar](100) NULL,
	[FromSerialNo] [varchar](100) NULL,
	[FromSite] [varchar](100) NULL,
	[FromStyle] [varchar](100) NULL,
	[FromWarehouse] [varchar](100) NULL,
	[InventoryTransferNo] [varchar](100) NULL,
	[InventoryUnit] [varchar](100) NULL,
	[ProductDescription] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[Quantity] [float] NULL,
	[Remarks] [varchar](100) NULL,
	[ServicePartsandMaterial] [varchar](100) NULL,
	[SourceData] [varchar](50) NULL,
	[StockNumber] [varchar](100) NULL,
	[StockNumberNV] [varchar](100) NULL,
	[StockNumberLookupName] [varchar](200) NULL,
	[StockNumberLookupType] [int] NULL,
	[ToBatchNo] [varchar](100) NULL,
	[ToDealer] [varchar](100) NULL,
	[ToConfiguration] [varchar](100) NULL,
	[ToExteriorColor] [varchar](100) NULL,
	[ToInteriorColor] [varchar](100) NULL,
	[ToLocation] [varchar](100) NULL,
	[ToSerialNo] [varchar](100) NULL,
	[ToSite] [varchar](100) NULL,
	[ToStyle] [varchar](100) NULL,
	[ToWarehouse] [varchar](100) NULL,
	[TransactionUnit] [varchar](100) NULL,
	[VIN] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_InventoryTransferDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[InventoryTransferDetail]  WITH CHECK ADD  CONSTRAINT [FK_InventoryTransferDetail[many]]_InventoryTransfer[one]]] FOREIGN KEY([InventoryTransferID])
REFERENCES [dbo].[InventoryTransfer] ([ID])
GO

ALTER TABLE [dbo].[InventoryTransferDetail] CHECK CONSTRAINT [FK_InventoryTransferDetail[many]]_InventoryTransfer[one]]]
GO


