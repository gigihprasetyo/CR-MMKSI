USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveEstimationEquipHeader]    Script Date: 18/03/2018 21:07:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveEstimationEquipHeader]
	@ID int OUTPUT
	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[EstimationNumber],
	[DealerID],
	[DepositBKewajibanHeaderID],
	[Status],
	[DMSPRNo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdatedBy],
	[LastUpdatedTime]
	
FROM	[dbo].[EstimationEquipHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF


GO

