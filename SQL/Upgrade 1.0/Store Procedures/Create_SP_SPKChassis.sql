
/****** Object:  Stored Procedure [dbo].[up_UpdateSPKChassis]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertSPKChassis]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertSPKChassis]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSPKChassis]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSPKChassis]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSPKChassis]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSPKChassisList]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSPKChassisList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSPKChassisList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateSPKChassis]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateSPKChassis]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateSPKChassis]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteSPKChassis]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteSPKChassis]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteSPKChassis]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateSPKChassis]    Script Date: 22 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateSPKChassis]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateSPKChassis]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSPKChassis
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SPKChassis]
VALUES
(
	@SPKDetailID,
	@ChassisMasterID,
	@MatchingType,
	@MatchingDate,
	@MatchingNumber,
	@ReferenceNumber,
	@KeyNumber,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSPKChassis
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SPKDetailID],
	[ChassisMasterID],
	[MatchingType],
	[MatchingDate],
	[MatchingNumber],
	[ReferenceNumber],
	[KeyNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SPKChassis]

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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSPKChassisList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SPKDetailID],
		[ChassisMasterID],
		[MatchingType],
		[MatchingDate],
		[MatchingNumber],
		[ReferenceNumber],
		[KeyNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SPKChassis] 

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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateSPKChassis
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SPKChassis]
SET
	[SPKDetailID] = @SPKDetailID,
	[ChassisMasterID] = @ChassisMasterID,
	[MatchingType] = @MatchingType,
	[MatchingDate] = @MatchingDate,
	[MatchingNumber] = @MatchingNumber,
	[ReferenceNumber] = @ReferenceNumber,
	[KeyNumber] = @KeyNumber,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSPKChassis
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SPKChassis]
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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateSPKChassis
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SPKDetailID int,
	@ChassisMasterID int,
	@MatchingType smallint,
	@MatchingDate datetime,
	@MatchingNumber varchar(50),
	@ReferenceNumber varchar(50),
	@KeyNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	@CreatedTime datetime,
	@LastUpdateBy varchar(50),
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
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertSPKChassis TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateSPKChassis TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSPKChassis TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSPKChassisList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateSPKChassis TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteSPKChassis TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



