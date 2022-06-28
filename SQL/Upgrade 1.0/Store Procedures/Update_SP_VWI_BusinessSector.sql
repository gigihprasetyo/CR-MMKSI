USE [BSIDNET_MMKSI_DMS_20180605_0100]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveVWI_BusinessSector]    Script Date: 25/06/2018 10:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveVWI_BusinessSector]
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


--------------------------------------------------------------------------------------------------------------
USE [BSIDNET_MMKSI_DMS_20180605_0100]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveVWI_BusinessSectorList]    Script Date: 25/06/2018 10:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveVWI_BusinessSectorList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[BusinessSectorName],
		[BusinessDomain],
		[BusinessName]--,
		--[Code]		
		FROM	
		[dbo].[VWI_BusinessSector] 

SET NOCOUNT OFF

