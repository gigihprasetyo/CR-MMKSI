
USE [BSIDNET_MMKSI_CR_IR]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertRevisionSPKFaktur
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy varchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)
	
AS
INSERT	INTO	[dbo].[RevisionSPKFaktur]
VALUES
(
	@SPKHeaderID,
	@EndCustomerID,
	@RowStatus,
	GETDATE(),	
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy)

	
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
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveRevisionSPKFaktur
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SPKHeaderID],
	[EndCustomerID],
	[RowStatus],
	[CreatedTime],
	[CreatedBy],
	[LastUpdateTime],
	[LastUpdateBy]	
FROM	[dbo].[RevisionSPKFaktur]

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
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveRevisionSPKFakturList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SPKHeaderID],
		[EndCustomerID],
		[RowStatus],
		[CreatedTime],
		[CreatedBy],
		[LastUpdateTime],
		[LastUpdateBy]		
		FROM	
		[dbo].[RevisionSPKFaktur] 

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
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateRevisionSPKFaktur
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy varchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)
	
AS

UPDATE	[dbo].[RevisionSPKFaktur]
SET
	[SPKHeaderID] = @SPKHeaderID,
	[EndCustomerID] = @EndCustomerID,
	[RowStatus] = @RowStatus,
	--[CreatedTime] = @CreatedTime,
	[CreatedBy] = @CreatedBy,
	[LastUpdateTime] = GETDATE(),
	[LastUpdateBy] = @LastUpdateBy	
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
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteRevisionSPKFaktur
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[RevisionSPKFaktur]
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
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateRevisionSPKFaktur
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SPKHeaderID int,
	@EndCustomerID int,
	@RowStatus smallint,
	@CreatedTime datetime,
	@CreatedBy varchar(20),
	@LastUpdateTime datetime,
	@LastUpdateBy varchar(20)	
AS

SET	@Result = ''

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




