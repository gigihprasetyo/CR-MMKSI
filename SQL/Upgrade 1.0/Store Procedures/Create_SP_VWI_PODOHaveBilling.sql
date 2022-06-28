
/****** Object:  Stored Procedure [dbo].[up_RetrieveVWI_PODOHaveBilling]    Script Date: 13 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVWI_PODOHaveBilling]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVWI_PODOHaveBilling]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVWI_PODOHaveBillingList]    Script Date: 13 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVWI_PODOHaveBillingList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVWI_PODOHaveBillingList]
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 April 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVWI_PODOHaveBilling
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartDOID],
	[DONumber],
	[DealerID],
	[DealerCode],
	[DoDate],
	[BillingDate],
	[ExpeditionNo],
	[LastUpdateTime]	
FROM	[dbo].[VWI_PODOHaveBilling]

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
-- Date Created	: 13 April 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVWI_PODOHaveBillingList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartDOID],
		[DONumber],
		[DealerID],
		[DealerCode],
		[DoDate],
		[BillingDate],
		[ExpeditionNo],
		[LastUpdateTime]		
		FROM	
		[dbo].[VWI_PODOHaveBilling] 

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
-- Date Created	: 13 April 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_RetrieveVWI_PODOHaveBilling TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVWI_PODOHaveBillingList TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



