USE [BSIDNET_MMKSI_DMS_20180908_0100]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveVWI_BusinessSector]    Script Date: 15/10/2018 9:06:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveVWI_BusinessSector]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[BusinessSectorName],
	[BusinessDomain],
	[BusinessName]--,
	--[Code]	
FROM	[dbo].[VWI_BusinessSector]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO


