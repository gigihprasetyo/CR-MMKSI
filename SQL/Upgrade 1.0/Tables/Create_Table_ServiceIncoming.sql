USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Table [dbo].[AssistServiceIncoming]    Script Date: 19/03/2018 14:02:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AssistServiceIncoming](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssistUploadLogID] [int] NULL,
	[TglBukaTransaksi] [date] NULL,
	[WaktuMasuk] [time](7) NULL,
	[TglTutupTransaksi] [date] NULL,
	[WaktuKeluar] [time](7) NULL,
	[DealerID] [int] NULL,
	[DealerCode] [varchar](50) NULL,
	[DealerBranchID] [int] NULL,
	[DealerBranchCode] [varchar](50) NULL,
	[TrTraineMekanikID] [int] NULL,
	[KodeMekanik] [varchar](50) NULL,
	[NoWorkOrder] [varchar](30) NULL,
	[ChassisMasterID] [int] NULL,
	[KodeChassis] [varchar](50) NULL,
	[WorkOrderCategoryID] [int] NULL,
	[WorkOrderCategoryCode] [varchar](50) NULL,
	[KMService] [int] NULL,
	[ServicePlaceID] [int] NULL,
	[ServicePlaceCode] [varchar](50) NULL,
	[ServiceTypeID] [int] NULL,
	[ServiceTypeCode] [varchar](100) NULL,
	[TotalLC] [money] NULL,
	[MetodePembayaran] [varchar](50) NULL,
	[Model] [varchar](100) NULL,
	[Transmition] [varchar](30) NULL,
	[DriveSystem] [varchar](20) NULL,
	[RemarksSystem] [varchar](max) NULL,
	[RemarksSpecial] [varchar](300) NULL,
	[RemarksBM] [varchar](300) NULL,
	[WOStatus] [smallint] NOT NULL,
	[StatusAktif] [smallint] NOT NULL,
	[ValidateSystemStatus] [smallint] NOT NULL,
	[RowStatus] [smallint] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[LastUpdateBy] [varchar](20) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_AssistServiceIncomingUploadTemp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'remarks yang diperbolehkan lolos validasi system' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssistServiceIncoming', @level2type=N'COLUMN',@level2name=N'RemarksSpecial'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:tidak dikonfirmasi MMKSI, 1:active atau sudah dikonfirmasi MMKSI' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssistServiceIncoming', @level2type=N'COLUMN',@level2name=N'StatusAktif'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: not success validate by system, 1 :success validate system' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AssistServiceIncoming', @level2type=N'COLUMN',@level2name=N'ValidateSystemStatus'
GO


