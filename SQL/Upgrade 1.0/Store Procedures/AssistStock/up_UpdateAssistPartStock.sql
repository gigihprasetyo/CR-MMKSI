USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateAssistPartStock]    Script Date: 13/03/2018 17:28:15 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateAssistPartStock]
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@Month nchar(10),
	@Year nchar(10),
	@DealerID int,
	@DealerCode varchar(30),
	@DealerBranchID int,
	@DealerBranchCode varchar(30),
	@SparepartMasterID int,
	@NoParts varchar(50),
	@JumlahStokAwal float,
	@JumlahDatang float,
	@HargaBeli money,
	@RemarksSystem varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[AssistPartStock]
SET
	[AssistUploadLogID] = @AssistUploadLogID,
	[Month] = @Month,
	[Year] = @Year,
	[DealerID] = @DealerID,
	[DealerCode] = @DealerCode,
	[DealerBranchID] = @DealerBranchID,
	[DealerBranchCode] = @DealerBranchCode,
	[SparepartMasterID] = @SparepartMasterID,
	[NoParts] = @NoParts,
	[JumlahStokAwal] = @JumlahStokAwal,
	[JumlahDatang] = @JumlahDatang,
	[HargaBeli] = @HargaBeli,
	[RemarksSystem] = @RemarksSystem,
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


