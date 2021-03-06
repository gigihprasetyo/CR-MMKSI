USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[StandardCodeChar]    Script Date: 02/03/2018 13:36:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StandardCodeChar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](100) NULL,
	[ValueId] [varchar](5) NOT NULL,
	[ValueDesc] [varchar](200) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_StandardCodeChar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


