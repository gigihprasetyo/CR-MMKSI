USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[CarrosserieHeader]    Script Date: 05/03/2018 8:46:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CarrosserieDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PDIStateCode] [smallint] NULL,
	[PDIStateName] [varchar](50) NULL,
	[PDIStatusCode] [smallint] NULL,
	[PDIStatusName] [varchar](50) NULL,
	[AccessorriesDescription] [varchar](100) NULL,
	[AccessorriesID] [int] NULL,
	[AccessorriesName] [varchar](100) NULL,
	[BUID] [int] NULL,
	[BUName] [varchar](100) NULL,
	[KITID] [int] NULL,
	[KITName] [varchar](100) NULL,
	[PBUID] [int] NULL,
	[PBUName] [varchar](100) NULL,
	[PDIDetailID] [int] NULL,
	[PDIDetailName] [varchar](100) NULL,
	[PDIReceiptDetailID] [int] NULL,
	[PDIReceiptDetailNO] [varchar](50) NULL,
	[PDIReceiptNO] [varchar](50) NULL,
	[PDIReceiptName] [varchar](100) NULL,
	[ReceiveQuantity] [float] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_CarrosserieDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


