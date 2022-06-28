USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[POOtherVendorDetail]    Script Date: 22/03/2018 16:17:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POOtherVendorDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[POOtherVendorID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[DealerCode] [varchar](100) NULL,
	[CloseLine] [bit] NULL,
	[CloseReason] [varchar](100) NULL,
	[Completed] [bit] NULL,
	[ConsumptionTax1Amount] [money] NULL,
	[ConsumptionTax1] [varchar](100) NULL,
	[ConsumptionTax2Amount] [money] NULL,
	[ConsumptionTax2] [varchar](100) NULL,
	[Department] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[DiscountAmount] [money] NULL,
	[DiscountPercentage] [float] NULL,
	[EventData] [varchar](100) NULL,
	[FormSource] [smallint] NULL,
	[BaseQtyOrder] [float] NULL,
	[BaseQtyReceipt] [float] NULL,
	[BaseQtyReturn] [float] NULL,
	[InventoryUnit] [varchar](100) NULL,
	[ProductCrossReference] [varchar](100) NULL,
	[ProductDescription] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[ProductSubstitute] [varchar](100) NULL,
	[ProductVariant] [varchar](100) NULL,
	[ProductVolume] [float] NULL,
	[ProductWeight] [float] NULL,
	[PromisedDate] [datetime] NULL,
	[PurchaseFor] [smallint] NULL,
	[PurchaseOrderNo] [varchar](100) NULL,
	[PurchaseRequisitionDetail] [varchar](100) NULL,
	[PurchaseUnit] [varchar](100) NULL,
	[QtyOrder] [float] NULL,
	[QtyReceipt] [float] NULL,
	[QtyReturn] [float] NULL,
	[RecallProduct] [bit] NULL,
	[ReferenceNo] [varchar](100) NULL,
	[RequiredDate] [datetime] NULL,
	[SalesOrderDetail] [varchar](100) NULL,
	[ScheduledShippingDate] [datetime] NULL,
	[ServicePartsAndMaterial] [varchar](100) NULL,
	[ShippingDate] [datetime] NULL,
	[Site] [varchar](100) NULL,
	[StockNumber] [varchar](100) NULL,
	[TitleRegistrationFee] [money] NULL,
	[TotalAmount] [money] NULL,
	[TotalAmountBeforeDiscount] [money] NULL,
	[TotalBaseAmount] [money] NULL,
	[TotalConsumptionTaxAmount] [money] NULL,
	[TotalVolume] [float] NULL,
	[TotalWeight] [float] NULL,
	[TransactionAmount] [money] NULL,
	[UnitCost] [money] NULL,
	[Warehouse] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_POOtherVendorDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[POOtherVendorDetail]  WITH CHECK ADD  CONSTRAINT [FK_POOtherVendorDetail[many]]_POOtherVendor[one]]] FOREIGN KEY([POOtherVendorID])
REFERENCES [dbo].[POOtherVendor] ([ID])
GO

ALTER TABLE [dbo].[POOtherVendorDetail] CHECK CONSTRAINT [FK_POOtherVendorDetail[many]]_POOtherVendor[one]]]
GO


