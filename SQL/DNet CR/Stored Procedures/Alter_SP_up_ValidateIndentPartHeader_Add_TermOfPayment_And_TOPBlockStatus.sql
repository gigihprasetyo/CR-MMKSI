/****** Object:  StoredProcedure [dbo].[up_ValidateIndentPartHeader]    Script Date: 27/08/2018 16:55:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateIndentPartHeader]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@TermOfPaymentID int,
	@TOPBlockStatusID int,
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
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
