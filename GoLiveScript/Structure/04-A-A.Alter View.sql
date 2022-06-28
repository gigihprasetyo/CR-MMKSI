set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

-- =============================================
-- Author:		SLA
-- Create date: 22-01-2018
-- Description:	
-- =============================================
ALTER VIEW [dbo].[V_AssistPartStockFlagLastData]    
AS    
SELECT  A.ID ,    
        AssistUploadLogID ,    
        A.[Month] ,    
        A.[Year] ,    
        A.DealerID ,    
        A.DealerCode ,    
	  A.DealerBranchID,  
	  A.DealerBranchCode,  
        A.SparepartMasterID ,    
        A.NoParts ,    
        A.JumlahStokAwal ,    
        A.JumlahDatang ,    
        A.HargaBeli ,    
        A.RemarksSystem ,    
        A.StatusAktif ,    
        A.ValidateSystemStatus ,    
        A.RowStatus ,    
        A.CreatedBy ,    
        A.CreatedTime ,    
        A.LastUpdateBy ,    
        A.LastUpdateTime ,    
        ( CASE WHEN LastData.DealerID IS NOT NULL THEN 1    
               ELSE 0    
          END ) IsLastData    
FROM    dbo.AssistPartStock A (NOLOCK)     
LEFT JOIN AssistUploadLog B (NOLOCK)  ON A.AssistUploadLogID = B.ID    
LEFT JOIN ( SELECT  C.DealerID ,    
                    C.[Month] ,    
                    C.[Year] ,    
                    NoParts ,    
                    MAX(ISNULL(D.UploadTime,C.lastupdatetime)) AS MaxDate    
            FROM    AssistPartStock C (NOLOCK)     
            LEFT JOIN AssistUploadLog D (NOLOCK)  ON C.AssistUploadLogID = D.ID    
            WHERE   C.StatusAktif = 1 --Not duplicate data    
                    AND ((D.ValidateStatus = 5 AND D.ID IS NOT NULL) OR (D.ID IS NULL)) 
            GROUP BY C.DealerID ,    
                    C.[Month] ,    
                    C.[Year] ,    
                    NoParts    
          ) LastData ON LastData.DealerID = A.DealerID    
                        AND A.[Month] = LastData.[Month]    
                        AND A.[Year] = LastData.[Year]    
                        AND A.NoParts = LastData.NoParts    
                        AND ISNULL(B.UploadTime, A.lastupdatetime) = LastData.MaxDate    
WHERE A.StatusAktif = 1
go

-- =============================================    
-- Author:  SLA    
-- Create date: 22-01-2018    
-- Description:     
-- UpdateBy : Mitrais  
-- Edit Date : 26.10.2018  
-- UpdateBy : Mitrais  
-- Edit Date : 26.10.2018  
-- Add Data API Data to report
-- =============================================    
alter VIEW V_AssistPartSalesConfirmation    
AS    
SELECT  A.ID ,    
        AssistUploadLogID ,    
        TglTransaksi ,    
        A.DealerID ,    
        A.DealerCode ,  
  A.DealerBranchID ,    
        A.DealerBranchCode ,    
        KodeCustomer ,    
        SalesChannelID ,    
        SalesChannelCode ,    
        TrTraineeSalesSparepartID ,    
        SalesmanHeaderID ,    
        KodeSalesman ,    
        NoWorkOrder ,    
        SparepartMasterID ,    
        NoParts ,    
        Qty ,    
        HargaBeli ,    
        HargaJual ,    
        B.[Month] AS MonthPeriod ,    
        B.[Year] AS YearPeriod ,    
        A.RowStatus ,    
        A.CreatedBy ,    
        A.CreatedTime ,    
        A.LastUpdateBy ,    
        A.LastUpdateTime    
FROM    dbo.AssistPartSales A ( NOLOCK )    
INNER JOIN AssistUploadLog B ( NOLOCK ) ON A.AssistUploadLogID = B.ID    
                                           AND A.StatusAktif = 1 --Not duplicate data    
                                           AND B.ValidateStatus = 5 --Konfirmasi MMKSI    
union

