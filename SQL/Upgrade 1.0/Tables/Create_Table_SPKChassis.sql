USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SPKChassis]    Script Date: 22/02/2018 14:46:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SPKChassis](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SPKDetailID] [int] NULL,
	[ChassisMasterID] [int] NULL,
	[MatchingType] [smallint] NULL,
	[MatchingDate] [datetime] NULL,
	[MatchingNumber] [varchar](50) NULL,
	[ReferenceNumber] [varchar](50) NULL,
	[KeyNumber] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SPKChassis] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SPKChassis]  WITH CHECK ADD  CONSTRAINT [FK_SPKChassis[many]]_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[SPKChassis] CHECK CONSTRAINT [FK_SPKChassis[many]]_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[SPKChassis]  WITH CHECK ADD  CONSTRAINT [FK_SPKChassis[many]]_SPKDetail[one]]] FOREIGN KEY([SPKDetailID])
REFERENCES [dbo].[SPKDetail] ([ID])
GO

ALTER TABLE [dbo].[SPKChassis] CHECK CONSTRAINT [FK_SPKChassis[many]]_SPKDetail[one]]]
GO


