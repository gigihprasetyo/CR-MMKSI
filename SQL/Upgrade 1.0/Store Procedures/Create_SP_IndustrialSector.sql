
/****** Object:  Stored Procedure [dbo].[up_UpdateIndustrialSector]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertIndustrialSector]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertIndustrialSector]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveIndustrialSector]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveIndustrialSector]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveIndustrialSector]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveIndustrialSectorList]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveIndustrialSectorList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveIndustrialSectorList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateIndustrialSector]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateIndustrialSector]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateIndustrialSector]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteIndustrialSector]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteIndustrialSector]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteIndustrialSector]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateIndustrialSector]    Script Date: 27 Februari 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateIndustrialSector]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateIndustrialSector]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertIndustrialSector
	@ID int OUTPUT,
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[IndustrialSector]
VALUES
(
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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveIndustrialSector
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[IndustrialSector]

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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveIndustrialSectorList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[IndustrialSector] 

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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateIndustrialSector
	@ID int OUTPUT,
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[IndustrialSector]
SET
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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteIndustrialSector
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[IndustrialSector]
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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateIndustrialSector
	@Result	varchar(1000),
	@ID int OUTPUT,
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
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertIndustrialSector TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateIndustrialSector TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveIndustrialSector TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveIndustrialSectorList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateIndustrialSector TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteIndustrialSector TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



