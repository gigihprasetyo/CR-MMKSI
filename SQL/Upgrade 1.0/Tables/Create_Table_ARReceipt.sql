USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[ARReceipt]    Script Date: 23/03/2018 16:08:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ARReceipt](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[GeneratedToken] [varchar](36) NULL,
	[ARInvoiceReferenceNo] [varchar](100) NULL,
	[ARReceiptNo] [varchar](50) NULL,
	[ARReceiptReferenceNo] [varchar](100) NULL,
	[Type] [smallint] NULL,
	[BookingFee] [bit] NULL,
	[BU] [varchar](100) NULL,
	[Cancelled] [bit] NULL,
	[CashAndBank] [varchar](100) NULL,
	[Customer] [varchar](100) NULL,
	[CustomerNo] [varchar](100) NULL,
	[EndOrderDate] [datetime] NULL,
	[MethodOfPayment] [varchar](100) NULL,
	[AvailableBalance] [money] NULL,
	[StartOrderDate] [datetime] NULL,
	[State] [smallint] NULL,
	[AppliedToDocument] [money] NULL,
	[TotalAmountBase] [money] NULL,
	[TotalChangeAmount] [money] NULL,
	[TotalOutstandingBalanceBase] [money] NULL,
	[TotalReceiptAmount] [money] NULL,
	[TotalRemainingBalanceBase] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ARReceipt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


