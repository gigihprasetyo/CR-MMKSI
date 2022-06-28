/****** Script for SelectTopNRows command from SSMS  ******/
SELECT ID,
	DealerCompanyName
  FROM [DealerCompany]
  WHERE RowStatus = 0
  AND (@DealerGroupID = 0 OR DealerGroupID = @DealerGroupID)