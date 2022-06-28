set xact_abort on
go

begin transaction
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, December 22, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertInvoice
      @ID INT OUTPUT ,
      @InvoiceNumber VARCHAR(10) ,
      @InvoiceDate DATETIME ,
      @Amount MONEY ,
      @SalesOrderID INT ,
      @InvoiceKind INT ,
      @InvoiceType VARCHAR(4) ,
      @InvoiceRef VARCHAR(10) ,
      @LogisticDNID INT ,
      @RowStatus SMALLINT ,
      @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
      @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
INSERT  INTO [dbo].[Invoice]
        (
          [InvoiceNumber] ,
          [InvoiceDate] ,
          [Amount] ,
          [SalesOrderID] ,
          [InvoiceType] ,
          [InvoiceRef] ,
          [LogisticDNID] ,
          [InvoiceKind] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  (
          @InvoiceNumber ,
          @InvoiceDate ,
          @Amount ,
          @SalesOrderID ,
          @InvoiceType ,
          @InvoiceRef ,
          @LogisticDNID ,
          @InvoiceKind ,
          @RowStatus ,
          @CreatedBy ,
          GETDATE() ,
          @LastUpdateBy ,
          GETDATE()
        )

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, December 22, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveInvoice
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[InvoiceNumber],
	[InvoiceDate],
	[Amount],
	[SalesOrderID],
	[InvoiceType],
	[InvoiceRef],
	LogisticDNID,
	[InvoiceKind],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[Invoice]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, December 22, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveInvoiceList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[InvoiceNumber],
		[InvoiceDate],
		[Amount],
		[SalesOrderID],
		[InvoiceType],
		[InvoiceRef],
		LogisticDNID,
		[InvoiceKind],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[Invoice] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, December 22, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateInvoice
	@ID int OUTPUT,
	@InvoiceNumber varchar(10),
	@InvoiceDate datetime,
	@Amount money,
	@SalesOrderID int,
	@InvoiceType varchar(4),
    @InvoiceRef varchar(10),
	@LogisticDNID int,
	@InvoiceKind INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[Invoice]
SET
	[InvoiceNumber] = @InvoiceNumber,
	[InvoiceDate] = @InvoiceDate,
	[Amount] = @Amount,
	[InvoiceKind] = @InvoiceKind,
	[SalesOrderID] = @SalesOrderID,
	[InvoiceType] = @InvoiceType,
	[InvoiceRef] = @InvoiceRef,
	LogisticDNID = @LogisticDNID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

commit
go


