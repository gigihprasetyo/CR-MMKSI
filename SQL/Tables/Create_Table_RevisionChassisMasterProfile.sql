USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[RevisionChassisMasterProfile]    Script Date: 16/08/2018 15:17:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionChassisMasterProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChassisMasterID] [int] NULL,
	[EndCustomerID] [int] NULL,
	[ProfileHeaderID] [tinyint] NULL,
	[GroupID] [tinyint] NULL,
	[ProfileValue] [varchar](250) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionChassisMasterProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile]  WITH CHECK ADD  CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile] CHECK CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile]  WITH CHECK ADD  CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ProfileGroup[one]]] FOREIGN KEY([GroupID])
REFERENCES [dbo].[ProfileGroup] ([ID])
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile] CHECK CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ProfileGroup[one]]]
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile]  WITH CHECK ADD  CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ProfileHeader[one]]] FOREIGN KEY([ProfileHeaderID])
REFERENCES [dbo].[ProfileHeader] ([ID])
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile] CHECK CONSTRAINT [FK_RevisionChassisMasterProfile[many]]_ProfileHeader[one]]]
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile]  WITH CHECK ADD  CONSTRAINT [FK_RevisionChassisMasterProfile[one]]_EndCustomer[one]]] FOREIGN KEY([EndCustomerID])
REFERENCES [dbo].[EndCustomer] ([ID])
GO

ALTER TABLE [dbo].[RevisionChassisMasterProfile] CHECK CONSTRAINT [FK_RevisionChassisMasterProfile[one]]_EndCustomer[one]]]
GO


