
/****** Object:  Stored Procedure [dbo].[up_UpdateBapiLog]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertBapiLog]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertBapiLog]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveBapiLog]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveBapiLog]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveBapiLog]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveBapiLogList]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveBapiLogList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveBapiLogList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateBapiLog]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateBapiLog]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateBapiLog]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteBapiLog]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteBapiLog]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteBapiLog]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateBapiLog]    Script Date: 22 Februari 2019 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateBapiLog]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateBapiLog]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertBapiLog
	@ID int OUTPUT,
	@SubmitDate datetime,
	@UserName varchar(50),
	@KindLog smallint,
	@Body varchar(6000),
	@Status bit,
	@ResponseLog varchar(6000),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[BapiLog]
VALUES
(
	@SubmitDate,
	@UserName,
	@KindLog,
	@Body,
	@Status,
	@ResponseLog,
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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveBapiLog
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SubmitDate],
	[UserName],
	[KindLog],
	[Body],
	[Status],
	[ResponseLog],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[BapiLog]

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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveBapiLogList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SubmitDate],
		[UserName],
		[KindLog],
		[Body],
		[Status],
		[ResponseLog],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[BapiLog] 

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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateBapiLog
	@ID int OUTPUT,
	@SubmitDate datetime,
	@UserName varchar(50),
	@KindLog smallint,
	@Body varchar(6000),
	@Status bit,
	@ResponseLog varchar(6000),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[BapiLog]
SET
	[SubmitDate] = @SubmitDate,
	[UserName] = @UserName,
	[KindLog] = @KindLog,
	[Body] = @Body,
	[Status] = @Status,
	[ResponseLog] = @ResponseLog,
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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteBapiLog
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[BapiLog]
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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateBapiLog
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SubmitDate datetime,
	@UserName varchar(50),
	@KindLog smallint,
	@Body varchar(6000),
	@Status bit,
	@ResponseLog varchar(6000),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
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
-- Date Created	: 22 Februari 2019
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertBapiLog TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateBapiLog TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveBapiLog TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveBapiLogList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateBapiLog TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteBapiLog TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



