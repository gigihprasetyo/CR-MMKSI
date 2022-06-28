 

/****** Object:  UserDefinedTableType [dbo].[TV_SparePartFlow]    Script Date: 2018-12-06 20:16:29 ******/
CREATE TYPE [dbo].[TV_SparePartFlow] AS TABLE(
	[Row] [BIGINT] IDENTITY(1,1) NOT NULL,
	[POID] [INT] NOT NULL,
	[PONumber] [VARCHAR](15) NULL,
	[PODate] [SMALLDATETIME] NULL,
	[POSendDate] [DATETIME] NULL,
	[TermOfPaymentID] [INT] NULL,
	[TOPDescription] [VARCHAR](30) NULL,
	[SOID] [INT] NULL,
	[SONumber] [VARCHAR](10) NULL,
	[SODate] [SMALLDATETIME] NULL,
	[DOID] [INT] NULL,
	[DONumber] [VARCHAR](20) NULL,
	[DoDate] [DATETIME] NULL,
	[BillingID] [INT] NULL,
	[BillingNumber] [VARCHAR](10) NULL,
	[BillingDate] [DATETIME] NULL,
	[DealerID] [SMALLINT] NULL,
	[DealerCode] [VARCHAR](10) NULL,
	[OrderType] [VARCHAR](1) NULL,
	[DocumentType] [VARCHAR](1) NULL,
	[TOPCeilingStatus] [VARCHAR](200) NULL,
	[STATUS] [INT] NULL
)
GO


