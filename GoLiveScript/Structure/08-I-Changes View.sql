GO

--========================================================================================================================                      
-- Created By: Mitrais (Prins Carl S)                      
-- Vehicle Information                      
--========================================================================================================================                      
alter view [dbo].[VWI_VehicleInformation]      
as      
select ROW_NUMBER() Over(Order By (select 1)) as ID, a.ChassisNumber, a.isBB as IsBB,    
    isnull(f.CategoryCode,'') as CategoryCode, CategoryDesc=isnull(f.Description, ''),     
    isnull(b.ColorCode, '') as ColorCode, isnull(b.ColorIndName, '') as ColorIndName, isnull(b.ColorEngName, '') as ColorEngName,    
    isnull(b.MaterialDescription, '') as MaterialDescription,      
    VehicleTypeCode=isnull(c.VechileTypeCode, ''), VehicleTypeDesc=isnull(c.Description, ''),     
    ModelSearchTerm1 = isnull(j.VechileModelIndCode, ''),     
    ModelSearchTerm2 = isnull(j.IndDescription, ''),    
    isnull(c.SegmentType, '') as SegmentType, isnull(c.FuelType, '') as FuelType,    
    isnull(c.TransmitType, '') as TransmitType, isnull(c.DriveSystemType,'') as DriveSystemType, 
	isnull(c.VariantType, '') as VariantType, VehicleBrand = 'Mitsubishi', isnull(c.SpeedType, '') as SpeedType,    
       a.VehicleKindID, isnull(d.Code,'') as Code, VehicleKindDesc=isnull(c.Description, ''),      
       a.SoldDealerID, e.DealerCode, e.DealerName,      
       isnull(a.EngineNumber,'') as EngineNumber, isnull(a.SerialNumber,'') as SerialNumber, isnull(a.ProductionYear,'') as ProductionYear, FleetCode=isnull(i.Code,''),     
    isnull(g.OpenFakturDate, dbo.DateTimeMinValue()) as OpenFakturDate, 
	isnull(g.FakturDate, dbo.DateTimeMinValue()) as FakturDate, 
	FSExtended='', isnull(k.PKTDate, dbo.DateTimeMinValue()) as PKTDate,    
    a.LastUpdateTime     
from     
(    
    select ID, EndCustomerId, ChassisNumber, isBB, CategoryID, VechileColorID, VehicleKindID, SoldDealerID, EngineNumber, SerialNumber, ProductionYear, LastUpdateTime    
 from    
 (    
     select ID, EndCustomerId, ChassisNumber, isBB=0, CategoryID, VechileColorID, VehicleKindID, SoldDealerID, EngineNumber, SerialNumber, ProductionYear, LastUpdateTime from ChassisMaster WITH (NOLOCK)  where RowStatus = 0  and FakturStatus = 4  
        union    
        select ID, EndCustomerId, ChassisNumber, isBB=1, CategoryID, VechileColorID, VehicleKindID, SoldDealerID, EngineNumber, SerialNumber, ProductionYear, LastUpdateTime from ChassisMasterBB  WITH (NOLOCK)  where RowStatus = 0    
 ) a where --a.CategoryID in (3)    
           a.CategoryID in (1,2)           
) a      
left join VechileColor b WITH (NOLOCK) on a.VechileColorID = b.ID and b.RowStatus = 0      
left join VechileType c WITH (NOLOCK) on c.ID = b.VechileTypeID and c.RowStatus = 0      
left join VehicleKind d WITH (NOLOCK) on a.VehicleKindID = d.ID and d.RowStatus = 0      
left join Dealer e WITH (NOLOCK) on a.SoldDealerID = e.ID and e.RowStatus = 0      
left join Category f WITH (NOLOCK) on a.CategoryID = f.ID and f.RowStatus = 0    
left join EndCustomer g WITH (NOLOCK) on a.EndCustomerID = g.ID and g.RowStatus = 0    
left join FleetCustomerToCustomer h WITH (NOLOCK) on g.CustomerID = h.CustomerID and h.RowStatus = 0    
left join FleetCustomer i WITH (NOLOCK) on h.FleetCustomerID = i.ID and i.RowStatus = 0     
left join VechileModel j WITH (NOLOCK) on c.ModelID = j.ID and j.RowStatus = 0    
left join ChassisMasterPKT k WITH (NOLOCK) on a.ID = k.ChassisMasterID and k.RowStatus = 0    
--where c.Status <> 'X'   Comment out by SLA untuk tes tarik data ALL ke SIT QA, nanti pas prod dibalikin lagi 20181115    
   

