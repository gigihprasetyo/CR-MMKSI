USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_DeleteAssistPartStock]    Script Date: 13/03/2018 17:29:49 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[up_DeleteAssistPartStock]
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[AssistPartStock]
WHERE
	[ID] = @ID

GO


