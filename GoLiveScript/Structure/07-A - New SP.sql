SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

SET ANSI_NULLS ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	: sp_InsertTOPBlockStatus_WS 7,'erwin'
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_InsertTOPBlockStatus_WS
	   @ID INT = 7 ,
	   @CreatedBy VARCHAR(20) = ''
AS
	   BEGIN

	   --SET NOCOUNT ON;

			 DECLARE @SparePartPOStatusID INT 

			 SELECT	@SparePartPOStatusID = a.[SparePartPOStatusID]
			 FROM	dbo.[TOPBlockStatus] a
			 WHERE	a.[ID] = @ID

			 PRINT @SparePartPOStatusID

			 IF ( ISNULL(@SparePartPOStatusID, 0) > 0 )
				BEGIN


					  UPDATE	SpPO
					  SET		[TOPBlockStatusID] = @ID ,
								[LastUpdateBy] = @CreatedBy ,
								[LastUpdateTime] = GETDATE()
					  FROM		dbo.[SparePartPO] SpPO
					  INNER JOIN dbo.[SparePartPOStatus] spSta ON spSta.[SparePartPOID] = SpPO.[ID]
					  WHERE		spSta.[ID] = @SparePartPOStatusID
								AND SpPO.[RowStatus] = 0



					  UPDATE	iph
					  SET		[TOPBlockStatusID] = @ID ,
								[LastUpdateBy] = @CreatedBy ,
								[LastUpdateTime] = GETDATE()
					  FROM		[dbo].[IndentPartHeader] iph
					  INNER JOIN (
								   SELECT	iph.[ID]
								   FROM		dbo.[SparePartPOStatus] a
								   INNER JOIN dbo.[SparePartPO] b ON [b].[ID] = [a].[SparePartPOID]
								   INNER JOIN dbo.[SparePartPODetail] c ON b.[ID] = c.[SparePartPOID]
								   INNER JOIN dbo.[IndentPartPO] i ON [i].[SparePartPODetailID] = [c].[ID]
								   INNER JOIN dbo.[IndentPartDetail] ipd ON [ipd].[ID] = [i].[IndentPartDetailID]
								   INNER JOIN dbo.[IndentPartHeader] iph ON [iph].[ID] = [ipd].[IndentPartHeaderID]
								   WHERE	a.[ID] = @SparePartPOStatusID
											AND b.[RowStatus] = 0
											AND c.[RowStatus] = 0
											AND i.[RowStatus] = 0
											AND ipd.[RowStatus] = 0
											AND iph.[RowStatus] = 0
								 ) X ON [X].[ID] = [iph].[ID]
							 
				END
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, October 07, 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:   
/*
[sp_RetrieveV_SparePartFlowList]

[sp_RetrieveV_SparePartFlowList] 
	@Where=' WHERE (V_SparePartFlow.POID > 1 AND V_SparePartFlow.PODate >= ''2018/01/01'' AND V_SparePartFlow.PODate <= ''2018/02/28'')',
	@PageNumber = 2,
	@PageSize = 50
[sp_RetrieveV_SparePartFlowList]
*/
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_RetrieveV_SparePartFlowList
	   @Where VARCHAR(8000) = '' ,
	   @Sort VARCHAR(8000) = '' ,
	   @PageNumber INT = 1 ,
	   @PageSize INT = 50
AS
	   BEGIN
			 SET TRANSACTION ISOLATION LEVEL READ COMMITTED
			 SET NOCOUNT ON
			 IF @PageNumber < 1
				SET @PageNumber = 1

		 

			 SET @Where = REPLACE(@Where, 'V_SparePartFlow.', 'V_SparePartFlow_v2.')
			 SET @Sort = REPLACE(@Sort, 'V_SparePartFlow.', 'V_SparePartFlow_v2.')

		

			 DECLARE @SortBy VARCHAR(100)= ''

			 DECLARE @strPageSize VARCHAR(50)
			 DECLARE @strStartRow VARCHAR(50)
			 DECLARE @strEndRow VARCHAR(50)
			 DECLARE @strFilter VARCHAR(MAX)
			 DECLARE @strGroup VARCHAR(MAX)		
			 DECLARE @TOP VARCHAR(100)

			 SET @TOP = CASE WHEN @Sort <> '' THEN 'TOP 100 PERCENT '
							 ELSE ''
						END 

			 IF @Sort <> ''
				SET @Sort = ' ORDER By ' + @Sort

			 SET @SortBy = @Sort
			 SET @SortBy = REPLACE(@Sort, 'V_SparePartFlow_v2.', 'cte_V_SparePartFlow_v2.')

	

  /*Set paging variables.*/
			 SET @strPageSize = CAST(@PageSize AS VARCHAR(50))
			 SET @strStartRow = CAST(( ( @PageNumber - 1 ) * @PageSize + 1 ) AS VARCHAR(50))
			 SET @strEndRow = CAST(( ( @PageNumber - 1 ) * @PageSize + @PageSize ) AS VARCHAR(50))

	

			 EXEC ( ' 

			 DECLARE @PageSize int
			 SET @PageSize = ' + @strPageSize + '
      
 


		 
       
			 ;WITH    cte_V_SparePartFlow_v2
			 AS ( 
			 SELECT		
			 '+  @Top +'
			 ROW_NUMBER() OVER ( ORDER BY ( SELECT  NULL ) ) AS [Row] , 
			 *
			 FROM     V_SparePartFlow_v2'
			 + @Where + '' 
			 + @Sort + '
			 )
			 SELECT  *
			 FROM    cte_V_SparePartFlow_v2
			 WHERE   1 = 1
			 AND [Row] BETWEEN ' + @strStartRow + ' AND ' + @strEndRow + ' 
			 ' +@SortBy+'
      

			 SELECT  COUNT(*)
			 FROM    V_SparePartFlow_v2
			 ' + @Where + '

	  
                

   

			 ' )
	   END
GO

CREATE PROCEDURE sp_select
	   (
		 @table_name sysname ,
		 @spid INT = NULL ,
		 @max_pages INT = 1000
	   )
AS
	   SET NOCOUNT ON
  
	   DECLARE @status INT ,
			   @object_id INT ,
			   @db_id INT

	   EXEC @status = sp_select_get_object_id
		@table_name = @table_name ,
		@spid = @spid ,
		@object_id = @object_id OUTPUT
  
	   IF @object_id IS NULL
		  BEGIN 
				RAISERROR('The table [%s] does not exist', 16, 1, @table_name)
				RETURN (-1)
		  END
  
	   SET @db_id = DB_ID(PARSENAME(@table_name, 3))
    
	   EXEC @status = sp_selectpages
		@object_id = @object_id ,
		@db_id = @db_id ,
		@max_pages = @max_pages
  
	   RETURN (@status)
GO

CREATE PROCEDURE sp_selectpages
	   (
		 @object_id INT ,
		 @db_id INT = NULL ,
		 @max_pages INT = 100
	   )
AS
	   BEGIN
			 SET NOCOUNT ON

			 DECLARE @SQL NVARCHAR(MAX) ,
					 @PageFID SMALLINT ,
					 @PagePID INT ,
					 @rowcount INT
            
			 IF @db_id IS NULL
				SET @db_id = DB_ID()

			 IF OBJECT_NAME(@object_id, @db_id) IS NULL
				BEGIN
					  RAISERROR ('The object with id [%d] does not exist in the database with id [%d]', 16, 1, @object_id, @db_id);
					  RETURN(-1)
				END

			 CREATE TABLE #DBCC_IND
					(
					  ROWID INTEGER IDENTITY(1, 1)
									PRIMARY KEY ,
					  PageFID SMALLINT ,
					  PagePID INTEGER ,
					  IAMFID INTEGER ,
					  IAMPID INTEGER ,
					  ObjectID INTEGER ,
					  IndexID INTEGER ,
					  PartitionNumber BIGINT ,
					  PartitionID BIGINT ,
					  Iam_Chain_Type VARCHAR(80) ,
					  PageType INTEGER ,
					  IndexLevel INTEGER ,
					  NexPageFID INTEGER ,
					  NextPagePID INTEGER ,
					  PrevPageFID INTEGER ,
					  PrevPagePID INTEGER
					)

			 CREATE TABLE #DBCC_Page
					(
					  ROWID INTEGER IDENTITY(1, 1)
									PRIMARY KEY ,
					  ParentObject VARCHAR(500) ,
					  Object VARCHAR(500) ,
					  Field VARCHAR(500) ,
					  Value VARCHAR(MAX)
					)

			 CREATE TABLE #Results
					(
					  ROWID INTEGER PRIMARY KEY ,
					  Page VARCHAR(100) ,
					  Slot VARCHAR(300) ,
					  Object VARCHAR(300) ,
					  FieldName VARCHAR(300) ,
					  Value VARCHAR(6000)
					)

			 CREATE TABLE #Columns
					(
					  ColumnID INTEGER PRIMARY KEY ,
					  Name VARCHAR(800)
					)

			 SELECT	@SQL = N'SELECT colid, name
                     FROM ' + QUOTENAME(DB_NAME(@db_id)) + N'..syscolumns
                    WHERE id = @object_id'

			 INSERT	INTO #Columns
					EXEC sp_executesql
						@SQL ,
						N'@object_id int' ,
						@object_id
    
			 SELECT	@rowcount = @@ROWCOUNT
    
			 IF @rowcount = 0
				BEGIN
					  RAISERROR('No columns to return for table with id [%d]', 16, 1, @object_id)
					  RETURN(-1)
				END

			 SELECT	@SQL = N'DBCC IND(' + QUOTENAME(DB_NAME(@db_id)) + N', ' + CONVERT(VARCHAR(11), @object_id)
					+ N', 1) WITH NO_INFOMSGS'

			 DBCC TRACEON(3604) WITH NO_INFOMSGS

			 INSERT	INTO #DBCC_IND
					EXEC (
						   @SQL
						)
    
			 DECLARE cCursor CURSOR LOCAL READ_ONLY
			 FOR
					 SELECT TOP ( @max_pages )
							PageFID ,
							PagePID
					 FROM	#DBCC_IND
					 WHERE	PageType = 1

			 OPEN cCursor

			 FETCH NEXT FROM cCursor INTO @PageFID, @PagePID 

			 WHILE @@FETCH_STATUS = 0
				   BEGIN
						 DELETE	#DBCC_Page
      
						 SELECT	@SQL = N'DBCC PAGE (' + QUOTENAME(DB_NAME(@db_id)) + N','
								+ CONVERT(VARCHAR(11), @PageFID) + N',' + CONVERT(VARCHAR(11), @PagePID)
								+ N', 3) WITH TABLERESULTS, NO_INFOMSGS '
      
						 INSERT	INTO #DBCC_Page
								EXEC (
									   @SQL
									)
      
						 DELETE	FROM #DBCC_Page
						 WHERE	Object NOT LIKE 'Slot %'
								OR Field = ''
								OR Field IN ( 'Record Type', 'Record Attributes' )
								OR ParentObject IN ( 'PAGE HEADER:' )
      
						 INSERT	INTO #Results
						 SELECT	ROWID ,
								CAST(@PageFID AS VARCHAR(20)) + ':' + CAST(@PagePID AS VARCHAR(20)) ,
								ParentObject ,
								Object ,
								Field ,
								Value
						 FROM	#DBCC_Page

						 FETCH NEXT FROM cCursor INTO @PageFID, @PagePID 
				   END
    
			 CLOSE cCursor
			 DEALLOCATE cCursor

			 SELECT	@SQL = N' SELECT ' + STUFF(CAST((
													  SELECT	N',' + QUOTENAME(Name) + N''
													  FROM		#Columns
													  ORDER BY	ColumnID
													FOR
													  XML PATH('')
													) AS VARCHAR(MAX)), 1, 1, N'') + N'
                    FROM (SELECT CONVERT(varchar(20), Page) + CONVERT(varchar(500), Slot) p
                               , FieldName x_FieldName_x
                               , Value x_Value_x 
                            FROM #Results) Tab
                    PIVOT(MAX(Tab.x_Value_x) FOR Tab.x_FieldName_x IN( ' + STUFF((
																				   SELECT	N',' + QUOTENAME(Name) + N''
																				   FROM		#Columns
																				   ORDER BY	ColumnID
																				 FOR
																				   XML PATH(N'')
																				 ), 1, 1, N'') + N' )
                    ) AS pvt'

			 EXEC (@SQL)
    
			 RETURN (0)
	   END
GO

CREATE PROCEDURE sp_select_get_object_id
	   (
		 @table_name sysname ,
		 @spid INT = NULL ,
		 @object_id INT OUTPUT
	   )
AS
	   DECLARE @table sysname ,
			   @db_name sysname ,
			   @db_id INT ,
			   @file_name NVARCHAR(MAX) ,
			   @status INT ,
			   @rowcount INT

	   IF PARSENAME(@table_name, 3) = N'tempdb'
		  BEGIN
				SET @table = PARSENAME(@table_name, 1)
      
				IF (
					 SELECT	COUNT(*)
					 FROM	tempdb.sys.tables
					 WHERE	name LIKE @table + N'[_][_]%'
				   ) > 1
				   BEGIN
            -- determine the default trace file
						 SELECT	@file_name = SUBSTRING(path, 0, LEN(path) - CHARINDEX(N'\', REVERSE(path)) + 1)
								+ N'\Log.trc'
						 FROM	sys.traces
						 WHERE	is_default = 1;  
						 CREATE TABLE #objects
								(
								  ObjectId sysname PRIMARY KEY
								)
            
            -- Match the spid with db_id and object_id via the default trace file
						 INSERT	INTO #objects
						 SELECT	o.object_id
						 FROM	sys.fn_trace_gettable(@file_name, DEFAULT) AS gt
						 JOIN	tempdb.sys.objects AS o ON gt.ObjectID = o.object_id
						 LEFT JOIN (
									 SELECT DISTINCT
											spid ,
											dbid
									 FROM	master..sysprocesses
									 WHERE	spid = @spid
											OR @spid IS NULL
								   ) dr ON dr.spid = gt.SPID
						 WHERE	gt.DatabaseID = 2
								AND gt.EventClass = 46 -- (Object:Created Event from sys.trace_events)  
								AND o.create_date >= DATEADD(ms, -100, gt.StartTime)
								AND o.create_date <= DATEADD(ms, 100, gt.StartTime)
								AND o.name LIKE @table + N'[_][_]%'
								AND (
									  gt.SPID = @spid
									  OR (
										   @spid IS NULL
										   AND dr.dbid = DB_ID()
										 )
									)
              
						 SET @rowcount = @@ROWCOUNT
            
						 IF @rowcount = 0
							BEGIN
								  RAISERROR('Unable to figure out which temp table with name [%s] to select, please run the procedure on a specific database, or specify a @spid to filter on.', 16,1, @table_name)
								  RETURN(-1)
							END
            
						 IF @rowcount > 1
							AND @spid IS NULL
							BEGIN
								  RAISERROR('There are %d temp tables with the name [%s] active in your database. Please specify the @spid you wish to find it for.', 16, 1, @rowcount, @table_name)
								  RETURN(-1)
							END   
            
						 IF @rowcount > 1
							BEGIN
								  RAISERROR('There are %d temp tables with the name [%s] active on the spid %d. There must be something wrong in this procedure. Showing the first one', 16, 1, @rowcount, @table_name, @spid)
                -- We'll continue with the first match.
							END

						 SELECT TOP 1
								@object_id = ObjectId
						 FROM	#objects
						 ORDER BY ObjectId
				   END
				ELSE
				   BEGIN
						 SELECT	@object_id = object_id
						 FROM	tempdb.sys.tables
						 WHERE	name LIKE @table + N'[_][_]%'
				   END
		  END
	   ELSE
		  SET @object_id = OBJECT_ID(@table_name)
  
	   RETURN (0)
GO

CREATE PROCEDURE sp_select_get_rowcount
	   (
		 @table_name sysname ,
		 @spid INT = NULL
	   )
AS
	   SET NOCOUNT ON
  
	   DECLARE @status INT ,
			   @object_id INT ,
			   @db_id INT ,
			   @nsql NVARCHAR(1000)

	   EXEC @status = sp_select_get_object_id
		@table_name = @table_name ,
		@spid = @spid ,
		@object_id = @object_id OUTPUT

	   IF @object_id IS NULL
		  BEGIN
				RAISERROR('The table %s does not exist.', 16, 1, @table_name) 
				RETURN (-1)
		  END 

	   SELECT	@nsql = N'USE ' + PARSENAME(@table_name, 3) + '
    SELECT rows = SUM(st.row_count)
      FROM tempdb.sys.dm_db_partition_stats st
     WHERE index_id < 2
       AND object_id = @object_id'

	   EXEC @status = sp_executesql
		@nsql ,
		N'@object_id int' ,
		@object_id

	   RETURN (@status)
GO

CREATE PROCEDURE sp_SetTOPSPOrderDueDate
	   (
		 @BillingNumber VARCHAR(25) ,
		 @DealerCode VARCHAR(20)
	   )
AS
	   BEGIN

			 SET NOCOUNT ON;
			 DECLARE @TVAL INT = NULL ,
					 @DATe DATETIME
			 DECLARE @SPBillingID INT= NULL

			 SELECT TOP 1
					@SPBillingID = a.ID ,
					@DATe = a.BillingDate ,
					@TVAL = d.TermOfPaymentValue
			 FROM	dbo.SparePartBilling a
			 INNER JOIN dbo.SparePartBillingDetail bd ON bd.SparePartBillingID = a.ID
														 AND bd.RowStatus = a.RowStatus
			 INNER JOIN SparePartDODetail spdd ON spdd.ID = bd.SparePartDODetailID
			 INNER JOIN dbo.SparePartPOEstimate spe ON spe.ID = spdd.SparePartPOEstimateID
			 INNER JOIN dbo.SparePartPO c ON c.ID = spe.SparePartPOID
											 AND c.RowStatus = 0
			 INNER JOIN dbo.TermOfPayment d ON d.ID = c.TermOfPaymentID
			 INNER JOIN dbo.Dealer dd ON dd.ID = a.DealerID
			 WHERE	1 = 1
		--a.ID = @SPBillingID
					AND a.BillingNumber = @BillingNumber
					AND dd.DealerCode = @DealerCode
					AND a.RowStatus = 0
              --   AND d.[PaymentType] = 2
			 ORDER BY a.LastUpdateTime DESC
		
        --SELECT  @SPBillingID

			 IF ISNULL(@SPBillingID, 0) > 0
				AND @TVAL > 0
				BEGIN

					  UPDATE	dbo.TOPSPDueDate
					  SET		DueDate = dbo.ufn_GetActiveDate(DATEADD(DAY, @TVAL, @DATe)) ,
								RowStatus = 0 ,
								LastUpdateTime = GETDATE() ,
								LastUpdateBy = 'WS'
					  WHERE		SparePartBillingID = @SPBillingID
								AND RowStatus = 0

					  INSERT	INTO dbo.TOPSPDueDate
								(
								  SparePartBillingID ,
								  DueDate ,
								  RowStatus ,
								  CreatedBy ,
								  CreatedTime ,
								  LastUpdateBy ,
								  LastUpdateTime
								)
					  SELECT	a.ID , -- SparePartBillingID - int
								dbo.ufn_GetActiveDate(DATEADD(DAY, @TVAL, @DATe)) , -- DueDate - datetime
								0 , -- RowStatus - smallint
								'ADMIN' , -- CreatedBy - varchar(20)
								GETDATE() , -- CreatedTime - datetime
								'' , -- LastUpdateBy - varchar(20)
								GETDATE()  -- LastUpdateTime - datetime
					  FROM		dbo.SparePartBilling a
					  WHERE		a.ID = @SPBillingID
								AND a.ID NOT IN ( SELECT	b.SparePartBillingID
												  FROM		dbo.TOPSPDueDate b
												  WHERE		b.SparePartBillingID = @SPBillingID )

				END
		 

	   END
GO

/*

exec [sp_TOPCombineCeiling] 1,'100128',2,'20180901','20180930',1,0--ceiling report
 exec [sp_TOPCombineCeiling] 1,'100128',2,'20180901','20180930',0,0--ceiling report

*/
CREATE PROCEDURE sp_TOPCombineCeiling
	   (
		 @ProductCategoryID INT = 1--0:all
		 ,
		 @CreditAccount VARCHAR(2000) = '' --'':all
		 ,
		 @PaymentType SMALLINT = 2--0:all
		 ,
		 @StartDate DATETIME ,
		 @EndDate DATETIME ,
		 @IsReport SMALLINT ,
		 @IsShowReportDetail SMALLINT 
	   )
AS
	   BEGIN
 
			 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
			 SET NOCOUNT ON

			 SET @PaymentType = 2
			 IF 1 = 1
				BEGIN
					  IF OBJECT_ID('tempdb..#header') IS NOT NULL
						 DROP TABLE #header

					  IF OBJECT_ID('tempdb..#rep') IS NOT NULL
						 DROP TABLE #rep

					  IF OBJECT_ID('tempdb..#repDetail') IS NOT NULL
						 DROP TABLE #repDetail
				END
			 CREATE TABLE #header
					(
					  ID INT IDENTITY(1, 1) ,
					  ProductCategoryID INT ,
					  ProductCategory VARCHAR(10) ,
					  CreditAccount VARCHAR(10) ,
					  PaymentType SMALLINT ,
					  PaymentTypeCode VARCHAR(20)
					) 
			 CREATE TABLE #rep
					(
					  --ID int identity(1,1)
		--, 
					  ROrder SMALLINT ,
					  ReportKind VARCHAR(100) ,
					  ProductCategoryID INT ,
					  ProductCategory VARCHAR(10) ,
					  CreditAccount VARCHAR(10) ,
					  PaymentType SMALLINT ,
					  PaymentTypeCode VARCHAR(20) ,
					  Data VARCHAR(50) ,
					  D1 MONEY ,
					  D2 MONEY ,
					  D3 MONEY ,
					  D4 MONEY ,
					  D5 MONEY ,
					  D6 MONEY ,
					  D7 MONEY ,
					  D8 MONEY ,
					  D9 MONEY ,
					  D10 MONEY ,
					  D11 MONEY ,
					  D12 MONEY ,
					  D13 MONEY ,
					  D14 MONEY ,
					  D15 MONEY ,
					  D16 MONEY ,
					  D17 MONEY ,
					  D18 MONEY ,
					  D19 MONEY ,
					  D20 MONEY ,
					  D21 MONEY ,
					  D22 MONEY ,
					  D23 MONEY ,
					  D24 MONEY ,
					  D25 MONEY ,
					  D26 MONEY ,
					  D27 MONEY ,
					  D28 MONEY ,
					  D29 MONEY ,
					  D30 MONEY ,
					  D31 MONEY
					)


			 CREATE TABLE #repDetail
					(
					  --ID int identity(1,1)
		--, 
					  ROrder SMALLINT ,
					  ReportKind VARCHAR(100) ,
					  ProductCategoryID INT ,
					  ProductCategory VARCHAR(10) ,
					  CreditAccount VARCHAR(10) ,
					  PaymentType SMALLINT ,
					  PaymentTypeCode VARCHAR(20) ,
					  Data VARCHAR(50) ,
					  D1 MONEY ,
					  D2 MONEY ,
					  D3 MONEY ,
					  D4 MONEY ,
					  D5 MONEY ,
					  D6 MONEY ,
					  D7 MONEY ,
					  D8 MONEY ,
					  D9 MONEY ,
					  D10 MONEY ,
					  D11 MONEY ,
					  D12 MONEY ,
					  D13 MONEY ,
					  D14 MONEY ,
					  D15 MONEY ,
					  D16 MONEY ,
					  D17 MONEY ,
					  D18 MONEY ,
					  D19 MONEY ,
					  D20 MONEY ,
					  D21 MONEY ,
					  D22 MONEY ,
					  D23 MONEY ,
					  D24 MONEY ,
					  D25 MONEY ,
					  D26 MONEY ,
					  D27 MONEY ,
					  D28 MONEY ,
					  D29 MONEY ,
					  D30 MONEY ,
					  D31 MONEY
					)
			 DECLARE @i INT ,
					 @n INT 
	
			 INSERT	INTO #header
					(
					  ProductCategoryID ,
					  ProductCategory ,
					  CreditAccount ,
					  PaymentType ,
					  PaymentTypeCode
					)
			 SELECT	pc.ID ,
					pc.Code ,
					ca.CreditAccount ,
					tp.PaymentType ,
					CASE tp.PaymentType
					  WHEN 1 THEN 'COD'
					  ELSE 'TOP'
					END PaymentTypeCode
			 FROM	(
					  SELECT	*
					  FROM		ProductCategory pc
					  WHERE		pc.ID IN ( 1, 2 )
					) pc
			 JOIN	v_CreditAccount ca ON 1 = 1
			 JOIN	(
					  SELECT DISTINCT
								( tp.PaymentType )
					  FROM		TermOfPayment tp
					  WHERE		tp.PaymentType IN ( 1, 2 )
					) tp ON 1 = 1
			 WHERE	1 = 1
					AND pc.ID = CASE WHEN @ProductCategoryID = 0 THEN pc.ID
									 ELSE @ProductCategoryID
								END
					AND (
						  ca.CreditAccount IN ( SELECT	[fn_DELIMITED].[value]
												FROM	[dbo].[fn_DELIMITED](@CreditAccount, ';') )
						  OR @CreditAccount = ''
						)
		--case when @CreditAccount='' then ca.CreditAccount else @CreditAccount end 
					AND tp.PaymentType = CASE WHEN @PaymentType = 0 THEN tp.PaymentType
											  ELSE @PaymentType
										 END 


			 DECLARE @vProductCategoryID INT ,
					 @vProductCategory VARCHAR(10) ,
					 @vCreditAccount VARCHAR(10) ,
					 @vPaymentType SMALLINT ,
					 @vPaymentTypeCode VARCHAR(10) 

		--Finish Unit
			 SET @i = 1
			 SELECT	@n = MAX(ID)
			 FROM	#header
		
			 WHILE ( @i <= @n )
				   BEGIN
                
		
						 SELECT	@vProductCategoryID = h.ProductCategoryID ,
								@vProductCategory = h.ProductCategory ,
								@vCreditAccount = h.CreditAccount ,
								@vPaymentType = h.PaymentType ,
								@vPaymentTypeCode = h.PaymentTypeCode
						 FROM	#header h
						 WHERE	h.ID = @i 
		
		 
						 INSERT	INTO #rep
						 SELECT	1 ,
								'Finish Unit' ,
								a.*
						 FROM	dbo.fn_TransferBalance(@vProductCategoryID, @vCreditAccount, @vPaymentType, @StartDate,
													   @EndDate, @IsReport, @IsShowReportDetail) a
				
						 SET @i = @i + 1
				   END 

   
	--SParepart
			 SET @i = 1
			 SELECT	@n = MAX(ID)
			 FROM	#header
		
			 WHILE ( @i <= @n )
				   BEGIN
              
		
						 SELECT	@vProductCategoryID = h.ProductCategoryID ,
								@vProductCategory = h.ProductCategory ,
								@vCreditAccount = h.CreditAccount ,
								@vPaymentType = h.PaymentType ,
								@vPaymentTypeCode = h.PaymentTypeCode
						 FROM	#header h
						 WHERE	h.ID = @i 
		
		 
						 INSERT	INTO #rep
						 SELECT	2 ,
								'Spare Part' ,
								a.*
						 FROM	dbo.fn_TOPSPBalance(@vProductCategoryID, @vCreditAccount, @vPaymentType, @StartDate,
													@EndDate, @IsReport, @IsShowReportDetail) a
				
						 SET @i = @i + 1
				   END 


	

	--Gabungan
			 IF @IsShowReportDetail = 0
				BEGIN 
					  SET @i = 1
					  SELECT	@n = MAX(ID)
					  FROM		#header
		
					  WHILE ( @i <= @n )
							BEGIN
              
		
								  SELECT	@vProductCategoryID = h.ProductCategoryID ,
											@vProductCategory = h.ProductCategory ,
											@vCreditAccount = h.CreditAccount ,
											@vPaymentType = h.PaymentType ,
											@vPaymentTypeCode = h.PaymentTypeCode
								  FROM		#header h
								  WHERE		h.ID = @i 
		
								  INSERT	INTO #rep
											(
											  ROrder ,
											  ReportKind ,
											  ProductCategoryID ,
											  ProductCategory ,
											  CreditAccount ,
											  PaymentType ,
											  PaymentTypeCode ,
											  Data ,
											  D1 ,
											  D2 ,
											  D3 ,
											  D4 ,
											  D5 ,
											  D6 ,
											  D7 ,
											  D8 ,
											  D9 ,
											  D10 ,
											  D11 ,
											  D12 ,
											  D13 ,
											  D14 ,
											  D15 ,
											  D16 ,
											  D17 ,
											  D18 ,
											  D19 ,
											  D20 ,
											  D21 ,
											  D22 ,
											  D23 ,
											  D24 ,
											  D25 ,
											  D26 ,
											  D27 ,
											  D28 ,
											  D29 ,
											  D30 ,
											  D31
											)
								  SELECT	3 ,
											'Gabungan' ,
											a.ProductCategoryID ,
											a.ProductCategory ,
											a.CreditAccount ,
											a.PaymentType ,
											a.PaymentTypeCode ,
											a.Data ,
											SUM(a.D1) ,
											SUM(a.D2) ,
											SUM(a.D3) ,
											SUM(a.D4) ,
											SUM(a.D5) ,
											SUM(a.D6) ,
											SUM(a.D7) ,
											SUM(a.D8) ,
											SUM(a.D9) ,
											SUM(a.D10) ,
											SUM(a.D11) ,
											SUM(a.D12) ,
											SUM(a.D13) ,
											SUM(a.D14) ,
											SUM(a.D15) ,
											SUM(a.D16) ,
											SUM(a.D17) ,
											SUM(a.D18) ,
											SUM(a.D19) ,
											SUM(a.D20) ,
											SUM(a.D21) ,
											SUM(a.D22) ,
											SUM(a.D23) ,
											SUM(a.D24) ,
											SUM(a.D25) ,
											SUM(a.D26) ,
											SUM(a.D27) ,
											SUM(a.D28) ,
											SUM(a.D29) ,
											SUM(a.D30) ,
											SUM(a.D31)
								  FROM		#rep a
								  WHERE		a.CreditAccount = @vCreditAccount
								  GROUP BY	a.ProductCategoryID ,
											a.ProductCategory ,
											a.CreditAccount ,
											a.PaymentType ,
											a.PaymentTypeCode ,
											a.Data 
				
								  SET @i = @i + 1
							END 
				END
	
			 IF @IsShowReportDetail = 0
				BEGIN
					  SELECT	1 ID ,
								*
					  FROM		#rep r
					  ORDER BY	r.CreditAccount ,
								r.ROrder
				END
			 ELSE
				BEGIN

					  SELECT	0 ID ,
								*
					  FROM		(
								  SELECT	[a].[ROrder] ,
											[a].[ReportKind] ,
											[a].[ProductCategoryID] ,
											[a].[ProductCategory] ,
											[a].[CreditAccount] ,
											[a].[PaymentType] ,
											[a].[PaymentTypeCode] ,
											CASE WHEN [a].[Data] = '01. Plafon'
													  AND a.[ROrder] = 1 THEN '01. Plafon FU'
												 WHEN [a].[Data] = '01. Plafon'
													  AND a.[ROrder] = 2 THEN '02. Plafon Spare Part'
												 WHEN [a].[Data] = '02. Total SO'
													  AND a.[ROrder] = 1 THEN '04. Total SO FU'
												 WHEN [a].[Data] = '02. Total SO'
													  AND a.[ROrder] = 2 THEN '05. Total Billing Spare Part'
												 WHEN [a].[Data] = '03. Total Payment'
													  AND a.[ROrder] = 1 THEN '06. Payment SO FU'
												 WHEN [a].[Data] = '03. Total Payment'
													  AND a.[ROrder] = 2 THEN '07. Payment Billing Spare Part'
												 WHEN [a].[Data] = '04. Available Ceiling'
													  AND a.[ROrder] = 1 THEN '08. Available Ceiling FU'
												 WHEN [a].[Data] = '04. Available Ceiling'
													  AND a.[ROrder] = 2 THEN '09. Available Ceiling Spare Part'
											END AS [Data] ,
											[a].[D1] ,
											[a].[D2] ,
											[a].[D3] ,
											[a].[D4] ,
											[a].[D5] ,
											[a].[D6] ,
											[a].[D7] ,
											[a].[D8] ,
											[a].[D9] ,
											[a].[D10] ,
											[a].[D11] ,
											[a].[D12] ,
											[a].[D13] ,
											[a].[D14] ,
											[a].[D15] ,
											[a].[D16] ,
											[a].[D17] ,
											[a].[D18] ,
											[a].[D19] ,
											[a].[D20] ,
											[a].[D21] ,
											[a].[D22] ,
											[a].[D23] ,
											[a].[D24] ,
											[a].[D25] ,
											[a].[D26] ,
											[a].[D27] ,
											[a].[D28] ,
											[a].[D29] ,
											[a].[D30] ,
											[a].[D31]
								  FROM		#rep a
								  WHERE		a.[CreditAccount] = @CreditAccount
											AND a.[Data] IN ( '01. Plafon', '02. Total SO', '03. Total Payment',
															  '04. Available Ceiling' )
							--ORDER BY [a].[ROrder]
								  UNION  ALL
								  SELECT	1 ,
											'Gabungan' ,
											[a].[ProductCategoryID] ,
											[a].[ProductCategory] ,
											[a].[CreditAccount] ,
											[a].[PaymentType] ,
											[a].[PaymentTypeCode] ,
											'03. Plafon Gabungan' AS [Data] ,
											SUM(a.D1) ,
											SUM(a.D2) ,
											SUM(a.D3) ,
											SUM(a.D4) ,
											SUM(a.D5) ,
											SUM(a.D6) ,
											SUM(a.D7) ,
											SUM(a.D8) ,
											SUM(a.D9) ,
											SUM(a.D10) ,
											SUM(a.D11) ,
											SUM(a.D12) ,
											SUM(a.D13) ,
											SUM(a.D14) ,
											SUM(a.D15) ,
											SUM(a.D16) ,
											SUM(a.D17) ,
											SUM(a.D18) ,
											SUM(a.D19) ,
											SUM(a.D20) ,
											SUM(a.D21) ,
											SUM(a.D22) ,
											SUM(a.D23) ,
											SUM(a.D24) ,
											SUM(a.D25) ,
											SUM(a.D26) ,
											SUM(a.D27) ,
											SUM(a.D28) ,
											SUM(a.D29) ,
											SUM(a.D30) ,
											SUM(a.D31)
								  FROM		#rep a
								  WHERE		a.[CreditAccount] = @CreditAccount
											AND a.[Data] = '01. Plafon'
								  GROUP BY	[a].[ProductCategoryID] ,
											[a].[ProductCategory] ,
											[a].[CreditAccount] ,
											[a].[PaymentType] ,
											[a].[PaymentTypeCode]
								  UNION  ALL
								  SELECT	1 ,
											'Gabungan' ,
											[a].[ProductCategoryID] ,
											[a].[ProductCategory] ,
											[a].[CreditAccount] ,
											[a].[PaymentType] ,
											[a].[PaymentTypeCode] ,
											'10. Ceiling Gabungan' AS [Data] ,
											SUM(a.D1) ,
											SUM(a.D2) ,
											SUM(a.D3) ,
											SUM(a.D4) ,
											SUM(a.D5) ,
											SUM(a.D6) ,
											SUM(a.D7) ,
											SUM(a.D8) ,
											SUM(a.D9) ,
											SUM(a.D10) ,
											SUM(a.D11) ,
											SUM(a.D12) ,
											SUM(a.D13) ,
											SUM(a.D14) ,
											SUM(a.D15) ,
											SUM(a.D16) ,
											SUM(a.D17) ,
											SUM(a.D18) ,
											SUM(a.D19) ,
											SUM(a.D20) ,
											SUM(a.D21) ,
											SUM(a.D22) ,
											SUM(a.D23) ,
											SUM(a.D24) ,
											SUM(a.D25) ,
											SUM(a.D26) ,
											SUM(a.D27) ,
											SUM(a.D28) ,
											SUM(a.D29) ,
											SUM(a.D30) ,
											SUM(a.D31)
								  FROM		#rep a
								  WHERE		a.[CreditAccount] = @CreditAccount
											AND a.[Data] = '04. Available Ceiling'
								  GROUP BY	[a].[ProductCategoryID] ,
											[a].[ProductCategory] ,
											[a].[CreditAccount] ,
											[a].[PaymentType] ,
											[a].[PaymentTypeCode]
								) X
					  ORDER BY	[X].[Data]
				END

			    
	
	   END
GO

/*

exec [sp_TOPSPCeiling] 1,'100128',2,'20180901','20180930',1,0--ceiling report
 exec [sp_TOPSPCeiling] 1,'100128',2,'20180901','20180930',1,1--ceiling report

*/
CREATE PROCEDURE sp_TOPSPCeiling
	   (
		 @ProductCategoryID INT = 1--0:all
		 ,
		 @CreditAccount VARCHAR(2000) = '' --'':all
		 ,
		 @PaymentType SMALLINT = 2--0:all
		 ,
		 @StartDate DATETIME ,
		 @EndDate DATETIME ,
		 @IsReport SMALLINT ,
		 @IsShowReportDetail SMALLINT 
	   )
AS
	   BEGIN

			 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
			 SET NOCOUNT ON

			 SET @PaymentType = 2
			 IF 1 = 1
				BEGIN
					  IF OBJECT_ID('tempdb..#header') IS NOT NULL
						 DROP TABLE #header

					  IF OBJECT_ID('tempdb..#rep') IS NOT NULL
						 DROP TABLE #rep
				END
			 CREATE TABLE #header
					(
					  ID INT IDENTITY(1, 1) ,
					  ProductCategoryID INT ,
					  ProductCategory VARCHAR(10) ,
					  CreditAccount VARCHAR(10) ,
					  PaymentType SMALLINT ,
					  PaymentTypeCode VARCHAR(20)
					) 
			 CREATE TABLE #rep
					(
					  --ID int identity(1,1)
		--, 
					  ProductCategoryID INT ,
					  ProductCategory VARCHAR(10) ,
					  CreditAccount VARCHAR(10) ,
					  PaymentType SMALLINT ,
					  PaymentTypeCode VARCHAR(20) ,
					  Data VARCHAR(50) ,
					  D1 MONEY ,
					  D2 MONEY ,
					  D3 MONEY ,
					  D4 MONEY ,
					  D5 MONEY ,
					  D6 MONEY ,
					  D7 MONEY ,
					  D8 MONEY ,
					  D9 MONEY ,
					  D10 MONEY ,
					  D11 MONEY ,
					  D12 MONEY ,
					  D13 MONEY ,
					  D14 MONEY ,
					  D15 MONEY ,
					  D16 MONEY ,
					  D17 MONEY ,
					  D18 MONEY ,
					  D19 MONEY ,
					  D20 MONEY ,
					  D21 MONEY ,
					  D22 MONEY ,
					  D23 MONEY ,
					  D24 MONEY ,
					  D25 MONEY ,
					  D26 MONEY ,
					  D27 MONEY ,
					  D28 MONEY ,
					  D29 MONEY ,
					  D30 MONEY ,
					  D31 MONEY
					)
			 DECLARE @i INT ,
					 @n INT 
	
			 INSERT	INTO #header
					(
					  ProductCategoryID ,
					  ProductCategory ,
					  CreditAccount ,
					  PaymentType ,
					  PaymentTypeCode
					)
			 SELECT	pc.ID ,
					pc.Code ,
					ca.CreditAccount ,
					tp.PaymentType ,
					CASE tp.PaymentType
					  WHEN 1 THEN 'COD'
					  ELSE 'TOP'
					END PaymentTypeCode
			 FROM	(
					  SELECT	*
					  FROM		ProductCategory pc
					  WHERE		pc.ID IN ( 1, 2 )
					) pc
			 JOIN	v_CreditAccount ca ON 1 = 1
			 JOIN	(
					  SELECT DISTINCT
								( tp.PaymentType )
					  FROM		TermOfPayment tp
					  WHERE		tp.PaymentType IN ( 1, 2 )
					) tp ON 1 = 1
			 WHERE	1 = 1
					AND pc.ID = CASE WHEN @ProductCategoryID = 0 THEN pc.ID
									 ELSE @ProductCategoryID
								END
					AND (
						  ca.CreditAccount IN ( SELECT	[fn_DELIMITED].[value]
												FROM	[dbo].[fn_DELIMITED](@CreditAccount, ';') )
						  OR @CreditAccount = ''
						)
		--case when @CreditAccount='' then ca.CreditAccount else @CreditAccount end 
					AND tp.PaymentType = CASE WHEN @PaymentType = 0 THEN tp.PaymentType
											  ELSE @PaymentType
										 END 

  --  SELECT  *
  --  FROM    #header
  --  SELECT  *
  --  FROM    #rep
	 --return 
	
			 SET @i = 1
			 SELECT	@n = MAX(ID)
			 FROM	#header
			 WHILE ( @i <= @n )
				   BEGIN
						 DECLARE @vProductCategoryID INT ,
								 @vProductCategory VARCHAR(10) ,
								 @vCreditAccount VARCHAR(10) ,
								 @vPaymentType SMALLINT ,
								 @vPaymentTypeCode VARCHAR(10) 
		
						 SELECT	@vProductCategoryID = h.ProductCategoryID ,
								@vProductCategory = h.ProductCategory ,
								@vCreditAccount = h.CreditAccount ,
								@vPaymentType = h.PaymentType ,
								@vPaymentTypeCode = h.PaymentTypeCode
						 FROM	#header h
						 WHERE	h.ID = @i 
		
		--select @i Looping, @vProductCategory PC
						 INSERT	INTO #rep
						 SELECT	*
						 FROM	dbo.fn_TOPSPBalance(@vProductCategoryID, @vCreditAccount, @vPaymentType, @StartDate,
													@EndDate, @IsReport, @IsShowReportDetail)
				
						 SET @i = @i + 1
				   END 
	
			 IF @IsShowReportDetail = 1
				BEGIN

					  SELECT	1 ID ,
								r.ProductCategoryID ,
								r.ProductCategory ,
								r.CreditAccount ,
								r.PaymentType ,
								r.PaymentTypeCode ,
								Data = CASE r.Data
										 WHEN '02. Total SO' THEN '02. Total Billing'
										 ELSE r.Data
									   END ,
								r.D1 ,
								r.D2 ,
								r.D3 ,
								r.D4 ,
								r.D5 ,
								r.D6 ,
								r.D7 ,
								r.D8 ,
								r.D9 ,
								r.D10 ,
								r.D11 ,
								r.D12 ,
								r.D13 ,
								r.D14 ,
								r.D15 ,
								r.D16 ,
								r.D17 ,
								r.D18 ,
								r.D19 ,
								r.D20 ,
								r.D21 ,
								r.D22 ,
								r.D23 ,
								r.D24 ,
								r.D25 ,
								r.D26 ,
								r.D27 ,
								r.D28 ,
								r.D29 ,
								r.D30 ,
								r.D31
					  FROM		#rep r
					  WHERE		r.Data IN ( '01. Plafon', '02. Total SO', '03. Total Payment', '04. Available Ceiling' )
					  ORDER BY	r.CreditAccount ,
								r.ProductCategoryID ,
								r.PaymentType 	
				END

			 ELSE
				BEGIN
					  SELECT	1 ID ,
								*
					  FROM		#rep r
					  ORDER BY	r.CreditAccount ,
								r.ProductCategoryID ,
								r.PaymentType 		
				END
	
	   END
GO

/*
 Add PO Block Filter
 */
CREATE PROCEDURE sp_TOPSPCeilingDetail ( @TCID INT )
AS
	   BEGIN

 
			 DECLARE @rep TABLE
					 (
					   ID INT IDENTITY(1, 1) ,
					   SONumber VARCHAR(20) ,
					   RegNumber VARCHAR(20) ,
					   PONumber VARCHAR(20) ,
					   Amount MONEY ,
					   IsIncome SMALLINT
					 )
			 DECLARE @startDate DATETIME ,
					 @endDate DATETIME ,
					 @productCategoryID INT ,
					 @PaymentType SMALLINT ,
					 @CreditAccount VARCHAR(10) 		
	
			 SELECT	@startDate = tc.EffectiveDate ,
					@productCategoryID = tc.ProductCategoryID ,
					@PaymentType = tc.PaymentType ,
					@CreditAccount = tc.CreditAccount
			 FROM	TransferCeiling tc WITH ( NOLOCK )
			 WHERE	tc.ID = @TCID 
	
	--set @startDate = DATEADD(day,1-day(@startDate), @startDate) 
	--set @endDate = DATEADD(second,-1, DATEADD(month,1,@startDate))
			 SET @endDate = @startDate 
	
			 INSERT	INTO @rep
			 SELECT	so.BillingNumber SONumber ,
					'' ,
					'' ,
					so.TotalAmount + so.Tax TotalVH ,
					0
			 FROM	dbo.TOPSPTransferCeilingDetail tcd WITH ( NOLOCK )
			 INNER JOIN dbo.SparePartBilling so WITH ( NOLOCK ) ON so.ID = tcd.SparepartBillingID
			 WHERE	1 = 1
					AND tcd.TOPSPTransferCeilingID = @TCID
					AND tcd.RowStatus = 0
					AND so.RowStatus = 0
			 ORDER BY so.BillingNumber 
		
			 INSERT	INTO @rep
			 SELECT	'' ,
					tp.RegNumber ,
					'' ,
					SUM(tcd.Amount) ,
					1
			 FROM	TOPSPTransferCeilingDetail tcd WITH ( NOLOCK )
			 JOIN	TOPSPTransferPayment tp WITH ( NOLOCK ) ON tp.ID = tcd.TOPSPTransferPaymentID
			 WHERE	1 = 1
					AND tcd.TOPSPTransferCeilingID = @TCID
					AND tp.RowStatus = 0
					AND tcd.[RowStatus] = 0
			 GROUP BY tp.RegNumber
			 ORDER BY tp.RegNumber 
	
	
	--refer to logic in  fn_TransferBalance
			 INSERT	INTO @rep
			 SELECT	'' ,
					'' ,
					y.BillingNumber PONumber ,
					SUM(y.TotalAmount + y.Tax) TotalHarga ,
					0
			 FROM	(
					  SELECT	a.BillingNumber
					  FROM		dbo.SparePartBilling a
					  INNER JOIN dbo.SparePartBillingDetail bd ON bd.SparePartBillingID = a.ID
																  AND bd.RowStatus = a.RowStatus
					  INNER JOIN SparePartDODetail spdd ON spdd.ID = bd.SparePartDODetailID
					  INNER JOIN dbo.SparePartPOEstimate spe ON spe.ID = spdd.SparePartPOEstimateID
					  INNER JOIN dbo.SparePartPO c ON c.ID = spe.SparePartPOID
													  AND c.RowStatus = 0
					  INNER JOIN dbo.TermOfPayment d ON d.ID = c.TermOfPaymentID
					  INNER JOIN dbo.Dealer dd ON dd.ID = a.DealerID
					  WHERE		1 = 1
								AND a.RowStatus = 0
								AND dd.CreditAccount = @CreditAccount
								AND a.BillingDate BETWEEN @startDate AND @endDate
								AND d.PaymentType = 2 --TOP
								AND a.RowStatus = 0
					  GROUP BY	a.BillingNumber
					) X
			 INNER JOIN dbo.SparePartBilling y ON y.BillingNumber = X.BillingNumber
			 WHERE	y.RowStatus = 0
			 GROUP BY y.BillingNumber
	
	
			 SELECT	1 ID ,
					r.SONumber ,
					r.RegNumber ,
					r.PONumber ,
					r.Amount ,
					r.IsIncome
			 FROM	@rep r
			 ORDER BY r.ID 
		
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: EWIN BIN Erwin Team by using CodeSmith v 2.6
-- Rev History	: sp_InsertTOPBlockStatus_WS 7,'erwin'
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_UpdateTOPBlockStatus_WSM
	   @ID INT ,
	   @CreatedBy VARCHAR(20) = ''
AS
	   BEGIN

			 SET NOCOUNT ON;
		 

			 IF EXISTS ( SELECT TOP 1
								'*'
						 FROM	[dbo].[SparePartPOStatus] a ( NOLOCK )
						 WHERE	a.[ID] = @ID
								AND a.[BillingNumber] <> '' )
				BEGIN
					  


					  UPDATE	dbo.[TOPBlockStatus]
					  SET		[Status] = 1 ,
								[LastUpdateBy] = @CreatedBy + '-WSM' ,
								[LastUpdateTime] = GETDATE()
					  WHERE		[SparePartPOStatusID] = @ID
				END
	   END
GO

/*
 
 exec TransferCeilingDetail_BalanceUpdate @ProductCategoryID=2, @CreditAccount='100011', @PaymentType=2, @EffectiveDateHeader='2017/08/09',@Amount=5970000003, @IsIncome=0,@TransDateDetail='2017/01/27', @SparePartBillingID=321488 
*/
CREATE PROCEDURE TOPSPTransferCeilingDetail_BalanceUpdate
	   (
		 @ProductCategoryID INT = 2--0:all
		 ,
		 @CreditAccount VARCHAR(1000) = '100011'--'':all
		 ,
		 @PaymentType SMALLINT = 2--0:all
		 ,
		 @EffectiveDateHeader DATETIME = NULL ,
		 @Amount MONEY ,
		 @TransDateDetail DATETIME = NULL ,
		 @SparePartBillingID INT = NULL ,
		 @TOPSPTransferPaymentID INT = NULL ,
		 @IsIncome SMALLINT

		 	
	   )
AS
	   BEGIN
			 SET NOCOUNT ON;
			 DECLARE @LastUpdatedBy VARCHAR(25)= 'WS-UPDATECEILING' 
			 DECLARE @LastUpdatedTime DATETIME = GETDATE()
			 DECLARE @TransferCeilingiD INT = 0
			-- SELECT @EffectiveDateHeader, @TransDateDetail,31
		 
			 UPDATE	dbo.TOPSPTransferCeilingDetail
			 SET	[RowStatus] = -1 ,
					[LastUpdatedBy] = '[TCDetail_B]' ,
					[LastUpdatedTime] = @LastUpdatedTime
			 WHERE	ID IN ( SELECT	b.[ID]
							FROM	dbo.TOPSPTransferCeiling a
							INNER JOIN dbo.TOPSPTransferCeilingDetail b ON [b].TOPSPTransferCeilingID = [a].[ID]
							WHERE	1 = 1
									AND [a].[RowStatus] = 0
									AND b.[RowStatus] = 0
									AND [CreditAccount] = @CreditAccount
									AND [ProductCategoryID] = @ProductCategoryID
									AND [PaymentType] = @PaymentType
									AND (
										  b.[SparePartBillingID] = @SparePartBillingID
										  OR @SparePartBillingID IS NULL
										)
									AND (
										  b.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
										  OR @TOPSPTransferPaymentID IS NULL
										)
									AND ( a.[EffectiveDate] >= @TransDateDetail )
									AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
									AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader)
									AND YEAR([EffectiveDate]) = YEAR(@TransDateDetail)
									AND MONTH([EffectiveDate]) = MONTH(@TransDateDetail) );
					WITH	CTR
							  AS (
								   SELECT	*
								   FROM		dbo.TOPSPTransferCeiling a ( NOLOCK )
								   WHERE	1 = 1
											AND [a].[RowStatus] = 0
											AND [CreditAccount] = @CreditAccount
											AND [ProductCategoryID] = @ProductCategoryID
											AND [PaymentType] = @PaymentType
											AND [EffectiveDate] >= @EffectiveDateHeader
											AND ( [a].[EffectiveDate] >= @TransDateDetail )
											AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
											AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader)
								 ),
							CTDetail
							  AS (
								   SELECT	a.* ,
											ISNULL(@TOPSPTransferPaymentID, -1) TOPSPTransferPaymentID ,
											ISNULL(@SparePartBillingID, -1) SparePartBillingID ,
											@Amount Amount ,
											@IsIncome IsIncome
								   FROM		[CTR] a
								   WHERE	1 = 1
								 )
						 MERGE dbo.TOPSPTransferCeilingDetail AS a
						 USING CTDetail AS b
						 ON a.TOPSPTransferCeilingID = b.[ID]
							AND ISNULL(a.[SparePartBillingID], -1) = b.[SparePartBillingID]
							AND ISNULL(a.[TOPSPTransferPaymentID], -1) = b.[TOPSPTransferPaymentID]
						 WHEN MATCHED THEN
							UPDATE SET
								   [a].[LastUpdatedBy] = @LastUpdatedBy ,
								   a.[LastUpdatedTime] = @LastUpdatedTime ,
								   [a].[IsIncome] = @IsIncome ,
								   a.[Amount] = b.[Amount] ,
								   [a].[RowStatus] = 0
						 WHEN NOT MATCHED THEN
							INSERT (
									 TOPSPTransferCeilingID ,
									 [SparePartBillingID] ,
									 [TOPSPTransferPaymentID] ,
									 [Amount] ,
									 [IsIncome] ,
									 [Status] ,
									 [RowStatus] ,
									 [CreatedBy] ,
									 [CreatedTime] ,
									 [LastUpdatedBy] ,
									 [LastUpdatedTime]
								 
								   )
							VALUES (
									 b.[ID] , -- TransferCeilingID - int
									 @SparePartBillingID , -- SparePartBillingID - int
									 @TOPSPTransferPaymentID , -- TOPSPTransferPaymentID - int
									 @Amount , -- Amount - money
									 @IsIncome , -- IsIncome - smallint
									 0 , -- Status - smallint
									 0 , -- RowStatus - smallint
									 @LastUpdatedBy , -- CreatedBy - varchar(20)
									 GETDATE() , -- CreatedTime - datetime
									 @LastUpdatedBy , -- LastUpdatedBy - varchar(20)
									 GETDATE()  -- LastUpdatedTime - datetime
													
												
									
							
             					   ) ;
								

			 UPDATE	dbo.TOPSPTransferCeilingDetail
			 SET	[RowStatus] = -1 ,
					[LastUpdatedBy] = '[TCDetail_B]' ,
					[LastUpdatedTime] = @LastUpdatedTime
			 WHERE	ID IN ( SELECT	b.[ID]
							FROM	dbo.TOPSPTransferCeiling a
							INNER JOIN dbo.TOPSPTransferCeilingDetail b ON [b].TOPSPTransferCeilingID = [a].[ID]
							WHERE	1 = 1
									AND [a].[RowStatus] = 0
									AND b.[RowStatus] = 0
									AND [CreditAccount] = @CreditAccount
									AND [ProductCategoryID] = @ProductCategoryID
									AND [PaymentType] = @PaymentType
									AND (
										  b.[SparePartBillingID] = @SparePartBillingID
										  OR @SparePartBillingID IS NULL
										)
									AND (
										  b.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
										  OR @TOPSPTransferPaymentID IS NULL
										)
									AND ( a.[EffectiveDate] < @TransDateDetail )
									AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
									AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader) )
       
	   END
