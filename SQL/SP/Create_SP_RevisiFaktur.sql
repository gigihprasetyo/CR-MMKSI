


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertRevisionFaktur
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionFaktur]
VALUES
(
	@ChassisMasterID,
	@EndCustomerID,
	@OldEndCustomerID,
	@RegNumber,
	@RevisionStatus,
	@RevisionTypeID,
	@IsPay,
	@NewValidationDate,
	@NewValidationBy,
	@NewConfirmationDate,
	@NewConfirmationBy,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY

--CREATE Autonumber @RegNumber/Nomor Pengajuan
DECLARE @SeqNum int

SELECT @SeqNum = MAX(RIGHT(RegNumber,6))
FROM RevisionFaktur 
WHERE '20'+SUBSTRING(RegNumber,3,2) = Year(getdate())

SET @SeqNum = ISNULL(@SeqNum,0)+1
SELECT @RegNumber='RF'+RIGHT(CONVERT(CHAR(4),Year(getdate())),2)+REPLICATE('0',6-LEN(@SeqNum))+CONVERT(VARCHAR(6),@SeqNum)

UPDATE RevisionFaktur SET RegNumber=@RegNumber
WHERE ID=@ID

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
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveRevisionFaktur
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ChassisMasterID],
	[EndCustomerID],
	[OldEndCustomerID],
	[RegNumber],
	[RevisionStatus],
	[RevisionTypeID],
	[IsPay],
	[NewValidationDate],
	[NewValidationBy],
	[NewConfirmationDate],
	[NewConfirmationBy],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionFaktur]

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
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveRevisionFakturList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ChassisMasterID],
		[EndCustomerID],
		[OldEndCustomerID],
		[RegNumber],
		[RevisionStatus],
		[RevisionTypeID],
		[IsPay],
		[NewValidationDate],
		[NewValidationBy],
		[NewConfirmationDate],
		[NewConfirmationBy],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionFaktur] 

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
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateRevisionFaktur
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[RevisionFaktur]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[EndCustomerID] = @EndCustomerID,
	[OldEndCustomerID] = @OldEndCustomerID,
	[RegNumber] = @RegNumber,
	[RevisionStatus] = @RevisionStatus,
	[RevisionTypeID] = @RevisionTypeID,
	[IsPay] = @IsPay,
	[NewValidationDate] = @NewValidationDate,
	[NewValidationBy] = @NewValidationBy,
	[NewConfirmationDate] = @NewConfirmationDate,
	[NewConfirmationBy] = @NewConfirmationBy,
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
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteRevisionFaktur
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[RevisionFaktur]
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
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateRevisionFaktur
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
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




