USE [BSIDNET_MMKSI_DMS_20180605_0100]
GO

/******
	- add new field Handphone varchar(20)
	- 12 Jul 2018
	- Mitrais Team
*******/

/****** Object:  Table [dbo].[FleetCustomerContact]    Script Date: 12/07/2018 10:36:05 ******/
DROP TABLE [dbo].[FleetCustomerContact]
GO

/****** Object:  Table [dbo].[FleetCustomerContact]    Script Date: 12/07/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[FleetCustomerContact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FleetCustomerID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Position] [varchar](50) NULL,
	[PhoneNo] [varchar](20) NULL,
	[Handphone] [varchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [varchar](50) NULL,
	[LastUpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_FleetCustomerContact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


