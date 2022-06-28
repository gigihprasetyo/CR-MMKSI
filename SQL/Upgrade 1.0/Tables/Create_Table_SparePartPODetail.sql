USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SparePartPODetail]    Script Date: 06/03/2018 13:44:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartPODetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SparePartPOID] [int] NULL,
	[SparePartMasterID] [int] NULL,
	[CheckListStatus] [varchar](2) NULL,
	[Quantity] [int] NULL,
	[RetailPrice] [money] NULL,
	[EstimateStatus] [varchar](1) NULL,
	[StopMark] [smallint] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
	[TotalForecast] [int] NULL,
 CONSTRAINT [PK_SparePartPODetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


