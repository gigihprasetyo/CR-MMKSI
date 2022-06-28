USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  Table [dbo].[RevisionFaktur]    Script Date: 07/09/2018 10:55:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionFaktur](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChassisMasterID] [int] NULL,
	[EndCustomerID] [int] NULL,
	[OldEndCustomerID] [int] NULL,
	[RegNumber] [varchar](15) NULL,
	[RevisionStatus] [smallint] NULL,
	[RevisionTypeID] [smallint] NULL,
	[IsPay] [smallint] NULL,
	[NewValidationDate] [datetime] NULL,
	[NewValidationBy] [varchar](20) NULL,
	[NewConfirmationDate] [datetime] NULL,
	[NewConfirmationBy] [varchar](20) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionFaktur] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[RevisionFaktur]  WITH CHECK ADD  CONSTRAINT [FK_RevisionFaktur[many]]_ChassisMaster[one]]] FOREIGN KEY([ChassisMasterID])
REFERENCES [dbo].[ChassisMaster] ([ID])
GO

ALTER TABLE [dbo].[RevisionFaktur] CHECK CONSTRAINT [FK_RevisionFaktur[many]]_ChassisMaster[one]]]
GO

ALTER TABLE [dbo].[RevisionFaktur]  WITH CHECK ADD  CONSTRAINT [FK_RevisionFaktur[one]]_EndCustomer[one]]] FOREIGN KEY([EndCustomerID])
REFERENCES [dbo].[EndCustomer] ([ID])
GO

ALTER TABLE [dbo].[RevisionFaktur] CHECK CONSTRAINT [FK_RevisionFaktur[one]]_EndCustomer[one]]]
GO

ALTER TABLE [dbo].[RevisionFaktur]  WITH CHECK ADD  CONSTRAINT [FK_RevisionFaktur[one]]_OldEndCustomer[one]]] FOREIGN KEY([OldEndCustomerID])
REFERENCES [dbo].[EndCustomer] ([ID])
GO

ALTER TABLE [dbo].[RevisionFaktur] CHECK CONSTRAINT [FK_RevisionFaktur[one]]_OldEndCustomer[one]]]
GO


