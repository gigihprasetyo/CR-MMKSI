USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[EstimationEquipHeader]    Script Date: 05/03/2018 13:16:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EstimationEquipHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EstimationNumber] [varchar](13) NOT NULL,
	[DealerID] [smallint] NOT NULL,
	[DepositBKewajibanHeaderID] [int] NULL,
	[Status] [smallint] NOT NULL,
	[Purpose] [varchar](max) NULL,
	[DMSPRNo] [varchar](50) NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [varchar](20) NULL,
	[LastUpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_EstimationEquipHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EstimationEquipHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_EstimationEquipHeader_Dealer] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[EstimationEquipHeader] NOCHECK CONSTRAINT [FK_EstimationEquipHeader_Dealer]
GO

ALTER TABLE [dbo].[EstimationEquipHeader]  WITH NOCHECK ADD  CONSTRAINT [FK_EstimationEquipHeader_DepositBKewajibanHeader] FOREIGN KEY([DepositBKewajibanHeaderID])
REFERENCES [dbo].[DepositBKewajibanHeader] ([ID])
GO

ALTER TABLE [dbo].[EstimationEquipHeader] NOCHECK CONSTRAINT [FK_EstimationEquipHeader_DepositBKewajibanHeader]
GO

