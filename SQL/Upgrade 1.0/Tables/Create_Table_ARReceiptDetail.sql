USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[ARReceiptDetail]    Script Date: 23/03/2018 16:09:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ARReceiptDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ARReceiptID] [int] NULL,
	[Owner] [varchar](100) NULL,
	[DetailNo] [varchar](50) NULL,
	[ARReceiptNo] [varchar](100) NULL,
	[BU] [varchar](100) NULL,
	[ChangeAmount] [money] NULL,
	[Customer] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[DifferenceValue] [float] NULL,
	[InvoiceNo] [varchar](100) NULL,
	[OrderDate] [datetime] NULL,
	[OrderNo] [varchar](100) NULL,
	[OrderNoSO] [varchar](100) NULL,
	[OrderNoUVSO] [varchar](100) NULL,
	[OrderNoWO] [varchar](100) NULL,
	[OutstandingBalance] [money] NULL,
	[PaidBackToCustomer] [bit] NULL,
	[ReceiptAmount] [money] NULL,
	[RemainingBalance] [money] NULL,
	[SourceType] [smallint] NULL,
	[TransactionDocument] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ARReceiptDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ARReceiptDetail]  WITH CHECK ADD  CONSTRAINT [FK_ARReceiptDetail[many]]_ARReceipt[one]]] FOREIGN KEY([ARReceiptID])
REFERENCES [dbo].[ARReceipt] ([ID])
GO

ALTER TABLE [dbo].[ARReceiptDetail] CHECK CONSTRAINT [FK_ARReceiptDetail[many]]_ARReceipt[one]]]
GO


