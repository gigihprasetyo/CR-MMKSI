
/****** Object:  Stored Procedure [dbo].[up_UpdateVWI_CampaignReport]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertVWI_CampaignReport]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertVWI_CampaignReport]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVWI_CampaignReport]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVWI_CampaignReport]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVWI_CampaignReport]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVWI_CampaignReportList]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVWI_CampaignReportList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVWI_CampaignReportList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateVWI_CampaignReport]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateVWI_CampaignReport]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateVWI_CampaignReport]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteVWI_CampaignReport]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteVWI_CampaignReport]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteVWI_CampaignReport]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateVWI_CampaignReport]    Script Date: 07 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateVWI_CampaignReport]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateVWI_CampaignReport]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertVWI_CampaignReport
	@ID int OUTPUT,
	@NomorSurat varchar(50),
	@Status smallint,
	@BenefitRegNo varchar(50),
	@Remarks varchar(100),
	@RowStatus smallint,
	--@LastUpdateTime datetime,
	@DetailRowStatus smallint,
	@DealerID smallint,
	@DealerCode varchar(50),
	@FakturValidationStart datetime,
	@FakturValidationEnd datetime,
	@FakturOpenStart datetime,
	@FakturOpenEnd datetime,
	@VechileTypeID smallint,
	@VechileTypeCode varchar(50),
	@VehicleTypeDesc varchar(50),
	@FormulaID char(1)
	
AS
--INSERT	INTO	[dbo].[VWI_CampaignReport]
--VALUES
--(
--	@NomorSurat,
--	@Status,
--	@BenefitRegNo,
--	@Remarks,
--	@RowStatus,
--	GETDATE(),	
--	@DetailRowStatus,
--	@DealerID,
--	@DealerCode,
--	@FakturValidationStart,
--	@FakturValidationEnd,
--	@FakturOpenStart,
--	@FakturOpenEnd,
--	@VechileTypeID,
--	@VechileTypeCode,
--	@VehicleTypeDesc,
--	@FormulaID)

	
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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVWI_CampaignReport
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[NomorSurat],
	[Status],
	[BenefitRegNo],
	[Remarks],
	[RowStatus],
	[LastUpdateTime],
	[DetailRowStatus],
	[DealerID],
	[DealerCode],
	[FakturValidationStart],
	[FakturValidationEnd],
	[FakturOpenStart],
	[FakturOpenEnd],
	[VechileTypeID],
	[VechileTypeCode],
	[VehicleTypeDesc],
	[FormulaID]	
FROM	[dbo].[VWI_CampaignReport]

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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVWI_CampaignReportList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[NomorSurat],
		[Status],
		[BenefitRegNo],
		[Remarks],
		[RowStatus],
		[LastUpdateTime],
		[DetailRowStatus],
		[DealerID],
		[DealerCode],
		[FakturValidationStart],
		[FakturValidationEnd],
		[FakturOpenStart],
		[FakturOpenEnd],
		[VechileTypeID],
		[VechileTypeCode],
		[VehicleTypeDesc],
		[FormulaID]		
		FROM	
		[dbo].[VWI_CampaignReport] 

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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateVWI_CampaignReport
	@ID int OUTPUT,
	@NomorSurat varchar(50),
	@Status smallint,
	@BenefitRegNo varchar(50),
	@Remarks varchar(100),
	@RowStatus smallint,
	--@LastUpdateTime datetime,
	@DetailRowStatus smallint,
	@DealerID smallint,
	@DealerCode varchar(50),
	@FakturValidationStart datetime,
	@FakturValidationEnd datetime,
	@FakturOpenStart datetime,
	@FakturOpenEnd datetime,
	@VechileTypeID smallint,
	@VechileTypeCode varchar(50),
	@VehicleTypeDesc varchar(50),	
	@FormulaID char(1)
	
AS

SET @ID = @@IDENTITY
--UPDATE	[dbo].[VWI_CampaignReport]
--SET
--	[NomorSurat] = @NomorSurat,
--	[Status] = @Status,
--	[BenefitRegNo] = @BenefitRegNo,
--	[Remarks] = @Remarks,
--	[RowStatus] = @RowStatus,
--	[LastUpdateTime] = GETDATE(),
--	[DetailRowStatus] = @DetailRowStatus,
--	[DealerID] = @DealerID,
--	[DealerCode] = @DealerCode,
--	[FakturValidationStart] = @FakturValidationStart,
--	[FakturValidationEnd] = @FakturValidationEnd,
--	[FakturOpenStart] = @FakturOpenStart,
--	[FakturOpenEnd] = @FakturOpenEnd,
--	[VechileTypeID] = @VechileTypeID,
--	[VechileTypeCode] = @VechileTypeCode,
--	[VehicleTypeDesc] = @VehicleTypeDesc,
--	[FormulaID] = @FormulaID	
--WHERE
--	[ID] = @ID

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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteVWI_CampaignReport
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[VWI_CampaignReport]
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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateVWI_CampaignReport
	@Result	varchar(1000),
	@ID int OUTPUT,
	@NomorSurat varchar(50),
	@Status smallint,
	@BenefitRegNo varchar(50),
	@Remarks varchar(100),
	@RowStatus smallint,
	@LastUpdateTime datetime,
	@DetailRowStatus smallint,
	@DealerID smallint,
	@DealerCode varchar(50),
	@FakturValidationStart datetime,
	@FakturValidationEnd datetime,
	@FakturOpenStart datetime,
	@FakturOpenEnd datetime,
	@VechileTypeID smallint,
	@VechileTypeCode varchar(50),
	@VehicleTypeDesc varchar(50),
	@FormulaID char(1)	
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
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertVWI_CampaignReport TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateVWI_CampaignReport TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVWI_CampaignReport TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVWI_CampaignReportList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateVWI_CampaignReport TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteVWI_CampaignReport TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO