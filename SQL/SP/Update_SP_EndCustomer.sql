USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertEndCustomer]    Script Date: 18/09/2018 11:45:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertEndCustomer]
	@ID int OUTPUT,
	@ProjectIndicator varchar(1),
	@RefChassisNumberID int,
	@CustomerID int,
	@Name1 varchar(50),
	@FakturDate datetime,
	@OpenFakturDate datetime,
	@FakturNumber varchar(18),
	@AreaViolationFlag varchar(50),
	@AreaViolationPaymentMethodID tinyint,
	@AreaViolationyAmount money,
	@AreaViolationBankName varchar(30),
	@AreaViolationGyroNumber varchar(30),
	@PenaltyFlag varchar(50),
	@PenaltyPaymentMethodID tinyint,
	@PenaltyAmount money,
	@PenaltyBankName varchar(30),
	@PenaltyGyroNumber varchar(30),
	@ReferenceLetterFlag varchar(1),
	@ReferenceLetter varchar(40),
	@SaveBy varchar(20),
	@SaveTime datetime,
	@ValidateBy varchar(20),
	@ValidateTime datetime,
	@ConfirmBy varchar(20),
	@ConfirmTime datetime,
	@DownloadBy varchar(20),
	@DownloadTime datetime,
	@PrintedBy varchar(20),
	@PrintedTime datetime,
	@CleansingCustomerID int,
	@MCPHeaderID int, 
	@MCPStatus SMALLINT,
	@LKPPHeaderID int, 
	@LKPPStatus SMALLINT,
	@Remark1 varchar(255),
	@Remark2 varchar(255),
	@HandoverDate datetime,
	@IsTemporary smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[EndCustomer]
VALUES
(
	@ProjectIndicator,
	@RefChassisNumberID,
	@CustomerID,
	@Name1,
	@FakturDate,
	@OpenFakturDate,
	@FakturNumber,
	@AreaViolationFlag,
	@AreaViolationPaymentMethodID,
	@AreaViolationyAmount,
	@AreaViolationBankName,
	@AreaViolationGyroNumber,
	@PenaltyFlag,
	@PenaltyPaymentMethodID,
	@PenaltyAmount,
	@PenaltyBankName,
	@PenaltyGyroNumber,
	@ReferenceLetterFlag,
	@ReferenceLetter,
	@SaveBy,
	@SaveTime,
	@ValidateBy,
	@ValidateTime,
	@ConfirmBy,
	@ConfirmTime,
	@DownloadBy,
	@DownloadTime,
	@PrintedBy,
	@PrintedTime,
	@CleansingCustomerID,
	@MCPHeaderID,
	@MCPStatus,
	@LKPPHeaderID,
	@LKPPStatus,
	@Remark1,
	@Remark2,
	@HandoverDate,
	@IsTemporary,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY




---------------------------------------------------------------
USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveEndCustomer]    Script Date: 18/09/2018 11:47:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveEndCustomer]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ProjectIndicator],
	[RefChassisNumberID],
	[CustomerID],
	[Name1],
	[FakturDate],
	[OpenFakturDate],
	[FakturNumber],
	[AreaViolationFlag],
	[AreaViolationPaymentMethodID],
	[AreaViolationyAmount],
	[AreaViolationBankName],
	[AreaViolationGyroNumber],
	[PenaltyFlag],
	[PenaltyPaymentMethodID],
	[PenaltyAmount],
	[PenaltyBankName],
	[PenaltyGyroNumber],
	[ReferenceLetterFlag],
	[ReferenceLetter],
	[SaveBy],
	[SaveTime],
	[ValidateBy],
	[ValidateTime],
	[ConfirmBy],
	[ConfirmTime],
	[DownloadBy],
	[DownloadTime],
	[PrintedBy],
	[PrintedTime],
	[CleansingCustomerID],
	[MCPHeaderID],
	[MCPStatus],
	[LKPPHeaderID],
	[LKPPStatus],
	[Remark1],
	[Remark2],
	[HandoverDate],
	[IsTemporary],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[EndCustomer]

WHERE
	[ID] = @ID

SET NOCOUNT OFF



---------------------------------------------------------------
USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveEndCustomerList]    Script Date: 18/09/2018 11:47:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveEndCustomerList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ProjectIndicator],
		[RefChassisNumberID],
		[CustomerID],
		[Name1],
		[FakturDate],
		[OpenFakturDate],
		[FakturNumber],
		[AreaViolationFlag],
		[AreaViolationPaymentMethodID],
		[AreaViolationyAmount],
		[AreaViolationBankName],
		[AreaViolationGyroNumber],
		[PenaltyFlag],
		[PenaltyPaymentMethodID],
		[PenaltyAmount],
		[PenaltyBankName],
		[PenaltyGyroNumber],
		[ReferenceLetterFlag],
		[ReferenceLetter],
		[SaveBy],
		[SaveTime],
		[ValidateBy],
		[ValidateTime],
		[ConfirmBy],
		[ConfirmTime],
		[DownloadBy],
		[DownloadTime],
		[PrintedBy],
		[PrintedTime],
		[CleansingCustomerID],
		[MCPHeaderID],
		[MCPStatus],
		[LKPPHeaderID],
		[LKPPStatus],
		[Remark1],
		[Remark2],
		[HandoverDate],
		[IsTemporary],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[EndCustomer] 

SET NOCOUNT OFF



---------------------------------------------------------------
USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_UpdateEndCustomer]    Script Date: 18/09/2018 11:48:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
/*
2015/11/25 - add recalculation MCPDetail.UnitRemain
*/
---------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[up_UpdateEndCustomer]
    @ID INT OUTPUT ,
    @ProjectIndicator VARCHAR(1) ,
    @RefChassisNumberID INT ,
    @CustomerID INT ,
    @Name1 VARCHAR(50) ,
    @FakturDate DATETIME ,
    @OpenFakturDate DATETIME ,
    @FakturNumber VARCHAR(18) ,
    @AreaViolationFlag VARCHAR(50) ,
    @AreaViolationPaymentMethodID TINYINT ,
    @AreaViolationyAmount MONEY ,
    @AreaViolationBankName VARCHAR(30) ,
    @AreaViolationGyroNumber VARCHAR(30) ,
    @PenaltyFlag VARCHAR(50) ,
    @PenaltyPaymentMethodID TINYINT ,
    @PenaltyAmount MONEY ,
    @PenaltyBankName VARCHAR(30) ,
    @PenaltyGyroNumber VARCHAR(30) ,
    @ReferenceLetterFlag VARCHAR(1) ,
    @ReferenceLetter VARCHAR(40) ,
    @SaveBy VARCHAR(20) ,
    @SaveTime DATETIME ,
    @ValidateBy VARCHAR(20) ,
    @ValidateTime DATETIME ,
    @ConfirmBy VARCHAR(20) ,
    @ConfirmTime DATETIME ,
    @DownloadBy VARCHAR(20) ,
    @DownloadTime DATETIME ,
    @PrintedBy VARCHAR(20) ,
    @PrintedTime DATETIME ,
    @CleansingCustomerID INT ,
    @MCPHeaderID INT ,
    @MCPStatus SMALLINT ,
	@LKPPHeaderID int, 
	@LKPPStatus SMALLINT,
    @Remark1 VARCHAR(255) ,
    @Remark2 VARCHAR(255) ,
	@HandoverDate DATETIME,
	@IsTemporary SMALLINT,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    BEGIN

	--Temp MCP before changed
       
        DECLARE @Temp_Table AS TABLE
            (
              MCPHeaderIDBefore INT,
		 	  LKPPHeaderIDBefore INT
            )
            
		 
        UPDATE  [dbo].[EndCustomer]
        SET     [ProjectIndicator] = @ProjectIndicator ,
                [RefChassisNumberID] = @RefChassisNumberID ,
                [CustomerID] = @CustomerID ,
                [Name1] = @Name1 ,
                [FakturDate] = @FakturDate ,
                [OpenFakturDate] = @OpenFakturDate ,
                [FakturNumber] = @FakturNumber ,
                [AreaViolationFlag] = @AreaViolationFlag ,
                [AreaViolationPaymentMethodID] = @AreaViolationPaymentMethodID ,
                [AreaViolationyAmount] = @AreaViolationyAmount ,
                [AreaViolationBankName] = @AreaViolationBankName ,
                [AreaViolationGyroNumber] = @AreaViolationGyroNumber ,
                [PenaltyFlag] = @PenaltyFlag ,
                [PenaltyPaymentMethodID] = @PenaltyPaymentMethodID ,
                [PenaltyAmount] = @PenaltyAmount ,
                [PenaltyBankName] = @PenaltyBankName ,
                [PenaltyGyroNumber] = @PenaltyGyroNumber ,
                [ReferenceLetterFlag] = @ReferenceLetterFlag ,
                [ReferenceLetter] = @ReferenceLetter ,
                [SaveBy] = @SaveBy ,
                [SaveTime] = @SaveTime ,
                [ValidateBy] = @ValidateBy ,
                [ValidateTime] = @ValidateTime ,
                [ConfirmBy] = @ConfirmBy ,
                [ConfirmTime] = @ConfirmTime ,
                [DownloadBy] = @DownloadBy ,
                [DownloadTime] = @DownloadTime ,
                [PrintedBy] = @PrintedBy ,
                [PrintedTime] = @PrintedTime ,
                [CleansingCustomerID] = @CleansingCustomerID ,
                [MCPHeaderID] = @MCPHeaderID ,
                [MCPStatus] = @MCPStatus ,
				[LKPPHeaderID] = @LKPPHeaderID ,
                [LKPPStatus] = @LKPPStatus ,
				[Remark1] = @Remark1 ,
                [Remark2] = @Remark2 ,
				[HandoverDate]=@HandoverDate,
				[IsTemporary] = @IsTemporary,
                [RowStatus] = @RowStatus ,
                [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
                [LastUpdateBy] = @LastUpdateBy ,
                [LastUpdateTime] = GETDATE()
        OUTPUT  Deleted.MCPHeaderID, deleted.LKPPHeaderID
                INTO @Temp_Table
        WHERE   [ID] = @ID


/*Validasi update rows*/
     
        DECLARE @MCPHeaderIDBefore INT = NULL
		DECLARE @LKPPHeaderIDBefore INT = NULL
	
        SELECT  @MCPHeaderIDBefore = MCPHeaderIDBefore,
				@LKPPHeaderIDBefore = LKPPHeaderIDBefore
        FROM    @Temp_Table
      
        BEGIN
            SET NOCOUNT ON
		  
			 -- Update Previous MCP
            IF @MCPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateMCP @MCPHeaderIDBefore

                END
           
				--update current MCP
            IF @MCPHeaderID IS NOT NULL AND @MCPHeaderIDBefore<> @MCPHeaderID
                BEGIN 
                    EXEC up_RecalculateMCP @MCPHeaderID
                END
		 
		     -- Update Previous LKPP
            IF @LKPPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateLKPP @LKPPHeaderIDBefore
                END

					--update current LKPP
            IF @LKPPHeaderID IS NOT NULL AND @LKPPHeaderIDBefore<> @LKPPHeaderID
                BEGIN 
                    EXEC up_RecalculateLKPP @LKPPHeaderID
                END


            SET NOCOUNT OFF 
        END


        
    END





-----------------------------------------------------------------
USE [BSIDNET_MMKSI_CR_IR]
GO
/****** Object:  StoredProcedure [dbo].[up_ValidateEndCustomer]    Script Date: 18/09/2018 11:48:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateEndCustomer]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ProjectIndicator varchar(1),
	@RefChassisNumberID int,
	@CustomerID int,
	@Name1 varchar(50),
	@FakturDate datetime,
	@OpenFakturDate datetime,
	@FakturNumber varchar(18),
	@AreaViolationFlag varchar(50),
	@AreaViolationPaymentMethodID tinyint,
	@AreaViolationyAmount money,
	@AreaViolationBankName varchar(30),
	@AreaViolationGyroNumber varchar(30),
	@PenaltyFlag varchar(50),
	@PenaltyPaymentMethodID tinyint,
	@PenaltyAmount money,
	@PenaltyBankName varchar(30),
	@PenaltyGyroNumber varchar(30),
	@ReferenceLetterFlag varchar(1),
	@ReferenceLetter varchar(40),
	@SaveBy varchar(20),
	@SaveTime datetime,
	@ValidateBy varchar(20),
	@ValidateTime datetime,
	@ConfirmBy varchar(20),
	@ConfirmTime datetime,
	@DownloadBy varchar(20),
	@DownloadTime datetime,
	@PrintedBy varchar(20),
	@PrintedTime datetime,
	@CleansingCustomerID int,
	@MCPHeaderID int,
	@MCPStatus SMALLINT,
	@LKPPHeaderID int,
	@LKPPStatus SMALLINT,
	@Remark1 varchar(255),
	@Remark2 varchar(255),
	@HandoverDate datetime,
	@IsTemporary smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

