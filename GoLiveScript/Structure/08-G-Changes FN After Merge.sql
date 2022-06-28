set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

drop function FNI_GetLatestSPKForChassis
go

--===============================================================================================================================================    
-- Created By : Prins Carl (Mitrais)    
-- Get the SPK Invoice Revision    
--===============================================================================================================================================    
create FUNCTION FNI_GetLatestSPKForChassis ( @ChassisMasterID INT )  
RETURNS @returnSPKChassis TABLE  
    (  
   ID INT ,  
   ChassisMasterID INT ,  
   EndCustomerID INT ,  
   RegNumber VARCHAR(15) ,  
   SPKHeaderID INT ,  
   DealerID INT  
    )  
AS  
 BEGIN    
  IF NOT EXISTS ( SELECT *  
      FROM RevisionFaktur  
      WHERE ChassisMasterID = @ChassisMasterID )  
     BEGIN    
     INSERT INTO @returnSPKChassis  
      (  
        ID ,  
        ChassisMasterID ,  
        EndCustomerID ,  
        RegNumber ,  
        SPKHeaderID ,  
        DealerID  
      )  
     SELECT a.ID ,  
      a.ID AS ChassisMasterID ,  
      a.EndCustomerID ,  
      NULL AS RegNumber ,  
      b.SPKHeaderID ,  
      c.DealerID  
     FROM ChassisMaster a (NOLOCK)  
     LEFT JOIN SPKFaktur b (NOLOCK) ON a.EndCustomerID = b.EndCustomerID  
            AND b.RowStatus = 0  
     LEFT JOIN SPKHeader c  (NOLOCK) ON b.SPKHeaderID = c.ID  
            AND c.RowStatus = 0  
     WHERE a.ID = @ChassisMasterID  
      AND a.RowStatus = 0    
     END    
  ELSE  
     BEGIN    
     DECLARE @Id INT ,  
       @ChassisMasterRefID INT ,  
       @EndCustomerID INT ,  
       @RegNumber VARCHAR(15) ,  
       @SPKHeaderID INT ,  
       @DealerID INT    
    
     DECLARE dnet_agent_cursor CURSOR STATIC  
     FOR  
       SELECT a.ID ,  
        a.ChassisMasterID ,  
        a.EndCustomerID ,  
        a.RegNumber ,  
        COALESCE(b.SPKHeaderID, d.SPKHeaderID) AS SPKHeaderID ,  
        COALESCE(c.DealerID, e.DealerID) AS DealerID  
       FROM RevisionFaktur a (NOLOCK)  
       LEFT JOIN RevisionSPKFaktur b (NOLOCK) ON a.EndCustomerID = b.EndCustomerID  
                AND b.RowStatus = 0  
       LEFT JOIN SPKHeader c (NOLOCK) ON  b.SPKHeaderID = c.ID  
              AND c.RowStatus = 0  
       LEFT JOIN SPKFaktur d (NOLOCK) ON a.OldEndCustomerID = d.EndCustomerID  
              AND d.RowStatus = 0  
       LEFT JOIN SPKHeader e (NOLOCK) ON d.SPKHeaderID = e.ID  
              AND e.RowStatus = 0  
       WHERE a.RowStatus = 0  
        AND ChassisMasterID = @ChassisMasterID  
       ORDER BY a.ID DESC    
     OPEN dnet_agent_cursor    
     FETCH NEXT FROM dnet_agent_cursor INTO @Id, @ChassisMasterRefID, @EndCustomerID, @RegNumber, @SPKHeaderID, @DealerID    
     WHILE @@FETCH_STATUS = 0  
        BEGIN    
        IF NOT EXISTS ( SELECT *  
            FROM @returnSPKChassis )  
        BEGIN  
           INSERT INTO @returnSPKChassis  
            (  
              ID ,  
              ChassisMasterID ,  
			  EndCustomerID,
              RegNumber ,  
              SPKHeaderID ,  
              DealerID  
            )  
           SELECT @Id AS ID ,  
            @ChassisMasterRefID AS ChassisMasterID ,  
			@EndCustomerID AS EndCustomerID,
            @RegNumber AS RegNumber ,  
            @SPKHeaderID AS SPKHeaderID ,  
            @DealerID AS DealerID   
        END  
  
        IF @SPKHeaderID IS NOT NULL  
        BEGIN    
           UPDATE @returnSPKChassis  
           SET  RegNumber = @RegNumber ,  
            SPKHeaderID = @SPKHeaderID ,  
            DealerID = @DealerID  
           WHERE  ChassisMasterID = @ChassisMasterID  
           BREAK    
        END    
        FETCH NEXT FROM dnet_agent_cursor INTO @Id, @ChassisMasterRefID, @EndCustomerID, @RegNumber, @SPKHeaderID, @DealerID    
        END    
     CLOSE dnet_agent_cursor    
     DEALLOCATE dnet_agent_cursor    
     END     
  RETURN    
 END;    
    
    
    
    
    
    
