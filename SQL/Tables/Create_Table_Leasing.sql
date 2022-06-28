USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[Leasing]    Script Date: 02/08/2018 14:39:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Leasing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LeasingGroupName] [varchar](50) NULL,
	[LeasingCode] [varchar](16) NULL,
	[LeasingName] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Alamat] [varchar](100) NULL,
	[Kelurahan] [varchar](50) NULL,
	[Kecamatan] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
	[PostalCode] [varchar](10) NULL,
	[PhoneNo] [varchar](30) NULL,
	[Fax] [varchar](20) NULL,
	[WebSite] [varchar](20) NULL,
	[Email] [nvarchar](255) NULL,
	[ContactPerson] [varchar](50) NULL,
	[HP] [varchar](20) NULL,
	[Status] [tinyint] NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_Leasing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


