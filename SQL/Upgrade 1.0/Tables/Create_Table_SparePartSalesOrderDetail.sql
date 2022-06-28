USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SparePartSalesOrderDetail]    Script Date: 22/03/2018 16:11:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartSalesOrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SparePartSalesOrderID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[Status] [smallint] NULL,
	[AmountBeforeDiscount] [money] NULL,
	[BaseAmount] [money] NULL,
	[KodeDealer] [varchar](100) NULL,
	[ConsumptionTax1Amount] [money] NULL,
	[ConsumptionTax1] [varchar](100) NULL,
	[ConsumptionTax2Amount] [money] NULL,
	[ConsumptionTax2] [varchar](100) NULL,
	[DiscountAmount] [money] NULL,
	[DiscountPercentAge] [decimal](18, 0) NULL,
	[ProductCrossReference] [varchar](100) NULL,
	[ProductDescription] [varchar](100) NULL,
	[Product] [varchar](100) NULL,
	[PromiseDate] [datetime] NULL,
	[QtyDelivered] [float] NULL,
	[QtyOrder] [float] NULL,
	[RequestDate] [datetime] NULL,
	[SalesOrderDetailID] [varchar](50) NULL,
	[SalesOrderNo] [varchar](100) NULL,
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
 CONSTRAINT [PK_SparePartSalesOrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SparePartSalesOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_SparePartSalesOrderDetail[many]]_SparePartSalesOrder[one]]] FOREIGN KEY([SparePartSalesOrderID])
REFERENCES [dbo].[SparePartSalesOrder] ([ID])
GO

ALTER TABLE [dbo].[SparePartSalesOrderDetail] CHECK CONSTRAINT [FK_SparePartSalesOrderDetail[many]]_SparePartSalesOrder[one]]]
GO


