USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SPKCustomer]    Script Date: 02/03/2018 11:07:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SPKCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[ReffCode] [varchar](50) NULL,
	[TipeCustomer] [smallint] NULL,
	[TipePerusahaan] [smallint] NULL,
	[Name1] [nvarchar](50) NULL,
	[Name2] [nvarchar](50) NULL,
	[Name3] [nvarchar](50) NULL,
	[Alamat] [nvarchar](100) NULL,
	[Kelurahan] [nvarchar](50) NULL,
	[Kecamatan] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[PreArea] [varchar](20) NULL,
	[CityID] [smallint] NULL,
	[PrintRegion] [varchar](1) NULL,
	[PhoneNo] [nvarchar](30) NULL,
	[OfficeNo] [nvarchar](30) NULL,
	[HomeNo] [nvarchar](30) NULL,
	[HpNo] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NULL,
	[Status] [int] NULL,
	[MCPStatus] [smallint] NULL,
	[LKPPStatus] [smallint] NULL,
	[SAPCustomerID] [int] NULL,
	[LKPPReference] [varchar](50) NULL,
	[BusinessSectorDetailID] int NULL,
	[ImagePath] [nvarchar](200) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
	[LastUpdateBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_SPKCustomer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SPKCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SPKCustomer[one]]_City[one]]] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
GO

ALTER TABLE [dbo].[SPKCustomer] CHECK CONSTRAINT [FK_SPKCustomer[one]]_City[one]]]
GO

ALTER TABLE [dbo].[SPKCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SPKCustomer[one]]_SAPCustomer[one]]] FOREIGN KEY([SAPCustomerID])
REFERENCES [dbo].[SAPCustomer] ([ID])
GO

ALTER TABLE [dbo].[SPKCustomer] CHECK CONSTRAINT [FK_SPKCustomer[one]]_SAPCustomer[one]]]
GO

ALTER TABLE [SPKCustomer]  WITH CHECK ADD  CONSTRAINT [FK_SPKCustomer_BusinessSectorDetail[one]]] FOREIGN KEY([BusinessSectorDetailID])
REFERENCES [BusinessSectorDetail] ([ID])
GO

ALTER TABLE [SPKCustomer] CHECK CONSTRAINT [FK_SPKCustomer_BusinessSectorDetail[one]]]
GO