GO

/*
 exec sp_TransferCeiling 2, '100011',2,'2017.08.01','2017.08.31',1, 1
*/
CREATE PROCEDURE TOPSPTransferCeiling_BalanceUpdate
	   (
		 @ProductCategoryID INT = 2--0:all
		 ,
		 @CreditAccount VARCHAR(1000) = '100011'--'':all
		 ,
		 @PaymentType SMALLINT = 2--0:all
		 ,
		 @EffectiveDate DATETIME ,
		 @EndOfMotnh DATETIME = NULL ,
		 @BalanceBefore MONEY
		 
	   )
AS
	   BEGIN
			 SET NOCOUNT ON;
			 DECLARE @LastUpdatedBy VARCHAR(25)= 'WS-UPDATECEILING' 
			
			 UPDATE	dbo.TOPSPTransferCeiling
			 SET	[LastUpdatedBy] = @LastUpdatedBy ,
					[LastUpdatedTime] = GETDATE() ,
					[BalanceBefore] = @BalanceBefore
			 WHERE	[RowStatus] = 0
					AND [CreditAccount] = @CreditAccount
					AND [ProductCategoryID] = @ProductCategoryID
					AND [PaymentType] = @PaymentType
					AND [EffectiveDate] >= @EffectiveDate
					AND YEAR([EffectiveDate]) = YEAR(@EffectiveDate)
					AND MONTH([EffectiveDate]) = MONTH(@EffectiveDate)


			 UPDATE	dbo.[TOPSPTransferCeilingDetail]
			 SET	[RowStatus] = -1 ,
					[LastUpdatedBy] = @LastUpdatedBy ,
					[LastUpdatedTime] = GETDATE()
			 FROM	dbo.[TOPSPTransferCeilingDetail] a
			 INNER JOIN dbo.[TOPSPTransferCeiling] b ON [b].[ID] = [a].TOPSPTransferCeilingID
														AND a.[RowStatus] = 0
														AND b.[RowStatus] = 0
			 WHERE	1 = 1
					AND b.[CreditAccount] = @CreditAccount
					AND b.[ProductCategoryID] = @ProductCategoryID
					AND b.[PaymentType] = @PaymentType
					AND [EffectiveDate] >= @EffectiveDate
					AND YEAR([EffectiveDate]) = YEAR(@EffectiveDate)
					AND MONTH([EffectiveDate]) = MONTH(@EffectiveDate)

					 
	   END
GO

/*
 
 exec TransferCeilingDetail_BalanceUpdate @ProductCategoryID=2, @CreditAccount='100011', @PaymentType=2, @EffectiveDateHeader='2017/08/09',@Amount=5970000003, @IsIncome=0,@TransDateDetail='2017/01/27', @SparePartBillingID=321488 
*/
CREATE PROCEDURE TOPTransferCeilingDetail_BalanceUpdate
	   (
		 @ProductCategoryID INT = 2--0:all
		 ,
		 @CreditAccount VARCHAR(1000) = '100011'--'':all
		 ,
		 @PaymentType SMALLINT = 2--0:all
		 ,
		 @EffectiveDateHeader DATETIME = NULL ,
		 @Amount MONEY ,
		 @TransDateDetail DATETIME = NULL ,
		 @SparePartBillingID INT = NULL ,
		 @TOPSPTransferPaymentID INT = NULL ,
		 @IsIncome SMALLINT

		 	
	   )
