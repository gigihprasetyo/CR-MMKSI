set xact_abort on
go

begin transaction
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 06, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertFSCampaign
    @ID INT OUTPUT ,
    @Description NVARCHAR(100) ,
    @ErrMessage NVARCHAR(100) ,
    @DateFrom DATETIME ,
    @DateTo DATETIME ,
    @DealerChecked BIT ,
    @FSTypeChecked BIT ,
    @VehicleTypeChecked BIT ,
    @FakturDateChecked BIT ,
	@ExtendedFleetChecked BIT,
	@RetailChecked BIT,
	@OpenFakturDateFrom DATETIME,
	@OpenFakturDateTo DATETIME,
    @Status SMALLINT ,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	--@LastUpdateTime datetime,
    @LastUpdateBy VARCHAR(20)
AS
BEGIN

INSERT INTO dbo.FSCampaign
        ( Description ,
          ErrMessage ,
          DateFrom ,
          DateTo ,
          DealerChecked ,
          FSTypeChecked ,
          VehicleTypeChecked ,
          FakturDateChecked ,
          Status ,
          ExtendedFleetChecked ,
          RetailChecked ,
          OpenFakturDateFrom ,
          OpenFakturDateTo ,
          RowStatus ,
          CreatedBy ,
          CreatedTime ,
          LastUpdateTime ,
          LastUpdateBy
        )
 
    VALUES  ( @Description, @ErrMessage, @DateFrom, @DateTo, @DealerChecked,
              @FSTypeChecked, @VehicleTypeChecked, @FakturDateChecked, @Status,
			  @ExtendedFleetChecked,  @RetailChecked, @OpenFakturDateFrom, @OpenFakturDateTo,
              @RowStatus, @CreatedBy, GETDATE(), GETDATE(), @LastUpdateBy )

	
    SET @ID = @@IDENTITY



END
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 06, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveFSCampaign
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	 [a].[ID] ,
	 [a].[Description] ,
	 [a].[ErrMessage] ,
	 [a].[DateFrom] ,
	 [a].[DateTo] ,
	 [a].[DealerChecked] ,
	 [a].[FSTypeChecked] ,
	 [a].[VehicleTypeChecked] ,
	 [a].[FakturDateChecked] ,
	 [a].[Status] ,
	 [a].[ExtendedFleetChecked] ,
	 [a].[RetailChecked] ,
	 [a].[OpenFakturDateFrom] ,
	 [a].[OpenFakturDateTo] ,
	 [a].[RowStatus] ,
	 [a].[CreatedBy] ,
	 [a].[CreatedTime] ,
	 [a].[LastUpdateTime] ,
	 [a].[LastUpdateBy]
FROM	[dbo].[FSCampaign] a

WHERE
	a.[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 06, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveFSCampaignList
AS
	  SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	  SET NOCOUNT ON


	  SELECT	[a].[ID] ,
				[a].[Description] ,
				[a].[ErrMessage] ,
				[a].[DateFrom] ,
				[a].[DateTo] ,
				[a].[DealerChecked] ,
				[a].[FSTypeChecked] ,
				[a].[VehicleTypeChecked] ,
				[a].[FakturDateChecked] ,
				[a].[Status] ,
				[a].[ExtendedFleetChecked] ,
				[a].[RetailChecked] ,
				[a].[OpenFakturDateFrom] ,
				[a].[OpenFakturDateTo] ,
				[a].[RowStatus] ,
				[a].[CreatedBy] ,
				[a].[CreatedTime] ,
				[a].[LastUpdateTime] ,
				[a].[LastUpdateBy]
	  FROM		[dbo].[FSCampaign] a

	  SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 06, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	: [up_RetrieveFSCampaign_Active] 4
---------------------------------------------------------------------------------------------------------------

create PROCEDURE up_RetrieveFSCampaign_Active @FSKindID INT
AS
	  SET NOCOUNT ON

	  SELECT   [a].[ID] ,
			   [a].[Description] ,
			   [a].[ErrMessage] ,
			   [a].[DateFrom] ,
			   [a].[DateTo] ,
			   [a].[DealerChecked] ,
			   [a].[FSTypeChecked] ,
			   [a].[VehicleTypeChecked] ,
			   [a].[FakturDateChecked] ,
			   [a].[Status] ,
			   [a].[ExtendedFleetChecked] ,
			   [a].[RetailChecked] ,
			   [a].[OpenFakturDateFrom] ,
			   [a].[OpenFakturDateTo] ,
			   [a].[RowStatus] ,
			   [a].[CreatedBy] ,
			   [a].[CreatedTime] ,
			   [a].[LastUpdateTime] ,
			   [a].[LastUpdateBy]
	  FROM	   [dbo].[FSCampaign] a ( NOLOCK )
	  INNER JOIN dbo.[FSCampaignKind] b ( NOLOCK ) ON [b].[CampaignID] = [a].[ID]
	  WHERE	   1 = 1
			   AND a.[RowStatus] = 0
			   AND a.[Status] = 0
			   AND b.[RowStatus] = 0
			   AND b.[FSKindID] = @FSKindID

	  SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 06, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateFSCampaign
	  @ID INT OUTPUT ,
	  @Description NVARCHAR(100) ,
	  @ErrMessage NVARCHAR(100) ,
	  @DateFrom DATETIME ,
	  @DateTo DATETIME ,
	  @DealerChecked BIT ,
	  @FSTypeChecked BIT ,
	  @VehicleTypeChecked BIT ,
	  @FakturDateChecked BIT ,
	  @Status SMALLINT ,
	  @ExtendedFleetChecked BIT ,
	  @RetailChecked BIT ,
	@OpenFakturDateFrom DATETIME,
	@OpenFakturDateTo DATETIME,
	  @RowStatus SMALLINT ,
	  @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	--@LastUpdateTime datetime,
	  @LastUpdateBy VARCHAR(20)
AS
	  UPDATE	[dbo].[FSCampaign]
	  SET		[Description] = @Description ,
				[ErrMessage] = @ErrMessage ,
				[DateFrom] = @DateFrom ,
				[DateTo] = @DateTo ,
				[DealerChecked] = @DealerChecked ,
				[FSTypeChecked] = @FSTypeChecked ,
				[VehicleTypeChecked] = @VehicleTypeChecked ,
				[FakturDateChecked] = @FakturDateChecked ,
				[ExtendedFleetChecked] = @ExtendedFleetChecked ,
				[RetailChecked] = @RetailChecked ,
				OpenFakturDateFrom = @OpenFakturDateFrom ,
				OpenFakturDateTo = @OpenFakturDateTo ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateTime] = GETDATE() ,
				[LastUpdateBy] = @LastUpdateBy
	  WHERE		[id] = @ID
go

commit
go


