USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[AssistPartSales]    Script Date: 14/03/2018 9:46:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AssistPartSales](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssistUploadLogID] [int] NULL,
	[TglTransaksi] [date] NULL,
	[DealerID] [int] NULL,
	[DealerCode] [varchar](50) NULL,
	[KodeCustomer] [varchar](80) NULL,
	[SalesChannelID] [int] NULL,
	[SalesChannelCode] [varchar](50) NULL,
	[TrTraineeSalesSparepartID] [int] NULL,
	[SalesmanHeaderID] [int] NULL,
	[KodeSalesman] [varchar](50) NULL,
	[NoWorkOrder] [varchar](50) NULL,
	[SparepartMasterID] [int] NULL,
	[NoParts] [varchar](50) NULL,
	[Qty] [float] NULL,
	[HargaBeli] [money] NULL,
	[HargaJual] [money] NULL,
	[IsCampaign] [bit] NULL,
	[CampaignNo] [varchar](20) NULL,
	[CampaignDescription] [varchar](100) NULL,
	[DealerBranchID] [smallint] NULL,
	[DealerBranchCode] [varchar](50) NULL,
	[RemarksSystem] [varchar](max) NULL,
	[StatusAktif] [smallint] NOT NULL,
	[ValidateSystemStatus] [smallint] NOT NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AssistPartSales] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AssistPartSales]  WITH CHECK ADD  CONSTRAINT [[FK_AssistPartSales[many]]]]_DealerBranch[one]]]]]]] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[AssistPartSales] CHECK CONSTRAINT [[FK_AssistPartSales[many]]]]_DealerBranch[one]]]]]]]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:tidak dikonfirmasi MMKSI, 1:active atau sudah dikonfirmasi MMKSI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssistPartSales', @level2type=N'COLUMN',@level2name=N'StatusAktif'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: not success validate by system, 1 :success validate system' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssistPartSales', @level2type=N'COLUMN',@level2name=N'ValidateSystemStatus'
GO

