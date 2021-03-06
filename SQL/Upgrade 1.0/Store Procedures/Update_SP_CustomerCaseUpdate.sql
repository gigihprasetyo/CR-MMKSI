USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_UpdateCustomerCase]    Script Date: 20/03/2018 12:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateCustomerCase]
	@ID int OUTPUT,
	@DealerID smallint,
	@SalesforceID nvarchar(255),
	@CaseNumber nvarchar(255),
	@CustomerName nvarchar(50),
	@Phone nvarchar(50),
	@Email nvarchar(50),
	@Category nvarchar(50),
	@SubCategory1 nvarchar(50),
	@SubCategory2 nvarchar(50),
	@SubCategory3 nvarchar(50),
	@SubCategory4 nvarchar(50),
	@CallerType nchar(10),
	@CarType nvarchar(50),
	@Variant nvarchar(50),
	@EngineNumber nvarchar(50),
	@ChassisNumber nvarchar(50),
	@Odometer int,
	@PlateNumber nvarchar(20),
	@Priority smallint,
	@CaseNumberReff nvarchar(255),
	@CaseDate datetime,
	@Subject varchar(255),
	@Description nvarchar(max),
	@ReservationNumber nvarchar(50),
	@Status smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTIme datetime
	
AS

IF @Status IN ('0', '1')
BEGIN
	SET @CreatedBy = 'Salesforce'
END 

UPDATE	[dbo].[CustomerCase]
SET
	[DealerID] = @DealerID,
	[SalesforceID] = @SalesforceID,
	[CaseNumber] = @CaseNumber,
	[CustomerName] = @CustomerName,
	[Phone] = @Phone,
	[Email] = @Email,
	[Category] = @Category,
	[SubCategory1] = @SubCategory1,
	[SubCategory2] = @SubCategory2,
	[SubCategory3] = @SubCategory3,
	[SubCategory4] = @SubCategory4,
	[CallerType] = @CallerType,
	[CarType] = @CarType,
	[Variant] = @Variant,
	[EngineNumber] = @EngineNumber,
	[ChassisNumber] = @ChassisNumber,
	[Odometer] = @Odometer,
	[PlateNumber] = @PlateNumber,
	[Priority] = @Priority,
	[CaseNumberReff] = @CaseNumberReff,
	[CaseDate] = @CaseDate,
	[Subject] = @Subject,
	[Description] = @Description,
	[ReservationNumber] = @ReservationNumber,
	[Status] = @Status,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTIme] = getdate()	
WHERE
	[ID] = @ID


-- Jika status case re-open
IF @Status IN ('1')
BEGIN
	INSERT	INTO	[dbo].[CustomerCaseResponse]
	VALUES
	(
		@ID,
		@Subject,
		Null,
		@Status,
		1,
		@RowStatus,
		@CreatedBy,
		GETDATE(),	
		@LastUpdateBy,
		@LastUpdateTIme)
END