SELECT  A.ID ,    
        AssistUploadLogID ,    
        TglTransaksi ,    
        A.DealerID ,    
        A.DealerCode ,  
  A.DealerBranchID ,    
        A.DealerBranchCode ,    
        KodeCustomer ,    
        SalesChannelID ,    
        SalesChannelCode ,    
        TrTraineeSalesSparepartID ,    
        SalesmanHeaderID ,    
        KodeSalesman ,    
        NoWorkOrder ,    
        SparepartMasterID ,    
        NoParts ,    
        Qty ,    
        HargaBeli ,    
        HargaJual ,    
        Month(TglTransaksi) AS MonthPeriod ,    
        Year(TglTransaksi) AS YearPeriod ,    
        A.RowStatus ,    
        A.CreatedBy ,    
        A.CreatedTime ,    
        A.LastUpdateBy ,    
        A.LastUpdateTime    
FROM    dbo.AssistPartSales A ( NOLOCK )    
where A.AssistUploadLogID is null AND A.StatusAktif = 1 --Not duplicate data    
                                           AND A.ValidateSystemStatus = 1 --Konfirmasi MMKSI
go

-- =============================================  
-- Author:  SLA  
-- Create date: 22-01-2018  
-- Description:   
-- UpdateBy : Mitrais
-- Edit Date : 26.10.2018
-- =============================================  
alter VIEW V_AssistPartStockConfirmation  
AS  
SELECT  A.ID ,  
        AssistUploadLogID ,  
        A.[Month] ,  
        A.[Year] ,  
        A.DealerID ,  
        A.DealerCode ,  
		A.DealerBranchID,
		A.DealerBranchCode,
        A.SparepartMasterID ,  
        A.NoParts ,  
        A.JumlahStokAwal ,  
        A.JumlahDatang ,  
        A.HargaBeli ,  
        A.RemarksSystem ,  
        A.StatusAktif ,  
        A.ValidateSystemStatus ,  
        A.RowStatus ,  
        A.CreatedBy ,  
        A.CreatedTime ,  
        A.LastUpdateBy ,  
        A.LastUpdateTime  
FROM    dbo.[V_AssistPartStockFlagLastData] A ( NOLOCK )  
INNER JOIN AssistUploadLog B ( NOLOCK ) ON A.AssistUploadLogID = B.ID  
WHERE   A.StatusAktif = 1 --Not duplicate data  
        AND B.ValidateStatus = 5 --Konfirmasi MMKSI  
        AND A.IsLastData = 1
go



alter VIEW V_AssistServiceIncomingConfirmation        
  
AS        

SELECT  A.ID ,  
        AssistUploadLogID ,  
        TglBukaTransaksi ,  
        WaktuMasuk ,  
        TglTutupTransaksi ,  
        WaktuKeluar ,  
        A.DealerID ,  
        DealerCode ,  
		A.DealerBranchID,
		DealerBranchCode,
        TrTraineMekanikID ,  
        KodeMekanik ,  
        NoWorkOrder ,  
        ChassisMasterID ,  
        KodeChassis ,  
        WorkOrderCategoryID ,  
        WorkOrderCategoryCode ,  
        KMService ,  
        ServicePlaceID ,  
        ServicePlaceCode ,  
        ServiceTypeID ,  
        ServiceTypeCode ,  
        TotalLC ,  
        MetodePembayaran ,  
        E.VechileTypeCode ,  
        D.ColorCode ,  
        Model ,  
        Transmition ,  
        DriveSystem ,  
        --B.[Month] AS MonthPeriod ,  
		ISNULL(B.[Month], MONTH(TglTutupTransaksi)) as MonthPeriod,
        --B.[Year] AS YearPeriod ,  
		ISNULL(B.[Year], YEAR(TglTutupTransaksi)) as YearPeriod,
        A.RowStatus ,  
        A.CreatedBy ,  
        A.CreatedTime ,  
        A.LastUpdateBy ,  
        A.LastUpdateTime  
FROM    dbo.AssistServiceIncoming A ( NOLOCK )  
INNER JOIN AssistUploadLog B ( NOLOCK ) ON A.AssistUploadLogID = B.ID  
                                           AND A.StatusAktif = 1 --Not duplicate data  
                                           AND B.ValidateStatus = 5 --Konfirmasi MMKSI  
