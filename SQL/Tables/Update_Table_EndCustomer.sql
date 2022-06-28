USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[EndCustomer]    Script Date: 18/09/2018 12:07:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EndCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectIndicator] [varchar](1) NOT NULL,
	[RefChassisNumberID] [int] NULL,
	[CustomerID] [int] NULL,
	[Name1] [varchar](50) NULL,
	[FakturDate] [datetime] NOT NULL,
	[OpenFakturDate] [datetime] NOT NULL,
	[FakturNumber] [varchar](18) NOT NULL,
	[AreaViolationFlag] [varchar](50) NULL,
	[AreaViolationPaymentMethodID] [tinyint] NULL,
	[AreaViolationyAmount] [money] NULL,
	[AreaViolationBankName] [varchar](30) NULL,
	[AreaViolationGyroNumber] [varchar](30) NULL,
	[PenaltyFlag] [varchar](50) NULL,
	[PenaltyPaymentMethodID] [tinyint] NULL,
	[PenaltyAmount] [money] NULL,
	[PenaltyBankName] [varchar](30) NULL,
	[PenaltyGyroNumber] [varchar](30) NULL,
	[ReferenceLetterFlag] [varchar](1) NULL,
	[ReferenceLetter] [varchar](40) NULL,
	[SaveBy] [varchar](20) NOT NULL,
	[SaveTime] [datetime] NOT NULL,
	[ValidateBy] [varchar](20) NOT NULL,
	[ValidateTime] [datetime] NOT NULL,
	[ConfirmBy] [varchar](20) NOT NULL,
	[ConfirmTime] [datetime] NOT NULL,
	[DownloadBy] [varchar](20) NOT NULL,
	[DownloadTime] [datetime] NOT NULL,
	[PrintedBy] [varchar](20) NOT NULL,
	[PrintedTime] [datetime] NOT NULL,
	[CleansingCustomerID] [int] NULL,
	[MCPHeaderID] [int] NULL,
	[MCPStatus] [smallint] NULL,
	[LKPPHeaderID] [int] NULL,
	[LKPPStatus] [smallint] NULL,
	[Remark1] [varchar](255) NULL,
	[Remark2] [varchar](255) NULL,
	[HandoverDate] [datetime] NULL,
	[IsTemporary] [smallint] NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateBy] [varchar](20) NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EndCustomer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EndCustomer]  WITH NOCHECK ADD  CONSTRAINT [FK_EndCustomer[many]]_Customer[one]]] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO

ALTER TABLE [dbo].[EndCustomer] CHECK CONSTRAINT [FK_EndCustomer[many]]_Customer[one]]]
GO

ALTER TABLE [dbo].[EndCustomer]  WITH NOCHECK ADD  CONSTRAINT [FK_EndCustomer_LKPPHeader] FOREIGN KEY([LKPPHeaderID])
REFERENCES [dbo].[LKPPHeader] ([ID])
GO

ALTER TABLE [dbo].[EndCustomer] CHECK CONSTRAINT [FK_EndCustomer_LKPPHeader]
GO

ALTER TABLE [dbo].[EndCustomer]  WITH NOCHECK ADD  CONSTRAINT [FK_EndCustomer_PaymentMethodAreaViolation] FOREIGN KEY([AreaViolationPaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO

ALTER TABLE [dbo].[EndCustomer] CHECK CONSTRAINT [FK_EndCustomer_PaymentMethodAreaViolation]
GO

ALTER TABLE [dbo].[EndCustomer]  WITH NOCHECK ADD  CONSTRAINT [FK_EndCustomer_PaymentMethodPenalty] FOREIGN KEY([PenaltyPaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO

ALTER TABLE [dbo].[EndCustomer] CHECK CONSTRAINT [FK_EndCustomer_PaymentMethodPenalty]
GO