AS
	   BEGIN
			 SET NOCOUNT ON;
			 DECLARE @LastUpdatedBy VARCHAR(25)= 'WS-UPDATECEILING' 
			 DECLARE @LastUpdatedTime DATETIME = GETDATE()
			 DECLARE @TransferCeilingiD INT = 0
			-- SELECT @EffectiveDateHeader, @TransDateDetail,31
		 
			 UPDATE	dbo.TOPSPTransferCeilingDetail
			 SET	[RowStatus] = -1 ,
					[LastUpdatedBy] = '[TCDetail_B]' ,
					[LastUpdatedTime] = @LastUpdatedTime
			 WHERE	ID IN ( SELECT	b.[ID]
							FROM	dbo.TOPSPTransferCeiling a
							INNER JOIN dbo.TOPSPTransferCeilingDetail b ON [b].TOPSPTransferCeilingID = [a].[ID]
							WHERE	1 = 1
									AND [a].[RowStatus] = 0
									AND b.[RowStatus] = 0
									AND [CreditAccount] = @CreditAccount
									AND [ProductCategoryID] = @ProductCategoryID
									AND [PaymentType] = @PaymentType
									AND (
										  b.[SparePartBillingID] = @SparePartBillingID
										  OR @SparePartBillingID IS NULL
										)
									AND (
										  b.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
										  OR @TOPSPTransferPaymentID IS NULL
										)
									AND ( a.[EffectiveDate] >= @TransDateDetail )
									AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
									AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader)
									AND YEAR([EffectiveDate]) = YEAR(@TransDateDetail)
									AND MONTH([EffectiveDate]) = MONTH(@TransDateDetail) );
					WITH	CTR
							  AS (
								   SELECT	*
								   FROM		dbo.TOPSPTransferCeiling a ( NOLOCK )
								   WHERE	1 = 1
											AND [a].[RowStatus] = 0
											AND [CreditAccount] = @CreditAccount
											AND [ProductCategoryID] = @ProductCategoryID
											AND [PaymentType] = @PaymentType
											AND [EffectiveDate] >= @EffectiveDateHeader
											AND ( [a].[EffectiveDate] >= @TransDateDetail )
											AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
											AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader)
								 ),
							CTDetail
							  AS (
								   SELECT	a.* ,
											ISNULL(@TOPSPTransferPaymentID, -1) TOPSPTransferPaymentID ,
											ISNULL(@SparePartBillingID, -1) SparePartBillingID ,
											@Amount Amount ,
											@IsIncome IsIncome
								   FROM		[CTR] a
								   WHERE	1 = 1
								 )
						 MERGE dbo.TOPSPTransferCeilingDetail AS a
						 USING CTDetail AS b
						 ON a.TOPSPTransferCeilingID = b.[ID]
							AND ISNULL(a.[SparePartBillingID], -1) = b.[SparePartBillingID]
							AND ISNULL(a.[TOPSPTransferPaymentID], -1) = b.[TOPSPTransferPaymentID]
						 WHEN MATCHED THEN
							UPDATE SET
								   [a].[LastUpdatedBy] = @LastUpdatedBy ,
								   a.[LastUpdatedTime] = @LastUpdatedTime ,
								   [a].[IsIncome] = @IsIncome ,
								   a.[Amount] = b.[Amount] ,
								   [a].[RowStatus] = 0
						 WHEN NOT MATCHED THEN
							INSERT (
									 TOPSPTransferCeilingID ,
									 [SparePartBillingID] ,
									 [TOPSPTransferPaymentID] ,
									 [Amount] ,
									 [IsIncome] ,
									 [Status] ,
									 [RowStatus] ,
									 [CreatedBy] ,
									 [CreatedTime] ,
									 [LastUpdatedBy] ,
									 [LastUpdatedTime]
								 
								   )
							VALUES (
									 b.[ID] , -- TransferCeilingID - int
									 @SparePartBillingID , -- SparePartBillingID - int
									 @TOPSPTransferPaymentID , -- TOPSPTransferPaymentID - int
									 @Amount , -- Amount - money
									 @IsIncome , -- IsIncome - smallint
									 0 , -- Status - smallint
									 0 , -- RowStatus - smallint
									 @LastUpdatedBy , -- CreatedBy - varchar(20)
									 GETDATE() , -- CreatedTime - datetime
									 @LastUpdatedBy , -- LastUpdatedBy - varchar(20)
									 GETDATE()  -- LastUpdatedTime - datetime
													
												
									
							
             					   ) ;
								

			 UPDATE	dbo.TOPSPTransferCeilingDetail
			 SET	[RowStatus] = -1 ,
					[LastUpdatedBy] = '[TCDetail_B]' ,
					[LastUpdatedTime] = @LastUpdatedTime
			 WHERE	ID IN ( SELECT	b.[ID]
							FROM	dbo.TOPSPTransferCeiling a
							INNER JOIN dbo.TOPSPTransferCeilingDetail b ON [b].TOPSPTransferCeilingID = [a].[ID]
							WHERE	1 = 1
									AND [a].[RowStatus] = 0
									AND b.[RowStatus] = 0
									AND [CreditAccount] = @CreditAccount
									AND [ProductCategoryID] = @ProductCategoryID
									AND [PaymentType] = @PaymentType
									AND (
										  b.[SparePartBillingID] = @SparePartBillingID
										  OR @SparePartBillingID IS NULL
										)
									AND (
										  b.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
										  OR @TOPSPTransferPaymentID IS NULL
										)
									AND ( a.[EffectiveDate] < @TransDateDetail )
									AND YEAR([EffectiveDate]) = YEAR(@EffectiveDateHeader)
									AND MONTH([EffectiveDate]) = MONTH(@EffectiveDateHeader) )
       
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteAPPayment @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[APPayment]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteAPPaymentDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[APPaymentDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteARReceipt @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[ARReceipt]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteARReceiptDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[ARReceiptDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteBusinessSectorDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[BusinessSectorDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteBusinessSectorHeader @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[BusinessSectorHeader]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteCarrosserieDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[CarrosserieDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteCarrosserieHeader @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[CarrosserieHeader]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteCustomerGroup @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[CustomerGroup]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteCustomerRequestOCR @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[CustomerRequestOCR]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteDealerSystems @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[DealerSystems]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteDMSWOWarrantyClaim @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[DMSWOWarrantyClaim]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteFleet @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[Fleet]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteFleetCustomer @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[FleetCustomer]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteFleetCustomerContact @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[FleetCustomerContact]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteFleetCustomerToCustomer @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[FleetCustomerToCustomer]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteFleetCustomerToDealer @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[FleetCustomerToDealer]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteIndustrialSector @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[IndustrialSector]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteInventoryTransaction @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[InventoryTransaction]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteInventoryTransactionDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[InventoryTransactionDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteInventoryTransfer @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[InventoryTransfer]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteInventoryTransferDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[InventoryTransferDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteKaroseri @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[Karoseri]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteLeasing @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[Leasing]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteMyAlertStatus @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[MyAlertStatus]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeletePODealer @DealerID INT OUTPUT
AS
	   DELETE	FROM [dbo].[VWI_PODealer]
	   WHERE	[DealerID] = @DealerID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeletePOOtherVendor @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[POOtherVendor]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeletePOOtherVendorDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[POOtherVendorDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionChassisMasterProfile @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionChassisMasterProfile]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionFaktur @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionFaktur]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionPaymentDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionPaymentDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionPaymentHeader @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionPaymentHeader]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionPrice @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionPrice]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteRevisionSAPDoc @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionSAPDoc]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionSPKFaktur @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionSPKFaktur]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_DeleteRevisionType @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[RevisionType]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartConversion @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartConversion]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartDeliveryOrder @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartDeliveryOrder]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartDeliveryOrderDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartDeliveryOrderDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartMasterTOP @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartMasterTOP]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartPOTypeTOP @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartPOTypeTOP]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartPRDetailFromVendor @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartPRDetailFromVendor]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartPRFromVendor @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartPRFromVendor]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartSalesOrder @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartSalesOrder]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSparePartSalesOrderDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SparePartSalesOrderDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteSPKChassis @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[SPKChassis]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteStandardCodeChar @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[StandardCodeChar]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTestCustomer @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TestCustomer]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTestStatus1 @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TestStatus1]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPBlockStatus @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPBlockStatus]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPCreditAccount @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPCreditAccount]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPDeposit @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPDeposit]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPDueDate @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPDueDate]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPTransferActual @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPTransferActual]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPTransferCeiling @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPTransferCeiling]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPTransferCeilingDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPTransferCeilingDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPTransferPayment @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPTransferPayment]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteTOPSPTransferPaymentDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[TOPSPTransferPaymentDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteVehiclePurchaseDetail @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[VehiclePurchaseDetail]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteVehiclePurchaseHeader @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[VehiclePurchaseHeader]
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_DeleteVWI_CampaignReport @ID INT OUTPUT
AS
	   DELETE	FROM [dbo].[VWI_CampaignReport]
	   WHERE	[ID] = @ID
GO

CREATE  PROCEDURE up_FleetCustomerToCustomer_InitDealer
	   @FleetCustomerID INT ,
	   @LastUpdateBy VARCHAR(50) = 'RECALCULATE'
AS
	   BEGIN  
			 SET NOCOUNT ON;  
			 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;  
      
  
			 DECLARE @Customer TABLE ( CustomerID INT )  
  
			 INSERT	INTO @Customer
					(
					  [CustomerID]  
					)
			 SELECT	a.[CustomerID]
			 FROM	[dbo].[FleetCustomerToCustomer] a
			 WHERE	1 = 1
					AND [a].[RowStatus] = 0
					AND a.[FleetCustomerID] = @FleetCustomerID
			 GROUP BY a.[CustomerID]  
  
         
  
  
			 INSERT	INTO [dbo].[FleetCustomerToDealer]
					(
					  [FleetCustomerID] ,
					  [DealerID] ,
					  [RowStatus] ,
					  [CreatedBy] ,
					  [CreatedTime] ,
					  [LastUpdatedBy] ,
					  [LastUpdatedTime]  
					)
			 SELECT	@FleetCustomerID , -- FleetCustomerID - int  
					a.[DealerID] , -- DealerID - smallint  
					0 , -- RowStatus - smallint  
					@LastUpdateBy , -- CreatedBy - varchar(50)  
					GETDATE() , -- CreatedTime - datetime  
					'' , -- LastUpdatedBy - varchar(50)  
					GETDATE()  -- LastUpdatedTime - datetime  
			 FROM	dbo.[CustomerDealer] a
			 INNER JOIN @Customer b ON [a].[NewCustomerID] = b.[CustomerID]
			 WHERE	1 = 1
					AND [a].[RowStatus] = 0
					AND a.[DealerID] NOT IN ( SELECT	[c].[DealerID]
											  FROM		FleetCustomerToDealer c
											  WHERE		c.[RowStatus] = 0
														AND c.[FleetCustomerID] = @FleetCustomerID )
			 GROUP BY a.[DealerID]  
      
     
  
	   END
GO

CREATE PROCEDURE up_GetTOPSPSparePartBillingID @SPBillingId INT
AS
	   BEGIN

			 SET NOCOUNT ON;

			 SELECT	d.*
			 FROM	TOPSPTransferPayment h ( NOLOCK )
			 JOIN	TOPSPTransferPaymentDetail d ( NOLOCK ) ON d.TOPSPTransferPaymentID = h.ID
			 JOIN	SparePartBilling s ( NOLOCK ) ON s.ID = d.SparePartBillingID
			 WHERE	h.RowStatus = 0
					AND d.RowStatus = 0
					AND s.ID = @SPBillingId
					AND h.Status NOT IN ( SELECT	ValueId
										  FROM		StandardCode
										  WHERE		Category = 'EnumTOPSPTransferPayment.Status'
													AND RowStatus = 0
													AND ValueDesc LIKE '%Batal%' ) 



	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertAPPayment
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentNo VARCHAR(50) ,
	   @APReferenceNo VARCHAR(100) ,
	   @APVoucherReferenceNo VARCHAR(100) ,
	   @AppliedToDocument MONEY ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @State SMALLINT ,
	   @TotalChangeAmount MONEY ,
	   @TotalPaymentAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @Type SMALLINT ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[APPayment]
	   VALUES	( @Owner, @APPaymentNo, @APReferenceNo, @APVoucherReferenceNo, @AppliedToDocument, @BU, @Cancelled,
				  @CashAndBank, @MethodOfPayment, @AvailableBalance, @State, @TotalChangeAmount, @TotalPaymentAmount,
				  @TransactionDate, @Type, @VendorDescription, @Vendor, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertAPPaymentDetail
	   @ID INT OUTPUT ,
	   @APPaymentID INT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentDetailNo VARCHAR(100) ,
	   @APPaymentNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @ExternalDocumentNo VARCHAR(50) ,
	   @ExternalDocumentType SMALLINT ,
	   @APVoucherNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNoNVSOReferral VARCHAR(100) ,
	   @OrderNoOutsourceWorkOrder VARCHAR(100) ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoUVSOReferral VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaymentAmount MONEY ,
	   @PaymentSlipNo VARCHAR(50) ,
	   @ReceiptFromVendor BIT ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[APPaymentDetail]
	   VALUES	( @APPaymentID, @Owner, @APPaymentDetailNo, @APPaymentNo, @BU, @ChangeAmount, @Description,
				  @DifferenceValue, @ExternalDocumentNo, @ExternalDocumentType, @APVoucherNo, @OrderDate,
				  @OrderNoNVSOReferral, @OrderNoOutsourceWorkOrder, @OrderNo, @OrderNoUVSOReferral, @OutstandingBalance,
				  @PaymentAmount, @PaymentSlipNo, @ReceiptFromVendor, @RemainingBalance, @SourceType,
				  @TransactionDocument, @Vendor, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertARReceipt
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @GeneratedToken VARCHAR(36) ,
	   @ARInvoiceReferenceNo VARCHAR(100) ,
	   @ARReceiptNo VARCHAR(50) ,
	   @ARReceiptReferenceNo VARCHAR(100) ,
	   @Type SMALLINT ,
	   @BookingFee BIT ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(100) ,
	   @EndOrderDate DATETIME ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @StartOrderDate DATETIME ,
	   @State SMALLINT ,
	   @AppliedToDocument MONEY ,
	   @TotalAmountBase MONEY ,
	   @TotalChangeAmount MONEY ,
	   @TotalOutstandingBalanceBase MONEY ,
	   @TotalReceiptAmount MONEY ,
	   @TotalRemainingBalanceBase MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[ARReceipt]
	   VALUES	( @Owner, @GeneratedToken, @ARInvoiceReferenceNo, @ARReceiptNo, @ARReceiptReferenceNo, @Type,
				  @BookingFee, @BU, @Cancelled, @CashAndBank, @Customer, @CustomerNo, @EndOrderDate, @MethodOfPayment,
				  @AvailableBalance, @StartOrderDate, @State, @AppliedToDocument, @TotalAmountBase, @TotalChangeAmount,
				  @TotalOutstandingBalanceBase, @TotalReceiptAmount, @TotalRemainingBalanceBase, @TransactionDate,
				  @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertARReceiptDetail
	   @ID INT OUTPUT ,
	   @ARReceiptID INT ,
	   @Owner VARCHAR(100) ,
	   @DetailNo VARCHAR(50) ,
	   @ARReceiptNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Customer VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @InvoiceNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoSO VARCHAR(100) ,
	   @OrderNoUVSO VARCHAR(100) ,
	   @OrderNoWO VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaidBackToCustomer BIT ,
	   @ReceiptAmount MONEY ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[ARReceiptDetail]
	   VALUES	( @ARReceiptID, @Owner, @DetailNo, @ARReceiptNo, @BU, @ChangeAmount, @Customer, @Description,
				  @DifferenceValue, @InvoiceNo, @OrderDate, @OrderNo, @OrderNoSO, @OrderNoUVSO, @OrderNoWO,
				  @OutstandingBalance, @PaidBackToCustomer, @ReceiptAmount, @RemainingBalance, @SourceType,
				  @TransactionDocument, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertBusinessSectorDetail
	   @ID INT OUTPUT ,
	   @BusinessSectorHeaderID INT ,
	   @BusinessDomain VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[BusinessSectorDetail]
	   VALUES	( @BusinessSectorHeaderID, @BusinessDomain, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertBusinessSectorHeader
	   @ID INT OUTPUT ,
	   @BusinessSectorName VARCHAR(500) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[BusinessSectorHeader]
	   VALUES	( @BusinessSectorName, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertCarrosserieDetail
	   @ID INT OUTPUT ,
	   @CarrosserieHeaderID INT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @AccessorriesDescription VARCHAR(100) ,
	   @AccessorriesName VARCHAR(100) ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @KITName VARCHAR(100) ,
	   @PBUCode VARCHAR(20) ,
	   @PBUName VARCHAR(100) ,
	   @PDIDetailName VARCHAR(100) ,
	   @PDIReceiptDetailNo VARCHAR(50) ,
	   @PDIReceiptName VARCHAR(100) ,
	   @ReceiveQuantity FLOAT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[CarrosserieDetail]
	   VALUES	( @CarrosserieHeaderID, @PDIStateCode, @PDIStatusCode, @AccessorriesDescription, @AccessorriesName,
				  @BUCode, @BUName, @KITName, @PBUCode, @PBUName, @PDIDetailName, @PDIReceiptDetailNo, @PDIReceiptName,
				  @ReceiveQuantity, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertCarrosserieHeader
	   @ID INT OUTPUT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @BUCode VARCHAR(50) ,
	   @BUName VARCHAR(100) ,
	   @PDIName VARCHAR(100) ,
	   @PDIReceiptNo VARCHAR(50) ,
	   @PDIReceiptRefName VARCHAR(100) ,
	   @PDIReceiptStatus SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @VendorName VARCHAR(100) ,
	   @ChassisNumber VARCHAR(20) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[CarrosserieHeader]
	   VALUES	( @PDIStateCode, @PDIStatusCode, @BUCode, @BUName, @PDIName, @PDIReceiptNo, @PDIReceiptRefName,
				  @PDIReceiptStatus, @TransactionDate, @TransactionType, @VendorName, @ChassisNumber, @RowStatus,
				  @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertCustomerGroup
	   @ID INT OUTPUT ,
	   @Code VARCHAR(20) ,
	   @Name VARCHAR(150) ,
	   @Description NVARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[CustomerGroup]
	   VALUES	( @Code, @Name, @Description, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertCustomerRequestOCR
	   @ID INT OUTPUT ,
	   @CustomerRequestID INT ,
	   @OCRIdentityID INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[CustomerRequestOCR]
	   VALUES	( @CustomerRequestID, @OCRIdentityID, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertCustomerRequestX
	   @ID INT OUTPUT ,
	   @RequestNo VARCHAR(50) ,
	   @RefRequestNo VARCHAR(50) ,
	   @RequestType VARCHAR(50) ,
	   @DealerID SMALLINT ,
	   @RequestUserID SMALLINT ,
	   @RequestDate SMALLDATETIME ,
	   @Status INT ,
	   @ProcessUserID VARCHAR(50) ,
	   @ProcessDate DATETIME ,
	   @CustomerCode VARCHAR(10) ,
	   @ReffCode VARCHAR(10) ,
	   @Name1 VARCHAR(50) ,
	   @Name2 VARCHAR(50) ,
	   @Name3 VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PreArea VARCHAR(20) ,
	   @CityID SMALLINT ,
	   @PrintRegion VARCHAR(1) ,
	   @PhoneNo VARCHAR(30) ,
	   @Email VARCHAR(50) ,
	   @Attachment VARCHAR(250) ,
	   @Status1 SMALLINT ,
	   @TipePerusahaan SMALLINT ,
	   @MCPStatus SMALLINT ,
	   @LKPPStatus SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[CustomerRequestX]
	   VALUES	( dbo.ufn_CreateReqCustomerNumber(@DealerId),--@RequestNo,
				  @RefRequestNo, @RequestType, @DealerId, @RequestUserID, @RequestDate, @Status, @ProcessUserID,
				  @ProcessDate, @CustomerCode, @ReffCode, @Name1, @Name2, @Name3, @Alamat, @Kelurahan, @Kecamatan,
				  @PostalCode, @PreArea, @CityID, @PrintRegion, @PhoneNo, @Email, @Attachment, @Status1, @TipePerusahaan,
				  @MCPStatus, @LKPPStatus, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertDealerSystems
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @SystemID INT ,
	   @isSPKMatchFaktur BIT ,
	   @isOnlyUploadPhotoTenagaPenjual BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[DealerSystems]
	   VALUES	( @DealerID, @SystemID, @isSPKMatchFaktur, @isOnlyUploadPhotoTenagaPenjual, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertDMSWOWarrantyClaim
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @DealerBranchID INT ,
	   @ChassisNumber VARCHAR(20) ,
	   @isBB BIT ,
	   @WorkOrderNumber VARCHAR(50) ,
	   @FailureDate DATETIME ,
	   @ServiceDate DATETIME ,
	   @Owner VARCHAR(50) ,
	   @Mileage INT ,
	   @ServiceBuletin VARCHAR(50) ,
	   @Symptoms VARCHAR(1000) ,
	   @Causes VARCHAR(1000) ,
	   @Results VARCHAR(1000) ,
	   @Notes VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreateBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[DMSWOWarrantyClaim]
	   VALUES	( @DealerID, @DealerBranchID, @ChassisNumber, @isBB, @WorkOrderNumber, @FailureDate, @ServiceDate,
				  @Owner, @Mileage, @ServiceBuletin, @Symptoms, @Causes, @Results, @Notes, @RowStatus, @CreateBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertFleet
	   @ID INT OUTPUT ,
	   @FleetCode VARCHAR(50) ,
	   @FleetName VARCHAR(100) ,
	   @FleetNickName VARCHAR(100) ,
	   @FleetGroup VARCHAR(100) ,
	   @Address VARCHAR(255) ,
	   @ProvinceId INT ,
	   @CityId SMALLINT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(50) ,
	   @BusinessSectorHeaderId INT ,
	   @BusinessSectorDetailId INT ,
	   @FleetNote VARCHAR(MAX) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[Fleet]
	   VALUES	( @FleetCode, @FleetName, @FleetNickName, @FleetGroup, @Address, @ProvinceId, @CityId, @IdentityType,
				  @IdentityNumber, @BusinessSectorHeaderId, @BusinessSectorDetailId, @FleetNote, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertFleetCustomer
	   @ID INT OUTPUT ,
	   @CustomerGroupID INT ,
	   @ProvinceID INT ,
	   @PreArea VARCHAR(50) ,
	   @CityID SMALLINT ,
	   @BusinessSectorDetailId INT ,
	   @RatioMatrixID INT ,
	   @CategoryIndex INT ,
	   @TypeIndex INT ,
	   @Code VARCHAR(30) ,
	   @Name VARCHAR(50) ,
	   @Gedung VARCHAR(50) ,
	   @Alamat VARCHAR(150) ,
	   @Kecamatan VARCHAR(75) ,
	   @Kelurahan VARCHAR(75) ,
	   @PostalCode VARCHAR(10) ,
	   @Email NVARCHAR(50) ,
	   @PhoneNo VARCHAR(15) ,
	   @TipeCustomer INT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(30) ,
	   @Attachment VARCHAR(100) ,
	   @ClassificationIndex INT ,
	   @FleetNickName VARCHAR(50) ,
	   @FleetNote VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   INSERT	INTO [dbo].[FleetCustomer]
	   VALUES	( @CustomerGroupID, @ProvinceID, @PreArea, @CityID, @BusinessSectorDetailId, @RatioMatrixID,
				  @CategoryIndex, @TypeIndex, @Code, --'F'+REPLACE(STR((SELECT COUNT(*) FROM FleetCustomer)+1, 5), SPACE(1), '0'),
				  @Name, @Gedung, @Alamat, @Kecamatan, @Kelurahan, @PostalCode, @Email, @PhoneNo, @TipeCustomer,
				  @IdentityType, @IdentityNumber, @Attachment, @ClassificationIndex, @FleetNickName, @FleetNote,
				  @RowStatus, @CreatedBy, GETDATE(), @LastUpdatedBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertFleetCustomerContact
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @Name VARCHAR(50) ,
	   @Position VARCHAR(50) ,
	   @PhoneNo VARCHAR(20) ,
	   @Handphone VARCHAR(20) ,
	   @Email NVARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   INSERT	INTO [dbo].[FleetCustomerContact]
	   VALUES	( @FleetCustomerID, @Name, @Position, @PhoneNo, @Handphone, @Email, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdatedBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertFleetCustomerToCustomer
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @CustomerID INT ,
	   @IsDefault BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   INSERT	INTO [dbo].[FleetCustomerToCustomer]
	   VALUES	( @FleetCustomerID, @CustomerID, @IsDefault, @RowStatus, @CreatedBy, GETDATE(), @LastUpdatedBy,
				  GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertFleetCustomerToDealer
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @DealerID SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   INSERT	INTO [dbo].[FleetCustomerToDealer]
	   VALUES	( @FleetCustomerID, @DealerID, @RowStatus, @CreatedBy, GETDATE(), @LastUpdatedBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertIndustrialSector
	   @ID INT OUTPUT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[IndustrialSector]
	   VALUES	( @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertInventoryTransaction
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @PersonInCharge VARCHAR(100) ,
	   @ProcessCode VARCHAR(10) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[InventoryTransaction]
	   VALUES	( @Owner, @DealerCode, @InventoryTransactionNo, @InventoryTransferNo, @PersonInCharge, @ProcessCode,
				  @SourceData, @State, @TransactionDate, @TransactionType, @WONo, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertInventoryTransactionDetail
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @FromBU VARCHAR(100) ,
	   @InventoryTransactionID INT ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferDetail VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @ReasonCode VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @RegisterSerialNumber VARCHAR(100) ,
	   @RunningNumber INT ,
	   @SerialNo VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @SourceData VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @TotalCost MONEY ,
	   @TransactionType VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @UnitCost MONEY ,
	   @VIN VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[InventoryTransactionDetail]
	   VALUES	( @Owner, @BaseQuantity, @BatchNo, @BU, @Department, @Description, @FromBU, @InventoryTransactionID,
				  @InventoryTransactionNo, @InventoryTransferDetail, @InventoryUnit, @Location, @ProductCrossReference,
				  @ProductDescription, @Product, @Quantity, @ReasonCode, @ReferenceNo, @RegisterSerialNumber,
				  @RunningNumber, @SerialNo, @ServicePartsAndMaterial, @Site, @SourceData, @StockNumber, @StockNumberNV,
				  @TotalCost, @TransactionType, @TransactionUnit, @UnitCost, @VIN, @Warehouse, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertInventoryTransfer
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(50) ,
	   @ItemTypeForTransfer SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @ReceiptDate DATETIME ,
	   @ReceiptNo VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @SearchVehicle VARCHAR(50) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @ToDealer VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @TransferStatus SMALLINT ,
	   @TransferStep BIT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[InventoryTransfer]
	   VALUES	( @Owner, @FromDealer, @FromSite, @InventoryTransferNo, @ItemTypeForTransfer, @PersonInCharge,
				  @ReceiptDate, @ReceiptNo, @ReferenceNo, @SearchVehicle, @SourceData, @State, @ToDealer, @ToSite,
				  @TransactionDate, @TransactionType, @TransferStatus, @TransferStep, @WONo, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertInventoryTransferDetail
	   @ID INT OUTPUT ,
	   @InventoryTransferID INT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @ConsumptionTaxIn VARCHAR(100) ,
	   @ConsumptionTaxOut VARCHAR(100) ,
	   @FromBatchNo VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromConfiguration VARCHAR(100) ,
	   @FromExteriorColor VARCHAR(100) ,
	   @FromInteriorColor VARCHAR(100) ,
	   @FromLocation VARCHAR(100) ,
	   @FromSerialNo VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @FromStyle VARCHAR(100) ,
	   @FromWarehouse VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @Remarks VARCHAR(100) ,
	   @ServicePartsandMaterial VARCHAR(100) ,
	   @SourceData VARCHAR(50) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @StockNumberLookupName VARCHAR(200) ,
	   @StockNumberLookupType INT ,
	   @ToBatchNo VARCHAR(100) ,
	   @ToDealer VARCHAR(100) ,
	   @ToConfiguration VARCHAR(100) ,
	   @ToExteriorColor VARCHAR(100) ,
	   @ToInteriorColor VARCHAR(100) ,
	   @ToLocation VARCHAR(100) ,
	   @ToSerialNo VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @ToStyle VARCHAR(100) ,
	   @ToWarehouse VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @VIN VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[InventoryTransferDetail]
	   VALUES	( @InventoryTransferID, @Owner, @BaseQuantity, @ConsumptionTaxIn, @ConsumptionTaxOut, @FromBatchNo,
				  @FromDealer, @FromConfiguration, @FromExteriorColor, @FromInteriorColor, @FromLocation, @FromSerialNo,
				  @FromSite, @FromStyle, @FromWarehouse, @InventoryTransferNo, @InventoryUnit, @ProductDescription,
				  @Product, @Quantity, @Remarks, @ServicePartsandMaterial, @SourceData, @StockNumber, @StockNumberNV,
				  @StockNumberLookupName, @StockNumberLookupType, @ToBatchNo, @ToDealer, @ToConfiguration,
				  @ToExteriorColor, @ToInteriorColor, @ToLocation, @ToSerialNo, @ToSite, @ToStyle, @ToWarehouse,
				  @TransactionUnit, @VIN, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertKaroseri
	   @ID INT OUTPUT ,
	   @Code VARCHAR(16) ,
	   @Name VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[Karoseri]
	   VALUES	( @Code, @Name, @City, @Alamat, @Kelurahan, @Kecamatan, @Province, @PostalCode, @PhoneNo, @Fax, @WebSite,
				  @Email, @ContactPerson, @HP, @Status, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertLeasing
	   @ID INT OUTPUT ,
	   @LeasingGroupName VARCHAR(50) ,
	   @LeasingCode VARCHAR(16) ,
	   @LeasingName VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[Leasing]
	   VALUES	( @LeasingGroupName, @LeasingCode, @LeasingName, @City, @Alamat, @Kelurahan, @Kecamatan, @Province,
				  @PostalCode, @PhoneNo, @Fax, @WebSite, @Email, @ContactPerson, @HP, @Status, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )
	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertMyAlertStatus
	   @ID INT OUTPUT ,
	   @AlertMasterID SMALLINT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy NCHAR(10) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[MyAlertStatus]
	   VALUES	( @AlertMasterID, @Status, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertPOOtherVendor
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @AllocationPeriod VARCHAR(100) ,
	   @Balance MONEY ,
	   @DealerCode VARCHAR(100) ,
	   @City VARCHAR(100) ,
	   @CloseRespon VARCHAR(100) ,
	   @Country VARCHAR(100) ,
	   @DeliveryMethod SMALLINT ,
	   @Description VARCHAR(100) ,
	   @DownPayment MONEY ,
	   @DownPaymentAmountPaid MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @EventDate VARCHAR(100) ,
	   @ExternalDocNo VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @GrandTotal MONEY ,
	   @PaymentGroup SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @PostalCode VARCHAR(100) ,
	   @Priority SMALLINT ,
	   @Province VARCHAR(100) ,
	   @PRPOType VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @SONo VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @State SMALLINT ,
	   @StockReferenceNo VARCHAR(100) ,
	   @Taxable SMALLINT ,
	   @TermsOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalTitleRegistrationFee MONEY ,
	   @PurchaseOrderDate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[POOtherVendor]
	   VALUES	( @Owner, @Address1, @Address2, @Address3, @AllocationPeriod, @Balance, @DealerCode, @City, @CloseRespon,
				  @Country, @DeliveryMethod, @Description, @DownPayment, @DownPaymentAmountPaid, @DownPaymentIsPaid,
				  @EventDate, @ExternalDocNo, @FormSource, @GrandTotal, @PaymentGroup, @PersonInCharge, @PostalCode,
				  @Priority, @Province, @PRPOType, @PurchaseOrderNo, @SONo, @Site, @State, @StockReferenceNo, @Taxable,
				  @TermsOfPayment, @TotalAmountBeforeDiscount, @TotalBaseAmount, @TotalConsumptionTaxAmount,
				  @TotalDiscountAmount, @TotalTitleRegistrationFee, @PurchaseOrderDate, @VendorDescription, @Vendor,
				  @Warehouse, @WONo, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertPOOtherVendorDetail
	   @ID INT OUTPUT ,
	   @POOtherVendorID INT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @EventData VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @BaseQtyOrder FLOAT ,
	   @BaseQtyReceipt FLOAT ,
	   @BaseQtyReturn FLOAT ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductSubstitute VARCHAR(100) ,
	   @ProductVariant VARCHAR(100) ,
	   @ProductVolume FLOAT ,
	   @ProductWeight FLOAT ,
	   @PromisedDate DATETIME ,
	   @PurchaseFor SMALLINT ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @PurchaseRequisitionDetail VARCHAR(100) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @ReferenceNo VARCHAR(100) ,
	   @RequiredDate DATETIME ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume FLOAT ,
	   @TotalWeight FLOAT ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[POOtherVendorDetail]
	   VALUES	( @POOtherVendorID, @Owner, @DealerCode, @CloseLine, @CloseReason, @Completed, @ConsumptionTax1Amount,
				  @ConsumptionTax1, @ConsumptionTax2Amount, @ConsumptionTax2, @Department, @Description, @DiscountAmount,
				  @DiscountPercentage, @EventData, @FormSource, @BaseQtyOrder, @BaseQtyReceipt, @BaseQtyReturn,
				  @InventoryUnit, @ProductCrossReference, @ProductDescription, @Product, @ProductSubstitute,
				  @ProductVariant, @ProductVolume, @ProductWeight, @PromisedDate, @PurchaseFor, @PurchaseOrderNo,
				  @PurchaseRequisitionDetail, @PurchaseUnit, @QtyOrder, @QtyReceipt, @QtyReturn, @RecallProduct,
				  @ReferenceNo, @RequiredDate, @SalesOrderDetail, @ScheduledShippingDate, @ServicePartsAndMaterial,
				  @ShippingDate, @Site, @StockNumber, @TitleRegistrationFee, @TotalAmount, @TotalAmountBeforeDiscount,
				  @TotalBaseAmount, @TotalConsumptionTaxAmount, @TotalVolume, @TotalWeight, @TransactionAmount,
				  @UnitCost, @Warehouse, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionChassisMasterProfile
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @ProfileHeaderID TINYINT ,
	   @GroupID TINYINT ,
	   @ProfileValue VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionChassisMasterProfile]
	   VALUES	( @ChassisMasterID, @EndCustomerID, @ProfileHeaderID, @GroupID, @ProfileValue, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )
	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionFaktur
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @OldEndCustomerID INT ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionStatus SMALLINT ,
	   @RevisionTypeID SMALLINT ,
	   @IsPay SMALLINT ,
	   @NewValidationDate DATETIME ,
	   @NewValidationBy VARCHAR(20) ,
	   @NewConfirmationDate DATETIME ,
	   @NewConfirmationBy VARCHAR(20) ,
	   @Remark VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionFaktur]
	   VALUES	( @ChassisMasterID, @EndCustomerID, @OldEndCustomerID, @RegNumber, @RevisionStatus, @RevisionTypeID,
				  @IsPay, @NewValidationDate, @NewValidationBy, @NewConfirmationDate, @NewConfirmationBy, @Remark,
				  @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )
	
	   SET @ID = @@IDENTITY

--CREATE Autonumber @RegNumber/Nomor Pengajuan
	   DECLARE @SeqNum INT

	   SELECT	@SeqNum = MAX(RIGHT(RegNumber, 6))
	   FROM		RevisionFaktur
	   WHERE	'20' + SUBSTRING(RegNumber, 3, 2) = YEAR(GETDATE())

	   SET @SeqNum = ISNULL(@SeqNum, 0) + 1
	   SELECT	@RegNumber = 'RF' + RIGHT(CONVERT(CHAR(4), YEAR(GETDATE())), 2) + REPLICATE('0', 6 - LEN(@SeqNum))
				+ CONVERT(VARCHAR(6), @SeqNum)

	   UPDATE	RevisionFaktur
	   SET		RegNumber = @RegNumber
	   WHERE	ID = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionPaymentDetail
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @RevisionPaymentHeaderID INT ,
	   @RevisionSAPDocID INT ,
	   @IsCancel SMALLINT ,
	   @CancelReason VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionPaymentDetail]
	   VALUES	( @RevisionFakturID, @RevisionPaymentHeaderID, @RevisionSAPDocID, @IsCancel, @CancelReason, @RowStatus,
				  @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionPaymentHeader
	   @ID INT OUTPUT ,
	   @DealerID INT ,
	   @PaymentType VARCHAR(3) ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionPaymentDocID INT ,
	   @SlipNumber VARCHAR(20) ,
	   @TotalAmount MONEY ,
	   @Status SMALLINT ,
	   @EvidencePath VARCHAR(150) ,
	   @ActualPaymentDate DATETIME ,
	   @ActualPaymentAmount MONEY ,
	   @AccDocNumber VARCHAR(30) ,
	   @GyroDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS --CREATE Autonumber Nomor Pembayaran
	   DECLARE @SeqNum INT ,
			   @DealerCode AS CHAR(5)

	   SELECT	@DealerCode = RIGHT(RTRIM(DealerCode), 5)
	   FROM		Dealer
	   WHERE	ID = @DealerID

	   SELECT	@SeqNum = MAX(RIGHT(RegNumber, 4))
	   FROM		RevisionPaymentHeader WITH ( NOLOCK )
	   WHERE	'20' + SUBSTRING(RegNumber, 7, 2) = YEAR(GETDATE())

	   SET @SeqNum = ISNULL(@SeqNum, 0) + 1
	   SELECT	@RegNumber = '3' + @DealerCode + RIGHT(CONVERT(CHAR(4), YEAR(GETDATE())), 2) + REPLICATE('0',
																										4 - LEN(@SeqNum))
				+ CONVERT(VARCHAR(4), @SeqNum)

	   INSERT	INTO [dbo].[RevisionPaymentHeader]
	   VALUES	( @DealerID, @PaymentType, @RegNumber, @RevisionPaymentDocID, @SlipNumber, @TotalAmount, @Status,
				  @EvidencePath, @ActualPaymentDate, @ActualPaymentAmount, @AccDocNumber, @GyroDate, @RowStatus,
				  @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionPrice
	   @ID INT OUTPUT ,
	   @CategoryID INT ,
	   @RevisionTypeID INT ,
	   @Amount MONEY ,
	   @ValidFrom SMALLDATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionPrice]
	   VALUES	( @CategoryID, @RevisionTypeID, @Amount, @ValidFrom, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE() )	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionSAPDoc
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @DebitChargeNo VARCHAR(10) ,
	   @DCAmount MONEY ,
	   @DebitMemoNo VARCHAR(15) ,
	   @DMAmount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionSAPDoc]
	   VALUES	( @RevisionFakturID, @DebitChargeNo, @DCAmount, @DebitMemoNo, @DMAmount, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )
	   SET @ID = @@IDENTITY


-----------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveRevisionSAPDoc]    Script Date: 19/09/2018 16:56:43 ******/
	   SET ANSI_NULLS OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionSPKFaktur
	   @ID INT OUTPUT ,
	   @SPKHeaderID INT ,
	   @EndCustomerID INT ,
	   @RowStatus SMALLINT ,
	--@CreatedTime datetime,
	   @CreatedBy VARCHAR(20) ,
	--@LastUpdateTime datetime,
	   @LastUpdateBy VARCHAR(20)
AS
	   INSERT	INTO [dbo].[RevisionSPKFaktur]
	   VALUES	( @SPKHeaderID, @EndCustomerID, @RowStatus, GETDATE(), @CreatedBy, GETDATE(), @LastUpdateBy )
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_InsertRevisionType
	   @ID INT OUTPUT ,
	   @Description VARCHAR(100) ,
	   @RevisionCode VARCHAR(5) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[RevisionType]
	   VALUES	( @Description, @RevisionCode, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartConversion
	   @ID INT OUTPUT ,
	   @SparePartMasterID INT ,
	   @UoMto VARCHAR(18) ,
	   @Qty INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartConversion]
	   VALUES	( @SparePartMasterID, @UoMto, @Qty, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartDeliveryOrder
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @Address4 VARCHAR(100) ,
	   @BusinessPhone VARCHAR(60) ,
	   @BU VARCHAR(100) ,
	   @CancellationDate DATETIME ,
	   @City VARCHAR(100) ,
	   @CustomerContacts VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DeliveryAddress VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(50) ,
	   @DeliveryType INT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Status SMALLINT ,
	   @MethodofPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @Salesperson VARCHAR(100) ,
	   @State SMALLINT ,
	   @TermofPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalMiscChargeBaseAmount MONEY ,
	   @TotalMiscChargeConsumptionTaxAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartDeliveryOrder]
	   VALUES	( @Owner, @Address1, @Address2, @Address3, @Address4, @BusinessPhone, @BU, @CancellationDate, @City,
				  @CustomerContacts, @Customer, @CustomerNo, @DeliveryAddress, @DeliveryOrderNo, @DeliveryType,
				  @ExternalReferenceNo, @GrandTotal, @Status, @MethodofPayment, @OrderType, @ReferenceNo, @Salesperson,
				  @State, @TermofPayment, @TotalAmountBeforeDiscount, @TotalBaseAmount, @TotalDiscountAmount,
				  @TotalMiscChargeBaseAmount, @TotalMiscChargeConsumptionTaxAmount, @TotalReceipt,
				  @TotalConsumptionTaxAmount, @TransactionDate, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartDeliveryOrderDetail
	   @ID INT OUTPUT ,
	   @SparePartDeliveryOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @BaseQtyDelivered FLOAT ,
	   @BaseQtyOrder FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DeliveryOrderDetail VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountBaseAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @RunningNumber INT ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartDeliveryOrderDetail]
	   VALUES	( @SparePartDeliveryOrderID, @Owner, @AmountBeforeDiscount, @BaseAmount, @BaseQtyDelivered,
				  @BaseQtyOrder, @BatchNo, @BU, @ConsumptionTax1Amount, @ConsumptionTax1, @ConsumptionTax2Amount,
				  @ConsumptionTax2, @DeliveryOrderDetail, @DeliveryOrderNo, @DiscountAmount, @DiscountBaseAmount,
				  @DiscountPercentage, @Location, @ProductCrossReference, @ProductDescription, @Product, @PromiseDate,
				  @QtyDelivered, @QtyOrder, @RequestDate, @RunningNumber, @SalesOrderDetail, @SalesUnit, @Site,
				  @TotalAmount, @TotalConsumptionTaxAmount, @TransactionAmount, @UnitPrice, @Warehouse, @RowStatus,
				  @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartMasterTOP
	   @ID INT OUTPUT ,
	   @SparePartPOTypeTOPID INT ,
	   @SparePartMasterID INT ,
	   @Status BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartMasterTOP]
	   VALUES	( @SparePartPOTypeTOPID, @SparePartMasterID, @Status, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartMasterTOP_WS
	   @SparePartMasterID INT ,
	   @CreatedBy VARCHAR(20)
AS
	   BEGIN



    ;
			 WITH	CTE_DATA
					  AS (
						   SELECT	a.ID SPID ,
									b.ID SPTID ,
									b.IsTOP
						   FROM		dbo.SparePartMaster a
						   CROSS JOIN SparePartPOTypeTOP b
						   WHERE	1 = 1
									AND a.ID = @SparePartMasterID
									AND b.RowStatus = 0
						 )
				  MERGE dbo.SparePartMasterTOP AS T
				  USING CTE_DATA AS S
				  ON T.SparePartPOTypeTOPID = S.SPTID
					AND T.SparePartMasterID = S.SPID
					AND T.RowStatus = 0
				  WHEN MATCHED THEN
					UPDATE SET [Status] = S.IsTOP ,
							  LastUpdateBy = @CreatedBy ,
							  LastUpdateTime = GETDATE()
				  WHEN NOT MATCHED THEN
					INSERT (
							 SparePartPOTypeTOPID ,
							 SparePartMasterID ,
							 [Status] ,
							 RowStatus ,
							 CreatedBy ,
							 CreatedTime ,
							 LastUpdateBy ,
							 LastUpdateTime
						   )
					VALUES (
							 S.SPTID , -- SparePartPOTypeTOPID - int
							 S.SPID , -- SparePartMasterID - int
							 S.IsTOP , -- Status - bit
							 0 , -- RowStatus - smallint
							 @CreatedBy , -- CreatedBy - varchar(20)
							 GETDATE() , -- CreatedTime - datetime
							 '' , -- LastUpdateBy - varchar(20)
							 GETDATE()  -- LastUpdateTime - datetime
						   ) ;

	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartPOTypeTOP
	   @ID INT OUTPUT ,
	   @SparePartPOType VARCHAR(5) ,
	   @IsTOP BIT ,
	   @TermOfPaymentIDNotTOP INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartPOTypeTOP]
	   VALUES	( @SparePartPOType, @IsTOP, @TermOfPaymentIDNotTOP, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE() )

	
	   SET @ID = @@IDENTITY

/****** Object:  StoredProcedure [dbo].[up_RetrieveSparePartPOTypeTOP]    Script Date: 06/10/2018 15:46:36 ******/
	   SET ANSI_NULLS OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartPRDetailFromVendor
	   @ID INT OUTPUT ,
	   @PRDetailNumber VARCHAR(50) ,
	   @SparePartPRID INT ,
	   @PRNumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @BaseReceivedQuantity DECIMAL(18, 9) ,
	   @BatchNumber VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @EngineNumber VARCHAR(50) ,
	   @EventData VARCHAR(1000) ,
	   @InventoryUnit VARCHAR(100) ,
	   @KeyNumber VARCHAR(50) ,
	   @LandedCost MONEY ,
	   @Location VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductVolume DECIMAL(18, 9) ,
	   @ProductWeight DECIMAL(18, 9) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @ReceivedQuantity DECIMAL(18, 9) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @ReturnPRDetail VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume DECIMAL(18, 9) ,
	   @TotalWeight DECIMAL(18, 9) ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartPRDetailFromVendor]
	   VALUES	( @PRDetailNumber, @SparePartPRID, @PRNumber, @Owner, @BaseReceivedQuantity, @BatchNumber, @DealerCode,
				  @ChassisModel, @ChassisNumberRegister, @ConsumptionTax1Amount, @ConsumptionTax1,
				  @ConsumptionTax2Amount, @ConsumptionTax2, @DiscountAmount, @EngineNumber, @EventData, @InventoryUnit,
				  @KeyNumber, @LandedCost, @Location, @ProductDescription, @Product, @ProductVolume, @ProductWeight,
				  @PurchaseUnit, @ReceivedQuantity, @ReferenceNumber, @ReturnPRDetail, @ServicePartsAndMaterial, @Site,
				  @StockNumber, @TitleRegistrationFee, @TotalAmount, @TotalBaseAmount, @TotalConsumptionTaxAmount,
				  @TotalVolume, @TotalWeight, @TransactionAmount, @UnitCost, @Warehouse, @RowStatus, @CreatedBy,
				  GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartPRFromVendor
	   @ID INT OUTPUT ,
	   @PRNumber NVARCHAR(50) ,
	   @PONumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @APVoucherNumber VARCHAR(100) ,
	   @AssignLandedCost BIT ,
	   @AutoInvoiced BIT ,
	   @DealerCode VARCHAR(100) ,
	   @DeliveryOrderDate DATETIME ,
	   @DeliveryOrderNumber VARCHAR(50) ,
	   @EventData VARCHAR(4000) ,
	   @EventData2 TEXT ,
	   @GrandTotal MONEY ,
	   @Handling VARCHAR(100) ,
	   @LoadData BIT ,
	   @PackingSlipDate DATETIME ,
	   @PackingSlipNumber VARCHAR(50) ,
	   @PRReferenceRequired BIT ,
	   @ReturnPRNumber VARCHAR(100) ,
	   @State VARCHAR(100) ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTax1Amount MONEY ,
	   @TotalConsumptionTax2Amount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalTitleRegistrationFree MONEY ,
	   @TransactionDate DATETIME ,
	   @TransferOrderRequestingNumber VARCHAR(100) ,
	   @Type VARCHAR(100) ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @VendorInvoiceNumber VARCHAR(50) ,
	   @WONumber VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartPRFromVendor]
	   VALUES	( @PRNumber, @PONumber, @Owner, @APVoucherNumber, @AssignLandedCost, @AutoInvoiced, @DealerCode,
				  @DeliveryOrderDate, @DeliveryOrderNumber, @EventData, @EventData2, @GrandTotal, @Handling, @LoadData,
				  @PackingSlipDate, @PackingSlipNumber, @PRReferenceRequired, @ReturnPRNumber, @State, @TotalBaseAmount,
				  @TotalConsumptionTax1Amount, @TotalConsumptionTax2Amount, @TotalConsumptionTaxAmount,
				  @TotalTitleRegistrationFree, @TransactionDate, @TransferOrderRequestingNumber, @Type,
				  @VendorDescription, @Vendor, @VendorInvoiceNumber, @WONumber, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartSalesOrder
	   @ID INT OUTPUT ,
	   @SalesChannel SMALLINT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @DealerCode VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DownPaymentAmount MONEY ,
	   @DownPaymentAmountReceived MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Handling SMALLINT ,
	   @MethodOfPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @SalesOrderNo VARCHAR(50) ,
	   @SalesPerson VARCHAR(100) ,
	   @ShipmentType VARCHAR(50) ,
	   @State VARCHAR(50) ,
	   @TermOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartSalesOrder]
	   VALUES	( @SalesChannel, @Owner, @Status, @DealerCode, @Customer, @CustomerNo, @DownPaymentAmount,
				  @DownPaymentAmountReceived, @DownPaymentIsPaid, @ExternalReferenceNo, @GrandTotal, @Handling,
				  @MethodOfPayment, @OrderType, @SalesOrderNo, @SalesPerson, @ShipmentType, @State, @TermOfPayment,
				  @TotalAmountBeforeDiscount, @TotalBaseAmount, @TotalConsumptionTaxAmount, @TotalDiscountAmount,
				  @TotalReceipt, @TransactionDate, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSparePartSalesOrderDetail
	   @ID INT OUTPUT ,
	   @SparePartSalesOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @KodeDealer VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentAge DECIMAL(18, 9) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @SalesOrderDetailID VARCHAR(50) ,
	   @SalesOrderNo VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SparePartSalesOrderDetail]
	   VALUES	( @SparePartSalesOrderID, @Owner, @Status, @AmountBeforeDiscount, @BaseAmount, @KodeDealer,
				  @ConsumptionTax1Amount, @ConsumptionTax1, @ConsumptionTax2Amount, @ConsumptionTax2, @DiscountAmount,
				  @DiscountPercentAge, @ProductCrossReference, @ProductDescription, @Product, @PromiseDate,
				  @QtyDelivered, @QtyOrder, @RequestDate, @SalesOrderDetailID, @SalesOrderNo, @SalesUnit, @Site,
				  @TotalAmount, @TotalConsumptionTaxAmount, @TransactionAmount, @UnitPrice, @Warehouse, @RowStatus,
				  @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertSPKChassis
	   @ID INT OUTPUT ,
	   @SPKDetailID INT ,
	   @ChassisMasterID INT ,
	   @MatchingType SMALLINT ,
	   @MatchingDate DATETIME ,
	   @MatchingNumber VARCHAR(50) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @KeyNumber VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[SPKChassis]
	   VALUES	( @SPKDetailID, @ChassisMasterID, @MatchingType, @MatchingDate, @MatchingNumber, @ReferenceNumber,
				  @KeyNumber, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertStandardCodeChar
	   @ID INT OUTPUT ,
	   @Category VARCHAR(100) ,
	   @ValueId VARCHAR(5) ,
	   @ValueCode VARCHAR(200) = '' ,
	   @ValueDesc VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20) ,
	--@LastUpdateTime datetime,
	   @Sequence INT
AS
	   INSERT	INTO [dbo].[StandardCodeChar]
	   VALUES	( @Category, @ValueId, @ValueCode, @ValueDesc, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy,
				  GETDATE(), @Sequence )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTestCustomer
	   @ID INT OUTPUT ,
	   @CustomerCode VARCHAR(10) ,
	   @CustomerName VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TestCustomer]
	   VALUES	( @CustomerCode, @CustomerName, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTestStatus1
	   @ID INT OUTPUT ,
	   @Name VARCHAR(50) ,
	   @Description VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TestStatus1]
	   VALUES	( @Name, @Description, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPBlockStatus
	   @ID INT OUTPUT ,
	   @SparePartPOStatusID INT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPBlockStatus]
	   VALUES	( @SparePartPOStatusID, @Status, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPCreditAccount
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @TermOfPaymentID INT ,
	   @KelipatanPembayaran INT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPCreditAccount]
	   VALUES	( @DealerID, @TermOfPaymentID, @KelipatanPembayaran, @Status, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPDeposit
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @AmountC2 MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPSPDeposit]
	   VALUES	( @SparePartBillingID, @AmountC2, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPDueDate
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @DueDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPSPDueDate]
	   VALUES	( @SparePartBillingID, @DueDate, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPTransferActual
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @RefTransferBank VARCHAR(100) ,
	   @Amount MONEY ,
	   @PostingDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPSPTransferActual]
	   VALUES	( @TOPSPTransferPaymentID, @RefTransferBank, @Amount, @PostingDate, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPTransferCeiling
	   @ID INT OUTPUT ,
	   @CreditAccount VARCHAR(20) ,
	   @ProductCategoryID SMALLINT ,
	   @PaymentType SMALLINT ,
	   @EffectiveDate DATETIME ,
	   @BalanceBefore MONEY ,
	   @AvailableCeiling MONEY ,
	   @LastSyncDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(20) ,
	   @LastUpdatedTime DATETIME
AS
	   BEGIN

 
			 DECLARE @TCID INT

			 SELECT	@TCID = MAX(tc.ID)
			 FROM	TOPSPTransferCeiling tc WITH ( NOLOCK )
			 WHERE	1 = 1
					AND tc.RowStatus = 0
					AND tc.CreditAccount = @CreditAccount
					AND tc.ProductCategoryID = @ProductCategoryID
					AND tc.PaymentType = @PaymentType
					AND tc.EffectiveDate = @EffectiveDate 
	
			 IF ( ISNULL(@TCID, 0) > 0 )
				BEGIN
	--create table Aaa(ID int identity(1,1), TransDate datetime, Remark varchar(20))
	--insert into Aaa select getdate(),'Update'
					  SET @ID = @TCID
					  EXEC dbo.up_UpdateTOPSPTransferCeiling
						@ID OUTPUT , --int OUTPUT,
						@CreditAccount , --varchar(20),
						@ProductCategoryID , -- int,
						@PaymentType , --smallint,
						@EffectiveDate , --datetime,
						@BalanceBefore , --money,
						@AvailableCeiling , --money,
						@LastSyncDate , --datetime,
						@RowStatus , --smallint,
						@CreatedBy , --varchar(20),
		--@CreatedTime datetime,
						@CreatedBy--, --@LastUpdatedBy,-- varchar(20),
                 --   @LastUpdatedTime-- datetime
				END 
			 ELSE
				BEGIN	
	--insert into Aaa select getdate(),'Insert'
	
					  INSERT	INTO [dbo].[TOPSPTransferCeiling]
					  VALUES	( @CreditAccount, @ProductCategoryID, @PaymentType, @EffectiveDate, @BalanceBefore,
								  @AvailableCeiling, @LastSyncDate, @RowStatus, @CreatedBy, GETDATE(), @LastUpdatedBy,
								  @LastUpdatedTime )

		
					  SET @ID = @@IDENTITY
				END

	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPTransferCeilingDetail
	   @ID INT OUTPUT ,
	   @TOPSPTransferCeilingID INT ,
	   @SparepartBillingID INT ,
	   @TOPSPTransferPaymentID INT ,
	   @Amount MONEY ,
	   @IsIncome SMALLINT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(20) ,
	   @LastUpdatedTime DATETIME
AS
	   INSERT	INTO [dbo].[TOPSPTransferCeilingDetail]
	   VALUES	( @TOPSPTransferCeilingID, @SparepartBillingID, @TOPSPTransferPaymentID, @Amount, @IsIncome, @Status,
				  @RowStatus, @CreatedBy, GETDATE(), @LastUpdatedBy, @LastUpdatedTime )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPTransferPayment
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @CreditAccount VARCHAR(6) ,
	   @RegNumber VARCHAR(15) OUTPUT ,
	   @DueDate DATETIME ,
	   @PaymentPurposeID TINYINT ,
	   @TransferPlanDate DATETIME ,
	   @TOPSPTransferPaymentIDReff INT ,
	   @IsAccelerated SMALLINT = 0 ,
	   @bankID INTEGER ,
	   @Status SMALLINT ,
	   @ValidatedBy VARCHAR(20) = '' ,
	   @ValidatedTime DATETIME = '1753-01-01 00:00:00.000' ,
	   @ConfirmedBy VARCHAR(20) = '' ,
	   @ConfirmedTime DATETIME = '1753-01-01 00:00:00.000' ,
	   @CanceledBy VARCHAR(20) = '' ,
	   @CanceledTime DATETIME = '1753-01-01 00:00:00.000' ,
	   @TransferAmount MONEY ,
	   @TransferActualDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20) --@LastUpdateTime datetime
AS
	   DECLARE @dealercode VARCHAR(10)= ''
	   DECLARE @year AS VARCHAR(4)= ''

	   SELECT	@dealercode = STUFF(CreditAccount, CHARINDEX(LEFT(CreditAccount, 1), CreditAccount), 1, '2') ,
				@year = RIGHT(YEAR(GETDATE()), 2)
	   FROM		Dealer
	   WHERE	ID = @DealerID

	   SELECT	@RegNumber = [dbo].[fn_GetNumberPrefixSuffix](@dealercode, @year)

	   INSERT	INTO [dbo].[TOPSPTransferPayment]
				(
				  DealerID ,
				  CreditAccount ,
				  RegNumber ,
				  DueDate ,
				  PaymentPurposeID ,
				  TransferPlanDate ,
				  BankID ,
				  TOPSPTransferPaymentIDReff ,
				  IsAccelerated ,
				  Status ,
				  ValidatedBy ,
				  ValidatedTime ,
				  ConfirmedBy ,
				  ConfirmedTime ,
				  CanceledBy ,
				  CanceledTime ,
				  TransferAmount ,
				  TransferActualDate ,
				  RowStatus ,
				  CreatedBy ,
				  CreatedTime ,
				  LastUpdateBy ,
				  LastUpdateTime
				)
	   VALUES	(
				  @DealerID ,
				  @CreditAccount ,
				  @dealercode + @year + @RegNumber ,
				  @DueDate ,
				  @PaymentPurposeID ,
				  @TransferPlanDate ,
				  @bankID ,
				  @TOPSPTransferPaymentIDReff ,
				  @IsAccelerated ,
				  @Status ,
				  @ValidatedBy ,
				  @ValidatedTime ,
				  @ConfirmedBy ,
				  @ConfirmedTime ,
				  @CanceledBy ,
				  @CanceledTime ,
				  @TransferAmount ,
				  @TransferActualDate ,
				  @RowStatus ,
				  @CreatedBy ,
				  GETDATE() ,
				  @LastUpdateBy ,
				  GETDATE()
				)


	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertTOPSPTransferPaymentDetail
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @SparePartBillingID INT ,
	   @Amount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[TOPSPTransferPaymentDetail]
	   VALUES	( @TOPSPTransferPaymentID, @SparePartBillingID, @Amount, @RowStatus, @CreatedBy, GETDATE(),
				  @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

CREATE PROCEDURE up_InsertTOPSPTransferPaymentPercepatan
	   @IDOld INT ,
	   @dealerID INT ,
	   @createdBy VARCHAR(40) ,
	   @NewPTDate DATE
AS
	   DECLARE @dealercode VARCHAR(10)= ''
	   DECLARE @year AS VARCHAR(4)= ''
	   DECLARE @RegNumber AS VARCHAR(25)= ''
	   DECLARE @id INTEGER

	   SELECT	@dealercode = STUFF(CreditAccount, CHARINDEX(LEFT(CreditAccount, 1), CreditAccount), 1, '2') ,
				@year = RIGHT(YEAR(GETDATE()), 2)
	   FROM		Dealer
	   WHERE	ID = @dealerID

	   SELECT	@RegNumber = [dbo].[fn_GetNumberPrefixSuffix](@dealercode, @year)

	   INSERT	INTO [dbo].[TOPSPTransferPayment]
				(
				  DealerID ,
				  CreditAccount ,
				  RegNumber ,
				  DueDate ,
				  PaymentPurposeID ,
				  TransferPlanDate ,
				  BankID ,
				  TOPSPTransferPaymentIDReff ,
				  IsAccelerated ,
				  Status ,
				  ValidatedBy ,
				  ValidatedTime ,
				  ConfirmedBy ,
				  ConfirmedTime ,
				  CanceledBy ,
				  CanceledTime ,
				  TransferAmount ,
				  TransferActualDate ,
				  RowStatus ,
				  CreatedBy ,
				  CreatedTime ,
				  LastUpdateBy ,
				  LastUpdateTime
				)
	   SELECT	[DealerID] ,
				[CreditAccount] ,
				@dealercode + @year + @RegNumber ,
				[DueDate] ,
				[PaymentPurposeID] ,
				@NewPTDate ,
				[BankID] ,
				@IDOld ,
				1 ,
				0 ,
				[ValidatedBy] ,
				[ValidatedTime] ,
				[ConfirmedBy] ,
				[ConfirmedTime] ,
				[CanceledBy] ,
				[CanceledTime] ,
				[TransferAmount] ,
				[TransferActualDate] ,
				[RowStatus] ,
				@createdBy ,
				GETDATE() ,
				'' ,
				GETDATE()
	   FROM		[dbo].[TOPSPTransferPayment] (NOLOCK)
	   WHERE	id = @IDOld

	   SET @id = @@IDENTITY

	   INSERT	INTO [dbo].[TOPSPTransferPaymentDetail]
	   SELECT	@id ,
				[SparePartBillingID] ,
				[Amount] ,
				[RowStatus] ,
				[CreatedBy] ,
				GETDATE() ,
				[LastUpdateBy] ,
				GETDATE()
	   FROM		[dbo].[TOPSPTransferPaymentDetail] (NOLOCK)
	   WHERE	[TOPSPTransferPaymentID] = @IDOld

	   UPDATE	TOPSPTransferPayment
	   SET		Status = 1
	--(select valueid from StandardCode where Category = 'EnumTOPSPTransferPayment.Status' and ValueDesc='Batal')
	   WHERE	id = @IDOld

	   SELECT	*
	   FROM		dbo.TOPSPTransferPayment
	   WHERE	id = @id
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created   : Wednesday, October 18, 2017
-- Created By     : DNet Team by using CodeSmith v 2.6
-- Rev History    : [up_InsertDiscountProposalProjectHistory_ByDPID] 11,'budi anduk'
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertUOMConversion
	   @no INT ,
	   @kode VARCHAR(50) ,
	   @uomfrom VARCHAR(50) ,
	   @uomto VARCHAR(50) ,
	   @partreff VARCHAR(50) ,
	   @qty INT
AS
	   BEGIN

			 SET NOCOUNT ON;

			 DECLARE @CreatedTime DATETIME = GETDATE()
			 DECLARE @PartID INT
			 DECLARE @idconv INT 
      
			 SET @idconv = 0

			 BEGIN TRAN
	--SELECT @kode + ' - ' + @uomfrom + ' - ' + @uomto +' - ' + @partreff

			 SELECT	@PartID = ID
			 FROM	SparePartMaster a
			 WHERE	a.RowStatus = 0
					AND a.PartNumber = @kode
      
			 IF @PartID IS NULL
				BEGIN
					  SELECT	@no
				END 
			 ELSE
				BEGIN
					  SELECT TOP 1
								@idconv = ID
					  FROM		SparePartConversion a
					  WHERE		a.RowStatus = 0
								AND a.SparePartMasterID = @PartID
      
					  UPDATE	SparePartMaster
					  SET		UoM = @uomfrom ,
								PartNumberReff = @partreff ,
								LastUpdateBy = 'MIGRATION' ,
								LastUpdateTime = GETDATE()
					  WHERE		ID = @PartID
		
					  IF @idconv = 0
						 BEGIN
							   INSERT	INTO SparePartConversion
										(
										  [SparePartMasterID] ,
										  [UoMto] ,
										  [Qty] ,
										  [RowStatus] ,
										  [CreatedBy] ,
										  [CreatedTime] ,
										  [LastUpdateBy] ,
										  [LastUpdateTime]
										)
							   SELECT	@PartID ,
										@uomto ,
										@qty ,
										0 ,
										'MIGRATION' ,
										GETDATE() ,
										'MIGRATION' ,
										GETDATE()
						 END
					  ELSE
						 BEGIN
							   UPDATE	SparePartConversion
							   SET		[SparePartMasterID] = @PartID ,
										[UoMto] = @uomto ,
										[Qty] = @qty
							   WHERE	ID = @idconv
						 END
				END
			 COMMIT


	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertVehiclePurchaseDetail
	   @ID INT OUTPUT ,
	   @VehiclePurchaseHeaderID INT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseLineName VARCHAR(100) ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @CompletedName VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @ProductName VARCHAR(100) ,
	   @ProductVariantName VARCHAR(100) ,
	   @PODetail VARCHAR(50) ,
	   @POName VARCHAR(100) ,
	   @PRDetailName VARCHAR(100) ,
	   @PurchaseUnitName VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @RecallProductName VARCHAR(50) ,
	   @SODetailName VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumberName VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[VehiclePurchaseDetail]
	   VALUES	( @VehiclePurchaseHeaderID, @BUCode, @BUName, @CloseLine, @CloseLineName, @CloseReason, @Completed,
				  @CompletedName, @ProductDescription, @ProductName, @ProductVariantName, @PODetail, @POName,
				  @PRDetailName, @PurchaseUnitName, @QtyOrder, @QtyReceipt, @QtyReturn, @RecallProduct,
				  @RecallProductName, @SODetailName, @ScheduledShippingDate, @ServicePartsAndMaterial, @ShippingDate,
				  @Site, @StockNumberName, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertVehiclePurchaseHeader
	   @ID INT OUTPUT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @DeliveryMethod VARCHAR(10) ,
	   @Description VARCHAR(100) ,
	   @PRPOTypeName VARCHAR(100) ,
	   @DMSPONo VARCHAR(50) ,
	   @DMSPOStatus INT ,
	   @DMSPODate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(50) ,
	   @PurchaseReceiptNo VARCHAR(50) ,
	   @PurchaseReceiptDetailNo VARCHAR(50) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   INSERT	INTO [dbo].[VehiclePurchaseHeader]
	   VALUES	( @BUCode, @BUName, @DeliveryMethod, @Description, @PRPOTypeName, @DMSPONo, @DMSPOStatus, @DMSPODate,
				  @VendorDescription, @Vendor, @PurchaseOrderNo, @PurchaseReceiptNo, @PurchaseReceiptDetailNo,
				  @ChassisModel, @ChassisNumberRegister, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_InsertVWI_CampaignReport
	   @ID INT OUTPUT ,
	   @NomorSurat VARCHAR(50) ,
	   @Status SMALLINT ,
	   @BenefitRegNo VARCHAR(50) ,
	   @Remarks VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	--@LastUpdateTime datetime,
	   @DetailRowStatus SMALLINT ,
	   @DealerID SMALLINT ,
	   @DealerCode VARCHAR(50) ,
	   @FakturValidationStart DATETIME ,
	   @FakturValidationEnd DATETIME ,
	   @FakturOpenStart DATETIME ,
	   @FakturOpenEnd DATETIME ,
	   @VechileTypeID SMALLINT ,
	   @VechileTypeCode VARCHAR(50) ,
	   @VehicleTypeDesc VARCHAR(50) ,
	   @FormulaID CHAR(1)
AS --INSERT	INTO	[dbo].[VWI_CampaignReport]
--VALUES
--(
--	@NomorSurat,
--	@Status,
--	@BenefitRegNo,
--	@Remarks,
--	@RowStatus,
--	GETDATE(),	
--	@DetailRowStatus,
--	@DealerID,
--	@DealerCode,
--	@FakturValidationStart,
--	@FakturValidationEnd,
--	@FakturOpenStart,
--	@FakturOpenEnd,
--	@VechileTypeID,
--	@VechileTypeCode,
--	@VehicleTypeDesc,
--	@FormulaID)

	
	   SET @ID = @@IDENTITY
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveAPPayment @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[APPaymentNo] ,
				[APReferenceNo] ,
				[APVoucherReferenceNo] ,
				[AppliedToDocument] ,
				[BU] ,
				[Cancelled] ,
				[CashAndBank] ,
				[MethodOfPayment] ,
				[AvailableBalance] ,
				[State] ,
				[TotalChangeAmount] ,
				[TotalPaymentAmount] ,
				[TransactionDate] ,
				[Type] ,
				[VendorDescription] ,
				[Vendor] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[APPayment]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveAPPaymentDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[APPaymentID] ,
				[Owner] ,
				[APPaymentDetailNo] ,
				[APPaymentNo] ,
				[BU] ,
				[ChangeAmount] ,
				[Description] ,
				[DifferenceValue] ,
				[ExternalDocumentNo] ,
				[ExternalDocumentType] ,
				[APVoucherNo] ,
				[OrderDate] ,
				[OrderNoNVSOReferral] ,
				[OrderNoOutsourceWorkOrder] ,
				[OrderNo] ,
				[OrderNoUVSOReferral] ,
				[OutstandingBalance] ,
				[PaymentAmount] ,
				[PaymentSlipNo] ,
				[ReceiptFromVendor] ,
				[RemainingBalance] ,
				[SourceType] ,
				[TransactionDocument] ,
				[Vendor] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[APPaymentDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveAPPaymentDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[APPaymentID] ,
				[Owner] ,
				[APPaymentDetailNo] ,
				[APPaymentNo] ,
				[BU] ,
				[ChangeAmount] ,
				[Description] ,
				[DifferenceValue] ,
				[ExternalDocumentNo] ,
				[ExternalDocumentType] ,
				[APVoucherNo] ,
				[OrderDate] ,
				[OrderNoNVSOReferral] ,
				[OrderNoOutsourceWorkOrder] ,
				[OrderNo] ,
				[OrderNoUVSOReferral] ,
				[OutstandingBalance] ,
				[PaymentAmount] ,
				[PaymentSlipNo] ,
				[ReceiptFromVendor] ,
				[RemainingBalance] ,
				[SourceType] ,
				[TransactionDocument] ,
				[Vendor] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[APPaymentDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveAPPaymentList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[APPaymentNo] ,
				[APReferenceNo] ,
				[APVoucherReferenceNo] ,
				[AppliedToDocument] ,
				[BU] ,
				[Cancelled] ,
				[CashAndBank] ,
				[MethodOfPayment] ,
				[AvailableBalance] ,
				[State] ,
				[TotalChangeAmount] ,
				[TotalPaymentAmount] ,
				[TransactionDate] ,
				[Type] ,
				[VendorDescription] ,
				[Vendor] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[APPayment] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveARReceipt @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[GeneratedToken] ,
				[ARInvoiceReferenceNo] ,
				[ARReceiptNo] ,
				[ARReceiptReferenceNo] ,
				[Type] ,
				[BookingFee] ,
				[BU] ,
				[Cancelled] ,
				[CashAndBank] ,
				[Customer] ,
				[CustomerNo] ,
				[EndOrderDate] ,
				[MethodOfPayment] ,
				[AvailableBalance] ,
				[StartOrderDate] ,
				[State] ,
				[AppliedToDocument] ,
				[TotalAmountBase] ,
				[TotalChangeAmount] ,
				[TotalOutstandingBalanceBase] ,
				[TotalReceiptAmount] ,
				[TotalRemainingBalanceBase] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[ARReceipt]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveARReceiptDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[ARReceiptID] ,
				[Owner] ,
				[DetailNo] ,
				[ARReceiptNo] ,
				[BU] ,
				[ChangeAmount] ,
				[Customer] ,
				[Description] ,
				[DifferenceValue] ,
				[InvoiceNo] ,
				[OrderDate] ,
				[OrderNo] ,
				[OrderNoSO] ,
				[OrderNoUVSO] ,
				[OrderNoWO] ,
				[OutstandingBalance] ,
				[PaidBackToCustomer] ,
				[ReceiptAmount] ,
				[RemainingBalance] ,
				[SourceType] ,
				[TransactionDocument] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[ARReceiptDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveARReceiptDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[ARReceiptID] ,
				[Owner] ,
				[DetailNo] ,
				[ARReceiptNo] ,
				[BU] ,
				[ChangeAmount] ,
				[Customer] ,
				[Description] ,
				[DifferenceValue] ,
				[InvoiceNo] ,
				[OrderDate] ,
				[OrderNo] ,
				[OrderNoSO] ,
				[OrderNoUVSO] ,
				[OrderNoWO] ,
				[OutstandingBalance] ,
				[PaidBackToCustomer] ,
				[ReceiptAmount] ,
				[RemainingBalance] ,
				[SourceType] ,
				[TransactionDocument] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[ARReceiptDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveARReceiptList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[GeneratedToken] ,
				[ARInvoiceReferenceNo] ,
				[ARReceiptNo] ,
				[ARReceiptReferenceNo] ,
				[Type] ,
				[BookingFee] ,
				[BU] ,
				[Cancelled] ,
				[CashAndBank] ,
				[Customer] ,
				[CustomerNo] ,
				[EndOrderDate] ,
				[MethodOfPayment] ,
				[AvailableBalance] ,
				[StartOrderDate] ,
				[State] ,
				[AppliedToDocument] ,
				[TotalAmountBase] ,
				[TotalChangeAmount] ,
				[TotalOutstandingBalanceBase] ,
				[TotalReceiptAmount] ,
				[TotalRemainingBalanceBase] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[ARReceipt] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveBusinessSectorDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[BusinessSectorHeaderID] ,
				[BusinessDomain] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[BusinessSectorDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveBusinessSectorDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[BusinessSectorHeaderID] ,
				[BusinessDomain] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[BusinessSectorDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveBusinessSectorHeader @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[BusinessSectorName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[BusinessSectorHeader]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveBusinessSectorHeaderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[BusinessSectorName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[BusinessSectorHeader] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCarrosserieDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[CarrosserieHeaderID] ,
				[PDIStateCode] ,
				[PDIStatusCode] ,
				[AccessorriesDescription] ,
				[AccessorriesName] ,
				[BUCode] ,
				[BUName] ,
				[KITName] ,
				[PBUCode] ,
				[PBUName] ,
				[PDIDetailName] ,
				[PDIReceiptDetailNo] ,
				[PDIReceiptName] ,
				[ReceiveQuantity] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CarrosserieDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCarrosserieDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[CarrosserieHeaderID] ,
				[PDIStateCode] ,
				[PDIStatusCode] ,
				[AccessorriesDescription] ,
				[AccessorriesName] ,
				[BUCode] ,
				[BUName] ,
				[KITName] ,
				[PBUCode] ,
				[PBUName] ,
				[PDIDetailName] ,
				[PDIReceiptDetailNo] ,
				[PDIReceiptName] ,
				[ReceiveQuantity] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CarrosserieDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCarrosserieHeader @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[PDIStateCode] ,
				[PDIStatusCode] ,
				[BUCode] ,
				[BUName] ,
				[PDIName] ,
				[PDIReceiptNo] ,
				[PDIReceiptRefName] ,
				[PDIReceiptStatus] ,
				[TransactionDate] ,
				[TransactionType] ,
				[VendorName] ,
				[ChassisNumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CarrosserieHeader]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCarrosserieHeaderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[PDIStateCode] ,
				[PDIStatusCode] ,
				[BUCode] ,
				[BUName] ,
				[PDIName] ,
				[PDIReceiptNo] ,
				[PDIReceiptRefName] ,
				[PDIReceiptStatus] ,
				[TransactionDate] ,
				[TransactionType] ,
				[VendorName] ,
				[ChassisNumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CarrosserieHeader] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCustomerGroup @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Code] ,
				[Name] ,
				[Description] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CustomerGroup]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCustomerGroupList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Code] ,
				[Name] ,
				[Description] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CustomerGroup] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCustomerRequestOCR @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[CustomerRequestID] ,
				[OCRIdentityID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CustomerRequestOCR]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveCustomerRequestOCRList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[CustomerRequestID] ,
				[OCRIdentityID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[CustomerRequestOCR] 

	   SET NOCOUNT OFF
GO

CREATE PROCEDURE up_RetrieveDAScoreGrade_PrevYear
	   @FleetCustomerID INT ,
	   @Period DATETIME
AS
	   SET NOCOUNT ON

	   SELECT TOP 1
				a.*
	   FROM		DAScoreGrade a ( NOLOCK )
	   INNER JOIN FleetCustomerGrade b ( NOLOCK ) ON a.[ID] = b.[RatioMatrixID]
	   WHERE	1 = 1
				AND a.[Type] = 5
				AND [a].[RowStatus] = 0
				AND YEAR(b.[Period]) = YEAR(@Period)
				AND MONTH(b.[Period]) = MONTH(@Period)
				AND b.[FleetCustomerID] = @FleetCustomerID
	   ORDER BY	b.[Period] DESC
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveDealerSystems @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[DealerID] ,
				[SystemID] ,
				[isSPKMatchFaktur] ,
				[isOnlyUploadPhotoTenagaPenjual] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[DealerSystems]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveDealerSystemsList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[DealerID] ,
				[SystemID] ,
				[isSPKMatchFaktur] ,
				[isOnlyUploadPhotoTenagaPenjual] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[DealerSystems] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveDMSWOWarrantyClaim @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[DealerID] ,
				[DealerBranchID] ,
				[ChassisNumber] ,
				[isBB] ,
				[WorkOrderNumber] ,
				[FailureDate] ,
				[ServiceDate] ,
				[Owner] ,
				[Mileage] ,
				[ServiceBuletin] ,
				[Symptoms] ,
				[Causes] ,
				[Results] ,
				[Notes] ,
				[RowStatus] ,
				[CreateBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[DMSWOWarrantyClaim]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveDMSWOWarrantyClaimList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[DealerID] ,
				[DealerBranchID] ,
				[ChassisNumber] ,
				[isBB] ,
				[WorkOrderNumber] ,
				[FailureDate] ,
				[ServiceDate] ,
				[Owner] ,
				[Mileage] ,
				[ServiceBuletin] ,
				[Symptoms] ,
				[Causes] ,
				[Results] ,
				[Notes] ,
				[RowStatus] ,
				[CreateBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[DMSWOWarrantyClaim] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFieldFixList @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[ChassisNo] ,
				[RecallRegNo] ,
				[Description] ,
				[BuletinDescription] ,
				[DealerCode] ,
				[DealerName]
	   FROM		[dbo].[FieldFixList]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFieldFixListList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[ChassisNo] ,
				[RecallRegNo] ,
				[Description] ,
				[BuletinDescription] ,
				[DealerCode] ,
				[DealerName]
	   FROM		[dbo].[FieldFixList] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleet @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[FleetCode] ,
				[FleetName] ,
				[FleetNickName] ,
				[FleetGroup] ,
				[Address] ,
				[ProvinceId] ,
				[CityId] ,
				[IdentityType] ,
				[IdentityNumber] ,
				[BusinessSectorHeaderId] ,
				[BusinessSectorDetailId] ,
				[FleetNote] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Fleet]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomer @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[CustomerGroupID] ,
				[ProvinceID] ,
				[PreArea] ,
				[CityID] ,
				[BusinessSectorDetailId] ,
				[RatioMatrixID] ,
				[CategoryIndex] ,
				[TypeIndex] ,
				[Code] ,
				[Name] ,
				[Gedung] ,
				[Alamat] ,
				[Kecamatan] ,
				[Kelurahan] ,
				[PostalCode] ,
				[Email] ,
				[PhoneNo] ,
				[TipeCustomer] ,
				[IdentityType] ,
				[IdentityNumber] ,
				[Attachment] ,
				[ClassificationIndex] ,
				[FleetNickName] ,
				[FleetNote] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomer]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerContact @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[Name] ,
				[Position] ,
				[PhoneNo] ,
				[Handphone] ,
				[Email] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerContact]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerContactList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[Name] ,
				[Position] ,
				[PhoneNo] ,
				[Handphone] ,
				[Email] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerContact] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[CustomerGroupID] ,
				[ProvinceID] ,
				[PreArea] ,
				[CityID] ,
				[BusinessSectorDetailId] ,
				[RatioMatrixID] ,
				[CategoryIndex] ,
				[TypeIndex] ,
				[Code] ,
				[Name] ,
				[Gedung] ,
				[Alamat] ,
				[Kecamatan] ,
				[Kelurahan] ,
				[PostalCode] ,
				[Email] ,
				[PhoneNo] ,
				[TipeCustomer] ,
				[IdentityType] ,
				[IdentityNumber] ,
				[Attachment] ,
				[ClassificationIndex] ,
				[FleetNickName] ,
				[FleetNote] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomer] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerToCustomer @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[CustomerID] ,
				[IsDefault] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerToCustomer]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerToCustomerList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[CustomerID] ,
				[IsDefault] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerToCustomer] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerToDealer @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[DealerID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerToDealer]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetCustomerToDealerList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[FleetCustomerID] ,
				[DealerID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[FleetCustomerToDealer] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveFleetList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[FleetCode] ,
				[FleetName] ,
				[FleetNickName] ,
				[FleetGroup] ,
				[Address] ,
				[ProvinceId] ,
				[CityId] ,
				[IdentityType] ,
				[IdentityNumber] ,
				[BusinessSectorHeaderId] ,
				[BusinessSectorDetailId] ,
				[FleetNote] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Fleet] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveIndustrialSector @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[IndustrialSector]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveIndustrialSectorList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[IndustrialSector] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransaction @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[DealerCode] ,
				[InventoryTransactionNo] ,
				[InventoryTransferNo] ,
				[PersonInCharge] ,
				[ProcessCode] ,
				[SourceData] ,
				[State] ,
				[TransactionDate] ,
				[TransactionType] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransaction]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransactionDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[BaseQuantity] ,
				[BatchNo] ,
				[BU] ,
				[Department] ,
				[Description] ,
				[FromBU] ,
				[InventoryTransactionID] ,
				[InventoryTransactionNo] ,
				[InventoryTransferDetail] ,
				[InventoryUnit] ,
				[Location] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[Quantity] ,
				[ReasonCode] ,
				[ReferenceNo] ,
				[RegisterSerialNumber] ,
				[RunningNumber] ,
				[SerialNo] ,
				[ServicePartsAndMaterial] ,
				[Site] ,
				[SourceData] ,
				[StockNumber] ,
				[StockNumberNV] ,
				[TotalCost] ,
				[TransactionType] ,
				[TransactionUnit] ,
				[UnitCost] ,
				[VIN] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransactionDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransactionDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[BaseQuantity] ,
				[BatchNo] ,
				[BU] ,
				[Department] ,
				[Description] ,
				[FromBU] ,
				[InventoryTransactionID] ,
				[InventoryTransactionNo] ,
				[InventoryTransferDetail] ,
				[InventoryUnit] ,
				[Location] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[Quantity] ,
				[ReasonCode] ,
				[ReferenceNo] ,
				[RegisterSerialNumber] ,
				[RunningNumber] ,
				[SerialNo] ,
				[ServicePartsAndMaterial] ,
				[Site] ,
				[SourceData] ,
				[StockNumber] ,
				[StockNumberNV] ,
				[TotalCost] ,
				[TransactionType] ,
				[TransactionUnit] ,
				[UnitCost] ,
				[VIN] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransactionDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransactionList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[DealerCode] ,
				[InventoryTransactionNo] ,
				[InventoryTransferNo] ,
				[PersonInCharge] ,
				[ProcessCode] ,
				[SourceData] ,
				[State] ,
				[TransactionDate] ,
				[TransactionType] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransaction] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransfer @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[FromDealer] ,
				[FromSite] ,
				[InventoryTransferNo] ,
				[ItemTypeForTransfer] ,
				[PersonInCharge] ,
				[ReceiptDate] ,
				[ReceiptNo] ,
				[ReferenceNo] ,
				[SearchVehicle] ,
				[SourceData] ,
				[State] ,
				[ToDealer] ,
				[ToSite] ,
				[TransactionDate] ,
				[TransactionType] ,
				[TransferStatus] ,
				[TransferStep] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransfer]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransferDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[InventoryTransferID] ,
				[Owner] ,
				[BaseQuantity] ,
				[ConsumptionTaxIn] ,
				[ConsumptionTaxOut] ,
				[FromBatchNo] ,
				[FromDealer] ,
				[FromConfiguration] ,
				[FromExteriorColor] ,
				[FromInteriorColor] ,
				[FromLocation] ,
				[FromSerialNo] ,
				[FromSite] ,
				[FromStyle] ,
				[FromWarehouse] ,
				[InventoryTransferNo] ,
				[InventoryUnit] ,
				[ProductDescription] ,
				[Product] ,
				[Quantity] ,
				[Remarks] ,
				[ServicePartsandMaterial] ,
				[SourceData] ,
				[StockNumber] ,
				[StockNumberNV] ,
				[StockNumberLookupName] ,
				[StockNumberLookupType] ,
				[ToBatchNo] ,
				[ToDealer] ,
				[ToConfiguration] ,
				[ToExteriorColor] ,
				[ToInteriorColor] ,
				[ToLocation] ,
				[ToSerialNo] ,
				[ToSite] ,
				[ToStyle] ,
				[ToWarehouse] ,
				[TransactionUnit] ,
				[VIN] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransferDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransferDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[InventoryTransferID] ,
				[Owner] ,
				[BaseQuantity] ,
				[ConsumptionTaxIn] ,
				[ConsumptionTaxOut] ,
				[FromBatchNo] ,
				[FromDealer] ,
				[FromConfiguration] ,
				[FromExteriorColor] ,
				[FromInteriorColor] ,
				[FromLocation] ,
				[FromSerialNo] ,
				[FromSite] ,
				[FromStyle] ,
				[FromWarehouse] ,
				[InventoryTransferNo] ,
				[InventoryUnit] ,
				[ProductDescription] ,
				[Product] ,
				[Quantity] ,
				[Remarks] ,
				[ServicePartsandMaterial] ,
				[SourceData] ,
				[StockNumber] ,
				[StockNumberNV] ,
				[StockNumberLookupName] ,
				[StockNumberLookupType] ,
				[ToBatchNo] ,
				[ToDealer] ,
				[ToConfiguration] ,
				[ToExteriorColor] ,
				[ToInteriorColor] ,
				[ToLocation] ,
				[ToSerialNo] ,
				[ToSite] ,
				[ToStyle] ,
				[ToWarehouse] ,
				[TransactionUnit] ,
				[VIN] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransferDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveInventoryTransferList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[FromDealer] ,
				[FromSite] ,
				[InventoryTransferNo] ,
				[ItemTypeForTransfer] ,
				[PersonInCharge] ,
				[ReceiptDate] ,
				[ReceiptNo] ,
				[ReferenceNo] ,
				[SearchVehicle] ,
				[SourceData] ,
				[State] ,
				[ToDealer] ,
				[ToSite] ,
				[TransactionDate] ,
				[TransactionType] ,
				[TransferStatus] ,
				[TransferStep] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[InventoryTransfer] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveKaroseri @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[Code] ,
				[Name] ,
				[City] ,
				[Alamat] ,
				[Kelurahan] ,
				[Kecamatan] ,
				[Province] ,
				[PostalCode] ,
				[PhoneNo] ,
				[Fax] ,
				[WebSite] ,
				[Email] ,
				[ContactPerson] ,
				[HP] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Karoseri]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveKaroseriList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[Code] ,
				[Name] ,
				[City] ,
				[Alamat] ,
				[Kelurahan] ,
				[Kecamatan] ,
				[Province] ,
				[PostalCode] ,
				[PhoneNo] ,
				[Fax] ,
				[WebSite] ,
				[Email] ,
				[ContactPerson] ,
				[HP] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Karoseri] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveLeasing @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				LeasingGroupName ,
				[LeasingCode] ,
				[LeasingName] ,
				[City] ,
				[Alamat] ,
				[Kelurahan] ,
				[Kecamatan] ,
				[Province] ,
				[PostalCode] ,
				[PhoneNo] ,
				[Fax] ,
				[WebSite] ,
				[Email] ,
				[ContactPerson] ,
				[HP] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Leasing]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveLeasingList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				LeasingGroupName ,
				[LeasingCode] ,
				[LeasingName] ,
				[City] ,
				[Alamat] ,
				[Kelurahan] ,
				[Kecamatan] ,
				[Province] ,
				[PostalCode] ,
				[PhoneNo] ,
				[Fax] ,
				[WebSite] ,
				[Email] ,
				[ContactPerson] ,
				[HP] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[Leasing] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveMyAlertStatus @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[AlertMasterID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[MyAlertStatus]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveMyAlertStatusList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[AlertMasterID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[MyAlertStatus] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrievePOOtherVendor @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[Address1] ,
				[Address2] ,
				[Address3] ,
				[AllocationPeriod] ,
				[Balance] ,
				[DealerCode] ,
				[City] ,
				[CloseRespon] ,
				[Country] ,
				[DeliveryMethod] ,
				[Description] ,
				[DownPayment] ,
				[DownPaymentAmountPaid] ,
				[DownPaymentIsPaid] ,
				[EventDate] ,
				[ExternalDocNo] ,
				[FormSource] ,
				[GrandTotal] ,
				[PaymentGroup] ,
				[PersonInCharge] ,
				[PostalCode] ,
				[Priority] ,
				[Province] ,
				[PRPOType] ,
				[PurchaseOrderNo] ,
				[SONo] ,
				[Site] ,
				[State] ,
				[StockReferenceNo] ,
				[Taxable] ,
				[TermsOfPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalDiscountAmount] ,
				[TotalTitleRegistrationFee] ,
				[PurchaseOrderDate] ,
				[VendorDescription] ,
				[Vendor] ,
				[Warehouse] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[POOtherVendor]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrievePOOtherVendorDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[POOtherVendorID] ,
				[Owner] ,
				[DealerCode] ,
				[CloseLine] ,
				[CloseReason] ,
				[Completed] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[Department] ,
				[Description] ,
				[DiscountAmount] ,
				[DiscountPercentage] ,
				[EventData] ,
				[FormSource] ,
				[BaseQtyOrder] ,
				[BaseQtyReceipt] ,
				[BaseQtyReturn] ,
				[InventoryUnit] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[ProductSubstitute] ,
				[ProductVariant] ,
				[ProductVolume] ,
				[ProductWeight] ,
				[PromisedDate] ,
				[PurchaseFor] ,
				[PurchaseOrderNo] ,
				[PurchaseRequisitionDetail] ,
				[PurchaseUnit] ,
				[QtyOrder] ,
				[QtyReceipt] ,
				[QtyReturn] ,
				[RecallProduct] ,
				[ReferenceNo] ,
				[RequiredDate] ,
				[SalesOrderDetail] ,
				[ScheduledShippingDate] ,
				[ServicePartsAndMaterial] ,
				[ShippingDate] ,
				[Site] ,
				[StockNumber] ,
				[TitleRegistrationFee] ,
				[TotalAmount] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalVolume] ,
				[TotalWeight] ,
				[TransactionAmount] ,
				[UnitCost] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[POOtherVendorDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrievePOOtherVendorDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[POOtherVendorID] ,
				[Owner] ,
				[DealerCode] ,
				[CloseLine] ,
				[CloseReason] ,
				[Completed] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[Department] ,
				[Description] ,
				[DiscountAmount] ,
				[DiscountPercentage] ,
				[EventData] ,
				[FormSource] ,
				[BaseQtyOrder] ,
				[BaseQtyReceipt] ,
				[BaseQtyReturn] ,
				[InventoryUnit] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[ProductSubstitute] ,
				[ProductVariant] ,
				[ProductVolume] ,
				[ProductWeight] ,
				[PromisedDate] ,
				[PurchaseFor] ,
				[PurchaseOrderNo] ,
				[PurchaseRequisitionDetail] ,
				[PurchaseUnit] ,
				[QtyOrder] ,
				[QtyReceipt] ,
				[QtyReturn] ,
				[RecallProduct] ,
				[ReferenceNo] ,
				[RequiredDate] ,
				[SalesOrderDetail] ,
				[ScheduledShippingDate] ,
				[ServicePartsAndMaterial] ,
				[ShippingDate] ,
				[Site] ,
				[StockNumber] ,
				[TitleRegistrationFee] ,
				[TotalAmount] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalVolume] ,
				[TotalWeight] ,
				[TransactionAmount] ,
				[UnitCost] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[POOtherVendorDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrievePOOtherVendorList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[Address1] ,
				[Address2] ,
				[Address3] ,
				[AllocationPeriod] ,
				[Balance] ,
				[DealerCode] ,
				[City] ,
				[CloseRespon] ,
				[Country] ,
				[DeliveryMethod] ,
				[Description] ,
				[DownPayment] ,
				[DownPaymentAmountPaid] ,
				[DownPaymentIsPaid] ,
				[EventDate] ,
				[ExternalDocNo] ,
				[FormSource] ,
				[GrandTotal] ,
				[PaymentGroup] ,
				[PersonInCharge] ,
				[PostalCode] ,
				[Priority] ,
				[Province] ,
				[PRPOType] ,
				[PurchaseOrderNo] ,
				[SONo] ,
				[Site] ,
				[State] ,
				[StockReferenceNo] ,
				[Taxable] ,
				[TermsOfPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalDiscountAmount] ,
				[TotalTitleRegistrationFee] ,
				[PurchaseOrderDate] ,
				[VendorDescription] ,
				[Vendor] ,
				[Warehouse] ,
				[WONo] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[POOtherVendor] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionChassisMasterProfile @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[ChassisMasterID] ,
				[EndCustomerID] ,
				[ProfileHeaderID] ,
				[GroupID] ,
				[ProfileValue] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionChassisMasterProfile]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionChassisMasterProfileList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[ChassisMasterID] ,
				[EndCustomerID] ,
				[ProfileHeaderID] ,
				[GroupID] ,
				[ProfileValue] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionChassisMasterProfile] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionFaktur @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[ChassisMasterID] ,
				[EndCustomerID] ,
				[OldEndCustomerID] ,
				[RegNumber] ,
				[RevisionStatus] ,
				[RevisionTypeID] ,
				[IsPay] ,
				[NewValidationDate] ,
				[NewValidationBy] ,
				[NewConfirmationDate] ,
				[NewConfirmationBy] ,
				[Remark] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionFaktur]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionFakturList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[ChassisMasterID] ,
				[EndCustomerID] ,
				[OldEndCustomerID] ,
				[RegNumber] ,
				[RevisionStatus] ,
				[RevisionTypeID] ,
				[IsPay] ,
				[NewValidationDate] ,
				[NewValidationBy] ,
				[NewConfirmationDate] ,
				[NewConfirmationBy] ,
				[Remark] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionFaktur] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPaymentDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[RevisionFakturID] ,
				[RevisionPaymentHeaderID] ,
				[RevisionSAPDocID] ,
				[IsCancel] ,
				[CancelReason] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPaymentDetail]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPaymentDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[RevisionFakturID] ,
				[RevisionPaymentHeaderID] ,
				[RevisionSAPDocID] ,
				[IsCancel] ,
				[CancelReason] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPaymentDetail] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPaymentHeader @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[DealerID] ,
				[PaymentType] ,
				[RegNumber] ,
				[RevisionPaymentDocID] ,
				[SlipNumber] ,
				[TotalAmount] ,
				[Status] ,
				[EvidencePath] ,
				[ActualPaymentDate] ,
				[ActualPaymentAmount] ,
				[AccDocNumber] ,
				[GyroDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPaymentHeader]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPaymentHeaderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[DealerID] ,
				[PaymentType] ,
				[RegNumber] ,
				[RevisionPaymentDocID] ,
				[SlipNumber] ,
				[TotalAmount] ,
				[Status] ,
				[EvidencePath] ,
				[ActualPaymentDate] ,
				[ActualPaymentAmount] ,
				[AccDocNumber] ,
				[GyroDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPaymentHeader] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPrice @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[CategoryID] ,
				[RevisionTypeID] ,
				[Amount] ,
				[ValidFrom] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPrice]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionPriceList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[CategoryID] ,
				[RevisionTypeID] ,
				[Amount] ,
				[ValidFrom] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionPrice] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionSAPDoc @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[RevisionFakturID] ,
				[DebitChargeNo] ,
				[DCAmount] ,
				[DebitMemoNo] ,
				[DMAmount] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionSAPDoc]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionSAPDocList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[RevisionFakturID] ,
				[DebitChargeNo] ,
				[DCAmount] ,
				[DebitMemoNo] ,
				[DMAmount] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionSAPDoc] 

	   SET NOCOUNT OFF


-------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_UpdateRevisionSAPDoc]    Script Date: 19/09/2018 16:54:25 ******/
	   SET ANSI_NULLS OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionSPKFaktur @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[SPKHeaderID] ,
				[EndCustomerID] ,
				[RowStatus] ,
				[CreatedTime] ,
				[CreatedBy] ,
				[LastUpdateTime] ,
				[LastUpdateBy]
	   FROM		[dbo].[RevisionSPKFaktur]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionSPKFakturList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[SPKHeaderID] ,
				[EndCustomerID] ,
				[RowStatus] ,
				[CreatedTime] ,
				[CreatedBy] ,
				[LastUpdateTime] ,
				[LastUpdateBy]
	   FROM		[dbo].[RevisionSPKFaktur] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionType @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[Description] ,
				[RevisionCode] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionType]
	   WHERE	[ID] = @ID
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_RetrieveRevisionTypeList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON
	   SELECT	[ID] ,
				[Description] ,
				[RevisionCode] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[RevisionType] 
	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartConversion @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartMasterID] ,
				[UoMto] ,
				[Qty] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartConversion]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartConversionList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartMasterID] ,
				[UoMto] ,
				[Qty] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartConversion] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartDeliveryOrder @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Owner] ,
				[Address1] ,
				[Address2] ,
				[Address3] ,
				[Address4] ,
				[BusinessPhone] ,
				[BU] ,
				[CancellationDate] ,
				[City] ,
				[CustomerContacts] ,
				[Customer] ,
				[CustomerNo] ,
				[DeliveryAddress] ,
				[DeliveryOrderNo] ,
				[DeliveryType] ,
				[ExternalReferenceNo] ,
				[GrandTotal] ,
				[Status] ,
				[MethodofPayment] ,
				[OrderType] ,
				[ReferenceNo] ,
				[Salesperson] ,
				[State] ,
				[TermofPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalDiscountAmount] ,
				[TotalMiscChargeBaseAmount] ,
				[TotalMiscChargeConsumptionTaxAmount] ,
				[TotalReceipt] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartDeliveryOrder]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartDeliveryOrderDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartDeliveryOrderID] ,
				[Owner] ,
				[AmountBeforeDiscount] ,
				[BaseAmount] ,
				[BaseQtyDelivered] ,
				[BaseQtyOrder] ,
				[BatchNo] ,
				[BU] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DeliveryOrderDetail] ,
				[DeliveryOrderNo] ,
				[DiscountAmount] ,
				[DiscountBaseAmount] ,
				[DiscountPercentage] ,
				[Location] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[PromiseDate] ,
				[QtyDelivered] ,
				[QtyOrder] ,
				[RequestDate] ,
				[RunningNumber] ,
				[SalesOrderDetail] ,
				[SalesUnit] ,
				[Site] ,
				[TotalAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionAmount] ,
				[UnitPrice] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartDeliveryOrderDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartDeliveryOrderDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartDeliveryOrderID] ,
				[Owner] ,
				[AmountBeforeDiscount] ,
				[BaseAmount] ,
				[BaseQtyDelivered] ,
				[BaseQtyOrder] ,
				[BatchNo] ,
				[BU] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DeliveryOrderDetail] ,
				[DeliveryOrderNo] ,
				[DiscountAmount] ,
				[DiscountBaseAmount] ,
				[DiscountPercentage] ,
				[Location] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[PromiseDate] ,
				[QtyDelivered] ,
				[QtyOrder] ,
				[RequestDate] ,
				[RunningNumber] ,
				[SalesOrderDetail] ,
				[SalesUnit] ,
				[Site] ,
				[TotalAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionAmount] ,
				[UnitPrice] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartDeliveryOrderDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartDeliveryOrderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Owner] ,
				[Address1] ,
				[Address2] ,
				[Address3] ,
				[Address4] ,
				[BusinessPhone] ,
				[BU] ,
				[CancellationDate] ,
				[City] ,
				[CustomerContacts] ,
				[Customer] ,
				[CustomerNo] ,
				[DeliveryAddress] ,
				[DeliveryOrderNo] ,
				[DeliveryType] ,
				[ExternalReferenceNo] ,
				[GrandTotal] ,
				[Status] ,
				[MethodofPayment] ,
				[OrderType] ,
				[ReferenceNo] ,
				[Salesperson] ,
				[State] ,
				[TermofPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalDiscountAmount] ,
				[TotalMiscChargeBaseAmount] ,
				[TotalMiscChargeConsumptionTaxAmount] ,
				[TotalReceipt] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartDeliveryOrder] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartMasterTOP @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartPOTypeTOPID] ,
				[SparePartMasterID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartMasterTOP]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartMasterTOPList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartPOTypeTOPID] ,
				[SparePartMasterID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartMasterTOP] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPOTypeTOP @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartPOType] ,
				[IsTOP] ,
				[TermOfPaymentIDNotTOP] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPOTypeTOP]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF

