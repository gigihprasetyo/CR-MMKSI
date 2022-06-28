USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[FreeService]    Script Date: 14/03/2018 10:33:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FreeService](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](1) NULL,
	[ChassisMasterID] [int] NULL,
	[FSKindID] [tinyint] NULL,
	[MileAge] [int] NULL,
	[ServiceDate] [smalldatetime] NULL,
	[ServiceDealerID] [smallint] NULL,
	[DealerBranchID] [smallint] NULL,
	[SoldDate] [smalldatetime] NULL,
	[NotificationNumber] [varchar](20) NULL,
	[NotificationType] [varchar](2) NULL,
	[TotalAmount] [money] NULL,
	[LabourAmount] [money] NULL,
	[PartAmount] [money] NULL,
	[PPNAmount] [money] NULL,
	[PPHAmount] [money] NULL,
	[Reject] [varchar](4) NULL,
	[Reason] [smallint] NULL,
	[ReleaseBy] [varchar](20) NULL,
	[ReleaseDate] [datetime] NULL,
	[FleetRequestID] [int] NULL,
	[WorkOrderNumber] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_FreeService_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[FreeService]  WITH CHECK ADD  CONSTRAINT [FK_FreeService[many]]_FleetRequest[one]]] FOREIGN KEY([FleetRequestID])
REFERENCES [dbo].[FleetRequest] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService[many]]_FleetRequest[one]]]
GO

ALTER TABLE [dbo].[FreeService]  WITH NOCHECK ADD  CONSTRAINT [FK_FreeService_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[FreeService]  WITH CHECK ADD  CONSTRAINT [FK_FreeService_Dealer[one]]] FOREIGN KEY([ServiceDealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService_Dealer[one]]]
GO

ALTER TABLE [dbo].[FreeService]  WITH CHECK ADD  CONSTRAINT [FK_FreeService_DealerBranch[one]]] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService_DealerBranch[one]]]
GO

ALTER TABLE [dbo].[FreeService]  WITH NOCHECK ADD  CONSTRAINT [FK_FreeService_FSKind[one]]] FOREIGN KEY([FSKindID])
REFERENCES [dbo].[FSKind] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService_FSKind[one]]]
GO

ALTER TABLE [dbo].[FreeService]  WITH NOCHECK ADD  CONSTRAINT [FK_FreeService_Reason[one]]] FOREIGN KEY([Reason])
REFERENCES [dbo].[Reason] ([ID])
GO

ALTER TABLE [dbo].[FreeService] CHECK CONSTRAINT [FK_FreeService_Reason[one]]]
GO


