USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SparePartSalesOrder]    Script Date: 22/03/2018 16:11:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartSalesOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SalesChannel] [smallint] NULL,
	[Owner] [varchar](100) NULL,
	[Status] [smallint] NULL,
	[DealerCode] [varchar](100) NULL,
	[Customer] [varchar](100) NULL,
	[CustomerNo] [varchar](50) NULL,
	[DownPaymentAmount] [money] NULL,
	[DownPaymentAmountReceived] [money] NULL,
	[DownPaymentIsPaid] [bit] NULL,
	[ExternalReferenceNo] [varchar](50) NULL,
	[GrandTotal] [money] NULL,
	[Handling] [smallint] NULL,
	[MethodOfPayment] [varchar](100) NULL,
	[OrderType] [varchar](100) NULL,
	[SalesOrderNo] [varchar](50) NULL,
	[SalesPerson] [varchar](100) NULL,
	[ShipmentType] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[TermOfPayment] [varchar](100) NULL,
	[TotalAmountBeforeDiscount] [money] NULL,
	[TotalBaseAmount] [money] NULL,
	[TotalConsumptionTaxAmount] [money] NULL,
	[TotalDiscountAmount] [money] NULL,
	[TotalReceipt] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SparePartSalesOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