GO

    
--========================================================================================================================                        
-- Created By: Mitrais (Prins Carl S)                        
-- Service History                    
--========================================================================================================================        
alter view [dbo].[VWI_ServiceHistory] -- with schemabinding    
as      
        
select ID, ChassisMasterID, KodeChassis, PKTDate, TglBukaTransaksi, TglTutupTransaksi, WaktuMasuk, WaktuKeluar, DealerCode, DealerBranchCode, WorkOrderType,
       WorkOrderCategoryCode, KMService, NoWorkOrder, ServicePlaceCode, ServiceTypeCode, LastUpdateTime
from (
select a.ID,      
    isnull(a.ChassisMasterID,f.ID) as ChassisMasterID, a.KodeChassis,       
    d.PKTDate,      
    a.TglBukaTransaksi, a.TglTutupTransaksi, a.WaktuMasuk, a.WaktuKeluar,          
    a.DealerCode,        
    a.DealerBranchCode,          
    e.WorkOrderType,      
    a.WorkOrderCategoryCode, a.KMService, a.NoWorkOrder, a.ServicePlaceCode, a.ServiceTypeCode, a.LastUpdateTime      
from V_assistServiceIncomingConfirmation a with (nolock)          
left join ChassisMasterPKT d with (nolock) on a.ChassisMasterID = d.ChassisMasterID and d.RowStatus = 0        
left join AssistWorkOrderCategory g with (nolock) on a.WorkOrderCategoryID = g.ID and g.RowStatus = 0    
left join AssistWorkOrderType e with (nolock) on g.WorkOrderTypeID = e.ID and e.RowStatus = 0         
left join ChassisMasterBB f with (nolock) on a.KodeChassis = f.ChassisNumber and f.RowStatus = 0    
) a
where a.ChassisMasterID is not null 


GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View For Province              
--========================================================================================================================     
ALTER view [dbo].[VWI_Province] AS  

select ID, ProvinceCode, ProvinceName, RowStatus as Status, LastUpdateTime 
from Province WITH (NOLOCK)
where ProvinceCode <> 'UK'   


GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View For City              
--========================================================================================================================     
ALTER VIEW VWI_City  
AS  
	 SELECT a.ID, 
	   b.ProvinceCode ,  
       b.ProvinceName ,  
       a.CityCode ,  
       a.CityName ,  
       a.LastUpdateTime ,  
       Status = CASE WHEN a.RowStatus <> 0 THEN a.RowStatus  
            ELSE CASE WHEN a.Status = 'X' THEN -1  
             ELSE a.RowStatus  
              END  
          END  
      FROM  City AS a WITH ( NOLOCK )  
      INNER JOIN Province AS b WITH ( NOLOCK ) ON a.ProvinceID = b.ID  
      WHERE  CityCode <> 'UNKNOWN' 

GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View For Lead Sales Force Data           
--========================================================================================================================  
ALTER VIEW [dbo].[VWI_LeadCustomerSalesForce]   
AS  
  
select ROW_NUMBER() OVER (ORDER BY A.DNETID) AS ID,                
       a.DNetID, a.SumberData, a.CreateDate, a.CreateDate_YYYYMMDD, a.DealerCode, a.DealerName, a.CustomerTypeID, a.CustomerType,  
       a.SalesmanCode, a.Name, a.CustomerCode, a.CustomerName, a.CustomerAddress, a.Phone, a.Email, a.SexID, a.Sex, a.AgeSegmentID, a.AgeSegment,  
    a.CustomerStatusID, a.CustomerStatus, InformationTypeID, InformationType, a.InformationSourceID, a.InformationSource, a.CustomerPurposeID, a.CustomerPurpose,   
    a.ProspectDate, a.ProspectDate_YYYYMMDD, a.VechileTypeCode, a.Description, a.CurrVehicleType, a.CurrVehicleBrand,  
    a.Note, a.Keterangan, a.StatusResponseID, a.StatusResponse, a.WebID, a.LastUpdateTime  
