USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertRevisionSAPDoc]    Script Date: 19/09/2018 16:53:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertRevisionSAPDoc]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionSAPDoc]
VALUES
(
	@RevisionFakturID,
	@DebitChargeNo,
	@DCAmount,
	@DebitMemoNo,
	@DMAmount,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY



-----------------------------------------

/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionSAPDoc]    Script Date: 19/09/2018 16:56:43 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveRevisionSAPDoc]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[RevisionFakturID],
	[DebitChargeNo],
	[DCAmount],
	[DMAmount],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RevisionSAPDoc]

WHERE
	[ID] = @ID

SET NOCOUNT OFF



-----------------------------------------

/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionSAPDocList]    Script Date: 19/09/2018 16:54:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveRevisionSAPDocList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[RevisionFakturID],
		[DebitChargeNo],
		[DCAmount],
		[DebitMemoNo],
		[DMAmount],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RevisionSAPDoc] 

SET NOCOUNT OFF




-------------------------------------------------

/****** Object:  StoredProcedure [dbo].[up_UpdateRevisionSAPDoc]    Script Date: 19/09/2018 16:54:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateRevisionSAPDoc]
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[RevisionSAPDoc]
SET
	[RevisionFakturID] = @RevisionFakturID,
	[DebitChargeNo] = @DebitChargeNo,
	[DCAmount] = @DCAmount,
	[DebitMemoNo] = @DebitMemoNo,
	[DMAmount] = @DMAmount,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID



------------------------------------------------

/****** Object:  StoredProcedure [dbo].[up_ValidateRevisionSAPDoc]    Script Date: 19/09/2018 16:55:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateRevisionSAPDoc]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@RevisionFakturID int,
	@DebitChargeNo varchar(10),
	@DCAmount money,
	@DebitMemoNo varchar(15),
	@DMAmount money,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	@CreatedTime datetime,
	@LastUpdateBy varchar(100),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