LEFT JOIN ChassisMaster C ( NOLOCK ) ON A.ChassisMasterID = C.ID  AND C.RowStatus = 0
LEFT JOIN VechileColor D ( NOLOCK ) ON C.VechileColorID = D.ID  AND D.RowStatus = 0
LEFT JOIN VechileType E ( NOLOCK ) ON D.VechileTypeID = E.ID  AND E.RowStatus = 0

UNION

SELECT  A.ID ,  
        AssistUploadLogID ,  
        TglBukaTransaksi ,  
        WaktuMasuk ,  
        TglTutupTransaksi ,  
        WaktuKeluar ,  
        A.DealerID ,  
        DealerCode ,  
		A.DealerBranchID,
		DealerBranchCode,
        TrTraineMekanikID ,  
        KodeMekanik ,  
        NoWorkOrder ,  
        ChassisMasterID ,  
        KodeChassis ,  
        WorkOrderCategoryID ,  
        WorkOrderCategoryCode ,  
        KMService ,  
        ServicePlaceID ,  
        ServicePlaceCode ,  
        ServiceTypeID ,  
        ServiceTypeCode ,  
        TotalLC ,  
        MetodePembayaran ,  
        E.VechileTypeCode ,  
        D.ColorCode ,  
        Model ,  
        Transmition ,  
        DriveSystem ,  
        --B.[Month] AS MonthPeriod ,  
	    MONTH(TglTutupTransaksi) as MonthPeriod,
        --B.[Year] AS YearPeriod ,  
		YEAR(TglTutupTransaksi) as YearPeriod,
        A.RowStatus ,  
        A.CreatedBy ,  
        A.CreatedTime ,  
        A.LastUpdateBy ,  
        A.LastUpdateTime  
FROM    dbo.AssistServiceIncoming A ( NOLOCK )  
LEFT JOIN ChassisMaster C ( NOLOCK ) ON A.ChassisMasterID = C.ID  AND C.RowStatus = 0
LEFT JOIN VechileColor D ( NOLOCK ) ON C.VechileColorID = D.ID  AND D.RowStatus = 0
LEFT JOIN VechileType E ( NOLOCK ) ON D.VechileTypeID = E.ID  AND E.RowStatus = 0
where a.AssistUploadLogID is null and a.StatusAktif = 1 and a.WOStatus = 2
 
go

commit
go





set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter VIEW V_FreeService  
AS  
SELECT  FreeService.ID ,
        FreeService.Status ,
        FreeService.ChassisMasterID ,
        FreeService.FSKindID ,
        FreeService.MileAge ,
        FreeService.ServiceDate ,
        FreeService.ServiceDealerID ,
        FreeService.SoldDate ,
        FreeService.NotificationNumber ,
        FreeService.NotificationType ,
        FreeService.TotalAmount ,
        FreeService.LabourAmount ,
        FreeService.PartAmount ,
        FreeService.PPNAmount ,
        FreeService.PPHAmount ,
        FreeService.Reject ,
        FreeService.Reason ,
        FreeService.ReleaseBy ,
        FreeService.ReleaseDate ,
        FreeService.VisitType ,
        FreeService.FleetRequestID ,
        FreeService.RowStatus ,
        FreeService.CreatedBy ,
        FreeService.CreatedTime ,
        FreeService.LastUpdateBy ,
        FreeService.LastUpdateTime ,  
        Dealer.DealerCode ,  
        DealerBranch.DealerBranchCode,  
        Dealer.SearchTerm1 ,  
        ChassisMaster.ChassisNumber ,  
        FSKind.KindCode ,  
        Category.ID AS CategoryID ,  
        Category.CategoryCode ,  
        Reason.ReasonCode ,  
        Reason.[Description] AS ReasonDescription ,  
        ISNULL(X.NoRegRequest, '') NoRegRequest  
