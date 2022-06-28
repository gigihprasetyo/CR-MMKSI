SELECT DealerCode, Status, COUNT(Status) AS Total FROM TransactionLog WITH(NOLOCK) 
WHERE CONVERT(DATE, CreatedTime) = @Date AND
DealerCode IS NOT NULL AND ( ParentId IS NULL OR ParentId = 0 )
GROUP BY DealerCode, Status
