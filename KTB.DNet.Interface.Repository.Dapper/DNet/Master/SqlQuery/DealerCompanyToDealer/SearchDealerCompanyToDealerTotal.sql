SELECT 
/**PagingIndexQuery**/
	COUNT (*)
	/**EndPagingIndexQuery**/
FROM [DealerCompanyToDealer] AS DealerCompanyToDealer WITH (NOLOCK) 
JOIN DealerCompany AS DealerCompany WITH (NOLOCK) ON DealerCompany.ID = DealerCompanyToDealer.DealerCompanyID
JOIN Dealer as Dealer WITH (NOLOCK) ON Dealer.ID = DealerCompanyToDealer.DealerID
WHERE
	(@ID = 0 OR DealerCompany.ID = @ID) AND
	(@DealerCompanyID = '' OR DealerCompanyToDealer.DealerCompanyID = @DealerCompanyID) AND
	(@DealerID = '' OR DealerCompanyToDealer.DealerID LIKE @DealerID) AND
	DealerCompanyToDealer.RowStatus = 0
	