--select a.Id, a.ChassisMasterID --, a.EndCustomerID, a.OldEndCustomerID,     
--       , a.RegNumber --, a.RevisionStatus, a.RevisionTypeID    
--       , coalesce(b.SPKHeaderID, d.SPKHeaderID) as SPKHeaderID    
--    , coalesce(c.DealerID, e.DealerID) as DealerID    
--from RevisionFaktur a   
--left join RevisionSPKFaktur b on a.EndCustomerID = b.EndCustomerID and b.RowStatus = 0    
--left join SPKHeader c on b.SPKHeaderID = c.ID and c.RowStatus = 0    
--left join SPKFaktur d on a.OldEndCustomerID = d.EndCustomerID and d.RowStatus = 0    
--left join SPKHeader e on d.SPKHeaderID = e.ID and e.RowStatus = 0    
--where a.RowStatus = 0    
--      and a.ChassisMasterID = 1505375    
--Order by a.ID desc
go

drop function fn_TOPSPBalance
go

/*

exec sp_TransferBalance 1,'100002',1,'2016.10.1','2016.10.31',1,1

*/



create FUNCTION fn_TOPSPBalance
    (
      --DECLARE 
      @ProductCategoryID INT = 1--can't be all
      ,
      @CreditAccount VARCHAR(10) = ''--can't be all
      ,
      @PaymentType SMALLINT = 2--can't be all
      ,
      @StartDate DATETIME = '20180901' ,
      @EndDate DATETIME = '20180930' ,
      @IsReport SMALLINT = 1 ,
      @IsShowReportDetail SMALLINT = 0
    )
--RETURNS 
--DECLARE 
RETURNS @result TABLE
    (
      --ID int identity(1,1)  
      ProductCategoryID INT ,
      ProductCategory VARCHAR(10) ,
      CreditAccount VARCHAR(10) ,
      PaymentType SMALLINT ,
      PaymentTypeCode VARCHAR(20) ,
      Data VARCHAR(50) ,
      D_1 MONEY ,
      D_2 MONEY ,
      D_3 MONEY ,
      D_4 MONEY ,
      D_5 MONEY ,
      D_6 MONEY ,
      D_7 MONEY ,
      D_8 MONEY ,
      D_9 MONEY ,
      D_10 MONEY ,
      D_11 MONEY ,
      D_12 MONEY ,
      D_13 MONEY ,
      D_14 MONEY ,
      D_15 MONEY ,
      D_16 MONEY ,
      D_17 MONEY ,
      D_18 MONEY ,
      D_19 MONEY ,
      D_20 MONEY ,
      D_21 MONEY ,
      D_22 MONEY ,
      D_23 MONEY ,
      D_24 MONEY ,
      D_25 MONEY ,
      D_26 MONEY ,
      D_27 MONEY ,
      D_28 MONEY ,
      D_29 MONEY ,
      D_30 MONEY ,
      D_31 MONEY
    )
