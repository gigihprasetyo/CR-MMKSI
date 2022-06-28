USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[RecallService]    Script Date: 19/03/2018 14:26:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RecallService](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChassisMasterID] [int] NULL,
	[MileAge] [int] NULL,
	[ServiceDate] [datetime] NULL,
	[ServiceDealerID] [smallint] NULL,
	[DealerBranchID] [smallint] NULL,
	[RecallChassisMasterID] [int] NULL,
	[WorkOrderNumber] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK__RecallSe__3214EC272D16A223] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RecallService]  WITH CHECK ADD  CONSTRAINT [FK_RecallService_ChassisMaster] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[RecallService] CHECK CONSTRAINT [FK_RecallService_ChassisMaster]
GO

ALTER TABLE [dbo].[RecallService]  WITH CHECK ADD  CONSTRAINT [FK_RecallService_Dealer] FOREIGN KEY([ServiceDealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[RecallService] CHECK CONSTRAINT [FK_RecallService_Dealer]
GO

ALTER TABLE [dbo].[RecallService]  WITH CHECK ADD  CONSTRAINT [FK_RecallService_DealerBranch] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[RecallService] CHECK CONSTRAINT [FK_RecallService_DealerBranch]
GO

ALTER TABLE [dbo].[RecallService]  WITH CHECK ADD  CONSTRAINT [FK_RecallService_RecallChassisMaster] FOREIGN KEY([RecallChassisMasterID])
REFERENCES [dbo].[RecallChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[RecallService] CHECK CONSTRAINT [FK_RecallService_RecallChassisMaster]
GO


