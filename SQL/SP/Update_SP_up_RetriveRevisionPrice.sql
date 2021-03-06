/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionPrice]    Script Date: 19/10/2018 14:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_RetrieveRevisionPrice]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[CategoryID],
	[RevisionTypeID],
	[Amount],
	[ValidFrom],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionPrice]

WHERE
	[ID] = @ID
SET NOCOUNT OFF
