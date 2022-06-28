USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SparePartDeliveryOrder]    Script Date: 24/03/2018 13:45:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartDeliveryOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[Address3] [varchar](100) NULL,
	[Address4] [varchar](100) NULL,
	[BusinessPhone] [varchar](60) NULL,
	[BU] [varchar](100) NULL,
	[CancellationDate] [datetime] NULL,
	[City] [varchar](100) NULL,
	[CustomerContacts] [varchar](100) NULL,
	[Customer] [varchar](100) NULL,
	[CustomerNo] [varchar](50) NULL,
	[DeliveryAddress] [varchar](100) NULL,
	[DeliveryOrderNo] [varchar](50) NULL,
	[DeliveryType] [int] NULL,
	[ExternalReferenceNo] [varchar](50) NULL,
	[GrandTotal] [money] NULL,
	[Status] [smallint] NULL,
	[MethodofPayment] [varchar](100) NULL,
	[OrderType] [varchar](100) NULL,
	[ReferenceNo] [varchar](100) NULL,
	[Salesperson] [varchar](100) NULL,
	[State] [smallint] NULL,
	[TermofPayment] [varchar](100) NULL,
	[TotalAmountBeforeDiscount] [money] NULL,
	[TotalBaseAmount] [money] NULL,
	[TotalDiscountAmount] [money] NULL,
	[TotalMiscChargeBaseAmount] [money] NULL,
	[TotalMiscChargeConsumptionTaxAmount] [money] NULL,
	[TotalReceipt] [money] NULL,
	[TotalConsumptionTaxAmount] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SparePartDeliveryOrderCustomer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



