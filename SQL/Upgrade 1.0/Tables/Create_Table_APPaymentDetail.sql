USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[APPaymentDetail]    Script Date: 24/03/2018 10:23:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APPaymentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[APPaymentID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[APPaymentDetailNo] [varchar](100) NULL,
	[APPaymentNo] [varchar](100) NULL,
	[BU] [varchar](100) NULL,
	[ChangeAmount] [money] NULL,
	[Description] [varchar](100) NULL,
	[DifferenceValue] [float] NULL,
	[ExternalDocumentNo] [varchar](50) NULL,
	[ExternalDocumentType] [smallint] NULL,
	[APVoucherNo] [varchar](100) NULL,
	[OrderDate] [datetime] NULL,
	[OrderNoNVSOReferral] [varchar](100) NULL,
	[OrderNoOutsourceWorkOrder] [varchar](100) NULL,
	[OrderNo] [varchar](100) NULL,
	[OrderNoUVSOReferral] [varchar](100) NULL,
	[OutstandingBalance] [money] NULL,
	[PaymentAmount] [money] NULL,
	[PaymentSlipNo] [varchar](50) NULL,
	[ReceiptFromVendor] [bit] NULL,
	[RemainingBalance] [money] NULL,
	[SourceType] [smallint] NULL,
	[TransactionDocument] [varchar](100) NULL,
	[Vendor] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_APPaymentDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[APPaymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_APPaymentDetail[many]]_APPayment[one]]] FOREIGN KEY([APPaymentID])
REFERENCES [dbo].[APPayment] ([ID])
GO

ALTER TABLE [dbo].[APPaymentDetail] CHECK CONSTRAINT [FK_APPaymentDetail[many]]_APPayment[one]]]
GO


