USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[PDI]    Script Date: 13/03/2018 14:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PDI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChassisMasterID] [int] NULL,
	[DealerID] [smallint] NULL,
	[DealerBranchID] [smallint] NULL,
	[Kind] [char](1) NULL,
	[PDIStatus] [char](1) NULL,
	[PDIDate] [datetime] NULL,
	[ReleaseBy] [varchar](20) NULL,
	[ReleaseDate] [datetime] NULL,
	[WorkOrderNumber] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_PDI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PDI]  WITH NOCHECK ADD  CONSTRAINT [FK_PDI_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[PDI] CHECK CONSTRAINT [FK_PDI_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[PDI]  WITH CHECK ADD  CONSTRAINT [FK_PDI_Dealer[one]]] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[PDI] CHECK CONSTRAINT [FK_PDI_Dealer[one]]]
GO

ALTER TABLE [dbo].[PDI]  WITH CHECK ADD  CONSTRAINT [FK_PDI_DealerBranch[one]]] FOREIGN KEY([DealerBranchID])
REFERENCES [dbo].[DealerBranch] ([ID])
GO

ALTER TABLE [dbo].[PDI] CHECK CONSTRAINT [FK_PDI_DealerBranch[one]]]
GO


