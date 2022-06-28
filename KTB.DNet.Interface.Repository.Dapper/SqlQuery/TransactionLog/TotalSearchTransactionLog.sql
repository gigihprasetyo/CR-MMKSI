SELECT 
	COUNT(*)
FROM TransactionLog WITH(NOLOCK)
WHERE
	(@Id = -1 OR Id = @Id) AND 
	(@StartDate = '' OR CONVERT(DATE, CreatedTime) >= @StartDate) AND
	(@EndDate = '' OR CONVERT(DATE, CreatedTime) <= @EndDate) AND
	(   @IncludeAPIRead = 1 OR (@IncludeAPIRead = 0 AND 
		EndPoint NOT LIKE '%Read' AND 
		EndPoint NOT LIKE '%GetPRHistorySO' 
		AND EndPoint NOT LIKE '%PRHistoryIndentStatusCancel') 
    ) AND
	(@AppId = '00000000-0000-0000-0000-000000000000' OR AppId = @AppId) AND 
	(@ClientId = '00000000-0000-0000-0000-000000000000' OR ClientId = @ClientId) AND
	(@DealerCode = '' OR DealerCode = @DealerCode) AND
	(@CreatedBy = '' OR CreatedBy LIKE '%' + @CreatedBy + '%') AND
	(@UserName = '' OR UserName LIKE '%' + @UserName + '%') AND
	(@Endpoint = '' OR Endpoint LIKE '%'+@Endpoint+'%') AND
	(@Input = '' OR Input LIKE '%'+@Input+'%') AND 
	(@Output = '' OR Output LIKE '%'+@Output+'%') AND
	(@SenderIP = '' OR SenderIP LIKE '%'+@SenderIP+'%') AND
	(@Status = -1 OR Status = @Status) AND
	(ParentId IS NULL OR ParentId = 0)
 