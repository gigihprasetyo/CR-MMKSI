USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertIndentPartDetail]    Script Date: 06/03/2018 10:27:59 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018, Mitrais Team
--				  Add column TotalForeCast 
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertIndentPartDetail]
	@ID int OUTPUT,
	@IndentPartHeaderID int,
	@SparePartMasterID int,
	@TotalForecast int,
	@Qty int,
	@Description varchar(255),
	@AllocationQty int,
	@IsCompletedAllocation tinyint,
	@Price money,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[IndentPartDetail]
VALUES
(
	@IndentPartHeaderID,
	@SparePartMasterID,
	@TotalForecast,
	@Qty,
	@Description,
	@AllocationQty,
	@IsCompletedAllocation,
	@Price,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateIndentPartDetail]    Script Date: 06/03/2018 10:28:55 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018, Mitrais Team
--				  Add column TotalForeCast 
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateIndentPartDetail]
	@ID int OUTPUT,
	@IndentPartHeaderID int,
	@SparePartMasterID int,
	@TotalForecast int,
	@Qty int,
	@Description varchar(255),
	@AllocationQty int,
	@IsCompletedAllocation tinyint,
	@Price money,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS

UPDATE	[dbo].[IndentPartDetail]
SET
	[IndentPartHeaderID] = @IndentPartHeaderID,
	[SparePartMasterID] = @SparePartMasterID,
	[TotalForecast] = @TotalForecast,
	[Qty] = @Qty,
	[Description] = @Description,
	[AllocationQty] = @AllocationQty,
	[IsCompletedAllocation] = @IsCompletedAllocation,
	[Price] = @Price,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
	
WHERE
	[ID] = @ID

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveIndentPartDetail]    Script Date: 06/03/2018 10:33:09 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018, Mitrais Team
--				  Add column TotalForeCast
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveIndentPartDetail]
	@ID int OUTPUT
	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[IndentPartHeaderID],
	[SparePartMasterID],
	[TotalForecast],
	[Qty],
	[Description],
	[AllocationQty],
	[IsCompletedAllocation],
	[Price],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
	
FROM	[dbo].[IndentPartDetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveIndentPartDetailList]    Script Date: 06/03/2018 10:33:36 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018, Mitrais Team
--				  Add column TotalForeCast
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveIndentPartDetailList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[IndentPartHeaderID],
		[SparePartMasterID],
		[TotalForecast],
		[Qty],
		[Description],
		[AllocationQty],
		[IsCompletedAllocation],
		[Price],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		
		FROM	
		[dbo].[IndentPartDetail] 

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_ValidateIndentPartDetail]    Script Date: 06/03/2018 10:34:22 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018, Mitrais Team
--				  Add column TotalForeCast
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_ValidateIndentPartDetail]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@IndentPartHeaderID int,
	@SparePartMasterID int,
	@TotalForecast int,
	@Qty int,
	@Description varchar(255),
	@AllocationQty int,
	@IsCompletedAllocation tinyint,
	@Price money,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
	
AS

SET	@Result = ''

GO

---------------------------------------------------------------------------------------------------------------