FROM    FreeService WITH ( NOLOCK )  
INNER JOIN Dealer ON Dealer.ID = FreeService.ServiceDealerID  
LEFT JOIN DealerBranch ON DealerBranch.ID = FreeService.DealerBranchID  
INNER JOIN ChassisMaster ON FreeService.ChassisMasterID = ChassisMaster.ID  
INNER JOIN FSKind ON FSKind.ID = FreeService.FSKindID  
INNER JOIN Category ON ChassisMaster.CategoryID = Category.ID  
LEFT OUTER JOIN Reason ON FreeService.Reason = Reason.ID  
OUTER APPLY ( SELECT TOP 1  
                        FleetRequest.NoRegRequest  
              FROM      FleetFaktur WITH ( NOLOCK )  
              LEFT OUTER JOIN dbo.FleetRequest WITH ( NOLOCK ) ON FleetRequest.ID = FleetFaktur.FleetRequestID  
              WHERE     FleetFaktur.ChassisMasterID = ChassisMaster.ID  
                        AND FleetFaktur.RowStatus = 0  
                        AND dbo.FleetRequest.RowStatus = 0  
              ORDER BY  dbo.FleetFaktur.CreatedTime DESC  
            ) X  
WHERE   FreeService.RowStatus = 0   
--AND FreeService.Reject = 'APP'  
--AND FreeService.NotificationType = 'Z1'  
--AND Category.ID = 2   
--AND FreeService.ReleaseDate >= '2016/10/11'  
--AND FreeService.ReleaseDate <= '2016/11/10'  
--AND FreeService.Status = '3')   
--ORDER BY ChassisMaster.ChassisNumber ASC
go

commit
go



set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter VIEW V_SalesmanPart as
select SH.*,D.DealerCode, DB.DealerBranchCode, ProvinceName
, ISNULL(AddInfo.DivisiID, null) as DivisiID, isnull(AddInfo.DivisiName,'') as DivisiName
, ISNULL(AddInfo.PosisiID, null) as PosisiID, isnull(AddInfo.PosisiName,'') as PosisiName
, ISNULL(AddInfo.LevelID, null) as LevelID, isnull(AddInfo.LevelName,'') as LevelName
, ISNULL(AddInfo.Salary, 0)  as Salary
, isnull(SH2.SalesmanCode,'') as LeaderCode, isnull(SH2.Name,'') as LeaderName, isnull(SA.AreaDesc ,'') as AreaDesc,
(Select top 1 ProfileValue from SalesmanProfile where SalesmanHeaderID=SH.ID and ProfileHeaderID=31 and RowStatus=0) as PENDIDIKAN,
(Select top 1 ProfileValue from SalesmanProfile where SalesmanHeaderID=SH.ID and ProfileHeaderID=26 and RowStatus=0) as EMAIL,
(Select top 1 ProfileValue from SalesmanProfile where SalesmanHeaderID=SH.ID and ProfileHeaderID=33 and RowStatus=0) as NO_HP,
(Select top 1 ProfileValue from SalesmanProfile where SalesmanHeaderID=SH.ID and ProfileHeaderID=29 and RowStatus=0) as NOKTP
from SalesmanHeader SH 
left join Dealer D on(SH.DealerID=D.ID)
left join DealerBranch DB on (SH.DealerBranchID=DB.ID) 
left join Province P on (D.ProvinceID=P.ID)
left join SalesmanLevel SL on (SH.SalesmanLevelID=SL.ID) 
left join SalesmanHeader SH2 on(SH.LeaderID=SH2.ID)
left join SalesmanArea SA on(SH.SalesmanAreaID=SA.ID)
left join 
	(select sai.ID, sai.SalesmanHeaderID, sai.ReligionID
		, DivPos.ParentID as DivisiID, DivPos.Divisi as DivisiName
		, sai.SalesmanCategoryLevelID as PosisiID, DivPos.Posisi as PosisiName
		, sai.SalesmanLevel as LevelID
		, Case SalesmanLevel 
			When 0 then 'Junior'
			When 1 then 'Senior'
			When 2 then 'Top'
		  End as LevelName
		, sai.Salary, sai.RowStatus
	from SalesmanAdditionalInfo as sai
	--left join SalesmanLevel as sl on sai.SalesmanLevel = sl.ID
	left join
		(
		select pos.ID, pos.ParentID, div.PositionName as Divisi, pos.PositionName as Posisi 
		from SalesmanCategoryLevel as div, SalesmanCategoryLevel as pos
		where div.ID = pos.ParentID
		) as DivPos On DivPos.ID = sai.SalesmanCategoryLevelID
	) as AddInfo On AddInfo.SalesmanHeaderID = SH.ID 
