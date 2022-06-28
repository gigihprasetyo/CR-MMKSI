USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[PMHeader]    Script Date: 22/03/2018 10:21:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PMHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DealerID] [smallint] NULL,
	[DealerBranchID] [smallint] NULL,
	[ChassisNumberID] [int] NULL,
	[StandKM] [int] NULL,
	[ServiceDate] [datetime] NULL,
	[ReleaseDate] [datetime] NULL,
	[PMStatus] [varchar](4) NULL,
	[EntryType] [varchar](20) NULL,
	[WorkOrderNumber] [varchar](50) NULL,
	[BookingNo] [varchar](5) NULL,
	[VisitType] [varchar](5) NULL,
	[Remarks] [varchar](250) NULL,
	[PMKindID] [int] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_PMHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PMHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_PMHeader[many]]_ChassisMaster[one]]] FOREIGN KEY([ChassisNumberID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[PMHeader] CHECK CONSTRAINT [FK_PMHeader[many]]_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[PMHeader]  WITH CHECK ADD  CONSTRAINT [FK_PMHeader[many]]_DealerBranch[one]]] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[PMHeader] CHECK CONSTRAINT [FK_PMHeader[many]]_DealerBranch[one]]]
GO

ALTER TABLE [dbo].[PMHeader]  WITH CHECK ADD  CONSTRAINT [FK_PMHeader[many]]_PMKind[one]]] FOREIGN KEY([PMKindID])
REFERENCES [dbo].[PMKind] ([ID])
GO

ALTER TABLE [dbo].[PMHeader] CHECK CONSTRAINT [FK_PMHeader[many]]_PMKind[one]]]
GO

ALTER TABLE [dbo].[PMHeader]  WITH CHECK ADD  CONSTRAINT [FK_PMHeader[one]]_Dealer[one]]] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[PMHeader] CHECK CONSTRAINT [FK_PMHeader[one]]_Dealer[one]]]
GO


