/****** Object:  Stored Procedure [dbo].[up_RetrieveServiceHistory]    Script Date: 15 Mei 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveServiceHistory]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveServiceHistory]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveServiceHistoryList]    Script Date: 15 Mei 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveServiceHistoryList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveServiceHistoryList]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Mei 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveServiceHistory
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
    [ID],
	[ChassisMasterID],
	[KodeChassis],
	[SoldDealerID],
	[VehicleKindID],
	[VechileColorID],
	[ColorEngName],
	[MaterialDescription],
	[Description],
	[ChassisNumber],
	[EngineNumber],
	[SerialNumber],
	[DODate],
	[GIDate],
	[FakturDate],
	[OpenFakturDate],
	[FakturNumber],
	[TglPKT],
	[TglBukaTransaksi],
	[TglTutupTransaksi],
	[WaktuMasuk],
	[WaktuKeluar],
	[DealerCode],
	[DealerBranchCode],
	[WorkOrderCategoryCode],
	[KMService]	
FROM	[dbo].[VWI_ServiceHistory]

WHERE [ID] = @ID

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
-- Date Created	: 15 Mei 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveServiceHistoryList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
	    [ID],
		[ChassisMasterID],
		[KodeChassis],
		[SoldDealerID],
		[VehicleKindID],
		[VechileColorID],
		[ColorEngName],
		[MaterialDescription],
		[Description],
		[ChassisNumber],
		[EngineNumber],
		[SerialNumber],
		[DODate],
		[GIDate],
		[FakturDate],
		[OpenFakturDate],
		[FakturNumber],
		[TglPKT],
		[TglBukaTransaksi],
		[TglTutupTransaksi],
		[WaktuMasuk],
		[WaktuKeluar],
		[DealerCode],
		[DealerBranchCode],
		[WorkOrderCategoryCode],
		[KMService]
		FROM	
		[dbo].[VWI_ServiceHistory] 

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Mei 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

--GRANT  EXECUTE  ON [dbo].up_RetrieveServiceHistory TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveServiceHistoryList TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



