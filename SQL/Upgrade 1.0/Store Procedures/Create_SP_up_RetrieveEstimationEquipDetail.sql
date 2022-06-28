USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveEstimationEquipDetail]    Script Date: 05/03/2018 13:42:33 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveEstimationEquipDetail]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[EstimationEquipHeaderID],
	[SparePartMasterID],
	[Harga],
	[Discount],
	[TotalForecast],
	[EstimationUnit],
	[Status],
	[ConfirmedDate],
	[Remark],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdatedBy],
	[LastUpdatedTime]	
FROM	[dbo].[EstimationEquipDetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO

