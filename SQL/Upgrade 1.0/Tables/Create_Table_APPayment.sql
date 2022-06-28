USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[APPayment]    Script Date: 24/03/2018 10:20:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APPayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](100) NULL,
	[APPaymentNo] [varchar](50) NULL,
	[APReferenceNo] [varchar](100) NULL,
	[APVoucherReferenceNo] [varchar](100) NULL,
	[AppliedToDocument] [money] NULL,
	[BU] [varchar](100) NULL,
	[Cancelled] [bit] NULL,
	[CashAndBank] [varchar](100) NULL,
	[MethodOfPayment] [varchar](100) NULL,
	[AvailableBalance] [money] NULL,
	[State] [smallint] NULL,
	[TotalChangeAmount] [money] NULL,
	[TotalPaymentAmount] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[Type] [smallint] NULL,
	[VendorDescription] [varchar](100) NULL,
	[Vendor] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_APPayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


