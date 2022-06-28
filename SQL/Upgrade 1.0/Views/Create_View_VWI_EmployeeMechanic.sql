USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view dbo.VWI_EmployeeMechanic

as

SELECT a.[ID]

      ,a.[Name]

      ,D1.DealerCode

      ,a.[BirthDate]

      ,a.[Gender]

      ,a.[StartWorkingDate]

      ,a.[JobPosition]

      ,a.[EducationLevel]

      ,a.[Photo]

      ,a.[ShirtSize]

	  , Status = case when a.RowStatus = -1 then a.RowStatus else case when a.Status <> 1 then -1 else 0 end end

      ,a.[LastUpdateTime]

  FROM [TrTrainee] a with (nolock)

  JOIN Dealer D1 with (nolock) ON a.DealerID = D1.ID and D1.RowStatus = 0

  WHERE JobPosition LIKE '%Mekanik%'

  GO