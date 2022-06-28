USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertIndentPartHeader]    Script Date: 06/03/2018 10:37:09 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertIndentPartHeader]
	@ID int OUTPUT,
	@DealerID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

set @DealerID = ISNULL(@DealerID,0)
if(@DealerID=0)
begin
	set @ID=-1
end 
else
begin
	Declare @IPNoGenerated  varchar(18)
	set @IPNoGenerated=dbo.ufn_CreateIndentPartNumber(@RequestDate,@DealerId)

	INSERT	INTO	[dbo].[IndentPartHeader]
	VALUES
	(
		@DealerID,
		@IPNoGenerated,
		@RequestDate,
		@MaterialType,
		@Status,
		@StatusKTB,
		@SubmitFile,
		@PaymentType,
		@Price,
		@KTBConfirmedDate,
		@DescID,
		@ChassisNumber,
		@DMSPRNo,
		@RowStatus,
		@CreatedBy,
		GETDATE(),	
		@LastUpdateBy,
		GETDATE())

		
	SET @ID = @@IDENTITY

end

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveIndentPartHeader]    Script Date: 06/03/2018 10:38:44 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveIndentPartHeader]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[RequestNo],
	[RequestDate],
	[MaterialType],
	[Status],
	[StatusKTB],
	[SubmitFile],
	[PaymentType],
	[Price],
	[KTBConfirmedDate],
	[DescID],
	[ChassisNumber],
	[DMSPRNo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[IndentPartHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveIndentPartHeaderList]    Script Date: 06/03/2018 10:39:09 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveIndentPartHeaderList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[RequestNo],
		[RequestDate],
		[MaterialType],
		[Status],
		[StatusKTB],
		[SubmitFile],
		[PaymentType],
		[Price],
		[KTBConfirmedDate],
		[DescID],
		[ChassisNumber],
		[DMSPRNo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[IndentPartHeader] 

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateIndentPartHeader]    Script Date: 06/03/2018 10:47:16 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateIndentPartHeader]
	@ID int OUTPUT,
	@DealerID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[IndentPartHeader]
SET
	[DealerID] = @DealerID,
	[RequestNo] = @RequestNo,
	[RequestDate] = @RequestDate,
	[MaterialType] = @MaterialType,
	[Status] = @Status,
	[StatusKTB] = @StatusKTB,
	[SubmitFile] = @SubmitFile,
	[PaymentType] = @PaymentType,
	[Price] = @Price,
	[KTBConfirmedDate] = @KTBConfirmedDate,
	[DescID] = @DescID,
	[ChassisNumber] = @ChassisNumber,
	[DMSPRNo] = @DMSPRNo,
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

/****** Object:  StoredProcedure [dbo].[up_ValidateIndentPartHeader]    Script Date: 06/03/2018 10:47:58 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_ValidateIndentPartHeader]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

GO


