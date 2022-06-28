SELECT 
/**PagingIndexQuery**/
	DealerCompanyToDealer.ID,
	DealerCompanyToDealer.DealerCompanyID,
	DealerCompany.DealerCompanyName,
	DealerCompanyToDealer.DealerID,
	Dealer.DealerCode AS DealerCode,
	DealerCompanyToDealer.RowStatus,
	DealerCompanyToDealer.CreatedBy,
	DealerCompanyToDealer.CreatedTime,
	DealerCompanyToDealer.LastUpdateBy,
	DealerCompanyToDealer.LastUpdateTime
	/**EndPagingIndexQuery**/
FROM [DealerCompanyToDealer] AS DealerCompanyToDealer WITH (NOLOCK) 
JOIN DealerCompany AS DealerCompany WITH (NOLOCK) ON DealerCompany.ID = DealerCompanyToDealer.DealerCompanyID
JOIN Dealer as Dealer WITH (NOLOCK) ON Dealer.ID = DealerCompanyToDealer.DealerID
WHERE
	(@ID = 0 OR DealerCompany.ID = @ID) AND
	(@DealerCompanyID = '' OR DealerCompanyToDealer.DealerCompanyID = @DealerCompanyID) AND
	(@DealerID = '' OR DealerCompanyToDealer.DealerID LIKE @DealerID) AND
	DealerCompanyToDealer.RowStatus = 0