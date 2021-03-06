USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertAssistPartStock]    Script Date: 13/03/2018 17:25:06 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertAssistPartStock]
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
INSERT	INTO	[dbo].[AssistPartStock]
VALUES
(
	@AssistUploadLogID,
	@Month,
	@Year,
	@DealerID,
	@DealerCode,
	@DealerBranchID,
	@DealerBranchCode,
	@SparepartMasterID,
	@NoParts,
	@JumlahStokAwal,
	@JumlahDatang,
	@HargaBeli,
	@RemarksSystem,
	@StatusAktif,
	@ValidateSystemStatus,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY

GO


