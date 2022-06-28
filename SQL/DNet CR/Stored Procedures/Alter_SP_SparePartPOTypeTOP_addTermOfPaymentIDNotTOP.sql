/****** Object:  StoredProcedure [dbo].[up_InsertSparePartPOTypeTOP]    Script Date: 06/10/2018 15:41:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertSparePartPOTypeTOP]
	@ID int OUTPUT,
	@SparePartPOType varchar(5),
	@IsTOP bit,
	@TermOfPaymentIDNotTOP int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartPOTypeTOP]
VALUES
(
	@SparePartPOType,
	@IsTOP,
	@TermOfPaymentIDNotTOP,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY

/****** Object:  StoredProcedure [dbo].[up_RetrieveSparePartPOTypeTOP]    Script Date: 06/10/2018 15:46:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveSparePartPOTypeTOP]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartPOType],
	[IsTOP],
	[TermOfPaymentIDNotTOP],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartPOTypeTOP]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

/****** Object:  StoredProcedure [dbo].[up_RetrieveSparePartPOTypeTOPList]    Script Date: 06/10/2018 15:47:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveSparePartPOTypeTOPList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartPOType],
		[IsTOP],
		[TermOfPaymentIDNotTOP],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartPOTypeTOP] 

SET NOCOUNT OFF


/****** Object:  StoredProcedure [dbo].[up_UpdateSparePartPOTypeTOP]    Script Date: 06/10/2018 15:47:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateSparePartPOTypeTOP]
	@ID int OUTPUT,
	@SparePartPOType varchar(5),
	@TermOfPaymentIDNotTOP int,
	@IsTOP bit,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartPOTypeTOP]
SET
	[SparePartPOType] = @SparePartPOType,
	[IsTOP] = @IsTOP,
	[TermOfPaymentIDNotTOP] = @TermOfPaymentIDNotTOP,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

/****** Object:  StoredProcedure [dbo].[up_ValidateSparePartPOTypeTOP]    Script Date: 06/10/2018 15:48:07 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateSparePartPOTypeTOP]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartPOType varchar(5),
	@IsTOP bit,
	@TermOfPaymentIDNotTOP int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''