AS
    BEGIN
 
        DECLARE @rep TABLE
            (
              Data VARCHAR(50) ,
              D INT ,
              Amount MONEY
            )
        DECLARE @tc TABLE
            (
              D INT ,
              BalanceBefore MONEY ,
              TotalSO MONEY ,
              TotalPayment MONEY ,
              AvailableCeiling MONEY ,
              TotalPO MONEY ,
              TotalPOAgg MONEY ,
              Balance MONEY
            ) 
        DECLARE @po TABLE ( D INT, Amount MONEY ) 	
        DECLARE @header TABLE
            (
              ID INT IDENTITY(1, 1) ,
              ProductCategoryID INT ,
              ProductCategory VARCHAR(10) ,
              CreditAccount VARCHAR(10) ,
              PaymentType SMALLINT ,
              PaymentTypeCode VARCHAR(10) ,
              Data VARCHAR(50)
            )
        DECLARE @day INT ,
            @nDay INT ,
            @TempTotalPO MONEY
        DECLARE @ProductCategory VARCHAR(10) ,
            @PaymentTypeCode VARCHAR(20)
	
        BEGIN --Parameter Initialization	
            SET @EndDate = DATEADD(SECOND, -1, DATEADD(DAY, 1, @EndDate))
		
            SET @day = 1
            SET @nDay = 1 + DATEDIFF(DAY, @StartDate, @EndDate)
        END
	
        BEGIN--Data Preparation
		--header
            SELECT  @ProductCategory = pc.Code
            FROM    ProductCategory pc
            WHERE   pc.ID = @ProductCategoryID
            SET @PaymentTypeCode = CASE WHEN @PaymentType = 1 THEN 'COD'
                                        ELSE 'TOP'
                                   END 
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '01. Plafon'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '02. Total SO'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '03. Total Payment'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '04. Available Ceiling'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '05. Daily Outstanding PO'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            '06. Agg Outstanding PO'
            INSERT  INTO @header
                    SELECT  @ProductCategoryID ,
                            @ProductCategory ,
                            @CreditAccount ,
                            @PaymentType ,
                            @PaymentTypeCode ,
                            'Balance of The Day'	
		
		--Outstanding PO in D-Net
            INSERT  INTO @po
                    SELECT  DAY(y.BillingDate) ,
                            SUM(y.TotalAmount + y.Tax)
                    FROM    ( SELECT    a.BillingNumber
                              FROM      dbo.SparePartBilling a
                                        INNER JOIN dbo.SparePartBillingDetail bd ON bd.SparePartBillingID = a.ID
                                                              AND bd.RowStatus = a.RowStatus
                                        INNER JOIN SparePartDODetail spdd ON spdd.ID = bd.SparePartDODetailID
                                        INNER JOIN dbo.SparePartPOEstimate spe ON spe.ID = spdd.SparePartPOEstimateID
                                        INNER JOIN dbo.SparePartPO c ON c.ID = spe.SparePartPOID
                                                              AND c.RowStatus = 0
                                        INNER JOIN dbo.TermOfPayment d ON d.ID = c.TermOfPaymentID
                                        INNER JOIN dbo.Dealer dd ON dd.ID = a.DealerID
                              WHERE     1 = 1
                                        AND a.RowStatus = 0
                                        AND dd.CreditAccount = @CreditAccount
                                        AND a.BillingDate BETWEEN @StartDate AND @EndDate
                                        AND a.RowStatus = 0
                                        AND d.PaymentType = @PaymentType
                              GROUP BY  a.BillingNumber
                            ) X
                            INNER JOIN dbo.SparePartBilling y ON y.BillingNumber = X.BillingNumber
                    WHERE   y.RowStatus = 0
                    GROUP BY DAY(y.BillingDate)
                   
				
			 
		--Aggregate Amount by Day
            INSERT  INTO @tc
                    SELECT  DAY(tc.EffectiveDate) ,
                            tc.BalanceBefore ,
                            ISNULL(tcd.[TotalSO], 0) ,
                            ISNULL(tcd.[TotalPayment], 0) ,
                            tc.AvailableCeiling ,
                            ISNULL(po.Amount, 0) ,
                            0 ,
                            0
                    FROM    TOPSPTransferCeiling tc WITH ( NOLOCK )
                            OUTER APPLY ( SELECT    SUM(CASE WHEN ISNULL(tcd.SparepartBillingID,
                                                              0) > 0
                                                             THEN tcd.Amount
                                                             ELSE 0
                                                        END) TotalSO ,
                                                    SUM(CASE WHEN ISNULL(tcd.TOPSPTransferPaymentID,
                                                              0) > 0
                                                             THEN tcd.Amount
                                                             ELSE 0
                                                        END) TotalPayment
                                          FROM      dbo.TOPSPTransferCeilingDetail tcd ( NOLOCK )
                                          WHERE     tcd.TOPSPTransferCeilingID = tc.ID
                                                    AND tcd.RowStatus = 0
                                        ) tcd
                            OUTER APPLY ( SELECT    SUM(ISNULL(po.Amount, 0)) Amount
                                          FROM      @po po
                                          WHERE     po.D = DAY(tc.EffectiveDate)
                                        ) po
                    WHERE   1 = 1
                            AND tc.ProductCategoryID = @ProductCategoryID
                            AND tc.PaymentType = @PaymentType
                            AND tc.CreditAccount = @CreditAccount
                            AND tc.EffectiveDate >= @StartDate
                            AND tc.EffectiveDate <= @EndDate
                            AND tc.RowStatus = 0
				 
		
		--Aggregate Calculation	
            SET @TempTotalPO = 0
            WHILE ( @day <= @nDay )
                BEGIN
                    SELECT  @TempTotalPO = @TempTotalPO + tc.TotalPO
                    FROM    @tc tc
                    WHERE   tc.D = @day 
			
                    UPDATE  @tc
                    SET     TotalPOAgg = @TempTotalPO
                    WHERE   D = @day 
                    SET @day = @day + 1
                END
            UPDATE  @tc
            SET     Balance = CASE WHEN @IsShowReportDetail =0 THEN  AvailableCeiling ELSE  AvailableCeiling - TotalPOAgg  END
        END 
	
		
        BEGIN--Report Generation
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '01. Plafon' ,
                            tc.D ,
                            tc.BalanceBefore
                    FROM    @tc tc
                    ORDER BY tc.D 
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '02. Total SO' ,
                            tc.D ,
                            tc.TotalSO
                    FROM    @tc tc
                    ORDER BY tc.D 
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '03. Total Payment' ,
                            tc.D ,
                            tc.TotalPayment
                    FROM    @tc tc
                    ORDER BY tc.D
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '04. Available Ceiling' ,
                            tc.D ,
                            tc.AvailableCeiling
                    FROM    @tc tc
                    ORDER BY tc.D 
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '05. Daily Outstanding PO' ,
                            tc.D ,
                            tc.TotalPO
                    FROM    @tc tc
                    ORDER BY tc.D 
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  '06. Agg Outstanding PO' ,
                            tc.D ,
                            tc.TotalPOAgg
                    FROM    @tc tc
                    ORDER BY tc.D 
            INSERT  INTO @rep
                    ( Data ,
                      D ,
                      Amount
                    )
                    SELECT  'Balance of The Day' ,
                            tc.D ,
                            tc.Balance
                    FROM    @tc tc
                    ORDER BY tc.D 
				
            IF ( @IsReport = 0 )
                BEGIN
                    INSERT  INTO @result
                            SELECT  @ProductCategoryID ,
                                    @ProductCategory ,
                                    @CreditAccount ,
                                    @PaymentType ,
                                    @PaymentTypeCode ,
                                    h.Data ,
                                    SUM(ISNULL(r.D_1, 0)) D_1 ,
                                    SUM(ISNULL(r.D_2, 0)) D_2 ,
                                    SUM(ISNULL(r.D_3, 0)) D_3 ,
                                    SUM(ISNULL(r.D_4, 0)) D_4 ,
                                    SUM(ISNULL(r.D_5, 0)) D_5 ,
                                    SUM(ISNULL(r.D_6, 0)) D_6 ,
                                    SUM(ISNULL(r.D_7, 0)) D_7 ,
                                    SUM(ISNULL(r.D_8, 0)) D_8 ,
                                    SUM(ISNULL(r.D_9, 0)) D_9 ,
                                    SUM(ISNULL(r.D_10, 0)) D_10 ,
                                    SUM(ISNULL(r.D_11, 0)) D_11 ,
                                    SUM(ISNULL(r.D_12, 0)) D_12 ,
                                    SUM(ISNULL(r.D_13, 0)) D_13 ,
                                    SUM(ISNULL(r.D_14, 0)) D_14 ,
                                    SUM(ISNULL(r.D_15, 0)) D_15 ,
                                    SUM(ISNULL(r.D_16, 0)) D_16 ,
                                    SUM(ISNULL(r.D_17, 0)) D_17 ,
                                    SUM(ISNULL(r.D_18, 0)) D_18 ,
                                    SUM(ISNULL(r.D_19, 0)) D_19 ,
                                    SUM(ISNULL(r.D_20, 0)) D_20 ,
                                    SUM(ISNULL(r.D_21, 0)) D_21 ,
                                    SUM(ISNULL(r.D_22, 0)) D_22 ,
                                    SUM(ISNULL(r.D_23, 0)) D_23 ,
                                    SUM(ISNULL(r.D_24, 0)) D_24 ,
                                    SUM(ISNULL(r.D_25, 0)) D_25 ,
                                    SUM(ISNULL(r.D_26, 0)) D_26 ,
                                    SUM(ISNULL(r.D_27, 0)) D_27 ,
                                    SUM(ISNULL(r.D_28, 0)) D_28 ,
                                    SUM(ISNULL(r.D_29, 0)) D_29 ,
                                    SUM(ISNULL(r.D_30, 0)) D_30 ,
                                    SUM(ISNULL(r.D_31, 0)) D_31
                            FROM    @header h
                                    LEFT JOIN ( SELECT  r.Data ,
                                                        CASE WHEN r.D = 1
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_1 ,
                                                        CASE WHEN r.D = 2
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_2 ,
                                                        CASE WHEN r.D = 3
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_3 ,
                                                        CASE WHEN r.D = 4
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_4 ,
                                                        CASE WHEN r.D = 5
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_5 ,
                                                        CASE WHEN r.D = 6
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_6 ,
                                                        CASE WHEN r.D = 7
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_7 ,
                                                        CASE WHEN r.D = 8
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_8 ,
                                                        CASE WHEN r.D = 9
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_9 ,
                                                        CASE WHEN r.D = 10
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_10 ,
                                                        CASE WHEN r.D = 11
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_11 ,
                                                        CASE WHEN r.D = 12
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_12 ,
                                                        CASE WHEN r.D = 13
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_13 ,
                                                        CASE WHEN r.D = 14
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_14 ,
                                                        CASE WHEN r.D = 15
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_15 ,
                                                        CASE WHEN r.D = 16
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_16 ,
                                                        CASE WHEN r.D = 17
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_17 ,
                                                        CASE WHEN r.D = 18
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_18 ,
                                                        CASE WHEN r.D = 19
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_19 ,
                                                        CASE WHEN r.D = 20
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_20 ,
                                                        CASE WHEN r.D = 21
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_21 ,
                                                        CASE WHEN r.D = 22
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_22 ,
                                                        CASE WHEN r.D = 23
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_23 ,
                                                        CASE WHEN r.D = 24
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_24 ,
                                                        CASE WHEN r.D = 25
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_25 ,
                                                        CASE WHEN r.D = 26
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_26 ,
                                                        CASE WHEN r.D = 27
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_27 ,
                                                        CASE WHEN r.D = 28
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_28 ,
                                                        CASE WHEN r.D = 29
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_29 ,
                                                        CASE WHEN r.D = 30
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_30 ,
                                                        CASE WHEN r.D = 31
                                                             THEN ISNULL(r.Amount,
                                                              0)
                                                             ELSE 0
                                                        END D_31
                                                FROM    @rep r
                                                WHERE   r.D = DAY(@EndDate)
                                              ) r ON r.Data = h.Data
                            WHERE   1 = 1
                            GROUP BY h.Data
                            ORDER BY h.Data 
                END 
            ELSE
                IF ( @IsReport = 1
                     AND @IsShowReportDetail = 0
                   )
                    BEGIN
                        INSERT  INTO @result
                                SELECT  @ProductCategoryID ,
                                        @ProductCategory ,
                                        @CreditAccount ,
                                        @PaymentType ,
                                        @PaymentTypeCode ,
                                        h.Data ,
                                        SUM(ISNULL(r.D_1, 0)) D_1 ,
                                        SUM(ISNULL(r.D_2, 0)) D_2 ,
                                        SUM(ISNULL(r.D_3, 0)) D_3 ,
                                        SUM(ISNULL(r.D_4, 0)) D_4 ,
                                        SUM(ISNULL(r.D_5, 0)) D_5 ,
                                        SUM(ISNULL(r.D_6, 0)) D_6 ,
                                        SUM(ISNULL(r.D_7, 0)) D_7 ,
                                        SUM(ISNULL(r.D_8, 0)) D_8 ,
                                        SUM(ISNULL(r.D_9, 0)) D_9 ,
                                        SUM(ISNULL(r.D_10, 0)) D_10 ,
                                        SUM(ISNULL(r.D_11, 0)) D_11 ,
                                        SUM(ISNULL(r.D_12, 0)) D_12 ,
                                        SUM(ISNULL(r.D_13, 0)) D_13 ,
                                        SUM(ISNULL(r.D_14, 0)) D_14 ,
                                        SUM(ISNULL(r.D_15, 0)) D_15 ,
                                        SUM(ISNULL(r.D_16, 0)) D_16 ,
                                        SUM(ISNULL(r.D_17, 0)) D_17 ,
                                        SUM(ISNULL(r.D_18, 0)) D_18 ,
                                        SUM(ISNULL(r.D_19, 0)) D_19 ,
                                        SUM(ISNULL(r.D_20, 0)) D_20 ,
                                        SUM(ISNULL(r.D_21, 0)) D_21 ,
                                        SUM(ISNULL(r.D_22, 0)) D_22 ,
                                        SUM(ISNULL(r.D_23, 0)) D_23 ,
                                        SUM(ISNULL(r.D_24, 0)) D_24 ,
                                        SUM(ISNULL(r.D_25, 0)) D_25 ,
                                        SUM(ISNULL(r.D_26, 0)) D_26 ,
                                        SUM(ISNULL(r.D_27, 0)) D_27 ,
                                        SUM(ISNULL(r.D_28, 0)) D_28 ,
                                        SUM(ISNULL(r.D_29, 0)) D_29 ,
                                        SUM(ISNULL(r.D_30, 0)) D_30 ,
                                        SUM(ISNULL(r.D_31, 0)) D_31
                                FROM    @header h
                                        LEFT JOIN ( SELECT  r.Data ,
                                                            CASE
                                                              WHEN r.D = 1
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_1 ,
                                                            CASE
                                                              WHEN r.D = 2
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_2 ,
                                                            CASE
                                                              WHEN r.D = 3
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_3 ,
                                                            CASE
                                                              WHEN r.D = 4
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_4 ,
                                                            CASE
                                                              WHEN r.D = 5
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_5 ,
                                                            CASE
                                                              WHEN r.D = 6
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_6 ,
                                                            CASE
                                                              WHEN r.D = 7
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_7 ,
                                                            CASE
                                                              WHEN r.D = 8
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_8 ,
                                                            CASE
                                                              WHEN r.D = 9
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_9 ,
                                                            CASE
                                                              WHEN r.D = 10
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_10 ,
                                                            CASE
                                                              WHEN r.D = 11
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_11 ,
                                                            CASE
                                                              WHEN r.D = 12
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_12 ,
                                                            CASE
                                                              WHEN r.D = 13
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_13 ,
                                                            CASE
                                                              WHEN r.D = 14
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_14 ,
                                                            CASE
                                                              WHEN r.D = 15
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_15 ,
                                                            CASE
                                                              WHEN r.D = 16
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_16 ,
                                                            CASE
                                                              WHEN r.D = 17
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_17 ,
                                                            CASE
                                                              WHEN r.D = 18
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_18 ,
                                                            CASE
                                                              WHEN r.D = 19
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_19 ,
                                                            CASE
                                                              WHEN r.D = 20
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_20 ,
                                                            CASE
                                                              WHEN r.D = 21
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_21 ,
                                                            CASE
                                                              WHEN r.D = 22
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_22 ,
                                                            CASE
                                                              WHEN r.D = 23
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_23 ,
                                                            CASE
                                                              WHEN r.D = 24
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_24 ,
                                                            CASE
                                                              WHEN r.D = 25
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_25 ,
                                                            CASE
                                                              WHEN r.D = 26
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_26 ,
                                                            CASE
                                                              WHEN r.D = 27
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_27 ,
                                                            CASE
                                                              WHEN r.D = 28
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_28 ,
                                                            CASE
                                                              WHEN r.D = 29
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_29 ,
                                                            CASE
                                                              WHEN r.D = 30
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_30 ,
                                                            CASE
                                                              WHEN r.D = 31
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                            END D_31
                                                    FROM    @rep r
                                                  ) r ON r.Data = h.Data
                                WHERE   1 = 1
                                        AND h.Data = 'Balance of The Day'
                                GROUP BY h.Data
                                ORDER BY h.Data
			
                    END 
                ELSE
                    IF ( @IsReport = 1
                         AND @IsShowReportDetail = 1
                       )
                        BEGIN		
                            INSERT  INTO @result
                                    SELECT  @ProductCategoryID ,
                                            @ProductCategory ,
                                            @CreditAccount ,
                                            @PaymentType ,
                                            @PaymentTypeCode ,
                                            h.Data ,
                                            SUM(ISNULL(r.D_1, 0)) D_1 ,
                                            SUM(ISNULL(r.D_2, 0)) D_2 ,
                                            SUM(ISNULL(r.D_3, 0)) D_3 ,
                                            SUM(ISNULL(r.D_4, 0)) D_4 ,
                                            SUM(ISNULL(r.D_5, 0)) D_5 ,
                                            SUM(ISNULL(r.D_6, 0)) D_6 ,
                                            SUM(ISNULL(r.D_7, 0)) D_7 ,
                                            SUM(ISNULL(r.D_8, 0)) D_8 ,
                                            SUM(ISNULL(r.D_9, 0)) D_9 ,
                                            SUM(ISNULL(r.D_10, 0)) D_10 ,
                                            SUM(ISNULL(r.D_11, 0)) D_11 ,
                                            SUM(ISNULL(r.D_12, 0)) D_12 ,
                                            SUM(ISNULL(r.D_13, 0)) D_13 ,
                                            SUM(ISNULL(r.D_14, 0)) D_14 ,
                                            SUM(ISNULL(r.D_15, 0)) D_15 ,
                                            SUM(ISNULL(r.D_16, 0)) D_16 ,
                                            SUM(ISNULL(r.D_17, 0)) D_17 ,
                                            SUM(ISNULL(r.D_18, 0)) D_18 ,
                                            SUM(ISNULL(r.D_19, 0)) D_19 ,
                                            SUM(ISNULL(r.D_20, 0)) D_20 ,
                                            SUM(ISNULL(r.D_21, 0)) D_21 ,
                                            SUM(ISNULL(r.D_22, 0)) D_22 ,
                                            SUM(ISNULL(r.D_23, 0)) D_23 ,
                                            SUM(ISNULL(r.D_24, 0)) D_24 ,
                                            SUM(ISNULL(r.D_25, 0)) D_25 ,
                                            SUM(ISNULL(r.D_26, 0)) D_26 ,
                                            SUM(ISNULL(r.D_27, 0)) D_27 ,
                                            SUM(ISNULL(r.D_28, 0)) D_28 ,
                                            SUM(ISNULL(r.D_29, 0)) D_29 ,
                                            SUM(ISNULL(r.D_30, 0)) D_30 ,
                                            SUM(ISNULL(r.D_31, 0)) D_31
                                    FROM    @header h
                                            LEFT JOIN ( SELECT
                                                              r.Data ,
                                                              CASE
                                                              WHEN r.D = 1
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_1 ,
                                                              CASE
                                                              WHEN r.D = 2
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_2 ,
                                                              CASE
                                                              WHEN r.D = 3
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_3 ,
                                                              CASE
                                                              WHEN r.D = 4
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_4 ,
                                                              CASE
                                                              WHEN r.D = 5
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_5 ,
                                                              CASE
                                                              WHEN r.D = 6
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_6 ,
                                                              CASE
                                                              WHEN r.D = 7
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_7 ,
                                                              CASE
                                                              WHEN r.D = 8
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_8 ,
                                                              CASE
                                                              WHEN r.D = 9
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_9 ,
                                                              CASE
                                                              WHEN r.D = 10
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_10 ,
                                                              CASE
                                                              WHEN r.D = 11
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_11 ,
                                                              CASE
                                                              WHEN r.D = 12
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_12 ,
                                                              CASE
                                                              WHEN r.D = 13
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_13 ,
                                                              CASE
                                                              WHEN r.D = 14
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_14 ,
                                                              CASE
                                                              WHEN r.D = 15
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_15 ,
                                                              CASE
                                                              WHEN r.D = 16
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_16 ,
                                                              CASE
                                                              WHEN r.D = 17
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_17 ,
                                                              CASE
                                                              WHEN r.D = 18
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_18 ,
                                                              CASE
                                                              WHEN r.D = 19
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_19 ,
                                                              CASE
                                                              WHEN r.D = 20
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_20 ,
                                                              CASE
                                                              WHEN r.D = 21
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_21 ,
                                                              CASE
                                                              WHEN r.D = 22
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_22 ,
                                                              CASE
                                                              WHEN r.D = 23
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_23 ,
                                                              CASE
                                                              WHEN r.D = 24
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_24 ,
                                                              CASE
                                                              WHEN r.D = 25
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_25 ,
                                                              CASE
                                                              WHEN r.D = 26
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_26 ,
                                                              CASE
                                                              WHEN r.D = 27
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_27 ,
                                                              CASE
                                                              WHEN r.D = 28
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_28 ,
                                                              CASE
                                                              WHEN r.D = 29
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_29 ,
                                                              CASE
                                                              WHEN r.D = 30
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_30 ,
                                                              CASE
                                                              WHEN r.D = 31
                                                              THEN ISNULL(r.Amount,
                                                              0)
                                                              ELSE 0
                                                              END D_31
                                                        FROM  @rep r
                                                      ) r ON r.Data = h.Data
                                    WHERE   1 = 1
                                    GROUP BY h.Data
                                    ORDER BY h.Data
                        END 
        END 
	
        RETURN 
    END
