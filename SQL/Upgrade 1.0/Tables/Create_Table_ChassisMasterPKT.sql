USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[ChassisMasterPKT]    Script Date: 09/03/2018 9:22:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ChassisMasterPKT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChassisMasterID] [int] NOT NULL,
	[TglPKT] [datetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_PKTDate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ChassisMasterPKT]  WITH CHECK ADD  CONSTRAINT [FK_ChassisMasterPKT[many]]_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[ChassisMasterPKT] CHECK CONSTRAINT [FK_ChassisMasterPKT[many]]_ChassisMaster[one]]]
GO