WHERE SH.SalesIndicator = 0 and SH.RowStatus = 0
go

commit
go







set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter VIEW V_SparePartFlow
AS
SELECT
  ROW_NUMBER() OVER (ORDER BY po.ID) AS Row,
  po.ID POID,
  po.PONumber,
  po.PODate,
  po.SentPODate POSendDate,
  po.TermOfPaymentID,
  teop.Description TOPDescription,
  so.ID SOID,
  so.SONumber,
  so.SODate,
  do.ID DOID,
  do.DONumber,
  do.DoDate,
  bill.ID BillingID,
  bill.BillingNumber,
  bill.BillingDate,
  po.DealerID,
  dl.DealerCode,
  po.OrderType,
  so.DocumentType,
  tbs.TOPCeilingStatus,
  [dbo].[fn_GetDOStatus](po.PONumber, so.SONumber, do.DONumber) [STATUS]
FROM SparePartPO po (NOLOCK)
LEFT JOIN SparePartPOEstimate so (NOLOCK)
  ON so.SparePartPOID = po.ID
  AND so.RowStatus = 0
LEFT JOIN SparePartDODetail dodet (NOLOCK)
  ON dodet.SparePartPOEstimateID = so.ID
  AND dodet.RowStatus = 0
LEFT JOIN SparePartDO do (NOLOCK)
  ON do.ID = dodet.SparePartDOID
  AND do.RowStatus = 0
LEFT JOIN SparePartBillingDetail billdet (NOLOCK)
  ON billdet.SparePartDODetailID = dodet.ID
  AND billdet.RowStatus = 0
LEFT JOIN SparePartBilling bill (NOLOCK)
  ON bill.ID = billdet.SparePartBillingID
  AND bill.RowStatus = 0
LEFT JOIN Dealer dl (NOLOCK)
  ON dl.ID = po.DealerID
  AND dl.RowStatus = 0
LEFT JOIN TermOfPayment teop (NOLOCK)
  ON teop.ID = po.TermOfPaymentID
  AND teop.RowStatus = 0
LEFT JOIN (SELECT
  tbs.ID TOPBlockStatusID,
  ValueDesc TOPCeilingStatus
FROM TOPBlockStatus tbs
LEFT JOIN StandardCode sc
  ON sc.valueid = tbs.status
  AND sc.Category = 'TOPCeilingStatus') tbs
  ON tbs.TOPBlockStatusID = po.TOPBlockStatusID
WHERE 1 = 1
AND po.RowStatus = 0
--and so.SONumber is not null
--and do.DONumber is not null
--and bill.BillingNumber is not null
GROUP BY po.ID,
         po.PONumber,
         po.PODate,
         po.SentPODate,
         po.TermOfPaymentID,
         so.ID,
         so.SONumber,
         so.SODate,
         do.ID,
         do.DONumber,
         do.DoDate,
         bill.ID,
         bill.BillingNumber,
         bill.BillingDate,
         po.DealerID,
         dl.DealerCode,
         po.OrderType,
         so.DocumentType,
         tbs.TOPCeilingStatus,
         teop.Description
  
   
