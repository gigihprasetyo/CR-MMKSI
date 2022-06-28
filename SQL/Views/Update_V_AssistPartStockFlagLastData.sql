/****** Object:  View [dbo].[V_AssistPartStockFlagLastData]    Script Date: 23/10/2018 11:41:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
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
		A.DealerBranchCode ,
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
INNER JOIN AssistUploadLog B (NOLOCK)  ON A.AssistUploadLogID = B.ID
LEFT JOIN ( SELECT  C.DealerID ,
                    C.[Month] ,
                    C.[Year] ,
                    NoParts ,
                    MAX(D.UploadTime) AS MaxDate
            FROM    AssistPartStock C (NOLOCK) 
            INNER JOIN AssistUploadLog D (NOLOCK)  ON C.AssistUploadLogID = D.ID
            WHERE   C.StatusAktif = 1 --Not duplicate data
                    AND D.ValidateStatus = 5 --Konfirmasi MMKSI
            GROUP BY C.DealerID ,
                    C.[Month] ,
                    C.[Year] ,
                    NoParts
          ) LastData ON LastData.DealerID = A.DealerID
                        AND A.[Month] = LastData.[Month]
                        AND A.[Year] = LastData.[Year]
                        AND A.NoParts = LastData.NoParts
                        AND B.UploadTime = LastData.MaxDate


GO


