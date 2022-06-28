USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[VehiclePurchaseDetail]    Script Date: 05/03/2018 13:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[VehiclePurchaseDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BUID] [int] NULL,
	[BUName] [varchar](100) NULL,
	[CloseLine] [bit] NULL,
	[CloseLineName] [varchar](50) NULL,
	[CloseReason] [varchar](100) NULL,
	[Completed] [bit] NULL,
	[CompletedName] [varchar](50) NULL,
	[ProductDescription] [varchar](100) NULL,
	[ProductID] [int] NULL,
	[ProductName] [varchar](100) NULL,
	[ProductVariantID] [int] NULL,
	[ProductVariantName] [varchar](100) NULL,
	[PODetail] [varchar](50) NULL,
	[PODetailID] [varchar](50) NULL,
	[PONO] [int] NULL,
	[POName] [varchar](100) NULL,
	[PRDetailID] [int] NULL,
	[PRDetailName] [varchar](100) NULL,
	[PurchaseUnitID] [int] NULL,
	[PurchaseUnitName] [varchar](100) NULL,
	[QtyOrder] [float] NULL,
	[QtyReceipt] [float] NULL,
	[QtyReturn] [float] NULL,
	[RecallProduct] [bit] NULL,
	[RecallProductName] [varchar](50) NULL,
	[SODetail] [int] NULL,
	[SODetailName] [varchar](100) NULL,
	[ScheduledShippingDate] [datetime] NULL,
	[ServicePartsAndMaterial] [varchar](100) NULL,
	[ShippingDate] [datetime] NULL,
	[Site] [int] NULL,
	[StockNumberID] [int] NULL,
	[StockNumberName] [varchar](100) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_VehiclePurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


