set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter PROCEDURE sp_TOPCombineCeiling
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
                     DECLARE       @i INT ,
                                  @n INT 
       
                     INSERT INTO #header
                                  (
                                    ProductCategoryID ,
                                    ProductCategory ,
                                    CreditAccount ,
                                    PaymentType ,
                                    PaymentTypeCode
                                  )
                     SELECT pc.ID ,
                                  pc.Code ,
                                  ca.CreditAccount ,
                                  tp.PaymentType ,
                                  CASE tp.PaymentType
                                    WHEN 1 THEN 'COD'
                                    ELSE 'TOP'
                                  END PaymentTypeCode
                     FROM   (
                                    SELECT      *
                                    FROM        ProductCategory pc
                                    WHERE              pc.ID IN ( 1, 2 )
                                  ) pc
                     JOIN   v_CreditAccount ca ON 1 = 1
                     JOIN   (
                                    SELECT DISTINCT
                                                       ( tp.PaymentType )
                                    FROM        TermOfPayment tp
                                    WHERE              tp.PaymentType IN ( 1, 2 )
                                  ) tp ON 1 = 1
                     WHERE  1 = 1
                                  AND pc.ID = CASE WHEN @ProductCategoryID = 0 THEN pc.ID
                                                              ELSE @ProductCategoryID
                                                       END
                                  AND (
                                           ca.CreditAccount IN ( SELECT    [fn_DELIMITED].[value]
                                                                                  FROM       [dbo].[fn_DELIMITED](@CreditAccount, ';') )
                                           OR @CreditAccount = ''
                                         )
              --case when @CreditAccount='' then ca.CreditAccount else @CreditAccount end 
                                  AND tp.PaymentType = CASE WHEN @PaymentType = 0 THEN tp.PaymentType
                                                                             ELSE @PaymentType
                                                                     END 


                     DECLARE       @vProductCategoryID INT ,
                                  @vProductCategory VARCHAR(10) ,
                                  @vCreditAccount VARCHAR(10) ,
                                  @vPaymentType SMALLINT ,
                                  @vPaymentTypeCode VARCHAR(10) 

              --Finish Unit
                     SET @i = 1
                     SELECT @n = MAX(ID)
                     FROM   #header
              
                     WHILE ( @i <= @n )
                             BEGIN
                
              
                                         SELECT @vProductCategoryID = h.ProductCategoryID ,
                                                       @vProductCategory = h.ProductCategory ,
                                                       @vCreditAccount = h.CreditAccount ,
                                                       @vPaymentType = h.PaymentType ,
                                                       @vPaymentTypeCode = h.PaymentTypeCode
                                         FROM   #header h
                                         WHERE  h.ID = @i 
              
               
                                         INSERT INTO #rep
                                         SELECT 1 ,
                                                       'Finish Unit' ,
                                                       a.*
                                         FROM   dbo.fn_TransferBalance(@vProductCategoryID, @vCreditAccount, @vPaymentType, @StartDate,
                                                                                            @EndDate, @IsReport, @IsShowReportDetail) a
                           
                                         SET @i = @i + 1
                             END 

   
       --SParepart
                     SET @i = 1
                     SELECT @n = MAX(ID)
                     FROM   #header
              
                     WHILE ( @i <= @n )
                             BEGIN
              
              
                                         SELECT @vProductCategoryID = h.ProductCategoryID ,
                                                       @vProductCategory = h.ProductCategory ,
                                                       @vCreditAccount = h.CreditAccount ,
                                                       @vPaymentType = h.PaymentType ,
                                                       @vPaymentTypeCode = h.PaymentTypeCode
                                         FROM   #header h
                                         WHERE  h.ID = @i 
              
               
                                         INSERT INTO #rep
                                         SELECT 2 ,
                                                       'Spare Part' ,
                                                       a.*
                                         FROM   dbo.fn_TOPSPBalance(@vProductCategoryID, @vCreditAccount, @vPaymentType, @StartDate,
                                                                                         @EndDate, @IsReport, @IsShowReportDetail) a
                           
                                         SET @i = @i + 1
                             END 


       

       --Gabungan
                     IF @IsShowReportDetail = 0
                        BEGIN 
                                   SET @i = 1
                                  SELECT       @n = MAX(ID)
                                  FROM  #header
              
                                  WHILE ( @i <= @n )
                                            BEGIN
              
              
                                                        SELECT       @vProductCategoryID = h.ProductCategoryID ,
                                                                     @vProductCategory = h.ProductCategory ,
                                                                     @vCreditAccount = h.CreditAccount ,
                                                                     @vPaymentType = h.PaymentType ,
                                                                     @vPaymentTypeCode = h.PaymentTypeCode
                                                       FROM  #header h
                                                       WHERE h.ID = @i 
              
                                                        INSERT       INTO #rep
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
                                                       SELECT       3 ,
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
                                                       FROM  #rep a
                                                       WHERE a.CreditAccount = @vCreditAccount
                                                       GROUP BY a.ProductCategoryID ,
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
                                  SELECT       1 ID ,
                                                *
                                  FROM  #rep r
                                  ORDER BY r.CreditAccount ,
                                                r.ROrder
                        END
                     ELSE
                        BEGIN

                                  SELECT       0 ID,*
                                  FROM  (
                                                  SELECT      [a].[ROrder] ,
                                                                     [a].[ReportKind] ,
                                                                     [a].[ProductCategoryID] ,
                                                                     [a].[ProductCategory] ,
                                                                     [a].[CreditAccount] ,
                                                                     [a].[PaymentType] ,
                                                                     [a].[PaymentTypeCode] ,
                                                                     CASE WHEN [a].[Data] = '01. Plafon'
                                                                                    AND a.[ROrder] = 1 THEN '02. Plafon FU'
                                                                           WHEN [a].[Data] = '01. Plafon'
                                                                                    AND a.[ROrder] = 2 THEN '06. Plafon Spare Part'
                                                                           WHEN [a].[Data] = '02. Total SO'
                                                                                    AND a.[ROrder] = 1 THEN '03. Total SO FU'
                                                                           WHEN [a].[Data] = '02. Total SO'
                                                                                    AND a.[ROrder] = 2 THEN '07. Total Billing Spare Part'
                                                                           WHEN [a].[Data] = '03. Total Payment'
                                                                                    AND a.[ROrder] = 1 THEN '04. Payment SO FU'
                                                                           WHEN [a].[Data] = '03. Total Payment'
                                                                                    AND a.[ROrder] = 2 THEN '08. Payment Billing Spare Part'
                                                                           WHEN [a].[Data] = '04. Available Ceiling'
                                                                                    AND a.[ROrder] = 1 THEN '05. Available Ceiling FU'
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
                                                  FROM        #rep a
                                                  WHERE              a.[CreditAccount] = @CreditAccount
                                                                     AND a.[Data] IN ( '01. Plafon', '02. Total SO', '03. Total Payment',
                                                                                                  '04. Available Ceiling' )
                                                --ORDER BY [a].[ROrder]
                                                  UNION  ALL
                                                  SELECT      1 ,
                                                                     'Gabungan' ,
                                                                     [a].[ProductCategoryID] ,
                                                                     [a].[ProductCategory] ,
                                                                     [a].[CreditAccount] ,
                                                                     [a].[PaymentType] ,
                                                                     [a].[PaymentTypeCode] ,
                                                                     '01. Plafon Gabungan' AS [Data] ,
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
                                                  FROM        #rep a
                                                  WHERE              a.[CreditAccount] = @CreditAccount
                                                                     AND a.[Data] = '01. Plafon'
                                                  GROUP BY    [a].[ProductCategoryID] ,
                                                                     [a].[ProductCategory] ,
                                                                     [a].[CreditAccount] ,
                                                                     [a].[PaymentType] ,
                                                                     [a].[PaymentTypeCode]
                                                  UNION  ALL
                                                  SELECT      1 ,
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
                                                  FROM        #rep a
                                                  WHERE              a.[CreditAccount] = @CreditAccount
                                                                     AND a.[Data] = '04. Available Ceiling'
                                                  GROUP BY    [a].[ProductCategoryID] ,
                                                                     [a].[ProductCategory] ,
                                                                     [a].[CreditAccount] ,
                                                                     [a].[PaymentType] ,
                                                                     [a].[PaymentTypeCode]
                                                ) X
                                  ORDER BY [X].[Data]
                        END

                         
       
         END
