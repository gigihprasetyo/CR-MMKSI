set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_DeleteSparePartConversion
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartConversion]
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_InsertSparePartConversion
	@ID int OUTPUT,
	@SparePartMasterID int,
	@UoMto varchar(18),
	@Qty int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartConversion]
VALUES
(
	@SparePartMasterID,
	@UoMto,
	@Qty,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_RetrieveSparePartConversion
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartMasterID],
	[UoMto],
	[Qty],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartConversion]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_RetrieveSparePartConversionList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartMasterID],
		[UoMto],
		[Qty],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartConversion] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_UpdateSparePartConversion
	@ID int OUTPUT,
	@SparePartMasterID int,
	@UoMto varchar(18),
	@Qty int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartConversion]
SET
	[SparePartMasterID] = @SparePartMasterID,
	[UoMto] = @UoMto,
	[Qty] = @Qty,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_ValidateSparePartConversion
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartMasterID int,
	@UoMto varchar(18),
	@Qty int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

commit
go


