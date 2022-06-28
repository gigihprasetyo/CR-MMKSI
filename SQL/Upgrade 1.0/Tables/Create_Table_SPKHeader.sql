USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SPKHeader]    Script Date: 02/03/2018 10:10:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SPKHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DealerID] [smallint] NULL,
	[Status] [varchar](2) NULL,
	[SPKNumber] [varchar](15) NULL,
	[DealerSPKNumber] [varchar](15) NULL,
	[IndentNumber] [varchar](10) NULL,
	[PlanDeliveryMonth] [tinyint] NULL,
	[PlanDeliveryYear] [smallint] NULL,
	[PlanDeliveryDate] [datetime] NULL,
	[PlanInvoiceMonth] [tinyint] NULL,
	[PlanInvoiceYear] [smallint] NULL,
	[PlanInvoiceDate] [datetime] NULL,
	[CustomerRequestID] [int] NULL,
	[SPKCustomerID] [int] NULL,
	[ValidateTime] [datetime] NULL,
	[ValidateBy] [nvarchar](20) NULL,
	[RejectedReason] [nvarchar](255) NULL,
	[SalesmanHeaderID] [smallint] NULL,
	[EvidenceFile] [nvarchar](255) NULL,
	[ValidationKey] [nvarchar](20) NULL,
	[FlagUpdate] [smallint] NULL,
	[DealerBranchID] [int] NULL,
	[IsSend] [smallint] NULL,
	[DealerSPKDate] [datetime] NULL,
	[BenefitMasterHeaderID] [int] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
	[LastUpdateBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_SPKHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SPKHeader]  WITH CHECK ADD  CONSTRAINT [FK_SPKHeader_Dealer[one]]] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[SPKHeader] CHECK CONSTRAINT [FK_SPKHeader_Dealer[one]]]
GO

ALTER TABLE [dbo].[SPKHeader]  WITH CHECK ADD  CONSTRAINT [FK_SPKHeader_DealerBranch[one]]] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[SPKHeader] CHECK CONSTRAINT [FK_SPKHeader_DealerBranch[one]]]
GO

ALTER TABLE [dbo].[SPKHeader]  WITH CHECK ADD  CONSTRAINT [FK_SPKHeader_SalesmanHeader[one]]] FOREIGN KEY([SalesmanHeaderID])
REFERENCES [dbo].[SalesmanHeader] ([ID])
GO

ALTER TABLE [dbo].[SPKHeader] CHECK CONSTRAINT [FK_SPKHeader_SalesmanHeader[one]]]
GO

ALTER TABLE [dbo].[SPKHeader]  WITH CHECK ADD  CONSTRAINT [FK_SPKHeader_SPKCustomer[one]]] FOREIGN KEY([SPKCustomerID])
REFERENCES [dbo].[SPKCustomer] ([ID])
GO

ALTER TABLE [dbo].[SPKHeader] CHECK CONSTRAINT [FK_SPKHeader_SPKCustomer[one]]]
GO

ALTER TABLE [dbo].[SPKHeader]  WITH CHECK ADD  CONSTRAINT [FK_SPKHeader_BenefitMasterHeader[one]]] FOREIGN KEY([BenefitMasterHeaderID])
REFERENCES [dbo].[BenefitMasterHeader] ([ID])
GO

ALTER TABLE [dbo].[SPKHeader] CHECK CONSTRAINT [FK_SPKHeader_BenefitMasterHeader[one]]]
GO