/****** Object:  StoredProcedure [dbo].[up_RetrieveSparePartPOTypeTOPList]    Script Date: 06/10/2018 15:47:02 ******/
	   SET ANSI_NULLS OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPOTypeTOPList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartPOType] ,
				[IsTOP] ,
				[TermOfPaymentIDNotTOP] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPOTypeTOP] 

	   SET NOCOUNT OFF


/****** Object:  StoredProcedure [dbo].[up_UpdateSparePartPOTypeTOP]    Script Date: 06/10/2018 15:47:29 ******/
	   SET ANSI_NULLS OFF
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_RetrieveSparePartPOTypeTOP_Validation
	   @partnumber VARCHAR(15) ,
	   @sppot INTEGER
AS
	   BEGIN

			 SET NOCOUNT ON;


			 SELECT	m.PartNumber ,
					t.Status AS spmTOPstatus ,
					p.ID ,
					p.SparePartPOType ,
					p.IsTOP AS sppotypeTOP
			 FROM	SparePartMasterTOP t
			 JOIN	SparePartMaster m ON m.ID = t.SparePartMasterID
			 JOIN	SparePartPOTypeTOP p ON p.ID = t.SparePartPOTypeTOPID
			 WHERE	PartNumber = @partnumber
					AND p.ID = @sppot
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPRDetailFromVendor @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[PRDetailNumber] ,
				[SparePartPRID] ,
				[PRNumber] ,
				[Owner] ,
				[BaseReceivedQuantity] ,
				[BatchNumber] ,
				[DealerCode] ,
				[ChassisModel] ,
				[ChassisNumberRegister] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DiscountAmount] ,
				[EngineNumber] ,
				[EventData] ,
				[InventoryUnit] ,
				[KeyNumber] ,
				[LandedCost] ,
				[Location] ,
				[ProductDescription] ,
				[Product] ,
				[ProductVolume] ,
				[ProductWeight] ,
				[PurchaseUnit] ,
				[ReceivedQuantity] ,
				[ReferenceNumber] ,
				[ReturnPRDetail] ,
				[ServicePartsAndMaterial] ,
				[Site] ,
				[StockNumber] ,
				[TitleRegistrationFee] ,
				[TotalAmount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalVolume] ,
				[TotalWeight] ,
				[TransactionAmount] ,
				[UnitCost] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPRDetailFromVendor]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPRDetailFromVendorList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[PRDetailNumber] ,
				[SparePartPRID] ,
				[PRNumber] ,
				[Owner] ,
				[BaseReceivedQuantity] ,
				[BatchNumber] ,
				[DealerCode] ,
				[ChassisModel] ,
				[ChassisNumberRegister] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DiscountAmount] ,
				[EngineNumber] ,
				[EventData] ,
				[InventoryUnit] ,
				[KeyNumber] ,
				[LandedCost] ,
				[Location] ,
				[ProductDescription] ,
				[Product] ,
				[ProductVolume] ,
				[ProductWeight] ,
				[PurchaseUnit] ,
				[ReceivedQuantity] ,
				[ReferenceNumber] ,
				[ReturnPRDetail] ,
				[ServicePartsAndMaterial] ,
				[Site] ,
				[StockNumber] ,
				[TitleRegistrationFee] ,
				[TotalAmount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalVolume] ,
				[TotalWeight] ,
				[TransactionAmount] ,
				[UnitCost] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPRDetailFromVendor] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPRFromVendor @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[PRNumber] ,
				[PONumber] ,
				[Owner] ,
				[APVoucherNumber] ,
				[AssignLandedCost] ,
				[AutoInvoiced] ,
				[DealerCode] ,
				[DeliveryOrderDate] ,
				[DeliveryOrderNumber] ,
				[EventData] ,
				[EventData2] ,
				[GrandTotal] ,
				[Handling] ,
				[LoadData] ,
				[PackingSlipDate] ,
				[PackingSlipNumber] ,
				[PRReferenceRequired] ,
				[ReturnPRNumber] ,
				[State] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTax1Amount] ,
				[TotalConsumptionTax2Amount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalTitleRegistrationFree] ,
				[TransactionDate] ,
				[TransferOrderRequestingNumber] ,
				[Type] ,
				[VendorDescription] ,
				[Vendor] ,
				[VendorInvoiceNumber] ,
				[WONumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPRFromVendor]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartPRFromVendorList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[PRNumber] ,
				[PONumber] ,
				[Owner] ,
				[APVoucherNumber] ,
				[AssignLandedCost] ,
				[AutoInvoiced] ,
				[DealerCode] ,
				[DeliveryOrderDate] ,
				[DeliveryOrderNumber] ,
				[EventData] ,
				[EventData2] ,
				[GrandTotal] ,
				[Handling] ,
				[LoadData] ,
				[PackingSlipDate] ,
				[PackingSlipNumber] ,
				[PRReferenceRequired] ,
				[ReturnPRNumber] ,
				[State] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTax1Amount] ,
				[TotalConsumptionTax2Amount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalTitleRegistrationFree] ,
				[TransactionDate] ,
				[TransferOrderRequestingNumber] ,
				[Type] ,
				[VendorDescription] ,
				[Vendor] ,
				[VendorInvoiceNumber] ,
				[WONumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartPRFromVendor] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartSalesOrder @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SalesChannel] ,
				[Owner] ,
				[Status] ,
				[DealerCode] ,
				[Customer] ,
				[CustomerNo] ,
				[DownPaymentAmount] ,
				[DownPaymentAmountReceived] ,
				[DownPaymentIsPaid] ,
				[ExternalReferenceNo] ,
				[GrandTotal] ,
				[Handling] ,
				[MethodOfPayment] ,
				[OrderType] ,
				[SalesOrderNo] ,
				[SalesPerson] ,
				[ShipmentType] ,
				[State] ,
				[TermOfPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalDiscountAmount] ,
				[TotalReceipt] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartSalesOrder]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartSalesOrderDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartSalesOrderID] ,
				[Owner] ,
				[Status] ,
				[AmountBeforeDiscount] ,
				[BaseAmount] ,
				[KodeDealer] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DiscountAmount] ,
				[DiscountPercentAge] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[PromiseDate] ,
				[QtyDelivered] ,
				[QtyOrder] ,
				[RequestDate] ,
				[SalesOrderDetailID] ,
				[SalesOrderNo] ,
				[SalesUnit] ,
				[Site] ,
				[TotalAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionAmount] ,
				[UnitPrice] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartSalesOrderDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartSalesOrderDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartSalesOrderID] ,
				[Owner] ,
				[Status] ,
				[AmountBeforeDiscount] ,
				[BaseAmount] ,
				[KodeDealer] ,
				[ConsumptionTax1Amount] ,
				[ConsumptionTax1] ,
				[ConsumptionTax2Amount] ,
				[ConsumptionTax2] ,
				[DiscountAmount] ,
				[DiscountPercentAge] ,
				[ProductCrossReference] ,
				[ProductDescription] ,
				[Product] ,
				[PromiseDate] ,
				[QtyDelivered] ,
				[QtyOrder] ,
				[RequestDate] ,
				[SalesOrderDetailID] ,
				[SalesOrderNo] ,
				[SalesUnit] ,
				[Site] ,
				[TotalAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TransactionAmount] ,
				[UnitPrice] ,
				[Warehouse] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartSalesOrderDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSparePartSalesOrderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SalesChannel] ,
				[Owner] ,
				[Status] ,
				[DealerCode] ,
				[Customer] ,
				[CustomerNo] ,
				[DownPaymentAmount] ,
				[DownPaymentAmountReceived] ,
				[DownPaymentIsPaid] ,
				[ExternalReferenceNo] ,
				[GrandTotal] ,
				[Handling] ,
				[MethodOfPayment] ,
				[OrderType] ,
				[SalesOrderNo] ,
				[SalesPerson] ,
				[ShipmentType] ,
				[State] ,
				[TermOfPayment] ,
				[TotalAmountBeforeDiscount] ,
				[TotalBaseAmount] ,
				[TotalConsumptionTaxAmount] ,
				[TotalDiscountAmount] ,
				[TotalReceipt] ,
				[TransactionDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SparePartSalesOrder] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSPKChassis @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SPKDetailID] ,
				[ChassisMasterID] ,
				[MatchingType] ,
				[MatchingDate] ,
				[MatchingNumber] ,
				[ReferenceNumber] ,
				[KeyNumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SPKChassis]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveSPKChassisList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SPKDetailID] ,
				[ChassisMasterID] ,
				[MatchingType] ,
				[MatchingDate] ,
				[MatchingNumber] ,
				[ReferenceNumber] ,
				[KeyNumber] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[SPKChassis] 

	   SET NOCOUNT OFF
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE up_RetrieveSPkChassisList_ByChNumber
	-- Add the parameters for the stored procedure here
	   @ChassisNumber AS VARCHAR(20)
