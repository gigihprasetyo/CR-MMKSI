SELECT /**PagingIndexQuery**/ * /**EndPagingIndexQuery**/  FROM (SELECT Endpoint, COUNT(Id) AS Total FROM TransactionLog WITH(NOLOCK) 
GROUP BY Endpoint) TopRanked