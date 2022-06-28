USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[VehiclePurchaseHeader]    Script Date: 05/03/2018 13:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[VehiclePurchaseHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BUID] [int] NULL,
	[BUName] [varchar](100) NULL,
	[DeliveryMethod] [varchar](10) NULL,
	[Description] [varchar](100) NULL,
	[PRPOTypeID] [int] NULL,
	[PRPOName] [varchar](100) NULL,
	[DMSPOID] [int] NULL,
	[DMSPONo] [varchar](50) NULL,
	[DMSPOStatus] [int] NULL,
	[DMSPODate] [datetime] NULL,
	[VendorDescription] [varchar](100) NULL,
	[Vendor] [varchar](100) NULL,
	[PurchaseOrderNo] [varchar](50) NULL,
	[PurchaseReceiptID] [int] NULL,
	[PurchaseReceiptNo] [varchar](50) NULL,
	[PurchaseReceiptDetailNo] [varchar](50) NULL,
	[PurchaseReceiptDetailID] [int] NULL,
	[ChassisModel] [varchar](50) NULL,
	[ChassisNumberRegister] [varchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_VehiclePurchaseHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


