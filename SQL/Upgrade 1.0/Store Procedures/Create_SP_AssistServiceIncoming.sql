
/****** Object:  Stored Procedure [dbo].[up_UpdateAssistServiceIncoming]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertAssistServiceIncoming]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertAssistServiceIncoming]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveAssistServiceIncoming]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveAssistServiceIncoming]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveAssistServiceIncoming]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveAssistServiceIncomingList]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveAssistServiceIncomingList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveAssistServiceIncomingList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateAssistServiceIncoming]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateAssistServiceIncoming]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateAssistServiceIncoming]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteAssistServiceIncoming]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteAssistServiceIncoming]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteAssistServiceIncoming]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateAssistServiceIncoming]    Script Date: 19 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateAssistServiceIncoming]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateAssistServiceIncoming]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertAssistServiceIncoming]    Script Date: 13/04/2018 10:01:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertAssistServiceIncoming]
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglBukaTransaksi date,
	@WaktuMasuk varchar(20),
	@TglTutupTransaksi date,
	@WaktuKeluar varchar(20),
	@DealerID int,
	@DealerCode varchar(50),
	@TrTraineMekanikID int,
	@KodeMekanik varchar(50),
	@NoWorkOrder varchar(30),
	@ChassisMasterID int,
	@KodeChassis varchar(50),
	@WorkOrderCategoryID int,
	@WorkOrderCategoryCode varchar(50),
	@KMService int,
	@ServicePlaceID int,
	@ServicePlaceCode varchar(50),
	@ServiceTypeID int,
	@ServiceTypeCode varchar(50),
	@TotalLC money,
	@MetodePembayaran varchar(50),
	@Model varchar(100),
	@Transmition varchar(30),
	@DriveSystem varchar(20),
	@DealerBranchID INT,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@RemarksSpecial varchar(MAX),
	@RemarksBM varchar(MAX),
	@WOStatus smallint,
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[AssistServiceIncoming]
([AssistUploadLogID]
           ,[TglBukaTransaksi]
           ,[WaktuMasuk]
           ,[TglTutupTransaksi]
           ,[WaktuKeluar]
           ,[DealerID]
           ,[DealerCode]
           ,[TrTraineMekanikID]
           ,[KodeMekanik]
           ,[NoWorkOrder]
           ,[ChassisMasterID]
           ,[KodeChassis]
           ,[WorkOrderCategoryID]
           ,[WorkOrderCategoryCode]
           ,[KMService]
           ,[ServicePlaceID]
           ,[ServicePlaceCode]
           ,[ServiceTypeID]
           ,[ServiceTypeCode]
           ,[TotalLC]
           ,[MetodePembayaran]
           ,[Model]
           ,[Transmition]
           ,[DriveSystem]
           ,[DealerBranchID]
           ,[DealerBranchCode]
           ,[RemarksSystem]
           ,[RemarksSpecial]
           ,[RemarksBM]
		   ,[WOStatus]
           ,[StatusAktif]
           ,[ValidateSystemStatus]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
VALUES
(
	@AssistUploadLogID,
	@TglBukaTransaksi,
	Convert(time,@WaktuMasuk),
	@TglTutupTransaksi,
	Convert(time,@WaktuKeluar),
	@DealerID,
	@DealerCode,
	@TrTraineMekanikID,
	@KodeMekanik,
	@NoWorkOrder,
	@ChassisMasterID,
	@KodeChassis,
	@WorkOrderCategoryID,
	@WorkOrderCategoryCode,
	@KMService,
	@ServicePlaceID,
	@ServicePlaceCode,
	@ServiceTypeID,
	@ServiceTypeCode,
	@TotalLC,
	@MetodePembayaran,
	@Model,
	@Transmition,
	@DriveSystem,
	@DealerBranchID,
	@DealerBranchCode,
	@RemarksSystem,
	@RemarksSpecial,
	@RemarksBM,
	@WOStatus,
	@StatusAktif,
	@ValidateSystemStatus,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY



GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveAssistServiceIncoming]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AssistUploadLogID],
	[TglBukaTransaksi],
	[WaktuMasuk],
	[TglTutupTransaksi],
	[WaktuKeluar],
	[DealerID],
	[DealerCode],
	[TrTraineMekanikID],
	[KodeMekanik],
	[NoWorkOrder],
	[ChassisMasterID],
	[KodeChassis],
	[WorkOrderCategoryID],
	[WorkOrderCategoryCode],
	[KMService],
	[ServicePlaceID],
	[ServicePlaceCode],
	[ServiceTypeID],
	[ServiceTypeCode],
	[TotalLC],
	[MetodePembayaran],
	[Model],
	[Transmition],
	[DriveSystem],
	[DealerBranchID],
    [DealerBranchCode],
	[RemarksSystem],
	[RemarksSpecial],
	[RemarksBM],
	[WOStatus],
	[StatusAktif],
	[ValidateSystemStatus],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[AssistServiceIncoming]

WHERE
	[ID] = @ID


SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveAssistServiceIncomingList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[AssistUploadLogID],
		[TglBukaTransaksi],
		[WaktuMasuk],
		[TglTutupTransaksi],
		[WaktuKeluar],
		[DealerID],
		[DealerCode],
		[TrTraineMekanikID],
		[KodeMekanik],
		[NoWorkOrder],
		[ChassisMasterID],
		[KodeChassis],
		[WorkOrderCategoryID],
		[WorkOrderCategoryCode],
		[KMService],
		[ServicePlaceID],
		[ServicePlaceCode],
		[ServiceTypeID],
		[ServiceTypeCode],
		[TotalLC],
		[MetodePembayaran],
		[Model],
		[Transmition],
		[DriveSystem],
		[DealerBranchID],
		[DealerBranchCode],
		[RemarksSystem],
		[RemarksSpecial],
		[RemarksBM],
		[WOStatus],
		[StatusAktif],
		[ValidateSystemStatus],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[AssistServiceIncoming] 

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateAssistServiceIncoming]
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglBukaTransaksi date,
	@WaktuMasuk varchar(20),
	@TglTutupTransaksi date,
	@WaktuKeluar varchar(20),
	@DealerID int,
	@DealerCode varchar(50),
	@TrTraineMekanikID int,
	@KodeMekanik varchar(50),
	@NoWorkOrder varchar(30),
	@ChassisMasterID int,
	@KodeChassis varchar(50),
	@WorkOrderCategoryID int,
	@WorkOrderCategoryCode varchar(50),
	@KMService int,
	@ServicePlaceID int,
	@ServicePlaceCode varchar(50),
	@ServiceTypeID int,
	@ServiceTypeCode varchar(50),
	@TotalLC money,
	@MetodePembayaran varchar(50),
	@Model varchar(100),
	@Transmition varchar(30),
	@DriveSystem varchar(20),
	@DealerBranchID int,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@RemarksSpecial varchar(MAX),
	@RemarksBM varchar(MAX),
	@WOStatus smallint,
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[AssistServiceIncoming]
SET
	[AssistUploadLogID] = @AssistUploadLogID,
	[TglBukaTransaksi] = @TglBukaTransaksi,
	[WaktuMasuk] = Convert(time,@WaktuMasuk),
	[TglTutupTransaksi] = @TglTutupTransaksi,
	[WaktuKeluar] = Convert(time,@WaktuKeluar),
	[DealerID] = @DealerID,
	[DealerCode] = @DealerCode,
	[TrTraineMekanikID] = @TrTraineMekanikID,
	[KodeMekanik] = @KodeMekanik,
	[NoWorkOrder] = @NoWorkOrder,
	[ChassisMasterID] = @ChassisMasterID,
	[KodeChassis] = @KodeChassis,
	[WorkOrderCategoryID] = @WorkOrderCategoryID,
	[WorkOrderCategoryCode] = @WorkOrderCategoryCode,
	[KMService] = @KMService,
	[ServicePlaceID] = @ServicePlaceID,
	[ServicePlaceCode] = @ServicePlaceCode,
	[ServiceTypeID] = @ServiceTypeID,
	[ServiceTypeCode] = @ServiceTypeCode,
	[TotalLC] = @TotalLC,
	[MetodePembayaran] = @MetodePembayaran,
	[Model] = @Model,
	[Transmition] = @Transmition,
	[DriveSystem] = @DriveSystem,
	[DealerBranchID] = @DealerBranchID,
    [DealerBranchCode] = @DealerBranchCode,
	[RemarksSystem] = @RemarksSystem,
	[RemarksSpecial] = @RemarksSpecial,
	[RemarksBM] = @RemarksBM,
	[WOStatus] = @WOStatus,
	[StatusAktif] = @StatusAktif,
	[ValidateSystemStatus] = @ValidateSystemStatus,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteAssistServiceIncoming
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[AssistServiceIncoming]
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateAssistServiceIncoming
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglBukaTransaksi date,
	@WaktuMasuk time,
	@TglTutupTransaksi date,
	@WaktuKeluar time,
	@DealerID int,
	@DealerCode varchar(50),
	@DealerBranchID int,
	@DealerBranchCode varchar(50),
	@TrTraineMekanikID int,
	@KodeMekanik varchar(50),
	@NoWorkOrder varchar(30),
	@ChassisMasterID int,
	@KodeChassis varchar(50),
	@WorkOrderCategoryID int,
	@WorkOrderCategoryCode varchar(50),
	@KMService int,
	@ServicePlaceID int,
	@ServicePlaceCode varchar(50),
	@ServiceTypeID int,
	@ServiceTypeCode varchar(100),
	@TotalLC money,
	@MetodePembayaran varchar(50),
	@Model varchar(100),
	@Transmition varchar(30),
	@DriveSystem varchar(20),
	@RemarksSystem varchar(max),
	@RemarksSpecial varchar(300),
	@RemarksBM varchar(300),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertAssistServiceIncoming TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateAssistServiceIncoming TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveAssistServiceIncoming TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveAssistServiceIncomingList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateAssistServiceIncoming TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteAssistServiceIncoming TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



