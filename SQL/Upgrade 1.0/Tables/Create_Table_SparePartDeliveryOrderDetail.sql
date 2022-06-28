

/****** Object:  Table [dbo].[SparePartDeliveryOrderDetail]    Script Date: 24/03/2018 13:45:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartDeliveryOrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SparePartDeliveryOrderID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[AmountBeforeDiscount] [money] NULL,
	[BaseAmount] [money] NULL,
	[BaseQtyDelivered] [float] NULL,
	[BaseQtyOrder] [float] NULL,
	[BatchNo] [varchar](100) NULL,
	[BU] [varchar](100) NULL,
	[ConsumptionTax1Amount] [money] NULL,
	[ConsumptionTax1] [varchar](100) NULL,
	[ConsumptionTax2Amount] [money] NULL,
	[ConsumptionTax2] [varchar](100) NULL,
	[DeliveryOrderDetail] [varchar](100) NULL,
	[DeliveryOrderNo] [varchar](100) NULL,
	[DiscountAmount] [money] NULL,
	[DiscountBaseAmount] [money] NULL,
	[DiscountPercentage] [float] NULL,
	[Location] [varchar](100) NULL,
	[ProductCrossReference] [varchar](100) NULL,
	[ProductDescription] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[PromiseDate] [datetime] NULL,
	[QtyDelivered] [float] NULL,
	[QtyOrder] [float] NULL,
	[RequestDate] [datetime] NULL,
	[RunningNumber] [int] NULL,
	[SalesOrderDetail] [varchar](100) NULL,
	[SalesUnit] [varchar](100) NULL,
	[Site] [varchar](100) NULL,
	[TotalAmount] [money] NULL,
	[TotalConsumptionTaxAmount] [money] NULL,
	[TransactionAmount] [money] NULL,
	[UnitPrice] [money] NULL,
	[Warehouse] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SparePartDeliveryOrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SparePartDeliveryOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_SparePartDeliveryOrderDetail[many]]_SparePartDeliveryOrder[one]]] FOREIGN KEY([SparePartDeliveryOrderID])
REFERENCES [dbo].[SparePartDeliveryOrder] ([ID])
GO

ALTER TABLE [dbo].[SparePartDeliveryOrderDetail] CHECK CONSTRAINT [FK_SparePartDeliveryOrderDetail[many]]_SparePartDeliveryOrder[one]]]
GO