from  
(  
SELECT   
c.id AS DNetID,  
c.InformationSource AS 'SumberData'  
--CASE   
--    WHEN c.InformationSource <= 10 THEN 'Dealer'  
--       WHEN c.InformationSource > 10 THEN 'Rekomendasi MMKSI'  
--    ELSE ''  
--END AS 'SumberData'  
, CONVERT(VARCHAR(10), c.CreatedTime, 111) [CreateDate]  
, CONVERT(VARCHAR(10), c.CreatedTime, 112) [CreateDate_YYYYMMDD]  
, d.DealerCode, d.DealerName  
, coalesce(p.ValueDesc, '-') AS CustomerType  
, c.CustomerType AS CustomerTypeID  
, s.SalesmanCode, s.Name  
, c.CustomerCode, c.CustomerName  
, c.CustomerAddress, c.Phone, c.Email  
, coalesce(q.ValueDesc, '-') AS Sex  
, c.Sex AS SexID  
, coalesce(t.ValueDesc, '-') AS AgeSegment  
, c.AgeSegment AS AgeSegmentID  
, coalesce(u.ValueDesc, '-') AS CustomerStatus  
, c.Status AS CustomerStatusID  
, coalesce(w.ValueDesc, '-') AS InformationType  
, c.InformationType AS InformationTypeID  
, coalesce(x.ValueDesc, '-') AS InformationSource  
, c.InformationSource AS InformationSourceID  
, coalesce(y.ValueDesc, '-') AS CustomerPurpose  
, c.CustomerPurpose AS CustomerPurposeID  
, CONVERT(VARCHAR(10), c.ProspectDate, 111) [ProspectDate]  
, CONVERT(VARCHAR(10), c.ProspectDate, 112) [ProspectDate_YYYYMMDD]  
, v.VechileTypeCode  
, v.Description  
, c.CurrVehicleBrand  
, c.CurrVehicleType  
, c.Note  
, isnull(r.Description, '') Keterangan  
, coalesce(z.ValueDesc, '-') AS StatusResponse  
, r.Status AS StatusResponseID  
, c.WebID AS WebID, c.LastUpdateTime  
FROM SAPCustomer c with (nolock)  
LEFT JOIN Dealer d with (nolock) on c.DealerID = d.ID  
LEFT JOIN SalesmanHeader s with (nolock) on s.ID = c.SalesmanHeaderID  
LEFT JOIN VechileType v with (nolock) on v.ID = c.VechileTypeID  
LEFT JOIN V_LeadCustomerResponse r with (nolock) on r.SAPCustomerID = c.id and r.DealerID = c.DealerID  
LEFT JOIN StandardCode z with (nolock) on z.Category = 'EnumSAPCustomerResponse.SAPCustomerResponse' and z.RowStatus = 0 and r.Status = z.ValueId  
LEFT JOIN StandardCode y with (nolock) on y.Category = 'EnumCustomerPurpose.CustomerPurpose' and y.RowStatus = 0 and c.CustomerPurpose = y.ValueId  
LEFT JOIN StandardCode x with (nolock) on x.Category = 'EnumInformationSource.InformationSource' and x.RowStatus = 0 and c.InformationSource = x.ValueId  
LEFT JOIN StandardCode w with (nolock) on w.Category = 'EnumInformationSource.InformationType' and w.RowStatus = 0 and c.InformationType = w.ValueId  
LEFT JOIN StandardCode u with (nolock) on u.Category = 'EnumSAPCustomerStatus.SAPCustomerStatus' and u.RowStatus = 0 and c.Status = u.ValueId  
LEFT JOIN StandardCode t with (nolock) on t.Category = 'EnumAgeSegment.AgeSegment' and t.RowStatus = 0 and c.AgeSegment = t.ValueId  
LEFT JOIN StandardCode q with (nolock) on q.Category = 'EnumGender.Gender' and q.RowStatus = 0 and c.Sex = q.ValueId  
LEFT JOIN StandardCode p with (nolock) on p.Category = 'EnumTipePelanggan' and p.RowStatus = 0 and c.CustomerType = p.ValueId  
where c.SalesforceID <> '' and c.SalesmanHeaderID is null   
) a  
  


  GO


  set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

