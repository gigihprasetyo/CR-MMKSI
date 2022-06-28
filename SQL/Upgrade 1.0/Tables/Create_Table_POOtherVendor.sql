USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[POOtherVendor]    Script Date: 22/03/2018 16:18:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POOtherVendor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[Address3] [varchar](100) NULL,
	[AllocationPeriod] [varchar](100) NULL,
	[Balance] [money] NULL,
	[DealerCode] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[CloseRespon] [varchar](100) NULL,
	[Country] [varchar](100) NULL,
	[DeliveryMethod] [smallint] NULL,
	[Description] [varchar](100) NULL,
	[DownPayment] [money] NULL,
	[DownPaymentAmountPaid] [money] NULL,
	[DownPaymentIsPaid] [bit] NULL,
	[EventDate] [varchar](100) NULL,
	[ExternalDocNo] [varchar](100) NULL,
	[FormSource] [smallint] NULL,
	[GrandTotal] [money] NULL,
	[PaymentGroup] [smallint] NULL,
	[PersonInCharge] [varchar](100) NULL,
	[PostalCode] [varchar](100) NULL,
	[Priority] [smallint] NULL,
	[Province] [varchar](100) NULL,
	[PRPOType] [varchar](100) NULL,
	[PurchaseOrderNo] [varchar](100) NULL,
	[SONo] [varchar](100) NULL,
	[Site] [varchar](100) NULL,
	[State] [smallint] NULL,
	[StockReferenceNo] [varchar](100) NULL,
	[Taxable] [smallint] NULL,
	[TermsOfPayment] [varchar](100) NULL,
	[TotalAmountBeforeDiscount] [money] NULL,
	[TotalBaseAmount] [money] NULL,
	[TotalConsumptionTaxAmount] [money] NULL,
	[TotalDiscountAmount] [money] NULL,
	[TotalTitleRegistrationFee] [money] NULL,
	[PurchaseOrderDate] [datetime] NULL,
	[VendorDescription] [varchar](100) NULL,
	[Vendor] [varchar](100) NULL,
	[Warehouse] [varchar](100) NULL,
	[WONo] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_POOtherVendor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


