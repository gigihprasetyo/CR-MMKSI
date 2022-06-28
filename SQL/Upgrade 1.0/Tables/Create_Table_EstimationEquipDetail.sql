USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[EstimationEquipDetail]    Script Date: 05/03/2018 13:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EstimationEquipDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EstimationEquipHeaderID] [int] NOT NULL,
	[SparePartMasterID] [int] NOT NULL,
	[Harga] [decimal](19, 4) NOT NULL,
	[Discount] [decimal](7, 2) NULL,
	[TotalForecast] [int] NULL,
	[EstimationUnit] [int] NOT NULL,
	[Status] [smallint] NULL,
	[ConfirmedDate] [datetime] NULL,
	[Remark] [varchar](500) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [varchar](20) NULL,
	[LastUpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_EstimationEquipDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EstimationEquipDetail]  WITH CHECK ADD  CONSTRAINT [FK_EstimationEquipDetail_EstimationEquipHeader] FOREIGN KEY([EstimationEquipHeaderID])
REFERENCES [dbo].[EstimationEquipHeader] ([ID])
GO

ALTER TABLE [dbo].[EstimationEquipDetail] CHECK CONSTRAINT [FK_EstimationEquipDetail_EstimationEquipHeader]
GO

ALTER TABLE [dbo].[EstimationEquipDetail]  WITH CHECK ADD  CONSTRAINT [FK_EstimationEquipDetail_SparePartMaster] FOREIGN KEY([SparePartMasterID])
REFERENCES [dbo].[SparePartMaster] ([ID])
GO

ALTER TABLE [dbo].[EstimationEquipDetail] CHECK CONSTRAINT [FK_EstimationEquipDetail_SparePartMaster]
GO

