/****** Object:  Stored Procedure [dbo].[up_RetrieveMSPMembership]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveMSPMembership]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveMSPMembership]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveMSPMembershipList]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveMSPMembershipList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveMSPMembershipList]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveMSPMembership
	@ID bigint OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[MSPCustomerID],
	[DealerId],
	[DealerCode],
	[DealerName],
	[ChassisMasterID],
	[MSPCode],
	[ChassisNumber],
	[VechileColorID],
	[MSPKm],
	[Duration],
	[Description],
	[ValidUntil],
	[RegistrationDate]	
FROM	[dbo].[VWI_MSPMembership]

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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveMSPMembershipList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[MSPCustomerID],
		[DealerId],
		[DealerCode],
		[DealerName],
		[ChassisMasterID],
		[MSPCode],
		[ChassisNumber],
		[VechileColorID],
		[MSPKm],
		[Duration],
		[Description],
		[ValidUntil],
		[RegistrationDate]		
		FROM	
		[dbo].[VWI_MSPMembership] 

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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_RetrieveMSPMembership TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveMSPMembershipList TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



