USE [BSIDNET_KTB_TOP]
GO
/****** Object:  StoredProcedure [dbo].[up_UpdateIndentPartHeader]    Script Date: 07/09/2018 15:39:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateIndentPartHeader]
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[IndentPartHeader]
SET
	[DealerID] = @DealerID,
	[TermOfPaymentID] = @TermOfPaymentID,
	[TOPBlockStatusID] = @TOPBlockStatusID,
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
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
