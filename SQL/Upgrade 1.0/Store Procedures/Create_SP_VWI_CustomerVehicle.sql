
/****** Object:  Stored Procedure [dbo].[up_UpdateCustomerVehicle]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertCustomerVehicle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertCustomerVehicle]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCustomerVehicle]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCustomerVehicle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCustomerVehicle]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCustomerVehicleList]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCustomerVehicleList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCustomerVehicleList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateCustomerVehicle]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateCustomerVehicle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateCustomerVehicle]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteCustomerVehicle]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteCustomerVehicle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteCustomerVehicle]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateCustomerVehicle]    Script Date: 24 April 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateCustomerVehicle]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateCustomerVehicle]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertCustomerVehicle
	@ID int OUTPUT,
	@CustomerCode varchar(10)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[VWI_CustomerVehicle]
VALUES
(
	@CustomerCode,
	GETDATE())

	
SET @ID = @@IDENTITY

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveCustomerVehicle
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[CustomerCode],
	[LastUpdateTime]	
FROM	[dbo].[VWI_CustomerVehicle]

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveCustomerVehicleList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[CustomerCode],
		[LastUpdateTime]		
		FROM	
		[dbo].[VWI_CustomerVehicle] 

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateCustomerVehicle
	@ID int OUTPUT,
	@CustomerCode varchar(10)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[VWI_CustomerVehicle]
SET
	[CustomerCode] = @CustomerCode,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteCustomerVehicle
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[VWI_CustomerVehicle]
WHERE
	[ID] = @ID

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateCustomerVehicle
	@Result	varchar(1000),
	@ID int OUTPUT,
	@CustomerCode varchar(10),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

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
-- Date Created	: 24 April 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertCustomerVehicle TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateCustomerVehicle TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCustomerVehicle TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCustomerVehicleList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateCustomerVehicle TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteCustomerVehicle TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