AS
	   BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
			 SET NOCOUNT ON;
			 SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


			 SELECT	spk.[SPKNumber] ,
					sh.[SalesmanCode] ,
					sh.[Name] SalesmanName ,
					sl.[Description] SalesmanLevel ,
					jp.[Description] SalesmanPosition ,
					ISNULL(db.[DealerBranchCode] + ' / ' + db.[Term1], '') DealerBranch ,
					cr.[CustomerCode] CustomerCode ,
					cr.[ID] CustomerRequestID ,
					e.[Name1] CustomerName ,
					json = spk.[SPKNumber] + ';' + ISNULL(sh.[SalesmanCode], '') + ';' + ISNULL(sh.[Name], '') + ';'
					+ ISNULL(sl.[Description], '') + ';' + ISNULL(jp.[Description], '') + ';'
					+ ISNULL(db.[DealerBranchCode] + ' / ' + db.[Term1], '') + ';' + ISNULL(cr.[CustomerCode], '') + ';'
					+ ISNULL(CONVERT(VARCHAR(10), cr.[ID]), '')
			 FROM	dbo.[ChassisMaster] a
			 INNER JOIN dbo.[SPKChassis] b ON [b].[ChassisMasterID] = [a].[ID]
			 INNER JOIN dbo.[SPKDetail] c ON [c].[ID] = [b].[SPKDetailID]
			 INNER JOIN dbo.[v_SPKTersedia] spk ON c.[SPKHeaderID] = spk.[ID]
			 LEFT	 JOIN dbo.[SalesmanHeader] sh ON spk.SalesmanHeaderID = sh.[ID]
			 LEFT JOIN dbo.[SalesmanLevel] sl ON [sl].[ID] = [sh].[SalesmanLevelID]
			 LEFT JOIN dbo.[JobPosition] jp ON jp.[ID] = sh.[JobPositionId_Main]
			 LEFT JOIN dbo.[DealerBranch] db ON sh.[DealerBranchID] = db.[ID]
			 LEFT JOIN dbo.[SPKCustomer] e ON spk.[SPKCustomerID] = e.[ID]
											  AND e.[RowStatus] = 0
			 LEFT JOIN dbo.CustomerRequest cr ON cr.[ID] = spk.CustomerRequestID
												 AND cr.[RowStatus] = 0
			 LEFT JOIN dbo.Customer cu ON cu.[Code] = cr.[CustomerCode]
										  AND cu.[RowStatus] = 0
			 WHERE	1 = 1
					AND a.[RowStatus] = 0
					AND b.[RowStatus] = 0
					AND b.[MatchingType] = '1'
					AND spk.[RowStatus] = 0
					AND a.[ChassisNumber] = @ChassisNumber
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveStandardCodeChar @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Category] ,
				[ValueId] ,
				[ValueCode] ,
				[ValueDesc] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime] ,
				[Sequence]
	   FROM		[dbo].[StandardCodeChar]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveStandardCodeCharList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Category] ,
				[ValueId] ,
				[ValueCode] ,
				[ValueDesc] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime] ,
				[Sequence]
	   FROM		[dbo].[StandardCodeChar] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTestCustomer @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[CustomerCode] ,
				[CustomerName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TestCustomer]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTestCustomerList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[CustomerCode] ,
				[CustomerName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TestCustomer] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTestStatus1 @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[Name] ,
				[Description] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TestStatus1]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTestStatus1List
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[Name] ,
				[Description] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TestStatus1] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPBlockStatus @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartPOStatusID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPBlockStatus]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPBlockStatusList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartPOStatusID] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPBlockStatus] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPCreditAccount @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[DealerID] ,
				[TermOfPaymentID] ,
				[KelipatanPembayaran] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPCreditAccount]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPCreditAccountList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[DealerID] ,
				[TermOfPaymentID] ,
				[KelipatanPembayaran] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPCreditAccount] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPDeposit @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartBillingID] ,
				[AmountC2] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPDeposit]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPDepositList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartBillingID] ,
				[AmountC2] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPDeposit] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPDueDate @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[SparePartBillingID] ,
				[DueDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPDueDate]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPDueDateList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[SparePartBillingID] ,
				[DueDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPDueDate] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferActual @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[TOPSPTransferPaymentID] ,
				[RefTransferBank] ,
				[Amount] ,
				[PostingDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferActual]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferActualList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[TOPSPTransferPaymentID] ,
				[RefTransferBank] ,
				[Amount] ,
				[PostingDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferActual] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferCeiling @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[CreditAccount] ,
				[ProductCategoryID] ,
				[PaymentType] ,
				[EffectiveDate] ,
				[BalanceBefore] ,
				[AvailableCeiling] ,
				[LastSyncDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[TOPSPTransferCeiling]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferCeilingDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[TOPSPTransferCeilingID] ,
				[SparepartBillingID] ,
				[TOPSPTransferPaymentID] ,
				[Amount] ,
				[IsIncome] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[TOPSPTransferCeilingDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferCeilingDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[TOPSPTransferCeilingID] ,
				[SparepartBillingID] ,
				[TOPSPTransferPaymentID] ,
				[Amount] ,
				[IsIncome] ,
				[Status] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[TOPSPTransferCeilingDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferCeilingList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[CreditAccount] ,
				[ProductCategoryID] ,
				[PaymentType] ,
				[EffectiveDate] ,
				[BalanceBefore] ,
				[AvailableCeiling] ,
				[LastSyncDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdatedBy] ,
				[LastUpdatedTime]
	   FROM		[dbo].[TOPSPTransferCeiling] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferPayment @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[DealerID] ,
				[CreditAccount] ,
				[RegNumber] ,
				[DueDate] ,
				[PaymentPurposeID] ,
				[TransferPlanDate] ,
				[BankID] ,
				[TOPSPTransferPaymentIDReff] ,
				[IsAccelerated] ,
				[Status] ,
				[ValidatedBy] ,
				[ValidatedTime] ,
				[ConfirmedBy] ,
				[ConfirmedTime] ,
				[CanceledBy] ,
				[CanceledTime] ,
				[TransferAmount] ,
				[TransferActualDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferPayment]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferPaymentDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[TOPSPTransferPaymentID] ,
				[SparePartBillingID] ,
				[Amount] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferPaymentDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferPaymentDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[TOPSPTransferPaymentID] ,
				[SparePartBillingID] ,
				[Amount] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferPaymentDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveTOPSPTransferPaymentList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[DealerID] ,
				[CreditAccount] ,
				[RegNumber] ,
				[DueDate] ,
				[PaymentPurposeID] ,
				[TransferPlanDate] ,
				[TOPSPTransferPaymentIDReff] ,
				[IsAccelerated] ,
				[Status] ,
				[ValidatedBy] ,
				[ValidatedTime] ,
				[ConfirmedBy] ,
				[ConfirmedTime] ,
				[CanceledBy] ,
				[CanceledTime] ,
				[TransferAmount] ,
				[TransferActualDate] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[TOPSPTransferPayment] 

	   SET NOCOUNT OFF
GO

CREATE PROCEDURE up_RetrieveTOPSPTransferPaymentMultiPercepatan @id INTEGER
AS
	   BEGIN
	
			 SET NOCOUNT ON;

			 WITH	AreasCTE
					  AS (
						   SELECT	*
						   FROM		dbo.TOPSPTransferPayment
						   WHERE	id = @id
						   UNION ALL
						   SELECT	a.*
						   FROM		dbo.TOPSPTransferPayment a
						   INNER JOIN AreasCTE s ON a.id = s.topsptransferpaymentIDreff
						 )
				  SELECT	*
				  FROM		AreasCTE
				  ORDER BY	ID 

	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVehiclePurchaseDetail @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[VehiclePurchaseHeaderID] ,
				[BUCode] ,
				[BUName] ,
				[CloseLine] ,
				[CloseLineName] ,
				[CloseReason] ,
				[Completed] ,
				[CompletedName] ,
				[ProductDescription] ,
				[ProductName] ,
				[ProductVariantName] ,
				[PODetail] ,
				[POName] ,
				[PRDetailName] ,
				[PurchaseUnitName] ,
				[QtyOrder] ,
				[QtyReceipt] ,
				[QtyReturn] ,
				[RecallProduct] ,
				[RecallProductName] ,
				[SODetailName] ,
				[ScheduledShippingDate] ,
				[ServicePartsAndMaterial] ,
				[ShippingDate] ,
				[Site] ,
				[StockNumberName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[VehiclePurchaseDetail]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVehiclePurchaseDetailList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[VehiclePurchaseHeaderID] ,
				[BUCode] ,
				[BUName] ,
				[CloseLine] ,
				[CloseLineName] ,
				[CloseReason] ,
				[Completed] ,
				[CompletedName] ,
				[ProductDescription] ,
				[ProductName] ,
				[ProductVariantName] ,
				[PODetail] ,
				[POName] ,
				[PRDetailName] ,
				[PurchaseUnitName] ,
				[QtyOrder] ,
				[QtyReceipt] ,
				[QtyReturn] ,
				[RecallProduct] ,
				[RecallProductName] ,
				[SODetailName] ,
				[ScheduledShippingDate] ,
				[ServicePartsAndMaterial] ,
				[ShippingDate] ,
				[Site] ,
				[StockNumberName] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[VehiclePurchaseDetail] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVehiclePurchaseHeader @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[BUCode] ,
				[BUName] ,
				[DeliveryMethod] ,
				[Description] ,
				[PRPOTypeName] ,
				[DMSPONo] ,
				[DMSPOStatus] ,
				[DMSPODate] ,
				[VendorDescription] ,
				[Vendor] ,
				[PurchaseOrderNo] ,
				[PurchaseReceiptNo] ,
				[PurchaseReceiptDetailNo] ,
				[ChassisModel] ,
				[ChassisNumberRegister] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[VehiclePurchaseHeader]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVehiclePurchaseHeaderList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[BUCode] ,
				[BUName] ,
				[DeliveryMethod] ,
				[Description] ,
				[PRPOTypeName] ,
				[DMSPONo] ,
				[DMSPOStatus] ,
				[DMSPODate] ,
				[VendorDescription] ,
				[Vendor] ,
				[PurchaseOrderNo] ,
				[PurchaseReceiptNo] ,
				[PurchaseReceiptDetailNo] ,
				[ChassisModel] ,
				[ChassisNumberRegister] ,
				[RowStatus] ,
				[CreatedBy] ,
				[CreatedTime] ,
				[LastUpdateBy] ,
				[LastUpdateTime]
	   FROM		[dbo].[VehiclePurchaseHeader] 

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVWI_BusinessSector @ID INT OUTPUT
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	   SET NOCOUNT ON

	   SELECT	[ID] ,
				[BusinessSectorName] ,
				[BusinessDomain] ,
				[BusinessName]--,
	--[Code]	
	   FROM		[dbo].[VWI_BusinessSector]
	   WHERE	[ID] = @ID

	   SET NOCOUNT OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_RetrieveVWI_BusinessSectorList
AS
	   SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	   SET NOCOUNT ON


	   SELECT	[ID] ,
				[BusinessSectorName] ,
				[BusinessDomain] ,
				[BusinessName]--,
		--[Code]		
	   FROM		[dbo].[VWI_BusinessSector] 

	   SET NOCOUNT OFF
GO

CREATE PROCEDURE up_Retrieve_OutStandingBilling
	   @DueDateStart DATE ,
	   @DueDateEnd DATE ,
	   @creacc AS VARCHAR(1000)
AS
	   BEGIN

			 SET NOCOUNT ON;

			 IF @creacc = ''
				BEGIN
					  SELECT	l.CreditAccount ,
								[BillingNumber] ,
								CASE WHEN DAY(d.DueDate) = 1 THEN 'D1'
									 WHEN DAY(d.DueDate) = 2 THEN 'D2'
									 WHEN DAY(d.DueDate) = 3 THEN 'D3'
									 WHEN DAY(d.DueDate) = 4 THEN 'D4'
									 WHEN DAY(d.DueDate) = 5 THEN 'D5'
									 WHEN DAY(d.DueDate) = 6 THEN 'D6'
									 WHEN DAY(d.DueDate) = 7 THEN 'D7'
									 WHEN DAY(d.DueDate) = 8 THEN 'D8'
									 WHEN DAY(d.DueDate) = 9 THEN 'D9'
									 WHEN DAY(d.DueDate) = 10 THEN 'D10'
									 WHEN DAY(d.DueDate) = 11 THEN 'D11'
									 WHEN DAY(d.DueDate) = 12 THEN 'D12'
									 WHEN DAY(d.DueDate) = 13 THEN 'D13'
									 WHEN DAY(d.DueDate) = 14 THEN 'D14'
									 WHEN DAY(d.DueDate) = 15 THEN 'D15'
									 WHEN DAY(d.DueDate) = 16 THEN 'D16'
									 WHEN DAY(d.DueDate) = 17 THEN 'D17'
									 WHEN DAY(d.DueDate) = 18 THEN 'D18'
									 WHEN DAY(d.DueDate) = 19 THEN 'D19'
									 WHEN DAY(d.DueDate) = 20 THEN 'D20'
									 WHEN DAY(d.DueDate) = 21 THEN 'D21'
									 WHEN DAY(d.DueDate) = 22 THEN 'D22'
									 WHEN DAY(d.DueDate) = 23 THEN 'D23'
									 WHEN DAY(d.DueDate) = 24 THEN 'D24'
									 WHEN DAY(d.DueDate) = 25 THEN 'D25'
									 WHEN DAY(d.DueDate) = 26 THEN 'D26'
									 WHEN DAY(d.DueDate) = 27 THEN 'D27'
									 WHEN DAY(d.DueDate) = 28 THEN 'D28'
									 WHEN DAY(d.DueDate) = 29 THEN 'D29'
									 WHEN DAY(d.DueDate) = 30 THEN 'D30'
									 WHEN DAY(d.DueDate) = 31 THEN 'D31'
								END AS duedated ,
								( t.AmountC2 + b.Tax + b.TotalAmount ) AS amount
					  INTO		#temp
					  FROM		[dbo].[SparePartBilling] b ( NOLOCK )
					  JOIN		TOPSPDueDate d ( NOLOCK ) ON d.SparePartBillingID = b.ID
					  JOIN		TOPSPDeposit t ( NOLOCK ) ON t.SparePartBillingID = b.ID
					  JOIN		Dealer l ( NOLOCK ) ON l.ID = b.DealerID
					  WHERE		b.RowStatus = 0
								AND d.DueDate BETWEEN @DueDateStart AND @DueDateEnd
								AND d.RowStatus = 0
								AND l.RowStatus = 0
								AND t.RowStatus = 0
								AND b.ID NOT IN (
								SELECT	SparePartBillingID
								FROM	TOPSPTransferPaymentDetail d1 ( NOLOCK )
								JOIN	topsptransferpayment (NOLOCK) h ON h.id = d1.TOPSPTransferPaymentID
								WHERE	d1.rowstatus = 0
										AND h.status NOT IN ( SELECT	ValueId
															  FROM		StandardCode c
															  WHERE		c.Category = 'EnumTOPSPTransferPayment.Status'
																		AND c.RowStatus = 0
																		AND c.ValueDesc LIKE '%Batal%' ) )

					  SELECT	CreditAccount ,
								BillingNumber ,
								ISNULL(D1, 0) AS D1 ,
								ISNULL(D2, 0) AS D2 ,
								ISNULL(D3, 0) AS D3 ,
								ISNULL(D4, 0) AS D4 ,
								ISNULL(D5, 0) AS D5 ,
								ISNULL(D6, 0) AS D6 ,
								ISNULL(D7, 0) AS D7 ,
								ISNULL(D8, 0) AS D8 ,
								ISNULL(D9, 0) AS D9 ,
								ISNULL(D10, 0) AS D10 ,
								ISNULL(D11, 0) AS D11 ,
								ISNULL(D12, 0) AS D12 ,
								ISNULL(D13, 0) AS D13 ,
								ISNULL(D14, 0) AS D14 ,
								ISNULL(D15, 0) AS D15 ,
								ISNULL(D16, 0) AS D16 ,
								ISNULL(D17, 0) AS D17 ,
								ISNULL(D18, 0) AS D18 ,
								ISNULL(D19, 0) AS D19 ,
								ISNULL(D20, 0) AS D20 ,
								ISNULL(D21, 0) AS D21 ,
								ISNULL(D22, 0) AS D22 ,
								ISNULL(D23, 0) AS D23 ,
								ISNULL(D24, 0) AS D24 ,
								ISNULL(D25, 0) AS D25 ,
								ISNULL(D26, 0) AS D26 ,
								ISNULL(D27, 0) AS D27 ,
								ISNULL(D28, 0) AS D28 ,
								ISNULL(D29, 0) AS D29 ,
								ISNULL(D30, 0) AS D30 ,
								ISNULL(D31, 0) AS D31
					  FROM		#temp PIVOT
		( AVG(amount) FOR duedated IN ( D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18,
										D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31 ) )AS pivottable

				END
			 ELSE
				BEGIN
					  SELECT	l.CreditAccount ,
								[BillingNumber] ,
								CASE WHEN DAY(d.DueDate) = 1 THEN 'D1'
									 WHEN DAY(d.DueDate) = 2 THEN 'D2'
									 WHEN DAY(d.DueDate) = 3 THEN 'D3'
									 WHEN DAY(d.DueDate) = 4 THEN 'D4'
									 WHEN DAY(d.DueDate) = 5 THEN 'D5'
									 WHEN DAY(d.DueDate) = 6 THEN 'D6'
									 WHEN DAY(d.DueDate) = 7 THEN 'D7'
									 WHEN DAY(d.DueDate) = 8 THEN 'D8'
									 WHEN DAY(d.DueDate) = 9 THEN 'D9'
									 WHEN DAY(d.DueDate) = 10 THEN 'D10'
									 WHEN DAY(d.DueDate) = 11 THEN 'D11'
									 WHEN DAY(d.DueDate) = 12 THEN 'D12'
									 WHEN DAY(d.DueDate) = 13 THEN 'D13'
									 WHEN DAY(d.DueDate) = 14 THEN 'D14'
									 WHEN DAY(d.DueDate) = 15 THEN 'D15'
									 WHEN DAY(d.DueDate) = 16 THEN 'D16'
									 WHEN DAY(d.DueDate) = 17 THEN 'D17'
									 WHEN DAY(d.DueDate) = 18 THEN 'D18'
									 WHEN DAY(d.DueDate) = 19 THEN 'D19'
									 WHEN DAY(d.DueDate) = 20 THEN 'D20'
									 WHEN DAY(d.DueDate) = 21 THEN 'D21'
									 WHEN DAY(d.DueDate) = 22 THEN 'D22'
									 WHEN DAY(d.DueDate) = 23 THEN 'D23'
									 WHEN DAY(d.DueDate) = 24 THEN 'D24'
									 WHEN DAY(d.DueDate) = 25 THEN 'D25'
									 WHEN DAY(d.DueDate) = 26 THEN 'D26'
									 WHEN DAY(d.DueDate) = 27 THEN 'D27'
									 WHEN DAY(d.DueDate) = 28 THEN 'D28'
									 WHEN DAY(d.DueDate) = 29 THEN 'D29'
									 WHEN DAY(d.DueDate) = 30 THEN 'D30'
									 WHEN DAY(d.DueDate) = 31 THEN 'D31'
								END AS duedated ,
								( t.AmountC2 + b.Tax + b.TotalAmount ) AS amount
					  INTO		#temp1
					  FROM		[dbo].[SparePartBilling] b ( NOLOCK )
					  JOIN		TOPSPDueDate d ( NOLOCK ) ON d.SparePartBillingID = b.ID
					  JOIN		TOPSPDeposit t ( NOLOCK ) ON t.SparePartBillingID = b.ID
					  JOIN		Dealer l ( NOLOCK ) ON l.ID = b.DealerID
					  WHERE		b.RowStatus = 0
								AND d.DueDate BETWEEN @DueDateStart AND @DueDateEnd
								AND d.RowStatus = 0
								AND l.RowStatus = 0
								AND t.RowStatus = 0
								AND l.CreditAccount = @creacc
								AND b.ID NOT IN (
								SELECT	SparePartBillingID
								FROM	TOPSPTransferPaymentDetail d1 ( NOLOCK )
								JOIN	topsptransferpayment (NOLOCK) h ON h.id = d1.TOPSPTransferPaymentID
								WHERE	d1.rowstatus = 0
										AND h.status NOT IN ( SELECT	ValueId
															  FROM		StandardCode c
															  WHERE		c.Category = 'EnumTOPSPTransferPayment.Status'
																		AND c.RowStatus = 0
																		AND c.ValueDesc LIKE '%Batal%' ) )

					  SELECT	CreditAccount ,
								BillingNumber ,
								ISNULL(D1, 0) AS D1 ,
								ISNULL(D2, 0) AS D2 ,
								ISNULL(D3, 0) AS D3 ,
								ISNULL(D4, 0) AS D4 ,
								ISNULL(D5, 0) AS D5 ,
								ISNULL(D6, 0) AS D6 ,
								ISNULL(D7, 0) AS D7 ,
								ISNULL(D8, 0) AS D8 ,
								ISNULL(D9, 0) AS D9 ,
								ISNULL(D10, 0) AS D10 ,
								ISNULL(D11, 0) AS D11 ,
								ISNULL(D12, 0) AS D12 ,
								ISNULL(D13, 0) AS D13 ,
								ISNULL(D14, 0) AS D14 ,
								ISNULL(D15, 0) AS D15 ,
								ISNULL(D16, 0) AS D16 ,
								ISNULL(D17, 0) AS D17 ,
								ISNULL(D18, 0) AS D18 ,
								ISNULL(D19, 0) AS D19 ,
								ISNULL(D20, 0) AS D20 ,
								ISNULL(D21, 0) AS D21 ,
								ISNULL(D22, 0) AS D22 ,
								ISNULL(D23, 0) AS D23 ,
								ISNULL(D24, 0) AS D24 ,
								ISNULL(D25, 0) AS D25 ,
								ISNULL(D26, 0) AS D26 ,
								ISNULL(D27, 0) AS D27 ,
								ISNULL(D28, 0) AS D28 ,
								ISNULL(D29, 0) AS D29 ,
								ISNULL(D30, 0) AS D30 ,
								ISNULL(D31, 0) AS D31
					  FROM		#temp1 PIVOT
		( AVG(amount) FOR duedated IN ( D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18,
										D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31 ) )AS pivottable
				END
	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateAPPayment
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentNo VARCHAR(50) ,
	   @APReferenceNo VARCHAR(100) ,
	   @APVoucherReferenceNo VARCHAR(100) ,
	   @AppliedToDocument MONEY ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @State SMALLINT ,
	   @TotalChangeAmount MONEY ,
	   @TotalPaymentAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @Type SMALLINT ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[APPayment]
	   SET		[Owner] = @Owner ,
				[APPaymentNo] = @APPaymentNo ,
				[APReferenceNo] = @APReferenceNo ,
				[APVoucherReferenceNo] = @APVoucherReferenceNo ,
				[AppliedToDocument] = @AppliedToDocument ,
				[BU] = @BU ,
				[Cancelled] = @Cancelled ,
				[CashAndBank] = @CashAndBank ,
				[MethodOfPayment] = @MethodOfPayment ,
				[AvailableBalance] = @AvailableBalance ,
				[State] = @State ,
				[TotalChangeAmount] = @TotalChangeAmount ,
				[TotalPaymentAmount] = @TotalPaymentAmount ,
				[TransactionDate] = @TransactionDate ,
				[Type] = @Type ,
				[VendorDescription] = @VendorDescription ,
				[Vendor] = @Vendor ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateAPPaymentDetail
	   @ID INT OUTPUT ,
	   @APPaymentID INT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentDetailNo VARCHAR(100) ,
	   @APPaymentNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @ExternalDocumentNo VARCHAR(50) ,
	   @ExternalDocumentType SMALLINT ,
	   @APVoucherNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNoNVSOReferral VARCHAR(100) ,
	   @OrderNoOutsourceWorkOrder VARCHAR(100) ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoUVSOReferral VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaymentAmount MONEY ,
	   @PaymentSlipNo VARCHAR(50) ,
	   @ReceiptFromVendor BIT ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[APPaymentDetail]
	   SET		[APPaymentID] = @APPaymentID ,
				[Owner] = @Owner ,
				[APPaymentDetailNo] = @APPaymentDetailNo ,
				[APPaymentNo] = @APPaymentNo ,
				[BU] = @BU ,
				[ChangeAmount] = @ChangeAmount ,
				[Description] = @Description ,
				[DifferenceValue] = @DifferenceValue ,
				[ExternalDocumentNo] = @ExternalDocumentNo ,
				[ExternalDocumentType] = @ExternalDocumentType ,
				[APVoucherNo] = @APVoucherNo ,
				[OrderDate] = @OrderDate ,
				[OrderNoNVSOReferral] = @OrderNoNVSOReferral ,
				[OrderNoOutsourceWorkOrder] = @OrderNoOutsourceWorkOrder ,
				[OrderNo] = @OrderNo ,
				[OrderNoUVSOReferral] = @OrderNoUVSOReferral ,
				[OutstandingBalance] = @OutstandingBalance ,
				[PaymentAmount] = @PaymentAmount ,
				[PaymentSlipNo] = @PaymentSlipNo ,
				[ReceiptFromVendor] = @ReceiptFromVendor ,
				[RemainingBalance] = @RemainingBalance ,
				[SourceType] = @SourceType ,
				[TransactionDocument] = @TransactionDocument ,
				[Vendor] = @Vendor ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateARReceipt
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @GeneratedToken VARCHAR(36) ,
	   @ARInvoiceReferenceNo VARCHAR(100) ,
	   @ARReceiptNo VARCHAR(50) ,
	   @ARReceiptReferenceNo VARCHAR(100) ,
	   @Type SMALLINT ,
	   @BookingFee BIT ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(100) ,
	   @EndOrderDate DATETIME ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @StartOrderDate DATETIME ,
	   @State SMALLINT ,
	   @AppliedToDocument MONEY ,
	   @TotalAmountBase MONEY ,
	   @TotalChangeAmount MONEY ,
	   @TotalOutstandingBalanceBase MONEY ,
	   @TotalReceiptAmount MONEY ,
	   @TotalRemainingBalanceBase MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[ARReceipt]
	   SET		[Owner] = @Owner ,
				[GeneratedToken] = @GeneratedToken ,
				[ARInvoiceReferenceNo] = @ARInvoiceReferenceNo ,
				[ARReceiptNo] = @ARReceiptNo ,
				[ARReceiptReferenceNo] = @ARReceiptReferenceNo ,
				[Type] = @Type ,
				[BookingFee] = @BookingFee ,
				[BU] = @BU ,
				[Cancelled] = @Cancelled ,
				[CashAndBank] = @CashAndBank ,
				[Customer] = @Customer ,
				[CustomerNo] = @CustomerNo ,
				[EndOrderDate] = @EndOrderDate ,
				[MethodOfPayment] = @MethodOfPayment ,
				[AvailableBalance] = @AvailableBalance ,
				[StartOrderDate] = @StartOrderDate ,
				[State] = @State ,
				[AppliedToDocument] = @AppliedToDocument ,
				[TotalAmountBase] = @TotalAmountBase ,
				[TotalChangeAmount] = @TotalChangeAmount ,
				[TotalOutstandingBalanceBase] = @TotalOutstandingBalanceBase ,
				[TotalReceiptAmount] = @TotalReceiptAmount ,
				[TotalRemainingBalanceBase] = @TotalRemainingBalanceBase ,
				[TransactionDate] = @TransactionDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateARReceiptDetail
	   @ID INT OUTPUT ,
	   @ARReceiptID INT ,
	   @Owner VARCHAR(100) ,
	   @DetailNo VARCHAR(50) ,
	   @ARReceiptNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Customer VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @InvoiceNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoSO VARCHAR(100) ,
	   @OrderNoUVSO VARCHAR(100) ,
	   @OrderNoWO VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaidBackToCustomer BIT ,
	   @ReceiptAmount MONEY ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[ARReceiptDetail]
	   SET		[ARReceiptID] = @ARReceiptID ,
				[Owner] = @Owner ,
				[DetailNo] = @DetailNo ,
				[ARReceiptNo] = @ARReceiptNo ,
				[BU] = @BU ,
				[ChangeAmount] = @ChangeAmount ,
				[Customer] = @Customer ,
				[Description] = @Description ,
				[DifferenceValue] = @DifferenceValue ,
				[InvoiceNo] = @InvoiceNo ,
				[OrderDate] = @OrderDate ,
				[OrderNo] = @OrderNo ,
				[OrderNoSO] = @OrderNoSO ,
				[OrderNoUVSO] = @OrderNoUVSO ,
				[OrderNoWO] = @OrderNoWO ,
				[OutstandingBalance] = @OutstandingBalance ,
				[PaidBackToCustomer] = @PaidBackToCustomer ,
				[ReceiptAmount] = @ReceiptAmount ,
				[RemainingBalance] = @RemainingBalance ,
				[SourceType] = @SourceType ,
				[TransactionDocument] = @TransactionDocument ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateBusinessSectorDetail
	   @ID INT OUTPUT ,
	   @BusinessSectorHeaderID INT ,
	   @BusinessDomain VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[BusinessSectorDetail]
	   SET		[BusinessSectorHeaderID] = @BusinessSectorHeaderID ,
				[BusinessDomain] = @BusinessDomain ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateBusinessSectorHeader
	   @ID INT OUTPUT ,
	   @BusinessSectorName VARCHAR(500) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[BusinessSectorHeader]
	   SET		[BusinessSectorName] = @BusinessSectorName ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateCarrosserieDetail
	   @ID INT OUTPUT ,
	   @CarrosserieHeaderID INT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @AccessorriesDescription VARCHAR(100) ,
	   @AccessorriesName VARCHAR(100) ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @KITName VARCHAR(100) ,
	   @PBUCode VARCHAR(20) ,
	   @PBUName VARCHAR(100) ,
	   @PDIDetailName VARCHAR(100) ,
	   @PDIReceiptDetailNo VARCHAR(50) ,
	   @PDIReceiptName VARCHAR(100) ,
	   @ReceiveQuantity FLOAT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[CarrosserieDetail]
	   SET		[CarrosserieHeaderID] = @CarrosserieHeaderID ,
				[PDIStateCode] = @PDIStateCode ,
				[PDIStatusCode] = @PDIStatusCode ,
				[AccessorriesDescription] = @AccessorriesDescription ,
				[AccessorriesName] = @AccessorriesName ,
				[BUCode] = @BUCode ,
				[BUName] = @BUName ,
				[KITName] = @KITName ,
				[PBUCode] = @PBUCode ,
				[PBUName] = @PBUName ,
				[PDIDetailName] = @PDIDetailName ,
				[PDIReceiptDetailNo] = @PDIReceiptDetailNo ,
				[PDIReceiptName] = @PDIReceiptName ,
				[ReceiveQuantity] = @ReceiveQuantity ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateCarrosserieHeader
	   @ID INT OUTPUT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @BUCode VARCHAR(50) ,
	   @BUName VARCHAR(100) ,
	   @PDIName VARCHAR(100) ,
	   @PDIReceiptNo VARCHAR(50) ,
	   @PDIReceiptRefName VARCHAR(100) ,
	   @PDIReceiptStatus SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @VendorName VARCHAR(100) ,
	   @ChassisNumber VARCHAR(20) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[CarrosserieHeader]
	   SET		[PDIStateCode] = @PDIStateCode ,
				[PDIStatusCode] = @PDIStatusCode ,
				[BUCode] = @BUCode ,
				[BUName] = @BUName ,
				[PDIName] = @PDIName ,
				[PDIReceiptNo] = @PDIReceiptNo ,
				[PDIReceiptRefName] = @PDIReceiptRefName ,
				[PDIReceiptStatus] = @PDIReceiptStatus ,
				[TransactionDate] = @TransactionDate ,
				[TransactionType] = @TransactionType ,
				[VendorName] = @VendorName ,
				[ChassisNumber] = @ChassisNumber ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateCustomerGroup
	   @ID INT OUTPUT ,
	   @Code VARCHAR(20) ,
	   @Name VARCHAR(150) ,
	   @Description NVARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[CustomerGroup]
	   SET		[Code] = @Code ,
				[Name] = @Name ,
				[Description] = @Description ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateCustomerRequestOCR
	   @ID INT OUTPUT ,
	   @CustomerRequestID INT ,
	   @OCRIdentityID INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[CustomerRequestOCR]
	   SET		[CustomerRequestID] = @CustomerRequestID ,
				[OCRIdentityID] = @OCRIdentityID ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateDealerSystems
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @SystemID INT ,
	   @isSPKMatchFaktur BIT ,
	   @isOnlyUploadPhotoTenagaPenjual BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[DealerSystems]
	   SET		[DealerID] = @DealerID ,
				[SystemID] = @SystemID ,
				[isSPKMatchFaktur] = @isSPKMatchFaktur ,
				[isOnlyUploadPhotoTenagaPenjual] = @isOnlyUploadPhotoTenagaPenjual ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateDMSWOWarrantyClaim
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @DealerBranchID INT ,
	   @ChassisNumber VARCHAR(20) ,
	   @isBB BIT ,
	   @WorkOrderNumber VARCHAR(50) ,
	   @FailureDate DATETIME ,
	   @ServiceDate DATETIME ,
	   @Owner VARCHAR(50) ,
	   @Mileage INT ,
	   @ServiceBuletin VARCHAR(50) ,
	   @Symptoms VARCHAR(1000) ,
	   @Causes VARCHAR(1000) ,
	   @Results VARCHAR(1000) ,
	   @Notes VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreateBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[DMSWOWarrantyClaim]
	   SET		[DealerID] = @DealerID ,
				[DealerBranchID] = @DealerBranchID ,
				[ChassisNumber] = @ChassisNumber ,
				[isBB] = @isBB ,
				[WorkOrderNumber] = @WorkOrderNumber ,
				[FailureDate] = @FailureDate ,
				[ServiceDate] = @ServiceDate ,
				[Owner] = @Owner ,
				[Mileage] = @Mileage ,
				[ServiceBuletin] = @ServiceBuletin ,
				[Symptoms] = @Symptoms ,
				[Causes] = @Causes ,
				[Results] = @Results ,
				[Notes] = @Notes ,
				[RowStatus] = @RowStatus ,
				[CreateBy] = @CreateBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateFleet
	   @ID INT OUTPUT ,
	   @FleetCode VARCHAR(50) ,
	   @FleetName VARCHAR(100) ,
	   @FleetNickName VARCHAR(100) ,
	   @FleetGroup VARCHAR(100) ,
	   @Address VARCHAR(255) ,
	   @ProvinceId INT ,
	   @CityId SMALLINT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(50) ,
	   @BusinessSectorHeaderId INT ,
	   @BusinessSectorDetailId INT ,
	   @FleetNote VARCHAR(MAX) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[Fleet]
	   SET		[FleetCode] = @FleetCode ,
				[FleetName] = @FleetName ,
				[FleetNickName] = @FleetNickName ,
				[FleetGroup] = @FleetGroup ,
				[Address] = @Address ,
				[ProvinceId] = @ProvinceId ,
				[CityId] = @CityId ,
				[IdentityType] = @IdentityType ,
				[IdentityNumber] = @IdentityNumber ,
				[BusinessSectorHeaderId] = @BusinessSectorHeaderId ,
				[BusinessSectorDetailId] = @BusinessSectorDetailId ,
				[FleetNote] = @FleetNote ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateFleetCustomer
	   @ID INT OUTPUT ,
	   @CustomerGroupID INT ,
	   @ProvinceID INT ,
	   @PreArea VARCHAR(50) ,
	   @CityID SMALLINT ,
	   @BusinessSectorDetailId INT ,
	   @RatioMatrixID INT ,
	   @CategoryIndex INT ,
	   @TypeIndex INT ,
	   @Code VARCHAR(30) ,
	   @Name VARCHAR(50) ,
	   @Gedung VARCHAR(50) ,
	   @Alamat VARCHAR(150) ,
	   @Kecamatan VARCHAR(75) ,
	   @Kelurahan VARCHAR(75) ,
	   @PostalCode VARCHAR(10) ,
	   @Email NVARCHAR(50) ,
	   @PhoneNo VARCHAR(15) ,
	   @TipeCustomer INT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(30) ,
	   @Attachment VARCHAR(100) ,
	   @ClassificationIndex INT ,
	   @FleetNickName VARCHAR(50) ,
	   @FleetNote VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   UPDATE	[dbo].[FleetCustomer]
	   SET		[CustomerGroupID] = @CustomerGroupID ,
				[ProvinceID] = @ProvinceID ,
				[PreArea] = @PreArea ,
				[CityID] = @CityID ,
				[BusinessSectorDetailId] = @BusinessSectorDetailId ,
				[RatioMatrixID] = @RatioMatrixID ,
				[CategoryIndex] = @CategoryIndex ,
				[TypeIndex] = @TypeIndex ,
				[Code] = @Code ,
				[Name] = @Name ,
				[Gedung] = @Gedung ,
				[Alamat] = @Alamat ,
				[Kecamatan] = @Kecamatan ,
				[Kelurahan] = @Kelurahan ,
				[PostalCode] = @PostalCode ,
				[Email] = @Email ,
				[PhoneNo] = @PhoneNo ,
				[TipeCustomer] = @TipeCustomer ,
				[IdentityType] = @IdentityType ,
				[IdentityNumber] = @IdentityNumber ,
				[Attachment] = @Attachment ,
				[ClassificationIndex] = @ClassificationIndex ,
				[FleetNickName] = @FleetNickName ,
				[FleetNote] = @FleetNote ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateFleetCustomerContact
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @Name VARCHAR(50) ,
	   @Position VARCHAR(50) ,
	   @PhoneNo VARCHAR(20) ,
	   @Handphone VARCHAR(20) ,
	   @Email NVARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   UPDATE	[dbo].[FleetCustomerContact]
	   SET		[FleetCustomerID] = @FleetCustomerID ,
				[Name] = @Name ,
				[Position] = @Position ,
				[PhoneNo] = @PhoneNo ,
				[Handphone] = @Handphone ,
				[Email] = @Email ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateFleetCustomerToCustomer
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @CustomerID INT ,
	   @IsDefault BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   UPDATE	[dbo].[FleetCustomerToCustomer]
	   SET		[FleetCustomerID] = @FleetCustomerID ,
				[CustomerID] = @CustomerID ,
				[IsDefault] = @IsDefault ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateFleetCustomerToDealer
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @DealerID SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(50)
AS
	   UPDATE	[dbo].[FleetCustomerToDealer]
	   SET		[FleetCustomerID] = @FleetCustomerID ,
				[DealerID] = @DealerID ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateIndustrialSector
	   @ID INT OUTPUT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[IndustrialSector]
	   SET		[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateInventoryTransaction
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @PersonInCharge VARCHAR(100) ,
	   @ProcessCode VARCHAR(10) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[InventoryTransaction]
	   SET		[Owner] = @Owner ,
				[DealerCode] = @DealerCode ,
				[InventoryTransactionNo] = @InventoryTransactionNo ,
				[InventoryTransferNo] = @InventoryTransferNo ,
				[PersonInCharge] = @PersonInCharge ,
				[ProcessCode] = @ProcessCode ,
				[SourceData] = @SourceData ,
				[State] = @State ,
				[TransactionDate] = @TransactionDate ,
				[TransactionType] = @TransactionType ,
				[WONo] = @WONo ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateInventoryTransactionDetail
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @FromBU VARCHAR(100) ,
	   @InventoryTransactionID INT ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferDetail VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @ReasonCode VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @RegisterSerialNumber VARCHAR(100) ,
	   @RunningNumber INT ,
	   @SerialNo VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @SourceData VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @TotalCost MONEY ,
	   @TransactionType VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @UnitCost MONEY ,
	   @VIN VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[InventoryTransactionDetail]
	   SET		[Owner] = @Owner ,
				[BaseQuantity] = @BaseQuantity ,
				[BatchNo] = @BatchNo ,
				[BU] = @BU ,
				[Department] = @Department ,
				[Description] = @Description ,
				[FromBU] = @FromBU ,
				[InventoryTransactionID] = @InventoryTransactionID ,
				[InventoryTransactionNo] = @InventoryTransactionNo ,
				[InventoryTransferDetail] = @InventoryTransferDetail ,
				[InventoryUnit] = @InventoryUnit ,
				[Location] = @Location ,
				[ProductCrossReference] = @ProductCrossReference ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[Quantity] = @Quantity ,
				[ReasonCode] = @ReasonCode ,
				[ReferenceNo] = @ReferenceNo ,
				[RegisterSerialNumber] = @RegisterSerialNumber ,
				[RunningNumber] = @RunningNumber ,
				[SerialNo] = @SerialNo ,
				[ServicePartsandMaterial] = @ServicePartsAndMaterial ,
				[Site] = @Site ,
				[SourceData] = @SourceData ,
				[StockNumber] = @StockNumber ,
				[StockNumberNV] = @StockNumberNV ,
				[TotalCost] = @TotalCost ,
				[TransactionType] = @TransactionType ,
				[TransactionUnit] = @TransactionUnit ,
				[UnitCost] = @UnitCost ,
				[VIN] = @VIN ,
				[Warehouse] = @Warehouse ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateInventoryTransfer
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(50) ,
	   @ItemTypeForTransfer SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @ReceiptDate DATETIME ,
	   @ReceiptNo VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @SearchVehicle VARCHAR(50) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @ToDealer VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @TransferStatus SMALLINT ,
	   @TransferStep BIT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[InventoryTransfer]
	   SET		[Owner] = @Owner ,
				[FromDealer] = @FromDealer ,
				[FromSite] = @FromSite ,
				[InventoryTransferNo] = @InventoryTransferNo ,
				[ItemTypeForTransfer] = @ItemTypeForTransfer ,
				[PersonInCharge] = @PersonInCharge ,
				[ReceiptDate] = @ReceiptDate ,
				[ReceiptNo] = @ReceiptNo ,
				[ReferenceNo] = @ReferenceNo ,
				[SearchVehicle] = @SearchVehicle ,
				[SourceData] = @SourceData ,
				[State] = @State ,
				[ToDealer] = @ToDealer ,
				[ToSite] = @ToSite ,
				[TransactionDate] = @TransactionDate ,
				[TransactionType] = @TransactionType ,
				[TransferStatus] = @TransferStatus ,
				[TransferStep] = @TransferStep ,
				[WONo] = @WONo ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateInventoryTransferDetail
	   @ID INT OUTPUT ,
	   @InventoryTransferID INT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @ConsumptionTaxIn VARCHAR(100) ,
	   @ConsumptionTaxOut VARCHAR(100) ,
	   @FromBatchNo VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromConfiguration VARCHAR(100) ,
	   @FromExteriorColor VARCHAR(100) ,
	   @FromInteriorColor VARCHAR(100) ,
	   @FromLocation VARCHAR(100) ,
	   @FromSerialNo VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @FromStyle VARCHAR(100) ,
	   @FromWarehouse VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @Remarks VARCHAR(100) ,
	   @ServicePartsandMaterial VARCHAR(100) ,
	   @SourceData VARCHAR(50) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @StockNumberLookupName VARCHAR(200) ,
	   @StockNumberLookupType INT ,
	   @ToBatchNo VARCHAR(100) ,
	   @ToDealer VARCHAR(100) ,
	   @ToConfiguration VARCHAR(100) ,
	   @ToExteriorColor VARCHAR(100) ,
	   @ToInteriorColor VARCHAR(100) ,
	   @ToLocation VARCHAR(100) ,
	   @ToSerialNo VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @ToStyle VARCHAR(100) ,
	   @ToWarehouse VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @VIN VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[InventoryTransferDetail]
	   SET		[InventoryTransferID] = @InventoryTransferID ,
				[Owner] = @Owner ,
				[BaseQuantity] = @BaseQuantity ,
				[ConsumptionTaxIn] = @ConsumptionTaxIn ,
				[ConsumptionTaxOut] = @ConsumptionTaxOut ,
				[FromBatchNo] = @FromBatchNo ,
				[FromDealer] = @FromDealer ,
				[FromConfiguration] = @FromConfiguration ,
				[FromExteriorColor] = @FromExteriorColor ,
				[FromInteriorColor] = @FromInteriorColor ,
				[FromLocation] = @FromLocation ,
				[FromSerialNo] = @FromSerialNo ,
				[FromSite] = @FromSite ,
				[FromStyle] = @FromStyle ,
				[FromWarehouse] = @FromWarehouse ,
				[InventoryTransferNo] = @InventoryTransferNo ,
				[InventoryUnit] = @InventoryUnit ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[Quantity] = @Quantity ,
				[Remarks] = @Remarks ,
				[ServicePartsandMaterial] = @ServicePartsandMaterial ,
				[SourceData] = @SourceData ,
				[StockNumber] = @StockNumber ,
				[StockNumberNV] = @StockNumberNV ,
				[StockNumberLookupName] = @StockNumberLookupName ,
				[StockNumberLookupType] = @StockNumberLookupType ,
				[ToBatchNo] = @ToBatchNo ,
				[ToDealer] = @ToDealer ,
				[ToConfiguration] = @ToConfiguration ,
				[ToExteriorColor] = @ToExteriorColor ,
				[ToInteriorColor] = @ToInteriorColor ,
				[ToLocation] = @ToLocation ,
				[ToSerialNo] = @ToSerialNo ,
				[ToSite] = @ToSite ,
				[ToStyle] = @ToStyle ,
				[ToWarehouse] = @ToWarehouse ,
				[TransactionUnit] = @TransactionUnit ,
				[VIN] = @VIN ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateKaroseri
	   @ID INT OUTPUT ,
	   @Code VARCHAR(16) ,
	   @Name VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[Karoseri]
	   SET		[Code] = @Code ,
				[Name] = @Name ,
				[City] = @City ,
				[Alamat] = @Alamat ,
				[Kelurahan] = @Kelurahan ,
				[Kecamatan] = @Kecamatan ,
				[Province] = @Province ,
				[PostalCode] = @PostalCode ,
				[PhoneNo] = @PhoneNo ,
				[Fax] = @Fax ,
				[WebSite] = @WebSite ,
				[Email] = @Email ,
				[ContactPerson] = @ContactPerson ,
				[HP] = @HP ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateLeasing
	   @ID INT OUTPUT ,
	   @LeasingGroupName VARCHAR(50) ,
	   @LeasingCode VARCHAR(16) ,
	   @LeasingName VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[Leasing]
	   SET		LeasingGroupName = @LeasingGroupName ,
				[LeasingCode] = @LeasingCode ,
				[LeasingName] = @LeasingName ,
				[City] = @City ,
				[Alamat] = @Alamat ,
				[Kelurahan] = @Kelurahan ,
				[Kecamatan] = @Kecamatan ,
				[Province] = @Province ,
				[PostalCode] = @PostalCode ,
				[PhoneNo] = @PhoneNo ,
				[Fax] = @Fax ,
				[WebSite] = @WebSite ,
				[Email] = @Email ,
				[ContactPerson] = @ContactPerson ,
				[HP] = @HP ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateMyAlertStatus
	   @ID INT OUTPUT ,
	   @AlertMasterID SMALLINT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy NCHAR(10) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[MyAlertStatus]
	   SET		[AlertMasterID] = @AlertMasterID ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdatePOOtherVendor
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @AllocationPeriod VARCHAR(100) ,
	   @Balance MONEY ,
	   @DealerCode VARCHAR(100) ,
	   @City VARCHAR(100) ,
	   @CloseRespon VARCHAR(100) ,
	   @Country VARCHAR(100) ,
	   @DeliveryMethod SMALLINT ,
	   @Description VARCHAR(100) ,
	   @DownPayment MONEY ,
	   @DownPaymentAmountPaid MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @EventDate VARCHAR(100) ,
	   @ExternalDocNo VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @GrandTotal MONEY ,
	   @PaymentGroup SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @PostalCode VARCHAR(100) ,
	   @Priority SMALLINT ,
	   @Province VARCHAR(100) ,
	   @PRPOType VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @SONo VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @State SMALLINT ,
	   @StockReferenceNo VARCHAR(100) ,
	   @Taxable SMALLINT ,
	   @TermsOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalTitleRegistrationFee MONEY ,
	   @PurchaseOrderDate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[POOtherVendor]
	   SET		[Owner] = @Owner ,
				[Address1] = @Address1 ,
				[Address2] = @Address2 ,
				[Address3] = @Address3 ,
				[AllocationPeriod] = @AllocationPeriod ,
				[Balance] = @Balance ,
				[DealerCode] = @DealerCode ,
				[City] = @City ,
				[CloseRespon] = @CloseRespon ,
				[Country] = @Country ,
				[DeliveryMethod] = @DeliveryMethod ,
				[Description] = @Description ,
				[DownPayment] = @DownPayment ,
				[DownPaymentAmountPaid] = @DownPaymentAmountPaid ,
				[DownPaymentIsPaid] = @DownPaymentIsPaid ,
				[EventDate] = @EventDate ,
				[ExternalDocNo] = @ExternalDocNo ,
				[FormSource] = @FormSource ,
				[GrandTotal] = @GrandTotal ,
				[PaymentGroup] = @PaymentGroup ,
				[PersonInCharge] = @PersonInCharge ,
				[PostalCode] = @PostalCode ,
				[Priority] = @Priority ,
				[Province] = @Province ,
				[PRPOType] = @PRPOType ,
				[PurchaseOrderNo] = @PurchaseOrderNo ,
				[SONo] = @SONo ,
				[Site] = @Site ,
				[State] = @State ,
				[StockReferenceNo] = @StockReferenceNo ,
				[Taxable] = @Taxable ,
				[TermsOfPayment] = @TermsOfPayment ,
				[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TotalDiscountAmount] = @TotalDiscountAmount ,
				[TotalTitleRegistrationFee] = @TotalTitleRegistrationFee ,
				[PurchaseOrderDate] = @PurchaseOrderDate ,
				[VendorDescription] = @VendorDescription ,
				[Vendor] = @Vendor ,
				[Warehouse] = @Warehouse ,
				[WONo] = @WONo ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdatePOOtherVendorDetail
	   @ID INT OUTPUT ,
	   @POOtherVendorID INT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @EventData VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @BaseQtyOrder FLOAT ,
	   @BaseQtyReceipt FLOAT ,
	   @BaseQtyReturn FLOAT ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductSubstitute VARCHAR(100) ,
	   @ProductVariant VARCHAR(100) ,
	   @ProductVolume FLOAT ,
	   @ProductWeight FLOAT ,
	   @PromisedDate DATETIME ,
	   @PurchaseFor SMALLINT ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @PurchaseRequisitionDetail VARCHAR(100) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @ReferenceNo VARCHAR(100) ,
	   @RequiredDate DATETIME ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume FLOAT ,
	   @TotalWeight FLOAT ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[POOtherVendorDetail]
	   SET		[POOtherVendorID] = @POOtherVendorID ,
				[Owner] = @Owner ,
				[DealerCode] = @DealerCode ,
				[CloseLine] = @CloseLine ,
				[CloseReason] = @CloseReason ,
				[Completed] = @Completed ,
				[ConsumptionTax1Amount] = @ConsumptionTax1Amount ,
				[ConsumptionTax1] = @ConsumptionTax1 ,
				[ConsumptionTax2Amount] = @ConsumptionTax2Amount ,
				[ConsumptionTax2] = @ConsumptionTax2 ,
				[Department] = @Department ,
				[Description] = @Description ,
				[DiscountAmount] = @DiscountAmount ,
				[DiscountPercentage] = @DiscountPercentage ,
				[EventData] = @EventData ,
				[FormSource] = @FormSource ,
				[BaseQtyOrder] = @BaseQtyOrder ,
				[BaseQtyReceipt] = @BaseQtyReceipt ,
				[BaseQtyReturn] = @BaseQtyReturn ,
				[InventoryUnit] = @InventoryUnit ,
				[ProductCrossReference] = @ProductCrossReference ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[ProductSubstitute] = @ProductSubstitute ,
				[ProductVariant] = @ProductVariant ,
				[ProductVolume] = @ProductVolume ,
				[ProductWeight] = @ProductWeight ,
				[PromisedDate] = @PromisedDate ,
				[PurchaseFor] = @PurchaseFor ,
				[PurchaseOrderNo] = @PurchaseOrderNo ,
				[PurchaseRequisitionDetail] = @PurchaseRequisitionDetail ,
				[PurchaseUnit] = @PurchaseUnit ,
				[QtyOrder] = @QtyOrder ,
				[QtyReceipt] = @QtyReceipt ,
				[QtyReturn] = @QtyReturn ,
				[RecallProduct] = @RecallProduct ,
				[ReferenceNo] = @ReferenceNo ,
				[RequiredDate] = @RequiredDate ,
				[SalesOrderDetail] = @SalesOrderDetail ,
				[ScheduledShippingDate] = @ScheduledShippingDate ,
				[ServicePartsAndMaterial] = @ServicePartsAndMaterial ,
				[ShippingDate] = @ShippingDate ,
				[Site] = @Site ,
				[StockNumber] = @StockNumber ,
				[TitleRegistrationFee] = @TitleRegistrationFee ,
				[TotalAmount] = @TotalAmount ,
				[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TotalVolume] = @TotalVolume ,
				[TotalWeight] = @TotalWeight ,
				[TransactionAmount] = @TransactionAmount ,
				[UnitCost] = @UnitCost ,
				[Warehouse] = @Warehouse ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionChassisMasterProfile
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @ProfileHeaderID TINYINT ,
	   @GroupID TINYINT ,
	   @ProfileValue VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionChassisMasterProfile]
	   SET		[ChassisMasterID] = @ChassisMasterID ,
				[EndCustomerID] = @EndCustomerID ,
				[ProfileHeaderID] = @ProfileHeaderID ,
				[GroupID] = @GroupID ,
				[ProfileValue] = @ProfileValue ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionFaktur
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @OldEndCustomerID INT ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionStatus SMALLINT ,
	   @RevisionTypeID SMALLINT ,
	   @IsPay SMALLINT ,
	   @NewValidationDate DATETIME ,
	   @NewValidationBy VARCHAR(20) ,
	   @NewConfirmationDate DATETIME ,
	   @NewConfirmationBy VARCHAR(20) ,
	   @Remark VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionFaktur]
	   SET		[ChassisMasterID] = @ChassisMasterID ,
				[EndCustomerID] = @EndCustomerID ,
				[OldEndCustomerID] = @OldEndCustomerID ,
				[RegNumber] = @RegNumber ,
				[RevisionStatus] = @RevisionStatus ,
				[RevisionTypeID] = @RevisionTypeID ,
				[IsPay] = @IsPay ,
				[NewValidationDate] = @NewValidationDate ,
				[NewValidationBy] = @NewValidationBy ,
				[NewConfirmationDate] = @NewConfirmationDate ,
				[NewConfirmationBy] = @NewConfirmationBy ,
				[Remark] = @Remark ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionPaymentDetail
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @RevisionPaymentHeaderID INT ,
	   @RevisionSAPDocID INT ,
	   @IsCancel SMALLINT ,
	   @CancelReason VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionPaymentDetail]
	   SET		[RevisionFakturID] = @RevisionFakturID ,
				[RevisionPaymentHeaderID] = @RevisionPaymentHeaderID ,
				[RevisionSAPDocID] = @RevisionSAPDocID ,
				[IsCancel] = @IsCancel ,
				[CancelReason] = @CancelReason ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionPaymentHeader
	   @ID INT OUTPUT ,
	   @DealerID INT ,
	   @PaymentType VARCHAR(3) ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionPaymentDocID INT ,
	   @SlipNumber VARCHAR(20) ,
	   @TotalAmount MONEY ,
	   @Status SMALLINT ,
	   @EvidencePath VARCHAR(150) ,
	   @ActualPaymentDate DATETIME ,
	   @ActualPaymentAmount MONEY ,
	   @AccDocNumber VARCHAR(30) ,
	   @GyroDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionPaymentHeader]
	   SET		[DealerID] = @DealerID ,
				[PaymentType] = @PaymentType ,
				[RegNumber] = @RegNumber ,
				[RevisionPaymentDocID] = @RevisionPaymentDocID ,
				[SlipNumber] = @SlipNumber ,
				[TotalAmount] = @TotalAmount ,
				[Status] = @Status ,
				[EvidencePath] = @EvidencePath ,
				[ActualPaymentDate] = @ActualPaymentDate ,
				[ActualPaymentAmount] = @ActualPaymentAmount ,
				[AccDocNumber] = @AccDocNumber ,
				[GyroDate] = @GyroDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionPrice
	   @ID INT OUTPUT ,
	   @RevisionTypeID INT ,
	   @CategoryID INT ,
	   @Amount MONEY ,
	   @ValidFrom SMALLDATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionPrice]
	   SET		[CategoryID] = @CategoryID ,
				[RevisionTypeID] = @RevisionTypeID ,
				[Amount] = @Amount ,
				[ValidFrom] = @ValidFrom ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionSAPDoc
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @DebitChargeNo VARCHAR(10) ,
	   @DCAmount MONEY ,
	   @DebitMemoNo VARCHAR(15) ,
	   @DMAmount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionSAPDoc]
	   SET		[RevisionFakturID] = @RevisionFakturID ,
				[DebitChargeNo] = @DebitChargeNo ,
				[DCAmount] = @DCAmount ,
				[DebitMemoNo] = @DebitMemoNo ,
				[DMAmount] = @DMAmount ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID


