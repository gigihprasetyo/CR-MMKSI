USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_DeleteAssistPartSales]    Script Date: 13/03/2018 16:54:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_DeleteAssistPartSales]
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[AssistPartSales]
WHERE
	[ID] = @ID
