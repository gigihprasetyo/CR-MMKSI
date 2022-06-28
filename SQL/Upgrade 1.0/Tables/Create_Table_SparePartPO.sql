USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[SparePartPO]    Script Date: 06/03/2018 13:43:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SparePartPO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PONumber] [varchar](15) NULL,
	[OrderType] [varchar](1) NULL,
	[DealerID] [smallint] NULL,
	[PODate] [smalldatetime] NULL,
	[DeliveryDate] [datetime] NULL,
	[ProcessCode] [varchar](1) NULL,
	[CancelRequestBy] [varchar](20) NULL,
	[IndentTransfer] [tinyint] NULL,
	[PickingTicket] [varchar](100) NULL,
	[SentPODate] [datetime] NULL,
	[IsTransfer] [bit] NULL,
	[Purpose] [varchar](max) NULL,
	[DMSPRNo] [varchar](20) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SparePartPO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [IPONumber_SparePartPO] UNIQUE NONCLUSTERED 
(
	[PONumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SparePartPO] ADD  CONSTRAINT [DF_SparePartPO_IndentTransfer]  DEFAULT ((0)) FOR [IndentTransfer]
GO

ALTER TABLE [dbo].[SparePartPO]  WITH CHECK ADD  CONSTRAINT [FK_SparePartPO_Dealer[one]]] FOREIGN KEY([DealerID])
REFERENCES [dbo].[Dealer] ([ID])
GO

ALTER TABLE [dbo].[SparePartPO] CHECK CONSTRAINT [FK_SparePartPO_Dealer[one]]]
GO