----select * from V_Sparepartflow  
--CREATE VIEW [dbo].[V_SparePartFlow]  
--AS  
--SELECT  ROW_NUMBER() OVER ( ORDER BY po.ID ) AS Row ,  
--        po.ID POID ,  
--        po.PONumber ,  
--        po.PODate ,  
--        po.SentPODate POSendDate ,  
--        so.ID SOID ,  
--        so.SONumber ,  
--        so.SODate ,  
--        do.ID DOID ,  
--        do.DONumber ,  
--        do.DoDate ,  
--        bill.ID BillingID ,  
--        bill.BillingNumber ,  
--        bill.BillingDate ,  
--        po.DealerID ,  
--        dl.DealerCode ,  
--        po.OrderType ,  
--        so.DocumentType ,  
--        [dbo].[fn_GetDOStatus](po.PONumber, so.SONumber, do.DONumber) [STATUS]  
--FROM    SparePartPO po ( NOLOCK )  
--LEFT JOIN SparePartPOEstimate so ( NOLOCK ) ON so.SparePartPOID = po.ID  
--                                               AND so.RowStatus = 0  
--LEFT JOIN SparePartDODetail dodet ( NOLOCK ) ON dodet.SparePartPOEstimateID = so.ID  
--                                                AND dodet.RowStatus = 0  
--LEFT JOIN SparePartDO do ( NOLOCK ) ON do.ID = dodet.SparePartDOID  
--                                       AND do.RowStatus = 0  
--LEFT JOIN SparePartBillingDetail billdet ( NOLOCK ) ON billdet.SparePartDODetailID = dodet.ID  
--                                                       AND billdet.RowStatus = 0  
--LEFT JOIN SparePartBilling bill ( NOLOCK ) ON bill.ID = billdet.SparePartBillingID  
--                                              AND bill.RowStatus = 0  
--LEFT JOIN Dealer dl ( NOLOCK ) ON dl.ID = po.DealerID  
--                                  AND dl.RowStatus = 0  
--WHERE   1 = 1  
--        AND po.RowStatus = 0  
----and so.SONumber is not null  
----and do.DONumber is not null  
----and bill.BillingNumber is not null  
--GROUP BY po.ID ,  
--        po.PONumber ,  
--        po.PODate ,  
--        po.SentPODate ,  
--        so.ID ,  
--        so.SONumber ,  
--        so.SODate ,  
--        do.ID ,  
--        do.DONumber ,  
--        do.DoDate ,  
--        bill.ID ,  
--        bill.BillingNumber ,  
--        bill.BillingDate ,  
--        po.DealerID ,  
--        dl.DealerCode ,  
--        po.OrderType ,  
--        so.DocumentType  
----order by po.ID desc
go

commit
GO



set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter VIEW V_SpPO_Indent
AS

SELECT     dbo.SparePartPODetail.SparePartPOID AS ID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType, 
                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName, 
                      dbo.IndentPartHeader.ID AS IndentPartHeaderID
					  , dbo.TermOfPayment.Description TOPDescription, dbo.TermOfPayment.ID TermOfPaymentID
FROM         dbo.SparePartPODetail INNER JOIN
                      dbo.IndentPartPO ON dbo.SparePartPODetail.ID = dbo.IndentPartPO.SparePartPODetailID INNER JOIN
                      dbo.IndentPartDetail ON dbo.IndentPartPO.IndentPartDetailID = dbo.IndentPartDetail.ID INNER JOIN
                      dbo.IndentPartHeader ON dbo.IndentPartDetail.IndentPartHeaderID = dbo.IndentPartHeader.ID INNER JOIN
                      dbo.SparePartPO ON dbo.SparePartPODetail.SparePartPOID = dbo.SparePartPO.ID INNER JOIN
                      dbo.Dealer ON dbo.SparePartPO.DealerID = dbo.Dealer.ID LEFT JOIN
                      dbo.TermOfPayment ON dbo.SparePartPO.TermOfPaymentID = dbo.TermOfPayment.ID 
WHERE dbo.IndentPartHeader.MaterialType<>4 --Exclude Equipment
GROUP BY dbo.SparePartPODetail.SparePartPOID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType, 
                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName, 
                      dbo.IndentPartHeader.ID
					  , dbo.TermOfPayment.Description ,dbo.TermOfPayment.ID
