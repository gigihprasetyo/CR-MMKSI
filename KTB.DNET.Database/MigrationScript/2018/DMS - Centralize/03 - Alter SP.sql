set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, Oktober 23, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE SP_InsertPrice
	   @ID INT OUTPUT,
	   @VechileColorID SMALLINT ,
	   @DealerCode VARCHAR(10) ,
	   @ValidFrom DATETIME ,
	   @BasePrice MONEY ,
	   @OptionPrice MONEY ,
	   @PPN_BM MONEY ,
	   @PPN MONEY ,
	   @PPh22 MONEY ,
	   @Interest MONEY ,
	   @FactoringInt MONEY ,
	   @PPh23 MONEY ,
	   @Status VARCHAR(1) ,
	   @DiscountReward MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   BEGIN

	;
			 WITH	CTE_Dealer
					  AS ( SELECT	d.ID
						   FROM		dbo.Dealer d
						   WHERE	( d.[ID] NOT IN ( 425, 1, 2 ) )
									AND ( d.RowStatus = 0 )
									AND ( d.[Status] = 1 )
									AND ( d.DealerCode = @DealerCode
										  OR @DealerCode = ''
										)
						 ),
					CTE_PRICE
					  AS ( SELECT	D.ID AS DealerID ,
									v.ID AS VechileColorID ,
									@ValidFrom ValidFrom ,
									@BasePrice BasePrice ,
									@OptionPrice OptionPrice ,
									@PPN_BM PPN_BM ,
									@PPN PPN ,
									@PPh22 PPh22 ,
									@Interest Interest ,
									@FactoringInt FactoringInt ,
									@PPh23 PPh23 ,
									@Status [Status] ,
									@DiscountReward DiscountReward ,
									@RowStatus RowStatus ,
									@CreatedBy CreatedBy
						   FROM		CTE_Dealer D
						   INNER JOIN dbo.VechileColor v ON 1 = 1
															AND v.ID = @VechileColorID
															AND v.RowStatus = 0
						 )
				  MERGE dbo.Price AS T
				  USING CTE_PRICE AS S
				  ON T.DealerID = S.DealerID
					AND T.VechileColorID = S.VechileColorID
					AND S.ValidFrom = T.ValidFrom
					AND T.RowStatus = 0
				  WHEN MATCHED THEN
					UPDATE SET [BasePrice] = S.BasePrice ,
							  [OptionPrice] = @OptionPrice ,
							  [PPN_BM] = @PPN_BM ,
							  [PPN] = @PPN ,
							  [PPh22] = @PPh22 ,
							  [Interest] = @Interest ,
							  [FactoringInt] = @FactoringInt ,
							  [PPh23] = @PPh23 ,
							  [Status] = @Status ,
							  [DiscountReward] = @DiscountReward ,
							  [RowStatus] = @RowStatus ,
							  [LastUpdateBy] = @CreatedBy ,
							  [LastUpdateTime] = GETDATE()
				  WHEN NOT MATCHED THEN
					INSERT ( VechileColorID ,
							 DealerID ,
							 ValidFrom ,
							 BasePrice ,
							 OptionPrice ,
							 PPN_BM ,
							 PPN ,
							 PPh22 ,
							 Interest ,
							 FactoringInt ,
							 PPh23 ,
							 [Status] ,
							 DiscountReward ,
							 RowStatus ,
							 CreatedBy ,
							 CreatedTime ,
							 LastUpdateBy ,
							 LastUpdateTime
						   )
					VALUES ( S.VechileColorID ,
							 S.DealerID ,
							 @ValidFrom ,
							 @BasePrice ,
							 @OptionPrice ,
							 @PPN_BM ,
							 @PPN ,
							 @PPh22 ,
							 @Interest ,
							 @FactoringInt ,
							 @PPh23 ,
							 @Status ,
							 @DiscountReward ,
							 @RowStatus ,
							 @CreatedBy ,
							 GETDATE() ,
							 @CreatedBy ,
							 GETDATE()
						   ) ;
						   
				  SET @ID = @@IDENTITY 

	   END
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_InsertArea1    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertArea1
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@PICSales VARCHAR(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@MainAreaID INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS


INSERT	INTO	[dbo].[Area1]
VALUES
(
	@AreaCode,
	@Description,
	@PICSales,
	@PICServices,
	@PICSpareparts,
	@MainAreaID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

--/****** Object:  Stored Procedure dbo.up_InsertArea2    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertArea2
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@ACFinishUnit varchar(50),
	@ACSparePart varchar(50),
	@ACService varchar(50),
	@Area1ID Int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS


INSERT	INTO	[dbo].[Area2]
VALUES
(
	@AreaCode,
	@Description,
	@ACFinishUnit,
	@ACSparePart,
	@ACService,
	@Area1ID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertMainArea
	@ID int OUTPUT,
	@AreaCode varchar(20),
	@Description varchar(50),
	@PICSales varchar(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[MainArea]
VALUES
(
	@AreaCode,
	@Description,
	@PICSales,
	@PICServices,
	@PICSpareparts,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSparePartMaster
	@ID int OUTPUT,
	@ProductCategoryID smallint,
	@PartNumber varchar(18),
	@PartName varchar(30),
	@PartNumberReff varchar(18),
	@UoM varchar(18),
	@AltPartNumber varchar(18),
	@AltPartName varchar(30),
	@PartCode varchar(1),
	@ModelCode varchar(9),
	@SupplierCode varchar(10),
	@TypeCode varchar(5),
	@Stock int,
	@RetalPrice money,
	@PartStatus varchar(1),
	@ActiveStatus smallint,
	@ProductType varchar(18),
	@AccessoriesType smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartMaster]
VALUES
(
	@ProductCategoryID,
	@PartNumber,
	@PartName,
	@PartNumberReff,
	@UoM,
	@AltPartNumber,
	@AltPartName,
	@PartCode,
	@ModelCode,
	@SupplierCode,
	@TypeCode,
	@Stock,
	@RetalPrice,
	@PartStatus,
	@ActiveStatus,
	@ProductType,
	@AccessoriesType,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertVechileModel
	@ID int OUTPUT,
	@SAPCode varchar(30),
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileIndModel varchar(30),
	@IndDescription varchar(40),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS


INSERT	INTO	[dbo].[VechileModel]
VALUES
(
	@SAPCode,
	@VechileModelCode,
	@CategoryID,
	@Description,
	@VechileIndModel,
	@IndDescription,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertVechileType
	@ID smallint OUTPUT,
	@VechileTypeCode varchar(4),
	@ModelID smallint,
	@CategoryID tinyint,
	@ProductCategoryID smallint,
	@Description varchar(40),
	@Status varchar(1),
	@VehicleClassID int,
	@IsVehicleKind1 tinyint,
	@IsVehicleKind2 tinyint,
	@IsVehicleKind3 tinyint,
	@IsVehicleKind4 tinyint,
	@SegmentType varchar(40),
	@VariantType varchar(30),
	@TransmitType varchar(5),
	@DriveSystemType varchar(5),
	@SpeedType varchar(1),
	@FuelType varchar(10),
	@SAPModel nvarchar(20) = NULL,
	@MaxTOPDays int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

INSERT	INTO	[dbo].[VechileType]
VALUES
(
	@VechileTypeCode,
	@ModelID,
	@CategoryID,
	/*set productcategoryid by donas on 20141001153*/
	(select c.ProductCategoryID from Category c where c.ID=@CategoryID),--@ProductCategoryID,
	@Description,
	@Status,
	@VehicleClassID,
	@IsVehicleKind1,
	@IsVehicleKind2,
	@IsVehicleKind3,
	@IsVehicleKind4,
	@SegmentType,
	@VariantType,
	@TransmitType,
	@DriveSystemType,
	@SpeedType,
	@FuelType,
	@MaxTOPDays,
	@SAPModel,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_RetrieveArea1    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea1
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AreaCode],
	[Description],
	[PICSales],
	[PICServices],
	[PICSpareparts],
	[MainAreaID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[Area1]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

--/****** Object:  Stored Procedure dbo.up_RetrieveArea1List    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea1List

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[AreaCode],
		[Description],
		[PICSales],
		[PICServices],
		[PICSpareparts],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[Area1] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMainArea
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AreaCode],
	[Description],
	[PICSales],
	[PICServices],
	[PICSpareparts],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[MainArea]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMainAreaList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[AreaCode],
		[Description],
		[PICSales],
		[PICServices],
		[PICSpareparts],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[MainArea] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartMaster
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ProductCategoryID],
	[PartNumber],
	[PartName],
	[PartNumberReff],
	[UoM],
	[AltPartNumber],
	[AltPartName],
	[PartCode],
	[ModelCode],
	[SupplierCode],
	[TypeCode],
	[Stock],
	[RetalPrice],
	[PartStatus],
	[ActiveStatus],
	[ProductType],
	[AccessoriesType],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartMaster]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileModel
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SAPCode],
	[VechileModelCode],
	[CategoryID],
	[Description],
	[VechileIndModel],
	[IndDescription],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[VechileModel]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileModelList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[SAPCode],
		[VechileModelCode],
		[CategoryID],
		[Description],
		[VechileIndModel],
		[IndDescription],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[VechileModel] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileType
	@ID smallint OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[VechileTypeCode],
	[ModelID],
	[CategoryID],
	[ProductCategoryID],
	[Description],
	[Status],
	[VehicleClassID],
	[IsVehicleKind1],
	[IsVehicleKind2],
	[IsVehicleKind3],
	[IsVehicleKind4],
	[SegmentType],
	[VariantType],
	[TransmitType],
	[DriveSystemType],
	[SpeedType],
	[FuelType],
	[MaxTOPDays],
	[SAPModel],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[VechileType]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileTypeList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[VechileTypeCode],
		[ModelID],
		[CategoryID],
		[ProductCategoryID],
		[Description],
		[Status],
		[VehicleClassID],
		[IsVehicleKind1],
		[IsVehicleKind2],
		[IsVehicleKind3],
		[IsVehicleKind4],
		[SegmentType],
		[VariantType],
		[TransmitType],
		[DriveSystemType],
		[SpeedType],
		[FuelType],
		[MaxTOPDays],
		[SAPModel],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[VechileType] 

SET NOCOUNT OFF
go

--/****** Object:  Stored Procedure dbo.up_UpdateArea1    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateArea1
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@PICSales VARCHAR(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@MainAreaID INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[Area1]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[PICSales] = @PICSales,
	[PICServices] = @PICServices,
	[PICSpareparts] = @PICSpareparts,
	[MainAreaID] = @MainAreaID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

--/****** Object:  Stored Procedure dbo.up_UpdateArea2    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateArea2
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@ACFinishUnit varchar(50),
	@ACSparePart varchar(50),
	@ACService varchar(50),
	@Area1ID Int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[Area2]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[ACFinishUnit] = @ACFinishUnit,
	[ACSparePart] = @ACSparePart,
	[ACService] = @ACService,
	[Area1ID] = @Area1ID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateMainArea
	@ID int OUTPUT,
	@AreaCode varchar(20),
	@Description varchar(50),
	@PICSales varchar(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[MainArea]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[PICSales] = @PICSales,
	[PICServices] = @PICServices,
	[PICSpareparts] = @PICSpareparts,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateSparePartMaster
	@ID int OUTPUT,
	@ProductCategoryID smallint,
	@PartNumber varchar(18),
	@PartName varchar(30),
	@PartNumberReff varchar(18),
	@UoM varchar(18),
	@AltPartNumber varchar(18),
	@AltPartName varchar(30),
	@PartCode varchar(1),
	@ModelCode varchar(9),
	@SupplierCode varchar(10),
	@TypeCode varchar(5),
	@Stock int,
	@RetalPrice money,
	@PartStatus varchar(1),
	@ActiveStatus smallint,
	@ProductType varchar(18),
	@AccessoriesType smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartMaster]
SET
	[ProductCategoryID] = @ProductCategoryID,
	[PartNumber] = @PartNumber,
	[PartName] = @PartName,
	[PartNumberReff] = @PartNumberReff,
	[UoM] = @UoM,
	[AltPartNumber] = @AltPartNumber,
	[AltPartName] = @AltPartName,
	[PartCode] = @PartCode,
	[ModelCode] = @ModelCode,
	[SupplierCode] = @SupplierCode,
	[TypeCode] = @TypeCode,
	[Stock] = @Stock,
	[RetalPrice] = @RetalPrice,
	[PartStatus] = @PartStatus,
	[ActiveStatus] = @ActiveStatus,
	[ProductType] = @ProductType,
	[AccessoriesType] = @AccessoriesType,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateVechileModel
	@ID int OUTPUT,
	@SAPCode varchar(30),
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileIndModel varchar(30),
	@IndDescription varchar(40),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[VechileModel]
SET
	[SAPCode] = @SAPCode,
	[VechileModelCode] = @VechileModelCode,
	[CategoryID] = @CategoryID,
	[Description] = @Description,
	[VechileIndModel] = @VechileIndModel,
	[IndDescription] = @IndDescription,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateVechileType
	@ID smallint OUTPUT,
	@VechileTypeCode varchar(4),
	@ModelID smallint,
	@CategoryID tinyint,
	@ProductCategoryID smallint,
	@Description varchar(40),
	@Status varchar(1),
	@VehicleClassID int,
	@IsVehicleKind1 tinyint,
	@IsVehicleKind2 tinyint,
	@IsVehicleKind3 tinyint,
	@IsVehicleKind4 tinyint,
	@SegmentType varchar(40),
	@VariantType varchar(30),
	@TransmitType varchar(5),
	@DriveSystemType varchar(5),
	@SpeedType varchar(1),
	@FuelType varchar(10),
	@MaxTOPDays int,
	@SAPModel nvarchar(20) = NULL,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[VechileType]
SET
	[VechileTypeCode] = @VechileTypeCode,
	[ModelID] = @ModelID,
	[CategoryID] = @CategoryID,
	/*set productcategoryid by donas on 20141001153*/
	[ProductCategoryID] = (select c.ProductCategoryID from Category c where c.ID=@CategoryID),--@ProductCategoryID,
	[Description] = @Description,
	[Status] = @Status,
	[VehicleClassID] = @VehicleClassID,
	[IsVehicleKind1] = @IsVehicleKind1,
	[IsVehicleKind2] = @IsVehicleKind2,
	[IsVehicleKind3] = @IsVehicleKind3,
	[IsVehicleKind4] = @IsVehicleKind4,
	[SegmentType] = @SegmentType,
	[VariantType] = @VariantType,
	[TransmitType] = @TransmitType,
	[DriveSystemType] = @DriveSystemType,
	[SpeedType] = @SpeedType,
	[FuelType] = @FuelType,
	[SAPModel] = ISNULL(@SAPModel, SAPModel),
	[MaxTOPDays] = @MaxTOPDays,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

--/****** Object:  Stored Procedure dbo.up_ValidateArea1    Script Date: 14/10/2005 11:06:14 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateArea1
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@PICSales VARCHAR(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@MainAreaID INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateMainArea
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AreaCode varchar(20),
	@Description varchar(50),
	@PICSales varchar(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateVechileModel
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SAPCode varchar(10),
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileIndModel varchar(4),
	@IndDescription varchar(40),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateVechileType
	@Result	varchar(1000),
	@ID smallint OUTPUT,
	@VechileTypeCode varchar(4),
	@ModelID smallint,
	@CategoryID tinyint,
	@ProductCategoryID smallint,
	@Description varchar(40),
	@Status varchar(1),
	@VehicleClassID int,
	@IsVehicleKind1 tinyint,
	@IsVehicleKind2 tinyint,
	@IsVehicleKind3 tinyint,
	@IsVehicleKind4 tinyint,
	@SegmentType varchar(40),
	@VariantType varchar(30),
	@TransmitType varchar(5),
	@DriveSystemType varchar(5),
	@SpeedType varchar(1),
	@FuelType varchar(10),
	@MaxTOPDays int,
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


