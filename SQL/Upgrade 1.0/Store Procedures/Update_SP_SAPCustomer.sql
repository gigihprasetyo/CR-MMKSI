USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertSAPCustomer]    Script Date: 2/03/2018 10:56:16 AM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertSAPCustomer]
	@ID int OUTPUT,
	@SalesforceID varchar(50),
	@DealerID smallint,
	@SalesmanHeaderID smallint,
	@VechileTypeID smallint,
	@CustomerCode varchar(8),
	@CustomerName varchar(50),
	@CustomerType smallint,
	@CustomerAddress varchar(100),
	@Phone varchar(30),
	@Email varchar(50),
	@Sex tinyint,
	@AgeSegment tinyint,
	@CustomerPurpose smallint,
	@InformationType smallint,
	@InformationSource smallint,
	@Status tinyint,
	@Qty int,
	@ProspectDate datetime,
	@isSPK bit,
	@CurrVehicleBrand varchar(50),
	@CurrVehicleType varchar(50),
	@Note varchar(100),
	@WebID nvarchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	--@LastUpdateTime datetime,
	@BirthDate date,
	@PreferedVehicleModel varchar(100),
	@Description varchar(2000),
	@EstimatedCloseDate date,
	@OriginatingLeadId uniqueidentifier,
	@StatusCode smallint,
	@LeadStatus tinyint,
	@StateCode tinyint,
	@CampaignName varchar(50),
	@BusinessSectorDetailID int	
	
AS
INSERT	INTO	[dbo].[SAPCustomer]
VALUES
(
	@SalesforceID,
	@DealerID,
	@SalesmanHeaderID,
	@VechileTypeID,
	@CustomerCode,
	@CustomerName,
	@CustomerType,
	@CustomerAddress,
	@Phone,
	@Email,
	@Sex,
	@AgeSegment,
	@CustomerPurpose,
	@InformationType,
	@InformationSource,
	@Status,
	@Qty,
	@ProspectDate,
	@isSPK,
	@CurrVehicleBrand,
	@CurrVehicleType,
	@Note,
	@WebID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE(),	
	@BirthDate,
	@PreferedVehicleModel,
	@Description,
	@EstimatedCloseDate,
	@OriginatingLeadId,
	@StatusCode,
	@LeadStatus,
	@StateCode,
	@CampaignName,
	@BusinessSectorDetailID)

	
SET @ID = @@IDENTITY


GO




----------------------------------------------------------------------------------------------------------------------------------------
USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_UpdateSAPCustomer]    Script Date: 2/03/2018 11:36:31 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateSAPCustomer]
	@ID int OUTPUT,
	@SalesforceID varchar(50),
	@DealerID smallint,
	@SalesmanHeaderID smallint,
	@VechileTypeID smallint,
	@CustomerCode varchar(8),
	@CustomerName varchar(50),
	@CustomerType smallint,
	@CustomerAddress varchar(100),
	@Phone varchar(30),
	@Email varchar(50),
	@Sex tinyint,
	@AgeSegment tinyint,
	@CustomerPurpose smallint,
	@InformationType smallint,
	@InformationSource smallint,
	@Status tinyint,
	@Qty int,
	@ProspectDate datetime,
	@isSPK bit,
	@CurrVehicleBrand varchar(50),
	@CurrVehicleType varchar(50),
	@Note varchar(100),
	@WebID nvarchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	--@LastUpdateTime datetime,
	@BirthDate date,
	@PreferedVehicleModel varchar(100),
	@Description varchar(2000),
	@EstimatedCloseDate date,
	@OriginatingLeadId uniqueidentifier,
	@StatusCode smallint,
	@LeadStatus tinyint,
	@StateCode tinyint,
	@CampaignName varchar(50),
	@BusinessSectorDetailID int	
AS

UPDATE	[dbo].[SAPCustomer]
SET
	[SalesforceID] = @SalesforceID,
	[DealerID] = @DealerID,
	[SalesmanHeaderID] = @SalesmanHeaderID,
	[VechileTypeID] = @VechileTypeID,
	[CustomerCode] = @CustomerCode,
	[CustomerName] = @CustomerName,
	[CustomerType] = @CustomerType,
	[CustomerAddress] = @CustomerAddress,
	[Phone] = @Phone,
	[Email] = @Email,
	[Sex] = @Sex,
	[AgeSegment] = @AgeSegment,
	[CustomerPurpose] = @CustomerPurpose,
	[InformationType] = @InformationType,
	[InformationSource] = @InformationSource,
	[Status] = @Status,
	[Qty] = @Qty,
	[ProspectDate] = @ProspectDate,
	[isSPK] = @isSPK,
	[CurrVehicleBrand] = @CurrVehicleBrand,
	[CurrVehicleType] = @CurrVehicleType,
	[Note] = @Note,
	[WebID] = @WebID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE(),
	[BirthDate] = @BirthDate,
	[PreferedVehicleModel] = @PreferedVehicleModel,
	[Description] = @Description,
	[EstimatedCloseDate] = @EstimatedCloseDate,
	[OriginatingLeadId] = @OriginatingLeadId,
	[StatusCode] = @StatusCode,
	[LeadStatus] = @LeadStatus,
	[StateCode] = @StateCode,
	[CampaignName] = @CampaignName,
	[BusinessSectorDetailID] = @BusinessSectorDetailID	
WHERE
	[ID] = @ID



------------------------------------------------------------------------------------------------------------------------------------------

USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveSAPCustomer]    Script Date: 2/03/2018 11:25:54 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveSAPCustomer]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SalesforceID],
	[DealerID],
	[SalesmanHeaderID],
	[VechileTypeID],
	[CustomerCode],
	[CustomerName],
	[CustomerType],
	[CustomerAddress],
	[Phone],
	[Email],
	[Sex],
	[AgeSegment],
	[CustomerPurpose],
	[InformationType],
	[InformationSource],
	[Status],
	[Qty],
	[ProspectDate],
	[isSPK],
	[CurrVehicleBrand],
	[CurrVehicleType],
	[Note],
	[WebID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime],
	[BirthDate],
	[PreferedVehicleModel],
	[Description],
	[EstimatedCloseDate],
	[OriginatingLeadId],
	[StatusCode],
	[LeadStatus],
	[StateCode],
	[CampaignName],
	[BusinessSectorDetailID]	
FROM	[dbo].[SAPCustomer]

WHERE
	[ID] = @ID

SET NOCOUNT OFF


----------------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveSAPCustomerList]    Script Date: 2/03/2018 11:35:41 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveSAPCustomerList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SalesforceID],
		[DealerID],
		[SalesmanHeaderID],
		[VechileTypeID],
		[CustomerCode],
		[CustomerName],
		[CustomerType],
		[CustomerAddress],
		[Phone],
		[Email],
		[Sex],
		[AgeSegment],
		[CustomerPurpose],
		[InformationType],
		[InformationSource],
		[Status],
		[Qty],
		[ProspectDate],
		[isSPK],
		[CurrVehicleBrand],
		[CurrVehicleType],
		[Note],
		[WebID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime],
		[BirthDate],
		[PreferedVehicleModel],
		[Description],
		[EstimatedCloseDate],
		[OriginatingLeadId],
		[StatusCode],
		[LeadStatus],
		[StateCode],
		[CampaignName],
		[BusinessSectorDetailID]			
		FROM	
		[dbo].[SAPCustomer] 

SET NOCOUNT OFF

