USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveCustomerCase]    Script Date: 20/03/2018 12:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveCustomerCase]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[SalesforceID],
	[CaseNumber],
	[CustomerName],
	[Phone],
	[Email],
	[Category],
	[SubCategory1],
	[SubCategory2],
	[SubCategory3],
	[SubCategory4],
	[CallerType],
	[CarType],
	[Variant],
	[EngineNumber],
	[ChassisNumber],
	[Odometer],
	[PlateNumber],
	[Priority],
	[CaseNumberReff],
	[CaseDate],
	[Subject],
	[Description],
	[Status],
	[ReservationNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTIme]	
FROM	[dbo].[CustomerCase]

WHERE
	[ID] = @ID

SET NOCOUNT OFF