go

commit
go




 
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertPrice]    Script Date: 2018-12-21 08:58:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_InsertPrice]
	    @ID INT OUTPUT  ,
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

						    SET @ID = 0
	   END


	   go




	    
GO
/****** Object:  StoredProcedure [dbo].[sp_SetTOPSPOrderDueDate]    Script Date: 2019-01-10 14:04:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_SetTOPSPOrderDueDate]
	  (
		@BillingNumber VARCHAR(25) ,
		@DealerCode VARCHAR(20)
	  )
AS
	  BEGIN

			SET NOCOUNT ON;
			DECLARE	@TVAL INT = NULL ,
					@DATe DATETIME
			DECLARE	@SPBillingID INT= NULL

			SELECT TOP 1
					@SPBillingID = a.ID ,
					@DATe = a.BillingDate ,
					@TVAL = d.TermOfPaymentValue
			FROM	dbo.SparePartBilling a
			INNER JOIN dbo.SparePartBillingDetail bd ON bd.SparePartBillingID = a.ID
														AND bd.RowStatus = a.RowStatus
			INNER JOIN SparePartDODetail spdd ON spdd.ID = bd.SparePartDODetailID
												 AND spdd.[RowStatus] = 0
			INNER JOIN dbo.SparePartPOEstimate spe ON spe.ID = spdd.SparePartPOEstimateID
													  AND spe.[RowStatus] = 0
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

					 UPDATE	TOP (1) dbo.TOPSPDueDate
					 SET	DueDate = dbo.ufn_GetActiveDate(DATEADD(DAY, @TVAL, @DATe)) ,
							RowStatus = 0 ,
							LastUpdateTime = GETDATE() ,
							LastUpdateBy = 'WS'
					 WHERE	SparePartBillingID = @SPBillingID
						 

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
					 FROM	dbo.SparePartBilling a
					 WHERE	a.ID = @SPBillingID
							AND a.ID NOT IN ( SELECT	b.SparePartBillingID
											  FROM		dbo.TOPSPDueDate b
											  WHERE		b.[RowStatus] = 0
														AND b.SparePartBillingID = @SPBillingID )

			   END
		 

	  END
