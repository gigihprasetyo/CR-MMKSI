/****** Object:  Table [dbo].[RevisionPaymentDetail]    Script Date: 01/10/2018 14:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RevisionPaymentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RevisionFakturID] [int] NULL,
	[RevisionPaymentHeaderID] [int] NULL,
	[RevisionSAPDocID] [int] NULL,
	[IsCancel] [smallint] NULL,
	[CancelReason] [varchar](250) NULL,
	[RowStatus] [smallint] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_RevisionPaymentDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


