USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[IndentPartHeader]    Script Date: 06/03/2018 10:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IndentPartHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DealerID] [smallint] NULL,
	[RequestNo] [varchar](13) NULL,
	[RequestDate] [datetime] NULL,
	[MaterialType] [int] NULL,
	[Status] [tinyint] NULL,
	[StatusKTB] [tinyint] NULL,
	[SubmitFile] [varchar](50) NULL,
	[PaymentType] [tinyint] NULL,
	[Price] [money] NULL,
	[KTBConfirmedDate] [datetime] NULL,
	[DescID] [tinyint] NULL,
	[ChassisNumber] [varchar](20) NULL,
	[DMSPRNo] [varchar](20) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK__IndentPartHeader__44AB0736] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IndentPartHeader]  WITH CHECK ADD  CONSTRAINT [FK_IndentPartHeader[many]]_Dealer[one]]] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[IndentPartHeader] CHECK CONSTRAINT [FK_IndentPartHeader[many]]_Dealer[one]]]
GO