go

commit
go


--===============================================================================================================================================    
-- Created By : Prins Carl (Mitrais)    
-- Get the SPK Invoice Revision    
--===============================================================================================================================================    
alter FUNCTION FNI_GetLatestSPKForChassis ( @ChassisMasterID INT )  
RETURNS @returnSPKChassis TABLE  
    (  
   ID INT ,  
   ChassisMasterID INT ,  
   EndCustomerID INT ,  
   RegNumber VARCHAR(15) ,  
   SPKHeaderID INT ,  
   DealerID INT  
    )  
AS  
 BEGIN    
  IF NOT EXISTS ( SELECT *  
      FROM RevisionFaktur  
      WHERE ChassisMasterID = @ChassisMasterID )  
     BEGIN    
     INSERT INTO @returnSPKChassis  
      (  
        ID ,  
        ChassisMasterID ,  
        EndCustomerID ,  
        RegNumber ,  
        SPKHeaderID ,  
        DealerID  
      )  
     SELECT a.ID ,  
      a.ID AS ChassisMasterID ,  
      a.EndCustomerID ,  
      NULL AS RegNumber ,  
      b.SPKHeaderID ,  
      c.DealerID  
     FROM ChassisMaster a (NOLOCK)  
     LEFT JOIN SPKFaktur b (NOLOCK) ON a.EndCustomerID = b.EndCustomerID  
            AND b.RowStatus = 0  
     LEFT JOIN SPKHeader c  (NOLOCK) ON b.SPKHeaderID = c.ID  
            AND c.RowStatus = 0  
     WHERE a.ID = @ChassisMasterID  
      AND a.RowStatus = 0    
     END    
  ELSE  
     BEGIN    
     DECLARE @Id INT ,  
       @ChassisMasterRefID INT ,  
       @EndCustomerID INT ,  
       @RegNumber VARCHAR(15) ,  
       @SPKHeaderID INT ,  
       @DealerID INT    
    
     DECLARE dnet_agent_cursor CURSOR STATIC  
     FOR  
       SELECT a.ID ,  
        a.ChassisMasterID ,  
        a.EndCustomerID ,  
        a.RegNumber ,  
        COALESCE(b.SPKHeaderID, d.SPKHeaderID) AS SPKHeaderID ,  
        COALESCE(c.DealerID, e.DealerID) AS DealerID  
       FROM RevisionFaktur a (NOLOCK)  
       LEFT JOIN RevisionSPKFaktur b (NOLOCK) ON a.EndCustomerID = b.EndCustomerID  
                AND b.RowStatus = 0  
       LEFT JOIN SPKHeader c (NOLOCK) ON  b.SPKHeaderID = c.ID  
              AND c.RowStatus = 0  
       LEFT JOIN SPKFaktur d (NOLOCK) ON a.OldEndCustomerID = d.EndCustomerID  
              AND d.RowStatus = 0  
       LEFT JOIN SPKHeader e (NOLOCK) ON d.SPKHeaderID = e.ID  
              AND e.RowStatus = 0  
       WHERE a.RowStatus = 0  
        AND ChassisMasterID = @ChassisMasterID  
       ORDER BY a.ID DESC    
     OPEN dnet_agent_cursor    
     FETCH NEXT FROM dnet_agent_cursor INTO @Id, @ChassisMasterRefID, @EndCustomerID, @RegNumber, @SPKHeaderID, @DealerID    
     WHILE @@FETCH_STATUS = 0  
        BEGIN    
        IF NOT EXISTS ( SELECT *  
            FROM @returnSPKChassis )  
        BEGIN  
           INSERT INTO @returnSPKChassis  
            (  
              ID ,  
              ChassisMasterID ,  
			  EndCustomerID,
              RegNumber ,  
              SPKHeaderID ,  
              DealerID  
            )  
           SELECT @Id AS ID ,  
            @ChassisMasterRefID AS ChassisMasterID ,  
			@EndCustomerID AS EndCustomerID,
            @RegNumber AS RegNumber ,  
            @SPKHeaderID AS SPKHeaderID ,  
            @DealerID AS DealerID   
        END  
  
        IF @SPKHeaderID IS NOT NULL  
        BEGIN    
           UPDATE @returnSPKChassis  
           SET  RegNumber = @RegNumber ,  
            SPKHeaderID = @SPKHeaderID ,  
            DealerID = @DealerID  
           WHERE  ChassisMasterID = @ChassisMasterID  
           BREAK    
        END    
        FETCH NEXT FROM dnet_agent_cursor INTO @Id, @ChassisMasterRefID, @EndCustomerID, @RegNumber, @SPKHeaderID, @DealerID    
        END    
     CLOSE dnet_agent_cursor    
     DEALLOCATE dnet_agent_cursor    
     END     
  RETURN    
 END;    

 GO



