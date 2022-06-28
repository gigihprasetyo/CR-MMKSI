SELECT 
/**PagingIndexQuery**/
	COUNT (*)
	/**EndPagingIndexQuery**/
FROM DealerCompany AS DealerCompany WITH (NOLOCK) 
JOIN DealerGroup AS DealerGroup WITH (NOLOCK) ON DealerCompany.DealerGroupID = DealerGroup.ID
WHERE
	(@ID = 0 OR DealerCompany.ID = @ID) AND
	(@DealerCompanyCode = '' OR DealerCompany.DealerCompanyCode = @DealerCompanyCode) AND
	(@DealerCompanyName = '' OR DealerCompany.DealerCompanyName LIKE @DealerCompanyName) AND
	(@DealerGroupID = 0 OR DealerCompany.DealerGroupID = @DealerGroupID) AND
	DealerCompany.RowStatus = 0
	

