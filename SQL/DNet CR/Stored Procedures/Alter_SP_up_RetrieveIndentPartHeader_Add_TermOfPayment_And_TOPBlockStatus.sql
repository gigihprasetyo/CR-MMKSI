/****** Object:  StoredProcedure [dbo].[up_RetrieveIndentPartHeader]    Script Date: 27/08/2018 16:53:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveIndentPartHeader]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[TermOfPaymentID],
	[TOPBlockStatusID],
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
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[IndentPartHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
