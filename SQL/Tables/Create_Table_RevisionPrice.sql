USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[RevisionPrice]    Script Date: 21/09/2018 11:19:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionPrice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [tinyint] NULL,
	[RevisionTypeID] [int] NULL,
	[Amount] [money] NULL,
	[ValidFrom] [smalldatetime] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionPrice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RevisionPrice]  WITH CHECK ADD  CONSTRAINT [FK_RevisionPrice[many]]_Category[one]]] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO

ALTER TABLE [dbo].[RevisionPrice] CHECK CONSTRAINT [FK_RevisionPrice[many]]_Category[one]]]
GO

ALTER TABLE [dbo].[RevisionPrice]  WITH CHECK ADD  CONSTRAINT [FK_RevisionPrice[many]]_RevisionType[one]]] FOREIGN KEY([RevisionTypeID])
REFERENCES [dbo].[RevisionType] ([ID])
GO

ALTER TABLE [dbo].[RevisionPrice] CHECK CONSTRAINT [FK_RevisionPrice[many]]_RevisionType[one]]]
GO