--SELECT     dbo.SparePartPODetail.SparePartPOID AS ID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType,   
--                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName,   
--                      dbo.IndentPartHeader.ID AS IndentPartHeaderID  
--FROM         dbo.SparePartPODetail INNER JOIN  
--                      dbo.IndentPartPO ON dbo.SparePartPODetail.ID = dbo.IndentPartPO.SparePartPODetailID INNER JOIN  
--                      dbo.IndentPartDetail ON dbo.IndentPartPO.IndentPartDetailID = dbo.IndentPartDetail.ID INNER JOIN  
--                      dbo.IndentPartHeader ON dbo.IndentPartDetail.IndentPartHeaderID = dbo.IndentPartHeader.ID INNER JOIN  
--                      dbo.SparePartPO ON dbo.SparePartPODetail.SparePartPOID = dbo.SparePartPO.ID INNER JOIN  
--                      dbo.Dealer ON dbo.SparePartPO.DealerID = dbo.Dealer.ID  
--WHERE dbo.IndentPartHeader.MaterialType<>4 --Exclude Equipment  
--GROUP BY dbo.SparePartPODetail.SparePartPOID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType,   
--                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName,   
--                      dbo.IndentPartHeader.ID
go

commit
go





set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter VIEW V_FreeService  
AS  
SELECT  FreeService.ID ,
        FreeService.Status ,
        FreeService.ChassisMasterID ,
        FreeService.FSKindID ,
        FreeService.MileAge ,
        FreeService.ServiceDate ,
        FreeService.ServiceDealerID ,
        FreeService.SoldDate ,
        FreeService.NotificationNumber ,
        FreeService.NotificationType ,
        FreeService.TotalAmount ,
        FreeService.LabourAmount ,
        FreeService.PartAmount ,
        FreeService.PPNAmount ,
        FreeService.PPHAmount ,
        FreeService.Reject ,
        FreeService.Reason ,
        FreeService.ReleaseBy ,
        FreeService.ReleaseDate ,
        FreeService.VisitType ,
		FreeService.WorkOrderNumber,
        FreeService.FleetRequestID ,
        FreeService.RowStatus ,
        FreeService.CreatedBy ,
        FreeService.CreatedTime ,
        FreeService.LastUpdateBy ,
        FreeService.LastUpdateTime ,  
        Dealer.DealerCode ,  
        DealerBranch.DealerBranchCode,  
        Dealer.SearchTerm1 ,  
        ChassisMaster.ChassisNumber ,  
        FSKind.KindCode ,  
        Category.ID AS CategoryID ,  
        Category.CategoryCode ,  
        Reason.ReasonCode ,  
        Reason.[Description] AS ReasonDescription ,  
        ISNULL(X.NoRegRequest, '') NoRegRequest  
FROM    FreeService WITH ( NOLOCK )  
INNER JOIN Dealer ON Dealer.ID = FreeService.ServiceDealerID  
LEFT JOIN DealerBranch ON DealerBranch.ID = FreeService.DealerBranchID  
INNER JOIN ChassisMaster ON FreeService.ChassisMasterID = ChassisMaster.ID  
INNER JOIN FSKind ON FSKind.ID = FreeService.FSKindID  
INNER JOIN Category ON ChassisMaster.CategoryID = Category.ID  
LEFT OUTER JOIN Reason ON FreeService.Reason = Reason.ID  
OUTER APPLY ( SELECT TOP 1  
                        FleetRequest.NoRegRequest  
              FROM      FleetFaktur WITH ( NOLOCK )  
              LEFT OUTER JOIN dbo.FleetRequest WITH ( NOLOCK ) ON FleetRequest.ID = FleetFaktur.FleetRequestID  
              WHERE     FleetFaktur.ChassisMasterID = ChassisMaster.ID  
                        AND FleetFaktur.RowStatus = 0  
                        AND dbo.FleetRequest.RowStatus = 0  
              ORDER BY  dbo.FleetFaktur.CreatedTime DESC  
            ) X  
WHERE   FreeService.RowStatus = 0   
--AND FreeService.Reject = 'APP'  
--AND FreeService.NotificationType = 'Z1'  
--AND Category.ID = 2   
--AND FreeService.ReleaseDate >= '2016/10/11'  
--AND FreeService.ReleaseDate <= '2016/11/10'  
--AND FreeService.Status = '3')   
--ORDER BY ChassisMaster.ChassisNumber ASC
go

commit
go


