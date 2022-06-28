USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[CarrosserieHeader]    Script Date: 05/03/2018 13:05:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CarrosserieHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PDIStateCode] [smallint] NULL,
	[PDIStateName] [varchar](50) NULL,
	[PDIStatusCode] [smallint] NULL,
	[PDIStatusName] [varchar](50) NULL,
	[BUID] [int] NULL,
	[BUName] [varchar](50) NULL,
	[PDINO] [int] NULL,
	[PDIName] [varchar](100) NULL,
	[PDIReceiptNO] [varchar](50) NULL,
	[PDIReceiptRef] [int] NULL,
	[PDIReceiptRefName] [varchar](100) NULL,
	[PDIReceiptStatus] [smallint] NULL,
	[PDIReceiptStatusName] [varchar](50) NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionType] [smallint] NULL,
	[VendorID] [int] NULL,
	[VendorName] [varchar](100) NULL,
	[ChassisNumber] [varchar](20) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_CarrosserieHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


