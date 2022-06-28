SELECT DealerCode, COUNT(Id) AS Total FROM TransactionLog WITH(NOLOCK)
WHERE CONVERT(DATE, CreatedTime) = @Date AND Status = 0 AND
DealerCode IS NOT NULL AND (@DealerCode = '' OR DealerCode = @DealerCode) AND ( ParentId IS NULL OR ParentId = 0 )
GROUP BY DealerCode