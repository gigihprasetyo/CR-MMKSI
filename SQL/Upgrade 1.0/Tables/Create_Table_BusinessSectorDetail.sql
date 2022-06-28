USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[BusinessSectorDetail]    Script Date: 02/03/2018 16:04:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BusinessSectorDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessSectorHeaderID] [int] NULL,
	[BusinessDomain] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_BusinessSectorDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BusinessSectorDetail]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSectorDetail[many]]_BusinessSectorHeader[one]]] FOREIGN KEY([BusinessSectorHeaderID])
REFERENCES [dbo].[BusinessSectorHeader] ([ID])
GO

ALTER TABLE [dbo].[BusinessSectorDetail] CHECK CONSTRAINT [FK_BusinessSectorDetail[many]]_BusinessSectorHeader[one]]]
GO


