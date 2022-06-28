USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[IndentPartDetail]    Script Date: 06/03/2018 10:35:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IndentPartDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IndentPartHeaderID] [int] NULL,
	[SparePartMasterID] [int] NULL,
	[TotalForecast] [int] NULL,
	[Qty] [int] NULL,
	[Description] [varchar](255) NULL,
	[AllocationQty] [int] NULL,
	[IsCompletedAllocation] [tinyint] NULL,
	[Price] [money] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK__IndentPartDetail__42C2BEC4] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IndentPartDetail]  WITH NOCHECK ADD  CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]] FOREIGN KEY([IndentPartHeaderID])
REFERENCES [dbo].[IndentPartHeader] ([ID])
GO

ALTER TABLE [dbo].[IndentPartDetail] CHECK CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]]
GO

ALTER TABLE [dbo].[IndentPartDetail]  WITH CHECK ADD  CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]] FOREIGN KEY([SparePartMasterID])
REFERENCES [dbo].[SparePartMaster] ([ID])
GO

ALTER TABLE [dbo].[IndentPartDetail] CHECK CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]]
GO