------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_ValidateRevisionSAPDoc]    Script Date: 19/09/2018 16:55:32 ******/
	   SET ANSI_NULLS OFF
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionSPKFaktur
	   @ID INT OUTPUT ,
	   @SPKHeaderID INT ,
	   @EndCustomerID INT ,
	   @RowStatus SMALLINT ,
	--@CreatedTime datetime,
	   @CreatedBy VARCHAR(20) ,
	--@LastUpdateTime datetime,
	   @LastUpdateBy VARCHAR(20)
AS
	   UPDATE	[dbo].[RevisionSPKFaktur]
	   SET		[SPKHeaderID] = @SPKHeaderID ,
				[EndCustomerID] = @EndCustomerID ,
				[RowStatus] = @RowStatus ,
	--[CreatedTime] = @CreatedTime,
				[CreatedBy] = @CreatedBy ,
				[LastUpdateTime] = GETDATE() ,
				[LastUpdateBy] = @LastUpdateBy
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_UpdateRevisionType
	   @ID INT OUTPUT ,
	   @Description VARCHAR(100) ,
	   @RevisionCode VARCHAR(5) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[RevisionType]
	   SET		[Description] = @Description ,
				[RevisionCode] = @RevisionCode ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartConversion
	   @ID INT OUTPUT ,
	   @SparePartMasterID INT ,
	   @UoMto VARCHAR(18) ,
	   @Qty INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartConversion]
	   SET		[SparePartMasterID] = @SparePartMasterID ,
				[UoMto] = @UoMto ,
				[Qty] = @Qty ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartDeliveryOrder
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @Address4 VARCHAR(100) ,
	   @BusinessPhone VARCHAR(60) ,
	   @BU VARCHAR(100) ,
	   @CancellationDate DATETIME ,
	   @City VARCHAR(100) ,
	   @CustomerContacts VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DeliveryAddress VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(50) ,
	   @DeliveryType INT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Status SMALLINT ,
	   @MethodofPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @Salesperson VARCHAR(100) ,
	   @State SMALLINT ,
	   @TermofPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalMiscChargeBaseAmount MONEY ,
	   @TotalMiscChargeConsumptionTaxAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartDeliveryOrder]
	   SET		[Owner] = @Owner ,
				[Address1] = @Address1 ,
				[Address2] = @Address2 ,
				[Address3] = @Address3 ,
				[Address4] = @Address4 ,
				[BusinessPhone] = @BusinessPhone ,
				[BU] = @BU ,
				[CancellationDate] = @CancellationDate ,
				[City] = @City ,
				[CustomerContacts] = @CustomerContacts ,
				[Customer] = @Customer ,
				[CustomerNo] = @CustomerNo ,
				[DeliveryAddress] = @DeliveryAddress ,
				[DeliveryOrderNo] = @DeliveryOrderNo ,
				[DeliveryType] = @DeliveryType ,
				[ExternalReferenceNo] = @ExternalReferenceNo ,
				[GrandTotal] = @GrandTotal ,
				[Status] = @Status ,
				[MethodofPayment] = @MethodofPayment ,
				[OrderType] = @OrderType ,
				[ReferenceNo] = @ReferenceNo ,
				[Salesperson] = @Salesperson ,
				[State] = @State ,
				[TermofPayment] = @TermofPayment ,
				[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalDiscountAmount] = @TotalDiscountAmount ,
				[TotalMiscChargeBaseAmount] = @TotalMiscChargeBaseAmount ,
				[TotalMiscChargeConsumptionTaxAmount] = @TotalMiscChargeConsumptionTaxAmount ,
				[TotalReceipt] = @TotalReceipt ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TransactionDate] = @TransactionDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartDeliveryOrderDetail
	   @ID INT OUTPUT ,
	   @SparePartDeliveryOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @BaseQtyDelivered FLOAT ,
	   @BaseQtyOrder FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DeliveryOrderDetail VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountBaseAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @RunningNumber INT ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartDeliveryOrderDetail]
	   SET		[SparePartDeliveryOrderID] = @SparePartDeliveryOrderID ,
				[Owner] = @Owner ,
				[AmountBeforeDiscount] = @AmountBeforeDiscount ,
				[BaseAmount] = @BaseAmount ,
				[BaseQtyDelivered] = @BaseQtyDelivered ,
				[BaseQtyOrder] = @BaseQtyOrder ,
				[BatchNo] = @BatchNo ,
				[BU] = @BU ,
				[ConsumptionTax1Amount] = @ConsumptionTax1Amount ,
				[ConsumptionTax1] = @ConsumptionTax1 ,
				[ConsumptionTax2Amount] = @ConsumptionTax2Amount ,
				[ConsumptionTax2] = @ConsumptionTax2 ,
				[DeliveryOrderDetail] = @DeliveryOrderDetail ,
				[DeliveryOrderNo] = @DeliveryOrderNo ,
				[DiscountAmount] = @DiscountAmount ,
				[DiscountBaseAmount] = @DiscountBaseAmount ,
				[DiscountPercentage] = @DiscountPercentage ,
				[Location] = @Location ,
				[ProductCrossReference] = @ProductCrossReference ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[PromiseDate] = @PromiseDate ,
				[QtyDelivered] = @QtyDelivered ,
				[QtyOrder] = @QtyOrder ,
				[RequestDate] = @RequestDate ,
				[RunningNumber] = @RunningNumber ,
				[SalesOrderDetail] = @SalesOrderDetail ,
				[SalesUnit] = @SalesUnit ,
				[Site] = @Site ,
				[TotalAmount] = @TotalAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TransactionAmount] = @TransactionAmount ,
				[UnitPrice] = @UnitPrice ,
				[Warehouse] = @Warehouse ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartMasterTOP
	   @ID INT OUTPUT ,
	   @SparePartPOTypeTOPID INT ,
	   @SparePartMasterID INT ,
	   @Status BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartMasterTOP]
	   SET		[SparePartPOTypeTOPID] = @SparePartPOTypeTOPID ,
				[SparePartMasterID] = @SparePartMasterID ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartPOTypeTOP
	   @ID INT OUTPUT ,
	   @SparePartPOType VARCHAR(5) ,
	   @TermOfPaymentIDNotTOP INT ,
	   @IsTOP BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartPOTypeTOP]
	   SET		[SparePartPOType] = @SparePartPOType ,
				[IsTOP] = @IsTOP ,
				[TermOfPaymentIDNotTOP] = @TermOfPaymentIDNotTOP ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartPRDetailFromVendor
	   @ID INT OUTPUT ,
	   @PRDetailNumber VARCHAR(50) ,
	   @SparePartPRID INT ,
	   @PRNumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @BaseReceivedQuantity DECIMAL(18, 9) ,
	   @BatchNumber VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @EngineNumber VARCHAR(50) ,
	   @EventData VARCHAR(1000) ,
	   @InventoryUnit VARCHAR(100) ,
	   @KeyNumber VARCHAR(50) ,
	   @LandedCost MONEY ,
	   @Location VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductVolume DECIMAL(18, 9) ,
	   @ProductWeight DECIMAL(18, 9) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @ReceivedQuantity DECIMAL(18, 9) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @ReturnPRDetail VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume DECIMAL(18, 9) ,
	   @TotalWeight DECIMAL(18, 9) ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartPRDetailFromVendor]
	   SET		[PRDetailNumber] = @PRDetailNumber ,
				[SparePartPRID] = @SparePartPRID ,
				[PRNumber] = @PRNumber ,
				[Owner] = @Owner ,
				[BaseReceivedQuantity] = @BaseReceivedQuantity ,
				[BatchNumber] = @BatchNumber ,
				[DealerCode] = @DealerCode ,
				[ChassisModel] = @ChassisModel ,
				[ChassisNumberRegister] = @ChassisNumberRegister ,
				[ConsumptionTax1Amount] = @ConsumptionTax1Amount ,
				[ConsumptionTax1] = @ConsumptionTax1 ,
				[ConsumptionTax2Amount] = @ConsumptionTax2Amount ,
				[ConsumptionTax2] = @ConsumptionTax2 ,
				[DiscountAmount] = @DiscountAmount ,
				[EngineNumber] = @EngineNumber ,
				[EventData] = @EventData ,
				[InventoryUnit] = @InventoryUnit ,
				[KeyNumber] = @KeyNumber ,
				[LandedCost] = @LandedCost ,
				[Location] = @Location ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[ProductVolume] = @ProductVolume ,
				[ProductWeight] = @ProductWeight ,
				[PurchaseUnit] = @PurchaseUnit ,
				[ReceivedQuantity] = @ReceivedQuantity ,
				[ReferenceNumber] = @ReferenceNumber ,
				[ReturnPRDetail] = @ReturnPRDetail ,
				[ServicePartsAndMaterial] = @ServicePartsAndMaterial ,
				[Site] = @Site ,
				[StockNumber] = @StockNumber ,
				[TitleRegistrationFee] = @TitleRegistrationFee ,
				[TotalAmount] = @TotalAmount ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TotalVolume] = @TotalVolume ,
				[TotalWeight] = @TotalWeight ,
				[TransactionAmount] = @TransactionAmount ,
				[UnitCost] = @UnitCost ,
				[Warehouse] = @Warehouse ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartPRFromVendor
	   @ID INT OUTPUT ,
	   @PRNumber NVARCHAR(50) ,
	   @PONumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @APVoucherNumber VARCHAR(100) ,
	   @AssignLandedCost BIT ,
	   @AutoInvoiced BIT ,
	   @DealerCode VARCHAR(100) ,
	   @DeliveryOrderDate DATETIME ,
	   @DeliveryOrderNumber VARCHAR(50) ,
	   @EventData VARCHAR(4000) ,
	   @EventData2 TEXT ,
	   @GrandTotal MONEY ,
	   @Handling VARCHAR(100) ,
	   @LoadData BIT ,
	   @PackingSlipDate DATETIME ,
	   @PackingSlipNumber VARCHAR(50) ,
	   @PRReferenceRequired BIT ,
	   @ReturnPRNumber VARCHAR(100) ,
	   @State VARCHAR(100) ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTax1Amount MONEY ,
	   @TotalConsumptionTax2Amount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalTitleRegistrationFree MONEY ,
	   @TransactionDate DATETIME ,
	   @TransferOrderRequestingNumber VARCHAR(100) ,
	   @Type VARCHAR(100) ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @VendorInvoiceNumber VARCHAR(50) ,
	   @WONumber VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(100)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartPRFromVendor]
	   SET		[PRNumber] = @PRNumber ,
				[PONumber] = @PONumber ,
				[Owner] = @Owner ,
				[APVoucherNumber] = @APVoucherNumber ,
				[AssignLandedCost] = @AssignLandedCost ,
				[AutoInvoiced] = @AutoInvoiced ,
				[DealerCode] = @DealerCode ,
				[DeliveryOrderDate] = @DeliveryOrderDate ,
				[DeliveryOrderNumber] = @DeliveryOrderNumber ,
				[EventData] = @EventData ,
				[EventData2] = @EventData2 ,
				[GrandTotal] = @GrandTotal ,
				[Handling] = @Handling ,
				[LoadData] = @LoadData ,
				[PackingSlipDate] = @PackingSlipDate ,
				[PackingSlipNumber] = @PackingSlipNumber ,
				[PRReferenceRequired] = @PRReferenceRequired ,
				[ReturnPRNumber] = @ReturnPRNumber ,
				[State] = @State ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalConsumptionTax1Amount] = @TotalConsumptionTax1Amount ,
				[TotalConsumptionTax2Amount] = @TotalConsumptionTax2Amount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TotalTitleRegistrationFree] = @TotalTitleRegistrationFree ,
				[TransactionDate] = @TransactionDate ,
				[TransferOrderRequestingNumber] = @TransferOrderRequestingNumber ,
				[Type] = @Type ,
				[VendorDescription] = @VendorDescription ,
				[Vendor] = @Vendor ,
				[VendorInvoiceNumber] = @VendorInvoiceNumber ,
				[WONumber] = @WONumber ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartSalesOrder
	   @ID INT OUTPUT ,
	   @SalesChannel SMALLINT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @DealerCode VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DownPaymentAmount MONEY ,
	   @DownPaymentAmountReceived MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Handling SMALLINT ,
	   @MethodOfPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @SalesOrderNo VARCHAR(50) ,
	   @SalesPerson VARCHAR(100) ,
	   @ShipmentType VARCHAR(50) ,
	   @State VARCHAR(50) ,
	   @TermOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartSalesOrder]
	   SET		[SalesChannel] = @SalesChannel ,
				[Owner] = @Owner ,
				[Status] = @Status ,
				[DealerCode] = @DealerCode ,
				[Customer] = @Customer ,
				[CustomerNo] = @CustomerNo ,
				[DownPaymentAmount] = @DownPaymentAmount ,
				[DownPaymentAmountReceived] = @DownPaymentAmountReceived ,
				[DownPaymentIsPaid] = @DownPaymentIsPaid ,
				[ExternalReferenceNo] = @ExternalReferenceNo ,
				[GrandTotal] = @GrandTotal ,
				[Handling] = @Handling ,
				[MethodOfPayment] = @MethodOfPayment ,
				[OrderType] = @OrderType ,
				[SalesOrderNo] = @SalesOrderNo ,
				[SalesPerson] = @SalesPerson ,
				[ShipmentType] = @ShipmentType ,
				[State] = @State ,
				[TermOfPayment] = @TermOfPayment ,
				[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount ,
				[TotalBaseAmount] = @TotalBaseAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TotalDiscountAmount] = @TotalDiscountAmount ,
				[TotalReceipt] = @TotalReceipt ,
				[TransactionDate] = @TransactionDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSparePartSalesOrderDetail
	   @ID INT OUTPUT ,
	   @SparePartSalesOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @KodeDealer VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentAge DECIMAL(18, 9) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @SalesOrderDetailID VARCHAR(50) ,
	   @SalesOrderNo VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SparePartSalesOrderDetail]
	   SET		[SparePartSalesOrderID] = @SparePartSalesOrderID ,
				[Owner] = @Owner ,
				[Status] = @Status ,
				[AmountBeforeDiscount] = @AmountBeforeDiscount ,
				[BaseAmount] = @BaseAmount ,
				[KodeDealer] = @KodeDealer ,
				[ConsumptionTax1Amount] = @ConsumptionTax1Amount ,
				[ConsumptionTax1] = @ConsumptionTax1 ,
				[ConsumptionTax2Amount] = @ConsumptionTax2Amount ,
				[ConsumptionTax2] = @ConsumptionTax2 ,
				[DiscountAmount] = @DiscountAmount ,
				[DiscountPercentAge] = @DiscountPercentAge ,
				[ProductCrossReference] = @ProductCrossReference ,
				[ProductDescription] = @ProductDescription ,
				[Product] = @Product ,
				[PromiseDate] = @PromiseDate ,
				[QtyDelivered] = @QtyDelivered ,
				[QtyOrder] = @QtyOrder ,
				[RequestDate] = @RequestDate ,
				[SalesOrderDetailID] = @SalesOrderDetailID ,
				[SalesOrderNo] = @SalesOrderNo ,
				[SalesUnit] = @SalesUnit ,
				[Site] = @Site ,
				[TotalAmount] = @TotalAmount ,
				[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount ,
				[TransactionAmount] = @TransactionAmount ,
				[UnitPrice] = @UnitPrice ,
				[Warehouse] = @Warehouse ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateSPKChassis
	   @ID INT OUTPUT ,
	   @SPKDetailID INT ,
	   @ChassisMasterID INT ,
	   @MatchingType SMALLINT ,
	   @MatchingDate DATETIME ,
	   @MatchingNumber VARCHAR(50) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @KeyNumber VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[SPKChassis]
	   SET		[SPKDetailID] = @SPKDetailID ,
				[ChassisMasterID] = @ChassisMasterID ,
				[MatchingType] = @MatchingType ,
				[MatchingDate] = @MatchingDate ,
				[MatchingNumber] = @MatchingNumber ,
				[ReferenceNumber] = @ReferenceNumber ,
				[KeyNumber] = @KeyNumber ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateStandardCodeChar
	   @ID INT OUTPUT ,
	   @Category VARCHAR(100) ,
	   @ValueId VARCHAR(5) ,
	   @ValueCode VARCHAR(200) = '' ,
	   @ValueDesc VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20) ,
	--@LastUpdateTime datetime,
	   @Sequence INT
