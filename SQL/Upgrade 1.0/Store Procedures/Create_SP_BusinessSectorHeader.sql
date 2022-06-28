
/****** Object:  Stored Procedure [dbo].[up_UpdateBusinessSectorHeader]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertBusinessSectorHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertBusinessSectorHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveBusinessSectorHeader]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveBusinessSectorHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveBusinessSectorHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveBusinessSectorHeaderList]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveBusinessSectorHeaderList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveBusinessSectorHeaderList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateBusinessSectorHeader]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateBusinessSectorHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateBusinessSectorHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteBusinessSectorHeader]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteBusinessSectorHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteBusinessSectorHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateBusinessSectorHeader]    Script Date: 02 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateBusinessSectorHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateBusinessSectorHeader]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertBusinessSectorHeader
	@ID int OUTPUT,
	@BusinessSectorName varchar(500),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[BusinessSectorHeader]
VALUES
(
	@BusinessSectorName,
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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveBusinessSectorHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[BusinessSectorName],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[BusinessSectorHeader]

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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveBusinessSectorHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[BusinessSectorName],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[BusinessSectorHeader] 

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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateBusinessSectorHeader
	@ID int OUTPUT,
	@BusinessSectorName varchar(500),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[BusinessSectorHeader]
SET
	[BusinessSectorName] = @BusinessSectorName,
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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteBusinessSectorHeader
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[BusinessSectorHeader]
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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateBusinessSectorHeader
	@Result	varchar(1000),
	@ID int OUTPUT,
	@BusinessSectorName varchar(500),
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
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertBusinessSectorHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateBusinessSectorHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveBusinessSectorHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveBusinessSectorHeaderList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateBusinessSectorHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteBusinessSectorHeader TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