GO


set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

--====================================EXECUTE FUNCTION =====================================================================================================

alter FUNCTION ufn_CreatePartShopCode ( @CityPartID AS INT )
RETURNS VARCHAR(10)
AS
    BEGIN 

        DECLARE @CityCode AS VARCHAR(4)
        DECLARE @ICode AS INT = 0
        DECLARE @MaxPartShopCode AS VARCHAR(2)
        DECLARE @PartShopCode AS VARCHAR(6)
        DECLARE @return VARCHAR(10)

        SELECT  @CityCode = CityCode
        FROM    CityPart
        WHERE   ID = @CityPartID 

        SELECT  @ICode = MAX(ISNULL(CONVERT(INTEGER, RIGHT(PartShopCode, 4)), 0))
        FROM    PartShop
        WHERE   LEFT(PartShopCode, 4) = @CityCode 
	
        SET @ICode = @ICode + 1 
        SELECT  @return = (@CityCode + RIGHT(( POWER(10, 4) + @ICode ), 4))
	
        RETURN @return

	  --SELECT  @CityCode = CityCode
	  --FROM	  CityPart
	  --WHERE	  ID = @CityPartID
	  --SET @ICode = ( SELECT	ISNULL(MAX(RIGHT(PartShopCode, 2)), 0)
			--		 FROM	PartShop
			--		 WHERE	CityPartID = @CityPartID
			--		 AND RowStatus=0
			--	   )
	  --SET @MaxPartShopCode = CAST(( CAST(@ICode AS INTEGER) + 1 ) AS VARCHAR(2))			
	  --SET @MaxPartShopCode = REPLICATE('0', 2 - LEN(@MaxPartShopCode))
		 -- + @MaxPartShopCode
	  --SET @PartShopCode = @CityCode + CONVERT(VARCHAR(2), @MaxPartShopCode)

	  --RETURN @PartShopCode

    END
go

commit
go