--========================================================================================================================                          
-- Created By: Mitrais (Prins Carl S)                          
-- ProductSpecification                        
--========================================================================================================================                          
alter VIEW VWI_VehicleSpecification
AS
	  SELECT	CASE WHEN ISNULL(a.VariantType, '') = '' THEN 'N/A'
					 ELSE a.VariantType
				END AS VehicleCategory_S1 ,
				a.VechileTypeCode AS ClassificationNumber ,
				a.Description AS VehicleDesc ,
				b.Code AS ProductCategory ,
				c.Description AS VehicleCatDesc ,
				'MITSUBISHI' AS VehicleBrand ,
				a.SpeedType AS SpeedType ,
				a.FuelType AS FuelType ,
				a.TransmitType AS Transmition ,
				a.DriveSystemType AS Drivesystem ,
				a.SegmentType AS SegmentType ,
				a.LastUpdateTime ,
				CASE WHEN a.RowStatus <> 0 THEN -1
					 ELSE CASE WHEN a.Status = 'X' THEN -1
							   ELSE 0
						  END
				END AS Status
	  FROM		VechileType AS a WITH ( NOLOCK )
	  INNER JOIN ProductCategory AS b WITH ( NOLOCK ) ON a.ProductCategoryID = b.ID
	  INNER JOIN Category AS c WITH ( NOLOCK ) ON a.CategoryID = c.ID
	  WHERE		b.ID = 1
go

--========================================================================================================================                          
-- Created By: Anna Nurhayanto (BSI)                         
-- Unique Vehicle Color By Type Color                      
--======================================================================================================================== 
create view V_VehicleColorByTypeColor as  
SELECT top 1 with ties  
      [VechileTypeID]  
      ,[ColorCode]  
      ,[ColorIndName]  
      ,[ColorEngName]  
      ,[MaterialNumber]  
      ,[MaterialDescription]  
      ,[HeaderBOM]  
      ,[MarketCode]  
      ,[SpecialFlag]  
      ,[Status]  
      ,[RowStatus]  
      ,[CreatedBy]  
      ,[CreatedTime]  
      ,[LastUpdateBy]  
      ,[LastUpdateTime]  
  FROM [dbo].[VechileColor]  
  --where colorcode='MJRM' and VechileTypeID = 260  
order by row_number() over (partition by VechileTypeID, ColorCode order by CreatedTime ASC) --2975

GO

--========================================================================================================================                          
-- Created By: Mitrais (Prins Carl S)                          
-- Quick Product                           
--======================================================================================================================== 
ALTER view [dbo].[VWI_QuickProduct]      
as      
select --ROW_NUMBER() OVER (ORDER BY a.VehicleType, a.ColorCode, a.Status) AS ID,       
       ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS ID,      
       a.VehicleType, a.VehicleDesc, a.ProductCategory, a.VehicleCatDesc, a.ColorCode, a.ColorDescription, a.VehicleBrand,      
    a.VehicleModel_S1, a.VehicleCategory_S2, a.ProductSegment_S3, a.DriveSystem_S4, a.LastUpdateTime, a.Status      
from      
(      
SELECT a.VechileTypeCode AS VehicleType, a.Description AS VehicleDesc,      
       c.Code AS ProductCategory, d.Description AS VehicleCatDesc,      
       b.ColorCode AS ColorCode, b.ColorIndName AS ColorDescription,      
    'MITSUBISHI' AS VehicleBrand,      
    a.VechileTypeCode AS VehicleModel_S1,      
    d.CategoryCode AS VehicleCategory_S2,       
    --e.VechileModelIndCode AS ProductSegment_S3,      
     e.Description AS ProductSegment_S3,      
  --  a.DriveSystemType AS DriveSystem_S4, -- Update Yusak 22 Nov 2018    
    a.SegmentType AS DriveSystem_S4,    
  a.LastUpdateTime,       
    Status = CASE when a.RowStatus = -1 then a.RowStatus else 
	             CASE when b.RowStatus = -1 then b.RowStatus else 
				     CASE when a.Status = 'X' then -1 else 
					     CASE when b.Status = 'X' then -1 else 
						     CASE when e.RowStatus = -1 then e.RowStatus else 0 
							 END 
					     END 
					 END 
			     END 
			 END
      
FROM VechileType AS a       
INNER JOIN V_VehicleColorByTypeColor AS b ON a.ID = b.VechileTypeID      
INNER JOIN ProductCategory AS c ON a.ProductCategoryID = c.ID      
INNER JOIN Category AS d ON a.CategoryID = d.ID      
INNER JOIN VechileModel As e ON a.ModelID = e.ID      
where c.ID = 1    
 
    
) a      
    

commit
go


