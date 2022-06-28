SELECT Status, COUNT(Status) Total FROM TransactionLog WITH(NOLOCK) 
WHERE CONVERT(DATE, CreatedTime) = @Date AND DealerCode IS NOT NULL AND (@DealerCode = '' OR DealerCode = @DealerCode)  
AND ( ParentId IS NULL OR ParentId = 0 )
GROUP BY Status