SELECT COUNT(*) Total FROM TransactionLog WITH(NOLOCK) 
WHERE CONVERT(DATE, CreatedTime) = @Date AND Status = 1 AND
DealerCode IS NOT NULL AND (@DealerCode = '' OR DealerCode = @DealerCode)
AND ParentId IS NOT NULL AND ParentId != 0 