AS
	   UPDATE	[dbo].[StandardCodeChar]
	   SET		[Category] = @Category ,
				[ValueId] = @ValueId ,
				[ValueCode] = @ValueCode ,
				[ValueDesc] = @ValueDesc ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE() ,
				[Sequence] = @Sequence
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPBlockStatus
	   @ID INT OUTPUT ,
	   @SparePartPOStatusID INT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPBlockStatus]
	   SET		[SparePartPOStatusID] = @SparePartPOStatusID ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPCreditAccount
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @TermOfPaymentID INT ,
	   @KelipatanPembayaran INT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPCreditAccount]
	   SET		[DealerID] = @DealerID ,
				[TermOfPaymentID] = @TermOfPaymentID ,
				[KelipatanPembayaran] = @KelipatanPembayaran ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPDeposit
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @AmountC2 MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPSPDeposit]
	   SET		[SparePartBillingID] = @SparePartBillingID ,
				[AmountC2] = @AmountC2 ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPDueDate
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @DueDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPSPDueDate]
	   SET		[SparePartBillingID] = @SparePartBillingID ,
				[DueDate] = @DueDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferActual
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @RefTransferBank VARCHAR(100) ,
	   @Amount MONEY ,
	   @PostingDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPSPTransferActual]
	   SET		[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID ,
				[RefTransferBank] = @RefTransferBank ,
				[Amount] = @Amount ,
				[PostingDate] = @PostingDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferCeiling
	   @ID INT OUTPUT ,
	   @CreditAccount VARCHAR(20) ,
	   @ProductCategoryID SMALLINT ,
	   @PaymentType SMALLINT ,
	   @EffectiveDate DATETIME ,
	   @BalanceBefore MONEY ,
	   @AvailableCeiling MONEY ,
	   @LastSyncDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(20)
	--,	@LastUpdatedTime datetime
AS
	   UPDATE	[dbo].[TOPSPTransferCeiling]
	   SET		[CreditAccount] = @CreditAccount ,
				[ProductCategoryID] = @ProductCategoryID ,
				[PaymentType] = @PaymentType ,
				[EffectiveDate] = @EffectiveDate ,
				[BalanceBefore] = @BalanceBefore ,
				[AvailableCeiling] = @AvailableCeiling ,
				[LastSyncDate] = @LastSyncDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferCeilingDetail
	   @ID INT OUTPUT ,
	   @TOPSPTransferCeilingID INT ,
	   @SparepartBillingID INT ,
	   @TOPSPTransferPaymentID INT ,
	   @Amount MONEY ,
	   @IsIncome SMALLINT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(20)
AS
	   UPDATE	[dbo].[TOPSPTransferCeilingDetail]
	   SET		[TOPSPTransferCeilingID] = @TOPSPTransferCeilingID ,
				[SparepartBillingID] = @SparepartBillingID ,
				[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID ,
				[Amount] = @Amount ,
				[IsIncome] = @IsIncome ,
				[Status] = @Status ,
				[RowStatus] = @RowStatus ,
            --[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdatedBy] = @LastUpdatedBy ,
				[LastUpdatedTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferPayment
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @CreditAccount VARCHAR(6) ,
	   @RegNumber VARCHAR(15) ,
	   @DueDate DATETIME ,
	   @PaymentPurposeID TINYINT ,
	   @TransferPlanDate DATETIME ,
	   @bankid INTEGER ,
	   @TOPSPTransferPaymentIDReff INT ,
	   @IsAccelerated SMALLINT ,
	   @Status SMALLINT ,
	   @ValidatedBy VARCHAR(20) ,
	   @ValidatedTime DATETIME ,
	   @ConfirmedBy VARCHAR(20) ,
	   @ConfirmedTime DATETIME ,
	   @CanceledBy VARCHAR(20) ,
	   @CanceledTime DATETIME ,
	   @TransferAmount MONEY ,
	   @TransferActualDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   UPDATE	[dbo].[TOPSPTransferPayment]
	   SET		[DealerID] = @DealerID ,
				[CreditAccount] = @CreditAccount ,
				[RegNumber] = @RegNumber ,
				[DueDate] = @DueDate ,
				[PaymentPurposeID] = @PaymentPurposeID ,
				[TransferPlanDate] = @TransferPlanDate ,
				[TOPSPTransferPaymentIDReff] = @TOPSPTransferPaymentIDReff ,
				[IsAccelerated] = @IsAccelerated ,
				[Status] = @Status ,
				[ValidatedBy] = @ValidatedBy ,
				[ValidatedTime] = CASE WHEN @Status = 4 THEN GETDATE()
									   ELSE @ValidatedTime
								  END ,
				[ConfirmedBy] = @ConfirmedBy ,
				[ConfirmedTime] = CASE WHEN @Status = 2 THEN GETDATE()
									   ELSE @ConfirmedTime
								  END ,
				[CanceledBy] = @CanceledBy ,
				[CanceledTime] = CASE WHEN @Status = 1 THEN GETDATE()
									  ELSE @CanceledTime
								 END ,
				[TransferAmount] = @TransferAmount ,
				[TransferActualDate] = @TransferActualDate ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferPaymentDetail
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @SparePartBillingID INT ,
	   @Amount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[TOPSPTransferPaymentDetail]
	   SET		[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID ,
				[SparePartBillingID] = @SparePartBillingID ,
				[Amount] = @Amount ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Juli 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateTOPSPTransferPayment_TransferActual
	   @TOPSPTransferPaymentID INT ,
	   @RefTransferbank VARCHAR(100) ,
	   @Amount MONEY ,
	   @PostingDate DATETIME ,
	 
	--@CreatedTime datetime,
	   @LastUpdatedBy VARCHAR(20)
	--,@LastUpdatedTime DATETIME
AS
	   BEGIN
			 SET NOCOUNT ON;

			 IF NOT EXISTS ( SELECT TOP 1
									'*'
							 FROM	dbo.TOPSPTransferActual a
							 WHERE	a.[RowStatus] = 0
									AND a.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
									AND a.[RefTransferBank] = @RefTransferbank )
				BEGIN
					  INSERT	INTO dbo.TOPSPTransferActual
								(
								  TOPSPTransferPaymentID ,
								  RefTransferBank ,
								  Amount ,
								  PostingDate ,
								  RowStatus ,
								  CreatedBy ,
								  CreatedTime ,
								  LastUpdateBy ,
								  LastUpdateTime
								)
					  VALUES	(
								  @TOPSPTransferPaymentID , -- TOPSPTransferPaymentID - int
								  @RefTransferbank , -- RefTransferbank - varchar(100)
								  @Amount , -- Amount - money
								  @PostingDate , -- PostingDate - datetime
								  0 , -- RowStatus - smallint
								  @LastUpdatedBy , -- CreatedBy - varchar(20)
								  GETDATE() , -- CreatedTime - datetime
								  '' , -- LastUpdatedBy - varchar(20)
								  GETDATE()  -- LastUpdatedTime - datetime
								)


				END

			 ELSE
				BEGIN
					  UPDATE	dbo.TOPSPTransferActual
					  SET		[PostingDate] = @PostingDate ,
								[Amount] = @Amount ,
								LastUpdateBy = @LastUpdatedBy ,
								LastUpdateTime = GETDATE()
					  WHERE		TOPSPTransferActual.[RowStatus] = 0
								AND TOPSPTransferActual.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
								AND TOPSPTransferActual.[RefTransferBank] = @RefTransferbank

				END

			 UPDATE	dbo.[TOPSPTransferPayment]
			 SET	[TransferAmount] = ISNULL(b.[Amount], 0) ,
					[TransferActualDate] = b.[PostingDate] ,
					LastUpdateBy = @LastUpdatedBy ,
					LastUpdateTime = GETDATE() ,
					[Status] = CASE	WHEN ISNULL(b.[Amount], 0) >= C.mDetail
										 AND ISNULL(C.mDetail, 0) > 0 THEN 5 -- selesai
									ELSE [a].[Status]
							   END
			 FROM	[dbo].[TOPSPTransferPayment] a
			 INNER JOIN (
						  SELECT	[TOPSPTransferPaymentID] ,
									MAX(TOPSPTransferActual.[PostingDate]) [PostingDate] ,
									SUM(Amount) Amount
						  FROM		[dbo].TOPSPTransferActual
						  WHERE		TOPSPTransferActual.[RowStatus] = 0
									AND TOPSPTransferActual.[TOPSPTransferPaymentID] = @TOPSPTransferPaymentID
						  GROUP BY	TOPSPTransferActual.[TOPSPTransferPaymentID]
						) b ON b.TOPSPTransferPaymentID = a.[ID]
			 OUTER APPLY (
						   SELECT	SUM(C.Amount) AS mDetail
						   FROM		dbo.TOPSPTransferPaymentDetail C
						   WHERE	1 = 1
									AND C.TOPSPTransferPaymentID = a.ID
						 ) C
			 WHERE	a.[ID] = @TOPSPTransferPaymentID

	   END
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateVehiclePurchaseDetail
	   @ID INT OUTPUT ,
	   @VehiclePurchaseHeaderID INT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseLineName VARCHAR(100) ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @CompletedName VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @ProductName VARCHAR(100) ,
	   @ProductVariantName VARCHAR(100) ,
	   @PODetail VARCHAR(50) ,
	   @POName VARCHAR(100) ,
	   @PRDetailName VARCHAR(100) ,
	   @PurchaseUnitName VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @RecallProductName VARCHAR(50) ,
	   @SODetailName VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumberName VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[VehiclePurchaseDetail]
	   SET		[VehiclePurchaseHeaderID] = @VehiclePurchaseHeaderID ,
				[BUCode] = @BUCode ,
				[BUName] = @BUName ,
				[CloseLine] = @CloseLine ,
				[CloseLineName] = @CloseLineName ,
				[CloseReason] = @CloseReason ,
				[Completed] = @Completed ,
				[CompletedName] = @CompletedName ,
				[ProductDescription] = @ProductDescription ,
				[ProductName] = @ProductName ,
				[ProductVariantName] = @ProductVariantName ,
				[PODetail] = @PODetail ,
				[POName] = @POName ,
				[PRDetailName] = @PRDetailName ,
				[PurchaseUnitName] = @PurchaseUnitName ,
				[QtyOrder] = @QtyOrder ,
				[QtyReceipt] = @QtyReceipt ,
				[QtyReturn] = @QtyReturn ,
				[RecallProduct] = @RecallProduct ,
				[RecallProductName] = @RecallProductName ,
				[SODetailName] = @SODetailName ,
				[ScheduledShippingDate] = @ScheduledShippingDate ,
				[ServicePartsAndMaterial] = @ServicePartsAndMaterial ,
				[ShippingDate] = @ShippingDate ,
				[Site] = @Site ,
				[StockNumberName] = @StockNumberName ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_UpdateVehiclePurchaseHeader
	   @ID INT OUTPUT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @DeliveryMethod VARCHAR(10) ,
	   @Description VARCHAR(100) ,
	   @PRPOTypeName VARCHAR(100) ,
	   @DMSPONo VARCHAR(50) ,
	   @DMSPOStatus INT ,
	   @DMSPODate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(50) ,
	   @PurchaseReceiptNo VARCHAR(50) ,
	   @PurchaseReceiptDetailNo VARCHAR(50) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(50)	--@LastUpdateTime datetime	
AS
	   UPDATE	[dbo].[VehiclePurchaseHeader]
	   SET		[BUCode] = @BUCode ,
				[BUName] = @BUName ,
				[DeliveryMethod] = @DeliveryMethod ,
				[Description] = @Description ,
				[PRPOTypeName] = @PRPOTypeName ,
				[DMSPONo] = @DMSPONo ,
				[DMSPOStatus] = @DMSPOStatus ,
				[DMSPODate] = @DMSPODate ,
				[VendorDescription] = @VendorDescription ,
				[Vendor] = @Vendor ,
				[PurchaseOrderNo] = @PurchaseOrderNo ,
				[PurchaseReceiptNo] = @PurchaseReceiptNo ,
				[PurchaseReceiptDetailNo] = @PurchaseReceiptDetailNo ,
				[ChassisModel] = @ChassisModel ,
				[ChassisNumberRegister] = @ChassisNumberRegister ,
				[RowStatus] = @RowStatus ,
				[CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
				[LastUpdateBy] = @LastUpdateBy ,
				[LastUpdateTime] = GETDATE()
	   WHERE	[ID] = @ID
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateAPPayment
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentNo VARCHAR(50) ,
	   @APReferenceNo VARCHAR(100) ,
	   @APVoucherReferenceNo VARCHAR(100) ,
	   @AppliedToDocument MONEY ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @State SMALLINT ,
	   @TotalChangeAmount MONEY ,
	   @TotalPaymentAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @Type SMALLINT ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateAPPaymentDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @APPaymentID INT ,
	   @Owner VARCHAR(100) ,
	   @APPaymentDetailNo VARCHAR(100) ,
	   @APPaymentNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @ExternalDocumentNo VARCHAR(50) ,
	   @ExternalDocumentType SMALLINT ,
	   @APVoucherNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNoNVSOReferral VARCHAR(100) ,
	   @OrderNoOutsourceWorkOrder VARCHAR(100) ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoUVSOReferral VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaymentAmount MONEY ,
	   @PaymentSlipNo VARCHAR(50) ,
	   @ReceiptFromVendor BIT ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateARReceipt
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @GeneratedToken VARCHAR(36) ,
	   @ARInvoiceReferenceNo VARCHAR(100) ,
	   @ARReceiptNo VARCHAR(50) ,
	   @ARReceiptReferenceNo VARCHAR(100) ,
	   @Type SMALLINT ,
	   @BookingFee BIT ,
	   @BU VARCHAR(100) ,
	   @Cancelled BIT ,
	   @CashAndBank VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(100) ,
	   @EndOrderDate DATETIME ,
	   @MethodOfPayment VARCHAR(100) ,
	   @AvailableBalance MONEY ,
	   @StartOrderDate DATETIME ,
	   @State SMALLINT ,
	   @AppliedToDocument MONEY ,
	   @TotalAmountBase MONEY ,
	   @TotalChangeAmount MONEY ,
	   @TotalOutstandingBalanceBase MONEY ,
	   @TotalReceiptAmount MONEY ,
	   @TotalRemainingBalanceBase MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateARReceiptDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @ARReceiptID INT ,
	   @Owner VARCHAR(100) ,
	   @DetailNo VARCHAR(50) ,
	   @ARReceiptNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ChangeAmount MONEY ,
	   @Customer VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DifferenceValue FLOAT ,
	   @InvoiceNo VARCHAR(100) ,
	   @OrderDate DATETIME ,
	   @OrderNo VARCHAR(100) ,
	   @OrderNoSO VARCHAR(100) ,
	   @OrderNoUVSO VARCHAR(100) ,
	   @OrderNoWO VARCHAR(100) ,
	   @OutstandingBalance MONEY ,
	   @PaidBackToCustomer BIT ,
	   @ReceiptAmount MONEY ,
	   @RemainingBalance MONEY ,
	   @SourceType SMALLINT ,
	   @TransactionDocument VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateBusinessSectorDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @BusinessSectorHeaderID INT ,
	   @BusinessDomain VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateBusinessSectorHeader
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @BusinessSectorName VARCHAR(500) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateCarrosserieDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @CarrosserieHeaderID INT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @AccessorriesDescription VARCHAR(100) ,
	   @AccessorriesName VARCHAR(100) ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @KITName VARCHAR(100) ,
	   @PBUCode VARCHAR(20) ,
	   @PBUName VARCHAR(100) ,
	   @PDIDetailName VARCHAR(100) ,
	   @PDIReceiptDetailNo VARCHAR(50) ,
	   @PDIReceiptName VARCHAR(100) ,
	   @ReceiveQuantity FLOAT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateCarrosserieHeader
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @PDIStateCode SMALLINT ,
	   @PDIStatusCode SMALLINT ,
	   @BUCode VARCHAR(50) ,
	   @BUName VARCHAR(100) ,
	   @PDIName VARCHAR(100) ,
	   @PDIReceiptNo VARCHAR(50) ,
	   @PDIReceiptRefName VARCHAR(100) ,
	   @PDIReceiptStatus SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @VendorName VARCHAR(100) ,
	   @ChassisNumber VARCHAR(20) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateCustomerGroup
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Code VARCHAR(20) ,
	   @Name VARCHAR(150) ,
	   @Description NVARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(50) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateCustomerRequestOCR
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @CustomerRequestID INT ,
	   @OCRIdentityID INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateDealerSystems
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @SystemID INT ,
	   @isSPKMatchFaktur BIT ,
	   @isOnlyUploadPhotoTenagaPenjual BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 30 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateDMSWOWarrantyClaim
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @DealerBranchID INT ,
	   @ChassisNumber VARCHAR(20) ,
	   @isBB BIT ,
	   @WorkOrderNumber VARCHAR(50) ,
	   @FailureDate DATETIME ,
	   @ServiceDate DATETIME ,
	   @Owner VARCHAR(50) ,
	   @Mileage INT ,
	   @ServiceBuletin VARCHAR(50) ,
	   @Symptoms VARCHAR(1000) ,
	   @Causes VARCHAR(1000) ,
	   @Results VARCHAR(1000) ,
	   @Notes VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreateBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateFleet
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @FleetCode VARCHAR(50) ,
	   @FleetName VARCHAR(100) ,
	   @FleetNickName VARCHAR(100) ,
	   @FleetGroup VARCHAR(100) ,
	   @Address VARCHAR(255) ,
	   @ProvinceId INT ,
	   @CityId SMALLINT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(50) ,
	   @BusinessSectorHeaderId INT ,
	   @BusinessSectorDetailId INT ,
	   @FleetNote VARCHAR(MAX) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateFleetCustomer
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @CustomerGroupID INT ,
	   @ProvinceID INT ,
	   @PreArea VARCHAR(50) ,
	   @CityID SMALLINT ,
	   @BusinessSectorDetailId INT ,
	   @RatioMatrixID INT ,
	   @CategoryIndex INT ,
	   @TypeIndex INT ,
	   @Code VARCHAR(30) ,
	   @Name VARCHAR(50) ,
	   @Gedung VARCHAR(50) ,
	   @Alamat VARCHAR(150) ,
	   @Kecamatan VARCHAR(75) ,
	   @Kelurahan VARCHAR(75) ,
	   @PostalCode VARCHAR(10) ,
	   @Email NVARCHAR(50) ,
	   @PhoneNo VARCHAR(15) ,
	   @TipeCustomer INT ,
	   @IdentityType INT ,
	   @IdentityNumber VARCHAR(30) ,
	   @Attachment VARCHAR(100) ,
	   @ClassificationIndex INT ,
	   @FleetNickName VARCHAR(50) ,
	   @FleetNote VARCHAR(1000) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(50) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateFleetCustomerContact
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @Name VARCHAR(50) ,
	   @Position VARCHAR(50) ,
	   @PhoneNo VARCHAR(20) ,
	   @Handphone VARCHAR(20) ,
	   @Email NVARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(50) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateFleetCustomerToCustomer
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @CustomerID INT ,
	   @IsDefault BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(50) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateFleetCustomerToDealer
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @FleetCustomerID INT ,
	   @DealerID SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(50) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 27 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateIndustrialSector
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(50) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateInventoryTransaction
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @PersonInCharge VARCHAR(100) ,
	   @ProcessCode VARCHAR(10) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateInventoryTransactionDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @FromBU VARCHAR(100) ,
	   @InventoryTransactionID INT ,
	   @InventoryTransactionNo VARCHAR(100) ,
	   @InventoryTransferDetail VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @ReasonCode VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @RegisterSerialNumber VARCHAR(100) ,
	   @RunningNumber INT ,
	   @SerialNo VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @SourceData VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @TotalCost MONEY ,
	   @TransactionType VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @UnitCost MONEY ,
	   @VIN VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateInventoryTransfer
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(50) ,
	   @ItemTypeForTransfer SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @ReceiptDate DATETIME ,
	   @ReceiptNo VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @SearchVehicle VARCHAR(50) ,
	   @SourceData VARCHAR(50) ,
	   @State SMALLINT ,
	   @ToDealer VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @TransactionDate DATETIME ,
	   @TransactionType SMALLINT ,
	   @TransferStatus SMALLINT ,
	   @TransferStep BIT ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateInventoryTransferDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @InventoryTransferID INT ,
	   @Owner VARCHAR(100) ,
	   @BaseQuantity FLOAT ,
	   @ConsumptionTaxIn VARCHAR(100) ,
	   @ConsumptionTaxOut VARCHAR(100) ,
	   @FromBatchNo VARCHAR(100) ,
	   @FromDealer VARCHAR(100) ,
	   @FromConfiguration VARCHAR(100) ,
	   @FromExteriorColor VARCHAR(100) ,
	   @FromInteriorColor VARCHAR(100) ,
	   @FromLocation VARCHAR(100) ,
	   @FromSerialNo VARCHAR(100) ,
	   @FromSite VARCHAR(100) ,
	   @FromStyle VARCHAR(100) ,
	   @FromWarehouse VARCHAR(100) ,
	   @InventoryTransferNo VARCHAR(100) ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @Quantity FLOAT ,
	   @Remarks VARCHAR(100) ,
	   @ServicePartsandMaterial VARCHAR(100) ,
	   @SourceData VARCHAR(50) ,
	   @StockNumber VARCHAR(100) ,
	   @StockNumberNV VARCHAR(100) ,
	   @StockNumberLookupName VARCHAR(200) ,
	   @StockNumberLookupType INT ,
	   @ToBatchNo VARCHAR(100) ,
	   @ToDealer VARCHAR(100) ,
	   @ToConfiguration VARCHAR(100) ,
	   @ToExteriorColor VARCHAR(100) ,
	   @ToInteriorColor VARCHAR(100) ,
	   @ToLocation VARCHAR(100) ,
	   @ToSerialNo VARCHAR(100) ,
	   @ToSite VARCHAR(100) ,
	   @ToStyle VARCHAR(100) ,
	   @ToWarehouse VARCHAR(100) ,
	   @TransactionUnit VARCHAR(100) ,
	   @VIN VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 28, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateKaroseri
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Code VARCHAR(16) ,
	   @Name VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, March 02, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateLeasing
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @LeasingGroupName VARCHAR(50) ,
	   @LeasingCode VARCHAR(16) ,
	   @LeasingName VARCHAR(50) ,
	   @City VARCHAR(50) ,
	   @Alamat VARCHAR(100) ,
	   @Kelurahan VARCHAR(50) ,
	   @Kecamatan VARCHAR(50) ,
	   @Province VARCHAR(50) ,
	   @PostalCode VARCHAR(10) ,
	   @PhoneNo VARCHAR(30) ,
	   @Fax VARCHAR(20) ,
	   @WebSite VARCHAR(20) ,
	   @Email NVARCHAR(255) ,
	   @ContactPerson VARCHAR(50) ,
	   @HP VARCHAR(20) ,
	   @Status TINYINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateMyAlertStatus
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @AlertMasterID SMALLINT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy NCHAR(10) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 07 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidatePODealer
	   @Result VARCHAR(1000) ,
	   @DealerID INT OUTPUT ,
	   @DealerCode VARCHAR(10) ,
	   @DealerName VARCHAR(50) ,
	   @POHeaderID INT ,
	   @PONumber VARCHAR(10) ,
	   @AllocQty MONEY ,
	   @Price MONEY ,
	   @Discount MONEY ,
	   @Interest MONEY ,
	   @ContractNumber VARCHAR(10) ,
	   @PKNumber VARCHAR(10) ,
	   @DealerPKNumber VARCHAR(40) ,
	   @ProjectName VARCHAR(40) ,
	   @SalesOrderID INT ,
	   @SONumber VARCHAR(10) ,
	   @SODate DATETIME ,
	   @PaymentRef VARCHAR(10) ,
	   @SOType VARCHAR(4) ,
	   @LastUpdateTime DATETIME ,
	   @VehicleColorID SMALLINT ,
	   @BasePrice MONEY ,
	   @OptionPrice MONEY ,
	   @DiscountBeforeTax NUMERIC ,
	   @NetPrice NUMERIC ,
	   @TotalHarga NUMERIC ,
	   @PPN NUMERIC ,
	   @TotalHargaPPN NUMERIC ,
	   @TotalHargaPP NUMERIC
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidatePOOtherVendor
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @AllocationPeriod VARCHAR(100) ,
	   @Balance MONEY ,
	   @DealerCode VARCHAR(100) ,
	   @City VARCHAR(100) ,
	   @CloseRespon VARCHAR(100) ,
	   @Country VARCHAR(100) ,
	   @DeliveryMethod SMALLINT ,
	   @Description VARCHAR(100) ,
	   @DownPayment MONEY ,
	   @DownPaymentAmountPaid MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @EventDate VARCHAR(100) ,
	   @ExternalDocNo VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @GrandTotal MONEY ,
	   @PaymentGroup SMALLINT ,
	   @PersonInCharge VARCHAR(100) ,
	   @PostalCode VARCHAR(100) ,
	   @Priority SMALLINT ,
	   @Province VARCHAR(100) ,
	   @PRPOType VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @SONo VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @State SMALLINT ,
	   @StockReferenceNo VARCHAR(100) ,
	   @Taxable SMALLINT ,
	   @TermsOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalTitleRegistrationFee MONEY ,
	   @PurchaseOrderDate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @Warehouse VARCHAR(100) ,
	   @WONo VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidatePOOtherVendorDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @POOtherVendorID INT ,
	   @Owner VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @Department VARCHAR(100) ,
	   @Description VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @EventData VARCHAR(100) ,
	   @FormSource SMALLINT ,
	   @BaseQtyOrder FLOAT ,
	   @BaseQtyReceipt FLOAT ,
	   @BaseQtyReturn FLOAT ,
	   @InventoryUnit VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductSubstitute VARCHAR(100) ,
	   @ProductVariant VARCHAR(100) ,
	   @ProductVolume FLOAT ,
	   @ProductWeight FLOAT ,
	   @PromisedDate DATETIME ,
	   @PurchaseFor SMALLINT ,
	   @PurchaseOrderNo VARCHAR(100) ,
	   @PurchaseRequisitionDetail VARCHAR(100) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @ReferenceNo VARCHAR(100) ,
	   @RequiredDate DATETIME ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume FLOAT ,
	   @TotalWeight FLOAT ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionChassisMasterProfile
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @ProfileHeaderID TINYINT ,
	   @GroupID TINYINT ,
	   @ProfileValue VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionFaktur
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @ChassisMasterID INT ,
	   @EndCustomerID INT ,
	   @OldEndCustomerID INT ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionStatus SMALLINT ,
	   @RevisionTypeID SMALLINT ,
	   @IsPay SMALLINT ,
	   @NewValidationDate DATETIME ,
	   @NewValidationBy VARCHAR(20) ,
	   @NewConfirmationDate DATETIME ,
	   @NewConfirmationBy VARCHAR(20) ,
	   @Remark VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionPaymentDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @RevisionPaymentHeaderID INT ,
	   @RevisionSAPDocID INT ,
	   @IsCancel SMALLINT ,
	   @CancelReason VARCHAR(250) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionPaymentHeader
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @DealerID INT ,
	   @PaymentType VARCHAR(3) ,
	   @RegNumber VARCHAR(15) ,
	   @RevisionPaymentDocID INT ,
	   @SlipNumber VARCHAR(20) ,
	   @TotalAmount MONEY ,
	   @Status SMALLINT ,
	   @EvidencePath VARCHAR(150) ,
	   @ActualPaymentDate DATETIME ,
	   @ActualPaymentAmount MONEY ,
	   @AccDocNumber VARCHAR(30) ,
	   @GyroDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionPrice
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @CategoryID INT ,
	   @RevisionTypeID INT ,
	   @Amount MONEY ,
	   @ValidFrom SMALLDATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionSAPDoc
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @RevisionFakturID INT ,
	   @DebitChargeNo VARCHAR(10) ,
	   @DCAmount MONEY ,
	   @DebitMemoNo VARCHAR(15) ,
	   @DMAmount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionSPKFaktur
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SPKHeaderID INT ,
	   @EndCustomerID INT ,
	   @RowStatus SMALLINT ,
	   @CreatedTime DATETIME ,
	   @CreatedBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME ,
	   @LastUpdateBy VARCHAR(20)
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE up_ValidateRevisionType
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Description VARCHAR(100) ,
	   @RevisionCode VARCHAR(5) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 26, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartConversion
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartMasterID INT ,
	   @UoMto VARCHAR(18) ,
	   @Qty INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartDeliveryOrder
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Owner VARCHAR(100) ,
	   @Address1 VARCHAR(100) ,
	   @Address2 VARCHAR(100) ,
	   @Address3 VARCHAR(100) ,
	   @Address4 VARCHAR(100) ,
	   @BusinessPhone VARCHAR(60) ,
	   @BU VARCHAR(100) ,
	   @CancellationDate DATETIME ,
	   @City VARCHAR(100) ,
	   @CustomerContacts VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DeliveryAddress VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(50) ,
	   @DeliveryType INT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Status SMALLINT ,
	   @MethodofPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @ReferenceNo VARCHAR(100) ,
	   @Salesperson VARCHAR(100) ,
	   @State SMALLINT ,
	   @TermofPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalMiscChargeBaseAmount MONEY ,
	   @TotalMiscChargeConsumptionTaxAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartDeliveryOrderDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartDeliveryOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @BaseQtyDelivered FLOAT ,
	   @BaseQtyOrder FLOAT ,
	   @BatchNo VARCHAR(100) ,
	   @BU VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DeliveryOrderDetail VARCHAR(100) ,
	   @DeliveryOrderNo VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountBaseAmount MONEY ,
	   @DiscountPercentage FLOAT ,
	   @Location VARCHAR(100) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @RunningNumber INT ,
	   @SalesOrderDetail VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 28 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartMasterTOP
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartPOTypeTOPID INT ,
	   @SparePartMasterID INT ,
	   @Status BIT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartPOTypeTOP
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartPOType VARCHAR(5) ,
	   @IsTOP BIT ,
	   @TermOfPaymentIDNotTOP INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartPRDetailFromVendor
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @PRDetailNumber VARCHAR(50) ,
	   @SparePartPRID INT ,
	   @PRNumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @BaseReceivedQuantity DECIMAL(18, 9) ,
	   @BatchNumber VARCHAR(100) ,
	   @DealerCode VARCHAR(100) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @EngineNumber VARCHAR(50) ,
	   @EventData VARCHAR(1000) ,
	   @InventoryUnit VARCHAR(100) ,
	   @KeyNumber VARCHAR(50) ,
	   @LandedCost MONEY ,
	   @Location VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @ProductVolume DECIMAL(18, 9) ,
	   @ProductWeight DECIMAL(18, 9) ,
	   @PurchaseUnit VARCHAR(100) ,
	   @ReceivedQuantity DECIMAL(18, 9) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @ReturnPRDetail VARCHAR(100) ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @StockNumber VARCHAR(100) ,
	   @TitleRegistrationFee MONEY ,
	   @TotalAmount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalVolume DECIMAL(18, 9) ,
	   @TotalWeight DECIMAL(18, 9) ,
	   @TransactionAmount MONEY ,
	   @UnitCost MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartPRFromVendor
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @PRNumber NVARCHAR(50) ,
	   @PONumber VARCHAR(100) ,
	   @Owner VARCHAR(100) ,
	   @APVoucherNumber VARCHAR(100) ,
	   @AssignLandedCost BIT ,
	   @AutoInvoiced BIT ,
	   @DealerCode VARCHAR(100) ,
	   @DeliveryOrderDate DATETIME ,
	   @DeliveryOrderNumber VARCHAR(50) ,
	   @EventData VARCHAR(4000) ,
	   @EventData2 TEXT ,
	   @GrandTotal MONEY ,
	   @Handling VARCHAR(100) ,
	   @LoadData BIT ,
	   @PackingSlipDate DATETIME ,
	   @PackingSlipNumber VARCHAR(50) ,
	   @PRReferenceRequired BIT ,
	   @ReturnPRNumber VARCHAR(100) ,
	   @State VARCHAR(100) ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTax1Amount MONEY ,
	   @TotalConsumptionTax2Amount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalTitleRegistrationFree MONEY ,
	   @TransactionDate DATETIME ,
	   @TransferOrderRequestingNumber VARCHAR(100) ,
	   @Type VARCHAR(100) ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @VendorInvoiceNumber VARCHAR(50) ,
	   @WONumber VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(100) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(100) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartSalesOrder
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SalesChannel SMALLINT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @DealerCode VARCHAR(100) ,
	   @Customer VARCHAR(100) ,
	   @CustomerNo VARCHAR(50) ,
	   @DownPaymentAmount MONEY ,
	   @DownPaymentAmountReceived MONEY ,
	   @DownPaymentIsPaid BIT ,
	   @ExternalReferenceNo VARCHAR(50) ,
	   @GrandTotal MONEY ,
	   @Handling SMALLINT ,
	   @MethodOfPayment VARCHAR(100) ,
	   @OrderType VARCHAR(100) ,
	   @SalesOrderNo VARCHAR(50) ,
	   @SalesPerson VARCHAR(100) ,
	   @ShipmentType VARCHAR(50) ,
	   @State VARCHAR(50) ,
	   @TermOfPayment VARCHAR(100) ,
	   @TotalAmountBeforeDiscount MONEY ,
	   @TotalBaseAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TotalDiscountAmount MONEY ,
	   @TotalReceipt MONEY ,
	   @TransactionDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSparePartSalesOrderDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartSalesOrderID INT ,
	   @Owner VARCHAR(100) ,
	   @Status SMALLINT ,
	   @AmountBeforeDiscount MONEY ,
	   @BaseAmount MONEY ,
	   @KodeDealer VARCHAR(100) ,
	   @ConsumptionTax1Amount MONEY ,
	   @ConsumptionTax1 VARCHAR(100) ,
	   @ConsumptionTax2Amount MONEY ,
	   @ConsumptionTax2 VARCHAR(100) ,
	   @DiscountAmount MONEY ,
	   @DiscountPercentAge DECIMAL(18, 9) ,
	   @ProductCrossReference VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @Product VARCHAR(100) ,
	   @PromiseDate DATETIME ,
	   @QtyDelivered FLOAT ,
	   @QtyOrder FLOAT ,
	   @RequestDate DATETIME ,
	   @SalesOrderDetailID VARCHAR(50) ,
	   @SalesOrderNo VARCHAR(100) ,
	   @SalesUnit VARCHAR(100) ,
	   @Site VARCHAR(100) ,
	   @TotalAmount MONEY ,
	   @TotalConsumptionTaxAmount MONEY ,
	   @TransactionAmount MONEY ,
	   @UnitPrice MONEY ,
	   @Warehouse VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateSPKChassis
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SPKDetailID INT ,
	   @ChassisMasterID INT ,
	   @MatchingType SMALLINT ,
	   @MatchingDate DATETIME ,
	   @MatchingNumber VARCHAR(50) ,
	   @ReferenceNumber VARCHAR(50) ,
	   @KeyNumber VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(50) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 02 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateStandardCodeChar
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @Category VARCHAR(100) ,
	   @ValueId VARCHAR(5) ,
	   @ValueDesc VARCHAR(200) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME ,
	   @Sequence INT
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 03 September 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPBlockStatus
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartPOStatusID INT ,
	   @Status INT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPCreditAccount
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @TermOfPaymentID INT ,
	   @KelipatanPembayaran INT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPDeposit
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @AmountC2 MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPDueDate
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @SparePartBillingID INT ,
	   @DueDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPTransferActual
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @RefTransferBank VARCHAR(100) ,
	   @Amount MONEY ,
	   @PostingDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPTransferCeiling
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @CreditAccount VARCHAR(20) ,
	   @ProductCategoryID SMALLINT ,
	   @PaymentType SMALLINT ,
	   @EffectiveDate DATETIME ,
	   @BalanceBefore MONEY ,
	   @AvailableCeiling MONEY ,
	   @LastSyncDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(20) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 13, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPTransferCeilingDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @TOPSPTransferCeilingID INT ,
	   @SparepartBillingID INT ,
	   @TOPSPTransferPaymentID INT ,
	   @Amount MONEY ,
	   @IsIncome SMALLINT ,
	   @Status SMALLINT ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdatedBy VARCHAR(20) ,
	   @LastUpdatedTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPTransferPayment
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @DealerID SMALLINT ,
	   @CreditAccount VARCHAR(6) ,
	   @RegNumber VARCHAR(15) ,
	   @DueDate DATETIME ,
	   @PaymentPurposeID TINYINT ,
	   @TransferPlanDate DATETIME ,
	   @TOPSPTransferPaymentIDReff INT ,
	   @IsAccelerated SMALLINT ,
	   @Status SMALLINT ,
	   @ValidatedBy VARCHAR(20) ,
	   @ValidatedTime DATETIME ,
	   @ConfirmedBy VARCHAR(20) ,
	   @ConfirmedTime DATETIME ,
	   @CanceledBy VARCHAR(20) ,
	   @CanceledTime DATETIME ,
	   @TransferAmount MONEY ,
	   @TransferActualDate DATETIME ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, September 10, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateTOPSPTransferPaymentDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @TOPSPTransferPaymentID INT ,
	   @SparePartBillingID INT ,
	   @Amount MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(20) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateVehiclePurchaseDetail
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @VehiclePurchaseHeaderID INT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @CloseLine BIT ,
	   @CloseLineName VARCHAR(100) ,
	   @CloseReason VARCHAR(100) ,
	   @Completed BIT ,
	   @CompletedName VARCHAR(100) ,
	   @ProductDescription VARCHAR(100) ,
	   @ProductName VARCHAR(100) ,
	   @ProductVariantName VARCHAR(100) ,
	   @PODetail VARCHAR(50) ,
	   @POName VARCHAR(100) ,
	   @PRDetailName VARCHAR(100) ,
	   @PurchaseUnitName VARCHAR(100) ,
	   @QtyOrder FLOAT ,
	   @QtyReceipt FLOAT ,
	   @QtyReturn FLOAT ,
	   @RecallProduct BIT ,
	   @RecallProductName VARCHAR(50) ,
	   @SODetailName VARCHAR(100) ,
	   @ScheduledShippingDate DATETIME ,
	   @ServicePartsAndMaterial VARCHAR(100) ,
	   @ShippingDate DATETIME ,
	   @Site VARCHAR(100) ,
	   @StockNumberName VARCHAR(100) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(50) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateVehiclePurchaseHeader
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @BUCode VARCHAR(20) ,
	   @BUName VARCHAR(100) ,
	   @DeliveryMethod VARCHAR(10) ,
	   @Description VARCHAR(100) ,
	   @PRPOTypeName VARCHAR(100) ,
	   @DMSPONo VARCHAR(50) ,
	   @DMSPOStatus INT ,
	   @DMSPODate DATETIME ,
	   @VendorDescription VARCHAR(100) ,
	   @Vendor VARCHAR(100) ,
	   @PurchaseOrderNo VARCHAR(50) ,
	   @PurchaseReceiptNo VARCHAR(50) ,
	   @PurchaseReceiptDetailNo VARCHAR(50) ,
	   @ChassisModel VARCHAR(50) ,
	   @ChassisNumberRegister VARCHAR(50) ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(50) ,
	   @CreatedTime DATETIME ,
	   @LastUpdateBy VARCHAR(50) ,
	   @LastUpdateTime DATETIME
AS
	   SET @Result = ''
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Juni 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE up_ValidateVWI_BusinessSector
	   @Result VARCHAR(1000) ,
	   @ID INT OUTPUT ,
	   @BusinessName VARCHAR(603) ,
	   @Code VARCHAR(2)
AS
	   SET @Result = ''
GO

COMMIT
GO


