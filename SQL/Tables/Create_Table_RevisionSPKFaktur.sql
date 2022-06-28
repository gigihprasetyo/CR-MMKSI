USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[RevisionSPKFaktur]    Script Date: 28/08/2018 17:36:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionSPKFaktur](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SPKHeaderID] [int] NULL,
	[EndCustomerID] [int] NULL,
	[RowStatus] [smallint] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
 CONSTRAINT [PK_RevisionSPKFaktur] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RevisionSPKFaktur]  WITH CHECK ADD  CONSTRAINT [FK_RevisionSPKFaktur[many]]_SPKHeader[one]]] FOREIGN KEY([SPKHeaderID])
REFERENCES [dbo].[SPKHeader] ([ID])
GO

ALTER TABLE [dbo].[RevisionSPKFaktur] CHECK CONSTRAINT [FK_RevisionSPKFaktur[many]]_SPKHeader[one]]]
GO

ALTER TABLE [dbo].[RevisionSPKFaktur]  WITH CHECK ADD  CONSTRAINT [FK_RevisionSPKFaktur[one]]_EndCustomer[one]]] FOREIGN KEY([EndCustomerID])
REFERENCES [dbo].[EndCustomer] ([ID])
GO

ALTER TABLE [dbo].[RevisionSPKFaktur] CHECK CONSTRAINT [FK_RevisionSPKFaktur[one]]_EndCustomer[one]]]
GO


