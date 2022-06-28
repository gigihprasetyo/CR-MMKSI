/****** Object:  Table [dbo].[RevisionPaymentHeader]    Script Date: 01/10/2018 14:13:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionPaymentHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DealerID] [int] NULL,
	[PaymentType] [varchar](3) NULL,
	[RegNumber] [varchar](15) NULL,
	[RevisionPaymentDocID] [int] NULL,
	[SlipNumber] [varchar](20) NULL,
	[TotalAmount] [money] NULL,
	[Status] [smallint] NULL,
	[EvidencePath] [varchar](150) NULL,
	[ActualPaymentDate] [datetime] NULL,
	[ActualPaymentAmount] [money] NULL,
	[AccDocNumber] [varchar](30) NULL,
	[GyroDate] [datetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionPaymentHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


