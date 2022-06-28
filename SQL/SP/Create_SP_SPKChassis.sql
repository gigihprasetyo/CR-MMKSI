USE [BSIDNET_MMKSI_CR_IR]
GO

/****** Object:  StoredProcedure [dbo].[up_DeleteSPKChassis]    Script Date: 09/08/2018 13:05:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_DeleteSPKChassis]
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SPKChassis]
WHERE
	[ID] = @ID


GO


/****** Object:  StoredProcedure [dbo].[up_InsertSPKChassis]    Script Date: 09/08/2018 13:05:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertSPKChassis]
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


/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKChassis]    Script Date: 09/08/2018 13:06:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveSPKChassis]
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



/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKChassisList]    Script Date: 09/08/2018 13:06:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveSPKChassisList]

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



/****** Object:  StoredProcedure [dbo].[up_UpdateSPKChassis]    Script Date: 09/08/2018 13:07:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateSPKChassis]
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




/****** Object:  StoredProcedure [dbo].[up_ValidateSPKChassis]    Script Date: 09/08/2018 13:07:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_ValidateSPKChassis]
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




